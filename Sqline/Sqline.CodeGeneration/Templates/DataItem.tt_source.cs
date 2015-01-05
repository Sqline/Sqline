using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using T4Compiler.Generator;
using Schemalizer.Model;
using Sqline.CodeGeneration.ViewModel;
using Sqline.ClientFramework;
namespace Sqline.CodeTemplate.Template6787ba7fadb3513670e1bf2d9fb0bee2 {
public class CodeTemplate : ICodeTemplate {
private StringBuilder FStringOutput = new StringBuilder();
public string Generate() {
Write(@"");
/* TODO: Usings and Namespace from Configuration XML.
			Get prefix symbol (default: _) for private fields based on configuration.
	*/
Write(@"
");
Write(@"using System;
");
Write(@"using System.Data;
");
Write(@"using System.Text;
");
Write(@"using System.Collections;
");
Write(@"
");
Write(@"using Sqline.ClientFramework;
");
Write(@"
");
Write(@"namespace Socialize.DataAccess {
");
Write(@"");
XElement OElement = XElement.Load(@"D:\Debug\SqlDBModel.xdml");
	SchemaModel OModel = SchemaModel.FromXElement(OElement);
	SchemaViewModel OViewModel = new SchemaViewModel(OModel);

	foreach (ViewTable OTable in OViewModel.Tables) {
Write(@"
");
Write(@"
");
Write(@"	");
/* Meta Class */
Write(@"
");
Write(@"	public static class ");
Write(OTable.TableName);
Write(@"Meta {
");
Write(@"	");
foreach (ViewColumn OColumn in OTable.Columns) {
Write(@"
");
Write(@"		");
if (OColumn.IsString) {
Write(@"
");
Write(@"		public static int ");
Write(OColumn.Name);
Write(@"MaxLength { get { return ");
Write(OColumn.MaxLength);
Write(@"; } }	
");
Write(@"		");
}
Write(@"
");
Write(@"	");
}
Write(@"
");
Write(@"	}
");
Write(@"
");
Write(@"    ");
/* Insert Class */
Write(@"
");
Write(@"	public class ");
Write(OTable.TableName);
Write(@"Insert : InsertDataItem {
");
Write(@"  	");
foreach (ViewColumn OColumn in OTable.Columns) {
Write(@"
");
Write(@"		private ");
Write(OColumn.ParamType);
Write(@" F");
Write(OColumn.Name);
Write(@";
");
Write(@"	");
}
Write(@"
");
Write(@"
");
Write(@"		public ");
Write(OTable.TableName);
Write(@"Insert(");
Write(OTable.RequiredColumns.BuildString((c) => c.ParamType + " " + c.Name.ToLower(), ", "));
Write(@") {
");
Write(@"			Initialize(""");
Write(OTable.SchemaName);
Write(@""", """);
Write(OTable.TableName);
Write(@""");
");
Write(@"			");
Write(OTable.RequiredColumns.BuildString((c) => "F" + c.Name + " = " + c.Name.ToLower() + ";", "\r\n"));
Write(@"
");
Write(@"		}
");
Write(@"
");
Write(@"		protected override void PreExecute() {
");
Write(@"			");
Write(OTable.Columns.BuildString((c) => "AddParameter(F" + c.Name + ", \"" + c.Name + "\");", "\r\n"));
Write(@"
");
Write(@"		}
");
Write(@"
");
Write(@"	");
foreach (ViewColumn OColumn in OTable.Columns) {
Write(@"
");
Write(@"		");
if (OColumn.Required) {
Write(@"
");
Write(@"			public ");
Write(OColumn.ParamType);
Write(@" ");
Write(OColumn.Name);
Write(@" { get { return F");
Write(OColumn.Name);
Write(@"; } private set { F");
Write(OColumn.Name);
Write(@" = value; } }
");
Write(@"		");
} else {
Write(@"
");
Write(@"			public ");
Write(OColumn.ParamType);
Write(@" ");
Write(OColumn.Name);
Write(@" { get { return F");
Write(OColumn.Name);
Write(@"; } set { F");
Write(OColumn.Name);
Write(@" = value; } }
");
Write(@"		");
}
Write(@"
");
Write(@"	");
}
Write(@"
");
Write(@"	}
");
Write(@"
");
Write(@"	");
/* Update Class */
Write(@"
");
Write(@"	public class ");
Write(OTable.TableName);
Write(@"Update : UpdateDataItem {
");
Write(@"  	");
foreach (ViewColumn OColumn in OTable.Columns) {
Write(@"
");
Write(@"		private ");
Write(OColumn.ParamType);
Write(@" F");
Write(OColumn.Name);
Write(@";
");
Write(@"		private ");
Write(OColumn.WhereType);
Write(@" FWhere");
Write(OColumn.Name);
Write(@";
");
Write(@"	");
}
Write(@"
");
Write(@"
");
Write(@"		public ");
Write(OTable.TableName);
Write(@"Update() {
");
Write(@"			Initialize(""");
Write(OTable.SchemaName);
Write(@""", """);
Write(OTable.TableName);
Write(@""");
");
Write(@"		}
");
Write(@"
");
Write(@"		protected override void PreExecute() {
");
Write(@"			");
Write(OTable.Columns.BuildString((c) => "AddParameter(F" + c.Name + ", \"" + c.Name + "\");", "\r\n"));
Write(@"
");
Write(@"			");
Write(OTable.Columns.BuildString((c) => "AddParameter(FWhere" + c.Name + ", \"" +c.Name + "\");", "\r\n"));
Write(@"
");
Write(@"		}
");
Write(@"
");
Write(@"	");
foreach (ViewColumn OColumn in OTable.Columns) {
Write(@"
");
Write(@"		public ");
Write(OColumn.ParamType);
Write(@" ");
Write(OColumn.Name);
Write(@" { get { return F");
Write(OColumn.Name);
Write(@"; } set { F");
Write(OColumn.Name);
Write(@" = value; } }
");
Write(@"		public ");
Write(OColumn.WhereType);
Write(@" Where");
Write(OColumn.Name);
Write(@" { get { return FWhere");
Write(OColumn.Name);
Write(@"; } set { FWhere");
Write(OColumn.Name);
Write(@" = value; } }
");
Write(@"	");
}
Write(@"
");
Write(@"	}
");
Write(@"
");
Write(@"	");
/* Delete Class */
Write(@"
");
Write(@"	public class ");
Write(OTable.TableName);
Write(@"Delete : DeleteDataItem {
");
Write(@"  	");
foreach (ViewColumn OColumn in OTable.Columns) {
Write(@"
");
Write(@"		private ");
Write(OColumn.WhereType);
Write(@" FWhere");
Write(OColumn.Name);
Write(@";
");
Write(@"	");
}
Write(@"
");
Write(@"
");
Write(@"		public ");
Write(OTable.TableName);
Write(@"Delete() {
");
Write(@"			Initialize(""");
Write(OTable.SchemaName);
Write(@""", """);
Write(OTable.TableName);
Write(@""");
");
Write(@"		}
");
Write(@"
");
Write(@"		protected override void PreExecute() {
");
Write(@"			");
Write(OTable.Columns.BuildString((c) => "AddParameter(FWhere" + c.Name + ", \"" + c.Name + "\");", "\r\n"));
Write(@"
");
Write(@"		}
");
Write(@"
");
Write(@"	");
foreach (ViewColumn OColumn in OTable.Columns) {
Write(@"
");
Write(@"		public ");
Write(OColumn.WhereType);
Write(@" Where");
Write(OColumn.Name);
Write(@" { get { return FWhere");
Write(OColumn.Name);
Write(@"; } set { FWhere");
Write(OColumn.Name);
Write(@" = value; } }
");
Write(@"	");
}
Write(@"
");
Write(@"	}
");
Write(@"");
}
Write(@"	
");
Write(@"}");
return FStringOutput.ToString();
}
private void Write(object obj) { FStringOutput.Append(obj); }
public void SetParameter(string name, object value) {
}
}
}
