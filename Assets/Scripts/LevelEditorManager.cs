using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorManager : MonoBehaviour
{
	private GameObject playerVehicle
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerVehicle;
			}
			return null;
		}
	}

	private void Start()
	{
		this.levelEditor = LevelEditor.Instance;
		this.warningPanel.alpha = 0f;
		this.SwitchMode(true);
	}

	public void SwitchMode(bool editing)
	{
		if (this.levelEditor == null)
		{
			return;
		}
		if (!editing)
		{
			this.spawnPoints = this.levelEditor.GetSpawnPoints();
			if (this.spawnPoints.Length == 0)
			{
				this.ShowWarning("You need at least one spawn point!");
				return;
			}
		}
		if (this.spawnPoints != null)
		{
			foreach (Transform transform in this.spawnPoints)
			{
				transform.gameObject.SetActive(editing);
			}
		}
		this.levelEditorComponents.SetActive(editing);
		this.gameplayComponents.SetActive(!editing);
		this.playMapButton.SetActive(editing);
		this.editMapButton.SetActive(!editing);
		if (editing)
		{
			this.levelEditor.CancelPreBakeProps();
			if (CarUIControl.Instance != null)
			{
				CarUIControl.Instance.LandDrone();
			}
		}
		else
		{
			VehicleLoader.Instance.UpdateUiAccordingToCar();
			this.levelEditor.PreBakeProps();
			this.levelEditor.terCollider.enabled = false;
			this.levelEditor.terCollider.enabled = true;
		}
		if (this.playerVehicle != null)
		{
			this.playerVehicle.SetActive(!editing);
			Transform transform2 = this.spawnPoints[UnityEngine.Random.Range(0, this.spawnPoints.Length)];
			this.playerVehicle.transform.position = transform2.position;
			this.playerVehicle.transform.rotation = transform2.rotation;
			this.playerVehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Utility.AlignVehicleByGround(this.playerVehicle.transform, true);
		}
		this.levelEditor.ChangeLevelCreationStep(LevelCreationStep.None);
		if (!editing)
		{
			PlayerRouteRacingManager.Instance.Initialize();
			this.levelEditor.CacheSplatMaps();
			LevelEditorTools.ApplyMudStamps(this.levelEditor.mudStamps, Terrain.activeTerrain);
			SurfaceManager.Instance.CreateMudTerrains(this.levelEditor.mudStamps);
			LevelEditorTools.ToggleMudIndicators(false);
		}
		else
		{
			LevelEditorTools.ToggleMudIndicators(true);
			if (SurfaceManager.Instance != null)
			{
				SurfaceManager.Instance.RemoveMudTerrains(this.levelEditor.mudStamps);
			}
			this.levelEditor.RestoreSplatMaps();
		}
	}

	private void ShowWarning(string text)
	{
		this.warningText.text = text;
		this.warningAlpha = 2f;
	}

	private void Update()
	{
		this.warningAlpha -= Time.deltaTime;
		this.warningPanel.alpha = this.warningAlpha;
	}

	public GameObject levelEditorComponents;

	public GameObject gameplayComponents;

	public GameObject playMapButton;

	public GameObject editMapButton;

	private LevelEditor levelEditor;

	public CanvasGroup warningPanel;

	public Text warningText;

	private float warningAlpha;

	private Transform[] spawnPoints;
}
