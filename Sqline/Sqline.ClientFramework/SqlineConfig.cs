// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public class SqlineConfig {
		private string FConnectionString;

		public string ConnectionString {
			get {
				return FConnectionString;
			}
			set {
				FConnectionString = value;
			}
		}
	}
}