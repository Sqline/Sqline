using System;
using System.Data;
using System.Text;
using System.Collections;
using Sqline.ClientFramework;
namespace Sqline.Tests.DataAccess.DataItems {
	public static class TypeTestMeta {
		public static int CharColumnMaxLength { get { return 10; } }	
		public static int NCharColumnMaxLength { get { return 20; } }	
		public static int NTextColumnMaxLength { get { return 16; } }	
		public static int NVarCharColumnMaxLength { get { return 100; } }	
		public static int TextColumnMaxLength { get { return 16; } }	
		public static int VarCharColumnMaxLength { get { return 50; } }	
		public static int XmlColumnMaxLength { get { return -1; } }	
	}
	public class TypeTestInsert : InsertDataItem {
		private NullableValueParam<long?> FBigintColumn;
		private NullableValueParam<byte[]> FBinaryColumn;
		private NullableValueParam<bool?> FBitColumn;
		private NullableValueParam<string> FCharColumn;
		private NullableValueParam<DateTime?> FDateColumn;
		private NullableValueParam<DateTime?> FDateTimeColumn;
		private NullableValueParam<DateTime?> FDateTime2Column;
		private NullableValueParam<DateTimeOffset?> FDateTimeOffsetColumn;
		private NullableValueParam<decimal?> FDecimalColumn;
		private NullableValueParam<byte[]> FFileStreamColumn;
		private NullableValueParam<double?> FFloatColumn;
		private NullableValueParam<byte[]> FImageColumn;
		private NullableValueParam<int?> FIntColumn;
		private NullableValueParam<decimal?> FMoneyColumn;
		private NullableValueParam<string> FNCharColumn;
		private NullableValueParam<string> FNTextColumn;
		private NullableValueParam<decimal?> FNumericColumn;
		private NullableValueParam<string> FNVarCharColumn;
		private NullableValueParam<float?> FRealColumn;
		private NullableValueParam<DateTime?> FSmallDateTimeColumn;
		private NullableValueParam<short?> FSmallIntColumn;
		private NullableValueParam<decimal?> FSmallMoneyColumn;
		private NullableValueParam<string> FTextColumn;
		private NullableValueParam<TimeSpan?> FTimeColumn;
		private NullableValueParam<byte[]> FTimestampColumn;
		private NullableValueParam<byte?> FTinyIntColumn;
		private NullableValueParam<Guid?> FUniqueIdentifierColumn;
		private NullableValueParam<byte[]> FVarBinaryColumn;
		private NullableValueParam<string> FVarCharColumn;
		private NullableValueParam<string> FXmlColumn;
		public TypeTestInsert() {
			Initialize("dbo", "TypeTest", Sqline.Tests.DataAccess.DAHandler.SqlineConfig);
			SetFetchPrimaryKeyValueAfterInsert(false);
			
		}

		protected override void PreExecute() {
			AddParameter(FBigintColumn, "BigintColumn");
AddParameter(FBinaryColumn, "BinaryColumn");
AddParameter(FBitColumn, "BitColumn");
AddParameter(FCharColumn, "CharColumn");
AddParameter(FDateColumn, "DateColumn");
AddParameter(FDateTimeColumn, "DateTimeColumn");
AddParameter(FDateTime2Column, "DateTime2Column");
AddParameter(FDateTimeOffsetColumn, "DateTimeOffsetColumn");
AddParameter(FDecimalColumn, "DecimalColumn");
AddParameter(FFileStreamColumn, "FileStreamColumn");
AddParameter(FFloatColumn, "FloatColumn");
AddParameter(FImageColumn, "ImageColumn");
AddParameter(FIntColumn, "IntColumn");
AddParameter(FMoneyColumn, "MoneyColumn");
AddParameter(FNCharColumn, "NCharColumn");
AddParameter(FNTextColumn, "NTextColumn");
AddParameter(FNumericColumn, "NumericColumn");
AddParameter(FNVarCharColumn, "NVarCharColumn");
AddParameter(FRealColumn, "RealColumn");
AddParameter(FSmallDateTimeColumn, "SmallDateTimeColumn");
AddParameter(FSmallIntColumn, "SmallIntColumn");
AddParameter(FSmallMoneyColumn, "SmallMoneyColumn");
AddParameter(FTextColumn, "TextColumn");
AddParameter(FTimeColumn, "TimeColumn");
AddParameter(FTimestampColumn, "TimestampColumn");
AddParameter(FTinyIntColumn, "TinyIntColumn");
AddParameter(FUniqueIdentifierColumn, "UniqueIdentifierColumn");
AddParameter(FVarBinaryColumn, "VarBinaryColumn");
AddParameter(FVarCharColumn, "VarCharColumn");
AddParameter(FXmlColumn, "XmlColumn");
			base.PreExecute();
		}

			public NullableValueParam<long?> BigintColumn { get { return FBigintColumn; } set { FBigintColumn = value; } }
			public NullableValueParam<byte[]> BinaryColumn { get { return FBinaryColumn; } set { FBinaryColumn = value; } }
			public NullableValueParam<bool?> BitColumn { get { return FBitColumn; } set { FBitColumn = value; } }
			public NullableValueParam<string> CharColumn { get { return FCharColumn; } set { FCharColumn = value; } }
			public NullableValueParam<DateTime?> DateColumn { get { return FDateColumn; } set { FDateColumn = value; } }
			public NullableValueParam<DateTime?> DateTimeColumn { get { return FDateTimeColumn; } set { FDateTimeColumn = value; } }
			public NullableValueParam<DateTime?> DateTime2Column { get { return FDateTime2Column; } set { FDateTime2Column = value; } }
			public NullableValueParam<DateTimeOffset?> DateTimeOffsetColumn { get { return FDateTimeOffsetColumn; } set { FDateTimeOffsetColumn = value; } }
			public NullableValueParam<decimal?> DecimalColumn { get { return FDecimalColumn; } set { FDecimalColumn = value; } }
			public NullableValueParam<byte[]> FileStreamColumn { get { return FFileStreamColumn; } set { FFileStreamColumn = value; } }
			public NullableValueParam<double?> FloatColumn { get { return FFloatColumn; } set { FFloatColumn = value; } }
			public NullableValueParam<byte[]> ImageColumn { get { return FImageColumn; } set { FImageColumn = value; } }
			public NullableValueParam<int?> IntColumn { get { return FIntColumn; } set { FIntColumn = value; } }
			public NullableValueParam<decimal?> MoneyColumn { get { return FMoneyColumn; } set { FMoneyColumn = value; } }
			public NullableValueParam<string> NCharColumn { get { return FNCharColumn; } set { FNCharColumn = value; } }
			public NullableValueParam<string> NTextColumn { get { return FNTextColumn; } set { FNTextColumn = value; } }
			public NullableValueParam<decimal?> NumericColumn { get { return FNumericColumn; } set { FNumericColumn = value; } }
			public NullableValueParam<string> NVarCharColumn { get { return FNVarCharColumn; } set { FNVarCharColumn = value; } }
			public NullableValueParam<float?> RealColumn { get { return FRealColumn; } set { FRealColumn = value; } }
			public NullableValueParam<DateTime?> SmallDateTimeColumn { get { return FSmallDateTimeColumn; } set { FSmallDateTimeColumn = value; } }
			public NullableValueParam<short?> SmallIntColumn { get { return FSmallIntColumn; } set { FSmallIntColumn = value; } }
			public NullableValueParam<decimal?> SmallMoneyColumn { get { return FSmallMoneyColumn; } set { FSmallMoneyColumn = value; } }
			public NullableValueParam<string> TextColumn { get { return FTextColumn; } set { FTextColumn = value; } }
			public NullableValueParam<TimeSpan?> TimeColumn { get { return FTimeColumn; } set { FTimeColumn = value; } }
			public NullableValueParam<byte[]> TimestampColumn { get { return FTimestampColumn; } set { FTimestampColumn = value; } }
			public NullableValueParam<byte?> TinyIntColumn { get { return FTinyIntColumn; } set { FTinyIntColumn = value; } }
			public NullableValueParam<Guid?> UniqueIdentifierColumn { get { return FUniqueIdentifierColumn; } set { FUniqueIdentifierColumn = value; } }
			public NullableValueParam<byte[]> VarBinaryColumn { get { return FVarBinaryColumn; } set { FVarBinaryColumn = value; } }
			public NullableValueParam<string> VarCharColumn { get { return FVarCharColumn; } set { FVarCharColumn = value; } }
			public NullableValueParam<string> XmlColumn { get { return FXmlColumn; } set { FXmlColumn = value; } }
	}
	public class TypeTestUpdate : UpdateDataItem {
		private NullableValueParam<long?> FBigintColumn;
		private NullableWhereParam<long?> FWhereBigintColumn;
		private NullableValueParam<byte[]> FBinaryColumn;
		private NullableWhereParam<byte[]> FWhereBinaryColumn;
		private NullableValueParam<bool?> FBitColumn;
		private NullableWhereParam<bool?> FWhereBitColumn;
		private NullableValueParam<string> FCharColumn;
		private NullableWhereParam<string> FWhereCharColumn;
		private NullableValueParam<DateTime?> FDateColumn;
		private NullableWhereParam<DateTime?> FWhereDateColumn;
		private NullableValueParam<DateTime?> FDateTimeColumn;
		private NullableWhereParam<DateTime?> FWhereDateTimeColumn;
		private NullableValueParam<DateTime?> FDateTime2Column;
		private NullableWhereParam<DateTime?> FWhereDateTime2Column;
		private NullableValueParam<DateTimeOffset?> FDateTimeOffsetColumn;
		private NullableWhereParam<DateTimeOffset?> FWhereDateTimeOffsetColumn;
		private NullableValueParam<decimal?> FDecimalColumn;
		private NullableWhereParam<decimal?> FWhereDecimalColumn;
		private NullableValueParam<byte[]> FFileStreamColumn;
		private NullableWhereParam<byte[]> FWhereFileStreamColumn;
		private NullableValueParam<double?> FFloatColumn;
		private NullableWhereParam<double?> FWhereFloatColumn;
		private NullableValueParam<byte[]> FImageColumn;
		private NullableWhereParam<byte[]> FWhereImageColumn;
		private NullableValueParam<int?> FIntColumn;
		private NullableWhereParam<int?> FWhereIntColumn;
		private NullableValueParam<decimal?> FMoneyColumn;
		private NullableWhereParam<decimal?> FWhereMoneyColumn;
		private NullableValueParam<string> FNCharColumn;
		private NullableWhereParam<string> FWhereNCharColumn;
		private NullableValueParam<string> FNTextColumn;
		private NullableWhereParam<string> FWhereNTextColumn;
		private NullableValueParam<decimal?> FNumericColumn;
		private NullableWhereParam<decimal?> FWhereNumericColumn;
		private NullableValueParam<string> FNVarCharColumn;
		private NullableWhereParam<string> FWhereNVarCharColumn;
		private NullableValueParam<float?> FRealColumn;
		private NullableWhereParam<float?> FWhereRealColumn;
		private NullableValueParam<DateTime?> FSmallDateTimeColumn;
		private NullableWhereParam<DateTime?> FWhereSmallDateTimeColumn;
		private NullableValueParam<short?> FSmallIntColumn;
		private NullableWhereParam<short?> FWhereSmallIntColumn;
		private NullableValueParam<decimal?> FSmallMoneyColumn;
		private NullableWhereParam<decimal?> FWhereSmallMoneyColumn;
		private NullableValueParam<string> FTextColumn;
		private NullableWhereParam<string> FWhereTextColumn;
		private NullableValueParam<TimeSpan?> FTimeColumn;
		private NullableWhereParam<TimeSpan?> FWhereTimeColumn;
		private NullableValueParam<byte[]> FTimestampColumn;
		private NullableWhereParam<byte[]> FWhereTimestampColumn;
		private NullableValueParam<byte?> FTinyIntColumn;
		private NullableWhereParam<byte?> FWhereTinyIntColumn;
		private NullableValueParam<Guid?> FUniqueIdentifierColumn;
		private NullableWhereParam<Guid?> FWhereUniqueIdentifierColumn;
		private NullableValueParam<byte[]> FVarBinaryColumn;
		private NullableWhereParam<byte[]> FWhereVarBinaryColumn;
		private NullableValueParam<string> FVarCharColumn;
		private NullableWhereParam<string> FWhereVarCharColumn;
		private NullableValueParam<string> FXmlColumn;
		private NullableWhereParam<string> FWhereXmlColumn;
		public TypeTestUpdate() {
			Initialize("dbo", "TypeTest", Sqline.Tests.DataAccess.DAHandler.SqlineConfig);
		}

		protected override void PreExecute() {
			AddParameter(FBigintColumn, "BigintColumn");
AddParameter(FBinaryColumn, "BinaryColumn");
AddParameter(FBitColumn, "BitColumn");
AddParameter(FCharColumn, "CharColumn");
AddParameter(FDateColumn, "DateColumn");
AddParameter(FDateTimeColumn, "DateTimeColumn");
AddParameter(FDateTime2Column, "DateTime2Column");
AddParameter(FDateTimeOffsetColumn, "DateTimeOffsetColumn");
AddParameter(FDecimalColumn, "DecimalColumn");
AddParameter(FFileStreamColumn, "FileStreamColumn");
AddParameter(FFloatColumn, "FloatColumn");
AddParameter(FImageColumn, "ImageColumn");
AddParameter(FIntColumn, "IntColumn");
AddParameter(FMoneyColumn, "MoneyColumn");
AddParameter(FNCharColumn, "NCharColumn");
AddParameter(FNTextColumn, "NTextColumn");
AddParameter(FNumericColumn, "NumericColumn");
AddParameter(FNVarCharColumn, "NVarCharColumn");
AddParameter(FRealColumn, "RealColumn");
AddParameter(FSmallDateTimeColumn, "SmallDateTimeColumn");
AddParameter(FSmallIntColumn, "SmallIntColumn");
AddParameter(FSmallMoneyColumn, "SmallMoneyColumn");
AddParameter(FTextColumn, "TextColumn");
AddParameter(FTimeColumn, "TimeColumn");
AddParameter(FTimestampColumn, "TimestampColumn");
AddParameter(FTinyIntColumn, "TinyIntColumn");
AddParameter(FUniqueIdentifierColumn, "UniqueIdentifierColumn");
AddParameter(FVarBinaryColumn, "VarBinaryColumn");
AddParameter(FVarCharColumn, "VarCharColumn");
AddParameter(FXmlColumn, "XmlColumn");
			AddParameter(FWhereBigintColumn, "BigintColumn");
AddParameter(FWhereBinaryColumn, "BinaryColumn");
AddParameter(FWhereBitColumn, "BitColumn");
AddParameter(FWhereCharColumn, "CharColumn");
AddParameter(FWhereDateColumn, "DateColumn");
AddParameter(FWhereDateTimeColumn, "DateTimeColumn");
AddParameter(FWhereDateTime2Column, "DateTime2Column");
AddParameter(FWhereDateTimeOffsetColumn, "DateTimeOffsetColumn");
AddParameter(FWhereDecimalColumn, "DecimalColumn");
AddParameter(FWhereFileStreamColumn, "FileStreamColumn");
AddParameter(FWhereFloatColumn, "FloatColumn");
AddParameter(FWhereImageColumn, "ImageColumn");
AddParameter(FWhereIntColumn, "IntColumn");
AddParameter(FWhereMoneyColumn, "MoneyColumn");
AddParameter(FWhereNCharColumn, "NCharColumn");
AddParameter(FWhereNTextColumn, "NTextColumn");
AddParameter(FWhereNumericColumn, "NumericColumn");
AddParameter(FWhereNVarCharColumn, "NVarCharColumn");
AddParameter(FWhereRealColumn, "RealColumn");
AddParameter(FWhereSmallDateTimeColumn, "SmallDateTimeColumn");
AddParameter(FWhereSmallIntColumn, "SmallIntColumn");
AddParameter(FWhereSmallMoneyColumn, "SmallMoneyColumn");
AddParameter(FWhereTextColumn, "TextColumn");
AddParameter(FWhereTimeColumn, "TimeColumn");
AddParameter(FWhereTimestampColumn, "TimestampColumn");
AddParameter(FWhereTinyIntColumn, "TinyIntColumn");
AddParameter(FWhereUniqueIdentifierColumn, "UniqueIdentifierColumn");
AddParameter(FWhereVarBinaryColumn, "VarBinaryColumn");
AddParameter(FWhereVarCharColumn, "VarCharColumn");
AddParameter(FWhereXmlColumn, "XmlColumn");
			base.PreExecute();
		}

		public NullableValueParam<long?> BigintColumn { get { return FBigintColumn; } set { FBigintColumn = value; } }
		public NullableWhereParam<long?> WhereBigintColumn { get { return FWhereBigintColumn; } set { FWhereBigintColumn = value; } }
		public NullableValueParam<byte[]> BinaryColumn { get { return FBinaryColumn; } set { FBinaryColumn = value; } }
		public NullableWhereParam<byte[]> WhereBinaryColumn { get { return FWhereBinaryColumn; } set { FWhereBinaryColumn = value; } }
		public NullableValueParam<bool?> BitColumn { get { return FBitColumn; } set { FBitColumn = value; } }
		public NullableWhereParam<bool?> WhereBitColumn { get { return FWhereBitColumn; } set { FWhereBitColumn = value; } }
		public NullableValueParam<string> CharColumn { get { return FCharColumn; } set { FCharColumn = value; } }
		public NullableWhereParam<string> WhereCharColumn { get { return FWhereCharColumn; } set { FWhereCharColumn = value; } }
		public NullableValueParam<DateTime?> DateColumn { get { return FDateColumn; } set { FDateColumn = value; } }
		public NullableWhereParam<DateTime?> WhereDateColumn { get { return FWhereDateColumn; } set { FWhereDateColumn = value; } }
		public NullableValueParam<DateTime?> DateTimeColumn { get { return FDateTimeColumn; } set { FDateTimeColumn = value; } }
		public NullableWhereParam<DateTime?> WhereDateTimeColumn { get { return FWhereDateTimeColumn; } set { FWhereDateTimeColumn = value; } }
		public NullableValueParam<DateTime?> DateTime2Column { get { return FDateTime2Column; } set { FDateTime2Column = value; } }
		public NullableWhereParam<DateTime?> WhereDateTime2Column { get { return FWhereDateTime2Column; } set { FWhereDateTime2Column = value; } }
		public NullableValueParam<DateTimeOffset?> DateTimeOffsetColumn { get { return FDateTimeOffsetColumn; } set { FDateTimeOffsetColumn = value; } }
		public NullableWhereParam<DateTimeOffset?> WhereDateTimeOffsetColumn { get { return FWhereDateTimeOffsetColumn; } set { FWhereDateTimeOffsetColumn = value; } }
		public NullableValueParam<decimal?> DecimalColumn { get { return FDecimalColumn; } set { FDecimalColumn = value; } }
		public NullableWhereParam<decimal?> WhereDecimalColumn { get { return FWhereDecimalColumn; } set { FWhereDecimalColumn = value; } }
		public NullableValueParam<byte[]> FileStreamColumn { get { return FFileStreamColumn; } set { FFileStreamColumn = value; } }
		public NullableWhereParam<byte[]> WhereFileStreamColumn { get { return FWhereFileStreamColumn; } set { FWhereFileStreamColumn = value; } }
		public NullableValueParam<double?> FloatColumn { get { return FFloatColumn; } set { FFloatColumn = value; } }
		public NullableWhereParam<double?> WhereFloatColumn { get { return FWhereFloatColumn; } set { FWhereFloatColumn = value; } }
		public NullableValueParam<byte[]> ImageColumn { get { return FImageColumn; } set { FImageColumn = value; } }
		public NullableWhereParam<byte[]> WhereImageColumn { get { return FWhereImageColumn; } set { FWhereImageColumn = value; } }
		public NullableValueParam<int?> IntColumn { get { return FIntColumn; } set { FIntColumn = value; } }
		public NullableWhereParam<int?> WhereIntColumn { get { return FWhereIntColumn; } set { FWhereIntColumn = value; } }
		public NullableValueParam<decimal?> MoneyColumn { get { return FMoneyColumn; } set { FMoneyColumn = value; } }
		public NullableWhereParam<decimal?> WhereMoneyColumn { get { return FWhereMoneyColumn; } set { FWhereMoneyColumn = value; } }
		public NullableValueParam<string> NCharColumn { get { return FNCharColumn; } set { FNCharColumn = value; } }
		public NullableWhereParam<string> WhereNCharColumn { get { return FWhereNCharColumn; } set { FWhereNCharColumn = value; } }
		public NullableValueParam<string> NTextColumn { get { return FNTextColumn; } set { FNTextColumn = value; } }
		public NullableWhereParam<string> WhereNTextColumn { get { return FWhereNTextColumn; } set { FWhereNTextColumn = value; } }
		public NullableValueParam<decimal?> NumericColumn { get { return FNumericColumn; } set { FNumericColumn = value; } }
		public NullableWhereParam<decimal?> WhereNumericColumn { get { return FWhereNumericColumn; } set { FWhereNumericColumn = value; } }
		public NullableValueParam<string> NVarCharColumn { get { return FNVarCharColumn; } set { FNVarCharColumn = value; } }
		public NullableWhereParam<string> WhereNVarCharColumn { get { return FWhereNVarCharColumn; } set { FWhereNVarCharColumn = value; } }
		public NullableValueParam<float?> RealColumn { get { return FRealColumn; } set { FRealColumn = value; } }
		public NullableWhereParam<float?> WhereRealColumn { get { return FWhereRealColumn; } set { FWhereRealColumn = value; } }
		public NullableValueParam<DateTime?> SmallDateTimeColumn { get { return FSmallDateTimeColumn; } set { FSmallDateTimeColumn = value; } }
		public NullableWhereParam<DateTime?> WhereSmallDateTimeColumn { get { return FWhereSmallDateTimeColumn; } set { FWhereSmallDateTimeColumn = value; } }
		public NullableValueParam<short?> SmallIntColumn { get { return FSmallIntColumn; } set { FSmallIntColumn = value; } }
		public NullableWhereParam<short?> WhereSmallIntColumn { get { return FWhereSmallIntColumn; } set { FWhereSmallIntColumn = value; } }
		public NullableValueParam<decimal?> SmallMoneyColumn { get { return FSmallMoneyColumn; } set { FSmallMoneyColumn = value; } }
		public NullableWhereParam<decimal?> WhereSmallMoneyColumn { get { return FWhereSmallMoneyColumn; } set { FWhereSmallMoneyColumn = value; } }
		public NullableValueParam<string> TextColumn { get { return FTextColumn; } set { FTextColumn = value; } }
		public NullableWhereParam<string> WhereTextColumn { get { return FWhereTextColumn; } set { FWhereTextColumn = value; } }
		public NullableValueParam<TimeSpan?> TimeColumn { get { return FTimeColumn; } set { FTimeColumn = value; } }
		public NullableWhereParam<TimeSpan?> WhereTimeColumn { get { return FWhereTimeColumn; } set { FWhereTimeColumn = value; } }
		public NullableValueParam<byte[]> TimestampColumn { get { return FTimestampColumn; } set { FTimestampColumn = value; } }
		public NullableWhereParam<byte[]> WhereTimestampColumn { get { return FWhereTimestampColumn; } set { FWhereTimestampColumn = value; } }
		public NullableValueParam<byte?> TinyIntColumn { get { return FTinyIntColumn; } set { FTinyIntColumn = value; } }
		public NullableWhereParam<byte?> WhereTinyIntColumn { get { return FWhereTinyIntColumn; } set { FWhereTinyIntColumn = value; } }
		public NullableValueParam<Guid?> UniqueIdentifierColumn { get { return FUniqueIdentifierColumn; } set { FUniqueIdentifierColumn = value; } }
		public NullableWhereParam<Guid?> WhereUniqueIdentifierColumn { get { return FWhereUniqueIdentifierColumn; } set { FWhereUniqueIdentifierColumn = value; } }
		public NullableValueParam<byte[]> VarBinaryColumn { get { return FVarBinaryColumn; } set { FVarBinaryColumn = value; } }
		public NullableWhereParam<byte[]> WhereVarBinaryColumn { get { return FWhereVarBinaryColumn; } set { FWhereVarBinaryColumn = value; } }
		public NullableValueParam<string> VarCharColumn { get { return FVarCharColumn; } set { FVarCharColumn = value; } }
		public NullableWhereParam<string> WhereVarCharColumn { get { return FWhereVarCharColumn; } set { FWhereVarCharColumn = value; } }
		public NullableValueParam<string> XmlColumn { get { return FXmlColumn; } set { FXmlColumn = value; } }
		public NullableWhereParam<string> WhereXmlColumn { get { return FWhereXmlColumn; } set { FWhereXmlColumn = value; } }
	}
	public class TypeTestDelete : DeleteDataItem {
		private NullableWhereParam<long?> FWhereBigintColumn;
		private NullableWhereParam<byte[]> FWhereBinaryColumn;
		private NullableWhereParam<bool?> FWhereBitColumn;
		private NullableWhereParam<string> FWhereCharColumn;
		private NullableWhereParam<DateTime?> FWhereDateColumn;
		private NullableWhereParam<DateTime?> FWhereDateTimeColumn;
		private NullableWhereParam<DateTime?> FWhereDateTime2Column;
		private NullableWhereParam<DateTimeOffset?> FWhereDateTimeOffsetColumn;
		private NullableWhereParam<decimal?> FWhereDecimalColumn;
		private NullableWhereParam<byte[]> FWhereFileStreamColumn;
		private NullableWhereParam<double?> FWhereFloatColumn;
		private NullableWhereParam<byte[]> FWhereImageColumn;
		private NullableWhereParam<int?> FWhereIntColumn;
		private NullableWhereParam<decimal?> FWhereMoneyColumn;
		private NullableWhereParam<string> FWhereNCharColumn;
		private NullableWhereParam<string> FWhereNTextColumn;
		private NullableWhereParam<decimal?> FWhereNumericColumn;
		private NullableWhereParam<string> FWhereNVarCharColumn;
		private NullableWhereParam<float?> FWhereRealColumn;
		private NullableWhereParam<DateTime?> FWhereSmallDateTimeColumn;
		private NullableWhereParam<short?> FWhereSmallIntColumn;
		private NullableWhereParam<decimal?> FWhereSmallMoneyColumn;
		private NullableWhereParam<string> FWhereTextColumn;
		private NullableWhereParam<TimeSpan?> FWhereTimeColumn;
		private NullableWhereParam<byte[]> FWhereTimestampColumn;
		private NullableWhereParam<byte?> FWhereTinyIntColumn;
		private NullableWhereParam<Guid?> FWhereUniqueIdentifierColumn;
		private NullableWhereParam<byte[]> FWhereVarBinaryColumn;
		private NullableWhereParam<string> FWhereVarCharColumn;
		private NullableWhereParam<string> FWhereXmlColumn;
		public TypeTestDelete() {
			Initialize("dbo", "TypeTest", Sqline.Tests.DataAccess.DAHandler.SqlineConfig);
		}

		protected override void PreExecute() {
			AddParameter(FWhereBigintColumn, "BigintColumn");
AddParameter(FWhereBinaryColumn, "BinaryColumn");
AddParameter(FWhereBitColumn, "BitColumn");
AddParameter(FWhereCharColumn, "CharColumn");
AddParameter(FWhereDateColumn, "DateColumn");
AddParameter(FWhereDateTimeColumn, "DateTimeColumn");
AddParameter(FWhereDateTime2Column, "DateTime2Column");
AddParameter(FWhereDateTimeOffsetColumn, "DateTimeOffsetColumn");
AddParameter(FWhereDecimalColumn, "DecimalColumn");
AddParameter(FWhereFileStreamColumn, "FileStreamColumn");
AddParameter(FWhereFloatColumn, "FloatColumn");
AddParameter(FWhereImageColumn, "ImageColumn");
AddParameter(FWhereIntColumn, "IntColumn");
AddParameter(FWhereMoneyColumn, "MoneyColumn");
AddParameter(FWhereNCharColumn, "NCharColumn");
AddParameter(FWhereNTextColumn, "NTextColumn");
AddParameter(FWhereNumericColumn, "NumericColumn");
AddParameter(FWhereNVarCharColumn, "NVarCharColumn");
AddParameter(FWhereRealColumn, "RealColumn");
AddParameter(FWhereSmallDateTimeColumn, "SmallDateTimeColumn");
AddParameter(FWhereSmallIntColumn, "SmallIntColumn");
AddParameter(FWhereSmallMoneyColumn, "SmallMoneyColumn");
AddParameter(FWhereTextColumn, "TextColumn");
AddParameter(FWhereTimeColumn, "TimeColumn");
AddParameter(FWhereTimestampColumn, "TimestampColumn");
AddParameter(FWhereTinyIntColumn, "TinyIntColumn");
AddParameter(FWhereUniqueIdentifierColumn, "UniqueIdentifierColumn");
AddParameter(FWhereVarBinaryColumn, "VarBinaryColumn");
AddParameter(FWhereVarCharColumn, "VarCharColumn");
AddParameter(FWhereXmlColumn, "XmlColumn");
			base.PreExecute();
		}

		public NullableWhereParam<long?> WhereBigintColumn { get { return FWhereBigintColumn; } set { FWhereBigintColumn = value; } }
		public NullableWhereParam<byte[]> WhereBinaryColumn { get { return FWhereBinaryColumn; } set { FWhereBinaryColumn = value; } }
		public NullableWhereParam<bool?> WhereBitColumn { get { return FWhereBitColumn; } set { FWhereBitColumn = value; } }
		public NullableWhereParam<string> WhereCharColumn { get { return FWhereCharColumn; } set { FWhereCharColumn = value; } }
		public NullableWhereParam<DateTime?> WhereDateColumn { get { return FWhereDateColumn; } set { FWhereDateColumn = value; } }
		public NullableWhereParam<DateTime?> WhereDateTimeColumn { get { return FWhereDateTimeColumn; } set { FWhereDateTimeColumn = value; } }
		public NullableWhereParam<DateTime?> WhereDateTime2Column { get { return FWhereDateTime2Column; } set { FWhereDateTime2Column = value; } }
		public NullableWhereParam<DateTimeOffset?> WhereDateTimeOffsetColumn { get { return FWhereDateTimeOffsetColumn; } set { FWhereDateTimeOffsetColumn = value; } }
		public NullableWhereParam<decimal?> WhereDecimalColumn { get { return FWhereDecimalColumn; } set { FWhereDecimalColumn = value; } }
		public NullableWhereParam<byte[]> WhereFileStreamColumn { get { return FWhereFileStreamColumn; } set { FWhereFileStreamColumn = value; } }
		public NullableWhereParam<double?> WhereFloatColumn { get { return FWhereFloatColumn; } set { FWhereFloatColumn = value; } }
		public NullableWhereParam<byte[]> WhereImageColumn { get { return FWhereImageColumn; } set { FWhereImageColumn = value; } }
		public NullableWhereParam<int?> WhereIntColumn { get { return FWhereIntColumn; } set { FWhereIntColumn = value; } }
		public NullableWhereParam<decimal?> WhereMoneyColumn { get { return FWhereMoneyColumn; } set { FWhereMoneyColumn = value; } }
		public NullableWhereParam<string> WhereNCharColumn { get { return FWhereNCharColumn; } set { FWhereNCharColumn = value; } }
		public NullableWhereParam<string> WhereNTextColumn { get { return FWhereNTextColumn; } set { FWhereNTextColumn = value; } }
		public NullableWhereParam<decimal?> WhereNumericColumn { get { return FWhereNumericColumn; } set { FWhereNumericColumn = value; } }
		public NullableWhereParam<string> WhereNVarCharColumn { get { return FWhereNVarCharColumn; } set { FWhereNVarCharColumn = value; } }
		public NullableWhereParam<float?> WhereRealColumn { get { return FWhereRealColumn; } set { FWhereRealColumn = value; } }
		public NullableWhereParam<DateTime?> WhereSmallDateTimeColumn { get { return FWhereSmallDateTimeColumn; } set { FWhereSmallDateTimeColumn = value; } }
		public NullableWhereParam<short?> WhereSmallIntColumn { get { return FWhereSmallIntColumn; } set { FWhereSmallIntColumn = value; } }
		public NullableWhereParam<decimal?> WhereSmallMoneyColumn { get { return FWhereSmallMoneyColumn; } set { FWhereSmallMoneyColumn = value; } }
		public NullableWhereParam<string> WhereTextColumn { get { return FWhereTextColumn; } set { FWhereTextColumn = value; } }
		public NullableWhereParam<TimeSpan?> WhereTimeColumn { get { return FWhereTimeColumn; } set { FWhereTimeColumn = value; } }
		public NullableWhereParam<byte[]> WhereTimestampColumn { get { return FWhereTimestampColumn; } set { FWhereTimestampColumn = value; } }
		public NullableWhereParam<byte?> WhereTinyIntColumn { get { return FWhereTinyIntColumn; } set { FWhereTinyIntColumn = value; } }
		public NullableWhereParam<Guid?> WhereUniqueIdentifierColumn { get { return FWhereUniqueIdentifierColumn; } set { FWhereUniqueIdentifierColumn = value; } }
		public NullableWhereParam<byte[]> WhereVarBinaryColumn { get { return FWhereVarBinaryColumn; } set { FWhereVarBinaryColumn = value; } }
		public NullableWhereParam<string> WhereVarCharColumn { get { return FWhereVarCharColumn; } set { FWhereVarCharColumn = value; } }
		public NullableWhereParam<string> WhereXmlColumn { get { return FWhereXmlColumn; } set { FWhereXmlColumn = value; } }
	}
	public static class TestMeta {
	}
	public class TestInsert : InsertDataItem {
		private ValueParam<int> FID;
		private ValueParam<bool> FProcessed;
		public TestInsert(ValueParam<bool> processed) {
			Initialize("dbo", "Test", Sqline.Tests.DataAccess.DAHandler.SqlineConfig);
			SetPrimaryKeyInfo("ID", "int");
			FProcessed = processed;
		}

		protected override void PreExecute() {
			AddParameter(FID, "ID");
AddParameter(FProcessed, "Processed");
			base.PreExecute();
		}

		public int GetInsertedPrimaryKeyValue() {
			return (int)FInsertedPKValue;
		}
			public ValueParam<int> ID { get { return FID; } set { FID = value; } }
			public ValueParam<bool> Processed { get { return FProcessed; } private set { FProcessed = value; } }
	}
	public class TestUpdate : UpdateDataItem {
		private ValueParam<int> FID;
		private WhereParam<int> FWhereID;
		private ValueParam<bool> FProcessed;
		private WhereParam<bool> FWhereProcessed;
		public TestUpdate() {
			Initialize("dbo", "Test", Sqline.Tests.DataAccess.DAHandler.SqlineConfig);
		}

		protected override void PreExecute() {
			AddParameter(FID, "ID");
AddParameter(FProcessed, "Processed");
			AddParameter(FWhereID, "ID");
AddParameter(FWhereProcessed, "Processed");
			base.PreExecute();
		}

		public ValueParam<int> ID { get { return FID; } set { FID = value; } }
		public WhereParam<int> WhereID { get { return FWhereID; } set { FWhereID = value; } }
		public ValueParam<bool> Processed { get { return FProcessed; } set { FProcessed = value; } }
		public WhereParam<bool> WhereProcessed { get { return FWhereProcessed; } set { FWhereProcessed = value; } }
	}
	public class TestDelete : DeleteDataItem {
		private WhereParam<int> FWhereID;
		private WhereParam<bool> FWhereProcessed;
		public TestDelete() {
			Initialize("dbo", "Test", Sqline.Tests.DataAccess.DAHandler.SqlineConfig);
		}

		protected override void PreExecute() {
			AddParameter(FWhereID, "ID");
AddParameter(FWhereProcessed, "Processed");
			base.PreExecute();
		}

		public WhereParam<int> WhereID { get { return FWhereID; } set { FWhereID = value; } }
		public WhereParam<bool> WhereProcessed { get { return FWhereProcessed; } set { FWhereProcessed = value; } }
	}
}