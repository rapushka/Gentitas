namespace Code.CodeGeneration.Plugins.Behaviours
{
	public class BehaviourData : ComponentDataBase
	{
		protected override string NameKey    => Constants.GeneratorName + ".Name";
		protected override string DataKey    => Constants.GeneratorName + ".Data";
		protected override string ContextKey => Constants.GeneratorName + ".Context";
	}
}