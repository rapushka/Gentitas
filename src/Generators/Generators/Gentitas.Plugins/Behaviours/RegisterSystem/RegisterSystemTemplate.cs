namespace Code.CodeGeneration.Plugins.Behaviours.RegisterSystem
{
	public class RegisterSystemTemplate : TemplateBase
	{
		public RegisterSystemTemplate(string context) : base(context) { }
		protected override string DirectoryName => Constants.RegistrationSystem.DirectoryName;

		protected override string ClassName => "RegisterEntityBehavioursSystem";

		public override string FileContent => $@"
using Entitas;
	
public sealed class {ClassName} : IInitializeSystem
{{
	private readonly Contexts _contexts;
	private readonly EntityBehaviourBase[] _allBehaviours;

	public {ClassName}(Contexts contexts, EntityBehaviourBase[] allBehaviours)
	{{
		_contexts = contexts;
		_allBehaviours = allBehaviours;
	}}

	public void Initialize()
	{{
		foreach (var behaviour in _allBehaviours)
		{{
			behaviour.Register();
		}}
	}}
}}";
	}
}