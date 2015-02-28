// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace T4Compiler.Generator {
	public class TemplateOptions {
		private bool FRemoveWhitespaceStatementLines = true;
		private string FAssemblyResolveDirectory;

		public bool RemoveWhitespaceStatementLines {
			get {
				return FRemoveWhitespaceStatementLines;
			}
			set {
				FRemoveWhitespaceStatementLines = value;
			}
		}

		public string AssemblyResolveDirectory {
			get {
				return FAssemblyResolveDirectory;
			}
			set {
				FAssemblyResolveDirectory = value;
			}
		}
	}
}
