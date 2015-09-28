using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.ClientFramework.Types {
	public class IDList<T> {
		[ThreadStatic]
		private static int FInstanceCount = 0;
		private static char[] FAlphabet =  { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
		private List<T> FValues = new List<T>();
		private StringBuilder FResult = new StringBuilder();
		private HashSet<object> FUniqueObjects = new HashSet<object>();
		private string FUniqueID;

		public IDList() {
			Init();
		}

		public IDList(params T[] values) {
			Init();
			foreach (T OValue in values) {
				Append(OValue);
			}
		}

		private void Init() {
			FInstanceCount++;
			GenerateUniqueID();
		}

		private void GenerateUniqueID() {
			StringBuilder OResult = new StringBuilder();
			string OStringID = FInstanceCount.ToString();
			for (int i = 0; i < OStringID.Length; i++) {
				int OID = int.Parse(OStringID[i].ToString());
				OResult.Append(FAlphabet[OID]);
			}
			FUniqueID = OResult.ToString();
		}

		public static IDList<long> ParseLongValues(String values) {
			IDList<long> OResult = new IDList<long>();
			foreach (String OValue in values.Split(',')) {
				long OLongValue;
				if (long.TryParse(OValue.Trim(), out OLongValue)) {
					OResult.Append(OLongValue);
				}
			}
			return OResult;
		}

		public static IDList<int> ParseIntValues(String values) {
			IDList<int> OResult = new IDList<int>();
			foreach (String OValue in values.Split(',')) {
				int OIntValue;
				if (int.TryParse(OValue.Trim(), out OIntValue)) {
					OResult.Append(OIntValue);
				}
			}
			return OResult;
		}

		public void Append(T value) {
			if (value != null) {				
				if (!FUniqueObjects.Contains(value)) {
					if (FResult.Length != 0) {
						FResult.Append(",");
					}
					if (value is string || value is ISpecializedString) {
						FResult.Append("'");
						FResult.Append(((string)(object)value).Replace("'", "\\'"));
						FResult.Append("'");
					}
					FResult.Append(value);										
					FValues.Add(value);
					FUniqueObjects.Add(value);
				}
			}
		}
		public String GetQuery() {
			return Provider.Current.GenerateParameterQuery("p" + FUniqueID, FValues.Count);
		}

		public List<T> Values {
			get {
				return FValues;
			}
		}

		public bool IsEmpty {
			get {
				return FValues.Count == 0;
			}
		}

		public int Count {
			get {
				return FValues.Count;
			}
		}

		public override string ToString() {
			return FResult.ToString();
		}
	}
}