// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sqline.ClientFramework.ProviderModel.SqlServer {
	public class SqlServerProvider : IProvider {
		private string FConnStr;
		private string FDatabaseName;
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

		public IDbConnection GetConnection(string connstr) {
			return new SqlConnection(connstr);
		}
	}
}
