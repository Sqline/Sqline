// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {

	public class ViewMethod : IOwner {
		private Configuration FConfiguration;
		private ViewItem FViewItem;
		private List<Field> FFields = new List<Field>();
		private List<FieldOption> FFieldOptions = new List<FieldOption>();
		private List<Parameter> FParameters = new List<Parameter>();
		private string FName;
		private string FVisibility = "public";
		private int FTimeout;
		private string FSort;
		private string FFilter;
		private Sql FSql;
		private bool FTransactionSupport;

		public ViewMethod(ViewItem viewItem, Configuration configuration, XElement element) {
			FConfiguration = configuration;
			FViewItem = viewItem;
			foreach (Field OField in viewItem.Fields) {
				FFields.Add(OField.Clone());
			}
			if (element.Attribute("name") == null) {
				FViewItem.Throw(element, "The required attribute 'name' is missing.");
			}
			FName = element.Attribute("name").Value;

			if (element.Element(ItemFile.XmlNamespace + "sql") != null) {
				FSql = new Sql(this, element.Element(ItemFile.XmlNamespace + "sql"));
			}
			else {
				FViewItem.Throw(element, "The required element 'sql' is missing.");
			}

			if (element.Attribute("visibility") != null) {
				FVisibility = element.Attribute("visibility").Value;
			}
			FTimeout = FConfiguration.Methods.Timeout;
			if (element.Attribute("timeout") != null) {
				FTimeout = int.Parse(element.Attribute("timeout").Value);
			}

			if (element.Attribute("transactionsupport") != null) {
				FTransactionSupport = element.Attribute("transactionsupport").Value == "true";
			}

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

			foreach (XElement OParameter in element.Elements(ItemFile.XmlNamespace + "parameter")) {
				FParameters.Add(new Parameter(this, OParameter));
			}

			foreach (XElement OFieldOption in element.Elements(ItemFile.XmlNamespace + "option")) {
				FFieldOptions.Add(new FieldOption(this, OFieldOption));
			}
			ApplyFieldOptions();
		}

		private Field GetField(string name) {
			foreach (Field OField in FFields) {
				if (OField.Name == name) {
					return OField;
				}
			}
			return null;
		}

		private void ApplyFieldOptions() {
			foreach (FieldOption OOption in FFieldOptions) {
				Field OField = GetField(OOption.For);
				if (OField != null) {
					if (OOption.Default != null) {
						OField.Default = OOption.Default;
					}
					if (OOption.Transform != null) {
						OField.Transform = OOption.Transform;
					}
				}
			}
		}

		public List<Field> GetFields(string source) {
			List<Field> OResult = new List<Field>();
			foreach (Field OField in FFields) {
				if (OField.Source == source) {
					OResult.Add(OField);
				}
			}
			return OResult;
		}

		public void Throw(XElement element, string message) {
			FViewItem.Throw(element, message);
		}

		public string Name {
			get {
				return FName;
			}
			set {
				FName = value;
			}
		}

		public string Visibility {
			get {
				return FVisibility;
			}
		}

		public ViewItem ViewItem {
			get {
				return FViewItem;
			}
			set {
				FViewItem = value;
			}
		}

		public Sql Sql {
			get {
				return FSql;
			}
			set {
				FSql = value;
			}
		}

		public int Timeout {
			get {
				return FTimeout;
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

		public List<Field> Fields {
			get {
				return FFields;
			}
		}

		public List<Parameter> Parameters {
			get {
				return FParameters;
			}
		}

		public bool TransactionSupport {
			get {
				return FTransactionSupport;
			}
			set {
				FTransactionSupport = value;
			}
		}
	}
}