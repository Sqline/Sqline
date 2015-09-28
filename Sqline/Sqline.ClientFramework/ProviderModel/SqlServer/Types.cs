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
			FTypes.Add("IntIDList", new TypeMapping() {
				ProviderType = "IntIDList",
				DBType = "Unknown",
				CSType = "IntIDList",
				CSNullable = "IntIDList",
				CSReader = "Unknown",
				IsNumber = false
			});
			FTypes.Add("LongIDList", new TypeMapping() {
				ProviderType = "LongIDList",
				DBType = "Unknown",
				CSType = "LongIDList",
				CSNullable = "LongIDList",
				CSReader = "Unknown",
				IsNumber = false
			});
			FTypes.Add("StringIDList", new TypeMapping() {
				ProviderType = "StringIDList",
				DBType = "Unknown",
				CSType = "StringIDList",
				CSNullable = "StringIDList",
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
