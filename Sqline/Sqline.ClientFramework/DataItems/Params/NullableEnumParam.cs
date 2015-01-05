// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public class NullableEnumParam<TEnum, TValue> : EnumParam<TEnum, TValue> where TValue : struct {

		protected NullableEnumParam() {
		}

		public NullableEnumParam(TEnum value) : this(value, EnumOperator.Equals) {
		}

		public NullableEnumParam(TEnum value, EnumOperator enumOperator) {
			if (typeof(TEnum).IsClass && value == null) {
				FOperator = EnumOperator.EqualsNull;
				HasValue = false;
				return;
			}
			FOperator = enumOperator;
			FValue = value;
			if (enumOperator == EnumOperator.EqualsNull) {
				HasValue = false;
			}
		}

		public static implicit operator NullableEnumParam<TEnum, TValue>(DBNull value) {
			return new NullableEnumParam<TEnum, TValue>(default(TEnum), EnumOperator.EqualsNull);
		}

		public static implicit operator DBNull(NullableEnumParam<TEnum, TValue> value) {
			return value.Operator == EnumOperator.EqualsNull ? DBNull.Value : null;
		}

		public static bool operator ==(NullableEnumParam<TEnum, TValue> obj, DBNull value) {
			return obj.Operator == EnumOperator.EqualsNull;
		}

		public static bool operator !=(NullableEnumParam<TEnum, TValue> obj, DBNull value) {
			return obj.Operator != EnumOperator.EqualsNull;
		}

		/* BEGIN: Inherited from EnumParam */
		public static implicit operator NullableEnumParam<TEnum, TValue>(TEnum value) {
			return new NullableEnumParam<TEnum, TValue>(value);
		}

		public static implicit operator TEnum(NullableEnumParam<TEnum, TValue> value) {
			return value.FValue;
		}
		/* END: Inherited from EnumParam */

		public override string GetStatement(string columnName, string parameterName) {
			if (FOperator == EnumOperator.EqualsNull) {
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
