using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schemalizer.Base {
	public static class SqlConnectionExtensions {

		public static void QuickOpen(this SqlConnection conn, int timeoutSeconds) {
			timeoutSeconds = timeoutSeconds * 1000;
			Stopwatch OStopWatch = new Stopwatch();
			bool OConnectSuccess = false;
			Thread OThread = new Thread(delegate() {
				try {
					OStopWatch.Start();
					conn.Open();
					OConnectSuccess = true;
				}
				catch { }
			});
			OThread.IsBackground = true;
			OThread.Start();
			while (timeoutSeconds > OStopWatch.ElapsedMilliseconds) {
				if (OThread.Join(1)) {
					break;
				}
			}			
			if (!OConnectSuccess) {				
				throw new Exception("Timed out while trying to establish a connection");
			}
		}
	}
}
