
    
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Sqline.ClientFramework;

namespace Socialize.DataAccess {

	public partial class UserHandler {
    
		public List<VUser> GetUsers() {
			List<VUser> OResult = new List<VUser>();
			string sql = @"SELECT ID, Name, Age FROM User";
			using (IDbConnection OConnection = Provider.Current.GetConnection(DA.ConnectionString)) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = sql;
					OCommand.CommandType = CommandType.Text;
					using (IDbDataReader OReader = OCommand.ExecuteReader()) {
						VUser OViewItem = new VUser();
						while (OReader.Read()) {
						
							OViewItem.ID = OReader.GetInt32(ID);
						
							OViewItem.Name = OReader.GetString(Name);
						
							OViewItem.Age = OReader.GetInt32OrDefault("Age", new Func<int?>(() => 12)());
						
							OViewItem.UniqueID = OReader.GetString(UniqueID);
						
						}
						OResult.Add(OViewItem);
					}
				}
			}
			return OResult;
		}

    
		public List<VUser> GetUsers2() {
			List<VUser> OResult = new List<VUser>();
			string sql = @"SELECT ID, Name, Age FROM User";
			using (IDbConnection OConnection = Provider.Current.GetConnection(DA.ConnectionString)) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = sql;
					OCommand.CommandType = CommandType.Text;
					using (IDbDataReader OReader = OCommand.ExecuteReader()) {
						VUser OViewItem = new VUser();
						while (OReader.Read()) {
						
							OViewItem.ID = OReader.GetInt32(ID);
						
							OViewItem.Name = OReader.GetString(Name);
						
							OViewItem.Age = OReader.GetInt32OrDefault("Age", new Func<int?>(() => 12)());
						
							OViewItem.UniqueID = OReader.GetString(UniqueID);
						
						}
						OResult.Add(OViewItem);
					}
				}
			}
			return OResult;
		}

    
	}
}

