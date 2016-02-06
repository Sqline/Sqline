using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqline.Tests.DataAccess.DataItems;

namespace Sqline.Tests.UnitTests {
	[TestClass]
	public class TypeTests : BaseTest {

		[TestMethod]
		public void TypeTest_Bigint() {
			DeleteTypeTest(x => x.WhereBigintColumn = x.WhereBigintColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { BigintColumn = 15444444444545554 });
			UpdateTypeTest(new TypeTestUpdate { BigintColumn = 15444444444545554 }, x => x.WhereBigintColumn = x.WhereBigintColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Binary() {
			DeleteTypeTest(x => x.WhereBinaryColumn = x.WhereBinaryColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { BinaryColumn = new byte[] { 0x16 } });
			UpdateTypeTest(new TypeTestUpdate { BinaryColumn = new byte[] { 0x16 } }, x => x.WhereBinaryColumn = x.WhereBinaryColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Bit() {
			DeleteTypeTest(x => x.WhereBitColumn = x.WhereBitColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { BitColumn = true });
			UpdateTypeTest(new TypeTestUpdate { BitColumn = true }, x => x.WhereBitColumn = x.WhereBitColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Char() {
			DeleteTypeTest(x => x.WhereCharColumn = x.WhereCharColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { CharColumn = "abc" });
			UpdateTypeTest(new TypeTestUpdate { CharColumn = "abc" }, x => x.WhereCharColumn = x.WhereCharColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Date() {
			DeleteTypeTest(x => x.WhereDateColumn = x.WhereDateColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DateColumn = DateTime.UtcNow });
			UpdateTypeTest(new TypeTestUpdate { DateColumn = DateTime.UtcNow }, x => x.WhereDateColumn = x.WhereDateColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_DateTime() {
			DeleteTypeTest(x => x.WhereDateTimeColumn = x.WhereDateTimeColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DateTimeColumn = DateTime.UtcNow });
			UpdateTypeTest(new TypeTestUpdate { DateTimeColumn = DateTime.UtcNow }, x => x.WhereDateTimeColumn = x.WhereDateTimeColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_DateTime2() {
			DeleteTypeTest(x => x.WhereDateTime2Column = x.WhereDateTime2Column != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DateTime2Column = DateTime.UtcNow });
			UpdateTypeTest(new TypeTestUpdate { DateTime2Column = DateTime.UtcNow }, x => x.WhereDateTime2Column = x.WhereDateTime2Column != DBNull.Value);			
		}

		[TestMethod]
		public void TypeTest_DateTimeOffset() {
			DeleteTypeTest(x => x.WhereDateTimeOffsetColumn = x.WhereDateTimeOffsetColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DateTimeOffsetColumn = DateTimeOffset.Now });
			UpdateTypeTest(new TypeTestUpdate { DateTimeOffsetColumn = DateTimeOffset.Now }, x => x.WhereDateTimeOffsetColumn = x.WhereDateTimeOffsetColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Decimal() {
			DeleteTypeTest(x => x.WhereDecimalColumn = x.WhereDecimalColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { DecimalColumn = 3.1415926536m });
			UpdateTypeTest(new TypeTestUpdate { DecimalColumn = 3.1415926536m }, x => x.WhereDecimalColumn = x.WhereDecimalColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_FileStream() {
			DeleteTypeTest(x => x.WhereFileStreamColumn = x.WhereFileStreamColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { FileStreamColumn = new byte[] { 0x16 } });
			UpdateTypeTest(new TypeTestUpdate { FileStreamColumn = new byte[] { 0x16 } }, x => x.WhereFileStreamColumn = x.WhereFileStreamColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Float() {
			DeleteTypeTest(x => x.WhereFloatColumn = x.WhereFloatColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { FloatColumn = 3.1415926536 });
			UpdateTypeTest(new TypeTestUpdate { FloatColumn = 3.1415926536 }, x => x.WhereFloatColumn = x.WhereFloatColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Image() {
			DeleteTypeTest(x => x.WhereImageColumn = x.WhereImageColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { ImageColumn = new byte[] { 0x16 } });
			UpdateTypeTest(new TypeTestUpdate { ImageColumn = new byte[] { 0x16 } }, x => x.WhereImageColumn = x.WhereImageColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Int() {
			DeleteTypeTest(x => x.WhereIntColumn = x.WhereIntColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { IntColumn = 2104589548 });
			UpdateTypeTest(new TypeTestUpdate { IntColumn = 2104589548 }, x => x.WhereIntColumn = x.WhereIntColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Money() {
			DeleteTypeTest(x => x.WhereMoneyColumn = x.WhereMoneyColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { MoneyColumn = 3.1415926536m });
			UpdateTypeTest(new TypeTestUpdate { MoneyColumn = 3.1415926536m }, x => x.WhereMoneyColumn = x.WhereMoneyColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_NChar() {
			DeleteTypeTest(x => x.WhereNCharColumn = x.WhereNCharColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { NCharColumn = "abcæøå" });
			UpdateTypeTest(new TypeTestUpdate { NCharColumn = "abcæøå" }, x => x.WhereNCharColumn = x.WhereNCharColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_NText() {
			DeleteTypeTest(x => x.WhereNTextColumn = x.WhereNTextColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { NTextColumn = "abcæøå" });
			UpdateTypeTest(new TypeTestUpdate { NTextColumn = "abcæøå" }, x => x.WhereNTextColumn = x.WhereNTextColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Numeric() {
			DeleteTypeTest(x => x.WhereNumericColumn = x.WhereNumericColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { NumericColumn = 3.1415926536m });
			UpdateTypeTest(new TypeTestUpdate { NumericColumn = 3.1415926536m }, x => x.WhereNumericColumn = x.WhereNumericColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_NVarChar() {
			DeleteTypeTest(x => x.WhereNVarCharColumn = x.WhereNVarCharColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { NVarCharColumn = "abcæøå" });
			UpdateTypeTest(new TypeTestUpdate { NVarCharColumn = "abcæøå" }, x => x.WhereNVarCharColumn = x.WhereNVarCharColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Real() {
			DeleteTypeTest(x => x.WhereRealColumn = x.WhereRealColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { RealColumn = 3.1415926536f });
			UpdateTypeTest(new TypeTestUpdate { RealColumn = 3.1415926536f }, x => x.WhereRealColumn = x.WhereRealColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_SmallDateTime() {
			DeleteTypeTest(x => x.WhereSmallDateTimeColumn = x.WhereSmallDateTimeColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { SmallDateTimeColumn = DateTime.UtcNow });
			UpdateTypeTest(new TypeTestUpdate { SmallDateTimeColumn = DateTime.UtcNow }, x => x.WhereSmallDateTimeColumn = x.WhereSmallDateTimeColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_SmallInt() {
			DeleteTypeTest(x => x.WhereSmallIntColumn = x.WhereSmallIntColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { SmallIntColumn = 16588 });
			UpdateTypeTest(new TypeTestUpdate { SmallIntColumn = 16588 }, x => x.WhereSmallIntColumn = x.WhereSmallIntColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_SmallMoney() {
			DeleteTypeTest(x => x.WhereSmallMoneyColumn = x.WhereSmallMoneyColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { SmallMoneyColumn = 16588 });
			UpdateTypeTest(new TypeTestUpdate { SmallMoneyColumn = 16588 }, x => x.WhereSmallMoneyColumn = x.WhereSmallMoneyColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Text() {
			DeleteTypeTest(x => x.WhereTextColumn = x.WhereTextColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { TextColumn = "abc" });
			UpdateTypeTest(new TypeTestUpdate { TextColumn = "abc" }, x => x.WhereTextColumn = x.WhereTextColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Time() {
			DeleteTypeTest(x => x.WhereTimeColumn = x.WhereTimeColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { TimeColumn = DateTime.UtcNow - DateTime.Now.AddHours(-2) });
			UpdateTypeTest(new TypeTestUpdate { TimeColumn = DateTime.UtcNow - DateTime.Now.AddHours(-2) }, x => x.WhereTimeColumn = x.WhereTimeColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Timestamp() {
			//TODO: Consider removing this type from the Value Property mappings and only supply a Where Property
			//      as this type cannot be explicitly inserted or updated			
		}

		[TestMethod]
		public void TypeTest_TinyInt() {
			DeleteTypeTest(x => x.WhereTinyIntColumn = x.WhereTinyIntColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { TinyIntColumn = 255 });
			UpdateTypeTest(new TypeTestUpdate { TinyIntColumn = 255 }, x => x.WhereTinyIntColumn = x.WhereTinyIntColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_UniqueIdentifier() {
			DeleteTypeTest(x => x.WhereUniqueIdentifierColumn = x.WhereUniqueIdentifierColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { UniqueIdentifierColumn = Guid.NewGuid() });
			UpdateTypeTest(new TypeTestUpdate { UniqueIdentifierColumn = Guid.NewGuid() }, x => x.WhereUniqueIdentifierColumn = x.WhereUniqueIdentifierColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_VarBinary() {
			DeleteTypeTest(x => x.WhereVarBinaryColumn = x.WhereVarBinaryColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { VarBinaryColumn = new byte[] { 0x16 } });
			UpdateTypeTest(new TypeTestUpdate { VarBinaryColumn = new byte[] { 0x16 } }, x => x.WhereVarBinaryColumn = x.WhereVarBinaryColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_VarChar() {
			DeleteTypeTest(x => x.WhereVarCharColumn = x.WhereVarCharColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { VarCharColumn = "abc" });
			UpdateTypeTest(new TypeTestUpdate { VarCharColumn = "abc" }, x => x.WhereVarCharColumn = x.WhereVarCharColumn != DBNull.Value);
		}

		[TestMethod]
		public void TypeTest_Xml() {
			DeleteTypeTest(x => x.WhereXmlColumn = x.WhereXmlColumn != DBNull.Value);
			InsertTypeTest(new TypeTestInsert { XmlColumn = @"<root></root>" });
			UpdateTypeTest(new TypeTestUpdate { XmlColumn = @"<root></root>" }, x => x.WhereXmlColumn = x.WhereXmlColumn != DBNull.Value);
		}
	}
}
