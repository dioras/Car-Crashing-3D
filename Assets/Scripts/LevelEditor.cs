using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LevelEditor : MonoBehaviour
{
	public Terrain terrain
	{
		get
		{
			if (this._cachedTerrain == null)
			{
				this._cachedTerrain = Terrain.activeTerrain;
			}
			return this._cachedTerrain;
		}
	}

	public TerrainData terData
	{
		get
		{
			if (this._cachedTerData == null)
			{
				this._cachedTerData = this.terrain.terrainData;
			}
			return this._cachedTerData;
		}
	}

	public TerrainCollider terCollider
	{
		get
		{
			if (this._cachedTerCollider == null)
			{
				this._cachedTerCollider = this.terrain.GetComponent<TerrainCollider>();
			}
			return this._cachedTerCollider;
		}
	}

	private int heightmapWidth
	{
		get
		{
			return this.terData.heightmapResolution;
		}
	}

	private int alphamapWidth
	{
		get
		{
			return this.terData.alphamapWidth;
		}
	}

	public Transform SelectedCheckpoint
	{
		get
		{
			if (this.selectedRouteID < 0)
			{
				return null;
			}
			if (this.selectedCheckpointID < 0)
			{
				return null;
			}
			return this.routes[this.selectedRouteID].checkpoints[this.selectedCheckpointID];
		}
	}

	public MudStamp SelectedMudStamp
	{
		get
		{
			if (this.selectedMudStampID >= 0)
			{
				return this.mudStamps[this.selectedMudStampID];
			}
			return null;
		}
	}

	private void Awake()
	{
		if (LevelEditor.Instance == null)
		{
			LevelEditor.Instance = this;
		}
		this.editorResources = LevelEditorTools.editorResources;
	}

	private void Start()
	{
		this.usedExtraObjects = new ExtraObjectReference[3];
		for (int i = 0; i < this.usedExtraObjects.Length; i++)
		{
			this.usedExtraObjects[i] = new ExtraObjectReference();
			this.usedExtraObjects[i].arrayID = -1;
		}
		this.ui = base.GetComponent<LevelEditorUI>();
		this.stampSize = 35f;
		this.modStrength = 0.5f;
		this.stampTextureID = 0;
		this.stampRotation = 0f;
		this.InitializeLineRenderer();
		this.InitializeCamera();
		this.UpdateStampProjector();
		this.ChangeLevelCreationStep(LevelCreationStep.None);
		this.ChangeModType(TerrainModifyingType.Stamp);
	}

	private void Update()
	{
		this.DoInput();
		this.DoCamera();
		if (this.drawingPath)
		{
			this.DrawPath();
		}
		if (this.movingProp)
		{
			this.MoveProp();
		}
		if (this.sizingProp)
		{
			this.SizeProp();
		}
		if (this.movingStamp)
		{
			this.MoveStamp();
		}
		if (this.sizingStamp)
		{
			this.SizeStamp();
		}
		if (this.liftingProp)
		{
			this.LiftProp();
		}
		if (this.movingCheckpoint)
		{
			this.MoveCheckpoint();
		}
		if (this.movingMudStamp)
		{
			this.MoveMudStamp();
		}
		if (this.sizingMudStamp)
		{
			this.SizeMudStamp();
		}
	}

	private void ResetTerrain()
	{
		LevelEditorTools.ResetTerrain(this.terrain);
	}

	[ContextMenu("Upload 100 maps")]
	private void Upload100Maps()
	{
		LevelBuilder component = base.GetComponent<LevelBuilder>();
		for (int i = 0; i < 100; i++)
		{
			component.UploadLevel(this, null, null);
		}
	}

	public void UploadMap()
	{
		LevelBuilder component = base.GetComponent<LevelBuilder>();
		if (component == null)
		{
			UnityEngine.Debug.LogError("Attach LevelBuilder to LevelEditor object!");
			return;
		}
		component.UploadLevel(this, new Action(this.LevelUploaded), new Action(this.LevelUploadFailed));
	}

	public void RemoveMap()
	{
		LevelBuilder component = base.GetComponent<LevelBuilder>();
		if (component == null)
		{
			UnityEngine.Debug.LogError("Attach LevelBuilder to LevelEditor object!");
			return;
		}
		component.RemoveLevel(this, new Action(this.LevelRemoved), new Action(this.LevelRemovalFailed));
	}

	public void LevelRemoved()
	{
		if (this.OnLevelRemovalFinished != null)
		{
			this.OnLevelRemovalFinished(false);
		}
		this.mapUploaded = false;
		LevelEditorTools.RemoveFromMyMaps(this.mapID);
		this.ChangeLevelCreationStep(LevelCreationStep.None);
	}

	public void LevelRemovalFailed()
	{
		if (this.OnLevelRemovalFinished != null)
		{
			this.OnLevelRemovalFinished(true);
		}
		this.ChangeLevelCreationStep(LevelCreationStep.None);
	}

	public void LevelUploaded()
	{
		if (this.OnLevelUploadFinished != null)
		{
			this.OnLevelUploadFinished(false);
		}
		this.mapUploaded = true;
		LevelEditorTools.AddMapToMyMaps(this.mapID);
		this.ChangeLevelCreationStep(LevelCreationStep.None);
	}

	public void LevelUploadFailed()
	{
		if (this.OnLevelUploadFinished != null)
		{
			this.OnLevelUploadFinished(true);
		}
		this.ChangeLevelCreationStep(LevelCreationStep.None);
	}

	public void ChangeLevelCreationStep(LevelCreationStep newStep)
	{
		if (this.levelCreationStep == LevelCreationStep.Modifying)
		{
			this.RemoveStamp();
			this.RemovePath();
		}
		this.stampSize = 35f;
		this.stampProjector.orthographicSize = 35f;
		this.ClearPreviewCache();
		this.CheckRoutesLenghts();
		this.ToggleStampProjector(false);
		this.levelCreationStep = newStep;
		this.SelectRoute(-1);
		this.SelectMudStamp(-1);
		this.ChangePropState(PropPlacementState.NotSelected);
		if (this.defHeights == null)
		{
			this.defHeights = this.terData.GetHeights(0, 0, this.heightmapWidth, this.heightmapWidth);
		}
		if (this.OnCreationStepChanged != null)
		{
			this.OnCreationStepChanged();
		}
	}

	public Transform[] GetSpawnPoints()
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < LevelEditorTools.PropsParent.childCount; i++)
		{
			if (LevelEditorTools.PropsParent.GetChild(i).name.Contains("SpawnPoint"))
			{
				list.Add(LevelEditorTools.PropsParent.GetChild(i));
			}
		}
		return list.ToArray();
	}

	public int CountProps()
	{
		int num = 0;
		for (int i = 0; i < LevelEditorTools.PropsParent.childCount; i++)
		{
			if (!LevelEditorTools.PropsParent.GetChild(i).name.Contains("SpawnPoint"))
			{
				num++;
			}
		}
		return num;
	}

	public void PreBakeProps()
	{
		foreach (Prop prop in LevelEditorTools.PropsParent.GetComponentsInChildren<Prop>())
		{
			prop.PreBakeProp();
		}
	}

	public void CancelPreBakeProps()
	{
		foreach (Prop prop in LevelEditorTools.PropsParent.GetComponentsInChildren<Prop>())
		{
			prop.CancelPreBake();
		}
	}

	public void CacheSplatMaps()
	{
		this.tempSplatMap = this.terData.GetAlphamaps(0, 0, this.alphamapWidth, this.alphamapWidth);
		this.splatCached = true;
	}

	public void RestoreSplatMaps()
	{
		if (!this.splatCached)
		{
			return;
		}
		this.terData.SetAlphamaps(0, 0, this.tempSplatMap);
		this.splatCached = false;
	}

	private void DoInput()
	{
		if (CrossPlatformInputManager.GetButtonDown("MoveStamp"))
		{
			this.movingStamp = true;
			this.CatchLastRaycastPos();
		}
		if (CrossPlatformInputManager.GetButtonUp("MoveStamp"))
		{
			this.movingStamp = false;
		}
		if (CrossPlatformInputManager.GetButtonDown("SizeStamp"))
		{
			this.sizingStamp = true;
			this.CatchLastRaycastPos();
			this.stampLookPos = this.stampPosition + this.stampProjector.transform.up * this.stampProjector.orthographicSize;
		}
		if (CrossPlatformInputManager.GetButtonUp("SizeStamp"))
		{
			this.sizingStamp = false;
		}
		if (CrossPlatformInputManager.GetButtonUp("RemoveStamp"))
		{
			this.RemoveStamp();
		}
		if (CrossPlatformInputManager.GetButtonUp("RemovePath"))
		{
			this.RemovePath();
		}
		if (CrossPlatformInputManager.GetButtonDown("MoveProp"))
		{
			this.movingProp = true;
			this.CatchLastRaycastPos();
		}
		if (CrossPlatformInputManager.GetButtonUp("MoveProp"))
		{
			this.movingProp = false;
		}
		if (CrossPlatformInputManager.GetButtonDown("SizeProp"))
		{
			this.sizingProp = true;
			this.placedPropLookPos = this.PlacedProp.transform.position + this.PlacedProp.transform.forward * this.ui.currentCircleDrawerRadius * this.PlacedProp.transform.localScale.x * this.PlacedProp.circleDrawerSizeMultiplier;
			this.CatchLastRaycastPos();
		}
		if (CrossPlatformInputManager.GetButtonUp("SizeProp"))
		{
			this.sizingProp = false;
		}
		if (CrossPlatformInputManager.GetButtonDown("LiftProp"))
		{
			this.liftingProp = true;
			this.CatchLastRaycastPos();
		}
		if (CrossPlatformInputManager.GetButtonUp("LiftProp"))
		{
			this.liftingProp = false;
		}
		if (CrossPlatformInputManager.GetButtonUp("ApplyCheckpoint"))
		{
			this.ApplyCheckpoint();
		}
		if (CrossPlatformInputManager.GetButtonUp("RemoveCheckpoint"))
		{
			this.RemoveCheckpoint();
		}
		if (CrossPlatformInputManager.GetButtonDown("MoveCheckpoint"))
		{
			this.movingCheckpoint = true;
			this.CatchLastRaycastPos();
		}
		if (CrossPlatformInputManager.GetButtonUp("MoveCheckpoint"))
		{
			this.movingCheckpoint = false;
		}
		if (CrossPlatformInputManager.GetButtonDown("MoveMudStamp"))
		{
			this.movingMudStamp = true;
			this.CatchLastRaycastPos();
		}
		if (CrossPlatformInputManager.GetButtonUp("MoveMudStamp"))
		{
			this.movingMudStamp = false;
		}
		if (CrossPlatformInputManager.GetButtonDown("SizeMudStamp"))
		{
			this.sizingMudStamp = true;
			this.CatchLastRaycastPos();
			this.mudStampLookPos = this.SelectedMudStamp.stampPosition + this.SelectedMudStamp.stampIndicator.transform.forward * this.SelectedMudStamp.stampSize;
		}
		if (CrossPlatformInputManager.GetButtonUp("SizeMudStamp"))
		{
			this.sizingMudStamp = false;
		}
		if (CrossPlatformInputManager.GetButtonUp("RemoveMudStamp"))
		{
			this.RemoveMudStamp();
		}
		if (CrossPlatformInputManager.GetButtonUp("ApplyMudStamp"))
		{
			this.ApplyMudStamp();
		}
		this.drawingPath = (this.levelCreationStep == LevelCreationStep.Modifying && this.terrainModType == TerrainModifyingType.Path && this.pathState == PathState.Drawing && this.draggingScreen);
	}

	private void CatchLastRaycastPos()
	{
		if (UnityEngine.Input.touchCount == 1)
		{
			this.lastTouchPos = UnityEngine.Input.GetTouch(0).position;
			Vector3 position = UnityEngine.Input.GetTouch(0).position;
			Ray ray = Camera.main.ScreenPointToRay(position);
			RaycastHit raycastHit;
			if (this.terCollider.Raycast(ray, out raycastHit, 10000f))
			{
				this.lastRaycastPos = raycastHit.point;
			}
		}
	}

	public void OnTouchTap(Vector3 pos, bool fingerMoved, bool doubleTap = false)
	{
		Ray ray = Camera.main.ScreenPointToRay(pos);
		switch (this.levelCreationStep)
		{
		case LevelCreationStep.Modifying:
			if (this.terrainModType == TerrainModifyingType.Stamp)
			{
				RaycastHit raycastHit;
				if (!fingerMoved && this.stampState == StampState.NotPlaced && this.terCollider.Raycast(ray, out raycastHit, 10000f))
				{
					this.PlaceStamp(raycastHit.point);
					return;
				}
			}
			else
			{
				if (this.pathState == PathState.NotDrawn && !fingerMoved)
				{
					this.ChangePathState(PathState.Drawing);
					return;
				}
				if (this.pathState == PathState.Drawing)
				{
					if (this.pathPositions.Count > 2)
					{
						this.ChangePathState(PathState.FinishedDrawing);
					}
					else
					{
						this.RemovePath();
					}
					return;
				}
			}
			break;
		case LevelCreationStep.PlacingObjects:
			if (!fingerMoved)
			{
				RaycastHit raycastHit;
				if (this.propState == PropPlacementState.Selected && this.PlacedProp == null && Physics.Raycast(ray, out raycastHit))
				{
					this.PlaceProp(raycastHit.point);
					this.PlacedProp.ToggleExtra0(false);
					this.PlacedProp.ToggleExtra1(false);
					this.ChangePropState(PropPlacementState.Placed);
					return;
				}
				RaycastHit[] array = Physics.RaycastAll(ray);
				foreach (RaycastHit raycastHit2 in array)
				{
					Prop componentInParent = raycastHit2.collider.GetComponentInParent<Prop>();
					if (componentInParent != null)
					{
						if (componentInParent.transform.parent == LevelEditorTools.ExtraObjectsParent)
						{
							ExtraObject component = componentInParent.GetComponent<ExtraObject>();
							this.ignoredExtraObjectIDs.Add(component.ID);
							componentInParent.transform.parent = LevelEditorTools.PropsParent;
							UnityEngine.Object.Destroy(component);
						}
						this.ChangeLevelCreationStep(LevelCreationStep.PlacingObjects);
						this.CatchProp(componentInParent);
						return;
					}
				}
			}
			break;
		case LevelCreationStep.PlacingRoutes:
			if (this.routeCreationStep == RouteCreationStep.Selected && !fingerMoved)
			{
				RaycastHit raycastHit;
				if (doubleTap)
				{
					if (Physics.Raycast(ray, out raycastHit))
					{
						this.AddRouteWaypoint(raycastHit.point);
						return;
					}
				}
				else if (Physics.Raycast(ray, out raycastHit))
				{
					PlayerRouteCheckpoint componentInParent2 = raycastHit.collider.GetComponentInParent<PlayerRouteCheckpoint>();
					if (componentInParent2 != null)
					{
						this.SelectCheckpoint(componentInParent2.checkpointID);
						return;
					}
				}
			}
			break;
		case LevelCreationStep.AddingMud:
			if (!fingerMoved)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.transform.parent == LevelEditorTools.MudStampsParent)
				{
					for (int j = 0; j < this.mudStamps.Count; j++)
					{
						if (this.mudStamps[j].stampIndicator == raycastHit.collider.gameObject && this.selectedMudStampID != j)
						{
							this.SelectMudStamp(j);
							return;
						}
					}
				}
				if (doubleTap && this.terCollider.Raycast(ray, out raycastHit, 10000f))
				{
					this.AddMudStamp(raycastHit.point);
					return;
				}
			}
			break;
		}
		if (!fingerMoved)
		{
			if (this.levelCreationStep == LevelCreationStep.PlacingRoutes && this.routeCreationStep == RouteCreationStep.Selected)
			{
				return;
			}
			RaycastHit[] array = Physics.RaycastAll(ray);
			foreach (RaycastHit raycastHit3 in array)
			{
				Prop componentInParent3 = raycastHit3.collider.GetComponentInParent<Prop>();
				if (componentInParent3 != null)
				{
					if (componentInParent3.transform.parent == LevelEditorTools.ExtraObjectsParent)
					{
						ExtraObject component2 = componentInParent3.GetComponent<ExtraObject>();
						this.ignoredExtraObjectIDs.Add(component2.ID);
						componentInParent3.transform.parent = LevelEditorTools.PropsParent;
						UnityEngine.Object.Destroy(component2);
					}
					this.ChangeLevelCreationStep(LevelCreationStep.PlacingObjects);
					this.CatchProp(componentInParent3);
					return;
				}
			}
			foreach (RaycastHit raycastHit4 in array)
			{
				if (raycastHit4.collider.transform.parent == LevelEditorTools.MudStampsParent)
				{
					for (int m = 0; m < this.mudStamps.Count; m++)
					{
						if (this.mudStamps[m].stampIndicator == raycastHit4.collider.gameObject)
						{
							this.ChangeLevelCreationStep(LevelCreationStep.AddingMud);
							this.SelectMudStamp(m);
							return;
						}
					}
				}
			}
		}
	}

	private void CatchProp(Prop prop)
	{
		this.PlacedProp = prop;
		this.PlacedProp.Highlight(true);
		this.selectedPropID = prop.propID;
		if (this.OnSelectedPropChanged != null)
		{
			this.OnSelectedPropChanged();
		}
		this.ChangePropState(PropPlacementState.Placed);
	}

	private void MoveMudStamp()
	{
		Vector3 vector = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
			Vector3 b = vector - this.lastTouchPos;
			this.movingMudStampButton.transform.position += b;
			this.lastTouchPos = vector;
		}
		if (vector != Vector3.zero)
		{
			Ray ray = Camera.main.ScreenPointToRay(vector);
			RaycastHit raycastHit;
			if (this.terCollider.Raycast(ray, out raycastHit, 10000f))
			{
				Vector3 b2 = raycastHit.point - this.lastRaycastPos;
				b2.y = 0f;
				Vector3 vector2 = this.SelectedMudStamp.stampIndicator.transform.position + b2;
				vector2 = this.FilterMudStampPosition(vector2, this.selectedMudStampID);
				this.SelectedMudStamp.stampIndicator.transform.position = vector2;
				this.SelectedMudStamp.stampPosition = vector2;
				this.lastRaycastPos = raycastHit.point;
			}
		}
	}

	private void SizeMudStamp()
	{
		Vector3 vector = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
			Vector3 b = vector - this.lastTouchPos;
			this.sizingMudStampButton.transform.position += b;
			this.lastTouchPos = vector;
		}
		if (vector != Vector3.zero)
		{
			Ray ray = Camera.main.ScreenPointToRay(vector);
			RaycastHit raycastHit;
			if (this.terCollider.Raycast(ray, out raycastHit, 10000f))
			{
				Vector3 b2 = raycastHit.point - this.lastRaycastPos;
				b2.y = 0f;
				this.mudStampLookPos += b2;
				this.lastRaycastPos = raycastHit.point;
				this.mudStampLookPos.y = this.SelectedMudStamp.stampPosition.y;
				Vector3 forward = this.mudStampLookPos - this.SelectedMudStamp.stampPosition;
				this.SelectedMudStamp.stampIndicator.transform.rotation = Quaternion.LookRotation(forward);
				this.SelectedMudStamp.stampRotation = this.SelectedMudStamp.stampIndicator.transform.eulerAngles.y;
				float num = Vector3.Distance(this.SelectedMudStamp.stampPosition, this.mudStampLookPos);
				num = Mathf.Clamp(num, this.minMudStampSize, this.maxMudStampSize);
				this.SelectedMudStamp.stampIndicator.transform.localScale = Vector3.one * num / 5f;
				this.SelectedMudStamp.stampSize = num;
				Vector3 vector2 = this.SelectedMudStamp.stampPosition;
				vector2 = this.FilterMudStampPosition(vector2, this.selectedMudStampID);
				this.SelectedMudStamp.stampPosition = vector2;
				this.SelectedMudStamp.stampIndicator.transform.position = vector2;
			}
		}
	}

	private void AddMudStamp(Vector3 pos)
	{
		if (this.mudStamps.Count >= 3)
		{
			return;
		}
		MudStamp mudStamp = new MudStamp();
		mudStamp.stampPosition = pos;
		mudStamp.stampRotation = 0f;
		mudStamp.stampSize = 20f;
		mudStamp.stampTextureID = 0;
		mudStamp.mudDepth = 0.3f;
		mudStamp.mudViscosity = 3f;
		mudStamp.stampIndicator = LevelEditorTools.CreateMudIndicator(mudStamp, this.terrain);
		this.mudStamps.Add(mudStamp);
		this.SelectMudStamp(this.mudStamps.Count - 1);
		pos = this.FilterMudStampPosition(pos, this.mudStamps.Count - 1);
		mudStamp.stampIndicator.transform.position = pos;
	}

	public void SelectMudStamp(int id)
	{
		this.selectedMudStampID = id;
		for (int i = 0; i < this.mudStamps.Count; i++)
		{
			this.mudStamps[i].stampIndicator.GetComponent<Renderer>().material.SetColor("_BaseColor", (i != this.selectedMudStampID) ? this.deselectedMudStampColor : this.selectedMudStampColor);
		}
		this.mudStampState = ((id != -1) ? MudStampState.Selected : MudStampState.NotSelected);
		if (this.OnSelectedMudStampChanged != null)
		{
			this.OnSelectedMudStampChanged();
		}
	}

	public void ChangeMudStampPattern(int newPatternID)
	{
		if (this.SelectedMudStamp == null)
		{
			return;
		}
		this.SelectedMudStamp.stampTextureID = newPatternID;
		Material material = this.SelectedMudStamp.stampIndicator.GetComponent<MeshRenderer>().material;
		material.SetTexture("_MainTex", this.editorResources.mudStampTextures[newPatternID]);
	}

	public void ChangeMudDepth(float depth)
	{
		if (this.SelectedMudStamp == null)
		{
			return;
		}
		this.SelectedMudStamp.mudDepth = depth;
	}

	public void ChangeMudViscosity(float viscosity)
	{
		if (this.SelectedMudStamp == null)
		{
			return;
		}
		this.SelectedMudStamp.mudViscosity = viscosity;
	}

	public void RemoveAllMudStamps()
	{
		this.mudStamps.Clear();
		UnityEngine.Object.DestroyImmediate(LevelEditorTools.MudStampsParent.gameObject);
		this.SelectMudStamp(-1);
	}

	public void ApplyMudStamp()
	{
		this.SelectMudStamp(-1);
	}

	public void RemoveMudStamp()
	{
		UnityEngine.Object.DestroyImmediate(this.SelectedMudStamp.stampIndicator);
		this.mudStamps.RemoveAt(this.selectedMudStampID);
		this.SelectMudStamp(-1);
	}

	public Vector3 FilterMudStampPosition(Vector3 posAttempt, int mudStampID)
	{
		for (int i = 0; i < this.mudStamps.Count; i++)
		{
			if (i != mudStampID)
			{
				float num = this.mudStamps[i].boundsRadius + this.mudStamps[mudStampID].boundsRadius;
				float num2 = Vector3.Distance(this.mudStamps[i].stampPosition, posAttempt);
				if (num2 < num)
				{
					Vector3 vector = posAttempt - this.mudStamps[i].stampPosition;
					posAttempt = this.mudStamps[i].stampPosition + vector.normalized * num;
				}
			}
		}
		posAttempt.y = this.terrain.SampleHeight(posAttempt);
		return posAttempt;
	}

	private void InitializeLineRenderer()
	{
		this.lineRenderer = new GameObject("Line renderer").AddComponent<LineRenderer>();
		this.lineRenderer.useWorldSpace = true;
		this.lineRenderer.material = this.pathMaterial;
		this.lineRenderer.alignment = LineAlignment.TransformZ;
		this.lineRenderer.widthMultiplier = this.pathWidth;
		this.lineRenderer.positionCount = 0;
		this.lineRenderer.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		this.lineRenderer.numCornerVertices = 5;
	}

	private void DrawPath()
	{
		Vector3 vector = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
		}
		if (vector == Vector3.zero)
		{
			return;
		}
		bool flag = this.pathPositions.Count == 0;
		Ray ray = this.mainCamera.ScreenPointToRay(vector);
		RaycastHit raycastHit;
		if (this.terCollider.Raycast(ray, out raycastHit, 10000f))
		{
			this.tempPathPoint = raycastHit.point;
			if (flag)
			{
				this.pathPositions.Add(this.tempPathPoint);
				this.pathPositions.Add(this.tempPathPoint);
				this.lastPathPoint = this.tempPathPoint;
				this.lineRenderer.positionCount = 2;
				this.lineRenderer.SetPosition(this.lineRenderer.positionCount - 2, this.tempPathPoint);
			}
			else
			{
				this.tempPathPoint.y = this.pathPositions[0].y;
				float num = Vector3.Distance(this.tempPathPoint, this.lastPathPoint);
				if (num > this.pathWaypointsDistance)
				{
					this.AddPathPoint(this.tempPathPoint);
				}
			}
			this.lineRenderer.SetPosition(this.lineRenderer.positionCount - 1, this.tempPathPoint);
		}
		if (this.pathPositions.Count > this.maxPathWaypoints)
		{
			this.ChangePathState(PathState.FinishedDrawing);
		}
	}

	private void AddPathPoint(Vector3 point)
	{
		if (this.pathPositions.Count > 0)
		{
			point.y = this.pathPositions[0].y;
		}
		this.pathPositions.Add(point);
		this.lastPathPoint = point;
		this.lineRenderer.positionCount++;
		this.lineRenderer.SetPosition(this.lineRenderer.positionCount - 2, point);
	}

	private void ChangePathState(PathState newState)
	{
		this.pathState = newState;
		if (this.OnPathStateChanged != null)
		{
			this.OnPathStateChanged();
		}
	}

	private void RemovePath()
	{
		this.CancelModPreviewChanges();
		this.pathPositions.Clear();
		this.lineRenderer.positionCount = 0;
		this.ChangePathState(PathState.NotDrawn);
	}

	public void UpdatePathSettings()
	{
		this.lineRenderer.widthMultiplier = this.pathWidth;
		this.lineRenderer.material.SetTexture("_MainTex", this.editorResources.pathPatterns[this.selectedPathPattern]);
	}

	public void PreviewPath()
	{
		base.StartCoroutine(this.PreviewPathCor());
	}

	private IEnumerator PreviewPathCor()
	{
		if (this.OnProcessingTerrainStarted != null)
		{
			this.OnProcessingTerrainStarted("Building preview...");
		}
		yield return null;
		this.CancelModPreviewChanges();
		if (this.heightsBeforePreview == null)
		{
			this.heightsBeforePreview = this.terData.GetHeights(0, 0, this.heightmapWidth, this.heightmapWidth);
		}
		if (this.splatBeforePreview == null)
		{
			this.splatBeforePreview = this.terData.GetAlphamaps(0, 0, this.alphamapWidth, this.alphamapWidth);
		}
		this.ApplyPath(true);
		yield return null;
		if (this.OnProcessingTerrainFinished != null)
		{
			this.OnProcessingTerrainFinished();
		}
		yield break;
	}

	public void ApplyAndSavePath()
	{
		base.StartCoroutine(this.ApplyAndSavePathCor());
	}

	private IEnumerator ApplyAndSavePathCor()
	{
		if (this.OnProcessingTerrainStarted != null)
		{
			this.OnProcessingTerrainStarted("Applying path...");
		}
		yield return null;
		this.CancelModPreviewChanges();
		this.ApplyPath(false);
		this.RemovePath();
		yield return null;
		if (this.OnProcessingTerrainFinished != null)
		{
			this.OnProcessingTerrainFinished();
		}
		yield break;
	}

	private void ApplyPath(bool previewOnly)
	{
		TerrainPath terrainPath = new TerrainPath();
		terrainPath.pathAction = this.modAction;
		terrainPath.pathPositions = new List<Vector3>(this.pathPositions);
		terrainPath.pathStrength = this.modStrength;
		terrainPath.pathWidth = this.pathWidth;
		terrainPath.pathPattern = this.selectedPathPattern;
		if (this.modAction == ModAction.Painting)
		{
			terrainPath.extraInt = this.modPaintTextureID;
		}
		if (this.modAction == ModAction.AddingExtraObjects)
		{
			terrainPath.extraInt = this.addingExtraObjectID;
		}
		if (!previewOnly)
		{
			this.paths.Add(terrainPath);
		}
		switch (this.modAction)
		{
		case ModAction.LandscapeRaising:
			this.ApplyHeightPath(terrainPath);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			this.CorrectCliffsPositions();
			break;
		case ModAction.LandscapeLowering:
			this.ApplyHeightPath(terrainPath);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			this.CorrectCliffsPositions();
			break;
		case ModAction.Smoothing:
			this.ApplySmoothPath(terrainPath);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			this.CorrectCliffsPositions();
			break;
		case ModAction.Painting:
			this.ApplyPaintPath(terrainPath);
			this.PaintRockAndWaterTextures();
			break;
		case ModAction.AddingExtraObjects:
			this.lastPlacedExtraObjectID = this.ApplyAddExtraObjectsPath(terrainPath, ref this.treesAddedForPreview, ref this.extraObjectsAddedForPreview);
			break;
		case ModAction.RemovingExtraObjects:
			this.ApplyRemovingExtraObjectsPath(terrainPath, ref this.treesRemovedForPreview, ref this.extraObjectsRemovedForPreview, previewOnly);
			break;
		}
		if (!previewOnly)
		{
			this.ClearPreviewCache();
		}
	}

	private void SizeStamp()
	{
		Vector3 vector = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
			Vector3 b = vector - this.lastTouchPos;
			this.sizingButton.transform.position += b;
			this.lastTouchPos = vector;
		}
		if (vector != Vector3.zero)
		{
			Ray ray = Camera.main.ScreenPointToRay(vector);
			RaycastHit raycastHit;
			if (this.terCollider.Raycast(ray, out raycastHit, 10000f))
			{
				Vector3 b2 = raycastHit.point - this.lastRaycastPos;
				b2.y = 0f;
				this.stampLookPos += b2;
				this.lastRaycastPos = raycastHit.point;
				Vector3 upwards = this.stampLookPos - this.stampPosition;
				this.stampProjector.transform.rotation = Quaternion.LookRotation(Vector3.down, upwards);
				this.stampRotation = this.stampProjector.transform.eulerAngles.y;
				float orthographicSize = Vector3.Distance(this.stampPosition, this.stampLookPos);
				this.stampProjector.orthographicSize = orthographicSize;
				this.stampSize = orthographicSize;
			}
		}
	}

	private void MoveStamp()
	{
		Vector3 vector = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
			Vector3 b = vector - this.lastTouchPos;
			this.movingButton.transform.position += b;
			this.lastTouchPos = vector;
		}
		if (vector != Vector3.zero)
		{
			Ray ray = Camera.main.ScreenPointToRay(vector);
			RaycastHit raycastHit;
			if (this.terCollider.Raycast(ray, out raycastHit, 10000f))
			{
				Vector3 b2 = raycastHit.point - this.lastRaycastPos;
				b2.y = 0f;
				Vector3 pos = this.stampPosition + b2;
				this.PlaceStamp(pos);
				this.lastRaycastPos = raycastHit.point;
			}
		}
	}

	public void ChangeModType(TerrainModifyingType newType)
	{
		this.terrainModType = newType;
		this.RemoveStamp();
		this.RemovePath();
		if (this.OnModTypeChanged != null)
		{
			this.OnModTypeChanged();
		}
	}

	private void ChangeStampState(StampState newState)
	{
		this.stampState = newState;
		this.ToggleStampProjector(this.stampState == StampState.Placed);
		this.UpdateStampProjector();
		if (this.OnStampStateChanged != null)
		{
			this.OnStampStateChanged();
		}
	}

	public void UpdateStampProjector()
	{
		this.stampProjector.material.SetTexture("_ShadowTex", this.editorResources.stampTextures[this.stampTextureID]);
	}

	private void ToggleStampProjector(bool enable)
	{
		this.stampProjector.gameObject.SetActive(enable);
	}

	private void PlaceStamp(Vector3 pos)
	{
		this.stampPosition = pos;
		this.stampProjector.transform.position = this.stampPosition + Vector3.up * 20f;
		this.ChangeStampState(StampState.Placed);
	}

	public void RemoveStamp()
	{
		this.ChangeStampState(StampState.NotPlaced);
		this.CancelModPreviewChanges();
	}

	public void ChangeModAction(ModAction newAction)
	{
		this.modAction = newAction;
		if (this.OnModActionChanged != null)
		{
			this.OnModActionChanged();
		}
	}

	public void ChangeModPaintTextureID(int newID)
	{
		this.modPaintTextureID = newID;
	}

	public void ApplyAndSaveStamp()
	{
		base.StartCoroutine(this.ApplyAndSaveStampCor());
	}

	private IEnumerator ApplyAndSaveStampCor()
	{
		if (this.OnProcessingTerrainStarted != null)
		{
			this.OnProcessingTerrainStarted("Applying stamp...");
		}
		yield return null;
		this.RemoveStamp();
		this.ApplyStamp(false);
		yield return null;
		if (this.OnProcessingTerrainFinished != null)
		{
			this.OnProcessingTerrainFinished();
		}
		yield break;
	}

	public void PreviewStamp()
	{
		base.StartCoroutine(this.PreviewStampCor());
	}

	private IEnumerator PreviewStampCor()
	{
		if (this.OnProcessingTerrainStarted != null)
		{
			this.OnProcessingTerrainStarted("Building preview...");
		}
		yield return null;
		this.CancelModPreviewChanges();
		if (this.heightsBeforePreview == null)
		{
			this.heightsBeforePreview = this.terData.GetHeights(0, 0, this.heightmapWidth, this.heightmapWidth);
		}
		if (this.splatBeforePreview == null)
		{
			this.splatBeforePreview = this.terData.GetAlphamaps(0, 0, this.alphamapWidth, this.alphamapWidth);
		}
		this.ApplyStamp(true);
		yield return null;
		if (this.OnProcessingTerrainFinished != null)
		{
			this.OnProcessingTerrainFinished();
		}
		yield break;
	}

	public void CancelModPreviewChanges()
	{
		if (this.heightsBeforePreview != null)
		{
			this.terData.SetHeights(0, 0, this.heightsBeforePreview);
		}
		if (this.splatBeforePreview != null)
		{
			this.terData.SetAlphamaps(0, 0, this.splatBeforePreview);
		}
		if (this.extraObjectsAddedForPreview.Count > 0)
		{
			this.lastPlacedExtraObjectID -= this.extraObjectsAddedForPreview.Count;
			foreach (GameObject obj in this.extraObjectsAddedForPreview)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
		if (this.extraObjectsRemovedForPreview.Count > 0)
		{
			foreach (GameObject gameObject in this.extraObjectsRemovedForPreview)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(true);
				}
			}
		}
		List<TreeInstance> list = new List<TreeInstance>(this.terData.treeInstances);
		UnityEngine.Debug.Log("Trees:" + list.Count);
		UnityEngine.Debug.Log("Added trees:" + this.treesAddedForPreview.Count);
		if (this.treesAddedForPreview.Count > 0)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < this.treesAddedForPreview.Count; j++)
				{
					if (i < list.Count && i >= 0)
					{
						Vector3 position = list[i].position;
						position.y = 0f;
						Vector3 position2 = this.treesAddedForPreview[j].position;
						position2.y = 0f;
						if (Vector3.Distance(position, position2) < 0.001f)
						{
							list.RemoveAt(i);
							i--;
							UnityEngine.Debug.Log("Tree removed");
						}
					}
				}
			}
		}
		if (this.treesRemovedForPreview.Count > 0)
		{
			for (int k = 0; k < this.treesRemovedForPreview.Count; k++)
			{
				list.Add(this.treesRemovedForPreview[k]);
			}
		}
		this.terData.treeInstances = list.ToArray();
		this.treesAddedForPreview.Clear();
		this.treesRemovedForPreview.Clear();
		this.extraObjectsAddedForPreview.Clear();
		this.extraObjectsRemovedForPreview.Clear();
	}

	private void ClearPreviewCache()
	{
		this.heightsBeforePreview = null;
		this.splatBeforePreview = null;
		this.treesAddedForPreview.Clear();
		this.treesRemovedForPreview.Clear();
		this.extraObjectsAddedForPreview.Clear();
		this.extraObjectsRemovedForPreview.Clear();
	}

	private void ApplyStamp(bool previewOnly)
	{
		TerrainStamp terrainStamp = new TerrainStamp();
		terrainStamp.stampAction = this.modAction;
		terrainStamp.stampPosition = new Vector2(this.stampProjector.transform.position.x, this.stampProjector.transform.position.z);
		terrainStamp.stampTextureID = this.stampTextureID;
		terrainStamp.stampRotation = this.stampRotation;
		terrainStamp.stampSize = this.stampSize;
		terrainStamp.stampStrength = this.modStrength;
		if (this.modAction == ModAction.Painting)
		{
			terrainStamp.extraInt = this.modPaintTextureID;
		}
		if (this.modAction == ModAction.AddingExtraObjects)
		{
			terrainStamp.extraInt = this.addingExtraObjectID;
		}
		if (!previewOnly)
		{
			this.stamps.Add(terrainStamp);
			this.ClearPreviewCache();
		}
		switch (this.modAction)
		{
		case ModAction.LandscapeRaising:
			this.ApplyHeightStamp(terrainStamp);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			this.CorrectCliffsPositions();
			break;
		case ModAction.LandscapeLowering:
			this.ApplyHeightStamp(terrainStamp);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			this.CorrectCliffsPositions();
			break;
		case ModAction.Smoothing:
			this.ApplySmoothStamp(terrainStamp);
			this.PaintRockAndWaterTextures();
			this.CorrectExtraObjectsTransforms();
			this.CorrectCliffsPositions();
			break;
		case ModAction.Painting:
			this.ApplyPaintStamp(terrainStamp);
			this.PaintRockAndWaterTextures();
			break;
		case ModAction.AddingExtraObjects:
			this.lastPlacedExtraObjectID = this.ApplyAddExtraObjectsStamp(terrainStamp, ref this.treesAddedForPreview, ref this.extraObjectsAddedForPreview);
			break;
		case ModAction.RemovingExtraObjects:
			this.ApplyRemovingExtraObjectsStamp(terrainStamp, ref this.treesRemovedForPreview, ref this.extraObjectsRemovedForPreview, previewOnly);
			break;
		}
		if (!previewOnly)
		{
			this.ClearPreviewCache();
		}
	}

	private void LiftProp()
	{
		Vector3 vector = Vector3.zero;
		Vector3 b = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
			b = vector - this.lastTouchPos;
			this.liftingPropButton.transform.position += b;
			this.lastTouchPos = vector;
		}
		if (vector == Vector3.zero)
		{
			return;
		}
		this.PlacedProp.currentLift += b.magnitude * this.liftSensevitity * Mathf.Sign(b.y);
		this.PlacedProp.currentLift = Mathf.Clamp(this.PlacedProp.currentLift, this.PlacedProp.minLift, this.PlacedProp.maxLift);
		this.PlaceProp(this.PlacedProp.transform.position);
	}

	private void MoveProp()
	{
		Vector3 vector = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
			Vector3 b = vector - this.lastTouchPos;
			this.movingPropButton.transform.position += b;
			this.lastTouchPos = vector;
		}
		if (vector == Vector3.zero)
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(vector);
		RaycastHit raycastHit;
		if (this.terCollider.Raycast(ray, out raycastHit, 100000f))
		{
			Vector3 b2 = raycastHit.point - this.lastRaycastPos;
			Vector3 vector2 = this.PlacedProp.transform.position + b2;
			vector2.y = this.terrain.SampleHeight(vector2);
			this.PlaceProp(vector2);
			this.lastRaycastPos = raycastHit.point;
		}
	}

	private void SizeProp()
	{
		Vector3 vector = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
			Vector3 b = vector - this.lastTouchPos;
			this.sizingPropButton.transform.position += b;
			this.lastTouchPos = vector;
		}
		if (vector != Vector3.zero)
		{
			Ray ray = Camera.main.ScreenPointToRay(vector);
			RaycastHit raycastHit;
			if (this.terCollider.Raycast(ray, out raycastHit, 10000f))
			{
				PlacementSettings component = this.PlacedProp.GetComponent<PlacementSettings>();
				Vector3 b2 = raycastHit.point - this.lastRaycastPos;
				b2.y = 0f;
				this.placedPropLookPos += b2;
				this.lastRaycastPos = raycastHit.point;
				this.PlacedProp.transform.rotation = Quaternion.LookRotation(this.placedPropLookPos - this.PlacedProp.transform.position, this.PlacedProp.transform.up);
				float num = Vector3.Distance(this.placedPropLookPos, this.PlacedProp.transform.position);
				float num2 = num / this.ui.currentCircleDrawerRadius / this.PlacedProp.circleDrawerSizeMultiplier;
				num2 = Mathf.Clamp(num2, this.PlacedProp.minScale, this.PlacedProp.maxScale);
				float num3 = num2 / this.PlacedProp.defaultScale;
				num3 = Mathf.Round(num3 * 10f) / 10f;
				this.PlacedProp.transform.localScale = Vector3.one * num3 * this.PlacedProp.defaultScale;
				if (this.OnPlacedObjectScaleChanged != null)
				{
					this.OnPlacedObjectScaleChanged(num3);
				}
				this.PlaceProp(this.PlacedProp.transform.position);
			}
		}
	}

	public void ResetPropScale()
	{
		float defaultScale = this.PlacedProp.defaultScale;
		this.PlacedProp.transform.localScale = Vector3.one * defaultScale;
		if (this.OnPlacedObjectScaleChanged != null)
		{
			this.OnPlacedObjectScaleChanged(defaultScale / this.PlacedProp.defaultScale);
		}
	}

	public void RemoveProp()
	{
		if (this.PlacedProp == null)
		{
			return;
		}
		this.PlacedProp.ResetSnapping();
		UnityEngine.Object.Destroy(this.PlacedProp.gameObject);
		this.SelectProp(-1);
		this.ChangePropState(PropPlacementState.NotSelected);
	}

	public void SetAlignBySlope(bool align)
	{
		this.alignBySlope = align;
		if (this.PlacedProp != null)
		{
			this.PlaceProp(this.PlacedProp.transform.position);
		}
	}

	public void SelectProp(int propID)
	{
		this.selectedPropID = propID;
		this.ChangePropState(PropPlacementState.Selected);
		if (this.OnSelectedPropChanged != null)
		{
			this.OnSelectedPropChanged();
		}
	}

	private void ResetSelectedProp()
	{
		this.selectedPropID = -1;
		if (this.OnSelectedPropChanged != null)
		{
			this.OnSelectedPropChanged();
		}
	}

	public void ChangePropState(PropPlacementState newState)
	{
		this.propState = newState;
		if (this.propState == PropPlacementState.NotSelected || this.propState == PropPlacementState.Selected)
		{
			if (this.PlacedProp != null)
			{
				this.PlacedProp.Highlight(false);
			}
			this.PlacedProp = null;
		}
		if (this.OnPropStateChanged != null)
		{
			this.OnPropStateChanged();
		}
	}

	private void PlaceProp(Vector3 pos)
	{
		this.ChangePropState(PropPlacementState.Placed);
		if (this.PlacedProp == null)
		{
			this.PlacedProp = UnityEngine.Object.Instantiate<GameObject>(this.editorResources.propsDictionary[this.selectedPropID].gameObject, pos, this.editorResources.propsDictionary[this.selectedPropID].transform.rotation, LevelEditorTools.PropsParent).GetComponent<Prop>();
			this.PlacedProp.propID = this.selectedPropID;
		}
		this.PlacedProp.Highlight(true);
		pos.y = this.terrain.SampleHeight(pos) + this.PlacedProp.currentLift;
		if (this.alignBySlope)
		{
			LevelEditorTools.AlignByNormal(this.terrain, this.PlacedProp.transform, this.PlacedProp.frontSupport, this.PlacedProp.rearSupport);
		}
		else
		{
			this.PlacedProp.transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(this.PlacedProp.transform.forward, Vector3.up), Vector3.up);
		}
		if (this.PlacedProp.DoSnapping() && this.PlacedProp.snapPosition)
		{
			pos.y = this.PlacedProp.transform.position.y;
		}
		this.PlacedProp.transform.position = pos;
		this.PlacedProp.DoSnapping();
	}

	public void ApplyProp()
	{
		if (this.PlacedProp != null)
		{
			this.PlacedProp.Highlight(false);
		}
		this.ChangePropState(PropPlacementState.NotSelected);
		this.ResetSelectedProp();
	}

	public void DuplicateProp()
	{
		int propID = this.PlacedProp.propID;
		bool extra0Enabled = this.PlacedProp.extra0Enabled;
		bool extra1Enabled = this.PlacedProp.extra1Enabled;
		float currentLift = this.PlacedProp.currentLift;
		Vector3 localScale = this.PlacedProp.transform.localScale;
		Quaternion rotation = this.PlacedProp.transform.rotation;
		Vector3 vector = this.PlacedProp.transform.position + this.PlacedProp.transform.forward * 20f;
		SnapPoint snapPoint = this.PlacedProp.ClosestFreeSnapPoint(vector);
		int num = -1;
		if (snapPoint != null)
		{
			Vector3 position = snapPoint.transform.position;
			int suitableSnapPointID = this.PlacedProp.GetSuitableSnapPointID(snapPoint);
			num = suitableSnapPointID;
		}
		this.ApplyProp();
		this.SelectProp(propID);
		this.PlaceProp(vector);
		this.PlacedProp.Initialize();
		this.PlacedProp.currentLift = currentLift;
		this.PlacedProp.transform.localScale = localScale;
		this.PlacedProp.transform.rotation = rotation;
		this.PlacedProp.ToggleExtra0(extra0Enabled);
		this.PlacedProp.ToggleExtra1(extra1Enabled);
		if (num != -1)
		{
			Vector3 b = snapPoint.transform.InverseTransformPoint(this.PlacedProp.snapPoints[num].transform.position);
			b = this.PlacedProp.snapPoints[num].transform.position - snapPoint.transform.position;
			this.PlacedProp.transform.position -= b;
		}
		this.PlaceProp(this.PlacedProp.transform.position);
		this.ChangePropState(PropPlacementState.Placed);
	}

	private void MoveCheckpoint()
	{
		Vector3 vector = Vector3.zero;
		if (UnityEngine.Input.touchCount == 1)
		{
			vector = UnityEngine.Input.GetTouch(0).position;
			Vector3 b = vector - this.lastTouchPos;
			this.movingCheckpointButton.transform.position += b;
			this.lastTouchPos = vector;
		}
		if (vector == Vector3.zero)
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(vector);
		RaycastHit[] array = (from h in Physics.RaycastAll(ray)
		orderby h.distance
		select h).ToArray<RaycastHit>();
		for (int i = 0; i < array.Length; i++)
		{
			if (!(array[i].collider.GetComponentInParent<PlayerRoute>() != null))
			{
				Vector3 b2 = array[i].point - this.lastRaycastPos;
				Vector3 position = this.SelectedCheckpoint.transform.position + b2;
				this.SelectedCheckpoint.transform.position = position;
				this.routes[this.selectedRouteID].UpdateLineRenderer();
				this.routes[this.selectedRouteID].AlignCheckpoints();
				this.lastRaycastPos = array[i].point;
				break;
			}
		}
		this.routes[this.selectedRouteID].routeID = LevelEditorTools.RandomName();
	}

	public void ChangeRouteCreationStep(RouteCreationStep newStep)
	{
		this.routeCreationStep = newStep;
		if (this.OnRouteCreationStepChanged != null)
		{
			this.OnRouteCreationStepChanged();
		}
	}

	public void CreateNewRoute()
	{
		PlayerRoute playerRoute = new GameObject("Route").AddComponent<PlayerRoute>();
		playerRoute.routeName = "New route";
		playerRoute.transform.parent = LevelEditorTools.RoutesParent;
		playerRoute.transform.position = Vector3.zero;
		playerRoute.InitializeLineRenderer(this.editorResources.routeMaterial);
		playerRoute.routeID = LevelEditorTools.RandomName();
		this.routes.Add(playerRoute);
		this.SelectRoute(this.routes.Count - 1);
		if (this.OnRouteCreated != null)
		{
			this.OnRouteCreated();
		}
	}

	public void AddRouteWaypoint(Vector3 pos)
	{
		this.routes[this.selectedRouteID].AddCheckpoint(pos);
		this.routes[this.selectedRouteID].UpdateCheckpointPrefabs();
		this.routes[this.selectedRouteID].routeID = LevelEditorTools.RandomName();
	}

	public void SelectRoute(int routeID)
	{
		this.selectedRouteID = routeID;
		for (int i = 0; i < this.routes.Count; i++)
		{
			this.routes[i].UpdateCheckpointPrefabs();
			if (i == this.selectedRouteID)
			{
				this.routes[i].UnBakeRoute();
			}
			else
			{
				this.routes[i].BakeRoute();
			}
		}
		if (routeID != -1)
		{
			this.ChangeRouteCreationStep(RouteCreationStep.Selected);
		}
		else
		{
			this.ChangeRouteCreationStep(RouteCreationStep.None);
		}
	}

	public void CheckRoutesLenghts()
	{
		for (int i = 0; i < this.routes.Count; i++)
		{
			if (this.routes[i].checkpoints.Count < 2)
			{
				UnityEngine.Object.Destroy(this.routes[i].gameObject);
				this.routes.RemoveAt(i);
			}
		}
	}

	public void ChangeCurrentRouteName(string name)
	{
		this.routes[this.selectedRouteID].routeName = name;
	}

	public void RemoveCurrentRoute()
	{
		UnityEngine.Object.Destroy(this.routes[this.selectedRouteID].gameObject);
		this.routes.RemoveAt(this.selectedRouteID);
		this.SelectRoute(-1);
	}

	public void ApplyRoute()
	{
		this.SelectRoute(-1);
	}

	public void SelectCheckpoint(int id)
	{
		this.selectedCheckpointID = id;
		this.ChangeRouteCreationStep(RouteCreationStep.ModifyingCheckpoint);
	}

	public void ApplyCheckpoint()
	{
		this.ChangeRouteCreationStep(RouteCreationStep.Selected);
	}

	public void RemoveCheckpoint()
	{
		UnityEngine.Object.DestroyImmediate(this.routes[this.selectedRouteID].checkpoints[this.selectedCheckpointID].gameObject);
		this.routes[this.selectedRouteID].checkpoints.RemoveAt(this.selectedCheckpointID);
		this.routes[this.selectedRouteID].UpdateCheckpointPrefabs();
		this.routes[this.selectedRouteID].UpdateLineRenderer();
		this.ChangeRouteCreationStep(RouteCreationStep.Selected);
		this.routes[this.selectedRouteID].routeID = LevelEditorTools.RandomName();
	}

	private void InitializeCamera()
	{
		this.mainCamera = Camera.main;
		this.cameraTarget_TargetPos = this.terrain.GetPosition() + this.terrain.terrainData.size / 2f;
		this.cameraTarget.position = this.cameraTarget_TargetPos;
		this.camYTarget = (this.camY = 45f);
		this.distance = (this.distanceTarget = this.maxDistance);
	}

	private void DoCamera()
	{
		this.doubleTouch = (UnityEngine.Input.touchCount == 2);
		if (!this.movingStamp && !this.drawingPath)
		{
			if (this.doubleTouch)
			{
				this.camXTarget += CrossPlatformInputManager.GetAxis("Drag X") * this.rotateSensevitity;
				this.camYTarget -= CrossPlatformInputManager.GetAxis("Drag Y") * this.rotateSensevitity;
				this.distanceTarget -= CrossPlatformInputManager.GetAxis("Zoom") * this.zoomSensevitity;
			}
			else
			{
				this.cameraTarget_TargetPos -= this.mainCamera.transform.right * CrossPlatformInputManager.GetAxis("Drag X") * this.moveSensevitity * this.distanceTarget / 50f;
				this.cameraTarget_TargetPos -= Vector3.ProjectOnPlane(this.mainCamera.transform.forward, Vector3.up).normalized * CrossPlatformInputManager.GetAxis("Drag Y") * this.moveSensevitity * this.distanceTarget / 50f;
			}
		}
		this.distanceTarget = Mathf.Clamp(this.distanceTarget, this.minDistance, this.maxDistance);
		this.distance = Mathf.Lerp(this.distance, this.distanceTarget, Time.deltaTime * 5f);
		this.camYTarget = Mathf.Clamp(this.camYTarget, 1f, 89f);
		this.cameraTarget_TargetPos.y = this.terrain.SampleHeight(this.cameraTarget.transform.position);
		this.cameraTarget.position = Vector3.Lerp(this.cameraTarget.position, this.cameraTarget_TargetPos, Time.deltaTime * 8f);
		this.camX = Mathf.Lerp(this.camX, this.camXTarget, Time.deltaTime * 8f);
		this.camY = Mathf.Lerp(this.camY, this.camYTarget, Time.deltaTime * 8f);
		Quaternion quaternion = Quaternion.Euler(this.camY, this.camX, 0f);
		Vector3 position = quaternion * new Vector3(0f, 0f, -this.distance) + this.cameraTarget.position;
		this.mainCamera.transform.rotation = Quaternion.Lerp(this.mainCamera.transform.rotation, quaternion, Time.deltaTime * 8f);
		this.mainCamera.transform.position = position;
		this.mainCamera.transform.LookAt(this.cameraTarget);
	}

	[ContextMenu("Generate random terrain")]
	public void GenerateRandomTerrain()
	{
		this.terrainSize = (float)UnityEngine.Random.Range(300, 1000);
		this.seed = UnityEngine.Random.Range(0, 10000);
		this.bumpsStrength = UnityEngine.Random.Range(0.3f, 1f);
		this.flatCenter = (UnityEngine.Random.Range(0f, 1f) < 0.5f);
		this.treesDensity = UnityEngine.Random.Range(0f, 1f);
		this.treesByEdgesOnly = true;
		this.mainTextureID = UnityEngine.Random.Range(0, 3);
		this.waterEnabled = (UnityEngine.Random.Range(0f, 1f) < 0.2f);
		this.waterHeight = UnityEngine.Random.Range(0.45f, 0.52f);
		this.frozenWater = (UnityEngine.Random.Range(0f, 1f) < 0.3f);
		for (int i = 0; i < this.usedExtraObjects.Length; i++)
		{
			this.usedExtraObjects[i].arrayID = UnityEngine.Random.Range(-1, this.editorResources.extraObjectsDictionary.Length);
			this.usedExtraObjects[i].density = UnityEngine.Random.Range(0f, 1f);
			this.usedExtraObjects[i].onlyByEdges = (UnityEngine.Random.Range(0f, 1f) < 0.5f);
		}
		if (this.OnTerrainValuesRandomized != null)
		{
			this.OnTerrainValuesRandomized();
		}
		this.GenerateTerrain(false);
	}

	public void RemoveAllModsAndProceedGeneratingTerrain()
	{
		this.stamps.Clear();
		this.paths.Clear();
		this.routes.Clear();
		this.ignoredExtraObjectIDs.Clear();
		this.mudStamps.Clear();
		UnityEngine.Object.DestroyImmediate(LevelEditorTools.PropsParent.gameObject);
		UnityEngine.Object.DestroyImmediate(LevelEditorTools.RoutesParent.gameObject);
		UnityEngine.Object.DestroyImmediate(LevelEditorTools.MudStampsParent.gameObject);
		this.GenerateTerrain(false);
	}

	public void GenerateTerrain(bool dontShowNotification)
	{
		base.StartCoroutine(this.GeneratingTerrainCor(dontShowNotification));
	}

	private IEnumerator GeneratingTerrainCor(bool dontShowNotification)
	{
		if (!dontShowNotification && (this.stamps.Count > 0 || this.paths.Count > 0 || this.routes.Count > 0 || this.ignoredExtraObjectIDs.Count > 0 || LevelEditorTools.PropsParent.childCount > 0))
		{
			if (this.OnModsResetWarning != null)
			{
				this.OnModsResetWarning();
			}
			yield break;
		}
		if (this.OnProcessingTerrainStarted != null)
		{
			this.OnProcessingTerrainStarted("Generating terrain...");
		}
		yield return null;
		float startTime = Time.realtimeSinceStartup;
		Vector3 cameraTargetRelativePos = (this.cameraTarget_TargetPos - this.terrain.GetPosition()) / this.terData.size.x;
		this.ResetTerrain();
		this.GenerateStampBasedTerrain();
		this.ApplyWater();
		this.PlaceTrees();
		this.lastPlacedExtraObjectID = this.PlaceExtraObjects();
		this.PlaceCliffs();
		this.PaintBaseTexture();
		this.PaintRockAndWaterTextures();
		this.defHeights = this.terData.GetHeights(0, 0, this.heightmapWidth, this.heightmapWidth);
		if (Application.isPlaying)
		{
			this.cameraTarget_TargetPos = this.terrain.GetPosition() + cameraTargetRelativePos * this.terData.size.x;
		}
		float endTime = Time.realtimeSinceStartup;
		float functionTime = endTime - startTime;
		UnityEngine.Debug.Log("Terrain generated for " + functionTime);
		yield return null;
		if (this.OnProcessingTerrainFinished != null)
		{
			this.OnProcessingTerrainFinished();
		}
		if (this.OnTerrainGenerated != null)
		{
			this.OnTerrainGenerated();
		}
		yield break;
	}

	private void GeneratePerlinTerrain()
	{
		LevelEditorTools.GeneratePerlinTerrain(this.terrain, this.seed, this.terrainSize, this.flatCenter, this.bumpsStrength);
	}

	private void GenerateStampBasedTerrain()
	{
		LevelEditorTools.GenerateStampBasedTerrain(this.terrain, this.editorResources.terrainGenerationStamps, this.seed, this.terrainSize, this.flatCenter, this.bumpsStrength);
	}

	private void ApplyWater()
	{
		LevelEditorTools.ApplyWater(this.terrain, this.waterPlane, this.waterEnabled, this.terrainSize, this.waterHeight, this.frozenWater, this.editorResources.frozenWaterMaterial, this.editorResources.waterMaterial);
	}

	private void PlaceTrees()
	{
		LevelEditorTools.PlaceTrees(this.terrain, this.waterPlane, this.seed, this.terrainSize, this.editorResources.maxTreesCount, this.treesDensity, this.treesByEdgesOnly);
	}

	private int PlaceExtraObjects()
	{
		return LevelEditorTools.PlaceExtraObjects(this.terrain, this.waterPlane, this.seed, this.usedExtraObjects, this.editorResources.extraObjectsDictionary, this.terrainSize, this.lastPlacedExtraObjectID);
	}

	private void PlaceCliffs()
	{
		LevelEditorTools.PlaceCliffs(this.terrain, this.editorResources.cliffPrefabs, this.terrainSize, this.seed, this.editorResources.baseCliffsCount, this.editorResources.minHillAngle);
	}

	private void PaintBaseTexture()
	{
		LevelEditorTools.PaintBaseTexture(this.terrain, this.mainTextureID);
	}

	private void PaintRockAndWaterTextures()
	{
		LevelEditorTools.PaintRockAndWaterTextures(this.terrain, this.waterPlane, this.waterEnabled, this.frozenWater, this.editorResources.underwaterTextureID, this.editorResources.rockTextureID, this.editorResources.minRockAngle, this.editorResources.maxRockAngle, this.mainTextureID);
	}

	private void ApplyHeightStamp(TerrainStamp stamp)
	{
		LevelEditorTools.ApplyHeightStamp(this.terrain, stamp, this.defHeights, this.editorResources.stampTextures);
	}

	private void ApplySmoothStamp(TerrainStamp stamp)
	{
		LevelEditorTools.ApplySmoothStamp(this.terrain, stamp, this.editorResources.stampTextures);
	}

	private void ApplyPaintStamp(TerrainStamp stamp)
	{
		LevelEditorTools.ApplyPaintStamp(this.terrain, this.editorResources.stampTextures[stamp.stampTextureID], stamp.stampRotation, stamp.stampPosition, stamp.stampSize, stamp.extraInt);
	}

	private void ApplyRemovingExtraObjectsStamp(TerrainStamp stamp, ref List<TreeInstance> removedTrees, ref List<GameObject> removedExtraObjects, bool previewOnly)
	{
		LevelEditorTools.ApplyRemoveExtraObjectsStamp(this.terrain, stamp, this.seed, this.editorResources.stampTextures, ref removedTrees, ref removedExtraObjects, previewOnly);
	}

	private int ApplyAddExtraObjectsStamp(TerrainStamp stamp, ref List<TreeInstance> addedTrees, ref List<GameObject> addedExtraObjects)
	{
		return LevelEditorTools.ApplyAddExtraObjectsStamp(this.terrain, this.waterPlane, stamp, this.editorResources.stampTextures, this.seed, this.editorResources.extraObjectsDictionary, this.terrainSize, this.waterEnabled, ref addedTrees, ref addedExtraObjects, this.lastPlacedExtraObjectID);
	}

	private void ApplyHeightPath(TerrainPath path)
	{
		LevelEditorTools.ApplyHeightPath(this.terrain, path, this.defHeights, this.editorResources.pathPatterns);
	}

	private void ApplySmoothPath(TerrainPath path)
	{
		LevelEditorTools.ApplySmoothPath(this.terrain, path, this.editorResources.pathPatterns);
	}

	private void ApplyPaintPath(TerrainPath path)
	{
		LevelEditorTools.ApplyPaintPath(this.terrain, path, this.editorResources.pathPatterns);
	}

	private void ApplyRemovingExtraObjectsPath(TerrainPath path, ref List<TreeInstance> removedTrees, ref List<GameObject> removedExtraObjects, bool previewOnly)
	{
		LevelEditorTools.ApplyRemoveExtraObjectsPath(this.terrain, path, this.seed, this.editorResources.pathPatterns, ref removedTrees, ref removedExtraObjects, previewOnly);
	}

	private int ApplyAddExtraObjectsPath(TerrainPath path, ref List<TreeInstance> addedTrees, ref List<GameObject> addedExtraObjects)
	{
		return LevelEditorTools.ApplyAddExtraObjectsPath(this.terrain, this.waterPlane, path, this.editorResources.pathPatterns, this.seed, this.editorResources.extraObjectsDictionary, this.terrainSize, this.waterEnabled, ref addedTrees, ref addedExtraObjects, this.lastPlacedExtraObjectID);
	}

	private void CorrectExtraObjectsTransforms()
	{
		LevelEditorTools.CorrectExtraObjectsTransforms(this.terrain, this.seed);
	}

	private void CorrectCliffsPositions()
	{
		LevelEditorTools.CorrectCliffsPositions(this.terrain);
	}

	public static LevelEditor Instance;

	private LevelEditorResources editorResources;

	private LevelEditorUI ui;

	[HideInInspector]
	public LevelCreationStep levelCreationStep;

	[HideInInspector]
	public string mapName;

	[HideInInspector]
	public string mapDescription;

	[HideInInspector]
	public string mapID;

	[HideInInspector]
	public int mapRating;

	[HideInInspector]
	public bool mapUploaded;

	[HideInInspector]
	public bool mapVisible;

	[Header("Landscape")]
	public int seed;

	public float terrainSize = 500f;

	[Range(0f, 1f)]
	public float bumpsStrength = 0.01f;

	private const float tileSize = 10f;

	public bool flatCenter;

	[Header("Trees")]
	[Range(0f, 1f)]
	public float treesDensity = 0.5f;

	public bool treesByEdgesOnly;

	public ExtraObjectReference[] usedExtraObjects;

	[HideInInspector]
	public List<int> ignoredExtraObjectIDs = new List<int>();

	[HideInInspector]
	public int lastPlacedExtraObjectID;

	[Header("Water")]
	public Transform waterPlane;

	[Range(0.3f, 0.6f)]
	public float waterHeight;

	public bool waterEnabled;

	public bool frozenWater;

	[Header("Textures")]
	public int mainTextureID;

	[Header("Camera navigation")]
	public Transform cameraTarget;

	private float minDistance = 5f;

	private float maxDistance = 600f;

	private float moveSensevitity = 0.2f;

	private float rotateSensevitity = 0.2f;

	private float zoomSensevitity = 0.1f;

	private Vector3 cameraTarget_TargetPos;

	private Camera mainCamera;

	private bool doubleTouch;

	private float camX;

	private float camY;

	private float distance;

	private float distanceTarget;

	private float camXTarget;

	private float camYTarget;

	[HideInInspector]
	public TerrainModifyingType terrainModType;

	[HideInInspector]
	public ModAction modAction;

	[HideInInspector]
	[Range(0f, 1f)]
	public float modStrength;

	[HideInInspector]
	public int modPaintTextureID;

	[HideInInspector]
	public int addingExtraObjectID;

	private float[,] defHeights;

	private float[,] heightsBeforePreview;

	private float[,,] splatBeforePreview;

	private List<TreeInstance> treesAddedForPreview = new List<TreeInstance>();

	private List<TreeInstance> treesRemovedForPreview = new List<TreeInstance>();

	private List<GameObject> extraObjectsAddedForPreview = new List<GameObject>();

	private List<GameObject> extraObjectsRemovedForPreview = new List<GameObject>();

	[Header("Stamps")]
	public Projector stampProjector;

	public float stampMoveSensevitity = 1f;

	public Transform sizingButton;

	public Transform movingButton;

	[HideInInspector]
	public int stampTextureID;

	[HideInInspector]
	public List<TerrainStamp> stamps = new List<TerrainStamp>();

	[HideInInspector]
	public float stampSize;

	[HideInInspector]
	public float stampRotation;

	[HideInInspector]
	public StampState stampState;

	[HideInInspector]
	public bool movingStamp;

	[HideInInspector]
	public bool sizingStamp;

	[HideInInspector]
	public Vector3 stampPosition;

	private Vector3 stampLookPos;

	private Vector3 lastTouchPos;

	[Header("Paths")]
	public float pathWaypointsDistance;

	public int maxPathWaypoints;

	public Material pathMaterial;

	[HideInInspector]
	public float pathWidth;

	[HideInInspector]
	public List<TerrainPath> paths = new List<TerrainPath>();

	[HideInInspector]
	public PathState pathState;

	[HideInInspector]
	public Vector3 lastPathPoint;

	[HideInInspector]
	public bool draggingScreen;

	[HideInInspector]
	public int selectedPathPattern;

	private List<Vector3> pathPositions = new List<Vector3>();

	private bool drawingPath;

	private LineRenderer lineRenderer;

	private Vector3 tempPathPoint;

	[Header("Objects placement")]
	public GameObject movingPropButton;

	public GameObject sizingPropButton;

	public GameObject liftingPropButton;

	public float liftSensevitity;

	public int spawnPointPropID;

	[HideInInspector]
	public PropPlacementState propState;

	[HideInInspector]
	public bool alignBySlope;

	[HideInInspector]
	public int selectedPropID;

	[HideInInspector]
	public Prop PlacedProp;

	[HideInInspector]
	public bool movingProp;

	[HideInInspector]
	public bool sizingProp;

	[HideInInspector]
	public bool liftingProp;

	private Vector3 lastRaycastPos;

	private Vector3 placedPropLookPos;

	[Header("Route placement")]
	public GameObject movingCheckpointButton;

	[HideInInspector]
	public RouteCreationStep routeCreationStep;

	[HideInInspector]
	public int selectedRouteID;

	[HideInInspector]
	public List<PlayerRoute> routes = new List<PlayerRoute>();

	[HideInInspector]
	public bool movingCheckpoint;

	[HideInInspector]
	public int selectedCheckpointID;

	[Header("Adding mud")]
	public Color selectedMudStampColor;

	public Color deselectedMudStampColor;

	public GameObject movingMudStampButton;

	public GameObject sizingMudStampButton;

	public Vector3 mudStampLookPos;

	public float minMudStampSize;

	public float maxMudStampSize;

	public List<MudStamp> mudStamps = new List<MudStamp>();

	[HideInInspector]
	public MudStampState mudStampState;

	[HideInInspector]
	public int selectedMudStampID;

	[HideInInspector]
	public bool movingMudStamp;

	[HideInInspector]
	public bool sizingMudStamp;

	private float[,,] tempSplatMap;

	public LevelEditor.CreationStepChanged OnCreationStepChanged;

	public LevelEditor.TerrainGenerated OnTerrainGenerated;

	public LevelEditor.StampStateChanged OnStampStateChanged;

	public LevelEditor.ModActionChanged OnModActionChanged;

	public LevelEditor.ProcessingTerrainStarted OnProcessingTerrainStarted;

	public LevelEditor.ProcessingTerrainFinished OnProcessingTerrainFinished;

	public LevelEditor.ModsResetWarning OnModsResetWarning;

	public LevelEditor.TerrainValuesRandomized OnTerrainValuesRandomized;

	public LevelEditor.ModTypeChanged OnModTypeChanged;

	public LevelEditor.PathStateChanged OnPathStateChanged;

	public LevelEditor.SelectedPropChanged OnSelectedPropChanged;

	public LevelEditor.PropStateChanged OnPropStateChanged;

	public LevelEditor.PlacedObjectScaleChanged OnPlacedObjectScaleChanged;

	public LevelEditor.RouteCreationStepChanged OnRouteCreationStepChanged;

	public LevelEditor.RouteCreated OnRouteCreated;

	public LevelEditor.LevelUploadFinished OnLevelUploadFinished;

	public LevelEditor.LevelRemovalFinished OnLevelRemovalFinished;

	public LevelEditor.SelectedMudStampChanged OnSelectedMudStampChanged;

	private Terrain _cachedTerrain;

	private TerrainData _cachedTerData;

	private TerrainCollider _cachedTerCollider;

	private bool splatCached;

	public delegate void CreationStepChanged();

	public delegate void TerrainGenerated();

	public delegate void StampStateChanged();

	public delegate void ModActionChanged();

	public delegate void ProcessingTerrainStarted(string text);

	public delegate void ProcessingTerrainFinished();

	public delegate void ModsResetWarning();

	public delegate void TerrainValuesRandomized();

	public delegate void ModTypeChanged();

	public delegate void PathStateChanged();

	public delegate void SelectedPropChanged();

	public delegate void PropStateChanged();

	public delegate void PlacedObjectScaleChanged(float scaleRatio);

	public delegate void RouteCreationStepChanged();

	public delegate void RouteCreated();

	public delegate void LevelUploadFinished(bool failed);

	public delegate void LevelRemovalFinished(bool failed);

	public delegate void SelectedMudStampChanged();
}
