using System;
using System.Collections;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RacingManager : MonoBehaviour
{
	private CarController carController
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerCarController;
			}
			return null;
		}
	}

	private BodyPartsSwitcher partsSwitcher
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerPartsSwitcher;
			}
			return null;
		}
	}

	private Rigidbody playerRigidbody
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerRigidbody;
			}
			return null;
		}
	}

	private PhotonTransformView photonTransformView
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerTView;
			}
			return null;
		}
	}

	private VehicleDataManager dataManager
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerDataManager;
			}
			return null;
		}
	}

	public bool InRace { get; internal set; }

	public bool IsPlayerBusy
	{
		get
		{
			return this.InRace || this.AnyEventIsNear;
		}
	}

	private void Awake()
	{
		RacingManager.Instance = this;
	}

	private void Start()
	{
		this.carUIControl = CarUIControl.Instance;
		this.CheckpointSound = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Sounds/Checkpoint") as GameObject).GetComponent<AudioSource>();
		this.CheckpointSound.transform.parent = base.transform;
	}

	public void RegisterRoute(Route route)
	{
		this.AllRoutes.Add(route);
		route.HideShowAllCheckpoints(false);
		route.HideShowStartAndFinish(false);
	}

	public void CollidedWithCheckpoint(Checkpoint checkpoint)
	{
		if (checkpoint.ID == this.CurrentCheckpoint)
		{
			this.NextCheckpoint();
		}
	}

	public void SaveVehicleData()
	{
		if (this.dataManager != null)
		{
			this.dataManager.SaveVehicleData();
		}
	}

	public void GetToGarage()
	{
		this.SaveVehicleData();
		SceneManager.LoadScene("Menu");
		print("exiting from garage");
	}

	private void Update()
	{
		if (this.carController == null)
		{
			return;
		}
		if (!this.InRace && !StashManager.Instance.LockboxActive && Time.frameCount % 10 == 0 && GameState.GameType != GameType.TrailRace && !VehicleLoader.Instance.droneMode)
		{
			this.CheckClosestEvents();
		}
		if (this.InRace)
		{
			this.CheckFinish();
			if (this.RaceTimerEnabled)
			{
				this.RaceTime += Time.deltaTime;
			}
			this.carUIControl.UpdateTimer(this.RaceTime);
		}
	}

	private void HideAllCheckpoints()
	{
		foreach (Route route in this.AllRoutes)
		{
			route.HideShowAllCheckpoints(false);
			route.HideShowStartAndFinish(false);
		}
	}

	public void HideShowEventMarks(bool Show)
	{
		foreach (Route route in this.AllRoutes)
		{
			route.HideShowEventMark(Show);
		}
	}

	public void StartRace()
	{
		this.InRace = true;
		this.RaceTimerEnabled = false;
		this.WinchUsedTimes = 0;
		this.RaceTime = 0f;
		this.AllCheckpointsPassed = false;
		this.CurrentLap = 1;
		this.CurrentCheckpoint = 0;
		this.StartHealth = this.carController.CarHealth;
		this.FlippedOverCount = 0;
		this.carUIControl.HideEventLobby();
		this.carUIControl.HideShowRaceUI(true, true);
		this.carUIControl.UpdateWinchUsedText(this.WinchUsedTimes);
		this.carUIControl.UpdateLapText(this.CurrentLap, this.ActiveRoute.LapsNumber);
		this.carUIControl.CurrentCheckpoint = this.ActiveRoute.SpawnedCheckpoints[0].transform;
		this.carController.m_Rigidbody.velocity = Vector3.zero;
		this.carController.transform.position = this.ActiveRoute.Waypoints[0].position + Vector3.up * 2f;
		this.carController.transform.LookAt(this.ActiveRoute.Waypoints[1]);
		this.ActiveRoute.HideShowStartAndFinish(true);
		this.ActiveRoute.ShowCheckpoint(0);
		this.HideShowEventMarks(false);
		this.countdownRoutine = base.StartCoroutine(this.Countdown());
	}

	public void CancelRace()
	{
		this.RaceTimerEnabled = false;
		this.carController.vehicleIsActive = true;
		if (this.countdownRoutine != null)
		{
			base.StopCoroutine(this.countdownRoutine);
		}
		this.carUIControl.HideShowRaceUI(false, false);
		this.carUIControl.HideShowCountdown(false);
		this.ActiveRoute.HideShowStartAndFinish(false);
		this.ActiveRoute.HideShowAllCheckpoints(false);
		this.InRace = false;
		this.HideShowEventMarks(true);
	}

	public void Continue()
	{
		this.carController.vehicleIsActive = true;
		this.carUIControl.FinishInfo.SetActive(false);
		this.carUIControl.HideShowRaceUI(false, false);
		this.ActiveRoute.Completed = true;
		this.ActiveRoute.HideShowStartAndFinish(false);
		this.ActiveRoute.HideShowAllCheckpoints(false);
		this.InRace = false;
		this.HideShowEventMarks(true);
	}

	public void FlippedOver()
	{
		if (this.InRace)
		{
			this.FlippedOverCount++;
		}
	}

	private IEnumerator Countdown()
	{
		this.carUIControl.HideShowCountdown(true);
		this.carController.vehicleIsActive = false;
		for (int c = 3; c > 0; c--)
		{
			this.carUIControl.ShowCountdownText(c);
			yield return new WaitForSeconds(1f);
		}
		this.carUIControl.HideShowCountdown(false);
		this.carController.vehicleIsActive = true;
		this.RaceTimerEnabled = true;
		yield break;
	}

	private void Finish()
	{
		this.carController.vehicleIsActive = false;
		this.RaceTimerEnabled = false;
		this.CheckpointSound.Play();
		if (this.dataManager.vehicleType == VehicleType.Any)
		{
			UnityEngine.Debug.Log("VEHICLE TYPE IS NOT SET FOR THE CURRENT VEHICLE!");
			return;
		}
		int num = Mathf.RoundToInt(this.RaceTime * 100f);
		this.ActiveRoute.SubmitTime(num, false);
		RoutePayment routePayment = this.ActiveRoute.GetRoutePayment(this.dataManager.vehicleType, num, this.WinchUsedTimes, this.FlippedOverCount, this.StartHealth - this.carController.CarHealth);
		StatsData statsData = GameState.LoadStatsData();
		int amount = Utility.AdjustedWinnings(routePayment.Cash);
		int amount2 = Utility.AdjustedWinnings(routePayment.XP);
		int num2 = Utility.AdjustedWinnings(routePayment.Gold) + Utility.AdjustedWinnings(routePayment.TrailblazerGoldBonus);
		UnityEngine.Debug.Log("TRAILBLAZER BONUS!" + routePayment.TrailblazerGoldBonus);
		GameState.AddCurrency(amount, Currency.Money);
		if (num2 > 0)
		{
			GameState.AddCurrency(num2, Currency.Gold);
		}
		GameState.AddXP(amount2);
		string s = this.ActiveRoute.RouteName.Replace("Route", string.Empty);
		int trailID = int.Parse(s);
		string text = this.ActiveRoute.GetRecordKeeper();
		if (text == string.Empty)
		{
			text = "Undefined";
		}
		if ((long)num < this.ActiveRoute.RouteRecord)
		{
			text = GameState.playerName;
		}
		this.carUIControl.HideShowFinishWindow(true, routePayment, (long)num, this.ActiveRoute.RouteRecord, trailID, text);
		if (this.OnRouteCompleteHandler != null)
		{
			this.OnRouteCompleteHandler(this.ActiveRoute, this.dataManager.vehicleType, routePayment);
		}
	}

	private void NewLap()
	{
		this.CurrentLap++;
		this.CurrentCheckpoint = 0;
		this.AllCheckpointsPassed = false;
		this.ActiveRoute.ShowCheckpoint(this.CurrentCheckpoint);
		this.carUIControl.UpdateLapText(this.CurrentLap, this.ActiveRoute.LapsNumber);
		this.carUIControl.ShowNotification("New lap", false);
		this.carUIControl.CurrentCheckpoint = this.ActiveRoute.SpawnedCheckpoints[this.CurrentCheckpoint].transform;
		this.CheckpointSound.Play();
	}

	private void CheckClosestEvents()
	{
		this.AnyEventIsNear = false;
		List<Route> list = new List<Route>();
		Route route = null;
		foreach (Route route2 in this.AllRoutes)
		{
			if (Vector3.Distance(this.carController.transform.position, route2.transform.position) < this.EventsDetectionDistance)
			{
				list.Add(route2);
			}
		}
		if (list.Count > 0)
		{
			float num = 99999f;
			for (int i = 0; i < list.Count; i++)
			{
				float num2 = Vector3.Distance(this.carController.transform.position, list[i].transform.position);
				if (num2 < num)
				{
					num = num2;
					route = list[i];
				}
			}
			if (route != this.CurrentLobbyRoute)
			{
				this.ShowingEventLobby = true;
				this.AnyEventIsNear = true;
				this.ActiveRoute = route;
				this.CurrentLobbyRoute = route;
				this.carUIControl.ShowEventLobby(route);
				route.RefreshRecord();
			}
		}
		else if (this.ShowingEventLobby)
		{
			UnityEngine.Debug.Log("Close routes: " + list.Count);
			this.ShowingEventLobby = false;
			UnityEngine.Debug.Log("HH");
			this.carUIControl.HideEventLobby();
			route = null;
			this.ActiveRoute = null;
			this.CurrentLobbyRoute = null;
		}
	}

	private void NextCheckpoint()
	{
		this.CurrentCheckpoint++;
		if (this.CurrentCheckpoint < this.ActiveRoute.SpawnedCheckpoints.Count)
		{
			this.carUIControl.CurrentCheckpoint = this.ActiveRoute.SpawnedCheckpoints[this.CurrentCheckpoint].transform;
		}
		else
		{
			this.AllCheckpointsPassed = true;
			if (this.ActiveRoute.SpawnedFinish != null)
			{
				this.carUIControl.CurrentCheckpoint = this.ActiveRoute.SpawnedFinish.transform;
			}
			else
			{
				this.carUIControl.CurrentCheckpoint = this.ActiveRoute.SpawnedStart.transform;
			}
		}
		this.ActiveRoute.ShowCheckpoint(this.CurrentCheckpoint);
		this.carUIControl.ShowNotification("Checkpoint", false);
		this.CheckpointSound.Play();
	}

	private void CheckCheckpoints()
	{
		if (this.CurrentCheckpoint >= this.ActiveRoute.SpawnedCheckpoints.Count)
		{
			return;
		}
		Vector3 position = this.carController.transform.position;
		Vector3 position2 = this.ActiveRoute.SpawnedCheckpoints[this.CurrentCheckpoint].transform.position;
		position2.y = position.y;
		if (Vector3.Distance(position, position2) < this.CheckpointDetectionDistance && !this.AllCheckpointsPassed)
		{
			this.NextCheckpoint();
		}
	}

	private void CheckFinish()
	{
		bool circuit = this.ActiveRoute.Circuit;
		if (circuit)
		{
			if (circuit)
			{
				if (Vector3.Distance(this.carController.transform.position, this.ActiveRoute.SpawnedStart.transform.position) < this.CheckpointDetectionDistance && this.AllCheckpointsPassed && this.RaceTimerEnabled)
				{
					if (this.CurrentLap == this.ActiveRoute.LapsNumber)
					{
						this.Finish();
					}
					else
					{
						this.NewLap();
					}
				}
			}
		}
		else if (Vector3.Distance(this.carController.transform.position, this.ActiveRoute.SpawnedFinish.transform.position) < this.CheckpointDetectionDistance && this.AllCheckpointsPassed && this.RaceTimerEnabled)
		{
			this.Finish();
		}
	}

	private RoutesData GetRoutesData()
	{
		RoutesData routesData = new RoutesData();
		routesData.RoutesCompleteness = new bool[this.AllRoutes.Count];
		routesData.RoutesNames = new string[this.AllRoutes.Count];
		for (int i = 0; i < this.AllRoutes.Count; i++)
		{
			routesData.RoutesNames[i] = this.AllRoutes[i].name;
			routesData.RoutesCompleteness[i] = this.AllRoutes[i].Completed;
		}
		return routesData;
	}

	private void SetRoutesData(RoutesData rData)
	{
		for (int i = 0; i < rData.RoutesNames.Length; i++)
		{
			foreach (Route route in this.AllRoutes)
			{
				if (route.name == rData.RoutesNames[i])
				{
					route.Completed = rData.RoutesCompleteness[i];
				}
			}
		}
		this.HideShowEventMarks(true);
	}

	[ContextMenu("Export data")]
	public string ExportData()
	{
		string text = string.Empty;
		RoutesData routesData = this.GetRoutesData();
		text = XmlSerialization.SerializeData<RoutesData>(routesData);
		this.SerializedString = text;
		return text;
	}

	public void ImportData(string XmlString)
	{
		RoutesData routesData = new RoutesData();
		routesData = (RoutesData)XmlSerialization.DeserializeData<RoutesData>(XmlString);
		this.SetRoutesData(routesData);
	}

	[ContextMenu("Import test data")]
	public void ImportTestData()
	{
		this.ImportData(this.SerializedString);
	}

	private CarUIControl carUIControl;

	private bool AnyEventIsNear;

	public Route ActiveRoute;

	private List<Route> AllRoutes = new List<Route>();

	private int CurrentCheckpoint;

	private int CurrentLap;

	private float StartHealth;

	private int FlippedOverCount;

	public float RaceTime;

	private int WinchUsedTimes;

	private bool WinchUsingCounted;

	private bool AllCheckpointsPassed;

	private bool RaceTimerEnabled;

	private bool ShowingEventLobby;

	private AudioSource CheckpointSound;

	private float CheckpointDetectionDistance = 10f;

	private float EventsDetectionDistance = 10f;

	private Coroutine countdownRoutine;

	public string SerializedString;

	public OnRouteCompleteDelegate OnRouteCompleteHandler;

	private Route ClosestRoute;

	private Route CurrentLobbyRoute;

	public static RacingManager Instance;
}
