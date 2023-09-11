using System;
using System.Collections.Generic;
using UnityEngine;

public class MudTest : MonoBehaviour
{
	[ContextMenu("Lower")]
	private void LowerTerrain()
	{
		Terrain component = base.GetComponent<Terrain>();
		TerrainData terrainData = component.terrainData;
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
		for (int i = 0; i < terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrainData.heightmapResolution; j++)
			{
				if (i > this.xStart && i < this.xEnd && j > this.yStart && j < this.yEnd)
				{
					heights[j, i] -= this.amount;
				}
			}
		}
		terrainData.SetHeights(0, 0, heights);
	}

	[ContextMenu("Show verts")]
	private void ShowVerts()
	{
		Terrain component = base.GetComponent<Terrain>();
		TerrainData terrainData = component.terrainData;
		for (int i = 0; i < terrainData.heightmapResolution; i++)
		{
			for (int j = 0; j < terrainData.heightmapResolution; j++)
			{
				Vector3 vector = component.transform.position + new Vector3((float)i / (float)(terrainData.heightmapResolution - 1) * terrainData.size.x, 0f, (float)j / (float)(terrainData.heightmapResolution - 1) * terrainData.size.z);
				vector.y = component.SampleHeight(vector);
				UnityEngine.Debug.DrawRay(vector, Vector3.up, Color.red, 5f);
			}
		}
	}

	[ContextMenu("Create plane")]
	private void CreatePlane()
	{
		Terrain component = base.GetComponent<Terrain>();
		TerrainData terrainData = component.terrainData;
		Mesh mesh = new Mesh();
		if (this.plane == null)
		{
			this.plane = new GameObject("MudPlane");
		}
		int num = this.xStart - 1;
		int num2 = this.xEnd + 1;
		int num3 = this.yStart - 1;
		int num4 = this.yEnd + 1;
		this.meshWidth = (float)(num2 - num) / (float)terrainData.heightmapResolution * terrainData.size.x;
		this.meshHeight = (float)(num4 - num3) / (float)terrainData.heightmapResolution * terrainData.size.z;
		Vector3 vector = component.transform.position + new Vector3((float)num / (float)(terrainData.heightmapResolution - 1) * terrainData.size.x, 0f, (float)num3 / (float)(terrainData.heightmapResolution - 1) * terrainData.size.z);
		vector.y = component.SampleHeight(vector);
		this.plane.transform.position = vector;
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		List<Vector3> list3 = new List<Vector3>();
		List<Vector2> list4 = new List<Vector2>();
		int num5 = (int)(this.meshWidth / this.tileSize);
		int num6 = (int)(this.meshHeight / this.tileSize);
		float num7 = this.meshWidth % this.tileSize;
		float num8 = this.meshHeight % this.tileSize;
		int num9 = (num7 != 0f) ? 1 : 0;
		int num10 = (num8 != 0f) ? 1 : 0;
		int[] array = new int[(num5 + num9) * (num6 + num10) * 2 * 3];
		Vector3[] array2 = new Vector3[(num5 + num9) * (num6 + num10)];
		int num11 = 0;
		for (int i = 0; i < num5 + num9; i++)
		{
			for (int j = 0; j < num6 + num10; j++)
			{
				float num12 = this.tileSize;
				float num13 = this.tileSize;
				if (i == num5)
				{
				}
				if (j == num6)
				{
				}
				array2[i * num6 + num10 + j] = new Vector3((float)(terrainData.heightmapResolution / (num5 + num9 - 1) * j), 0f, (float)(terrainData.heightmapResolution / (num6 + num10 - 1) * i));
				if (i > 0 && j > 0 && i < num5 + num9 - 1 && j < num6 + num10 - 1)
				{
					array[num11] = i * num6 + num10 + j - 1;
					array[num11 + 1] = (i - 1) * num6 + num10 + j;
					array[num11 + 2] = (i - 1) * num6 + num10 + j - 1;
					num11 += 3;
					array[num11] = i * num6 + num10 + j;
					array[num11 + 1] = (i - 1) * num6 + num10 + j;
					array[num11 + 2] = i * num6 + num10 + j - 1;
					num11 += 3;
				}
			}
		}
		for (int k = 0; k < list.Count; k++)
		{
			Vector3 vector2 = this.plane.transform.TransformPoint(list[k]);
			vector2.y = component.SampleHeight(vector2);
			list[k] = this.plane.transform.InverseTransformPoint(vector2);
		}
		mesh.vertices = array2;
		mesh.triangles = array;
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		mesh.RecalculateBounds();
		MeshFilter meshFilter = this.plane.GetComponent<MeshFilter>();
		if (meshFilter == null)
		{
			meshFilter = this.plane.AddComponent<MeshFilter>();
		}
		meshFilter.mesh = mesh;
		MeshRenderer meshRenderer = this.plane.GetComponent<MeshRenderer>();
		if (meshRenderer == null)
		{
			meshRenderer = this.plane.AddComponent<MeshRenderer>();
		}
		meshRenderer.sharedMaterial = this.planeMat;
		this.plane.transform.position -= Vector3.up * 0.1f;
	}

	private void AddVertices(int x, int y, List<Vector3> vertices, float currentTileWidth, float currentTileHeight)
	{
		vertices.Add(new Vector3((float)x * this.tileSize, 0f, (float)y * this.tileSize));
		vertices.Add(new Vector3((float)x * this.tileSize + currentTileWidth, 0f, (float)y * this.tileSize));
		vertices.Add(new Vector3((float)x * this.tileSize + currentTileWidth, 0f, (float)y * this.tileSize + currentTileHeight));
		vertices.Add(new Vector3((float)x * this.tileSize, 0f, (float)y * this.tileSize + currentTileHeight));
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
		float num = (float)x * 0.01f * this.tileSize;
		float num2 = (float)y * 0.01f * this.tileSize;
		float num3 = 0.01f * (currentTileWidth / this.tileSize) * this.tileSize;
		float num4 = 0.01f * (currentTileHeight / this.tileSize) * this.tileSize;
		uvs.Add(new Vector2(num, num2));
		uvs.Add(new Vector2(num + num3, num2));
		uvs.Add(new Vector2(num + num3, num2 + num4));
		uvs.Add(new Vector2(num, num2 + num4));
	}

	public int xStart;

	public int xEnd;

	public int yStart;

	public int yEnd;

	public float amount;

	public float tileSize = 10f;

	public float meshWidth = 50f;

	public float meshHeight = 50f;

	public Material planeMat;

	public GameObject plane;
}
