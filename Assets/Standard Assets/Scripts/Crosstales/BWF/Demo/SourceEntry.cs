using System;
using Crosstales.BWF.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.BWF.Demo
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_demo_1_1_source_entry.html")]
	public class SourceEntry : MonoBehaviour
	{
		public void Start()
		{
			this.disabledColor = this.Main.color;
		}

		public void Update()
		{
			this.Text.text = this.Source.Name;
			this.Icon.sprite = this.Source.Icon;
			if (this.GuiMain.Sources.Contains(this.Source.Name))
			{
				this.Main.color = this.EnabledColor;
			}
			else
			{
				this.Main.color = this.disabledColor;
			}
		}

		public void Click()
		{
			if (this.GuiMain.Sources.Contains(this.Source.Name))
			{
				this.GuiMain.Sources.Remove(this.Source.Name);
			}
			else
			{
				this.GuiMain.Sources.Add(this.Source.Name);
			}
		}

		public Text Text;

		public Image Icon;

		public Image Main;

		public Source Source;

		public GUIMain GuiMain;

		public Color32 EnabledColor = new Color32(0, byte.MaxValue, 0, 192);

		private Color32 disabledColor;
	}
}
