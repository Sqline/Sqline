using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqline.Tests.DataAccess.DataItems;
using Sqline.Tests.DataAccess.SqlServer;
using System.Diagnostics;

namespace Sqline.Tests.UnitTests {
	[TestClass]
	public class SqlServerSqlServerTypeTests : BaseTest {
	

		[TestMethod]
		public void SqlServerTypeTest_Bigint() {
			long value = 15444444444545554;
			DeleteTypeTest(x => x.WhereBigintColumn = x.WhereBigintColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { BigintColumn = value });
			UpdateTypeTest(new TypeTestUpdate { BigintColumn = value }, x => x.WhereBigintColumn = x.WhereBigintColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetBigInt(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Binary() {
			byte[] value = new byte[] { 0x16 };
			DeleteTypeTest(x => x.WhereBinaryColumn = x.WhereBinaryColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { BinaryColumn = value });
			UpdateTypeTest(new TypeTestUpdate { BinaryColumn = value }, x => x.WhereBinaryColumn = x.WhereBinaryColumn != DBNull.Value);			
			Assert.IsTrue(CompareBytes(DA.Types.GetBinary(), value));
		}

		[TestMethod]
		public void SqlServerTypeTest_Bit() {
			bool value = true;
			DeleteTypeTest(x => x.WhereBitColumn = x.WhereBitColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { BitColumn = value });
			UpdateTypeTest(new TypeTestUpdate { BitColumn = value }, x => x.WhereBitColumn = x.WhereBitColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetBit(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Char() {
			string value = "abcdefghij";
			DeleteTypeTest(x => x.WhereCharColumn = x.WhereCharColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { CharColumn = value });
			UpdateTypeTest(new TypeTestUpdate { CharColumn = value }, x => x.WhereCharColumn = x.WhereCharColumn != DBNull.Value);			
			Assert.AreEqual(DA.Types.GetChar(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Date() {
			DateTime value = DateTime.UtcNow.Date;
			DeleteTypeTest(x => x.WhereDateColumn = x.WhereDateColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DateColumn = value });
			UpdateTypeTest(new TypeTestUpdate { DateColumn = value }, x => x.WhereDateColumn = x.WhereDateColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetDate(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_DateTime() {
			DateTime value = DateTime.UtcNow;
			value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
			DeleteTypeTest(x => x.WhereDateTimeColumn = x.WhereDateTimeColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DateTimeColumn = value });
			UpdateTypeTest(new TypeTestUpdate { DateTimeColumn = value }, x => x.WhereDateTimeColumn = x.WhereDateTimeColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetDateTime(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_DateTime2() {
			DateTime value = DateTime.UtcNow;
			value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
			DeleteTypeTest(x => x.WhereDateTime2Column = x.WhereDateTime2Column != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DateTime2Column = value });
			UpdateTypeTest(new TypeTestUpdate { DateTime2Column = value }, x => x.WhereDateTime2Column = x.WhereDateTime2Column != DBNull.Value);
			Assert.AreEqual(DA.Types.GetDateTime2(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_DateTimeOffset() {
			DateTimeOffset value = DateTimeOffset.UtcNow;
			DeleteTypeTest(x => x.WhereDateTimeOffsetColumn = x.WhereDateTimeOffsetColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DateTimeOffsetColumn = value });
			UpdateTypeTest(new TypeTestUpdate { DateTimeOffsetColumn = value }, x => x.WhereDateTimeOffsetColumn = x.WhereDateTimeOffsetColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetDateTimeOffset(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Decimal() {
			decimal value = 3.1415926536m;
			DeleteTypeTest(x => x.WhereDecimalColumn = x.WhereDecimalColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DecimalColumn = value });
			UpdateTypeTest(new TypeTestUpdate { DecimalColumn = value }, x => x.WhereDecimalColumn = x.WhereDecimalColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetDecimal(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_FileStream() {
			byte[] value = new byte[] { 0x16 };
			DeleteTypeTest(x => x.WhereFileStreamColumn = x.WhereFileStreamColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { FileStreamColumn = value });
			UpdateTypeTest(new TypeTestUpdate { FileStreamColumn = value }, x => x.WhereFileStreamColumn = x.WhereFileStreamColumn != DBNull.Value);
			Assert.IsTrue(CompareBytes(DA.Types.GetFileStream(), value));
		}

		[TestMethod]
		public void SqlServerTypeTest_Float() {
			double value = 3.1415926536;
			DeleteTypeTest(x => x.WhereFloatColumn = x.WhereFloatColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { FloatColumn = value });
			UpdateTypeTest(new TypeTestUpdate { FloatColumn = value }, x => x.WhereFloatColumn = x.WhereFloatColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetFloat(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Image() {
			byte[] value = new byte[] { 0x16 };
			DeleteTypeTest(x => x.WhereImageColumn = x.WhereImageColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { ImageColumn = value });
			UpdateTypeTest(new TypeTestUpdate { ImageColumn = value }, x => x.WhereImageColumn = x.WhereImageColumn != DBNull.Value);
			Assert.IsTrue(CompareBytes(DA.Types.GetImage(), value));
		}

		[TestMethod]
		public void SqlServerTypeTest_Int() {
			int value = 2104589548;
			DeleteTypeTest(x => x.WhereIntColumn = x.WhereIntColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { IntColumn = value });
			UpdateTypeTest(new TypeTestUpdate { IntColumn = value }, x => x.WhereIntColumn = x.WhereIntColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetInt(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Money() {
			decimal value = 3.1416m;
			DeleteTypeTest(x => x.WhereMoneyColumn = x.WhereMoneyColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { MoneyColumn = value });
			UpdateTypeTest(new TypeTestUpdate { MoneyColumn = value }, x => x.WhereMoneyColumn = x.WhereMoneyColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetMoney(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_NChar() {
			string value = "abcdefgæøå";
			DeleteTypeTest(x => x.WhereNCharColumn = x.WhereNCharColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { NCharColumn = value });
			UpdateTypeTest(new TypeTestUpdate { NCharColumn = value }, x => x.WhereNCharColumn = x.WhereNCharColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetNChar(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_NText() {
			string value = "abcæøå";
			DeleteTypeTest(x => x.WhereNTextColumn = x.WhereNTextColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { NTextColumn = value });
			UpdateTypeTest(new TypeTestUpdate { NTextColumn = value }, x => x.WhereNTextColumn = x.WhereNTextColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetNText(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Numeric() {
			decimal value = 3.1416m;
			DeleteTypeTest(x => x.WhereNumericColumn = x.WhereNumericColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { NumericColumn = value });
			UpdateTypeTest(new TypeTestUpdate { NumericColumn = value }, x => x.WhereNumericColumn = x.WhereNumericColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetNumeric(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_NVarChar() {
			string value = "abcæøå";
			DeleteTypeTest(x => x.WhereNVarCharColumn = x.WhereNVarCharColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { NVarCharColumn = value });
			UpdateTypeTest(new TypeTestUpdate { NVarCharColumn = value }, x => x.WhereNVarCharColumn = x.WhereNVarCharColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetNVarChar(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Real() {
			float value = 3.14f;
			DeleteTypeTest(x => x.WhereRealColumn = x.WhereRealColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { RealColumn = value });
			UpdateTypeTest(new TypeTestUpdate { RealColumn = value }, x => x.WhereRealColumn = x.WhereRealColumn != DBNull.Value);
			Assert.IsTrue(Math.Abs(DA.Types.GetFloat() - value) < 1);
		}

		[TestMethod]
		public void SqlServerTypeTest_SmallDateTime() {
			DateTime value = DateTime.UtcNow;
			value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
			DeleteTypeTest(x => x.WhereSmallDateTimeColumn = x.WhereSmallDateTimeColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { SmallDateTimeColumn = value });
			UpdateTypeTest(new TypeTestUpdate { SmallDateTimeColumn = value }, x => x.WhereSmallDateTimeColumn = x.WhereSmallDateTimeColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetSmallDateTime(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_SmallInt() {
			short value = 16588;
			DeleteTypeTest(x => x.WhereSmallIntColumn = x.WhereSmallIntColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { SmallIntColumn = value });
			UpdateTypeTest(new TypeTestUpdate { SmallIntColumn = value }, x => x.WhereSmallIntColumn = x.WhereSmallIntColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetSmallInt(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_SmallMoney() {
			decimal value = 16588;
			DeleteTypeTest(x => x.WhereSmallMoneyColumn = x.WhereSmallMoneyColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { SmallMoneyColumn = value });
			UpdateTypeTest(new TypeTestUpdate { SmallMoneyColumn = value }, x => x.WhereSmallMoneyColumn = x.WhereSmallMoneyColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetSmallMoney(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Text() {
			string value = "abc";
			DeleteTypeTest(x => x.WhereTextColumn = x.WhereTextColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { TextColumn = value });
			UpdateTypeTest(new TypeTestUpdate { TextColumn = value }, x => x.WhereTextColumn = x.WhereTextColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetText(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Time() {
			TimeSpan value = DateTime.UtcNow - DateTime.Now.AddHours(-2);
			DeleteTypeTest(x => x.WhereTimeColumn = x.WhereTimeColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { TimeColumn = value });
			UpdateTypeTest(new TypeTestUpdate { TimeColumn = value }, x => x.WhereTimeColumn = x.WhereTimeColumn != DBNull.Value);
			Assert.AreEqual(DA.Types.GetTime(), value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Timestamp() {
			//TODO: Consider removing this type from the Value Property mappings and only supply a Where Property
			//      as this type cannot be explicitly inserted or updated			
			Assert.IsTrue(DA.Types.GetTimestamp().Length > 0);
		}

		[TestMethod]
		public void SqlServerTypeTest_TinyInt() {
			DeleteTypeTest(x => x.WhereTinyIntColumn = x.WhereTinyIntColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { TinyIntColumn = 255 });
			UpdateTypeTest(new TypeTestUpdate { TinyIntColumn = 255 }, x => x.WhereTinyIntColumn = x.WhereTinyIntColumn != DBNull.Value);
		}

		[TestMethod]
		public void SqlServerTypeTest_UniqueIdentifier() {
			DeleteTypeTest(x => x.WhereUniqueIdentifierColumn = x.WhereUniqueIdentifierColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { UniqueIdentifierColumn = Guid.NewGuid() });
			UpdateTypeTest(new TypeTestUpdate { UniqueIdentifierColumn = Guid.NewGuid() }, x => x.WhereUniqueIdentifierColumn = x.WhereUniqueIdentifierColumn != DBNull.Value);
		}

		[TestMethod]
		public void SqlServerTypeTest_VarBinary() {
			DeleteTypeTest(x => x.WhereVarBinaryColumn = x.WhereVarBinaryColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { VarBinaryColumn = new byte[] { 0x16 } });
			UpdateTypeTest(new TypeTestUpdate { VarBinaryColumn = new byte[] { 0x16 } }, x => x.WhereVarBinaryColumn = x.WhereVarBinaryColumn != DBNull.Value);
		}

		[TestMethod]
		public void SqlServerTypeTest_VarChar() {
			DeleteTypeTest(x => x.WhereVarCharColumn = x.WhereVarCharColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { VarCharColumn = "abc" });
			UpdateTypeTest(new TypeTestUpdate { VarCharColumn = "abc" }, x => x.WhereVarCharColumn = x.WhereVarCharColumn != DBNull.Value);
		}

		[TestMethod]
		public void SqlServerTypeTest_Xml() {
			DeleteTypeTest(x => x.WhereXmlColumn = x.WhereXmlColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { XmlColumn = @"<root></root>" });
			UpdateTypeTest(new TypeTestUpdate { XmlColumn = @"<root></root>" }, x => x.WhereXmlColumn = x.WhereXmlColumn != DBNull.Value);
		}
	}
}