using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
	public class SpawnTextureExtension : SpawnRuleExtension
	{
		public override void Initialise()
		{
			this.m_textureHM = new UnityHeightMap(this.m_textureMask);
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

		public override bool AffectsTextures()
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
			List<HeightMap> textureMaps = spawnInfo.m_spawner.GetTextureMaps(spawnInfo.m_hitTerrain.GetInstanceID());
			if (textureMaps == null || this.m_textureIndex >= textureMaps.Count)
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
			for (float num7 = num2; num7 < num4; num7 += num6)
			{
				for (float num8 = num3; num8 < num5; num8 += num6)
				{
					point = new Vector3(num7, spawnInfo.m_hitLocationWU.y, num8);
					point = Utils.RotatePointAroundPivot(point, spawnInfo.m_hitLocationWU, new Vector3(0f, spawnInfo.m_spawnRotationY, 0f));
					float num9 = point.x / x + 0.5f;
					float num10 = point.z / z + 0.5f;
					if (num9 >= 0f && num9 < 1f && num10 >= 0f && num10 <= 1f)
					{
						float num11 = textureMaps[this.m_textureIndex][num10, num9];
						float num12 = this.m_textureHM[(num7 - num2) / num, (num8 - num3) / num];
						if (num12 > num11)
						{
							float num13 = num12 - num11;
							float num14 = 1f - num11;
							float num15 = 0f;
							if (num14 != 0f)
							{
								num15 = 1f - num13 / num14;
							}
							for (int i = 0; i < textureMaps.Count; i++)
							{
								if (i == this.m_textureIndex)
								{
									textureMaps[i][num10, num9] = num12;
								}
								else
								{
									HeightMap heightMap= textureMaps[i];
									float x2= num10;
									float z2= num9;
									(heightMap )[x2 , z2 ] = heightMap[x2, z2] * num15;
								}
							}
						}
					}
				}
			}
			spawnInfo.m_spawner.SetTextureMapsDirty();
		}

		[Tooltip("The zero based index of the terrain texture to be applied.")]
		public int m_textureIndex;

		[Tooltip("The mask used to display this texture.")]
		public Texture2D m_textureMask;

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
