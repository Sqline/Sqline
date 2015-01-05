// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;

namespace T4Compiler.Generator {
	//<#@ import namespace="namespace" #>
	public class IncludeDirective : DirectiveBase {
		private string FFile;

		public IncludeDirective(string tagcontent) : base() {
			FFile = ExtractParam(tagcontent, "file");
			if (FFile == null) {
				throw new ArgumentNullException("file", "An inclide directive must specify a value for the file attribute");
			} 
		}

		public string File {
			get {
				return FFile;
			}
		}
	}
}