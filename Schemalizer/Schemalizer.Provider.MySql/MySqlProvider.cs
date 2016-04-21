// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Data;
using MySql.Data.MySqlClient;
using Schemalizer.Model;

namespace Schemalizer.ProviderModel.MySql {
	public class MySqlProvider : ISchemalizerProvider {
		private Database FDatabase;
		public string ConnectionString { get; set; }

		public bool HasSchemaChanged(SchemaModel model) {
			return true; //TODO: Compare to DB change date
		}

		public void ExtractMetadata(SchemaModel model, string databaseName) {
			FDatabase = model.CreateDatabase(databaseName);
			using (MySqlConnection OConnection = new MySqlConnection(ConnectionString)) {
				using (MySqlCommand OCommand = new MySqlCommand(ExtractSchemaSql, OConnection)) {
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
							C.COLUMN_NAME AS ColumnName,
							IF(C.EXTRA = 'auto_increment', 1, 0) AS IsAutoIncrement,
							C.DATA_TYPE AS DataType,
							IFNULL(C.CHARACTER_MAXIMUM_LENGTH, 0) AS MaxLength,
							IF(C.IS_NULLABLE='YES', 1, 0) AS Nullable,
							C.COLUMN_DEFAULT AS DefaultValue,
							IF(C.COLUMN_KEY = 'PRI', 1, 0) AS IsPrimaryKey,
							T.TABLE_SCHEMA AS SchemaName,
							T.TABLE_NAME AS TableName,
							0 AS TableIsReplicated, /* Not supported in this release - please let us know if you find a reliable way to determine if a table takes part (either as Master or Slave) in Replication */
							LEAST(COALESCE(T.CREATE_TIME, T.UPDATE_TIME), COALESCE(T.CREATE_TIME, T.UPDATE_TIME)) AS TableCreatedDate, /* Unfortunately does not represent the true creation date in MySQL */
							GREATEST(COALESCE(T.UPDATE_TIME, T.CREATE_TIME), COALESCE(T.UPDATE_TIME, T.CREATE_TIME)) AS TableLastModifiedDate /* For innodb Update_Time is represented by the Create_Time column (!) */
						FROM INFORMATION_SCHEMA.COLUMNS AS C
						INNER JOIN INFORMATION_SCHEMA.TABLES AS T ON T.TABLE_NAME = C.TABLE_NAME AND T.TABLE_SCHEMA = C.TABLE_SCHEMA
						WHERE T.TABLE_SCHEMA = Database()";
			}
		}
	}
}