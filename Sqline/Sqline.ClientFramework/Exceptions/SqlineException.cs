using System;

namespace Sqline.ClientFramework {
    public class SqlineException : Exception {
		private string FFilename;
		private int FLine;
		private int FColumn;

		public SqlineException(string message) : base(message) {
		}	

		public SqlineException(string filename, int line, int column, string message) : base(message) {
			FFilename = filename;
			FLine = line;
			FColumn = column;
		}

		public SqlineException(string filename, int line, int column, string message, Exception innerException) : base(message, innerException) {
			FFilename = filename;
			FLine = line;
			FColumn = column;
		}

		public string Filename {
			get {
				return FFilename;
			}
		}

		public int Line {
			get {
				return FLine;
			}
		}

		public int Column {
			get {
				return FColumn;
			}
		}
	}
}
