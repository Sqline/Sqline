// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public interface IBaseParam {
		string GetStatement(string columnName, string parameterName);

		void AddParameter(IDbCommand command);
		object Value { get; }
		Type Type { get; }
		string ColumnName { get; }
		string ParameterName { get; set; }
		bool HasValue { get; }
	}
}