using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Code.CodeGeneration.Plugins.Behaviours
{
	// ReSharper disable once UnusedType.Global – used by Jenny
	public class ComponentBehavioursGenerator : ICodeGenerator
	{
		public string name         => Constants.GeneratorName;
		public int    priority     => Constants.GeneratorPriority;
		public bool   runInDryMode => Constants.GeneratorRunInDryMode;

		public CodeGenFile[] Generate(CodeGeneratorData[] data)
			=> data
			   .OfType<BehaviourData>()
			   .Select(DataToSystem)
			   .ToArray();

		private CodeGenFile DataToSystem(BehaviourData data)
		{
			var template = new ComponentBehaviourTemplate(data);

			return new CodeGenFile(template.FileName, template.FileContent, GeneratorName);
		}

		private string GeneratorName => GetType().FullName;
	}
}