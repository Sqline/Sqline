// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ConfigurationModel {
	public class Configuration {
		public const string EMPTY_NAMESPACE = "NoNameSpace.Defined";
		private ConnectionString FConnectionString;
		private ViewItems FViewItems;
		private DataItems FDataItems;
		private Methods FMethods;
		private ProjectHandler FProjectHandler;

		public Configuration(XElement element, XNamespace xmlNamespace) {
			XElement OConnectionString = element.Element(xmlNamespace + "connectionstring");
			FConnectionString = OConnectionString != null ? new ConnectionString(OConnectionString, xmlNamespace) : new ConnectionString();

			XElement OViewItems = element.Element(xmlNamespace + "viewitems");
			FViewItems = OViewItems != null ? new ViewItems(OViewItems, xmlNamespace) : new ViewItems();

			XElement ODataItems = element.Element(xmlNamespace + "dataitems");
			FDataItems = ODataItems != null ? new DataItems(ODataItems, xmlNamespace) : new DataItems();

			XElement OMethods = element.Element(xmlNamespace + "methods");
			FMethods = OMethods != null ? new Methods(OMethods, xmlNamespace) : new Methods();

			XElement OProjectHandler = element.Element(xmlNamespace + "projecthandler");
			FProjectHandler = OProjectHandler != null ? new ProjectHandler(OProjectHandler, xmlNamespace) : new ProjectHandler();

			if (FProjectHandler.Namespace == null) { 
				FProjectHandler.Namespace = FMethods.Namespace;
			}
			if (FProjectHandler.Name == null) {
				FProjectHandler.Name = "DAHandler";
			}
		}

		public void Append(Configuration configuration) {
			if (!configuration.FConnectionString.IsEmpty) {
				FConnectionString = configuration.FConnectionString;
			}
			FViewItems.Append(configuration.ViewItems);
			FMethods.Append(configuration.Methods);
		}

		public ViewItems ViewItems {
			get {
				return FViewItems;
			}
		}

		public DataItems DataItems {
			get {
				return FDataItems;
			}
		}

		public Methods Methods { 
			get {
				return FMethods;
			}
		}

		public ProjectHandler ProjectHandler {
			get {
				return FProjectHandler;
			}
		}

		public ConnectionString ConnectionString {
			get {
				return FConnectionString;
			}
		}
	}
}