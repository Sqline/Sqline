// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Sqline.VSPackage {
	public class AddinContext {
		private DTE2 FApplication;
		private Package FPackage;
		private string FPackageDirectory = "";

		public AddinContext(DTE2 application, Package package) {
			FApplication = application;
			FPackage = package;
			FPackageDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location);
			Debug.WriteLine("FPackageDirectory: " + FPackageDirectory);
		}

		public String ResolvePath(string path) {
			path = path.Trim();
			if (path[1] == ':') {
				return path;
			}
			if (path.StartsWith("/") || path.StartsWith("\\")) {
				path = path.Remove(0, 1);
				string templatedir = Environment.GetEnvironmentVariable("sqline_templates");
				if (templatedir != null) {
					Debug.WriteLine("Templatedir: " + templatedir);
					path = Path.GetFullPath(Path.Combine(templatedir, path));
				}
				else {
					path = Path.GetFullPath(Path.Combine(FPackageDirectory, path));
				}
			}
			Debug.WriteLine("Resolved: " + path);
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

		public string PackageDirectory {
			get {
				return FPackageDirectory;
			}
		}
	}
}
