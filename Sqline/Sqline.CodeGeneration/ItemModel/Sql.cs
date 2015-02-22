// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ViewModel {
	public class Sql {
		private IOwner FOwner;
		private string FStatement;

		public Sql(IOwner owner, XElement element) {
			FOwner = owner;
			FStatement = element.Value.Trim();
		}

		public string Statement {
			get {
				return FStatement;
			}
		}
	}
}
