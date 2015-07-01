// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqlingo.Statement.Items;

namespace Sqlingo.Statement {
	public class Select : IView, IColumn {
		private string FName;
		private TokenSet FTokens;
		private From FFrom;
		private List<Join> FJoins = new List<Join>();
		private Where FWhere;
		private OrderBy FOrderBy;
		private GroupBy FGroupBy;
		private Having FHaving;
		private Limit FLimit;
		private string FTop = null;
		private bool FDistinct = false;
		private List<IColumn> FColumns = new List<IColumn>();
		private List<IView> FViews = new List<IView>();

		public Select(List<Token> tokens) {
			FTokens = new TokenSet(tokens);
			Token OToken = FTokens.Advance();
			if (OToken.Value.ToUpper() != "SELECT") {
				throw new Exception("Not a select statement");
			}
			while (!FTokens.Eof && !CheckForStatement()) {
				OToken = FTokens.Advance();
				if (OToken.Type == TokenType.Raw && OToken.Value.ToUpper() == "TOP") {
					OToken = FTokens.Advance();
					if (OToken.Value == "(") {
						OToken = FTokens.Advance();
						FTop = OToken.Value;
						OToken = FTokens.Advance();
						if (OToken.Value != ")") {
							Console.WriteLine("Unexpected token: " + OToken);
						}
					}
					else {
						FTop = OToken.Value;
					}
				}
				else if (OToken.Type == TokenType.Raw && OToken.Value.ToUpper() == "DISTINCT") {
					FDistinct = true;
				}
				else if (OToken.Type == TokenType.Raw && OToken.Value.ToUpper() == "(") {
					FColumns.Add(ParseSubSelect());
				}
				else {
					List<Token> OColumn = new List<Token>();
					OColumn.Add(OToken);
					bool OFound = false;
					bool OIsFunction = false;
					while (!FTokens.Eof) {
						if (CheckForStatement()) {
							OFound = true;
							break;
						}
						OToken = FTokens.Advance();
						if (OToken.Type == TokenType.Raw && OToken.Value == ",") {
							OFound = true;
							break;
						}
						if (OToken.Type == TokenType.Raw && OToken.Value == "(") {
							OIsFunction = true;
						}
						OColumn.Add(OToken);
					}
					if (!OFound) {
						throw new Exception("Unexpected token: " + OToken);
					}
					if (OIsFunction) {
						FColumns.Add(new Function(OColumn));
					}
					else {
						FColumns.Add(new SelectColumn(OColumn));
					}
				}
			}
		}

		public Select(string name, List<Token> tokens) : this(tokens) {
			FName = name;
		}

		internal Select ParseSubSelect() {
			Token OToken;
			List<Token> OSubSelect = new List<Token>();
			int ODepth = 1;
			while (!FTokens.Eof) {
				OToken = FTokens.Advance();
				if (OToken.Value == "(") {
					ODepth++;
				}
				else if (OToken.Value == ")") {
					if (--ODepth == 0) {
						break;
					}
				}
				OSubSelect.Add(OToken);
			}
			string OName = null;
			if (!CheckForStatement()) {
				OToken = FTokens.Advance();
				if (OToken.Type == TokenType.Raw && OToken.Value != ",") {
					if (OToken.Type == TokenType.Raw && OToken.Value.ToUpper() == "AS") {
						OToken = FTokens.Advance();
					}
					OName = OToken.Value;
					Token OPeek = FTokens.Peek();
					if (OPeek.Type == TokenType.Raw && OPeek.Value == ",") {
						FTokens.Advance();
					}
				}
			}
			if (ODepth != 0) {
				throw new Exception("Subselect does not end");
			}
			return new Select(OName, OSubSelect);
		}

		internal void AddView(IView view) {
			FViews.Add(view);
		}

		public bool CheckForStatement() {
			if (FTokens.SequenceFollows("FROM")) {
				FFrom = new From(this, FTokens);
				return true;
			}
			else if (FTokens.SequenceFollows("INNER", "JOIN")
				|| FTokens.SequenceFollows("LEFT", "JOIN") || FTokens.SequenceFollows("LEFT", "OUTER", "JOIN")
				|| FTokens.SequenceFollows("RIGHT", "JOIN") || FTokens.SequenceFollows("RIGHT", "OUTER", "JOIN")
				|| FTokens.SequenceFollows("FULL", "JOIN") || FTokens.SequenceFollows("FULL", "OUTER", "JOIN")
				||FTokens.SequenceFollows("CROSS", "JOIN")
				) {
				FJoins.Add(new Join(this, FTokens));
				return true;
			}
			else if (FTokens.SequenceFollows("WHERE")) {
				FWhere = new Where(this, FTokens);
				return true;
			}
			else if (FTokens.SequenceFollows("ORDER", "BY")) {
				FOrderBy = new OrderBy(this, FTokens);
				return true;
			}
			else if (FTokens.SequenceFollows("GROUP", "BY")) {
				FGroupBy = new GroupBy(this, FTokens);
				return true;
			}
			else if (FTokens.SequenceFollows("HAVING")) {
				FHaving = new Having(this, FTokens);
				return true;
			}
			else if (FTokens.SequenceFollows("LIMIT")) {
				FLimit = new Limit(this, FTokens);
				return true;
			}
			return false;
		}

		public string Top {
			get {
				return FTop;
			}
		}

		public bool Distinct {
			get {
				return FDistinct;
			}
		}

		public List<IColumn> Columns {
			get {
				return FColumns;
			}
		}

		public List<IView> Views {
			get {
				return FViews;
			}
		}

		public string Name {
			get {
				return FName;
			}
		}

		public string Path {
			get {
				return "subselect";
			}
		}

		public override string ToString() {
			return "SubSelect: " + FName;
		}

	}
}
