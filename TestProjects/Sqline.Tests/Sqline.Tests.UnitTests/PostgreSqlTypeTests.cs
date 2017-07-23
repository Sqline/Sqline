using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqline.Tests.DataAccess.PostgreSql;
using Sqline.Tests.DataAccess.PostgreSql.DataItems;
using System;

namespace Sqline.Tests.UnitTests
{
	[TestClass]
	public class PostgreSqlPostgreSqlTypeTests : PostgreSqlBaseTest
	{
		[TestMethod]
		public void PostgreSqlTypeTest_Integer() {
			int value = 2104589548;
			DeleteTypeTest(x => x.WhereIntegerColumn = x.WhereIntegerColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { IntegerColumn = value });
			UpdateTypeTest(new TypeTestUpdate { IntegerColumn = value }, x => x.WhereIntegerColumn = x.WhereIntegerColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetInt(), value);
		}
	}
}