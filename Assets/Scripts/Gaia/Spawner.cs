using System;
using System.Collections;
using System.Collections.Generic;
using Gaia.FullSerializer;
using UnityEngine;

namespace Gaia
{
	[ExecuteInEditMode]
	public class Spawner : MonoBehaviour
	{
		private void OnEnable()
		{
			if (this.m_spawnCollisionLayers.value == 0)
			{
				this.m_spawnCollisionLayers = TerrainHelper.GetActiveTerrainLayer();
			}
			this.m_spawnColliderLayer = TerrainHelper.GetActiveTerrainLayerAsInt();
			if (this.m_rndGenerator == null)
			{
				this.m_rndGenerator = new XorshiftPlus(this.m_seed);
			}
		}

		private void OnDisable()
		{
		}

		public void StartEditorUpdates()
		{
		}

		public void StopEditorUpdates()
		{
		}

		private void EditorUpdate()
		{
		}

		private void Start()
		{
			if (Application.isPlaying)
			{
				Transform transform = base.transform.Find("Bounds_ColliderCache");
				if (transform != null)
				{
					this.m_areaBoundsColliderCache = transform.gameObject;
					this.m_areaBoundsColliderCache.SetActive(false);
				}
				if (!this.m_enableColliderCacheAtRuntime)
				{
					transform = base.transform.Find("GameObject_ColliderCache");
					if (transform != null)
					{
						this.m_goColliderCache = transform.gameObject;
						this.m_goColliderCache.SetActive(false);
					}
				}
			}
			if (this.m_mode == GaiaConstants.OperationMode.RuntimeInterval || this.m_mode == GaiaConstants.OperationMode.RuntimeTriggeredInterval)
			{
				this.Initialise();
				base.InvokeRepeating("RunSpawnerIteration", 1f, this.m_spawnInterval);
			}
		}

		public void Initialise()
		{
			if (this.m_showDebug)
			{
				UnityEngine.Debug.Log("Initialising spawner");
			}
			this.m_spawnColliderLayer = TerrainHelper.GetActiveTerrainLayerAsInt();
			List<Transform> list = new List<Transform>();
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform item = (Transform)obj;
					list.Add(item);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			foreach (Transform transform in list)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(transform.gameObject);
				}
			}
			this.SetUpSpawnerTypeFlags();
			if (this.IsGameObjectSpawner())
			{
				this.m_goParentGameObject = new GameObject("Spawned_GameObjects");
				this.m_goParentGameObject.transform.parent = base.transform;
			}
			if (this.IsTreeSpawner() || this.IsGameObjectSpawner())
			{
				this.m_areaBoundsColliderCache = new GameObject("Bounds_ColliderCache");
				this.m_areaBoundsColliderCache.transform.parent = base.transform;
			}
			if (this.IsGameObjectSpawner())
			{
				this.m_goColliderCache = new GameObject("GameObject_ColliderCache");
				this.m_goColliderCache.transform.parent = base.transform;
			}
			this.ResetRandomGenertor();
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain != null)
			{
				this.m_terrainHeight = terrain.terrainData.size.y;
			}
			this.m_spawnerBounds = new Bounds(base.transform.position, new Vector3(this.m_spawnRange * 2f, this.m_spawnRange * 2f, this.m_spawnRange * 2f));
			foreach (SpawnRule spawnRule in this.m_spawnerRules)
			{
				spawnRule.m_currInstanceCnt = 0UL;
				spawnRule.m_activeInstanceCnt = 0UL;
				spawnRule.m_inactiveInstanceCnt = 0UL;
			}
			this.UpdateCounters();
		}

		private void PreSpawnInitialise()
		{
			this.m_spawnerBounds = new Bounds(base.transform.position, new Vector3(this.m_spawnRange * 2f, this.m_spawnRange * 2f, this.m_spawnRange * 2f));
			if (this.m_rndGenerator == null)
			{
				this.ResetRandomGenertor();
			}
			this.m_spawnColliderLayer = TerrainHelper.GetActiveTerrainLayerAsInt();
			this.SetUpSpawnerTypeFlags();
			if (this.IsGameObjectSpawner() && base.transform.Find("Spawned_GameObjects") == null)
			{
				this.m_goParentGameObject = new GameObject("Spawned_GameObjects");
				this.m_goParentGameObject.transform.parent = base.transform;
			}
			if ((this.IsTreeSpawner() || this.IsGameObjectSpawner()) && base.transform.Find("Bounds_ColliderCache") == null)
			{
				this.m_areaBoundsColliderCache = new GameObject("Bounds_ColliderCache");
				this.m_areaBoundsColliderCache.transform.parent = base.transform;
			}
			if (this.IsGameObjectSpawner() && base.transform.Find("GameObject_ColliderCache") == null)
			{
				this.m_goColliderCache = new GameObject("GameObject_ColliderCache");
				this.m_goColliderCache.transform.parent = base.transform;
			}
			foreach (SpawnRule spawnRule in this.m_spawnerRules)
			{
				spawnRule.Initialise(this);
			}
			if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.PerlinNoise)
			{
				this.m_noiseGenerator = new FractalGenerator(this.m_noiseMaskFrequency, this.m_noiseMaskLacunarity, this.m_noiseMaskOctaves, this.m_noiseMaskPersistence, this.m_noiseMaskSeed, FractalGenerator.Fractals.Perlin);
			}
			else if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.BillowNoise)
			{
				this.m_noiseGenerator = new FractalGenerator(this.m_noiseMaskFrequency, this.m_noiseMaskLacunarity, this.m_noiseMaskOctaves, this.m_noiseMaskPersistence, this.m_noiseMaskSeed, FractalGenerator.Fractals.Billow);
			}
			else if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.RidgedNoise)
			{
				this.m_noiseGenerator = new FractalGenerator(this.m_noiseMaskFrequency, this.m_noiseMaskLacunarity, this.m_noiseMaskOctaves, this.m_noiseMaskPersistence, this.m_noiseMaskSeed, FractalGenerator.Fractals.RidgeMulti);
			}
			this.UpdateCounters();
		}

		public void SetUpSpawnerTypeFlags()
		{
			this.m_isDetailSpawner = false;
			for (int i = 0; i < this.m_spawnerRules.Count; i++)
			{
				if (this.m_spawnerRules[i].m_resourceType == GaiaConstants.SpawnerResourceType.TerrainDetail)
				{
					this.m_isDetailSpawner = true;
					break;
				}
			}
			this.m_isTextureSpawner = false;
			for (int j = 0; j < this.m_spawnerRules.Count; j++)
			{
				if (this.m_spawnerRules[j].m_resourceType == GaiaConstants.SpawnerResourceType.TerrainTexture)
				{
					this.m_isTextureSpawner = true;
					break;
				}
			}
			for (int k = 0; k < this.m_spawnerRules.Count; k++)
			{
				if (this.m_spawnerRules[k].m_resourceType == GaiaConstants.SpawnerResourceType.TerrainTree)
				{
					this.m_isTreeSpawnwer = true;
					break;
				}
			}
			this.m_isGameObjectSpawner = false;
			for (int l = 0; l < this.m_spawnerRules.Count; l++)
			{
				if (this.m_spawnerRules[l].m_resourceType == GaiaConstants.SpawnerResourceType.GameObject)
				{
					this.m_isGameObjectSpawner = true;
					break;
				}
			}
		}

		public void AssociateAssets()
		{
			if (this.m_resources != null)
			{
				this.m_resources.AssociateAssets();
			}
			else
			{
				UnityEngine.Debug.LogWarning("Could not associated assets for " + base.name + " - resources file was missing");
			}
		}

		public int[] GetMissingResources()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_spawnerRules.Count; i++)
			{
				if (this.m_spawnerRules[i].m_isActive && !this.m_spawnerRules[i].ResourceIsLoadedInTerrain(this))
				{
					list.Add(i);
				}
			}
			return list.ToArray();
		}

		public void AddResourcesToTerrain(int[] rules)
		{
			for (int i = 0; i < rules.GetLength(0); i++)
			{
				if (!this.m_spawnerRules[rules[i]].ResourceIsLoadedInTerrain(this))
				{
					this.m_spawnerRules[rules[i]].AddResourceToTerrain(this);
				}
			}
		}

		private void PostSpawn()
		{
			this.m_spawnProgress = 0f;
			this.m_spawnComplete = true;
			this.m_updateCoroutine = null;
			this.UpdateCounters();
		}

		public bool IsTextureSpawner()
		{
			return this.m_isTextureSpawner;
		}

		public bool IsDetailSpawner()
		{
			return this.m_isDetailSpawner;
		}

		public bool IsTreeSpawner()
		{
			return this.m_isTreeSpawnwer;
		}

		public bool IsGameObjectSpawner()
		{
			return this.m_isGameObjectSpawner;
		}

		public void ResetSpawner()
		{
			this.Initialise();
		}

		public void CancelSpawn()
		{
			this.m_cancelSpawn = true;
			this.m_spawnProgress = 0f;
		}

		public bool IsSpawning()
		{
			return !this.m_spawnComplete;
		}

		private bool CanSpawnInstances()
		{
			bool result = false;
			for (int i = 0; i < this.m_spawnerRules.Count; i++)
			{
				SpawnRule spawnRule = this.m_spawnerRules[i];
				if (spawnRule.m_isActive)
				{
					if (spawnRule.m_ignoreMaxInstances)
					{
						return true;
					}
					if (spawnRule.m_activeInstanceCnt < spawnRule.m_maxInstances)
					{
						return true;
					}
				}
			}
			return result;
		}

		public void RunSpawnerIteration()
		{
			this.m_cancelSpawn = false;
			this.m_spawnComplete = false;
			this.PreSpawnInitialise();
			if (this.m_activeRuleCnt <= 0)
			{
				if (this.m_showDebug)
				{
					UnityEngine.Debug.Log(string.Format("{0}: There are no active spawn rules. Can't spawn without rules.", base.gameObject.name));
				}
				this.m_spawnComplete = true;
				return;
			}
			if (!this.CanSpawnInstances())
			{
				if (this.m_showDebug)
				{
					UnityEngine.Debug.Log(string.Format("{0}: Can't spawn or activate new instance - max instance count reached.", base.gameObject.name));
				}
				this.m_spawnComplete = true;
				return;
			}
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain != null)
			{
				this.m_terrainHeight = terrain.terrainData.size.y;
				if (this.m_resources != null && this.m_resources.m_terrainHeight != this.m_terrainHeight)
				{
					UnityEngine.Debug.LogWarning(string.Format("There is a mismatch between your resources Terrain Height {0} and your actual Terrain Height {1}. Your Spawn may not work as intended!", this.m_resources.m_terrainHeight, this.m_terrainHeight));
				}
			}
			if (this.m_mode == GaiaConstants.OperationMode.RuntimeTriggeredInterval)
			{
				this.m_checkDistance = this.m_triggerRange + 1f;
				List<GameObject> list = new List<GameObject>();
				string[] array = new string[0];
				if (!string.IsNullOrEmpty(this.m_triggerTags))
				{
					array = this.m_triggerTags.Split(new char[]
					{
						','
					});
				}
				else
				{
					UnityEngine.Debug.LogError("You have not supplied a trigger tag. Spawner will not spawn!");
				}
				if (this.m_triggerTags.Length <= 0 || array.Length <= 0)
				{
					if (this.m_showDebug)
					{
						UnityEngine.Debug.Log(string.Format("{0}: No triggers found", base.gameObject.name));
					}
					this.m_spawnComplete = true;
					return;
				}
				for (int i = 0; i < array.Length; i++)
				{
					list.AddRange(GameObject.FindGameObjectsWithTag(array[i]));
				}
				for (int i = 0; i < list.Count; i++)
				{
					this.m_checkDistance = Vector3.Distance(base.transform.position, list[i].transform.position);
					if (this.m_checkDistance <= this.m_triggerRange)
					{
						break;
					}
				}
				if (this.m_checkDistance > this.m_triggerRange)
				{
					if (this.m_showDebug)
					{
						UnityEngine.Debug.Log(string.Format("{0}: No triggers were close enough", base.gameObject.name));
					}
					this.m_spawnComplete = true;
					return;
				}
			}
			if (!Application.isPlaying)
			{
				this.AddToSession(GaiaOperation.OperationType.Spawn, "Spawning " + base.transform.name);
			}
			if (this.m_spawnLocationAlgorithm == GaiaConstants.SpawnerLocation.RandomLocation || this.m_spawnLocationAlgorithm == GaiaConstants.SpawnerLocation.RandomLocationClustered)
			{
				base.StartCoroutine(this.RunRandomSpawnerIteration());
			}
			else
			{
				base.StartCoroutine(this.RunAreaSpawnerIteration());
			}
		}

		public IEnumerator RunRandomSpawnerIteration()
		{
			if (this.m_showDebug)
			{
				UnityEngine.Debug.Log(string.Format("{0}: Running random iteration", base.gameObject.name));
			}
			SpawnInfo spawnInfo = new SpawnInfo();
			List<Spawner.SpawnLocation> spawnLocations = new List<Spawner.SpawnLocation>();
			int spawnLocationsIdx = 0;
			int failedSpawns = 0;
			this.m_spawnProgress = 0f;
			this.m_spawnComplete = false;
			float currentTime = Time.realtimeSinceStartup;
			float accumulatedTime = 0f;
			spawnInfo.m_textureStrengths = new float[Terrain.activeTerrain.terrainData.alphamapLayers];
			this.CreateSpawnCaches();
			this.LoadImageMask();
			for (int checks = 0; checks < this.m_locationChecksPerInt; checks++)
			{
				Spawner.SpawnLocation spawnLocation = new Spawner.SpawnLocation();
				if (this.m_spawnLocationAlgorithm == GaiaConstants.SpawnerLocation.RandomLocation)
				{
					spawnLocation.m_location = this.GetRandomV3(this.m_spawnRange);
					spawnLocation.m_location = base.transform.position + spawnLocation.m_location;
				}
				else if (spawnLocations.Count == 0 || spawnLocations.Count > this.m_maxRandomClusterSize || failedSpawns > this.m_maxRandomClusterSize)
				{
					spawnLocation.m_location = this.GetRandomV3(this.m_spawnRange);
					spawnLocation.m_location = base.transform.position + spawnLocation.m_location;
					failedSpawns = 0;
					spawnLocationsIdx = 0;
					spawnLocations.Clear();
				}
				else
				{
					if (spawnLocationsIdx >= spawnLocations.Count)
					{
						spawnLocationsIdx = 0;
					}
					spawnLocation.m_location = this.GetRandomV3(spawnLocations[spawnLocationsIdx].m_seedDistance);
					Spawner.SpawnLocation spawnLocation2 = spawnLocation;
					List<Spawner.SpawnLocation> list = spawnLocations;
					int index;
					spawnLocationsIdx = (index = spawnLocationsIdx) + 1;
					spawnLocation2.m_location = list[index].m_location + spawnLocation.m_location;
				}
				if (this.CheckLocation(spawnLocation.m_location, ref spawnInfo))
				{
					if (this.m_spawnRuleSelector == GaiaConstants.SpawnerRuleSelector.All)
					{
						for (int ruleIdx = 0; ruleIdx < this.m_spawnerRules.Count; ruleIdx++)
						{
							SpawnRule rule = this.m_spawnerRules[ruleIdx];
							spawnInfo.m_fitness = rule.GetFitness(ref spawnInfo);
							if (this.TryExecuteRule(ref rule, ref spawnInfo))
							{
								failedSpawns = 0;
								spawnLocation.m_seedDistance = rule.GetSeedThrowRange(ref spawnInfo);
								spawnLocations.Add(spawnLocation);
							}
							else
							{
								failedSpawns++;
							}
						}
					}
					else if (this.m_spawnRuleSelector == GaiaConstants.SpawnerRuleSelector.Random)
					{
						SpawnRule rule = this.m_spawnerRules[this.GetRandomInt(0, this.m_spawnerRules.Count - 1)];
						spawnInfo.m_fitness = rule.GetFitness(ref spawnInfo);
						if (this.TryExecuteRule(ref rule, ref spawnInfo))
						{
							failedSpawns = 0;
							spawnLocation.m_seedDistance = rule.GetSeedThrowRange(ref spawnInfo);
							spawnLocations.Add(spawnLocation);
						}
						else
						{
							failedSpawns++;
						}
					}
					else if (this.m_spawnRuleSelector == GaiaConstants.SpawnerRuleSelector.Fittest)
					{
						SpawnRule fittestRule = null;
						float maxFitness = 0f;
						for (int ruleIdx = 0; ruleIdx < this.m_spawnerRules.Count; ruleIdx++)
						{
							SpawnRule rule = this.m_spawnerRules[ruleIdx];
							float fitness = rule.GetFitness(ref spawnInfo);
							if (fitness > maxFitness)
							{
								maxFitness = fitness;
								fittestRule = rule;
							}
							else if (Utils.Math_ApproximatelyEqual(fitness, maxFitness, 0.005f) && this.GetRandomFloat(0f, 1f) > 0.5f)
							{
								maxFitness = fitness;
								fittestRule = rule;
							}
						}
						spawnInfo.m_fitness = maxFitness;
						if (this.TryExecuteRule(ref fittestRule, ref spawnInfo))
						{
							failedSpawns = 0;
							spawnLocation.m_seedDistance = fittestRule.GetSeedThrowRange(ref spawnInfo);
							spawnLocations.Add(spawnLocation);
						}
						else
						{
							failedSpawns++;
						}
					}
					else
					{
						SpawnRule selectedRule;
						SpawnRule fittestRule = selectedRule = null;
						float selectedFitness;
						float maxFitness = selectedFitness = 0f;
						for (int ruleIdx = 0; ruleIdx < this.m_spawnerRules.Count; ruleIdx++)
						{
							SpawnRule rule = this.m_spawnerRules[ruleIdx];
							float fitness = rule.GetFitness(ref spawnInfo);
							if (this.GetRandomFloat(0f, 1f) < fitness)
							{
								selectedRule = rule;
								selectedFitness = fitness;
							}
							if (fitness > maxFitness)
							{
								fittestRule = rule;
								maxFitness = fitness;
							}
						}
						if (selectedRule == null)
						{
							selectedRule = fittestRule;
							selectedFitness = maxFitness;
						}
						if (selectedRule != null)
						{
							spawnInfo.m_fitness = selectedFitness;
							if (this.TryExecuteRule(ref selectedRule, ref spawnInfo))
							{
								failedSpawns = 0;
								spawnLocation.m_seedDistance = selectedRule.GetSeedThrowRange(ref spawnInfo);
								spawnLocations.Add(spawnLocation);
							}
							else
							{
								failedSpawns++;
							}
						}
					}
				}
				this.m_spawnProgress = (float)checks / (float)this.m_locationChecksPerInt;
				float newTime = Time.realtimeSinceStartup;
				float stepTime = newTime - currentTime;
				currentTime = newTime;
				accumulatedTime += stepTime;
				if (accumulatedTime > this.m_updateTimeAllowed)
				{
					accumulatedTime = 0f;
					yield return null;
				}
				if (!this.CanSpawnInstances())
				{
					break;
				}
				if (this.m_cancelSpawn)
				{
					break;
				}
			}
			this.DeleteSpawnCaches(false);
			this.PostSpawn();
			yield break;
		}

		public IEnumerator RunAreaSpawnerIteration()
		{
			if (this.m_showDebug)
			{
				UnityEngine.Debug.Log(string.Format("{0}: Running area iteration", base.gameObject.name));
			}
			SpawnInfo spawnInfo = new SpawnInfo();
			Vector3 location = default(Vector3);
			this.m_spawnProgress = 0f;
			this.m_spawnComplete = false;
			float currentTime = Time.realtimeSinceStartup;
			float accumulatedTime = 0f;
			this.CreateSpawnCaches();
			this.LoadImageMask();
			spawnInfo.m_textureStrengths = new float[Terrain.activeTerrain.terrainData.alphamapLayers];
			float xWUMin = base.transform.position.x - this.m_spawnRange + this.m_locationIncrement / 2f;
			float xWUMax = xWUMin + this.m_spawnRange * 2f;
			float yMid = base.transform.position.y;
			float zWUMin = base.transform.position.z - this.m_spawnRange + this.m_locationIncrement / 2f;
			float zWUMax = zWUMin + this.m_spawnRange * 2f;
			float jitMin = -1f * this.m_maxJitteredLocationOffsetPct * this.m_locationIncrement;
			float jitMax = 1f * this.m_maxJitteredLocationOffsetPct * this.m_locationIncrement;
			long currChecks = 0L;
			long totalChecks = (long)((xWUMax - xWUMin) / this.m_locationIncrement * ((zWUMax - zWUMin) / this.m_locationIncrement));
			for (float xWU = xWUMin; xWU < xWUMax; xWU += this.m_locationIncrement)
			{
				for (float zWU = zWUMin; zWU < zWUMax; zWU += this.m_locationIncrement)
				{
					currChecks += 1L;
					location.x = xWU;
					location.y = yMid;
					location.z = zWU;
					if (this.m_spawnLocationAlgorithm == GaiaConstants.SpawnerLocation.EveryLocationJittered)
					{
						location.x += this.GetRandomFloat(jitMin, jitMax);
						location.z += this.GetRandomFloat(jitMin, jitMax);
					}
					if (this.CheckLocation(location, ref spawnInfo))
					{
						if (this.m_spawnRuleSelector == GaiaConstants.SpawnerRuleSelector.All)
						{
							for (int ruleIdx = 0; ruleIdx < this.m_spawnerRules.Count; ruleIdx++)
							{
								SpawnRule rule = this.m_spawnerRules[ruleIdx];
								spawnInfo.m_fitness = rule.GetFitness(ref spawnInfo);
								this.TryExecuteRule(ref rule, ref spawnInfo);
							}
						}
						else if (this.m_spawnRuleSelector == GaiaConstants.SpawnerRuleSelector.Random)
						{
							int ruleIdx = this.GetRandomInt(0, this.m_spawnerRules.Count - 1);
							SpawnRule rule = this.m_spawnerRules[ruleIdx];
							spawnInfo.m_fitness = rule.GetFitness(ref spawnInfo);
							this.TryExecuteRule(ref rule, ref spawnInfo);
						}
						else if (this.m_spawnRuleSelector == GaiaConstants.SpawnerRuleSelector.Fittest)
						{
							SpawnRule fittestRule = null;
							float maxFitness = 0f;
							for (int ruleIdx = 0; ruleIdx < this.m_spawnerRules.Count; ruleIdx++)
							{
								SpawnRule rule = this.m_spawnerRules[ruleIdx];
								float fitness = rule.GetFitness(ref spawnInfo);
								if (fitness > maxFitness)
								{
									maxFitness = fitness;
									fittestRule = rule;
								}
								else if (Utils.Math_ApproximatelyEqual(fitness, maxFitness, 0.005f) && this.GetRandomFloat(0f, 1f) > 0.5f)
								{
									maxFitness = fitness;
									fittestRule = rule;
								}
							}
							spawnInfo.m_fitness = maxFitness;
							this.TryExecuteRule(ref fittestRule, ref spawnInfo);
						}
						else
						{
							SpawnRule selectedRule;
							SpawnRule fittestRule = selectedRule = null;
							float selectedFitness;
							float maxFitness = selectedFitness = 0f;
							for (int ruleIdx = 0; ruleIdx < this.m_spawnerRules.Count; ruleIdx++)
							{
								SpawnRule rule = this.m_spawnerRules[ruleIdx];
								float fitness = rule.GetFitness(ref spawnInfo);
								if (this.GetRandomFloat(0f, 1f) < fitness)
								{
									selectedRule = rule;
									selectedFitness = fitness;
								}
								if (fitness > maxFitness)
								{
									fittestRule = rule;
									maxFitness = fitness;
								}
							}
							if (selectedRule == null)
							{
								selectedRule = fittestRule;
								selectedFitness = maxFitness;
							}
							if (selectedRule != null)
							{
								spawnInfo.m_fitness = selectedFitness;
								this.TryExecuteRule(ref selectedRule, ref spawnInfo);
							}
						}
						if (this.m_textureMapsDirty)
						{
							List<HeightMap> textureMaps = spawnInfo.m_spawner.GetTextureMaps(spawnInfo.m_hitTerrain.GetInstanceID());
							if (textureMaps != null)
							{
								for (int i = 0; i < spawnInfo.m_textureStrengths.Length; i++)
								{
									textureMaps[i][spawnInfo.m_hitLocationNU.z, spawnInfo.m_hitLocationNU.x] = spawnInfo.m_textureStrengths[i];
								}
							}
						}
					}
					this.m_spawnProgress = (float)currChecks / (float)totalChecks;
					float newTime = Time.realtimeSinceStartup;
					float stepTime = newTime - currentTime;
					currentTime = newTime;
					accumulatedTime += stepTime;
					if (accumulatedTime > this.m_updateTimeAllowed)
					{
						accumulatedTime = 0f;
						yield return null;
					}
					if (!this.CanSpawnInstances())
					{
						break;
					}
					if (this.m_cancelSpawn)
					{
						break;
					}
				}
			}
			this.DeleteSpawnCaches(true);
			this.PostSpawn();
			yield break;
		}

		public void GroundToTerrain()
		{
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain == null)
			{
				terrain = Terrain.activeTerrain;
			}
			if (terrain == null)
			{
				UnityEngine.Debug.LogError("Could not fit to terrain - no terrain present");
				return;
			}
			Bounds bounds = default(Bounds);
			if (TerrainHelper.GetTerrainBounds(terrain, ref bounds))
			{
				base.transform.position = new Vector3(base.transform.position.x, terrain.transform.position.y, base.transform.position.z);
			}
		}

		public void FitToTerrain()
		{
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain == null)
			{
				terrain = Terrain.activeTerrain;
			}
			if (terrain == null)
			{
				UnityEngine.Debug.LogError("Could not fit to terrain - no terrain present");
				return;
			}
			Bounds bounds = default(Bounds);
			if (TerrainHelper.GetTerrainBounds(terrain, ref bounds))
			{
				base.transform.position = new Vector3(bounds.center.x, terrain.transform.position.y, bounds.center.z);
				this.m_spawnRange = bounds.extents.x;
			}
		}

		public bool IsFitToTerrain()
		{
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain == null)
			{
				terrain = Terrain.activeTerrain;
			}
			if (terrain == null)
			{
				UnityEngine.Debug.LogError("Could not check if fit to terrain - no terrain present");
				return false;
			}
			Bounds bounds = default(Bounds);
			return TerrainHelper.GetTerrainBounds(terrain, ref bounds) && bounds.center.x == base.transform.position.x && bounds.center.z == base.transform.position.z && bounds.extents.x == this.m_spawnRange;
		}

		public bool LoadImageMask()
		{
			this.m_imageMaskHM = null;
			if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.None || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.PerlinNoise)
			{
				return false;
			}
			if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.ImageRedChannel || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.ImageGreenChannel || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.ImageBlueChannel || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.ImageAlphaChannel || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.ImageGreyScale)
			{
				if (this.m_imageMask == null)
				{
					UnityEngine.Debug.LogError("You requested an image mask but did not supply one. Please select mask texture.");
					return false;
				}
				Utils.MakeTextureReadable(this.m_imageMask);
				this.m_imageMaskHM = new HeightMap(this.m_imageMask.width, this.m_imageMask.height);
				for (int i = 0; i < this.m_imageMaskHM.Width(); i++)
				{
					for (int j = 0; j < this.m_imageMaskHM.Depth(); j++)
					{
						switch (this.m_areaMaskMode)
						{
						case GaiaConstants.ImageFitnessFilterMode.ImageGreyScale:
							this.m_imageMaskHM[i, j] = this.m_imageMask.GetPixel(i, j).grayscale;
							break;
						case GaiaConstants.ImageFitnessFilterMode.ImageRedChannel:
							this.m_imageMaskHM[i, j] = this.m_imageMask.GetPixel(i, j).r;
							break;
						case GaiaConstants.ImageFitnessFilterMode.ImageGreenChannel:
							this.m_imageMaskHM[i, j] = this.m_imageMask.GetPixel(i, j).g;
							break;
						case GaiaConstants.ImageFitnessFilterMode.ImageBlueChannel:
							this.m_imageMaskHM[i, j] = this.m_imageMask.GetPixel(i, j).b;
							break;
						case GaiaConstants.ImageFitnessFilterMode.ImageAlphaChannel:
							this.m_imageMaskHM[i, j] = this.m_imageMask.GetPixel(i, j).a;
							break;
						}
					}
				}
			}
			else
			{
				if (Terrain.activeTerrain == null)
				{
					UnityEngine.Debug.LogError("You requested an terrain texture mask but there is no active terrain.");
					return false;
				}
				Terrain activeTerrain = Terrain.activeTerrain;
				switch (this.m_areaMaskMode)
				{
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture0:
					if (activeTerrain.terrainData.splatPrototypes.Length < 1)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 0 but there is no active texture in slot 0.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, activeTerrain.terrainData.alphamapWidth, activeTerrain.terrainData.alphamapHeight), 0);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture1:
					if (activeTerrain.terrainData.splatPrototypes.Length < 2)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 1 but there is no active texture in slot 1.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, activeTerrain.terrainData.alphamapWidth, activeTerrain.terrainData.alphamapHeight), 1);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture2:
					if (activeTerrain.terrainData.splatPrototypes.Length < 3)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 2 but there is no active texture in slot 2.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, activeTerrain.terrainData.alphamapWidth, activeTerrain.terrainData.alphamapHeight), 2);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture3:
					if (activeTerrain.terrainData.splatPrototypes.Length < 4)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 3 but there is no active texture in slot 3.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, activeTerrain.terrainData.alphamapWidth, activeTerrain.terrainData.alphamapHeight), 3);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture4:
					if (activeTerrain.terrainData.splatPrototypes.Length < 5)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 4 but there is no active texture in slot 4.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, activeTerrain.terrainData.alphamapWidth, activeTerrain.terrainData.alphamapHeight), 4);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture5:
					if (activeTerrain.terrainData.splatPrototypes.Length < 6)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 5 but there is no active texture in slot 5.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, activeTerrain.terrainData.alphamapWidth, activeTerrain.terrainData.alphamapHeight), 5);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture6:
					if (activeTerrain.terrainData.splatPrototypes.Length < 7)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 6 but there is no active texture in slot 6.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, activeTerrain.terrainData.alphamapWidth, activeTerrain.terrainData.alphamapHeight), 6);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture7:
					if (activeTerrain.terrainData.splatPrototypes.Length < 8)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 7 but there is no active texture in slot 7.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(activeTerrain.terrainData.GetAlphamaps(0, 0, activeTerrain.terrainData.alphamapWidth, activeTerrain.terrainData.alphamapHeight), 7);
					break;
				}
				this.m_imageMaskHM.Flip();
			}
			if (this.m_imageMaskSmoothIterations > 0)
			{
				this.m_imageMaskHM.Smooth(this.m_imageMaskSmoothIterations);
			}
			if (this.m_imageMaskFlip)
			{
				this.m_imageMaskHM.Flip();
			}
			if (this.m_imageMaskNormalise)
			{
				this.m_imageMaskHM.Normalise();
			}
			if (this.m_imageMaskInvert)
			{
				this.m_imageMaskHM.Invert();
			}
			return true;
		}

		public void CreateSpawnCaches()
		{
			this.m_cacheTextures = false;
			this.m_textureMapsDirty = false;
			for (int i = 0; i < this.m_spawnerRules.Count; i++)
			{
				if (this.m_spawnerRules[i].CacheTextures(this))
				{
					this.CacheTextureMapsFromTerrain(Terrain.activeTerrain.GetInstanceID());
					this.m_cacheTextures = true;
					break;
				}
			}
			this.m_cacheDetails = false;
			for (int i = 0; i < this.m_spawnerRules.Count; i++)
			{
				if (this.m_spawnerRules[i].CacheDetails(this))
				{
					this.CacheDetailMapsFromTerrain(Terrain.activeTerrain.GetInstanceID());
					this.m_cacheDetails = true;
					break;
				}
			}
			this.m_cacheTags = false;
			List<string> list = new List<string>();
			for (int i = 0; i < this.m_spawnerRules.Count; i++)
			{
				this.m_spawnerRules[i].AddProximityTags(this, ref list);
			}
			if (list.Count > 0)
			{
				this.CacheTaggedGameObjectsFromScene(list);
				this.m_cacheTags = true;
			}
			this.m_cacheHeightMaps = false;
			for (int i = 0; i < this.m_spawnerRules.Count; i++)
			{
				if (this.m_spawnerRules[i].CacheHeightMaps(this))
				{
					this.CacheHeightMapFromTerrain(Terrain.activeTerrain.GetInstanceID());
					this.m_cacheHeightMaps = true;
					break;
				}
			}
		}

		public void CreateSpawnCaches(GaiaConstants.SpawnerResourceType resourceType, int resourceIdx)
		{
			this.m_cacheTextures = false;
			this.m_textureMapsDirty = false;
			this.m_cacheDetails = false;
			this.m_cacheTags = false;
			switch (resourceType)
			{
			case GaiaConstants.SpawnerResourceType.TerrainTexture:
				if (resourceIdx < this.m_resources.m_texturePrototypes.Length)
				{
					this.CacheTextureMapsFromTerrain(Terrain.activeTerrain.GetInstanceID());
					this.m_cacheTextures = true;
					List<string> list = new List<string>();
					this.m_resources.m_texturePrototypes[resourceIdx].AddTags(ref list);
					if (list.Count > 0)
					{
						this.CacheTaggedGameObjectsFromScene(list);
						this.m_cacheTags = true;
					}
				}
				break;
			case GaiaConstants.SpawnerResourceType.TerrainDetail:
				if (resourceIdx < this.m_resources.m_detailPrototypes.Length)
				{
					this.CacheDetailMapsFromTerrain(Terrain.activeTerrain.GetInstanceID());
					this.m_cacheDetails = true;
					if (this.m_resources.m_detailPrototypes[resourceIdx].ChecksTextures())
					{
						this.CacheTextureMapsFromTerrain(Terrain.activeTerrain.GetInstanceID());
						this.m_cacheTextures = true;
					}
					List<string> list2 = new List<string>();
					this.m_resources.m_detailPrototypes[resourceIdx].AddTags(ref list2);
					if (list2.Count > 0)
					{
						this.CacheTaggedGameObjectsFromScene(list2);
						this.m_cacheTags = true;
					}
				}
				break;
			case GaiaConstants.SpawnerResourceType.TerrainTree:
				if (resourceIdx < this.m_resources.m_treePrototypes.Length)
				{
					if (this.m_resources.m_treePrototypes[resourceIdx].ChecksTextures())
					{
						this.CacheTextureMapsFromTerrain(Terrain.activeTerrain.GetInstanceID());
						this.m_cacheTextures = true;
					}
					List<string> list3 = new List<string>();
					this.m_resources.m_treePrototypes[resourceIdx].AddTags(ref list3);
					if (list3.Count > 0)
					{
						this.CacheTaggedGameObjectsFromScene(list3);
						this.m_cacheTags = true;
					}
				}
				break;
			case GaiaConstants.SpawnerResourceType.GameObject:
				if (resourceIdx < this.m_resources.m_gameObjectPrototypes.Length)
				{
					if (this.m_resources.m_gameObjectPrototypes[resourceIdx].ChecksTextures())
					{
						this.CacheTextureMapsFromTerrain(Terrain.activeTerrain.GetInstanceID());
						this.m_cacheTextures = true;
					}
					List<string> list4 = new List<string>();
					this.m_resources.m_gameObjectPrototypes[resourceIdx].AddTags(ref list4);
					if (list4.Count > 0)
					{
						this.CacheTaggedGameObjectsFromScene(list4);
						this.m_cacheTags = true;
					}
				}
				break;
			}
		}

		public void DeleteSpawnCaches(bool flushDirty = false)
		{
			if (this.m_cacheTextures)
			{
				if (flushDirty && this.m_textureMapsDirty && !this.m_cancelSpawn)
				{
					this.m_textureMapsDirty = false;
					this.SaveTextureMapsToTerrain(Terrain.activeTerrain.GetInstanceID());
				}
				this.DeleteTextureMapCache();
				this.m_cacheTextures = false;
			}
			if (this.m_cacheDetails)
			{
				if (!this.m_cancelSpawn)
				{
					this.SaveDetailMapsToTerrain(Terrain.activeTerrain.GetInstanceID());
				}
				this.DeleteDetailMapCache();
				this.m_cacheDetails = false;
			}
			if (this.m_cacheTags)
			{
				this.DeleteTagCache();
				this.m_cacheTags = false;
			}
			if (this.m_cacheHeightMaps)
			{
				if (flushDirty && this.m_heightMapDirty && !this.m_cancelSpawn)
				{
					this.m_heightMapDirty = false;
					this.SaveHeightMapToTerrain(Terrain.activeTerrain.GetInstanceID());
				}
				this.DeleteHeightMapCache();
				this.m_cacheHeightMaps = false;
			}
		}

		public bool TryExecuteRule(ref SpawnRule rule, ref SpawnInfo spawnInfo)
		{
			if (rule != null && (rule.m_ignoreMaxInstances || rule.m_activeInstanceCnt < rule.m_maxInstances))
			{
				spawnInfo.m_fitness *= this.m_spawnFitnessAttenuator.Evaluate(Mathf.Clamp01(spawnInfo.m_hitDistanceWU / this.m_spawnRange));
				if (this.m_areaMaskMode != GaiaConstants.ImageFitnessFilterMode.None)
				{
					if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.PerlinNoise || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.BillowNoise || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.RidgedNoise)
					{
						if (!this.m_noiseInvert)
						{
							spawnInfo.m_fitness *= this.m_noiseGenerator.GetNormalisedValue(100000f + spawnInfo.m_hitLocationWU.x * (1f / this.m_noiseZoom), 100000f + spawnInfo.m_hitLocationWU.z * (1f / this.m_noiseZoom));
						}
						else
						{
							spawnInfo.m_fitness *= 1f - this.m_noiseGenerator.GetNormalisedValue(100000f + spawnInfo.m_hitLocationWU.x * (1f / this.m_noiseZoom), 100000f + spawnInfo.m_hitLocationWU.z * (1f / this.m_noiseZoom));
						}
					}
					else if (this.m_imageMaskHM.HasData())
					{
						float x = (spawnInfo.m_hitLocationWU.x - (base.transform.position.x - this.m_spawnRange)) / (this.m_spawnRange * 2f);
						float z = (spawnInfo.m_hitLocationWU.z - (base.transform.position.z - this.m_spawnRange)) / (this.m_spawnRange * 2f);
						spawnInfo.m_fitness *= this.m_imageMaskHM[x, z];
					}
				}
				if (spawnInfo.m_fitness > rule.m_minViableFitness && this.GetRandomFloat(0f, 1f) > rule.m_failureRate)
				{
					rule.Spawn(ref spawnInfo);
					return true;
				}
			}
			return false;
		}

		public bool CheckLocation(Vector3 locationWU, ref SpawnInfo spawnInfo)
		{
			spawnInfo.m_spawner = this;
			spawnInfo.m_outOfBounds = true;
			spawnInfo.m_wasVirginTerrain = false;
			spawnInfo.m_spawnRotationY = 0f;
			spawnInfo.m_hitDistanceWU = Vector3.Distance(base.transform.position, locationWU);
			spawnInfo.m_hitLocationWU = locationWU;
			spawnInfo.m_hitNormal = Vector3.zero;
			spawnInfo.m_hitObject = null;
			spawnInfo.m_hitTerrain = null;
			spawnInfo.m_terrainNormalWU = Vector3.one;
			spawnInfo.m_terrainHeightWU = 0f;
			spawnInfo.m_terrainSlopeWU = 0f;
			spawnInfo.m_areaHitSlopeWU = 0f;
			spawnInfo.m_areaMinSlopeWU = 0f;
			spawnInfo.m_areaAvgSlopeWU = 0f;
			spawnInfo.m_areaMaxSlopeWU = 0f;
			locationWU.y = this.m_terrainHeight + 1000f;
			if (Physics.Raycast(locationWU, Vector3.down, out this.m_checkHitInfo, float.PositiveInfinity, this.m_spawnCollisionLayers))
			{
				if (spawnInfo.m_spawner.IsDetailSpawner() && (this.m_checkHitInfo.collider is SphereCollider || this.m_checkHitInfo.collider is CapsuleCollider) && this.m_checkHitInfo.collider.name == "_GaiaCollider_Grass")
				{
					locationWU.y = this.m_checkHitInfo.point.y - 0.01f;
					if (!Physics.Raycast(locationWU, Vector3.down, out this.m_checkHitInfo, float.PositiveInfinity, this.m_spawnCollisionLayers))
					{
						return false;
					}
				}
				spawnInfo.m_hitLocationWU = this.m_checkHitInfo.point;
				spawnInfo.m_hitDistanceWU = Vector3.Distance(base.transform.position, spawnInfo.m_hitLocationWU);
				spawnInfo.m_hitNormal = this.m_checkHitInfo.normal;
				spawnInfo.m_hitObject = this.m_checkHitInfo.transform;
				if (this.m_spawnerShape == GaiaConstants.SpawnerShape.Box)
				{
					if (!this.m_spawnerBounds.Contains(spawnInfo.m_hitLocationWU))
					{
						return false;
					}
				}
				else if (spawnInfo.m_hitDistanceWU > this.m_spawnRange)
				{
					return false;
				}
				spawnInfo.m_outOfBounds = false;
				Terrain terrain;
				if (this.m_checkHitInfo.collider is TerrainCollider)
				{
					terrain = this.m_checkHitInfo.transform.GetComponent<Terrain>();
					spawnInfo.m_wasVirginTerrain = true;
				}
				else
				{
					terrain = TerrainHelper.GetTerrain(this.m_checkHitInfo.point);
				}
				if (terrain != null)
				{
					spawnInfo.m_hitTerrain = terrain;
					spawnInfo.m_terrainHeightWU = terrain.SampleHeight(this.m_checkHitInfo.point);
					Vector3 vector = terrain.transform.InverseTransformPoint(this.m_checkHitInfo.point);
					Vector3 hitLocationNU = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector.z));
					spawnInfo.m_hitLocationNU = hitLocationNU;
					spawnInfo.m_terrainSlopeWU = terrain.terrainData.GetSteepness(hitLocationNU.x, hitLocationNU.z);
					spawnInfo.m_areaHitSlopeWU = (spawnInfo.m_areaMinSlopeWU = (spawnInfo.m_areaAvgSlopeWU = (spawnInfo.m_areaMaxSlopeWU = spawnInfo.m_terrainSlopeWU)));
					spawnInfo.m_terrainNormalWU = terrain.terrainData.GetInterpolatedNormal(hitLocationNU.x, hitLocationNU.z);
					if (spawnInfo.m_wasVirginTerrain && !Utils.Math_ApproximatelyEqual(spawnInfo.m_hitLocationWU.y, spawnInfo.m_terrainHeightWU, GaiaConstants.VirginTerrainCheckThreshold))
					{
						spawnInfo.m_wasVirginTerrain = false;
					}
					if (this.m_textureMapCache != null && this.m_textureMapCache.Count > 0)
					{
						List<HeightMap> list = this.m_textureMapCache[terrain.GetInstanceID()];
						for (int i = 0; i < spawnInfo.m_textureStrengths.Length; i++)
						{
							spawnInfo.m_textureStrengths[i] = list[i][hitLocationNU.z, hitLocationNU.x];
						}
					}
					else
					{
						float[,,] alphamaps = terrain.terrainData.GetAlphamaps((int)(hitLocationNU.x * (float)(terrain.terrainData.alphamapWidth - 1)), (int)(hitLocationNU.z * (float)(terrain.terrainData.alphamapHeight - 1)), 1, 1);
						for (int j = 0; j < spawnInfo.m_textureStrengths.Length; j++)
						{
							spawnInfo.m_textureStrengths[j] = alphamaps[0, 0, j];
						}
					}
				}
				return true;
			}
			return false;
		}

		public bool CheckLocationBounds(ref SpawnInfo spawnInfo, float distance)
		{
			spawnInfo.m_areaHitSlopeWU = (spawnInfo.m_areaMinSlopeWU = (spawnInfo.m_areaAvgSlopeWU = (spawnInfo.m_areaMaxSlopeWU = spawnInfo.m_terrainSlopeWU)));
			if (spawnInfo.m_areaHitsWU == null)
			{
				spawnInfo.m_areaHitsWU = new Vector3[4];
			}
			spawnInfo.m_areaHitsWU[0] = new Vector3(spawnInfo.m_hitLocationWU.x + distance, spawnInfo.m_hitLocationWU.y + 3000f, spawnInfo.m_hitLocationWU.z);
			spawnInfo.m_areaHitsWU[1] = new Vector3(spawnInfo.m_hitLocationWU.x - distance, spawnInfo.m_hitLocationWU.y + 3000f, spawnInfo.m_hitLocationWU.z);
			spawnInfo.m_areaHitsWU[2] = new Vector3(spawnInfo.m_hitLocationWU.x, spawnInfo.m_hitLocationWU.y + 3000f, spawnInfo.m_hitLocationWU.z + distance);
			spawnInfo.m_areaHitsWU[3] = new Vector3(spawnInfo.m_hitLocationWU.x, spawnInfo.m_hitLocationWU.y + 3000f, spawnInfo.m_hitLocationWU.z - distance);
			Vector3 vector = Vector3.zero;
			Vector3 zero = Vector3.zero;
			RaycastHit raycastHit;
			if (!Physics.SphereCast(new Vector3(spawnInfo.m_hitLocationWU.x, spawnInfo.m_hitLocationWU.y + 3000f, spawnInfo.m_hitLocationWU.z), distance, Vector3.down, out raycastHit, float.PositiveInfinity, this.m_spawnCollisionLayers))
			{
				return false;
			}
			Terrain terrain;
			if (spawnInfo.m_wasVirginTerrain)
			{
				if (raycastHit.collider is TerrainCollider)
				{
					terrain = raycastHit.transform.GetComponent<Terrain>();
					float b = terrain.SampleHeight(raycastHit.point);
					if (!Utils.Math_ApproximatelyEqual(raycastHit.point.y, b, GaiaConstants.VirginTerrainCheckThreshold))
					{
						spawnInfo.m_wasVirginTerrain = false;
					}
				}
				else
				{
					spawnInfo.m_wasVirginTerrain = false;
				}
			}
			if (!Physics.Raycast(spawnInfo.m_areaHitsWU[0], Vector3.down, out raycastHit, float.PositiveInfinity, this.m_spawnCollisionLayers))
			{
				return false;
			}
			spawnInfo.m_areaHitsWU[0] = raycastHit.point;
			terrain = raycastHit.transform.GetComponent<Terrain>();
			if (terrain == null)
			{
				terrain = TerrainHelper.GetTerrain(raycastHit.point);
			}
			if (terrain != null)
			{
				float b = terrain.SampleHeight(raycastHit.point);
				vector = terrain.transform.InverseTransformPoint(raycastHit.point);
				zero = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector.z));
				float steepness = terrain.terrainData.GetSteepness(zero.x, zero.z);
				spawnInfo.m_areaAvgSlopeWU += steepness;
				if (steepness > spawnInfo.m_areaMaxSlopeWU)
				{
					spawnInfo.m_areaMaxSlopeWU = steepness;
				}
				if (steepness < spawnInfo.m_areaMinSlopeWU)
				{
					spawnInfo.m_areaMinSlopeWU = steepness;
				}
				if (spawnInfo.m_wasVirginTerrain)
				{
					if (raycastHit.collider is TerrainCollider)
					{
						if (!Utils.Math_ApproximatelyEqual(raycastHit.point.y, b, GaiaConstants.VirginTerrainCheckThreshold))
						{
							spawnInfo.m_wasVirginTerrain = false;
						}
					}
					else
					{
						spawnInfo.m_wasVirginTerrain = false;
					}
				}
			}
			if (!Physics.Raycast(spawnInfo.m_areaHitsWU[1], Vector3.down, out raycastHit, float.PositiveInfinity, this.m_spawnCollisionLayers))
			{
				return false;
			}
			spawnInfo.m_areaHitsWU[1] = raycastHit.point;
			terrain = raycastHit.transform.GetComponent<Terrain>();
			if (terrain == null)
			{
				terrain = TerrainHelper.GetTerrain(raycastHit.point);
			}
			if (terrain != null)
			{
				float b = terrain.SampleHeight(raycastHit.point);
				vector = terrain.transform.InverseTransformPoint(raycastHit.point);
				zero = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector.z));
				float steepness = terrain.terrainData.GetSteepness(zero.x, zero.z);
				spawnInfo.m_areaAvgSlopeWU += steepness;
				if (steepness > spawnInfo.m_areaMaxSlopeWU)
				{
					spawnInfo.m_areaMaxSlopeWU = steepness;
				}
				if (steepness < spawnInfo.m_areaMinSlopeWU)
				{
					spawnInfo.m_areaMinSlopeWU = steepness;
				}
				if (spawnInfo.m_wasVirginTerrain)
				{
					if (raycastHit.collider is TerrainCollider)
					{
						if (!Utils.Math_ApproximatelyEqual(raycastHit.point.y, b, GaiaConstants.VirginTerrainCheckThreshold))
						{
							spawnInfo.m_wasVirginTerrain = false;
						}
					}
					else
					{
						spawnInfo.m_wasVirginTerrain = false;
					}
				}
			}
			if (!Physics.Raycast(spawnInfo.m_areaHitsWU[2], Vector3.down, out raycastHit, float.PositiveInfinity, this.m_spawnCollisionLayers))
			{
				return false;
			}
			spawnInfo.m_areaHitsWU[2] = raycastHit.point;
			terrain = raycastHit.transform.GetComponent<Terrain>();
			if (terrain == null)
			{
				terrain = TerrainHelper.GetTerrain(raycastHit.point);
			}
			if (terrain != null)
			{
				float b = terrain.SampleHeight(raycastHit.point);
				vector = terrain.transform.InverseTransformPoint(raycastHit.point);
				zero = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector.z));
				float steepness = terrain.terrainData.GetSteepness(zero.x, zero.z);
				spawnInfo.m_areaAvgSlopeWU += steepness;
				if (steepness > spawnInfo.m_areaMaxSlopeWU)
				{
					spawnInfo.m_areaMaxSlopeWU = steepness;
				}
				if (steepness < spawnInfo.m_areaMinSlopeWU)
				{
					spawnInfo.m_areaMinSlopeWU = steepness;
				}
				if (spawnInfo.m_wasVirginTerrain)
				{
					if (raycastHit.collider is TerrainCollider)
					{
						if (!Utils.Math_ApproximatelyEqual(raycastHit.point.y, b, GaiaConstants.VirginTerrainCheckThreshold))
						{
							spawnInfo.m_wasVirginTerrain = false;
						}
					}
					else
					{
						spawnInfo.m_wasVirginTerrain = false;
					}
				}
			}
			if (!Physics.Raycast(spawnInfo.m_areaHitsWU[3], Vector3.down, out raycastHit, float.PositiveInfinity, this.m_spawnCollisionLayers))
			{
				return false;
			}
			spawnInfo.m_areaHitsWU[3] = raycastHit.point;
			terrain = raycastHit.transform.GetComponent<Terrain>();
			if (terrain == null)
			{
				terrain = TerrainHelper.GetTerrain(raycastHit.point);
			}
			if (terrain != null)
			{
				float b = terrain.SampleHeight(raycastHit.point);
				vector = terrain.transform.InverseTransformPoint(raycastHit.point);
				zero = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector.z));
				float steepness = terrain.terrainData.GetSteepness(zero.x, zero.z);
				spawnInfo.m_areaAvgSlopeWU += steepness;
				if (steepness > spawnInfo.m_areaMaxSlopeWU)
				{
					spawnInfo.m_areaMaxSlopeWU = steepness;
				}
				if (steepness < spawnInfo.m_areaMinSlopeWU)
				{
					spawnInfo.m_areaMinSlopeWU = steepness;
				}
				if (spawnInfo.m_wasVirginTerrain)
				{
					if (raycastHit.collider is TerrainCollider)
					{
						if (!Utils.Math_ApproximatelyEqual(raycastHit.point.y, b, GaiaConstants.VirginTerrainCheckThreshold))
						{
							spawnInfo.m_wasVirginTerrain = false;
						}
					}
					else
					{
						spawnInfo.m_wasVirginTerrain = false;
					}
				}
			}
			spawnInfo.m_areaAvgSlopeWU /= 5f;
			float num = spawnInfo.m_areaHitsWU[0].y - spawnInfo.m_areaHitsWU[1].y;
			float num2 = spawnInfo.m_areaHitsWU[2].y - spawnInfo.m_areaHitsWU[3].y;
			spawnInfo.m_areaHitSlopeWU = Utils.Math_Clamp(0f, 90f, (float)Math.Sqrt((double)(num * num + num2 * num2)));
			return true;
		}

		public void UpdateCounters()
		{
			this.m_totalRuleCnt = 0;
			this.m_activeRuleCnt = 0;
			this.m_inactiveRuleCnt = 0;
			this.m_maxInstanceCnt = 0UL;
			this.m_activeInstanceCnt = 0UL;
			this.m_inactiveInstanceCnt = 0UL;
			this.m_totalInstanceCnt = 0UL;
			foreach (SpawnRule spawnRule in this.m_spawnerRules)
			{
				this.m_totalRuleCnt++;
				if (spawnRule.m_isActive)
				{
					this.m_activeRuleCnt++;
					this.m_maxInstanceCnt += spawnRule.m_maxInstances;
					this.m_activeInstanceCnt += spawnRule.m_activeInstanceCnt;
					this.m_inactiveInstanceCnt += spawnRule.m_inactiveInstanceCnt;
					this.m_totalInstanceCnt += spawnRule.m_activeInstanceCnt + spawnRule.m_inactiveInstanceCnt;
				}
				else
				{
					this.m_inactiveRuleCnt++;
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (this.m_showGizmos)
			{
				if (this.m_spawnerShape == GaiaConstants.SpawnerShape.Sphere)
				{
					if (this.m_mode == GaiaConstants.OperationMode.RuntimeTriggeredInterval)
					{
						Gizmos.color = Color.yellow;
						Gizmos.DrawWireSphere(base.transform.position, this.m_triggerRange);
					}
					Gizmos.color = Color.red;
					Gizmos.DrawWireSphere(base.transform.position, this.m_spawnRange);
				}
				else
				{
					if (this.m_mode == GaiaConstants.OperationMode.RuntimeTriggeredInterval)
					{
						Gizmos.color = Color.yellow;
						Gizmos.DrawWireCube(base.transform.position, new Vector3(this.m_triggerRange * 2f, this.m_triggerRange * 2f, this.m_triggerRange * 2f));
					}
					Gizmos.color = Color.red;
					Gizmos.DrawWireCube(base.transform.position, new Vector3(this.m_spawnRange * 2f, this.m_spawnRange * 2f, this.m_spawnRange * 2f));
				}
				if (this.m_resources != null)
				{
					Bounds bounds = default(Bounds);
					if (TerrainHelper.GetTerrainBounds(base.transform.position, ref bounds))
					{
						bounds.center = new Vector3(bounds.center.x, this.m_resources.m_seaLevel, bounds.center.z);
						bounds.size = new Vector3(bounds.size.x, 0.05f, bounds.size.z);
						Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, Color.blue.a / 4f);
						Gizmos.DrawCube(bounds.center, bounds.size);
					}
				}
			}
			this.UpdateCounters();
		}

		public void CacheTextureMapsFromTerrain(int terrainID)
		{
			if (this.m_textureMapCache == null)
			{
				this.m_textureMapCache = new Dictionary<int, List<HeightMap>>();
			}
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				if (terrain.GetInstanceID() == terrainID)
				{
					float[,,] alphamaps = terrain.terrainData.GetAlphamaps(0, 0, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);
					List<HeightMap> list = new List<HeightMap>();
					for (int j = 0; j < terrain.terrainData.alphamapLayers; j++)
					{
						HeightMap item = new HeightMap(alphamaps, j);
						list.Add(item);
					}
					this.m_textureMapCache[terrainID] = list;
					return;
				}
			}
			UnityEngine.Debug.LogError("Attempted to get textures on terrain that does not exist!");
		}

		public List<HeightMap> GetTextureMaps(int terrainID)
		{
			List<HeightMap> result;
			if (!this.m_textureMapCache.TryGetValue(terrainID, out result))
			{
				return null;
			}
			return result;
		}

		public void SaveTextureMapsToTerrain(int terrainID)
		{
			List<HeightMap> list;
			if (!this.m_textureMapCache.TryGetValue(terrainID, out list))
			{
				UnityEngine.Debug.LogError("Texture map list was not found for terrain ID : " + terrainID + " !");
				return;
			}
			if (list.Count <= 0)
			{
				UnityEngine.Debug.LogError("Texture map list was empty for terrain ID : " + terrainID + " !");
				return;
			}
			int i = 0;
			while (i < Terrain.activeTerrains.Length)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				if (terrain.GetInstanceID() == terrainID)
				{
					if (list.Count != terrain.terrainData.alphamapLayers)
					{
						UnityEngine.Debug.LogError("Texture map prototype list does not match terrain prototype list for terrain ID : " + terrainID + " !");
						return;
					}
					float[,,] array = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, terrain.terrainData.alphamapLayers];
					for (int j = 0; j < terrain.terrainData.alphamapLayers; j++)
					{
						HeightMap heightMap = list[j];
						for (int k = 0; k < heightMap.Width(); k++)
						{
							for (int l = 0; l < heightMap.Depth(); l++)
							{
								array[k, l, j] = heightMap[k, l];
							}
						}
					}
					terrain.terrainData.SetAlphamaps(0, 0, array);
					return;
				}
				else
				{
					i++;
				}
			}
			UnityEngine.Debug.LogError("Attempted to locate a terrain that does not exist!");
		}

		public void DeleteTextureMapCache()
		{
			this.m_textureMapCache = new Dictionary<int, List<HeightMap>>();
		}

		public void SetTextureMapsDirty()
		{
			this.m_textureMapsDirty = true;
		}

		public void CacheDetailMapsFromTerrain(int terrainID)
		{
			if (this.m_detailMapCache == null)
			{
				this.m_detailMapCache = new Dictionary<int, List<HeightMap>>();
			}
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				if (terrain.GetInstanceID() == terrainID)
				{
					List<HeightMap> list = new List<HeightMap>();
					for (int j = 0; j < terrain.terrainData.detailPrototypes.Length; j++)
					{
						HeightMap item = new HeightMap(terrain.terrainData.GetDetailLayer(0, 0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, j));
						list.Add(item);
					}
					this.m_detailMapCache[terrainID] = list;
					return;
				}
			}
			UnityEngine.Debug.LogError("Attempted to get details on terrain that does not exist!");
		}

		public void SaveDetailMapsToTerrain(int terrainID)
		{
			List<HeightMap> list;
			if (!this.m_detailMapCache.TryGetValue(terrainID, out list))
			{
				UnityEngine.Debug.LogWarning(string.Concat(new object[]
				{
					base.gameObject.name,
					"Detail map list was not found for terrain ID : ",
					terrainID,
					" !"
				}));
				return;
			}
			if (list.Count <= 0)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new object[]
				{
					base.gameObject.name,
					": Detail map list was empty for terrain ID : ",
					terrainID,
					" !"
				}));
				return;
			}
			int i = 0;
			while (i < Terrain.activeTerrains.Length)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				if (terrain.GetInstanceID() == terrainID)
				{
					if (list.Count != terrain.terrainData.detailPrototypes.Length)
					{
						UnityEngine.Debug.LogError("Detail map protoype list does not match terrain prototype list for terrain ID : " + terrainID + " !");
						return;
					}
					int[,] array = new int[list[0].Width(), list[0].Depth()];
					for (int j = 0; j < terrain.terrainData.detailPrototypes.Length; j++)
					{
						HeightMap heightMap = list[j];
						for (int k = 0; k < heightMap.Width(); k++)
						{
							for (int l = 0; l < heightMap.Depth(); l++)
							{
								array[k, l] = (int)heightMap[k, l];
							}
						}
						terrain.terrainData.SetDetailLayer(0, 0, j, array);
					}
					terrain.Flush();
					return;
				}
				else
				{
					i++;
				}
			}
			UnityEngine.Debug.LogError("Attempted to locate a terrain that does not exist!");
		}

		public List<HeightMap> GetDetailMaps(int terrainID)
		{
			List<HeightMap> result;
			if (!this.m_detailMapCache.TryGetValue(terrainID, out result))
			{
				return null;
			}
			return result;
		}

		public HeightMap GetDetailMap(int terrainID, int detailIndex)
		{
			List<HeightMap> list;
			if (!this.m_detailMapCache.TryGetValue(terrainID, out list))
			{
				return null;
			}
			if (detailIndex >= 0 && detailIndex < list.Count)
			{
				return list[detailIndex];
			}
			return null;
		}

		public void DeleteDetailMapCache()
		{
			this.m_detailMapCache = new Dictionary<int, List<HeightMap>>();
		}

		public void AddToSession(GaiaOperation.OperationType opType, string opName)
		{
			GaiaSessionManager sessionManager = GaiaSessionManager.GetSessionManager(false);
			if (sessionManager != null && !sessionManager.IsLocked())
			{
				GaiaOperation gaiaOperation = new GaiaOperation();
				gaiaOperation.m_description = opName;
				gaiaOperation.m_generatedByID = this.m_spawnerID;
				gaiaOperation.m_generatedByName = base.transform.name;
				gaiaOperation.m_generatedByType = base.GetType().ToString();
				gaiaOperation.m_isActive = true;
				gaiaOperation.m_operationDateTime = DateTime.Now.ToString();
				gaiaOperation.m_operationType = opType;
				gaiaOperation.m_operationDataJson = new string[1];
				gaiaOperation.m_operationDataJson[0] = this.SerialiseJson();
				sessionManager.AddOperation(gaiaOperation);
				sessionManager.AddResource(this.m_resources);
			}
		}

		public string SerialiseJson()
		{
			fsSerializer fsSerializer = new fsSerializer();
			fsData data;
			fsSerializer.TrySerialize<Spawner>(this, out data);
			return fsJsonPrinter.CompressedJson(data);
		}

		public void DeSerialiseJson(string json)
		{
			fsData data = fsJsonParser.Parse(json);
			fsSerializer fsSerializer = new fsSerializer();
			Spawner spawner = this;
			fsSerializer.TryDeserialize<Spawner>(data, ref spawner);
			spawner.m_resources = (Utils.GetAsset(this.m_resourcesPath, typeof(GaiaResource)) as GaiaResource);
		}

		public void FlattenTerrain()
		{
			this.AddToSession(GaiaOperation.OperationType.FlattenTerrain, "Flattening terrain");
			GaiaWorldManager gaiaWorldManager = new GaiaWorldManager(Terrain.activeTerrains);
			gaiaWorldManager.FlattenWorld();
		}

		public void SmoothTerrain()
		{
			this.AddToSession(GaiaOperation.OperationType.SmoothTerrain, "Smoothing terrain");
			GaiaWorldManager gaiaWorldManager = new GaiaWorldManager(Terrain.activeTerrains);
			gaiaWorldManager.SmoothWorld();
		}

		public void ClearTrees()
		{
			this.AddToSession(GaiaOperation.OperationType.ClearTrees, "Clearing terrain trees");
			TerrainHelper.ClearTrees();
		}

		public void ClearDetails()
		{
			this.AddToSession(GaiaOperation.OperationType.ClearDetails, "Clearing terrain details");
			TerrainHelper.ClearDetails();
		}

		public void CacheHeightMapFromTerrain(int terrainID)
		{
			if (this.m_heightMapCache == null)
			{
				this.m_heightMapCache = new Dictionary<int, UnityHeightMap>();
			}
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				if (terrain.GetInstanceID() == terrainID)
				{
					this.m_heightMapCache[terrainID] = new UnityHeightMap(terrain);
					return;
				}
			}
			UnityEngine.Debug.LogError("Attempted to get height maps on a terrain that does not exist!");
		}

		public UnityHeightMap GetHeightMap(int terrainID)
		{
			UnityHeightMap result;
			if (!this.m_heightMapCache.TryGetValue(terrainID, out result))
			{
				return null;
			}
			return result;
		}

		public void SaveHeightMapToTerrain(int terrainID)
		{
			UnityHeightMap unityHeightMap;
			if (!this.m_heightMapCache.TryGetValue(terrainID, out unityHeightMap))
			{
				UnityEngine.Debug.LogError("Heightmap was not found for terrain ID : " + terrainID + " !");
				return;
			}
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				Terrain terrain = Terrain.activeTerrains[i];
				if (terrain.GetInstanceID() == terrainID)
				{
					unityHeightMap.SaveToTerrain(terrain);
					return;
				}
			}
			UnityEngine.Debug.LogError("Attempted to locate a terrain that does not exist!");
		}

		public void DeleteHeightMapCache()
		{
			this.m_heightMapCache = new Dictionary<int, UnityHeightMap>();
		}

		public void SetHeightMapsDirty()
		{
			this.m_heightMapDirty = true;
		}

		public void CacheStamps(List<string> stampList)
		{
			if (this.m_stampCache == null)
			{
				this.m_stampCache = new Dictionary<string, HeightMap>();
			}
			for (int i = 0; i < stampList.Count; i++)
			{
			}
		}

		private void CacheTaggedGameObjectsFromScene(List<string> tagList)
		{
			this.m_taggedGameObjectCache = new Dictionary<string, Quadtree<GameObject>>();
			Rect boundaries = new Rect(Terrain.activeTerrain.transform.position.x, Terrain.activeTerrain.transform.position.z, Terrain.activeTerrain.terrainData.size.x, Terrain.activeTerrain.terrainData.size.z);
			for (int i = 0; i < tagList.Count; i++)
			{
				string text = tagList[i].Trim();
				bool flag = false;
				if (!string.IsNullOrEmpty(text))
				{
					flag = true;
				}
				if (flag)
				{
					Quadtree<GameObject> quadtree = null;
					if (!this.m_taggedGameObjectCache.TryGetValue(text, out quadtree))
					{
						quadtree = new Quadtree<GameObject>(boundaries, 32);
						this.m_taggedGameObjectCache.Add(text, quadtree);
					}
					foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(text))
					{
						Vector2 vector = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
						if (boundaries.Contains(vector))
						{
							quadtree.Insert(vector, gameObject);
						}
					}
				}
			}
		}

		private void DeleteTagCache()
		{
			this.m_taggedGameObjectCache = null;
		}

		public List<GameObject> GetNearbyObjects(List<string> tagList, Rect area)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < tagList.Count; i++)
			{
				Quadtree<GameObject> quadtree = null;
				string key = tagList[i];
				if (this.m_taggedGameObjectCache.TryGetValue(key, out quadtree))
				{
					IEnumerable<GameObject> enumerable = quadtree.Find(area);
					foreach (GameObject item in enumerable)
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		public GameObject GetClosestObject(List<string> tagList, Rect area)
		{
			float num = float.MaxValue;
			GameObject result = null;
			for (int i = 0; i < tagList.Count; i++)
			{
				Quadtree<GameObject> quadtree = null;
				string key = tagList[i];
				if (this.m_taggedGameObjectCache.TryGetValue(key, out quadtree))
				{
					IEnumerable<GameObject> enumerable = quadtree.Find(area);
					foreach (GameObject gameObject in enumerable)
					{
						float num2 = Vector2.Distance(area.center, new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
						if (num2 < num)
						{
							num = num2;
							result = gameObject;
						}
					}
				}
			}
			return result;
		}

		public GameObject GetClosestObject(string tag, Rect area)
		{
			float num = float.MaxValue;
			GameObject result = null;
			Quadtree<GameObject> quadtree = null;
			if (this.m_taggedGameObjectCache.TryGetValue(tag, out quadtree))
			{
				IEnumerable<GameObject> enumerable = quadtree.Find(area);
				foreach (GameObject gameObject in enumerable)
				{
					float num2 = Vector2.Distance(area.center, new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
					if (num2 < num)
					{
						num = num2;
						result = gameObject;
					}
				}
			}
			return result;
		}

		public void ResetRandomGenertor()
		{
			this.m_rndGenerator = new XorshiftPlus(this.m_seed);
		}

		public int GetRandomInt(int min, int max)
		{
			return this.m_rndGenerator.Next(min, max);
		}

		public float GetRandomFloat(float min, float max)
		{
			return this.m_rndGenerator.Next(min, max);
		}

		public Vector3 GetRandomV3(float range)
		{
			return this.m_rndGenerator.NextVector(-range, range);
		}

		[fsIgnore]
		public GaiaResource m_resources;

		public string m_resourcesPath;

		public string m_spawnerID = Guid.NewGuid().ToString();

		public GaiaConstants.OperationMode m_mode;

		public int m_seed = DateTime.Now.Millisecond;

		public GaiaConstants.SpawnerShape m_spawnerShape;

		public GaiaConstants.SpawnerRuleSelector m_spawnRuleSelector = GaiaConstants.SpawnerRuleSelector.WeightedFittest;

		public GaiaConstants.SpawnerLocation m_spawnLocationAlgorithm;

		public GaiaConstants.SpawnerLocationCheckType m_spawnLocationCheckType;

		public float m_locationIncrement = 1f;

		public float m_maxJitteredLocationOffsetPct = 0.9f;

		public int m_locationChecksPerInt = 1;

		public int m_maxRandomClusterSize = 50;

		public float m_spawnRange = 500f;

		public AnimationCurve m_spawnFitnessAttenuator = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		public GaiaConstants.ImageFitnessFilterMode m_areaMaskMode;

		public bool m_enableColliderCacheAtRuntime;

		public Texture2D m_imageMask;

		public bool m_imageMaskInvert;

		public bool m_imageMaskNormalise;

		public bool m_imageMaskFlip;

		public int m_imageMaskSmoothIterations = 3;

		[fsIgnore]
		public HeightMap m_imageMaskHM;

		private FractalGenerator m_noiseGenerator;

		public float m_noiseMaskSeed;

		public int m_noiseMaskOctaves = 8;

		public float m_noiseMaskPersistence = 0.25f;

		public float m_noiseMaskFrequency = 1f;

		public float m_noiseMaskLacunarity = 1.5f;

		public float m_noiseZoom = 10f;

		public bool m_noiseInvert;

		public float m_spawnInterval = 5f;

		public string m_triggerTags = "Player";

		public float m_triggerRange = 130f;

		public List<SpawnRule> m_spawnerRules = new List<SpawnRule>();

		public LayerMask m_spawnCollisionLayers;

		public int m_spawnColliderLayer;

		public bool m_showGizmos = true;

		public bool m_showDebug;

		public bool m_showStatistics = true;

		public bool m_showTerrainHelper = true;

		public XorshiftPlus m_rndGenerator;

		private XorshiftPlus m_rndSubGenerator;

		private bool m_cacheDetails;

		private Dictionary<int, List<HeightMap>> m_detailMapCache = new Dictionary<int, List<HeightMap>>();

		private bool m_cacheTextures;

		private bool m_textureMapsDirty;

		private Dictionary<int, List<HeightMap>> m_textureMapCache = new Dictionary<int, List<HeightMap>>();

		private bool m_cacheTags;

		private Dictionary<string, Quadtree<GameObject>> m_taggedGameObjectCache = new Dictionary<string, Quadtree<GameObject>>();

		private bool m_cacheHeightMaps;

		private bool m_heightMapDirty;

		private Dictionary<int, UnityHeightMap> m_heightMapCache = new Dictionary<int, UnityHeightMap>();

		private Dictionary<string, HeightMap> m_stampCache = new Dictionary<string, HeightMap>();

		[fsIgnore]
		public GameObject m_areaBoundsColliderCache;

		[fsIgnore]
		public GameObject m_goColliderCache;

		[fsIgnore]
		public GameObject m_goParentGameObject;

		private bool m_cancelSpawn;

		public int m_totalRuleCnt;

		public int m_activeRuleCnt;

		public int m_inactiveRuleCnt;

		public ulong m_maxInstanceCnt;

		public ulong m_activeInstanceCnt;

		public ulong m_inactiveInstanceCnt;

		public ulong m_totalInstanceCnt;

		private float m_terrainHeight;

		private float m_checkDistance;

		private RaycastHit m_checkHitInfo = default(RaycastHit);

		public IEnumerator m_updateCoroutine;

		public float m_updateTimeAllowed = 0.0333333351f;

		public float m_spawnProgress;

		public bool m_spawnComplete = true;

		public Bounds m_spawnerBounds = default(Bounds);

		private bool m_isTextureSpawner;

		private bool m_isDetailSpawner;

		private bool m_isTreeSpawnwer;

		private bool m_isGameObjectSpawner;

		private class SpawnLocation
		{
			public Vector3 m_location;

			public float m_seedDistance;
		}
	}
}
