using System;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class VehicleLoader : MonoBehaviour
{
	private CarUIControl ui
	{
		get
		{
			return CarUIControl.Instance;
		}
	}

	public bool droneMode { get; internal set; }

	private void Awake()
	{
		VehicleLoader.Instance = this;
	}

	private void Start()
	{
		if (CarUIControl.Instance != null)
		{
			this.ShowLoading();
		}
		UnityEngine.Debug.Log("Starting vehicle loader");
		this.racingManager = RacingManager.Instance;
		this.nextResourcesUnloadTime = Time.time + 120f;
	}

	public void Update()
	{
		if (!this.levelEditor && Time.time > this.nextResourcesUnloadTime)
		{
			this.nextResourcesUnloadTime = Time.time + 120f;
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}
		if (this.loadVehicle)
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.none);
			this.LoadVehicle(false);
			if (CarUIControl.Instance != null)
			{
				base.Invoke("HideLoading", 0.1f);
			}
		}
		if (this.loadingComplete && !this.teamSetupComplete && !this.customMap && !this.levelEditor)
		{
			base.Invoke("SetupTeam", 0.1f);
		}
		if (CrossPlatformInputManager.GetButtonUp("SwapVehicles"))
		{
			this.SwapTrailerVehicles();
		}
		if (!this.levelEditor || (this.levelEditor && this.playerVehicle.activeSelf))
		{
			if (this.swapTimer > 0f)
			{
				this.swapTimer -= Time.deltaTime;
			}
			else
			{
				this.swapTimer = 0f;
			}
			this.ui.UpdateSwapButtonText((int)this.swapTimer);
		}
	}

	public void EnterDroneMode()
	{
		this.droneMode = true;
		this.ui.DroneModeChanged(this.droneMode);
		this.playerDrone.gameObject.SetActive(true);
		this.playerDrone.transform.position = this.playerVehicle.transform.position + Vector3.up * 3f;
		this.playerDrone.transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(this.playerVehicle.transform.forward, Vector3.up));
		this.playerEngine.enabled = false;
		CameraController.Instance.SetDroneCamera(this.playerDrone);
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			this.playerTView.SendDroneModeChanged(true);
		}
	}

	public void ExitDroneMode()
	{
		this.droneMode = false;
		this.ui.DroneModeChanged(this.droneMode);
		if (this.playerDrone && this.playerDrone.gameObject)
		{
			this.playerDrone.gameObject.SetActive(false);
		}
		if (this.playerEngine != null)
		{
			this.playerEngine.enabled = true;
		}
		CameraController.Instance.cameraMode = CameraController.CameraMode.Follow;
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			this.playerTView.SendDroneModeChanged(false);
		}
	}

	public void HideLoading()
	{
		CarUIControl.Instance.LoadingScreen.SetActive(false);
	}

	public void ShowLoading()
	{
		CarUIControl.Instance.LoadingScreen.SetActive(true);
	}

	public void SetupTeam()
	{
		if (this.teamSetupComplete)
		{
			return;
		}
		if (GameState.GameType == GameType.CaptureTheFlag)
		{
			UnityEngine.Debug.Log("Blue players: " + PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count);
			UnityEngine.Debug.Log("Red players: " + PunTeams.PlayersPerTeam[PunTeams.Team.red].Count);
			if (PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count >= PunTeams.PlayersPerTeam[PunTeams.Team.red].Count)
			{
				UnityEngine.Debug.Log("Setting team to red");
				PhotonNetwork.player.SetTeam(PunTeams.Team.red);
			}
			else
			{
				UnityEngine.Debug.Log("Setting team to blue");
				PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
			}
		}
		if (PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count > 0 || PunTeams.PlayersPerTeam[PunTeams.Team.red].Count > 0)
		{
			this.teamSetupComplete = true;
		}
	}

	public void LoadVehicle(bool swapping)
	{
		if (this.SpawnPoints.Length == 0)
		{
			UnityEngine.Debug.LogError("Assign spawn point(s)");
			return;
		}
		Transform availableSpawnPoint = this.GetAvailableSpawnPoint();
		Vector3 position = availableSpawnPoint.position;
		Quaternion rotation = availableSpawnPoint.rotation;
		if (this.car == null)
		{
			string text = GameState.CurrentVehicleID;
			if (text == string.Empty)
			{
				string @string = DataStore.GetString("VehiclesList");
				if (@string != string.Empty)
				{
					SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(@string);
					text = savedVehiclesList.VehicleIDs[0];
				}
			}
			string string2 = DataStore.GetString(text);
			VehicleData vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string2);
			if (swapping)
			{
				position = this.playerVehicle.transform.position;
				rotation = this.playerVehicle.transform.rotation;
			}
			if (GameState.GameMode == GameMode.Multiplayer)
			{
				if (swapping)
				{
					PhotonNetwork.Destroy(this.playerVehicle.GetPhotonView());
				}
				this.playerVehicle = PhotonNetwork.Instantiate("Vehicles/" + vehicleData.VehicleName, position, rotation, 0);
			}
			else
			{
				if (swapping)
				{
					UnityEngine.Object.DestroyImmediate(this.playerVehicle);
				}
				this.playerVehicle = (UnityEngine.Object.Instantiate(Resources.Load("Vehicles/" + vehicleData.VehicleName, typeof(GameObject)), position, rotation) as GameObject);
			}
			this.playerVehicle.name = vehicleData.VehicleName;
			this.CachePlayerComponents();
			Utility.AlignVehicleByGround(this.playerVehicle.transform, !swapping && (this.customMap || this.levelEditor));
			if (this.playerDriver != null)
			{
				this.playerDriver.ToggleDriver(true, false);
			}
			this.playerDataManager.VehicleID = text;
			this.playerDataManager.LoadVehicleData();
			this.playerPartsSwitcher.UpdateColor(true);
			this.playerPartsSwitcher.UpdateDirtiness();
			this.UpdateUiAccordingToCar();
			bool @bool = DataStore.GetBool("DronePurchased", false);
			if (@bool)
			{
				int @int = DataStore.GetInt("SelectedDrone");
				GameObject original = Resources.Load("Drones/Drone" + @int) as GameObject;
				this.playerDrone = UnityEngine.Object.Instantiate<GameObject>(original, this.playerVehicle.transform.position, Quaternion.identity).GetComponent<DroneController>();
				this.playerDrone.gameObject.SetActive(false);
				if (GameState.GameMode == GameMode.Multiplayer)
				{
					this.playerTView.SpawnDrone(@int);
				}
			}
			bool flag = false;
			if (GameState.GameType == GameType.CaptureTheFlag || GameState.GameType == GameType.TrailRace || this.playerDataManager.vehicleType == VehicleType.Bike)
			{
				flag = true;
			}
			if (this.playerDataManager.vehicleType == VehicleType.ATV && !swapping)
			{
				flag = true;
			}
			if (Utility.EqiuppedTrailer() != string.Empty)
			{
				string string3 = DataStore.GetString(Utility.EqiuppedTrailer());
				VehicleData vehicleData2 = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string3);
				TrailerController trailerController = Resources.Load("Vehicles/" + vehicleData2.VehicleName, typeof(TrailerController)) as TrailerController;
				if (trailerController != null && trailerController.gooseneck && this.playerPartsSwitcher.gooseneckMount == null && !swapping)
				{
					flag = true;
				}
			}
			if (this.levelEditor)
			{
				flag = true;
			}
			if (!flag)
			{
				string vehicleID = Utility.EqiuppedTrailer();
				if (Utility.DoesTruckExist(vehicleID))
				{
					Vector3 position2 = Vector3.zero;
					Quaternion rotation2 = Quaternion.identity;
					if (swapping)
					{
						position2 = this.trailer.transform.position;
						rotation2 = this.trailer.transform.rotation;
						UnityEngine.Object.DestroyImmediate(this.trailer);
					}
					string string4 = DataStore.GetString(Utility.EqiuppedTrailer());
					VehicleData vehicleData3 = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string4);
					this.trailer = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Vehicles/" + vehicleData3.VehicleName), position2, rotation2);
					TrailerController component = this.trailer.GetComponent<TrailerController>();
					if (!swapping)
					{
						component.ConnectToCar();
					}
					this.playerCarController.myTrailer = component;
					if (GameState.GameMode == GameMode.Multiplayer)
					{
						this.playerTView.SpawnTrailer(vehicleData3.VehicleName);
					}
				}
				string string5 = DataStore.GetString("VehicleOnTrailer");
				if (Utility.DoesTruckExist(string5))
				{
					if (swapping)
					{
						UnityEngine.Object.DestroyImmediate(this.carOnTrailer);
					}
					string string6 = DataStore.GetString(string5);
					VehicleData vehicleData4 = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string6);
					this.carOnTrailer = (UnityEngine.Object.Instantiate(Resources.Load("Vehicles/" + vehicleData4.VehicleName, typeof(GameObject))) as GameObject);
					this.carOnTrailer.name = vehicleData4.VehicleName;
					IKDriverController component2 = this.carOnTrailer.GetComponent<IKDriverController>();
					if (component2 != null)
					{
						component2.ToggleDriver(false, false);
						component2.enabled = false;
					}
					VehicleDataManager component3 = this.carOnTrailer.GetComponent<VehicleDataManager>();
					component3.VehicleID = string5;
					component3.LoadVehicleData();
					this.carOnTrailer.GetComponent<BodyPartsSwitcher>().UpdateColor(true);
					this.carOnTrailer.GetComponent<BodyPartsSwitcher>().UpdateDirtiness();
					this.carOnTrailer.GetComponent<CarController>().UpdateEngineModel();
					this.carOnTrailer.GetComponent<CarController>().SetCalculatedCOM();
					if (this.carOnTrailer.GetComponentInChildren<EngineSoundProcessor>() != null)
					{
						this.carOnTrailer.GetComponentInChildren<EngineSoundProcessor>().enabled = false;
					}
					if (this.carOnTrailer.GetComponent<TankController>() != null)
					{
						this.carOnTrailer.GetComponent<TankController>().Start();
					}
					component3.LoadOnTrailer(this.trailer.GetComponent<TrailerController>(), true);
					if (GameState.GameMode == GameMode.Multiplayer)
					{
						this.playerTView.SpawnTraileredCar(string6);
					}
				}
			}
		}
		else
		{
			this.playerVehicle = this.car;
			this.CachePlayerComponents();
			if (this.playerDriver != null)
			{
				this.playerDriver.ToggleDriver(true, false);
			}
			this.playerPartsSwitcher.UpdateColor(true);
			this.playerPartsSwitcher.UpdateDirtiness();
			this.playerPartsSwitcher.WinchInstalled = true;
			this.UpdateUiAccordingToCar();
		}
		if (GameState.GameType == GameType.TrailRace)
		{
			TrailRaceManager.Instance.InitializeRace();
			TrailRaceManager.Instance.ImTotallyLoaded();
		}
		if (MultiplayerManager.Instance != null)
		{
			MultiplayerManager.Instance.TotallyReady = true;
			MultiplayerManager.RefreshCurrentPlayers();
		}
		this.loadVehicle = false;
		this.loadingComplete = true;
		if (this.levelEditor)
		{
			this.playerVehicle.SetActive(false);
		}
	}

	public void UpdateUiAccordingToCar()
	{
		if (CarUIControl.Instance != null)
		{
			CarUIControl.Instance.ToggleEbrake(this.playerCarController.Ebrake == 1);
			CarUIControl.Instance.ToggleGearShifter(this.playerCarController.transmissionType == TransmissionType.Manual);
			CarUIControl.Instance.SwitchWinchToggleButton(this.playerPartsSwitcher.WinchInstalled);
			CarUIControl.Instance.ToggleRepairButton(this.playerPartsSwitcher.RepairPackInstalled);
		}
	}

	private void CachePlayerComponents()
	{
		this.playerCarController = this.playerVehicle.GetComponent<CarController>();
		this.playerPartsSwitcher = this.playerVehicle.GetComponent<BodyPartsSwitcher>();
		this.playerSuspensionController = this.playerVehicle.GetComponent<SuspensionController>();
		this.playerRigidbody = this.playerVehicle.GetComponent<Rigidbody>();
		this.playerTView = this.playerVehicle.GetComponent<PhotonTransformView>();
		this.playerDriver = this.playerVehicle.GetComponent<IKDriverController>();
		this.playerEngine = this.playerVehicle.GetComponent<EngineController>();
		this.playerDataManager = this.playerVehicle.GetComponent<VehicleDataManager>();
	}

	[ContextMenu("Swap")]
	public void SwapTrailerVehicles()
	{
		this.swapTimer = 10f;
		this.playerDataManager.SaveVehicleData();
		string vehicleID = this.playerDataManager.VehicleID;
		string vehicleID2 = this.carOnTrailer.GetComponent<VehicleDataManager>().VehicleID;
		GameState.CurrentVehicleID = vehicleID2;
		DataStore.SetString("VehicleOnTrailer", vehicleID);
		this.LoadVehicle(true);
	}

	public Transform GetAvailableSpawnPoint()
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < this.SpawnPoints.Length; i++)
		{
			bool flag = true;
			foreach (PhotonView photonView in PhotonNetwork.networkingPeer.photonViewList.Values)
			{
				if (Vector3.Distance(photonView.transform.position, this.SpawnPoints[i].position) < 10f)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				list.Add(this.SpawnPoints[i]);
			}
		}
		if (list.Count > 0)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		return this.SpawnPoints[UnityEngine.Random.Range(0, this.SpawnPoints.Length)];
	}

	private RacingManager racingManager;

	public Transform[] SpawnPoints;

	public bool levelEditor;

	public bool customMap;

	public static VehicleLoader Instance;

	[HideInInspector]
	public GameObject playerVehicle;

	[HideInInspector]
	public CarController playerCarController;

	[HideInInspector]
	public BodyPartsSwitcher playerPartsSwitcher;

	[HideInInspector]
	public SuspensionController playerSuspensionController;

	[HideInInspector]
	public Rigidbody playerRigidbody;

	[HideInInspector]
	public PhotonTransformView playerTView;

	[HideInInspector]
	public IKDriverController playerDriver;

	[HideInInspector]
	public VehicleDataManager playerDataManager;

	[HideInInspector]
	public EngineController playerEngine;

	[HideInInspector]
	public DroneController playerDrone;

	[HideInInspector]
	public GameObject carOnTrailer;

	[HideInInspector]
	public GameObject trailer;

	private float swapTimer;

	private float nextResourcesUnloadTime;

	public GameObject car;

	public bool loadFirstCar;

	[HideInInspector]
	public GameObject carToFollow;

	[HideInInspector]
	public bool loadVehicle = true;

	private bool loadingComplete;

	private bool teamSetupComplete;
}
