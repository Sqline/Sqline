// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Data;

namespace Sqline.ClientFramework {
	public static class IDataRecordExtensions {

		public static int GetIndex(this IDataRecord record, string columnName) {
			// Matches behavior of GetOrdinal without throwing IndexOutofBoundsException
			for (int i = 0; i < record.FieldCount; i++) {
				if (record.GetName(i) == columnName) {
					return i;
				}
			}
			for (int i = 0; i < record.FieldCount; i++) {
				if (record.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase)) {
					return i;
				}
			}
			return -1;
		}

		/* Boolean */

		public static bool GetBoolean(this IDataRecord record, string columnName) {
			return record.GetBoolean(record.GetOrdinal(columnName));
		}

		public static bool? GetBooleanOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetBoolean(ordinal);
			}
			return null;
		}

		public static bool? GetBooleanOrNull(this IDataRecord record, string columnName) {
			return GetBooleanOrNull(record, record.GetOrdinal(columnName));
		}

		public static bool GetBoolean(this IDataRecord record, int ordinal, out bool hasValue) {
			bool? OValue = record.GetBooleanOrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(bool);
		}

		public static bool GetBooleanOrDefault(this IDataRecord record, int ordinal, bool defaultValue) {
			bool? OValue = record.GetBooleanOrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static bool GetBooleanOrDefault(this IDataRecord record, string columnName, bool defaultValue) {
			return GetBooleanOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Byte */

		public static byte GetByte(this IDataRecord record, string columnName) {
			return record.GetByte(record.GetOrdinal(columnName));
		}

		public static byte? GetByteOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetByte(ordinal);
			}
			return null;
		}

		public static byte? GetByteOrNull(this IDataRecord record, string columnName) {
			return GetByteOrNull(record, record.GetOrdinal(columnName));
		}

		public static byte GetByte(this IDataRecord record, int ordinal, out bool hasValue) {
			byte? OValue = record.GetByteOrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(byte);
		}

		public static byte GetByteOrDefault(this IDataRecord record, int ordinal, byte defaultValue) {
			byte? OValue = record.GetByteOrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static byte GetByteOrDefault(this IDataRecord record, string columnName, byte defaultValue) {
			return GetByteOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Bytes */

		public static byte[] GetBytes(this IDataRecord record, string columnName) {
			return record.GetBytes(record.GetOrdinal(columnName));
		}

		public static byte[] GetBytes(this IDataRecord record, int ordinal) {
			return (byte[])record.GetValue(ordinal);
		}

		public static byte[] GetBytesOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetBytes(ordinal);
			}
			return null;
		}

		public static byte[] GetBytesOrNull(this IDataRecord record, string columnName) {
			return GetBytesOrNull(record, record.GetOrdinal(columnName));
		}

		public static byte[] GetBytes(this IDataRecord record, int ordinal, out bool hasValue) {
			byte[] OValue = record.GetBytesOrNull(ordinal);
			hasValue = OValue != null;
			return OValue != null ? OValue : default(byte[]);
		}

		public static byte[] GetBytesOrDefault(this IDataRecord record, int ordinal, byte[] defaultValue) {
			byte[] OValue = record.GetBytesOrNull(ordinal);
			return OValue != null ? OValue : defaultValue;
		}

		public static byte[] GetBytesOrDefault(this IDataRecord record, string columnName, byte[] defaultValue) {
			return GetBytesOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Char */

		public static char GetChar(this IDataRecord record, string columnName) {
			return record.GetChar(record.GetOrdinal(columnName));
		}

		public static char? GetCharOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetChar(ordinal);
			}
			return null;
		}

		public static char? GetCharOrNull(this IDataRecord record, string columnName) {
			return GetCharOrNull(record, record.GetOrdinal(columnName));
		}

		public static char GetChar(this IDataRecord record, int ordinal, out bool hasValue) {
			char? OValue = record.GetCharOrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(char);
		}

		public static char GetCharOrDefault(this IDataRecord record, int ordinal, char defaultValue) {
			char? OValue = record.GetCharOrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static char GetCharOrDefault(this IDataRecord record, string columnName, char defaultValue) {
			return GetCharOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* DateTime */

		public static DateTime GetDateTime(this IDataRecord record, string columnName) {
			return record.GetDateTime(record.GetOrdinal(columnName));
		}

		public static DateTime? GetDateTimeOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetDateTime(ordinal);
			}
			return null;
		}

		public static DateTime? GetDateTimeOrNull(this IDataRecord record, string columnName) {
			return GetDateTimeOrNull(record, record.GetOrdinal(columnName));
		}

		public static DateTime GetDateTime(this IDataRecord record, int ordinal, out bool hasValue) {
			DateTime? OValue = record.GetDateTimeOrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(DateTime);
		}

		public static DateTime GetDateTimeOrDefault(this IDataRecord record, int ordinal, DateTime defaultValue) {
			DateTime? OValue = record.GetDateTimeOrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static DateTime GetDateTimeOrDefault(this IDataRecord record, string columnName, DateTime defaultValue) {
			return GetDateTimeOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Decimal */

		public static decimal GetDecimal(this IDataRecord record, string columnName) {
			return record.GetDecimal(record.GetOrdinal(columnName));
		}

		public static decimal? GetDecimalOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetDecimal(ordinal);
			}
			return null;
		}

		public static decimal? GetDecimalOrNull(this IDataRecord record, string columnName) {
			return GetDecimalOrNull(record, record.GetOrdinal(columnName));
		}

		public static decimal GetDecimal(this IDataRecord record, int ordinal, out bool hasValue) {
			Decimal? OValue = record.GetDecimalOrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(decimal);
		}

		public static decimal GetDecimalOrDefault(this IDataRecord record, int ordinal, decimal defaultValue) {
			Decimal? OValue = record.GetDecimalOrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static decimal GetDecimalOrDefault(this IDataRecord record, string columnName, decimal defaultValue) {
			return GetDecimalOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Double */

		public static double GetDouble(this IDataRecord record, string columnName) {
			return record.GetDouble(record.GetOrdinal(columnName));
		}

		public static double? GetDoubleOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetDouble(ordinal);
			}
			return null;
		}

		public static double? GetDoubleOrNull(this IDataRecord record, string columnName) {
			return GetDoubleOrNull(record, record.GetOrdinal(columnName));
		}

		public static double GetDouble(this IDataRecord record, int ordinal, out bool hasValue) {
			double? OValue = record.GetDoubleOrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(double);
		}

		public static double GetDoubleOrDefault(this IDataRecord record, int ordinal, double defaultValue) {
			double? OValue = record.GetDoubleOrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static double GetDoubleOrDefault(this IDataRecord record, string columnName, double defaultValue) {
			return GetDoubleOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Float */

		public static float GetFloat(this IDataRecord record, string columnName) {
			return record.GetFloat(record.GetOrdinal(columnName));
		}

		public static float? GetFloatOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetFloat(ordinal);
			}
			return null;
		}

		public static float? GetFloatOrNull(this IDataRecord record, string columnName) {
			return GetFloatOrNull(record, record.GetOrdinal(columnName));
		}

		public static float GetFloat(this IDataRecord record, int ordinal, out bool hasValue) {
			float? OValue = record.GetFloatOrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(float);
		}

		public static float GetFloatOrDefault(this IDataRecord record, int ordinal, float defaultValue) {
			float? OValue = record.GetFloatOrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static float GetFloatOrDefault(this IDataRecord record, string columnName, float defaultValue) {
			return GetFloatOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Guid */

		public static Guid GetGuid(this IDataRecord record, string columnName) {
			return record.GetGuid(record.GetOrdinal(columnName));
		}

		public static Guid? GetGuidOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetGuid(ordinal);
			}
			return null;
		}

		public static Guid? GetGuidOrNull(this IDataRecord record, string columnName) {
			return GetGuidOrNull(record, record.GetOrdinal(columnName));
		}

		public static Guid GetGuid(this IDataRecord record, int ordinal, out bool hasValue) {
			Guid? OValue = record.GetGuidOrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(Guid);
		}

		public static Guid GetGuidOrDefault(this IDataRecord record, int ordinal, Guid defaultValue) {
			Guid? OValue = record.GetGuidOrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static Guid GetGuidOrDefault(this IDataRecord record, string columnName, Guid defaultValue) {
			return GetGuidOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Int16 */

		public static short GetInt16(this IDataRecord record, string columnName) {
			return record.GetInt16(record.GetOrdinal(columnName));
		}

		public static short? GetInt16OrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetInt16(ordinal);
			}
			return null;
		}

		public static short? GetInt16OrNull(this IDataRecord record, string columnName) {
			return GetInt16OrNull(record, record.GetOrdinal(columnName));
		}
		public static short GetInt16(this IDataRecord record, int ordinal, out bool hasValue) {
			short? OValue = record.GetInt16OrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(short);
		}

		public static short GetInt16OrDefault(this IDataRecord record, int ordinal, short defaultValue) {
			short? OValue = record.GetInt16OrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static short GetInt16OrDefault(this IDataRecord record, string columnName, short defaultValue) {
			return GetInt16OrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Int32 */

		public static int GetInt32(this IDataRecord record, string columnName) {
			return record.GetInt32(record.GetOrdinal(columnName));
		}

		public static int? GetInt32OrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetInt32(ordinal);
			}
			return null;
		}

		public static int? GetInt32OrNull(this IDataRecord record, string columnName) {
			return GetInt32OrNull(record, record.GetOrdinal(columnName));
		}

		public static int GetInt32(this IDataRecord record, int ordinal, out bool hasValue) {
			int? OValue = record.GetInt32OrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(int);
		}

		public static int GetInt32OrDefault(this IDataRecord record, int ordinal, int defaultValue) {
			int? OValue = record.GetInt32OrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static int GetInt32OrDefault(this IDataRecord record, string columnName, int defaultValue) {
			return GetInt32OrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* Int64 */

		public static long GetInt64(this IDataRecord record, string columnName) {
			return record.GetInt64(record.GetOrdinal(columnName));
		}

		public static long? GetInt64OrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetInt64(ordinal);
			}
			return null;
		}

		public static long? GetInt64OrNull(this IDataRecord record, string columnName) {
			return GetInt64OrNull(record, record.GetOrdinal(columnName));
		}

		public static long GetInt64(this IDataRecord record, int ordinal, out bool hasValue) {
			long? OValue = record.GetInt64OrNull(ordinal);
			hasValue = OValue.HasValue;
			return OValue.HasValue ? OValue.Value : default(long);
		}

		public static long GetInt64OrDefault(this IDataRecord record, int ordinal, long defaultValue) {
			long? OValue = record.GetInt64OrNull(ordinal);
			return OValue.HasValue ? OValue.Value : defaultValue;
		}

		public static long GetInt64OrDefault(this IDataRecord record, string columnName, long defaultValue) {
			return GetInt64OrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}

		/* String */

		public static string GetString(this IDataRecord record, string columnName) {
			return record.GetString(record.GetOrdinal(columnName));
		}

		public static string GetStringOrNull(this IDataRecord record, int ordinal) {
			if (!record.IsDBNull(ordinal)) {
				return record.GetString(ordinal);
			}
			return null;
		}

		public static string GetStringOrNull(this IDataRecord record, string columnName) {
			return GetStringOrNull(record, record.GetOrdinal(columnName));
		}

		public static string GetString(this IDataRecord record, int ordinal, out bool hasValue) {
			string OValue = record.GetStringOrNull(ordinal);
			hasValue = OValue != null;
			return OValue != null ? OValue : default(string);
		}

		public static string GetStringOrDefault(this IDataRecord record, int ordinal, string defaultValue) {
			string OValue = record.GetStringOrNull(ordinal);
			return OValue != null ? OValue : defaultValue;
		}

		public static string GetStringOrDefault(this IDataRecord record, string columnName, string defaultValue) {
			return GetStringOrDefault(record, record.GetOrdinal(columnName), defaultValue);
		}
	}
}