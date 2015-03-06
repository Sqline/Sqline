// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {

	public class ViewMethod : ResultMethod {
		private ViewItem FViewItem;
		private List<Field> FFields = new List<Field>();
		private List<FieldOption> FFieldOptions = new List<FieldOption>();

		public ViewMethod(ViewItem viewItem, Configuration configuration, XElement element) : base(viewItem, configuration, element) {
			FViewItem = viewItem;
			foreach (Field OField in viewItem.Fields) {
				FFields.Add(OField.Clone());
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

		public ViewItem ViewItem {
			get {
				return FViewItem;
			}
			set {
				FViewItem = value;
			}
		}

		public List<Field> Fields {
			get {
				return FFields;
			}
		}
	}
}