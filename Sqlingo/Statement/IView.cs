// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlingo.Statement {

	public interface IColumn {
		string Name { get; }
		string Path { get; }
	}

	public interface IView {

		List<IColumn> Columns { get; }
		string Name { get; }

	}
}
