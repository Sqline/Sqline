// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class ViewItems {
		private string FNamespace = null;
		private string FPrefix = null;
		private string FPostfix = null;
		private List<ItemBase> FBases = new List<ItemBase>();
		private List<Using> FUsings = new List<Using>();
		private List<Include> FIncludes = new List<Include>();

		internal ViewItems() {
		}

		public ViewItems(XElement element, XNamespace xmlNamespace) {
			foreach (XElement OUsing in element.Elements(xmlNamespace + "using")) {
				FUsings.Add(new Using(OUsing, xmlNamespace));
			}
			foreach (XElement OInclude in element.Elements(xmlNamespace + "include")) {
				FIncludes.Add(new Include(OInclude, xmlNamespace));
			}
			foreach (XElement OBase in element.Elements(xmlNamespace + "base")) {
				FBases.Add(new ItemBase(OBase, xmlNamespace));
			}
			if (element.Attribute("namespace") != null) {
				FNamespace = element.Attribute("namespace").Value;
			}
			if (element.Attribute("prefix") != null) {
				FPrefix = element.Attribute("prefix").Value;
			}
			if (element.Attribute("postfix") != null) {
				FPostfix = element.Attribute("postfix").Value;
			}
		}

		public void Append(ViewItems viewitems) {
			if (viewitems.FNamespace != null) {
				FNamespace = viewitems.FNamespace;
			}
			if (viewitems.FPrefix != null) {
				FPrefix = viewitems.FPrefix;
			}
			if (viewitems.FPostfix != null) {
				FPostfix = viewitems.FPostfix;
			}
			if (viewitems.Usings.Count > 0) {
				foreach (Using OUsing in viewitems.Usings) {
					if (!HasUsing(OUsing.Namespace)) {
						FUsings.Add(OUsing);
					}
				}
			}
			if (viewitems.Includes.Count > 0) {
				foreach (Include OInclude in viewitems.Includes) {
					if (!HasInclude(OInclude.File)) {
						FIncludes.Add(OInclude);
					}
				}
			}
			if (viewitems.Bases.Count > 0) {
				foreach (ItemBase OBase in viewitems.Bases) {
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
		}

		private bool HasUsing(string key) {
			return FUsings.Any(u => u.Namespace == key);
		}

		private bool HasInclude(string key) {
			return FIncludes.Any(i => i.File == key);
		}

		private bool HasBase(string key) {
			return FBases.Any(b => b.Name == key);
		}

		public string Namespace {
			get {
				return FNamespace ?? Configuration.EMPTY_NAMESPACE;
			}
		}

		public string Prefix {
			get {
				return FPrefix ?? "";
			}
		}

		public string Postfix {
			get {
				return FPostfix ?? "";
			}
		}

		public List<Using> Usings {
			get {
				return FUsings;
			}
		}

		public List<Include> Includes {
			get {
				return FIncludes;
			}
		}

		public List<ItemBase> Bases {
			get {
				return FBases;
			}
		}
	}
}
