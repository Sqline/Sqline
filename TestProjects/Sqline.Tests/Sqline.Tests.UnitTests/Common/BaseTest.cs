using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqline.Tests.DataAccess;
using Sqline.Tests.DataAccess.DataItems;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.Tests.UnitTests {
	public abstract class BaseTest {

		public void InsertTypeTest(TypeTestInsert insert) {
			Debug.WriteLine(insert.GetGeneratedStatement());
			int OAffected = insert.Execute();
			Assert.IsTrue(OAffected == 1);
		}

		public void UpdateTypeTest(TypeTestUpdate update, Action<TypeTestUpdate> assignWhere) {
			update.AllowUnsafeQuery();
			assignWhere(update);
			Debug.WriteLine(update.GetGeneratedStatement());
			int OAffected = update.Execute();
			Assert.IsTrue(OAffected > 0);
		}

		public void DeleteTypeTest(Action<TypeTestDelete> assignWhere) {
			TypeTestDelete delete = new TypeTestDelete();
			assignWhere(delete);
			delete.AllowUnsafeQuery();
			Debug.WriteLine(delete.GetGeneratedStatement());
			int OAffected = delete.Execute();			
			Assert.IsTrue(OAffected >= 0);
		}
	}
}