using DesperateDevs.Utils;
using Entitas.CodeGeneration.Plugins;

namespace Code.CodeGeneration
{
	public static class MemberDataExtensions
	{
		public static string GetCamelCaseName(this MemberData @this) => @this.name.LowercaseFirst();
	}
}