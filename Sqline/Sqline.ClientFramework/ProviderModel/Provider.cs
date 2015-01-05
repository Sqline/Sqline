// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel.SqlServer;

namespace Sqline.ClientFramework.ProviderModel {
	public class Provider {
		public static /*IProvider*/ SqlServerProvider Current = new SqlServerProvider("", ""); //HACK: Needs refactoring
	}
}