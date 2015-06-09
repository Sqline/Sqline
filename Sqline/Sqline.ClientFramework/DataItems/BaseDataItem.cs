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
		protected int FParameterIndex = 0;

		public virtual void Initialize(string schemaName, string tableName, SqlineConfig config) {
			FSchemaName = schemaName;
			FTableName = tableName;
			FConfig = config;
		}

		public virtual int Execute() {
			using (IDbConnection OConnection = Provider.Current.GetConnection(FConfig.ConnectionString)) {
				OConnection.Open();
				return Execute(OConnection, null);
			}
		}

		public virtual int Execute(IDbConnection connection, IDbTransaction transaction) {
			FParameters.Clear();
			PreExecute();
			FSqlStatement = PrepareStatement();
			int OResult = 0;
			using (IDbCommand OCommand = connection.CreateCommand()) {
				OCommand.Transaction = transaction;
				OCommand.CommandText = FSqlStatement;
				foreach (IBaseParam OParam in FParameters) {
					if (OParam.HasValue) { /* Is this check really necessary? */
						OParam.AddParameter(OCommand);						
					}
					else {
						throw new Exception("Yes, I think it is necessary");
					}
				}
				OResult = OCommand.ExecuteNonQuery();
			}
			PostExecute(OResult);
			return OResult;
		}

		public virtual void AddParameter(BaseParam param, string columnName) {
			if ((object)param != null) {
				param.Initialize(columnName);
				FParameters.Add(param);
			}
		}

		protected internal abstract void PreExecute();
		protected internal abstract void PostExecute(int modifiedCount);
		protected internal abstract string PrepareStatement();

		/* NOTE: Use Get/Set functions instead of properties to avoid name-clashing with auto-generated properties */

		public List<IBaseParam> GetParameters() {
			return FParameters;
		}

		public string GetSchemaName() {			
			return FSchemaName;			
		}

		public void SetSchemaName(string schemaName) {
			FSchemaName = schemaName;
		}

		public string GetTableName() {			
			return FTableName;
		}

		public void SetTableName(string tableName) {
			FTableName = tableName;
		}

		public int GetParameterIndex() {
			return FParameterIndex;
		}

		public void SetParameterIndex(int index) {
			FParameterIndex = index;
		}

		protected internal SqlineConfig GetSqlineConfig() {
			return FConfig;
		}
	}
}
