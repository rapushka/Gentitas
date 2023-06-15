using DesperateDevs.Utils;

namespace Code.CodeGeneration.Plugins
{
	public static class StringExtensions
	{
		public static string ToCamelCase(this string @this) => @this.LowercaseFirst();
	}
}