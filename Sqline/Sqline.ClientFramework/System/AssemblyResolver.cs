// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sqline.ClientFramework {
    public static class AssemblyResolver {
		private static List<Assembly> FCachedAssemblies = new List<Assembly>();
		private static object FCacheSync = new object();

		public static Assembly LoadRessourceAssembly(Assembly resourceAssembly, string name) {
			Assembly OResult = GetCachedAssembly(name);
			if (OResult != null) {
				return OResult;
			}
			string OResourceName = GetAssemblyName(name);
			string[] OResourceNames = resourceAssembly.GetManifestResourceNames();
			if (!OResourceNames.Contains(OResourceName))
			{
				throw new ArgumentException("Resource was not found for " + name);
			}
			Stream OStream = resourceAssembly.GetManifestResourceStream(OResourceName);
			OResult = Assembly.Load(ReadAllBytes(OStream));
			StoreAssembly(OResult);
			return OResult;
		}

		private static Assembly GetCachedAssembly(string name) {
			lock (FCacheSync) {
				return FCachedAssemblies.FirstOrDefault(a => a.GetName().Name == name);
			}
		}

		private static void StoreAssembly(Assembly assembly) {
			lock (FCacheSync) {
				FCachedAssemblies.Add(assembly);
			}
		}

		private static string GetAssemblyName(string name) {
			string OAsssemblyName = name.Split(',')[0];
			if (OAsssemblyName.EndsWith(".resources")) {
				return OAsssemblyName.Replace(".resources", ".dll");
			}
			else {
				return $"Sqline.ClientFramework.Resources.{OAsssemblyName}.dll";
			}
		}

		private static byte[] ReadAllBytes(Stream stream) {
			using (MemoryStream OStream = new MemoryStream()) {
				byte[] OBuffer = new byte[25600];
				while (true) {
					int ORead = stream.Read(OBuffer, 0, OBuffer.Length);
					if (ORead == 0) {
						return OStream.ToArray();
					}
					OStream.Write(OBuffer, 0, ORead);
				}
			}
		}
	}
}