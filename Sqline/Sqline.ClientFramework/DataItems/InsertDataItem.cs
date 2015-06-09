// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;
using Sqline.ClientFramework.ProviderModel.SqlServer;

namespace Sqline.ClientFramework {
	public abstract class InsertDataItem : BaseDataItem {
		private string FColumns;
		private string FValues;
		private bool FFetchPrimaryKeyValueAfterInsert = true;
		private string FPrimaryKeyColumn;
		private DbType FPrimaryKeyType;
		protected object FInsertedPKValue;

		protected internal void SetPrimaryKeyInfo(string columnName, DbType type) {
			FPrimaryKeyColumn = columnName;
			FPrimaryKeyType = type;
		}

		protected internal override void PreExecute() {
			StringBuilder OColumns = new StringBuilder();
			StringBuilder OValues = new StringBuilder();

			foreach (IBaseParam OParam in FParameters) {
				if (OParam is IValueParam || OParam is INumberParam || OParam is IEnumParam) {
					if (OValues.Length > 0) {
						OColumns.Append(",");
						OValues.Append(",");
					}
					string OParameterName = Provider.Current.GetParameterName("p" + FParameterIndex++);
					OColumns.Append(Provider.Current.GetSafeColumnName(OParam.ColumnName));
					OValues.Append(OParameterName);
					OParam.ParameterName = OParameterName;
				}
			}
			FColumns = OColumns.ToString();
			FValues = OValues.ToString();
		}

		protected internal override string PrepareStatement() {
			StringBuilder OSql = new StringBuilder();
			OSql.Append("INSERT INTO ");
			OSql.Append(Provider.Current.GetSafeTableName(GetSchemaName(), GetTableName()));
			OSql.Append(" (");
			OSql.Append(FColumns);
			OSql.Append(") ");
			if (Provider.Current is SqlServerProvider && FFetchPrimaryKeyValueAfterInsert) {
				/* TODO: The responsibility of returning the inserted PK value should be implemented in the provider instead */
				OSql.Append("OUTPUT inserted.ID");
				OSql.Append(FPrimaryKeyColumn);
				OSql.Append(", 1 ");
			}
			OSql.Append("VALUES (");
			OSql.Append(FValues);
			OSql.Append(")");
			return OSql.ToString();
		}

		public override int Execute(IDbConnection connection, IDbTransaction transaction) {
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
				if (Provider.Current is SqlServerProvider && FFetchPrimaryKeyValueAfterInsert) {
					/* TODO: The responsibility of returning the inserted PK value should be implemented in the provider instead */
					using (IDataReader OReader = OCommand.ExecuteReader()) {
						if (OReader.Read()) {
							FInsertedPKValue = OReader.GetValue(0);
							OResult = OReader.GetInt32(1);
						}
					}
					using (IDbCommand OAffectedCommand = connection.CreateCommand()) {
						OAffectedCommand.Transaction = transaction;
						OAffectedCommand.CommandText = "";
					}
				}
				else {
					OResult = OCommand.ExecuteNonQuery();
				}
			}
			PostExecute(OResult);
			return OResult;
		}

		protected internal override void PostExecute(int modifiedCount) {
		}

		public void SetFetchPrimaryKeyValueAfterInsert(bool value) {
			FFetchPrimaryKeyValueAfterInsert = value;
		}

		public string GetSqlColumns() {
			return FColumns;
		}

		public string GetSqlValues() {
			return FValues;
		}
	}
}
