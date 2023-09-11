using System;
using System.Collections.Generic;
using System.Text;

namespace Gaia
{
	public class Template
	{
		public Template(string filePath, bool debug)
		{
			this.FilePath = filePath;
			if (this.Frame == null && !this.findPreviouslyBuiltFrame())
			{
				this.Frame = new TemplateFrame(filePath, debug);
			}
			this.Variables = new Dictionary<string, TemplateValue>();
		}

		private bool findPreviouslyBuiltFrame()
		{
			foreach (TemplateFrame templateFrame in TemplateFrames.List)
			{
				if (templateFrame.FilePath == this.FilePath)
				{
					this.Frame = templateFrame;
					return true;
				}
			}
			return false;
		}

		public void Set(string name, string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (this.Variables.ContainsKey(name))
			{
				this.Variables[name].Value = value;
			}
			else
			{
				this.Variables.Add(name, new TemplateValue(value, this.Frame.Variables[name]));
			}
		}

		private int[] CopyIndicies(TemplateFrameVariable tfv)
		{
			int[] array = new int[tfv.Indicies.Count];
			tfv.Indicies.CopyTo(array);
			return array;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Frame.Text);
			foreach (TemplateValue templateValue in this.Variables.Values)
			{
				templateValue.Indicies = this.CopyIndicies(templateValue.FrameVar);
			}
			foreach (TemplateValue templateValue2 in this.Variables.Values)
			{
				for (int i = 0; i < templateValue2.Indicies.Length; i++)
				{
					stringBuilder.Insert(templateValue2.Indicies[i], templateValue2.Value);
					this.UpdateIndicies(templateValue2.FrameVar.Positions[i], templateValue2.Value.Length);
				}
			}
			return stringBuilder.ToString();
		}

		private void UpdateIndicies(int position, int length)
		{
			foreach (TemplateValue templateValue in this.Variables.Values)
			{
				for (int i = 0; i < templateValue.FrameVar.Positions.Count; i++)
				{
					if (templateValue.FrameVar.Positions[i] > position)
					{
						templateValue.Indicies[i] = templateValue.Indicies[i] + length;
					}
				}
			}
		}

		public string FilePath;

		public TemplateFrame Frame;

		private Dictionary<string, TemplateValue> Variables;
	}
}
