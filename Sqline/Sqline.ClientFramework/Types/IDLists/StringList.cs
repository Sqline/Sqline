using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public class StringList: IDList<string> {

		public StringList() : base() {
		}

		public StringList(params string[] values) : base(values) {
		}
	}
}