// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using EnvDTE;
using System.IO;
using T4Compiler.Generator;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Sqline.Base;

namespace Sqline.VSPackage {
	internal class ItemFileGenerator {
		private AddinContext FContext;
		private Document FDocument;
		private FileInfo FFileInfo;
		private List<string> FOutputFiles = new List<string>();

		public ItemFileGenerator(AddinContext context, Document document) {
			FContext = context;
			FDocument = document;
			FFileInfo = new FileInfo(FDocument.FullName).GetCorrectlyCasedFileInfo();
		}

		public void Generate() {
			GenerateViewItems();
			GenerateMethods();
		}

		public string ProjectDir {
			get {
				return new FileInfo(FDocument.ProjectItem.ContainingProject.FullName).DirectoryName;
			}
		}

		private void GenerateViewItems() {
			string OTemplatePath = FContext.ResolvePath("/Templates/ViewItem.t4");
			Debug.WriteLine("GenerateViewItems: " + OTemplatePath);
			TemplateOptions OOptions = new TemplateOptions { RemoveWhitespaceStatementLines = true, AssemblyResolveDirectory = FContext.PackageDirectory };
			Template OTemplate = new Template(OTemplatePath, OOptions);
			OTemplate.Parameters.Add("Filename", OTemplatePath);
			OTemplate.Parameters.Add("ItemFilename", FDocument.FullName);
			OTemplate.Parameters.Add("ProjectDir", ProjectDir);
			//TODO: Add API object to pass information back to VSPlugin (Info, Warnings, Errors)
			try {
				OTemplate.Process();
				string OContent = OTemplate.InvokeTemplate();
				string OOutputFile = Path.GetFullPath(Directory + "/" + FileNameWithoutExtension + OTemplate.Extension);
				WriteToFile(OOutputFile, OContent, OTemplate.Encoding);
				FOutputFiles.Add(OOutputFile);
			}
			finally {
				if (OTemplate.Debug) {
					string OSourceFile = Path.GetFullPath(Directory + "/" + FileNameWithoutExtension + ".ViewItemSource" + OTemplate.Extension);
					WriteToFile(OSourceFile, OTemplate.GeneratedSourceCode, Encoding.UTF8);
					FOutputFiles.Add(OSourceFile);
				}
			}
		}

		private void GenerateMethods() {
			string OTemplatePath = FContext.ResolvePath("/Templates/ItemMethods.t4");
			Debug.WriteLine("GenerateViewItems: " + OTemplatePath);
			TemplateOptions OOptions = new TemplateOptions { RemoveWhitespaceStatementLines = true, AssemblyResolveDirectory = FContext.PackageDirectory };
			Template OTemplate = new Template(OTemplatePath, OOptions);
			OTemplate.Parameters.Add("Filename", OTemplatePath);
			OTemplate.Parameters.Add("ItemFilename", FDocument.FullName);
			OTemplate.Parameters.Add("ProjectDir", ProjectDir);
			try {
				OTemplate.Process();
				string OContent = OTemplate.InvokeTemplate();
				string OOutputFile = Path.GetFullPath(Directory + "/" + FileNameWithoutExtension + OTemplate.Extension);
				WriteToFile(OOutputFile, OContent, OTemplate.Encoding);
				FOutputFiles.Add(OOutputFile);
			}
			finally {
				if (OTemplate.Debug) {
					string OSourceFile = Path.GetFullPath(Directory + "/" + FileNameWithoutExtension + ".ViewMethodsSource" + OTemplate.Extension);
					WriteToFile(OSourceFile, OTemplate.GeneratedSourceCode, Encoding.UTF8);
				}
			}
		}

		private void WriteToFile(string filename, string content, Encoding encoding) {
			using (StreamWriter OWriter = new StreamWriter(filename, false, encoding)) {
				OWriter.Write(content);
			}
		}

		public string Directory {
			get {
				return FFileInfo.DirectoryName;
			}
		}

		public string FileNameWithoutExtension {
			get {
				return FFileInfo.GetNameWithoutExt();
			}
		}

		public List<string> OutputFiles {
			get {
				return FOutputFiles;
			}
		}
	}
}