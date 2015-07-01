// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo {
	public class TokenSet {
		private List<Token> FTokens;
		private int FIndex = 0;
		private bool FEof = false;

		public TokenSet(List<Token> tokens) {
			FTokens = tokens;
		}

		public Token Advance() {
			Token OToken = FTokens[FIndex++];
			if (FIndex >= FTokens.Count) {
				FEof = true;
			}
			return OToken;
		}

		public Token Peek() {
			return FTokens[FIndex];
		}

		public bool SequenceFollows(params string[] tokens) {
			if (FIndex + tokens.Length > FTokens.Count) {
				return false;
			}
			for (int i = 0; i < tokens.Length; i++) {
				Token OToken = FTokens[FIndex + i];
				if (OToken.Type != TokenType.Raw) {
					return false;
				}
				if (OToken.Value.ToUpper() != tokens[i]) {
					return false;
				}
			}
			return true;
		}

		public bool Eof {
			get {
				return FEof;
			}
		}

	}
}
