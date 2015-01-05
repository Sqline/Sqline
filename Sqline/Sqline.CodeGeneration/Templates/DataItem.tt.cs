
using System;
using System.Data;
using System.Text;
using System.Collections;

using Sqline.ClientFramework;

namespace Socialize.DataAccess {


	
	public static class UserMeta {
	
		
	
		
		public static int NameMaxLength { get { return 100; } }	
		
	
		
	
		
	
		
	
	}

    
	public class UserInsert : InsertDataItem {
  	
		private ValueParam<int> FID;
	
		private ValueParam<string> FName;
	
		private ValueParam<int> FAge;
	
		private ValueParam<DateTime> FCreatedDate;
	
		private ValueParam<DateTime> FUpdatedDate;
	

		public UserInsert(ValueParam<string> name, ValueParam<int> age) {
			Initialize("dbo", "User");
			FName = name;
FAge = age;
		}

		protected override void PreExecute() {
			AddParameter(FID, "ID");
AddParameter(FName, "Name");
AddParameter(FAge, "Age");
AddParameter(FCreatedDate, "CreatedDate");
AddParameter(FUpdatedDate, "UpdatedDate");
		}

	
		
			public ValueParam<int> ID { get { return FID; } set { FID = value; } }
		
	
		
			public ValueParam<string> Name { get { return FName; } private set { FName = value; } }
		
	
		
			public ValueParam<int> Age { get { return FAge; } private set { FAge = value; } }
		
	
		
			public ValueParam<DateTime> CreatedDate { get { return FCreatedDate; } set { FCreatedDate = value; } }
		
	
		
			public ValueParam<DateTime> UpdatedDate { get { return FUpdatedDate; } set { FUpdatedDate = value; } }
		
	
	}

	
	public class UserUpdate : UpdateDataItem {
  	
		private ValueParam<int> FID;
		private WhereParam<int> FWhereID;
	
		private ValueParam<string> FName;
		private WhereParam<string> FWhereName;
	
		private ValueParam<int> FAge;
		private WhereParam<int> FWhereAge;
	
		private ValueParam<DateTime> FCreatedDate;
		private WhereParam<DateTime> FWhereCreatedDate;
	
		private ValueParam<DateTime> FUpdatedDate;
		private WhereParam<DateTime> FWhereUpdatedDate;
	

		public UserUpdate() {
			Initialize("dbo", "User");
		}

		protected override void PreExecute() {
			AddParameter(FID, "ID");
AddParameter(FName, "Name");
AddParameter(FAge, "Age");
AddParameter(FCreatedDate, "CreatedDate");
AddParameter(FUpdatedDate, "UpdatedDate");
			AddParameter(FWhereID, "ID");
AddParameter(FWhereName, "Name");
AddParameter(FWhereAge, "Age");
AddParameter(FWhereCreatedDate, "CreatedDate");
AddParameter(FWhereUpdatedDate, "UpdatedDate");
		}

	
		public ValueParam<int> ID { get { return FID; } set { FID = value; } }
		public WhereParam<int> WhereID { get { return FWhereID; } set { FWhereID = value; } }
	
		public ValueParam<string> Name { get { return FName; } set { FName = value; } }
		public WhereParam<string> WhereName { get { return FWhereName; } set { FWhereName = value; } }
	
		public ValueParam<int> Age { get { return FAge; } set { FAge = value; } }
		public WhereParam<int> WhereAge { get { return FWhereAge; } set { FWhereAge = value; } }
	
		public ValueParam<DateTime> CreatedDate { get { return FCreatedDate; } set { FCreatedDate = value; } }
		public WhereParam<DateTime> WhereCreatedDate { get { return FWhereCreatedDate; } set { FWhereCreatedDate = value; } }
	
		public ValueParam<DateTime> UpdatedDate { get { return FUpdatedDate; } set { FUpdatedDate = value; } }
		public WhereParam<DateTime> WhereUpdatedDate { get { return FWhereUpdatedDate; } set { FWhereUpdatedDate = value; } }
	
	}

	
	public class UserDelete : DeleteDataItem {
  	
		private WhereParam<int> FWhereID;
	
		private WhereParam<string> FWhereName;
	
		private WhereParam<int> FWhereAge;
	
		private WhereParam<DateTime> FWhereCreatedDate;
	
		private WhereParam<DateTime> FWhereUpdatedDate;
	

		public UserDelete() {
			Initialize("dbo", "User");
		}

		protected override void PreExecute() {
			AddParameter(FWhereID, "ID");
AddParameter(FWhereName, "Name");
AddParameter(FWhereAge, "Age");
AddParameter(FWhereCreatedDate, "CreatedDate");
AddParameter(FWhereUpdatedDate, "UpdatedDate");
		}

	
		public WhereParam<int> WhereID { get { return FWhereID; } set { FWhereID = value; } }
	
		public WhereParam<string> WhereName { get { return FWhereName; } set { FWhereName = value; } }
	
		public WhereParam<int> WhereAge { get { return FWhereAge; } set { FWhereAge = value; } }
	
		public WhereParam<DateTime> WhereCreatedDate { get { return FWhereCreatedDate; } set { FWhereCreatedDate = value; } }
	
		public WhereParam<DateTime> WhereUpdatedDate { get { return FWhereUpdatedDate; } set { FWhereUpdatedDate = value; } }
	
	}
	
}