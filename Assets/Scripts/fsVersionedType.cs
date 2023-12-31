using System;

namespace Gaia.FullSerializer.Internal
{
	public struct fsVersionedType
	{
		public object Migrate(object ancestorInstance)
		{
			return Activator.CreateInstance(this.ModelType, new object[]
			{
				ancestorInstance
			});
		}

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"fsVersionedType [ModelType=",
				this.ModelType,
				", VersionString=",
				this.VersionString,
				", Ancestors.Length=",
				this.Ancestors.Length,
				"]"
			});
		}

		public static bool operator ==(fsVersionedType a, fsVersionedType b)
		{
			return a.ModelType == b.ModelType;
		}

		public static bool operator !=(fsVersionedType a, fsVersionedType b)
		{
			return a.ModelType != b.ModelType;
		}

		public override bool Equals(object obj)
		{
			return obj is fsVersionedType && this.ModelType == ((fsVersionedType)obj).ModelType;
		}

		public override int GetHashCode()
		{
			return this.ModelType.GetHashCode();
		}

		public fsVersionedType[] Ancestors;

		public string VersionString;

		public Type ModelType;
	}
}
