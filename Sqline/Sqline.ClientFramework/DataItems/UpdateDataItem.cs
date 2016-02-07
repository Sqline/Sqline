// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public abstract class UpdateDataItem : QueryableDataItem {

		public UpdateDataItem() : base(true) {

		}

		protected override string GetQueryableStatement(string tableName, string valueParameters, string whereParameters) {
			if (whereParameters.Length == 0 && !FAllowUnsafeQuery) {
				throw new Exception("Unsafe UPDATE statement, no wheres specified!");
			}

			StringBuilder OSql = new StringBuilder();
			OSql.Append("UPDATE ");
			if (FTopNumberOfRecords > 0) {
				OSql.Append("TOP(" + FTopNumberOfRecords + ") ");
			}
			OSql.Append(tableName);
			OSql.Append(" SET ");
			OSql.Append(valueParameters);
			if (whereParameters.Length > 0) {
				OSql.Append(" WHERE ");
				OSql.Append(whereParameters);
			}
			return OSql.ToString();
		}
	}
}
