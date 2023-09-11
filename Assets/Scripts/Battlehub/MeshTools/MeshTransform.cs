using System;
using UnityEngine;

namespace Battlehub.MeshTools
{
	public class MeshTransform
	{
		public MeshTransform(Mesh mesh, Matrix4x4 transform)
		{
			this.Mesh = mesh;
			this.Transform = transform;
		}

		public Mesh Mesh;

		public Matrix4x4 Transform;
	}
}
