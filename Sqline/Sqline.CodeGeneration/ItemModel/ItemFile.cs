// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Sqline.Base;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class ItemFile : IOwner {
		private string FFilename;
		private string FProjectRoot;
		private string FItemName;
		private XDocument FDocument;
		private Configuration FConfiguration;
		private List<ViewItem> FViewItems = new List<ViewItem>();
		private List<ScalarItem> FScalarItems = new List<ScalarItem>();
		private List<VoidItem> FVoidItems = new List<VoidItem>();

		public ItemFile(string projectRoot, string filename) {
			FProjectRoot = projectRoot;
			FFilename = filename;
			FDocument = XDocument.Load(filename, LoadOptions.SetLineInfo);
			ParseDocument();
		}

		private void ParseDocument() {
			ConfigurationSystem OConfigurationSystem = new ConfigurationSystem(FProjectRoot);
			FileInfo OFileInfo = new FileInfo(FFilename).GetCorrectlyCasedFileInfo();
			FItemName = OFileInfo.GetNameWithoutExt();
			XElement ORoot = FDocument.Element(XmlNamespace + "items");
			Configuration OConfiguration = OConfigurationSystem.GetConfigurationFor(OFileInfo.DirectoryName);
			FConfiguration = OConfiguration;
			if (ORoot.Element(XmlNamespace + "configuration") != null) {
				FConfiguration = new Configuration(ORoot.Element(XmlNamespace + "configuration"), XmlNamespace);
				if (OConfiguration != null) {
					OConfiguration.Append(FConfiguration);
					FConfiguration = OConfiguration;
				}
			}
			foreach (XElement OViewItem in ORoot.Elements(XmlNamespace + "viewitem")) {
				FViewItems.Add(new ViewItem(this, FConfiguration, OViewItem));
			}
			foreach (XElement OScalarItem in ORoot.Elements(XmlNamespace + "scalar")) {
				FScalarItems.Add(new ScalarItem(this, FConfiguration, OScalarItem));
			}
			foreach (XElement OVoidItem in ORoot.Elements(XmlNamespace + "void")) {
				FVoidItems.Add(new VoidItem(this, FConfiguration, OVoidItem));
			}
		}

		public void Throw(XObject element, string message) {
			IXmlLineInfo OInfo = (IXmlLineInfo)element;
			throw new SqlineException(FFilename, OInfo.LineNumber - 1, OInfo.LinePosition - 1, message);
		}

		public List<ViewItem> ViewItems {
			get {
				return FViewItems;
			}
		}

		public List<ScalarItem> ScalarItems {
			get {
				return FScalarItems;
			}
		}

		public List<VoidItem> VoidItems {
			get {
				return FVoidItems;
			}
		}

		public Configuration Configuration {
			get {
				return FConfiguration;
			}
		}

		public string ItemName {
			get {
				return FItemName;
			}
		}

		internal static XNamespace XmlNamespace {
			get {
				return "http://sqline.net/schemas/items.xsd";
			}
		}
	}
}