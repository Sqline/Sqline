// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;

namespace Sqline.ClientFramework {
    public enum ValueOperator { Equals, EqualsNull }

	public class ValueParam<T> : BaseParam, IValueParam {
		protected ValueOperator FOperator = ValueOperator.Equals;
		protected T FValue;

		protected ValueParam() {
		}

		public ValueParam(T value) : this(value, ValueOperator.Equals) {
		}

		public ValueParam(T value, ValueOperator valueOperator) {
			if (typeof(T).IsClass && value == null) {
				throw new NotNullableException("ValueParam cannot be null");
			}
			FOperator = valueOperator;
			FValue = value;
		}

		public static implicit operator ValueParam<T>(T value) {
			return new ValueParam<T>(value);
		}

		public static implicit operator T(ValueParam<T> obj) {
			return obj.FValue;
		}

		public override string GetStatement(string columnName, string parameterName) {
			return columnName + " = " + parameterName;
		}

		public ValueOperator Operator {
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