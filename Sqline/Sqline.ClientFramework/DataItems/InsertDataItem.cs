// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public abstract class InsertDataItem : BaseDataItem {

		protected override string PrepareStatement() {
			String OTableName = Provider.Current.GetSafeTableName(SchemaName, TableName);
			StringBuilder OColumns = new StringBuilder();
			StringBuilder OValues = new StringBuilder();
			int OParameterCount = 0;

			foreach (IBaseParam OParam in FParameters) {
				if (OParam is IValueParam || OParam is INumberParam || OParam is IEnumParam) {
					if (OValues.Length > 0) {
						OColumns.Append(",");
						OValues.Append(",");
					}
					string OParameterName = Provider.Current.GetParameterName("p" + OParameterCount++);
					OColumns.Append(Provider.Current.GetSafeColumnName(OParam.ColumnName));
					OValues.Append(OParameterName);
					OParam.ParameterName = OParameterName;
				}
			}

			StringBuilder OSql = new StringBuilder();
			OSql.Append("INSERT INTO ");
			OSql.Append(OTableName);
			OSql.Append(" (");
			OSql.Append(OColumns);
			OSql.Append(") VALUES (");
			OSql.Append(OValues);
			OSql.Append(")");
			return OSql.ToString();
		}

		protected override void PreExecute() {
		}

		protected override void PostExecute(int modifiedCount) {
		}
	}
}
