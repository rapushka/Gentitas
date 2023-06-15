using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Code.CodeGeneration.Plugins.Behaviours
{
	// ReSharper disable once UnusedType.Global – used by Jenny
	public class EntityBehaviourGenerator : ICodeGenerator
	{
		public string name         => Constants.GeneratorName;
		public int    priority     => Constants.GeneratorPriority;
		public bool   runInDryMode => Constants.GeneratorRunInDryMode;

		public CodeGenFile[] Generate(CodeGeneratorData[] data)
			=> data
			   .OfType<BehaviourData>()
			   .Select((d) => d.Context)
			   .ToHashSet()
			   .Select(AsFile)
			   .ToArray();

		private CodeGenFile AsFile(string context)
		{
			var template = new EntityBehaviourTemplate(context);

			return new CodeGenFile(template.FileName, template.FileContent, GeneratorName);
		}

		private string GeneratorName => GetType().FullName;
	}
}