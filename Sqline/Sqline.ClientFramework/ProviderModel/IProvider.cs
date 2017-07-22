// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Data;

namespace Sqline.ClientFramework {
    public interface IProvider {
        // Conver DB Types, Parameter Name, Table/Column Escape Name, DB Name, Version, Support
		string ProviderName { get; }
        string IdentifierStartDelimiter { get; }
        string IdentifierEndDelimiter { get; }
        string DelimitName(string name);
        string UndelimitName(string name);
        string GetSafeTableName(string schema, string name);
        string GetSafeColumnName(string name);
        string GetParameterName(string name);
        IDbConnection GetConnection(string connstr);
        ITypeMapping GetTypeMapping(string providerType);
        string GenerateParameterQuery(string prefix, int count);
    }
}
