// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using EnvDTE;
using System.IO;
using T4Compiler.Generator;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Schemalizer.Model;
using Schemalizer.ProviderModel.SqlServer;
using Schemalizer.ProviderModel;
using System.Xml.Linq;
using ConfigModel = Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.VSPackage {
	internal class DataItemGenerator {
		private AddinContext FContext;
		private SqlineProject FProject;
		private List<string> FOutputFiles = new List<string>();

		public DataItemGenerator(AddinContext context, SqlineProject project) {
			FContext = context;
			FProject = project;
		}

		public void Generate() {
			string ODatabaseFilePath = ProcessShemaInfo();
			GenerateDataItems(ODatabaseFilePath);
		}

		private string ProcessShemaInfo() {			
			string ODatabaseFilePath = Path.Combine(FProject.ProjectDir, "Database.xdml");
			SchemaModel OModel = SchemaModel.Load(ODatabaseFilePath);
			Debug.WriteLine("DataItemGenerator::Loaded Database Model Successfully");
			ISchemalizerProvider OProvider = ProviderFactory.Create(FProject.Configuration.ConnectionString.Provider);
			Debug.WriteLine("DataItemGenerator::Created Provider Successfully");
			OProvider.ConnectionString = FProject.Configuration.ConnectionString.Value;
			if (OModel == null || OProvider.HasSchemaChanged(OModel)) {
				OModel = new SchemaModel();
				Debug.WriteLine("DataItemGenerator::Extracting Metadata");
				OProvider.ExtractMetadata(OModel, "Sqline");
				Debug.WriteLine("DataItemGenerator::Extracting Metadata Successfully");
				XElement OElement = OModel.ToXElement();
				OElement.Save(ODatabaseFilePath);
				FOutputFiles.Add(ODatabaseFilePath);
			}
			return ODatabaseFilePath;
		}

		private void GenerateDataItems(string databaseFilePath) {
			string OTemplatePath = FContext.ResolvePath("/Templates/DataItem.t4");
			Debug.WriteLine("GenerateDataItems: " + OTemplatePath);
			TemplateOptions OOptions = new TemplateOptions { RemoveWhitespaceStatementLines = true, AssemblyResolveDirectory = FContext.PackageDirectory };
			Template OTemplate = new Template(OTemplatePath, OOptions);
			OTemplate.Parameters.Add("Filename", OTemplatePath);
			OTemplate.Parameters.Add("ProjectDir", FProject.ProjectDir);
			OTemplate.Parameters.Add("DatabaseFilePath", databaseFilePath);
			try {
				OTemplate.Process();
				string OContent = OTemplate.InvokeTemplate();
				string OOutputFile = Path.GetFullPath(FProject.ProjectDir + "/" + "DataItems" + OTemplate.Extension);
				WriteToFile(OOutputFile, OContent, OTemplate.Encoding);
				FOutputFiles.Add(OOutputFile);
			}
			finally {
				if (OTemplate.Debug) {
					string OSourceFile = Path.GetFullPath(FProject.ProjectDir + "/" + "DataItems" + ".DataItemSource" + OTemplate.Extension);
					WriteToFile(OSourceFile, OTemplate.GeneratedSourceCode, Encoding.UTF8);
				}
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