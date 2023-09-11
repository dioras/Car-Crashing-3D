using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshCombineStudio
{
	public class RemoveGeometryBelowTerrain : MonoBehaviour
	{
		private void Start()
		{
			if (this.runOnStart)
			{
				this.Remove(base.gameObject);
			}
		}

		public void Remove(GameObject go)
		{
			MeshFilter[] componentsInChildren = go.GetComponentsInChildren<MeshFilter>(true);
			this.totalTriangles = 0;
			this.removeTriangles = 0;
			this.skippedObjects = 0;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.RemoveMesh(componentsInChildren[i].transform, componentsInChildren[i].mesh);
			}
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Removeable ",
				this.removeTriangles,
				" total ",
				this.totalTriangles,
				" improvement ",
				((float)this.removeTriangles / (float)this.totalTriangles * 100f).ToString("F2")
			}));
			UnityEngine.Debug.Log("Skipped Objects " + this.skippedObjects);
		}

		public void RemoveMesh(Transform t, Mesh mesh)
		{
			if (mesh == null)
			{
				return;
			}
			if (!this.IsMeshUnderTerrain(t, mesh))
			{
				this.skippedObjects++;
				return;
			}
			Vector3[] vertices = mesh.vertices;
			List<int> list = new List<int>();
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				list.AddRange(mesh.GetTriangles(i));
				int count = list.Count;
				this.RemoveTriangles(t, list, vertices);
				if (list.Count < count)
				{
					mesh.SetTriangles(list.ToArray(), i);
				}
			}
		}

		public bool IsMeshUnderTerrain(Transform t, Mesh mesh)
		{
			Bounds bounds = mesh.bounds;
			bounds.center += t.position;
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			Vector3 center = bounds.center;
			Vector2 vector = new Vector2(max.x - min.x, max.z - min.z);
			for (float num = 0f; num < 1f; num += 0.125f)
			{
				for (float num2 = 0f; num2 < 1f; num2 += 0.125f)
				{
					Vector3 vector2 = new Vector3(min.x + num2 * vector.x, min.y, min.z + num * vector.y);
					float num3 = 0f;
					if (vector2.y < num3)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void GetTerrainComponents()
		{
			this.terrainComponents = new Terrain[this.terrains.Count];
			for (int i = 0; i < this.terrains.Count; i++)
			{
				Terrain component = this.terrains[i].GetComponent<Terrain>();
				this.terrainComponents[i] = component;
			}
		}

		public void GetMeshRenderersAndComponents()
		{
			this.mrs = new MeshRenderer[this.meshTerrains.Count];
			this.meshTerrainComponents = new Mesh[this.meshTerrains.Count];
			for (int i = 0; i < this.meshTerrains.Count; i++)
			{
				this.mrs[i] = this.meshTerrains[i].GetComponent<MeshRenderer>();
				MeshFilter component = this.meshTerrains[i].GetComponent<MeshFilter>();
				this.meshTerrainComponents[i] = component.sharedMesh;
			}
		}

		public void CreateTerrainBounds()
		{
			this.terrainBounds = new Bounds[this.terrainComponents.Length];
			for (int i = 0; i < this.terrainBounds.Length; i++)
			{
				this.terrainBounds[i] = default(Bounds);
				this.terrainBounds[i].min = this.terrains[i].position;
				this.terrainBounds[i].max = this.terrainBounds[i].min + this.terrainComponents[i].terrainData.size;
			}
			this.meshBounds = new Bounds[this.meshTerrains.Count];
			for (int j = 0; j < this.meshTerrains.Count; j++)
			{
				this.meshBounds[j] = this.mrs[j].bounds;
			}
		}

		public void MakeIntersectLists(Bounds bounds)
		{
			List<Terrain> list = new List<Terrain>();
			List<Mesh> list2 = new List<Mesh>();
			List<Bounds> list3 = new List<Bounds>();
			List<Bounds> list4 = new List<Bounds>();
			Vector3[] array = new Vector3[8];
			Vector3 size = bounds.size;
			array[0] = bounds.min;
			array[1] = array[0] + new Vector3(size.x, 0f, 0f);
			array[2] = array[0] + new Vector3(0f, 0f, size.z);
			array[3] = array[0] + new Vector3(size.x, 0f, size.z);
			array[4] = array[0] + new Vector3(0f, size.y, 0f);
			array[5] = array[0] + new Vector3(size.x, size.y, 0f);
			array[6] = array[0] + new Vector3(0f, size.y, size.z);
			array[7] = array[0] + size;
			for (int i = 0; i < 8; i++)
			{
				int num = this.InterectTerrain(array[i]);
				if (num != -1)
				{
					list.Add(this.terrainArray[num]);
					list3.Add(this.terrainBounds[num]);
				}
				num = this.InterectMesh(array[i]);
				if (num != -1)
				{
					list2.Add(this.meshArray[num]);
					list4.Add(this.meshBounds[num]);
				}
			}
			this.terrainArray = list.ToArray();
			this.meshArray = list2.ToArray();
			this.terrainBoundsArray = list3.ToArray();
		}

		public int InterectTerrain(Vector3 pos)
		{
			for (int i = 0; i < this.terrainBounds.Length; i++)
			{
				if (this.terrainBounds[i].Contains(pos))
				{
					return i;
				}
			}
			return -1;
		}

		public int InterectMesh(Vector3 pos)
		{
			for (int i = 0; i < this.meshBounds.Length; i++)
			{
				if (this.meshBounds[i].Contains(pos))
				{
					return i;
				}
			}
			return -1;
		}

		public float GetTerrainHeight(Vector3 pos)
		{
			int num = -1;
			for (int i = 0; i < this.terrainArray.Length; i++)
			{
				if (this.terrainBoundsArray[i].Contains(pos))
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				return this.terrainArray[num].SampleHeight(pos);
			}
			return float.PositiveInfinity;
		}

		public void RemoveTriangles(Transform t, List<int> newTriangles, Vector3[] vertices)
		{
			bool[] array = new bool[vertices.Length];
			Vector3 pos = Vector3.zero;
			for (int i = 0; i < newTriangles.Count; i += 3)
			{
				this.totalTriangles++;
				int num = newTriangles[i];
				bool flag = array[num];
				if (!flag)
				{
					pos = t.TransformPoint(vertices[num]);
					float terrainHeight = this.GetTerrainHeight(pos);
					flag = (pos.y < terrainHeight);
				}
				if (flag)
				{
					array[num] = true;
					num = newTriangles[i + 1];
					flag = array[num];
					if (!flag)
					{
						pos = t.TransformPoint(vertices[num]);
						float terrainHeight = this.GetTerrainHeight(pos);
						flag = (pos.y < terrainHeight);
					}
					if (flag)
					{
						array[num] = true;
						num = newTriangles[i + 2];
						flag = array[num];
						if (!flag)
						{
							pos = t.TransformPoint(vertices[num]);
							float terrainHeight = this.GetTerrainHeight(pos);
							flag = (pos.y < terrainHeight);
						}
						if (flag)
						{
							array[num] = true;
							this.removeTriangles++;
							newTriangles.RemoveAt(i + 2);
							newTriangles.RemoveAt(i + 1);
							newTriangles.RemoveAt(i);
							if (i + 3 < newTriangles.Count)
							{
								i -= 3;
							}
						}
					}
				}
			}
		}

		private int totalTriangles;

		private int removeTriangles;

		private int skippedObjects;

		public List<Transform> terrains = new List<Transform>();

		public List<Transform> meshTerrains = new List<Transform>();

		public Bounds[] terrainBounds;

		public Bounds[] meshBounds;

		private Terrain[] terrainComponents;

		private Terrain[] terrainArray;

		private Bounds[] terrainBoundsArray;

		private MeshRenderer[] mrs;

		private Mesh[] meshTerrainComponents;

		private Mesh[] meshArray;

		public bool runOnStart;
	}
}
