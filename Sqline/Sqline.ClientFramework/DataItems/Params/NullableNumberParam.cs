// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public class NullableNumberParam<T> : NumberParam<T> {

		protected NullableNumberParam() {
		}

		public NullableNumberParam(T value) : this(value, NumberOperator.Equals) {
		}

		public NullableNumberParam(T value, NumberOperator numberOperator) {
			if (typeof(T).IsClass && value == null) {
				FOperator = NumberOperator.EqualsNull;
				HasValue = false;
				return;
			}
			FOperator = numberOperator;
			FValue = value;
			if (numberOperator == NumberOperator.EqualsNull || numberOperator == NumberOperator.Increment || numberOperator == NumberOperator.Decrement || numberOperator == NumberOperator.Add || numberOperator == NumberOperator.Subtract) {
				HasValue = false;
			}
		}

		public static bool operator !=(NullableNumberParam<T> obj, DBNull value) {
			return obj.Operator == NumberOperator.EqualsNull;
		}

		public static bool operator ==(NullableNumberParam<T> obj, DBNull value) {
			return obj.Operator != NumberOperator.EqualsNull;
		}

		public static implicit operator NullableNumberParam<T>(DBNull value) {
			return new NullableNumberParam<T>(default(T), NumberOperator.EqualsNull);
		}

		public static implicit operator DBNull(NullableNumberParam<T> value) {
			return value.Operator == NumberOperator.EqualsNull ? DBNull.Value : null;
		}

		/* BEGIN: Inherited from NumberParam */
		public static NullableNumberParam<T> operator ++(NullableNumberParam<T> obj) {
			return new NullableNumberParam<T>(default(T), NumberOperator.Increment);
		}

		public static NullableNumberParam<T> operator --(NullableNumberParam<T> obj) {
			return new NullableNumberParam<T>(default(T), NumberOperator.Decrement);
		}

		public static NullableNumberParam<T> operator +(NullableNumberParam<T> obj, T value) {
			return new NullableNumberParam<T>(value, NumberOperator.Add);
		}

		public static NullableNumberParam<T> operator -(NullableNumberParam<T> obj, T value) {
			return new NullableNumberParam<T>(value, NumberOperator.Subtract);
		}

		public static NullableNumberParam<T> operator *(NullableNumberParam<T> obj, T value) {
			return new NullableNumberParam<T>(value, NumberOperator.Multiply);
		}

		public static NullableNumberParam<T> operator /(NullableNumberParam<T> obj, T value) {
			return new NullableNumberParam<T>(value, NumberOperator.Divide);
		}

		public static implicit operator NullableNumberParam<T>(T value) {
			return new NullableNumberParam<T>(value, NumberOperator.Equals);
		}

		public static implicit operator T(NullableNumberParam<T> obj) {
			return obj.FValue;
		}
		/* END: Inherited from NumberParam */

		public override string GetStatement(string columnName, string parameterName) {
			if (FOperator == NumberOperator.EqualsNull) {
				return columnName + " = " + parameterName;
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
