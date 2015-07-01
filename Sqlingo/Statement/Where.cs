// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo.Statement {
	public class Where {
		private Select FOwner;
		private TokenSet FTokens;

		public Where(Select owner, TokenSet tokens) {
			FOwner = owner;
			FTokens = tokens;
			Token OToken = FTokens.Advance();
			if (OToken.Value.ToUpper() != "WHERE") {
				throw new Exception("Not where statement");
			}
			while (!FTokens.Eof && !FOwner.CheckForStatement()) {
				OToken = FTokens.Advance();
			}
		}

	}
}
