// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class ConfigurationFile {
		private string FFilename;
		private XDocument FDocument;
		private bool IsLoaded = false;
		private Configuration FConfiguration;

		public ConfigurationFile(String filename) {
			FFilename = filename;
		}

		private void Load() {
			IsLoaded = true;
			FDocument = XDocument.Load(FFilename);
			XElement ORoot = FDocument.Element(XmlNamespace + "sqline");
			if (ORoot.Element(XmlNamespace + "configuration") != null) {
				Debug.WriteLine("Loaded configuration: " + FFilename);
				FConfiguration = new Configuration(ORoot.Element(XmlNamespace + "configuration"), XmlNamespace);
			}
		}

		private void EnsureLoaded() {
			if (!IsLoaded) {
				Load();
			}
		}

		internal static XNamespace XmlNamespace {
			get {
				return "http://sqline.net/schemas/config.xsd";
			}
		}

		public Configuration Configuration {
			get {
				EnsureLoaded();
				return FConfiguration;
			}
		}

		public string Filename {
			get {
				return FFilename;
			}
		}
	}
}
