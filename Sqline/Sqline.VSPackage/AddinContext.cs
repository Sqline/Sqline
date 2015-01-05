// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.IO;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Sqline.VSPackage {
	internal class AddinContext {
		private DTE2 FApplication;
		private Package FPackage;
		private string FAssemblyPath = "";

		public AddinContext(DTE2 application, Package package) {
			FApplication = application;
			FPackage = package;
			FAssemblyPath = Path.GetDirectoryName(this.GetType().Assembly.Location);
		}

		public String ResolvePath(string path) {
			if (path[1] != ':') {
				string sqlinedir = Environment.GetEnvironmentVariable("sqline");
				return Path.GetFullPath(Path.Combine(sqlinedir, path));
			}
			return path;
		}

		public void Log(Object obj) {
			IVsOutputWindow outputWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
			Guid guidGeneral = Microsoft.VisualStudio.VSConstants.OutputWindowPaneGuid.GeneralPane_guid;
			IVsOutputWindowPane pane;
			int hr = outputWindow.CreatePane(guidGeneral, "General", 1, 0);
			hr = outputWindow.GetPane(guidGeneral, out pane);
			pane.Activate();
			pane.OutputString(obj.ToString());
		}

		public DTE2 Application {
			get {
				return FApplication;
			}
		}

		public Package Package {
			get {
				return FPackage;
			}
		}
	}
}
