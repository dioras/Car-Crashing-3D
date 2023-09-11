using System;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.UI.Util
{
	public class FPSDisplay : MonoBehaviour
	{
		public void Update()
		{
			this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime > 1f)
			{
				if (Time.frameCount % 3 == 0 && this.FPS != null)
				{
					this.msec = this.deltaTime * 1000f;
					this.fps = 1f / this.deltaTime;
					this.text = string.Format("<b>FPS: {0:0.}</b> ({1:0.0} ms)", this.fps, this.msec);
					if (this.fps < 15f)
					{
						this.FPS.text = "<color=red>" + this.text + "</color>";
					}
					else if (this.fps < 29f)
					{
						this.FPS.text = "<color=orange>" + this.text + "</color>";
					}
					else
					{
						this.FPS.text = "<color=green>" + this.text + "</color>";
					}
				}
			}
			else
			{
				this.FPS.text = "<i>...calculating <b>FPS</b>...</i>";
			}
		}

		public Text FPS;

		private float deltaTime;

		private float elapsedTime;

		private float msec;

		private float fps;

		private string text;
	}
}
