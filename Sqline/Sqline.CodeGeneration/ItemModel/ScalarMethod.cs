// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.ClientFramework.ProviderModel;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class ScalarMethod : BaseMethod {
		private ScalarItem FScalarItem;
		private string FSort;
		private string FFilter;

		public ScalarMethod(ScalarItem scalarItem, Configuration configuration, XElement element) : base(scalarItem, configuration, element) {
			FScalarItem = scalarItem;

			if (element.Attribute("sort") != null) {
				FSort = element.Attribute("sort").Value;
			}
			else {
				XElement OSortElem = element.Element(ItemFile.XmlNamespace + "sort");
				if (OSortElem != null) {
					FSort = OSortElem.Value;
				}
			}

			if (element.Attribute("filter") != null) {
				FFilter = element.Attribute("filter").Value;
			}
			else {
				XElement OFilterElem = element.Element(ItemFile.XmlNamespace + "filter");
				if (OFilterElem != null) {
					FFilter = OFilterElem.Value;
				}
			}
		}

		public ScalarItem ScalarItem {
			get {
				return FScalarItem;
			}
		}

		public string Sort {
			get {
				return FSort;
			}
		}

		public string Filter {
			get {
				return FFilter;
			}
		}
	}
}
