using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GeNa
{
	public class ProbeManager
	{
		public void LoadProbesFromScene()
		{
			this.m_probeLocations = null;
			float f = float.NaN;
			float num = float.NaN;
			float num2 = float.NaN;
			float num3 = float.NaN;
			float num4 = float.NaN;
			Terrain x = null;
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				if (float.IsNaN(f))
				{
					x = terrain;
					f = terrain.transform.position.y;
					num = terrain.transform.position.x;
					num3 = terrain.transform.position.z;
					num2 = num + terrain.terrainData.size.x;
					num4 = num3 + terrain.terrainData.size.z;
				}
				else
				{
					if (terrain.transform.position.x < num)
					{
						num = terrain.transform.position.x;
					}
					if (terrain.transform.position.z < num3)
					{
						num3 = terrain.transform.position.z;
					}
					if (terrain.transform.position.x + terrain.terrainData.size.x > num2)
					{
						num2 = terrain.transform.position.x + terrain.terrainData.size.x;
					}
					if (terrain.transform.position.z + terrain.terrainData.size.z > num4)
					{
						num4 = terrain.transform.position.z + terrain.terrainData.size.z;
					}
				}
			}
			if (x != null)
			{
				Rect boundaries = new Rect(num, num3, num2 - num, num4 - num3);
				this.m_probeLocations = new Quadtree<LightProbeGroup>(boundaries, 32);
			}
			else
			{
				Rect boundaries2 = new Rect(-10000f, -10000f, 20000f, 20000f);
				this.m_probeLocations = new Quadtree<LightProbeGroup>(boundaries2, 32);
			}
			foreach (LightProbeGroup lightProbeGroup in UnityEngine.Object.FindObjectsOfType<LightProbeGroup>())
			{
				for (int k = 0; k < lightProbeGroup.probePositions.Length; k++)
				{
					this.m_probeLocations.Insert(lightProbeGroup.transform.position.x + lightProbeGroup.probePositions[k].x, lightProbeGroup.transform.position.z + lightProbeGroup.probePositions[k].z, lightProbeGroup);
				}
			}
		}

		public void AddProbe(Vector3 position, LightProbeGroup probeGroup)
		{
			if (this.m_probeLocations == null)
			{
				return;
			}
			this.m_probeLocations.Insert(position.x, position.z, probeGroup);
		}

		public List<LightProbeGroup> GetProbeGroups(Vector3 position, float range)
		{
			if (this.m_probeLocations == null)
			{
				return new List<LightProbeGroup>();
			}
			Rect range2 = new Rect(position.x - range, position.z - range, range * 2f, range * 2f);
			return this.m_probeLocations.Find(range2).ToList<LightProbeGroup>();
		}

		public int Count(Vector3 position, float range)
		{
			if (this.m_probeLocations == null)
			{
				return 0;
			}
			Rect range2 = new Rect(position.x - range, position.z - range, range * 2f, range * 2f);
			return this.m_probeLocations.Find(range2).Count<LightProbeGroup>();
		}

		public int Count()
		{
			if (this.m_probeLocations == null)
			{
				return 0;
			}
			return this.m_probeLocations.Count;
		}

		private Quadtree<LightProbeGroup> m_probeLocations = new Quadtree<LightProbeGroup>(new Rect(0f, 0f, 10f, 10f), 32);
	}
}
