// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
namespace Sqline.ClientFramework {
    public static class Provider {
        public static IProvider Current { get; private set; }

        public static void Initialize(IProvider provider) {
            Current = provider;
        }
	}
}