using System;
using UnityEngine;

namespace Gaia
{
	[Serializable]
	public class SpawnCritera
	{
		public void Initialise(Spawner spawner)
		{
			string text = string.Empty;
			char c = 4.ToString()[0];
			if (Terrain.activeTerrain != null)
			{
				c = Terrain.activeTerrain.terrainData.alphamapLayers.ToString()[0];
			}
			for (int i = 0; i < this.m_matchingTextures.Length; i++)
			{
				if ((this.m_matchingTextures[i] >= '0' && this.m_matchingTextures[i] <= c) || this.m_matchingTextures[i] == '!')
				{
					text += this.m_matchingTextures[i];
				}
			}
			this.m_matchingTextures = text;
			this.m_isInitialised = true;
		}

		public float GetSlopeFitness(float slope)
		{
			float num = 1f;
			if (this.m_checkSlope)
			{
				if (slope < this.m_minSlope || slope > this.m_maxSlope)
				{
					return 0f;
				}
				num = this.m_maxSlope - this.m_minSlope;
				if (num > 0f)
				{
					num = (slope - this.m_minSlope) / num;
					num = this.m_slopeFitness.Evaluate(num);
				}
				else
				{
					num = 0f;
				}
			}
			return num;
		}

		public float GetHeightFitness(float height, float sealLevel)
		{
			float num = 1f;
			if (this.m_checkHeight)
			{
				height -= sealLevel;
				if (height < this.m_minHeight || height > this.m_maxHeight)
				{
					return 0f;
				}
				num = this.m_maxHeight - this.m_minHeight;
				if (num > 0f)
				{
					num = (height - this.m_minHeight) / num;
					num = this.m_heightFitness.Evaluate(num);
				}
				else
				{
					num = 0f;
				}
			}
			return num;
		}

		public float GetProximityFitness(float proximity)
		{
			float num = 1f;
			if (this.m_checkProximity)
			{
				if (proximity < this.m_minProximity || proximity > this.m_maxProximity)
				{
					return 0f;
				}
				num = this.m_maxProximity - this.m_minProximity;
				if (num > 0f)
				{
					num = (proximity - this.m_minProximity) / num;
					num = this.m_proximityFitness.Evaluate(num);
				}
				else
				{
					num = 0f;
				}
			}
			return num;
		}

		public float GetTextureFitness(float[] textures)
		{
			bool flag = false;
			float num = 1f;
			if (this.m_checkTexture)
			{
				num = float.MaxValue;
				for (int i = 0; i < this.m_matchingTextures.Length; i++)
				{
					if (this.m_matchingTextures[i] == '!')
					{
						flag = true;
					}
					else if (flag)
					{
						float num2 = 1f - textures[(int)char.GetNumericValue(this.m_matchingTextures[i])];
						flag = false;
						if (num == 3.40282347E+38f || num2 < num)
						{
							num = num2;
						}
					}
					else
					{
						float num2 = textures[(int)char.GetNumericValue(this.m_matchingTextures[i])];
						if (num == 3.40282347E+38f || num2 > num)
						{
							num = num2;
						}
					}
				}
			}
			return num;
		}

		public float GetFitness(ref SpawnInfo spawnInfo)
		{
			if (!this.m_isInitialised)
			{
				this.Initialise(spawnInfo.m_spawner);
			}
			if (!this.m_isActive)
			{
				return 0f;
			}
			if (this.m_virginTerrain && !spawnInfo.m_wasVirginTerrain)
			{
				return 0f;
			}
			float num = 1f;
			if (this.m_checkHeight)
			{
				num = Mathf.Min(num, this.GetHeightFitness(spawnInfo.m_terrainHeightWU, spawnInfo.m_spawner.m_resources.m_seaLevel));
			}
			if (this.m_checkSlope && num > 0f)
			{
				if (this.m_checkType == GaiaConstants.SpawnerLocationCheckType.PointCheck)
				{
					num = Mathf.Min(num, this.GetSlopeFitness(spawnInfo.m_terrainSlopeWU));
				}
				else
				{
					num = Mathf.Min(num, this.GetSlopeFitness(spawnInfo.m_areaAvgSlopeWU));
				}
			}
			if (this.m_checkTexture && num > 0f)
			{
				num = Mathf.Min(num, this.GetTextureFitness(spawnInfo.m_textureStrengths));
			}
			if (this.m_checkProximity && num > 0f)
			{
				Rect area = new Rect(spawnInfo.m_hitLocationWU.x - this.m_maxProximity, spawnInfo.m_hitLocationWU.z - this.m_maxProximity, this.m_maxProximity * 2f, this.m_maxProximity * 2f);
				GameObject closestObject = spawnInfo.m_spawner.GetClosestObject(this.m_proximityTag, area);
				if (closestObject != null)
				{
					num = Mathf.Min(num, this.GetProximityFitness(Vector3.Distance(closestObject.transform.position, spawnInfo.m_hitLocationWU)));
				}
				else
				{
					num = 0f;
				}
			}
			return num;
		}

		[Tooltip("Criteria name")]
		public string m_name;

		[Tooltip("CHECK TYPE - a single point on the terrain or area based. The size of the area checks is based on the Bounds Radius in the DNA. Area based checks are good for larger structures but substantially slower so use with care.")]
		public GaiaConstants.SpawnerLocationCheckType m_checkType;

		[Tooltip("When selected, the criteria will only be valid if the terrain was clear of any other objects at this location. A location is determined to be ‘virgin’ when raycast collision test hits clear terrain. To detect other objects at this location they must have colliders. You can use an invisible collider and this test to stop any resources that require virgin terrain from spawning at that location.")]
		public bool m_virginTerrain = true;

		[Tooltip("Whether or not this location will be checked for height.")]
		public bool m_checkHeight = true;

		[Tooltip("The minimum valid height relative to sea level. Only tested when Check Height is checked.")]
		public float m_minHeight;

		[Tooltip("The maximum valid height relative to sea level. Only tested when Check Height is checked.")]
		public float m_maxHeight = 1000f;

		[Tooltip("The fitness curve - evaluated between the minimum and the maximum height when Check Height is checked. 0 is low fitness and unlikely to spawn, 1 is high fitness and likely to spawn.")]
		public AnimationCurve m_heightFitness = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0f)
		});

		[Tooltip("Whether or not this location will be checked for slope.")]
		public bool m_checkSlope = true;

		[Tooltip("The minimum valid slope. Only tested when Check Slope is checked.")]
		public float m_minSlope;

		[Tooltip("The maximum valid slope. Only tested when Check Slope is checked.")]
		public float m_maxSlope = 90f;

		[Tooltip("The fitness curve - evaluated between the minimum and the maximum slope when Check Slope is checked. 0 is low fitness and unlikely to spawn, 1 is high fitness and likely to spawn.")]
		public AnimationCurve m_slopeFitness = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0f)
		});

		[Tooltip("Whether or not to check proximity to other tags near this location.")]
		public bool m_checkProximity;

		[Tooltip("The tag that will be checked in proximity to this location e.g. House")]
		public string m_proximityTag = string.Empty;

		[Tooltip("The minimum valid proximity. Only tested when Check Proximity is checked.")]
		public float m_minProximity;

		[Tooltip("The maximum valid proximity. Only tested when Check Proximity is checked.")]
		public float m_maxProximity = 100f;

		[Tooltip("The fitness curve - evaluated between the minimum and the maximum proximity when Check Proximity is checked. 0 is low fitness and unlikely to spawn, 1 is high fitness and likely to spawn.")]
		public AnimationCurve m_proximityFitness = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(0.3f, 0.2f),
			new Keyframe(1f, 0f)
		});

		[Tooltip("Check textures at this location.")]
		public bool m_checkTexture;

		[Tooltip("Texture slots from your terrain (first valid slot is 0). Will select for presence that texture. Use exclamation mark in front of slot to select for absence of that texture. For example 3 selects for presence of texture 3, !3 checks for absence of texture 3. Fitness is based on the strength of the texture at that location in range 0..1. Only tested when Check Texture is checked.")]
		public string m_matchingTextures = "!3";

		[Tooltip("Whether or not this spawn criteria is active. It will be ignored if not active.")]
		public bool m_isActive = true;

		private bool m_isInitialised;
	}
}
