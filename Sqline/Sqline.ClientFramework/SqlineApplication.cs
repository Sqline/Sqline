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
			Provider.Initialize(ProviderFactory.Create(provider));
		}

		private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
			Debug.WriteLine("### Assembly Resolve: " + args.Name);
			Assembly OResourceAssembly = typeof(SqlineApplication).Assembly;
			if (args.Name.Contains("Sqline.ClientFramework"))
			{
				return OResourceAssembly;
			}
			if (args.Name.StartsWith("Sqline.") || args.Name.StartsWith("Npgsql") || args.Name.StartsWith("MySql."))
			{
				return AssemblyResolver.LoadRessourceAssembly(OResourceAssembly, args.Name);
			}
			return null;
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
			return Provider.Current.GetConnection(ConnectionString);
		}
	}
}