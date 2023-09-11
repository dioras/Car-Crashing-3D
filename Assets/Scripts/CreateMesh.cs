using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
	[ContextMenu("Create plane")]
	private void CreatePlane()
	{
		Mesh mesh = new Mesh();
		MeshFilter meshFilter = base.GetComponent<MeshFilter>();
		if (meshFilter == null)
		{
			meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		meshFilter.mesh = mesh;
		MeshRenderer x = base.GetComponent<MeshRenderer>();
		if (x == null)
		{
			x = base.gameObject.AddComponent<MeshRenderer>();
		}
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		List<Vector3> list3 = new List<Vector3>();
		List<Vector2> list4 = new List<Vector2>();
		int num = (int)this.meshWidth / this.tileSize;
		int num2 = (int)this.meshHeight / this.tileSize;
		float num3 = this.meshWidth % (float)this.tileSize;
		float num4 = this.meshHeight % (float)this.tileSize;
		int num5 = (num3 != 0f) ? 1 : 0;
		int num6 = (num4 != 0f) ? 1 : 0;
		int index = 0;
		for (int i = 0; i < num + num5; i++)
		{
			for (int j = 0; j < num2 + num6; j++)
			{
				float currentTileWidth = (float)this.tileSize;
				float currentTileHeight = (float)this.tileSize;
				if (i == num)
				{
					currentTileWidth = num3;
				}
				if (j == num2)
				{
					currentTileHeight = num4;
				}
				this.AddVertices(i, j, list, currentTileWidth, currentTileHeight);
				index = this.AddTriangles(index, list2);
				this.AddNormals(list3);
				this.AddUvs(i, j, currentTileWidth, currentTileHeight, list4);
			}
		}
		mesh.vertices = list.ToArray();
		mesh.normals = list3.ToArray();
		mesh.triangles = list2.ToArray();
		mesh.uv = list4.ToArray();
		mesh.RecalculateNormals();
	}

	private void AddVertices(int x, int y, List<Vector3> vertices, float currentTileWidth, float currentTileHeight)
	{
		vertices.Add(new Vector3((float)(x * this.tileSize), 0f, (float)(y * this.tileSize)));
		vertices.Add(new Vector3((float)(x * this.tileSize) + currentTileWidth, 0f, (float)(y * this.tileSize)));
		vertices.Add(new Vector3((float)(x * this.tileSize) + currentTileWidth, 0f, (float)(y * this.tileSize) + currentTileHeight));
		vertices.Add(new Vector3((float)(x * this.tileSize), 0f, (float)(y * this.tileSize) + currentTileHeight));
	}

	private int AddTriangles(int index, List<int> triangles)
	{
		triangles.Add(index + 2);
		triangles.Add(index + 1);
		triangles.Add(index);
		triangles.Add(index);
		triangles.Add(index + 3);
		triangles.Add(index + 2);
		index += 4;
		return index;
	}

	private void AddNormals(List<Vector3> normals)
	{
		normals.Add(Vector3.forward);
		normals.Add(Vector3.forward);
		normals.Add(Vector3.forward);
		normals.Add(Vector3.forward);
	}

	private void AddUvs(int x, int y, float currentTileWidth, float currentTileHeight, List<Vector2> uvs)
	{
		float num = (float)x * 0.01f * (float)this.tileSize;
		float num2 = (float)y * 0.01f * (float)this.tileSize;
		float num3 = 0.01f * (currentTileWidth / (float)this.tileSize) * (float)this.tileSize;
		float num4 = 0.01f * (currentTileHeight / (float)this.tileSize) * (float)this.tileSize;
		uvs.Add(new Vector2(num, num2));
		uvs.Add(new Vector2(num + num3, num2));
		uvs.Add(new Vector2(num + num3, num2 + num4));
		uvs.Add(new Vector2(num, num2 + num4));
	}

	public int tileSize = 10;

	public float meshWidth = 50f;

	public float meshHeight = 50f;
}
