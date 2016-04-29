// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Collections;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;
using System;

namespace T4Compiler.Generator {

	public class TemplateCompiler {
		private CompilerParameters FParams = new CompilerParameters();
		private List<string> FSourceCodes = new List<string>();

		public TemplateCompiler(string outputAssembly) {
			FParams.ReferencedAssemblies.Add("mscorlib.dll");
			FParams.ReferencedAssemblies.Add("System.dll");
			FParams.ReferencedAssemblies.Add("System.Core.dll");
			FParams.ReferencedAssemblies.Add("System.Linq.dll");
			FParams.ReferencedAssemblies.Add("System.Xml.dll");
			FParams.ReferencedAssemblies.Add("System.Xml.Linq.dll");
			FParams.OutputAssembly = outputAssembly;
		}
		
		public CompilerResults Compile() {
			FParams.ReferencedAssemblies.Add(new Uri(typeof(TemplateCompiler).Assembly.CodeBase).LocalPath); //Add current assembly as a reference
			FParams.GenerateExecutable = false;
			FParams.GenerateInMemory = false;
			FParams.TreatWarningsAsErrors = false;
			FParams.IncludeDebugInformation = true;
			FParams.WarningLevel = 4;
			CSharpCodeProvider OProvider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });
			return OProvider.CompileAssemblyFromSource(FParams, FSourceCodes.ToArray());
		}

		public IList Imports {
			get {
				return FParams.ReferencedAssemblies;
			}
		}

		public List<string> SourceCodes {
			get {
				return FSourceCodes;
			}
		}
	}
}