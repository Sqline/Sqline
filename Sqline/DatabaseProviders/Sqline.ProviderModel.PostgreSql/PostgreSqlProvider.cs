// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Data;
using Schemalizer.Model;
using Npgsql;
using System.Reflection;
using System.Text;
using Sqline.ClientFramework;

namespace Sqline.ProviderModel.PostgreSql {
    public class PostgreSqlProvider : IProvider {
        private Types FTypes = new Types(Assembly.GetExecutingAssembly(), "Sqline.ProviderModel.PostgreSql.Types.xml");
		public string ProviderName { get; } = "PostgreSql";

		public string IdentifierStartDelimiter {
            get {
                return "\"";
            }
        }

        public string IdentifierEndDelimiter {
            get {
                return "\"";
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

        public ITypeMapping GetTypeMapping(string providerType) {
            return FTypes.GetTypeMapping(providerType);
        }

        public IDbConnection GetConnection(string connstr) {
            return new NpgsqlConnection(connstr);
        }

        public string GenerateParameterQuery(string prefix, int count) {
            StringBuilder OResult = new StringBuilder();
            for (int i = 0; i < count; i++) {
                if (i > 0) {
                    OResult.Append(",");
                }
                OResult.Append("@" + prefix + i);
            }
            return OResult.ToString();
        }
    }
}