using System;
using System.Collections;
using CustomVP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class TrailRaceManager : MonoBehaviour
{
	public TrailRaceManager()
	{
		if (TrailRaceManager.Instance == null)
		{
			TrailRaceManager.Instance = this;
		}
	}

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

	private string RaceResultText
	{
		get
		{
			TrailRaceManager.CompetitionState competitionState = this.competitionState;
			if (competitionState == TrailRaceManager.CompetitionState.Disqualified)
			{
				return "You're disqualified!";
			}
			if (competitionState == TrailRaceManager.CompetitionState.Lost)
			{
				return "You lost!";
			}
			if (competitionState != TrailRaceManager.CompetitionState.Won)
			{
				return string.Empty;
			}
			return "You won!";
		}
	}

	private void Awake()
	{
		TrailRaceManager.Instance = this;
	}

	private void Start()
	{
		TrailRaceManager.Instance = this;
		if (GameState.GameType != GameType.TrailRace)
		{
			base.enabled = false;
			return;
		}
		this.SetTrailRaceState(TrailRaceManager.TrailRaceState.Connected);
		this.IAmHost = (PhotonNetwork.room.PlayerCount < 2);
	}

	public void OnOtherPlayerTotallyLoaded(string name)
	{
		UnityEngine.Debug.Log("Other player loaded");
		this.OtherPlayerTotallyLoaded = true;
		PhotonNetwork.room.IsOpen = false;
		this.OpponentName = name;
		if (this.opponentLightSet != null)
		{
			this.opponentLightSet.SetLightState(LightState.PreStage);
		}
		CarUIControl.Instance.HideShowRestartOffering(false);
		CarUIControl.Instance.HideShowOfferRestartButton(true);
		CarUIControl.Instance.HideShowWaitForOtherPlayerButton(false);
		CarUIControl.Instance.HideShowSpectateButton(true);
	}

	public void OnOtherPlayerDisconnected()
	{
		CarUIControl.Instance.HideShowRestartOffering(false);
		CarUIControl.Instance.HideShowOfferRestartButton(false);
		CarUIControl.Instance.HideShowWaitForOtherPlayerButton(true);
		CarUIControl.Instance.HideShowSpectateButton(false);
		CameraController.Instance.forcedTarget = null;
		CameraController.Instance.cameraMode = CameraController.CameraMode.Follow;
		this.OtherPlayerTotallyLoaded = false;
		if (this.trailRaceState == TrailRaceManager.TrailRaceState.WaitingForReadiness)
		{
			this.InitializeRace();
			return;
		}
		if (!this.iFinished)
		{
			CarUIControl.Instance.HideShowOtherPlayerDisconnectedWindow(true);
			this.ToggleUI(false);
			this.carController.vehicleIsActive = false;
		}
		else
		{
			if (this.competitionState == TrailRaceManager.CompetitionState.None)
			{
				this.competitionState = TrailRaceManager.CompetitionState.Won;
			}
			if (this.OpponentTime == 0f)
			{
				this.OpponentTime = -1f;
			}
			CarUIControl.Instance.HideShoTrailRaceFinishWindow(true, this.RaceResultText, this.raceTimer, this.OpponentTime, GameState.playerName, this.OpponentName);
			CarUIControl.Instance.HideShowSpectateButton(false);
			CarUIControl.Instance.HideShowOfferRestartButton(false);
		}
		if (this.Timer != null)
		{
			base.StopCoroutine(this.Timer);
			this.Timer = null;
		}
	}

	public void ImTotallyLoaded()
	{
		this.photonTransformView.ImTotallyLoaded();
	}

	private IEnumerator DoLosingPlayerTimer()
	{
		for (int i = 30; i > 0; i--)
		{
			CarUIControl.Instance.ShowNotification(i.ToString(), false);
			yield return new WaitForSeconds(1f);
		}
		this.LosingPlayerTimerOver();
		yield break;
	}

	private void LosingPlayerTimerOver()
	{
		this.carController.vehicleIsActive = false;
		this.RaceStarted = false;
		this.iFinished = true;
		this.competitionState = TrailRaceManager.CompetitionState.Disqualified;
		CarUIControl.Instance.HideShoTrailRaceFinishWindow(true, this.RaceResultText, -1f, this.OpponentTime, GameState.playerName, this.OpponentName);
		CarUIControl.Instance.HideShowOfferRestartButton(this.OpponentFinished);
		CarUIControl.Instance.HideShowSpectateButton(false);
		this.raceTimer = -1f;
		this.photonTransformView.iFinishedTrailRace(this.raceTimer);
		this.Timer = null;
	}

	public void OnOtherPlayerFinished(float hisTime)
	{
		this.OpponentTime = hisTime;
		this.OpponentFinished = true;
		if (this.competitionState == TrailRaceManager.CompetitionState.None && this.iFinished)
		{
			if (this.OpponentTime != -1f)
			{
				this.competitionState = ((this.raceTimer >= this.OpponentTime) ? TrailRaceManager.CompetitionState.Lost : TrailRaceManager.CompetitionState.Won);
			}
			else
			{
				this.competitionState = TrailRaceManager.CompetitionState.Won;
			}
		}
		CarUIControl.Instance.HideShowSpectateButton(false);
		if (this.iFinished)
		{
			CarUIControl.Instance.HideShoTrailRaceFinishWindow(true, this.RaceResultText, this.raceTimer, this.OpponentTime, GameState.playerName, this.OpponentName);
		}
		else
		{
			CarUIControl.Instance.ShowNotification(this.OpponentName + " finished!", false);
			this.Timer = base.StartCoroutine(this.DoLosingPlayerTimer());
		}
		CarUIControl.Instance.HideShowOfferRestartButton(this.iFinished && this.OpponentFinished);
		CarUIControl.Instance.HideShowRestartOffering(false);
	}

	public void InitializeRace()
	{
		this.ToggleUI(false);
		if (this.currentRoute == null)
		{
			int trailID = GameState.TrailID;
			string trailName = Trails.trails[trailID].TrailName;
			UnityEngine.Debug.Log("Looking for route: " + trailName);
			foreach (Route route in UnityEngine.Object.FindObjectsOfType<Route>())
			{
				if (route.MultiplayerTrail && route.MultiplayerName == trailName)
				{
					this.currentRoute = route;
					this.CurrentTrailFound = true;
					break;
				}
			}
		}
		CameraController.Instance.forcedTarget = null;
		CameraController.Instance.cameraMode = CameraController.CameraMode.Follow;
		CarUIControl.Instance.HideShoTrailRaceFinishWindow(false);
		CarUIControl.Instance.HideShowOtherPlayerDisconnectedWindow(false);
		CarUIControl.Instance.ToggleReadyToRaceWindow(false);
		if (PhotonNetwork.room.PlayerCount < 2)
		{
			this.IAmHost = true;
		}
		PhotonNetwork.room.IsOpen = (PhotonNetwork.room.PlayerCount < 2);
		Transform transform = (!this.IAmHost) ? this.currentRoute.Player2StartPos : this.currentRoute.Player1StartPos;
		this.carController.transform.position = transform.position + Vector3.up * 2f;
		this.carController.transform.rotation = transform.rotation;
		this.carController.vehicleIsActive = false;
		this.playerRigidbody.velocity = Vector3.zero;
		this.playerRigidbody.angularVelocity = Vector3.zero;
		this.SetupLightTree();
		this.myLightSet.SetLightState(LightState.PreStage);
		this.opponentLightSet.SetLightState(LightState.ShutAll);
		if (this.Timer != null)
		{
			base.StopCoroutine(this.Timer);
			this.Timer = null;
		}
		this.competitionState = TrailRaceManager.CompetitionState.None;
		this.OtherPlayerReady = false;
		this.ImReady = false;
		this.RaceStarted = false;
		this.raceTimer = 0f;
		this.CurrentCheckpoint = 0;
		this.AllCheckpointsPassed = false;
		this.OpponentTime = 0f;
		this.iFinished = false;
		this.OpponentFinished = false;
		this.SetTrailRaceState(TrailRaceManager.TrailRaceState.WaitingForOtherPlayer);
		this.Initialized = true;
	}

	private void SetupLightTree()
	{
		if (PhotonNetwork.room.PlayerCount < 2)
		{
			this.IAmHost = true;
		}
		this.myLightSet = ((!this.IAmHost) ? this.currentRoute.lightTree.Player2Lights : this.currentRoute.lightTree.Player1Lights);
		this.opponentLightSet = ((!this.IAmHost) ? this.currentRoute.lightTree.Player1Lights : this.currentRoute.lightTree.Player2Lights);
	}

	public void CollidedWithCheckpoint(Checkpoint cp)
	{
		if (cp.ID == this.CurrentCheckpoint)
		{
			this.NextCheckpoint();
		}
	}

	private void NextCheckpoint()
	{
		this.CurrentCheckpoint++;
		if (this.CurrentCheckpoint < this.currentRoute.SpawnedCheckpoints.Count)
		{
			CarUIControl.Instance.CurrentCheckpoint = this.currentRoute.SpawnedCheckpoints[this.CurrentCheckpoint].transform;
		}
		else
		{
			this.AllCheckpointsPassed = true;
			if (this.currentRoute.SpawnedFinish != null)
			{
				CarUIControl.Instance.CurrentCheckpoint = this.currentRoute.SpawnedFinish.transform;
			}
			else
			{
				CarUIControl.Instance.CurrentCheckpoint = this.currentRoute.SpawnedStart.transform;
			}
		}
		this.currentRoute.ShowCheckpoint(this.CurrentCheckpoint);
		CarUIControl.Instance.ShowNotification("Checkpoint", false);
	}

	private void CheckFinish()
	{
		if (Vector3.Distance(this.carController.transform.position, this.currentRoute.SpawnedFinish.transform.position) < 5f && this.AllCheckpointsPassed && this.RaceStarted)
		{
			this.Finish();
		}
	}

	private void SetTrailRaceState(TrailRaceManager.TrailRaceState state)
	{
		this.trailRaceState = state;
		this.ShowMessageContiniously = false;
		switch (this.trailRaceState)
		{
		case TrailRaceManager.TrailRaceState.WaitingForOtherPlayer:
			this.myLightSet.SetLightState(LightState.PreStage);
			this.ToggleUI(false);
			CarUIControl.Instance.ToggleCarExtras(true);
			this.Message = "Waiting for other player...";
			this.ShowMessageContiniously = true;
			break;
		case TrailRaceManager.TrailRaceState.WaitingForReadiness:
			CarUIControl.Instance.ToggleReadyToRaceWindow(true);
			this.ToggleUI(false);
			CarUIControl.Instance.ToggleCarExtras(true);
			break;
		case TrailRaceManager.TrailRaceState.Countdown:
			this.ToggleUI(true);
			base.StartCoroutine(this.Countdown(this.countdownTime));
			CarUIControl.Instance.CurrentCheckpoint = this.currentRoute.SpawnedCheckpoints[0].transform;
			if (this.CurrentTrailFound)
			{
				this.currentRoute.HideShowEventMark(false);
				this.currentRoute.HideShowAllCheckpoints(true);
				this.currentRoute.HideShowStartAndFinish(true);
			}
			RacingManager.Instance.HideShowEventMarks(false);
			break;
		case TrailRaceManager.TrailRaceState.Racing:
			this.RaceStarted = true;
			this.carController.vehicleIsActive = true;
			CarUIControl.Instance.ShowNotification("GO!!!", false);
			break;
		}
	}

	private void PlayerIsReady()
	{
		UnityEngine.Debug.Log("PlayerIsReady");
		this.ImReady = true;
		this.photonTransformView.ImReadyToRace();
		this.myLightSet.SetLightState(LightState.Stage);
		CarUIControl.Instance.ToggleReadyToRaceWindow(false);
		this.CheckReadiness();
	}

	public void OnOtherPlayerReady()
	{
		UnityEngine.Debug.Log("OnOtherPlayerReady");
		this.OtherPlayerReady = true;
		this.CheckReadiness();
		if (this.opponentLightSet == null)
		{
			this.SetupLightTree();
		}
		this.opponentLightSet.SetLightState(LightState.Stage);
	}

	private void CheckReadiness()
	{
		UnityEngine.Debug.Log("CheckReadiness");
		if (this.ImReady && this.OtherPlayerReady)
		{
			this.SetTrailRaceState(TrailRaceManager.TrailRaceState.Countdown);
		}
	}

	private IEnumerator Countdown(int time)
	{
		for (int i = time; i > 0; i--)
		{
			if (i != 3)
			{
				if (i != 2)
				{
					if (i == 1)
					{
						this.myLightSet.SetLightState(LightState.Countdown1);
						this.opponentLightSet.SetLightState(LightState.Countdown1);
						CarUIControl.Instance.ShowNotification("1", false);
					}
				}
				else
				{
					this.myLightSet.SetLightState(LightState.Countdown2);
					this.opponentLightSet.SetLightState(LightState.Countdown2);
					CarUIControl.Instance.ShowNotification("2", false);
				}
			}
			else
			{
				this.myLightSet.SetLightState(LightState.Countdown3);
				this.opponentLightSet.SetLightState(LightState.Countdown3);
				CarUIControl.Instance.ShowNotification("3", false);
			}
			yield return new WaitForSeconds(1f);
		}
		this.myLightSet.SetLightState(LightState.Start);
		this.opponentLightSet.SetLightState(LightState.Start);
		this.SetTrailRaceState(TrailRaceManager.TrailRaceState.Racing);
		yield break;
	}

	private void ToggleUI(bool show)
	{
		if (CarUIControl.Instance == null)
		{
			return;
		}
		CarUIControl.Instance.ToggleCarExtras(show);
		CarUIControl.Instance.ToggleCarControls(show);
		CarUIControl.Instance.ToggleWinchControls(show);
		CarUIControl.Instance.HideShowRaceUI(show, false);
	}

	private void Finish()
	{
		this.carController.vehicleIsActive = false;
		this.RaceStarted = false;
		this.iFinished = true;
		if (this.Timer != null)
		{
			base.StopCoroutine(this.Timer);
			this.Timer = null;
		}
		if (this.competitionState == TrailRaceManager.CompetitionState.None)
		{
			if (this.OpponentFinished)
			{
				this.competitionState = ((this.raceTimer >= this.OpponentTime) ? TrailRaceManager.CompetitionState.Lost : TrailRaceManager.CompetitionState.Won);
			}
			else if (PhotonNetwork.room.PlayerCount < 2)
			{
				this.competitionState = TrailRaceManager.CompetitionState.Won;
			}
		}
		string resultText = string.Empty;
		TrailRaceManager.CompetitionState competitionState = this.competitionState;
		if (competitionState != TrailRaceManager.CompetitionState.None)
		{
			if (competitionState != TrailRaceManager.CompetitionState.Won)
			{
				if (competitionState == TrailRaceManager.CompetitionState.Lost)
				{
					resultText = "You lost!";
				}
			}
			else
			{
				resultText = "You won!";
			}
		}
		else
		{
			resultText = "Waiting for opponent";
		}
		if (this.OpponentFinished && this.competitionState == TrailRaceManager.CompetitionState.Won)
		{
			GameState.AddCurrency(GameState.TrailRaceBet * 2, Currency.Money);
		}
		CarUIControl.Instance.HideShoTrailRaceFinishWindow(true, resultText, this.raceTimer, this.OpponentTime, GameState.playerName, this.OpponentName);
		CarUIControl.Instance.HideShowOfferRestartButton(this.iFinished && this.OpponentFinished && PhotonNetwork.room.PlayerCount == 2);
		CarUIControl.Instance.HideShowSpectateButton(!this.OpponentFinished);
		CarUIControl.Instance.HideShowRestartOffering(false);
		this.photonTransformView.iFinishedTrailRace(this.raceTimer);
	}

	private void BackToGarage()
	{
		if (this.carController != null)
		{
			this.carController.GetComponent<VehicleDataManager>().SaveVehicleData();
		}
		SceneManager.LoadScene("Menu");
	}

	private void Spectate()
	{
		if (this.iFinished && this.OpponentFinished)
		{
			return;
		}
		foreach (PhotonTransformView photonTransformView in UnityEngine.Object.FindObjectsOfType<PhotonTransformView>())
		{
			if (photonTransformView != this.photonTransformView)
			{
				CameraController.Instance.forcedTarget = photonTransformView.transform;
				CameraController.Instance.cameraMode = CameraController.CameraMode.Free;
				CarUIControl.Instance.HideShoTrailRaceFinishWindow(false);
				this.ToggleUI(false);
				break;
			}
		}
	}

	private void OfferRestart()
	{
		CarUIControl.Instance.HideShowOfferRestartButton(false);
		this.photonTransformView.SendRestartOffering();
	}

	public void OnRestartOfferingReceived()
	{
		CarUIControl.Instance.HideShowRestartOffering(true);
		CarUIControl.Instance.HideShowOfferRestartButton(false);
	}

	private void AcceptRestart()
	{
		this.photonTransformView.SendRestartAcceptation();
		this.InitializeRace();
	}

	public void OnRestartAccepted()
	{
		this.InitializeRace();
	}

	private void ContinueRaceAlone()
	{
		CarUIControl.Instance.HideShowOtherPlayerDisconnectedWindow(false);
		this.ToggleUI(true);
		this.carController.vehicleIsActive = true;
	}

	private void Update()
	{
		if (this.ShowMessageContiniously && CarUIControl.Instance != null)
		{
			CarUIControl.Instance.ShowNotification(this.Message, false);
		}
		if (this.trailRaceState == TrailRaceManager.TrailRaceState.WaitingForOtherPlayer && PhotonNetwork.room.PlayerCount > 1 && MultiplayerManager.Instance.TotallyReady && this.OtherPlayerTotallyLoaded)
		{
			this.SetTrailRaceState(TrailRaceManager.TrailRaceState.WaitingForReadiness);
		}
		if (CrossPlatformInputManager.GetButtonDown("ReadyToRace"))
		{
			this.PlayerIsReady();
		}
		if (CrossPlatformInputManager.GetButtonDown("BackToMenu"))
		{
			this.BackToGarage();
		}
		if (CrossPlatformInputManager.GetButtonDown("Spectate"))
		{
			this.Spectate();
		}
		if (CrossPlatformInputManager.GetButtonDown("OfferRestart"))
		{
			this.OfferRestart();
		}
		if (CrossPlatformInputManager.GetButtonDown("AcceptRestart"))
		{
			this.AcceptRestart();
		}
		if (CrossPlatformInputManager.GetButtonDown("WaitForAnotherPlayer"))
		{
			this.InitializeRace();
		}
		if (CrossPlatformInputManager.GetButtonDown("ContinueRaceAlone"))
		{
			this.ContinueRaceAlone();
		}
		if (this.RaceStarted)
		{
			this.raceTimer += Time.deltaTime;
			CarUIControl.Instance.UpdateTimer(this.raceTimer);
			this.CheckFinish();
		}
	}

	public static TrailRaceManager Instance;

	[HideInInspector]
	public TrailRaceManager.TrailRaceState trailRaceState;

	[HideInInspector]
	public TrailRaceManager.CompetitionState competitionState;

	public bool Initialized;

	private Route currentRoute;

	private int countdownTime = 3;

	private bool OtherPlayerReady;

	private bool ImReady;

	private bool CurrentTrailFound;

	private bool OtherPlayerTotallyLoaded;

	private bool ShowMessageContiniously;

	private string Message;

	private bool RaceStarted;

	private float raceTimer;

	private int CurrentCheckpoint;

	private bool AllCheckpointsPassed;

	private float OpponentTime;

	private bool OpponentFinished;

	private bool iFinished;

	private bool IAmHost;

	private LightSet myLightSet;

	private LightSet opponentLightSet;

	private string OpponentName;

	private Coroutine Timer;

	public enum TrailRaceState
	{
		Connected,
		WaitingForOtherPlayer,
		WaitingForReadiness,
		Countdown,
		Racing
	}

	public enum CompetitionState
	{
		None,
		Lost,
		Won,
		Disqualified
	}
}
