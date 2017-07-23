using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Sqline.ClientFramework;
using Sqline.Tests.DataAccess.PostgreSql.ViewItems;
namespace Sqline.Tests.DataAccess.PostgreSql {

	public partial class TypesHandler {

		public int GetInt() {
			int OResult = default(int);
			string OSql = @"SELECT ""IntegerColumn"" FROM ""public"".""TypeTest"" WHERE ""IntegerColumn"" IS NOT NULL";
			using (IDbConnection OConnection = Sqline.Tests.DataAccess.PostgreSql.DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							int OScalarItem = OReader.GetInt32(0);
							OResult = OScalarItem;
						}
					}
				}
			}
			return OResult;
		}
	}
}
