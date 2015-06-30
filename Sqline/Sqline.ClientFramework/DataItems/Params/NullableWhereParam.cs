// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public class NullableWhereParam<T> : WhereParam<T> {

		protected NullableWhereParam() {
		}

		public NullableWhereParam(T value) : this(value, WhereOperator.Equals) {
		}

		public NullableWhereParam(T value, WhereOperator whereOperator) {
			if (typeof(T).IsClass && value == null) {
				FOperator = WhereOperator.EqualsNull;
				IsNull = false;
				return;
			}
			FOperator = whereOperator;
			FValue = value;
			if (whereOperator == WhereOperator.EqualsNull || whereOperator == WhereOperator.NotEqualsNull) {
				IsNull = false;
			}
		}

		public static NullableWhereParam<T> operator !=(NullableWhereParam<T> obj, DBNull value) {
			return new NullableWhereParam<T>(default(T), WhereOperator.NotEqualsNull);
		}

		public static NullableWhereParam<T> operator ==(NullableWhereParam<T> obj, DBNull value) {
			return new NullableWhereParam<T>(default(T), WhereOperator.EqualsNull);
		}

		public static implicit operator NullableWhereParam<T>(DBNull value) {
			return new NullableWhereParam<T>(default(T), WhereOperator.EqualsNull);
		}

		public static implicit operator DBNull(NullableWhereParam<T> obj) {
			return obj.Operator == WhereOperator.EqualsNull ? DBNull.Value : null;
		}

		/* BEGIN: Inherited from WhereParam */
		public static NullableWhereParam<T> operator ==(NullableWhereParam<T> obj, T value) {
			return new NullableWhereParam<T>(value, WhereOperator.Equals);
		}

		public static NullableWhereParam<T> operator !=(NullableWhereParam<T> obj, T value) {
			return new NullableWhereParam<T>(value, WhereOperator.NotEquals);
		}

		public static NullableWhereParam<T> operator >(NullableWhereParam<T> obj, T value) {
			return new NullableWhereParam<T>(value, WhereOperator.GreaterThan);
		}

		public static NullableWhereParam<T> operator >=(NullableWhereParam<T> obj, T value) {
			return new NullableWhereParam<T>(value, WhereOperator.GreaterThanEquals);
		}

		public static NullableWhereParam<T> operator <(NullableWhereParam<T> obj, T value) {
			return new NullableWhereParam<T>(value, WhereOperator.LowerThan);
		}

		public static NullableWhereParam<T> operator <=(NullableWhereParam<T> obj, T value) {
			return new NullableWhereParam<T>(value, WhereOperator.LowerThanEquals);
		}
		/* END: Inherited from WhereParam */
		
		public override string GetStatement(string columnName, string parameterName) {
			if (FOperator == WhereOperator.EqualsNull) {
				return columnName + " IS NULL";
			}
			if (FOperator == WhereOperator.NotEqualsNull) {
				return columnName + " IS NOT NULL";
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