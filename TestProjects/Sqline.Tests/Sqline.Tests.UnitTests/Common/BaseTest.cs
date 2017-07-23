using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqline.Tests.DataAccess;
using Sqline.Tests.DataAccess.SqlServer.DataItems;
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

		public static bool CompareBytes(byte[] bytes1, byte[] bytes2) {
			bytes1 = TrimEnd(bytes1);
			bytes2 = TrimEnd(bytes2);
			//Debug.WriteLine("bytes1: " + DebugByteArrayToString(bytes1));
			//Debug.WriteLine("bytes2: " + DebugByteArrayToString(bytes2));
			return bytes1.SequenceEqual(bytes2);
		}

		public static string DebugByteArrayToString(byte[] bytes) {
			StringBuilder OResult = new StringBuilder(bytes.Length * 2);
			foreach (byte OByte in bytes) {
				OResult.AppendFormat("{0:x2}", OByte);
			}
			return OResult.ToString();
		}

		public static byte[] TrimEnd(byte[] array) {
			int OLastIndex = Array.FindLastIndex(array, b => b != 0);
			Array.Resize(ref array, OLastIndex + 1);
			return array;
		}
	}
}