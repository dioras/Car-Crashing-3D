using System;
using UnityEngine;

namespace Battlehub.MeshTools
{
	public class CombineResult
	{
		public CombineResult(GameObject gameObject, Mesh mesh)
		{
			this.GameObject = gameObject;
			this.Mesh = mesh;
		}

		public GameObject GameObject;

		public Mesh Mesh;
	}
}
