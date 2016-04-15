// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Data;
using Npgsql;
using Schemalizer.Model;

namespace Schemalizer.Provider.PostgreSql {
	public class MySqlProvider : ISchemalizerProvider {
		private string FConnStr;
		private Database FDatabase;

		public MySqlProvider(string connstr) {
			FConnStr = connstr;
		}

		public bool HasSchemaChanged(SchemaModel model) {
			return true; //TODO: Compare to DB change date
		}

		public void ExtractMetadata(SchemaModel model, string databaseName) {
			FDatabase = model.CreateDatabase(databaseName);
			using (NpgsqlConnection OConnection = new NpgsqlConnection(FConnStr)) {
				using (NpgsqlCommand OCommand = new NpgsqlCommand(ExtractSchemaSql, OConnection)) {
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						while (OReader.Read()) {
							Table OTable = GetTableData(model, OReader);
							AddColumnData(OTable, OReader);
						}
					}
				}
			}
		}

		private Table GetTableData(SchemaModel model, IDataReader reader) {
			string OSchemaName = reader.GetString(reader.GetOrdinal("SchemaName"));
			string OTableName = reader.GetString(reader.GetOrdinal("TableName"));
			Table OTable = model.CreateTable(OSchemaName, OTableName, FDatabase);
			OTable.CreatedDate = reader.GetDateTime(reader.GetOrdinal("TableCreatedDate"));
			OTable.LastModifiedDate = reader.GetDateTime(reader.GetOrdinal("TableLastModifiedDate"));
			OTable.IsReplicated = reader.GetBoolean(reader.GetOrdinal("TableIsReplicated"));
			return OTable;
		}

		private void AddColumnData(Table table, IDataReader reader) {
			string OColumnName = reader.GetString(reader.GetOrdinal("ColumnName"));
			Column OColumn = table.CreateColumn(OColumnName, FDatabase);
			OColumn.AutoIncrement = reader.GetBoolean(reader.GetOrdinal("IsAutoIncrement"));
			OColumn.DataType = reader.GetString(reader.GetOrdinal("DataType"));
			OColumn.MaxLength = reader.GetInt32(reader.GetOrdinal("MaxLength"));
			OColumn.Nullable = reader.GetBoolean(reader.GetOrdinal("Nullable"));
			if (!reader.IsDBNull(reader.GetOrdinal("DefaultValue"))) {
				OColumn.DefaultValue = reader.GetString(reader.GetOrdinal("DefaultValue"));
			}
			else {
				OColumn.DefaultValue = null;
			}
			OColumn.PrimaryKey = reader.GetBoolean(reader.GetOrdinal("IsPrimaryKey"));
		}

		public string ExtractSchemaSql {
			get {
				return @"SELECT 
							C.column_name AS ColumnName,
							C.is_identity AS IsAutoIncrement,
							C.data_type AS DataType,
							C.character_maximum_length AS MaxLength,
							C.is_nullable AS IsNullable,
							C.column_default AS DefaultValue,
							(CASE TC.constraint_type 
								   WHEN 'PRIMARY KEY' THEN 1
								   ELSE 0 
								  END) AS IsPrimaryKey,
							T.table_schema AS SchemaName,
							T.table_name AS TableName,
							0 AS TableIsReplicated /* Not supported in this release - please let us know if you find a reliable way to determine if a table takes part (either as Master or Slave) in Replication */
							NULL AS TableCreatedDate, /* Not properly supported by PostgreSql (!) */
							NULL AS TableLastModifiedDate /* Not properly supported by PostgreSql (!) */
						FROM information_schema.tables T
						INNER JOIN information_schema.columns C ON C.table_schema = T.table_schema AND C.table_name = T.table_name
						INNER JOIN information_schema.table_constraints TC ON TC.constraint_schema = C.table_schema AND TC.table_name = C.table_name
						WHERE 
							T.table_schema NOT IN ('information_schema', 'pg_catalog') AND
							T.table_type = 'BASE TABLE'";
			}
		}
	}
}