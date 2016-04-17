// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Data;
using System.Diagnostics;

namespace Sqline.ClientFramework {
	public class SqlineApplication {
		private string FConnectionString;

        public SqlineApplication() {
			Debug.WriteLine("SqlineConfig.ctor()");
			AssemblyResolver.Initialize();			
        }

		public void Initialize(string connectionString) {
			ConnectionString = connectionString;
			Sqline.ProviderModel.Provider.Initialize(new Sqline.ProviderModel.SqlServer.SqlServerProvider()); //TODO: Should not be tied to SqlServer
		}

		public string ConnectionString {
			get {
				return FConnectionString;
			}
			set {
				FConnectionString = value;
			}
		}

		public IDbConnection GetConnection() {
			return Sqline.ProviderModel.Provider.Current.GetConnection(ConnectionString);
		}
	}
}