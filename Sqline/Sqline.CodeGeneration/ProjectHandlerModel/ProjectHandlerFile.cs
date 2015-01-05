// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Xml.Linq;
using Schemalizer.Model;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class ProjectHandlerFile {
		private string FProjectRoot;
		private Configuration FConfiguration;

		public ProjectHandlerFile(string projectRoot) {
			FProjectRoot = projectRoot;
			InitConfiguration();
		}

		private void InitConfiguration() {
			ConfigurationSystem OConfigurationSystem = new ConfigurationSystem(FProjectRoot);
			Configuration OConfiguration = OConfigurationSystem.GetConfigurationFor(FProjectRoot);
			FConfiguration = OConfiguration;
		}

		public Configuration Configuration {
			get {
				return FConfiguration;
			}
		}
	}
}