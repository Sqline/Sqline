using Sqline.ClientFramework;

namespace Sqline.Tests.DataAccess.PostgreSql
{
	public class DA
	{
		private static TypesHandler FTypesHandler = new TypesHandler();

		public static void Initialize(string connstr, string provider = "SqlServer")
		{
			SqlineApplication OApplication = new SqlineApplication();
			OApplication.Initialize(connstr, provider);
			DAHandler.Initialize(OApplication);
		}

		public static TypesHandler Types
		{
			get
			{
				return FTypesHandler;
			}
		}
	}
}
