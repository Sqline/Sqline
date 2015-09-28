// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sqline.ClientFramework.Types {
	public class CastableString : IConvertible, ISpecializedString {
		private string FValue;

		public CastableString(string value) {
			FValue = value;
		}

		public CastableString(object value, IFormatProvider provider) {
			FValue = (string)System.Convert.ChangeType(value, typeof(string), provider);
		}

		public TypeCode GetTypeCode() {
			return TypeCode.Object;
		}

		public static implicit operator bool(CastableString value) {
			return value.ToBoolean();
		}

		public static implicit operator byte(CastableString value) {
			return value.ToByte();
		}

		public static implicit operator char(CastableString value) {
			return value.ToChar();
		}

		public static implicit operator DateTime(CastableString value) {
			return value.ToDateTime();
		}

		public static implicit operator decimal(CastableString value) {
			return value.ToDecimal();
		}

		public static implicit operator double(CastableString value) {
			return value.ToDouble();
		}

		public static implicit operator short(CastableString value) {
			return value.ToInt16();
		}

		public static implicit operator int(CastableString value) {
			return value.ToInt32();
		}

		public static implicit operator long(CastableString value) {
			return value.ToInt64();
		}

		public static implicit operator sbyte(CastableString value) {
			return value.ToSByte();
		}

		public static implicit operator float(CastableString value) {
			return value.ToSingle();
		}

		public static implicit operator ushort(CastableString value) {
			return value.ToUInt16();
		}

		public static implicit operator uint(CastableString value) {
			return value.ToUInt32();
		}

		public static implicit operator ulong(CastableString value) {
			return value.ToUInt64();
		}

		public static implicit operator Guid(CastableString value) {
			return value.ToGuid();
		}

		public static implicit operator CastableString(bool value) {
			return new CastableString(value ? "true" : "false");
		}

		public static implicit operator CastableString(byte value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(char value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(DateTime value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(decimal value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(double value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(short value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(int value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(long value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(sbyte value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(float value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(ushort value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(uint value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(ulong value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(Guid value) {
			return new CastableString(value.ToString());
		}

		public static implicit operator CastableString(string value) {
			return new CastableString(value);
		}

		private bool IsValid<T>() {
			return IsValid<T>(null);
		}

		private bool IsValid<T>(IFormatProvider provider) {
			if (FValue == null) {
				return false;
			}
			try {
				Convert<T>(FValue, provider);
				return true;
			}
			catch {
				return false;
			}
		}
	
		public bool ToBoolean(IFormatProvider provider) {
			try {
				return Convert<bool>(FValue, provider);
			}
			catch {
				return default(bool);
			}
		}

		public byte ToByte(IFormatProvider provider) {
			try {
				return Convert<byte>(FValue, provider);
			}
			catch {
				return default(byte);
			}
		}

		public char ToChar(IFormatProvider provider) {
			try {
				return Convert<char>(FValue, provider);
			}
			catch {
				return default(char);
			}
		}

		public DateTime ToDateTime(IFormatProvider provider) {
			try {
				return Convert<DateTime>(FValue, provider);
			}
			catch {
				return default(DateTime);
			}
		}

		public decimal ToDecimal(IFormatProvider provider) {
			try {
				return Convert<decimal>(FValue, provider);
			}
			catch {
				return default(decimal);
			}
		}

		public double ToDouble(IFormatProvider provider) {
			try {
				return Convert<double>(FValue, provider);
			}
			catch {
				return default(double);
			}
		}

		public short ToInt16(IFormatProvider provider) {
			try {
				return Convert<short>(FValue, provider);
			}
			catch {
				return default(short);
			}
		}

		public int ToInt32(IFormatProvider provider) {
			try {
				return Convert<int>(FValue, provider);
			}
			catch {
				return default(int);
			}
		}

		public long ToInt64(IFormatProvider provider) {
			try {
				return Convert<long>(FValue, provider);
			}
			catch {
				return default(long);
			}
		}

		public sbyte ToSByte(IFormatProvider provider) {
			try {
				return Convert<sbyte>(FValue, provider);
			}
			catch {
				return default(sbyte);
			}
		}

		public float ToSingle(IFormatProvider provider) {
			try {
				return Convert<byte>(FValue, provider);
			}
			catch {
				return default(byte);
			}
		}

		public string ToString(IFormatProvider provider) {
			if (FValue == null) {
				return string.Empty;
			}
			return FValue;
		}

		public object ToType(Type conversionType, IFormatProvider provider) {
			return System.Convert.ChangeType(FValue, conversionType, provider);
		}

		public ushort ToUInt16(IFormatProvider provider) {
			try {
				return Convert<ushort>(FValue, provider);
			}
			catch {
				return default(ushort);
			}
		}

		public uint ToUInt32(IFormatProvider provider) {
			try {
				return Convert<uint>(FValue, provider);
			}
			catch {
				return default(uint);
			}
		}

		public ulong ToUInt64(IFormatProvider provider) {
			try {
				return Convert<ulong>(FValue, provider);
			}
			catch {
				return default(ulong);
			}
		}

		public Guid ToGuid(IFormatProvider provider) {
			try {
				return Convert<Guid>(FValue, provider);
			}
			catch {
				return default(Guid);
			}
		}

		public bool ToBoolean() {
			try {
				return Convert<bool>(FValue);
			}
			catch {
				return default(bool);
			}
		}

		public byte ToByte() {
			try {
				return Convert<byte>(FValue);
			}
			catch {
				return default(byte);
			}
		}

		public char ToChar() {
			try {
				return Convert<char>(FValue);
			}
			catch {
				return default(char);
			}
		}

		public DateTime ToDateTime() {
			try {
				return Convert<DateTime>(FValue);
			}
			catch {
				return default(DateTime);
			}
		}

		public decimal ToDecimal() {
			try {
				return Convert<decimal>(FValue);
			}
			catch {
				return default(decimal);
			}
		}

		public double ToDouble() {
			try {
				return Convert<double>(FValue);
			}
			catch {
				return default(double);
			}
		}

		public short ToInt16() {
			try {
				return Convert<short>(FValue);
			}
			catch {
				return default(short);
			}
		}

		public int ToInt32() {
			try {
				return Convert<int>(FValue);
			}
			catch {
				return default(int);
			}
		}

		public long ToInt64() {
			try {
				return Convert<long>(FValue);
			}
			catch {
				return default(long);
			}
		}

		public sbyte ToSByte() {
			try {
				return Convert<sbyte>(FValue);
			}
			catch {
				return default(sbyte);
			}
		}

		public float ToSingle() {
			try {
				return Convert<byte>(FValue);
			}
			catch {
				return default(byte);
			}
		}

		public override string ToString() {
			if (FValue == null) {
				return string.Empty;
			}
			return FValue;
		}

		public ushort ToUInt16() {
			try {
				return Convert<ushort>(FValue);
			}
			catch {
				return default(ushort);
			}
		}

		public uint ToUInt32() {
			try {
				return Convert<uint>(FValue);
			}
			catch {
				return default(uint);
			}
		}

		public ulong ToUInt64() {
			try {
				return Convert<ulong>(FValue);
			}
			catch {
				return default(ulong);
			}
		}

		public T To<T>() {
			return Convert<T>(FValue);
		}

		public Guid ToGuid() {
			try {
				return Convert<Guid>(FValue);
			}
			catch {
				return default(Guid);
			}
		}

		public static bool TryParseSpecialCases<T>(object value, out T result, IFormatProvider provider) {
			if (typeof(T) == typeof(bool) && value is string) {
				result = (T)(object)(((string)value).Equals("true", StringComparison.OrdinalIgnoreCase) || (string)value == "1");
				return true;
			}
			if (typeof(T) == typeof(Guid) && value is string) {
				result = (T)(object)(new Guid((string)value));
				return true;
			}
			if (typeof(T) == typeof(CastableString)) {
				result = (T)(object)new CastableString(value, provider);
				return true;
			}
			result = default(T);
			return false;
		}

		public static T Convert<T>(object value) {
			T OResult;
			if (TryParseSpecialCases(value, out OResult, null)) {
				return OResult;
			}
			else {
				return (T)System.Convert.ChangeType(value, typeof(T));
			}
		}

		public static T Convert<T>(object value, IFormatProvider provider) {
			T OResult;
			if (TryParseSpecialCases(value, out OResult, provider)) {
				return OResult;
			}
			else {
				return (T)System.Convert.ChangeType(value, typeof(T), provider);
			}
		}

		string ISpecializedString.Value {
			get { return FValue; }
		}
	}
}
