using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	[Serializable]
	public class SingleMeshes
	{
		public SingleMeshes(Transform t, Material mat, Mesh mesh)
		{
			this.mat = mat;
			this.meshes.Add(new MeshInfo(t, mesh));
		}

		public Material mat;

		public List<MeshInfo> meshes = new List<MeshInfo>();
	}
}
