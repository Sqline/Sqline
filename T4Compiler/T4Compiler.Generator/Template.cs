// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace T4Compiler.Generator {
	public class Template {
		private static Dictionary<string, Type> FAssemblies = new Dictionary<string, Type>();
		private Dictionary<string, object> FParameters = new Dictionary<string, object>();
		private TemplateOptions FOptions = new TemplateOptions();
		private TemplateCompiler FCompiler;
		private TemplateParser FTemplateParser;
		private Type FType;
		private string FTemplateSource = "";
		private string FGeneratedSourceCode = "";
		private string FFile = "";
		private string FFileHash = "";
		private string FTempAssemblyFile = "";
		private bool FDebug = false;

		public Template(string filepath, TemplateOptions options) {
			FOptions = options;
			using (FileStream OStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				using (StreamReader OReader = new StreamReader(OStream, Encoding.UTF8)) {
					FTemplateSource = OReader.ReadToEnd();
				}
			}
			Init(filepath);
		}

		public Template(string filepath) : this(filepath, new TemplateOptions()) {
		}

		public Template(Stream stream, string virtualFilepath) {
			using (StreamReader OReader = new StreamReader(stream)) {
				FTemplateSource = OReader.ReadToEnd();
			}
			Init(virtualFilepath);
		}

		private void Init(string filename) {
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve; //TODO: Move somewhere more appropriate
			FFile = filename;
			FFileHash = FFile.ToMD5Hash();
			FTemplateParser = new TemplateParser(FFile, FFileHash, FOptions);
			FCompiler = new TemplateCompiler(FTempAssemblyFile);
		}

		Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
			/*TODO: This way of importing the referenced assembly is a bit of a hack, should be reworked */
			string OAssemblyName = args.Name.Remove(args.Name.IndexOf(','));
			foreach (string OImport in FTemplateParser.Imports) {
				if (OImport.ContainsIC(OAssemblyName + ".dll")) {
					return Assembly.LoadFrom(OImport);
				}
			}
			Console.WriteLine("Unable to resolve: " + args.Name);
			return null;
		}

		private bool HasValidCachedAssembly() {
			try {
				if (File.Exists(FTempAssemblyFile) && File.GetLastWriteTime(FFile) <= File.GetLastWriteTime(FTempAssemblyFile)) {
					if (!FAssemblies.ContainsKey(FFile)) {
						Assembly OAssembly = Assembly.LoadFrom(FTempAssemblyFile);
						FAssemblies.Add(FFile, OAssembly.GetType(FTemplateParser.FullClassName));
					}
					return true;
				}
			}
			catch { }
			return false;
		}

		public void Process() {
			FTemplateParser.SetParameters(FParameters);
			FGeneratedSourceCode = FTemplateParser.Parse(FTemplateSource);
			FDebug = FTemplateParser.Debug;

			if (HasValidCachedAssembly()) {
				FType = FAssemblies[FFile];
				return;
			}

			foreach (string OAssembly in FTemplateParser.Imports) {
				FCompiler.Imports.Add(OAssembly);
			}

			FCompiler.SourceCodes.Add(FGeneratedSourceCode);
			CompilerResults OResults = FCompiler.Compile();
			if (OResults.Errors.Count > 0) {
				StringBuilder OBuilder = new StringBuilder();
				foreach (CompilerError OError in OResults.Errors) {
					OBuilder.AppendLine(OError.ToString());
				}
				throw new Exception(OBuilder.ToString());
			}
			FType = OResults.CompiledAssembly.GetType(FTemplateParser.FullClassName);
		}

		private string GetTempAssemblyPath(string file) {
			return Path.GetTempPath() + "Template" + file.ToMD5Hash() + ".dll";
		}

		public string InvokeTemplate() {
			ICodeTemplate OCodeTemplate = (ICodeTemplate)Activator.CreateInstance(FType);
			foreach (KeyValuePair<string, object> pair in FParameters) {
				OCodeTemplate.SetParameter(pair.Key, pair.Value);
			}
			return OCodeTemplate.Generate();
		}

		public string GeneratedSourceCode {
			get {
				return FGeneratedSourceCode;
			}
		}

		public string TemplateSource {
			get {
				return FTemplateSource;
			}
		}

		public Encoding Encoding {
			get {
				return FTemplateParser.Encoding;
			}
		}

		public string Extension {
			get {
				return FTemplateParser.Extension;
			}
		}

		public Dictionary<string, object> Parameters {
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
	}
}
