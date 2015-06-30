// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using Schemalizer.Model;

namespace Sqline.CodeGeneration.ViewModel {
	public class ViewDatabase {
		private Database FBase;
		
		public ViewDatabase(Database database) {
			FBase = database;
		}

		public String Name {
			get {
				return FBase.Name;
			}
		}

		public DateTime LastModified {
			get {
				return FBase.LastModified;
			}
		}
	}
}
