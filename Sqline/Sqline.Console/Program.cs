// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using Sqline.ClientFramework;

namespace Sqline.Console {
	public class Program {
		static void Main(string[] args) {
			ValueParam<int> OParam1 = new ValueParam<int>(1);
			ValueParam<byte[]> OParam2 = new ValueParam<byte[]>(new byte[0]);

			NullableValueParam<string> OParam = new NullableValueParam<string>("Hello World!");
			OParam = DBNull.Value;
			System.Console.WriteLine((OParam == DBNull.Value));

			int OInt = OParam1;
			byte[] OBytes = OParam2;
			string OString = OParam;
		}
	}
}