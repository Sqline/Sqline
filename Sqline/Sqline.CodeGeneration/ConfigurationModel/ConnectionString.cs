// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class ConnectionString {
		private string FValue = "";
		private string FProvider = "SqlServer";

		public ConnectionString() {
		}

		public ConnectionString(XElement element, XNamespace xmlNamespace) {
			FValue = element.Attribute("value").Value;
			if (element.Attribute("provider") != null) {
				FProvider = element.Attribute("provider").Value;
			}
		}

		public bool IsEmpty {
			get {
				return FValue == "";
			}
		}

		public string Value {
			get {
				return FValue;
			}
		}

		public string Provider {
			get {
				return FProvider;
			}
		}
	}
}
