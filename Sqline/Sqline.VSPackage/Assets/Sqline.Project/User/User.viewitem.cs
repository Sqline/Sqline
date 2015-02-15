using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Sqline.ClientFramework;
using Sqline.ClientFramework.ProviderModel;


namespace $safeprojectname$.ViewItems {

	public partial class VUser{

		public VUser() {	
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
		
		private string FEmail;
		public string Email {
			get {
				return FEmail;
			}
			set {
				FEmail = value;
			}
		}
		
	}


}

