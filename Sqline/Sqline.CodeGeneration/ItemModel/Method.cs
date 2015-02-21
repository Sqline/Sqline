// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class Method {
		private Configuration FConfiguration;
		private List<Field> FFields = new List<Field>();
		private List<FieldOption> FFieldOptions = new List<FieldOption>();
		private List<Parameter> FParameters = new List<Parameter>();
		private string FName;
		private string FVisibility = "public";
		private int FTimeout;
		private string FSort;
		private ViewItem FViewItem;
		private Sql FSql;

		public Method(Configuration configuration, ViewItem viewItem, XElement element) {
			FConfiguration = configuration;
			FViewItem = viewItem;
			foreach (Field OField in viewItem.Fields) {
				FFields.Add(OField.Clone());
			}
			if (element.Attribute("name") == null) {
				//TODO: Throw error
			}
			FName = element.Attribute("name").Value;
			if (element.Element(ItemFile.XmlNamespace + "sql") != null) {
				FSql = new Sql(element.Element(ItemFile.XmlNamespace + "sql"));
			}
			else {
				//TODO: Throw error
			}
			if (element.Attribute("visibility") != null) {
				FVisibility = element.Attribute("visibility").Value;
			}
			FTimeout = FConfiguration.Methods.Timeout;
			if (element.Attribute("timeout") != null) {
				FTimeout = int.Parse(element.Attribute("timeout").Value);
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

			foreach (XElement OParameter in element.Elements(ItemFile.XmlNamespace + "parameter")) {
				FParameters.Add(new Parameter(OParameter));
			}

			foreach (XElement OFieldOption in element.Elements(ItemFile.XmlNamespace + "option")) {
				FFieldOptions.Add(new FieldOption(OFieldOption));
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
	}
}
