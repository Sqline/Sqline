// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Linq;
using System.Collections.Generic;
using Schemalizer.Base;
using Schemalizer.Model;

namespace Sqline.CodeGeneration.ViewModel {
	public class ViewTable {
		private OrderedDictionary<String, ViewColumn> FColumns = new OrderedDictionary<String, ViewColumn>();
		private List<ViewColumn> FRequiredColumns = new List<ViewColumn>();
		private OrderedDictionary<String, ViewDatabase> FDatabaseRelationships = new OrderedDictionary<String, ViewDatabase>();
		private Table FBase;
		
		public ViewTable(Table table) {
			FBase = table;
			foreach (Column OColumn in table.Columns) {
				ViewColumn OViewColumn = new ViewColumn(OColumn);
				FColumns.Add(OColumn.Name, OViewColumn);
				if (OViewColumn.Required) {
					FRequiredColumns.Add(OViewColumn);
				}
			}
			foreach (Database ODatabase in table.DatabaseRelationships) {
				FDatabaseRelationships.Add(ODatabase.Name, new ViewDatabase(ODatabase));
			}
		}

		public ViewColumn GetFirstPrimaryKeyColumn() {
			return FColumns.Values.FirstOrDefault(c => c.PrimaryKey);
		}

		public List<ViewDatabase> DatabaseRelationships {
			get {
				return FDatabaseRelationships.Values;
			}
		}

		public List<ViewColumn> Columns {
			get {
				return FColumns.Values;
			}
		}

		public List<ViewColumn> RequiredColumns {
			get {
				return FRequiredColumns;
			}
		}

		public String SchemaName {
			get {
				return FBase.SchemaName;
			}
		}

		public String TableName {
			get {
				return FBase.TableName;
			}
		}

		public String FullName {
			get {
				return FBase.FullName;
			}
		}

		public DateTime CreatedDate {
			get {
				return FBase.CreatedDate;
			}
		}

		public DateTime LastModifiedDate {
			get {
				return FBase.LastModifiedDate;
			}
		}

		public bool IsReplicated {
			get {
				return FBase.IsReplicated;
			}
		}
	}
}
