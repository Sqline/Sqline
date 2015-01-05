using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using T4Compiler.Generator;
using Schemalizer.Model;
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
Write(@"using System.Data;
");
Write(@"using Sqline.ClientFramework;
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
Write(@"    ");
ItemFile OItemFile = new ItemFile(@"D:\Projects\!ScanApps\Socialize\Socialize.DataAccess\user.items");
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
Write(@"					using (IDbDataReader OReader = OCommand.ExecuteReader()) {
");
Write(@"						");
Write(OViewItem.FullName);
Write(@" OViewItem = new ");
Write(OViewItem.FullName);
Write(@"();
");
Write(@"						while (OReader.Read()) {
");
Write(@"						");
foreach (Field OField in OMethod.Fields) {
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
Write(@"						}
");
Write(@"						OResult.Add(OViewItem);
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
Write(@"    ");
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
public void SetParameter(string name, object value) {
}
}
}
