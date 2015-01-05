// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using Schemalizer.Model;

namespace Schemalizer.Provider {
	public interface ISchemalizerProvider {
		void ExtractMetadata(SchemaModel model, string databaseName);
		bool HasSchemaChanged(SchemaModel model);
	}
}
