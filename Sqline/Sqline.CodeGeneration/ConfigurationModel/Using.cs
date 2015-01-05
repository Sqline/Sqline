// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class Using {
		private string FNamespace = "";
		private string FRemove = "";

		public Using(XElement element, XNamespace xmlNamespace) {
			if (element.Attribute("namespace") != null) {
				FNamespace = element.Attribute("namespace").Value;
			}
			if (element.Attribute("remove") != null) {
				FRemove = element.Attribute("remove").Value;
			}
		}

		public string Namespace {
			get {
				return FNamespace;
			}
		}

		public string Remove {
			get {
				return FRemove;
			}
		}
	}
}
