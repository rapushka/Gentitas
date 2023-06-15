using static Code.CodeGeneration.Plugins.Behaviours.Constants.Method;

namespace Code.CodeGeneration.Plugins.Behaviours
{
	public class ComponentBehaviourTemplate : TemplateBase
	{
		public ComponentBehaviourTemplate(ComponentDataBase data) : base(data) { }

		protected override string DirectoryName => Constants.ComponentBehaviour.DirectoryName;

		public override    string FileContent   => IsFlag ? FlagClass : ValuedClass;

		private string FlagClass
			=> $@"
public class {ClassName} : {BaseClassName}
{{
	public override void {AddToEntity}(ref {Context}Entity entity) => entity.is{ComponentName} = true;

	public override void {RemoveFromEntity}(ref {Context}Entity entity) => entity.is{ComponentName} = false;
}}
";

		private string ValuedClass
			=> $@"
using UnityEngine;

public class {ClassName} : {BaseClassName}
{{
{Fields}

	public override void {AddToEntity}(ref {Context}Entity entity) => entity.Add{ComponentName}({Args});

	public override void {RemoveFromEntity}(ref {Context}Entity entity) => entity.Remove{ComponentName}();
}}
";

		protected override string ClassName => ComponentName + Constants.ComponentBehaviour.GeneratorClassPostfix;

		private string BaseClassName => Context + Constants.ComponentBehaviour.BaseClassPostfix;

		private string Fields => MembersAs(Field, separator: LineBreak);

		private string Args => MembersAs((m) => $"_{m.GetCamelCaseName()}", separator: Coma);
	}
}