using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DesperateDevs.Roslyn;
using Entitas.CodeGeneration.Attributes;
using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;

namespace Code.CodeGeneration.Plugins
{
	public static class NamedTypeSymbolExtensions
	{
		private const string Attribute = nameof(Attribute);

		public static IEnumerable<INamedTypeSymbol> WithAttribute<T>(this IEnumerable<INamedTypeSymbol> @this)
			=> @this.Where((t) => t.HasAttribute<T>());

		private static bool HasAttribute<T>(this ISymbol @this)
			=> @this.GetAttributes().Any((a) => a.ToString() == typeof(T).Name.RemoveAttributeSuffix());

		private static bool Any(this ImmutableArray<AttributeData> @this, Func<AttributeData, bool> predicate)
			=> Enumerable.Any(@this, predicate);

		private static string RemoveAttributeSuffix(this string @this)
			=> @this.EndsWith(Attribute) ? @this.Substring(0, @this.Length - Attribute.Length) : @this;

		public static string GetContext(this INamedTypeSymbol @this)
			=> @this.GetAttributes()
			        .Select((ad) => ad.AttributeClass)
			        .SingleOrDefault((a) => a.BaseType is null == false && a.BaseType.Name == nameof(ContextAttribute))
			        ?.Name.RemoveAttributeSuffix()
			   ?? string.Empty;

		public static MemberData[] GetData(this INamedTypeSymbol @this)
			=> @this.GetMembers()
			        .OfType<IFieldSymbol>()
			        .Select((f) => new MemberData(f.Type.ToCompilableString(), f.Name))
			        .ToArray();

		public static bool IsFlag(this ComponentDataBase @this) => @this.MemberData.ToArray().Any() == false;
	}
}