// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo.Statement.Items {
	public class SelectColumn : IColumn {
		private string FPath;
		private string FName;
		public SelectColumn(List<Token> tokens) {
			bool OPathComplete = false;
			bool OPrevWasDot = true;
			FPath = "";
			foreach (Token OToken in tokens) {
				bool OIsDot = OToken.Type == TokenType.Raw && OToken.Value == ".";
				if (OToken.Type == TokenType.Raw && OToken.Value == "AS") {
					OPathComplete = true;
				}
				if (!OPrevWasDot && !OIsDot) {
					OPathComplete = true;
				}
				if (!OPathComplete) {
					FPath += OToken.Value;
				}
				FName = OToken.Value;
				OPrevWasDot = OIsDot;
			}
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
