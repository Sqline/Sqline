using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework.Types {
	public class IntIDList: IDList<int> {

		public IntIDList() : base() {
		}

		public IntIDList(params int[] values) : base(values) {
		}
	}
}