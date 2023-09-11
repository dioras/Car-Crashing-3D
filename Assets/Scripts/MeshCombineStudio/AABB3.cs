using System;
using UnityEngine;

namespace MeshCombineStudio
{
	public class AABB3
	{
		public AABB3(Vector3 min, Vector3 max)
		{
			this.min = min;
			this.max = max;
		}

		public Vector3 min;

		public Vector3 max;
	}
}
