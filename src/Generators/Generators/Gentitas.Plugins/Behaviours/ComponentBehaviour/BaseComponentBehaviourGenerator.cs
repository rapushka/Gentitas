using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using static Code.CodeGeneration.Plugins.Behaviours.Constants.Method;

namespace Code.CodeGeneration.Plugins.Behaviours
{
	// ReSharper disable once UnusedType.Global – used by Jenny
	public class BaseComponentBehaviourGenerator : ICodeGenerator
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
			=> new CodeGenFile
			(
				Path.Combine(Constants.ComponentBehaviour.DirectoryName, context, $"{Template.ClassName(context)}.cs"),
				Template.ClassContent(context),
				GetType().FullName
			);

		private static class Template
		{
			public static string ClassContent(string context)
				=> $@"
using UnityEngine;

public abstract class {ClassName(context)} : MonoBehaviour
{{
	public abstract void {AddToEntity}(ref {context}Entity entity);

	public abstract void {RemoveFromEntity}(ref {context}Entity entity);
}}";

			public static string ClassName(string context) => context + Constants.ComponentBehaviour.BaseClassPostfix;
		}
	}
}