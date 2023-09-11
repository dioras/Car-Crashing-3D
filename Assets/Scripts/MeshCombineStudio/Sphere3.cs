using System;
using UnityEngine;

namespace MeshCombineStudio
{
	public class Sphere3
	{
		public Sphere3()
		{
		}

		public Sphere3(Vector3 center, float radius)
		{
			this.center = center;
			this.radius = radius;
		}

		public Vector3 center;

		public float radius;
	}
}
