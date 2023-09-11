using System;
using System.Collections.Generic;
using Gaia.FullSerializer;
using UnityEngine;

namespace Gaia
{
	[Serializable]
	public class SpawnRule
	{
		public void Initialise(Spawner spawner)
		{
			if (!this.m_isActive)
			{
				return;
			}
			if (spawner.m_resources.ResourceIdxOutOfBounds(this.m_resourceType, this.m_resourceIdx))
			{
				UnityEngine.Debug.Log(string.Format("Warning: {0} - {1} :: Disabling rule {2} idx {3}, index out of bounds", new object[]
				{
					spawner.gameObject.name,
					this.m_name,
					this.m_resourceType,
					this.m_resourceIdx
				}));
				this.m_isActive = false;
				return;
			}
			if (!spawner.m_resources.ResourceIsInUnity(this.m_resourceType, this.m_resourceIdx))
			{
				UnityEngine.Debug.Log(string.Format("Warning: {0} - {1} :: Disabling rule {2} idx {3}, resource missing from unity", new object[]
				{
					spawner.gameObject.name,
					this.m_name,
					this.m_resourceType,
					this.m_resourceIdx
				}));
				this.m_isActive = false;
				return;
			}
			this.m_resourceIdxPhysical = spawner.m_resources.PrototypeIdxInTerrain(this.m_resourceType, this.m_resourceIdx);
			if (this.m_resourceIdxPhysical < 0 && Application.isPlaying)
			{
				UnityEngine.Debug.Log(string.Format("Warning: {0} - {1} :: Disabling rule as resource is physically missing", spawner.gameObject.name, this.m_name));
				this.m_isActive = false;
				return;
			}
			if (this.m_noiseGenerator == null)
			{
				this.m_noiseGenerator = new FractalGenerator();
			}
			this.m_noiseGenerator.Frequency = this.m_noiseMaskFrequency;
			this.m_noiseGenerator.Lacunarity = this.m_noiseMaskLacunarity;
			this.m_noiseGenerator.Octaves = this.m_noiseMaskOctaves;
			this.m_noiseGenerator.Persistence = this.m_noiseMaskPersistence;
			this.m_noiseGenerator.Seed = this.m_noiseMaskSeed;
			GaiaConstants.NoiseType noiseMask = this.m_noiseMask;
			if (noiseMask != GaiaConstants.NoiseType.Billow)
			{
				if (noiseMask != GaiaConstants.NoiseType.Ridged)
				{
					this.m_noiseGenerator.FractalType = FractalGenerator.Fractals.Perlin;
				}
				else
				{
					this.m_noiseGenerator.FractalType = FractalGenerator.Fractals.RidgeMulti;
				}
			}
			else
			{
				this.m_noiseGenerator.FractalType = FractalGenerator.Fractals.Billow;
			}
			if (this.m_resourceType == GaiaConstants.SpawnerResourceType.GameObject)
			{
				if (spawner.m_goParentGameObject == null)
				{
					Transform transform = spawner.transform.Find("Spawned_GameObjects");
					if (transform == null)
					{
						spawner.m_goParentGameObject = new GameObject("Spawned_GameObjects");
					}
					else
					{
						spawner.m_goParentGameObject = transform.gameObject;
					}
				}
				Transform transform2 = spawner.m_goParentGameObject.transform.Find(this.m_name);
				if (transform2 == null)
				{
					transform2 = new GameObject(this.m_name)
					{
						transform = 
						{
							parent = spawner.m_goParentGameObject.transform
						}
					}.transform;
				}
				this.m_spawnParent = transform2;
			}
			if (this.m_resourceType == GaiaConstants.SpawnerResourceType.GameObject)
			{
				if (spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_dna.m_extParam.ToLower().Contains("nograss"))
				{
					this.m_colliderName = "_GaiaCollider_NoGrass";
				}
				else
				{
					this.m_colliderName = "_GaiaCollider_Grass";
				}
			}
			if (this.m_resourceType == GaiaConstants.SpawnerResourceType.TerrainTree)
			{
				if (spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_dna.m_extParam.ToLower().Contains("nograss"))
				{
					this.m_colliderName = "_GaiaCollider_NoGrass";
				}
				else
				{
					this.m_colliderName = "_GaiaCollider_Grass";
				}
			}
			SpawnRuleExtension[] array = null;
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				array = spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				array = spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				array = spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.GameObject:
				array = spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			}
			if (array != null)
			{
				for (int i = 0; i < array.GetLength(0); i++)
				{
					array[i].Initialise();
				}
			}
		}

		public bool ResourceIsInUnity(Spawner spawner)
		{
			return spawner.m_resources.ResourceIsInUnity(this.m_resourceType, this.m_resourceIdx);
		}

		public bool ResourceIsLoadedInTerrain(Spawner spawner)
		{
			this.m_resourceIdxPhysical = spawner.m_resources.PrototypeIdxInTerrain(this.m_resourceType, this.m_resourceIdx);
			return this.m_resourceIdxPhysical >= 0;
		}

		public void AddResourceToTerrain(Spawner spawner)
		{
			spawner.m_resources.AddPrototypeToTerrain(this.m_resourceType, this.m_resourceIdx);
		}

		public float GetFitness(ref SpawnInfo spawnInfo)
		{
			if (!this.m_isActive)
			{
				return 0f;
			}
			SpawnCritera[] array = null;
			SpawnRuleExtension[] array2 = null;
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				array = spawnInfo.m_spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].m_spawnCriteria;
				array2 = spawnInfo.m_spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				array = spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_spawnCriteria;
				array2 = spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				array = spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_spawnCriteria;
				array2 = spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.GameObject:
				array = spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_spawnCriteria;
				array2 = spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			}
			if (array == null || array.Length == 0)
			{
				return 0f;
			}
			float num = 1f;
			if (this.m_noiseMask != GaiaConstants.NoiseType.None && this.m_noiseGenerator != null)
			{
				if (!this.m_noiseInvert)
				{
					num = Mathf.Clamp01(this.m_noiseGenerator.GetNormalisedValue(100000f + spawnInfo.m_hitLocationWU.x * (1f / this.m_noiseZoom), 100000f + spawnInfo.m_hitLocationWU.z * (1f / this.m_noiseZoom)) * this.m_noiseStrength);
				}
				else
				{
					num = Mathf.Clamp01(1f - this.m_noiseGenerator.GetNormalisedValue(100000f + spawnInfo.m_hitLocationWU.x * (1f / this.m_noiseZoom), 100000f + spawnInfo.m_hitLocationWU.z * (1f / this.m_noiseZoom)) * this.m_noiseStrength);
				}
			}
			float num2 = float.MinValue;
			foreach (SpawnCritera spawnCritera in array)
			{
				if (spawnCritera.m_checkType == GaiaConstants.SpawnerLocationCheckType.BoundedAreaCheck && !spawnInfo.m_spawner.CheckLocationBounds(ref spawnInfo, this.GetMaxScaledRadius(ref spawnInfo)))
				{
					return 0f;
				}
				float num3 = spawnCritera.GetFitness(ref spawnInfo) * num;
				if (array2 != null)
				{
					for (int j = 0; j < array2.GetLength(0); j++)
					{
						SpawnRuleExtension spawnRuleExtension = array2[j];
						if (spawnRuleExtension != null)
						{
							num3 = spawnRuleExtension.GetFitness(num3, ref spawnInfo);
						}
					}
				}
				if (num3 > num2)
				{
					num2 = num3;
					if (num2 >= 1f)
					{
						return num2;
					}
				}
			}
			if (num2 == -3.40282347E+38f)
			{
				return 0f;
			}
			return num2;
		}

		public float GetRadius(ref SpawnInfo spawnInfo)
		{
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				return 1f;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				return spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_dna.m_boundsRadius;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				return spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_dna.m_boundsRadius;
			case GaiaConstants.SpawnerResourceType.GameObject:
				return spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_dna.m_boundsRadius;
			default:
				return 0f;
			}
		}

		public float GetMaxScaledRadius(ref SpawnInfo spawnInfo)
		{
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				return 1f;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				return spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_dna.m_boundsRadius * spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_dna.m_maxScale;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				return spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_dna.m_boundsRadius * spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_dna.m_maxScale;
			case GaiaConstants.SpawnerResourceType.GameObject:
				return spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_dna.m_boundsRadius * spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_dna.m_maxScale;
			default:
				return 0f;
			}
		}

		public float GetSeedThrowRange(ref SpawnInfo spawnInfo)
		{
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				return 1f;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				return spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_dna.m_seedThrow;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				return spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_dna.m_seedThrow;
			case GaiaConstants.SpawnerResourceType.GameObject:
				return spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_dna.m_seedThrow;
			default:
				return 0f;
			}
		}

		public void Spawn(ref SpawnInfo spawnInfo)
		{
			if (!this.m_isActive)
			{
				return;
			}
			if (!this.m_ignoreMaxInstances && this.m_activeInstanceCnt > this.m_maxInstances)
			{
				return;
			}
			this.m_activeInstanceCnt += 1UL;
			SpawnRuleExtension[] array = null;
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				array = spawnInfo.m_spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				array = spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				array = spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			case GaiaConstants.SpawnerResourceType.GameObject:
				array = spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_spawnExtensions;
				break;
			}
			if (this.m_resourceType != GaiaConstants.SpawnerResourceType.GameObject)
			{
				spawnInfo.m_spawnRotationY = spawnInfo.m_spawner.GetRandomFloat(0f, 359.9f);
			}
			else
			{
				spawnInfo.m_spawnRotationY = spawnInfo.m_spawner.GetRandomFloat(this.m_minDirection, this.m_maxDirection);
			}
			bool flag = false;
			if (array != null)
			{
				for (int i = 0; i < array.GetLength(0); i++)
				{
					SpawnRuleExtension spawnRuleExtension = array[i];
					if (spawnRuleExtension != null && spawnRuleExtension.OverridesSpawn(this, ref spawnInfo))
					{
						flag = true;
						spawnRuleExtension.Spawn(this, ref spawnInfo);
					}
				}
			}
			if (!flag)
			{
				switch (this.m_resourceType)
				{
				case GaiaConstants.SpawnerResourceType.TerrainTexture:
					if (spawnInfo.m_fitness > spawnInfo.m_textureStrengths[this.m_resourceIdxPhysical])
					{
						float num = spawnInfo.m_fitness - spawnInfo.m_textureStrengths[this.m_resourceIdxPhysical];
						float num2 = 1f - spawnInfo.m_textureStrengths[this.m_resourceIdxPhysical];
						float num3 = 0f;
						if (num2 != 0f)
						{
							num3 = 1f - num / num2;
						}
						for (int j = 0; j < spawnInfo.m_textureStrengths.Length; j++)
						{
							if (j == this.m_resourceIdx)
							{
								spawnInfo.m_textureStrengths[j] = spawnInfo.m_fitness;
							}
							else
							{
								spawnInfo.m_textureStrengths[j] *= num3;
							}
						}
						spawnInfo.m_spawner.SetTextureMapsDirty();
					}
					break;
				case GaiaConstants.SpawnerResourceType.TerrainDetail:
				{
					HeightMap detailMap = spawnInfo.m_spawner.GetDetailMap(spawnInfo.m_hitTerrain.GetInstanceID(), this.m_resourceIdxPhysical);
					int num4;
					if (spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_dna.m_rndScaleInfluence)
					{
						num4 = (int)Mathf.Clamp(15f * spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_dna.GetScale(spawnInfo.m_fitness, spawnInfo.m_spawner.GetRandomFloat(0f, 1f)), 1f, 15f);
					}
					else
					{
						num4 = (int)Mathf.Clamp(15f * spawnInfo.m_spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_dna.GetScale(spawnInfo.m_fitness), 1f, 15f);
					}
					if (detailMap == null)
					{
						int xBase = (int)(spawnInfo.m_hitLocationNU.x * (float)spawnInfo.m_hitTerrain.terrainData.detailWidth);
						int yBase = (int)(spawnInfo.m_hitLocationNU.z * (float)spawnInfo.m_hitTerrain.terrainData.detailHeight);
						int[,] detailLayer = spawnInfo.m_hitTerrain.terrainData.GetDetailLayer(xBase, yBase, 1, 1, this.m_resourceIdxPhysical);
						if (detailLayer[0, 0] < num4)
						{
							detailLayer[0, 0] = num4;
							spawnInfo.m_hitTerrain.terrainData.SetDetailLayer(xBase, yBase, this.m_resourceIdxPhysical, detailLayer);
						}
					}
					else if (detailMap[spawnInfo.m_hitLocationNU.z, spawnInfo.m_hitLocationNU.x] < (float)num4)
					{
						detailMap[spawnInfo.m_hitLocationNU.z, spawnInfo.m_hitLocationNU.x] = (float)num4;
					}
					break;
				}
				case GaiaConstants.SpawnerResourceType.TerrainTree:
				{
					ResourceProtoTree resourceProtoTree = spawnInfo.m_spawner.m_resources.m_treePrototypes[this.m_resourceIdx];
					TreeInstance instance = default(TreeInstance);
					instance.prototypeIndex = this.m_resourceIdxPhysical;
					instance.position = spawnInfo.m_hitLocationNU;
					if (resourceProtoTree.m_dna.m_rndScaleInfluence)
					{
						instance.widthScale = resourceProtoTree.m_dna.GetScale(spawnInfo.m_fitness, spawnInfo.m_spawner.GetRandomFloat(0f, 1f));
					}
					else
					{
						instance.widthScale = resourceProtoTree.m_dna.GetScale(spawnInfo.m_fitness);
					}
					instance.heightScale = instance.widthScale;
					instance.rotation = spawnInfo.m_spawnRotationY * 0.0174532924f;
					instance.color = resourceProtoTree.m_healthyColour;
					instance.lightmapColor = Color.white;
					spawnInfo.m_hitTerrain.AddTreeInstance(instance);
					float num5 = resourceProtoTree.m_dna.m_boundsRadius * instance.widthScale;
					GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
					gameObject.name = this.m_colliderName;
					gameObject.transform.position = new Vector3(spawnInfo.m_hitLocationWU.x, spawnInfo.m_hitLocationWU.y + num5, spawnInfo.m_hitLocationWU.z);
					gameObject.GetComponent<MeshRenderer>().enabled = false;
					gameObject.transform.localScale = new Vector3(num5, num5, num5);
					gameObject.AddComponent<CapsuleCollider>();
					gameObject.layer = spawnInfo.m_spawner.m_spawnColliderLayer;
					if (spawnInfo.m_spawner.m_areaBoundsColliderCache == null)
					{
						spawnInfo.m_spawner.m_areaBoundsColliderCache = new GameObject("Bounds_ColliderCache");
						spawnInfo.m_spawner.m_areaBoundsColliderCache.transform.parent = spawnInfo.m_spawner.transform;
					}
					gameObject.transform.parent = spawnInfo.m_spawner.m_areaBoundsColliderCache.transform;
					break;
				}
				case GaiaConstants.SpawnerResourceType.GameObject:
				{
					ResourceProtoGameObject resourceProtoGameObject = spawnInfo.m_spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx];
					float scale;
					if (resourceProtoGameObject.m_dna.m_rndScaleInfluence)
					{
						scale = resourceProtoGameObject.m_dna.GetScale(spawnInfo.m_fitness, spawnInfo.m_spawner.GetRandomFloat(0f, 1f));
					}
					else
					{
						scale = resourceProtoGameObject.m_dna.GetScale(spawnInfo.m_fitness);
					}
					float num6 = resourceProtoGameObject.m_dna.m_boundsRadius * scale;
					Vector3 localScale = new Vector3(scale, scale, scale);
					Vector3 vector = spawnInfo.m_hitLocationWU;
					SpawnInfo spawnInfo2 = new SpawnInfo();
					spawnInfo2.m_spawner = spawnInfo.m_spawner;
					spawnInfo2.m_textureStrengths = new float[Terrain.activeTerrain.terrainData.alphamapLayers];
					for (int k = 0; k < resourceProtoGameObject.m_instances.Length; k++)
					{
						ResourceProtoGameObjectInstance resourceProtoGameObjectInstance = resourceProtoGameObject.m_instances[k];
						int randomInt = spawnInfo.m_spawner.GetRandomInt(resourceProtoGameObjectInstance.m_minInstances, resourceProtoGameObjectInstance.m_maxInstances);
						for (int l = 0; l < randomInt; l++)
						{
							if (spawnInfo.m_spawner.GetRandomFloat(0f, 1f) >= resourceProtoGameObjectInstance.m_failureRate)
							{
								vector = spawnInfo.m_hitLocationWU;
								vector.x += spawnInfo.m_spawner.GetRandomFloat(resourceProtoGameObjectInstance.m_minSpawnOffsetX, resourceProtoGameObjectInstance.m_maxSpawnOffsetX) * scale;
								vector.z += spawnInfo.m_spawner.GetRandomFloat(resourceProtoGameObjectInstance.m_minSpawnOffsetZ, resourceProtoGameObjectInstance.m_maxSpawnOffsetZ) * scale;
								vector = Utils.RotatePointAroundPivot(vector, spawnInfo.m_hitLocationWU, new Vector3(0f, spawnInfo.m_spawnRotationY, 0f));
								vector.y += 500f;
								if (spawnInfo.m_spawner.CheckLocation(vector, ref spawnInfo2) && (!resourceProtoGameObjectInstance.m_virginTerrain || spawnInfo2.m_wasVirginTerrain))
								{
									GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(resourceProtoGameObjectInstance.m_desktopPrefab);
									gameObject2.name = "_Sp_" + gameObject2.name;
									vector = spawnInfo2.m_hitLocationWU;
									vector.y = spawnInfo2.m_terrainHeightWU;
									vector.y += spawnInfo.m_spawner.GetRandomFloat(resourceProtoGameObjectInstance.m_minSpawnOffsetY, resourceProtoGameObjectInstance.m_maxSpawnOffsetY) * scale;
									gameObject2.transform.position = vector;
									float num7;
									if (resourceProtoGameObjectInstance.m_useParentScale)
									{
										num7 = scale;
										gameObject2.transform.localScale = localScale;
									}
									else
									{
										float num8 = Vector3.Distance(spawnInfo.m_hitLocationWU, spawnInfo2.m_hitLocationWU);
										num7 = resourceProtoGameObjectInstance.m_minScale + resourceProtoGameObjectInstance.m_scaleByDistance.Evaluate(num8 / num6) * spawnInfo.m_spawner.GetRandomFloat(0f, resourceProtoGameObjectInstance.m_maxScale - resourceProtoGameObjectInstance.m_minScale);
										gameObject2.transform.localScale = new Vector3(num7, num7, num7);
									}
									gameObject2.transform.rotation = Quaternion.Euler(new Vector3(spawnInfo.m_spawner.GetRandomFloat(resourceProtoGameObjectInstance.m_minRotationOffsetX, resourceProtoGameObjectInstance.m_maxRotationOffsetX), spawnInfo.m_spawner.GetRandomFloat(resourceProtoGameObjectInstance.m_minRotationOffsetY + spawnInfo.m_spawnRotationY, resourceProtoGameObjectInstance.m_maxRotationOffsetY + spawnInfo.m_spawnRotationY), spawnInfo.m_spawner.GetRandomFloat(resourceProtoGameObjectInstance.m_minRotationOffsetZ, resourceProtoGameObjectInstance.m_maxRotationOffsetZ)));
									if (resourceProtoGameObject.m_instances[k].m_rotateToSlope)
									{
										gameObject2.transform.rotation = Quaternion.FromToRotation(gameObject2.transform.up, spawnInfo2.m_terrainNormalWU) * gameObject2.transform.rotation;
									}
									if (this.m_spawnParent != null)
									{
										gameObject2.transform.parent = this.m_spawnParent;
									}
									GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
									if (resourceProtoGameObjectInstance.m_extParam.ToLower().Contains("nograss"))
									{
										gameObject3.name = "_GaiaCollider_NoGrass";
									}
									else
									{
										gameObject3.name = "_GaiaCollider_Grass";
									}
									gameObject3.transform.position = vector;
									gameObject3.GetComponent<MeshRenderer>().enabled = false;
									float num9 = resourceProtoGameObjectInstance.m_localBounds * num7;
									gameObject3.transform.localScale = new Vector3(num9, num9, num9);
									gameObject3.AddComponent<SphereCollider>();
									gameObject3.layer = spawnInfo.m_spawner.m_spawnColliderLayer;
									if (spawnInfo.m_spawner.m_areaBoundsColliderCache == null)
									{
										spawnInfo.m_spawner.m_areaBoundsColliderCache = new GameObject("Bounds_ColliderCache");
										spawnInfo.m_spawner.m_areaBoundsColliderCache.transform.parent = spawnInfo.m_spawner.transform;
									}
									gameObject3.transform.parent = spawnInfo.m_spawner.m_areaBoundsColliderCache.transform;
								}
							}
						}
					}
					GameObject gameObject4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					gameObject4.name = this.m_colliderName;
					gameObject4.transform.position = spawnInfo.m_hitLocationWU;
					gameObject4.GetComponent<MeshRenderer>().enabled = false;
					gameObject4.transform.localScale = new Vector3(num6, num6, num6);
					gameObject4.AddComponent<SphereCollider>();
					gameObject4.layer = spawnInfo.m_spawner.m_spawnColliderLayer;
					if (spawnInfo.m_spawner.m_areaBoundsColliderCache == null)
					{
						spawnInfo.m_spawner.m_areaBoundsColliderCache = new GameObject("Bounds_ColliderCache");
						spawnInfo.m_spawner.m_areaBoundsColliderCache.transform.parent = spawnInfo.m_spawner.transform;
					}
					gameObject4.transform.parent = spawnInfo.m_spawner.m_areaBoundsColliderCache.transform;
					break;
				}
				}
				if (array != null)
				{
					for (int i = 0; i < array.GetLength(0); i++)
					{
						SpawnRuleExtension spawnRuleExtension = array[i];
						if (spawnRuleExtension != null)
						{
							spawnRuleExtension.PostSpawn(this, ref spawnInfo);
						}
					}
				}
			}
		}

		public bool CacheHeightMaps(Spawner spawner)
		{
			return !this.m_isActive && false;
		}

		public bool CacheTextures(Spawner spawner)
		{
			if (!this.m_isActive)
			{
				return false;
			}
			if (this.m_resourceType == GaiaConstants.SpawnerResourceType.TerrainTexture)
			{
				return true;
			}
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
			{
				SpawnRuleExtension[] spawnExtensions = spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].m_spawnExtensions;
				if (spawnExtensions != null && spawnExtensions.GetLength(0) > 0)
				{
					for (int i = 0; i < spawnExtensions.GetLength(0); i++)
					{
						SpawnRuleExtension spawnRuleExtension = spawnExtensions[i];
						if (spawnRuleExtension != null && spawnRuleExtension.AffectsTextures())
						{
							return true;
						}
					}
				}
				return spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].ChecksTextures();
			}
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
			{
				SpawnRuleExtension[] spawnExtensions = spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_spawnExtensions;
				if (spawnExtensions != null && spawnExtensions.GetLength(0) > 0)
				{
					for (int j = 0; j < spawnExtensions.GetLength(0); j++)
					{
						SpawnRuleExtension spawnRuleExtension = spawnExtensions[j];
						if (spawnRuleExtension != null && spawnRuleExtension.AffectsTextures())
						{
							return true;
						}
					}
				}
				return spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].ChecksTextures();
			}
			case GaiaConstants.SpawnerResourceType.TerrainTree:
			{
				SpawnRuleExtension[] spawnExtensions = spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_spawnExtensions;
				if (spawnExtensions != null && spawnExtensions.GetLength(0) > 0)
				{
					for (int k = 0; k < spawnExtensions.GetLength(0); k++)
					{
						SpawnRuleExtension spawnRuleExtension = spawnExtensions[k];
						if (spawnRuleExtension != null && spawnRuleExtension.AffectsTextures())
						{
							return true;
						}
					}
				}
				return spawner.m_resources.m_treePrototypes[this.m_resourceIdx].ChecksTextures();
			}
			case GaiaConstants.SpawnerResourceType.GameObject:
			{
				SpawnRuleExtension[] spawnExtensions = spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_spawnExtensions;
				if (spawnExtensions != null && spawnExtensions.GetLength(0) > 0)
				{
					for (int l = 0; l < spawnExtensions.GetLength(0); l++)
					{
						SpawnRuleExtension spawnRuleExtension = spawnExtensions[l];
						if (spawnRuleExtension != null && spawnRuleExtension.AffectsTextures())
						{
							return true;
						}
					}
				}
				return spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].ChecksTextures();
			}
			default:
				return false;
			}
		}

		public bool CacheDetails(Spawner spawner)
		{
			if (!this.m_isActive)
			{
				return false;
			}
			if (this.m_resourceType == GaiaConstants.SpawnerResourceType.TerrainDetail)
			{
				return true;
			}
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
			{
				SpawnRuleExtension[] spawnExtensions = spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].m_spawnExtensions;
				if (spawnExtensions != null && spawnExtensions.GetLength(0) > 0)
				{
					for (int i = 0; i < spawnExtensions.GetLength(0); i++)
					{
						SpawnRuleExtension spawnRuleExtension = spawnExtensions[i];
						if (spawnRuleExtension != null && spawnRuleExtension.AffectsDetails())
						{
							return true;
						}
					}
				}
				return spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].ChecksTextures();
			}
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
			{
				SpawnRuleExtension[] spawnExtensions = spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].m_spawnExtensions;
				if (spawnExtensions != null && spawnExtensions.GetLength(0) > 0)
				{
					for (int j = 0; j < spawnExtensions.GetLength(0); j++)
					{
						SpawnRuleExtension spawnRuleExtension = spawnExtensions[j];
						if (spawnRuleExtension != null && spawnRuleExtension.AffectsDetails())
						{
							return true;
						}
					}
				}
				return spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].ChecksTextures();
			}
			case GaiaConstants.SpawnerResourceType.TerrainTree:
			{
				SpawnRuleExtension[] spawnExtensions = spawner.m_resources.m_treePrototypes[this.m_resourceIdx].m_spawnExtensions;
				if (spawnExtensions != null && spawnExtensions.GetLength(0) > 0)
				{
					for (int k = 0; k < spawnExtensions.GetLength(0); k++)
					{
						SpawnRuleExtension spawnRuleExtension = spawnExtensions[k];
						if (spawnRuleExtension != null && spawnRuleExtension.AffectsDetails())
						{
							return true;
						}
					}
				}
				return spawner.m_resources.m_treePrototypes[this.m_resourceIdx].ChecksTextures();
			}
			case GaiaConstants.SpawnerResourceType.GameObject:
			{
				SpawnRuleExtension[] spawnExtensions = spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].m_spawnExtensions;
				if (spawnExtensions != null && spawnExtensions.GetLength(0) > 0)
				{
					for (int l = 0; l < spawnExtensions.GetLength(0); l++)
					{
						SpawnRuleExtension spawnRuleExtension = spawnExtensions[l];
						if (spawnRuleExtension != null && spawnRuleExtension.AffectsDetails())
						{
							return true;
						}
					}
				}
				return spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].ChecksTextures();
			}
			default:
				return false;
			}
		}

		public bool CacheProximity(Spawner spawner)
		{
			if (!this.m_isActive)
			{
				return false;
			}
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				return spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].ChecksProximity();
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				return spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].ChecksProximity();
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				return spawner.m_resources.m_treePrototypes[this.m_resourceIdx].ChecksProximity();
			case GaiaConstants.SpawnerResourceType.GameObject:
				return spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].ChecksProximity();
			default:
				return false;
			}
		}

		public void AddProximityTags(Spawner spawner, ref List<string> tagList)
		{
			if (!this.m_isActive)
			{
				return;
			}
			switch (this.m_resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				spawner.m_resources.m_texturePrototypes[this.m_resourceIdx].AddTags(ref tagList);
				break;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				spawner.m_resources.m_detailPrototypes[this.m_resourceIdx].AddTags(ref tagList);
				break;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				spawner.m_resources.m_treePrototypes[this.m_resourceIdx].AddTags(ref tagList);
				break;
			case GaiaConstants.SpawnerResourceType.GameObject:
				spawner.m_resources.m_gameObjectPrototypes[this.m_resourceIdx].AddTags(ref tagList);
				break;
			}
		}

		public string m_name;

		public bool m_useExtendedSpawn;

		public float m_minViableFitness = 0.25f;

		public float m_failureRate;

		public ulong m_maxInstances = 40000000UL;

		public bool m_ignoreMaxInstances;

		public float m_minDirection;

		public float m_maxDirection = 359.9f;

		public GaiaConstants.SpawnerResourceType m_resourceType;

		public int m_resourceIdx;

		[fsIgnore]
		public int m_resourceIdxPhysical;

		[fsIgnore]
		public Transform m_spawnParent;

		[fsIgnore]
		public string m_colliderName = "_GaiaCollider_Grass";

		public GaiaConstants.NoiseType m_noiseMask;

		public float m_noiseMaskSeed;

		public int m_noiseMaskOctaves = 8;

		public float m_noiseMaskPersistence = 0.25f;

		public float m_noiseMaskFrequency = 1f;

		public float m_noiseMaskLacunarity = 1.5f;

		public float m_noiseZoom = 50f;

		public float m_noiseStrength = 1f;

		public bool m_noiseInvert;

		private FractalGenerator m_noiseGenerator;

		public bool m_isActive = true;

		public bool m_isFoldedOut;

		public ulong m_currInstanceCnt;

		public ulong m_activeInstanceCnt;

		public ulong m_inactiveInstanceCnt;
	}
}
