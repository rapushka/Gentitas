using System.Collections.Generic;
using System.Linq;
using Code.CodeGeneration.Attributes;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Serialization;
using PluginUtil = DesperateDevs.Roslyn.CodeGeneration.Plugins.PluginUtil;

namespace Code.CodeGeneration.Plugins.Behaviours
{
	// ReSharper disable once UnusedType.Global – used in Jenny
	public class ComponentBehaviourDataProvider : IDataProvider, IConfigurable, ICachable
	{
		private readonly ProjectPathConfig _projectPathConfig = new ProjectPathConfig();

		public string name         => Constants.GeneratorName;
		public int    priority     => Constants.GeneratorPriority;
		public bool   runInDryMode => Constants.GeneratorRunInDryMode;

		public Dictionary<string, object> objectCache { get; set; }

		public Dictionary<string, string> defaultProperties => _projectPathConfig.defaultProperties;

		public void Configure(Preferences preferences) => _projectPathConfig.Configure(preferences);

		// ReSharper disable once CoVariantArrayConversion - this is a Roslyn API
		public CodeGeneratorData[] GetData()
			=> PluginUtil
			   .GetCachedProjectParser(objectCache, _projectPathConfig.projectPath)
			   .GetTypes()
			   .WithAttribute<BehaviourAttribute>()
			   .SelectMany(ComponentDataBase.Create<BehaviourData>)
			   .ToArray();
	}
}