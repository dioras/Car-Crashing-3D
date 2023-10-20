using System;
using System.Collections;
using System.Collections.Generic;
using CustomVP;
using GameAnalyticsSDK;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CarUIControl : MonoBehaviour
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

	private EngineController engine
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerEngine;
			}
			return null;
		}
	}

	private RacingManager racingManager
	{
		get
		{
			return RacingManager.Instance;
		}
	}

	private CameraController camController
	{
		get
		{
			return CameraController.Instance;
		}
	}

	private void Awake()
	{
		CarUIControl.Instance = this;
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			this.LoadingScreenLabel.text = "Joining other Outlaws...";
		}
		else
		{
			this.LoadingScreenLabel.text = string.Empty;
		}
		if (SceneManager.GetActiveScene().name == "CustomMap")
		{
			this.LoadingScreenLabel.text = "Level editor builds your level, it may take some time. Wait...";
		}
		this.ChatBox.SetActive(GameState.GameMode == GameMode.Multiplayer);

		InitUGS();
	}

	private void Start()
	{
		this.MapScript = this.Map.GetComponent<NavigationMap>();
		Color color = this.NotificationText.color;
		color.a = 0f;
		this.NotificationText.color = color;
		this.DroneModeChanged(false);
		this.ToggleCarControls(true);
		this.ToggleCarExtras(true);
		this.ToggleWinchControls(true);
		this.HideEventLobby();
		this.HideShowCountdown(false);
		this.HideShowRaceUI(false, false);
		this.SwitchWinchTowButton(false);
		this.SwitchWinchTargetSelector(false);
		this.SwitchFlipButton(false);
		this.HideWinchRequestWindow();
		this.ToggleAttachButton(false, false);
		this.SwitchDetachButton(false);
		this.PasswordPanel.SetActive(false);
		this.ToggleReadyToRaceWindow(false);
		this.HideShoTrailRaceFinishWindow(false, string.Empty, 0f, 0f, string.Empty, string.Empty);
		this.HideShowOtherPlayerDisconnectedWindow(false);
		this.HidePlayerRouteFinish();
		this.detachTrailerButton.SetActive(false);
		this.attachTrailerButton.SetActive(false);
		this.swapVehiclesButton.gameObject.SetActive(false);
		this.loadOnOtherPlayerTrailerButton.SetActive(false);
		this.unloadFromOtherPlayerTrailerButton.SetActive(false);
		this.traileringRequestWindow.SetActive(false);
		this.waitingForLoadOnTrailerResponseWindow.SetActive(false);
		this.droneExtras.SetActive(false);
		this.droneControls.SetActive(false);
		this.followVehicleOnDroneWindow.SetActive(false);
		this.cancelFollowingVehicleButton.SetActive(false);
		this.RepairWindow.SetActive(false);
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			if (GameState.GameType == GameType.TrailRace)
			{
				this.RaceBetText.gameObject.SetActive(true);
				string text = "$" + GameState.TrailRaceBet.ToString();
				if (text == "$0")
				{
					text = "FREE RACE";
				}
				this.RaceBetText.text = "Race bet: " + text;
			}
			if (GameState.Password != null && GameState.Password != string.Empty)
			{
				this.PasswordPanel.SetActive(true);
				this.MapPassword.text = "Map password: " + GameState.Password;
			}
		}
		foreach (GameObject gameObject in this.TouchAccelerators)
		{
			gameObject.SetActive(!DataStore.GetBool("SlideAccelerator", false));
		}
		foreach (GameObject gameObject2 in this.SlideAccelerators)
		{
			gameObject2.SetActive(DataStore.GetBool("SlideAccelerator", false));
		}
		this.ArrowControls.SetActive(false);
		this.SteeringWheelControls.SetActive(false);
		this.TiltControls.SetActive(false);
		this.controlType = (CarUIControl.ControlType)DataStore.GetInt("ControlsType", 0);
		this.ToggleCarControls(true);
		if (this.Map != null)
		{
			this.Map.SetActive(false);
		}
		if (DataStore.GetInt("GameSound", 1) == 0)
		{
			AudioListener.volume = 0f;
		}
		else
		{
			AudioListener.volume = 1f;
		}
		this.PlayerInformationTemplate = Resources.Load<GameObject>("UI/PlayerInfoPanel");
		this.droneInfoTemplate = Resources.Load<GameObject>("UI/DroneInfoPanel");
		if (GameState.GameMode != GameMode.Multiplayer)
		{
			this.MultiplayerLabelsButton.SetActive(false);
		}
		else
		{
			this.MultiplayerLabelsButton.SetActive(true);
		}
		for (int k = 0; k < 10; k++)
		{
			this.DirectionalArrowsPool.Add(UnityEngine.Object.Instantiate<Image>(this.DirectionalArrow, this.DirectionalArrow.transform.parent));
			this.DirectionalArrowsPool[k].gameObject.SetActive(false);
		}
		
		if(!IsTutorial()) Advertisements.Instance.ShowInterstitial();

		if (storeListener == null)
		{
			storeListener = new StoreListener();
			storeListener.InitializeIAP();
		}
		else
		{
			Debug.Log("Already initialized");
		}		

		var statsData = GameState.LoadStatsData();
		hasUnlimitedFuel = statsData.HasUnlimitedFuel;
		
		if(IsTutorial()) StartCoroutine(StartTrail());
	}

	private bool IsTutorial() => PlayerPrefs.GetInt("Tutorial", 0).Equals(0);
	private IEnumerator StartTrail()
	{
		yield return new WaitForSeconds(0);
		GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "Tutorial", "Garage", "BuyCar");
		StartRace();
	}

	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			this.ToggleCarControls(this.CarControlsEnabled);
		}
	}

	public void DroneModeChanged(bool droneMode)
	{
		this.enterDroneModeButton.gameObject.SetActive(!droneMode);
		this.returnToVehicleButton.gameObject.SetActive(droneMode);
	}

	public void ToggleMultiplayerLabels()
	{
		this.ShowMultiplayerLabels = !this.ShowMultiplayerLabels;
	}

	public void ToggleMap()
	{
		this.Map.SetActive(!this.Map.activeSelf);
	}

	public void ToggleEbrake(bool Show)
	{
		foreach (GameObject gameObject in this.EbrakeButtons)
		{
			gameObject.SetActive(Show);
		}
	}

	public void ToggleGearShifter(bool Show)
	{
		foreach (GameObject gameObject in this.GearShifters)
		{
			gameObject.SetActive(Show);
		}
	}

	private void Update()
	{
		UpdateAdsTimer();

		Color color = this.NotificationText.color;
		color.a = Mathf.MoveTowards(color.a, 0f, Time.deltaTime);
		if (this.notificationBlinking && color.a == 0f)
		{
			color.a = 1f;
		}
		this.NotificationText.color = color;
		if (this.engine != null && this.carController != null)
		{
			this.DoGauges();
		}
		if (this.CurrentCheckpoint != null)
		{
			this.DoCheckpointArrow();
		}
		this.DoLockbox();
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			this.DoPlayerInfoBoxes();
			this.DoDroneInfoBoxes();
		}
		if (GameState.GameMode == GameMode.Multiplayer && MultiplayerManager.CurrentPlayers != null && this.MapScript != null)
		{
			if (this.MapScript.OtherCars.Length != MultiplayerManager.CurrentPlayers.Count)
			{
				this.MapScript.OtherCars = new Transform[MultiplayerManager.CurrentPlayers.Count];
			}
			else
			{
				List<Transform> list = new List<Transform>();
				for (int i = 0; i < MultiplayerManager.CurrentPlayers.Count; i++)
				{
					bool flag = false;
					for (int j = 0; j < this.MapScript.OtherCars.Length; j++)
					{
						if (MultiplayerManager.CurrentPlayers[i] != null && this.MapScript.OtherCars[j] == MultiplayerManager.CurrentPlayers[i].transform)
						{
							flag = true;
						}
					}
					if (!flag && MultiplayerManager.CurrentPlayers[i] != null)
					{
						list.Add(MultiplayerManager.CurrentPlayers[i].transform);
					}
				}
				if (list.Count > 0)
				{
					for (int k = 0; k < MultiplayerManager.CurrentPlayers.Count; k++)
					{
						if (MultiplayerManager.CurrentPlayers[k] != null)
						{
							this.MapScript.OtherCars[k] = MultiplayerManager.CurrentPlayers[k].transform;
						}
					}
				}
			}
		}
		if (GameState.GameMode == GameMode.Multiplayer && MultiplayerManager.CurrentPlayers != null && MultiplayerManager.CurrentPlayers.Count > 0 && MultiplayerManager.CurrentPlayers[0] != null)
		{
			if (MultiplayerManager.CurrentPlayers.Count != this.DirectionalArrows.Count)
			{
				for (int l = 0; l < this.DirectionalArrows.Count; l++)
				{
					if (this.DirectionalArrows[l] != null)
					{
						this.DirectionalArrows[l].gameObject.SetActive(false);
					}
				}
				this.DirectionalArrows.Clear();
				for (int m = 0; m < MultiplayerManager.CurrentPlayers.Count; m++)
				{
					this.DirectionalArrows.Add(this.DirectionalArrowsPool[m]);
					this.DirectionalArrows[this.DirectionalArrows.Count - 1].gameObject.SetActive(true);
					this.DirectionalArrows[this.DirectionalArrows.Count - 1].color = new Color(0f, 1f, 0f, 0.8f);
				}
			}
			int num = 0;
			while (num < this.DirectionalArrows.Count && num < MultiplayerManager.CurrentPlayers.Count)
			{
				if (MultiplayerManager.CurrentPlayers[num] != null)
				{
					this.DoDirectionalArrow(this.DirectionalArrows[num], MultiplayerManager.CurrentPlayers[num].transform);
				}
				else if (this.DirectionalArrows[num] != null)
				{
					this.DirectionalArrows[num].gameObject.SetActive(false);
				}
				num++;
			}
		}
		else
		{
			this.DirectionalArrow.gameObject.SetActive(false);
			foreach (Image image in this.DirectionalArrows)
			{
				if (image != null)
				{
					image.gameObject.SetActive(false);
				}
			}
		}
	}

	private void UpdateAdsTimer()
	{

		if (fuelAmountCurrent <= 1)
		{
			refuelPanel.SetActive(true);
			isAdsPanelOpened = true;
		}
	}

	public void ShowInterstitial()
	{
		fuelAmountCurrent = fuelAmountMax;
		isAdsPanelOpened = false;
		Advertisements.Instance.ShowInterstitial();
	}
	public PlayerInfoUI CreatePlayerInfoBox(PhotonView v)
	{
		Hashtable customProperties = v.owner.CustomProperties;
		bool isMember = false;
		string name = "-------";
		if (customProperties.ContainsKey("IsMember"))
		{
			isMember = bool.Parse(customProperties["IsMember"].ToString());
		}
		if (customProperties.ContainsKey("DisplayName"))
		{
			name = customProperties["DisplayName"].ToString();
		}
		PlayerInfoUI component = UnityEngine.Object.Instantiate<GameObject>(this.PlayerInformationTemplate, base.transform).GetComponent<PlayerInfoUI>();
		component.Populate(name, isMember, v);
		this.playerInfoBoxes.Add(component);
		this.DoPlayerInfoBoxes();
		return component;
	}

	public DroneInfoUI CreateDroneInfoBox(DroneController dc, PhotonView v)
	{
		Hashtable customProperties = v.owner.CustomProperties;
		bool isMember = false;
		string name = "-------";
		if (customProperties.ContainsKey("IsMember"))
		{
			isMember = bool.Parse(customProperties["IsMember"].ToString());
		}
		if (customProperties.ContainsKey("DisplayName"))
		{
			name = customProperties["DisplayName"].ToString();
		}
		DroneInfoUI component = UnityEngine.Object.Instantiate<GameObject>(this.droneInfoTemplate, base.transform).GetComponent<DroneInfoUI>();
		component.Populate(name, isMember, dc);
		this.droneInfoBoxes.Add(component);
		this.DoDroneInfoBoxes();
		return component;
	}

	private void DoPlayerInfoBoxes()
	{
		foreach (PlayerInfoUI playerInfoUI in this.playerInfoBoxes)
		{
			if (playerInfoUI == null)
			{
				this.playerInfoBoxes.Remove(playerInfoUI);
				break;
			}
			if (!(playerInfoUI.myView == null))
			{
				if (this.carController == null)
				{
					break;
				}
				float value = Vector3.Distance(playerInfoUI.myView.transform.position, this.camController.transform.position);
				float num = 1f - Mathf.InverseLerp(0f, 50f, value);
				if (!this.ShowMultiplayerLabels)
				{
					num = 0f;
				}
				Vector3 vector = Camera.main.WorldToScreenPoint(playerInfoUI.myView.transform.position + Vector3.up * 1.3f);
				if (vector.z < 0f)
				{
					num = 0f;
				}
				playerInfoUI.alpha = Mathf.Sqrt(num);
				playerInfoUI.transform.position = new Vector3(vector.x, vector.y, 0f);
				float d = 0.1f;
				float d2 = 0.5f;
				playerInfoUI.transform.localScale = Vector3.Lerp(Vector3.one * d, Vector3.one * d2, num);
			}
		}
	}

	private void DoDroneInfoBoxes()
	{
		foreach (DroneInfoUI droneInfoUI in this.droneInfoBoxes)
		{
			if (droneInfoUI == null)
			{
				this.droneInfoBoxes.Remove(droneInfoUI);
				break;
			}
			if (!(droneInfoUI.myDrone == null))
			{
				if (this.carController == null)
				{
					break;
				}
				float value = Vector3.Distance(droneInfoUI.myDrone.transform.position, this.camController.transform.position);
				float num = 1f - Mathf.InverseLerp(0f, 50f, value);
				if (!this.ShowMultiplayerLabels)
				{
					num = 0f;
				}
				Vector3 vector = Camera.main.WorldToScreenPoint(droneInfoUI.myDrone.transform.position + Vector3.up * 0.5f);
				if (vector.z < 0f)
				{
					num = 0f;
				}
				droneInfoUI.alpha = Mathf.Sqrt(num);
				droneInfoUI.transform.position = new Vector3(vector.x, vector.y, 0f);
				float d = 0.1f;
				float d2 = 0.5f;
				droneInfoUI.transform.localScale = Vector3.Lerp(Vector3.one * d, Vector3.one * d2, num * num);
			}
		}
	}

	public void ShowFollowVehicleWindow(string playerName)
	{
		this.followVehicleOnDroneText.text = "Do you want to follow " + playerName + "?";
		this.followVehicleOnDroneWindow.SetActive(true);
	}

	public void StartFollowingVehicle()
	{
		VehicleLoader.Instance.playerDrone.StartFollowingObject(VehicleLoader.Instance.carToFollow);
		this.followVehicleOnDroneWindow.SetActive(false);
		this.cancelFollowingVehicleButton.SetActive(true);
	}

	public void StopFollowingVehicle()
	{
		VehicleLoader.Instance.playerDrone.StopFollowingObject();
		this.followVehicleOnDroneWindow.SetActive(false);
		this.cancelFollowingVehicleButton.SetActive(false);
	}

	private void DoLockbox()
	{
		if (StashManager.Instance != null)
		{
			if (StashManager.Instance.LockboxActive)
			{
				if (this.LockboxArrow == null)
				{
					this.LockboxArrow = UnityEngine.Object.Instantiate<Image>(this.DirectionalArrow, this.DirectionalArrow.transform.parent);
					this.LockboxArrow.gameObject.SetActive(true);
					this.LockboxArrow.color = new Color(1f, 0f, 0f, 0.8f);
				}
				if (this.LockboxArrow != null)
				{
					this.DoDirectionalArrow(this.LockboxArrow, StashManager.Instance.CurrentLockbox.transform);
					this.LockboxArrow.gameObject.SetActive(true);
				}
			}
			else if (this.LockboxArrow != null && this.LockboxArrow.gameObject.activeInHierarchy)
			{
				this.LockboxArrow.gameObject.SetActive(false);
			}
		}
	}

	public void ShowMessage(string text)
	{
		this.MessageBox.SetActive(true);
		this.MessageText.text = text;
	}

	public void HideMessage()
	{
		this.MessageBox.SetActive(false);
	}

	private void DoGauges()
	{
		this.SpeedText.text = Mathf.Abs((int)this.carController.Speed).ToString();
		this.RevsPointer.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(this.TachoMinAngle, this.TachoMaxAngle, Mathf.InverseLerp(this.MinRevs, this.MaxRevs, Mathf.Abs(this.engine.RPM))));
		this.HPBar.fillAmount = this.carController.CarHealth / 100f;
		this.InclinometerBG.localPosition = new Vector3(0f, Mathf.LerpUnclamped(-30f, 0f, this.carController.LongTilt + 1f), 0f);
		this.InclinometerHolder.eulerAngles = new Vector3(0f, 0f, Mathf.LerpUnclamped(-90f, 0f, this.carController.LatTilt + 1f));
		
		UpdateFuel();
	}

	public void LockboxCountdown(float timeLeft)
	{
		if (!this.TickingSound.isPlaying)
		{
			this.TickingSound.Play();
		}
		this.TickingSound.pitch = 1f + Mathf.Clamp(1f / timeLeft, 0.01f, 4f);
	}

	public void LockboxBomb()
	{
		this.BombSound.Play();
	}

	private void DoCheckpointArrow()
	{
		Vector3 vector = this.carController.transform.InverseTransformDirection(this.CurrentCheckpoint.position - this.carController.transform.position);
		float z = Mathf.Atan2(-vector.x, vector.z) * 57.29578f;
		this.CheckpointArrow.rectTransform.eulerAngles = new Vector3(0f, 0f, z);
		Vector3 vector2 = Camera.main.WorldToViewportPoint(this.CurrentCheckpoint.position);
		this.LeftSideArrow.SetActive(vector2.x < 0f);
		this.RightSideArrow.SetActive(vector2.x > 1f);
	}

	[ContextMenu("Fix fonts")]
	private void FixFonts()
	{
		foreach (Text text in base.GetComponentsInChildren<Text>(true))
		{
			text.font = this.font;
		}
	}

	private void DoDirectionalArrow(Image arrow, Transform target)
	{
		Vector3 vector = Camera.main.transform.InverseTransformDirection(target.position - Camera.main.transform.position);
		float z = Mathf.Atan2(-vector.x, vector.z) * 57.29578f;
		arrow.rectTransform.eulerAngles = new Vector3(0f, 0f, z);
		arrow.gameObject.SetActive(true);
	}

	public void SetCurrentGear(int number)
	{
		foreach (Text text in this.GearTexts)
		{
			text.text = number.ToString();
			if (number == -1)
			{
				text.text = "N";
			}
			if (number == -2)
			{
				text.text = "R";
			}
		}
	}

	public void SetupGearButton(int SelectedPosition)
	{
		for (int i = 0; i < this.GearButtons.Length; i++)
		{
			this.GearButtons[i].SetActive(i == SelectedPosition);
		}
	}

	public void SetupDiffLockButton(int SelectedPosition)
	{
		for (int i = 0; i < this.DiffLockButtons.Length; i++)
		{
			this.DiffLockButtons[i].SetActive(i == SelectedPosition);
		}
	}

	public void SetupDriveButton(int SelectedPosition)
	{
		for (int i = 0; i < this.DriveButtons.Length; i++)
		{
			this.DriveButtons[i].SetActive(i == SelectedPosition);
		}
	}

	public void HideAllDrivetrainOptions()
	{
		for (int i = 0; i < this.GearButtons.Length; i++)
		{
			this.GearButtons[i].SetActive(false);
		}
		for (int j = 0; j < this.DiffLockButtons.Length; j++)
		{
			this.DiffLockButtons[j].SetActive(false);
		}
		for (int k = 0; k < this.DriveButtons.Length; k++)
		{
			this.DriveButtons[k].SetActive(false);
		}
	}

	public void SwitchWinchTargetSelector(bool Show)
	{
		this.LeftArrowButton.SetActive(Show);
		this.RightArrowButton.SetActive(Show);
		this.LandAnchorButton.SetActive(Show);
	}

	public void ToggleAttachButton(bool DynamicTarget, bool Show)
	{
		this.AttachButton.SetActive(Show && !DynamicTarget);
		this.SendWinchRequestButton.SetActive(Show && DynamicTarget);
	}

	public void ShowWinchRequestWindow(string text)
	{
		this.WinchRequestWindow.SetActive(true);
		this.WinchRequestText.text = text;
	}

	private void HideWinchRequestWindow()
	{
		this.WinchRequestWindow.SetActive(false);
	}

	public void SwitchWinchToggleButton(bool Show)
	{
		this.ToggleButton.SetActive(Show);
	}

	public void ToggleRepairButton(bool Show)
	{
		this.RepairButton.SetActive(Show);
	}

	public void SwitchWinchTowButton(bool Show)
	{
		this.TowButton.SetActive(Show);
	}

	public void SwitchFlipButton(bool Show)
	{
		this.FlipButton.SetActive(Show);
	}

	public void SwitchDetachButton(bool Show)
	{
		this.DetachAttachedCarButton.SetActive(Show);
	}

	public void UpdateTimer(float Seconds)
	{
		this.RaceTimeText.text = string.Format("{0:0}:{1:00}:{2:0}", Mathf.Floor(Seconds / 60f), Mathf.Floor(Seconds) % 60f, Mathf.Floor(Seconds * 10f % 10f));
	}

	public void UpdateSwapButtonText(int swapTimer)
	{
		this.swapButtonText.text = "Swap vehicles";
		if (swapTimer > 0)
		{
			Text text = this.swapButtonText;
			string text2 = text.text;
			text.text = string.Concat(new object[]
			{
				text2,
				" (",
				swapTimer,
				")"
			});
		}
		this.swapVehiclesButton.interactable = (swapTimer == 0);
	}

	public void UpdateThermometer(float TemperatureRatio)
	{
		this.TemperatureArrow.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(this.ThermometerMinAngle, this.ThermometerMaxAngle, TemperatureRatio));
	}
	public void UpdateFuel()
	{
		if(hasUnlimitedFuel) return;

		float fuelConsumption = Mathf.Abs(carController.CurrentTorque / carController.LeveledMaxTorque) * Time.deltaTime / carFuelConsumption;

		fuelAmountCurrent -= fuelConsumption;

		var fuelRatio = Mathf.Clamp01(fuelAmountCurrent / fuelAmountMax);
		
		this.FuelArrow.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(this.FuelMinAngle, this.FuelMaxAngle, fuelRatio));
	}

	public void UpdateWinchUsedText(int WinchUsedTimes)
	{
		this.WinchUsedText.text = "Winch used " + WinchUsedTimes + " times";
	}

	public void UpdateLapText(int Lap, int LapsNumber)
	{
		this.LapText.text = string.Concat(new object[]
		{
			"Lap ",
			Lap,
			"/",
			LapsNumber
		});
	}

	public void ShowCountdownText(int Seconds)
	{
		this.CountdownText.text = Seconds.ToString();
	}

	public void HideShowCountdown(bool Show)
	{
		this.Countdown.SetActive(Show);
	}

	public void HideShowOtherPlayerDisconnectedWindow(bool Show)
	{
		this.OtherPlayerDisconnectedWindow.SetActive(Show);
		this.OpponentLeftRewardText.text = "Your opponent left!";
	}

	public void ToggleReadyToRaceWindow(bool Show)
	{
		this.ReadyToRaceWindow.SetActive(Show);
	}

	public void HideShowRaceUI(bool Show, bool ShowCancelButton)
	{
		this.InRaceUI.SetActive(Show);
		this.RaceCancelButton.SetActive(ShowCancelButton);
	}

	public void ShowRammingWindow(string playerName)
	{
		this.RammingPlayerName.text = "Looks like player " + playerName + " is ramming you. Block collisions with him?";
		this.RammingWindow.SetActive(true);
	}

	public void ToggleRearView()
	{
		this.camController.ForceRearView = !this.camController.ForceRearView;
	}

	public void RepairVehicle()
	{
		StatsData statsData = GameState.LoadStatsData();
		int num = (int)(1000f * (100f - this.carController.CarHealth) / 100f);
		if (statsData.Money > num)
		{
			this.carController.CarHealth = 100f;
			GameState.SubtractCurrency(num, Currency.Money);
		}
		else if (statsData.Gold > Utility.CashToGold(num))
		{
			this.carController.CarHealth = 100f;
			GameState.SubtractCurrency(Utility.CashToGold(num), Currency.Gold);
		}
		else
		{
			this.ShowNotification("Not enough money!", false);
		}
	}

	public void CalculateRepairCost()
	{
		int num = (int)(1000f * (100f - this.carController.CarHealth) / 100f);
		this.RepairCostText.text = "Repair vehicle for $" + num + "?";
	}

	public void ShowEventLobby(Route route)
	{
		this.EventLobby.SetActive(true);
		this.RecordTime.text = "--Loading--";
		foreach (Text text in this.AwardTimes)
		{
			text.text = "----";
		}
		foreach (Text text2 in this.AwardAmounts)
		{
			text2.text = "----";
		}
	}

	public void DisplayEventInfo()
	{
		long @long = DataStore.GetLong(RouteManager.Instance.mapName + RacingManager.Instance.ActiveRoute.RouteName + this.carController.vehicleDataManager.vehicleType.ToString());
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)((float)@long / 100f));
		this.RecordTime.text = string.Format("{0:D2}:{1:D2}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.TotalMilliseconds / 10.0);
		if (@long == -1L || @long == 0L)
		{
			RouteGoal routeGoal = RouteGoal.Default(@long, RacingManager.Instance.ActiveRoute, this.carController.vehicleDataManager.vehicleType);
			this.RecordTime.text = "----";
			this.AwardTimes[0].text = "Finish";
			this.AwardAmounts[0].text = routeGoal.BaseCashPayment.ToString("$0,0");
		}
		else
		{
			RouteGoal routeGoal2;
			if (RacingManager.Instance.ActiveRoute.RouteGoals.Count > 0)
			{
				routeGoal2 = RacingManager.Instance.ActiveRoute.RouteGoals[0];
			}
			else
			{
				routeGoal2 = RouteGoal.Default(@long, RacingManager.Instance.ActiveRoute, this.carController.vehicleDataManager.vehicleType);
			}
			for (int i = 0; i < 4; i++)
			{
				RouteGoalLimit limits = routeGoal2.GetLimits((AwardLevel)i);
				TimeSpan timeSpan2 = TimeSpan.FromSeconds((double)((float)limits.TimeLimit / 100f));
				if (i == 0)
				{
					this.AwardTimes[i].text = "Finish";
				}
				else
				{
					this.AwardTimes[i].text = string.Format("{0:D2}:{1:D2}.{2:00}", timeSpan2.Minutes, timeSpan2.Seconds, timeSpan2.Milliseconds / 10);
				}
				this.AwardAmounts[i].text = (routeGoal2.BaseCashPayment + routeGoal2.LevelUpCashIncrement * i).ToString("$0,0");
			}
		}
	}

	public void ShowPause()
	{
		bool flag = this.racingManager != null && this.racingManager.InRace;
		
		if (PlayerRouteRacingManager.Instance != null && PlayerRouteRacingManager.Instance.inRace)
		{
			flag = true;
		}
		this.enterDroneModeButton.interactable = (DataStore.GetBool("DronePurchased", false) && GameState.GameType != GameType.TrailRace && GameState.GameType != GameType.CaptureTheFlag && !flag);
		AudioListener.volume = 0f;
		Time.timeScale = 0f;
		this.PausePanel.SetActive(true);
	}

	public void ShowTeam(PunTeams.Team myTeam)
	{
		this.ShowNotification(string.Concat(new object[]
		{
			"You are on team ",
			(myTeam != PunTeams.Team.blue) ? "RED" : "BLUE",
			"(",
			PhotonNetwork.playerList.Length,
			")"
		}), false);
	}

	public void Unpause(bool exiting = true)
	{
		if (exiting && PhotonNetwork.inRoom)
		{
			PhotonNetwork.LeaveRoom();
		}
		if (exiting)
		{
			AudioListener.volume = 0f;
		}
		else if (DataStore.GetInt("GameSound", 1) == 1)
		{
			AudioListener.volume = 1f;
		}
		Time.timeScale = 1f;
		this.LoadingScreen.GetComponent<LoadMainScene>().sceneToLoad = "Menu";
		this.LoadingScreen.SetActive(exiting);
		this.LoadingScreenLabel.text = "Going back home!";
		this.PausePanel.SetActive(false);
		this.CaptureTheFlagGameOverMessage.SetActive(false);
		if (this.racingManager != null)
		{
			this.racingManager.SaveVehicleData();
		}
		if (exiting)
		{
			//SceneManager.LoadScene("Menu");
		}
	}

	

	public void LaunchDrone()
	{
		this.Unpause(false);
		VehicleLoader.Instance.EnterDroneMode();
		this.carController.CancelTrailerLoadWaiting();
		if (WinchManager.Instance.WinchMode)
		{
			WinchManager.Instance.ToggleWinch();
		}
		this.HideWinchRequestWindow();
		this.traileringRequestWindow.SetActive(false);
		if (this.racingManager != null && this.racingManager.InRace)
		{
			this.racingManager.CancelRace();
		}
		if (PlayerRouteRacingManager.Instance != null && PlayerRouteRacingManager.Instance.inRace)
		{
			PlayerRouteRacingManager.Instance.CancelRace();
		}
		this.HideEventLobby();
		this.FinishInfo.SetActive(false);
		this.RepairWindow.SetActive(false);
		this.RammingWindow.SetActive(false);
		this.ToggleWinchControls(false);
		if (this.Map.activeSelf)
		{
			this.ToggleMap();
		}
		this.HideMessage();
		this.StopFollowingVehicle();
		this.ToggleCarControls(false);
		this.ToggleCarExtras(false);
		this.droneControls.SetActive(true);
		this.droneExtras.SetActive(true);
	}

	public void LandDrone()
	{
		this.Unpause(false);
		VehicleLoader.Instance.ExitDroneMode();
		this.droneControls.SetActive(false);
		this.followVehicleOnDroneWindow.SetActive(false);
		this.cancelFollowingVehicleButton.SetActive(false);
		this.ToggleWinchControls(true);
		this.ToggleCarControls(true);
		this.ToggleCarExtras(true);
		this.droneExtras.SetActive(false);
	}

	public void SwitchDroneCamera()
	{
		this.camController.SwitchDroneCamera();
	}

	public void CaptureTheFlagGameOver(PunTeams.Team winningTeam, PunTeams.Team myTeam)
	{
		Camera.main.GetComponent<AudioListener>().enabled = false;
		Time.timeScale = 0f;
		this.CaptureTheFlagGameOverText.text = ((winningTeam != PunTeams.Team.blue) ? "RED" : "BLUE") + " WON!";
		this.CaptureTheFlagGameOverMessage.SetActive(true);
	}

	public void HideEventLobby()
	{
		this.EventLobby.SetActive(false);
	}

	public void HideShowFinishWindow(bool Show, RoutePayment payment, long finishTime, long recordTime, int trailID, string recordKeeper)
	{
		this.FinishInfo.SetActive(Show);
		StatsData statsData = GameState.LoadStatsData();
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)((float)finishTime / 100f));
		TimeSpan timeSpan2 = TimeSpan.FromSeconds((double)((float)recordTime / 100f));
		this.FinishTime.text = string.Format("{0:D2}:{1:D2}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
		this.RecordTimeFinished.text = string.Format("{0:D2}:{1:D2}.{2:00}", timeSpan2.Minutes, timeSpan2.Seconds, timeSpan2.Milliseconds / 10);
		this.TrailIDText.text = trailID.ToString();
		this.RecordKeeperText.text = recordKeeper;
		this.AwardLevel.text = payment.AwardLevelString();
		this.MoneyWon.text = payment.Cash + ((!statsData.IsMember) ? string.Empty : "x3");
		this.GoldWon.text = payment.Gold + payment.TrailblazerGoldBonus + ((!statsData.IsMember) ? string.Empty : "x3");
		this.XPWon.text = payment.XP + ((!statsData.IsMember) ? string.Empty : "x3");
		this.TrailblazerLabel.SetActive(payment.Trailblazer);
		if (payment.Trailblazer)
		{
			this.ShowMessage("TRAILBLAZER! You were among the first to complete the route!\r\nGold Bonus: " + payment.TrailblazerGoldBonus.ToString() + ((!statsData.IsMember) ? string.Empty : "x3"));
		}
	}

	public void ToggleCarControls(bool Show)
	{
		this.ArrowControls.SetActive(Show && this.controlType == CarUIControl.ControlType.Arrow);
		this.TiltControls.SetActive(Show && this.controlType == CarUIControl.ControlType.Tilt);
		this.SteeringWheelControls.SetActive(Show && this.controlType == CarUIControl.ControlType.SteeringWheel);
		this.CarControlsEnabled = Show;
	}

	public void HideShoTrailRaceFinishWindow(bool Show, string ResultText, float raceTime, float opponentTime, string playerName, string opponentName)
	{
		this.TrailRaceFinishWindow.SetActive(Show);
		this.TrailRaceResultText.text = ResultText;
		this.PlayerName.text = playerName;
		this.OpponentName.text = opponentName;
		this.DateText.text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " " + Trails.GetByID(GameState.TrailID).TrailName;
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)raceTime);
		this.TrailRaceMyTimeText.text = string.Format("{0:D2}:{1:D2}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
		if (raceTime == -1f)
		{
			this.TrailRaceMyTimeText.text = "Disqualified";
		}
		TimeSpan timeSpan2 = TimeSpan.FromSeconds((double)opponentTime);
		if (opponentTime == 0f)
		{
			this.TrailRaceOpponentTimeText.text = "-----";
		}
		else if (opponentTime == -1f)
		{
			this.TrailRaceOpponentTimeText.text = "Disqualified";
		}
		else
		{
			this.TrailRaceOpponentTimeText.text = string.Format("{0:D2}:{1:D2}.{2:00}", timeSpan2.Minutes, timeSpan2.Seconds, timeSpan2.Milliseconds / 10);
		}
	}

	public void HidePlayerRouteInfo()
	{
		this.EventLobby.SetActive(false);
	}

	public void DisplayPlayerRouteInfo()
	{
		if (this.EventLobby.activeSelf)
		{
			return;
		}
		this.EventLobby.SetActive(true);
		this.RecordTime.text = "--Loading--";
		foreach (Text text in this.AwardTimes)
		{
			text.text = "----";
		}
		foreach (Text text2 in this.AwardAmounts)
		{
			text2.text = "----";
		}
	}

	public void DisplayPlayerRouteValues(float bronzeTime, float silverTime, float goldTime, float finishReward, float bronzeReward, float silverReward, float goldReward, float record)
	{
		if (bronzeTime != 0f)
		{
			this.AwardTimes[1].text = this.ConvertTime(bronzeTime);
		}
		if (silverTime != 0f)
		{
			this.AwardTimes[2].text = this.ConvertTime(silverTime);
		}
		if (goldTime != 0f)
		{
			this.AwardTimes[3].text = this.ConvertTime(goldTime);
		}
		this.AwardAmounts[0].text = finishReward.ToString();
		this.AwardAmounts[1].text = bronzeReward.ToString();
		this.AwardAmounts[2].text = silverReward.ToString();
		this.AwardAmounts[3].text = goldReward.ToString();
		this.RecordTime.text = this.ConvertTime(record);
		if (record == 0f)
		{
			this.RecordTime.text = "Not set";
		}
	}

	public void ShowPlayerRouteFinish(float time, float record, string keeper, string awardLevel, float xp, float golds, float money)
	{
		if (this.playerRouteFinishWindow == null)
		{
			return;
		}
		this.playerRouteFinishWindow.SetActive(true);
		this.playerRouteTime.text = this.ConvertTime(time);
		this.playerRouteRecord.text = this.ConvertTime(record);
		this.playerRouteRecordKeeper.text = keeper;
		this.playerRouteAwardLevel.text = awardLevel;
		this.playerRouteXP.text = xp.ToString();
		this.playerRouteGolds.text = golds.ToString();
		this.playerRouteMoney.text = money.ToString();
	}

	private string ConvertTime(float rawSeconds)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)rawSeconds);
		return string.Format("{0:D2}:{1:D2}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
	}

	public void DisplayPlayerRouteFinish()
	{
		if (this.playerRouteFinishWindow == null)
		{
			return;
		}
		this.playerRouteFinishWindow.SetActive(true);
	}

	public void HidePlayerRouteFinish()
	{
		if (this.playerRouteFinishWindow == null)
		{
			return;
		}
		this.playerRouteFinishWindow.SetActive(false);
	}

	public void HideShoTrailRaceFinishWindow(bool Show)
	{
		this.TrailRaceFinishWindow.SetActive(Show);
	}

	public void HideShowOfferRestartButton(bool Show)
	{
		this.OfferRestartButton.SetActive(Show);
	}

	public void HideShowRestartOffering(bool Show)
	{
		this.RestartOfferingWindow.SetActive(Show);
	}

	public void HideShowWaitForOtherPlayerButton(bool Show)
	{
		this.WaitForOtherPlayerButton.SetActive(Show);
	}

	public void HideShowSpectateButton(bool Show)
	{
		this.SpectateButton.SetActive(Show);
	}

	public void ToggleCarExtras(bool Show)
	{
		this.CarExtras.SetActive(Show);
	}

	public void ToggleWinchControls(bool Show)
	{
		this.WinchControls.SetActive(Show);
	}

	public void ShowNotification(string text, bool blinking)
	{
		this.notificationBlinking = blinking;
		this.NotificationText.text = text;
		Color color = this.NotificationText.color;
		color.a = 1f;
		this.NotificationText.color = color;
	}

	public void HideNotification()
	{
		this.notificationBlinking = false;
	}

	public void FlipCar()
	{
		this.carController.FlipCar();
	}

	public void RespawnCar()
	{
		if (GameState.GameType == GameType.TrailRace)
		{
			return;
		}
		if (PlayerRouteRacingManager.Instance != null && PlayerRouteRacingManager.Instance.inRace)
		{
			return;
		}
		this.carController.RespawnCar();
	}

	public void StartRace()
	{
		if (this.racingManager != null)
		{
			this.racingManager.StartRace();
		}
		if (PlayerRouteRacingManager.Instance != null)
		{
			PlayerRouteRacingManager.Instance.StartRace();
		}
	}

	public void CancelRace()
	{
		if (this.racingManager != null)
		{
			this.racingManager.CancelRace();
		}
		if (PlayerRouteRacingManager.Instance != null)
		{
			PlayerRouteRacingManager.Instance.CancelRace();
		}
	}

	public void Continue()
	{
		if (this.racingManager != null)
		{
			this.racingManager.Continue();
		}
		if (PlayerRouteRacingManager.Instance != null)
		{
			PlayerRouteRacingManager.Instance.Continue();
		}
	}

	public void GameOn()
	{
		this.Ding.Play();
	}

	public void ExtrasChanged(string TextToDisplay)
	{
		if (this.camController != null)
		{
			this.camController.Shake();
		}
		this.ToggleSound.volume = this.ToggleSoundVolume;
		this.ToggleSound.Play();
		this.ShowNotification(TextToDisplay, false);
	}

	public void SwitchCamera()
	{
		if (this.camController == null)
		{
			return;
		}
		this.ShowNotification(this.camController.SwitchCamera(), false);
	}

	public void ToggleGauge()
	{
		this.MainGauge.SetActive(!this.MainGauge.activeInHierarchy);
		this.HideGaugeImage.transform.rotation = Quaternion.Euler(0f, 0f, -this.HideGaugeImage.transform.rotation.eulerAngles.z);
	}

	public void ProcessPurchase(string product)
	{
		this.storeListener.PurchaseFromMap(product);
	}

	public void SetUnlimitedFuel(bool flag)
	{
		fuelAmountCurrent = fuelAmountMax;
		UpdateFuel();

		hasUnlimitedFuel = flag;
		
		refuelPanel.SetActive(false);
		
		ShowMessage("You have unlimited fuel now!!"); 
	}

	private async void InitUGS()
	{
		string environment = "production";
        try
        {
            var options = new InitializationOptions()
                .SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
           Debug.LogException(exception);
        }
	}

	[Header("Refuel Ads")]
	public bool hasUnlimitedFuel;
	public GameObject refuelPanel;
	private bool isAdsPanelOpened;
	private StoreListener storeListener;

	public CarUIControl.ControlType controlType;

	public AudioSource ToggleSound;

	public AudioSource TickingSound;

	public AudioSource BombSound;

	[Range(0f, 1f)]
	private float ToggleSoundVolume = 0.1f;

	public Text NotificationText;

	public GameObject[] GearButtons;

	public GameObject[] DiffLockButtons;

	public GameObject[] DriveButtons;

	public GameObject ChatBox;

	public GameObject WinchControls;

	public GameObject ArrowControls;

	public GameObject TiltControls;

	public GameObject SteeringWheelControls;

	public GameObject CarExtras;

	public GameObject FlipButton;

	public GameObject PausePanel;

	public GameObject LoadingScreen;

	public GameObject MessageBox;

	public GameObject ReadyToRaceWindow;

	public GameObject RepairButton;

	public Button enterDroneModeButton;

	public Button returnToVehicleButton;

	public GameObject droneExtras;

	public GameObject droneControls;

	public Text RepairCostText;

	public GameObject LandAnchorButton;

	public GameObject RepairWindow;

	public Text MessageText;

	public Text LoadingScreenLabel;

	public GameObject CaptureTheFlagGameOverMessage;

	public Text CaptureTheFlagGameOverText;

	public GameObject PasswordPanel;

	public Text MapPassword;

	public AudioSource Ding;

	public GameObject MultiplayerLabelsButton;

	public GameObject OtherPlayerDisconnectedWindow;

	public GameObject RammingWindow;

	public GameObject[] TouchAccelerators;

	public GameObject[] SlideAccelerators;

	public GameObject[] EbrakeButtons;

	public GameObject[] GearShifters;

	public Image DirectionalArrow;

	public Transform DirectionalArrowTarget;

	public Text RammingPlayerName;

	public Text swapButtonText;

	public GameObject followVehicleOnDroneWindow;

	public Text followVehicleOnDroneText;

	public GameObject cancelFollowingVehicleButton;

	public List<Image> DirectionalArrows = new List<Image>();

	public List<Image> DirectionalArrowsPool = new List<Image>();

	public Image LockboxArrow;

	public List<Text> PlayerNames = new List<Text>();

	[Header("Gauges")]
	public GameObject MainGauge;

	public Text SpeedText;

	public Text[] GearTexts;

	[Space(10f)]
	public float TachoMinAngle;

	public float TachoMaxAngle;

	public float MinRevs;

	public float MaxRevs;

	[Space(10f)]
	public RectTransform RevsPointer;

	[Space(10f)]
	public float ThermometerMinAngle;

	public float ThermometerMaxAngle;

	public RectTransform TemperatureArrow;
	
	public RectTransform FuelArrow;

	private float FuelMinAngle = 17;

	private float FuelMaxAngle = -37;

	private float fuelAmountCurrent = 85;
	private float fuelAmountMax = 85;
	public float carFuelConsumption = 20;
	

	[Space(10f)]
	public Image HPBar;

	[Space(10f)]
	public RectTransform InclinometerHolder;

	public RectTransform InclinometerBG;

	public float InclinometerMaxY;

	public float InclinometerMaxZ;

	private bool CarControlsEnabled;

	public GameObject HideGaugeImage;

	[Header("Winch")]
	public GameObject ToggleButton;

	public GameObject TowButton;

	public GameObject LeftArrowButton;

	public GameObject RightArrowButton;

	public GameObject AttachButton;

	public GameObject SendWinchRequestButton;

	public GameObject WinchRequestWindow;

	public Text WinchRequestText;

	public GameObject DetachAttachedCarButton;

	private bool notificationBlinking;

	[Header("Trailers")]
	public GameObject loadOnOtherPlayerTrailerButton;

	public GameObject unloadFromOtherPlayerTrailerButton;

	public GameObject detachTrailerButton;

	public GameObject attachTrailerButton;

	public Button swapVehiclesButton;

	public GameObject waitingForLoadOnTrailerResponseWindow;

	public GameObject traileringRequestWindow;

	[Header("Race controls")]
	public GameObject EventLobby;

	public GameObject InRaceUI;

	public GameObject FinishInfo;

	public GameObject Countdown;

	public Text RaceTimeText;

	public Text WinchUsedText;

	public Text FlipsText;

	public Text FinishText;

	public Text LapText;

	public Text RecordTime;

	public Text[] AwardTimes;

	public Text[] AwardAmounts;

	public Text TrailRaceResultText;

	public Text TrailRaceOpponentTimeText;

	public Text TrailRaceMyTimeText;

	public Text RecordKeeperText;

	public Text TrailIDText;

	public Text RecordDateText;

	public Text FinishTime;

	public Text RecordTimeFinished;

	public Text AwardLevel;

	public Text MoneyWon;

	public Text GoldWon;

	public Text XPWon;

	public Text RaceBetText;

	public Text OpponentLeftRewardText;

	public Text PlayerName;

	public Text OpponentName;

	public Text DateText;

	public GameObject RaceCancelButton;

	public GameObject TrailblazerLabel;

	public GameObject TrailRaceFinishWindow;

	public GameObject OfferRestartButton;

	public GameObject RestartOfferingWindow;

	public GameObject WaitForOtherPlayerButton;

	public GameObject SpectateButton;

	public GameObject LeftSideArrow;

	public GameObject RightSideArrow;

	public Image CheckpointArrow;

	public Text CountdownText;

	public GameObject Map;

	public NavigationMap MapScript;

	[HideInInspector]
	public Transform CurrentCheckpoint;

	public static CarUIControl Instance;

	[Header("Player routes")]
	public GameObject playerRouteFinishWindow;

	public Text playerRouteTime;

	public Text playerRouteRecord;

	public Text playerRouteRecordKeeper;

	public Text playerRouteAwardLevel;

	public Text playerRouteXP;

	public Text playerRouteGolds;

	public Text playerRouteMoney;

	private GameObject PlayerInformationTemplate;

	private GameObject droneInfoTemplate;

	public List<PlayerInfoUI> playerInfoBoxes = new List<PlayerInfoUI>();

	public List<DroneInfoUI> droneInfoBoxes = new List<DroneInfoUI>();

	private bool ShowMultiplayerLabels = true;

	public Font font;

	public enum ControlType
	{
		Arrow,
		Tilt,
		SteeringWheel
	}
}
