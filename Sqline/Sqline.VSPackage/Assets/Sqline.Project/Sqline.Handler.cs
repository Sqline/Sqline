using Sqline.ClientFramework;

namespace $safeprojectname$ {
	public static class DAHandler {
		private static SqlineConfig FSqlineConfig;

		public static void Initialize(SqlineConfig config) {
			FSqlineConfig = config;
		}

		public static SqlineConfig SqlineConfig {
			get {
				return FSqlineConfig;
			}
			private set {
				FSqlineConfig = value;
			}
		}
	}
}