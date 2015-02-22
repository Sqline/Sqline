// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class ScalarItem : IOwner {
		private List<Method> FMethods = new List<Method>();
		private IOwner FOwner;

		public ScalarItem(IOwner owner, Configuration configuration, XElement element) {
			FOwner = owner;
		}

		public void Throw(XElement element, string message) {
			FOwner.Throw(element, message);
		}
	}
}
