// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public abstract class DeleteDataItem : BaseDataItem {

		protected override string PrepareStatement() {
			String OTableName = Provider.Current.GetSafeTableName(SchemaName, TableName);
			StringBuilder OWheres = new StringBuilder();
			int OParameterCount = 0;

			foreach (IBaseParam OParam in FParameters) {
				if (OParam is IWhereParam) {
					if (OWheres.Length > 0) {
						OWheres.Append(",");
					}
					string OParameterName = Provider.Current.GetParameterName("p" + OParameterCount++);
					OWheres.Append(OParam.GetStatement(Provider.Current.GetSafeColumnName(OParam.ColumnName), OParameterName));
					OParam.ParameterName = OParameterName;
				}
			}

			if (OWheres.Length == 0) {
				throw new Exception("Unsafe DELETE statement, no wheres specified!");
			}

			StringBuilder OSql = new StringBuilder();
			OSql.Append("DELETE FROM ");
			OSql.Append(OTableName);
			OSql.Append(" WHERE ");
			OSql.Append(OWheres);
			return OSql.ToString();
		}

		protected override void PreExecute() {
		}

		protected override void PostExecute(int modifiedCount) {
		}
	}
}
