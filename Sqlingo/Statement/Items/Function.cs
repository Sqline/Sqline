// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo.Statement.Items {
	public class Function : IColumn {
		private string FPath;
		private string FName;
		public Function(List<Token> tokens) {
			bool OPathComplete = false;
			FPath = "";
			foreach (Token OToken in tokens) {
				if (OToken.Type == TokenType.Raw && OToken.Value == "AS") {
					OPathComplete = true;
				}
				if (!OPathComplete) {
					FPath += OToken.Value;
				}
			}
			FName = "Func";
		}

		public string Name {
			get {
				return FName;
			}
		}

		public string Path {
			get {
				return FPath;
			}
		}
	}
}
