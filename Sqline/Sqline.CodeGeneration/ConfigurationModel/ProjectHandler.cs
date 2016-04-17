// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class ProjectHandler {
		private string FNamespace = null;
		private string FName = null;

		internal ProjectHandler() {
		}

		public ProjectHandler(XElement element, XNamespace xmlNamespace) {
			if (element.Attribute("namespace") != null) {
				FNamespace = element.Attribute("namespace").Value;
			}
			if (element.Attribute("name") != null) {
				FName = element.Attribute("name").Value;
			}
		}

		public string Namespace {
			get {
				return FNamespace;
			}
			internal set {
				FNamespace = value;
			}
		}

		public string Name {
			get {
				return FName;
			}
			internal set {
				FName = value;
			}
		}

		public string SqlineApplication {
			get {
				return FNamespace + "." + FName + ".SqlineApplication";
			}
		}
	}
}
