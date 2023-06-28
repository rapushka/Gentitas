using System.IO;
using DesperateDevs.CodeGeneration;

namespace Code.CodeGeneration.Plugins.Behaviours
{
	// ReSharper disable once UnusedType.Global – used by Jenny
	public class BaseEntityBehaviourGenerator : ICodeGenerator
	{
		public string name         => Constants.GeneratorName;
		public int    priority     => Constants.GeneratorPriority;
		public bool   runInDryMode => Constants.GeneratorRunInDryMode;

		public CodeGenFile[] Generate(CodeGeneratorData[] data) => Generate().AsArray();

		private CodeGenFile Generate()
		{
			return new CodeGenFile
			(
				Path.Combine(Constants.EntityBehaviour.DirectoryName, $"{Constants.EntityBehaviour.BaseClassName}.cs"),
				FileContent,
				GeneratorName
			);
		}

		private string GeneratorName => GetType().FullName;

		private static string FileContent
			=> $@"
using UnityEngine;

public abstract class EntityBehaviourBase : MonoBehaviour
{{
	public abstract void {Constants.Method.Register}();
	public abstract void {Constants.Method.CollectComponentsFromGameObject}();
}}
";
	}
}