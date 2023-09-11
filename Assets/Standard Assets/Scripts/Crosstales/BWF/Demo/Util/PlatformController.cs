using System;
using System.Collections.Generic;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Demo.Util
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_demo_1_1_util_1_1_platform_controller.html")]
	public class PlatformController : MonoBehaviour
	{
		public void Start()
		{
			if (Helper.isWindowsPlatform)
			{
				this.currentPlatform = Platform.Windows;
			}
			else if (Helper.isMacOSPlatform)
			{
				this.currentPlatform = Platform.OSX;
			}
			else if (Helper.isAndroidPlatform)
			{
				this.currentPlatform = Platform.Android;
			}
			else if (Helper.isIOSPlatform)
			{
				this.currentPlatform = Platform.IOS;
			}
			else if (Helper.isWSAPlatform)
			{
				this.currentPlatform = Platform.WSA;
			}
			else if (Helper.isWebPlatform)
			{
				this.currentPlatform = Platform.Web;
			}
			else
			{
				this.currentPlatform = Platform.Unsupported;
			}
			bool active = (!this.Platforms.Contains(this.currentPlatform)) ? (!this.Active) : this.Active;
			foreach (GameObject gameObject in this.Objects)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(active);
				}
			}
		}

		[Header("Configuration")]
		[Tooltip("Selected platforms for the controller.")]
		public List<Platform> Platforms;

		[Tooltip("Enable or disable the 'Objects' for the selected 'Platforms' (default: true).")]
		public bool Active = true;

		[Header("Objects")]
		[Tooltip("Selected objects for the controller.")]
		public GameObject[] Objects;

		private Platform currentPlatform;
	}
}
