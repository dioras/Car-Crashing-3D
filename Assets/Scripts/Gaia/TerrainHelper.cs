using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
	public class TerrainHelper : MonoBehaviour
	{
		private void Awake()
		{
			base.gameObject.SetActive(false);
		}

		public static void Flatten()
		{
			TerrainHelper.FlattenTerrain(Terrain.activeTerrains);
		}

		public static void FlattenTerrain(Terrain terrain)
		{
			int heightmapWidth = terrain.terrainData.heightmapResolution;
			int heightmapHeight = terrain.terrainData.heightmapResolution;
			float[,] heights = new float[heightmapWidth, heightmapHeight];
			terrain.terrainData.SetHeights(0, 0, heights);
		}

		public static void FlattenTerrain(Terrain[] terrains)
		{
			foreach (Terrain terrain in terrains)
			{
				int heightmapWidth = terrain.terrainData.heightmapResolution;
				int heightmapHeight = terrain.terrainData.heightmapResolution;
				float[,] heights = new float[heightmapWidth, heightmapHeight];
				terrain.terrainData.SetHeights(0, 0, heights);
			}
		}

		public static void Stitch()
		{
			TerrainHelper.StitchTerrains(Terrain.activeTerrains);
		}

		public static void StitchTerrains(Terrain[] terrains)
		{
			foreach (Terrain terrain in terrains)
			{
				Terrain right = null;
				Terrain left = null;
				Terrain bottom = null;
				Terrain top = null;
				foreach (Terrain terrain2 in terrains)
				{
					if (terrain2.transform.position.x == terrain.transform.position.x)
					{
						if (terrain2.transform.position.z + terrain2.terrainData.size.z == terrain.transform.position.z)
						{
							top = terrain2;
						}
						else if (terrain.transform.position.z + terrain.terrainData.size.z == terrain2.transform.position.z)
						{
							bottom = terrain2;
						}
					}
					else if (terrain2.transform.position.z == terrain.transform.position.z)
					{
						if (terrain2.transform.position.x + terrain2.terrainData.size.z == terrain.transform.position.z)
						{
							left = terrain2;
						}
						else if (terrain.transform.position.x + terrain.terrainData.size.x == terrain2.transform.position.x)
						{
							right = terrain2;
						}
					}
				}
				terrain.SetNeighbors(left, top, right, bottom);
			}
		}

		public void Smooth()
		{
			TerrainHelper.Smooth(this.m_smoothIterations);
		}

		public static void Smooth(int iterations)
		{
			UnityHeightMap unityHeightMap = new UnityHeightMap(Terrain.activeTerrain);
			unityHeightMap.Smooth(iterations);
			unityHeightMap.SaveToTerrain(Terrain.activeTerrain);
		}

		public static Vector3 GetActiveTerrainCenter(bool flushToGround = true)
		{
			Bounds bounds = default(Bounds);
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (!TerrainHelper.GetTerrainBounds(activeTerrain, ref bounds))
			{
				return Vector3.zero;
			}
			if (flushToGround)
			{
				return new Vector3(bounds.center.x, activeTerrain.SampleHeight(bounds.center), bounds.center.z);
			}
			return bounds.center;
		}

		public static Terrain GetActiveTerrain()
		{
			Terrain terrain = Terrain.activeTerrain;
			if (terrain != null && terrain.isActiveAndEnabled)
			{
				return terrain;
			}
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				terrain = Terrain.activeTerrains[i];
				if (terrain != null && terrain.isActiveAndEnabled)
				{
					return terrain;
				}
			}
			return null;
		}

		public static LayerMask GetActiveTerrainLayer()
		{
			LayerMask result = default(LayerMask);
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain != null)
			{
				result.value = 1 << activeTerrain.gameObject.layer;
				return result;
			}
			result.value = 1 << LayerMask.NameToLayer("Default");
			return result;
		}

		public static LayerMask GetActiveTerrainLayerAsInt()
		{
			LayerMask mask = TerrainHelper.GetActiveTerrainLayer().value;
			for (int i = 0; i < 32; i++)
			{
				if (mask == 1 << i)
				{
					return i;
				}
			}
			return LayerMask.NameToLayer("Default");
		}

		public static int GetActiveTerrainCount()
		{
			int num = 0;
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				if (terrain != null && terrain.isActiveAndEnabled)
				{
					num++;
				}
			}
			return num;
		}

		public static Terrain GetTerrain(Vector3 locationWU)
		{
			Vector3 a = default(Vector3);
			Vector3 vector = default(Vector3);
			Terrain terrain = Terrain.activeTerrain;
			if (terrain != null)
			{
				a = terrain.GetPosition();
				vector = a + terrain.terrainData.size;
				if (locationWU.x >= a.x && locationWU.x <= vector.x && locationWU.z >= a.z && locationWU.z <= vector.z)
				{
					return terrain;
				}
			}
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				terrain = Terrain.activeTerrains[i];
				a = terrain.GetPosition();
				vector = a + terrain.terrainData.size;
				if (locationWU.x >= a.x && locationWU.x <= vector.x && locationWU.z >= a.z && locationWU.z <= vector.z)
				{
					return terrain;
				}
			}
			return null;
		}

		public static bool GetTerrainBounds(Terrain terrain, ref Bounds bounds)
		{
			if (terrain == null)
			{
				return false;
			}
			bounds.center = terrain.transform.position;
			bounds.size = terrain.terrainData.size;
			bounds.center += bounds.extents;
			return true;
		}

		public static bool GetTerrainBounds(Vector3 locationWU, ref Bounds bounds)
		{
			Terrain terrain = TerrainHelper.GetTerrain(locationWU);
			if (terrain == null)
			{
				return false;
			}
			bounds.center = terrain.transform.position;
			bounds.size = terrain.terrainData.size;
			bounds.center += bounds.extents;
			return true;
		}

		public static Vector3 GetRandomPositionOnTerrain(Terrain terrain, Vector3 start, float radius)
		{
			Vector3 position = terrain.GetPosition();
			Vector3 vector = position + terrain.terrainData.size;
			Vector3 vector2;
			do
			{
				vector2 = UnityEngine.Random.insideUnitSphere * radius;
				vector2 = start + vector2;
			}
			while (vector2.x < position.x || vector2.x > vector.x || vector2.z < position.z || vector2.z > vector.z);
			vector2.y = terrain.SampleHeight(vector2);
			return vector2;
		}

		public static void ClearTrees()
		{
			List<TreeInstance> list = new List<TreeInstance>();
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				terrain.terrainData.treeInstances = list.ToArray();
				terrain.Flush();
			}
			Spawner[] array = UnityEngine.Object.FindObjectsOfType<Spawner>();
			foreach (Spawner spawner in array)
			{
				spawner.SetUpSpawnerTypeFlags();
				if (spawner.IsTreeSpawner())
				{
					spawner.ResetSpawner();
				}
			}
		}

		public static void ClearDetails()
		{
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				int[,] details = new int[terrain.terrainData.detailWidth, terrain.terrainData.detailHeight];
				for (int j = 0; j < terrain.terrainData.detailPrototypes.Length; j++)
				{
					terrain.terrainData.SetDetailLayer(0, 0, j, details);
				}
				terrain.Flush();
			}
			Spawner[] array = UnityEngine.Object.FindObjectsOfType<Spawner>();
			foreach (Spawner spawner in array)
			{
				if (spawner.IsDetailSpawner())
				{
					spawner.ResetSpawner();
				}
			}
		}

		public static float GetRangeFromTerrain()
		{
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain != null)
			{
				return Mathf.Max(activeTerrain.terrainData.size.x, activeTerrain.terrainData.size.z) / 2f;
			}
			return 0f;
		}

		[Range(1f, 5f)]
		[Tooltip("Number of smoothing interations to run. Can be run multiple times.")]
		public int m_smoothIterations = 1;
	}
}
