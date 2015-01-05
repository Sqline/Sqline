// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
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
		private List<KeyValuePair<string, string>> FParameters = new List<KeyValuePair<string,string>>();
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
				if (OParam.StartsWith("/p:", StringComparison.OrdinalIgnoreCase) || OParam.StartsWith("/parameters:", StringComparison.OrdinalIgnoreCase)) {
					string OParameters = OParam[2] == ':' ? OParam.Remove(0, 3) : OParam.Remove(0, 12);
					ParseParameters(OParameters);
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

		private void ParseParameters(string parameters) {
			string[] OParameters = parameters.Split(';');
			foreach (string OParameter in OParameters) {
				string[] OPair = OParameter.Split('=');
				if (OPair.Length >= 2) {
					FParameters.Add(new KeyValuePair<string, string>(OPair[0], OPair[1]));
				}
			}
		}

		public List<string> SourceFiles {
			get {
				return FSourceFiles;
			}
		}

		public List<KeyValuePair<string, string>> Parameters {
			get {
				return FParameters;
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