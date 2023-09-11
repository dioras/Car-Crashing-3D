using System;
using UnityEngine;

[ExecuteInEditMode]
public class ListMeshVertCount : MonoBehaviour
{
	private void OnEnable()
	{
		MeshFilter[] componentsInChildren = base.GetComponentsInChildren<MeshFilter>(true);
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Mesh sharedMesh = componentsInChildren[i].sharedMesh;
			if (!(sharedMesh == null))
			{
				num += sharedMesh.vertexCount;
				num2 += sharedMesh.triangles.Length;
			}
		}
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			base.gameObject.name,
			" Vertices ",
			num,
			"  Triangles ",
			num2
		}));
	}
}
