// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;

namespace T4Compiler.Generator {
	//<#@ template [language="VB"] [compilerOptions="options"] [culture="code"] [debug="true"] [hostspecific="true"] [inherits="templateBaseClass"] [visibility="internal"] [linePragmas="false"] #>
	public class TemplateDirective : DirectiveBase {
		private bool FDebug;
		private bool FLinePragmas;

		public TemplateDirective(string tagcontent) : base() {
			string ODebugValue = ExtractParam(tagcontent, "debug");
			FDebug = ODebugValue == null ? false : ODebugValue.EqualsIC("true");
			string OLinePragmasValue = ExtractParam(tagcontent, "linepragmas");
			FLinePragmas = OLinePragmasValue == null ? false : OLinePragmasValue.EqualsIC("true");
			//TODO: Support the rest of the template directive arguments
		}

		public bool Debug {
			get {
				return FDebug;
			}
		}

		public bool LinePragmas {
			get {
				return FLinePragmas;
			}
		}
	}
}