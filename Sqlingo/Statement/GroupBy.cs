// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo.Statement {
	public class GroupBy {
		private Select FOwner;
		private TokenSet FTokens;

		public GroupBy(Select owner, TokenSet tokens) {
			FOwner = owner;
			FTokens = tokens;
			Token OToken = FTokens.Advance();
			if (OToken.Value.ToUpper() != "GROUP") {
				throw new Exception("Not a group by statement");
			}
			OToken = FTokens.Advance();
			if (OToken.Value.ToUpper() != "BY") {
				throw new Exception("Not a group by statement");
			}
			while (!FTokens.Eof && !FOwner.CheckForStatement()) {
				OToken = FTokens.Advance();
			}
		}

	}
}
