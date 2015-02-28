// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.IO;
using System.Text;

namespace T4Compiler.Generator {

	public class TemplateParser : TemplateParserBase {
		private enum BlockType { Text, DirectiveStart, DirectiveEnd, StatementStart, StatementEnd, OutputStart, OutputEnd, ClassFeatureStart, ClassFeatureEnd }
		private TemplateOptions FOptions;
		private StringBuilder FTemplateHeader = new StringBuilder();
		private StringBuilder FTemplateBody = new StringBuilder();
		private StringBuilder FTemplateMethods = new StringBuilder();
		private string FData = "";
		private int i = 0;
		private int FStart = -1;
		private int FTextStart = 0;
		private int FTextEnd = 0;
		private bool FEndsWithClassFeature = false;
		private bool FClassFeaturePresent = false;
		private BlockType FLastBlockType = BlockType.Text;
		private BlockType FCurrentBlockType = BlockType.Text;

		public TemplateParser(string filename, string uniqueName, TemplateOptions options) : base(filename, uniqueName) {
			FOptions = options;
		}
		
		public override string Parse(string content) {
			FData = content;
			ParseBody();
			AppendParameterMethod(FTemplateMethods);
			FTemplateHeader.AppendLine("using System;");
			FTemplateHeader.AppendLine("using System.Text;");
			FTemplateHeader.AppendLine("using System.Linq;");
			FTemplateHeader.AppendLine("using System.Xml.Linq;");
			FTemplateHeader.AppendLine("using T4Compiler.Generator;");
			foreach (string OUsing in FUsings) {
				FTemplateHeader.AppendLine("using " + OUsing + ";");
			}
			FTemplateHeader.AppendLine("namespace " + FNamespace + " {");
			FTemplateHeader.AppendLine("public class " + FClassName + " : ICodeTemplate {");
			FTemplateHeader.AppendLine("private StringBuilder FStringOutput = new StringBuilder();");
			FTemplateHeader.AppendLine("public string Generate() {");

			FTemplateBody.AppendLine("return FStringOutput.ToString();");
			FTemplateBody.AppendLine("}");
			FTemplateBody.AppendLine("private void Write(object obj) { FStringOutput.Append(obj); }");
			FTemplateBody.Append(FTemplateMethods);
			FTemplateBody.AppendLine("}");
			FTemplateBody.AppendLine("}");
			FTemplateBody.Insert(0, FTemplateHeader);
			return FTemplateBody.ToString();
		}

		private void ParseBody() {
			while (i < FData.Length) {
				if (FData[i] == '\n') {
					FTemplateLine++;
					if (FCurrentBlockType == BlockType.Text) {
						if (FLastBlockType != BlockType.DirectiveEnd && FLastBlockType != BlockType.ClassFeatureEnd) {
							string OText = FData.Substring(FTextStart, i - FTextStart + 1);
							if (!FOptions.RemoveWhitespaceStatementLines || FLastBlockType != BlockType.StatementEnd || OText.Trim() != "") {
								WriteLine(Builder, "Write(@\"" + EscapeString(OText) + "\");", false);
							}
						}
						WriteDebugLine(Builder);
						FTextStart = i + 1;
					}
				}
				if (FCurrentBlockType == BlockType.Text && Matches("<#")) {
					i = i+2;
					HandleCodeTagBegin();
				}
				else if (FCurrentBlockType != BlockType.Text && Matches("#>")) {
					i++;
					HandleCodeTagEnd();
				}
				i++;
			}
			ValidateParserState();
			WriteLine(FTemplateBody, "Write(@\"" + EscapeString(FData.Substring(FTextStart)) + "\");", true);
		}

		private void HandleCodeTagBegin() {
			FLastBlockType = BlockType.Text;
			if (FData[i] == '=') {
				FStart = i + 1;
				FTextEnd = i - 2;
				FCurrentBlockType = BlockType.OutputStart;
				FEndsWithClassFeature = false;
			}
			else if (FData[i] == '@') {
				FStart = i + 1;
				FTextEnd = i - 2;
				FCurrentBlockType = BlockType.DirectiveStart;
				FEndsWithClassFeature = false;
			}
			else if (FData[i] == '+') {
				FStart = i + 1;
				FTextEnd = i - 2;
				FCurrentBlockType = BlockType.ClassFeatureStart;
				FClassFeaturePresent = true;
				FEndsWithClassFeature = true;
			}
			else {
				FStart = i;
				FTextEnd = i - 2;
				FCurrentBlockType = BlockType.StatementStart;
				FEndsWithClassFeature = false;
			}
		}

		private void HandleCodeTagEnd() {
			if (FCurrentBlockType == BlockType.OutputStart) {
				WriteLine(Builder, "Write(@\"" + EscapeString(FData.Substring(FTextStart, FTextEnd - FTextStart)) + "\");", true);
				WriteLine(Builder, "Write(" + FData.Substring(FStart, i - FStart - 1).Trim() + ");", true);
				FTextStart = i + 1;
				FLastBlockType = BlockType.OutputEnd;
			}
			else if (FCurrentBlockType == BlockType.StatementStart) {
				string OText = FData.Substring(FTextStart, FTextEnd - FTextStart);
				if (!FOptions.RemoveWhitespaceStatementLines || OText.Trim() != "") {
					WriteLine(Builder, "Write(@\"" + EscapeString(OText) + "\");", true);
				}
				WriteLine(Builder, FData.Substring(FStart, i - FStart - 1).Trim(), true);
				FTextStart = i + 1;
				FLastBlockType = BlockType.StatementEnd;
			}
			else if (FCurrentBlockType == BlockType.DirectiveStart) {
				string OTagContent = FData.Substring(FStart, i - FStart - 1).Trim();
				if (OTagContent.StartsWith("template", StringComparison.OrdinalIgnoreCase)) {
					TemplateDirective ODirective = new TemplateDirective(OTagContent);
					FDebug = ODirective.Debug;
					FLinePragmas = ODirective.LinePragmas;
				}
				if (OTagContent.StartsWithIC("import")) {
					ImportDirective ODirective = new ImportDirective(OTagContent);
					FUsings.Add(ODirective.Namespace);
				}
				if (OTagContent.StartsWithIC("include")) {
					IncludeDirective ODirective = new IncludeDirective(OTagContent);
					string OFileContent = ReadFile(ODirective);
					FData = FData.Insert(i + 1, OFileContent);
				}
				if (OTagContent.StartsWithIC("assembly")) {
					AssemblyDirective ODirective = new AssemblyDirective(this, OTagContent);
					FImports.Add(ODirective.FullName);
				}
				if (OTagContent.StartsWithIC("output")) {
					OutputDirective ODirective = new OutputDirective(OTagContent);
					FEncoding = ODirective.Encoding ?? FEncoding;
					FExtension = ODirective.Extension ?? FExtension;
				}
				if (OTagContent.StartsWithIC("parameter")) {
					ParameterDirective ODirective = new ParameterDirective(OTagContent);
					FParameterDirectives.Add(ODirective);
					WriteLine(FTemplateMethods, "\r\npublic " + ODirective.Type + " " + ODirective.Name + " { get; set; }", true);
				}
				FTextStart = i + 1;
				FLastBlockType = BlockType.DirectiveEnd;
			}
			else if (FCurrentBlockType == BlockType.ClassFeatureStart) {
				WriteLine(Builder, FData.Substring(FTextStart, FTextEnd - FTextStart), true);
				WriteLine(Builder, FData.Substring(FStart, i - FStart - 1), true);
				FTextStart = i + 1;
				FLastBlockType = BlockType.ClassFeatureEnd;
			}
			FCurrentBlockType = BlockType.Text;
		}

		private string ReadFile(IncludeDirective include) {
			string OFilename = Path.Combine(new FileInfo(FFilename).DirectoryName, include.File);
			FileInfo OFile = new FileInfo(OFilename);
			if (OFile.Exists) {
				using (FileStream OStream = new FileStream(OFilename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					using (StreamReader OReader = new StreamReader(OStream, Encoding.UTF8)) {
						return OReader.ReadToEnd();
					}
				}
			}
			else {
				throw new IOException("File not found - include: '" + include.File + "'");
			}
		}
		private void ValidateParserState() {
			if (FClassFeaturePresent && !FEndsWithClassFeature) {
				throw new ArgumentException("A class feature <#+ .. #> must be placed at the end of the template");	
			}
		}

		private bool Matches(string match) {
			if (i + match.Length > FData.Length) {
				return false;
			}
			return FData.Substring(i, match.Length) == match;
		}

		private StringBuilder Builder {
			get {
				return FClassFeaturePresent ? FTemplateMethods : FTemplateBody;
			}
		}

		internal TemplateOptions Options {
			get {
				return FOptions;
			}
		}
	}
}