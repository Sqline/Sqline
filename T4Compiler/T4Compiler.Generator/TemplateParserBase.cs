// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Text;

namespace T4Compiler.Generator {

	public abstract class TemplateParserBase {
		protected IDictionary<string, object> FParameters = new Dictionary<string, object>();
		protected List<ParameterDirective> FParameterDirectives = new List<ParameterDirective>();
		protected List<string> FUsings = new List<string>();
		protected List<string> FImports = new List<string>();
		protected string FFilename = "";
		protected string FClassName = "CodeTemplate";
		protected string FNamespace = "";
		protected string FParamType = "object";
		protected string FExtension = ".cs";
		protected int FTemplateLine = 0;
		protected bool FLinePragmas = false;
		protected Encoding FEncoding = Encoding.UTF8;
		protected bool FDebug;

		public TemplateParserBase(string filename, string uniqueName) {
			FFilename = filename;
			FNamespace = "Sqline.CodeTemplate.Template" + uniqueName;
			FClassName = "CodeTemplate";
		}

		public abstract string Parse(string content);

		protected string ExtractParam(string text, string name) {
			int OIndex = text.IndexOf(name, StringComparison.OrdinalIgnoreCase);
			if (OIndex == -1) {
				return null;
			}
			OIndex = text.IndexOf("\"", OIndex) + 1;
			int OIndex2 = text.IndexOf("\"", OIndex);
			return text.Substring(OIndex, OIndex2 - OIndex).Trim();
		}

		protected void WriteLine(StringBuilder builder, string text, bool writeDebugLine) {
			if (writeDebugLine) {
				WriteDebugLine(builder);
			}
			builder.AppendLine(text);
		}

		protected void WriteDebugLine(StringBuilder builder) {
			if (FLinePragmas) {
				builder.AppendLine("#line " + FTemplateLine + " \"" + FFilename + "\"");
			}
		}

		protected string EscapeString(string text) {
			return text.Replace("\"", "\"\"");
		}

		public void SetParameters(IDictionary<string, object> parameters) {
			FParameters = parameters;
		}

		protected void AppendParameterMethod(StringBuilder builder) {
			builder.AppendLine("public void SetParameter(string name, object value) {");
			foreach (ParameterDirective OParameter in FParameterDirectives) {
				builder.AppendLine("  if (name == \"" + OParameter.Name + "\") {");
				builder.AppendLine("    " + OParameter.Name + " = (" + OParameter.Type + ")value;");
				builder.AppendLine("  }");
			}
			builder.AppendLine("}");
		}

		public Encoding Encoding {
			get {
				return FEncoding;
			}
		}

		public string Extension {
			get {
				return FExtension;
			}
		}

		public string ClassName {
			get {
				return FClassName;
			}
		}

		public string FullClassName {
			get {
				return FNamespace + "." + FClassName;
			}
		}

		public string Filename {
			get {
				return FFilename;
			}
		}

		public List<string> Imports {
			get {
				return FImports;
			}
		}

		public bool Debug {
			get {
				return FDebug;
			}
		}
	}
}