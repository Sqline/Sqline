// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.ClientFramework.ProviderModel;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class ScalarMethod : IOwner {
		private Configuration FConfiguration;
		private ScalarItem FScalarItem;
		private List<Parameter> FParameters = new List<Parameter>();
		private string FName;
		private string FVisibility = "public";
		private int FTimeout;
		private Sql FSql;
		private bool FTransactionSupport;

		public ScalarMethod(ScalarItem scalarItem, Configuration configuration, XElement element) {
			FConfiguration = configuration;
			FScalarItem = scalarItem;
			if (element.Attribute("name") == null) {
				FScalarItem.Throw(element, "The required attribute 'name' is missing.");
			}
			FName = element.Attribute("name").Value;

			if (element.Element(ItemFile.XmlNamespace + "sql") != null) {
				FSql = new Sql(this, element.Element(ItemFile.XmlNamespace + "sql"));
			}
			else {
				FScalarItem.Throw(element, "The required element 'sql' is missing.");
			}

			if (element.Attribute("visibility") != null) {
				FVisibility = element.Attribute("visibility").Value;
			}
			FTimeout = FConfiguration.Methods.Timeout;
			if (element.Attribute("timeout") != null) {
				FTimeout = int.Parse(element.Attribute("timeout").Value);
			}

			if (element.Attribute("transactionsupport") != null) {
				FTransactionSupport = element.Attribute("transactionsupport").Value == "true";
			}

			foreach (XElement OParameter in element.Elements(ItemFile.XmlNamespace + "parameter")) {
				FParameters.Add(new Parameter(this, OParameter));
			}
		}


		public void Throw(XElement element, string message) {
			FScalarItem.Throw(element, message);
		}

		public string Name {
			get {
				return FName;
			}
			set {
				FName = value;
			}
		}

		public string Visibility {
			get {
				return FVisibility;
			}
		}

		public Sql Sql {
			get {
				return FSql;
			}
			set {
				FSql = value;
			}
		}

		public int Timeout {
			get {
				return FTimeout;
			}
		}

		public List<Parameter> Parameters {
			get {
				return FParameters;
			}
		}

		public bool TransactionSupport {
			get {
				return FTransactionSupport;
			}
			set {
				FTransactionSupport = value;
			}
		}
	}
}
