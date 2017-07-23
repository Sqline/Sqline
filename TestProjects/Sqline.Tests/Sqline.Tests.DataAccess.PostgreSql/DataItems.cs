using System;
using System.Data;
using System.Text;
using System.Collections;
using Sqline.ClientFramework;
namespace Sqline.Tests.DataAccess.PostgreSql.DataItems {
	public static class TypeTestMeta {
	}
	public class TypeTestInsert : InsertDataItem {
		private NullableValueParam<int?> FIntegerColumn;
		public TypeTestInsert() {
			Initialize("public", "TypeTest", Sqline.Tests.DataAccess.PostgreSql.DAHandler.SqlineApplication);
			SetFetchPrimaryKeyValueAfterInsert(false);
			
		}

		protected override void PreExecute() {
			AddParameter(FIntegerColumn, "IntegerColumn");
			base.PreExecute();
		}

			public NullableValueParam<int?> IntegerColumn { get { return FIntegerColumn; } set { FIntegerColumn = value; } }
	}
	public class TypeTestUpdate : UpdateDataItem {
		private NullableValueParam<int?> FIntegerColumn;
		private NullableWhereParam<int?> FWhereIntegerColumn;
		public TypeTestUpdate() {
			Initialize("public", "TypeTest", Sqline.Tests.DataAccess.PostgreSql.DAHandler.SqlineApplication);
		}

		protected override void PreExecute() {
			AddParameter(FIntegerColumn, "IntegerColumn");
			AddParameter(FWhereIntegerColumn, "IntegerColumn");
			base.PreExecute();
		}

		public NullableValueParam<int?> IntegerColumn { get { return FIntegerColumn; } set { FIntegerColumn = value; } }
		public NullableWhereParam<int?> WhereIntegerColumn { get { return FWhereIntegerColumn; } set { FWhereIntegerColumn = value; } }
	}
	public class TypeTestDelete : DeleteDataItem {
		private NullableWhereParam<int?> FWhereIntegerColumn;
		public TypeTestDelete() {
			Initialize("public", "TypeTest", Sqline.Tests.DataAccess.PostgreSql.DAHandler.SqlineApplication);
		}

		protected override void PreExecute() {
			AddParameter(FWhereIntegerColumn, "IntegerColumn");
			base.PreExecute();
		}

		public NullableWhereParam<int?> WhereIntegerColumn { get { return FWhereIntegerColumn; } set { FWhereIntegerColumn = value; } }
	}
}