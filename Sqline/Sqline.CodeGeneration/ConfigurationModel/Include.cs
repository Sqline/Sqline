// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class Include {
		private string FFile = "";
		private string FRemove = "";

		public Include(XElement element, XNamespace xmlNamespace) {
			if (element.Attribute("file") != null) {
				FFile = element.Attribute("file").Value;
			}
			if (element.Attribute("remove") != null) {
				FRemove = element.Attribute("remove").Value;
			}
		}

		public string File {
			get {
				return FFile;
			}
		}

		public string Remove {
			get {
				return FRemove;
			}
		}
	}
}
