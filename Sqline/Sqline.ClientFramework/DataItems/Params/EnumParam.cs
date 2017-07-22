// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;

namespace Sqline.ClientFramework {
    public enum EnumOperator { Equals, EqualsNull }

	public class EnumParam<TEnum, TValue> : BaseParam, IEnumParam where TValue : struct {
		protected EnumOperator FOperator = EnumOperator.Equals;
		protected TEnum FValue;

		protected EnumParam() {
		}

		public EnumParam(TEnum value): this(value, EnumOperator.Equals) {
		}

		public EnumParam(TEnum value, EnumOperator valueOperator) {
			if (valueOperator == EnumOperator.EqualsNull) {
				throw new NotNullableException("EnumParam cannot be null");
			}
			FOperator = valueOperator;
			FValue = value;
		}

		public static implicit operator EnumParam<TEnum, TValue>(TEnum value) {
			return new EnumParam<TEnum, TValue>(value);
		}

		public static implicit operator TEnum(EnumParam<TEnum, TValue> obj) {
			return obj.FValue;
		}

		public override string GetStatement(string columnName, string parameterName) {
			return columnName + " = " + parameterName;
		}

		public EnumOperator Operator {
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
				return typeof(TValue);
			}
		}
	}
}
