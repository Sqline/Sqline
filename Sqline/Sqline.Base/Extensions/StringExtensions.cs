// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.Base {
	public static class StringExtensions {

		public static string ToCamelCase(this string value) {
			string OResult = value.Remove(0, 1);
			return value.Substring(0, 1).ToLower() + OResult;
		}
	}
}
