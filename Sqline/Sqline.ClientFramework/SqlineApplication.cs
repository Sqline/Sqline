// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"

using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace Sqline.ClientFramework {
	public class SqlineApplication {
		private string FConnectionString;

		public SqlineApplication() {
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		public void Initialize(string connectionString, string provider = "SqlServer") {
			ConnectionString = connectionString;			
			Sqline.ProviderModel.Provider.Initialize(ProviderFactory.Create(provider));
		}

		private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
			return AssemblyResolver.LoadRessourceAssembly(Assembly.GetExecutingAssembly(), args.Name);
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