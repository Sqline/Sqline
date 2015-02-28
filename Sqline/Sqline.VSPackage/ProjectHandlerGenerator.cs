// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using EnvDTE;
using System.IO;
using T4Compiler.Generator;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Schemalizer.Model;
using Schemalizer.Provider.SqlServer;
using Schemalizer.Provider;
using System.Xml.Linq;
using ConfigModel = Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.VSPackage {
	internal class ProjectHandlerGenerator {
		private AddinContext FContext;
		private Project FProject;
		private List<string> FOutputFiles = new List<string>();

		public ProjectHandlerGenerator(AddinContext context, Project project) {
			FContext = context;
			FProject = project;
		}

		public void Generate() {
			string OTemplatePath = FContext.ResolvePath("/Templates/ProjectHandler.t4");
			Debug.WriteLine("GenerateProjectHandler: " + OTemplatePath);
			TemplateOptions OOptions = new TemplateOptions { RemoveWhitespaceStatementLines = true, AssemblyResolveDirectory = FContext.PackageDirectory };
			Template OTemplate = new Template(OTemplatePath, OOptions);
			OTemplate.Parameters.Add("Filename", OTemplatePath);
			OTemplate.Parameters.Add("ProjectDir", ProjectDir);
			try {
				OTemplate.Process();
				string OContent = OTemplate.InvokeTemplate();
				string OOutputFile = Path.GetFullPath(ProjectDir + "/Sqline.Handler" + OTemplate.Extension);
				WriteToFile(OOutputFile, OContent, OTemplate.Encoding);
				FOutputFiles.Add(OOutputFile);
			}
			finally {
				if (OTemplate.Debug) {
					string OSourceFile = Path.GetFullPath(ProjectDir + "/Sqline.Handler" + ".Source" + OTemplate.Extension);
					WriteToFile(OSourceFile, OTemplate.GeneratedSourceCode, Encoding.UTF8);
				}
			}
		}

		public string ProjectDir {
			get {
				return new FileInfo(FProject.FullName).DirectoryName;
			}
		}

		private void WriteToFile(string filename, string content, Encoding encoding) {
			using (StreamWriter OWriter = new StreamWriter(filename, false, encoding)) {
				OWriter.Write(content);
			}
		}

		public List<string> OutputFiles {
			get {
				return FOutputFiles;
			}
		}
	}
}