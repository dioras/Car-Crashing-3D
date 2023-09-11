using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GeNa
{
	public class TreeManager
	{
		public void LoadTreesFromTerrain()
		{
			this.m_terrainTrees = null;
			this.m_terrainTreeLocations = null;
			float f = float.NaN;
			float num = float.NaN;
			float num2 = float.NaN;
			float num3 = float.NaN;
			float num4 = float.NaN;
			Terrain terrain = null;
			foreach (Terrain terrain2 in Terrain.activeTerrains)
			{
				if (float.IsNaN(f))
				{
					terrain = terrain2;
					f = terrain2.transform.position.y;
					num = terrain2.transform.position.x;
					num3 = terrain2.transform.position.z;
					num2 = num + terrain2.terrainData.size.x;
					num4 = num3 + terrain2.terrainData.size.z;
				}
				else
				{
					if (terrain2.transform.position.x < num)
					{
						num = terrain2.transform.position.x;
					}
					if (terrain2.transform.position.z < num3)
					{
						num3 = terrain2.transform.position.z;
					}
					if (terrain2.transform.position.x + terrain2.terrainData.size.x > num2)
					{
						num2 = terrain2.transform.position.x + terrain2.terrainData.size.x;
					}
					if (terrain2.transform.position.z + terrain2.terrainData.size.z > num4)
					{
						num4 = terrain2.transform.position.z + terrain2.terrainData.size.z;
					}
				}
			}
			if (terrain != null)
			{
				Rect boundaries = new Rect(num, num3, num2 - num, num4 - num3);
				this.m_terrainTreeLocations = new Quadtree<int>(boundaries, 32);
				this.m_terrainTrees = new List<TreePrototype>(terrain.terrainData.treePrototypes);
				foreach (Terrain terrain3 in Terrain.activeTerrains)
				{
					float x = terrain3.transform.position.x;
					float z = terrain3.transform.position.z;
					float x2 = terrain3.terrainData.size.x;
					float z2 = terrain3.terrainData.size.z;
					TreeInstance[] treeInstances = terrain3.terrainData.treeInstances;
					for (int k = 0; k < treeInstances.Length; k++)
					{
						TreeInstance treeInstance = treeInstances[k];
						this.m_terrainTreeLocations.Insert(x + treeInstance.position.x * x2, z + treeInstance.position.z * z2, treeInstances[k].prototypeIndex);
					}
				}
			}
		}

		public void AddTree(Vector3 position, int prototypeIdx)
		{
			if (this.m_terrainTreeLocations == null)
			{
				return;
			}
			this.m_terrainTreeLocations.Insert(position.x, position.z, prototypeIdx);
		}

		public int Count(Vector3 position, float range)
		{
			if (this.m_terrainTreeLocations == null)
			{
				return 0;
			}
			Rect range2 = new Rect(position.x - range, position.z - range, range * 2f, range * 2f);
			return this.m_terrainTreeLocations.Find(range2).Count<int>();
		}

		public int Count()
		{
			if (this.m_terrainTreeLocations == null)
			{
				return 0;
			}
			return this.m_terrainTreeLocations.Count;
		}

		private List<TreePrototype> m_terrainTrees = new List<TreePrototype>();

		private Quadtree<int> m_terrainTreeLocations = new Quadtree<int>(new Rect(0f, 0f, 10f, 10f), 32);
	}
}
