using System;
using System.IO;
using System.Linq;
using Entitas.CodeGeneration.Plugins;

namespace Code.CodeGeneration.Plugins.Behaviours
{
	public abstract class TemplateBase
	{
		protected const string Coma = ", ";
		protected const string LineBreak = "\n";

		protected TemplateBase(ComponentDataBase data)
		{
			Name = data.Name;
			Context = data.Context;
			MemberData = data.MemberData;

			IsFlag = data.IsFlag();
		}

		protected TemplateBase(string context) => Context = context;

		protected TemplateBase() { }

		protected string       Name       { get; }
		protected string       Context    { get; }
		protected MemberData[] MemberData { get; }
		protected bool         IsFlag     { get; }

		public virtual     string FileName      => Path.Combine(DirectoryName, Context, $"{ClassName}.cs");
		protected abstract string DirectoryName { get; }
		public abstract    string FileContent   { get; }
		protected abstract string ClassName     { get; }
		protected          string ComponentName => Name.ToComponentName(ignoreNamespaces: true);

		protected string MembersAs(Func<MemberData, string> format, string separator)
			=> string.Join(separator, MemberData.Select(format.Invoke).ToArray());

		protected static string Field(MemberData member)
			=> $"\t[SerializeField] private {member.type} _{member.GetCamelCaseName()};";
	}
}