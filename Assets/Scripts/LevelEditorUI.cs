using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditorUI : MonoBehaviour
{
	private void Awake()
	{
		this.usedExtraObjects = new ExtraObjectReference[3];
		for (int i = 0; i < this.usedExtraObjects.Length; i++)
		{
			this.usedExtraObjects[i] = new ExtraObjectReference();
			this.usedExtraObjects[i].arrayID = -1;
		}
		this.editorResources = LevelEditorTools.editorResources;
		this.circleDrawer = new GameObject("CircleDrawer").AddComponent<CircleDrawer>();
		this.circleDrawer.pointsCount = this.circleDrawerPoints;
		this.circleDrawer.width = this.circleDrawerWidth;
		this.circleDrawer.mat = this.circleDrawerMaterial;
		this.circleDrawer.enabled = false;
		this.currentCircleDrawerRadius = this.circleDrawerBaseRadius;
		this.levelEditor = base.GetComponent<LevelEditor>();
		LevelEditor levelEditor = this.levelEditor;
		levelEditor.OnCreationStepChanged = (LevelEditor.CreationStepChanged)Delegate.Combine(levelEditor.OnCreationStepChanged, new LevelEditor.CreationStepChanged(this.OnLevelCreationStepChanged));
		LevelEditor levelEditor2 = this.levelEditor;
		levelEditor2.OnModActionChanged = (LevelEditor.ModActionChanged)Delegate.Combine(levelEditor2.OnModActionChanged, new LevelEditor.ModActionChanged(this.OnModActionChanged));
		LevelEditor levelEditor3 = this.levelEditor;
		levelEditor3.OnStampStateChanged = (LevelEditor.StampStateChanged)Delegate.Combine(levelEditor3.OnStampStateChanged, new LevelEditor.StampStateChanged(this.OnStampStateChanged));
		LevelEditor levelEditor4 = this.levelEditor;
		levelEditor4.OnProcessingTerrainStarted = (LevelEditor.ProcessingTerrainStarted)Delegate.Combine(levelEditor4.OnProcessingTerrainStarted, new LevelEditor.ProcessingTerrainStarted(this.OnProcessingTerrainStarted));
		LevelEditor levelEditor5 = this.levelEditor;
		levelEditor5.OnProcessingTerrainFinished = (LevelEditor.ProcessingTerrainFinished)Delegate.Combine(levelEditor5.OnProcessingTerrainFinished, new LevelEditor.ProcessingTerrainFinished(this.OnProcessingTerrainFinished));
		LevelEditor levelEditor6 = this.levelEditor;
		levelEditor6.OnModsResetWarning = (LevelEditor.ModsResetWarning)Delegate.Combine(levelEditor6.OnModsResetWarning, new LevelEditor.ModsResetWarning(this.OnModsResetWarning));
		LevelEditor levelEditor7 = this.levelEditor;
		levelEditor7.OnTerrainValuesRandomized = (LevelEditor.TerrainValuesRandomized)Delegate.Combine(levelEditor7.OnTerrainValuesRandomized, new LevelEditor.TerrainValuesRandomized(this.OnTerrainValuesRandomized));
		LevelEditor levelEditor8 = this.levelEditor;
		levelEditor8.OnModTypeChanged = (LevelEditor.ModTypeChanged)Delegate.Combine(levelEditor8.OnModTypeChanged, new LevelEditor.ModTypeChanged(this.OnModTypeChanged));
		LevelEditor levelEditor9 = this.levelEditor;
		levelEditor9.OnPathStateChanged = (LevelEditor.PathStateChanged)Delegate.Combine(levelEditor9.OnPathStateChanged, new LevelEditor.PathStateChanged(this.OnPathStateChanged));
		LevelEditor levelEditor10 = this.levelEditor;
		levelEditor10.OnSelectedPropChanged = (LevelEditor.SelectedPropChanged)Delegate.Combine(levelEditor10.OnSelectedPropChanged, new LevelEditor.SelectedPropChanged(this.OnSelectedPropChanged));
		LevelEditor levelEditor11 = this.levelEditor;
		levelEditor11.OnPropStateChanged = (LevelEditor.PropStateChanged)Delegate.Combine(levelEditor11.OnPropStateChanged, new LevelEditor.PropStateChanged(this.OnPropStateChanged));
		LevelEditor levelEditor12 = this.levelEditor;
		levelEditor12.OnPlacedObjectScaleChanged = (LevelEditor.PlacedObjectScaleChanged)Delegate.Combine(levelEditor12.OnPlacedObjectScaleChanged, new LevelEditor.PlacedObjectScaleChanged(this.OnPlacedObjectScaleChanged));
		LevelEditor levelEditor13 = this.levelEditor;
		levelEditor13.OnTerrainGenerated = (LevelEditor.TerrainGenerated)Delegate.Combine(levelEditor13.OnTerrainGenerated, new LevelEditor.TerrainGenerated(this.OnTerrainGenerated));
		LevelEditor levelEditor14 = this.levelEditor;
		levelEditor14.OnRouteCreationStepChanged = (LevelEditor.RouteCreationStepChanged)Delegate.Combine(levelEditor14.OnRouteCreationStepChanged, new LevelEditor.RouteCreationStepChanged(this.OnRouteCreationStepChanged));
		LevelEditor levelEditor15 = this.levelEditor;
		levelEditor15.OnLevelUploadFinished = (LevelEditor.LevelUploadFinished)Delegate.Combine(levelEditor15.OnLevelUploadFinished, new LevelEditor.LevelUploadFinished(this.LevelUploadFinished));
		LevelEditor levelEditor16 = this.levelEditor;
		levelEditor16.OnLevelRemovalFinished = (LevelEditor.LevelRemovalFinished)Delegate.Combine(levelEditor16.OnLevelRemovalFinished, new LevelEditor.LevelRemovalFinished(this.LevelRemovalFinished));
		LevelEditor levelEditor17 = this.levelEditor;
		levelEditor17.OnSelectedMudStampChanged = (LevelEditor.SelectedMudStampChanged)Delegate.Combine(levelEditor17.OnSelectedMudStampChanged, new LevelEditor.SelectedMudStampChanged(this.OnSelectedMudStampChanged));
	}

	private void Start()
	{
		this.InitializeUI();
		this.mainCamera = Camera.main;
	}

	private void Update()
	{
		this.PoseButtons();
		this.UpdateCircleDrawer();
		Color color = this.scaleValueText.color;
		color.a = Mathf.MoveTowards(color.a, 0f, Time.deltaTime);
		this.scaleValueText.color = color;
	}

	private void LevelUploadFinished(bool failed)
	{
		this.mapUploadingWindow.SetActive(false);
		string caption = (!failed) ? "Success" : "Error";
		string body = (!failed) ? "Your map was successfully uploaded!" : "An error occurred while uploading";
		this.ShowMessage(caption, body);
	}

	private void LevelRemovalFinished(bool failed)
	{
		UnityEngine.Debug.Log("Level removal finished. Failed: " + failed);
		this.mapRemovalWindow.SetActive(false);
		string caption = (!failed) ? "Success" : "Error";
		string body = (!failed) ? "Your map was successfully removed from server!" : "Something went wrong, try again";
		this.ShowMessage(caption, body);
	}

	private void ShowMessage(string caption, string body)
	{
		this.messageWindow.SetActive(true);
		this.messageText.text = body;
		this.messageCaption.text = caption;
	}

	public void InitializeUI()
	{
		this.generateTerrainWindow.SetActive(false);
		this.seedSlider.value = (float)this.levelEditor.seed;
		this.sizeSlider.value = this.levelEditor.terrainSize;
		this.bumpsStrengthSlider.value = this.levelEditor.bumpsStrength;
		this.treeDensitySlider.value = this.levelEditor.treesDensity;
		this.onlyOnEdgesToggle.isOn = this.levelEditor.treesByEdgesOnly;
		this.flatCenterToggle.isOn = this.levelEditor.flatCenter;
		this.modifyExtraObjectWindow.SetActive(false);
		this.enableWaterToggle.isOn = this.levelEditor.waterEnabled;
		this.waterHeightSlider.value = this.levelEditor.waterHeight;
		this.frozenWaterToggle.isOn = this.levelEditor.frozenWater;
		this.devPropsTab.SetActive(GameState.devRights);
		this.modifyTerrainWindow.SetActive(false);
		this.tapOnScreenText.gameObject.SetActive(false);
		this.drawPathText.gameObject.SetActive(false);
		this.modStrengthSlider.value = this.levelEditor.modStrength;
		this.moveStampButton.SetActive(false);
		this.sizeStampButton.SetActive(false);
		this.removeStampButton.SetActive(false);
		this.removePathButton.SetActive(false);
		this.pathSelectorWindow.SetActive(false);
		this.processingTerrainWindow.SetActive(false);
		this.modsResetWarningWindow.SetActive(false);
		this.propSelectorWindow.SetActive(false);
		this.selectedPropNameText.text = "NONE";
		this.tapToPlacePropText.SetActive(false);
		this.selectPropText.SetActive(false);
		this.movePropButton.SetActive(false);
		this.sizePropButton.SetActive(false);
		this.removePropButton.SetActive(false);
		this.liftPropButton.SetActive(false);
		this.scaleValueText.gameObject.SetActive(false);
		this.alignBySlopeToggle.isOn = this.levelEditor.alignBySlope;
		this.extra0Toggle.gameObject.SetActive(false);
		this.extra1Toggle.gameObject.SetActive(false);
		this.UpdateStampButtons();
		this.ChangeModAction(0);
		this.ChangePropCategory(0);
		this.modifyTerrainButton.interactable = false;
		this.placeObjectsButton.interactable = false;
		this.finalizeButton.interactable = false;
		this.placeRoutesButton.interactable = false;
		this.addMudButton.interactable = false;
		this.finalizingMapWindow.SetActive(false);
		this.uploadWarningWindow.SetActive(false);
		this.removalWarningWindow.SetActive(false);
		this.addingMudWindow.SetActive(false);
		this.moveCheckpointButton.SetActive(false);
		this.removeCheckpointButton.SetActive(false);
		this.applyCheckpointButton.SetActive(false);
		this.moveMudStampButton.SetActive(false);
		this.removeMudStampButton.SetActive(false);
		this.sizeMudStampButton.SetActive(false);
		this.applyMudStampButton.SetActive(false);
		this.mapUploadingWindow.SetActive(false);
		this.menu.SetActive(false);
		this.messageWindow.SetActive(false);
		this.ToggleMapGenerationApproach(true);
	}

	private void UpdateCircleDrawer()
	{
		if (this.circleDrawer.enabled)
		{
			Transform transform = null;
			float num = this.currentCircleDrawerRadius;
			if (this.levelEditor.levelCreationStep == LevelCreationStep.PlacingObjects)
			{
				transform = this.levelEditor.PlacedProp.transform;
				num *= this.levelEditor.PlacedProp.transform.localScale.x * this.levelEditor.PlacedProp.circleDrawerSizeMultiplier;
			}
			if (this.levelEditor.levelCreationStep == LevelCreationStep.PlacingRoutes)
			{
				transform = this.levelEditor.SelectedCheckpoint.transform;
				num = 20f;
			}
			if (transform == null)
			{
				return;
			}
			this.circleDrawer.transform.position = transform.position;
			Vector3 vector = Camera.main.WorldToScreenPoint(transform.position);
			Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * num);
			Vector3 screenPos2 = Camera.main.WorldToScreenPoint(transform.position - transform.forward * num);
			Vector3 screenPos3 = Camera.main.WorldToScreenPoint(transform.position + transform.right * num);
			Vector3 screenPos4 = Camera.main.WorldToScreenPoint(transform.position - transform.right * num);
			this.directions.Clear();
			if (!this.PosWithinScreen(screenPos))
			{
				Vector3 a = Camera.main.WorldToScreenPoint(transform.position + Vector3.ProjectOnPlane(transform.forward, Vector3.up));
				this.directions.Add((a - vector).normalized);
			}
			if (!this.PosWithinScreen(screenPos2))
			{
				Vector3 a2 = Camera.main.WorldToScreenPoint(transform.position - Vector3.ProjectOnPlane(transform.forward, Vector3.up));
				this.directions.Add((a2 - vector).normalized);
			}
			if (!this.PosWithinScreen(screenPos3))
			{
				Vector3 a3 = Camera.main.WorldToScreenPoint(transform.position + Vector3.ProjectOnPlane(transform.right, Vector3.up));
				this.directions.Add((a3 - vector).normalized);
			}
			if (!this.PosWithinScreen(screenPos4))
			{
				Vector3 a4 = Camera.main.WorldToScreenPoint(transform.position - Vector3.ProjectOnPlane(transform.right, Vector3.up));
				this.directions.Add((a4 - vector).normalized);
			}
			if (this.PosWithinScreen(vector))
			{
				foreach (Vector3 dir in this.directions)
				{
					Vector3 vector2 = this.ProjectVectorOnScreenBorders(vector, dir);
					Ray ray = Camera.main.ScreenPointToRay(vector2);
					Plane plane = new Plane(transform.position, transform.position + Vector3.forward, transform.position + Vector3.right);
					float distance;
					if (plane.Raycast(ray, out distance) && vector2 != Vector3.zero)
					{
						Vector3 point = ray.GetPoint(distance);
						UnityEngine.Debug.DrawRay(point, Vector3.up * 5f, Color.red);
						float num2 = Vector3.Distance(point, transform.position);
						if (num > num2)
						{
							num = num2;
						}
					}
				}
				if (num < this.circleDrawerMinRadius)
				{
					num = this.circleDrawerMinRadius;
				}
				this.circleDrawer.radius = num;
			}
		}
	}

	private bool PosWithinScreen(Vector3 screenPos)
	{
		return screenPos.x <= (float)(Screen.width - 20) && screenPos.x >= 20f && screenPos.y <= (float)(Screen.height - 20) && screenPos.y >= 20f;
	}

	private Vector3 ProjectVectorOnScreenBorders(Vector3 origin, Vector3 dir)
	{
		origin.z = 0f;
		dir.z = 0f;
		Vector3 vector;
		if (LevelEditorUI.LineLineIntersection(out vector, new Vector3(20f, 20f, 0f), new Vector3((float)Screen.width, 0f, 0f), origin, dir * 10000f) && dir.y < 0f && this.PosWithinScreen(vector))
		{
			return vector;
		}
		if (LevelEditorUI.LineLineIntersection(out vector, new Vector3(20f, 20f, 0f), new Vector3(0f, (float)Screen.height, 0f), origin, dir * 10000f) && dir.x < 0f && this.PosWithinScreen(vector))
		{
			return vector;
		}
		if (LevelEditorUI.LineLineIntersection(out vector, new Vector3(20f, (float)(Screen.height - 20), 0f), new Vector3((float)Screen.width, 0f, 0f), origin, dir * 10000f) && dir.y > 0f && this.PosWithinScreen(vector))
		{
			return vector;
		}
		if (LevelEditorUI.LineLineIntersection(out vector, new Vector3((float)(Screen.width - 20), 20f, 0f), new Vector3(0f, (float)Screen.height, 0f), origin, dir * 10000f) && dir.x > 0f && this.PosWithinScreen(vector))
		{
			return vector;
		}
		return Vector3.zero;
	}

	public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
	{
		Vector3 lhs = linePoint2 - linePoint1;
		Vector3 rhs = Vector3.Cross(lineVec1, lineVec2);
		Vector3 lhs2 = Vector3.Cross(lhs, lineVec2);
		float f = Vector3.Dot(lhs, rhs);
		if (Mathf.Abs(f) < 0.0001f && rhs.sqrMagnitude > 0.0001f)
		{
			float d = Vector3.Dot(lhs2, rhs) / rhs.sqrMagnitude;
			intersection = linePoint1 + lineVec1 * d;
			return true;
		}
		intersection = Vector3.zero;
		return false;
	}

	private float GetMinEdgeDistance(Vector3 _pos)
	{
		float num = 10000f;
		if (_pos.x < num)
		{
			num = _pos.x;
		}
		if ((float)Screen.width - _pos.x < num)
		{
			num = (float)Screen.width - _pos.x;
		}
		if (_pos.y < num)
		{
			num = _pos.y;
		}
		if ((float)Screen.height - _pos.y < num)
		{
			num = (float)Screen.height - _pos.y;
		}
		return num;
	}

	private void PoseButtons()
	{
		if (this.moveStampButton.activeSelf && !this.levelEditor.movingStamp)
		{
			Vector3 vector = this.mainCamera.WorldToScreenPoint(this.levelEditor.stampPosition);
			if (vector.z > 0f)
			{
				this.moveStampButton.transform.position = new Vector3(vector.x, vector.y, 0f);
			}
		}
		if (this.sizeStampButton.activeSelf && !this.levelEditor.sizingStamp)
		{
			Vector3 vector2 = this.mainCamera.WorldToScreenPoint(this.levelEditor.stampPosition + this.levelEditor.stampProjector.transform.up * this.levelEditor.stampProjector.orthographicSize);
			if (vector2.z > 0f)
			{
				this.sizeStampButton.transform.position = new Vector3(vector2.x, vector2.y, 0f);
			}
		}
		if (this.removeStampButton.activeSelf)
		{
			Vector3 vector3 = this.mainCamera.WorldToScreenPoint(this.levelEditor.stampPosition - this.levelEditor.stampProjector.transform.up * this.levelEditor.stampProjector.orthographicSize);
			if (vector3.z > 0f)
			{
				this.removeStampButton.transform.position = new Vector3(vector3.x, vector3.y, 0f);
			}
		}
		if (this.removePathButton.activeSelf)
		{
			Vector3 vector4 = this.mainCamera.WorldToScreenPoint(this.levelEditor.lastPathPoint);
			if (vector4.z > 0f)
			{
				this.removePathButton.transform.position = new Vector3(vector4.x, vector4.y, 0f);
			}
		}
		if (this.movePropButton.activeSelf && !this.levelEditor.movingProp)
		{
			Vector3 vector5 = this.mainCamera.WorldToScreenPoint(this.levelEditor.PlacedProp.transform.position + Vector3.ProjectOnPlane(this.levelEditor.PlacedProp.transform.right, Vector3.up).normalized * this.circleDrawer.radius);
			if (vector5.z > 0f)
			{
				this.movePropButton.transform.position = new Vector3(vector5.x, vector5.y, 0f);
			}
		}
		if (this.sizePropButton.activeSelf && !this.levelEditor.sizingProp)
		{
			Vector3 vector6 = this.mainCamera.WorldToScreenPoint(this.levelEditor.PlacedProp.transform.position + Vector3.ProjectOnPlane(this.levelEditor.PlacedProp.transform.forward, Vector3.up).normalized * this.circleDrawer.radius);
			if (vector6.z > 0f)
			{
				this.sizePropButton.transform.position = new Vector3(vector6.x, vector6.y, 0f);
			}
		}
		if (this.removePropButton.activeSelf)
		{
			Vector3 vector7 = this.mainCamera.WorldToScreenPoint(this.levelEditor.PlacedProp.transform.position - Vector3.ProjectOnPlane(this.levelEditor.PlacedProp.transform.forward, Vector3.up).normalized * this.circleDrawer.radius);
			if (vector7.z > 0f)
			{
				this.removePropButton.transform.position = new Vector3(vector7.x, vector7.y, 0f);
			}
		}
		if (this.liftPropButton.activeSelf && !this.levelEditor.liftingProp)
		{
			Vector3 vector8 = this.mainCamera.WorldToScreenPoint(this.levelEditor.PlacedProp.transform.position - Vector3.ProjectOnPlane(this.levelEditor.PlacedProp.transform.right, Vector3.up).normalized * this.circleDrawer.radius);
			if (vector8.z > 0f)
			{
				this.liftPropButton.transform.position = new Vector3(vector8.x, vector8.y, 0f);
			}
		}
		if (this.scaleValueText.gameObject.activeSelf)
		{
			Vector3 vector9 = this.mainCamera.WorldToScreenPoint(this.levelEditor.PlacedProp.transform.position);
			if (vector9.z > 0f)
			{
				this.scaleValueText.transform.position = new Vector3(vector9.x, vector9.y, 0f);
			}
		}
		if (this.moveCheckpointButton.activeSelf && !this.levelEditor.movingCheckpoint)
		{
			Vector3 vector10 = this.mainCamera.WorldToScreenPoint(this.levelEditor.SelectedCheckpoint.transform.position + Vector3.ProjectOnPlane(this.levelEditor.SelectedCheckpoint.transform.right, Vector3.up).normalized * 20f);
			if (vector10.z > 0f)
			{
				this.moveCheckpointButton.transform.position = new Vector3(vector10.x, vector10.y, 0f);
			}
		}
		if (this.removeCheckpointButton.activeSelf)
		{
			Vector3 vector11 = this.mainCamera.WorldToScreenPoint(this.levelEditor.SelectedCheckpoint.transform.position - Vector3.ProjectOnPlane(this.levelEditor.SelectedCheckpoint.transform.right, Vector3.up).normalized * 20f);
			if (vector11.z > 0f)
			{
				this.removeCheckpointButton.transform.position = new Vector3(vector11.x, vector11.y, 0f);
			}
		}
		if (this.applyCheckpointButton.activeSelf)
		{
			Vector3 vector12 = this.mainCamera.WorldToScreenPoint(this.levelEditor.SelectedCheckpoint.transform.position + Vector3.ProjectOnPlane(this.levelEditor.SelectedCheckpoint.transform.forward, Vector3.up).normalized * 20f);
			if (vector12.z > 0f)
			{
				this.applyCheckpointButton.transform.position = new Vector3(vector12.x, vector12.y, 0f);
			}
		}
		if (this.moveMudStampButton.activeSelf && !this.levelEditor.movingMudStamp)
		{
			Vector3 vector13 = this.mainCamera.WorldToScreenPoint(this.levelEditor.SelectedMudStamp.stampPosition + Vector3.ProjectOnPlane(this.levelEditor.SelectedMudStamp.stampIndicator.transform.right, Vector3.up).normalized * this.levelEditor.SelectedMudStamp.stampSize);
			if (vector13.z > 0f)
			{
				this.moveMudStampButton.transform.position = new Vector3(vector13.x, vector13.y, 0f);
			}
		}
		if (this.sizeMudStampButton.activeSelf && !this.levelEditor.sizingMudStamp)
		{
			Vector3 vector14 = this.mainCamera.WorldToScreenPoint(this.levelEditor.SelectedMudStamp.stampPosition + Vector3.ProjectOnPlane(this.levelEditor.SelectedMudStamp.stampIndicator.transform.forward, Vector3.up).normalized * this.levelEditor.SelectedMudStamp.stampSize);
			if (vector14.z > 0f)
			{
				this.sizeMudStampButton.transform.position = new Vector3(vector14.x, vector14.y, 0f);
			}
		}
		if (this.removeMudStampButton.activeSelf)
		{
			Vector3 vector15 = this.mainCamera.WorldToScreenPoint(this.levelEditor.SelectedMudStamp.stampPosition + Vector3.ProjectOnPlane(-this.levelEditor.SelectedMudStamp.stampIndicator.transform.right, Vector3.up).normalized * this.levelEditor.SelectedMudStamp.stampSize);
			if (vector15.z > 0f)
			{
				this.removeMudStampButton.transform.position = new Vector3(vector15.x, vector15.y, 0f);
			}
		}
		if (this.applyMudStampButton.activeSelf)
		{
			Vector3 vector16 = this.mainCamera.WorldToScreenPoint(this.levelEditor.SelectedMudStamp.stampPosition + Vector3.ProjectOnPlane(-this.levelEditor.SelectedMudStamp.stampIndicator.transform.forward, Vector3.up).normalized * this.levelEditor.SelectedMudStamp.stampSize);
			if (vector16.z > 0f)
			{
				this.applyMudStampButton.transform.position = new Vector3(vector16.x, vector16.y, 0f);
			}
		}
	}

	public void ChangeLevelGenerationStep(int stepID)
	{
		this.levelEditor.ChangeLevelCreationStep((LevelCreationStep)stepID);
	}

	public void GenerateRandomSeed()
	{
		int num = UnityEngine.Random.Range(0, 10000);
		this.seedSlider.value = (float)num;
	}

	public void SetPattern(int id)
	{
		this.selectedTerrainPatternID = id;
		this.UpdatePatternButtons();
	}

	public void SetPaintTextureID(int id)
	{
		this.levelEditor.ChangeModPaintTextureID(id);
		this.UpdatePaintTextureButtons();
	}

	public void ChangeStampStrengthDirectly(float value)
	{
		this.levelEditor.modStrength = value;
	}

	public void ChangeStampID(int ID)
	{
		this.levelEditor.stampTextureID = ID;
		this.levelEditor.UpdateStampProjector();
		this.UpdateStampButtons();
	}

	public void ChangePathPatternID(int ID)
	{
		this.levelEditor.selectedPathPattern = ID;
		this.levelEditor.UpdatePathSettings();
		this.UpdatePathPatternButtons();
	}

	public void ChangePathWidth(float width)
	{
		this.levelEditor.pathWidth = width;
		this.levelEditor.UpdatePathSettings();
	}

	private void UpdateStampButtons()
	{
		for (int i = 0; i < this.stampButtons.Length; i++)
		{
			this.stampButtons[i].color = ((i != this.levelEditor.stampTextureID) ? this.deselectedButtonColor : this.selectedButtonColor);
		}
	}

	private void UpdatePathPatternButtons()
	{
		for (int i = 0; i < this.pathPatternButtons.Length; i++)
		{
			this.pathPatternButtons[i].color = ((i != this.levelEditor.selectedPathPattern) ? this.deselectedButtonColor : this.selectedButtonColor);
		}
	}

	public void ChangeModAction(int actionID)
	{
		this.levelEditor.ChangeModAction((ModAction)actionID);
	}

	public void ChangeModType(int typeID)
	{
		this.levelEditor.ChangeModType((TerrainModifyingType)typeID);
	}

	public void RemoveMod()
	{
		this.levelEditor.RemoveStamp();
	}

	public void PreviewMod()
	{
		this.levelEditor.addingExtraObjectID = this.addingExtraObjectDropdown.value - 1;
		if (this.levelEditor.terrainModType == TerrainModifyingType.Stamp)
		{
			this.levelEditor.PreviewStamp();
		}
		if (this.levelEditor.terrainModType == TerrainModifyingType.Path)
		{
			this.levelEditor.PreviewPath();
		}
	}

	public void ApplyMod()
	{
		this.levelEditor.addingExtraObjectID = this.addingExtraObjectDropdown.value - 1;
		if (this.levelEditor.terrainModType == TerrainModifyingType.Stamp)
		{
			this.levelEditor.ApplyAndSaveStamp();
		}
		if (this.levelEditor.terrainModType == TerrainModifyingType.Path)
		{
			this.levelEditor.ApplyAndSavePath();
		}
	}

	public void GenerateTerrain()
	{
		this.levelEditor.seed = (int)this.seedSlider.value;
		this.levelEditor.terrainSize = this.sizeSlider.value;
		this.levelEditor.bumpsStrength = this.bumpsStrengthSlider.value;
		this.levelEditor.flatCenter = this.flatCenterToggle.isOn;
		this.levelEditor.treesDensity = this.treeDensitySlider.value;
		this.levelEditor.treesByEdgesOnly = this.onlyOnEdgesToggle.isOn;
		this.levelEditor.mainTextureID = this.selectedTerrainPatternID;
		this.levelEditor.waterEnabled = this.enableWaterToggle.isOn;
		this.levelEditor.waterHeight = this.waterHeightSlider.value;
		this.levelEditor.frozenWater = this.frozenWaterToggle.isOn;
		this.levelEditor.usedExtraObjects[0] = this.usedExtraObjects[0].DeepCopy();
		this.levelEditor.usedExtraObjects[1] = this.usedExtraObjects[1].DeepCopy();
		this.levelEditor.usedExtraObjects[2] = this.usedExtraObjects[2].DeepCopy();
		this.levelEditor.GenerateTerrain(false);
	}

	public void GenerateRandomTerrain()
	{
		this.levelEditor.GenerateRandomTerrain();
	}

	public void SelectProp(int propID)
	{
		this.levelEditor.SelectProp(propID);
		this.propSelectorWindow.SetActive(false);
	}

	public void OpenPropSelector()
	{
		this.BuildPropsGrid(this.selectedPropCategory);
		this.propSelectorWindow.SetActive(true);
	}

	public void ApplyProp()
	{
		this.levelEditor.ApplyProp();
	}

	public void DuplicateProp()
	{
		this.levelEditor.DuplicateProp();
	}

	public void ResetPropScale()
	{
		this.levelEditor.ResetPropScale();
	}

	public void AlignBySlope(bool align)
	{
		this.levelEditor.SetAlignBySlope(align);
	}

	public void SetSnapping(bool snap)
	{
	}

	public void RemoveProp()
	{
		this.levelEditor.RemoveProp();
	}

	public void ChangePropCategory(int newCategory)
	{
		this.selectedPropCategory = newCategory;
		this.BuildPropsGrid(this.selectedPropCategory);
		for (int i = 0; i < this.propCategoryTabs.Length; i++)
		{
			this.propCategoryTabs[i].color = ((i != newCategory) ? this.deselectedPropCategoryTabColor : this.selectedPropCategoryTabColor);
		}
	}

	public void ToggleExtra0(bool on)
	{
		if (this.levelEditor.PlacedProp != null)
		{
			this.levelEditor.PlacedProp.ToggleExtra0(on);
		}
	}

	public void ToggleExtra1(bool on)
	{
		if (this.levelEditor.PlacedProp != null)
		{
			this.levelEditor.PlacedProp.ToggleExtra1(on);
		}
	}

	public void ShowUploadWarning()
	{
		this.uploadWarningWindow.SetActive(true);
	}

	public void ShowRemovalWarning()
	{
		this.removalWarningWindow.SetActive(true);
	}

	public void UploadMap(bool makeVisible)
	{
		this.mapUploadingWindow.SetActive(true);
		this.levelEditor.mapVisible = makeVisible;
		this.levelEditor.UploadMap();
	}

	public void RemoveMap()
	{
		this.mapRemovalWindow.SetActive(true);
		this.levelEditor.RemoveMap();
	}

	public void MapMetaChanged()
	{
		this.levelEditor.mapName = this.mapNameField.text;
		this.levelEditor.mapDescription = this.mapDescriptionField.text;
		this.uploadButton.interactable = (this.spawnPointsCount >= 8 && this.mapNameField.text != string.Empty && this.mapDescriptionField.text != string.Empty);
		this.metaWarning.SetActive(this.mapNameField.text == string.Empty || this.mapDescriptionField.text == string.Empty);
	}

	public void CreateNewRoute()
	{
		this.levelEditor.CreateNewRoute();
	}

	public void SelectRoute(int id)
	{
		this.levelEditor.SelectRoute(id);
	}

	private void UpdateRouteList()
	{
		foreach (RouteButton routeButton in this.routeButtonsParent.GetComponentsInChildren<RouteButton>(false))
		{
			UnityEngine.Object.Destroy(routeButton.gameObject);
		}
		for (int j = 0; j < this.levelEditor.routes.Count; j++)
		{
			RouteButton component = UnityEngine.Object.Instantiate<RouteButton>(this.exampleRouteButton, this.exampleRouteButton.transform.parent).GetComponent<RouteButton>();
			component.routeNameText.text = this.levelEditor.routes[j].routeName;
			component.GetComponent<Image>().color = ((j != this.levelEditor.selectedRouteID) ? this.deselectedRouteButtonColor : this.selectedRouteButtonColor);
			int _id = j;
			component.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.SelectRoute(_id);
			});
			component.gameObject.SetActive(true);
			component.transform.SetSiblingIndex(2);
		}
	}

	public void RouteNameChanged(string routeName)
	{
		this.levelEditor.ChangeCurrentRouteName(routeName);
		this.UpdateRouteList();
	}

	public void RemoveCurrentRoute()
	{
		this.levelEditor.RemoveCurrentRoute();
		this.UpdateRouteList();
	}

	public void ApplyRoute()
	{
		this.levelEditor.ApplyRoute();
	}

	public void ShowMenu()
	{
		this.menu.SetActive(true);
	}

	public void LeaveToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void ToggleMapGenerationApproach(bool random)
	{
		this.randomGenerationButton.color = ((!random) ? this.deselectedButtonColor : this.selectedButtonColor);
		this.advancedGenerationButton.color = ((!random) ? this.selectedButtonColor : this.deselectedButtonColor);
		this.randomGenerationWindow.SetActive(random);
		this.advancedGenerationWindow.SetActive(!random);
	}

	public void AddSpawnPoint()
	{
		this.levelEditor.ChangeLevelCreationStep(LevelCreationStep.PlacingObjects);
		this.SelectProp(this.levelEditor.spawnPointPropID);
	}

	private void UpdateMudStampPatternButtons()
	{
		int num = -1;
		if (this.levelEditor.SelectedMudStamp != null)
		{
			num = this.levelEditor.SelectedMudStamp.stampTextureID;
		}
		for (int i = 0; i < this.mudStampPatterns.Length; i++)
		{
			this.mudStampPatterns[i].effectColor = ((i != num) ? this.deselectedModTypeButtonColor : this.selectedModTypeButtonColor);
		}
	}

	public void ChangeMudStampPattern(int ID)
	{
		this.levelEditor.ChangeMudStampPattern(ID);
		this.UpdateMudStampPatternButtons();
	}

	public void ChangeMudDepth(float depth)
	{
		this.levelEditor.ChangeMudDepth(depth);
	}

	public void ChangeMudViscosity(float viscosity)
	{
		this.levelEditor.ChangeMudViscosity(viscosity);
	}

	public void RemoveAllMudStamps()
	{
		this.levelEditor.RemoveAllMudStamps();
	}

	private void OnModActionChanged()
	{
		this.stampSettingsPanel.SetActive(true);
		this.terrainPatternPanel.SetActive(false);
		this.objectToAddWindow.SetActive(false);
		switch (this.levelEditor.modAction)
		{
		case ModAction.LandscapeRaising:
			this.modificationTypeText.text = "Raising landscape";
			this.modStrengthText.text = "Height";
			break;
		case ModAction.LandscapeLowering:
			this.modificationTypeText.text = "Lowering landscape";
			this.modStrengthText.text = "Height";
			break;
		case ModAction.Smoothing:
			this.modificationTypeText.text = "Flattening landscape";
			this.modStrengthText.text = "Flattening power";
			break;
		case ModAction.Painting:
			this.modificationTypeText.text = "Painting pattern";
			this.modStrengthText.text = "Paint power";
			this.terrainPatternPanel.SetActive(true);
			break;
		case ModAction.AddingExtraObjects:
			this.modificationTypeText.text = "Adding extra objects";
			this.modStrengthText.text = "Density";
			this.UpdateAddingExtraObjectDropdown();
			this.objectToAddWindow.SetActive(true);
			break;
		case ModAction.RemovingExtraObjects:
			this.modificationTypeText.text = "Cutting extra objects";
			this.modStrengthText.text = "Cut power";
			break;
		}
		this.UpdateStampActionButtons();
	}

	private void OnModTypeChanged()
	{
		this.tapOnScreenText.SetActive(this.levelEditor.stampState == StampState.NotPlaced && this.levelEditor.terrainModType == TerrainModifyingType.Stamp);
		this.tapTodrawPathText.SetActive(this.levelEditor.pathState == PathState.NotDrawn && this.levelEditor.terrainModType == TerrainModifyingType.Path);
		this.drawPathText.SetActive(this.levelEditor.pathState == PathState.Drawing && this.levelEditor.terrainModType == TerrainModifyingType.Path);
		this.UpdateModTypeButtons();
		this.pathSettingsPanel.SetActive(this.levelEditor.terrainModType == TerrainModifyingType.Path);
		this.stampSelectorWindow.SetActive(this.levelEditor.terrainModType == TerrainModifyingType.Stamp);
		this.pathSelectorWindow.SetActive(this.levelEditor.terrainModType == TerrainModifyingType.Path);
	}

	private void OnPathStateChanged()
	{
		this.tapTodrawPathText.SetActive(this.levelEditor.pathState == PathState.NotDrawn);
		this.drawPathText.SetActive(this.levelEditor.pathState == PathState.Drawing);
		this.modsToolsPanel.interactable = (this.levelEditor.pathState == PathState.FinishedDrawing);
		this.removePathButton.SetActive(this.levelEditor.pathState == PathState.FinishedDrawing);
		this.placeStampWarning.SetActive(this.levelEditor.pathState == PathState.NotDrawn);
	}

	private void OnLevelCreationStepChanged()
	{
		this.moveStampButton.SetActive(false);
		this.sizeStampButton.SetActive(false);
		this.removeStampButton.SetActive(false);
		this.removePathButton.SetActive(false);
		this.movePropButton.SetActive(false);
		this.sizePropButton.SetActive(false);
		this.removePropButton.SetActive(false);
		this.liftPropButton.SetActive(false);
		this.scaleValueText.gameObject.SetActive(false);
		this.propSelectorWindow.SetActive(false);
		this.modsResetWarningWindow.SetActive(false);
		LevelCreationStep levelCreationStep = this.levelEditor.levelCreationStep;
		this.generateTerrainWindow.SetActive(levelCreationStep == LevelCreationStep.Generation);
		this.modifyExtraObjectWindow.SetActive(false);
		this.placingPropsWindow.SetActive(levelCreationStep == LevelCreationStep.PlacingObjects);
		this.modifyTerrainWindow.SetActive(levelCreationStep == LevelCreationStep.Modifying);
		this.UpdateStampButtons();
		this.UpdateExtraObjectsButtons();
		this.tapOnScreenText.SetActive(levelCreationStep == LevelCreationStep.Modifying && this.levelEditor.stampState == StampState.NotPlaced && this.levelEditor.terrainModType == TerrainModifyingType.Stamp);
		this.tapTodrawPathText.SetActive(levelCreationStep == LevelCreationStep.Modifying && this.levelEditor.pathState == PathState.NotDrawn && this.levelEditor.terrainModType == TerrainModifyingType.Path);
		this.drawPathText.SetActive(levelCreationStep == LevelCreationStep.Modifying && this.levelEditor.pathState == PathState.Drawing && this.levelEditor.terrainModType == TerrainModifyingType.Path);
		this.tapToPlacePropText.SetActive(false);
		this.selectPropText.SetActive(true);
		this.finalizingMapWindow.SetActive(levelCreationStep == LevelCreationStep.Finalizing);
		this.addingMudWindow.SetActive(levelCreationStep == LevelCreationStep.AddingMud);
		this.UpdatePatternButtons();
		this.UpdatePaintTextureButtons();
		this.UpdateStampActionButtons();
		this.UpdateModTypeButtons();
		string mapName = this.levelEditor.mapName;
		string mapDescription = this.levelEditor.mapDescription;
		this.mapDescriptionField.text = mapDescription;
		this.mapNameField.text = mapName;
		this.uploadWarningWindow.SetActive(false);
		this.removalWarningWindow.SetActive(false);
		if (levelCreationStep == LevelCreationStep.Finalizing)
		{
			this.spawnPointsCount = this.levelEditor.GetSpawnPoints().Length;
			int num = this.levelEditor.CountProps();
			this.propsCountText.text = num.ToString();
			this.spawnPointsCountText.text = this.spawnPointsCount + "/8";
			this.uploadButton.interactable = (this.spawnPointsCount >= 8 && this.mapNameField.text != string.Empty && this.mapDescriptionField.text != string.Empty);
			this.metaWarning.SetActive(this.mapNameField.text == string.Empty || this.mapDescriptionField.text == string.Empty);
			this.spawnPointsWarning.SetActive(this.spawnPointsCount < 8);
		}
		this.placingRoutesWindow.SetActive(levelCreationStep == LevelCreationStep.PlacingRoutes);
		this.tapToPlaceWaypointText.SetActive(false);
		this.moveCheckpointButton.SetActive(false);
		this.removeCheckpointButton.SetActive(false);
		this.applyCheckpointButton.SetActive(false);
		this.mapUploadStatusText.text = ((!this.levelEditor.mapUploaded) ? "Not uploaded" : "Uploaded");
		this.mapUploadStatusText.color = ((!this.levelEditor.mapUploaded) ? Color.red : Color.green);
		this.mapVisiblityStatusText.text = ((!this.levelEditor.mapVisible) ? "Hidden" : "Visible");
		this.mapVisiblityStatusText.color = ((!this.levelEditor.mapVisible) ? Color.blue : Color.green);
		this.mapRemovalButton.interactable = this.levelEditor.mapUploaded;
		if (!this.levelEditor.mapUploaded)
		{
			this.mapVisiblityStatusText.text = "Not uploaded";
			this.mapVisiblityStatusText.color = Color.red;
		}
	}

	private void OnStampStateChanged()
	{
		this.modsToolsPanel.interactable = false;
		this.placeStampWarning.SetActive(true);
		this.sizeStampButton.SetActive(false);
		this.moveStampButton.SetActive(false);
		this.removeStampButton.SetActive(false);
		StampState stampState = this.levelEditor.stampState;
		if (stampState != StampState.NotPlaced)
		{
			if (stampState == StampState.Placed)
			{
				this.modsToolsPanel.interactable = true;
				this.placeStampWarning.SetActive(false);
				this.tapOnScreenText.gameObject.SetActive(false);
				this.sizeStampButton.SetActive(true);
				this.moveStampButton.SetActive(true);
				this.removeStampButton.SetActive(true);
			}
		}
		else if (this.levelEditor.levelCreationStep == LevelCreationStep.Modifying)
		{
			this.tapOnScreenText.gameObject.SetActive(true);
		}
	}

	private void OnModsResetWarning()
	{
		this.modsResetWarningWindow.SetActive(true);
	}

	public void OnProcessingTerrainStarted(string text)
	{
		this.processingTerrainWindow.SetActive(true);
		this.processingTerrainText.text = text;
	}

	public void OnProcessingTerrainFinished()
	{
		this.processingTerrainWindow.SetActive(false);
	}

	private void OnTerrainValuesRandomized()
	{
		this.seedSlider.value = (float)this.levelEditor.seed;
		this.sizeSlider.value = this.levelEditor.terrainSize;
		this.bumpsStrengthSlider.value = this.levelEditor.bumpsStrength;
		this.treeDensitySlider.value = this.levelEditor.treesDensity;
		this.onlyOnEdgesToggle.isOn = this.levelEditor.treesByEdgesOnly;
		this.flatCenterToggle.isOn = this.levelEditor.flatCenter;
		this.enableWaterToggle.isOn = this.levelEditor.waterEnabled;
		this.waterHeightSlider.value = this.levelEditor.waterHeight;
		this.usedExtraObjects[0] = this.levelEditor.usedExtraObjects[0].DeepCopy();
		this.usedExtraObjects[1] = this.levelEditor.usedExtraObjects[1].DeepCopy();
		this.usedExtraObjects[2] = this.levelEditor.usedExtraObjects[2].DeepCopy();
		this.UpdateExtraObjectsButtons();
	}

	private void OnSelectedPropChanged()
	{
		if (this.levelEditor.selectedPropID == -1)
		{
			this.selectedPropNameText.text = "NONE";
		}
		else
		{
			this.selectedPropNameText.text = this.editorResources.propsDictionary[this.levelEditor.selectedPropID].propName;
			this.extra0NameText.text = this.editorResources.propsDictionary[this.levelEditor.selectedPropID].extra0Name;
			this.extra1NameText.text = this.editorResources.propsDictionary[this.levelEditor.selectedPropID].extra1Name;
		}
	}

	private void OnPropStateChanged()
	{
		this.tapToPlacePropText.SetActive(this.levelEditor.propState == PropPlacementState.Selected);
		this.selectPropText.SetActive(this.levelEditor.propState == PropPlacementState.NotSelected);
		this.movePropButton.SetActive(this.levelEditor.propState == PropPlacementState.Placed);
		this.sizePropButton.SetActive(this.levelEditor.propState == PropPlacementState.Placed);
		this.removePropButton.SetActive(this.levelEditor.propState == PropPlacementState.Placed);
		this.liftPropButton.SetActive(this.levelEditor.propState == PropPlacementState.Placed);
		this.scaleValueText.gameObject.SetActive(this.levelEditor.propState == PropPlacementState.Placed);
		this.circleDrawer.enabled = (this.levelEditor.propState == PropPlacementState.Placed);
		this.propInteractionMenu.interactable = (this.levelEditor.propState == PropPlacementState.Placed);
		if (this.levelEditor.propState == PropPlacementState.Placed)
		{
			if (this.levelEditor.PlacedProp != null)
			{
				this.extra0Toggle.isOn = this.levelEditor.PlacedProp.extra0Enabled;
				this.extra1Toggle.isOn = this.levelEditor.PlacedProp.extra1Enabled;
				this.extra0Toggle.gameObject.SetActive(this.levelEditor.PlacedProp.extra0 != null);
				this.extra1Toggle.gameObject.SetActive(this.levelEditor.PlacedProp.extra1 != null);
			}
		}
		else
		{
			this.extra0Toggle.gameObject.SetActive(false);
			this.extra1Toggle.gameObject.SetActive(false);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.placementWindowsParent);
	}

	private void OnPlacedObjectScaleChanged(float newScale)
	{
		this.scaleValueText.text = "Size: " + Mathf.Round(newScale * 10f) / 10f;
		this.scaleValueText.color = Color.white;
	}

	private void OnTerrainGenerated()
	{
		this.modifyTerrainButton.interactable = true;
		this.placeObjectsButton.interactable = true;
		this.finalizeButton.interactable = true;
		this.placeRoutesButton.interactable = true;
		this.addMudButton.interactable = true;
	}

	private void OnRouteCreationStepChanged()
	{
		this.UpdateRouteList();
		this.tapToPlaceWaypointText.SetActive(this.levelEditor.routeCreationStep == RouteCreationStep.Selected);
		this.routeSettingsWindow.interactable = (this.levelEditor.routeCreationStep == RouteCreationStep.Selected);
		if (this.levelEditor.selectedRouteID != -1)
		{
			this.routeNameInputField.text = this.levelEditor.routes[this.levelEditor.selectedRouteID].routeName;
		}
		this.moveCheckpointButton.SetActive(this.levelEditor.routeCreationStep == RouteCreationStep.ModifyingCheckpoint);
		this.removeCheckpointButton.SetActive(this.levelEditor.routeCreationStep == RouteCreationStep.ModifyingCheckpoint);
		this.applyCheckpointButton.SetActive(this.levelEditor.routeCreationStep == RouteCreationStep.ModifyingCheckpoint);
		this.circleDrawer.enabled = (this.levelEditor.routeCreationStep == RouteCreationStep.ModifyingCheckpoint);
	}

	private void OnSelectedMudStampChanged()
	{
		this.moveMudStampButton.SetActive(this.levelEditor.mudStampState == MudStampState.Selected);
		this.removeMudStampButton.SetActive(this.levelEditor.mudStampState == MudStampState.Selected);
		this.sizeMudStampButton.SetActive(this.levelEditor.mudStampState == MudStampState.Selected);
		this.applyMudStampButton.SetActive(this.levelEditor.mudStampState == MudStampState.Selected);
		this.mudStampSettingsGroup.interactable = (this.levelEditor.mudStampState == MudStampState.Selected);
		this.mudWarningMessage.SetActive(this.levelEditor.mudStampState == MudStampState.NotSelected);
		if (this.levelEditor.mudStampState == MudStampState.Selected)
		{
			this.mudDepthSlider.value = this.levelEditor.SelectedMudStamp.mudDepth;
			this.mudViscositySlider.value = this.levelEditor.SelectedMudStamp.mudViscosity;
		}
		this.mudStampsCountText.text = this.levelEditor.mudStamps.Count + "/3";
		this.UpdateMudStampPatternButtons();
	}

	public void ModifyExtraObject(int objID)
	{
		this.extraObjectDensitySlider.value = this.usedExtraObjects[objID].density;
		this.extraObjectEdgesOnlyToggle.isOn = this.usedExtraObjects[objID].onlyByEdges;
		this.extraObjectDropdown.value = this.usedExtraObjects[objID].arrayID + 1;
		this.extraObjectDropdown.ClearOptions();
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		for (int i = -1; i < this.editorResources.extraObjectsDictionary.Length; i++)
		{
			Dropdown.OptionData optionData = new Dropdown.OptionData();
			if (i == -1)
			{
				optionData.text = "NONE";
			}
			else
			{
				optionData.text = this.editorResources.extraObjectsDictionary[i].displayedName;
			}
			list.Add(optionData);
		}
		this.extraObjectDropdown.options = list;
		this.modifyExtraObjectWindow.SetActive(true);
		this.lastExtraObjectSelected = objID;
	}

	private void UpdateAddingExtraObjectDropdown()
	{
		this.addingExtraObjectDropdown.ClearOptions();
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		for (int i = -1; i < this.editorResources.extraObjectsDictionary.Length; i++)
		{
			Dropdown.OptionData optionData = new Dropdown.OptionData();
			if (i == -1)
			{
				optionData.text = "Trees";
			}
			else
			{
				optionData.text = this.editorResources.extraObjectsDictionary[i].displayedName;
			}
			list.Add(optionData);
		}
		this.addingExtraObjectDropdown.options = list;
	}

	[ContextMenu("Update extra obj buttons")]
	private void UpdateExtraObjectsButtons()
	{
		for (int i = 0; i < this.usedExtraObjects.Length; i++)
		{
			ExtraObjectArray extraObjectArray = null;
			if (this.usedExtraObjects[i].arrayID > -1)
			{
				extraObjectArray = this.editorResources.extraObjectsDictionary[this.usedExtraObjects[i].arrayID];
			}
			string text = "NONE";
			if (extraObjectArray != null)
			{
				text = extraObjectArray.displayedName;
				text += "\n";
				text = text + "Density: " + (Mathf.Round(this.usedExtraObjects[i].density * 10f) / 10f).ToString();
				if (this.usedExtraObjects[i].onlyByEdges)
				{
					text += ". By edges";
				}
			}
			this.extraObjectButtons[i].extraObjectNameText.text = text;
		}
	}

	private void UpdateModTypeButtons()
	{
		this.stampModTypeButton.color = ((this.levelEditor.terrainModType != TerrainModifyingType.Stamp) ? this.deselectedModTypeButtonColor : this.selectedModTypeButtonColor);
		this.pathModTypeButton.color = ((this.levelEditor.terrainModType != TerrainModifyingType.Path) ? this.deselectedModTypeButtonColor : this.selectedModTypeButtonColor);
	}

	private void UpdatePatternButtons()
	{
		for (int i = 0; i < this.patternImages.Length; i++)
		{
			this.patternImages[i].color = ((i != this.selectedTerrainPatternID) ? this.deselectedButtonColor : this.selectedButtonColor);
		}
	}

	private void UpdateStampActionButtons()
	{
		for (int i = 0; i < this.modActionButtons.Length; i++)
		{
			this.modActionButtons[i].effectColor = ((i != (int)this.levelEditor.modAction) ? this.deselectedButtonColor : this.selectedButtonColor);
		}
	}

	private void UpdatePaintTextureButtons()
	{
		for (int i = 0; i < this.paintTextureButtons.Length; i++)
		{
			this.paintTextureButtons[i].color = ((i != this.levelEditor.modPaintTextureID) ? this.deselectedButtonColor : this.selectedButtonColor);
		}
	}

	private void BuildPropsGrid(int propType)
	{
		this.propButtonExample.gameObject.SetActive(false);
		foreach (PropButton propButton in this.propButtonsParent.GetComponentsInChildren<PropButton>(false))
		{
			UnityEngine.Object.Destroy(propButton.gameObject);
		}
		for (int j = 0; j < this.editorResources.propsDictionary.Length; j++)
		{
			if (this.editorResources.propsDictionary[j].propType == (PropType)propType)
			{
				PropButton component = UnityEngine.Object.Instantiate<GameObject>(this.propButtonExample.gameObject, this.propButtonsParent.transform).GetComponent<PropButton>();
				component.propNameText.text = this.editorResources.propsDictionary[j].propName;
				component.propImage.sprite = this.editorResources.propsDictionary[j].propImage;
				component.propID = j;
				int propID = j;
				component.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.SelectProp(propID);
				});
				component.gameObject.SetActive(true);
			}
		}
	}

	public void SaveExtraObjectChanges()
	{
		this.usedExtraObjects[this.lastExtraObjectSelected].density = this.extraObjectDensitySlider.value;
		this.usedExtraObjects[this.lastExtraObjectSelected].onlyByEdges = this.extraObjectEdgesOnlyToggle.isOn;
		this.usedExtraObjects[this.lastExtraObjectSelected].arrayID = this.extraObjectDropdown.value - 1;
		this.modifyExtraObjectWindow.SetActive(false);
		this.UpdateExtraObjectsButtons();
	}

	private LevelEditor levelEditor;

	private LevelEditorResources editorResources;

	private Camera mainCamera;

	public Button modifyTerrainButton;

	public Button placeObjectsButton;

	public Button finalizeButton;

	public Button placeRoutesButton;

	public Button addMudButton;

	public GameObject messageWindow;

	public Text messageText;

	public Text messageCaption;

	[Header("Step 1 - terrain generation")]
	public GameObject generateTerrainWindow;

	public Slider seedSlider;

	public Slider sizeSlider;

	public Slider bumpsStrengthSlider;

	public Slider treeDensitySlider;

	public Toggle onlyOnEdgesToggle;

	public Toggle flatCenterToggle;

	public GameObject modifyExtraObjectWindow;

	public Dropdown extraObjectDropdown;

	public Slider extraObjectDensitySlider;

	public Toggle extraObjectEdgesOnlyToggle;

	public ExtraObjectButton[] extraObjectButtons;

	public Image[] patternImages;

	public int selectedTerrainPatternID;

	public Toggle enableWaterToggle;

	public Slider waterHeightSlider;

	public Toggle frozenWaterToggle;

	public Image randomGenerationButton;

	public Image advancedGenerationButton;

	public GameObject randomGenerationWindow;

	public GameObject advancedGenerationWindow;

	private int lastExtraObjectSelected;

	private ExtraObjectReference[] usedExtraObjects;

	[Header("Step 2 - terrain modifying")]
	public GameObject modifyTerrainWindow;

	public GameObject tapOnScreenText;

	public GameObject tapTodrawPathText;

	public GameObject drawPathText;

	public Image[] stampButtons;

	public Image[] pathPatternButtons;

	public Outline[] modActionButtons;

	public Image[] paintTextureButtons;

	public Color selectedButtonColor;

	public Color deselectedButtonColor;

	public GameObject placeStampWarning;

	public CanvasGroup modsToolsPanel;

	public Text modificationTypeText;

	public Slider modStrengthSlider;

	public GameObject stampSettingsPanel;

	public GameObject pathSettingsPanel;

	public GameObject terrainPatternPanel;

	public GameObject moveStampButton;

	public GameObject sizeStampButton;

	public GameObject removeStampButton;

	public GameObject removePathButton;

	public GameObject processingTerrainWindow;

	public Text processingTerrainText;

	public GameObject modsResetWarningWindow;

	public Text modStrengthText;

	public Dropdown addingExtraObjectDropdown;

	public GameObject objectToAddWindow;

	public Image stampModTypeButton;

	public Image pathModTypeButton;

	public Color selectedModTypeButtonColor;

	public Color deselectedModTypeButtonColor;

	public GameObject stampSelectorWindow;

	public GameObject pathSelectorWindow;

	[Header("Step 3 - placing props")]
	public GameObject placingPropsWindow;

	public GameObject propSelectorWindow;

	public PropButton propButtonExample;

	public GameObject propButtonsParent;

	public Text selectedPropNameText;

	public GameObject tapToPlacePropText;

	public GameObject selectPropText;

	public GameObject movePropButton;

	public GameObject sizePropButton;

	public GameObject removePropButton;

	public GameObject liftPropButton;

	public Text scaleValueText;

	public CanvasGroup propInteractionMenu;

	public Toggle alignBySlopeToggle;

	public Toggle snapToggle;

	public Image[] propCategoryTabs;

	public Color selectedPropCategoryTabColor;

	public Color deselectedPropCategoryTabColor;

	public Toggle extra0Toggle;

	public Toggle extra1Toggle;

	public Text extra0NameText;

	public Text extra1NameText;

	public RectTransform placementWindowsParent;

	public GameObject devPropsTab;

	[HideInInspector]
	public CircleDrawer circleDrawer;

	public float circleDrawerWidth = 0.5f;

	public float circleDrawerBaseRadius = 5f;

	public float circleDrawerMinRadius = 2f;

	public int circleDrawerPoints = 30;

	public Material circleDrawerMaterial;

	public List<Vector3> directions = new List<Vector3>();

	private int selectedPropCategory;

	[HideInInspector]
	public float currentCircleDrawerRadius;

	[Header("Step 4 - placing routes")]
	public GameObject placingRoutesWindow;

	public RouteButton exampleRouteButton;

	public GameObject tapToPlaceWaypointText;

	public GameObject routeButtonsParent;

	public Color selectedRouteButtonColor;

	public Color deselectedRouteButtonColor;

	public CanvasGroup routeSettingsWindow;

	public InputField routeNameInputField;

	public GameObject moveCheckpointButton;

	public GameObject removeCheckpointButton;

	public GameObject applyCheckpointButton;

	[Header("Step 5 - adding mud")]
	public GameObject addingMudWindow;

	public CanvasGroup mudStampSettingsGroup;

	public Text mudStampsCountText;

	public Outline[] mudStampPatterns;

	public GameObject moveMudStampButton;

	public GameObject removeMudStampButton;

	public GameObject sizeMudStampButton;

	public GameObject applyMudStampButton;

	public GameObject mudWarningMessage;

	public Slider mudDepthSlider;

	public Slider mudViscositySlider;

	[Header("Step 6 - finalizing map")]
	public GameObject finalizingMapWindow;

	public GameObject uploadWarningWindow;

	public GameObject removalWarningWindow;

	public Text spawnPointsCountText;

	public Text propsCountText;

	public Button uploadButton;

	public InputField mapNameField;

	public InputField mapDescriptionField;

	public GameObject spawnPointsWarning;

	public GameObject metaWarning;

	public Button mapRemovalButton;

	public GameObject mapUploadingWindow;

	public GameObject mapRemovalWindow;

	public Text mapUploadStatusText;

	public Text mapVisiblityStatusText;

	public GameObject menu;

	private int spawnPointsCount;
}
