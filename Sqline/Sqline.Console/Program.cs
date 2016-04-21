// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using Sqline.ClientFramework;
using Sqlingo;
using Sqlingo.Statement;

namespace Sqline.ConsoleApp {
	public class Program {
		static void Main(string[] args) {
			ProviderTest OTest = new ProviderTest();
			OTest.Execute();
		}
	}
}