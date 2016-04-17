using Sqline.ClientFramework;
namespace Sqline.Tests.DataAccess {
	public static class DAHandler {
		private static SqlineApplication FSqlineApplication;

		public static void Initialize(SqlineApplication application) {
			FSqlineApplication = application;
		}

		public static SqlineApplication SqlineApplication {
			get {
				return FSqlineApplication;
			}
			private set {
				FSqlineApplication = value;
			}
		}
	}
}