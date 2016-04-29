using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Sqline.ClientFramework;
namespace Sqline.Tests.DataAccess.PostgreSql.ViewItems {
	public partial class User : IViewItem{

		public User() {	
		}

		public void PreInitialize() {
			OnPreInitialize();
		}

		public void PostInitialize() {
			OnPostInitialize();
		}

		partial void OnPreInitialize();
		partial void OnPostInitialize();

		private DateTime FName;
		public DateTime Name {
			get {
				return FName;
			}
			set {
				FName = value;
			}
		}
		private int FEmail;
		public int Email {
			get {
				return FEmail;
			}
			set {
				FEmail = value;
			}
		}
	}
}
