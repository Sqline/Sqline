// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {

	public enum Visibility { Public, Protected, Internal, InternalProtected, Private };

	public class BaseMethod : IOwner {
		protected Configuration FConfiguration;
		protected IOwner FOwner;
		protected List<Parameter> FParameters = new List<Parameter>();
		protected string FName;
		protected Visibility FVisibility = Visibility.Public;
		protected string FVisibilityString = "public";
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
				ParseVisibility(element.Attribute("visibility"));
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

		private void ParseVisibility(XAttribute attribute) {
			string OVisibilityValue = attribute.Value;
			if (OVisibilityValue.Equals("public", StringComparison.OrdinalIgnoreCase)) {
				FVisibility = Visibility.Public;
				FVisibilityString = "public";
			}
			else if (OVisibilityValue.Equals("protected", StringComparison.OrdinalIgnoreCase)) {
				FVisibility = Visibility.Protected;
				FVisibilityString = "protected";
			}
			else if (OVisibilityValue.Equals("internal", StringComparison.OrdinalIgnoreCase)) {
				FVisibility = Visibility.Internal;
				FVisibilityString = "internal";
			}
			else if (OVisibilityValue.Equals("internal protected", StringComparison.OrdinalIgnoreCase)) {
				FVisibility = Visibility.InternalProtected;
				FVisibilityString = "internal protected";
			}
			else if (OVisibilityValue.Equals("private", StringComparison.OrdinalIgnoreCase)) {
				FVisibility = Visibility.Private;
				FVisibilityString = "private";
			}
			else {
				Throw(attribute, "Unable to parse Visibility attribute value '" + OVisibilityValue + "'");
			}
		}

		public void Throw(XObject element, string message) {
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

		public Visibility Visibility {
			get {
				return FVisibility;
			}
		}

		public string VisibilityString {
			get {
				return FVisibilityString;
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