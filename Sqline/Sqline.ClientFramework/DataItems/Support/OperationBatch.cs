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
	public class OperationBatch<T> where T : BaseDataItem {
		private List<T> FOperations = new List<T>();
		private int FBatchSize = 100;

		public OperationBatch() {
		}

		public OperationBatch(int batchSize) {
			FBatchSize = batchSize;
		}

		public void Add(T operation) {
			FOperations.Add(operation);
		}

		private bool IsAllInserts() {
			if (FOperations.Count == 0) {
				return false;
			}
			String OName = FOperations[0].GetType().FullName;
			foreach (T OOperation in FOperations) {
				if (OOperation.GetType().FullName != OName) {
					throw new Exception("All operations added to OperationBatch<T> must be of the same type: " + OOperation.GetType().FullName + " != " + OName);
				}
				if (!(OOperation is InsertDataItem)) {
					return false;
				}
			}
			return true;
		}

		public int Execute() {
			using (IDbConnection OConnection = Provider.Current.GetConnection(FOperations[0].GetSqlineConfig().ConnectionString)) {
				OConnection.Open();
				return Execute(OConnection, null);
			}
		}

		public int Execute(IDbConnection connection, IDbTransaction transaction) {
			using (IDbCommand OCommand = connection.CreateCommand()) {
				OCommand.Transaction = transaction;

				int OParameterIndex = 0;
				foreach (T OOperation in FOperations) {
					OOperation.SetParameterIndex(OParameterIndex);
					OOperation.PreExecute();
					OParameterIndex = OOperation.GetParameterIndex();
					foreach (IBaseParam OParam in OOperation.GetParameters()) {
						if (OParam.HasValue) { /* Is this check really necessary? */
							OParam.AddParameter(OCommand);
						}
						else {
							throw new Exception("Yes, I think it is necessary");
						}
					}
				}
				String OSql = IsAllInserts() ? PrepareAllInsertStatement() : PrepareAppendedStatement();
				Console.Write(OSql);

				OCommand.CommandText = OSql;
				int OResult = OCommand.ExecuteNonQuery();

				foreach (T OOperation in FOperations) {
					OOperation.PostExecute(-1);
				}
				return OResult;
			}
		}

		private string PrepareAllInsertStatement() {
			InsertDataItem OFirstItem = FOperations[0] as InsertDataItem;
			StringBuilder OSql = new StringBuilder();
			OSql.Append("INSERT INTO ");
			OSql.Append(Provider.Current.GetSafeTableName(OFirstItem.GetSchemaName(), OFirstItem.GetTableName()));
			OSql.Append(" (");
			OSql.Append(OFirstItem.GetSqlColumns());
			OSql.Append(") VALUES ");
			for (int i = 0; i < FOperations.Count; i++) {
				InsertDataItem OInsertItem = FOperations[i] as InsertDataItem;
				if (i > 0) {
					OSql.Append(",\r\n");
				}
				OSql.Append("(");
				OSql.Append(OInsertItem.GetSqlValues());
				OSql.Append(")");
			}
			return OSql.ToString();
		}

		private string PrepareAppendedStatement() {
			StringBuilder OSql = new StringBuilder();
			foreach (BaseDataItem OItem in FOperations) {
				OSql.Append(OItem.PrepareStatement());
				OSql.Append(";\r\n");
			}
			return OSql.ToString();
		}
	}
}