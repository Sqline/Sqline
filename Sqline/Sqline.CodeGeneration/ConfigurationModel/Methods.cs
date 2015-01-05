// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class Methods {
		private string FNamespace = null;
		private int? FTimeout = null;
		private string FPrefix = null;
		private string FPostfix = "Handler";
		private List<Using> FUsings = new List<Using>();
		private List<Include> FIncludes = new List<Include>();

		internal Methods() {
		}

		public Methods(XElement element, XNamespace xmlNamespace) {
			foreach (XElement OUsing in element.Elements(xmlNamespace + "using")) {
				FUsings.Add(new Using(OUsing, xmlNamespace));
			}
			foreach (XElement OInclude in element.Elements(xmlNamespace + "include")) {
				FIncludes.Add(new Include(OInclude, xmlNamespace));
			}
			if (element.Attribute("namespace") != null) {
				FNamespace = element.Attribute("namespace").Value;
			}
			if (element.Attribute("timeout") != null) {
				FTimeout = int.Parse(element.Attribute("timeout").Value);
			}
			if (element.Attribute("prefix") != null) {
				FPrefix = element.Attribute("prefix").Value;
			}
			if (element.Attribute("postfix") != null) {
				FPostfix = element.Attribute("postfix").Value;
			}
		}

		public void Append(Methods methods) {
			if (methods.FNamespace != null) {
				FNamespace = methods.FNamespace;
			}
			if (methods.FTimeout != null) {
				FTimeout = methods.FTimeout;
			}
			if (methods.Usings.Count > 0) {
				foreach (Using OUsing in methods.Usings) {
					if (!HasUsing(OUsing.Namespace)) {
						FUsings.Add(OUsing);
					}
				}
			}
			if (methods.Includes.Count > 0) {
				foreach (Include OInclude in methods.Includes) {
					if (!HasInclude(OInclude.File)) {
						FIncludes.Add(OInclude);
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

		public string Namespace {
			get {
				return FNamespace ?? Configuration.EMPTY_NAMESPACE;
			}
		}

		public int Timeout {
			get {
				return FTimeout ?? 0;
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
	}
}
