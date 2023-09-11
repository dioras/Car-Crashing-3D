using System;
using UnityEngine;

namespace Gaia
{
	[Serializable]
	public class ResourceProtoDNA
	{
		public void Update(int protoIdx)
		{
			this.m_protoIdx = protoIdx;
		}

		public void Update(int protoIdx, float width, float height)
		{
			this.m_protoIdx = protoIdx;
			this.m_width = width;
			this.m_height = height;
			this.m_boundsRadius = this.m_width;
			this.m_seedThrow = Mathf.Max(this.m_width, this.m_height) * 1.5f;
		}

		public void Update(int protoIdx, float width, float height, float minscale, float maxscale)
		{
			this.m_protoIdx = protoIdx;
			this.m_width = width;
			this.m_height = height;
			this.m_boundsRadius = this.m_width;
			this.m_seedThrow = Mathf.Max(this.m_width, this.m_height) * 3f;
			this.m_minScale = minscale;
			this.m_maxScale = maxscale;
		}

		public float GetScale(float fitness)
		{
			return this.m_minScale + (this.m_maxScale - this.m_minScale) * fitness;
		}

		public float GetScale(float fitness, float random)
		{
			return this.m_minScale + (this.m_maxScale - this.m_minScale) * fitness * random;
		}

		[Tooltip("Width in world units.")]
		public float m_width = 1f;

		[Tooltip("Height in world units.")]
		public float m_height = 1f;

		[Tooltip("Radius from centre of object in world units for bounded area checks. Make this larger if you want more free space around your object when it is spawned.")]
		public float m_boundsRadius = 1f;

		[Tooltip("The maximum distance a seed can be thrown when a new instance is spawned. Used to control spread area random clustered spawning.")]
		public float m_seedThrow = 12f;

		[Tooltip("The minimum scale that this can be rendered into the world. For terrain details this is the minimum strength that detail will render at.")]
		public float m_minScale = 0.5f;

		[Tooltip("The maximum scale that this can be rendered into the world. For terrain details this is the maximum strength that detail will render at.")]
		public float m_maxScale = 1.5f;

		[Tooltip("Randomise the scale somewhere between minimum and maximum scale. If not selected then the scale will be proportionally influenced by the locations fitness.")]
		public bool m_rndScaleInfluence;

		[Tooltip("Custom parameter to be interpreted by an extension if there is one. Use 'nograss' to exclude grass being grown within the volumne covered by the area bounds.")]
		public string m_extParam = string.Empty;

		[HideInInspector]
		public int m_protoIdx;
	}
}
