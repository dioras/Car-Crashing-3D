using System;
using UnityEngine;

namespace MeshCombineStudio
{
	[ExecuteInEditMode]
	public class GarbageCollectMesh : MonoBehaviour
	{
		private void Awake()
		{
			MeshFilter component = base.GetComponent<MeshFilter>();
			if (component != null)
			{
				this.mesh = component.sharedMesh;
			}
			else
			{
				UnityEngine.Debug.Log("MeshFilter = null");
			}
		}

		private void OnDestroy()
		{
			if (this.mesh != null)
			{
				UnityEngine.Object.Destroy(this.mesh);
			}
		}

		public Mesh mesh;
	}
}
