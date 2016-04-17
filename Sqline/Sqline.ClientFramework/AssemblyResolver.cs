// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using Sqline.ClientFramework.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sqline.ClientFramework {
	public static class AssemblyResolver {
		
		public static void Initialize() {
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
			string OAssemblyName = GetAssemblyName(args.Name);
			Assembly OAssembly = Assembly.GetExecutingAssembly();
			string[] OResourceNames = OAssembly.GetManifestResourceNames();
			if (!OResourceNames.Contains(OAssemblyName)) {
				throw new ArgumentException("Resource was not found for " + args.Name);
			}
			Stream OStream = OAssembly.GetManifestResourceStream(OAssemblyName);
			return Assembly.Load(ReadAllBytes(OStream));
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