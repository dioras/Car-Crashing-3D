using System;
using UnityEngine;

namespace Gaia
{
	public class PreviewTextureAttribute : PropertyAttribute
	{
		public PreviewTextureAttribute()
		{
		}

		public PreviewTextureAttribute(int expire)
		{
			this.m_expire = (long)(expire * 1000 * 10000);
		}

		public PreviewTextureAttribute(float offset, float width)
		{
			this.m_offset = offset;
			this.m_width = width;
		}

		public Rect m_lastPosition = new Rect(0f, 0f, 0f, 0f);

		public long m_expire = 6000000000L;

		public WWW m_www;

		public Texture2D m_cached;

		public float m_width = 1f;

		public float m_offset;
	}
}
