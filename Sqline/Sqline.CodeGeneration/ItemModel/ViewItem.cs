// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using Sqline.CodeGeneration.ConfigurationModel;
using System;
using Sqline.Base;

namespace Sqline.CodeGeneration.ViewModel {
	public class ViewItem : IOwner {
		private string FName;
		private IOwner FOwner;
		private Configuration FConfiguration;
		private List<Field> FFields = new List<Field>();
		private List<ViewMethod> FMethods = new List<ViewMethod>();
		private List<ItemBase> FBases = new List<ItemBase>();

		public ViewItem(IOwner owner, Configuration configuration, XElement element) {
			FOwner = owner;
			FConfiguration = configuration;
			if (element.Attribute("name") == null) {
				FOwner.Throw(element, "The required attribute 'name' is missing.");
			}
			FName = element.Attribute("name").Value;
			foreach (XElement OField in element.Elements(ItemFile.XmlNamespace + "field")) {
				FFields.Add(new Field(this, OField));
			}
			foreach (XElement OMethod in element.Elements(ItemFile.XmlNamespace + "method")) {
				FMethods.Add(new ViewMethod(this, FConfiguration, OMethod));
			}
			foreach (ItemBase OBase in FConfiguration.ViewItems.Bases) {
				FBases.Add(OBase);
			}
			foreach (XElement OBaseElement in element.Elements(ItemFile.XmlNamespace + "base")) {
				ItemBase OBase = new ItemBase(OBaseElement, ItemFile.XmlNamespace);
				if (!string.IsNullOrEmpty(OBase.Remove)) {
					if (OBase.Remove.Equals("all", StringComparison.OrdinalIgnoreCase)) {
						FBases.Clear();
					}
					else {
						FBases.RemoveAll(i => i.Name == OBase.Name);
					}
				}
				if (!string.IsNullOrEmpty(OBase.Name)) {
					if (!HasBase(OBase.Name)) {
						FBases.Add(OBase);
					}
				}
			}
		}

		private bool HasBase(string key) {
			return FBases.Any(b => b.Name == key);
		}

		public void Throw(XObject element, string message) {
			FOwner.Throw(element, message);
		}

		public string Name {
			get {
				return FName;
			}
			set {
				FName = value;
			}
		}

		public string FullName {
			get {
				return FConfiguration.ViewItems.Prefix + FName + FConfiguration.ViewItems.Postfix;
			}
		}

		public List<Field> Fields {
			get {
				return FFields;
			}
		}

		public List<ViewMethod> Methods {
			get {
				return FMethods;
			}
		}

		public List<ItemBase> Bases {
			get {
				return FBases;
			}
		}
	}
}
