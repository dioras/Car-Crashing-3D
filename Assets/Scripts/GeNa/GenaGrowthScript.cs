using System;
using System.Collections;
using UnityEngine;

namespace Gena
{
	public class GenaGrowthScript : MonoBehaviour
	{
		private void Start()
		{
			this.Initialise();
		}

		public virtual void Initialise()
		{
			this.m_actualEndScale = this.m_endScale + UnityEngine.Random.Range(0f, this.m_scaleVariance);
			base.StartCoroutine(this.Grow());
		}

		private IEnumerator Grow()
		{
			float startTime = Time.realtimeSinceStartup;
			float currentTime = startTime;
			float deltaScale = this.m_actualEndScale - this.m_startScale;
			float finishTime = startTime + this.m_growthTime;
			while (currentTime < finishTime)
			{
				float scale = 1f - (finishTime - currentTime) / this.m_growthTime;
				scale = this.m_startScale + scale * deltaScale;
				base.gameObject.transform.localScale = Vector3.one * scale;
				yield return null;
				currentTime = Time.realtimeSinceStartup;
			}
			if (this.m_lifeTime > 0f)
			{
				yield return new WaitForSeconds(this.m_lifeTime);
			}
			if (this.m_destroyObjectAtEndOfLife)
			{
				this.Die();
			}
			else if (this.m_disableScriptAtEndOfLife)
			{
				base.enabled = false;
			}
			yield break;
		}

		public virtual void Die()
		{
			UnityEngine.Object.Destroy(base.gameObject, 0.25f);
		}

		[Range(0.1f, 2f)]
		[Tooltip("The start scale in the game.")]
		public float m_startScale = 0.15f;

		[Range(0.1f, 2f)]
		[Tooltip("The end scale in the game.")]
		public float m_endScale = 1f;

		[Range(0f, 2f)]
		[Tooltip("Scale variance. Final scale is equal to end scale plus a a random value between 0 and this.")]
		public float m_scaleVariance = 0.25f;

		[Tooltip("The time it takes to grow in seconds.")]
		public float m_growthTime = 5f;

		[Tooltip("The time the object will live for after it has finished growing in seconds.")]
		public float m_lifeTime = 30f;

		[Tooltip("Disable the script at the end.")]
		public bool m_disableScriptAtEndOfLife = true;

		[Tooltip("Destroy the object at the end of its living time.")]
		public bool m_destroyObjectAtEndOfLife;

		private float m_actualEndScale;
	}
}
