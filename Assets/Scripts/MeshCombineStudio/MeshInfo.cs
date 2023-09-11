using System;
using UnityEngine;

namespace MeshCombineStudio
{
	[Serializable]
	public class MeshInfo
	{
		public MeshInfo(Transform t, Mesh mesh)
		{
			this.t = t;
			this.mesh = mesh;
		}

		public Transform t;

		public Mesh mesh;
	}
}
