using System;
using UnityEngine;

namespace Crosstales.BWF.Demo.Util
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_demo_1_1_util_1_1_random_rotator.html")]
	public class RandomRotator : MonoBehaviour
	{
		public void Start()
		{
			this.tf = base.transform;
			this.elapsedTime = (this.changeTime = UnityEngine.Random.Range(this.ChangeInterval.x, this.ChangeInterval.y));
		}

		public void Update()
		{
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime > this.changeTime)
			{
				this.speed.x = UnityEngine.Random.Range(-Mathf.Abs(this.Speed.x), Mathf.Abs(this.Speed.x));
				this.speed.y = UnityEngine.Random.Range(-Mathf.Abs(this.Speed.y), Mathf.Abs(this.Speed.y));
				this.speed.z = UnityEngine.Random.Range(-Mathf.Abs(this.Speed.z), Mathf.Abs(this.Speed.z));
				this.changeTime = UnityEngine.Random.Range(this.ChangeInterval.x, this.ChangeInterval.y);
				this.elapsedTime = 0f;
			}
			this.tf.Rotate(this.speed.x * Time.deltaTime, this.speed.y * Time.deltaTime, this.speed.z * Time.deltaTime);
		}

		public Vector3 Speed;

		public Vector2 ChangeInterval = new Vector2(10f, 45f);

		private Transform tf;

		private Vector3 speed;

		private float elapsedTime;

		private float changeTime;
	}
}
