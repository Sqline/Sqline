// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
    public abstract class QueryableDataItem : BaseDataItem {
		protected bool FAllowUnsafeQuery = false;
		protected int FTopNumberOfRecords = 0;
        protected bool FIncludeValueParams = false;

		public QueryableDataItem(bool includeValueParams) {
			FIncludeValueParams = includeValueParams;
		}

		protected internal override string PrepareStatement() {
			string OSafeTableName = Provider.Current.GetSafeTableName(GetSchemaName(), GetTableName());
            StringBuilder OValues = new StringBuilder();
            StringBuilder OWheres = new StringBuilder();

            foreach (IBaseParam OParam in FParameters) {
                if (FIncludeValueParams) {
                    if (OParam is IValueParam || OParam is INumberParam || OParam is IEnumParam) {
                        if (OValues.Length > 0) {
                            OValues.Append(",");
                        }
                        string OParameterName = Provider.Current.GetParameterName("p" + FParameterIndex++);
                        OValues.Append(OParam.GetStatement(Provider.Current.GetSafeColumnName(OParam.ColumnName), OParameterName));
                        OParam.ParameterName = OParameterName;
                    }
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

			return GetQueryableStatement(OSafeTableName, OValues.ToString(), OWheres.ToString());			
		}

		protected abstract string GetQueryableStatement(string tableName, string valueParameters, string whereParameters);

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
