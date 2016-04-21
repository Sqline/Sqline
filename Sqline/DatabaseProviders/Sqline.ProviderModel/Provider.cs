using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ProviderModel {
    public static class Provider {
        public static IProvider Current { get; private set; }

        public static void Initialize(IProvider provider) {
            Current = provider;
        }
	}
}