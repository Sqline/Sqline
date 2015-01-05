// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public abstract class UpdateDataItem : BaseDataItem {

		protected override string PrepareStatement() {
			String OTableName = Provider.Current.GetSafeTableName(SchemaName, TableName);
			StringBuilder OValues = new StringBuilder();
			StringBuilder OWheres = new StringBuilder();
			int OParameterCount = 0;

			foreach (IBaseParam OParam in FParameters) {
				if (OParam is IValueParam || OParam is INumberParam || OParam is IEnumParam) {
					if (OValues.Length > 0) {
						OValues.Append(",");
					}
					string OParameterName = Provider.Current.GetParameterName("p" + OParameterCount++);
					OValues.Append(OParam.GetStatement(Provider.Current.GetSafeColumnName(OParam.ColumnName), OParameterName));
					OParam.ParameterName = OParameterName;
				}
				if (OParam is IWhereParam) {
					if (OWheres.Length > 0) {
						OWheres.Append(",");
					}
					string OParameterName = Provider.Current.GetParameterName("p" + OParameterCount++);
					OWheres.Append(OParam.GetStatement(Provider.Current.GetSafeColumnName(OParam.ColumnName), OParameterName));
					OParam.ParameterName = OParameterName;
				}
			}

			StringBuilder OSql = new StringBuilder();
			OSql.Append("UPDATE ");
			OSql.Append(OTableName);
			OSql.Append(" SET ");
			OSql.Append(OValues);
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
