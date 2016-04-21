// Authors="Daniel Jonas Møller, Anders Eggers-Krag" License="New BSD License http://sqline.codeplex.com/license"
using System;
using Schemalizer.Model;

namespace Schemalizer.ProviderModel {
	public interface ISchemalizerProvider {
		string ConnectionString { get; set; }
		void ExtractMetadata(SchemaModel model, string databaseName);
		bool HasSchemaChanged(SchemaModel model);
	}
}
