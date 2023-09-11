using System;
using System.Collections.Generic;

namespace Gaia
{
	public class TemplateFrameVariable
	{
		public TemplateFrameVariable(List<int> indicies, List<int> positions)
		{
			this.Indicies = indicies;
			this.Positions = positions;
		}

		public List<int> Indicies;

		public List<int> Positions;
	}
}
