// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {

	public class BaseMethod : IOwner {
		protected Configuration FConfiguration;
		protected IOwner FOwner;
		protected List<Parameter> FParameters = new List<Parameter>();
		protected string FName;
		protected string FVisibility = "public";
		protected int FTimeout;
		protected Sql FSql;
		protected bool FTransactionSupport;

		public BaseMethod(IOwner owner, Configuration configuration, XElement element) {
			FConfiguration = configuration;
			FOwner = owner;

			if (element.Attribute("name") == null) {
				Throw(element, "The required attribute 'name' is missing.");
			}
			FName = element.Attribute("name").Value;

			if (element.Element(ItemFile.XmlNamespace + "sql") != null) {
				FSql = new Sql(this, element.Element(ItemFile.XmlNamespace + "sql"));
			}
			else {
				Throw(element, "The required element 'sql' is missing.");
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
			FOwner.Throw(element, message);
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