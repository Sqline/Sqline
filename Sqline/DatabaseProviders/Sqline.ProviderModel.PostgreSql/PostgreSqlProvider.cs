// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Data;
using Schemalizer.Model;
using Npgsql;

namespace Sqline.ProviderModel.PostgreSql {
	public class PostgreSqlProvider : IProvider, ISchemalizerProvider {
		private string FConnStr;
		private string FDatabaseName;
		private Database FDatabase;
		private SchemaModel FModel;

		public string IdentifierStartDelimiter { 
			get {
				return "`";
			} 
		}

		public string IdentifierEndDelimiter {
			get {
				return "`";
			}
		}

		public string DelimitName(string name) {
			return IdentifierStartDelimiter + name + IdentifierEndDelimiter;
		}

		public string UndelimitName(string name) {
			if (name.StartsWith(IdentifierStartDelimiter) && name.EndsWith(IdentifierEndDelimiter)) {
				return name.Substring(1, name.Length - 2); //TODO: Not testet
			}
			return name;
		}

		public PostgreSqlProvider(SchemaModel model, string databaseName, string connstr) {
			FModel = model;
			FDatabaseName = databaseName;
			FConnStr = connstr;
		}

		public ITypeMapping GetTypeMapping(string providerType) {
			throw new NotImplementedException();
		}

		public void ExtractMetadata() {
			FDatabase = FModel.CreateDatabase(FDatabaseName);
			using (NpgsqlConnection OConnection = new NpgsqlConnection(FConnStr)) {
				using (NpgsqlCommand OCommand = new NpgsqlCommand(ExtractSchemaSql, OConnection)) {
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						while (OReader.Read()) {
							Table OTable = GetTableData(OReader);
							AddColumnData(OTable, OReader);
						}
					}
				}
			}
		}


		private Table GetTableData(IDataReader reader) {
			String OSchemaName = reader.GetString(reader.GetOrdinal("SchemaName"));
			String OTableName = reader.GetString(reader.GetOrdinal("TableName"));
			Table OTable = FModel.CreateTable(OSchemaName, OTableName, FDatabase);
			OTable.CreatedDate = reader.GetDateTime(reader.GetOrdinal("TableCreatedDate"));
			OTable.LastModifiedDate = reader.GetDateTime(reader.GetOrdinal("TableLastModifiedDate"));
			OTable.IsReplicated = reader.GetBoolean(reader.GetOrdinal("TableIsReplicated"));
			return OTable;
		}

		private void AddColumnData(Table table, IDataReader reader) {
			String OColumnName = reader.GetString(reader.GetOrdinal("ColumnName"));
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
										0 AS TableIsReplicated, /* Not supported in this release - please let us know if you find a reliable way to determine if a table takes part (either as Master og Slave) in Replication */
										LEAST(COALESCE(T.CREATE_TIME, T.UPDATE_TIME), COALESCE(T.CREATE_TIME, T.UPDATE_TIME)) AS TableCreatedDate, /* Unfortunately does not represent the true creation date in MySQL */
										GREATEST(COALESCE(T.UPDATE_TIME, T.CREATE_TIME), COALESCE(T.UPDATE_TIME, T.CREATE_TIME)) AS TableLastModifiedDate /* For innodb Update_Time is represented by the Create_Time column (!) */
										FROM INFORMATION_SCHEMA.COLUMNS AS C
										INNER JOIN INFORMATION_SCHEMA.TABLES AS T ON T.TABLE_NAME = C.TABLE_NAME AND T.TABLE_SCHEMA = C.TABLE_SCHEMA
										WHERE T.TABLE_SCHEMA = Database()";
			}
		}
	}
}