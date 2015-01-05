// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class ConfigurationSystem {
		private DirectoryInfo FProjectDirectory;
		private Dictionary<string, ConfigurationFile> FConfigurationFiles = new Dictionary<string, ConfigurationFile>();

		public ConfigurationSystem(String projectDirectory) {
			Debug.WriteLine("ConfigurationSystem::ProjectDir: " + projectDirectory);
			FProjectDirectory = new DirectoryInfo(projectDirectory);
			SearchConfigurationFiles(FProjectDirectory);
		}

		private void SearchConfigurationFiles(DirectoryInfo directory) {
			string OPath = Path.Combine(directory.FullName, "Sqline.config");
			if (File.Exists(OPath)) {
				string ORelativePath = GetRelativePath(directory.FullName);
				FConfigurationFiles.Add(ORelativePath, new ConfigurationFile(OPath));
			}
			foreach (DirectoryInfo ODirectory in directory.GetDirectories()) {
				SearchConfigurationFiles(ODirectory);
			}
		}

		public Configuration GetConfigurationFor(string path) {
			DirectoryInfo ODirectory = new DirectoryInfo(path);
			List<ConfigurationFile> OFiles = GetConfigurationFiles(ODirectory);
			if (OFiles.Count > 0) {
				Configuration ORootConfig = OFiles[OFiles.Count - 1].Configuration;
				for (int i = OFiles.Count - 2; i >= 0; i--) {
					ORootConfig.Append(OFiles[i].Configuration);
				}
				return ORootConfig;
			}
			return null;
		}

		private List<ConfigurationFile> GetConfigurationFiles(DirectoryInfo directory) {
			List<ConfigurationFile> OResult = new List<ConfigurationFile>();
			if (!directory.FullName.StartsWith(FProjectDirectory.FullName, StringComparison.OrdinalIgnoreCase)) {
				return OResult;
			}
			while (directory.FullName.Length >= FProjectDirectory.FullName.Length) {
				String OPath = GetRelativePath(directory.FullName);
				if (FConfigurationFiles.ContainsKey(OPath)) {
					OResult.Add(FConfigurationFiles[OPath]);
				}
				directory = directory.Parent;
			}
			return OResult;
		}

		private string GetRelativePath(string fullpath) {
			return fullpath.Substring(FProjectDirectory.FullName.Length).ToUpper();
		}
	}
}
