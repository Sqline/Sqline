// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ViewModel {
	public class FieldOption {
		private string FFor;
		private string FDefault;
		private string FTransform;

		public FieldOption(XElement element) {
			if (element.Attribute("field") == null) {
				//TODO: throw error
			}
			FFor = element.Attribute("field").Value;
			if (element.Attribute("default") != null) {
				FDefault = element.Attribute("default").Value;
			}
			if (element.Attribute("transform") != null) {
				FTransform = element.Attribute("transform").Value;
			}
		}

		public string For {
			get {
				return FFor;
			}
			set {
				FFor = value;
			}
		}

		public string Default {
			get {
				return FDefault;
			}
			set {
				FDefault = value;
			}
		}

		public string Transform {
			get {
				return FTransform;
			}
			set {
				FTransform = value;
			}
		}
	}
}
