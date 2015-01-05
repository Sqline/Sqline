// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;

namespace Sqline.ClientFramework.ProviderModel {
	public interface IProvider {
		 // Conver DB Types, Parameter Name, Table/Column Escape Name, DB Name, Version, Support

		string IdentifierStartDelimiter { get; }
		string IdentifierEndDelimiter { get; }
		string DelimitName(string name);
		string UndelimitName(string name);
		ITypeMapping GetTypeMapping(string providerType);
	}
}
