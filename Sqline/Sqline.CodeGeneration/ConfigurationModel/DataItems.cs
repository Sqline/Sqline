// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class DataItems {
		private string FNamespace = null;

		internal DataItems() {
		}

		public DataItems(XElement element, XNamespace xmlNamespace) {
			if (element.Attribute("namespace") != null) {
				FNamespace = element.Attribute("namespace").Value;
			}
		}

		public string Namespace {
			get {
				return FNamespace ?? Configuration.EMPTY_NAMESPACE;
			}
		}
	}
}
