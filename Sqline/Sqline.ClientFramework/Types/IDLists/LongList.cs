using System.Collections.Generic;

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