// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Sqline.ClientFramework {
    public class Types {
        protected Dictionary<string, TypeMapping> FTypes = new Dictionary<string, TypeMapping>();
        protected TypeMapping FEmptyMapping;
        protected Assembly FAssembly;
        protected string FTypeResourceName;

        public Types(Assembly assembly, string typeResourceName) {
            FAssembly = assembly;
            FTypeResourceName = typeResourceName;
            PopulateTypes();
            FEmptyMapping = new TypeMapping() {
                ProviderType = "Unknown",
                DBType = "Unknown",
                CSType = "Unknown",
                CSNullable = "Unknown",
                CSReader = "Unknown",
                IsNumber = false
            };
        }

        protected void PopulateTypes() {
            using (Stream OStream = FAssembly.GetManifestResourceStream(FTypeResourceName)) {
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

        public virtual TypeMapping GetTypeMapping(string providerType) {
            if (FTypes.ContainsKey(providerType)) {
                return FTypes[providerType];
            }
            else {
                return FEmptyMapping;
            }
        }
    }
}
