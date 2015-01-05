// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Schemalizer.Model {
	public class Database : IXmlSerializable {
		protected SchemaModel FModel;
		protected String FName;
		protected DateTime FLastModified;
		protected Dictionary<String, String> FAdddionalMetadata = new Dictionary<String, String>();

		internal Database(String name, SchemaModel model) {
			FName = name;
			FModel = model;
		}

		internal void QueryLastModifiedDate(DateTime lastModifiedDate) {
				if (lastModifiedDate > FLastModified) {
							FLastModified = lastModifiedDate;
				}
		}

		public void AddAdditionalMetadata(String key, String value) {
			if (!FAdddionalMetadata.ContainsKey(key)) {
				FAdddionalMetadata.Add(key, value);
			}
		}

		public XElement ToXElement() {
			XElement OElement = new XElement("Database", new XAttribute("Name", Name), new XAttribute("LastModified", LastModified));
			foreach (String OKey in FAdddionalMetadata.Keys) {
				OElement.Add(new XAttribute(OKey, FAdddionalMetadata[OKey]));
			}
			return OElement;
		}

		public XElement ToRelationshipXElement() {
			XElement OElement = new XElement("DatabaseRelationship", new XAttribute("Name", Name));
			return OElement;
		}

		public static Database FromXElement(XElement element, SchemaModel model) {
			Database ODatabase = new Database(element.Attribute("Name").Value, model) {
				FLastModified = Convert.ToDateTime(element.Attribute("LastModified").Value)
			};
			foreach (XAttribute OAttribute in element.Attributes()) {
				String OKey = OAttribute.Name.LocalName;
				if (OKey != "Name" && OKey != "LastModified") {
					ODatabase.FAdddionalMetadata.Add(OKey, OAttribute.Value);
				}
			}
			return ODatabase;
		}

		public String this[String key] {
			get {
				if (!FAdddionalMetadata.ContainsKey(key)) {
					return null;
				}
				return FAdddionalMetadata[key];
			}
		}

		public String Name {
			get {
				return FName;
			}
		}

		public DateTime LastModified {
			get {
				return FLastModified;
			}
		}
	}
}
