using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;

namespace Code.CodeGeneration.Plugins
{
	public abstract class ComponentDataBase : CodeGeneratorData
	{
		protected abstract string NameKey    { get; }
		protected abstract string DataKey    { get; }
		protected abstract string ContextKey { get; }

		public string Name
		{
			get => (string)this[NameKey];
			set => this[NameKey] = value;
		}

		public MemberData[] MemberData
		{
			get => (MemberData[])this[DataKey];
			set => this[DataKey] = value;
		}

		public string Context
		{
			get => (string)this[ContextKey];
			set => this[ContextKey] = value;
		}

		public static T Create<T>(INamedTypeSymbol type)
			where T : ComponentDataBase, new()
			=> new T
			{
				Name = type.Name,
				MemberData = type.GetData(),
				Context = type.GetContext(),
			};
	}
}