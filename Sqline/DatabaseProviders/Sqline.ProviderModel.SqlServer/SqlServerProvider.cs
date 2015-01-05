// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Data;
using System.Data.SqlClient;
using Schemalizer.Model;

namespace Sqline.ProviderModel.SqlServer {
	public class SqlServerProvider : IProvider/*, ISchemalizerProvider*/ {
		private string FConnStr;
		private string FDatabaseName;
		private Database FDatabase;
		private SchemaModel FModel;
		private Types FTypes = new Types();

		public string IdentifierStartDelimiter {
			get {
				return "[";
			}
		}

		public string IdentifierEndDelimiter {
			get {
				return "]";
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

		public string GetSafeTableName(string schemaName, string tableName) {
			return DelimitName(schemaName) + "." + DelimitName(tableName);
		}

		public string GetSafeColumnName(string columnName) {
			return DelimitName(columnName);
		}

		public string GetParameterName(string name) {
			return "@" + name;
		}
		
		public SqlServerProvider(string databaseName, string connstr) {
			FDatabaseName = databaseName;
			FConnStr = connstr;
		}

		public ITypeMapping GetTypeMapping(string providerType) {
			return FTypes.GetTypeMapping(providerType);
		}

		public void ExtractMetadata(SchemaModel model) {
			FModel = model; //Needs restructuring
			FDatabase = FModel.CreateDatabase(FDatabaseName);
			using (SqlConnection OConnection = new SqlConnection(FConnStr)) {
				using (SqlCommand OCommand = new SqlCommand(ExtractSchemaSql, OConnection)) {
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

		public IDbConnection GetConnection(string connstr) {
			return new SqlConnection(connstr);
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
			OColumn.MaxLength = reader.GetInt16(reader.GetOrdinal("MaxLength"));
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
									C.Name AS ColumnName,
									C.Is_Identity AS IsAutoIncrement,
									DT.Name AS DataType,
									C.Max_Length AS MaxLength,
									C.Is_Nullable AS Nullable,
									DC.Definition AS DefaultValue,
									COALESCE((
										SELECT TOP 1 I.is_primary_key FROM sys.index_columns AS IC 
										INNER JOIN sys.indexes AS I ON C.object_id = I.object_id AND IC.Index_ID = I.Index_ID
										WHERE C.object_id = IC.object_id AND IC.Column_ID = C.Column_ID AND I.is_primary_key = 1
									), CAST(0 AS BIT)) AS IsPrimaryKey,
									SCHEMA_NAME(T.schema_id) AS SchemaName,
									T.name AS TableName,
									T.Is_Replicated AS TableIsReplicated,
									T.create_date AS TableCreatedDate,
									T.modify_date AS TableLastModifiedDate
								FROM sys.columns AS C
								INNER JOIN sys.Tables AS T ON C.object_id = T.object_id
								INNER JOIN sys.types AS DT ON C.user_type_id = DT.user_type_id
								LEFT OUTER JOIN sys.default_constraints DC on DC.parent_object_id = C.object_id AND DC.parent_column_id = C.column_id
								ORDER BY C.column_id ASC";
			}
		}
	}
}
