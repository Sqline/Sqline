using System;
using System.Reflection;

namespace Sqline.ClientFramework {
    public class ProviderFactory {	

		public static IProvider Create(string name) {
            if (name.Equals("PostgreSql", StringComparison.OrdinalIgnoreCase)) {
				Assembly OAssembly = Assembly.Load("Sqline.ProviderModel.PostgreSql");
				Type OType = OAssembly.GetType("Sqline.ProviderModel.PostgreSql.PostgreSqlProvider");
				return (IProvider)Activator.CreateInstance(OType);
			}
			else if (name.Equals("MySql", StringComparison.OrdinalIgnoreCase)) {
				Assembly OAssembly = Assembly.Load("Sqline.ProviderModel.MySql");
				Type OType = OAssembly.GetType("Sqline.ProviderModel.MySql.MySqlProvider");
				return (IProvider)Activator.CreateInstance(OType);
			}
            else if (name.Equals("SqlServer", StringComparison.OrdinalIgnoreCase)) {                
                Assembly OAssembly = Assembly.Load("Sqline.ProviderModel.SqlServer");
				Type OType = OAssembly.GetType("Sqline.ProviderModel.SqlServer.SqlServerProvider");
				return (IProvider)Activator.CreateInstance(OType);
			}
            else {                
                Assembly OAssembly = Assembly.Load($"Sqline.ProviderModel.{name}");
                Type OType = OAssembly.GetType($"Sqline.ProviderModel.{name}.{name}Provider");
                return (IProvider)Activator.CreateInstance(OType);
            }
		}
	}
}