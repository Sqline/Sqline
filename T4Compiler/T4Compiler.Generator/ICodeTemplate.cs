// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;

namespace T4Compiler.Generator {
	public interface ICodeTemplate {
		void SetParameter(string name, object value);
		string Generate();
	}
}
