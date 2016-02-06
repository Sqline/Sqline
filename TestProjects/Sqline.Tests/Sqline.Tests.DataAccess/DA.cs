using Sqline.ClientFramework;

namespace Sqline.Tests.DataAccess {
	public class DA {
		private static TypesHandler FTypesHandler = new TypesHandler();

		public static void Initialize(string connstr) {
			SqlineConfig OConfig = new SqlineConfig { ConnectionString = connstr };
			DAHandler.Initialize(OConfig);
		}

		public static TypesHandler Types {
			get {
				return FTypesHandler;
			}
		}
	}
}
