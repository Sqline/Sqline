// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Text.RegularExpressions;

namespace Sqline.ClientFramework {
    public static class StringExtensions {
		private static Regex FLineEndingNormalizationRegex = new Regex(@"\r\n|\n|\r", RegexOptions.Compiled);

		public static string ToCamelCase(this string value) {
			string OResult = value.Remove(0, 1);
			return value.Substring(0, 1).ToLower() + OResult;
		}


		public static string NormalizeLineEndings(this string value) {
			return FLineEndingNormalizationRegex.Replace(value, "\r\n");
		}
	}
}
