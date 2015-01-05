using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using T4Compiler.Generator;
using Sqline.CodeGeneration.ViewModel;
using Sqline.ClientFramework;
namespace Sqline.CodeTemplate.Templatec93630d359ec6fb9649eb00399be14bc {
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
Write(@"");
//ItemFile OItemFile = new ItemFile(@"D:\Projects\!ScanApps\Socialize\Socialize.DataAccess\user.items");
	ItemFile OItemFile = new ItemFile(ItemFilename);
	foreach (ViewItem OViewItem in OItemFile.ViewItems) {
Write(@"
");
Write(@"	public partial class ");
Write(OViewItem.FullName);
Write(@" {
");
Write(@"
");
Write(@"		public ");
Write(OViewItem.FullName);
Write(@"() {
");
Write(@"		}
");
Write(@"
");
Write(@"		");
foreach (Field OField in OViewItem.Fields) {
Write(@"
");
Write(@"		private ");
Write(OField.CsType);
Write(@" F");
Write(OField.Name);
Write(@";
");
Write(@"		");
Write(OField.Visibility);
Write(@" ");
Write(OField.CsType);
Write(@" ");
Write(OField.Name);
Write(@" {
");
Write(@"			get {
");
Write(@"				return F");
Write(OField.Name);
Write(@";
");
Write(@"			}
");
Write(@"			set {
");
Write(@"				F");
Write(OField.Name);
Write(@" = value;
");
Write(@"			}
");
Write(@"		}
");
Write(@"		");
}
Write(@"
");
Write(@"	}
");
Write(@"
");
Write(@"");
}
Write(@"
");
Write(@"}");
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
