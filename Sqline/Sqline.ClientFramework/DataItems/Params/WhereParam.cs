// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public enum WhereOperator { Equals, NotEquals, EqualsNull, NotEqualsNull, LowerThan, LowerThanEquals, GreaterThan, GreaterThanEquals }

	public class WhereParam<T> : BaseParam, IWhereParam {
		protected WhereOperator FOperator = WhereOperator.Equals;
		protected T FValue;

		protected WhereParam() {
		}

		public WhereParam(T value) : this(value, WhereOperator.Equals) {
		}

		public WhereParam(T value, WhereOperator whereOperator) {
			if (typeof(T).IsClass && value == null) {
				throw new NotNullableException("WhereParam cannot be null");
			}
			FOperator = whereOperator;
			FValue = value;
		}

		public static implicit operator WhereParam<T>(T value) {
			return new WhereParam<T>(value);
		}

		public static implicit operator T(WhereParam<T> obj) {
			return obj.FValue;
		}

		public static WhereParam<T> operator ==(WhereParam<T> obj, T value) {
			return new WhereParam<T>(value, WhereOperator.Equals);
		}

		public static WhereParam<T> operator !=(WhereParam<T> obj, T value) {
			return new WhereParam<T>(value, WhereOperator.NotEquals);
		}

		public static WhereParam<T> operator >(WhereParam<T> obj, T value) {
			return new WhereParam<T>(value, WhereOperator.GreaterThan);
		}

		public static WhereParam<T> operator >=(WhereParam<T> obj, T value) {
			return new WhereParam<T>(value, WhereOperator.GreaterThanEquals);
		}

		public static WhereParam<T> operator <(WhereParam<T> obj, T value) {
			return new WhereParam<T>(value, WhereOperator.LowerThan);
		}

		public static WhereParam<T> operator <=(WhereParam<T> obj, T value) {
			return new WhereParam<T>(value, WhereOperator.LowerThanEquals);
		}

		public override string GetStatement(string columnName, string parameterName) {
			if (FOperator == WhereOperator.Equals) {
				return columnName + " = " + parameterName;
			}
			if (FOperator == WhereOperator.NotEquals) {
				return columnName + " <> " + parameterName;
			}
			if (FOperator == WhereOperator.LowerThan) {
				return columnName + " < " + parameterName;
			}
			if (FOperator == WhereOperator.LowerThanEquals) {
				return columnName + " <= " + parameterName;
			}
			if (FOperator == WhereOperator.GreaterThan) {
				return columnName + " > " + parameterName;
			}
			if (FOperator == WhereOperator.GreaterThanEquals) {
				return columnName + " >= " + parameterName;
			}
			return "";
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public WhereOperator Operator {
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