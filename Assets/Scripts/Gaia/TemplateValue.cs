using System;

namespace Gaia
{
	public class TemplateValue
	{
		public TemplateValue(string value, TemplateFrameVariable fv)
		{
			this.Value = value;
			this.FrameVar = fv;
		}

		public string Value;

		public int[] Indicies;

		public readonly TemplateFrameVariable FrameVar;
	}
}
