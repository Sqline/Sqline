// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public abstract class InsertDataItem : BaseDataItem {
		private string FColumns;
		private string FValues;

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
			OSql.Append(") VALUES (");
			OSql.Append(FValues);
			OSql.Append(")");
			return OSql.ToString();
		}

		protected internal override void PostExecute(int modifiedCount) {
		}

		public string GetSqlColumns() {
			return FColumns;
		}

		public string GetSqlValues() {
			return FValues;
		}
	}
}
