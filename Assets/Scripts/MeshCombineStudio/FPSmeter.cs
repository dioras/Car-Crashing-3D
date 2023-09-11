using System;
using UnityEngine;

namespace MeshCombineStudio
{
	public class FPSmeter : MonoBehaviour
	{
		private void OnGUI()
		{
			if (this.showFPS)
			{
				GUI.color = Color.red;
				GUI.Label(new Rect((float)(Screen.width - 75), 10f, 150f, 20f), "FPS " + (Mathf.Round(FPSmeter.fps * 100f) / 100f).ToString("F0"));
				GUI.color = Color.white;
			}
		}

		private void Update()
		{
			this.timeNow = Time.realtimeSinceStartup;
			this.frames++;
			if (this.timeNow > this.lastInterval + this.updateInterval)
			{
				FPSmeter.fps = (float)this.frames / (this.timeNow - this.lastInterval);
				this.frames = 0;
				this.lastInterval = this.timeNow;
			}
		}

		public float updateInterval = 0.5f;

		private float lastInterval;

		private int frames;

		public static float fps;

		public bool showFPS = true;

		private float timeNow;
	}
}
