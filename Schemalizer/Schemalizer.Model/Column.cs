// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Schemalizer.Base;

namespace Schemalizer.Model {
	public class Column : IXmlSerializable {
		protected SchemaModel FModel;
		protected Table FTable;
		protected String FName;
		protected String FDataType;
		protected String FDefaultValue;
		protected int FMaxLength;
		protected bool FNullable;
		protected bool FPrimaryKey;
		protected bool FAutoIncrement;
		protected Dictionary<String, String> FAdddionalMetadata = new Dictionary<String, String>();
		protected OrderedDictionary<String, Database> FDatabaseRelationships = new OrderedDictionary<String, Database>();

		internal Column(String name, Table table, SchemaModel model, Database database) {
			FName = name;
			FTable = table;
			FModel = model;
			AssignDatabase(database);
		}

		private Column(String name, Table table, SchemaModel model) {
			FName = name;
			FTable = table;
			FModel = model;
		}

		internal void AssignDatabase(Database database) {
			if (!FDatabaseRelationships.ContainsKey(database.Name)) {
				FDatabaseRelationships.Add(database.Name, database);
			}
		}

		public void AddAdditionalMetadata(String key, String value) {
			if (!FAdddionalMetadata.ContainsKey(key)) {
				FAdddionalMetadata.Add(key, value);
			}
		}

		public XElement ToXElement() {
			XElement OElement = new XElement("Column",
												new XAttribute("Name", Name),
												new XAttribute("DataType", DataType),
												new XAttribute("MaxLength", MaxLength),
												new XAttribute("Nullable", Nullable),
												new XAttribute("DefaultValue", DefaultValue ?? ""),
												new XAttribute("PrimaryKey", PrimaryKey),
												new XAttribute("Required", Required),
												new XAttribute("AutoIncrement", AutoIncrement));
			foreach (String OKey in FAdddionalMetadata.Keys) {
				OElement.Add(new XAttribute(OKey, FAdddionalMetadata[OKey]));
			}
			XElement ORelationships = new XElement("DatabaseRelationships");
			OElement.Add(ORelationships);
			foreach (Database ODatabase in FDatabaseRelationships.Values) {
				ORelationships.Add(ODatabase.ToRelationshipXElement());
			}
			return OElement;
		}

		public static Column FromXElement(XElement element, Table table, SchemaModel model) {
			Column OColumn = new Column(element.Attribute("Name").Value, table, model) {
				AutoIncrement = Convert.ToBoolean(element.Attribute("AutoIncrement").Value),
				DataType = element.Attribute("DataType").Value,
				MaxLength = Convert.ToInt32(element.Attribute("MaxLength").Value),
				Nullable = Convert.ToBoolean(element.Attribute("Nullable").Value),
				DefaultValue = element.Attribute("DefaultValue").Value,
				PrimaryKey = Convert.ToBoolean(element.Attribute("PrimaryKey").Value)
			};
			foreach (XAttribute OAttribute in element.Attributes()) {
				String OKey = OAttribute.Name.LocalName;
				if (OKey != "Name" && OKey != "AutoIncrement" && OKey != "DataType" && OKey != "MaxLength" && OKey != "Nullable" && OKey != "DefaultValue" && OKey != "PrimaryKey" && OKey != "Required") {
					OColumn.FAdddionalMetadata.Add(OKey, OAttribute.Value);
				}
			}
			var ODatabaseRelationships = from r in element.Element("DatabaseRelationships").Elements("DatabaseRelationship") select r.Attribute("Name").Value;
			foreach (String ODatabaseName in ODatabaseRelationships) {
				Database ODatabase = model.GetDatabase(ODatabaseName);
				if (ODatabase == null) {
					throw new ArgumentException("Invalid database relationship '" + ODatabaseName + "' for column '" + OColumn.Name + "' in table '" + table.FullName + "'");
				}
				OColumn.FDatabaseRelationships.Add(ODatabase.Name, ODatabase);
			}
			return OColumn;
		}

		public List<Database> DatabaseRelationships {
			get {
				return FDatabaseRelationships.Values;
			}
		}

		public String this[String key] {
				get {
					if (!FAdddionalMetadata.ContainsKey(key)) {
						return null;
					}
					return FAdddionalMetadata[key];
				}
		}

		public String Name {
			get {
				return FName;
			}
			set {
				FName = value;
			}
		}

		public String DataType {
			get {
				return FDataType;
			}
			set {
				FDataType = value;
			}
		}

		public String DefaultValue {
			get {
				return FDefaultValue;
			}
			set {
				FDefaultValue = value;
			}
		}

		public int MaxLength {
			get {
				return FMaxLength;
			}
			set {
				FMaxLength = value;
			}
		}

		public bool Nullable {
			get {
				return FNullable;
			}
			set {
				FNullable = value;
			}
		}

		public bool PrimaryKey {
			get {
				return FPrimaryKey;
			}
			set {
				FPrimaryKey = value;
			}
		}

		public bool Required {
			get {
				return !Nullable && String.IsNullOrEmpty(DefaultValue) && !AutoIncrement;
			}
		}

		public bool AutoIncrement {
			get {
				return FAutoIncrement;
			}
			set {
				FAutoIncrement = value;
			}
		}
	}
}
