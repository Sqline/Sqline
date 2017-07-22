// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using Sqline.ClientFramework;
using ISchemalizerProvider = Schemalizer.ProviderModel.ISchemalizerProvider;
using ProviderFactory = Schemalizer.ProviderModel.ProviderFactory;

namespace Sqline.ConsoleApp {
	public class ProviderTest {
		public void Execute() {
			SqlineApplication OApplication1 = new SqlineApplication();
			OApplication1.Initialize("", "SqlServer");
			Console.WriteLine(Provider.Current.IdentifierStartDelimiter);

			SqlineApplication OApplication2 = new SqlineApplication();
			OApplication2.Initialize("", "MySql");
			Console.WriteLine(Provider.Current.IdentifierStartDelimiter);

			SqlineApplication OApplication3 = new SqlineApplication();
			OApplication3.Initialize("", "PostgreSql");
			Console.WriteLine(Provider.Current.IdentifierStartDelimiter);

			ISchemalizerProvider OProvider = ProviderFactory.Create("SqlServer");
			Console.WriteLine(OProvider.HasSchemaChanged(null));

			OProvider = ProviderFactory.Create("MySql");
			Console.WriteLine(OProvider.HasSchemaChanged(null));

			OProvider = ProviderFactory.Create("PostgreSql");
			Console.WriteLine(OProvider.HasSchemaChanged(null));
		}
	}
}