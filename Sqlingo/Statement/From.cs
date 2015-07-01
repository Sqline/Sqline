// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqlingo.Statement.Items;

namespace Sqlingo.Statement {
	public class From {
		private Select FOwner;
		private TokenSet FTokens;

		public From(Select owner, TokenSet tokens) {
			FOwner = owner;
			FTokens = tokens;
			Token OToken = FTokens.Advance();
			if (OToken.Value.ToUpper() != "FROM") {
				throw new Exception("Not a from statement");
			}
			while (!FTokens.Eof && !FOwner.CheckForStatement()) {
				OToken = FTokens.Advance();
				if (OToken.Type == TokenType.Raw && OToken.Value == "(") {
					FOwner.AddView(FOwner.ParseSubSelect());
				}
				else {
					List<Token> OView = new List<Token>();
					OView.Add(OToken);
					bool OIsFunction = false;
					while (!FTokens.Eof) {
						if (FOwner.CheckForStatement()) {
							break;
						}
						OToken = FTokens.Advance();
						if (OToken.Type == TokenType.Raw && OToken.Value == ",") {
							break;
						}
						if (OToken.Type == TokenType.Raw && OToken.Value == "(") {
							OIsFunction = true;
						}
						OView.Add(OToken);
					}
					if (OIsFunction) {
						FOwner.AddView(new StoredProcedure(OView));
					}
					else {
						FOwner.AddView(new Table(OView));
					}
				}
			}
		}

	}
}
