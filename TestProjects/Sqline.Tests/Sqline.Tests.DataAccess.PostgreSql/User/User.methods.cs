using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Sqline.ClientFramework;
using Sqline.Tests.DataAccess.PostgreSql.ViewItems;
namespace Sqline.Tests.DataAccess.PostgreSql {

	public partial class UserHandler {

		public List<User> GetUsers() {
			List<User> OResult = new List<User>();
			string OSql = @"SELECT Name, EMail FROM [User]";
			using (IDbConnection OConnection = Sqline.Tests.DataAccess.PostgreSql.DAHandler.SqlineApplication.GetConnection()) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = OSql;
					OCommand.CommandType = CommandType.Text;
					OCommand.CommandTimeout = 30;
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						int ONameOrdinal = OReader.GetIndex("Name");
						int OEmailOrdinal = OReader.GetIndex("Email");
						while (OReader.Read()) {
						User OViewItem = new User();
						OViewItem.PreInitialize();
							OViewItem.Name = OReader.GetDateTime(ONameOrdinal);
							OViewItem.Email = OReader.GetInt32(OEmailOrdinal);
						OViewItem.PostInitialize();
							OResult.Add(OViewItem);
						}
					}
				}
			}
			return OResult;
		}
	}
}
