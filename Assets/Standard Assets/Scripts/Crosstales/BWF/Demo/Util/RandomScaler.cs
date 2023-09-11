using System;
using UnityEngine;

namespace Crosstales.BWF.Demo.Util
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_demo_1_1_util_1_1_random_scaler.html")]
	public class RandomScaler : MonoBehaviour
	{
		public void Start()
		{
			this.tf = base.transform;
			this.elapsedTime = (this.changeTime = UnityEngine.Random.Range(this.ChangeInterval.x, this.ChangeInterval.y));
			this.startScale = this.tf.localScale;
		}

		public void Update()
		{
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime > this.changeTime)
			{
				if (this.Uniform)
				{
					this.endScale.x = (this.endScale.y = (this.endScale.z = UnityEngine.Random.Range(this.ScaleMin.x, Mathf.Abs(this.ScaleMax.x))));
				}
				else
				{
					this.endScale.x = UnityEngine.Random.Range(this.ScaleMin.x, Mathf.Abs(this.ScaleMax.x));
					this.endScale.y = UnityEngine.Random.Range(this.ScaleMin.y, Mathf.Abs(this.ScaleMax.y));
					this.endScale.z = UnityEngine.Random.Range(this.ScaleMin.z, Mathf.Abs(this.ScaleMax.z));
				}
				this.changeTime = UnityEngine.Random.Range(this.ChangeInterval.x, this.ChangeInterval.y);
				this.lerpTime = (this.elapsedTime = 0f);
			}
			this.tf.localScale = Vector3.Lerp(this.startScale, this.endScale, this.lerpTime);
			if (this.lerpTime < 1f)
			{
				this.lerpTime += Time.deltaTime / (this.changeTime - 0.2f);
			}
			else
			{
				this.startScale = this.tf.localScale;
			}
		}

		public Vector3 ScaleMin = Vector3.zero;

		public Vector3 ScaleMax = Vector3.one;

		public bool Uniform;

		public Vector2 ChangeInterval = new Vector2(10f, 45f);

		private Transform tf;

		private Vector3 endScale;

		private float elapsedTime;

		private float changeTime;

		private Vector3 startScale;

		private float lerpTime;
	}
}
