using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework {
	public class IntList: IDList<int> {

		public IntList() : base() {
		}

		public IntList(params int[] values) : base(values) {
		}
	}
}