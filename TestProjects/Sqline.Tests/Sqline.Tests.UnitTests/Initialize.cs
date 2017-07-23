using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Sqline.Tests.UnitTests {

	[TestClass]
	public static class Initialize {

		[AssemblyInitialize]
		public static void InitializeTests(TestContext context) {
			DataAccess.SqlServer.DA.Initialize(ConfigurationManager.ConnectionStrings["TestDB_SqlServer"].ConnectionString, "SqlServer");
			//DataAccess.PostgreSql.DA.Initialize(ConfigurationManager.ConnectionStrings["TestDB_PostgreSql"].ConnectionString, "PostgreSql");
		}
	}
}
