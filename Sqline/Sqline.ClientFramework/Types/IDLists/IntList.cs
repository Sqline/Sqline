using System.Collections.Generic;

namespace Sqline.ClientFramework {
    public class IntList: IDList<int> {

		public IntList() : base() {
		}

		public IntList(IEnumerable<int> values) : base(values) {
		}

		public IntList(params int[] values) : base(values) {
		}
	}
}