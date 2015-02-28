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
	internal class DataItemGenerator {
		private AddinContext FContext;
		private Project FProject;
		private List<string> FOutputFiles = new List<string>();

		public DataItemGenerator(AddinContext context, Project project) {
			FContext = context;
			FProject = project;
		}

		public void Generate() {
			String ODatabaseFilePath = ProcessShemaInfo();
			GenerateDataItems(ODatabaseFilePath);
		}

		private String ProcessShemaInfo() {
			ConfigModel.ConfigurationSystem OConfigurationSystem = new ConfigModel.ConfigurationSystem(ProjectDir);
			ConfigModel.Configuration OConfiguration = OConfigurationSystem.GetConfigurationFor(ProjectDir);
			String ODatabaseFilePath = Path.Combine(ProjectDir, "Database.xdml");
			SchemaModel OModel = SchemaModel.Load(ODatabaseFilePath);
			ISchemalizerProvider OProvider = new SqlProvider(OConfiguration.ConnectionString.Value);
			if (OModel == null || OProvider.HasSchemaChanged(OModel)) {
				OModel = new SchemaModel();
				OProvider.ExtractMetadata(OModel, "Sqline");
				XElement OElement = OModel.ToXElement();
				OElement.Save(ODatabaseFilePath);
				FOutputFiles.Add(ODatabaseFilePath);
			}
			return ODatabaseFilePath;
		}

		private void GenerateDataItems(String databaseFilePath) {
			string OTemplatePath = FContext.ResolvePath("/Templates/DataItem.t4");
			Debug.WriteLine("GenerateDataItems: " + OTemplatePath);
			TemplateOptions OOptions = new TemplateOptions { RemoveWhitespaceStatementLines = true, AssemblyResolveDirectory = FContext.PackageDirectory };
			Template OTemplate = new Template(OTemplatePath, OOptions);
			OTemplate.Parameters.Add("Filename", OTemplatePath);
			OTemplate.Parameters.Add("ProjectDir", ProjectDir);
			OTemplate.Parameters.Add("DatabaseFilePath", databaseFilePath);
			try {
				OTemplate.Process();
				string OContent = OTemplate.InvokeTemplate();
				string OOutputFile = Path.GetFullPath(ProjectDir + "/" + "DataItems" + OTemplate.Extension);
				WriteToFile(OOutputFile, OContent, OTemplate.Encoding);
				FOutputFiles.Add(OOutputFile);
			}
			finally {
				if (OTemplate.Debug) {
					string OSourceFile = Path.GetFullPath(ProjectDir + "/" + "DataItems" + ".DataItemSource" + OTemplate.Extension);
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