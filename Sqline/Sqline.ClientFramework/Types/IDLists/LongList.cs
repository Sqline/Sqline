using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public class LongList: IDList<long> {

		public LongList() : base() {
		}

		public LongList(IEnumerable<long> values) : base(values) {
		}

		public LongList(params long[] values) : base(values) {
		}
	}
}