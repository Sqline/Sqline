// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Schemalizer.Base;

namespace Schemalizer.Model {
	public class Table : IXmlSerializable {
		protected String FTableName;
		protected String FSchemaName;
		protected SchemaModel FModel;
		protected DateTime FCreatedDate = DateTime.MaxValue;
		protected DateTime FLastModifiedDate = DateTime.MinValue;
		protected bool FIsReplicated;
		protected OrderedDictionary<String, Column> FColumns = new OrderedDictionary<String, Column>();
		protected Dictionary<String, String> FAdddionalMetadata = new Dictionary<String, String>();
		protected OrderedDictionary<String, Database> FDatabaseRelationships = new OrderedDictionary<String, Database>();

		public static String GetFullName(String schema, String name) {
			return schema + "." + name;
		}

		internal Table(String schemaName, String tableName, Database database, SchemaModel model) {
			FSchemaName = schemaName;
			FTableName = tableName;
			FModel = model;
			AssignDatabase(database);
		}

		private Table(String schemaName, String tableName, SchemaModel model) {
			FSchemaName = schemaName;
			FTableName = tableName;
			FModel = model;
		}

		internal void AssignDatabase(Database database) {
			if (!FDatabaseRelationships.ContainsKey(database.Name)) {
				FDatabaseRelationships.Add(database.Name, database);
			}
		}

		public Column CreateColumn(String name, Database database) {
			if (FColumns.ContainsKey(name)) {
				Column OColumn = FColumns[name];
				OColumn.AssignDatabase(database);
				return OColumn;
			}
			else {
				Column OColumn = new Column(name, this, FModel, database);
				FColumns.Add(name, OColumn);
				return OColumn;
			}
		}
		
		public void AddAdditionalMetadata(String key, String value) {
			if (!FAdddionalMetadata.ContainsKey(key)) {
				FAdddionalMetadata.Add(key, value);
			}
		}

		public Column GetColumn(String name) {
			if (FColumns.ContainsKey(name)) {
				return FColumns[name];
			}
			return null;
		}

		public XElement ToXElement() {
			XElement OElement = new XElement("Table", 
															new XAttribute("Schema", SchemaName),
															new XAttribute("Name", TableName),
															new XAttribute("Replicated", IsReplicated),
															new XAttribute("CreatedDate", CreatedDate),
															new XAttribute("LastModifiedDate", LastModifiedDate));
			foreach (String OKey in FAdddionalMetadata.Keys) {
				OElement.Add(new XAttribute(OKey, FAdddionalMetadata[OKey]));
			}
			XElement ORelationships = new XElement("DatabaseRelationships");
			OElement.Add(ORelationships);
			foreach (Database ODatabase in FDatabaseRelationships.Values) {
				ORelationships.Add(ODatabase.ToRelationshipXElement());
			}
			foreach (Column OColumn in FColumns.Values) {
				OElement.Add(OColumn.ToXElement());
			}
			return OElement;
		}

		public static Table FromXElement(XElement element, SchemaModel model) {
			Table OTable = new Table(element.Attribute("Schema").Value, element.Attribute("Name").Value, model) {
				FIsReplicated = Convert.ToBoolean(element.Attribute("Replicated").Value),
				FCreatedDate = Convert.ToDateTime(element.Attribute("CreatedDate").Value),
				FLastModifiedDate = Convert.ToDateTime(element.Attribute("LastModifiedDate").Value)
			};
			foreach (XAttribute OAttribute in element.Attributes()) {
				String OKey = OAttribute.Name.LocalName;
				if (OKey != "Schema" && OKey != "Name" && OKey != "Replicated" && OKey != "CreatedDate" && OKey != "LastModifiedDate") {
					OTable.FAdddionalMetadata.Add(OKey, OAttribute.Value);
				}
			}
			var OColumns = from c in element.Descendants("Column") select Column.FromXElement(c, OTable, model);
			
			foreach (Column OColumn in OColumns) {
				OTable.FColumns.Add(OColumn.Name, OColumn);
			}
			var ODatabaseRelationships = from r in element.Element("DatabaseRelationships").Elements("DatabaseRelationship") select r.Attribute("Name").Value;
			foreach (String ODatabaseName in ODatabaseRelationships) {
				Database ODatabase = model.GetDatabase(ODatabaseName);
				if (ODatabase == null) {
					throw new ArgumentException("Invalid database relationship '" + ODatabaseName + "' for table '" + OTable.FullName + "'");
				}
				OTable.FDatabaseRelationships.Add(ODatabase.Name, ODatabase);
			}
			return OTable;
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

		public String SchemaName {
			get {
				return FSchemaName;
			}
		}

		public String TableName {
			get {
				return FTableName;
			}
		}

		public String FullName {
			get {
				return GetFullName(FSchemaName, FTableName);
			}
		}

		public DateTime CreatedDate {
			get {
				return FCreatedDate;
			}
			set {
				if (value < FCreatedDate) {
					FCreatedDate = value;
				}
			}
		}

		public DateTime LastModifiedDate {
			get {
				return FLastModifiedDate;
			}
			set {
				if (value > FLastModifiedDate) {
					FLastModifiedDate = value;
					FModel.QueryLastModifiedDate(FLastModifiedDate);
				}
			}
		}

		public bool IsReplicated {
			get {
				return FIsReplicated;
			}
			set {
				FIsReplicated = value;
			}
		}

		public List<Column> Columns {
			get {
				return FColumns.Values;
			}
		}
	}
}
