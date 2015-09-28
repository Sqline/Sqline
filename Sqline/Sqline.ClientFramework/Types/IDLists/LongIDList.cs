using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework.Types {
	public class LongIDList: IDList<long> {

		public LongIDList() : base() {
		}

		public LongIDList(params long[] values) : base(values) {
		}
	}
}