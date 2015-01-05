// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public abstract class BaseDataItem {
		protected SqlineConfig FConfig;
		protected List<IBaseParam> FParameters = new List<IBaseParam>();
		protected string FTableName = "";
		protected string FSchemaName = "";
		protected string FSqlStatement = "";

		public void Initialize(string schemaName, string tableName, SqlineConfig config) {
			FSchemaName = schemaName;
			FTableName = tableName;
			FConfig = config;
		}
	
		public int Execute() {
			FParameters.Clear();
			PreExecute();
			FSqlStatement = PrepareStatement();
			int OResult = 0;
			using (IDbConnection OConnection = Provider.Current.GetConnection(FConfig.ConnectionString)) {
				using (IDbCommand OCommand = OConnection.CreateCommand()) {
					OCommand.CommandText = FSqlStatement;
					foreach (IBaseParam OParam in FParameters) {
						if (OParam.HasValue) {
							IDbDataParameter OParameter = OCommand.CreateParameter();
							OParameter.ParameterName = OParam.ParameterName;
							OParameter.Value = OParam.Value;
							OCommand.Parameters.Add(OParameter);
						}
					}
					OConnection.Open();
					OResult = OCommand.ExecuteNonQuery();
				}
			}
			PostExecute(OResult);
			return OResult;
		}

		public void AddParameter(BaseParam param, string columnName) {
			if ((object)param != null) {
				param.Initialize(columnName);
				FParameters.Add(param);
			}
		}

		protected abstract void PreExecute();
		protected abstract void PostExecute(int modifiedCount);
		protected abstract string PrepareStatement();
		
		public string SchemaName {
			get {
				return FSchemaName;
			}
			set {
				FSchemaName = value;
			}
		}

		public string TableName {
			get {
				return FTableName;
			}
			set {
				FTableName = value;
			}
		}
	}
}
