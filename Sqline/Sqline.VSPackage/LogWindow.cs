using System;
using System.Collections.Generic;
using System.Diagnostics;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Sqline.Sqline_VSPackage;

namespace Sqline.VSPackage {
	public class LogWindow {
		private Package FPackage;
		private AddinContext FContext;
		private OutputWindowPane FWindow;
		private ErrorListProvider FProvider;
		private Project FProject;
		private IVsSolution FVsSolution;
		private IVsHierarchy FvsHierarchy;
		private List<LogEntry> FEntries = new List<LogEntry>();

		public LogWindow(Package package, AddinContext context) {
			FPackage = package;
			FContext = context;
			FWindow = context.Application.ToolWindows.OutputWindow.OutputWindowPanes.Item("Build");
			FProvider = new ErrorListProvider(FPackage);
			FProvider.ProviderGuid = GuidList.guidSqlinePkg;
			FProvider.ProviderName = "Sqline";
			FVsSolution = (IVsSolution)Package.GetGlobalService(typeof(IVsSolution));
		}

		public void SetProject(Project project) {
			FProject = project;
			FVsSolution.GetProjectOfUniqueName(FProject.FullName, out FvsHierarchy);
		}

		public void Add(LogEntry entry) {
			FEntries.Add(entry);
		}

		public void Add(Exception ex) {
			FEntries.Add(new LogEntry(ex));
		}

		public void UpdateView() {
			foreach (LogEntry entry in FEntries) {
				Task OTask = entry.GetTask(FvsHierarchy);
				OTask.Navigate += OTask_Navigate;
				FProvider.Tasks.Add(OTask);
			}
		}

		void OTask_Navigate(object sender, EventArgs e) {
			Task OTask = (Task)sender;
			OTask.Line++;
			OTask.Column++;
			FProvider.Navigate(OTask, new Guid(EnvDTE.Constants.vsViewKindCode));
			OTask.Line--;
			OTask.Column--;
		}

		public void Clear() {
			FWindow.Clear();
			FEntries.Clear();
			FProvider.Tasks.Clear();
		}
	}
}