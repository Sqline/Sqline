// Authors="Daniel Jonas M�ller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.Collections.Generic;

namespace T4Compiler.ConsoleApp {
	public class CmdArguments {
		private List<string> FSourceFiles = new List<string>();
		private bool FDebug;
		private string FOutputFile;
	
		public CmdArguments(string[] args) {
			//.exe /debug /p[arameters]:Param1=value;Param2=value /output:D:\folder *.tt
			foreach (string OParam in args) {
				if (OParam.Equals("/debug", StringComparison.OrdinalIgnoreCase)) {
					FDebug = true;
					continue;
				}
				if (OParam.StartsWith("/output:", StringComparison.OrdinalIgnoreCase)) {
					string OPath = OParam.Remove(0, 8);
					//TODO: Check if its a valid file path
					FOutputFile = OPath;
					continue;
				}
				if (OParam.Contains("*") || OParam.Contains("?")) {
					string OParam2 = OParam.Replace("*", "#A#a#B#b#").Replace("?", "#A#a#2#B#b#");
					string OFullPath = Path.GetFullPath(OParam2);
					FSourceFiles.AddRange(Directory.GetFiles(Path.GetDirectoryName(OFullPath), Path.GetFileName(OFullPath).Replace("#A#a#B#b#", "*").Replace("#A#a#2#B#b#", "?")));
				}
				else {
					FSourceFiles.Add(OParam);
				}
			}
		}

		public List<string> SourceFiles {
			get {
				return FSourceFiles;
			}
		}

		public bool Debug {
			get {
				return FDebug;
			}
			set {
				FDebug = value;
			}
		}

		public string OutputFile {
			get {
				return FOutputFile;
			}
			set {
				FOutputFile = value;
			}
		}
	}	
}