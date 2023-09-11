using System;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Demo.Util
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_demo_1_1_util_1_1_random_color.html")]
	public class RandomColor : MonoBehaviour
	{
		public void Start()
		{
			this.currentRenderer = base.GetComponent<Renderer>();
			this.elapsedTime = (this.changeTime = UnityEngine.Random.Range(this.ChangeInterval.x, this.ChangeInterval.y));
			this.startColor = this.currentRenderer.material.color;
		}

		public void Update()
		{
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime > this.changeTime)
			{
				this.endColor = Helper.HSVToRGB(UnityEngine.Random.Range(0f, 360f), 1f, 1f, 1f);
				this.changeTime = UnityEngine.Random.Range(this.ChangeInterval.x, this.ChangeInterval.y);
				this.lerpTime = (this.elapsedTime = 0f);
			}
			this.currentRenderer.material.color = Color.Lerp(this.startColor, this.endColor, this.lerpTime);
			if (this.lerpTime < 1f)
			{
				this.lerpTime += Time.deltaTime / (this.changeTime - 0.2f);
			}
			else
			{
				this.startColor = this.currentRenderer.material.color;
			}
		}

		public Vector2 ChangeInterval = new Vector2(5f, 15f);

		private float elapsedTime;

		private float changeTime;

		private Renderer currentRenderer;

		private Color32 startColor;

		private Color32 endColor;

		private float lerpTime;
	}
}
