// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ViewModel {
	public class Sql {
		private string FStatement;

		public Sql(XElement element) {
			FStatement = element.Value.Trim();
		}

		public string Statement {
			get {
				return FStatement;
			}
		}
	}
}
