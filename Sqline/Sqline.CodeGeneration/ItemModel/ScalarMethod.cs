// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.ClientFramework.ProviderModel;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class ScalarMethod : ResultMethod {
		private ScalarItem FScalarItem;

		public ScalarMethod(ScalarItem scalarItem, Configuration configuration, XElement element) : base(scalarItem, configuration, element) {
			FScalarItem = scalarItem;
		}

		public ScalarItem ScalarItem {
			get {
				return FScalarItem;
			}
		}
	}
}
