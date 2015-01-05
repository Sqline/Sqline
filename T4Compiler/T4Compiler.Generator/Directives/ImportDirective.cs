// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;

namespace T4Compiler.Generator {
	//<#@ import namespace="namespace" #>
	public class ImportDirective : DirectiveBase {
		private string FNamespace;

		public ImportDirective(string tagcontent) : base() {
			FNamespace = ExtractParam(tagcontent, "namespace");
			if (FNamespace == null) {
				throw new ArgumentNullException("namespace", "An import directive must specify a value for the namespace attribute");
			} 
		}

		public string Namespace {
			get {
				return FNamespace;
			}
		}
	}
}