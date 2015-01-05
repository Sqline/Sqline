// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;

namespace T4Compiler.Generator {
	//<#@ parameter type="Full.TypeName" name="ParameterName" #>
	public class ParameterDirective : DirectiveBase {
		private string FName;
		private string FType;

		public ParameterDirective(string tagcontent) : base() {
			FName = ExtractParam(tagcontent, "name");
			FType = ExtractParam(tagcontent, "type");
			if (FName == null) {
				throw new ArgumentNullException("name", "A parameter directive must specify a value for the name attribute");
			}
			if (FType == null) {
				throw new ArgumentNullException("type", "A parameter directive must specify a value for the type attribute");
			}
		}

		public string Name {
			get {
				return FName;
			}
		}

		public string Type {
			get {
				return FType;
			}
		}
	}
}