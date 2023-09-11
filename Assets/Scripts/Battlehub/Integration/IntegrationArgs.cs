using System;
using UnityEngine;

namespace Battlehub.Integration
{
	public class IntegrationArgs
	{
		public IntegrationArgs()
		{
		}

		public IntegrationArgs(GameObject gameObject)
		{
			this.GameObject = gameObject;
		}

		public IntegrationArgs(GameObject gameObject, Mesh mesh)
		{
			this.GameObject = gameObject;
			this.Mesh = mesh;
		}

		public GameObject GameObject;

		public Mesh Mesh;

		public bool Cancel;
	}
}
