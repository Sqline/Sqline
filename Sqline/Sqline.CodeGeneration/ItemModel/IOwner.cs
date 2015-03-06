using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sqline.CodeGeneration.ViewModel {
	public interface IOwner {
		void Throw(XObject element, string message);
	}
}
