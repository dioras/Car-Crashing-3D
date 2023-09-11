using System;

namespace Gaia.FullSerializer
{
	public abstract class fsDirectConverter : fsBaseConverter
	{
		public abstract Type ModelType { get; }
	}
}
