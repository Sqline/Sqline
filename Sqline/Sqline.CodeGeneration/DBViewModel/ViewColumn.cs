// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using Schemalizer.Base;
using Schemalizer.Model;
using Sqline.ClientFramework.ProviderModel;

namespace Sqline.CodeGeneration.ViewModel {
	public class ViewColumn {
		private OrderedDictionary<String, ViewDatabase> FDatabaseRelationships = new OrderedDictionary<String, ViewDatabase>();
		private Column FBase;
		private ITypeMapping FTypeMapping;
		
		public ViewColumn(Column column) {
			FBase = column;
			foreach (Database ODatabase in column.DatabaseRelationships) {
				FDatabaseRelationships.Add(ODatabase.Name, new ViewDatabase(ODatabase));
			}
			FTypeMapping = Provider.Current.GetTypeMapping(FBase.DataType);
		}

		public List<ViewDatabase> DatabaseRelationships {
			get {
				return FDatabaseRelationships.Values;
			}
		}

		public String Name {
			get {
				return FBase.Name;
			}
		}

		public String DataType {
			get {
				return FBase.DataType;
			}
		}

		public String CsType {
			get {
				if (FBase.Nullable) {
					return FTypeMapping.CSNullable;
				}
				return FTypeMapping.CSType;
			}
		}

		public String ParamType {
			get {
				if (FBase.Nullable) {
					return "NullableValueParam<" + CsType + ">";
				}
				else {
					return "ValueParam<" + CsType + ">";
				}				
			}
		}

		public String WhereType {
			get {
				if (FBase.Nullable) {
					return "NullableWhereParam<" + CsType + ">";
				}
				else {
					return "WhereParam<" + CsType + ">";
				}				
			}
		}

		public bool IsString {
			get {
				return CsType == "string";
			}
		}

		public String DefaultValue {
			get {
				return FBase.DefaultValue;
			}
		}

		public int MaxLength {
			get {
				return FBase.MaxLength;
			}
		}

		public bool Nullable {
			get {
				return FBase.Nullable;
			}
		}

		public bool PrimaryKey {
			get {
				return FBase.PrimaryKey;
			}
		}

		public bool Required {
			get {
				return FBase.Required;
			}
		}

		public bool AutoIncrement {
			get {
				return FBase.AutoIncrement;
			}
		}
	}
}
