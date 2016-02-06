// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public abstract class UpdateDataItem : BaseDataItem {
		protected bool FAllowUnsafeQuery = false;
		protected int FTopNumberOfRecords = 0;

		protected internal override string PrepareStatement() {
			String OTableName = Provider.Current.GetSafeTableName(GetSchemaName(), GetTableName());
			StringBuilder OValues = new StringBuilder();
			StringBuilder OWheres = new StringBuilder();

			foreach (IBaseParam OParam in FParameters) {
				if (OParam is IValueParam || OParam is INumberParam || OParam is IEnumParam) {
					if (OValues.Length > 0) {
						OValues.Append(",");
					}
					string OParameterName = Provider.Current.GetParameterName("p" + FParameterIndex++);
					OValues.Append(OParam.GetStatement(Provider.Current.GetSafeColumnName(OParam.ColumnName), OParameterName));
					OParam.ParameterName = OParameterName;
				}
				if (OParam is IWhereParam) {
					if (OWheres.Length > 0) {
						OWheres.Append(" AND ");
					}
					string OParameterName = Provider.Current.GetParameterName("p" + FParameterIndex++);
					OWheres.Append(OParam.GetStatement(Provider.Current.GetSafeColumnName(OParam.ColumnName), OParameterName));
					OParam.ParameterName = OParameterName;
				}
			}

			if (OWheres.Length == 0 && !FAllowUnsafeQuery) {
				throw new Exception("Unsafe UPDATE statement, no wheres specified!");
			}

			StringBuilder OSql = new StringBuilder();
			OSql.Append("UPDATE ");
			if (FTopNumberOfRecords > 0) {
				OSql.Append("TOP(" + FTopNumberOfRecords + ") ");
			}
			OSql.Append(OTableName);
			OSql.Append(" SET ");
			OSql.Append(OValues);
			if (OWheres.Length > 0) {
				OSql.Append(" WHERE ");
				OSql.Append(OWheres);
			}
			return OSql.ToString();
		}

		protected internal override void PreExecute() {
		}

		protected internal override void PostExecute(int modifiedCount) {
		}

		public void AllowUnsafeQuery() {
			FAllowUnsafeQuery = true;
		}
		public void SetTop(int topNumberOfRecords) {
			FTopNumberOfRecords = topNumberOfRecords;
		}
	}
}
