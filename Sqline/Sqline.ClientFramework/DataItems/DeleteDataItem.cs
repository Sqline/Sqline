// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
    public abstract class DeleteDataItem : QueryableDataItem {

		public DeleteDataItem() : base(false) {

		}

		protected override string GetQueryableStatement(string tableName, string valueParameters, string whereParameters) {
			if (whereParameters.Length == 0 && !FAllowUnsafeQuery) {
				throw new Exception("Unsafe DELETE statement, no wheres specified!");
			}

			StringBuilder OSql = new StringBuilder();
			OSql.Append("DELETE ");
			if (FTopNumberOfRecords > 0) {
				OSql.Append("TOP(" + FTopNumberOfRecords + ") ");
			}
			OSql.Append("FROM ");
			OSql.Append(tableName);
			if (whereParameters.Length > 0) {
				OSql.Append(" WHERE ");
				OSql.Append(whereParameters);
			}
			return OSql.ToString();
		}
	}
}
