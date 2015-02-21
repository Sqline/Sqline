// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System;

namespace T4Compiler.Generator {
	public class TemplateProcessor {

		public TemplateProcessor() {
		}

		public void Process(string file) {
			Process(file, file, null);
		}

		public void Process(string file, string outputfile, IDictionary<string, object> parameters) {
			Process(file, outputfile, parameters, new TemplateOptions());
		}

		public void Process(string file, string outputfile, IDictionary<string, object> parameters, TemplateOptions options) {
			Stopwatch OWatch = Stopwatch.StartNew();
			Template OTemplate = new Template(file);
			AssignParameters(OTemplate, parameters);
			try {
				OTemplate.Process();
				string OContent = OTemplate.InvokeTemplate();
				//TODO: Split result from generation based on file marker for multi-file output
				//TODO: Support writing multiple files and handle output extension <%@ Output extension=".vb">
				//TODO: Save Hash output for last run and compare to this run to not touch files that have not been changed
				string OOutputFile = Path.GetFullPath(outputfile + OTemplate.Extension);
				WriteToFile(OOutputFile, OContent, OTemplate.Encoding);
			}
			finally {
				if (OTemplate.Debug) {
					string OSourceFile = Path.GetFullPath(file + "_source" + OTemplate.Extension);
					WriteToFile(OSourceFile, OTemplate.GeneratedSourceCode, Encoding.UTF8);
				}
			}
			System.Console.WriteLine("Time: " + OWatch.ElapsedMilliseconds);
		}

		private void AssignParameters(Template template, IDictionary<string, object> parameters) {
			if (parameters != null) {
				foreach (KeyValuePair<string, object> OParameter in parameters) {
					template.Parameters.Add(OParameter.Key, OParameter.Value);
				}
			}
		}

		private void WriteToFile(string filename, string content, Encoding encoding) {
			using (StreamWriter OWriter = new StreamWriter(filename, false, encoding)) {
				OWriter.Write(content);
			}
		}
	}
}