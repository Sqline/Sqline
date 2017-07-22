using System.Collections.Generic;

namespace Sqline.ClientFramework {
    public class StringList: IDList<string> {

		public StringList() : base() {
		}

		public StringList(IEnumerable<string> values) : base(values) {
		}

		public StringList(params string[] values) : base(values) {
		}
	}
}