using System.IO;
using EnvDTE;
using Sqline.ClientFramework;
using ConfigModel = Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.VSPackage {
	
	public class SqlineProject {
		private SqlineApplication FApplication;
		private Project FProject;
		private string FProjectDir;
		private ConfigModel.Configuration FConfiguration;

		public SqlineProject(SqlineApplication application, Project project) {
			FApplication = application;
			FProject = project;
			FProjectDir = new FileInfo(FProject.FullName).DirectoryName;
			Initialize();
		}

		private void Initialize() {
			ConfigModel.ConfigurationSystem OConfigurationSystem = new ConfigModel.ConfigurationSystem(FProjectDir);
			FConfiguration = OConfigurationSystem.GetConfigurationFor(FProjectDir);
			FApplication.Initialize(FConfiguration.ConnectionString.Value, FConfiguration.ConnectionString.Provider);
		}

		public string ProjectDir {
			get { return FProjectDir; }
		}

		public Project Project {
			get { return FProject; }
		}
		public ConfigModel.Configuration Configuration
		{
			get { return FConfiguration; }
		}

	}
}