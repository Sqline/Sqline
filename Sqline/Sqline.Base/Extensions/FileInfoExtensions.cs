// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.Base {
	public static class FileInfoExtensions {
		public static FileInfo GetCorrectlyCasedFileInfo(this FileInfo fileinfo) {
			return new FileInfo(Directory.GetFiles(fileinfo.DirectoryName, fileinfo.Name).First<string>());
		}

		public static string GetNameWithoutExt(this FileInfo fileinfo) {
			return fileinfo.Name.Remove(fileinfo.Name.Length - fileinfo.Extension.Length);
		}
	}
}
