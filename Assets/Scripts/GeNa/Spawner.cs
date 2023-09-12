using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeNa
{
	public class Spawner : MonoBehaviour
	{
		public void SetDefaults()
		{
			if (this.m_defaults != null)
			{
				return;
			}
			if (this.m_defaults == null)
			{
				this.m_defaults = ScriptableObject.CreateInstance<GenaDefaults>();
			}
			this.m_advShowDetailedHelp = this.m_defaults.m_showDetailedHelp;
			this.m_advShowMouseOverHelp = this.m_defaults.m_showTooltips;
			this.m_autoProbe = this.m_defaults.m_autoLightProbe;
			this.m_minProbeGroupDistance = this.m_defaults.m_minProbeGroupDistance;
			this.m_minProbeDistance = this.m_defaults.m_minProbeDistance;
			this.m_autoOptimise = this.m_defaults.m_autoOptimize;
			this.m_maxSizeToOptimize = this.m_defaults.m_maxOptimizeSize;
			this.m_randomSeed = UnityEngine.Random.Range(100, 999999);
			this.m_randomGenerator = new XorshiftPlus(this.m_randomSeed);
		}

		public void SetSpawnOriginAndUpdateRanges(Transform groundObject, Vector3 location, Vector3 normal)
		{
			this.m_spawnOriginLocation = location;
			this.m_spawnOriginNormal = normal;
			this.m_spawnOriginBounds = new Bounds(location, new Vector3(this.m_maxSpawnRange, 5000f, this.m_maxSpawnRange));
			this.m_spawnOriginGroundTransform = groundObject;
			if (groundObject != null)
			{
				this.m_spawnOriginObjectID = groundObject.GetInstanceID();
				if (groundObject.GetComponent<Terrain>() != null)
				{
					this.m_spawnOriginIsTerrain = true;
				}
				else
				{
					this.m_spawnOriginIsTerrain = false;
				}
			}
			else
			{
				this.m_spawnOriginObjectID = int.MinValue;
				this.m_spawnOriginIsTerrain = false;
			}
			if (this.m_critCheckTextures)
			{
				Terrain terrain = this.GetTerrain(this.m_spawnOriginLocation);
				if (terrain != null)
				{
					Vector3 vector = terrain.transform.InverseTransformPoint(this.m_spawnOriginLocation);
					Vector3 vector2 = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector.z));
					float[,,] alphamaps = terrain.terrainData.GetAlphamaps((int)(vector2.x * (float)(terrain.terrainData.alphamapWidth - 1)), (int)(vector2.z * (float)(terrain.terrainData.alphamapHeight - 1)), 1, 1);
					this.m_critMaxSelectedTexture = alphamaps.GetLength(2) - 1;
					float num = 0f;
					for (int i = 0; i <= this.m_critMaxSelectedTexture; i++)
					{
						if (alphamaps[0, 0, i] > num)
						{
							num = alphamaps[0, 0, i];
							this.m_critTextureStrength = num;
							this.m_critSelectedTextureIdx = i;
						}
					}
					this.m_critSelectedTextureName = terrain.terrainData.splatPrototypes[this.m_critSelectedTextureIdx].texture.name;
				}
				else
				{
					this.m_critSelectedTextureName = "Missing terrain";
				}
			}
			if (!Application.isPlaying)
			{
				this.m_treeManager.LoadTreesFromTerrain();
			}
			if (this.m_critCheckMask && this.m_critMaskType == Constants.MaskType.Image && this.m_critMaskImage != null)
			{
				int width = this.m_critMaskImage.width;
				int height = this.m_critMaskImage.height;
				this.m_critMaskImageData = new Heightmap(width, height);
				this.m_critMaskAlphaData = new Heightmap(width, height);
				for (int j = 0; j < width; j++)
				{
					for (int k = 0; k < height; k++)
					{
						Color pixel = this.m_critMaskImage.GetPixel(j, k);
						this.m_critMaskImageData[j, k] = pixel.r * 2.55E+08f + pixel.g * 255000f + pixel.b * 255f;
						this.m_critMaskAlphaData[j, k] = pixel.a;
					}
				}
			}
			this.UpdateTargetSpawnerRanges();
			this.UpdateChildSpawners();
			foreach (Spawner spawner in this.m_childSpawners)
			{
				if (spawner != null)
				{
					spawner.SetSpawnOriginAndUpdateRanges(groundObject, location, normal);
				}
			}
		}

		public void UpdateTargetSpawnerRanges()
		{
			this.m_critMinHeight = this.m_spawnOriginLocation.y - this.m_critHeightVariance / 2f;
			if (this.m_critMinHeight < this.m_critMinSpawnHeight)
			{
				this.m_critMinHeight = this.m_critMinSpawnHeight;
			}
			this.m_critMaxHeight = this.m_spawnOriginLocation.y + this.m_critHeightVariance / 2f;
			if (this.m_critMaxHeight < this.m_critMinHeight)
			{
				this.m_critMaxHeight = this.m_critMinHeight;
			}
			float num = Vector3.Angle(Vector3.up, this.m_spawnOriginNormal);
			this.m_critMinSlope = Mathf.Clamp(num - this.m_critSlopeVariance / 2f, 0f, 90f);
			this.m_critMaxSlope = Mathf.Clamp(num + this.m_critSlopeVariance / 2f, 0f, 90f);
			this.m_critMinTextureStrength = Mathf.Clamp01(this.m_critTextureStrength - this.m_critTextureVariance / 2f);
			this.m_critMaxTextureStrength = Mathf.Clamp01(this.m_critTextureStrength + this.m_critTextureVariance / 2f);
			this.m_critMaskFractalMin = Mathf.Clamp01(this.m_critMaskFractalMidpoint - this.m_critMaskFractalRange / 2f);
			this.m_critMaskFractalMax = Mathf.Clamp01(this.m_critMaskFractalMidpoint + this.m_critMaskFractalRange / 2f);
			this.m_needsVisualisationUpdate = true;
		}

		private void SetSpawnOrigin(Vector3 location)
		{
			this.m_spawnOriginLocation = location;
			if (this.m_spawnOriginIsTerrain)
			{
				Terrain terrain = this.GetTerrain(location);
				if (terrain != null)
				{
					this.m_spawnOriginLocation.y = terrain.SampleHeight(location);
				}
			}
			this.m_spawnOriginBounds = new Bounds(location, new Vector3(this.m_maxSpawnRange, 5000f, this.m_maxSpawnRange));
		}

		private void UpdateSpawnerVisualisation()
		{
			this.m_needsVisualisationUpdate = false;
			Vector3 zero = Vector3.zero;
			float num = this.m_maxSpawnRange / 2f;
			Vector3 vector = this.m_spawnOriginLocation + Vector3.one * num;
			Vector3 zero2 = Vector3.zero;
			Vector3 zero3 = Vector3.zero;
			float num2 = 1f;
			Vector3 vector2 = Vector3.zero;
			List<Prototype> list = new List<Prototype>();
			foreach (Prototype prototype in this.m_spawnPrototypes)
			{
				if (prototype.m_active)
				{
					list.Add(prototype);
				}
			}
			int num3 = (int)this.m_maxSpawnRange + 1;
			if (num3 > this.m_maxVisualisationDimensions)
			{
				num3 = this.m_maxVisualisationDimensions + 1;
			}
			float num4 = this.m_maxSpawnRange / ((float)num3 - 1f);
			if (num3 != this.m_fitnessArray.GetLength(0))
			{
				this.m_fitnessArray = new float[num3, num3];
			}
			int i;
			for (i = 0; i < num3; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					this.m_fitnessArray[i, j] = float.MinValue;
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			if (this.m_critVirginCheckType == Constants.VirginCheckType.Bounds)
			{
				float num5 = 0f;
				Vector3 vector3 = Vector3.zero;
				for (int k = 0; k < this.m_spawnPrototypes.Count; k++)
				{
					vector3 = this.m_spawnPrototypes[k].m_extents;
					vector3 += new Vector3((this.m_critBoundsBorder + this.m_spawnPrototypes[k].m_boundsBorder) * vector3.x, (this.m_critBoundsBorder + this.m_spawnPrototypes[k].m_boundsBorder) * vector3.y, (this.m_critBoundsBorder + this.m_spawnPrototypes[k].m_boundsBorder) * vector3.z);
					float num6 = vector3.x * vector3.z;
					if (num5 == 0f)
					{
						num5 = num6;
						vector2 = vector3;
					}
					else if (num6 < num5)
					{
						num5 = num6;
						vector2 = vector3;
					}
				}
			}
			DateTime now = DateTime.Now;
			vector2 += new Vector3(this.m_critBoundsBorder * vector2.x, this.m_critBoundsBorder * vector2.y, this.m_critBoundsBorder * vector2.z);
			Prototype prototype2 = null;
			float rotation = 0f;
			if (Spawner.ApproximatelyEqual(this.m_minRotationY, this.m_maxRotationY, 1.401298E-45f))
			{
				rotation = this.m_minRotationY;
			}
			i = 0;
			zero.x = this.m_spawnOriginLocation.x - num;
			while (zero.x < vector.x)
			{
				int j = 0;
				zero.z = this.m_spawnOriginLocation.z - num;
				while (zero.z < vector.z)
				{
					if (this.CheckLocationForSpawn(zero, rotation, list, out prototype2, out zero2, out zero3, out num2))
					{
						this.m_fitnessArray[i, j] = zero2.y;
						if (this.m_critVirginCheckType == Constants.VirginCheckType.Bounds)
						{
							if (this.CheckBoundedLocationForSpawn(zero2, rotation, null, vector2, true))
							{
								this.m_fitnessArray[i, j] = zero2.y;
							}
							else
							{
								this.m_fitnessArray[i, j] = float.MinValue;
							}
						}
					}
					j++;
					zero.z += num4;
				}
				i++;
				zero.x += num4;
			}
			if ((DateTime.Now - now).TotalMilliseconds > 200.0)
			{
				double totalMilliseconds = (DateTime.Now - now).TotalMilliseconds;
				this.m_maxVisualisationDimensions = (int)((double)((float)this.m_maxVisualisationDimensions) * (200.0 / totalMilliseconds));
				if (this.m_maxVisualisationDimensions < 1)
				{
					this.m_maxVisualisationDimensions = 1;
				}
			}
		}

		private void UpdateChildSpawners()
		{
			this.m_childSpawners.Clear();
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					Spawner component = transform.gameObject.GetComponent<Spawner>();
					if (component != null)
					{
						this.m_childSpawners.Add(component);
						component.m_showGizmos = false;
						component.UpdateChildSpawners();
					}
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
		}

		public void SpawnGlobally()
		{
			float num = float.NaN;
			float num2 = float.NaN;
			float num3 = float.NaN;
			float num4 = float.NaN;
			float num5 = float.NaN;
			if (this.m_spawnOriginIsTerrain)
			{
				foreach (Terrain terrain in Terrain.activeTerrains)
				{
					if (float.IsNaN(num2))
					{
						num2 = terrain.transform.position.y;
						num = terrain.transform.position.x;
						num3 = terrain.transform.position.z;
						num4 = num + terrain.terrainData.size.x;
						num5 = num3 + terrain.terrainData.size.z;
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
						if (terrain.transform.position.x + terrain.terrainData.size.x > num4)
						{
							num4 = terrain.transform.position.x + terrain.terrainData.size.x;
						}
						if (terrain.transform.position.z + terrain.terrainData.size.z > num5)
						{
							num5 = terrain.transform.position.z + terrain.terrainData.size.z;
						}
					}
				}
			}
			else if (this.m_spawnOriginGroundTransform != null)
			{
				Bounds bounds = default(Bounds);
				if (this.GetObjectBounds(this.m_spawnOriginGroundTransform.gameObject, ref bounds))
				{
					num = bounds.min.x;
					num2 = this.m_spawnOriginGroundTransform.position.y;
					num3 = bounds.min.z;
					num4 = bounds.max.x;
					num5 = bounds.max.z;
				}
			}
			Vector3 spawnOriginLocation = this.m_spawnOriginLocation;
			if (!Application.isPlaying)
			{
				this.m_treeManager.LoadTreesFromTerrain();
				if (this.m_autoProbe)
				{
					this.m_probeManager.LoadProbesFromScene();
					if (this.m_probeParent == null)
					{
						this.m_probeParent = GameObject.Find("GeNa Light Probes");
						if (this.m_probeParent == null)
						{
							this.m_probeParent = new GameObject("GeNa Light Probes");
						}
					}
				}
			}
			bool flag = false;
			Vector3 location = new Vector3(num, num2, num3);
			for (float num6 = num + this.JitterAsPct(this.m_maxSpawnRange, 0.25f); num6 < num4; num6 += this.JitterAsPct(this.m_maxSpawnRange, 0.25f))
			{
				for (float num7 = num3 + this.JitterAsPct(this.m_maxSpawnRange, 0.25f); num7 < num5; num7 += this.JitterAsPct(this.m_maxSpawnRange, 0.25f))
				{
					location.x = this.JitterAround(num6, this.m_maxSpawnRange);
					location.z = this.JitterAround(num7, this.m_maxSpawnRange);
					this.Spawn(location, true);
				}
				if (flag)
				{
					break;
				}
			}
			this.SetSpawnOrigin(spawnOriginLocation);
		}

		public void Spawn(Vector3 location, float rotation, bool subSpawn)
		{
			this.m_maxRotationY = rotation;
			this.m_minRotationY = rotation;
			this.Spawn(location, subSpawn);
		}

		public void Spawn(Vector3 location, bool subSpawn)
		{
			if (this.m_spawnPrototypes.Count == 0)
			{
				UnityEngine.Debug.LogWarning("No prototypes to spawn.");
				return;
			}
			this.SetSpawnOrigin(location);
			float num = this.m_maxSpawnRange / 2f;
			Vector3 vector = location;
			Vector3 vector2 = location;
			float alpha = 1f;
			Vector3 vector3 = new Vector3(-num, 0f, -num);
			Vector3 vector4 = new Vector3(num, 0f, num);
			Vector3 b = vector3;
			Vector3 vector5 = vector3;
			vector5.x -= this.m_seedThrowRange;
			Vector3 one = Vector3.one;
			Vector3 vector6 = Vector3.zero;
			Vector3 spawnOriginNormal = this.m_spawnOriginNormal;
			Prototype prototype = null;
			long num2 = 0L;
			long num3 = 0L;
			long num4 = (long)this.m_randomGenerator.Next((int)this.m_minInstances, (int)this.m_maxInstances);
			long num5 = num4 * 20L;
			int num6 = 0;
			float rotation = this.m_randomGenerator.Next(this.m_minRotationY, this.m_maxRotationY);
			GameObject gameObject = null;
			List<Vector3> list = new List<Vector3>();
			List<Prototype> list2 = new List<Prototype>();
			List<GameObject> list3 = new List<GameObject>();
			foreach (Prototype prototype2 in this.m_spawnPrototypes)
			{
				if (prototype2.m_active)
				{
					list2.Add(prototype2);
				}
			}
			if (list2.Count == 0)
			{
				UnityEngine.Debug.LogWarning("No active prototypes to spawn.");
				return;
			}
			float defaultContactOffset = Physics.defaultContactOffset;
			int defaultSolverIterations = Physics.defaultSolverIterations;
			if (!Application.isPlaying)
			{
				Physics.defaultContactOffset = 0.003f;
				Physics.defaultSolverIterations = 25;
			}
			if (!subSpawn && !Application.isPlaying)
			{
				this.m_treeManager.LoadTreesFromTerrain();
				if (this.m_autoProbe)
				{
					this.m_probeManager.LoadProbesFromScene();
					if (this.m_probeParent == null)
					{
						this.m_probeParent = GameObject.Find("GeNa Light Probes");
						if (this.m_probeParent == null)
						{
							this.m_probeParent = new GameObject("GeNa Light Probes");
						}
					}
				}
			}
			if (this.m_spawnAlgorithm == Constants.LocationAlgorithm.Every)
			{
				num5 = (long)(this.m_maxSpawnRange / this.m_seedThrowRange + 1f);
				num5 *= num5;
			}
			while (num3 < num4 && num2 < num5)
			{
				gameObject = null;
				prototype = list2[this.m_randomGenerator.Next(0, list2.Count - 1)];
				rotation = this.m_randomGenerator.Next(this.m_minRotationY, this.m_maxRotationY);
				if (this.m_lastSpawnedObject != null && this.m_rotationAlgorithm != Constants.RotationAlgorithm.Ranged)
				{
					if (this.m_rotationAlgorithm == Constants.RotationAlgorithm.LastSpawnClosest)
					{
						Collider[] componentsInChildren = this.m_lastSpawnedObject.GetComponentsInChildren<Collider>();
						if (componentsInChildren.Length > 0)
						{
							Vector3 a = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
							Vector3 vector7 = Vector3.zero;
							for (int i = 0; i < componentsInChildren.Length; i++)
							{
								vector7 = componentsInChildren[i].ClosestPointOnBounds(vector);
								if (Vector3.Distance(vector7, vector) < Vector3.Distance(a, vector))
								{
									a = vector7;
								}
							}
							rotation = Quaternion.LookRotation(a - vector).eulerAngles.y;
						}
						else
						{
							rotation = Quaternion.LookRotation(this.m_lastSpawnedObject.transform.position - vector).eulerAngles.y;
						}
					}
					else
					{
						rotation = Quaternion.LookRotation(this.m_lastSpawnedObject.transform.position - vector).eulerAngles.y;
					}
				}
				if (this.m_sameScale)
				{
					float num7 = this.m_randomGenerator.Next(this.m_minScaleX, this.m_maxScaleX);
					one = new Vector3(num7, num7, num7);
				}
				else
				{
					one = new Vector3(this.m_randomGenerator.Next(this.m_minScaleX, this.m_maxScaleX), this.m_randomGenerator.Next(this.m_minScaleY, this.m_maxScaleY), this.m_randomGenerator.Next(this.m_minScaleZ, this.m_maxScaleZ));
				}
				if (num3 == 0L && !subSpawn && this.m_advForcePlaceAtClickLocation)
				{
					if (this.PaintPrototype(prototype, vector, spawnOriginNormal, alpha, one, rotation, true, out gameObject))
					{
						num3 += 1L;
					}
					list.Add(vector);
					goto IL_85C;
				}
				if (this.m_spawnAlgorithm == Constants.LocationAlgorithm.Every)
				{
					if (vector5.x < vector4.x)
					{
						vector5.x += this.m_seedThrowRange;
					}
					else
					{
						vector5.x = vector3.x;
						vector5.z += this.m_seedThrowRange;
						if (vector5.z > vector4.z)
						{
							num2 = num5;
							goto IL_87A;
						}
					}
					b.x = vector5.x + this.m_seedThrowJitter * this.m_randomGenerator.Next(-this.m_seedThrowRange, this.m_seedThrowRange);
					b.z = vector5.z + this.m_seedThrowJitter * this.m_randomGenerator.Next(-this.m_seedThrowRange, this.m_seedThrowRange);
				}
				else
				{
					b = new Vector3(this.m_randomGenerator.Next(-this.m_seedThrowRange, this.m_seedThrowRange), 0f, this.m_randomGenerator.Next(-this.m_seedThrowRange, this.m_seedThrowRange));
				}
				if (this.m_spawnAlgorithm == Constants.LocationAlgorithm.LastSpawn)
				{
					if (list.Count > 0)
					{
						vector = list[list.Count - 1] + b;
					}
					else
					{
						vector = location + b;
					}
				}
				else if (this.m_spawnAlgorithm == Constants.LocationAlgorithm.Organic)
				{
					if (list.Count > 0)
					{
						vector = list[num6++] + b;
						if (num6 >= list.Count)
						{
							num6 = 0;
						}
					}
					else
					{
						vector = location + b;
					}
				}
				else
				{
					vector = location + b;
				}
				if (this.m_lastSpawnedObject != null && this.m_rotationAlgorithm != Constants.RotationAlgorithm.Ranged)
				{
					if (this.m_rotationAlgorithm == Constants.RotationAlgorithm.LastSpawnClosest)
					{
						Collider[] componentsInChildren2 = this.m_lastSpawnedObject.GetComponentsInChildren<Collider>();
						if (componentsInChildren2.Length > 0)
						{
							Vector3 a2 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
							Vector3 vector8 = Vector3.zero;
							for (int j = 0; j < componentsInChildren2.Length; j++)
							{
								vector8 = componentsInChildren2[j].ClosestPointOnBounds(vector);
								if (Vector3.Distance(vector8, vector) < Vector3.Distance(a2, vector))
								{
									a2 = vector8;
								}
							}
							rotation = Quaternion.LookRotation(a2 - vector).eulerAngles.y + this.m_randomGenerator.Next(this.m_minRotationY, this.m_maxRotationY);
						}
						else
						{
							rotation = Quaternion.LookRotation(this.m_lastSpawnedObject.transform.position - vector).eulerAngles.y + this.m_randomGenerator.Next(this.m_minRotationY, this.m_maxRotationY);
						}
					}
					else
					{
						rotation = Quaternion.LookRotation(this.m_lastSpawnedObject.transform.position - vector).eulerAngles.y + this.m_randomGenerator.Next(this.m_minRotationY, this.m_maxRotationY);
					}
				}
				if (!this.CheckLocationForSpawn(vector, rotation, list2, out prototype, out vector2, out spawnOriginNormal, out alpha))
				{
					goto IL_85C;
				}
				if (this.m_critVirginCheckType != Constants.VirginCheckType.Bounds)
				{
					if (this.PaintPrototype(prototype, vector2, spawnOriginNormal, alpha, one, rotation, false, out gameObject))
					{
						num3 += 1L;
						list.Add(vector2);
					}
					goto IL_85C;
				}
				vector6 = Vector3.Scale(prototype.m_extents, one);
				vector6 += new Vector3((this.m_critBoundsBorder + prototype.m_boundsBorder) * prototype.m_extents.x, (this.m_critBoundsBorder + prototype.m_boundsBorder) * prototype.m_extents.y, (this.m_critBoundsBorder + prototype.m_boundsBorder) * prototype.m_extents.z);
				vector = vector2;
				if (this.CheckBoundedLocationForSpawn(vector, rotation, prototype, vector6, false) && this.PaintPrototype(prototype, vector2, spawnOriginNormal, alpha, one, rotation, false, out gameObject))
				{
					num3 += 1L;
					list.Add(vector2);
					goto IL_85C;
				}
				goto IL_85C;
				IL_87A:
				num2 += 1L;
				continue;
				IL_85C:
				if (gameObject != null)
				{
					list3.Add(gameObject);
					this.m_lastSpawnedObject = gameObject;
					goto IL_87A;
				}
				goto IL_87A;
			}
			if (list3.Count > 0)
			{
				GameObject gameObject2 = null;
				if (this.m_mergeSpawns)
				{
					gameObject2 = GameObject.Find(this.m_parentName);
				}
				if (gameObject2 == null)
				{
					gameObject2 = new GameObject(this.m_parentName);
					gameObject2.transform.position = location;
				}
				for (int k = 0; k < list3.Count; k++)
				{
					list3[k].transform.parent = gameObject2.transform;
				}
			}
			if (!Application.isPlaying)
			{
				Physics.defaultContactOffset = defaultContactOffset;
				Physics.defaultSolverIterations = defaultSolverIterations;
			}
			foreach (Spawner spawner in this.m_childSpawners)
			{
				if (spawner != null && spawner.gameObject.activeInHierarchy)
				{
					spawner.Spawn(location, rotation, true);
				}
			}
		}

		public void LoadLightProbes()
		{
		}

		private bool PaintPrototype(Prototype prototype, Vector3 location, Vector3 normal, float alpha, Vector3 scaleFactor, float rotation, bool spawnAtLeastOneResource, out GameObject spawnedInstance)
		{
			spawnedInstance = null;
			if (prototype == null)
			{
				UnityEngine.Debug.Log("Missing prototype - aborting paint");
				return false;
			}
			List<GameObject> list = new List<GameObject>();
			List<Gravity.GravityInstance> list2 = new List<Gravity.GravityInstance>();
			Terrain terrain = null;
			Vector3 vector = location;
			Vector3 toDirection = normal;
			Vector3 vector2 = Vector3.zero;
			Vector3 zero = Vector3.zero;
			bool flag = false;
			rotation += prototype.m_forwardRotation;
			if (!spawnAtLeastOneResource && this.m_critMaskType == Constants.MaskType.Image)
			{
				if (prototype.m_invertMaskedAlpha)
				{
					alpha = 1f - alpha;
				}
				if (prototype.m_successOnMaskedAlpha && this.m_randomGenerator.Next() > alpha)
				{
					return false;
				}
				if (prototype.m_scaleOnMaskedAlpha)
				{
					float num = prototype.m_scaleOnMaskedAlphaMin + (prototype.m_scaleOnMaskedAlphaMax - prototype.m_scaleOnMaskedAlphaMin) * alpha;
					if (Spawner.ApproximatelyEqual(num, 0f, 1.401298E-45f))
					{
						return false;
					}
					scaleFactor *= num;
				}
			}
			foreach (Resource resource in prototype.m_resources)
			{
				if (spawnAtLeastOneResource)
				{
					if (list.Count > 0 && this.m_randomGenerator.Next() > resource.m_successRate)
					{
						continue;
					}
				}
				else if (this.m_randomGenerator.Next() > resource.m_successRate)
				{
					continue;
				}
				vector = location + new Vector3(this.m_randomGenerator.Next(resource.m_minOffset.x, resource.m_maxOffset.x), this.m_randomGenerator.Next(resource.m_minOffset.y, resource.m_maxOffset.y), this.m_randomGenerator.Next(resource.m_minOffset.z, resource.m_maxOffset.z));
				vector = Spawner.RotatePointAroundPivot(vector, location, new Vector3(0f, rotation, 0f));
				if (this.m_spawnOriginIsTerrain)
				{
					terrain = this.GetTerrain(vector);
					if (terrain != null)
					{
						vector.y = terrain.SampleHeight(vector) + terrain.transform.position.y + this.m_randomGenerator.Next(resource.m_minOffset.y, resource.m_maxOffset.y);
						vector2 = terrain.transform.InverseTransformPoint(vector);
						zero = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector2.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector2.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector2.z));
						toDirection = terrain.terrainData.GetInterpolatedNormal(zero.x, zero.z);
					}
				}
				else
				{
					vector.y += this.m_randomGenerator.Next(resource.m_minOffset.y, resource.m_maxOffset.y);
					toDirection = normal;
				}
				if (resource.m_resourceType == Constants.ResourceType.Prefab)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(resource.m_prefab);
					gameObject.name = "_Sp_" + resource.m_name;
					if (resource.m_conformToSlope)
					{
						gameObject.name = "_Sp_" + resource.m_name + " C";
					}
					gameObject.transform.position = vector;
					if (this.m_scaleToNearestInt)
					{
						gameObject.transform.localScale = this.ScaleToNearestInt(Vector3.Scale(resource.m_baseScale, scaleFactor));
					}
					else
					{
						gameObject.transform.localScale = Vector3.Scale(resource.m_baseScale, scaleFactor);
					}
					gameObject.transform.rotation = Quaternion.Euler(this.m_randomGenerator.Next(resource.m_minRotation.x, resource.m_maxRotation.x), this.m_randomGenerator.Next(resource.m_minRotation.y + rotation, resource.m_maxRotation.y + rotation), this.m_randomGenerator.Next(resource.m_minRotation.z, resource.m_maxRotation.z));
					if (resource.m_conformToSlope)
					{
						gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection) * gameObject.transform.rotation;
					}
					if (this.m_gravity != null)
					{
						if (!resource.m_hasRootCollider)
						{
							BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
							boxCollider.center = resource.m_baseColliderCenter;
							if (resource.m_baseColliderUseConstScale)
							{
								boxCollider.size = resource.m_baseColliderScale * resource.m_baseColliderConstScaleAmount;
							}
							else
							{
								boxCollider.size = resource.m_baseColliderScale;
							}
						}
						if (!resource.m_hasRigidBody)
						{
							gameObject.AddComponent<Rigidbody>();
						}
						list2.Add(new Gravity.GravityInstance
						{
							m_resource = resource,
							m_instance = gameObject,
							m_startPosition = gameObject.transform.position,
							m_startRotation = gameObject.transform.rotation.eulerAngles
						});
					}
					else
					{
						this.AutoOptimiseGameObject(resource, gameObject);
						this.AutoProbeGameObject(resource, gameObject);
					}
					list.Add(gameObject);
					resource.m_instancesSpawned += 1L;
					flag = true;
				}
				else if (resource.m_resourceType == Constants.ResourceType.TerrainTree)
				{
					if (terrain != null && resource.m_terrainProtoIdx < terrain.terrainData.treePrototypes.Length)
					{
						TreeInstance instance = default(TreeInstance);
						instance.prototypeIndex = resource.m_terrainProtoIdx;
						instance.position = zero;
						if (this.m_scaleToNearestInt)
						{
							instance.widthScale = Mathf.Ceil(scaleFactor.x);
							instance.heightScale = Mathf.Ceil(scaleFactor.y);
						}
						else
						{
							instance.widthScale = scaleFactor.x;
							instance.heightScale = scaleFactor.y;
						}
						rotation += this.m_randomGenerator.Next(resource.m_minRotation.y + rotation, resource.m_maxRotation.y + rotation);
						instance.rotation = rotation * 0.0174532924f;
						instance.color = Color.white;
						instance.lightmapColor = Color.white;
						terrain.AddTreeInstance(instance);
						this.m_treeManager.AddTree(vector, instance.prototypeIndex);
						resource.m_instancesSpawned += 1L;
						flag = true;
					}
				}
				else if (terrain != null && resource.m_terrainProtoIdx < terrain.terrainData.detailPrototypes.Length)
				{
					int num2 = (int)(zero.x * (float)(terrain.terrainData.detailWidth - 1));
					int num3 = (int)(zero.z * (float)(terrain.terrainData.detailHeight - 1));
					TerrainData terrainData = terrain.terrainData;
					int xBase = num2;
					int yBase = num3;
					int terrainProtoIdx = resource.m_terrainProtoIdx;
					int[,] array = new int[1, 1];
					array[0, 0] = (int)this.m_randomGenerator.Next(resource.m_minScale.x * 16f, resource.m_maxScale.x * 16f);
					terrainData.SetDetailLayer(xBase, yBase, terrainProtoIdx, array);
					resource.m_instancesSpawned += 1L;
					flag = true;
				}
			}
			if (this.m_gravity != null)
			{
				this.m_gravity.AddInstances(list2);
			}
			if (list.Count == 1)
			{
				spawnedInstance = list[0];
				this.m_prefabUndoList.Add(spawnedInstance);
			}
			else if (list.Count > 1)
			{
				GameObject gameObject = new GameObject(prototype.m_name);
				gameObject.transform.position = location;
				foreach (GameObject gameObject2 in list)
				{
					gameObject2.transform.parent = gameObject.transform;
				}
				if (this.m_advAddColliderToSpawnedPrefabs)
				{
					SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
					Vector3 vector3 = Vector3.Scale(prototype.m_extents, scaleFactor);
					sphereCollider.radius = Mathf.Max(vector3.x, vector3.z);
					gameObject.AddComponent<DisableColliderOnAwake>();
				}
				spawnedInstance = gameObject;
				this.m_prefabUndoList.Add(spawnedInstance);
			}
			if (flag)
			{
				prototype.m_instancesSpawned += 1L;
				this.m_instancesSpawned += 1L;
			}
			return flag;
		}

		private bool CanOptimiseGameObject(Resource resource, GameObject go)
		{
			if (!this.m_autoOptimise)
			{
				return false;
			}
			if (resource.m_flagForceOptimise)
			{
				return true;
			}
			if (!resource.m_flagCanBeOptimised)
			{
				return false;
			}
			Vector3 vector = Vector3.Scale(resource.m_baseSize, go.transform.localScale);
			return vector.x < this.m_maxSizeToOptimize && vector.y < this.m_maxSizeToOptimize && vector.z < this.m_maxSizeToOptimize;
		}

		private void OptimiseGameObject(Resource resource, GameObject go)
		{
		}

		public void AutoOptimiseGameObject(Resource resource, GameObject go)
		{
		}

		private bool CanProbeGameObject(Resource resource, GameObject go)
		{
			return this.m_autoProbe && !Application.isPlaying;
		}

		private void ProbeGameObject(Resource resource, GameObject go)
		{
		}

		private LightProbeGroup GetOrCreateNearestProbeGroup(Vector3 position, out bool canAddNewProbes)
		{
			List<LightProbeGroup> probeGroups = this.m_probeManager.GetProbeGroups(position, this.m_minProbeDistance);
			if (probeGroups.Count != 0)
			{
				canAddNewProbes = false;
				return probeGroups[0];
			}
			canAddNewProbes = true;
			probeGroups = this.m_probeManager.GetProbeGroups(position, this.m_minProbeGroupDistance);
			if (probeGroups.Count != 0)
			{
				return probeGroups[0];
			}
			GameObject gameObject = new GameObject(string.Format("Light Probe Group {0:0}x {1:0}z", position.x, position.z));
			gameObject.transform.position = position;
			if (this.m_probeParent == null)
			{
				this.m_probeParent = GameObject.Find("GeNa Light Probes");
				if (this.m_probeParent == null)
				{
					this.m_probeParent = new GameObject("GeNa Light Probes");
				}
			}
			gameObject.transform.parent = this.m_probeParent.transform;
			LightProbeGroup lightProbeGroup = gameObject.AddComponent<LightProbeGroup>();
			//lightProbeGroup.probePositions = new Vector3[0];
			return lightProbeGroup;
		}

		public void AutoProbeGameObject(Resource resource, GameObject go)
		{
		}

		private bool CheckLocationForSpawn(Vector3 location, float rotation, List<Prototype> prototypes, out Prototype selectedPrototype, out Vector3 hitLocation, out Vector3 hitNormal, out float hitAlpha)
		{
			selectedPrototype = null;
			hitLocation = location;
			hitNormal = Vector3.up;
			hitAlpha = 0f;
			if (prototypes.Count <= 0)
			{
				return false;
			}
			selectedPrototype = prototypes[this.m_randomGenerator.Next(0, prototypes.Count - 1)];
			Ray ray = new Ray(new Vector3(location.x, location.y + 10000f, location.z), Vector3.down);
			RaycastHit raycastHit;
			if (!Physics.Raycast(ray, out raycastHit, 20000f))
			{
				return false;
			}
			hitLocation = raycastHit.point;
			hitNormal = raycastHit.normal;
			if (this.m_spawnRangeShape == Constants.SpawnRangeShape.Circle)
			{
				if (Vector3.Distance(this.m_spawnOriginLocation, hitLocation) > this.m_maxSpawnRange / 2f)
				{
					return false;
				}
			}
			else if (!this.m_spawnOriginBounds.Contains(hitLocation))
			{
				return false;
			}
			if (this.m_critCheckHeight && (hitLocation.y < this.m_critMinHeight || hitLocation.y > this.m_critMaxHeight))
			{
				return false;
			}
			if (this.m_critCheckSlope)
			{
				float num = Vector3.Angle(Vector3.up, hitNormal);
				if (num < this.m_critMinSlope || num > this.m_critMaxSlope)
				{
					return false;
				}
			}
			if (this.m_critCheckMask)
			{
				if (this.m_critMaskType != Constants.MaskType.Image)
				{
					float normalisedValue = this.m_critMaskFractal.GetNormalisedValue(100000f + hitLocation.x, 100000f + hitLocation.z);
					if (this.m_critMaskInvert)
					{
						if (normalisedValue >= this.m_critMaskFractalMin && normalisedValue <= this.m_critMaskFractalMax)
						{
							return false;
						}
					}
					else if (normalisedValue < this.m_critMaskFractalMin || normalisedValue > this.m_critMaskFractalMax)
					{
						return false;
					}
				}
				else if (this.m_critMaskImageData != null && this.m_critMaskImageData.HasData())
				{
					Vector3 vector = Spawner.RotatePointAroundPivot(hitLocation, this.m_spawnOriginLocation, new Vector3(0f, 180f - rotation, 0f));
					float num2 = (this.m_spawnOriginLocation.x - vector.x) / this.m_maxSpawnRange + 0.5f;
					float num3 = (this.m_spawnOriginLocation.z - vector.z) / this.m_maxSpawnRange + 0.5f;
					if (num2 < 0f || num2 >= 1f || num3 < 0f || num3 > 1f)
					{
						return false;
					}
					hitAlpha = this.m_critMaskAlphaData[num2, num3];
					float num4 = this.m_critMaskImageData[num2, num3];
					Color c = default(Color);
					c.b = num4 % 1000f;
					num4 -= c.b;
					num4 /= 1000f;
					c.b /= 255f;
					c.g = num4 % 1000f;
					num4 -= c.g;
					num4 /= 1000f;
					c.g /= 255f;
					c.r = num4;
					c.r /= 255f;
					List<Prototype> list = new List<Prototype>();
					for (int i = 0; i < prototypes.Count; i++)
					{
						Prototype prototype = prototypes[i];
						if (this.RGBDifference(c, prototype.m_imageFilterColour) < (1f - prototype.m_imageFilterFuzzyMatch) * 100f)
						{
							list.Add(prototype);
						}
					}
					if (list.Count == 0)
					{
						selectedPrototype = null;
						return false;
					}
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j].m_successOnMaskedAlpha)
						{
							if (!list[j].m_invertMaskedAlpha)
							{
								if (Spawner.ApproximatelyEqual(hitAlpha, 0f, 1.401298E-45f))
								{
									list.RemoveAt(j);
									continue;
								}
							}
							else if (Spawner.ApproximatelyEqual(1f - hitAlpha, 0f, 1.401298E-45f))
							{
								list.RemoveAt(j);
								continue;
							}
						}
					}
					if (list.Count == 0)
					{
						selectedPrototype = null;
						return false;
					}
					selectedPrototype = list[this.m_randomGenerator.Next(0, list.Count - 1)];
				}
			}
			Terrain terrain = null;
			if (raycastHit.collider is TerrainCollider)
			{
				terrain = raycastHit.transform.GetComponent<Terrain>();
			}
			if (this.m_critVirginCheckType != Constants.VirginCheckType.None)
			{
				if (this.m_spawnOriginIsTerrain)
				{
					if (terrain == null)
					{
						return false;
					}
					if (this.m_treeManager.Count(hitLocation, 0.5f) > 0)
					{
						return false;
					}
				}
				else if (raycastHit.transform.GetInstanceID() != this.m_spawnOriginObjectID)
				{
					return false;
				}
			}
			if (this.m_critCheckTextures && terrain != null)
			{
				Vector3 vector2 = terrain.transform.InverseTransformPoint(hitLocation);
				Vector3 vector3 = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector2.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector2.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector2.z));
				float[,,] alphamaps = terrain.terrainData.GetAlphamaps((int)(vector3.x * (float)(terrain.terrainData.alphamapWidth - 1)), (int)(vector3.z * (float)(terrain.terrainData.alphamapHeight - 1)), 1, 1);
				if (alphamaps.GetLength(2) - 1 < this.m_critSelectedTextureIdx)
				{
					return false;
				}
				if (alphamaps[0, 0, this.m_critSelectedTextureIdx] < this.m_critMinTextureStrength || alphamaps[0, 0, this.m_critSelectedTextureIdx] > this.m_critMaxTextureStrength)
				{
					return false;
				}
			}
			return true;
		}

		private bool CheckBoundedLocationForSpawn(Vector3 location, float rotation, Prototype prototype, Vector3 extents, bool visualising)
		{
			if (this.m_spawnOriginIsTerrain && this.m_treeManager.Count(location, Mathf.Max(extents.x, extents.z)) > 0)
			{
				return false;
			}
			float num = this.m_metersPerScan;
			float num2 = this.m_metersPerScan;
			if (visualising)
			{
				num = this.m_metersPerScanVisualisation;
				num2 = this.m_metersPerScanVisualisation;
			}
			Vector3 origin = new Vector3(location.x - extents.x, location.y + 10000f, location.z - extents.z);
			Vector3 vector = new Vector3(location.x + extents.x, location.y + 10000f, location.z + extents.z);
			origin.x = location.x - extents.x;
			while (origin.x < vector.x)
			{
				origin.z = location.z - extents.z;
				while (origin.z < vector.z)
				{
					Ray ray = new Ray(origin, Vector3.down);
					RaycastHit raycastHit;
					if (!Physics.Raycast(ray, out raycastHit, 20000f))
					{
						return false;
					}
					if (this.m_critCheckHeight && (raycastHit.point.y < this.m_critMinHeight || raycastHit.point.y > this.m_critMaxHeight))
					{
						return false;
					}
					if (this.m_critCheckSlope)
					{
						float num3 = Vector3.Angle(Vector3.up, raycastHit.normal);
						if (num3 < this.m_critMinSlope || num3 > this.m_critMaxSlope)
						{
							return false;
						}
					}
					Terrain terrain = null;
					if (raycastHit.collider is TerrainCollider)
					{
						terrain = raycastHit.transform.GetComponent<Terrain>();
					}
					if (this.m_spawnOriginIsTerrain)
					{
						if (terrain == null)
						{
							return false;
						}
					}
					else if (raycastHit.transform.GetInstanceID() != this.m_spawnOriginObjectID)
					{
						return false;
					}
					if (this.m_critCheckMask && this.m_critMaskType == Constants.MaskType.Image && prototype != null && prototype.m_constrainWithinMaskedBounds && this.m_critMaskImageData != null && this.m_critMaskImageData.HasData())
					{
						Vector3 vector2 = Spawner.RotatePointAroundPivot(raycastHit.point, this.m_spawnOriginLocation, new Vector3(0f, 180f - rotation, 0f));
						float num4 = (this.m_spawnOriginLocation.x - vector2.x) / this.m_maxSpawnRange + 0.5f;
						float num5 = (this.m_spawnOriginLocation.z - vector2.z) / this.m_maxSpawnRange + 0.5f;
						if (num4 < 0f || num4 >= 1f || num5 < 0f || num5 > 1f)
						{
							return false;
						}
						float num6 = this.m_critMaskImageData[num4, num5];
						Color c = default(Color);
						c.b = num6 % 1000f;
						num6 -= c.b;
						num6 /= 1000f;
						c.b /= 255f;
						c.g = num6 % 1000f;
						num6 -= c.g;
						num6 /= 1000f;
						c.g /= 255f;
						c.r = num6;
						c.r /= 255f;
						if (this.RGBDifference(c, prototype.m_imageFilterColour) > (1f - prototype.m_imageFilterFuzzyMatch) * 100f)
						{
							return false;
						}
						if (prototype.m_successOnMaskedAlpha)
						{
							float num7 = this.m_critMaskAlphaData[num4, num5];
							if (!prototype.m_invertMaskedAlpha)
							{
								if (Spawner.ApproximatelyEqual(num7, 0f, 1.401298E-45f))
								{
									return false;
								}
							}
							else if (Spawner.ApproximatelyEqual(1f - num7, 0f, 1.401298E-45f))
							{
								return false;
							}
						}
					}
					if (this.m_critCheckTextures && terrain != null && UnityEngine.Random.Range(0, 5) == 1)
					{
						Vector3 vector3 = terrain.transform.InverseTransformPoint(raycastHit.point);
						Vector3 vector4 = new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector3.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector3.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector3.z));
						float[,,] alphamaps = terrain.terrainData.GetAlphamaps((int)(vector4.x * (float)(terrain.terrainData.alphamapWidth - 1)), (int)(vector4.z * (float)(terrain.terrainData.alphamapHeight - 1)), 1, 1);
						if (alphamaps.GetLength(2) - 1 < this.m_critSelectedTextureIdx)
						{
							return false;
						}
						if (alphamaps[0, 0, this.m_critSelectedTextureIdx] < this.m_critMinTextureStrength || alphamaps[0, 0, this.m_critSelectedTextureIdx] > this.m_critMaxTextureStrength)
						{
							return false;
						}
					}
					origin.z += num2;
				}
				origin.x += num;
			}
			return true;
		}

		private void LateUpdate()
		{
			if (this.m_gravity != null && (DateTime.Now - this.m_lastUpdated).TotalSeconds > 2.0)
			{
				this.m_lastUpdated = DateTime.Now;
				this.m_gravity.UpdateInstances();
			}
		}

		public void UnspawnAll()
		{
			this.m_randomGenerator.Reset();
			foreach (GameObject obj in this.m_prefabUndoList)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			this.m_prefabUndoList.Clear();
			foreach (Prototype prototype in this.m_spawnPrototypes)
			{
				Constants.ResourceType resourceType = prototype.m_resourceType;
				if (resourceType != Constants.ResourceType.TerrainTree)
				{
					if (resourceType == Constants.ResourceType.TerrainGrass)
					{
						this.UnspawnGrass(prototype);
					}
				}
				else
				{
					this.UnspawnTree(prototype);
				}
				prototype.m_instancesSpawned = 0L;
				foreach (Resource resource in prototype.m_resources)
				{
					resource.m_instancesSpawned = 0L;
				}
			}
			this.m_instancesSpawned = 0L;
			foreach (Spawner spawner in this.m_childSpawners)
			{
				if (spawner != null && spawner.gameObject.activeInHierarchy)
				{
					spawner.UnspawnAll();
				}
			}
		}

		public void UnspawnGameObject(Prototype proto)
		{
			if (proto.m_resourceType != Constants.ResourceType.Prefab)
			{
				return;
			}
		}

		public void UnspawnGrass(Prototype proto)
		{
			if (proto.m_resourceType != Constants.ResourceType.TerrainGrass)
			{
				return;
			}
			Resource resource = proto.m_resources[0];
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				terrain.terrainData.SetDetailLayer(0, 0, resource.m_terrainProtoIdx, new int[terrain.terrainData.detailWidth, terrain.terrainData.detailWidth]);
			}
			proto.m_instancesSpawned -= resource.m_instancesSpawned;
			this.m_instancesSpawned -= resource.m_instancesSpawned;
			resource.m_instancesSpawned = 0L;
		}

		public void UnspawnTree(Prototype proto)
		{
			if (proto.m_resourceType != Constants.ResourceType.TerrainTree)
			{
				return;
			}
			List<TreeInstance> list = new List<TreeInstance>();
			Resource resource = proto.m_resources[0];
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				for (int j = 0; j < terrain.terrainData.treeInstances.Length; j++)
				{
					TreeInstance item = terrain.terrainData.treeInstances[j];
					if (item.prototypeIndex != resource.m_terrainProtoIdx)
					{
						list.Add(item);
					}
					else
					{
						resource.m_instancesSpawned -= 1L;
						proto.m_instancesSpawned -= 1L;
						this.m_instancesSpawned -= 1L;
					}
				}
				terrain.terrainData.treeInstances = list.ToArray();
				list.Clear();
			}
		}

		private float JitterAsPct(float value, float percent)
		{
			return this.m_randomGenerator.Next(Mathf.Clamp01(percent) * value, value);
		}

		private float JitterAround(float value, float delta)
		{
			return this.m_randomGenerator.Next(value - delta, value + delta);
		}

		private Vector3 ScaleToNearestInt(Vector3 sourceScale)
		{
			float num = sourceScale.x;
			float num2 = sourceScale.y;
			float num3 = sourceScale.z;
			if (num - Mathf.Floor(num) < Mathf.Ceil(num) - num)
			{
				num = Mathf.Floor(num);
			}
			else
			{
				num = Mathf.Ceil(num);
			}
			if (num < 1f)
			{
				num = 1f;
			}
			if (num2 - Mathf.Floor(num2) < Mathf.Ceil(num2) - num2)
			{
				num2 = Mathf.Floor(num2);
			}
			else
			{
				num2 = Mathf.Ceil(num2);
			}
			if (num2 < 1f)
			{
				num2 = 1f;
			}
			if (num3 - Mathf.Floor(num3) < Mathf.Ceil(num3) - num3)
			{
				num3 = Mathf.Floor(num3);
			}
			else
			{
				num3 = Mathf.Ceil(num3);
			}
			if (num3 < 1f)
			{
				num3 = 1f;
			}
			return new Vector3(num, num2, num3);
		}

		private void CombineMeshes(GameObject go)
		{
			Vector3 position = go.transform.position;
			go.transform.position = Vector3.zero;
			MeshFilter[] componentsInChildren = base.GetComponentsInChildren<MeshFilter>();
			CombineInstance[] array = new CombineInstance[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				array[i].mesh = componentsInChildren[i].sharedMesh;
				array[i].transform = componentsInChildren[i].transform.localToWorldMatrix;
				componentsInChildren[i].gameObject.SetActive(false);
			}
			if (go.transform.GetComponent<MeshFilter>() == null)
			{
				go.AddComponent<MeshFilter>();
			}
			go.transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
			go.transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(array, true, true);
			go.transform.gameObject.SetActive(true);
			go.transform.position = position;
			go.AddComponent<MeshCollider>();
		}

		private Terrain GetTerrain(Vector3 location)
		{
			Vector3 a = default(Vector3);
			Vector3 vector = default(Vector3);
			Terrain terrain = Terrain.activeTerrain;
			if (terrain != null)
			{
				a = terrain.GetPosition();
				vector = a + terrain.terrainData.size;
				if (location.x >= a.x && location.x <= vector.x && location.z >= a.z && location.z <= vector.z)
				{
					return terrain;
				}
			}
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				terrain = Terrain.activeTerrains[i];
				a = terrain.GetPosition();
				vector = a + terrain.terrainData.size;
				if (location.x >= a.x && location.x <= vector.x && location.z >= a.z && location.z <= vector.z)
				{
					return terrain;
				}
			}
			return null;
		}

		private bool GetTerrainBounds(Vector3 location, ref Bounds bounds)
		{
			Terrain terrain = this.GetTerrain(location);
			if (terrain == null)
			{
				return false;
			}
			bounds.center = terrain.transform.position;
			bounds.size = terrain.terrainData.size;
			bounds.center += bounds.extents;
			return true;
		}

		private bool GetObjectBounds(GameObject go, ref Bounds bounds)
		{
			if (go == null)
			{
				return false;
			}
			bounds.center = go.transform.position;
			bounds.size = Vector3.zero;
			foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
			{
				bounds.Encapsulate(renderer.bounds);
			}
			foreach (Collider collider in go.GetComponentsInChildren<Collider>())
			{
				bounds.Encapsulate(collider.bounds);
			}
			return true;
		}

		public static bool ApproximatelyEqual(float a, float b, float delta = 1.401298E-45f)
		{
			return a == b || Mathf.Abs(a - b) < delta;
		}

		public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angle)
		{
			Vector3 vector = point - pivot;
			vector = Quaternion.Euler(angle) * vector;
			point = vector + pivot;
			return point;
		}

		private float RGBDifference(Color c1, Color c2)
		{
			if (Spawner.ApproximatelyEqual(c1.r, c2.r, 1.401298E-45f) && Spawner.ApproximatelyEqual(c1.g, c2.g, 1.401298E-45f) && Spawner.ApproximatelyEqual(c1.b, c2.b, 1.401298E-45f))
			{
				return 0f;
			}
			Vector3 vector = this.RGBtoLAB(c1);
			Vector3 vector2 = this.RGBtoLAB(c2);
			float num = 0f;
			num += Mathf.Pow(vector.x - vector2.x, 2f);
			num += Mathf.Pow(vector.y - vector2.y, 2f);
			num += Mathf.Pow(vector.z - vector2.z, 2f);
			return Mathf.Max(Mathf.Min(Mathf.Sqrt(num), 100f), 0f);
		}

		private Vector3 RGBtoLAB(Color c)
		{
			return this.XYZtoLAB(this.RGBtoXYZ(c));
		}

		private Vector3 RGBtoXYZ(Color c)
		{
			float num = c.r;
			float num2 = c.g;
			float num3 = c.b;
			if (num > 0.04045f)
			{
				num = Mathf.Pow((num + 0.055f) / 1.055f, 2.4f);
			}
			else
			{
				num /= 12.92f;
			}
			if (num2 > 0.04045f)
			{
				num2 = Mathf.Pow((num2 + 0.055f) / 1.055f, 2.4f);
			}
			else
			{
				num2 /= 12.92f;
			}
			if (num3 > 0.04045f)
			{
				num3 = Mathf.Pow((num3 + 0.055f) / 1.055f, 2.4f);
			}
			else
			{
				num3 /= 12.92f;
			}
			num *= 100f;
			num2 *= 100f;
			num3 *= 100f;
			float x = num * 0.4124f + num2 * 0.3576f + num3 * 0.1805f;
			float y = num * 0.2126f + num2 * 0.7152f + num3 * 0.0722f;
			float z = num * 0.0193f + num2 * 0.1192f + num3 * 0.9505f;
			return new Vector3(x, y, z);
		}

		private Vector3 XYZtoLAB(Vector3 c)
		{
			float num = 100f;
			float num2 = 108.883f;
			float num3 = 95.047f;
			float num4 = c.y / num;
			float num5 = c.z / num2;
			float num6 = c.x / num3;
			if (num6 > 0.008856f)
			{
				num6 = Mathf.Pow(num6, 0.333333343f);
			}
			else
			{
				num6 = 7.787f * num6 + 0.137931034f;
			}
			if ((double)num4 > 0.008856)
			{
				num4 = Mathf.Pow(num4, 0.333333343f);
			}
			else
			{
				num4 = 7.787f * num4 + 0.137931034f;
			}
			if (num5 > 0.008856f)
			{
				num5 = Mathf.Pow(num5, 0.333333343f);
			}
			else
			{
				num5 = 7.787f * num5 + 0.137931034f;
			}
			float x = 116f * num4 - 16f;
			float y = 500f * (num6 - num4);
			float z = 200f * (num4 - num5);
			return new Vector3(x, y, z);
		}

		private void OnDrawGizmosSelected()
		{
			if (!this.m_showGizmos)
			{
				return;
			}
			if (this.m_spawnOriginLocation == Vector3.zero)
			{
				return;
			}
			float num = this.m_maxSpawnRange / 2f;
			int num2 = (int)this.m_maxSpawnRange + 1;
			if (num2 > this.m_maxVisualisationDimensions)
			{
				num2 = this.m_maxVisualisationDimensions + 1;
			}
			float num3 = this.m_maxSpawnRange / ((float)num2 - 1f);
			if (num2 != this.m_fitnessArray.GetLength(0))
			{
				this.m_needsVisualisationUpdate = true;
			}
			if (this.m_needsVisualisationUpdate)
			{
				this.UpdateSpawnerVisualisation();
			}
			Vector3 zero = Vector3.zero;
			Vector3 vector = this.m_spawnOriginLocation + Vector3.one * num;
			Gizmos.color = Color.green;
			int num4 = 0;
			zero.x = this.m_spawnOriginLocation.x - num;
			while (zero.x < vector.x)
			{
				int num5 = 0;
				zero.z = this.m_spawnOriginLocation.z - num;
				while (zero.z < vector.z)
				{
					zero.y = this.m_fitnessArray[num4, num5];
					if (zero.y > -3.40282347E+38f)
					{
						Gizmos.DrawSphere(zero, num3 / 4f);
					}
					num5++;
					zero.z += num3;
				}
				num4++;
				zero.x += num3;
			}
			if (this.m_critCheckHeight)
			{
				Bounds bounds = default(Bounds);
				if (this.GetTerrainBounds(this.m_critSpawnCentre, ref bounds))
				{
					bounds.center = new Vector3(bounds.center.x, this.m_critMinSpawnHeight, bounds.center.z);
					bounds.size = new Vector3(bounds.size.x, 0.05f, bounds.size.z);
					Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, Color.blue.a / 2f);
					Gizmos.DrawCube(bounds.center, bounds.size);
				}
			}
		}

		public string m_parentName = "GeNa Spawner";

		public bool m_mergeSpawns = true;

		public List<Prototype> m_spawnPrototypes = new List<Prototype>();

		public Vector3 m_spawnOriginLocation = Vector3.zero;

		public Vector3 m_spawnOriginNormal = Vector3.up;

		public int m_spawnOriginObjectID = int.MinValue;

		public bool m_spawnOriginIsTerrain;

		public Bounds m_spawnOriginBounds = default(Bounds);

		public Transform m_spawnOriginGroundTransform;

		public Constants.LocationAlgorithm m_spawnAlgorithm = Constants.LocationAlgorithm.Organic;

		public long m_minInstances = 1L;

		public long m_maxInstances = 1L;

		public long m_instancesSpawned;

		public float m_seedThrowRange = 5f;

		public float m_seedThrowJitter = 1f;

		public float m_maxSpawnRange = 50f;

		public Constants.SpawnRangeShape m_spawnRangeShape;

		public Constants.VirginCheckType m_critVirginCheckType = Constants.VirginCheckType.Point;

		public float m_critBoundsBorder;

		public bool m_critCheckHeight = true;

		public float m_critMinSpawnHeight = 50f;

		public float m_critHeightVariance = 30f;

		public bool m_critCheckSlope = true;

		public float m_critSlopeVariance = 30f;

		public bool m_critCheckTextures;

		public float m_critTextureStrength;

		public float m_critTextureVariance = 0.1f;

		public int m_critSelectedTextureIdx;

		public string m_critSelectedTextureName = string.Empty;

		public int m_critMaxSelectedTexture = 1;

		public bool m_critCheckMask;

		public Constants.MaskType m_critMaskType;

		public Fractal m_critMaskFractal = new Fractal();

		public float m_critMaskFractalMidpoint = 0.5f;

		public float m_critMaskFractalRange = 0.5f;

		public Texture2D m_critMaskImage;

		public Heightmap m_critMaskImageData;

		public Heightmap m_critMaskAlphaData;

		public bool m_critMaskInvert;

		private Vector3 m_critSpawnCentre = Vector3.zero;

		private float m_critMinHeight;

		private float m_critMaxHeight = 1000f;

		private float m_critMinSlope;

		private float m_critMaxSlope = 90f;

		private float m_critMinTextureStrength;

		private float m_critMaxTextureStrength = 1f;

		private float m_critMaskFractalMin;

		private float m_critMaskFractalMax = 1f;

		public Constants.RotationAlgorithm m_rotationAlgorithm;

		public float m_rotationYOffsetXX;

		public float m_minRotationY;

		public float m_maxRotationY = 360f;

		public bool m_sameScale = true;

		public bool m_scaleToNearestInt = true;

		public float m_minScaleX = 0.7f;

		public float m_maxScaleX = 1.3f;

		public float m_minScaleY = 1f;

		public float m_maxScaleY = 1f;

		public float m_minScaleZ = 1f;

		public float m_maxScaleZ = 1f;

		public bool m_useGravity;

		public bool m_enableRotationDragUpdate;

		public bool m_autoOptimise = true;

		public float m_maxSizeToOptimize = 10f;

		public float m_minProbeGroupDistance = 100f;

		public float m_minProbeDistance = 15f;

		public bool m_autoProbe = true;

		public Gravity m_gravity;

		public bool m_advUseLargeRanges;

		public bool m_advShowMouseOverHelp = true;

		public bool m_advShowDetailedHelp = true;

		public bool m_advForcePlaceAtClickLocation = true;

		public bool m_advAddColliderToSpawnedPrefabs;

		public bool m_showSpawnCriteria;

		public bool m_showPlacementCriteria;

		public bool m_showPrototypes;

		public bool m_showGizmos;

		public bool m_showAdvancedSettings;

		public bool m_needsVisualisationUpdate = true;

		public int m_maxVisualisationDimensions = 50;

		private float[,] m_fitnessArray = new float[1, 1];

		public float m_metersPerScan = 1f;

		public float m_metersPerScanVisualisation = 4f;

		private TreeManager m_treeManager = new TreeManager();

		private ProbeManager m_probeManager = new ProbeManager();

		private GameObject m_probeParent;

		public GameObject m_lastSpawnedObject;

		public List<GameObject> m_prefabUndoList = new List<GameObject>();

		private List<Spawner> m_childSpawners = new List<Spawner>();

		private DateTime m_lastUpdated = DateTime.MinValue;

		public GenaDefaults m_defaults;

		public int m_randomSeed = 1000;

		public XorshiftPlus m_randomGenerator = new XorshiftPlus(1000);
	}
}
