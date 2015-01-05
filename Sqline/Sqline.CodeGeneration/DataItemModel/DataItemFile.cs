// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Xml.Linq;
using Schemalizer.Model;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class DataItemFile {
		private string FProjectRoot;
		private string FDatabaseFilePath;
		private SchemaViewModel FModel; 
		private Configuration FConfiguration;

		public DataItemFile(string projectRoot, string databaseFilePath) {
			FProjectRoot = projectRoot;
			FDatabaseFilePath = databaseFilePath;
			InitConfiguration();
			InitShemaModel();
		}

		private void InitConfiguration() {
			ConfigurationSystem OConfigurationSystem = new ConfigurationSystem(FProjectRoot);
			Configuration OConfiguration = OConfigurationSystem.GetConfigurationFor(FProjectRoot);
			FConfiguration = OConfiguration;
		}

		private void InitShemaModel() {
			XElement OElement = XElement.Load(FDatabaseFilePath);
			SchemaModel OModel = SchemaModel.FromXElement(OElement);
			FModel = new SchemaViewModel(OModel);
		}
		
		public Configuration Configuration {
			get {
				return FConfiguration;
			}
		}

		public SchemaViewModel Model {
			get {
				return FModel;
			}
		}
	}
}