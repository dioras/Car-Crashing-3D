using System;

namespace Gaia.FullSerializer
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class fsPropertyAttribute : Attribute
	{
		public fsPropertyAttribute() : this(string.Empty)
		{
		}

		public fsPropertyAttribute(string name)
		{
			this.Name = name;
		}

		public string Name;
	}
}
