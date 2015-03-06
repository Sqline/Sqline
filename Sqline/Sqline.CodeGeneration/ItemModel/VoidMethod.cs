// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Sqline.CodeGeneration.ConfigurationModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class VoidMethod : BaseMethod {
		private VoidItem FVoidItem;

		public VoidMethod(VoidItem voidItem, Configuration configuration, XElement element) : base(voidItem, configuration, element) {
			FVoidItem = voidItem;
		}

		public VoidItem VoidItem {
			get {
				return FVoidItem;
			}
		}
	}
}