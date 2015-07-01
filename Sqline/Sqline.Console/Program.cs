// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using Sqline.ClientFramework;
using Sqlingo;
using Sqlingo.Statement;

namespace Sqline.ConsoleApp {
	public class Program {
		static void Main(string[] args) {

			SqlTokenizer OTokenizer = new SqlTokenizer(@"
			SELECT TOP(@Test) DISTINCT p.PeopleID, p.Name
				,n.PhoneNumberID, (SELECT TOP 1 myColumn from myTable) as col, n.PeopleID, n.Number, MAX(n.Number) AS MaxNumber, n.Number
				FROM dbo.People p, (SELECT TOP 1 myColumn from myTable) as test
			RIGHT JOIN dbo.PhoneNumbers n
				ON p.PeopleID = n.PeopleID
				WHERE p.PeopleID IS NULL
				order by p.PeopleID
				GROUP BY p.PeopleID
				HAVING MAX(p.PeopleID) > 5
				LIMIT 5, 4
			");

			

			if (OTokenizer.Tokenize()) {
				foreach (Token OToken in OTokenizer.Tokens) {
					Console.WriteLine("Token: " + OToken);
				}
				Select OSelect = new Select(OTokenizer.Tokens);
				Console.WriteLine("Select.Path: " + OTokenizer.Tokens);
				foreach (IColumn OColumn in OSelect.Columns) {
					Console.WriteLine(OColumn.Name + " : " + OColumn.Path);
				}				
				foreach (IView OView in OSelect.Views) {
					Console.WriteLine("View: " + OView);
				}
			}
			else {
				foreach (TokenError OError in OTokenizer.Errors) {
					Console.WriteLine(OError);
				}
			}


		/*	ValueParam<int> OParam1 = new ValueParam<int>(1);
			ValueParam<byte[]> OParam2 = new ValueParam<byte[]>(new byte[0]);

			NullableValueParam<string> OParam = new NullableValueParam<string>("Hello World!");
			OParam = DBNull.Value;
			System.Console.WriteLine((OParam == DBNull.Value));

			int OInt = OParam1;
			byte[] OBytes = OParam2;
			string OString = OParam;*/
		}
	}
}