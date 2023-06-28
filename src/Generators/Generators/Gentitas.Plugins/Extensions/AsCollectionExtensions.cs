namespace Code.CodeGeneration
{
	public static class AsCollectionExtensions
	{
		public static T[] AsArray<T>(this T @this) => new[] { @this };
	}
}