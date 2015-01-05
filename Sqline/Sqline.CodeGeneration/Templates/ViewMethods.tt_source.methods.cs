using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using T4Compiler.Generator;
using Sqline.CodeGeneration.ViewModel;
using Sqline.ClientFramework;
namespace Sqline.CodeTemplate.Templatea8f9822ed84d322c4e0e018fc5683c19 {
public class CodeTemplate : ICodeTemplate {
private StringBuilder FStringOutput = new StringBuilder();
public string Generate() {
Write(@"");
/* TODO: Usings and Namespace from Configuration XML.
		Get prefix symbol (default: _) for private fields based on configuration.
		Get postfix class name (default: Handler) based on configuration.
*/
Write(@"
");
Write(@"	
");
Write(@"using System;
");
Write(@"using System.Data;
");
Write(@"using System.Text;
");
Write(@"using System.Collections.Generic;
");
Write(@"using Sqline.ClientFramework;
");
Write(@"using Sqline.ClientFramework.ProviderModel;
");
Write(@"
");
Write(@"namespace Socialize.DataAccess {
");
Write(@"
");
Write(@"	public partial class ");
Write("" /* name of item file*/);
Write(@"UserHandler {
");
Write(@"	");
ItemFile OItemFile = new ItemFile(ItemFilename);
		foreach (ViewItem OViewItem in OItemFile.ViewItems) {
			foreach (Method OMethod in OViewItem.Methods) {
Write(@"
");
Write(@"		public List<");
Write(OViewItem.FullName);
Write(@"> ");
Write(OMethod.Name);
Write(@"() {
");
Write(@"			List<");
Write(OViewItem.FullName);
Write(@"> OResult = new List<");
Write(OViewItem.FullName);
Write(@">();
");
Write(@"			string sql = @""");
Write(OMethod.Sql.Statement);
Write(@""";
");
Write(@"			using (IDbConnection OConnection = Provider.Current.GetConnection(DA.ConnectionString)) {
");
Write(@"				using (IDbCommand OCommand = OConnection.CreateCommand()) {
");
Write(@"					OCommand.CommandText = sql;
");
Write(@"					OCommand.CommandType = CommandType.Text;
");
Write(@"					OConnection.Open();
");
Write(@"					using (IDataReader OReader = OCommand.ExecuteReader()) {
");
Write(@"						");
foreach (Field OField in OMethod.GetFields("queryfield")) {
Write(@"
");
Write(@"						int ");
Write(OField.IndexFieldName);
Write(@" = OReader.GetIndex(""");
Write(OField.Name);
Write(@""");
");
Write(@"						");
if (OField.Default != null) {
Write(@"
");
Write(@"						bool ");
Write(OField.HasValueFieldName);
Write(@" = false;
");
Write(@"						");
}
Write(@"
");
Write(@"						");
}
Write(@"
");
Write(@"						while (OReader.Read()) {
");
Write(@"						");
Write(OViewItem.FullName);
Write(@" OViewItem = new ");
Write(OViewItem.FullName);
Write(@"();
");
Write(@"						");
foreach (Field OField in OMethod.GetFields("queryfield")) {
Write(@"
");
Write(@"							OViewItem.");
Write(OField.Name);
Write(@" = OReader.");
Write(OField.CsReaderMethod);
Write(@";
");
Write(@"						");
}
Write(@"
");
Write(@"						");
foreach (Field OField in OMethod.Fields) {
Write(@"
");
Write(@"							");
if (OField.Default != null) {
Write(@"
");
Write(@"							if (!");
Write(OField.HasValueFieldName);
Write(@") {
");
Write(@"								OViewItem.");
Write(OField.Name);
Write(@" = new Func<");
Write(OViewItem.FullName);
Write(@", ");
Write(OField.CsTypeNonNullable);
Write(@">((viewitem) => ");
Write(OField.Default);
Write(@")(OViewItem);
");
Write(@"							}
");
Write(@"							");
}
Write(@"
");
Write(@"							");
if (OField.Transform != null) {
Write(@"
");
Write(@"							OViewItem.");
Write(OField.Name);
Write(@" = new Func<");
Write(OField.CsTypeNonNullable);
Write(@", ");
Write(OViewItem.FullName);
Write(@", ");
Write(OField.CsTypeNonNullable);
Write(@">((");
Write(OField.Name);
Write(@", viewitem) => ");
Write(OField.Transform);
Write(@")((");
Write(OField.CsTypeNonNullable);
Write(@")OViewItem.");
Write(OField.Name);
Write(@", OViewItem);
");
Write(@"							");
}
Write(@"
");
Write(@"						");
}
Write(@"
");
Write(@"							OResult.Add(OViewItem);
");
Write(@"						}
");
Write(@"					}
");
Write(@"				}
");
Write(@"			}
");
Write(@"			return OResult;
");
Write(@"		}
");
Write(@"
");
Write(@"	");
} }
Write(@"
");
Write(@"	}
");
Write(@"}
");
Write(@"
");
Write(@"");
return FStringOutput.ToString();
}
private void Write(object obj) { FStringOutput.Append(obj); }

public System.String ItemFilename { get; set; }
public void SetParameter(string name, object value) {
  if (name == "ItemFilename") {
    ItemFilename = (System.String)value;
  }
}
}
}
