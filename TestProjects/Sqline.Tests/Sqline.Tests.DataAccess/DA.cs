using Sqline.ClientFramework;
using System.Diagnostics;

namespace Sqline.Tests.DataAccess {
	public class DA {
		private static TypesHandler FTypesHandler = new TypesHandler();

		public static void Initialize(string connstr) {			
			SqlineApplication OConfig = new SqlineApplication();
			OConfig.Initialize(connstr);
			DAHandler.Initialize(OConfig);
		}

		public static TypesHandler Types {
			get {
				return FTypesHandler;
			}
		}
	}
}
