// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Schemalizer.Base;

namespace Schemalizer.Model {
	public class SchemaModel : IXmlSerializable {
		protected OrderedDictionary<String, Table> FTables = new OrderedDictionary<String, Table>();
		protected OrderedDictionary<String, Database> FDatabases = new OrderedDictionary<String, Database>();
		protected Dictionary<String, String> FAdddionalMetadata = new Dictionary<String, String>();
		protected DateTime FLastModified = DateTime.MinValue;

		public Table CreateTable(String schemaName, String tableName, Database database) {
			String OFullName = Table.GetFullName(schemaName, tableName);
			if (FTables.ContainsKey(OFullName)) {
				Table OTable = FTables[OFullName];
				OTable.AssignDatabase(database);
				return OTable;
			}
			else {
				Table OTable = new Table(schemaName, tableName, database, this);
				FTables.Add(OTable.FullName, OTable);
				return OTable;
			}
		}

		public Database CreateDatabase(String name) {
			if (FTables.ContainsKey(name)) {
				throw new ArgumentException("Schema Model already contains a database named '" + name + "'");
			}
			Database ODatabase = new Database(name, this);
			FDatabases.Add(name, ODatabase);
			return ODatabase;
		}

		public void AddAdditionalMetadata(String key, String value) {
			if (!FAdddionalMetadata.ContainsKey(key)) {
				FAdddionalMetadata.Add(key, value);
			}
		}

		internal void QueryLastModifiedDate(DateTime lastModifiedDate) {
			if (lastModifiedDate > FLastModified) {
				FLastModified = lastModifiedDate;
			}
		}

		public XElement ToXElement() {
			XElement OSChemaElement = new XElement("Model");
			XElement OTablesElement = new XElement("Tables");
			XElement ODatabasesElement = new XElement("Databases");
			OSChemaElement.Add(OTablesElement);
			OSChemaElement.Add(ODatabasesElement);
			foreach (String OKey in FAdddionalMetadata.Keys) {
				OSChemaElement.Add(new XAttribute(OKey, FAdddionalMetadata[OKey]));
			}
			foreach (Table OTable in FTables.Values) {
				OTablesElement.Add(OTable.ToXElement());
			}
			foreach (Database ODatabase in FDatabases.Values) {
				ODatabasesElement.Add(ODatabase.ToXElement());
			}
			return OSChemaElement;
		}

		public static SchemaModel FromXElement(XElement element) {
			SchemaModel OModel = new SchemaModel();
			var ODatabases = from db in element.Descendants("Database") select Database.FromXElement(db, OModel);
			foreach (Database ODatabase in ODatabases) {
				OModel.FDatabases.Add(ODatabase.Name, ODatabase);
			}
			var OTables = from t in element.Descendants("Table") select Table.FromXElement(t, OModel);
			foreach (Table OTable in OTables) {
				OModel.FTables.Add(OTable.FullName, OTable);
			}
			foreach (XAttribute OAttribute in element.Attributes()) {
				String OKey = OAttribute.Name.LocalName;
					OModel.FAdddionalMetadata.Add(OKey, OAttribute.Value);
			}
			return OModel;
		}

		public static SchemaModel Load(String databaseFilePath) {
			FileInfo OFileInfo = new FileInfo(databaseFilePath);
			if (OFileInfo.Exists) {
				return FromXElement(XElement.Load(OFileInfo.FullName));
			}
			return null;
		}

		public Table GetTable(String schemaName, String tableName) {
			String OFullName = Table.GetFullName(schemaName, tableName);
			if (FTables.ContainsKey(OFullName)) {
				return FTables[OFullName];
			}
			return null;
		}

		public Database GetDatabase(String name) {
			if (FDatabases.ContainsKey(name)) {
				return FDatabases[name];
			}
			return null;
		}

		public List<Database> DatabaseRelationships {
			get {
				return FDatabases.Values;
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

		public List<Table> Tables {
			get {
				return FTables.Values;
			}
		}

		public DateTime LastModified {
			get {
				return FLastModified;
			}
		}
	}
}
