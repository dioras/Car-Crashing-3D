using System;
using System.Collections.Generic;
using Gaia.FullSerializer;
using UnityEngine;

namespace Gaia
{
	[Serializable]
	public class GaiaResource : ScriptableObject
	{
		public bool SetAssetAssociations()
		{
			bool result = false;
			for (int i = 0; i < this.m_texturePrototypes.GetLength(0); i++)
			{
				ResourceProtoTexture resourceProtoTexture = this.m_texturePrototypes[i];
				if (resourceProtoTexture.SetAssetAssociations())
				{
					result = true;
				}
			}
			for (int i = 0; i < this.m_detailPrototypes.GetLength(0); i++)
			{
				ResourceProtoDetail resourceProtoDetail = this.m_detailPrototypes[i];
				if (resourceProtoDetail.SetAssetAssociations())
				{
					result = true;
				}
			}
			for (int i = 0; i < this.m_treePrototypes.GetLength(0); i++)
			{
				ResourceProtoTree resourceProtoTree = this.m_treePrototypes[i];
				if (resourceProtoTree.SetAssetAssociations())
				{
					result = true;
				}
			}
			for (int i = 0; i < this.m_gameObjectPrototypes.GetLength(0); i++)
			{
				ResourceProtoGameObject resourceProtoGameObject = this.m_gameObjectPrototypes[i];
				if (resourceProtoGameObject.SetAssetAssociations())
				{
					result = true;
				}
			}
			return result;
		}

		public bool AssociateAssets()
		{
			bool result = false;
			for (int i = 0; i < this.m_texturePrototypes.GetLength(0); i++)
			{
				ResourceProtoTexture resourceProtoTexture = this.m_texturePrototypes[i];
				if (resourceProtoTexture.AssociateAssets())
				{
					result = true;
				}
			}
			for (int i = 0; i < this.m_detailPrototypes.GetLength(0); i++)
			{
				ResourceProtoDetail resourceProtoDetail = this.m_detailPrototypes[i];
				if (resourceProtoDetail.AssociateAssets())
				{
					result = true;
				}
			}
			for (int i = 0; i < this.m_treePrototypes.GetLength(0); i++)
			{
				ResourceProtoTree resourceProtoTree = this.m_treePrototypes[i];
				if (resourceProtoTree.AssociateAssets())
				{
					result = true;
				}
			}
			for (int i = 0; i < this.m_gameObjectPrototypes.GetLength(0); i++)
			{
				ResourceProtoGameObject resourceProtoGameObject = this.m_gameObjectPrototypes[i];
				if (resourceProtoGameObject.AssociateAssets())
				{
					result = true;
				}
			}
			return result;
		}

		public void DeletePrototypes()
		{
			this.m_texturePrototypes = new ResourceProtoTexture[0];
			this.m_detailPrototypes = new ResourceProtoDetail[0];
			this.m_treePrototypes = new ResourceProtoTree[0];
			this.m_gameObjectPrototypes = new ResourceProtoGameObject[0];
		}

		public bool PrototypesMissingFromTerrain()
		{
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain == null)
			{
				UnityEngine.Debug.LogWarning("Could not check assets in terrain as no terrain has been supplied.");
				return false;
			}
			for (int i = 0; i < this.m_texturePrototypes.GetLength(0); i++)
			{
				if (this.PrototypeMissingFromTerrain(GaiaConstants.SpawnerResourceType.TerrainTexture, i))
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_detailPrototypes.GetLength(0); i++)
			{
				if (this.PrototypeMissingFromTerrain(GaiaConstants.SpawnerResourceType.TerrainDetail, i))
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_treePrototypes.GetLength(0); i++)
			{
				if (this.PrototypeMissingFromTerrain(GaiaConstants.SpawnerResourceType.TerrainTree, i))
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_gameObjectPrototypes.GetLength(0); i++)
			{
				if (this.PrototypeMissingFromTerrain(GaiaConstants.SpawnerResourceType.GameObject, i))
				{
					return true;
				}
			}
			return false;
		}

		public bool PrototypeMissingFromTerrain(GaiaConstants.SpawnerResourceType resourceType, int resourceIdx)
		{
			return this.PrototypeIdxInTerrain(resourceType, resourceIdx) == -1;
		}

		public int PrototypeIdxInTerrain(GaiaConstants.SpawnerResourceType resourceType, int resourceIdx)
		{
			int result = -1;
			int num = 0;
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain == null)
			{
				return result;
			}
			if (this.ResourceIdxOutOfBounds(resourceType, resourceIdx))
			{
				return result;
			}
			if (!this.ResourceIsInUnity(resourceType, resourceIdx))
			{
				return result;
			}
			switch (resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
			{
				ResourceProtoTexture resourceProtoTexture = this.m_texturePrototypes[resourceIdx];
				foreach (SplatPrototype splatPrototype in activeTerrain.terrainData.splatPrototypes)
				{
					if (Utils.IsSameTexture(resourceProtoTexture.m_texture, splatPrototype.texture, false))
					{
						return num;
					}
					num++;
				}
				return result;
			}
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
			{
				ResourceProtoDetail resourceProtoDetail = this.m_detailPrototypes[resourceIdx];
				foreach (DetailPrototype detailPrototype in activeTerrain.terrainData.detailPrototypes)
				{
					if (resourceProtoDetail.m_renderMode == detailPrototype.renderMode)
					{
						if (Utils.IsSameTexture(resourceProtoDetail.m_detailTexture, detailPrototype.prototypeTexture, false))
						{
							return num;
						}
						if (Utils.IsSameGameObject(resourceProtoDetail.m_detailProtoype, detailPrototype.prototype, false))
						{
							return num;
						}
					}
					num++;
				}
				return result;
			}
			case GaiaConstants.SpawnerResourceType.TerrainTree:
			{
				ResourceProtoTree resourceProtoTree = this.m_treePrototypes[resourceIdx];
				foreach (TreePrototype treePrototype in activeTerrain.terrainData.treePrototypes)
				{
					if (Utils.IsSameGameObject(resourceProtoTree.m_desktopPrefab, treePrototype.prefab, false))
					{
						return num;
					}
					num++;
				}
				return result;
			}
			case GaiaConstants.SpawnerResourceType.GameObject:
				return resourceIdx;
			default:
				return result;
			}
		}

		public bool ResourceIdxOutOfBounds(GaiaConstants.SpawnerResourceType resourceType, int resourceIdx)
		{
			switch (resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				return resourceIdx < 0 || resourceIdx >= this.m_texturePrototypes.GetLength(0);
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				return resourceIdx < 0 || resourceIdx >= this.m_detailPrototypes.GetLength(0);
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				return resourceIdx < 0 || resourceIdx >= this.m_treePrototypes.GetLength(0);
			case GaiaConstants.SpawnerResourceType.GameObject:
				return resourceIdx < 0 || resourceIdx >= this.m_gameObjectPrototypes.GetLength(0);
			default:
				return true;
			}
		}

		public bool ResourceIsInUnity(GaiaConstants.SpawnerResourceType resourceType, int resourceIdx)
		{
			if (this.ResourceIdxOutOfBounds(resourceType, resourceIdx))
			{
				return false;
			}
			switch (resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
			{
				ResourceProtoTexture resourceProtoTexture = this.m_texturePrototypes[resourceIdx];
				return !(resourceProtoTexture.m_texture == null);
			}
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
			{
				ResourceProtoDetail resourceProtoDetail = this.m_detailPrototypes[resourceIdx];
				return !(resourceProtoDetail.m_detailTexture == null) || !(resourceProtoDetail.m_detailProtoype == null);
			}
			case GaiaConstants.SpawnerResourceType.TerrainTree:
			{
				ResourceProtoTree resourceProtoTree = this.m_treePrototypes[resourceIdx];
				return !(resourceProtoTree.m_desktopPrefab == null);
			}
			case GaiaConstants.SpawnerResourceType.GameObject:
			{
				ResourceProtoGameObject resourceProtoGameObject = this.m_gameObjectPrototypes[resourceIdx];
				return resourceProtoGameObject.m_instances[0] != null;
			}
			default:
				return false;
			}
		}

		public void UpdatePrototypesFromTerrain()
		{
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain == null)
			{
				UnityEngine.Debug.LogWarning("Can not update prototypes from the terrain as there is no terrain currently active in this scene.");
				return;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			this.m_terrainHeight = activeTerrain.terrainData.size.y;
			List<ResourceProtoTexture> list = new List<ResourceProtoTexture>(this.m_texturePrototypes);
			while (list.Count > activeTerrain.terrainData.splatPrototypes.Length)
			{
				list.RemoveAt(list.Count - 1);
			}
			for (int i = 0; i < activeTerrain.terrainData.splatPrototypes.Length; i++)
			{
				SplatPrototype splatPrototype = activeTerrain.terrainData.splatPrototypes[i];
				ResourceProtoTexture resourceProtoTexture;
				if (i < list.Count)
				{
					resourceProtoTexture = list[i];
				}
				else
				{
					resourceProtoTexture = new ResourceProtoTexture();
					list.Add(resourceProtoTexture);
				}
				resourceProtoTexture.m_name = this.GetUniqueName(splatPrototype.texture.name, ref dictionary);
				resourceProtoTexture.m_texture = splatPrototype.texture;
				resourceProtoTexture.m_normal = splatPrototype.normalMap;
				resourceProtoTexture.m_offsetX = splatPrototype.tileOffset.x;
				resourceProtoTexture.m_offsetY = splatPrototype.tileOffset.y;
				resourceProtoTexture.m_sizeX = splatPrototype.tileSize.x;
				resourceProtoTexture.m_sizeY = splatPrototype.tileSize.y;
				resourceProtoTexture.m_metalic = splatPrototype.metallic;
				resourceProtoTexture.m_smoothness = splatPrototype.smoothness;
				if (resourceProtoTexture.m_spawnCriteria.Length == 0)
				{
					resourceProtoTexture.m_spawnCriteria = new SpawnCritera[1];
					SpawnCritera spawnCritera = new SpawnCritera();
					spawnCritera.m_isActive = true;
					spawnCritera.m_virginTerrain = false;
					spawnCritera.m_checkType = GaiaConstants.SpawnerLocationCheckType.PointCheck;
					switch (i)
					{
					case 0:
						spawnCritera.m_checkHeight = true;
						spawnCritera.m_minHeight = this.m_seaLevel * -1f;
						spawnCritera.m_maxHeight = this.m_terrainHeight - this.m_seaLevel;
						spawnCritera.m_heightFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 1f),
							new Keyframe(1f, 1f)
						});
						spawnCritera.m_checkSlope = false;
						spawnCritera.m_minSlope = 0f;
						spawnCritera.m_maxSlope = 90f;
						spawnCritera.m_slopeFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 1f),
							new Keyframe(1f, 0f)
						});
						spawnCritera.m_checkProximity = false;
						spawnCritera.m_checkTexture = false;
						break;
					case 1:
						spawnCritera.m_checkHeight = true;
						spawnCritera.m_minHeight = 1f;
						spawnCritera.m_maxHeight = this.m_terrainHeight - this.m_seaLevel;
						spawnCritera.m_heightFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f),
							new Keyframe(0.01f, 1f),
							new Keyframe(1f, 1f)
						});
						spawnCritera.m_checkSlope = false;
						spawnCritera.m_minSlope = 0f;
						spawnCritera.m_maxSlope = 90f;
						spawnCritera.m_slopeFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 1f),
							new Keyframe(1f, 0f)
						});
						spawnCritera.m_checkProximity = false;
						spawnCritera.m_checkTexture = false;
						break;
					case 2:
						spawnCritera.m_checkHeight = true;
						spawnCritera.m_minHeight = 2f;
						spawnCritera.m_maxHeight = this.m_terrainHeight - this.m_seaLevel;
						spawnCritera.m_heightFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f),
							new Keyframe(0.02f, 1f),
							new Keyframe(1f, 1f)
						});
						spawnCritera.m_checkSlope = true;
						spawnCritera.m_minSlope = 0f;
						spawnCritera.m_maxSlope = 90f;
						spawnCritera.m_slopeFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f),
							new Keyframe(0.1f, 1f),
							new Keyframe(1f, 1f)
						});
						spawnCritera.m_checkProximity = false;
						spawnCritera.m_checkTexture = false;
						break;
					case 3:
						spawnCritera.m_checkHeight = false;
						spawnCritera.m_minHeight = this.m_seaLevel * -1f;
						spawnCritera.m_maxHeight = this.m_terrainHeight - this.m_seaLevel;
						spawnCritera.m_heightFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 1f),
							new Keyframe(1f, 1f)
						});
						spawnCritera.m_checkSlope = true;
						spawnCritera.m_minSlope = 15f;
						spawnCritera.m_maxSlope = 90f;
						spawnCritera.m_slopeFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f),
							new Keyframe(0.2f, 1f),
							new Keyframe(1f, 1f)
						});
						spawnCritera.m_checkProximity = false;
						spawnCritera.m_checkTexture = false;
						break;
					default:
						spawnCritera.m_isActive = false;
						spawnCritera.m_checkHeight = false;
						spawnCritera.m_minHeight = UnityEngine.Random.Range(this.m_beachHeight - this.m_beachHeight / 4f, this.m_beachHeight * 2f);
						spawnCritera.m_maxHeight = this.m_terrainHeight - this.m_seaLevel;
						spawnCritera.m_heightFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 1f),
							new Keyframe(1f, 1f)
						});
						spawnCritera.m_checkSlope = false;
						spawnCritera.m_minSlope = 0f;
						spawnCritera.m_maxSlope = 90f;
						spawnCritera.m_slopeFitness = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 1f),
							new Keyframe(1f, 0f)
						});
						spawnCritera.m_checkProximity = false;
						spawnCritera.m_checkTexture = false;
						break;
					}
					resourceProtoTexture.m_spawnCriteria[0] = spawnCritera;
				}
			}
			this.m_texturePrototypes = list.ToArray();
			dictionary.Clear();
			List<ResourceProtoDetail> list2 = new List<ResourceProtoDetail>(this.m_detailPrototypes);
			while (list2.Count > activeTerrain.terrainData.detailPrototypes.Length)
			{
				list2.RemoveAt(list2.Count - 1);
			}
			for (int i = 0; i < activeTerrain.terrainData.detailPrototypes.Length; i++)
			{
				DetailPrototype detailPrototype = activeTerrain.terrainData.detailPrototypes[i];
				ResourceProtoDetail resourceProtoDetail;
				if (i < list2.Count)
				{
					resourceProtoDetail = list2[i];
				}
				else
				{
					resourceProtoDetail = new ResourceProtoDetail();
					list2.Add(resourceProtoDetail);
				}
				resourceProtoDetail.m_renderMode = detailPrototype.renderMode;
				if (detailPrototype.prototype != null)
				{
					resourceProtoDetail.m_name = this.GetUniqueName(detailPrototype.prototype.name, ref dictionary);
					resourceProtoDetail.m_detailProtoype = detailPrototype.prototype;
				}
				else
				{
					resourceProtoDetail.m_name = this.GetUniqueName(detailPrototype.prototypeTexture.name, ref dictionary);
					resourceProtoDetail.m_detailTexture = detailPrototype.prototypeTexture;
				}
				resourceProtoDetail.m_dryColour = detailPrototype.dryColor;
				resourceProtoDetail.m_healthyColour = detailPrototype.healthyColor;
				resourceProtoDetail.m_maxHeight = detailPrototype.maxHeight;
				resourceProtoDetail.m_maxWidth = detailPrototype.maxWidth;
				resourceProtoDetail.m_minHeight = detailPrototype.minHeight;
				resourceProtoDetail.m_minWidth = detailPrototype.minWidth;
				resourceProtoDetail.m_noiseSpread = detailPrototype.noiseSpread;
				resourceProtoDetail.m_bendFactor = detailPrototype.bendFactor;
				if (resourceProtoDetail.m_dna == null)
				{
					resourceProtoDetail.m_dna = new ResourceProtoDNA();
				}
				resourceProtoDetail.m_dna.m_rndScaleInfluence = false;
				resourceProtoDetail.m_dna.Update(i, resourceProtoDetail.m_maxWidth, resourceProtoDetail.m_maxHeight, 0.1f, 1f);
				if (resourceProtoDetail.m_spawnCriteria.Length == 0)
				{
					resourceProtoDetail.m_spawnCriteria = new SpawnCritera[1];
					SpawnCritera spawnCritera = new SpawnCritera();
					spawnCritera.m_isActive = true;
					spawnCritera.m_virginTerrain = true;
					spawnCritera.m_checkType = GaiaConstants.SpawnerLocationCheckType.PointCheck;
					spawnCritera.m_checkHeight = true;
					spawnCritera.m_minHeight = UnityEngine.Random.Range(this.m_beachHeight * 0.25f, this.m_beachHeight);
					spawnCritera.m_maxHeight = this.m_terrainHeight - this.m_seaLevel;
					spawnCritera.m_heightFitness = new AnimationCurve(new Keyframe[]
					{
						new Keyframe(0f, 0f),
						new Keyframe(0.05f, 1f),
						new Keyframe(1f, 0f)
					});
					spawnCritera.m_checkSlope = true;
					spawnCritera.m_minSlope = 0f;
					spawnCritera.m_maxSlope = UnityEngine.Random.Range(25f, 40f);
					spawnCritera.m_slopeFitness = new AnimationCurve(new Keyframe[]
					{
						new Keyframe(0f, 0f),
						new Keyframe(0.05f, 1f),
						new Keyframe(1f, 0f)
					});
					spawnCritera.m_checkProximity = false;
					spawnCritera.m_checkTexture = false;
					resourceProtoDetail.m_spawnCriteria[0] = spawnCritera;
				}
			}
			this.m_detailPrototypes = list2.ToArray();
			dictionary.Clear();
			List<ResourceProtoTree> list3 = new List<ResourceProtoTree>(this.m_treePrototypes);
			while (list3.Count > activeTerrain.terrainData.treePrototypes.Length)
			{
				list3.RemoveAt(list3.Count - 1);
			}
			for (int i = 0; i < activeTerrain.terrainData.treePrototypes.Length; i++)
			{
				TreePrototype treePrototype = activeTerrain.terrainData.treePrototypes[i];
				ResourceProtoTree resourceProtoTree;
				if (i < list3.Count)
				{
					resourceProtoTree = list3[i];
				}
				else
				{
					resourceProtoTree = new ResourceProtoTree();
					list3.Add(resourceProtoTree);
				}
				resourceProtoTree.m_name = this.GetUniqueName(treePrototype.prefab.name, ref dictionary);
				resourceProtoTree.m_desktopPrefab = (resourceProtoTree.m_mobilePrefab = treePrototype.prefab);
				resourceProtoTree.m_bendFactor = treePrototype.bendFactor;
				if (resourceProtoTree.m_dna == null)
				{
					resourceProtoTree.m_dna = new ResourceProtoDNA();
					resourceProtoTree.m_dna.Update(i);
				}
				this.UpdateDNA(treePrototype.prefab, ref resourceProtoTree.m_dna);
				resourceProtoTree.m_dna.m_boundsRadius = resourceProtoTree.m_dna.m_width * 0.25f;
				resourceProtoTree.m_dna.m_seedThrow = resourceProtoTree.m_dna.m_height * 1.5f;
				if (resourceProtoTree.m_spawnCriteria.Length == 0)
				{
					resourceProtoTree.m_spawnCriteria = new SpawnCritera[1];
					SpawnCritera spawnCritera = new SpawnCritera();
					spawnCritera.m_isActive = true;
					spawnCritera.m_virginTerrain = true;
					spawnCritera.m_checkType = GaiaConstants.SpawnerLocationCheckType.PointCheck;
					spawnCritera.m_checkHeight = true;
					spawnCritera.m_minHeight = 0f;
					spawnCritera.m_maxHeight = this.m_terrainHeight - this.m_seaLevel;
					spawnCritera.m_heightFitness = new AnimationCurve(new Keyframe[]
					{
						new Keyframe(0f, 1f),
						new Keyframe(1f, 0f)
					});
					spawnCritera.m_checkSlope = true;
					spawnCritera.m_minSlope = 0f;
					spawnCritera.m_maxSlope = UnityEngine.Random.Range(25f, 40f);
					spawnCritera.m_slopeFitness = new AnimationCurve(new Keyframe[]
					{
						new Keyframe(0f, 1f),
						new Keyframe(1f, 0f)
					});
					spawnCritera.m_checkProximity = false;
					spawnCritera.m_checkTexture = false;
					resourceProtoTree.m_spawnCriteria[0] = spawnCritera;
				}
			}
			this.m_treePrototypes = list3.ToArray();
			this.SetAssetAssociations();
		}

		private string GetUniqueName(string name, ref Dictionary<string, string> names)
		{
			int num = 0;
			string text = name;
			while (names.ContainsKey(text))
			{
				text = name + " " + num.ToString();
				num++;
			}
			names.Add(text, text);
			return text;
		}

		private void UpdateDNA(GameObject prefab, ref ResourceProtoDNA dna)
		{
			if (prefab != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
				Bounds bounds = new Bounds(gameObject.transform.position, Vector3.zero);
				foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
				{
					bounds.Encapsulate(renderer.bounds);
				}
				foreach (Collider collider in gameObject.GetComponentsInChildren<Collider>())
				{
					bounds.Encapsulate(collider.bounds);
				}
				UnityEngine.Object.DestroyImmediate(gameObject);
				dna.Update(dna.m_protoIdx, bounds.size.x, bounds.size.y);
			}
		}

		public void ChangeHeight(float oldHeight, float newHeight)
		{
			float num = oldHeight - this.m_seaLevel;
			float maxHeight = newHeight - this.m_seaLevel;
			for (int i = 0; i < this.m_texturePrototypes.Length; i++)
			{
				foreach (SpawnCritera spawnCritera in this.m_texturePrototypes[i].m_spawnCriteria)
				{
					if (spawnCritera.m_maxHeight == num)
					{
						spawnCritera.m_maxHeight = maxHeight;
					}
				}
			}
			for (int k = 0; k < this.m_detailPrototypes.Length; k++)
			{
				foreach (SpawnCritera spawnCritera in this.m_detailPrototypes[k].m_spawnCriteria)
				{
					if (spawnCritera.m_maxHeight == num)
					{
						spawnCritera.m_maxHeight = maxHeight;
					}
				}
			}
			for (int m = 0; m < this.m_treePrototypes.Length; m++)
			{
				foreach (SpawnCritera spawnCritera in this.m_treePrototypes[m].m_spawnCriteria)
				{
					if (spawnCritera.m_maxHeight == num)
					{
						spawnCritera.m_maxHeight = maxHeight;
					}
				}
			}
			for (int num2 = 0; num2 < this.m_gameObjectPrototypes.Length; num2++)
			{
				foreach (SpawnCritera spawnCritera in this.m_gameObjectPrototypes[num2].m_spawnCriteria)
				{
					if (spawnCritera.m_maxHeight == num)
					{
						spawnCritera.m_maxHeight = maxHeight;
					}
				}
			}
		}

		public void ChangeSeaLevel(float newSeaLevel)
		{
			if (newSeaLevel != this.m_seaLevel)
			{
				this.ChangeSeaLevel(this.m_seaLevel, newSeaLevel);
			}
		}

		public void ChangeSeaLevel(float oldSeaLevel, float newSeaLevel)
		{
			float num = oldSeaLevel * -1f;
			float minHeight = newSeaLevel * -1f;
			float num2 = this.m_terrainHeight - oldSeaLevel;
			float maxHeight = this.m_terrainHeight - newSeaLevel;
			for (int i = 0; i < this.m_texturePrototypes.Length; i++)
			{
				foreach (SpawnCritera spawnCritera in this.m_texturePrototypes[i].m_spawnCriteria)
				{
					if (spawnCritera.m_minHeight == num)
					{
						spawnCritera.m_minHeight = minHeight;
					}
					if (spawnCritera.m_maxHeight == num2)
					{
						spawnCritera.m_maxHeight = maxHeight;
					}
				}
			}
			for (int k = 0; k < this.m_detailPrototypes.Length; k++)
			{
				foreach (SpawnCritera spawnCritera in this.m_detailPrototypes[k].m_spawnCriteria)
				{
					if (spawnCritera.m_minHeight == num)
					{
						spawnCritera.m_minHeight = minHeight;
					}
					if (spawnCritera.m_maxHeight == num2)
					{
						spawnCritera.m_maxHeight = maxHeight;
					}
				}
			}
			for (int m = 0; m < this.m_treePrototypes.Length; m++)
			{
				foreach (SpawnCritera spawnCritera in this.m_treePrototypes[m].m_spawnCriteria)
				{
					if (spawnCritera.m_minHeight == num)
					{
						spawnCritera.m_minHeight = minHeight;
					}
					if (spawnCritera.m_maxHeight == num2)
					{
						spawnCritera.m_maxHeight = maxHeight;
					}
				}
			}
			for (int num3 = 0; num3 < this.m_gameObjectPrototypes.Length; num3++)
			{
				foreach (SpawnCritera spawnCritera in this.m_gameObjectPrototypes[num3].m_spawnCriteria)
				{
					if (spawnCritera.m_minHeight == num)
					{
						spawnCritera.m_minHeight = minHeight;
					}
					if (spawnCritera.m_maxHeight == num2)
					{
						spawnCritera.m_maxHeight = maxHeight;
					}
				}
			}
			this.m_seaLevel = newSeaLevel;
		}

		public void ApplyPrototypesToTerrain()
		{
			this.AssociateAssets();
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				this.ApplyPrototypesToTerrain(terrain);
			}
		}

		public void ApplyPrototypesToTerrain(Terrain terrain)
		{
			if (terrain == null)
			{
				UnityEngine.Debug.LogWarning("Can not apply assets to terrain no terrain has been supplied.");
				return;
			}
			List<SplatPrototype> list = new List<SplatPrototype>();
			foreach (ResourceProtoTexture resourceProtoTexture in this.m_texturePrototypes)
			{
				if (resourceProtoTexture.m_texture != null)
				{
					list.Add(new SplatPrototype
					{
						normalMap = resourceProtoTexture.m_normal,
						tileOffset = new Vector2(resourceProtoTexture.m_offsetX, resourceProtoTexture.m_offsetY),
						tileSize = new Vector2(resourceProtoTexture.m_sizeX, resourceProtoTexture.m_sizeY),
						texture = resourceProtoTexture.m_texture
					});
				}
				else
				{
					UnityEngine.Debug.LogWarning("Unable to find resource for " + resourceProtoTexture.m_name + "... ignoring.");
				}
			}
			terrain.terrainData.splatPrototypes = list.ToArray();
			List<DetailPrototype> list2 = new List<DetailPrototype>();
			foreach (ResourceProtoDetail resourceProtoDetail in this.m_detailPrototypes)
			{
				if (resourceProtoDetail.m_detailProtoype != null || resourceProtoDetail.m_detailTexture != null)
				{
					DetailPrototype detailPrototype = new DetailPrototype();
					detailPrototype.renderMode = resourceProtoDetail.m_renderMode;
					if (resourceProtoDetail.m_detailProtoype != null)
					{
						detailPrototype.usePrototypeMesh = true;
						detailPrototype.prototype = resourceProtoDetail.m_detailProtoype;
					}
					else
					{
						detailPrototype.usePrototypeMesh = false;
						detailPrototype.prototypeTexture = resourceProtoDetail.m_detailTexture;
					}
					detailPrototype.dryColor = resourceProtoDetail.m_dryColour;
					detailPrototype.healthyColor = resourceProtoDetail.m_healthyColour;
					detailPrototype.maxHeight = resourceProtoDetail.m_maxHeight;
					detailPrototype.maxWidth = resourceProtoDetail.m_maxWidth;
					detailPrototype.minHeight = resourceProtoDetail.m_minHeight;
					detailPrototype.minWidth = resourceProtoDetail.m_minWidth;
					detailPrototype.noiseSpread = resourceProtoDetail.m_noiseSpread;
					detailPrototype.bendFactor = resourceProtoDetail.m_bendFactor;
					list2.Add(detailPrototype);
				}
				else
				{
					UnityEngine.Debug.LogWarning("Unable to find resource for " + resourceProtoDetail.m_name + "... ignoring.");
				}
			}
			terrain.terrainData.detailPrototypes = list2.ToArray();
			List<TreePrototype> list3 = new List<TreePrototype>();
			foreach (ResourceProtoTree resourceProtoTree in this.m_treePrototypes)
			{
				if (resourceProtoTree.m_desktopPrefab != null)
				{
					list3.Add(new TreePrototype
					{
						bendFactor = resourceProtoTree.m_bendFactor,
						prefab = resourceProtoTree.m_desktopPrefab
					});
				}
				else
				{
					UnityEngine.Debug.LogWarning("Unable to find resource for " + resourceProtoTree.m_name + "... ignoring.");
				}
			}
			terrain.terrainData.treePrototypes = list3.ToArray();
			terrain.Flush();
		}

		public void AddMissingPrototypesToTerrain()
		{
			this.AssociateAssets();
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				this.AddMissingPrototypesToTerrain(terrain);
			}
		}

		public void AddMissingPrototypesToTerrain(Terrain terrain)
		{
			if (terrain == null)
			{
				UnityEngine.Debug.LogWarning("Can not add resources to the terrain as no terrain has been supplied.");
				return;
			}
			bool flag = false;
			List<SplatPrototype> list = new List<SplatPrototype>(terrain.terrainData.splatPrototypes);
			foreach (ResourceProtoTexture resourceProtoTexture in this.m_texturePrototypes)
			{
				flag = false;
				foreach (SplatPrototype splatPrototype in list)
				{
					if (Utils.IsSameTexture(splatPrototype.texture, resourceProtoTexture.m_texture, false))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					list.Add(new SplatPrototype
					{
						normalMap = resourceProtoTexture.m_normal,
						tileOffset = new Vector2(resourceProtoTexture.m_offsetX, resourceProtoTexture.m_offsetY),
						tileSize = new Vector2(resourceProtoTexture.m_sizeX, resourceProtoTexture.m_sizeY),
						texture = resourceProtoTexture.m_texture
					});
				}
			}
			terrain.terrainData.splatPrototypes = list.ToArray();
			List<DetailPrototype> list2 = new List<DetailPrototype>(terrain.terrainData.detailPrototypes);
			foreach (ResourceProtoDetail resourceProtoDetail in this.m_detailPrototypes)
			{
				flag = false;
				foreach (DetailPrototype detailPrototype in list2)
				{
					if (detailPrototype.renderMode == resourceProtoDetail.m_renderMode)
					{
						if (Utils.IsSameTexture(detailPrototype.prototypeTexture, resourceProtoDetail.m_detailTexture, false))
						{
							flag = true;
						}
						if (Utils.IsSameGameObject(detailPrototype.prototype, resourceProtoDetail.m_detailProtoype, false))
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					DetailPrototype detailPrototype2 = new DetailPrototype();
					detailPrototype2.renderMode = resourceProtoDetail.m_renderMode;
					if (resourceProtoDetail.m_detailProtoype != null)
					{
						detailPrototype2.usePrototypeMesh = true;
						detailPrototype2.prototype = resourceProtoDetail.m_detailProtoype;
					}
					else
					{
						detailPrototype2.usePrototypeMesh = false;
						detailPrototype2.prototypeTexture = resourceProtoDetail.m_detailTexture;
					}
					detailPrototype2.dryColor = resourceProtoDetail.m_dryColour;
					detailPrototype2.healthyColor = resourceProtoDetail.m_healthyColour;
					detailPrototype2.maxHeight = resourceProtoDetail.m_maxHeight;
					detailPrototype2.maxWidth = resourceProtoDetail.m_maxWidth;
					detailPrototype2.minHeight = resourceProtoDetail.m_minHeight;
					detailPrototype2.minWidth = resourceProtoDetail.m_minWidth;
					detailPrototype2.noiseSpread = resourceProtoDetail.m_noiseSpread;
					detailPrototype2.bendFactor = resourceProtoDetail.m_bendFactor;
					list2.Add(detailPrototype2);
				}
			}
			terrain.terrainData.detailPrototypes = list2.ToArray();
			List<TreePrototype> list3 = new List<TreePrototype>(terrain.terrainData.treePrototypes);
			foreach (ResourceProtoTree resourceProtoTree in this.m_treePrototypes)
			{
				flag = false;
				foreach (TreePrototype treePrototype in list3)
				{
					if (Utils.IsSameGameObject(treePrototype.prefab, resourceProtoTree.m_desktopPrefab, false))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					list3.Add(new TreePrototype
					{
						bendFactor = resourceProtoTree.m_bendFactor,
						prefab = resourceProtoTree.m_desktopPrefab
					});
				}
			}
			terrain.terrainData.treePrototypes = list3.ToArray();
			terrain.Flush();
		}

		public void AddPrototypeToTerrain(GaiaConstants.SpawnerResourceType resourceType, int resourceIdx)
		{
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				this.AddPrototypeToTerrain(resourceType, resourceIdx, terrain);
			}
		}

		public void AddPrototypeToTerrain(GaiaConstants.SpawnerResourceType resourceType, int resourceIdx, Terrain terrain)
		{
			if (this.ResourceIdxOutOfBounds(resourceType, resourceIdx))
			{
				return;
			}
			if (!this.PrototypeMissingFromTerrain(resourceType, resourceIdx))
			{
				return;
			}
			if (resourceType != GaiaConstants.SpawnerResourceType.TerrainTexture)
			{
				if (resourceType != GaiaConstants.SpawnerResourceType.TerrainDetail)
				{
					if (resourceType == GaiaConstants.SpawnerResourceType.TerrainTree)
					{
						ResourceProtoTree resourceProtoTree = this.m_treePrototypes[resourceIdx];
						List<TreePrototype> list = new List<TreePrototype>(terrain.terrainData.treePrototypes);
						list.Add(new TreePrototype
						{
							bendFactor = resourceProtoTree.m_bendFactor,
							prefab = resourceProtoTree.m_desktopPrefab
						});
						terrain.terrainData.treePrototypes = list.ToArray();
					}
				}
				else
				{
					ResourceProtoDetail resourceProtoDetail = this.m_detailPrototypes[resourceIdx];
					List<DetailPrototype> list2 = new List<DetailPrototype>(terrain.terrainData.detailPrototypes);
					DetailPrototype detailPrototype = new DetailPrototype();
					detailPrototype.renderMode = resourceProtoDetail.m_renderMode;
					if (resourceProtoDetail.m_detailProtoype != null)
					{
						detailPrototype.usePrototypeMesh = true;
						detailPrototype.prototype = resourceProtoDetail.m_detailProtoype;
					}
					else
					{
						detailPrototype.usePrototypeMesh = false;
						detailPrototype.prototypeTexture = resourceProtoDetail.m_detailTexture;
					}
					detailPrototype.dryColor = resourceProtoDetail.m_dryColour;
					detailPrototype.healthyColor = resourceProtoDetail.m_healthyColour;
					detailPrototype.maxHeight = resourceProtoDetail.m_maxHeight;
					detailPrototype.maxWidth = resourceProtoDetail.m_maxWidth;
					detailPrototype.minHeight = resourceProtoDetail.m_minHeight;
					detailPrototype.minWidth = resourceProtoDetail.m_minWidth;
					detailPrototype.noiseSpread = resourceProtoDetail.m_noiseSpread;
					detailPrototype.bendFactor = resourceProtoDetail.m_bendFactor;
					list2.Add(detailPrototype);
					terrain.terrainData.detailPrototypes = list2.ToArray();
				}
			}
			else
			{
				ResourceProtoTexture resourceProtoTexture = this.m_texturePrototypes[resourceIdx];
				List<SplatPrototype> list3 = new List<SplatPrototype>(terrain.terrainData.splatPrototypes);
				list3.Add(new SplatPrototype
				{
					normalMap = resourceProtoTexture.m_normal,
					tileOffset = new Vector2(resourceProtoTexture.m_offsetX, resourceProtoTexture.m_offsetY),
					tileSize = new Vector2(resourceProtoTexture.m_sizeX, resourceProtoTexture.m_sizeY),
					texture = resourceProtoTexture.m_texture
				});
				terrain.terrainData.splatPrototypes = list3.ToArray();
			}
			terrain.Flush();
		}

		public bool ChecksTextures()
		{
			for (int i = 0; i < this.m_texturePrototypes.Length; i++)
			{
				if (this.m_texturePrototypes[i].ChecksTextures())
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_detailPrototypes.Length; i++)
			{
				if (this.m_detailPrototypes[i].ChecksTextures())
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_treePrototypes.Length; i++)
			{
				if (this.m_treePrototypes[i].ChecksTextures())
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_gameObjectPrototypes.Length; i++)
			{
				if (this.m_gameObjectPrototypes[i].ChecksTextures())
				{
					return true;
				}
			}
			return false;
		}

		public bool ChecksProximity()
		{
			for (int i = 0; i < this.m_texturePrototypes.Length; i++)
			{
				if (this.m_texturePrototypes[i].ChecksProximity())
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_detailPrototypes.Length; i++)
			{
				if (this.m_detailPrototypes[i].ChecksProximity())
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_treePrototypes.Length; i++)
			{
				if (this.m_treePrototypes[i].ChecksProximity())
				{
					return true;
				}
			}
			for (int i = 0; i < this.m_gameObjectPrototypes.Length; i++)
			{
				if (this.m_gameObjectPrototypes[i].ChecksProximity())
				{
					return true;
				}
			}
			return false;
		}

		public void AddGameObject(GameObject prefab)
		{
			if (prefab == null)
			{
				UnityEngine.Debug.LogWarning("Can't add null game object");
			}
		}

		public void AddGameObject(List<GameObject> prototypes)
		{
			if (prototypes == null || prototypes.Count < 1)
			{
				UnityEngine.Debug.LogWarning("Can't add null or empty prototypes list");
				return;
			}
		}

		public GameObject CreateCoverageTextureSpawner(float range)
		{
			this.CreateOrFindSessionManager();
			GameObject gameObject = this.CreateOrFindGaia();
			GameObject gameObject2 = new GameObject("Coverage Texture Spawner");
			gameObject2.AddComponent<Spawner>();
			Spawner component = gameObject2.GetComponent<Spawner>();
			component.m_resources = this;
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.position = TerrainHelper.GetActiveTerrainCenter(true);
			component.m_resources = this;
			component.m_mode = GaiaConstants.OperationMode.DesignTime;
			component.m_spawnerShape = GaiaConstants.SpawnerShape.Box;
			component.m_rndGenerator = new XorshiftPlus(component.m_seed);
			component.m_spawnRange = range;
			component.m_spawnFitnessAttenuator = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			component.m_spawnRuleSelector = GaiaConstants.SpawnerRuleSelector.All;
			component.m_spawnLocationAlgorithm = GaiaConstants.SpawnerLocation.EveryLocation;
			component.m_spawnCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			component.m_locationIncrement = 1f;
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain != null)
			{
				component.m_locationIncrement = Mathf.Clamp(Mathf.Min(activeTerrain.terrainData.size.x, activeTerrain.terrainData.size.z) / (float)activeTerrain.terrainData.alphamapWidth, 0.05f, 1f);
			}
			for (int i = 0; i < this.m_texturePrototypes.Length; i++)
			{
				SpawnRule spawnRule = new SpawnRule();
				spawnRule.m_name = this.m_texturePrototypes[i].m_name;
				spawnRule.m_resourceType = GaiaConstants.SpawnerResourceType.TerrainTexture;
				spawnRule.m_resourceIdx = i;
				spawnRule.m_minViableFitness = 0f;
				spawnRule.m_failureRate = 0f;
				spawnRule.m_maxInstances = (ulong)(range * 2f * (range * 2f));
				spawnRule.m_isActive = true;
				spawnRule.m_isFoldedOut = false;
				spawnRule.m_ignoreMaxInstances = true;
				spawnRule.m_useExtendedSpawn = false;
				component.m_activeRuleCnt++;
				component.m_spawnerRules.Add(spawnRule);
				if (i == 2)
				{
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 1.5f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = (float)UnityEngine.Random.Range(0, 5000);
					spawnRule.m_noiseStrength = 1f;
					spawnRule.m_noiseZoom = 150f;
				}
			}
			SpawnerGroup.SpawnerInstance spawnerInstance = new SpawnerGroup.SpawnerInstance();
			spawnerInstance.m_name = gameObject2.name;
			spawnerInstance.m_interationsPerSpawn = 1;
			spawnerInstance.m_spawner = component;
			return gameObject2;
		}

		public GameObject CreateCoverageDetailSpawner(float range)
		{
			this.CreateOrFindSessionManager();
			GameObject gameObject = this.CreateOrFindGaia();
			GameObject gameObject2 = new GameObject("Coverage Detail Spawner");
			gameObject2.AddComponent<Spawner>();
			Spawner component = gameObject2.GetComponent<Spawner>();
			component.m_resources = this;
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.position = TerrainHelper.GetActiveTerrainCenter(true);
			component.m_resources = this;
			component.m_mode = GaiaConstants.OperationMode.DesignTime;
			component.m_spawnerShape = GaiaConstants.SpawnerShape.Box;
			component.m_rndGenerator = new XorshiftPlus(component.m_seed);
			component.m_spawnRange = range;
			component.m_spawnFitnessAttenuator = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			component.m_spawnRuleSelector = GaiaConstants.SpawnerRuleSelector.All;
			component.m_spawnLocationAlgorithm = GaiaConstants.SpawnerLocation.EveryLocationJittered;
			component.m_spawnCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			component.m_locationIncrement = 1.2f;
			for (int i = 0; i < this.m_detailPrototypes.Length; i++)
			{
				SpawnRule spawnRule = new SpawnRule();
				spawnRule.m_name = this.m_detailPrototypes[i].m_name;
				spawnRule.m_resourceType = GaiaConstants.SpawnerResourceType.TerrainDetail;
				spawnRule.m_resourceIdx = i;
				spawnRule.m_minViableFitness = UnityEngine.Random.Range(0.2f, 0.5f);
				spawnRule.m_failureRate = UnityEngine.Random.Range(0.7f, 0.95f);
				spawnRule.m_maxInstances = (ulong)(range * 2f * (range * 2f));
				spawnRule.m_ignoreMaxInstances = true;
				spawnRule.m_isActive = true;
				spawnRule.m_isFoldedOut = false;
				spawnRule.m_useExtendedSpawn = false;
				switch (i)
				{
				case 0:
					spawnRule.m_minViableFitness = 0.1f;
					spawnRule.m_failureRate = 0f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 2f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 0f;
					spawnRule.m_noiseStrength = 1.5f;
					spawnRule.m_noiseZoom = 50f;
					spawnRule.m_noiseInvert = true;
					break;
				case 1:
					spawnRule.m_minViableFitness = 0.1f;
					spawnRule.m_failureRate = 0.8f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 2f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 0f;
					spawnRule.m_noiseStrength = 1.5f;
					spawnRule.m_noiseZoom = 50f;
					spawnRule.m_noiseInvert = true;
					break;
				case 2:
					spawnRule.m_minViableFitness = 0.4f;
					spawnRule.m_failureRate = 0f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 2f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 0f;
					spawnRule.m_noiseStrength = 1.5f;
					spawnRule.m_noiseZoom = 50f;
					spawnRule.m_noiseInvert = false;
					break;
				case 3:
					spawnRule.m_minViableFitness = 0.2f;
					spawnRule.m_failureRate = 0f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 2f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 0f;
					spawnRule.m_noiseStrength = 1.5f;
					spawnRule.m_noiseZoom = 30f;
					spawnRule.m_noiseInvert = true;
					break;
				case 4:
					spawnRule.m_minViableFitness = 0.5f;
					spawnRule.m_failureRate = 0.65f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 2f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 13390f;
					spawnRule.m_noiseStrength = 1.5f;
					spawnRule.m_noiseZoom = 50f;
					spawnRule.m_noiseInvert = true;
					break;
				case 5:
					spawnRule.m_minViableFitness = 0.4f;
					spawnRule.m_failureRate = 0.3f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 2f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 13390f;
					spawnRule.m_noiseStrength = 1.5f;
					spawnRule.m_noiseZoom = 30f;
					spawnRule.m_noiseInvert = false;
					break;
				case 6:
					spawnRule.m_minViableFitness = 0.5f;
					spawnRule.m_failureRate = 0.9f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 1.5f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 6886f;
					spawnRule.m_noiseStrength = 1f;
					spawnRule.m_noiseZoom = 90f;
					spawnRule.m_noiseInvert = false;
					break;
				default:
					spawnRule.m_isActive = false;
					break;
				}
				component.m_activeRuleCnt++;
				component.m_spawnerRules.Add(spawnRule);
			}
			SpawnerGroup.SpawnerInstance spawnerInstance = new SpawnerGroup.SpawnerInstance();
			spawnerInstance.m_name = gameObject2.name;
			spawnerInstance.m_interationsPerSpawn = 1;
			spawnerInstance.m_spawner = component;
			return gameObject2;
		}

		public GameObject CreateOrFindGaia()
		{
			GameObject gameObject = GameObject.Find("Gaia");
			if (gameObject == null)
			{
				gameObject = new GameObject("Gaia");
			}
			return gameObject;
		}

		public GameObject CreateOrFindSessionManager()
		{
			GaiaSessionManager sessionManager = GaiaSessionManager.GetSessionManager(false);
			this.ChangeSeaLevel(sessionManager.m_session.m_seaLevel);
			return sessionManager.gameObject;
		}

		public GameObject CreateClusteredDetailSpawner(float range)
		{
			this.CreateOrFindSessionManager();
			GameObject gameObject = this.CreateOrFindGaia();
			GameObject gameObject2 = new GameObject("Clustered Detail Spawner");
			gameObject2.AddComponent<Spawner>();
			Spawner component = gameObject2.GetComponent<Spawner>();
			component.m_resources = this;
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.position = TerrainHelper.GetActiveTerrainCenter(true);
			component.m_resources = this;
			component.m_mode = GaiaConstants.OperationMode.DesignTime;
			component.m_spawnerShape = GaiaConstants.SpawnerShape.Box;
			component.m_rndGenerator = new XorshiftPlus(component.m_seed);
			component.m_spawnRange = range;
			component.m_spawnFitnessAttenuator = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			component.m_spawnRuleSelector = GaiaConstants.SpawnerRuleSelector.Random;
			component.m_spawnLocationAlgorithm = GaiaConstants.SpawnerLocation.RandomLocationClustered;
			component.m_locationChecksPerInt = UnityEngine.Random.Range((int)range * 7, (int)range * 10);
			component.m_maxRandomClusterSize = UnityEngine.Random.Range(10, 100);
			component.m_spawnCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			component.m_locationIncrement = 1.5f;
			for (int i = 0; i < this.m_detailPrototypes.Length; i++)
			{
				SpawnRule spawnRule = new SpawnRule();
				spawnRule.m_name = this.m_detailPrototypes[i].m_name;
				spawnRule.m_resourceType = GaiaConstants.SpawnerResourceType.TerrainDetail;
				spawnRule.m_resourceIdx = i;
				spawnRule.m_minViableFitness = UnityEngine.Random.Range(0.3f, 0.6f);
				spawnRule.m_failureRate = UnityEngine.Random.Range(0.1f, 0.3f);
				spawnRule.m_maxInstances = (ulong)(range * 2f * (range * 2f));
				spawnRule.m_ignoreMaxInstances = true;
				spawnRule.m_isActive = false;
				spawnRule.m_isFoldedOut = false;
				spawnRule.m_useExtendedSpawn = false;
				component.m_activeRuleCnt++;
				component.m_spawnerRules.Add(spawnRule);
				if (i > 2)
				{
					spawnRule.m_isActive = true;
				}
			}
			SpawnerGroup.SpawnerInstance spawnerInstance = new SpawnerGroup.SpawnerInstance();
			spawnerInstance.m_name = gameObject2.name;
			spawnerInstance.m_interationsPerSpawn = 1;
			spawnerInstance.m_spawner = component;
			return gameObject2;
		}

		public GameObject CreateClusteredTreeSpawner(float range)
		{
			this.CreateOrFindSessionManager();
			GameObject gameObject = this.CreateOrFindGaia();
			GameObject gameObject2 = new GameObject("Clustered Tree Spawner");
			gameObject2.AddComponent<Spawner>();
			Spawner component = gameObject2.GetComponent<Spawner>();
			component.m_resources = this;
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.position = TerrainHelper.GetActiveTerrainCenter(true);
			component.m_resources = this;
			component.m_mode = GaiaConstants.OperationMode.DesignTime;
			component.m_spawnerShape = GaiaConstants.SpawnerShape.Box;
			component.m_rndGenerator = new XorshiftPlus(component.m_seed);
			component.m_spawnRange = range;
			component.m_spawnFitnessAttenuator = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			component.m_spawnRuleSelector = GaiaConstants.SpawnerRuleSelector.Random;
			component.m_spawnLocationAlgorithm = GaiaConstants.SpawnerLocation.RandomLocationClustered;
			component.m_spawnCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			component.m_locationChecksPerInt = (int)range * 5;
			component.m_maxRandomClusterSize = 30;
			for (int i = 0; i < this.m_treePrototypes.Length; i++)
			{
				SpawnRule spawnRule = new SpawnRule();
				spawnRule.m_name = this.m_treePrototypes[i].m_name;
				spawnRule.m_resourceType = GaiaConstants.SpawnerResourceType.TerrainTree;
				spawnRule.m_resourceIdx = i;
				spawnRule.m_minViableFitness = 0.25f;
				spawnRule.m_failureRate = 0f;
				spawnRule.m_maxInstances = (ulong)(range * range / 5f);
				spawnRule.m_isActive = true;
				spawnRule.m_isFoldedOut = false;
				spawnRule.m_useExtendedSpawn = false;
				component.m_activeRuleCnt++;
				component.m_spawnerRules.Add(spawnRule);
				if (i != 0)
				{
					if (i == 1)
					{
						spawnRule.m_minViableFitness = 0.2f;
						spawnRule.m_failureRate = 0f;
						spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
						spawnRule.m_noiseMaskFrequency = 1f;
						spawnRule.m_noiseMaskLacunarity = 2f;
						spawnRule.m_noiseMaskOctaves = 8;
						spawnRule.m_noiseMaskPersistence = 0.25f;
						spawnRule.m_noiseMaskSeed = 0f;
						spawnRule.m_noiseStrength = 1.5f;
						spawnRule.m_noiseZoom = 50f;
						spawnRule.m_noiseInvert = true;
					}
				}
				else
				{
					spawnRule.m_minViableFitness = 0.2f;
					spawnRule.m_failureRate = 0f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 2f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 0f;
					spawnRule.m_noiseStrength = 1.5f;
					spawnRule.m_noiseZoom = 50f;
					spawnRule.m_noiseInvert = false;
				}
			}
			SpawnerGroup.SpawnerInstance spawnerInstance = new SpawnerGroup.SpawnerInstance();
			spawnerInstance.m_name = gameObject2.name;
			spawnerInstance.m_interationsPerSpawn = 1;
			spawnerInstance.m_spawner = component;
			return gameObject2;
		}

		public GameObject CreateCoverageTreeSpawner(float range)
		{
			this.CreateOrFindSessionManager();
			GameObject gameObject = this.CreateOrFindGaia();
			GameObject gameObject2 = new GameObject("Coverage Tree Spawner");
			gameObject2.AddComponent<Spawner>();
			Spawner component = gameObject2.GetComponent<Spawner>();
			component.m_resources = this;
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.position = TerrainHelper.GetActiveTerrainCenter(true);
			component.m_resources = this;
			component.m_mode = GaiaConstants.OperationMode.DesignTime;
			component.m_spawnerShape = GaiaConstants.SpawnerShape.Box;
			component.m_rndGenerator = new XorshiftPlus(component.m_seed);
			component.m_spawnRange = range;
			component.m_spawnFitnessAttenuator = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			component.m_spawnRuleSelector = GaiaConstants.SpawnerRuleSelector.Random;
			component.m_spawnLocationAlgorithm = GaiaConstants.SpawnerLocation.EveryLocationJittered;
			component.m_locationIncrement = 45f;
			component.m_maxJitteredLocationOffsetPct = 0.85f;
			component.m_spawnCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			component.m_locationChecksPerInt = (int)range * 5;
			for (int i = 0; i < this.m_treePrototypes.Length; i++)
			{
				SpawnRule spawnRule = new SpawnRule();
				spawnRule.m_name = this.m_treePrototypes[i].m_name;
				spawnRule.m_resourceType = GaiaConstants.SpawnerResourceType.TerrainTree;
				spawnRule.m_resourceIdx = i;
				spawnRule.m_minViableFitness = 0.25f;
				spawnRule.m_failureRate = 0f;
				spawnRule.m_maxInstances = (ulong)(range * range / 5f);
				spawnRule.m_isActive = true;
				spawnRule.m_isFoldedOut = false;
				spawnRule.m_useExtendedSpawn = false;
				component.m_activeRuleCnt++;
				component.m_spawnerRules.Add(spawnRule);
				if (i != 0)
				{
					if (i == 1)
					{
						spawnRule.m_minViableFitness = 0.2f;
						spawnRule.m_failureRate = 0f;
						spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
						spawnRule.m_noiseMaskFrequency = 1f;
						spawnRule.m_noiseMaskLacunarity = 2f;
						spawnRule.m_noiseMaskOctaves = 8;
						spawnRule.m_noiseMaskPersistence = 0.25f;
						spawnRule.m_noiseMaskSeed = 0f;
						spawnRule.m_noiseStrength = 1.5f;
						spawnRule.m_noiseZoom = 50f;
						spawnRule.m_noiseInvert = true;
					}
				}
				else
				{
					spawnRule.m_minViableFitness = 0.2f;
					spawnRule.m_failureRate = 0f;
					spawnRule.m_noiseMask = GaiaConstants.NoiseType.Perlin;
					spawnRule.m_noiseMaskFrequency = 1f;
					spawnRule.m_noiseMaskLacunarity = 2f;
					spawnRule.m_noiseMaskOctaves = 8;
					spawnRule.m_noiseMaskPersistence = 0.25f;
					spawnRule.m_noiseMaskSeed = 0f;
					spawnRule.m_noiseStrength = 1.5f;
					spawnRule.m_noiseZoom = 50f;
					spawnRule.m_noiseInvert = false;
				}
			}
			SpawnerGroup.SpawnerInstance spawnerInstance = new SpawnerGroup.SpawnerInstance();
			spawnerInstance.m_name = gameObject2.name;
			spawnerInstance.m_interationsPerSpawn = 1;
			spawnerInstance.m_spawner = component;
			return gameObject2;
		}

		public GameObject CreateCoverageGameObjectSpawner(float range)
		{
			this.CreateOrFindSessionManager();
			GameObject gameObject = this.CreateOrFindGaia();
			GameObject gameObject2 = new GameObject("Coverage GameObject Spawner");
			gameObject2.AddComponent<Spawner>();
			Spawner component = gameObject2.GetComponent<Spawner>();
			component.m_resources = this;
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.position = TerrainHelper.GetActiveTerrainCenter(true);
			component.m_resources = this;
			component.m_mode = GaiaConstants.OperationMode.DesignTime;
			component.m_spawnerShape = GaiaConstants.SpawnerShape.Box;
			component.m_rndGenerator = new XorshiftPlus(component.m_seed);
			component.m_spawnRange = range;
			component.m_spawnFitnessAttenuator = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			component.m_spawnRuleSelector = GaiaConstants.SpawnerRuleSelector.Random;
			component.m_spawnLocationAlgorithm = GaiaConstants.SpawnerLocation.EveryLocationJittered;
			component.m_locationIncrement = 45f;
			component.m_maxJitteredLocationOffsetPct = 0.85f;
			component.m_spawnCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			component.m_locationChecksPerInt = (int)range * 5;
			for (int i = 0; i < this.m_gameObjectPrototypes.Length; i++)
			{
				SpawnRule spawnRule = new SpawnRule();
				spawnRule.m_name = this.m_gameObjectPrototypes[i].m_name;
				spawnRule.m_resourceType = GaiaConstants.SpawnerResourceType.GameObject;
				spawnRule.m_resourceIdx = i;
				spawnRule.m_minViableFitness = 0.25f;
				spawnRule.m_failureRate = 0f;
				spawnRule.m_maxInstances = (ulong)(range * range / 5f);
				spawnRule.m_isActive = true;
				spawnRule.m_isFoldedOut = false;
				spawnRule.m_useExtendedSpawn = false;
				component.m_activeRuleCnt++;
				component.m_spawnerRules.Add(spawnRule);
			}
			SpawnerGroup.SpawnerInstance spawnerInstance = new SpawnerGroup.SpawnerInstance();
			spawnerInstance.m_name = gameObject2.name;
			spawnerInstance.m_interationsPerSpawn = 1;
			spawnerInstance.m_spawner = component;
			return gameObject2;
		}

		public GameObject CreateClusteredGameObjectSpawner(float range)
		{
			this.CreateOrFindSessionManager();
			GameObject gameObject = this.CreateOrFindGaia();
			GameObject gameObject2 = new GameObject("Clustered GameObject Spawner");
			gameObject2.AddComponent<Spawner>();
			Spawner component = gameObject2.GetComponent<Spawner>();
			component.m_resources = this;
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.position = TerrainHelper.GetActiveTerrainCenter(true);
			component.m_resources = this;
			component.m_mode = GaiaConstants.OperationMode.DesignTime;
			component.m_spawnerShape = GaiaConstants.SpawnerShape.Box;
			component.m_rndGenerator = new XorshiftPlus(component.m_seed);
			component.m_spawnRange = range;
			component.m_spawnFitnessAttenuator = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			component.m_spawnRuleSelector = GaiaConstants.SpawnerRuleSelector.Random;
			component.m_spawnLocationAlgorithm = GaiaConstants.SpawnerLocation.RandomLocationClustered;
			component.m_spawnCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			component.m_locationChecksPerInt = 2000;
			component.m_maxRandomClusterSize = 20;
			for (int i = 0; i < this.m_gameObjectPrototypes.Length; i++)
			{
				SpawnRule spawnRule = new SpawnRule();
				spawnRule.m_name = this.m_gameObjectPrototypes[i].m_name;
				spawnRule.m_resourceType = GaiaConstants.SpawnerResourceType.GameObject;
				spawnRule.m_resourceIdx = i;
				spawnRule.m_minViableFitness = 0.25f;
				spawnRule.m_failureRate = 0f;
				spawnRule.m_maxInstances = (ulong)range * 2UL;
				spawnRule.m_isActive = true;
				spawnRule.m_isFoldedOut = false;
				spawnRule.m_useExtendedSpawn = false;
				component.m_activeRuleCnt++;
				component.m_spawnerRules.Add(spawnRule);
			}
			SpawnerGroup.SpawnerInstance spawnerInstance = new SpawnerGroup.SpawnerInstance();
			spawnerInstance.m_name = gameObject2.name;
			spawnerInstance.m_interationsPerSpawn = 1;
			spawnerInstance.m_spawner = component;
			return gameObject2;
		}

		public void ExportTexture()
		{
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				int alphamapWidth = terrain.terrainData.alphamapWidth;
				int alphamapHeight = terrain.terrainData.alphamapHeight;
				int alphamapLayers = terrain.terrainData.alphamapLayers;
				Texture2D texture2D = new Texture2D(alphamapWidth, alphamapHeight, TextureFormat.ARGB32, false);
				float[,,] alphamaps = terrain.terrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight);
				for (int j = 0; j < alphamapWidth; j++)
				{
					for (int k = 0; k < alphamapHeight; k++)
					{
						float num4;
						float num3;
						float num2;
						float num = num2 = (num3 = (num4 = 0f));
						for (int l = 0; l < alphamapLayers; l++)
						{
							Color pixel = this.m_texturePrototypes[l].m_texture.GetPixel(j % ((int)this.m_texturePrototypes[l].m_sizeX / this.m_texturePrototypes[l].m_texture.width), k % ((int)this.m_texturePrototypes[l].m_sizeY / this.m_texturePrototypes[l].m_texture.height));
							num2 += alphamaps[j, k, l] * pixel.r;
							num += alphamaps[j, k, l] * pixel.g;
							num3 += alphamaps[j, k, l] * pixel.b;
							num4 += alphamaps[j, k, l] * pixel.a;
						}
						texture2D.SetPixel(j, k, new Color(num2, num, num3, num4));
					}
				}
				Utils.ExportPNG(terrain.name + " - Export", texture2D);
				UnityEngine.Object.DestroyImmediate(texture2D);
			}
			UnityEngine.Debug.LogError("Attempted to export textures on terrain that does not exist!");
		}

		public string SerialiseJson()
		{
			fsSerializer fsSerializer = new fsSerializer();
			fsData data;
			fsSerializer.TrySerialize<GaiaResource>(this, out data);
			return fsJsonPrinter.CompressedJson(data);
		}

		public void DeSerialiseJson(string json)
		{
			fsData data = fsJsonParser.Parse(json);
			fsSerializer fsSerializer = new fsSerializer();
			GaiaResource gaiaResource = this;
			fsSerializer.TryDeserialize<GaiaResource>(data, ref gaiaResource);
		}

		[Tooltip("Unique identifier for these resources.")]
		[HideInInspector]
		public string m_resourcesID = Guid.NewGuid().ToString();

		[Tooltip("Resource name")]
		public string m_name = "Gaia Resource";

		[Tooltip("The absolute height of the sea or water table in meters. All spawn criteria heights are calculated relative to this. This can also be thought of as the water level. This value is sourced from the defaults file, and managed on a session by session basis.")]
		public float m_seaLevel = 100f;

		[Tooltip("The beach height in meters. Beaches are spawned at sea level and are extended for this height above sea level. This is used when creating default spawn rules in order to create a beach in the zone between water and land.")]
		public float m_beachHeight = 5f;

		[Tooltip("Terrain height.")]
		public float m_terrainHeight = 1000f;

		[Tooltip("Texture prototypes and fitness criteria.")]
		public ResourceProtoTexture[] m_texturePrototypes = new ResourceProtoTexture[0];

		[Tooltip("Detail prototypes, dna and fitness criteria.")]
		public ResourceProtoDetail[] m_detailPrototypes = new ResourceProtoDetail[0];

		[Tooltip("Tree prototypes, dna and fitness criteria.")]
		public ResourceProtoTree[] m_treePrototypes = new ResourceProtoTree[0];

		[Tooltip("Game object prototypes, dna and fitness criteria.")]
		public ResourceProtoGameObject[] m_gameObjectPrototypes = new ResourceProtoGameObject[0];
	}
}
