// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo.Statement {
	public class Limit {
		private Select FOwner;
		private TokenSet FTokens;

		public Limit(Select owner, TokenSet tokens) {
			FOwner = owner;
			FTokens = tokens;
			Token OToken = FTokens.Advance();
			if (OToken.Value.ToUpper() != "LIMIT") {
				throw new Exception("Not a limit statement");
			}
			while (!FTokens.Eof && !FOwner.CheckForStatement()) {
				OToken = FTokens.Advance();
			}
		}

	}
}
