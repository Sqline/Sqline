// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public enum NumberOperator { Equals, EqualsNull, Increment, Decrement, Add, Subtract, Multiply, Divide };

	public class NumberParam<T> : BaseParam, INumberParam {
		protected NumberOperator FOperator = NumberOperator.Equals;
		protected T FValue;

		protected NumberParam() {
		}

		public NumberParam(T value) : this(value, NumberOperator.Equals) {
		}

		public NumberParam(T value, NumberOperator numberOperator) {
			if (typeof(T).IsClass && value == null) {
				throw new NotNullableException("NumberParam cannot be null");
			}
			FOperator = numberOperator;
			FValue = value;
			if (numberOperator == NumberOperator.Increment || numberOperator == NumberOperator.Decrement || numberOperator == NumberOperator.Add || numberOperator == NumberOperator.Subtract) {
				IsNull = false;
			}
		}

		public static NumberParam<T> operator ++(NumberParam<T> obj) {
			return new NumberParam<T>(default(T), NumberOperator.Increment);
		}

		public static NumberParam<T> operator --(NumberParam<T> obj) {
			return new NumberParam<T>(default(T), NumberOperator.Decrement);
		}

		public static NumberParam<T> operator +(NumberParam<T> obj, T value) {
			return new NumberParam<T>(value, NumberOperator.Add);
		}

		public static NumberParam<T> operator -(NumberParam<T> obj, T value) {
			return new NumberParam<T>(value, NumberOperator.Subtract);
		}

		public static NumberParam<T> operator *(NumberParam<T> obj, T value) {
			return new NumberParam<T>(value, NumberOperator.Multiply);
		}

		public static NumberParam<T> operator /(NumberParam<T> obj, T value) {
			return new NumberParam<T>(value, NumberOperator.Divide);
		}

		public static implicit operator NumberParam<T>(T value) {
			return new NumberParam<T>(value, NumberOperator.Equals);
		}

		public static implicit operator T(NumberParam<T> obj) {
			return obj.FValue;
		}

		public override string GetStatement(string columnName, string parameterName) {
			if (FOperator == NumberOperator.Equals) {
				return columnName + " = " + parameterName;
			}
			if (FOperator == NumberOperator.Increment) {
				return columnName + " = " + columnName + " + 1";
			}
			if (FOperator == NumberOperator.Decrement) {
				return columnName + " = " + columnName + " - 1";
			}
			if (FOperator == NumberOperator.Add) {
				return columnName + " = " + columnName + " + " + FValue;
			}
			if (FOperator == NumberOperator.Subtract) {
				return columnName + " = " + columnName + " - " + FValue;
			}
			if (FOperator == NumberOperator.Multiply) {
				return columnName + " = " + columnName + " * " + FValue;
			}
			if (FOperator == NumberOperator.Divide) {
				return columnName + " = " + columnName + " / " + FValue;
			}
			return "";
		}

		public NumberOperator Operator {
			get {
				return FOperator;
			}
		}

		public override object Value {
			get {
				return FValue;
			}
		}

		public override Type Type {
			get {
				return typeof(T);
			}
		}
	}
}
