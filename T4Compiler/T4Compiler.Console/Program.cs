// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"	
using System.Collections.Generic;
using System.IO;
using T4Compiler.Generator;

namespace T4Compiler.ConsoleApp {
	public class Program {
		public static void Main(string[] args) {
			CmdArguments OArguments = new CmdArguments(args);
			TemplateProcessor OProcessor = new TemplateProcessor();
			for (int i = 0; i < OArguments.SourceFiles.Count; i++) {
				string OFile = (string)OArguments.SourceFiles[i];
				string OFullPath = Path.GetFullPath(OFile);
				Dictionary<string, object> OParameters = new Dictionary<string, object>();
				OParameters.Add("Filename", OFullPath);
				foreach (KeyValuePair<string, string> OPair in OArguments.Parameters) {
					OParameters.Add(OPair.Key, OPair.Value);
				}
				OProcessor.Process(OFullPath, OArguments.OutputFile, OParameters);
			}
		}
	}
}
