// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo {
	public class TokenError {
		private string FError;
		private int FLine;
		private int FColumn;
		public TokenError(string error, int line, int column) {
			FError = error;
			FLine = line;
			FColumn = column;
		}
		public override string ToString() {
			return FError + "\t(" + FLine + ":" + FColumn + ")";
		}
	}

	public enum TokenType { Raw, Name, Value };

	public class Token {
		private string FToken;
		private TokenType FType;
		private int FLine;
		private int FColumn;
		public Token(string token, TokenType type, int line, int column) {
			FToken = token;
			FType = type;
			FLine = line;
			FColumn = column;
		}

		public string Value {
			get {
				return FToken;
			}
		}

		public TokenType Type {
			get {
				return FType;
			}
		}

		public int Line {
			get {
				return FLine;
			}
		}

		public int Column {
			get {
				return FColumn;
			}
		}

		public override string ToString() {
			return FToken + "\t(" + FLine + ":" + FColumn + ")";
		}
	}

	public class SqlTokenizer {
		private char[] FOperatorChars = { '=', '!', '<', '>', '+', '-', '*', '/', '%', '|', '&', '^', '~' };
		private static string[] FOperators = { "==", "=", "!=", "!", ">=", ">>", ">>=", ">", "<=", "<<", "<<=", "<>", "<", "+=", "++", "+", "-=", "--", "-", "*=", "*", "/=", "/", "%=", "%", "|=", "|", "&=", "&", "^", "^=", "~", "~=" };
		private List<TokenError> FErrors = new List<TokenError>();
		private List<Token> FTokens = new List<Token>();
		private StringBuilder FCurrentToken = new StringBuilder();
		private int FLineNoToken = -1;
		private int FColumnToken = -1;
		private int FLineNo = 1;
		private int FColumn = 0;
		private int FIndex = 0;
		private string FStatement;
		private bool FEof = false;
		private bool FFailed = false;

		private void CommitToken(TokenType type) {
			if (FCurrentToken.Length > 0) {
				FTokens.Add(new Token(FCurrentToken.ToString(), type, FLineNoToken, FColumnToken));
				FCurrentToken.Clear();
				FLineNoToken = -1;
				FColumnToken = -1;
			}
		}

		private void ErrorIfToken(char c) {
			if (FCurrentToken.Length > 0) {
				FFailed = true;
				FErrors.Add(new TokenError("Unexpected character '" + c + "' after " + FCurrentToken, FLineNo, FColumn));
			}
		}

		private void UnexpectedEndOfStatement(int line, int column) {
			FFailed = true;
			FErrors.Add(new TokenError("Unexpected end of statement starting at (" + line + ":" + column + ")", FLineNo, FColumn));
		}

		private char Advance() {
			char c = FStatement[FIndex++];
			FColumn++;
			if (c == '\n') {
				FLineNo++;
				FColumn = 0;
			}
			if (FIndex >= FStatement.Length) {
				FEof = true;
			}
			return c;
		}

		private char Peek() {
			if (FEof) {
				return ' ';
			}
			return FStatement[FIndex];
		}

		private void LookFor(char end, TokenType type) {
			FLineNoToken = FLineNo;
			FColumnToken = FColumn;
			if (FEof) {
				UnexpectedEndOfStatement(FLineNoToken, FColumnToken);
				return;
			}
			char c = Advance();
			bool OLastWasBackslash = c == '\\';
			while (!FEof && !FFailed) {
				FCurrentToken.Append(c);
				c = Advance();
				if (c == end) {
					if (OLastWasBackslash) {
						FCurrentToken.Length = FCurrentToken.Length - 1;
					}
					else {
						break;
					}
				}
				OLastWasBackslash = c == '\\';
			}
			if (c != end) {
				UnexpectedEndOfStatement(FLineNoToken, FColumnToken);
				return;
			}
			CommitToken(type);
		}

		public SqlTokenizer(string statement) {
			FStatement = statement;
		}

		public bool Tokenize() {
			while (!FEof && !FFailed) {
				char c = Advance();
				if (char.IsWhiteSpace(c)) {
					CommitToken(TokenType.Raw);
				}
				else if (c == '.' || c == ';' || c == ',' || c == '(' || c == ')') {
					CommitToken(TokenType.Raw);
					FTokens.Add(new Token(c.ToString(), TokenType.Raw, FLineNo, FColumn));
				}
				else if (Array.IndexOf(FOperatorChars, c) != -1) {
					CommitToken(TokenType.Raw);
					FLineNoToken = FLineNo;
					FColumnToken = FColumn;
					FCurrentToken.Append(c);
					while (true) {
						char p = Peek();
						if (Array.IndexOf(FOperatorChars, p) != -1) {
							if (Array.IndexOf(FOperators, FCurrentToken.ToString() + p) != -1) {
								c = Advance();
								FCurrentToken.Append(c);
							}
							else {
								break;
							}
						}
						else {
							break;
						}
					}
					CommitToken(TokenType.Raw);
				}
				else if (c == '[') {
					ErrorIfToken(c);
					LookFor(']', TokenType.Name);
				}
				else if (c == '\'') {
					ErrorIfToken(c);
					LookFor('\'', TokenType.Value);
				}
				else if (c == '"') {
					ErrorIfToken(c);
					LookFor('"', TokenType.Name);
				}
				else if (c == '`') {
					ErrorIfToken(c);
					LookFor('`', TokenType.Name);
				}
				else {
					if (FLineNoToken == -1) {
						FLineNoToken = FLineNo;
						FColumnToken = FColumn;
					}
					FCurrentToken.Append(c);
				}
			}
			if (!FFailed) {
				CommitToken(TokenType.Raw);
			}
			return !FFailed;
		}

		public List<Token> Tokens {
			get {
				return FTokens;
			}
		}

		public List<TokenError> Errors {
			get {
				return FErrors;
			}
		}


	}
}
