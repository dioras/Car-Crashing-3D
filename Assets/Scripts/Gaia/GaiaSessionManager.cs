using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Gaia.FullSerializer;
using UnityEngine;

namespace Gaia
{
	[ExecuteInEditMode]
	public class GaiaSessionManager : MonoBehaviour
	{
		public static GaiaSessionManager GetSessionManager(bool pickupExistingTerrain = false)
		{
			GameObject gameObject = GameObject.Find("Gaia");
			if (gameObject == null)
			{
				gameObject = new GameObject("Gaia");
			}
			GameObject gameObject2 = GameObject.Find("Session Manager");
			GaiaSessionManager gaiaSessionManager;
			if (gameObject2 == null)
			{
				gameObject2 = new GameObject("Session Manager");
				gaiaSessionManager = gameObject2.AddComponent<GaiaSessionManager>();
				gaiaSessionManager.CreateSession(pickupExistingTerrain);
				gameObject2.transform.parent = gameObject.transform;
				gameObject2.transform.position = TerrainHelper.GetActiveTerrainCenter(true);
			}
			else
			{
				gaiaSessionManager = gameObject2.GetComponent<GaiaSessionManager>();
			}
			return gaiaSessionManager;
		}

		public bool IsLocked()
		{
			if (this.m_session == null)
			{
				this.CreateSession(false);
			}
			return this.m_session.m_isLocked;
		}

		public bool LockSession()
		{
			if (this.m_session == null)
			{
				this.CreateSession(false);
			}
			bool isLocked = this.m_session.m_isLocked;
			this.m_session.m_isLocked = true;
			if (!isLocked)
			{
				this.SaveSession();
			}
			return isLocked;
		}

		public bool UnLockSession()
		{
			if (this.m_session == null)
			{
				this.CreateSession(false);
			}
			bool isLocked = this.m_session.m_isLocked;
			this.m_session.m_isLocked = false;
			if (isLocked)
			{
				this.SaveSession();
			}
			return isLocked;
		}

		public void AddOperation(GaiaOperation operation)
		{
			if (this.IsLocked())
			{
				UnityEngine.Debug.Log("Cant add operation on locked session");
				return;
			}
			this.m_session.m_operations.Add(operation);
			this.SaveSession();
		}

		public GaiaOperation GetOperation(int operationIdx)
		{
			if (this.m_session == null)
			{
				this.CreateSession(false);
			}
			if (operationIdx < 0 || operationIdx >= this.m_session.m_operations.Count)
			{
				return null;
			}
			return this.m_session.m_operations[operationIdx];
		}

		public void RemoveOperation(int operationIdx)
		{
			if (this.IsLocked())
			{
				UnityEngine.Debug.Log("Cant remove operation on locked session");
				return;
			}
			if (operationIdx < 0 || operationIdx >= this.m_session.m_operations.Count)
			{
				return;
			}
			this.m_session.m_operations.RemoveAt(operationIdx);
			this.SaveSession();
		}

		public void AddResource(GaiaResource resource)
		{
			if (this.IsLocked())
			{
				UnityEngine.Debug.Log("Cant add resource on locked session");
				return;
			}
			if (!(resource != null) || !this.m_session.m_resources.ContainsKey(resource.m_resourcesID + resource.name))
			{
			}
		}

		public void AddDefaults(GaiaDefaults defaults)
		{
			if (this.IsLocked())
			{
				UnityEngine.Debug.Log("Cant add defaults on locked session");
				return;
			}
			if (defaults != null)
			{
			}
		}

		public void AddPreviewImage(Texture2D image)
		{
			if (this.IsLocked())
			{
				UnityEngine.Debug.Log("Cant add preview on locked session");
				return;
			}
			this.m_session.m_previewImageWidth = image.width;
			this.m_session.m_previewImageHeight = image.height;
			this.m_session.m_previewImageBytes = image.GetRawTextureData();
			this.SaveSession();
		}

		public bool HasPreviewImage()
		{
			return this.m_session.m_previewImageWidth > 0 && this.m_session.m_previewImageHeight > 0 && this.m_session.m_previewImageBytes.GetLength(0) > 0;
		}

		public void RemovePreviewImage()
		{
			if (this.IsLocked())
			{
				UnityEngine.Debug.Log("Cant remove preview on locked session");
				return;
			}
			this.m_session.m_previewImageWidth = 0;
			this.m_session.m_previewImageHeight = 0;
			this.m_session.m_previewImageBytes = new byte[0];
			this.SaveSession();
		}

		public Texture2D GetPreviewImage()
		{
			if (this.m_session.m_previewImageBytes.GetLength(0) == 0)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(this.m_session.m_previewImageWidth, this.m_session.m_previewImageHeight, TextureFormat.ARGB32, false);
			texture2D.LoadRawTextureData(this.m_session.m_previewImageBytes);
			texture2D.Apply();
			texture2D.name = this.m_session.m_name;
			return texture2D;
		}

		public void SaveSession()
		{
		}

		public void StartEditorUpdates()
		{
		}

		public void StopEditorUpdates()
		{
			this.m_currentSpawner = null;
			this.m_currentStamper = null;
			this.m_updateOperationCoroutine = null;
			this.m_updateSessionCoroutine = null;
		}

		private void EditorUpdate()
		{
			if (this.m_cancelPlayback)
			{
				if (this.m_currentSpawner != null)
				{
					this.m_currentSpawner.CancelSpawn();
				}
				if (this.m_currentStamper != null)
				{
					this.m_currentStamper.CancelStamp();
				}
				this.StopEditorUpdates();
			}
			else if (this.m_updateSessionCoroutine == null && this.m_updateOperationCoroutine == null)
			{
				this.StopEditorUpdates();
			}
			else if (this.m_updateOperationCoroutine != null)
			{
				this.m_updateOperationCoroutine.MoveNext();
			}
			else
			{
				this.m_updateSessionCoroutine.MoveNext();
			}
		}

		public GaiaSession CreateSession(bool pickupExistingTerrain = false)
		{
			this.m_session = ScriptableObject.CreateInstance<GaiaSession>();
			this.m_session.m_description = "Rocking out at Creativity Central! If you like Gaia please consider rating it :)";
			GaiaSettings gaiaSettings = Utils.GetGaiaSettings();
			if (gaiaSettings != null && gaiaSettings.m_currentDefaults != null)
			{
				this.m_session.m_seaLevel = gaiaSettings.m_currentDefaults.m_seaLevel;
			}
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain != null)
			{
				this.m_session.m_terrainWidth = (int)activeTerrain.terrainData.size.x;
				this.m_session.m_terrainDepth = (int)activeTerrain.terrainData.size.z;
				this.m_session.m_terrainHeight = (int)activeTerrain.terrainData.size.y;
				if (pickupExistingTerrain)
				{
					GaiaDefaults gaiaDefaults = ScriptableObject.CreateInstance<GaiaDefaults>();
					gaiaDefaults.UpdateFromTerrain();
					GaiaResource gaiaResource = ScriptableObject.CreateInstance<GaiaResource>();
					gaiaResource.UpdatePrototypesFromTerrain();
					gaiaResource.ChangeSeaLevel(this.m_session.m_seaLevel);
					this.AddDefaults(gaiaDefaults);
					this.AddResource(gaiaResource);
					this.AddOperation(gaiaDefaults.GetTerrainCreationOperation(gaiaResource));
				}
			}
			else if (gaiaSettings != null && gaiaSettings.m_currentDefaults != null)
			{
				this.m_session.m_terrainWidth = gaiaSettings.m_currentDefaults.m_terrainSize;
				this.m_session.m_terrainDepth = gaiaSettings.m_currentDefaults.m_terrainHeight;
				this.m_session.m_terrainHeight = gaiaSettings.m_currentDefaults.m_terrainSize;
			}
			return this.m_session;
		}

		public void SetSeaLevel(float seaLevel)
		{
			this.m_session.m_seaLevel = seaLevel;
		}

		public float GetSeaLevel()
		{
			return this.m_session.m_seaLevel;
		}

		public void ResetSession()
		{
			if (this.m_session == null)
			{
				UnityEngine.Debug.LogError("Can not erase the session as there is no existing session!");
				return;
			}
			if (this.m_session.m_isLocked)
			{
				UnityEngine.Debug.LogError("Can not erase the session as it is locked!");
				return;
			}
			if (this.m_session.m_operations.Count > 1)
			{
				GaiaOperation gaiaOperation = this.m_session.m_operations[0];
				this.m_session.m_operations.Clear();
				if (gaiaOperation.m_operationType == GaiaOperation.OperationType.CreateTerrain)
				{
					this.AddOperation(gaiaOperation);
				}
			}
		}

		public void RandomiseStamps()
		{
			if (this.m_session == null)
			{
				UnityEngine.Debug.LogError("Can not randomise stamps as there is no existing session!");
				return;
			}
			if (this.m_session.m_isLocked)
			{
				UnityEngine.Debug.LogError("Can not randomise stamps as the existing session is locked!");
				return;
			}
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain == null)
			{
				GaiaSettings gaiaSettings = (GaiaSettings)Utils.GetAssetScriptableObject("GaiaSettings");
				if (gaiaSettings == null)
				{
					UnityEngine.Debug.LogError("Can not randomise stamps as we are missing the terrain and settings!");
					return;
				}
				GaiaDefaults currentDefaults = gaiaSettings.m_currentDefaults;
				GaiaResource currentResources = gaiaSettings.m_currentResources;
				if (currentDefaults == null || currentResources == null)
				{
					UnityEngine.Debug.LogError("Can not randomise stamps as we are missing the terrain defaults or resources!");
					return;
				}
				currentDefaults.CreateTerrain(currentResources);
			}
			Bounds bounds = default(Bounds);
			TerrainHelper.GetTerrainBounds(activeTerrain, ref bounds);
			GameObject gameObject = GameObject.Find("Gaia");
			if (gameObject == null)
			{
				gameObject = new GameObject("Gaia");
			}
			GameObject gameObject2 = GameObject.Find("Stamper");
			Stamper stamper;
			if (gameObject2 == null)
			{
				stamper = new GameObject("Stamper")
				{
					transform = 
					{
						parent = gameObject.transform
					}
				}.AddComponent<Stamper>();
			}
			else
			{
				stamper = gameObject2.GetComponent<Stamper>();
			}
			for (int i = 0; i < this.m_genNumStampsToGenerate; i++)
			{
				string empty = string.Empty;
				GaiaConstants.FeatureType featureType = GaiaConstants.FeatureType.Hills;
				stamper.LoadStamp(empty);
				stamper.FitToTerrain();
				stamper.HidePreview();
				if (i == 0)
				{
					float width = stamper.m_width;
					this.PositionStamp(bounds, stamper, featureType);
					stamper.m_rotation = 0f;
					stamper.m_x = 0f;
					stamper.m_z = 0f;
					stamper.m_width = width;
					if (this.m_genBorderStyle == GaiaConstants.GeneratorBorderStyle.Mountains)
					{
						stamper.m_distanceMask = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(1f, 1f),
							new Keyframe(1f, 1f)
						});
						stamper.m_areaMaskMode = GaiaConstants.ImageFitnessFilterMode.ImageGreyScale;
						stamper.m_imageMask = (Utils.GetAsset("Island Mask 1.jpg", typeof(Texture2D)) as Texture2D);
						stamper.m_imageMaskNormalise = true;
						stamper.m_imageMaskInvert = true;
					}
					else
					{
						stamper.m_distanceMask = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(1f, 1f),
							new Keyframe(1f, 1f)
						});
						stamper.m_areaMaskMode = GaiaConstants.ImageFitnessFilterMode.ImageGreyScale;
						stamper.m_imageMask = (Utils.GetAsset("Island Mask 1.jpg", typeof(Texture2D)) as Texture2D);
						stamper.m_imageMaskNormalise = true;
						stamper.m_imageMaskInvert = false;
					}
				}
				else
				{
					this.PositionStamp(bounds, stamper, featureType);
					float num = UnityEngine.Random.Range(0f, 1f);
					if (num < 0.1f)
					{
						stamper.m_stampOperation = GaiaConstants.FeatureOperation.LowerHeight;
						stamper.m_invertStamp = true;
					}
					else if (num < 0.35f)
					{
						stamper.m_stampOperation = GaiaConstants.FeatureOperation.StencilHeight;
						stamper.m_normaliseStamp = true;
						if (featureType == GaiaConstants.FeatureType.Rivers || featureType == GaiaConstants.FeatureType.Lakes)
						{
							stamper.m_invertStamp = true;
							stamper.m_stencilHeight = UnityEngine.Random.Range(-80f, -5f);
						}
						else if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
						{
							stamper.m_invertStamp = true;
							stamper.m_stencilHeight = UnityEngine.Random.Range(-80f, -5f);
						}
						else
						{
							stamper.m_invertStamp = false;
							stamper.m_stencilHeight = UnityEngine.Random.Range(5f, 80f);
						}
					}
					else
					{
						stamper.m_stampOperation = GaiaConstants.FeatureOperation.RaiseHeight;
						stamper.m_invertStamp = false;
					}
				}
				stamper.UpdateStamp();
				stamper.AddToSession(GaiaOperation.OperationType.Stamp, "Stamping " + stamper.m_stampPreviewImage.name);
			}
		}

		private void PositionStamp(Bounds bounds, Stamper stamper, GaiaConstants.FeatureType stampType)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = stamper.m_height * 4f;
			float num5 = 0f;
			if ((float)this.m_session.m_terrainHeight > 0f)
			{
				num5 = this.m_session.m_seaLevel / (float)this.m_session.m_terrainHeight;
			}
			if (stamper.GetHeightRange(ref num, ref num2, ref num3))
			{
				stamper.m_stampOperation = GaiaConstants.FeatureOperation.RaiseHeight;
				stamper.m_invertStamp = false;
				stamper.m_normaliseStamp = false;
				stamper.m_rotation = UnityEngine.Random.Range(-179f, 179f);
				stamper.m_width = UnityEngine.Random.Range(0.7f, 1.3f) * this.m_genScaleWidth;
				stamper.m_height = UnityEngine.Random.Range(0.7f, 1.3f) * this.m_genScaleHeight;
				float num6 = stamper.m_height / num4 * (float)this.m_session.m_terrainHeight;
				float num7 = num6 / 2f;
				float num8 = num5 * (float)this.m_session.m_terrainHeight;
				stamper.m_stickBaseToGround = false;
				stamper.m_y = num7 + num8 - num * num6;
				if (this.m_genBorderStyle == GaiaConstants.GeneratorBorderStyle.None)
				{
					stamper.m_x = UnityEngine.Random.Range(-bounds.extents.x, bounds.extents.x);
					stamper.m_z = UnityEngine.Random.Range(-bounds.extents.z, bounds.extents.z);
				}
				else
				{
					float num9 = 0.65f;
					stamper.m_x = UnityEngine.Random.Range(-(bounds.extents.x * num9), bounds.extents.x * num9);
					stamper.m_z = UnityEngine.Random.Range(-(bounds.extents.z * num9), bounds.extents.z * num9);
				}
				stamper.m_distanceMask = new AnimationCurve(new Keyframe[]
				{
					new Keyframe(0f, 1f),
					new Keyframe(1f, 0f)
				});
				stamper.m_areaMaskMode = GaiaConstants.ImageFitnessFilterMode.None;
				stamper.m_imageMask = null;
			}
		}

		private GaiaConstants.FeatureType GetWeightedRandomFeatureType()
		{
			float num = UnityEngine.Random.Range(0f, 1f);
			float num2 = this.m_genChanceOfHills + this.m_genChanceOfIslands + this.m_genChanceOfLakes + this.m_genChanceOfMesas + this.m_genChanceOfMountains + this.m_genChanceOfPlains + this.m_genChanceOfRivers + this.m_genChanceOfValleys + this.m_genChanceOfVillages + this.m_genChanceOfWaterfalls;
			if (num2 == 0f)
			{
				num2 = 1f;
			}
			float num3 = 0f;
			float num4 = num3 + this.m_genChanceOfHills / num2;
			if (num >= num3 && num < num4)
			{
				return GaiaConstants.FeatureType.Hills;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfIslands / num2;
			if (num >= num3 && num < num4)
			{
				return GaiaConstants.FeatureType.Islands;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfLakes / num2;
			if (num >= num3 && num < num4)
			{
				return GaiaConstants.FeatureType.Lakes;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfMesas / num2;
			if (num >= num3 && num < num4)
			{
				return GaiaConstants.FeatureType.Mesas;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfMountains / num2;
			if (num >= num3 && num < num4)
			{
				return GaiaConstants.FeatureType.Mountains;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfPlains / num2;
			if (num >= num3 && num < num4)
			{
				return GaiaConstants.FeatureType.Plains;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfRivers / num2;
			if (num >= num3 && num < num4)
			{
				return GaiaConstants.FeatureType.Rivers;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfValleys / num2;
			if (num >= num3 && num < num3)
			{
				return GaiaConstants.FeatureType.Valleys;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfVillages / num2;
			if (num >= num3 && num < num3)
			{
				return GaiaConstants.FeatureType.Villages;
			}
			num3 = num4;
			num4 = num3 + this.m_genChanceOfWaterfalls / num2;
			if (num >= num3 && num < num3)
			{
				return GaiaConstants.FeatureType.Waterfalls;
			}
			return (GaiaConstants.FeatureType)UnityEngine.Random.Range(2, 7);
		}

		public string GetRandomStampPath(GaiaConstants.FeatureType featureType)
		{
			switch (featureType)
			{
			case GaiaConstants.FeatureType.Adhoc:
				return string.Empty;
			case GaiaConstants.FeatureType.Bases:
				return string.Empty;
			case GaiaConstants.FeatureType.Hills:
				if (this.m_genHillStamps.Count == 0)
				{
					this.m_genHillStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Hills);
				}
				if (this.m_genHillStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genHillStamps[UnityEngine.Random.Range(0, this.m_genHillStamps.Count - 1)];
			case GaiaConstants.FeatureType.Islands:
				if (this.m_genIslandStamps.Count == 0)
				{
					this.m_genIslandStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Islands);
				}
				if (this.m_genIslandStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genIslandStamps[UnityEngine.Random.Range(0, this.m_genIslandStamps.Count - 1)];
			case GaiaConstants.FeatureType.Lakes:
				if (this.m_genLakeStamps.Count == 0)
				{
					this.m_genLakeStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Lakes);
				}
				if (this.m_genLakeStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genLakeStamps[UnityEngine.Random.Range(0, this.m_genLakeStamps.Count - 1)];
			case GaiaConstants.FeatureType.Mesas:
				if (this.m_genMesaStamps.Count == 0)
				{
					this.m_genMesaStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Mesas);
				}
				if (this.m_genMesaStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genMesaStamps[UnityEngine.Random.Range(0, this.m_genMesaStamps.Count - 1)];
			case GaiaConstants.FeatureType.Mountains:
				if (this.m_genMountainStamps.Count == 0)
				{
					this.m_genMountainStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Mountains);
				}
				if (this.m_genMountainStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genMountainStamps[UnityEngine.Random.Range(0, this.m_genMountainStamps.Count - 1)];
			case GaiaConstants.FeatureType.Plains:
				if (this.m_genPlainsStamps.Count == 0)
				{
					this.m_genPlainsStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Plains);
				}
				if (this.m_genPlainsStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genPlainsStamps[UnityEngine.Random.Range(0, this.m_genPlainsStamps.Count - 1)];
			case GaiaConstants.FeatureType.Rivers:
				if (this.m_genRiverStamps.Count == 0)
				{
					this.m_genRiverStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Rivers);
				}
				if (this.m_genRiverStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genRiverStamps[UnityEngine.Random.Range(0, this.m_genRiverStamps.Count - 1)];
			case GaiaConstants.FeatureType.Rocks:
				return string.Empty;
			case GaiaConstants.FeatureType.Valleys:
				if (this.m_genValleyStamps.Count == 0)
				{
					this.m_genValleyStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Valleys);
				}
				if (this.m_genValleyStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genValleyStamps[UnityEngine.Random.Range(0, this.m_genValleyStamps.Count - 1)];
			case GaiaConstants.FeatureType.Villages:
				if (this.m_genVillageStamps.Count == 0)
				{
					this.m_genVillageStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Villages);
				}
				if (this.m_genVillageStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genVillageStamps[UnityEngine.Random.Range(0, this.m_genVillageStamps.Count - 1)];
			case GaiaConstants.FeatureType.Waterfalls:
				if (this.m_genWaterfallStamps.Count == 0)
				{
					this.m_genWaterfallStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Waterfalls);
				}
				if (this.m_genWaterfallStamps.Count == 0)
				{
					return string.Empty;
				}
				return this.m_genWaterfallStamps[UnityEngine.Random.Range(0, this.m_genWaterfallStamps.Count - 1)];
			default:
				return string.Empty;
			}
		}

		public string GetRandomMountainFieldPath()
		{
			if (this.m_genMountainStamps.Count == 0)
			{
				this.m_genMountainStamps = Utils.GetGaiaStampsList(GaiaConstants.FeatureType.Mountains);
			}
			if (this.m_genMountainStamps.Count == 0)
			{
				return string.Empty;
			}
			int num = 0;
			for (int i = 0; i < this.m_genMountainStamps.Count; i++)
			{
				string text = this.m_genMountainStamps[i];
				if (text.Contains("Field"))
				{
					num++;
				}
			}
			int num2 = 0;
			int num3 = UnityEngine.Random.Range(0, num - 1);
			for (int i = 0; i < this.m_genMountainStamps.Count; i++)
			{
				string text = this.m_genMountainStamps[i];
				if (text.Contains("Field"))
				{
					if (num2 == num3)
					{
						return text;
					}
					num2++;
				}
			}
			return string.Empty;
		}

		public GameObject Apply(int operationIdx)
		{
			if (operationIdx < 0 || operationIdx >= this.m_session.m_operations.Count)
			{
				UnityEngine.Debug.LogWarning(string.Format("Can not Apply operation because the index {0} is out of bounds.", operationIdx));
				return null;
			}
			GaiaOperation gaiaOperation = this.m_session.m_operations[operationIdx];
			GameObject gameObject = this.FindOrCreateObject(gaiaOperation);
			if (gameObject == null)
			{
				return gameObject;
			}
			Stamper component = gameObject.GetComponent<Stamper>();
			if (component != null && gaiaOperation.m_operationType == GaiaOperation.OperationType.Stamp)
			{
				component.DeSerialiseJson(gaiaOperation.m_operationDataJson[0]);
				component.m_resources = (Utils.GetAsset(ScriptableObjectWrapper.GetSessionedFileName(this.m_session.GetSessionFileName(), component.m_resourcesPath), typeof(GaiaResource)) as GaiaResource);
				if (component.m_resources == null)
				{
					this.ExportSessionResource(component.m_resourcesPath);
					component.m_resources = (Utils.GetAsset(ScriptableObjectWrapper.GetSessionedFileName(this.m_session.GetSessionFileName(), component.m_resourcesPath), typeof(GaiaResource)) as GaiaResource);
				}
				component.m_seaLevel = this.m_session.m_seaLevel;
			}
			Spawner component2 = gameObject.GetComponent<Spawner>();
			if (component2 != null && gaiaOperation.m_operationType == GaiaOperation.OperationType.Spawn)
			{
				component2.DeSerialiseJson(gaiaOperation.m_operationDataJson[0]);
				component2.m_resources = (Utils.GetAsset(ScriptableObjectWrapper.GetSessionedFileName(this.m_session.GetSessionFileName(), component2.m_resourcesPath), typeof(GaiaResource)) as GaiaResource);
				if (component2.m_resources == null)
				{
					this.ExportSessionResource(component2.m_resourcesPath);
					component2.m_resources = (Utils.GetAsset(ScriptableObjectWrapper.GetSessionedFileName(this.m_session.GetSessionFileName(), component2.m_resourcesPath), typeof(GaiaResource)) as GaiaResource);
				}
				if (component2.m_resources == null)
				{
					UnityEngine.Debug.LogError("Unable to get resources file for " + component2.name);
				}
				else
				{
					component2.AssociateAssets();
					int[] missingResources = component2.GetMissingResources();
					if (missingResources.GetLength(0) > 0)
					{
						component2.AddResourcesToTerrain(missingResources);
					}
					component2.m_resources.ChangeSeaLevel(this.m_session.m_seaLevel);
				}
			}
			return gameObject;
		}

		public void PlaySession()
		{
			this.m_cancelPlayback = false;
			this.ExportSessionResources();
			base.StartCoroutine(this.PlaySessionCoRoutine());
		}

		public IEnumerator PlaySessionCoRoutine()
		{
			this.m_progress = 0UL;
			if (Application.isPlaying)
			{
				for (int idx = 0; idx < this.m_session.m_operations.Count; idx++)
				{
					if (!this.m_cancelPlayback && this.m_session.m_operations[idx].m_isActive)
					{
						yield return base.StartCoroutine(this.PlayOperationCoRoutine(idx));
					}
				}
			}
			else
			{
				for (int idx2 = 0; idx2 < this.m_session.m_operations.Count; idx2++)
				{
					if (!this.m_cancelPlayback && this.m_session.m_operations[idx2].m_isActive)
					{
						this.m_updateOperationCoroutine = this.PlayOperationCoRoutine(idx2);
						yield return new WaitForSeconds(0.2f);
					}
				}
			}
			UnityEngine.Debug.Log("Finished playing session " + this.m_session.m_name);
			this.m_updateSessionCoroutine = null;
			yield break;
		}

		public void PlayOperation(int opIdx)
		{
			this.m_cancelPlayback = false;
			base.StartCoroutine(this.PlayOperationCoRoutine(opIdx));
		}

		public IEnumerator PlayOperationCoRoutine(int operationIdx)
		{
			if (operationIdx < 0 || operationIdx >= this.m_session.m_operations.Count)
			{
				UnityEngine.Debug.LogWarning(string.Format("Operation index {0} is out of bounds.", operationIdx));
				this.m_updateOperationCoroutine = null;
				yield break;
			}
			if (!this.m_session.m_operations[operationIdx].m_isActive)
			{
				UnityEngine.Debug.LogWarning(string.Format("Operation '{0}' is not active. Ignoring.", this.m_session.m_operations[operationIdx].m_description));
				this.m_updateOperationCoroutine = null;
				yield break;
			}
			bool lockState = this.m_session.m_isLocked;
			this.m_session.m_isLocked = true;
			GaiaOperation operation = this.m_session.m_operations[operationIdx];
			GameObject go = this.Apply(operationIdx);
			Stamper stamper = null;
			Spawner spawner = null;
			if (go != null)
			{
				stamper = go.GetComponent<Stamper>();
				spawner = go.GetComponent<Spawner>();
			}
			switch (operation.m_operationType)
			{
			case GaiaOperation.OperationType.CreateTerrain:
				if (TerrainHelper.GetActiveTerrainCount() != 0 || this.m_session.m_defaults == null || this.m_session.m_defaults.m_content.GetLength(0) > 0)
				{
				}
				break;
			case GaiaOperation.OperationType.FlattenTerrain:
				if (stamper != null)
				{
					stamper.FlattenTerrain();
				}
				break;
			case GaiaOperation.OperationType.SmoothTerrain:
				if (stamper != null)
				{
					stamper.SmoothTerrain();
				}
				break;
			case GaiaOperation.OperationType.ClearDetails:
				if (stamper != null)
				{
					stamper.ClearDetails();
				}
				break;
			case GaiaOperation.OperationType.ClearTrees:
				if (stamper != null)
				{
					stamper.ClearTrees();
				}
				break;
			case GaiaOperation.OperationType.Stamp:
				if (stamper != null)
				{
					this.m_currentStamper = stamper;
					this.m_currentSpawner = null;
					if (!Application.isPlaying)
					{
						stamper.HidePreview();
						stamper.Stamp();
						while (stamper.IsStamping())
						{
							if ((DateTime.Now - this.m_lastUpdateDateTime).Milliseconds > 250)
							{
								this.m_lastUpdateDateTime = DateTime.Now;
								this.m_progress += 1UL;
							}
							yield return new WaitForSeconds(0.2f);
						}
					}
					else
					{
						yield return base.StartCoroutine(stamper.ApplyStamp());
					}
				}
				break;
			case GaiaOperation.OperationType.StampUndo:
				if (stamper != null)
				{
					stamper.Undo();
				}
				break;
			case GaiaOperation.OperationType.StampRedo:
				if (stamper != null)
				{
					stamper.Redo();
				}
				break;
			case GaiaOperation.OperationType.Spawn:
				if (spawner != null)
				{
					this.m_currentStamper = null;
					this.m_currentSpawner = spawner;
					if (!Application.isPlaying)
					{
						spawner.RunSpawnerIteration();
						while (spawner.IsSpawning())
						{
							if ((DateTime.Now - this.m_lastUpdateDateTime).Milliseconds > 250)
							{
								this.m_lastUpdateDateTime = DateTime.Now;
								this.m_progress += 1UL;
							}
							yield return new WaitForSeconds(0.2f);
						}
					}
				}
				break;
			}
			this.m_session.m_isLocked = lockState;
			this.m_updateOperationCoroutine = null;
			yield break;
		}

		public void CancelPlayback()
		{
			this.m_cancelPlayback = true;
			if (this.m_currentStamper != null)
			{
				this.m_currentStamper.CancelStamp();
			}
			if (this.m_currentSpawner != null)
			{
				this.m_currentSpawner.CancelSpawn();
			}
		}

		public void ExportSessionResources()
		{
			string text = "Assets/GaiaSessions/";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			text = Path.Combine(text, Utils.FixFileName(this.m_session.m_name));
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (this.m_session.m_defaults != null && this.m_session.m_defaults.m_content.GetLength(0) > 0)
			{
				string path = Path.Combine(text, ScriptableObjectWrapper.GetSessionedFileName(this.m_session.m_name, this.m_session.m_defaults.m_fileName));
				Utils.WriteAllBytes(path, this.m_session.m_defaults.m_content);
			}
			foreach (KeyValuePair<string, ScriptableObjectWrapper> keyValuePair in this.m_session.m_resources)
			{
				string path2 = Path.Combine(text, ScriptableObjectWrapper.GetSessionedFileName(this.m_session.m_name, keyValuePair.Value.m_fileName));
				Utils.WriteAllBytes(path2, keyValuePair.Value.m_content);
			}
		}

		public void ExportSessionDefaults()
		{
			string text = "Assets/GaiaSessions/";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			text = Path.Combine(text, Utils.FixFileName(this.m_session.m_name));
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (this.m_session.m_defaults != null && this.m_session.m_defaults.m_content.GetLength(0) > 0)
			{
				string path = Path.Combine(text, ScriptableObjectWrapper.GetSessionedFileName(this.m_session.m_name, this.m_session.m_defaults.m_fileName));
				Utils.WriteAllBytes(path, this.m_session.m_defaults.m_content);
			}
		}

		public void ExportSessionResource(string resourcePath)
		{
			string text = "Assets/GaiaSessions/";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			text = Path.Combine(text, Utils.FixFileName(this.m_session.m_name));
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			foreach (KeyValuePair<string, ScriptableObjectWrapper> keyValuePair in this.m_session.m_resources)
			{
				if (Path.GetFileName(resourcePath).ToLower() == Path.GetFileName(keyValuePair.Value.m_fileName).ToLower())
				{
					string path = Path.Combine(text, ScriptableObjectWrapper.GetSessionedFileName(this.m_session.m_name, keyValuePair.Value.m_fileName));
					Utils.WriteAllBytes(path, keyValuePair.Value.m_content);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (this.m_session != null)
			{
				Bounds bounds = default(Bounds);
				if (TerrainHelper.GetTerrainBounds(base.transform.position, ref bounds))
				{
					Gizmos.color = Color.white;
					Gizmos.DrawWireCube(bounds.center, bounds.size);
					bounds.center = new Vector3(bounds.center.x, this.m_session.m_seaLevel, bounds.center.z);
					bounds.size = new Vector3(bounds.size.x, 0.05f, bounds.size.z);
					Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, Color.blue.a / 4f);
					Gizmos.DrawCube(bounds.center, bounds.size);
				}
			}
		}

		private GameObject FindOrCreateObject(GaiaOperation operation)
		{
			if (operation.m_generatedByType == "Gaia.Stamper")
			{
				Stamper[] array = UnityEngine.Object.FindObjectsOfType<Stamper>();
				for (int i = 0; i < array.GetLength(0); i++)
				{
					if (array[i].m_stampID == operation.m_generatedByID && array[i].name == operation.m_generatedByName)
					{
						return array[i].gameObject;
					}
				}
				return this.ShowStamper(operation.m_generatedByName, operation.m_generatedByID);
			}
			if (operation.m_generatedByType == "Gaia.Spawner")
			{
				Spawner[] array2 = UnityEngine.Object.FindObjectsOfType<Spawner>();
				for (int j = 0; j < array2.GetLength(0); j++)
				{
					if (array2[j].m_spawnerID == operation.m_generatedByID && array2[j].name == operation.m_generatedByName)
					{
						return array2[j].gameObject;
					}
				}
				return this.CreateSpawner(operation.m_generatedByName, operation.m_generatedByID);
			}
			return null;
		}

		private GameObject ShowStamper(string name, string id)
		{
			GameObject gameObject = GameObject.Find("Gaia");
			if (gameObject == null)
			{
				gameObject = new GameObject("Gaia");
			}
			GameObject gameObject2 = GameObject.Find(name);
			if (gameObject2 == null)
			{
				gameObject2 = new GameObject(name);
				gameObject2.transform.parent = gameObject.transform;
				Stamper stamper = gameObject2.AddComponent<Stamper>();
				stamper.m_stampID = id;
				stamper.HidePreview();
				stamper.m_seaLevel = this.m_session.m_seaLevel;
			}
			return gameObject2;
		}

		private GameObject CreateSpawner(string name, string id)
		{
			GameObject gameObject = GameObject.Find("Gaia");
			if (gameObject == null)
			{
				gameObject = new GameObject("Gaia");
			}
			GameObject gameObject2 = new GameObject(name);
			gameObject2.transform.parent = gameObject.transform;
			Spawner spawner = gameObject2.AddComponent<Spawner>();
			spawner.m_spawnerID = id;
			return gameObject2;
		}

		public IEnumerator m_updateSessionCoroutine;

		public IEnumerator m_updateOperationCoroutine;

		private bool m_cancelPlayback;

		public GaiaSession m_session;

		public bool m_genShowRandomGenerator;

		public bool m_genShowTerrainHelper;

		public GaiaConstants.GeneratorBorderStyle m_genBorderStyle = GaiaConstants.GeneratorBorderStyle.Water;

		public int m_genNumStampsToGenerate = 10;

		public float m_genScaleWidth = 10f;

		public float m_genScaleHeight = 4f;

		public float m_genChanceOfHills = 0.7f;

		public float m_genChanceOfIslands;

		public float m_genChanceOfLakes;

		public float m_genChanceOfMesas = 0.1f;

		public float m_genChanceOfMountains = 0.1f;

		public float m_genChanceOfPlains;

		public float m_genChanceOfRivers = 0.1f;

		public float m_genChanceOfValleys;

		public float m_genChanceOfVillages;

		public float m_genChanceOfWaterfalls;

		[fsIgnore]
		public Stamper m_currentStamper;

		[fsIgnore]
		public Spawner m_currentSpawner;

		[fsIgnore]
		public DateTime m_lastUpdateDateTime = DateTime.Now;

		[fsIgnore]
		public ulong m_progress;

		private List<string> m_genHillStamps = new List<string>();

		private List<string> m_genIslandStamps = new List<string>();

		private List<string> m_genLakeStamps = new List<string>();

		private List<string> m_genMesaStamps = new List<string>();

		private List<string> m_genMountainStamps = new List<string>();

		private List<string> m_genPlainsStamps = new List<string>();

		private List<string> m_genRiverStamps = new List<string>();

		private List<string> m_genValleyStamps = new List<string>();

		private List<string> m_genVillageStamps = new List<string>();

		private List<string> m_genWaterfallStamps = new List<string>();
	}
}
