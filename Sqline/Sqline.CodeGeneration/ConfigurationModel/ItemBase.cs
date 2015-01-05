// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class ItemBase {
		private string FName = null;
		private string FType = null;
		private string FRemove = null;

		public ItemBase(XElement element, XNamespace xmlNamespace) {
			if (element.Attribute("name") != null) {
				FName = element.Attribute("name").Value;
			}
			if (element.Attribute("type") != null) {
				FType = element.Attribute("type").Value;
			}
			if (element.Attribute("remove") != null) {
				FRemove = element.Attribute("remove").Value;
			}
		}

		public string Name {
			get {
				return FName ?? string.Empty;
			}
		}

		public string Type {
			get {
				return FType ?? string.Empty;
			}
		}

		public string Remove {
			get {
				return FRemove ?? string.Empty;
			}
		}
	}
}
