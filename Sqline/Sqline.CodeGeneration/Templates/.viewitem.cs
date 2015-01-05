
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Sqline.ClientFramework;

namespace Socialize.DataAccess {

	public partial class VUser {

		public VUser() {
		}

		
		private int FID;
		public int ID {
			get {
				return FID;
			}
			set {
				FID = value;
			}
		}
		
		private string FName;
		public string Name {
			get {
				return FName;
			}
			set {
				FName = value;
			}
		}
		
		private int? FAge;
		internal int? Age {
			get {
				return FAge;
			}
			set {
				FAge = value;
			}
		}
		
		private string FUniqueID;
		public string UniqueID {
			get {
				return FUniqueID;
			}
			set {
				FUniqueID = value;
			}
		}
		
	}


}