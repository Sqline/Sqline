using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework.Types {
	public class StringIDList: IDList<string> {

		public StringIDList() : base() {
		}

		public StringIDList(params string[] values) : base(values) {
		}
	}
}