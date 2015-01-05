// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;

namespace T4Compiler.Generator {
	public abstract class DirectiveBase {

		public DirectiveBase() {
		}

		protected string ExtractParam(string text, string name) {
			int OIndex = text.IndexOfIC(name);
			if (OIndex == -1) {
				return null;
			}
			OIndex = text.IndexOf("\"", OIndex) + 1;
			int OIndex2 = text.IndexOf("\"", OIndex);
			return text.Substring(OIndex, OIndex2 - OIndex).Trim();
		}
	}
}