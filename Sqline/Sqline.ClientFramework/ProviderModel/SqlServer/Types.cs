// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sqline.ClientFramework.ProviderModel.SqlServer {
	public class Types {
		private Dictionary<string, TypeMapping> FTypes = new Dictionary<string, TypeMapping>();
		private TypeMapping FEmptyMapping;

		public Types() {
			PopulateTypes();
			FEmptyMapping = new TypeMapping() {
				ProviderType = "Unknown",
				DBType = "Unknown",
				CSType = "Unknown",
				CSNullable = "Unknown",
				CSReader = "Unknown",
				IsNumber = false
			};
			FTypes.Add("IntList", new TypeMapping() {
				ProviderType = "IntList",
				DBType = "Unknown",
				CSType = "IntList",
				CSNullable = "IntList",
				CSReader = "Unknown",
				IsNumber = false
			});
			FTypes.Add("LongList", new TypeMapping() {
				ProviderType = "LongList",
				DBType = "Unknown",
				CSType = "LongList",
				CSNullable = "LongList",
				CSReader = "Unknown",
				IsNumber = false
			});
			FTypes.Add("StringList", new TypeMapping() {
				ProviderType = "StringList",
				DBType = "Unknown",
				CSType = "StringList",
				CSNullable = "StringList",
				CSReader = "Unknown",
				IsNumber = false
			});
			FTypes.Add("DateList", new TypeMapping() {
				ProviderType = "DateList",
				DBType = "Unknown",
				CSType = "DateList",
				CSNullable = "DateList",
				CSReader = "Unknown",
				IsNumber = false
			});
		}
		
		private void PopulateTypes() {
			using (Stream OStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Sqline.ClientFramework.ProviderModel.SqlServer.Types.xml")) {
				XDocument ODoc = XDocument.Load(OStream);
				foreach (XElement OElement in ODoc.Descendants("type")) {
					TypeMapping OTypeMapping = new TypeMapping() {
							ProviderType = OElement.Attribute("providertype").Value,
							DBType = OElement.Attribute("dbtype").Value,
							CSType = OElement.Attribute("cstype").Value,
							CSNullable = OElement.Attribute("csnullable").Value,
							CSReader = OElement.Attribute("csreader").Value,
							IsNumber = OElement.Attribute("isnumber").Value == "1"
					};
					FTypes.Add(OTypeMapping.ProviderType, OTypeMapping);
				}
			}
		}

		public TypeMapping GetTypeMapping(string providerType) {
			if (FTypes.ContainsKey(providerType)) {
				return FTypes[providerType];
			}
			else {
				return FEmptyMapping;
			}
		}
	}
}
