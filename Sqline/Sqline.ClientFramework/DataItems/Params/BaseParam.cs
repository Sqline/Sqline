// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public abstract class BaseParam : IBaseParam {
		private string FColumnName;
		private string FParameterName;
		private bool FHasValue = true;

		public void Initialize(string columnName) {
			FColumnName = columnName;
		}

		//public abstract string GetStatement(int index);
		public abstract object Value { get; }
		public abstract Type Type { get; }
		public abstract string GetStatement(string columnName, string parameterName);

		public void AddParameter(IDbCommand command) {
			IDbDataParameter OParameter = command.CreateParameter();
			OParameter.ParameterName = ParameterName;
			OParameter.Value = Value;
			command.Parameters.Add(OParameter);
		}

		public string ParameterName {
			get {
				return FParameterName;
			}
			set {
				FParameterName = value;
			}
		}

		public string ColumnName {
			get {
				return FColumnName;
			}
		}

		public bool HasValue {
			get {
				return FHasValue;
			}
			protected set {
				FHasValue = value;
			}
		}
	}
}