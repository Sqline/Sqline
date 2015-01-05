// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;

namespace T4Compiler.Generator {
	//<#@ output extension=".fileNameExtension" [encoding="encoding"] #>
	public class OutputDirective : DirectiveBase {
		private Encoding FEncoding;
		private string FExtension;
		
		public OutputDirective(string tagcontent) : base() {
			FExtension = ExtractParam(tagcontent, "extension");
			if (FExtension == null) {
				throw new ArgumentNullException("extension", "An output directive must specify a value for the extension attribute");
			} 
			string OEncoding = ExtractParam(tagcontent, "encoding");
			if (OEncoding != null) {
				try {
					FEncoding = Encoding.GetEncoding(OEncoding);
				}
				catch {
					throw new ArgumentException("Invalid encoding attribute value '" + OEncoding + "' for output directive");
				}
			}
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
	}
}