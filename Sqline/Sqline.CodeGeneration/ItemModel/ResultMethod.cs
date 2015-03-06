// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.ClientFramework.ProviderModel;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public enum ResultType { List, SingleItem };

	public class ResultMethod : BaseMethod {
		protected ResultType FResult = ResultType.List;
		protected string FSort;
		protected string FFilter;

		public ResultMethod(IOwner owner, Configuration configuration, XElement element) : base(owner, configuration, element) {
			if (element.Attribute("result") != null) {
				ParseResultType(element.Attribute("result"));
			}

			ParseSort(element);
			ParseFilter(element);
		}

		private void ParseResultType(XAttribute attribute) {
			string OResultType = attribute.Value;
			if (OResultType.Equals("List", StringComparison.OrdinalIgnoreCase)) {
				FResult = ResultType.List;
			}
			else if (OResultType.Equals("SingleItem", StringComparison.OrdinalIgnoreCase)) {
				FResult = ResultType.SingleItem;
			}
			else {
				Throw(attribute, "Unable to parse Result attribute value '" + OResultType + "'");
			}
		}

		private void ParseSort(XElement element) {
			if (element.Attribute("sort") != null) {
				FSort = element.Attribute("sort").Value;
				if (FResult == ResultType.SingleItem) {
					Throw(element.Attribute("sort"), "It is not possible to sort a SingleItem result"); //TODO: Should be a warning
				}
			}
			else {
				XElement OSortElem = element.Element(ItemFile.XmlNamespace + "sort");
				if (OSortElem != null) {
					FSort = OSortElem.Value;
					if (FResult == ResultType.SingleItem) {
						Throw(OSortElem, "It is not possible to sort a SingleItem result"); //TODO: Should be a warning
					}
				}
			}
		}

		private void ParseFilter(XElement element) {
			if (element.Attribute("filter") != null) {
				FFilter = element.Attribute("filter").Value;
				if (FResult == ResultType.SingleItem) {
					Throw(element.Attribute("filter"), "It is not possible to filter a SingleItem result"); //TODO: Should be a warning
				}
			}
			else {
				XElement OFilterElem = element.Element(ItemFile.XmlNamespace + "filter");
				if (OFilterElem != null) {
					FFilter = OFilterElem.Value;
					if (FResult == ResultType.SingleItem) {
						Throw(OFilterElem, "It is not possible to filter a SingleItem result"); //TODO: Should be a warning
					}
				}
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

		public ResultType Result {
			get {
				return FResult;
			}
		}
	}
}
