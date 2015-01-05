// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace T4Compiler.Generator {
	public static class StringExtensions {

		public static string ToMD5Hash(this string str) {
			using (MD5 OHash = MD5.Create()) {
				StringBuilder OResult = new StringBuilder();
				byte[] OBytes = OHash.ComputeHash(Encoding.UTF8.GetBytes(str));
				for (int i = 0; i < OBytes.Length; i++) {
					OResult.Append(OBytes[i].ToString("x2").ToLower());
				}
				return OResult.ToString();
			}
		}

		public static bool EqualsIC(this string str, string value) {
			return str.Equals(value, StringComparison.OrdinalIgnoreCase);
		}

		public static bool ContainsIC(this string str, string value) {
			return str.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		public static int IndexOfIC(this string str, string value) {
			return str.IndexOf(value, StringComparison.OrdinalIgnoreCase);
		}

		public static bool EndsWithIC(this string str, string value) {
			return str.EndsWith(value, StringComparison.OrdinalIgnoreCase);
		}

		public static bool StartsWithIC(this string str, string value) {
			return str.StartsWith(value, StringComparison.OrdinalIgnoreCase);
		}

		public static string BuildString<T>(this IEnumerable<T> list, Func<T, string> func, string delimiter) {
			StringBuilder OResult = new StringBuilder();
			bool OFirst = true;
			foreach (T OItem in list) {
				if (!OFirst) {
					OResult.Append(delimiter);
				}
				OFirst = false;
				OResult.Append(func(OItem));
			}
			return OResult.ToString();
		}
	}
}
