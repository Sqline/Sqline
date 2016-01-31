// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqline.ClientFramework;

namespace Sqline.UnitTests {
	[TestClass]
	public class ParamTests {

		[TestMethod]
		public void ValueParamTest() {
			ValueParam<string> OParam = new ValueParam<string>("Hello World!");
			Assert.IsTrue(OParam == "Hello World!");
		}
		
		[TestMethod]
		public void NullableValueParamTest() {
			NullableValueParam<string> OParam = new NullableValueParam<string>("Hello World!");
			OParam = DBNull.Value;
			Assert.IsTrue(OParam == DBNull.Value);
		}
	}
}