// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.IO;
using System.Text;

namespace T4Compiler.Generator {
	//<#@ assembly name="[assembly strong name|assembly file name]" #>
	public class AssemblyDirective : DirectiveBase {
		private TemplateParser FParser;
		private string FName;
		private string FFullName;

		public AssemblyDirective(TemplateParser parser, string tagcontent) : base() {
			FParser = parser;
			FName = ExtractParam(tagcontent, "name");
			if (FName == null) {
				throw new ArgumentNullException("name", "An assembly directive must specify a value for the name attribute");
			}
			ResolvePath(FName);
		}

		private void ResolvePath(String name) {
			if (name.ContainsIC("$(ProjectDir)")) {
				string OProjectDir = getProjectDir();
				if (OProjectDir != null) {
					name = name.Replace("$(ProjectDir)", OProjectDir);
				}
			}
			if (name.ContainsIC("$(SolutionDir)")) {
				string OSolutionDir = getSolutionDir();
				if (OSolutionDir != null) {
					name = name.Replace("$(SolutionDir)", OSolutionDir);
				}
			}
			if (name.StartsWith("..")) {
				name = Path.Combine(new FileInfo(FParser.Filename).DirectoryName, name);
			}
			if (name[1] != ':' && FParser.Options.AssemblyResolveDirectory != null) {
				name = Path.Combine(FParser.Options.AssemblyResolveDirectory, name);
			}
			FFullName = Path.GetFullPath(name);
		}

		private string getProjectDir() {
			FileInfo OFileInfo = new FileInfo(FParser.Filename);
			return getProjectDirRecursive(OFileInfo.DirectoryName);
		}

		private string getProjectDirRecursive(string dir) {
			DirectoryInfo ODirectoryInfo = new DirectoryInfo(dir);
			if (ODirectoryInfo.GetFiles("*.csproj", SearchOption.TopDirectoryOnly).Length > 0) {
				return dir;
			}
			else {
				DirectoryInfo OParentDir = ODirectoryInfo.Parent;
				if (OParentDir != null) {
					return getProjectDirRecursive(OParentDir.FullName);
				}
				else {
					return null;
				}
			}
		}

		private string getSolutionDir() {
			FileInfo OFileInfo = new FileInfo(FParser.Filename);
			return getSolutionDirRecursive(OFileInfo.DirectoryName);
		}

		private string getSolutionDirRecursive(string dir) {
			DirectoryInfo ODirectoryInfo = new DirectoryInfo(dir);
			if (ODirectoryInfo.GetFiles("*.sln", SearchOption.TopDirectoryOnly).Length > 0) {
				return dir;
			}
			else {
				DirectoryInfo OParentDir = ODirectoryInfo.Parent;
				if (OParentDir != null) {
					return getSolutionDirRecursive(OParentDir.FullName);
				}
				else {
					return null;
				}
			}
		}

		public string Name {
			get {
				return FName;
			}
		}

		public string FullName {
			get {
				return FFullName;
			}
		}
	}
}