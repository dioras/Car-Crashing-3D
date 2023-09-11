using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
	public class SpawnGrassExtension : SpawnRuleExtension
	{
		public override void Initialise()
		{
			this.m_textureHM = new UnityHeightMap(this.m_grassMask);
			if (this.m_normaliseMask && this.m_textureHM.HasData())
			{
				this.m_textureHM.Normalise();
			}
			if (this.m_invertMask && this.m_textureHM.HasData())
			{
				this.m_textureHM.Invert();
			}
			if (this.m_flipMask && this.m_textureHM.HasData())
			{
				this.m_textureHM.Flip();
			}
		}

		public override bool AffectsDetails()
		{
			return true;
		}

		public override void PostSpawn(SpawnRule spawnRule, ref SpawnInfo spawnInfo)
		{
			if (this.m_textureHM == null || !this.m_textureHM.HasData())
			{
				return;
			}
			Terrain terrain = TerrainHelper.GetTerrain(spawnInfo.m_hitLocationWU);
			if (terrain == null)
			{
				return;
			}
			List<HeightMap> detailMaps = spawnInfo.m_spawner.GetDetailMaps(spawnInfo.m_hitTerrain.GetInstanceID());
			if (detailMaps == null || this.m_grassIndex >= detailMaps.Count)
			{
				return;
			}
			float x = spawnInfo.m_hitTerrain.terrainData.size.x;
			float z = spawnInfo.m_hitTerrain.terrainData.size.z;
			float num = spawnRule.GetMaxScaledRadius(ref spawnInfo) * this.m_scaleMask;
			float num2 = spawnInfo.m_hitLocationWU.x - num / 2f;
			float num3 = spawnInfo.m_hitLocationWU.z - num / 2f;
			float num4 = num2 + num;
			float num5 = num3 + num;
			float num6 = 0.5f;
			Vector3 point = Vector3.zero;
			float num7 = (float)(this.m_maxGrassStrength - this.m_minGrassStrenth);
			for (float num8 = num2; num8 < num4; num8 += num6)
			{
				for (float num9 = num3; num9 < num5; num9 += num6)
				{
					point = new Vector3(num8, spawnInfo.m_hitLocationWU.y, num9);
					point = Utils.RotatePointAroundPivot(point, spawnInfo.m_hitLocationWU, new Vector3(0f, spawnInfo.m_spawnRotationY, 0f));
					float num10 = point.x / x + 0.5f;
					float num11 = point.z / z + 0.5f;
					if (num10 >= 0f && num10 < 1f && num11 >= 0f && num11 <= 1f)
					{
						detailMaps[this.m_grassIndex][num11, num10] = Mathf.Clamp(this.m_textureHM[(num8 - num2) / num, (num9 - num3) / num] * num7 + (float)this.m_minGrassStrenth, 0f, 15f);
					}
				}
			}
		}

		[Tooltip("The zero based index of the grass texture to be applied.")]
		public int m_grassIndex;

		[Tooltip("The minimum strength of the grass to be applied.")]
		[Range(0f, 15f)]
		public int m_minGrassStrenth;

		[Tooltip("The maximum strength of the grass to be applied.")]
		[Range(0f, 15f)]
		public int m_maxGrassStrength = 15;

		[Tooltip("The mask used to display this grass.")]
		public Texture2D m_grassMask;

		[Tooltip("Whether or not to normalise the mask. Normalisation allows the full dynamic range of the mask to be used.")]
		public bool m_normaliseMask = true;

		[Tooltip("Whether or not to invert the mask.")]
		public bool m_invertMask;

		[Tooltip("Whether or not to flip the mask.")]
		public bool m_flipMask;

		[Tooltip("The mask scale with respect to the areas bounds of this spawn.")]
		[Range(0.1f, 10f)]
		public float m_scaleMask = 1f;

		private UnityHeightMap m_textureHM;
	}
}
