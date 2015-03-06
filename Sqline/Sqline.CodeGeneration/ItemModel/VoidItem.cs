// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using Sqline.CodeGeneration.ConfigurationModel;
using System;
using Sqline.Base;

namespace Sqline.CodeGeneration.ViewModel {
	public class VoidItem : IOwner {
		private IOwner FOwner;
		private Configuration FConfiguration;
		private List<Field> FFields = new List<Field>();
		private List<VoidMethod> FMethods = new List<VoidMethod>();
		private List<ItemBase> FBases = new List<ItemBase>();

		public VoidItem(IOwner owner, Configuration configuration, XElement element) {
			FOwner = owner;
			FConfiguration = configuration;
			foreach (XElement OMethod in element.Elements(ItemFile.XmlNamespace + "method")) {
				FMethods.Add(new VoidMethod(this, FConfiguration, OMethod));
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

		public List<VoidMethod> Methods {
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
