using System;
using Crosstales.BWF.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.BWF.Demo.Util
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_demo_1_1_util_1_1_scroll_rect_handler.html")]
	public class ScrollRectHandler : MonoBehaviour
	{
		public void Start()
		{
			if (Helper.isWindowsPlatform)
			{
				this.Scroll.scrollSensitivity = this.WindowsSensitivity;
			}
			else if (Helper.isMacOSPlatform)
			{
				this.Scroll.scrollSensitivity = this.MacSensitivity;
			}
		}

		public ScrollRect Scroll;

		private float WindowsSensitivity = 35f;

		private float MacSensitivity = 25f;
	}
}
