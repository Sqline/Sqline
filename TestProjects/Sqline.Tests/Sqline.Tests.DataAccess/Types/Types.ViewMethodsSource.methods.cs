using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using T4Compiler.Generator;
using Sqline.CodeGeneration.ConfigurationModel;
using Sqline.CodeGeneration.ViewModel;
using Sqline.ClientFramework;
using System.IO;
namespace Sqline.CodeTemplate.Templatef4447af7c4c7f036593f8847d67fb791 {
public class CodeTemplate : ICodeTemplate {
private StringBuilder FStringOutput = new StringBuilder();
public string Generate() {
/* TODO: Usings and Namespace from Configuration XML.
		Get prefix symbol (default: _) for private fields based on configuration.
		Get postfix class name (default: Handler) based on configuration.
*/
ItemFile OItemFile = new ItemFile(ProjectDir, ItemFilename);
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
foreach (Using OUsing in OItemFile.Configuration.Methods.Usings) {
Write(@"using ");
Write(OUsing.Namespace);
Write(@";
");
}
if (OItemFile.Configuration.ViewItems.Namespace != OItemFile.Configuration.Methods.Namespace) {
Write(@"using ");
Write(OItemFile.Configuration.ViewItems.Namespace);
Write(@";
");
}
Write(@"namespace ");
Write(OItemFile.Configuration.Methods.Namespace);
Write(@" {
");
Write(@"
");
Write(@"	public partial class ");
Write(OItemFile.Configuration.Methods.Prefix);
Write(@"");
Write(OItemFile.ItemName);
Write(@"");
Write(OItemFile.Configuration.Methods.Postfix);
Write(@" {
");
Write(@"
");
foreach (ViewItem OViewItem in OItemFile.ViewItems) {
			foreach (ViewMethod OMethod in OViewItem.Methods) {
bool OTransactionSupport = false;
Write(@"		");
Write(OMethod.VisibilityString);
Write(@" ");
Write(OMethod.Result == ResultType.List ? "List<" + OViewItem.FullName + ">" :  OViewItem.FullName);
Write(@" ");
Write(OMethod.Name);
Write(@"(");
Write(GetArguments(OMethod, OTransactionSupport));
Write(@") {
");
if (OMethod.Result == ResultType.List) {
Write(@"			List<");
Write(OViewItem.FullName);
Write(@"> OResult = new List<");
Write(OViewItem.FullName);
Write(@">();
");
} else {
Write(@"			");
Write(OViewItem.FullName);
Write(@" OResult = null;
");
}
Write(@"			string OSql = @""");
Write(OMethod.Sql.Statement);
Write(@""";
");
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
					Parameter OParameter = OMethod.Parameters[i];
					if (OParameter.Type.EndsWith("List")) {
Write(@"			OSql = OSql.Replace(Provider.Current.GetParameterName(""");
Write(OParameter.Name);
Write(@"""), ""("" + Provider.Current.GenerateParameterQuery(""p");
Write(i);
Write(@"_"", ");
Write(OParameter.ArgumentName);
Write(@".Count) + "")"");
");
}
}
if (!OTransactionSupport) {
Write(@"			using (IDbConnection OConnection = Provider.Current.GetConnection(");
Write(OItemFile.Configuration.ProjectHandler.SqlineConfig);
Write(@".ConnectionString)) {
");
}
Write(@"				using (IDbCommand OCommand = ");
Write(OTransactionSupport ? "connection" : "OConnection");
Write(@".CreateCommand()) {
");
if (OTransactionSupport) {
Write(@"					OCommand.Connection = connection;
");
Write(@"					OCommand.Transaction = transaction;
");
}
Write(@"					OCommand.CommandText = OSql;
");
Write(@"					OCommand.CommandType = CommandType.Text;
");
if (OMethod.Timeout > 0) {
Write(@"					OCommand.CommandTimeout = ");
Write(OMethod.Timeout);
Write(@";
");
}
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
						 Parameter OParameter = OMethod.Parameters[i];
if (OParameter.Type.EndsWith("List")) {
Write(@"					for (int i = 0; i < ");
Write(OParameter.ArgumentName);
Write(@".Count; i++) {
");
Write(@"						IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@" = OCommand.CreateParameter();
");
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".ParameterName = ""@p");
Write(i);
Write(@"_"" + i;
");
if (OParameter.Type == "DateList") {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".DbType = DbType.Date;
");
}
if (OParameter.Nullable) {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@"[i] ?? (object)DBNull.Value;
");
} else {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@"[i];
");
}
Write(@"						OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@");
");
Write(@"					}
");
} else {
Write(@"					IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@" = OCommand.CreateParameter();
");
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".ParameterName = ""@");
Write(OParameter.Name);
Write(@""";
");
if (OParameter.Nullable) {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@" ?? (object)DBNull.Value;
");
} else {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@";
");
}
Write(@"					OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@");
");
}
}
if (!OTransactionSupport) {
Write(@"					OConnection.Open();
");
}
Write(@"					using (IDataReader OReader = OCommand.ExecuteReader()) {
");
foreach (Field OField in OMethod.GetFields("queryfield")) {
Write(@"						int ");
Write(OField.IndexFieldName);
Write(@" = OReader.GetIndex(""");
Write(OField.Name);
Write(@""");
");
if (OField.Default != null) {
Write(@"						bool ");
Write(OField.HasValueFieldName);
Write(@" = false;
");
}
}
Write(@"						");
Write(OMethod.Result == ResultType.List ? "while" :  "if");
Write(@" (OReader.Read()) {
");
Write(@"						");
Write(OViewItem.FullName);
Write(@" OViewItem = new ");
Write(OViewItem.FullName);
Write(@"();
");
Write(@"						OViewItem.PreInitialize();
");
foreach (Field OField in OMethod.GetFields("queryfield")) {
Write(@"							OViewItem.");
Write(OField.Name);
Write(@" = OReader.");
Write(OField.CsReaderMethod);
Write(@";
");
}
foreach (Field OField in OMethod.Fields) {
if (OField.Default != null) {
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
Write(@">((item) => ");
Write(OField.Default);
Write(@")(OViewItem);
");
Write(@"							}
");
}
if (OField.Transform != null) {
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
Write(@", item) => ");
Write(OField.Transform);
Write(@")((");
Write(OField.CsTypeNonNullable);
Write(@")OViewItem.");
Write(OField.Name);
Write(@", OViewItem);
");
}
}
Write(@"						OViewItem.PostInitialize();
");
if (OMethod.Result == ResultType.List) {
if (OMethod.Filter != null) {
Write(@"							if (new Func<");
Write(OViewItem.FullName);
Write(@", bool>(item => ");
Write(OMethod.Filter);
Write(@")(OViewItem)) {
");
Write(@"								OResult.Add(OViewItem);
");
Write(@"							}
");
} else {
Write(@"							OResult.Add(OViewItem);
");
}
}
if (OMethod.Result == ResultType.SingleItem) {
Write(@"						OResult = OViewItem;
");
}
Write(@"						}
");
Write(@"					}
");
Write(@"				}
");
if (!OTransactionSupport) {
Write(@"			}
");
}
if (OMethod.Result == ResultType.List) {
if (OMethod.Sort != null) {
Write(@"			OResult.Sort((item1, item2) => ");
Write(OMethod.Sort);
Write(@");
");
}
}
Write(@"			return OResult;
");
Write(@"		}
");
if (OMethod.TransactionSupport) {
OTransactionSupport = true;
Write(@"		");
Write(OMethod.VisibilityString);
Write(@" ");
Write(OMethod.Result == ResultType.List ? "List<" + OViewItem.FullName + ">" :  OViewItem.FullName);
Write(@" ");
Write(OMethod.Name);
Write(@"(");
Write(GetArguments(OMethod, OTransactionSupport));
Write(@") {
");
if (OMethod.Result == ResultType.List) {
Write(@"			List<");
Write(OViewItem.FullName);
Write(@"> OResult = new List<");
Write(OViewItem.FullName);
Write(@">();
");
} else {
Write(@"			");
Write(OViewItem.FullName);
Write(@" OResult = null;
");
}
Write(@"			string OSql = @""");
Write(OMethod.Sql.Statement);
Write(@""";
");
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
					Parameter OParameter = OMethod.Parameters[i];
					if (OParameter.Type.EndsWith("List")) {
Write(@"			OSql = OSql.Replace(Provider.Current.GetParameterName(""");
Write(OParameter.Name);
Write(@"""), ""("" + Provider.Current.GenerateParameterQuery(""p");
Write(i);
Write(@"_"", ");
Write(OParameter.ArgumentName);
Write(@".Count) + "")"");
");
}
}
if (!OTransactionSupport) {
Write(@"			using (IDbConnection OConnection = Provider.Current.GetConnection(");
Write(OItemFile.Configuration.ProjectHandler.SqlineConfig);
Write(@".ConnectionString)) {
");
}
Write(@"				using (IDbCommand OCommand = ");
Write(OTransactionSupport ? "connection" : "OConnection");
Write(@".CreateCommand()) {
");
if (OTransactionSupport) {
Write(@"					OCommand.Connection = connection;
");
Write(@"					OCommand.Transaction = transaction;
");
}
Write(@"					OCommand.CommandText = OSql;
");
Write(@"					OCommand.CommandType = CommandType.Text;
");
if (OMethod.Timeout > 0) {
Write(@"					OCommand.CommandTimeout = ");
Write(OMethod.Timeout);
Write(@";
");
}
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
						 Parameter OParameter = OMethod.Parameters[i];
if (OParameter.Type.EndsWith("List")) {
Write(@"					for (int i = 0; i < ");
Write(OParameter.ArgumentName);
Write(@".Count; i++) {
");
Write(@"						IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@" = OCommand.CreateParameter();
");
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".ParameterName = ""@p");
Write(i);
Write(@"_"" + i;
");
if (OParameter.Type == "DateList") {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".DbType = DbType.Date;
");
}
if (OParameter.Nullable) {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@"[i] ?? (object)DBNull.Value;
");
} else {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@"[i];
");
}
Write(@"						OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@");
");
Write(@"					}
");
} else {
Write(@"					IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@" = OCommand.CreateParameter();
");
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".ParameterName = ""@");
Write(OParameter.Name);
Write(@""";
");
if (OParameter.Nullable) {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@" ?? (object)DBNull.Value;
");
} else {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@";
");
}
Write(@"					OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@");
");
}
}
if (!OTransactionSupport) {
Write(@"					OConnection.Open();
");
}
Write(@"					using (IDataReader OReader = OCommand.ExecuteReader()) {
");
foreach (Field OField in OMethod.GetFields("queryfield")) {
Write(@"						int ");
Write(OField.IndexFieldName);
Write(@" = OReader.GetIndex(""");
Write(OField.Name);
Write(@""");
");
if (OField.Default != null) {
Write(@"						bool ");
Write(OField.HasValueFieldName);
Write(@" = false;
");
}
}
Write(@"						");
Write(OMethod.Result == ResultType.List ? "while" :  "if");
Write(@" (OReader.Read()) {
");
Write(@"						");
Write(OViewItem.FullName);
Write(@" OViewItem = new ");
Write(OViewItem.FullName);
Write(@"();
");
Write(@"						OViewItem.PreInitialize();
");
foreach (Field OField in OMethod.GetFields("queryfield")) {
Write(@"							OViewItem.");
Write(OField.Name);
Write(@" = OReader.");
Write(OField.CsReaderMethod);
Write(@";
");
}
foreach (Field OField in OMethod.Fields) {
if (OField.Default != null) {
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
Write(@">((item) => ");
Write(OField.Default);
Write(@")(OViewItem);
");
Write(@"							}
");
}
if (OField.Transform != null) {
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
Write(@", item) => ");
Write(OField.Transform);
Write(@")((");
Write(OField.CsTypeNonNullable);
Write(@")OViewItem.");
Write(OField.Name);
Write(@", OViewItem);
");
}
}
Write(@"						OViewItem.PostInitialize();
");
if (OMethod.Result == ResultType.List) {
if (OMethod.Filter != null) {
Write(@"							if (new Func<");
Write(OViewItem.FullName);
Write(@", bool>(item => ");
Write(OMethod.Filter);
Write(@")(OViewItem)) {
");
Write(@"								OResult.Add(OViewItem);
");
Write(@"							}
");
} else {
Write(@"							OResult.Add(OViewItem);
");
}
}
if (OMethod.Result == ResultType.SingleItem) {
Write(@"						OResult = OViewItem;
");
}
Write(@"						}
");
Write(@"					}
");
Write(@"				}
");
if (!OTransactionSupport) {
Write(@"			}
");
}
if (OMethod.Result == ResultType.List) {
if (OMethod.Sort != null) {
Write(@"			OResult.Sort((item1, item2) => ");
Write(OMethod.Sort);
Write(@");
");
}
}
Write(@"			return OResult;
");
Write(@"		}
");
}
} }
foreach (ScalarItem OScalarItem in OItemFile.ScalarItems) {
			foreach (ScalarMethod OMethod in OScalarItem.Methods) {
bool OTransactionSupport = false;
Write(@"		");
Write(OMethod.VisibilityString);
Write(@" ");
Write(OMethod.Result == ResultType.List ? "List<" + OScalarItem.CsType + ">" :  OScalarItem.CsType);
Write(@" ");
Write(OMethod.Name);
Write(@"(");
Write(GetArguments(OMethod, OTransactionSupport));
Write(@") {
");
if (OMethod.Result == ResultType.List) {
Write(@"			List<");
Write(OScalarItem.CsType);
Write(@"> OResult = new List<");
Write(OScalarItem.CsType);
Write(@">();
");
} else {
Write(@"			");
Write(OScalarItem.CsType);
Write(@" OResult = default(");
Write(OScalarItem.CsType);
Write(@");
");
}
Write(@"			string OSql = @""");
Write(OMethod.Sql.Statement);
Write(@""";
");
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
					Parameter OParameter = OMethod.Parameters[i];
					if (OParameter.Type.EndsWith("List")) {
Write(@"			OSql = OSql.Replace(Provider.Current.GetParameterName(""");
Write(OParameter.Name);
Write(@"""), ""("" + Provider.Current.GenerateParameterQuery(""p");
Write(i);
Write(@"_"", ");
Write(OParameter.ArgumentName);
Write(@".Count) + "")"");
");
}
}
if (!OTransactionSupport) {
Write(@"			using (IDbConnection OConnection = Provider.Current.GetConnection(");
Write(OItemFile.Configuration.ProjectHandler.SqlineConfig);
Write(@".ConnectionString)) {
");
}
Write(@"				using (IDbCommand OCommand = ");
Write(OTransactionSupport ? "connection" : "OConnection");
Write(@".CreateCommand()) {
");
if (OTransactionSupport) {
Write(@"					OCommand.Connection = connection;
");
Write(@"					OCommand.Transaction = transaction;
");
}
Write(@"					OCommand.CommandText = OSql;
");
Write(@"					OCommand.CommandType = CommandType.Text;
");
if (OMethod.Timeout > 0) {
Write(@"					OCommand.CommandTimeout = ");
Write(OMethod.Timeout);
Write(@";
");
}
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
						 Parameter OParameter = OMethod.Parameters[i];
if (OParameter.Type.EndsWith("List")) {
Write(@"					for (int i = 0; i < ");
Write(OParameter.ArgumentName);
Write(@".Count; i++) {
");
Write(@"						IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@" = OCommand.CreateParameter();
");
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".ParameterName = ""@p");
Write(i);
Write(@"_"" + i;
");
if (OParameter.Type == "DateList") {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".DbType = DbType.Date;
");
}
if (OParameter.Nullable) {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@"[i] ?? (object)DBNull.Value;
");
} else {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@"[i];
");
}
Write(@"						OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@");
");
Write(@"					}
");
} else {
Write(@"					IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@" = OCommand.CreateParameter();
");
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".ParameterName = ""@");
Write(OParameter.Name);
Write(@""";
");
if (OParameter.Nullable) {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@" ?? (object)DBNull.Value;
");
} else {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@";
");
}
Write(@"					OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@");
");
}
}
if (!OTransactionSupport) {
Write(@"					OConnection.Open();
");
}
Write(@"					using (IDataReader OReader = OCommand.ExecuteReader()) {
");
if (OScalarItem.Default != null) {
Write(@"						bool OHasValue = false;
");
}
Write(@"						");
Write(OMethod.Result == ResultType.List ? "while" :  "if");
Write(@" (OReader.Read()) {
");
Write(@"							");
Write(OScalarItem.CsType);
Write(@" OScalarItem = OReader.");
Write(OScalarItem.CsReaderMethod);
Write(@";
");
if (OScalarItem.Default != null) {
Write(@"							if (!OHasValue) {
");
Write(@"								OScalarItem = new Func<");
Write(OScalarItem.CsTypeNonNullable);
Write(@">(() => ");
Write(OScalarItem.Default);
Write(@")();
");
Write(@"							}
");
}
if (OScalarItem.Transform != null) {
Write(@"							OScalarItem = new Func<");
Write(OScalarItem.CsType);
Write(@", ");
Write(OScalarItem.CsType);
Write(@">((value) => ");
Write(OScalarItem.Transform);
Write(@")(OScalarItem);
");
}
if (OMethod.Result == ResultType.List) {
if (OMethod.Filter != null) {
Write(@"							if (new Func<");
Write(OScalarItem.CsType);
Write(@", bool>(value => ");
Write(OMethod.Filter);
Write(@")(OScalarItem)) {
");
Write(@"								OResult.Add(OScalarItem);
");
Write(@"							}
");
} else {
Write(@"							OResult.Add(OScalarItem);
");
}
}
if (OMethod.Result == ResultType.SingleItem) {
Write(@"							OResult = OScalarItem;
");
}
Write(@"						}
");
Write(@"					}
");
Write(@"				}
");
if (!OTransactionSupport) {
Write(@"			}
");
}
if (OMethod.Result == ResultType.List) {
if (OMethod.Sort != null) {
Write(@"			OResult.Sort((value1, value2) => ");
Write(OMethod.Sort);
Write(@");
");
}
}
Write(@"			return OResult;
");
Write(@"		}
");
if (OMethod.TransactionSupport) {
OTransactionSupport = true;
Write(@"		");
Write(OMethod.VisibilityString);
Write(@" ");
Write(OMethod.Result == ResultType.List ? "List<" + OScalarItem.CsType + ">" :  OScalarItem.CsType);
Write(@" ");
Write(OMethod.Name);
Write(@"(");
Write(GetArguments(OMethod, OTransactionSupport));
Write(@") {
");
if (OMethod.Result == ResultType.List) {
Write(@"			List<");
Write(OScalarItem.CsType);
Write(@"> OResult = new List<");
Write(OScalarItem.CsType);
Write(@">();
");
} else {
Write(@"			");
Write(OScalarItem.CsType);
Write(@" OResult = default(");
Write(OScalarItem.CsType);
Write(@");
");
}
Write(@"			string OSql = @""");
Write(OMethod.Sql.Statement);
Write(@""";
");
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
					Parameter OParameter = OMethod.Parameters[i];
					if (OParameter.Type.EndsWith("List")) {
Write(@"			OSql = OSql.Replace(Provider.Current.GetParameterName(""");
Write(OParameter.Name);
Write(@"""), ""("" + Provider.Current.GenerateParameterQuery(""p");
Write(i);
Write(@"_"", ");
Write(OParameter.ArgumentName);
Write(@".Count) + "")"");
");
}
}
if (!OTransactionSupport) {
Write(@"			using (IDbConnection OConnection = Provider.Current.GetConnection(");
Write(OItemFile.Configuration.ProjectHandler.SqlineConfig);
Write(@".ConnectionString)) {
");
}
Write(@"				using (IDbCommand OCommand = ");
Write(OTransactionSupport ? "connection" : "OConnection");
Write(@".CreateCommand()) {
");
if (OTransactionSupport) {
Write(@"					OCommand.Connection = connection;
");
Write(@"					OCommand.Transaction = transaction;
");
}
Write(@"					OCommand.CommandText = OSql;
");
Write(@"					OCommand.CommandType = CommandType.Text;
");
if (OMethod.Timeout > 0) {
Write(@"					OCommand.CommandTimeout = ");
Write(OMethod.Timeout);
Write(@";
");
}
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
						 Parameter OParameter = OMethod.Parameters[i];
if (OParameter.Type.EndsWith("List")) {
Write(@"					for (int i = 0; i < ");
Write(OParameter.ArgumentName);
Write(@".Count; i++) {
");
Write(@"						IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@" = OCommand.CreateParameter();
");
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".ParameterName = ""@p");
Write(i);
Write(@"_"" + i;
");
if (OParameter.Type == "DateList") {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".DbType = DbType.Date;
");
}
if (OParameter.Nullable) {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@"[i] ?? (object)DBNull.Value;
");
} else {
Write(@"						OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@"[i];
");
}
Write(@"						OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@"_");
Write(i);
Write(@");
");
Write(@"					}
");
} else {
Write(@"					IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@" = OCommand.CreateParameter();
");
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".ParameterName = ""@");
Write(OParameter.Name);
Write(@""";
");
if (OParameter.Nullable) {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@" ?? (object)DBNull.Value;
");
} else {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@";
");
}
Write(@"					OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@");
");
}
}
if (!OTransactionSupport) {
Write(@"					OConnection.Open();
");
}
Write(@"					using (IDataReader OReader = OCommand.ExecuteReader()) {
");
if (OScalarItem.Default != null) {
Write(@"						bool OHasValue = false;
");
}
Write(@"						");
Write(OMethod.Result == ResultType.List ? "while" :  "if");
Write(@" (OReader.Read()) {
");
Write(@"							");
Write(OScalarItem.CsType);
Write(@" OScalarItem = OReader.");
Write(OScalarItem.CsReaderMethod);
Write(@";
");
if (OScalarItem.Default != null) {
Write(@"							if (!OHasValue) {
");
Write(@"								OScalarItem = new Func<");
Write(OScalarItem.CsTypeNonNullable);
Write(@">(() => ");
Write(OScalarItem.Default);
Write(@")();
");
Write(@"							}
");
}
if (OScalarItem.Transform != null) {
Write(@"							OScalarItem = new Func<");
Write(OScalarItem.CsType);
Write(@", ");
Write(OScalarItem.CsType);
Write(@">((value) => ");
Write(OScalarItem.Transform);
Write(@")(OScalarItem);
");
}
if (OMethod.Result == ResultType.List) {
if (OMethod.Filter != null) {
Write(@"							if (new Func<");
Write(OScalarItem.CsType);
Write(@", bool>(value => ");
Write(OMethod.Filter);
Write(@")(OScalarItem)) {
");
Write(@"								OResult.Add(OScalarItem);
");
Write(@"							}
");
} else {
Write(@"							OResult.Add(OScalarItem);
");
}
}
if (OMethod.Result == ResultType.SingleItem) {
Write(@"							OResult = OScalarItem;
");
}
Write(@"						}
");
Write(@"					}
");
Write(@"				}
");
if (!OTransactionSupport) {
Write(@"			}
");
}
if (OMethod.Result == ResultType.List) {
if (OMethod.Sort != null) {
Write(@"			OResult.Sort((value1, value2) => ");
Write(OMethod.Sort);
Write(@");
");
}
}
Write(@"			return OResult;
");
Write(@"		}
");
}
} }
foreach (VoidItem OVoidItem in OItemFile.VoidItems) {
			foreach (VoidMethod OMethod in OVoidItem.Methods) {
bool OTransactionSupport = false;
Write(@"		");
Write(OMethod.VisibilityString);
Write(@" int ");
Write(OMethod.Name);
Write(@"(");
Write(GetArguments(OMethod, OTransactionSupport));
Write(@") {
");
Write(@"			int OResult = 0;
");
Write(@"			string OSql = @""");
Write(OMethod.Sql.Statement);
Write(@""";
");
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
					Parameter OParameter = OMethod.Parameters[i];
					if (OParameter.Type.EndsWith("List")) {
Write(@"			OSql = OSql.Replace(Provider.Current.GetParameterName(""");
Write(OParameter.Name);
Write(@"""), ""("" + Provider.Current.GenerateParameterQuery(""p");
Write(i);
Write(@"_"", ");
Write(OParameter.ArgumentName);
Write(@".Count) + "")"");
");
}
}
if (!OTransactionSupport) {
Write(@"			using (IDbConnection OConnection = Provider.Current.GetConnection(");
Write(OItemFile.Configuration.ProjectHandler.SqlineConfig);
Write(@".ConnectionString)) {
");
}
Write(@"				using (IDbCommand OCommand = ");
Write(OTransactionSupport ? "connection" : "OConnection");
Write(@".CreateCommand()) {
");
if (OTransactionSupport) {
Write(@"					OCommand.Connection = connection;
");
Write(@"					OCommand.Transaction = transaction;
");
}
Write(@"					OCommand.CommandText = OSql;
");
Write(@"					OCommand.CommandType = CommandType.Text;
");
if (OMethod.Timeout > 0) {
Write(@"					OCommand.CommandTimeout = ");
Write(OMethod.Timeout);
Write(@";
");
}
foreach (Parameter OParameter in OMethod.Parameters) {
Write(@"					IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@" = OCommand.CreateParameter();
");
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".ParameterName = ""@");
Write(OParameter.Name);
Write(@""";
");
if (OParameter.Nullable) {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@" ?? (object)DBNull.Value;
");
} else {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@";
");
}
Write(@"					OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@");
");
}
if (!OTransactionSupport) {
Write(@"					OConnection.Open();
");
}
Write(@"					OResult = OCommand.ExecuteNonQuery();
");
Write(@"				}
");
if (!OTransactionSupport) {
Write(@"			}
");
}
Write(@"			return OResult;
");
Write(@"		}
");
if (OMethod.TransactionSupport) {
OTransactionSupport = true;
Write(@"		");
Write(OMethod.VisibilityString);
Write(@" int ");
Write(OMethod.Name);
Write(@"(");
Write(GetArguments(OMethod, OTransactionSupport));
Write(@") {
");
Write(@"			int OResult = 0;
");
Write(@"			string OSql = @""");
Write(OMethod.Sql.Statement);
Write(@""";
");
for (int i = 0; i < OMethod.Parameters.Count; i++) { 
					Parameter OParameter = OMethod.Parameters[i];
					if (OParameter.Type.EndsWith("List")) {
Write(@"			OSql = OSql.Replace(Provider.Current.GetParameterName(""");
Write(OParameter.Name);
Write(@"""), ""("" + Provider.Current.GenerateParameterQuery(""p");
Write(i);
Write(@"_"", ");
Write(OParameter.ArgumentName);
Write(@".Count) + "")"");
");
}
}
if (!OTransactionSupport) {
Write(@"			using (IDbConnection OConnection = Provider.Current.GetConnection(");
Write(OItemFile.Configuration.ProjectHandler.SqlineConfig);
Write(@".ConnectionString)) {
");
}
Write(@"				using (IDbCommand OCommand = ");
Write(OTransactionSupport ? "connection" : "OConnection");
Write(@".CreateCommand()) {
");
if (OTransactionSupport) {
Write(@"					OCommand.Connection = connection;
");
Write(@"					OCommand.Transaction = transaction;
");
}
Write(@"					OCommand.CommandText = OSql;
");
Write(@"					OCommand.CommandType = CommandType.Text;
");
if (OMethod.Timeout > 0) {
Write(@"					OCommand.CommandTimeout = ");
Write(OMethod.Timeout);
Write(@";
");
}
foreach (Parameter OParameter in OMethod.Parameters) {
Write(@"					IDbDataParameter OParameter");
Write(OParameter.Name);
Write(@" = OCommand.CreateParameter();
");
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".ParameterName = ""@");
Write(OParameter.Name);
Write(@""";
");
if (OParameter.Nullable) {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@" ?? (object)DBNull.Value;
");
} else {
Write(@"					OParameter");
Write(OParameter.Name);
Write(@".Value = ");
Write(OParameter.ArgumentName);
Write(@";
");
}
Write(@"					OCommand.Parameters.Add(OParameter");
Write(OParameter.Name);
Write(@");
");
}
if (!OTransactionSupport) {
Write(@"					OConnection.Open();
");
}
Write(@"					OResult = OCommand.ExecuteNonQuery();
");
Write(@"				}
");
if (!OTransactionSupport) {
Write(@"			}
");
}
Write(@"			return OResult;
");
Write(@"		}
");
}
} }
Write(@"	}
");
Write(@"}
");
Write(@"");
return FStringOutput.ToString();
}
private void Write(object obj) { FStringOutput.Append(obj); }

public System.String ItemFilename { get; set; }

public System.String ProjectDir { get; set; }

 

public string GetArguments(BaseMethod method, bool transactionSupport) {
	StringBuilder result = new StringBuilder();
	if (transactionSupport) {
		result.Append("IDbConnection connection, IDbTransaction transaction");
	}
	foreach (Parameter OParameter in method.Parameters) {
		if (result.Length > 0) {
			result.Append(", ");
		}
		result.Append(OParameter.CsType);
		result.Append(" ");
		result.Append(OParameter.ArgumentName);
	}
	return result.ToString();
}


public void SetParameter(string name, object value) {
  if (name == "ItemFilename") {
    ItemFilename = (System.String)value;
  }
  if (name == "ProjectDir") {
    ProjectDir = (System.String)value;
  }
}
}
}
