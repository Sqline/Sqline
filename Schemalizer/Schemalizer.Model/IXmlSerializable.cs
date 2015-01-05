// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System.Xml.Linq;

namespace Schemalizer.Model {
	interface IXmlSerializable {
		XElement ToXElement();
	}
}
