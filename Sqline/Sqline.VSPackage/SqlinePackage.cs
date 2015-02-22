// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE80;
using EnvDTE;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sqline.VSPackage {
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	[Guid(GuidList.guidSqlinePkgString)]
	[ProvideAutoLoad("{f1536ef8-92ec-443c-9ed7-fdadf150da82}")]
	[ProvideAutoLoad("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}")]
	public sealed class SqlinePackage : Package {
		private AddinContext FContext;
		private DocumentEvents FDocumentEvents;
		private LogWindow FLog;

		public SqlinePackage() {
		}

		protected override void Initialize() {
			base.Initialize();
			FContext = new AddinContext((DTE2)GetService(typeof(SDTE)), this);
			FLog = new LogWindow(this, FContext);
			FDocumentEvents = Context.Application.Events.get_DocumentEvents(null);
			FDocumentEvents.DocumentSaved += OnDocumentSaved;
			FContext.Application.Events.BuildEvents.OnBuildBegin += OnBuildBegin;
		}

		private void OnBuildBegin(vsBuildScope Scope, vsBuildAction Action) {
			FLog.Clear();
			try {
				List<Project> OProjects = new List<Project>();
				FindSqlineProjects(OProjects);
				foreach (Project OProject in OProjects) {
					FLog.SetProject(OProject);
					GenerateDataItems(OProject);
					GenerateProjectHandler(OProject);
				}
			}
			catch (Exception ex) {
				FLog.Add(ex);
			}
			FLog.UpdateView();
		}

		private void GenerateDataItems(Project project) {
			DataItemGenerator OGenerator = new DataItemGenerator(Context, project);
			OGenerator.Generate();
			foreach (string OFile in OGenerator.OutputFiles) {
				ProjectItem OItem = project.ProjectItems.AddFromFile(OFile);
			}
		}

		private void GenerateProjectHandler(Project project) {
			ProjectHandlerGenerator OProjectHandlerGenerator = new ProjectHandlerGenerator(Context, project);
			OProjectHandlerGenerator.Generate();
			foreach (ProjectItem OProjectItem in project.ProjectItems) {
				if (OProjectItem.Name.Equals("sqline.config", StringComparison.OrdinalIgnoreCase)) {
					foreach (string OFile in OProjectHandlerGenerator.OutputFiles) {
						ProjectItem OItem = OProjectItem.ProjectItems.AddFromFile(OFile);
					}
				}
			}
		}

		private void FindSqlineProjects(List<Project> result) {
			foreach (Project OProject in FContext.Application.Solution.Projects) {
				FindSqlineProjects(OProject, result);
			}
		}

		private void FindSqlineProjects(Project project, List<Project> result) {
			if (project == null) {
				return;
			}
			if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder) {
				foreach (ProjectItem OProjectItem in project.ProjectItems) {
					FindSqlineProjects(OProjectItem.SubProject, result);
				}
			}
			else {
				if (project.ProjectItems == null) {
					return;
				}
				foreach (ProjectItem OProjectItem in project.ProjectItems) {
					if (OProjectItem.Name.Equals("sqline.config", StringComparison.OrdinalIgnoreCase)) {
						result.Add(project);
					}
				}
			}
		}	

		private void OnDocumentSaved(Document document) {
			FLog.Clear();
			try {
				if (document.FullName.EndsWith(".items")) {
					FLog.SetProject(document.ProjectItem.ContainingProject);
					ItemFileGenerator OGenerator = new ItemFileGenerator(Context, document);
					OGenerator.Generate();
					foreach (string OFile in OGenerator.OutputFiles) {
						ProjectItem OItem = document.ProjectItem.ProjectItems.AddFromFile(OFile);
					}
				}
			}
			catch (Exception ex) {
				FLog.Add(ex);
			}
			FLog.UpdateView();
		}

		internal AddinContext Context {
			get {
				return FContext;
			}
		}
	}
}
