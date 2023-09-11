using System;
using System.Collections;
using Gaia.FullSerializer;
using UnityEngine;

namespace Gaia
{
	[ExecuteInEditMode]
	public class Stamper : MonoBehaviour
	{
		public void LoadStamp()
		{
			this.m_featureID = -1;
			this.m_scanBounds = new Bounds(base.transform.position, Vector3.one * 10f);
			if (this.m_stampPreviewImage == null)
			{
				UnityEngine.Debug.LogWarning("Can't load feature - texture not set");
				return;
			}
			this.m_featureID = this.m_stampPreviewImage.GetInstanceID();
			if (!Utils.CheckValidGaiaStampPath(this.m_stampPreviewImage))
			{
				UnityEngine.Debug.LogError("The file provided is not a valid stamp. You need to drag the stamp preview from one of the directories underneath your Gaia Stamps directory.");
				this.m_featureID = -1;
				this.m_stampPreviewImage = null;
				return;
			}
			string gaiaStampPath = Utils.GetGaiaStampPath(this.m_stampPreviewImage);
			this.m_stampHM = new UnityHeightMap(gaiaStampPath);
			if (!this.m_stampHM.HasData())
			{
				this.m_featureID = -1;
				this.m_stampPreviewImage = null;
				UnityEngine.Debug.LogError("Was unable to load " + gaiaStampPath);
				return;
			}
			float[] array = new float[5];
			Buffer.BlockCopy(this.m_stampHM.GetMetaData(), 0, array, 0, array.Length * 4);
			this.m_scanWidth = (int)array[0];
			this.m_scanDepth = (int)array[1];
			this.m_scanHeight = (int)array[2];
			this.m_scanResolution = array[3];
			this.m_baseLevel = array[4];
			this.m_scanBounds = new Bounds(base.transform.position, new Vector3((float)this.m_scanWidth * this.m_scanResolution * this.m_width, (float)this.m_scanHeight * this.m_scanResolution * this.m_height, (float)this.m_scanDepth * this.m_scanResolution * this.m_width));
			if (this.m_invertStamp)
			{
				this.m_stampHM.Invert();
			}
			if (this.m_normaliseStamp)
			{
				this.m_stampHM.Normalise();
			}
			this.GeneratePreviewMesh();
		}

		public void LoadStamp(string imagePreviewPath)
		{
			this.LoadStamp();
		}

		public bool LoadRuntimeStamp(TextAsset stamp)
		{
			this.m_stampHM = new UnityHeightMap(stamp);
			if (!this.m_stampHM.HasData())
			{
				this.m_featureID = -1;
				this.m_stampPreviewImage = null;
				UnityEngine.Debug.LogError("Was unable to load textasset stamp");
				return false;
			}
			float[] array = new float[5];
			Buffer.BlockCopy(this.m_stampHM.GetMetaData(), 0, array, 0, array.Length * 4);
			this.m_scanWidth = (int)array[0];
			this.m_scanDepth = (int)array[1];
			this.m_scanHeight = (int)array[2];
			this.m_scanResolution = array[3];
			this.m_baseLevel = array[4];
			this.m_scanBounds = new Bounds(base.transform.position, new Vector3((float)this.m_scanWidth * this.m_scanResolution * this.m_width, (float)this.m_scanHeight * this.m_scanResolution * this.m_height, (float)this.m_scanDepth * this.m_scanResolution * this.m_width));
			if (this.m_invertStamp)
			{
				this.m_stampHM.Invert();
			}
			if (this.m_normaliseStamp)
			{
				this.m_stampHM.Normalise();
			}
			return true;
		}

		public void InvertStamp()
		{
			this.m_stampHM.Invert();
			this.GeneratePreviewMesh();
		}

		public void NormaliseStamp()
		{
			this.m_stampHM.Normalise();
			this.GeneratePreviewMesh();
		}

		public void Stamp()
		{
			this.m_cancelStamp = false;
			this.m_stampComplete = false;
			this.m_stampProgress = 0f;
			this.AddToSession(GaiaOperation.OperationType.Stamp, "Stamping " + this.m_stampPreviewImage.name);
			base.StartCoroutine(this.ApplyStamp());
		}

		public void CancelStamp()
		{
			this.m_cancelStamp = true;
		}

		public bool IsStamping()
		{
			return !this.m_stampComplete;
		}

		public void UpdateStamp()
		{
			if (this.m_stickBaseToGround)
			{
				this.AlignToGround();
			}
			base.transform.position = new Vector3(this.m_x, this.m_y, this.m_z);
			base.transform.localScale = new Vector3(this.m_width, this.m_height, this.m_width);
			base.transform.localRotation = Quaternion.AngleAxis(this.m_rotation, Vector3.up);
			this.m_scanBounds.center = base.transform.position;
			this.m_scanBounds.size = new Vector3((float)this.m_scanWidth * this.m_scanResolution * this.m_width, (float)this.m_scanHeight * this.m_scanResolution * this.m_height, (float)this.m_scanDepth * this.m_scanResolution * this.m_width);
			if (this.m_stampHM != null)
			{
				this.m_stampHM.SetBoundsWU(this.m_scanBounds);
			}
			base.transform.hasChanged = false;
		}

		public void AlignToGround()
		{
			if (this.m_stampHM == null || !this.m_stampHM.HasData())
			{
				return;
			}
			float num = 0f;
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain == null)
			{
				terrain = Terrain.activeTerrain;
			}
			if (terrain != null)
			{
				num = terrain.transform.position.y;
			}
			this.m_scanBounds.center = base.transform.position;
			this.m_scanBounds.size = new Vector3((float)this.m_scanWidth * this.m_scanResolution * this.m_width, (float)this.m_scanHeight * this.m_scanResolution * this.m_height, (float)this.m_scanDepth * this.m_scanResolution * this.m_width);
			if (terrain == null)
			{
				this.m_y = num + this.m_scanBounds.extents.y;
			}
			else
			{
				float num2 = this.m_scanBounds.min.y + this.m_scanBounds.size.y * this.m_baseLevel;
				this.m_y = this.m_scanBounds.center.y - (num2 - num);
			}
		}

		public bool GetHeightRange(ref float baseLevel, ref float minHeight, ref float maxHeight)
		{
			if (this.m_stampHM == null || !this.m_stampHM.HasData())
			{
				return false;
			}
			baseLevel = this.m_baseLevel;
			this.m_stampHM.GetHeightRange(ref minHeight, ref maxHeight);
			return true;
		}

		public void FitToTerrain()
		{
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain == null)
			{
				terrain = TerrainHelper.GetActiveTerrain();
			}
			if (terrain == null)
			{
				return;
			}
			Bounds bounds = default(Bounds);
			if (TerrainHelper.GetTerrainBounds(terrain, ref bounds))
			{
				this.m_height = bounds.size.y / 100f * 2f;
				if (this.m_stampHM != null && this.m_stampHM.HasData())
				{
					this.m_width = bounds.size.x / (float)this.m_stampHM.Width() * 10f;
				}
				else
				{
					this.m_width = this.m_height;
				}
				this.m_height *= 0.25f;
				this.m_x = bounds.center.x;
				this.m_y = bounds.center.y;
				this.m_z = bounds.center.z;
				this.m_rotation = 0f;
			}
			if (this.m_stickBaseToGround)
			{
				this.AlignToGround();
			}
		}

		public bool IsFitToTerrain()
		{
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain == null)
			{
				terrain = Terrain.activeTerrain;
			}
			if (terrain == null || this.m_stampHM == null || !this.m_stampHM.HasData())
			{
				UnityEngine.Debug.LogError("Could not check if fit to terrain - no terrain present");
				return false;
			}
			Bounds bounds = default(Bounds);
			if (TerrainHelper.GetTerrainBounds(terrain, ref bounds))
			{
				float num = bounds.size.x / (float)this.m_stampHM.Width() * 10f;
				float x = bounds.center.x;
				float z = bounds.center.z;
				float num2 = 0f;
				return num == this.m_width && x == this.m_x && z == this.m_z && num2 == this.m_rotation;
			}
			return false;
		}

		public void AddToSession(GaiaOperation.OperationType opType, string opName)
		{
			GaiaSessionManager sessionManager = GaiaSessionManager.GetSessionManager(false);
			if (sessionManager != null && !sessionManager.IsLocked())
			{
				GaiaOperation gaiaOperation = new GaiaOperation();
				gaiaOperation.m_description = opName;
				gaiaOperation.m_generatedByID = this.m_stampID;
				gaiaOperation.m_generatedByName = base.transform.name;
				gaiaOperation.m_generatedByType = base.GetType().ToString();
				gaiaOperation.m_isActive = true;
				gaiaOperation.m_operationDateTime = DateTime.Now.ToString();
				gaiaOperation.m_operationType = opType;
				if (opType == GaiaOperation.OperationType.Stamp)
				{
					gaiaOperation.m_operationDataJson = new string[1];
					gaiaOperation.m_operationDataJson[0] = this.SerialiseJson();
				}
				else
				{
					gaiaOperation.m_operationDataJson = new string[0];
				}
				sessionManager.AddOperation(gaiaOperation);
			}
		}

		public string SerialiseJson()
		{
			fsSerializer fsSerializer = new fsSerializer();
			fsData data;
			fsSerializer.TrySerialize<Stamper>(this, out data);
			return fsJsonPrinter.CompressedJson(data);
		}

		public void DeSerialiseJson(string json)
		{
			fsData data = fsJsonParser.Parse(json);
			fsSerializer fsSerializer = new fsSerializer();
			Stamper stamper = this;
			fsSerializer.TryDeserialize<Stamper>(data, ref stamper);
			stamper.LoadStamp();
			stamper.UpdateStamp();
		}

		public void FlattenTerrain()
		{
			this.AddToSession(GaiaOperation.OperationType.FlattenTerrain, "Flattening terrain");
			this.m_undoMgr = new GaiaWorldManager(Terrain.activeTerrains);
			this.m_undoMgr.LoadFromWorld();
			this.m_redoMgr = new GaiaWorldManager(Terrain.activeTerrains);
			this.m_redoMgr.FlattenWorld();
			this.m_redoMgr = null;
		}

		public void SmoothTerrain()
		{
			this.AddToSession(GaiaOperation.OperationType.SmoothTerrain, "Smoothing terrain");
			this.m_undoMgr = new GaiaWorldManager(Terrain.activeTerrains);
			this.m_undoMgr.LoadFromWorld();
			this.m_redoMgr = new GaiaWorldManager(Terrain.activeTerrains);
			this.m_redoMgr.SmoothWorld();
			this.m_redoMgr = null;
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

		public bool CanPreview()
		{
			return this.m_previewRenderer != null;
		}

		public bool CurrentPreviewState()
		{
			return this.m_previewRenderer != null && this.m_previewRenderer.enabled;
		}

		public void ShowPreview()
		{
			if (this.m_previewRenderer != null)
			{
				this.m_previewRenderer.enabled = true;
			}
		}

		public void HidePreview()
		{
			if (this.m_previewRenderer != null)
			{
				this.m_previewRenderer.enabled = false;
			}
		}

		public void TogglePreview()
		{
			if (this.m_previewRenderer != null)
			{
				this.m_previewRenderer.enabled = !this.m_previewRenderer.enabled;
			}
		}

		public bool CanUndo()
		{
			return this.m_undoMgr != null;
		}

		public void CreateUndo()
		{
			this.m_undoMgr = new GaiaWorldManager(Terrain.activeTerrains);
			this.m_undoMgr.LoadFromWorld();
			this.m_redoMgr = null;
		}

		public void Undo()
		{
			if (this.m_undoMgr != null)
			{
				this.AddToSession(GaiaOperation.OperationType.StampUndo, "Undoing stamp");
				this.m_redoMgr = new GaiaWorldManager(Terrain.activeTerrains);
				this.m_redoMgr.LoadFromWorld();
				this.m_undoMgr.SaveToWorld(true);
			}
		}

		public bool CanRedo()
		{
			return this.m_redoMgr != null;
		}

		public void Redo()
		{
			if (this.m_redoMgr != null)
			{
				this.AddToSession(GaiaOperation.OperationType.StampRedo, "Redoing stamp");
				this.m_redoMgr.SaveToWorld(true);
				this.m_redoMgr = null;
			}
		}

		private void OnEnable()
		{
			if (this.m_stampPreviewImage != null)
			{
				this.LoadStamp();
			}
			if (Application.isPlaying)
			{
				this.HidePreview();
			}
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

		private void OnDrawGizmosSelected()
		{
			this.DrawGizmos(true);
		}

		private void OnDrawGizmos()
		{
			this.DrawGizmos(false);
		}

		private void DrawGizmos(bool isSelected)
		{
			if (this.m_stampPreviewImage == null)
			{
				return;
			}
			if (base.transform.hasChanged)
			{
				this.m_x = base.transform.position.x;
				this.m_y = base.transform.position.y;
				this.m_z = base.transform.position.z;
				this.m_rotation = base.transform.localEulerAngles.y;
				if (base.transform.localScale.x != this.m_width || base.transform.localScale.z != this.m_width)
				{
					float num = Mathf.Abs(base.transform.localScale.x - this.m_width);
					float num2 = Mathf.Abs(base.transform.localScale.z - this.m_width);
					if (num > num2)
					{
						if (base.transform.localScale.x > 0f)
						{
							this.m_width = base.transform.localScale.x;
						}
					}
					else if (base.transform.localScale.z > 0f)
					{
						this.m_width = base.transform.localScale.z;
					}
				}
				if (base.transform.localScale.y != this.m_height && base.transform.localScale.y > 0f)
				{
					this.m_height = base.transform.localScale.y;
				}
				this.UpdateStamp();
			}
			if (!isSelected && !this.m_alwaysShow)
			{
				return;
			}
			if (this.m_showBase)
			{
				Bounds bounds = default(Bounds);
				if (TerrainHelper.GetTerrainBounds(base.transform.position, ref bounds))
				{
					bounds.center = new Vector3(bounds.center.x, this.m_scanBounds.min.y + this.m_scanBounds.size.y * this.m_baseLevel, bounds.center.z);
					bounds.size = new Vector3(bounds.size.x, 0.05f, bounds.size.z);
					Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, Color.yellow.a / 2f);
					Gizmos.DrawCube(bounds.center, bounds.size);
				}
			}
			if (this.m_resources != null)
			{
				this.m_seaLevel = this.m_resources.m_seaLevel;
			}
			if (this.m_showSeaLevel)
			{
				Bounds bounds2 = default(Bounds);
				if (TerrainHelper.GetTerrainBounds(base.transform.position, ref bounds2))
				{
					bounds2.center = new Vector3(bounds2.center.x, this.m_seaLevel, bounds2.center.z);
					bounds2.size = new Vector3(bounds2.size.x, 0.05f, bounds2.size.z);
					if (isSelected)
					{
						Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, Color.blue.a / 2f);
						Gizmos.DrawCube(bounds2.center, bounds2.size);
					}
					else
					{
						Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, Color.blue.a / 4f);
						Gizmos.DrawCube(bounds2.center, bounds2.size);
					}
				}
			}
			if (this.m_showRulers)
			{
				this.DrawRulers();
			}
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Vector3 size = new Vector3((float)this.m_scanWidth * this.m_scanResolution, (float)this.m_scanHeight * this.m_scanResolution, (float)this.m_scanDepth * this.m_scanResolution);
			Gizmos.color = new Color(this.m_gizmoColour.r, this.m_gizmoColour.g, this.m_gizmoColour.b, this.m_gizmoColour.a / 2f);
			Gizmos.DrawWireCube(Vector3.zero, size);
			Gizmos.matrix = matrix;
			Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
			if (terrain != null)
			{
				Gizmos.color = Color.white;
				Bounds bounds3 = default(Bounds);
				TerrainHelper.GetTerrainBounds(terrain, ref bounds3);
				Gizmos.DrawWireCube(bounds3.center, bounds3.size);
			}
		}

		private void DrawRulers()
		{
		}

		public IEnumerator ApplyStamp()
		{
			this.UpdateStamp();
			GaiaWorldManager mgr = new GaiaWorldManager(Terrain.activeTerrains);
			mgr.LoadFromWorld();
			if (mgr.TileCount == 0)
			{
				UnityEngine.Debug.LogError("Can not stamp without a terrain present!");
				this.m_stampProgress = 0f;
				this.m_stampComplete = true;
				this.m_updateCoroutine = null;
				yield break;
			}
			this.CreateUndo();
			if (this.m_areaMaskMode != GaiaConstants.ImageFitnessFilterMode.None && !this.LoadImageMask())
			{
				this.m_stampProgress = 0f;
				this.m_stampComplete = true;
				this.m_updateCoroutine = null;
				yield break;
			}
			Vector3 rotation = new Vector3(0f, base.transform.localRotation.eulerAngles.y, 0f);
			Vector3 negRotation = new Vector3(0f, base.transform.localRotation.eulerAngles.y * -1f, 0f);
			Bounds origSmBoundsWU = this.m_stampHM.GetBoundsWU();
			Bounds newSmBoundsWU = default(Bounds);
			newSmBoundsWU.center = origSmBoundsWU.center;
			newSmBoundsWU.Encapsulate(this.RotatePointAroundPivot(new Vector3(origSmBoundsWU.min.x, origSmBoundsWU.center.y, origSmBoundsWU.min.z), origSmBoundsWU.center, rotation));
			newSmBoundsWU.Encapsulate(this.RotatePointAroundPivot(new Vector3(origSmBoundsWU.min.x, origSmBoundsWU.center.y, origSmBoundsWU.max.z), origSmBoundsWU.center, rotation));
			newSmBoundsWU.Encapsulate(this.RotatePointAroundPivot(new Vector3(origSmBoundsWU.max.x, origSmBoundsWU.center.y, origSmBoundsWU.min.z), origSmBoundsWU.center, rotation));
			newSmBoundsWU.Encapsulate(this.RotatePointAroundPivot(new Vector3(origSmBoundsWU.max.x, origSmBoundsWU.center.y, origSmBoundsWU.max.z), origSmBoundsWU.center, rotation));
			Vector3 newSmSizeTU = mgr.Ceil(mgr.WUtoTU(newSmBoundsWU.size));
			Vector3 pivot = new Vector3(0.5f, 0f, 0.5f);
			int newSmMaxX = (int)newSmSizeTU.x;
			int newSmMaxZ = (int)newSmSizeTU.z;
			float newSmXtoNU = 1f / newSmSizeTU.x;
			float newSmZtoNU = 1f / newSmSizeTU.z;
			float xNewSMtoOrigSMScale = newSmBoundsWU.size.x / origSmBoundsWU.size.x;
			float zNewSMtoOrigSMScale = newSmBoundsWU.size.x / origSmBoundsWU.size.z;
			float scaleOffsetX = 0.5f * ((origSmBoundsWU.size.x - newSmBoundsWU.size.x) / origSmBoundsWU.size.x);
			float scaleOffsetZ = 0.5f * ((origSmBoundsWU.size.z - newSmBoundsWU.size.x) / origSmBoundsWU.size.z);
			float currentTime = Time.realtimeSinceStartup;
			float accumulatedTime = 0f;
			int currChecks = 0;
			int totalChecks = newSmMaxX * newSmMaxZ;
			Vector3 globalCentreTU = mgr.WUtoTU(base.transform.position);
			Vector3 globalOffsetTU = globalCentreTU - newSmSizeTU * 0.5f;
			Vector3 globalPositionTU = Vector3.one;
			float smToOrigHeightConversion = origSmBoundsWU.size.y / mgr.WorldBoundsWU.size.y;
			float smHeightOffset = (origSmBoundsWU.min.y - mgr.WorldBoundsWU.min.y) / mgr.WorldBoundsWU.size.y;
			float stencilHeightNU = this.m_stencilHeight / mgr.WorldBoundsWU.size.y;
			for (int x = 0; x < newSmMaxX; x++)
			{
				float newSmXNU = (float)x * newSmXtoNU;
				for (int z = 0; z < newSmMaxZ; z++)
				{
					float newSmZNU = (float)z * newSmZtoNU;
					int num;
					currChecks = (num = currChecks) + 1;
					this.m_stampProgress = (float)num / (float)totalChecks;
					float newTime = Time.realtimeSinceStartup;
					float stepTime = newTime - currentTime;
					currentTime = newTime;
					accumulatedTime += stepTime;
					if (accumulatedTime > this.m_updateTimeAllowed)
					{
						accumulatedTime = 0f;
						yield return null;
					}
					if (this.m_cancelStamp)
					{
						break;
					}
					globalPositionTU.x = (float)z + globalOffsetTU.z;
					globalPositionTU.y = globalCentreTU.y;
					globalPositionTU.z = (float)x + globalOffsetTU.x;
					if (mgr.InBoundsTU(globalPositionTU))
					{
						Vector3 position = new Vector3(newSmXNU, 0f, newSmZNU);
						position = this.RotatePointAroundPivot(position, pivot, negRotation);
						float origSmXNU = position.x * xNewSMtoOrigSMScale + scaleOffsetX;
						float origSmZNU = position.z * zNewSMtoOrigSMScale + scaleOffsetZ;
						if (origSmXNU >= 0f && origSmXNU <= 1f)
						{
							if (origSmZNU >= 0f && origSmZNU <= 1f)
							{
								float distance = Utils.Math_Distance(origSmXNU, origSmZNU, pivot.x, pivot.z) * 2f;
								float strength = this.m_distanceMask.Evaluate(distance);
								if (this.m_areaMaskMode != GaiaConstants.ImageFitnessFilterMode.None && this.m_imageMaskHM != null)
								{
									strength *= this.m_imageMaskHM[origSmXNU, origSmZNU];
								}
								float smHeightRaw = this.m_heightModifier.Evaluate(this.m_stampHM[origSmXNU, origSmZNU]);
								float smHeightAdj;
								if (this.m_stampOperation != GaiaConstants.FeatureOperation.StencilHeight)
								{
									smHeightAdj = smHeightOffset + smHeightRaw * smToOrigHeightConversion;
								}
								else
								{
									smHeightAdj = smHeightRaw;
								}
								float terrainHeight = mgr.GetHeightTU(globalPositionTU);
								float newHeight = this.CalculateHeight(terrainHeight, smHeightRaw, smHeightAdj, stencilHeightNU, strength);
								mgr.SetHeightTU(globalPositionTU, Mathf.Clamp01(newHeight));
							}
						}
					}
				}
			}
			if (!this.m_cancelStamp)
			{
				mgr.SaveToWorld(false);
			}
			else
			{
				this.m_undoMgr = null;
				this.m_redoMgr = null;
			}
			this.m_stampProgress = 0f;
			this.m_stampComplete = true;
			this.m_updateCoroutine = null;
			yield break;
		}

		private void GeneratePreviewMesh()
		{
			if (this.m_previewMaterial == null)
			{
				this.m_previewMaterial = new Material(Shader.Find("Diffuse"));
				this.m_previewMaterial.color = Color.white;
				if (Terrain.activeTerrain != null && Terrain.activeTerrain.terrainData.splatPrototypes.Length > 0)
				{
					Texture2D texture;
					if (Terrain.activeTerrain.terrainData.splatPrototypes.Length == 4)
					{
						texture = Terrain.activeTerrain.terrainData.splatPrototypes[3].texture;
					}
					else
					{
						texture = Terrain.activeTerrain.terrainData.splatPrototypes[0].texture;
					}
					Utils.MakeTextureReadable(texture);
					Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, true);
					texture2D.SetPixels32(texture.GetPixels32());
					texture2D.wrapMode = TextureWrapMode.Repeat;
					texture2D.Apply();
					this.m_previewMaterial.mainTexture = texture2D;
					this.m_previewMaterial.mainTextureScale = new Vector2(30f, 30f);
				}
				this.m_previewMaterial.hideFlags = HideFlags.HideInInspector;
				this.m_previewMaterial.name = "StamperMaterial";
			}
			this.m_previewFilter = base.GetComponent<MeshFilter>();
			if (this.m_previewFilter == null)
			{
				base.gameObject.AddComponent<MeshFilter>();
				this.m_previewFilter = base.GetComponent<MeshFilter>();
				this.m_previewFilter.hideFlags = HideFlags.HideInInspector;
			}
			this.m_previewRenderer = base.GetComponent<MeshRenderer>();
			if (this.m_previewRenderer == null)
			{
				base.gameObject.AddComponent<MeshRenderer>();
				this.m_previewRenderer = base.GetComponent<MeshRenderer>();
				this.m_previewRenderer.hideFlags = HideFlags.HideInInspector;
			}
			this.m_previewRenderer.sharedMaterial = this.m_previewMaterial;
			Vector3 targetSize = new Vector3((float)this.m_scanWidth * this.m_scanResolution, (float)this.m_scanHeight * this.m_scanResolution, (float)this.m_scanDepth * this.m_scanResolution);
			this.m_previewFilter.mesh = Utils.CreateMesh(this.m_stampHM.Heights(), targetSize);
		}

		private bool LoadImageMask()
		{
			this.m_imageMaskHM = null;
			if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.None)
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
			else if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.PerlinNoise || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.RidgedNoise || this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.BillowNoise)
			{
				int num = 2048;
				int num2 = 2048;
				Terrain terrain = TerrainHelper.GetTerrain(base.transform.position);
				if (terrain == null)
				{
					terrain = Terrain.activeTerrain;
				}
				if (terrain != null)
				{
					num = terrain.terrainData.heightmapResolution;
					num2 = terrain.terrainData.heightmapResolution;
				}
				this.m_imageMaskHM = new HeightMap(num, num2);
				FractalGenerator fractalGenerator = new FractalGenerator();
				fractalGenerator.Seed = this.m_noiseMaskSeed;
				fractalGenerator.Octaves = this.m_noiseMaskOctaves;
				fractalGenerator.Persistence = this.m_noiseMaskPersistence;
				fractalGenerator.Frequency = this.m_noiseMaskFrequency;
				fractalGenerator.Lacunarity = this.m_noiseMaskLacunarity;
				if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.PerlinNoise)
				{
					fractalGenerator.FractalType = FractalGenerator.Fractals.Perlin;
				}
				else if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.RidgedNoise)
				{
					fractalGenerator.FractalType = FractalGenerator.Fractals.RidgeMulti;
				}
				else if (this.m_areaMaskMode == GaiaConstants.ImageFitnessFilterMode.BillowNoise)
				{
					fractalGenerator.FractalType = FractalGenerator.Fractals.Billow;
				}
				float num3 = 1f / this.m_noiseZoom;
				for (int k = 0; k < num; k++)
				{
					for (int l = 0; l < num2; l++)
					{
						this.m_imageMaskHM[k, l] = fractalGenerator.GetValue((float)k * num3, (float)l * num3);
					}
				}
			}
			else
			{
				Terrain terrain2 = TerrainHelper.GetTerrain(base.transform.position);
				if (terrain2 == null)
				{
					terrain2 = Terrain.activeTerrain;
				}
				if (terrain2 == null)
				{
					UnityEngine.Debug.LogError("You requested an terrain texture mask but there is no terrain.");
					return false;
				}
				switch (this.m_areaMaskMode)
				{
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture0:
					if (terrain2.terrainData.splatPrototypes.Length < 1)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 0 but there is no active texture in slot 0.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(terrain2.terrainData.GetAlphamaps(0, 0, terrain2.terrainData.alphamapWidth, terrain2.terrainData.alphamapHeight), 0);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture1:
					if (terrain2.terrainData.splatPrototypes.Length < 2)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 1 but there is no active texture in slot 1.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(terrain2.terrainData.GetAlphamaps(0, 0, terrain2.terrainData.alphamapWidth, terrain2.terrainData.alphamapHeight), 1);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture2:
					if (terrain2.terrainData.splatPrototypes.Length < 3)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 2 but there is no active texture in slot 2.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(terrain2.terrainData.GetAlphamaps(0, 0, terrain2.terrainData.alphamapWidth, terrain2.terrainData.alphamapHeight), 2);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture3:
					if (terrain2.terrainData.splatPrototypes.Length < 4)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 3 but there is no active texture in slot 3.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(terrain2.terrainData.GetAlphamaps(0, 0, terrain2.terrainData.alphamapWidth, terrain2.terrainData.alphamapHeight), 3);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture4:
					if (terrain2.terrainData.splatPrototypes.Length < 5)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 4 but there is no active texture in slot 4.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(terrain2.terrainData.GetAlphamaps(0, 0, terrain2.terrainData.alphamapWidth, terrain2.terrainData.alphamapHeight), 4);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture5:
					if (terrain2.terrainData.splatPrototypes.Length < 6)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 5 but there is no active texture in slot 5.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(terrain2.terrainData.GetAlphamaps(0, 0, terrain2.terrainData.alphamapWidth, terrain2.terrainData.alphamapHeight), 5);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture6:
					if (terrain2.terrainData.splatPrototypes.Length < 7)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 6 but there is no active texture in slot 6.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(terrain2.terrainData.GetAlphamaps(0, 0, terrain2.terrainData.alphamapWidth, terrain2.terrainData.alphamapHeight), 6);
					break;
				case GaiaConstants.ImageFitnessFilterMode.TerrainTexture7:
					if (terrain2.terrainData.splatPrototypes.Length < 8)
					{
						UnityEngine.Debug.LogError("You requested an terrain texture mask 7 but there is no active texture in slot 7.");
						return false;
					}
					this.m_imageMaskHM = new HeightMap(terrain2.terrainData.GetAlphamaps(0, 0, terrain2.terrainData.alphamapWidth, terrain2.terrainData.alphamapHeight), 7);
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

		private float CalculateHeight(float terrainHeight, float smHeightRaw, float smHeightAdj, float stencilHeightNU, float strength)
		{
			if (!this.m_drawStampBase && smHeightRaw < this.m_baseLevel)
			{
				return terrainHeight;
			}
			switch (this.m_stampOperation)
			{
			case GaiaConstants.FeatureOperation.RaiseHeight:
				if (smHeightAdj > terrainHeight)
				{
					float num = (smHeightAdj - terrainHeight) * strength;
					terrainHeight += num;
				}
				break;
			case GaiaConstants.FeatureOperation.LowerHeight:
				if (smHeightAdj < terrainHeight)
				{
					float num = (terrainHeight - smHeightAdj) * strength;
					terrainHeight -= num;
				}
				break;
			case GaiaConstants.FeatureOperation.BlendHeight:
			{
				float num2 = this.m_blendStrength * smHeightAdj + (1f - this.m_blendStrength) * terrainHeight;
				float num = (num2 - terrainHeight) * strength;
				terrainHeight += num;
				break;
			}
			case GaiaConstants.FeatureOperation.StencilHeight:
			{
				float num2 = terrainHeight + smHeightAdj * stencilHeightNU;
				float num = (num2 - terrainHeight) * strength;
				terrainHeight += num;
				break;
			}
			case GaiaConstants.FeatureOperation.DifferenceHeight:
			{
				float num2 = Mathf.Abs(smHeightAdj - terrainHeight);
				float num = (num2 - terrainHeight) * strength;
				terrainHeight += num;
				break;
			}
			}
			return terrainHeight;
		}

		private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angle)
		{
			Vector3 vector = point - pivot;
			vector = Quaternion.Euler(angle) * vector;
			point = vector + pivot;
			return point;
		}

		public string m_stampID = Guid.NewGuid().ToString();

		public Texture2D m_stampPreviewImage;

		public float m_x;

		public float m_y = 50f;

		public float m_z;

		public float m_width = 10f;

		public float m_height = 10f;

		public float m_rotation;

		public bool m_stickBaseToGround = true;

		[fsIgnore]
		public GaiaResource m_resources;

		[fsIgnore]
		public float m_seaLevel;

		public string m_resourcesPath;

		public bool m_invertStamp;

		public bool m_normaliseStamp;

		public float m_baseLevel;

		public bool m_drawStampBase = true;

		public GaiaConstants.FeatureOperation m_stampOperation;

		public int m_smoothIterations;

		public float m_blendStrength = 0.5f;

		public float m_stencilHeight = 1f;

		public AnimationCurve m_heightModifier = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		public AnimationCurve m_distanceMask = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		public GaiaConstants.ImageFitnessFilterMode m_areaMaskMode;

		public Texture2D m_imageMask;

		public bool m_imageMaskInvert;

		public bool m_imageMaskNormalise;

		public bool m_imageMaskFlip;

		public int m_imageMaskSmoothIterations = 3;

		[fsIgnore]
		public HeightMap m_imageMaskHM;

		public float m_noiseMaskSeed;

		public int m_noiseMaskOctaves = 8;

		public float m_noiseMaskPersistence = 0.25f;

		public float m_noiseMaskFrequency = 1f;

		public float m_noiseMaskLacunarity = 1.5f;

		public float m_noiseZoom = 10f;

		public bool m_alwaysShow;

		public bool m_showBase = true;

		public bool m_showSeaLevel = true;

		public bool m_showRulers;

		public bool m_showTerrainHelper;

		[fsIgnore]
		public Color m_gizmoColour = new Color(1f, 0.6f, 0f, 1f);

		[fsIgnore]
		public IEnumerator m_updateCoroutine;

		[fsIgnore]
		public float m_updateTimeAllowed = 0.0333333351f;

		[fsIgnore]
		public float m_stampProgress;

		[fsIgnore]
		public bool m_stampComplete = true;

		[fsIgnore]
		public bool m_cancelStamp;

		[fsIgnore]
		public Material m_previewMaterial;

		private int m_featureID;

		private int m_scanWidth;

		private int m_scanDepth;

		private int m_scanHeight;

		private float m_scanResolution = 0.1f;

		private Bounds m_scanBounds;

		private UnityHeightMap m_stampHM;

		private GaiaWorldManager m_undoMgr;

		private GaiaWorldManager m_redoMgr;

		private MeshFilter m_previewFilter;

		private MeshRenderer m_previewRenderer;
	}
}
