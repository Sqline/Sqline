using System;
using System.Collections.Generic;

namespace Sqline.ClientFramework {
    public class DateList: IDList<DateTime> {

		public DateList() : base() {
		}

		public DateList(IEnumerable<DateTime> values) : base(values) {
		}

		public DateList(params DateTime[] values) : base(values) {
		}
	}
}