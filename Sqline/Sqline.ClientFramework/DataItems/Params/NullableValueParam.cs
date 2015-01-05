// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public class NullableValueParam<T> : ValueParam<T> {

		protected NullableValueParam() {
		}

		public NullableValueParam(T value) : this(value, ValueOperator.Equals) {
		}

		public NullableValueParam(T value, ValueOperator valueOperator) {
			if (typeof(T).IsClass && value == null) {
				FOperator = ValueOperator.EqualsNull;
				HasValue = false;
				return;
			}
			FOperator = valueOperator;
			FValue = value;
			if (valueOperator == ValueOperator.EqualsNull) {
				HasValue = false;
			}
		}

		public static implicit operator NullableValueParam<T>(DBNull value) {
			return new NullableValueParam<T>(default(T), ValueOperator.EqualsNull);
		}

		public static implicit operator DBNull(NullableValueParam<T> value) {
			return value.Operator == ValueOperator.EqualsNull ? DBNull.Value : null;
		}

		public static bool operator ==(NullableValueParam<T> obj, DBNull value) {
			return obj.Operator == ValueOperator.EqualsNull;
		}

		public static bool operator !=(NullableValueParam<T> obj, DBNull value) {
			return obj.Operator != ValueOperator.EqualsNull;
		}

		/* BEGIN: Inherited from ValueParam */
		public static implicit operator NullableValueParam<T>(T value) {
			return new NullableValueParam<T>(value);
		}

		public static implicit operator T(NullableValueParam<T> value) {
			return value.FValue;
		}
		/* END: Inherited from ValueParam */

		public override string GetStatement(string columnName, string parameterName) {
			if (FOperator == ValueOperator.EqualsNull) {
				return columnName + " = NULL";
			}
			return base.GetStatement(columnName, parameterName);
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
}