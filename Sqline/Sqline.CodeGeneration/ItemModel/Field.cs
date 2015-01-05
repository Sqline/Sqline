﻿// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Xml.Linq;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class Field : ICloneable {
		private XElement FElement;
		private ViewItem FViewItem;
		private string FName;
		private string FType;
		private bool FNullable;
		private ITypeMapping FTypeMapping;
		private string FVisibility = "public";
		private string FTransform;
		private string FDefault;
		private string FSource = "queryfield";

		public Field(ViewItem viewitem, XElement element) {
			FElement = element;
			FViewItem = viewitem;
			if (element.Attribute("name") == null) {
				//TODO: Throw error
			}
			if (element.Attribute("type") == null) {
				//TODO: Throw error
			}
			FName = element.Attribute("name").Value;
			FType = element.Attribute("type").Value;
			FTypeMapping = Provider.Current.GetTypeMapping(FType);
			if (element.Attribute("nullable") != null) {
				FNullable = element.Attribute("nullable").Value.Equals("true", StringComparison.OrdinalIgnoreCase);
			}
			if (element.Attribute("default") != null) {
				FDefault = element.Attribute("default").Value;
			}
			if (element.Attribute("visibility") != null) {
				FVisibility = element.Attribute("visibility").Value;
			}
			if (element.Attribute("transform") != null) {
				FTransform = element.Attribute("transform").Value;
			}
			if (element.Attribute("source") != null) {
				FSource = element.Attribute("source").Value;
			}
		}

		public string Name {
			get {
				return FName;
			}
			set {
				FName = value;
			}
		}

		public string Default {
			get {
				return FDefault;
			}
			set {
				FDefault = value;
			}
		}

		public string Type {
			get {
				return FType;
			}
			set {
				FType = value;
			}
		}

		public string Source {
			get {
				return FSource;
			}
			set {
				FSource = value;
			}
		}

		public String CsType {
			get {
				if (FNullable) {
					return FTypeMapping.CSNullable;
				}
				return FTypeMapping.CSType;
			}
		}

		public String CsTypeNonNullable {
			get {
				return FTypeMapping.CSType;
			}
		}

		public String HasValueFieldName {
			get {
				return "O" + Name + "HasValue";
			}
		}

		public String IndexFieldName {
			get {
				return "O" + Name + "Ordinal";
			}
		}

		public String CsReaderMethod {
			get {
				if (!String.IsNullOrEmpty(FDefault)) {
					return FTypeMapping.CSReader + "(" + IndexFieldName + ", out " + HasValueFieldName + ")";
				}
				if (FNullable) {
					return FTypeMapping.CSReader + "OrNull(" + IndexFieldName + ")";
				}
				return FTypeMapping.CSReader + "(" + IndexFieldName + ")";
			}
		}

		public string Visibility {
			get {
				return FVisibility;
			}
			set {
				FVisibility = value;
			}
		}

		public string Transform {
			get {
				return FTransform;
			}
			set {
				FTransform = value;
			}
		}

		public Field Clone() {
			return (Field)(((ICloneable)this).Clone());
		}

		object ICloneable.Clone() {
			return new Field(FViewItem, FElement);
		}
	}
}
