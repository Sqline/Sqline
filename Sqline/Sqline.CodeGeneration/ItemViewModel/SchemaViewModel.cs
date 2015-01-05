// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using Schemalizer.Base;
using Schemalizer.Model;

namespace Sqline.CodeGeneration.ViewModel {
	public class SchemaViewModel {
		private OrderedDictionary<String, ViewTable> FTables = new OrderedDictionary<String, ViewTable>();
		private OrderedDictionary<String, ViewDatabase> FDatabaseRelationships = new OrderedDictionary<String, ViewDatabase>();
		private SchemaModel FBase;

		public SchemaViewModel(SchemaModel model) {
			FBase = model;
			foreach (Table OTable in model.Tables) {
				FTables.Add(OTable.FullName, new ViewTable(OTable));
			}
			foreach (Database ODatabase in model.DatabaseRelationships) {
				FDatabaseRelationships.Add(ODatabase.Name, new ViewDatabase(ODatabase));
			}
		}

		public ViewTable GetTable(String schemaName, String tableName) {
			String OFullName = Table.GetFullName(schemaName, tableName);
			if (FTables.ContainsKey(OFullName)) {
				return FTables[OFullName];
			}
			return null;
		}

		public List<ViewTable> Tables {
			get {
				return FTables.Values;
			}
		}

		public List<ViewDatabase> DatabaseRelationships {
			get {
				return FDatabaseRelationships.Values;
			}
		}

		public DateTime LastModified {
			get {
				return FBase.LastModified;
			}
		}
	}
}
