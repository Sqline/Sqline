// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using Sqline.CodeGeneration.ConfigurationModel;
using System;
using Sqline.Base;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class ScalarItem : IOwner {
		private IOwner FOwner;
		private Configuration FConfiguration;
		private List<Field> FFields = new List<Field>();
		private List<ScalarMethod> FMethods = new List<ScalarMethod>();
		private List<ItemBase> FBases = new List<ItemBase>();
		private string FType;
		private bool FNullable;
		private string FDefault;
		private string FTransform;
		private ITypeMapping FTypeMapping;

		public ScalarItem(IOwner owner, Configuration configuration, XElement element) {
			FOwner = owner;
			FConfiguration = configuration;

			if (element.Attribute("type") == null) {
				FOwner.Throw(element, "The required attribute 'type' is missing.");
			}

			FType = element.Attribute("type").Value;
			FTypeMapping = Provider.Current.GetTypeMapping(FType);

			if (element.Attribute("nullable") != null) {
				FNullable = element.Attribute("nullable").Value.Equals("true", StringComparison.OrdinalIgnoreCase);
			}

			if (element.Attribute("default") != null) {
				FDefault = element.Attribute("default").Value;
			}
			else {
				XElement ODefaultElem = element.Element(ItemFile.XmlNamespace + "default");
				if (ODefaultElem != null) {
					FDefault = ODefaultElem.Value;
				}
			}

			if (FDefault != null) {
				FNullable = false;
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

			foreach (XElement OMethod in element.Elements(ItemFile.XmlNamespace + "method")) {
				FMethods.Add(new ScalarMethod(this, FConfiguration, OMethod));
			}
		}

		public void Throw(XObject element, string message) {
			FOwner.Throw(element, message);
		}

		public List<ScalarMethod> Methods {
			get {
				return FMethods;
			}
		}

		public String CsReaderMethod {
			get {
				if (!String.IsNullOrEmpty(FDefault)) {
					return FTypeMapping.CSReader + "(0, out OHasValue)";
				}
				if (FNullable) {
					return FTypeMapping.CSReader + "OrNull(0)";
				}
				return FTypeMapping.CSReader + "(0)";
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

		public string Type {
			get {
				return FType;
			}
			set {
				FType = value;
			}
		}

		public String CsType {
			get {
				if (FNullable) {
					return FTypeMapping.CSNullable;
				}
				return FTypeMapping.CSType;
			}
		}

		public String CsTypeNonNullable {
			get {
				return FTypeMapping.CSType;
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
