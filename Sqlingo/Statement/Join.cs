// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqlingo.Statement.Items;

namespace Sqlingo.Statement {
	public enum JoinType { Inner, Left, Right, Full, Cross };

	public class Join {
		private Select FOwner;
		private TokenSet FTokens;
		private JoinType FType;
		private Table FTable;

		public Join(Select owner, TokenSet tokens) {
			FOwner = owner;
			FTokens = tokens;
			Token OToken = FTokens.Advance();
			if (OToken.Value.ToUpper() == "INNER") FType = JoinType.Inner;
			else if (OToken.Value.ToUpper() == "LEFT") FType = JoinType.Left;
			else if (OToken.Value.ToUpper() == "RIGHT") FType = JoinType.Right;
			else if (OToken.Value.ToUpper() == "FULL") FType = JoinType.Full;
			else if (OToken.Value.ToUpper() == "CROSS") FType = JoinType.Cross;
			else {
				Console.WriteLine("Unkown join type: " + OToken);
			}
			OToken = FTokens.Advance();
			if (OToken.Value == "OUTER") {
				OToken = FTokens.Advance();
			}
			if (OToken.Value.ToUpper() != "JOIN") {
				throw new Exception("Not a join statement");
			}

			List<Token> OTable = new List<Token>();
			while (!FTokens.Eof && !FOwner.CheckForStatement()) {
				OToken = FTokens.Advance();
				if (OToken.Type == TokenType.Raw && OToken.Value == "ON") {
					break;
				}
				OTable.Add(OToken);
			}
			FTable = new Table(OTable);
			FOwner.AddView(FTable);
			while (!FTokens.Eof && !FOwner.CheckForStatement()) {
				OToken = FTokens.Advance();

			}
		}

	}
}
