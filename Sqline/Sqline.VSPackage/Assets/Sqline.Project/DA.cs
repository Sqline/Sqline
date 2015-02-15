using Sqline.ClientFramework;

namespace $safeprojectname$ {
	public class DA {
		private static UserHandler FUserHandler = new UserHandler();

		public static void Initialize(string connstr) {
			SqlineConfig OConfig = new SqlineConfig { ConnectionString = connstr };
			DAHandler.Initialize(OConfig);
		}

		public static UserHandler User {
			get {
				return FUserHandler;
			}
		}
	}
}
