// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo.Statement.Items {
	public class StoredProcedure : IView {

		public StoredProcedure(List<Token> tokens) {

		}

		public List<IColumn> Columns {
			get {
				return null;
			}
		}

		public string Name {
			get {
				return "proc";
			}
		}

		public override string ToString() {
			return "PROC";
		}
	}
}
