// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ViewModel {
	public class FieldOption {
		private IOwner FOwner;
		private string FFor;
		private string FDefault;
		private string FTransform;

		public FieldOption(IOwner owner, XElement element) {
			FOwner = owner;
			if (element.Attribute("field") == null) {
				FOwner.Throw(element, "The required attribute 'field' is missing.");
			}
			FFor = element.Attribute("field").Value;
			if (element.Attribute("default") != null) {
				FDefault = element.Attribute("default").Value;
			}
			else {
				XElement ODefaultElem = element.Element(ItemFile.XmlNamespace + "default");
				if (ODefaultElem != null) {
					FDefault = ODefaultElem.Value;
				}
			}
			if (element.Attribute("transform") != null) {
				FTransform = element.Attribute("transform").Value;
			}
			else {
				XElement OTransformElem = element.Element(ItemFile.XmlNamespace + "transform");
				if (OTransformElem != null) {
					FTransform = OTransformElem.Value;
				}
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
