using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Sqline.ClientFramework;
using Sqline.ClientFramework.ProviderModel;


using $safeprojectname$.ViewItems;


namespace $safeprojectname$ {

	public partial class UserHandler {

	
		public List<VUser> GetUsers() {
			List<VUser> OResult = new List<VUser>();
			string sql = @"SELECT Name, EMail FROM [User]";
			using (IDbConnection OConnection = Provider.Current.GetConnection($safeprojectname$.DAHandler.SqlineConfig.ConnectionString)) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = sql;
					OCommand.CommandType = CommandType.Text;
					
					OCommand.CommandTimeout = 5;
					
					
					OConnection.Open();
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						
						int ONameOrdinal = OReader.GetIndex("Name");
						
						
						int OEmailOrdinal = OReader.GetIndex("Email");
						
						
						while (OReader.Read()) {
						VUser OViewItem = new VUser();
						
							OViewItem.Name = OReader.GetString(ONameOrdinal);
						
							OViewItem.Email = OReader.GetString(OEmailOrdinal);
						
						
							
							
						
							
							
						
							OResult.Add(OViewItem);
						}
					}
				}
			}
			return OResult;
		}

	
	}
}

