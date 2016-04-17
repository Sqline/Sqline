// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;

namespace Sqline.ProviderModel.SqlServer {
	public class TypeMapping : ITypeMapping {
		private string FProviderType;
		private string FDBType;
		private string FCSType;
		private string FCSNullable;
		private string FCSReader;
		private bool FIsNumber;

		public bool IsNumber {
			get {
				return FIsNumber;
			}
			set {
				FIsNumber = value;
			}
		}

		public string CSReader {
			get {
				return FCSReader;
			}
			set {
				FCSReader = value;
			}
		}

		public string CSNullable {
			get {
				return FCSNullable;
			}
			set {
				FCSNullable = value;
			}
		}

		public string CSType {
			get {
				return FCSType;
			}
			set {
				FCSType = value;
			}
		}

		public string DBType {
			get {
				return FDBType;
			}
			set {
				FDBType = value;
			}
		}

		public string ProviderType {
			get {
				return FProviderType;
			}
			set {
				FProviderType = value;
			}
		}
	}
}
