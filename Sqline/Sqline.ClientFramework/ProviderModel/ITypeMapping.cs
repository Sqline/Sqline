// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sqline.ClientFramework.ProviderModel {
	public interface ITypeMapping {
		string ProviderType { get; }
		string DBType { get; }
		string CSType { get; }
		string CSNullable { get; }
		string CSReader { get; }
		bool IsNumber { get; }
	}
}
