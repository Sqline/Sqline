// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"

namespace Sqline.ClientFramework {
    public interface ITypeMapping {
		string ProviderType { get; }
		string DBType { get; }
		string CSType { get; }
		string CSNullable { get; }
		string CSReader { get; }
		bool IsNumber { get; }
	}
}
