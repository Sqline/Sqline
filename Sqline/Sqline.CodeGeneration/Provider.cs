// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using Sqline.ClientFramework.ProviderModel;
using Sqline.ClientFramework.ProviderModel.SqlServer;

namespace Sqline.CodeGeneration {
	public class Provider {
		public static IProvider Current = new SqlServerProvider("", ""); //HACK: Needs refactoring
	}
}
