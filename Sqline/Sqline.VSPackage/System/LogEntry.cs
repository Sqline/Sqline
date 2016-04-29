using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Sqline.Base;

namespace Sqline.VSPackage {
	public enum LogLevel { Warning, Error };

	public class LogEntry {
		private LogLevel FLogLevel;
		private string FDescription = "";
		private string FFilename = "";
		private int FLine;
		private int FColumn;	

		public LogEntry(Exception ex) {
			FLogLevel = LogLevel.Error;
			FDescription = ex.Message;
			if (ex is SqlineException) {
				SqlineException sex = (SqlineException)ex;
				FFilename = sex.Filename;
				FLine = sex.Line;
				FColumn = sex.Column;
			}
			else {
				FFilename = ex.Source;
			}
		}

		public LogEntry(LogLevel level, string description, string filename, int line, int column) {
			FLogLevel = level;
			FDescription = description;
			FFilename = filename;
			FLine = line;
			FColumn = column;
		}

		public Task GetTask(IVsHierarchy hierarchy) {
			String OText = FFilename + "(" + FLine + ", " + FColumn + "): " + FDescription;
			Debug.WriteLine(OText);
			ErrorTask OTask = new ErrorTask();
			OTask.Document = FFilename;
			OTask.Line = FLine;
			OTask.Column = FColumn;
			OTask.Priority = TaskPriority.High;
			OTask.Category = TaskCategory.BuildCompile;
			OTask.ErrorCategory = FLogLevel == LogLevel.Error ? TaskErrorCategory.Error : TaskErrorCategory.Warning;
			OTask.HierarchyItem = hierarchy;
			OTask.Text = "Sqline: " + FDescription;
			return OTask;
		}

		public LogLevel LogLevel {
			get {
				return FLogLevel;
			}
			set {
				FLogLevel = value;
			}
		}

		public string Description {
			get {
				return FDescription;
			}
			set {
				FDescription = value;
			}
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