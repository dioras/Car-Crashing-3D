using System;
using UnityEngine;

namespace MeshCombineStudio
{
	public class DirectDraw : MonoBehaviour
	{
		private void Awake()
		{
			this.mrs = base.GetComponentsInChildren<MeshRenderer>(false);
			this.SetMeshRenderersEnabled(false);
			this.meshes = new Mesh[this.mrs.Length];
			this.mats = new Material[this.mrs.Length];
			this.positions = new Vector3[this.mrs.Length];
			this.rotations = new Quaternion[this.mrs.Length];
			for (int i = 0; i < this.mrs.Length; i++)
			{
				MeshFilter component = this.mrs[i].GetComponent<MeshFilter>();
				this.meshes[i] = component.sharedMesh;
				this.mats[i] = this.mrs[i].sharedMaterial;
				this.positions[i] = this.mrs[i].transform.position;
				this.rotations[i] = this.mrs[i].transform.rotation;
			}
		}

		private void SetMeshRenderersEnabled(bool enabled)
		{
			for (int i = 0; i < this.mrs.Length; i++)
			{
				this.mrs[i].enabled = enabled;
			}
		}

		private void Update()
		{
			for (int i = 0; i < this.mrs.Length; i++)
			{
				Graphics.DrawMesh(this.meshes[i], this.positions[i], this.rotations[i], this.mats[i], 0);
			}
		}

		private MeshRenderer[] mrs;

		private Mesh[] meshes;

		private Material[] mats;

		private Vector3[] positions;

		private Quaternion[] rotations;
	}
}
