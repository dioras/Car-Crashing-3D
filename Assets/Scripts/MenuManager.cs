using System;
using System.Collections;
using System.Collections.Generic;
using CustomVP;
using Facebook.Unity;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UpgradeScripts;

public class MenuManager : MonoBehaviour
{
	public MenuManager()
	{
		if (MenuManager.Instance == null)
		{
			MenuManager.Instance = this;
		}
	}

	private bool blockRaycasts
	{
		get
		{
			return this.truckSellWindow.activeSelf || this.PlayMenu.activeSelf || this.MapMenu.activeSelf || this.PINEntryScreen.activeSelf || this.IAPMenu.activeSelf || this.MessageBox.activeSelf || this.dronePurchaseMessage.activeSelf || this.equipTrailerWarning.activeSelf || this.FieldFindBox.activeSelf || this.FramerateWarning.activeSelf;
		}
	}

	private void Start()
	{
		StatsData statsData = GameState.LoadStatsData();
		if (DataStore.GetLong("UpdateOpenedOn1") == 0L)
		{
			DataStore.SetLong("UpdateOpenedOn1", DateTime.Now.Ticks);
		}
		if (DataStore.GetLong("UpdateOpenedOn2") == 0L)
		{
			DataStore.SetLong("UpdateOpenedOn2", DateTime.Now.Ticks);
		}
		if (DataStore.GetLong("UpdateOpenedOn3") == 0L)
		{
			DataStore.SetLong("UpdateOpenedOn3", DateTime.Now.Ticks);
		}
		if (!DataStore.GetBool("Opened", false))
		{
			if (statsData == null)
			{
				statsData = new StatsData();
			}
			statsData.Money = 30000;
			statsData.Gold = 0;
			statsData.XP = 0;
			GameState.SaveStatsData(statsData);
			DataStore.SetBool("Opened", true);
			this.ShowMessage("We've given you $30,000 to start - visit the dealership!\r\n\r\nHint: That's enough for a truck and a quad!", true);
		}
		if (DataStore.GetString("GeneratedName", string.Empty) == string.Empty)
		{
			DataStore.SetString("GeneratedName", Utility.GenerateName());
		}
		this.UpdateStats();
		if (this.SelectedVehicleInGarageID != 0)
		{
			GameState.SelectedGarageVehicleID = this.SelectedVehicleInGarageID;
		}
		else
		{
			this.SelectedVehicleInGarageID = GameState.SelectedGarageVehicleID;
		}
		this.SetCameraTarget(this.GarageVehiclePoints[this.SelectedVehicleInGarageID].position, true);
		this.loadingSettingsFileMenu.SetActive(false);
		this.SettingsTab.SetActive(false);
		this.DefaultTab.SetActive(true);
		this.BodyPartColorBar.SetActive(false);
		this.WheelsColorBar.SetActive(false);
		this.truckSellWindow.SetActive(false);
		this.dronePurchaseMessage.SetActive(false);
		this.ApplySettings();
		if (GameState.FramerateWarning)
		{
			GameState.FramerateWarning = false;
			if (!DataStore.GetBool("IgnoreFramerateWarnings", false))
			{
				this.ShowFramerateWarning();
			}
		}
		int num = DataStore.LastFoundFieldFind() - 1;
		UnityEngine.Debug.Log("Last field find found: " + num);
		if (num >= 0)
		{
			string name = FieldFind.FieldFindNames[num];
			if (!Utility.OwnsVehicle(name) && Utility.FoundAllParts((num + 1).ToString()))
			{
				this.BuyFieldFindParts(false);
			}
		}
		for (int i = 0; i < this.StaticFieldFinds.Length; i++)
		{
			string text = FieldFind.FieldFindNames[i];
			this.StaticFieldFinds[i].SetActive(i == num && !Utility.OwnsVehicle(text));
			UnityEngine.Debug.Log(text + " set active? " + (i == num && !Utility.OwnsVehicle(text)).ToString());
		}
		MultiplayerManager.Connect();
		if (PhotonNetwork.inRoom)
		{
			PhotonNetwork.LeaveRoom();
		}
		if (PhotonNetwork.connectedAndReady && !PhotonNetwork.insideLobby)
		{
			PhotonNetwork.JoinLobby();
		}
		PlayFabSettings.TitleId = "433F";
		//FB.Init(new InitDelegate(this.OnFacebookInitialized), null, null);
		this.MembershipButton.SetActive(!statsData.IsMember);
		this.AlreadyMemberText.SetActive(statsData.IsMember);
		if (GameState.FailedToJoin)
		{
			this.ShowMessage("Something went wrong joining the room. Please try again.", true);
		}
		GameState.FailedToJoin = false;
		if (!GameState.JustOpenedGame)
		{
			DataStore.CloudSave(false);
		}
		GameState.JustOpenedGame = false;
		this.LoadMenu(MenuState.MainMenu, false, true);
		if (!GameState.settingsFileLoaded)
		{
			this.LoadSpecificGameSettings(null, false);
		}
		
		if(Time.time > 10) 	Advertisements.Instance.ShowInterstitial();
	}

	private void LoadSpecificGameSettings(Action callback, bool showErrorMessage)
	{
		if (this.loadSettingsCor != null)
		{
			base.StopCoroutine(this.loadSettingsCor);
			this.loadSettingsCor = null;
		}
		//this.loadSettingsCor = base.StartCoroutine(this.LoadSpecificGameSettingsCor(callback, showErrorMessage));
	}

	public void CancelLoadingGameSettings()
	{
		if (this.loadSettingsCor != null)
		{
			base.StopCoroutine(this.loadSettingsCor);
			this.loadSettingsCor = null;
		}
		this.loadingSettingsFileMenu.SetActive(false);
	}

	private IEnumerator LoadSpecificGameSettingsCor(Action successCallback, bool showErrorMessage)
	{
		WWW w = new WWW("http://offroadoutlaws.online/gameSettings.cfg");
		yield return w;
		string[] lines = w.text.Split(new char[]
		{
			'\n'
		});
		foreach (string text in lines)
		{
			string[] array2 = text.Split(new char[]
			{
				':'
			});
			if (array2.Length >= 2)
			{
				if (array2[0] == "CustomMapsAvailable")
				{
					bool.TryParse(array2[1], out GameState.customMapsAvailable);
				}
			}
		}
		if (lines.Length > 1)
		{
			GameState.settingsFileLoaded = true;
			if (successCallback != null)
			{
				successCallback();
			}
		}
		else if (showErrorMessage)
		{
			this.ShowMessage("Error occurred while connecting to server!", true);
			this.loadingSettingsFileMenu.SetActive(false);
		}
		this.loadSettingsCor = null;
		yield break;
	}

	public void Awake()
	{
		MenuManager.Instance = this;
		if (MenuManager.storeListener == null)
		{
			MenuManager.storeListener = new StoreListener();
			MenuManager.storeListener.InitializeIAP();
		}
		else
		{
			UnityEngine.Debug.Log("Already initialized");
		}		
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}

	public void LogoTapped()
	{
		UnityEngine.Debug.Log("Tapped");
		this.LogoTaps++;
		if (this.LogoTaps == 4)
		{
			this.LoadMenu(MenuState.EnterPIN, true, false);
			this.LogoTaps = 0;
		}
	}

	public void ConfirmPIN()
	{
		UnityEngine.Debug.Log("PIN WAS: " + this.CurrentPIN.text);
		if (this.CurrentPIN.text == "1")
		{
			this.devModeKeyboard = TouchScreenKeyboard.Open(string.Empty, TouchScreenKeyboardType.Default);
		}
		else
		{
			this.url = "http://www.racerslog.net/NoLimit/Remote/RemoteOO.aspx?password=" + this.CurrentPIN.text;
			base.StartCoroutine(this.MakeWebRequest());
		}
		this.LoadMainMenu(false);
	}

	public void HidePINEntry()
	{
		this.PINEntryScreen.SetActive(false);
	}

	private IEnumerator MakeWebRequest()
	{
		UnityEngine.Debug.Log("Making request");
		WWW www = new WWW(this.url);
		yield return www;
		string unlockString = www.text.Trim();
		int gold = 0;
		int cash = 0;
		int level = 0;
		int ads = 0;
		int membership = 0;
		string[] data = unlockString.Split(new char[]
		{
			','
		});
		int.TryParse(data[0], out gold);
		int.TryParse(data[1], out cash);
		int.TryParse(data[2], out level);
		int.TryParse(data[3], out ads);
		int.TryParse(data[4], out membership);
		if (gold > 0)
		{
			GameState.AddCurrency(gold, global::Currency.Gold);
		}
		if (cash > 0)
		{
			GameState.AddCurrency(cash, global::Currency.Money);
		}
		if (membership > 0)
		{
			GameState.SetMembership(true);
		}
		if (cash > 0 || gold > 0 || membership > 0)
		{
			this.ShowMessage("Success!", true);
		}
		else
		{
			this.ShowMessage("Invalid PIN!", true);
		}
		UnityEngine.Debug.Log("Response: " + www.text);
		this.UpdateStats();
		yield break;
	}

	private void Update()
	{
		this.CameraTarget.position = Vector3.Lerp(this.CameraTarget.position, this.cameraTargetPos, Time.deltaTime * 10f);
		Vector3 b = new Vector3((float)((!this.SideBarExpanded) ? -600 : -200), 0f, 0f);
		this.Sidebar.localPosition = Vector3.Lerp(this.Sidebar.localPosition, b, Time.deltaTime * 10f);
		if (this.enableCloudSave)
		{
			DataStore.CloudSave(false);
		}
		if (this.keyboard != null && this.keyboard.done)
		{
			this.ChangeTrailName();
			this.keyboard = null;
		}
		if (this.devModeKeyboard != null && this.devModeKeyboard.done)
		{
			if (this.devModeKeyboard.text == "1")
			{
				GameState.devRights = true;
				this.ShowMessage("You've got dev rights!", true);
			}
			else
			{
				this.ShowMessage("Wrong code!", true);
			}
			this.devModeKeyboard = null;
		}
		if (this.storeCallbackTimerCounting)
		{
			this.storeCallbackTimer += Time.deltaTime;
			if (this.storeCallbackTimer > 180f)
			{
				this.ShowMessage("Purhase timeout", true);
				this.storeCallbackTimerCounting = false;
			}
		}
	}

	private void OnApplicationQuit()
	{
		DataStore.CloudSave(false);
	}

	public void PrivateRace()
	{
		this.LoadMenu(MenuState.PrivateMultiplayer, true, false);
	}

	public void SelectTrail(int id)
	{
		GameState.TrailID = id;
		Trail trail = Trails.trails.Find((Trail t) => t.id == id);
		GameState.SceneName = trail.MapName;
		GameState.GameType = GameType.TrailRace;
		this.StartGame(string.Empty);
	}

	public void AddPasswordNumber(int number)
	{
		if (this.CurrentPassword.text.IndexOf("-") != -1)
		{
			this.CurrentPassword.text = string.Empty;
		}
		if (this.CurrentPassword.text.Length < 4)
		{
			Text currentPassword = this.CurrentPassword;
			currentPassword.text += number.ToString();
		}
	}

	public void RemovePasswordNumber()
	{
		if (this.CurrentPassword.text.Length > 0)
		{
			this.CurrentPassword.text = this.CurrentPassword.text.Substring(0, this.CurrentPassword.text.Length - 1);
		}
		if (this.CurrentPassword.text.Length == 0)
		{
			this.CurrentPassword.text = "----";
		}
	}

	public void AddPINNumber(int number)
	{
		if (this.CurrentPIN.text.IndexOf("-") != -1)
		{
			this.CurrentPIN.text = string.Empty;
		}
		if (this.CurrentPIN.text.Length < 6)
		{
			Text currentPIN = this.CurrentPIN;
			currentPIN.text += number.ToString();
		}
	}

	public void RemovePINNumber()
	{
		if (this.CurrentPIN.text.Length > 0)
		{
			this.CurrentPIN.text = this.CurrentPIN.text.Substring(0, this.CurrentPIN.text.Length - 1);
		}
		if (this.CurrentPIN.text.Length == 0)
		{
			this.CurrentPIN.text = "------";
		}
	}

	public void JoinPrivateNow()
	{
		if (this.CurrentPassword.text.Length != 4)
		{
			this.ShowMessage("Password must be 4 digits.", true);
			return;
		}
		RoomInfo roomInfo = MultiplayerManager.FindPrivateRoom(this.CurrentPassword.text);
		if (roomInfo != null)
		{
			this.HideWaiting();
			GameState.Password = this.CurrentPassword.text;
			GameState.GameType = (GameType)roomInfo.CustomProperties["GameType"];
			GameState.RoomName = roomInfo.Name;
			GameState.SceneName = roomInfo.CustomProperties["Scene"].ToString();
			if (roomInfo.CustomProperties["TrailID"] != null)
			{
				GameState.TrailID = (int)roomInfo.CustomProperties["TrailID"];
			}
			if (roomInfo.CustomProperties["TrailRaceBet"] != null)
			{
				GameState.TrailRaceBet = (int)roomInfo.CustomProperties["TrailRaceBet"];
			}
			this.StartGame(string.Empty);
		}
		else
		{
			this.HideWaiting();
			this.ShowMessage("Couldn't find that map. If it was just created you might need to try again in a moment.", true);
		}
	}

	public void HostNow()
	{
		string password = Utility.RandomDigits(4);
		GameState.RoomName = Utility.RandomDigits(10);
		this.GoPlaying(true, password);
	}

	public void HostTrailRace()
	{
		GameState.Populate(this.CurrentVehicle.VehicleID, null, GameMode.Multiplayer, GameType.TrailRace, string.Empty);
		this.SetRaceBet(0);
	}

	public void ShowFramerateWarning()
	{
		this.FramerateWarning.SetActive(true);
	}

	public void HideFramerateWarning()
	{
		this.FramerateWarning.SetActive(false);
	}

	public void IgnoreFramerateWarning(bool never)
	{
		DataStore.SetBool("IgnoreFramerateWarnings", never);
		this.HideFramerateWarning();
		if (never)
		{
			this.ShowMessage("Ok, remember: You can change the graphics quality in settings any time you want!", true);
		}
	}

	public void AcceptFramerateWarning()
	{
		int num = DataStore.GetInt("GraphicsLevel", 2);
		if (num > 0)
		{
			num--;
		}
		DataStore.SetInt("GraphicsLevel", num);
		this.ApplySettings();
		this.HideFramerateWarning();
		this.ShowMessage("Graphics level lowered!", true);
	}

	public void EditTrailName()
	{
		this.keyboard = TouchScreenKeyboard.Open(DataStore.GetString("GeneratedName"), TouchScreenKeyboardType.Default);
	}

	private void ChangeTrailName()
	{
		if (this.keyboard.text != string.Empty)
		{
			string text = this.keyboard.text;
			text = Utility.CleanBadWords(text);
			DataStore.SetString("GeneratedName", text);
			this.ApplySettings();
		}
	}

	public void ToggleControlsType()
	{
		int num = DataStore.GetInt("ControlsType");
		if (num >= 2)
		{
			num = 0;
		}
		else
		{
			num++;
		}
		DataStore.SetInt("ControlsType", num);
		this.ApplySettings();
	}

	public void ToggleTrailName()
	{
		if (AccessToken.CurrentAccessToken == null)
		{
			DataStore.SetBool("UseFBName", false);
		}
		else
		{
			DataStore.SetBool("UseFBName", !DataStore.GetBool("UseFBName", false));
		}
		this.ApplySettings();
	}

	public void ToggleSound()
	{
		int value = (DataStore.GetInt("GameSound") != 0) ? 0 : 1;
		DataStore.SetInt("GameSound", value);
		this.ApplySettings();
	}

	public void ToggleMusic()
	{
		int value = (DataStore.GetInt("BackgroundMusic") != 0) ? 0 : 1;
		DataStore.SetInt("BackgroundMusic", value);
		this.ApplySettings();
	}

	public void ToggleAccelerator()
	{
		bool flag = DataStore.GetBool("SlideAccelerator", false);
		flag = !flag;
		DataStore.SetBool("SlideAccelerator", flag);
		this.ApplySettings();
	}

	public void ToggleAirControl()
	{
		int @int = DataStore.GetInt("AirControl");
		int num = @int + 1;
		if (num > 0)
		{
			num = -2;
		}
		DataStore.SetInt("AirControl", num);
		this.ApplySettings();
	}

	public void TogglePostFX()
	{
		bool @bool = DataStore.GetBool("PostFX", true);
		DataStore.SetBool("PostFX", !@bool);
		this.ApplySettings();
	}

	public void ToggleGraphicsLevel()
	{
		int num = DataStore.GetInt("GraphicsLevel", 2);
		if (num >= 4)
		{
			num = 0;
		}
		else
		{
			num++;
		}
		DataStore.SetInt("GraphicsLevel", num);
		this.ApplySettings();
	}

	public void ApplySettings()
	{
		QualitySettings.SetQualityLevel(DataStore.GetInt("GraphicsLevel", 2), true);
		switch (QualitySettings.GetQualityLevel())
		{
		case 0:
			this.GraphicsLevel.text = "Very Low";
			break;
		case 1:
			this.GraphicsLevel.text = "Low";
			break;
		case 2:
			this.GraphicsLevel.text = "Medium";
			break;
		case 3:
			this.GraphicsLevel.text = "High";
			break;
		case 4:
			this.GraphicsLevel.text = "Very High";
			break;
		case 5:
			this.GraphicsLevel.text = "Ultra";
			break;
		}
		this.SoundStatus.text = ((DataStore.GetInt("GameSound", 1) != 0) ? "On" : "Off");
		this.MusicStatus.text = ((DataStore.GetInt("BackgroundMusic", 1) != 0) ? "On" : "Off");
		int @int = DataStore.GetInt("ControlsType", 0);
		if (@int != 0)
		{
			if (@int != 2)
			{
				if (@int == 1)
				{
					this.ControlsType.text = "Tilt";
				}
			}
			else
			{
				this.ControlsType.text = "Wheel";
			}
		}
		else
		{
			this.ControlsType.text = "Arrows";
		}
		if (DataStore.GetBool("SlideAccelerator", false))
		{
			this.AcceleratorType.text = "Slide";
		}
		else
		{
			this.AcceleratorType.text = "Touch";
		}
		if (DataStore.GetInt("BackgroundMusic", 1) == 0)
		{
			AudioListener.volume = 0f;
		}
		else
		{
			AudioListener.volume = 1f;
		}
		UnityEngine.Debug.Log(DataStore.GetString("GeneratedName"));
		if (AccessToken.CurrentAccessToken == null)
		{
			DataStore.SetBool("UseFBName", false);
		}
		bool @bool = DataStore.GetBool("UseFBName", false);
		if (@bool)
		{
			this.TrailName.text = GameState.PlayerName;
			this.TrailNameHint.text = "Tap to use generated name";
		}
		else
		{
			if (AccessToken.CurrentAccessToken == null)
			{
				this.TrailNameHint.text = "Tap the \"f\" to login using Facebook";
			}
			else
			{
				this.TrailNameHint.text = "Tap to use Facebook name";
			}
			this.TrailName.text = DataStore.GetString("GeneratedName");
		}
		PhotonNetwork.player.NickName = this.TrailName.text;
		string text = "Off";
		if (DataStore.GetInt("AirControl") == -1)
		{
			text = "Low";
		}
		if (DataStore.GetInt("AirControl") == 0)
		{
			text = "High";
		}
		this.airControlPowerText.text = text;
		string text2 = "Off";
		if (DataStore.GetBool("PostFX", true))
		{
			text2 = "On";
		}
		if (SimpleLUT.Instance != null)
		{
			SimpleLUT.Instance.enabled = DataStore.GetBool("PostFX", true);
		}
	}

	public void PurchaseProduct(string productName)
	{
		MenuManager.storeListener.PurchaseIAP(productName);
	}

	public void RestorePurchases()
	{
		MenuManager.storeListener.RestoreIAP();
	}

	public void LoadMainMenu(bool FromMainMenu)
	{
		this.LoadMenu(MenuState.MainMenu, true, FromMainMenu);
	}

	public void LoadVehicleTypeSelector(bool FromMainMenu)
	{
		this.LoadMenu(MenuState.TruckTypeSelector, true, FromMainMenu);
	}

	public void LoadVehicleSelector(bool FromMainMenu)
	{
		this.LoadMenu(MenuState.TruckSelector, true, FromMainMenu);
	}

	public void LoadStorageArea(bool FromMainMenu)
	{
		this.LoadMenu(MenuState.StorageArea, true, FromMainMenu);
	}

	public void LoadCustomizeCategorySelector(bool FromMainMenu)
	{
		if (this.CurrentVehicle != null && this.CurrentPartsSwitcher != null)
		{
			this.LoadMenu(MenuState.CustomizeCategorySelector, true, FromMainMenu);
		}
		else
		{
			this.ShowMessage("No vehicle!", true);
		}
	}

	public void LoadDrivetrain(bool FromMainMenu)
	{
		if (this.CurrentVehicle != null && this.CurrentSuspensionController != null)
		{
			this.LoadMenu(MenuState.Drivetrain, true, FromMainMenu);
		}
		else
		{
			this.ShowMessage("No vehicle!", true);
		}
	}

	public void LoadPower(bool FromMainMenu)
	{
		if (this.CurrentVehicle != null && this.CurrentCarController != null)
		{
			this.LoadMenu(MenuState.Power, true, FromMainMenu);
		}
		else
		{
			this.ShowMessage("No vehicle!", true);
		}
	}

	public void LoadIAPMenu()
	{
		this.IAPMenu.SetActive(true);
		this.Logo.SetActive(false);
	}

	public void LoadDronesMenu()
	{
		if (DataStore.GetBool("DronePurchased", false))
		{
			this.LoadMenu(MenuState.Drones, true, true);
		}
		else
		{
			this.dronePurchaseMessage.SetActive(true);
		}
	}

	public void BuyDrone()
	{
		if (this.ProcessPurchase(global::Currency.Gold, 100))
		{
			DataStore.SetBool("DronePurchased", true);
			this.LoadMenu(MenuState.Drones, true, true);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	public void ToggleLights()
	{
		this.LightsOn = !this.LightsOn;
		if (this.LoadedVehiclesInGarage != null)
		{
			foreach (VehicleDataManager vehicleDataManager in this.LoadedVehiclesInGarage)
			{
				LightsController component = vehicleDataManager.GetComponent<LightsController>();
				if (component != null)
				{
					component.LightsOn = this.LightsOn;
				}
			}
		}
		if (this.LoadedVehiclesInGarage != null && this.LoadedVehiclesInGarage.Count == 0 && this.CurrentVehicle != null)
		{
			LightsController component2 = this.CurrentVehicle.GetComponent<LightsController>();
			if (component2 != null)
			{
				component2.LightsOn = this.LightsOn;
			}
		}
	}

	public void HideIAPMenu()
	{
		this.IAPMenu.SetActive(false);
		if (this.menuState == MenuState.MainMenu)
		{
			this.Logo.SetActive(true);
		}
	}

	public void TryEnteringCustomMaps()
	{
		if (!GameState.settingsFileLoaded)
		{
			this.LoadSpecificGameSettings(new Action(this.LoadCommunityMapsMenu), true);
			this.loadingSettingsFileMenu.SetActive(true);
			return;
		}
		this.LoadCommunityMapsMenu();
	}

	public void LoadCommunityMapsMenu()
	{
		this.loadingSettingsFileMenu.SetActive(false);
		if (!GameState.customMapsAvailable)
		{
			this.ShowMessage("Custom maps are closed for maintainance or you have bad internet connection!", true);
			return;
		}
		this.LoadMenu(MenuState.CommunityMaps, true, true);
	}

	public void LoadPlay(bool FromMainMenu)
	{
		if (this.CurrentVehicle != null && this.CurrentCarController != null)
		{
			this.LoadMenu(MenuState.Play, true, FromMainMenu);
		}
		else if (this.CurrentVehicle != null)
		{
			this.ShowMessage("Choose a vehicle, you can't drive this!", true);
		}
		else
		{
			this.ShowMessage("No vehicle!", true);
		}
	}

	public void Play(bool isMultiplayer)
	{
		this.GoPlaying(isMultiplayer);
	}

	public void BuyMap(string mapName)
	{
		if (this.ProcessPurchase(global::Currency.Gold, 50))
		{
			DataStore.SetBool(mapName + "Unlocked", true);
			this.LoadMenu(MenuState.Map, false, false);
		}
		else
		{
			this.ShowMessage("You don't have enough gold. To continue, you can purchase gold, or do more races to earn more XP!", true);
			this.LoadIAPMenu();
		}
	}

	public void StartGame(string mapName = "")
	{
		if (mapName != string.Empty)
		{
			GameState.SceneName = mapName;
		}
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			base.StartCoroutine(this.StartMultiplayer());
		}
		else
		{
			this.LoadScene(mapName);
		}
	}

	private IEnumerator StartMultiplayer()
	{
		StatsData sData = GameState.LoadStatsData();
		if (GameState.SceneName == "MapDesertNG" && sData.XP < this.UnlockDesertXP && !DataStore.GetBool(GameState.SceneName + "Unlocked", false) && !sData.IsMember)
		{
			this.BuyMap("MapDesertNG");
			yield break;
		}
		if (GameState.SceneName == "StuntPark" && sData.XP < this.UnlockStuntParkXP && !DataStore.GetBool(GameState.SceneName + "Unlocked", false) && !sData.IsMember)
		{
			this.BuyMap("StuntPark");
			yield break;
		}
		if (GameState.SceneName == "RockParkNG" && sData.XP < this.UnlockRockParkXP && !DataStore.GetBool(GameState.SceneName + "Unlocked", false) && !sData.IsMember)
		{
			this.BuyMap("RockParkNG");
			yield break;
		}
		this.SceneLoadingText.text = "Loading trailer...";
		this.SceneLoading.SetActive(true);
		if (!PhotonNetwork.connectedAndReady || !PhotonNetwork.insideLobby || PhotonNetwork.inRoom)
		{
			int num = 0;
			if (!PhotonNetwork.connectedAndReady)
			{
				num = 1;
			}
			if (!PhotonNetwork.insideLobby)
			{
				num = 2;
			}
			if (PhotonNetwork.inRoom)
			{
				num = 3;
			}
			this.ShowMessage("Multiplayer isn't ready yet. Please try again in a moment. Make sure you have Internet access! (" + num.ToString() + ")", true);
			this.SceneLoading.SetActive(false);
			yield break;
		}
		MultiplayerManager.JoinRoom();
		for (float time = 0f; time < 10f; time += 1f)
		{
			yield return new WaitForSeconds(1f);
		}
		if (!PhotonNetwork.inRoom)
		{
			this.ShowMessage("Can't connect to the room. Try again", true);
		}
		yield break;
	}

	public void LoadScene(string sceneName)
	{
		StatsData statsData = GameState.LoadStatsData();
		if (sceneName == "MapDesertNG" && !DataStore.GetBool(sceneName + "Unlocked", false))
		{
			RequestRewardedForDesert();
			return;
		}
		if (sceneName == "StuntPark" && statsData.XP < this.UnlockStuntParkXP && !DataStore.GetBool(sceneName + "Unlocked", false) && !statsData.IsMember)
		{
			this.BuyMap("StuntPark");
			return;
		}
		if (sceneName == "RockParkNG" && statsData.XP < this.UnlockRockParkXP && !DataStore.GetBool(sceneName + "Unlocked", false) && !statsData.IsMember)
		{
			this.BuyMap("RockParkNG");
			return;
		}
		this.SceneLoadingText.text = "Loading trailer...";
		this.SceneLoading.SetActive(true);
		if (GameState.GameMode != GameMode.Multiplayer && PhotonNetwork.insideLobby)
		{
			PhotonNetwork.LeaveLobby();
		}
		SceneManager.LoadScene(sceneName);
	}

	public void RequestRewardedForDesert()
	{
		Advertisements.Instance.ShowRewardedVideo(DesertRewardedComplete);
	}

	private void DesertRewardedComplete()
	{
		//if (isCompleted)
		//{
			var desertWatchCount = PlayerPrefs.GetInt("Desert", 0);
			if (desertWatchCount == 2)
			{
				DataStore.SetBool("MapDesertNG" + "Unlocked", true);
				this.LoadMenu(MenuState.Map, false, false);
			}
			else
			{
				PlayerPrefs.SetInt("Desert", PlayerPrefs.GetInt("Desert")+1);
				desertAdText.text = PlayerPrefs.GetInt(("Desert"), 0) + "/3";
			}
		//}
		//else
		//{
		//	this.ShowMessage("You should watch the Ads to open the Map !", true);
		//}
	}

	public void SelectTrailRaceRoom(GameObject roomElement)
	{
		int num = -1;
		for (int i = 0; i < roomElement.transform.parent.childCount; i++)
		{
			if (roomElement.transform.parent.GetChild(i).gameObject == roomElement)
			{
				num = i;
				break;
			}
		}
		RoomInfo roomInfo = null;
		string name = this.TrailRaceRooms[num].Name;
		foreach (RoomInfo roomInfo2 in PhotonNetwork.GetRoomList())
		{
			if (roomInfo2.Name == name)
			{
				roomInfo = roomInfo2;
				break;
			}
		}
		if (roomInfo == null)
		{
			this.ShowMessage("Sorry, this game no longer exists!", true);
			this.CreateTrailRaceRoomsList(false);
			return;
		}
		if (roomInfo.PlayerCount > 1 || !roomInfo.IsOpen)
		{
			this.ShowMessage("Sorry, this game is full already!", true);
			this.CreateTrailRaceRoomsList(false);
			return;
		}
		if (roomInfo.CustomProperties["TrailID"] != null)
		{
			GameState.TrailID = (int)roomInfo.CustomProperties["TrailID"];
			this.HideWaiting();
			GameState.RoomName = roomInfo.Name;
			GameState.GameType = GameType.TrailRace;
			GameState.SceneName = roomInfo.CustomProperties["Scene"].ToString();
			this.StartGame(string.Empty);
			return;
		}
		this.ShowMessage("Oops! Something went wrong. Try again", true);
	}

	public void Repair()
	{
		this.RepairVehicle(global::Currency.Money);
	}

	public void EquipTrailer()
	{
		this.equipTrailerWarning.SetActive(true);
	}

	public void SelectStreetTrucks()
	{
		this.SelectedVehicleType = VehicleType.Truck;
	}

	public void SelectATVs()
	{
		this.SelectedVehicleType = VehicleType.ATV;
	}

	public void SelectSideBySides()
	{
		this.SelectedVehicleType = VehicleType.SideBySide;
	}

	public void SelectCrawlers()
	{
		this.SelectedVehicleType = VehicleType.Crawler;
	}

	public void SelectBikes()
	{
		this.SelectedVehicleType = VehicleType.Bike;
	}

	public void SelectTrailers()
	{
		this.SelectedVehicleType = VehicleType.Trailer;
	}

	public void SelectStorageVehicles()
	{
		this.SelectedVehicleType = VehicleType.Any;
	}

	public void SelectTurnKeyVehicles()
	{
		this.SelectedVehicleType = VehicleType.TurnKey;
	}

	public void NextVehicleInSelector(bool isStorage)
	{
		this.SelectedTruckIDInSelector++;
		this.UpdateVehicleSelector(isStorage);
	}

	public void PrevVehicleInSelector(bool isStorage)
	{
		this.SelectedTruckIDInSelector--;
		this.UpdateVehicleSelector(isStorage);
	}

	public void NextDrone()
	{
		this.selectedDroneInSelectorID++;
		this.UpdateDrone();
	}

	public void PrevDrone()
	{
		this.selectedDroneInSelectorID--;
		this.UpdateDrone();
	}

	public void BuyVehicleForMoney()
	{
		this.BuyVehicle(global::Currency.Money, false);
	}

	public void BuyVehicleForGold()
	{
		this.BuyVehicle(global::Currency.Gold, false);
	}

	public void BuyVehicleForCash()
	{
		this.BuyVehicleForRealCash();
	}
	
	public void BuyVehicleForAds()
	{
		Advertisements.Instance.ShowRewardedVideo(RewardedCompleteMethod);
	}

	private void RewardedCompleteMethod()
	{
		//if (isCompleted)
		//{
			var component = this.LoadedVehicleInSelector.GetComponent<VehicleDataManager>();
			if (component == null) return;
			
			SetAdData(component);
			
			if (SaveAdData.Instance.GetValue(component.gameObject.name) >= component.adCountToWatch)
			{
				this.BuyVehicle(global::Currency.Ads);
				
				SaveAdData.Instance.ResetKey(component.gameObject.name);
			}
		//}
		//else
		//{
		//	print("rewarded is " + isCompleted);
		//}
	}

	public void Wash()
	{
		this.WashVehicle(global::Currency.Money);
	}

	public void Randomize()
	{
		this.RandomizeVehicle();
	}

	public void Stock()
	{
		this.SetStock();
	}

	public void BuyAllModsForMoney()
	{
		this.BuyAllMods(global::Currency.Money);
	}

	public void BuyAllModsForGold()
	{
		this.BuyAllMods(global::Currency.Gold);
	}

	public void Customize_BodyParts(bool FromMainMenu)
	{
		if (this.CurrentPartsSwitcher.partGroups.Length > 0)
		{
			this.LoadMenu(MenuState.CustomizeBodyParts, false, FromMainMenu);
		}
		else
		{
			this.ShowMessage("No mods available", true);
		}
	}

	public void Customize_Paint(bool FromMainMenu)
	{
		if (this.CurrentPartsSwitcher.noPaintAllowed)
		{
			this.ShowMessage("You can't paint this!", true);
		}
		else
		{
			this.LoadMenu(MenuState.CustomizePaint, false, FromMainMenu);
		}
	}

	public void Customize_Rims(bool FromMainMenu)
	{
		if (this.CurrentSuspensionController.FrontWheelsControls.TankTracks)
		{
			this.ShowMessage("Uninstall the tracks first!", true);
		}
		else if (this.CurrentSuspensionController.CurrentFrontSuspension.DontLoadWheels || this.CurrentSuspensionController.CurrentRearSuspension.DontLoadWheels)
		{
			this.ShowMessage("Can't install rims!", true);
		}
		else
		{
			this.LoadMenu(MenuState.CustomizeRims, false, FromMainMenu);
		}
	}

	public void Customize_Tires(bool FromMainMenu)
	{
		if (this.CurrentSuspensionController.FrontWheelsControls.TankTracks)
		{
			this.ShowMessage("Uninstall the tracks first!", true);
		}
		else if (this.CurrentSuspensionController.CurrentFrontSuspension.DontLoadWheels || this.CurrentSuspensionController.CurrentRearSuspension.DontLoadWheels)
		{
			this.ShowMessage("Can't install tires!", true);
		}
		else
		{
			this.LoadMenu(MenuState.CustomizeTires, false, FromMainMenu);
		}
	}

	public void Customize_Wraps(bool FromMainMenu)
	{
		if (this.CurrentPartsSwitcher.noPaintAllowed)
		{
			this.ShowMessage("You can't put wraps on this!", true);
		}
		else
		{
			this.LoadMenu(MenuState.CustomizeWraps, false, FromMainMenu);
		}
	}

	public void NextBodyPart()
	{
		this.ChangePart(this.SelectedPartID + 1);
	}

	public void PrevBodyPart()
	{
		this.ChangePart(this.SelectedPartID - 1);
	}

	public void NextBodyPartGroup()
	{
		this.ChangePartGroup(this.SelectedPartGroupID + 1);
	}

	public void PrevBodyPartGroup()
	{
		this.ChangePartGroup(this.SelectedPartGroupID - 1);
	}

	public void NextRim()
	{
		this.ChangeRim(this.SelectedRimID + 1);
	}

	public void PrevRim()
	{
		this.ChangeRim(this.SelectedRimID - 1);
	}

	public void NextRimSide()
	{
		this.ChangeRimSide((this.currentSide != Side.Front) ? Side.Front : Side.Rear);
	}

	public void PrevRimSide()
	{
		this.ChangeRimSide((this.currentSide != Side.Front) ? Side.Front : Side.Rear);
	}

	public void NextTire()
	{
		this.ChangeTire(this.SelectedTireID + 1);
	}

	public void PrevTire()
	{
		this.ChangeTire(this.SelectedTireID - 1);
	}

	public void NextTireSide()
	{
		this.ChangeTireSide((this.currentSide != Side.Front) ? Side.Front : Side.Rear);
	}

	public void PrevTireSide()
	{
		this.ChangeTireSide((this.currentSide != Side.Front) ? Side.Front : Side.Rear);
	}

	public void ChooseColor(Image image)
	{
		this.SetVehicleColorInCustomization(image.color);
		this.ToggleBodyColorPicker(false);
	}

	public void ChooseCustomBodyColor()
	{
		this.SetVehicleColorInCustomization(this.BodyColorPicker.Color);
	}

	public void ChooseCustomWheelColor()
	{
		if (this.rimColorMode == RimColorMode.Spoke)
		{
			this.SetRimColor(this.RimColorPicker.Color);
		}
		else
		{
			this.SetBeadlockColor(this.RimColorPicker.Color);
		}
	}

	public void ChooseCustomWrapColor()
	{
		this.WrapColor = this.WrapColorPicker.Color;
		this.UpdateWrap();
	}

	public void ToggleBodyColorPicker(bool Show)
	{
		this.BodyColorPicker.gameObject.SetActive(Show);
		this.BodyColorPicker.Color = this.CurrentPartsSwitcher.BodyColor;
		this.BodyColorPicker.ColorChangedCallback = new Action(this.ChooseCustomBodyColor);
	}

	public void ToggleWheelColorPicker(bool Show)
	{
		this.RimColorPicker.gameObject.SetActive(Show);
		if (Show)
		{
			this.RimColorPicker.Color = Color.white;
		}
		this.RimColorPicker.ColorChangedCallback = new Action(this.ChooseCustomWheelColor);
	}

	public void ChooseWheelColor(Image image)
	{
		if (this.rimColorMode == RimColorMode.Spoke)
		{
			this.ChooseRimColor(image);
		}
		else
		{
			this.ChooseBedlockColor(image);
		}
	}

	public void ChooseRimColor(Image image)
	{
		this.SetRimColor(image.color);
		this.ToggleWheelColorPicker(false);
	}

	public void ChooseBedlockColor(Image image)
	{
		this.SetBeadlockColor(image.color);
		this.ToggleWheelColorPicker(false);
	}

	public void ChooseBodyPartColor(Image image)
	{
		this.SetBodyPartColor(image.color);
	}

	public void SetMattePaint()
	{
		this.ChangeGlossiness(false);
	}

	public void SetGlossy()
	{
		this.ChangeGlossiness(true);
	}

	public void BuyGlossy()
	{
		this.BuyGlossyPaint(global::Currency.Ads);
	}

	public void ToggleBodyColorBar()
	{
		this.BodyPartColorBar.SetActive(!this.BodyPartColorBar.activeSelf);
	}

	public void ToggleRimsColorBar()
	{
		this.WheelsColorBar.SetActive(true);
		this.rimColorMode = RimColorMode.Spoke;
		this.ToggleWheelColorPicker(false);
	}

	public void ToggleBeadlockColorBar()
	{
		this.WheelsColorBar.SetActive(true);
		this.rimColorMode = RimColorMode.Bead;
		this.ToggleWheelColorPicker(false);
	}

	public void NextWrap()
	{
		this.LoadWrap(this.SelectedWrap + 1);
	}

	public void PrevWrap()
	{
		this.LoadWrap(this.SelectedWrap - 1);
	}

	public void OffsetChanged()
	{
		this.WrapOffsetChanged();
	}

	public void Power_EnginePower()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.EnginePower);
	}

	public void Power_EngineBlock()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.EngineBlock);
	}

	public void Power_Head()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Head);
	}

	public void Power_Valvetrain()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Valvetrain);
	}

	public void Power_Grip()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Grip);
	}

	public void Power_Weight()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Weight);
	}

	public void Power_Durability()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Durability);
	}

	public void Power_Diesel()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Diesel);
	}

	public void Power_Gearbox()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Gearbox);
	}

	public void Power_Ebrake()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Ebrake);
	}

	public void Power_TankTracks()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.TankTracks);
	}

	public void Power_Turbo()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Turbo);
	}

	public void Power_Blower()
	{
		this.LoadPowerSubtypeScreen(PowerPartType.Blower);
	}

	public void Power_Uninstall()
	{
		this.UninstallPowerPart();
	}

	public void UpgradePower()
	{
		this.UpgradePowerPart(global::Currency.Money);
	}

	public void UpgradePowerGold()
	{
		this.UpgradePowerPart(global::Currency.Gold);
	}
	public void UpgradePowerAds()
	{
		this.UpgradePowerPart(global::Currency.Ads);
	}

	public void Drivetrain_SwitchSuspension()
	{
		this.LoadMenu(MenuState.SwitchSuspension, true, false);
	}

	public void Drivetrain_TuneSuspension()
	{
		this.LoadMenu(MenuState.TuneSuspension, true, false);
	}

	public void Drivetrain_TuneWheels()
	{
		if (this.CurrentSuspensionController.FrontWheelsControls.TankTracks)
		{
			this.ShowMessage("Uninstall the tracks first!", true);
		}
		else
		{
			this.LoadMenu(MenuState.TuneWheels, true, false);
		}
	}

	public void Drivetrain_TestSuspension()
	{
		this.LoadMenu(MenuState.TestSuspension, true, false);
	}

	public void Drivetrain_TuneGearing()
	{
		if (this.CurrentCarController.GetComponent<TankController>() != null)
		{
			this.ShowMessage("Can't tune gears on this!", true);
			return;
		}
		this.LoadMenu(MenuState.TuneGearing, true, false);
	}

	public void Drivetrain_DynoTest()
	{
		if (this.CurrentCarController.GetComponent<TankController>() != null)
		{
			this.ShowMessage("Can't dyno test this!", true);
			return;
		}
		this.LoadMenu(MenuState.Dyno, true, false);
	}

	public void Drivetrain_BuyDynoRuns()
	{
		this.LoadMenu(MenuState.BuyingDynoRuns, false, false);
	}

	public void Drivetrain_DynoResult()
	{
		this.LoadMenu(MenuState.DynoResult, true, false);
	}

	public void NextSuspension()
	{
		this.ChangeSuspension(this.SelectedSuspensionID + 1);
	}

	public void PrevSuspension()
	{
		this.ChangeSuspension(this.SelectedSuspensionID - 1);
	}

	public void NextSuspensionSide()
	{
		this.ChangeSuspensionSide((this.currentSide != Side.Front) ? Side.Front : Side.Rear);
	}

	public void PrevSuspensionSide()
	{
		this.ChangeSuspensionSide((this.currentSide != Side.Front) ? Side.Front : Side.Rear);
	}

	public void SetFrontSuspensionAdjustmentSide()
	{
		this.ChangeSuspensionAdjustmentsSide(Side.Front);
	}

	public void SetRearSuspensionAdjustmentSide()
	{
		this.ChangeSuspensionAdjustmentsSide(Side.Rear);
	}

	public void SetFrontWheelsSide()
	{
		this.ChangeWheelsSide(Side.Front);
	}

	public void SetRearWheelsSide()
	{
		this.ChangeWheelsSide(Side.Rear);
	}

	public void OnSuspensionAdjustmentChanged()
	{
		this.SuspensionAdjustmentChanged();
	}

	public void OnGearValueChanged()
	{
		this.GearValueChanged();
	}

	public void InstallSuspension()
	{
		this.InstallChosenSuspension(global::Currency.Money);
	}
	public void InstallSuspensionAds()
	{
		this.InstallChosenSuspension(global::Currency.Ads);
	}

	public void InstallSuspensionGold()
	{
		this.InstallChosenSuspension(global::Currency.Gold);
	}

	public void UpgradeSuspension()
	{
		this.UpgradeSelectedSuspension(global::Currency.Money);
	}
	public void UpgradeSuspensionAds()
	{
		this.UpgradeSelectedSuspension(global::Currency.Ads);
	}

	public void UpgradeSuspensionGold()
	{
		this.UpgradeSelectedSuspension(global::Currency.Gold);
	}

	public void UpgradeGearing()
	{
		this.UpgradeGearingStage();
	}

	public void UpgradeWheels()
	{
		this.UpgradeSelectedWheels(global::Currency.Money);
	}
	public void UpgradeWheelsAds()
	{
		this.UpgradeSelectedWheels(global::Currency.Ads);
	}

	public void UpgradeWheelsGold()
	{
		this.UpgradeSelectedWheels(global::Currency.Gold);
	}

	public void SaveVehicle()
	{
		this.CurrentVehicle.SaveVehicleData();
	}

	public void Wheels_SelectRimSizeAdjustment()
	{
		this.SelectRimSizeAdjustment();
	}

	public void Wheels_SelectWheelsRadiusAdjustment()
	{
		this.SelectWheelsRadiusAdjustment();
	}

	public void Wheels_SelelectWheelsWidthAdjustment()
	{
		this.SelectWheelsWidthAdjustment();
	}

	public void OnWheelsAdjustmentChanged()
	{
		this.WheelsAdjustmentChanged();
	}

	public void SetDefaulGears()
	{
		this.CurrentCarController.GearRatios = GearsManager.DefaultGears;
		this.CurrentCarController.LowGearRatio = GearsManager.DefaultLowGear;
		this.UpdateGearingSlider();
	}

	public void SetRaceBet(int bet)
	{
		StatsData statsData = GameState.LoadStatsData();
		if (statsData.Money < bet)
		{
			this.ShowMessage("Not enough money!", true);
			return;
		}
		GameState.TrailRaceBet = bet;
		this.LoadMenu(MenuState.TrailSelectorScreen, false, false);
	}

	public void OpenSettingsTab()
	{
		this.OpenSettings();
	}

	private void OpenSettings()
	{
		this.SettingsTab.SetActive(true);
		this.DefaultTab.SetActive(false);
	}

	public void SwitchSideBar()
	{
		this.SideBarExpanded = !this.SideBarExpanded;
		this.TimeStamp.text = DateTime.Now.ToString();
	}

	public void BackToGarageFromCustomize()
	{
		if (this.PurchaseModsButton.activeInHierarchy)
		{
			this.ShowModConfirmation();
		}
		else
		{
			this.LoadMainMenu(false);
		}
	}

	private void ShowModConfirmation()
	{
		this.ModConfirmation.SetActive(true);
	}

	private void WashVehicle(global::Currency currency)
	{
		int num = 300;
		if (currency == global::Currency.Gold)
		{
			num = Utility.CashToGold(num);
		}
		if (this.ProcessPurchase(currency, num))
		{
			this.CurrentPartsSwitcher.WashVehicle();
			this.WashButton.SetActive(false);
		}
		else if (currency == global::Currency.Money && Utility.CashToGold(300) <= GameState.LoadStatsData().Gold)
		{
			this.WashVehicle(global::Currency.Gold);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	public void DoneWashing()
	{
		this.UpdateSideButtons();
	}

	private void RandomizeVehicle()
	{
		this.CurrentSuspensionController.SetRandomWheels();
		this.CurrentPartsSwitcher.SetRandomModification();
		this.PurchaseModsButton.SetActive(true);
		this.UpdatePurchaseModsButton();
	}

	private void SetStock()
	{
		this.CurrentSuspensionController.SetStockWheels();
		this.CurrentPartsSwitcher.SetStockModification();
		this.PurchaseModsButton.SetActive(true);
		this.UpdatePurchaseModsButton();
	}

	private void SetVehicleColorInCustomization(Color color)
	{
		this.CurrentPartsSwitcher.BodyColor = color;
		this.CurrentPartsSwitcher.UpdateColor(false);
	}

	private void BuyAllMods(global::Currency currency)
	{
		int num = this.TotalModsCost();
		if (currency == global::Currency.Gold)
		{
			num = Utility.CashToGold(num);
		}
		if (this.ProcessPurchase(currency, num))
		{
			foreach (PartGroup partGroup in this.CurrentPartsSwitcher.partGroups)
			{
				if (partGroup.Parts[partGroup.InstalledPart] != null && !this.CurrentVehicle.PurchasedPartsList.Contains(partGroup.Parts[partGroup.InstalledPart].name))
				{
					this.CurrentVehicle.PurchasedPartsList.Add(partGroup.Parts[partGroup.InstalledPart].name);
				}
			}
			int intValue = this.CurrentSuspensionController.FrontWheelsControls.Rim.IntValue;
			int intValue2 = this.CurrentSuspensionController.RearWheelsControls.Rim.IntValue;
			int intValue3 = this.CurrentSuspensionController.FrontWheelsControls.Tire.IntValue;
			int intValue4 = this.CurrentSuspensionController.RearWheelsControls.Tire.IntValue;
			if (!this.CurrentVehicle.PurchasedPartsList.Contains("Rim" + intValue.ToString()))
			{
				this.CurrentVehicle.PurchasedPartsList.Add("Rim" + intValue.ToString());
			}
			if (!this.CurrentVehicle.PurchasedPartsList.Contains("Rim" + intValue2.ToString()))
			{
				this.CurrentVehicle.PurchasedPartsList.Add("Rim" + intValue2.ToString());
			}
			if (!this.CurrentVehicle.PurchasedPartsList.Contains("Tire" + intValue3.ToString()))
			{
				this.CurrentVehicle.PurchasedPartsList.Add("Tire" + intValue3.ToString());
			}
			if (!this.CurrentVehicle.PurchasedPartsList.Contains("Tire" + intValue4.ToString()))
			{
				this.CurrentVehicle.PurchasedPartsList.Add("Tire" + intValue4.ToString());
			}
			this.CurrentVehicle.SaveVehicleData();
			this.LoadMenu(MenuState.MainMenu, true, false);
		}
		else if (currency == global::Currency.Money && Utility.CashToGold(num) <= GameState.LoadStatsData().Gold)
		{
			this.BuyAllMods(global::Currency.Gold);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	private void ChangePartGroup(int newGroupID)
	{
		this.SelectedPartGroupID = newGroupID;
		if (this.SelectedPartGroupID > this.CurrentPartsSwitcher.partGroups.Length - 1)
		{
			this.SelectedPartGroupID = 0;
		}
		if (this.SelectedPartGroupID < 0)
		{
			this.SelectedPartGroupID = this.CurrentPartsSwitcher.partGroups.Length - 1;
		}
		this.SelectedPartGroup = this.CurrentPartsSwitcher.partGroups[this.SelectedPartGroupID];
		this.SelectedPartID = this.SelectedPartGroup.InstalledPart;
		if (this.SelectedPartGroup.partType == PartType.Headlights && !this.LightsOn)
		{
			this.ToggleLights();
		}
		if (this.SelectedPartGroup.partType != PartType.Headlights && this.LightsOn)
		{
			this.ToggleLights();
		}
		this.BodyPartColorMenu.SetActive(this.SelectedPartGroup.Paintable);
		foreach (CameraPosition cameraPosition in this.cameraPositions)
		{
			if (cameraPosition.bodyPartType == this.CurrentPartsSwitcher.partGroups[this.SelectedPartGroupID].partType)
			{
				CameraController.Instance.SetCameraPos(cameraPosition.XAngle, cameraPosition.YAngle, cameraPosition.Distance);
			}
		}
		this.GroupNameText.text = this.SelectedPartGroup.GroupName;
		this.ChangePart(this.SelectedPartID);
	}

	private void ChangePart(int newPartID)
	{
		this.SelectedPartID = newPartID;
		if (this.SelectedPartID > this.SelectedPartGroup.Parts.Length - 1)
		{
			this.SelectedPartID = 0;
		}
		if (this.SelectedPartID < 0)
		{
			this.SelectedPartID = this.SelectedPartGroup.Parts.Length - 1;
		}
		if (this.SelectedPartGroup.Parts[this.SelectedPartID] == null)
		{
			this.PartCostText.text = "Owned";
			this.CurrentPartsSwitcher.InstallBodyPart(this.SelectedPartGroup, this.SelectedPartID);
			return;
		}
		string name = this.SelectedPartGroup.Parts[this.SelectedPartID].name;
		PartType partType = this.SelectedPartGroup.partType;
		BodyPart part = VehicleParts.GetPart(this.CurrentVehicle.vehicleType, partType, name);
		bool flag = this.CurrentVehicle.PurchasedPartsList.Contains(name);
		this.PartCostText.text = ((!flag && part.partCost != 0) ? ("$" + part.partCost.ToString()) : "Owned");
		this.CurrentPartsSwitcher.InstallBodyPart(this.SelectedPartGroup, this.SelectedPartID);
		this.CurrentPartsSwitcher.UpdateDirtiness();
		this.CurrentPartsSwitcher.UpdateColor(false);
	}

	private int TotalModsCost()
	{
		int num = 0;
		foreach (PartGroup partGroup in this.partGroupsBeforeEnteringCustomization)
		{
			foreach (PartGroup partGroup2 in this.CurrentPartsSwitcher.partGroups)
			{
				if (partGroup.GroupName == partGroup2.GroupName && partGroup.InstalledPart != partGroup2.InstalledPart && partGroup2.Parts[partGroup2.InstalledPart] != null && !this.CurrentVehicle.PurchasedPartsList.Contains(partGroup2.Parts[partGroup2.InstalledPart].name))
				{
					BodyPart part = VehicleParts.GetPart(this.CurrentVehicle.vehicleType, partGroup2.partType, partGroup2.Parts[partGroup2.InstalledPart].name);
					if (part != null)
					{
						num += part.partCost;
					}
				}
			}
		}
		foreach (PartGroup partGroup3 in this.partGroupsBeforeEnteringCustomization)
		{
			foreach (PartGroup partGroup4 in this.CurrentPartsSwitcher.partGroups)
			{
				if (partGroup4.GroupName == partGroup3.GroupName && partGroup4.color != partGroup3.color)
				{
					BodyPart part2 = VehicleParts.GetPart(VehicleType.Any, PartType.Other, "BodyPartPaint");
					if (part2 != null)
					{
						num += part2.partCost;
					}
				}
			}
		}
		foreach (SuspensionValue suspensionValue in this.CurrentSuspensionController.FrontWheelsControls.GetAllValues())
		{
			foreach (SuspensionValue suspensionValue2 in this.FrontWheelsBeforeEnteringCustomization.GetAllValues())
			{
				if (suspensionValue.ValueName == suspensionValue2.ValueName && suspensionValue.valueType == global::ValueType.Int && !this.CurrentVehicle.PurchasedPartsList.Contains(suspensionValue.ValueName + suspensionValue.IntValue))
				{
					string partName = suspensionValue.ValueName + suspensionValue.IntValue.ToString();
					BodyPart part3 = VehicleParts.GetPart(this.CurrentVehicle.vehicleType, PartType.Wheel, partName);
					if (part3 != null)
					{
						num += part3.partCost;
					}
				}
			}
		}
		if (this.FrontRimsColorBeforeEngeringCustomizaiton != this.CurrentPartsSwitcher.FRimsColor)
		{
			BodyPart part4 = VehicleParts.GetPart(VehicleType.Any, PartType.Other, "RimPaint");
			if (part4 != null)
			{
				num += part4.partCost;
			}
		}
		if (this.FrontBeadlockColorBeforeEnteringCustomization != this.CurrentPartsSwitcher.FBeadlocksColor)
		{
			BodyPart part5 = VehicleParts.GetPart(VehicleType.Any, PartType.Other, "BeadlockPaint");
			if (part5 != null)
			{
				num += part5.partCost;
			}
		}
		foreach (SuspensionValue suspensionValue3 in this.CurrentSuspensionController.RearWheelsControls.GetAllValues())
		{
			foreach (SuspensionValue suspensionValue4 in this.RearWheelsBeforeEnteringCustomization.GetAllValues())
			{
				if (suspensionValue3.ValueName == suspensionValue4.ValueName && suspensionValue3.valueType == global::ValueType.Int && !this.CurrentVehicle.PurchasedPartsList.Contains(suspensionValue3.ValueName + suspensionValue3.IntValue))
				{
					foreach (SuspensionValue suspensionValue5 in this.CurrentSuspensionController.FrontWheelsControls.GetAllValues())
					{
						if (suspensionValue3.ValueName == suspensionValue5.ValueName && suspensionValue3.IntValue != suspensionValue5.IntValue)
						{
							string partName2 = suspensionValue3.ValueName + suspensionValue3.IntValue.ToString();
							BodyPart part6 = VehicleParts.GetPart(this.CurrentVehicle.vehicleType, PartType.Wheel, partName2);
							if (part6 != null)
							{
								num += part6.partCost;
							}
						}
					}
				}
			}
		}
		if (this.RearRimsColorBeforeEngeringCustomizaiton != this.CurrentPartsSwitcher.RRimsColor)
		{
			BodyPart part7 = VehicleParts.GetPart(VehicleType.Any, PartType.Other, "RimPaint");
			if (part7 != null)
			{
				num += part7.partCost;
			}
		}
		if (this.RearBeadlockColorBeforeEnteringCustomization != this.CurrentPartsSwitcher.RBeadlocksColor)
		{
			BodyPart part8 = VehicleParts.GetPart(VehicleType.Any, PartType.Other, "BeadlockPaint");
			if (part8 != null)
			{
				num += part8.partCost;
			}
		}
		return num;
	}

	private void UpdatePurchaseModsButton()
	{
		int num = this.TotalModsCost();
		if (num == 0)
		{
			this.TotalModsCostText.text = "FREE";
		}
		else
		{
			this.TotalModsCostText.text = "$" + num.ToString();
		}
		if (num == 0)
		{
			this.PurchaseModsButtonGold.SetActive(false);
			this.TotalModsCostGoldText.text = "0";
		}
		else
		{
			this.PurchaseModsButtonGold.SetActive(true);
			this.TotalModsCostGoldText.text = Utility.CashToGold(num).ToString();
		}
	}

	private void ChangeRimSide(Side side)
	{
		this.SelectedWheelsControls = ((side != Side.Front) ? this.CurrentSuspensionController.RearWheelsControls : this.CurrentSuspensionController.FrontWheelsControls);
		this.SelectedRimID = this.SelectedWheelsControls.Rim.IntValue;
		if (side == Side.Front)
		{
			this.SetCameraTargetWithoutOffset(this.CurrentSuspensionController.CurrentFrontSuspension.transform.position, false);
		}
		else
		{
			this.SetCameraTargetWithoutOffset(this.CurrentSuspensionController.CurrentRearSuspension.transform.position, false);
		}
		CameraController.Instance.SetCameraPos(90f, 0f, 3f);
		this.WheelsColorBar.SetActive(false);
		this.ToggleWheelColorPicker(false);
		this.currentSide = side;
		this.RimSideText.text = side.ToString();
		this.ChangeRim(this.SelectedRimID);
	}

	private void ChangeRim(int RimID)
	{
		SuspensionControlLimit limit = SuspensionControlLimits.getLimit((this.currentSide != Side.Front) ? this.CurrentSuspensionController.CurrentRearSuspension.gameObject.name : this.CurrentSuspensionController.CurrentFrontSuspension.gameObject.name, "Rim");
		if (RimID > limit.iMax)
		{
			RimID = 0;
		}
		if (RimID < 0)
		{
			RimID = limit.iMax;
		}
		this.SelectedRimID = RimID;
		string text = "Rim" + RimID.ToString();
		BodyPart part = VehicleParts.GetPart(this.CurrentVehicle.vehicleType, PartType.Wheel, text);
		bool flag = this.CurrentVehicle.PurchasedPartsList.Contains(text);
		this.RimCostText.text = ((!flag && part.partCost != 0) ? ("$" + part.partCost.ToString()) : "Owned");
		this.SelectedWheelsControls.Rim.IntValue = RimID;
		this.CurrentSuspensionController.LoadWheels();
		this.CurrentPartsSwitcher.GenerateRimsTexture();
	}

	private void SetRimColor(Color color)
	{
		if (this.currentSide == Side.Front)
		{
			this.CurrentPartsSwitcher.FRimsColor = color;
		}
		else
		{
			this.CurrentPartsSwitcher.RRimsColor = color;
		}
		this.CurrentPartsSwitcher.GenerateRimsTexture();
	}

	private void SetBeadlockColor(Color color)
	{
		if (this.currentSide == Side.Front)
		{
			this.CurrentPartsSwitcher.FBeadlocksColor = color;
		}
		else
		{
			this.CurrentPartsSwitcher.RBeadlocksColor = color;
		}
		this.CurrentPartsSwitcher.GenerateRimsTexture();
	}

	private void SetBodyPartColor(Color color)
	{
		this.SelectedPartGroup.color = color;
		this.SelectedPartGroup.PaintPart();
	}

	private void UpdatePaintTypeButtons()
	{
		this.BuyGlossyPaintButton.SetActive(!this.CurrentPartsSwitcher.GlossyPaintPurchased);
		this.SetGlossyPaintButton.SetActive(this.CurrentPartsSwitcher.GlossyPaintPurchased);
		this.CurrentPaintTypeText.text = ((!this.CurrentPartsSwitcher.GlossyPaint) ? "Matte" : "Glossy");
	}

	private void BuyGlossyPaint(global::Currency currency)
	{
		int num = 10000;
		if (currency == global::Currency.Gold)
		{
			num = Utility.CashToGold(num);
		}
		if (this.ProcessPurchase(currency, num))
		{
			this.ChangeGlossiness(true);
			this.CurrentPartsSwitcher.GlossyPaintPurchased = true;
			this.CurrentVehicle.SaveOnlyGlossinessData();
			this.UpdatePaintTypeButtons();
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	private void ChangeGlossiness(bool Glossy)
	{
		this.CurrentPartsSwitcher.GlossyPaint = Glossy;
		this.CurrentPartsSwitcher.UpdateColor(false);
		this.CurrentVehicle.SaveOnlyGlossinessData();
		this.UpdatePaintTypeButtons();
	}

	private void ChangeTireSide(Side side)
	{
		this.SelectedWheelsControls = ((side != Side.Front) ? this.CurrentSuspensionController.RearWheelsControls : this.CurrentSuspensionController.FrontWheelsControls);
		this.UpdateTiresTypeMenu(side);
		this.SelectedTireID = this.SelectedWheelsControls.Tire.IntValue;
		if (side == Side.Front)
		{
			this.SetCameraTargetWithoutOffset(this.CurrentSuspensionController.CurrentFrontSuspension.transform.position, false);
		}
		else
		{
			this.SetCameraTargetWithoutOffset(this.CurrentSuspensionController.CurrentRearSuspension.transform.position, false);
		}
		CameraController.Instance.SetCameraPos(90f, 0f, 3f);
		this.currentSide = side;
		this.TireSideText.text = side.ToString();
		this.ChangeTire(this.SelectedTireID);
	}

	private void UpdateTiresTypeMenu(Side side)
	{
		this.dualTiresMenu.SetActive(this.CurrentVehicle.vehicleType == VehicleType.Truck);
		this.buyDualTiresButton.SetActive((side == Side.Front && !this.CurrentCarController.frontDuallyPurchased) || (side == Side.Rear && !this.CurrentCarController.rearDuallyPurchased));
		this.dualTiresButton.SetActive(!this.buyDualTiresButton.activeSelf);
		this.currentTiresTypeText.text = ((!this.SelectedWheelsControls.duallyTires) ? "Non-dually" : "Dually");
	}

	public void BuyDuallies()
	{
		if (this.ProcessPurchase(global::Currency.Ads, 50))
		{
			if (this.currentSide == Side.Front)
			{
				this.CurrentCarController.frontDuallyPurchased = true;
			}
			else
			{
				this.CurrentCarController.rearDuallyPurchased = true;
			}
			this.CurrentVehicle.SaveDualliesData();
			this.ChangeTiresType(true);
			this.UpdateTiresTypeMenu(this.currentSide);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	public void ChangeTiresType(bool duallies)
	{
		this.SelectedWheelsControls.duallyTires = duallies;
		this.ChangeTire(this.SelectedWheelsControls.Tire.IntValue);
		this.UpdateTiresTypeMenu(this.currentSide);
	}

	private void ChangeTire(int TireID)
	{
		SuspensionControlLimit limit = SuspensionControlLimits.getLimit((this.currentSide != Side.Front) ? this.CurrentSuspensionController.CurrentRearSuspension.gameObject.name : this.CurrentSuspensionController.CurrentFrontSuspension.gameObject.name, "Tire");
		if (TireID > limit.iMax)
		{
			TireID = 0;
		}
		if (TireID < 0)
		{
			TireID = limit.iMax;
		}
		this.SelectedTireID = TireID;
		string text = "Tire" + TireID.ToString();
		BodyPart part = VehicleParts.GetPart(this.CurrentVehicle.vehicleType, PartType.Wheel, text);
		bool flag = this.CurrentVehicle.PurchasedPartsList.Contains(text);
		this.TireCostText.text = ((!flag && part.partCost != 0) ? ("$" + part.partCost.ToString()) : "Owned");
		this.SelectedWheelsControls.Tire.IntValue = TireID;
		this.CurrentSuspensionController.LoadWheels();
		this.CurrentPartsSwitcher.GenerateRimsTexture();
	}

	private void ChangeSuspensionSide(Side side)
	{
		this.SelectedSuspension = ((side != Side.Front) ? this.CurrentSuspensionController.CurrentRearSuspension : this.CurrentSuspensionController.CurrentFrontSuspension);
		this.SelectedSuspensionID = ((side != Side.Front) ? this.CurrentSuspensionController.rearSuspension : this.CurrentSuspensionController.frontSuspension);
		this.currentSide = side;
		this.SuspensionSideText.text = side.ToString();
		this.ChangeSuspension(this.SelectedSuspensionID);
		CameraController.Instance.SetCameraPos((float)((side != Side.Front) ? 180 : 0), -20f, 4f);
		this.CurrentPartsSwitcher.GenerateRimsTexture();
	}

	private void ChangeSuspension(int SuspensionID)
	{
		List<Suspension> list = (this.currentSide != Side.Front) ? this.CurrentSuspensionController.RearSuspensions : this.CurrentSuspensionController.FrontSuspensions;
		if (SuspensionID > list.Count - 1)
		{
			SuspensionID = 0;
		}
		if (SuspensionID < 0)
		{
			SuspensionID = list.Count - 1;
		}
		bool flag = SuspensionID - this.SelectedSuspensionID == 1 || SuspensionID == 0 || this.SelectedSuspensionID > 2;
		this.SelectedSuspensionID = SuspensionID;
		if (Debug.isDebugBuild)
		{
		}
		bool flag2 = false;
		if (this.currentSide == Side.Front)
		{
			if (!flag2 && this.CurrentSuspensionController.FrontSuspensions[this.SelectedSuspensionID].GetType() == typeof(MonsterSuspension))
			{
				if (flag)
				{
					SuspensionID = 0;
					this.SelectedSuspensionID = 0;
				}
				else
				{
					SuspensionID = this.CurrentSuspensionController.FrontSuspensions.Count - 2;
					this.SelectedSuspensionID = this.CurrentSuspensionController.FrontSuspensions.Count - 2;
				}
			}
			this.CurrentSuspensionController.SetFrontSuspension(SuspensionID);
			this.SelectedSuspension = this.CurrentSuspensionController.CurrentFrontSuspension;
		}
		else
		{
			if (!flag2 && this.CurrentSuspensionController.RearSuspensions[this.SelectedSuspensionID].GetType() == typeof(MonsterSuspension))
			{
				if (flag)
				{
					SuspensionID = 0;
					this.SelectedSuspensionID = 0;
				}
				else
				{
					SuspensionID = this.CurrentSuspensionController.RearSuspensions.Count - 2;
					this.SelectedSuspensionID = this.CurrentSuspensionController.RearSuspensions.Count - 2;
				}
			}
			this.CurrentSuspensionController.SetRearSuspension(SuspensionID);
			this.SelectedSuspension = this.CurrentSuspensionController.CurrentRearSuspension;
		}
		SuspensionPart suspension = Suspensions.GetSuspension(this.CurrentVehicle.vehicleType, this.SelectedSuspension.gameObject.name);
		this.SuspensionCostText.text = ((!this.CurrentVehicle.PurchasedPartsList.Contains(this.SelectedSuspension.gameObject.name) && suspension.partCost != 0) ? ("$" + suspension.partCost) : "Owned");
		this.SuspensionCostGoldText.text = ((!this.CurrentVehicle.PurchasedPartsList.Contains(this.SelectedSuspension.gameObject.name) && suspension.partCost != 0) ? Utility.CashToGold(suspension.partCost).ToString() : "--");
		this.SuspensionDescriptionText.text = suspension.partDescription;
		this.SuspensionNameText.text = suspension.displayedName;
		this.CurrentPartsSwitcher.GenerateRimsTexture();
	}

	private void InstallChosenSuspension(global::Currency currency)
	{
		SuspensionPart suspension = Suspensions.GetSuspension(this.CurrentVehicle.vehicleType, this.SelectedSuspension.gameObject.name);
		int num = (!this.CurrentVehicle.PurchasedPartsList.Contains(this.SelectedSuspension.gameObject.name)) ? suspension.partCost : 0;
		if (currency == global::Currency.Gold)
		{
			num = Utility.CashToGold(num);
		}
		if (this.ProcessPurchase(currency, num))
		{
			if (!this.CurrentVehicle.PurchasedPartsList.Contains(this.SelectedSuspension.gameObject.name))
			{
				this.CurrentVehicle.PurchasedPartsList.Add(this.SelectedSuspension.gameObject.name);
			}
			this.CurrentVehicle.SaveVehicleData();
			this.ShowMessage(suspension.displayedName + " installed", true);
			this.ChangeSuspension(this.SelectedSuspensionID);
		}
		else if (currency == global::Currency.Money && (float)num / 100f <= (float)GameState.LoadStatsData().Gold)
		{
			this.InstallChosenSuspension(global::Currency.Gold);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	private void ChangeSuspensionAdjustmentsSide(Side side)
	{
		CameraController.Instance.SetCameraPos((float)((side != Side.Front) ? 180 : 0), -20f, 4f);
		this.currentSide = side;
		this.SelectedSuspension = ((side != Side.Front) ? this.CurrentSuspensionController.CurrentRearSuspension : this.CurrentSuspensionController.CurrentFrontSuspension);
		SuspensionPart suspension = Suspensions.GetSuspension(this.CurrentVehicle.vehicleType, this.SelectedSuspension.gameObject.name);
		this.SuspensionNameInUpgradeBarText.text = suspension.displayedName;
		this.UpdateSuspensionUpgradeButton();
		this.BuildSuspensionAdjustmentsList();
		this.SuspensionAdjustmentSlider.gameObject.SetActive(false);
	}

	private void BuildSuspensionAdjustmentsList()
	{
		this.ClearAdjustments();
		SuspensionValue[] controlValues = this.SelectedSuspension.GetControlValues();
		this.LoadedAdjustmentButtons.Add(this.FirstAdjustmentButton);
		this.FirstAdjustmentButton.gameObject.SetActive(true);
		bool flag = false;
		for (int i = 0; i < controlValues.Length; i++)
		{
			SuspensionControlLimit limit = SuspensionControlLimits.getLimit(this.SelectedSuspension.gameObject.name, controlValues[i].ValueName);
			if (limit != null && limit.ModifiableByPlayer)
			{
				if (!flag)
				{
					int firstID = i;
					this.FirstAdjustmentButton.onClick.RemoveAllListeners();
					this.FirstAdjustmentButton.onClick.AddListener(delegate()
					{
						this.SelectSuspensionAdjustment(firstID);
					});
					this.FirstAdjustmentButton.GetComponentInChildren<Text>().text = controlValues[i].ValueName;
					flag = true;
				}
				else
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FirstAdjustmentButton.gameObject);
					gameObject.transform.parent = this.FirstAdjustmentButton.transform.parent;
					gameObject.transform.localScale = this.FirstAdjustmentButton.transform.localScale;
					Button component = gameObject.GetComponent<Button>();
					int ID = i;
					component.onClick.RemoveAllListeners();
					component.onClick.AddListener(delegate()
					{
						this.SelectSuspensionAdjustment(ID);
					});
					component.GetComponentInChildren<Text>().text = controlValues[i].ValueName;
					this.LoadedAdjustmentButtons.Add(component);
				}
			}
		}
		if (!flag)
		{
			this.FirstAdjustmentButton.gameObject.SetActive(false);
		}
	}

	private void BuildGearingList()
	{
		this.ClearAdjustments();
		this.LoadedAdjustmentButtons.Add(this.FirstGearButton);
		this.GearingTutorialWindow.SetActive(false);
		this.UpdateGearingUpgradeButton();
		this.GearingAdjustmentSlider.gameObject.SetActive(false);
		bool flag = false;
		for (int i = 0; i < this.CurrentCarController.MaxGear; i++)
		{
			if (!flag)
			{
				int firstID = i;
				this.FirstGearButton.onClick.RemoveAllListeners();
				this.FirstGearButton.onClick.AddListener(delegate()
				{
					this.SelectGear(firstID);
				});
				this.FirstGearButton.GetComponentInChildren<Text>().text = this.GetGearName(firstID);
				flag = true;
			}
			else
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FirstGearButton.gameObject);
				gameObject.transform.parent = this.FirstGearButton.transform.parent;
				gameObject.transform.localScale = this.FirstGearButton.transform.localScale;
				Button component = gameObject.GetComponent<Button>();
				int ID = i;
				component.onClick.RemoveAllListeners();
				component.onClick.AddListener(delegate()
				{
					this.SelectGear(ID);
				});
				component.GetComponentInChildren<Text>().text = this.GetGearName(i);
				this.LoadedAdjustmentButtons.Add(component);
			}
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.FirstGearButton.gameObject);
		gameObject2.transform.parent = this.FirstGearButton.transform.parent;
		gameObject2.transform.localScale = this.FirstGearButton.transform.localScale;
		Button component2 = gameObject2.GetComponent<Button>();
		component2.onClick.RemoveAllListeners();
		component2.onClick.AddListener(delegate()
		{
			this.SelectGear(-1);
		});
		component2.GetComponentInChildren<Text>().text = "Low gear";
		this.LoadedAdjustmentButtons.Add(component2);
	}

	private void SetupAdjustmentSlider(AdjustmentSlider slider, SuspensionValue value, int Stage)
	{
		if (value == null)
		{
			return;
		}
		SuspensionControlLimit limit = SuspensionControlLimits.getLimit(this.SelectedSuspension.gameObject.name, value.ValueName);
		global::ValueType valueType = value.valueType;
		if (valueType != global::ValueType.Float)
		{
			if (valueType == global::ValueType.Int)
			{
				int iMin = limit.iMin;
				int iMax = limit.iMax;
				slider.SetupIntValue(value.ValueName, limit.iMin, limit.iMax, iMax, iMin, value.IntValue);
			}
		}
		else
		{
			float maxClamp = limit.fDef + (limit.fMax - limit.fDef) / 5f * (float)(Stage + 1);
			float minClamp = limit.fDef - (limit.fDef - limit.fMin) / 5f * (float)(Stage + 1);
			slider.SetupFloatValue(value.ValueName, limit.fMin, limit.fMax, minClamp, maxClamp, value.FloatValue);
		}
	}

	private void ClearAdjustments()
	{
		if (this.LoadedAdjustmentButtons != null)
		{
			for (int i = 1; i < this.LoadedAdjustmentButtons.Count; i++)
			{
				UnityEngine.Object.Destroy(this.LoadedAdjustmentButtons[i].gameObject);
			}
		}
		this.LoadedAdjustmentButtons = new List<Button>();
	}

	private string GetGearName(int GearID)
	{
		string result = string.Empty;
		switch (GearID + 1)
		{
		case 0:
			result = "Low gear";
			break;
		case 1:
			result = "1st gear";
			break;
		case 2:
			result = "2nd gear";
			break;
		case 3:
			result = "3rd gear";
			break;
		case 4:
			result = "4th gear";
			break;
		case 5:
			result = "5th gear";
			break;
		}
		return result;
	}

	private void SelectGear(int ID)
	{
		this.SelectedGear = ID;
		this.GearingAdjustmentSlider.gameObject.SetActive(true);
		this.UpdateGearingSlider();
	}

	private void UpdateGearingSlider()
	{
		string gearName = this.GetGearName(this.SelectedGear);
		float defaultGear = GearsManager.GetDefaultGear(this.SelectedGear);
		float minLimit = GearsManager.GetMinLimit(this.SelectedGear);
		float maxLimit = GearsManager.GetMaxLimit(this.SelectedGear);
		float maxClamp = defaultGear + (maxLimit - defaultGear) / 5f * (float)(this.CurrentCarController.GearingStage + 1);
		float minClamp = defaultGear - (defaultGear - minLimit) / 5f * (float)(this.CurrentCarController.GearingStage + 1);
		float currentValue;
		if (this.SelectedGear < 0)
		{
			currentValue = this.CurrentCarController.LowGearRatio;
		}
		else
		{
			currentValue = this.CurrentCarController.GearRatios[this.SelectedGear];
		}
		this.GearingAdjustmentSlider.SetupFloatValue(gearName, minLimit, maxLimit, minClamp, maxClamp, currentValue);
	}

	private void SelectSuspensionAdjustment(int ID)
	{
		this.SelectedSuspensionValue = this.SelectedSuspension.GetControlValues()[ID];
		this.SetupAdjustmentSlider(this.SuspensionAdjustmentSlider, this.SelectedSuspensionValue, this.SelectedSuspension.UpgradeStage);
		this.SuspensionAdjustmentSlider.gameObject.SetActive(true);
	}

	private void SuspensionAdjustmentChanged()
	{
		this.SelectedSuspensionValue.FloatValue = this.SuspensionAdjustmentSlider.slider.value;
		this.SelectedSuspensionValue.IntValue = (int)this.SuspensionAdjustmentSlider.slider.value;
		this.SelectedSuspension.OnValidate();
	}

	private void GearValueChanged()
	{
		if (this.SelectedGear >= 0)
		{
			this.CurrentCarController.GearRatios[this.SelectedGear] = this.GearingAdjustmentSlider.slider.value;
		}
		else
		{
			this.CurrentCarController.LowGearRatio = this.GearingAdjustmentSlider.slider.value;
		}
	}

	private void UpgradeSelectedSuspension(global::Currency currency)
	{
		int stage = this.SelectedSuspension.UpgradeStage + 1;
		int num = Suspensions.GetSuspensionUpgrade(this.SelectedSuspension.gameObject.name, stage).upgradeCost;
		if (currency == global::Currency.Gold)
		{
			num = Utility.CashToGold(num);
		}
		if (this.ProcessPurchase(currency, num))
		{
			this.SelectedSuspension.UpgradeStage++;
			this.CurrentVehicle.SaveVehicleData();
			this.UpdateSuspensionUpgradeButton();
			this.SetupAdjustmentSlider(this.SuspensionAdjustmentSlider, this.SelectedSuspensionValue, stage);
		}
		else if (currency == global::Currency.Money && (float)num / 100f <= (float)GameState.LoadStatsData().Gold)
		{
			this.UpgradeSelectedSuspension(global::Currency.Gold);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	private void UpgradeGearingStage()
	{
		int stage = this.CurrentCarController.GearingStage + 1;
		int num = PowerParts.GetPart(VehicleType.Any, PowerPartType.Gearing, stage).partCost;
		num = Utility.CashToGold(num);
		if (this.ProcessPurchase(global::Currency.Gold, num))
		{
			this.CurrentCarController.GearingStage++;
			this.CurrentVehicle.SaveVehicleData();
			this.UpdateGearingUpgradeButton();
			this.UpdateGearingSlider();
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	private void UpdateSuspensionUpgradeButton()
	{
		int upgradeStage = this.SelectedSuspension.UpgradeStage;
		SuspensionUpgrade suspensionUpgrade = Suspensions.GetSuspensionUpgrade(this.SelectedSuspension.gameObject.name, upgradeStage + 1);
		this.SuspensionUpgradeButton.interactable = (upgradeStage < 4 && suspensionUpgrade != null);
		this.SuspensionUpgradeButtonGold.interactable = (upgradeStage < 4 && suspensionUpgrade != null);
		this.SuspensionUpgradeCostText.text = ((upgradeStage >= 4 || suspensionUpgrade == null) ? "MAX" : ("$" + suspensionUpgrade.upgradeCost));
		this.SuspensionUpgradeCostGoldText.text = ((upgradeStage >= 4 || suspensionUpgrade == null) ? "--" : Utility.CashToGold(suspensionUpgrade.upgradeCost).ToString());
		this.SuspensionStageInUpgradeBarText.text = "Stage " + (upgradeStage + 1).ToString();
	}

	private void UpdateGearingUpgradeButton()
	{
		int gearingStage = this.CurrentCarController.GearingStage;
		PowerPart part = PowerParts.GetPart(VehicleType.Any, PowerPartType.Gearing, gearingStage + 1);
		this.GearingUpgradeButton.interactable = (gearingStage < 4);
		this.GearingUpgradeCostText.text = ((gearingStage >= 4) ? "--" : Utility.CashToGold(part.partCost).ToString());
		this.GearingStageInUpgradeBarText.text = "Stage " + (gearingStage + 1).ToString();
	}

	private void UpdateWheelsUpgradeButton()
	{
		int stage = this.SelectedWheelsControls.Stage;
		WheelsUpgrade wheelsUpgrade = Suspensions.GetWheelsUpgrade(stage + 1);
		bool dontLoadWheels = this.SelectedSuspension.DontLoadWheels;
		this.WheelsUpgradeButton.interactable = (stage < 4 && !dontLoadWheels);
		this.WheelsUpgradeButtonGold.interactable = (stage < 4 && !dontLoadWheels);
		this.WheelsUpgradeCostText.text = ((stage >= 4 || dontLoadWheels) ? "MAX" : ("$" + wheelsUpgrade.upgradeCost));
		this.WheelsUpgradeCostGoldText.text = ((stage >= 4 || dontLoadWheels) ? "--" : Utility.CashToGold(wheelsUpgrade.upgradeCost).ToString());
		this.WheelsStageText.text = "Stage " + (stage + 1).ToString();
	}

	private void UpgradeSelectedWheels(global::Currency currency)
	{
		int stage = this.SelectedWheelsControls.Stage + 1;
		int num = Suspensions.GetWheelsUpgrade(stage).upgradeCost;
		if (currency == global::Currency.Gold)
		{
			num = Utility.CashToGold(num);
		}
		if (this.ProcessPurchase(currency, num))
		{
			this.SelectedWheelsControls.Stage++;
			this.CurrentVehicle.SaveVehicleData();
			this.UpdateWheelsUpgradeButton();
			this.SetupAdjustmentSlider(this.WheelsAdjustmentSlider, this.SelectedSuspensionValue, stage);
		}
		else if (currency == global::Currency.Money && (float)num / 100f <= (float)GameState.LoadStatsData().Gold)
		{
			this.UpgradeSelectedWheels(global::Currency.Gold);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	private void ChangeWheelsSide(Side side)
	{
		this.currentSide = side;
		this.SelectedWheelsControls = ((side != Side.Front) ? this.CurrentSuspensionController.RearWheelsControls : this.CurrentSuspensionController.FrontWheelsControls);
		this.SelectedSuspension = ((side != Side.Front) ? this.CurrentSuspensionController.CurrentRearSuspension : this.CurrentSuspensionController.CurrentFrontSuspension);
		this.WheelsSideText.text = ((side != Side.Front) ? "Rear wheels" : "Front wheels");
		this.UpdateWheelsUpgradeButton();
		this.rimTuneButton.SetActive(!this.SelectedSuspension.DontLoadWheels);
		this.tireWidthTuneButton.SetActive(!this.SelectedSuspension.DontLoadWheels);
		this.tireRadiusTuneButton.SetActive(!this.SelectedSuspension.DontLoadWheels);
		this.WheelsAdjustmentSlider.gameObject.SetActive(false);
	}

	private void SelectRimSizeAdjustment()
	{
		this.SelectedSuspensionValue = this.SelectedWheelsControls.RimSize;
		this.SetupAdjustmentSlider(this.WheelsAdjustmentSlider, this.SelectedWheelsControls.RimSize, this.SelectedWheelsControls.Stage);
		this.WheelsAdjustmentSlider.gameObject.SetActive(true);
	}

	private void SelectWheelsRadiusAdjustment()
	{
		this.SelectedSuspensionValue = this.SelectedWheelsControls.WheelsRadius;
		this.SetupAdjustmentSlider(this.WheelsAdjustmentSlider, this.SelectedWheelsControls.WheelsRadius, this.SelectedWheelsControls.Stage);
		this.WheelsAdjustmentSlider.gameObject.SetActive(true);
	}

	private void SelectWheelsWidthAdjustment()
	{
		this.SelectedSuspensionValue = this.SelectedWheelsControls.WheelsWidth;
		this.SetupAdjustmentSlider(this.WheelsAdjustmentSlider, this.SelectedWheelsControls.WheelsWidth, this.SelectedWheelsControls.Stage);
		this.WheelsAdjustmentSlider.gameObject.SetActive(true);
	}

	private void WheelsAdjustmentChanged()
	{
		this.SelectedSuspensionValue.FloatValue = this.WheelsAdjustmentSlider.slider.value;
		this.CurrentSuspensionController.DoWheelsSize();
	}

	private void UninstallPowerPart()
	{
		PowerPartType selectedPowerPartType = this.SelectedPowerPartType;
		if (selectedPowerPartType != PowerPartType.Blower)
		{
			if (selectedPowerPartType == PowerPartType.Turbo)
			{
				this.CurrentCarController.TurboStage = 0;
				this.LoadPowerSubtypeScreen(this.SelectedPowerPartType);
			}
		}
		else
		{
			this.CurrentCarController.BlowerStage = 0;
			this.LoadPowerSubtypeScreen(this.SelectedPowerPartType);
		}
		this.CurrentCarController.UpdateEngineModel();
		this.CurrentVehicle.SaveVehicleData();
	}

	private void LoadPowerSubtypeScreen(PowerPartType partType)
	{
		if (partType == PowerPartType.Diesel && this.CurrentVehicle.vehicleType != VehicleType.Truck)
		{
			this.ShowMessage("Can't diesel swap this kind of vehicle!", true);
			return;
		}
		if (partType == PowerPartType.TankTracks && this.CurrentVehicle.vehicleType != VehicleType.Truck && this.CurrentVehicle.vehicleType != VehicleType.Crawler)
		{
			this.ShowMessage("Can't install tracks on this kind of vehicle!", true);
			return;
		}
		this.DescriptionPanel.SetActive(true);
		this.UninstallButton.gameObject.SetActive(false);
		this.SelectedPowerPartType = partType;
		int num = 4;
		switch (partType)
		{
		case PowerPartType.EngineBlock:
			this.TypeText.text = "Engine block";
			this.TypeImage.sprite = this.EngineBlockImage;
			this.CurrentPowerPartStage = this.CurrentCarController.EngineBlockStage;
			break;
		case PowerPartType.Head:
			this.TypeText.text = "Head";
			this.TypeImage.sprite = this.HeadsImage;
			this.CurrentPowerPartStage = this.CurrentCarController.HeadStage;
			break;
		case PowerPartType.Valvetrain:
			this.TypeText.text = "Valvetrain";
			this.TypeImage.sprite = this.ValvetrainImage;
			this.CurrentPowerPartStage = this.CurrentCarController.ValvetrainStage;
			break;
		case PowerPartType.Grip:
			this.TypeText.text = "Grip";
			this.TypeImage.sprite = this.GripImage;
			this.CurrentPowerPartStage = this.CurrentCarController.GripStage;
			break;
		case PowerPartType.Weight:
			this.TypeText.text = "Weight";
			this.TypeImage.sprite = this.WeightImage;
			this.CurrentPowerPartStage = this.CurrentCarController.WeightStage;
			break;
		case PowerPartType.Diesel:
			this.TypeText.text = "Diesel Swap";
			this.TypeImage.sprite = this.DieselImage;
			this.CurrentPowerPartStage = this.CurrentCarController.DieselStage;
			break;
		case PowerPartType.Durability:
			this.TypeText.text = "Durability";
			this.TypeImage.sprite = this.TitanImage;
			this.CurrentPowerPartStage = this.CurrentCarController.DurabilityStage;
			break;
		case PowerPartType.Gearbox:
			this.TypeText.text = "Gearbox type";
			this.TypeImage.sprite = this.GearboxImage;
			this.CurrentPowerPartStage = (int)this.CurrentCarController.transmissionType;
			num = 1;
			break;
		case PowerPartType.Ebrake:
			this.TypeText.text = "E-brake";
			this.TypeImage.sprite = this.EbrakeImage;
			this.CurrentPowerPartStage = this.CurrentCarController.Ebrake;
			num = 1;
			break;
		case PowerPartType.EnginePower:
			this.TypeText.text = "Engine power";
			this.TypeImage.sprite = this.EngineBlockImage;
			this.CurrentPowerPartStage = this.CurrentCarController.EngineBlockStage;
			partType = PowerPartType.EngineBlock;
			if (this.CurrentCarController.EngineBlockStage >= 4)
			{
				this.CurrentPowerPartStage = this.CurrentCarController.HeadStage;
				partType = PowerPartType.Head;
			}
			if (this.CurrentCarController.HeadStage >= 4)
			{
				this.CurrentPowerPartStage = this.CurrentCarController.ValvetrainStage;
				partType = PowerPartType.Valvetrain;
			}
			break;
		case PowerPartType.TankTracks:
			this.TypeText.text = "Tracks";
			this.TypeImage.sprite = this.TankTracksImage;
			this.CurrentPowerPartStage = ((!this.CurrentSuspensionController.FrontWheelsControls.TankTracks) ? 0 : 1);
			num = 1;
			break;
		case PowerPartType.Blower:
			this.TypeText.text = "Blower";
			this.TypeImage.sprite = this.BlowerImage;
			this.CurrentPowerPartStage = this.CurrentCarController.BlowerStage;
			if (this.CurrentCarController.BlowerStage > 0 && this.CurrentCarController.BlowerStage == this.CurrentCarController.PurchasedBlowerStage)
			{
				this.UninstallButton.gameObject.SetActive(true);
			}
			break;
		case PowerPartType.Turbo:
			this.TypeText.text = "Turbo";
			this.TypeImage.sprite = this.TurboImage;
			this.CurrentPowerPartStage = this.CurrentCarController.TurboStage;
			if (this.CurrentCarController.TurboStage > 0 && this.CurrentCarController.TurboStage == this.CurrentCarController.PurchasedTurboStage)
			{
				this.UninstallButton.gameObject.SetActive(true);
			}
			break;
		}
		if (partType == PowerPartType.Diesel && this.CurrentPowerPartStage < 3)
		{
			this.CurrentPowerPartStage = 3;
		}
		this.UpdatePowerStats();
		if (this.CurrentPowerPartStage < num)
		{
			this.DescriptionText.text = PowerParts.GetPart(this.CurrentVehicle.vehicleType, partType, this.CurrentPowerPartStage).Description;
			this.StageText.text = "Stage " + (this.CurrentPowerPartStage + 1).ToString();
		}
		else
		{
			this.DescriptionText.text = PowerParts.GetPart(this.CurrentVehicle.vehicleType, partType, this.CurrentPowerPartStage).Description;
			this.StageText.text = "Stage " + this.CurrentPowerPartStage.ToString();
		}
		if (partType == PowerPartType.Diesel)
		{
			this.StageText.text = ((this.CurrentCarController.DieselStage != 4) ? "Gas" : "Diesel");
		}
		if (partType == PowerPartType.TankTracks)
		{
			this.StageText.text = ((!this.CurrentSuspensionController.FrontWheelsControls.TankTracks) ? "Not installed" : "Installed");
		}
		if (partType == PowerPartType.Gearbox)
		{
			if (this.CurrentCarController.transmissionType == TransmissionType.AT)
			{
				this.StageText.text = "Stage - A/T";
			}
			else
			{
				this.StageText.text = "Stage - Manual";
			}
		}
		if (partType == PowerPartType.Ebrake)
		{
			if (this.CurrentCarController.Ebrake == 0)
			{
				this.StageText.text = "Stage - Not installed";
			}
			else
			{
				this.StageText.text = "Stage - Installed";
			}
		}
		switch (this.CurrentPowerPartStage + 2)
		{
		case 1:
			this.StageIcon.sprite = this.Stage1Icon;
			break;
		case 2:
			this.StageIcon.sprite = this.Stage2Icon;
			break;
		case 3:
			this.StageIcon.sprite = this.Stage3Icon;
			break;
		case 4:
			this.StageIcon.sprite = this.Stage4Icon;
			break;
		case 5:
			this.StageIcon.sprite = this.Stage5Icon;
			break;
		}
		this.UpgradeButton.interactable = (this.CurrentPowerPartStage < num || partType == PowerPartType.Gearbox || partType == PowerPartType.Diesel);
		this.UpgradeButtonGold.interactable = (this.CurrentPowerPartStage < num);
		if (partType == PowerPartType.Gearbox && this.CurrentCarController.ManualTransmissionPurchased)
		{
			this.UpgradeButtonGold.interactable = false;
		}
		if (partType == PowerPartType.Diesel && this.CurrentCarController.DieselPurchased)
		{
			this.UpgradeButtonGold.interactable = false;
		}
		if (partType == PowerPartType.TankTracks)
		{
			this.UpgradeButtonGold.interactable = !this.CurrentCarController.TankTracksPurchased;
			this.UpgradeButton.interactable = this.CurrentCarController.TankTracksPurchased;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		if (this.CurrentPowerPartStage == num)
		{
			text = "MAXED OUT!";
			text2 = "--";
			this.UpgradeCostDieselText.text = "MAXED OUT!";
		}
		else
		{
			int partCost = PowerParts.GetPart(this.CurrentVehicle.vehicleType, partType, this.CurrentPowerPartStage + 1).partCost;
			text = partCost.ToString("$0,0");
			text2 = Utility.CashToGold(partCost).ToString();
		}
		if (partType == PowerPartType.TankTracks)
		{
			text = "Gold Only";
		}
		if (partType == PowerPartType.Gearbox)
		{
			if (this.CurrentCarController.ManualTransmissionPurchased)
			{
				text2 = "--";
				if (this.CurrentCarController.transmissionType == TransmissionType.Manual)
				{
					text = "Back to A/T";
				}
				else
				{
					text = "Install Manual";
				}
			}
			if (this.CurrentCarController.GetComponent<TankController>() != null)
			{
				this.UpgradeButtonGold.interactable = false;
				this.UpgradeButton.interactable = false;
			}
		}
		if (partType == PowerPartType.TankTracks)
		{
			if (this.CurrentCarController.TankTracksPurchased)
			{
				text2 = "--";
				if (this.CurrentSuspensionController.FrontWheelsControls.TankTracks)
				{
					text = "Back to wheels";
				}
				else
				{
					text = "Install tracks";
				}
			}
			if (this.CurrentCarController.GetComponent<TankController>() != null)
			{
				this.UpgradeButtonGold.interactable = false;
				this.UpgradeButton.interactable = false;
			}
		}
		if (partType == PowerPartType.Diesel && this.CurrentCarController.DieselPurchased)
		{
			text2 = "--";
			if (this.CurrentCarController.DieselStage == 4)
			{
				text = "Back to gas";
			}
			else
			{
				text = "Swap to diesel";
			}
		}
		if (partType == PowerPartType.Ebrake)
		{
			if (this.CurrentCarController.Ebrake == 1)
			{
				text = "Installed";
			}
			if (this.CurrentCarController.GetComponent<TankController>() != null)
			{
				this.UpgradeButtonGold.interactable = false;
				this.UpgradeButton.interactable = false;
			}
		}
		if (partType == PowerPartType.Blower && this.CurrentCarController.PurchasedBlowerStage > this.CurrentCarController.BlowerStage)
		{
			text = "Install";
			text2 = "0";
			this.UpgradeButtonGold.interactable = false;
		}
		if (partType == PowerPartType.Turbo && this.CurrentCarController.PurchasedTurboStage > this.CurrentCarController.TurboStage)
		{
			text = "Install";
			text2 = "0";
			this.UpgradeButtonGold.interactable = false;
		}
		this.UpgradeCostText.text = text;
		this.UpgradeCostGoldText.text = text2;
		this.SelectedSubPowerPartType = partType;
	}

	private void UpgradePowerPart(global::Currency currency)
	{
		PowerPart part = PowerParts.GetPart(this.CurrentVehicle.vehicleType, this.SelectedSubPowerPartType, this.CurrentPowerPartStage + 1);
		if (this.CurrentCarController.DieselStage == 4)
		{
			this.CurrentCarController.DieselPurchased = true;
		}
		int num = 0;
		if (part != null)
		{
			num = part.partCost;
		}
		else
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Part was null: ",
				this.CurrentVehicle.vehicleType,
				" : ",
				this.SelectedSubPowerPartType,
				" : ",
				(this.CurrentPowerPartStage + 1).ToString()
			}));
		}
		UnityEngine.Debug.Log("Cost: " + num);
		if (currency == global::Currency.Gold)
		{
			num = Utility.CashToGold(num);
		}
		if (this.SelectedSubPowerPartType == PowerPartType.Gearbox && this.CurrentCarController.ManualTransmissionPurchased)
		{
			num = 0;
		}
		if (this.SelectedSubPowerPartType == PowerPartType.TankTracks && this.CurrentCarController.TankTracksPurchased)
		{
			num = 0;
		}
		if (this.SelectedSubPowerPartType == PowerPartType.Diesel && this.CurrentCarController.DieselPurchased)
		{
			num = 0;
		}
		if (this.SelectedSubPowerPartType == PowerPartType.Blower && this.CurrentCarController.BlowerStage == 0 && this.CurrentCarController.PurchasedBlowerStage > 0)
		{
			num = 0;
		}
		if (this.SelectedSubPowerPartType == PowerPartType.Turbo && this.CurrentCarController.TurboStage == 0 && this.CurrentCarController.PurchasedTurboStage > 0)
		{
			num = 0;
		}
		if (this.ProcessPurchase(currency, num))
		{
			switch (this.SelectedSubPowerPartType)
			{
			case PowerPartType.EngineBlock:
				this.CurrentCarController.EngineBlockStage++;
				break;
			case PowerPartType.Head:
				this.CurrentCarController.HeadStage++;
				break;
			case PowerPartType.Valvetrain:
				this.CurrentCarController.ValvetrainStage++;
				break;
			case PowerPartType.Grip:
				this.CurrentCarController.GripStage++;
				break;
			case PowerPartType.Weight:
				this.CurrentCarController.WeightStage++;
				break;
			case PowerPartType.Diesel:
				if (this.CurrentCarController.BlowerStage > 0)
				{
					this.ShowMessage("Uninstall blower first!", true);
					return;
				}
				if (this.CurrentCarController.DieselStage == 3)
				{
					this.CurrentCarController.DieselStage = 4;
					this.CurrentCarController.DieselPurchased = true;
				}
				else
				{
					this.CurrentCarController.DieselStage = 3;
				}
				break;
			case PowerPartType.Durability:
				this.CurrentCarController.DurabilityStage++;
				break;
			case PowerPartType.Gearbox:
				if (this.CurrentCarController.transmissionType == TransmissionType.AT)
				{
					this.CurrentCarController.transmissionType = TransmissionType.Manual;
					this.CurrentCarController.ManualTransmissionPurchased = true;
				}
				else
				{
					this.CurrentCarController.transmissionType = TransmissionType.AT;
				}
				break;
			case PowerPartType.Ebrake:
				this.CurrentCarController.Ebrake = 1;
				break;
			case PowerPartType.EnginePower:
				if (this.CurrentCarController.EngineBlockStage < 4)
				{
					this.CurrentCarController.EngineBlockStage++;
				}
				else if (this.CurrentCarController.HeadStage < 4)
				{
					this.CurrentCarController.HeadStage++;
				}
				else if (this.CurrentCarController.ValvetrainStage < 4)
				{
					this.CurrentCarController.ValvetrainStage++;
				}
				break;
			case PowerPartType.TankTracks:
				if (!this.CurrentSuspensionController.FrontWheelsControls.TankTracks)
				{
					this.CurrentSuspensionController.FrontWheelsControls.TankTracks = true;
					this.CurrentSuspensionController.RearWheelsControls.TankTracks = true;
					this.CurrentCarController.TankTracksPurchased = true;
				}
				else
				{
					this.CurrentSuspensionController.FrontWheelsControls.TankTracks = false;
					this.CurrentSuspensionController.RearWheelsControls.TankTracks = false;
				}
				this.CurrentSuspensionController.LoadWheels();
				this.CurrentPartsSwitcher.GenerateRimsTexture();
				break;
			case PowerPartType.Blower:
				if (this.CurrentCarController.DieselStage == 4)
				{
					this.ShowMessage("Uninstall diesel first!", true);
					return;
				}
				if (this.CurrentCarController.TurboStage > 0)
				{
					this.ShowMessage("Uninstall turbo first!", true);
					return;
				}
				if (this.CurrentCarController.PurchasedBlowerStage > this.CurrentCarController.BlowerStage)
				{
					this.CurrentCarController.BlowerStage = this.CurrentCarController.PurchasedBlowerStage;
				}
				else
				{
					this.CurrentCarController.BlowerStage++;
					this.CurrentCarController.PurchasedBlowerStage = this.CurrentCarController.BlowerStage;
				}
				break;
			case PowerPartType.Turbo:
				if (this.CurrentCarController.BlowerStage > 0)
				{
					this.ShowMessage("Uninstall blower first!", true);
					return;
				}
				if (this.CurrentCarController.PurchasedTurboStage > this.CurrentCarController.TurboStage)
				{
					this.CurrentCarController.TurboStage = this.CurrentCarController.PurchasedTurboStage;
				}
				else
				{
					this.CurrentCarController.TurboStage++;
					this.CurrentCarController.PurchasedTurboStage = this.CurrentCarController.TurboStage;
				}
				break;
			}
			this.CurrentCarController.UpdateEngineModel();
			this.CurrentVehicle.SaveVehicleData();
			this.LoadPowerSubtypeScreen(this.SelectedPowerPartType);
		}
		else if (currency == global::Currency.Money && Utility.CashToGold(num) <= GameState.LoadStatsData().Gold)
		{
			this.UpgradePowerPart(global::Currency.Gold);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	private void UpdatePowerStats()
	{
		float fillAmount = ((float)this.CurrentCarController.EngineBlockStage + (float)this.CurrentCarController.HeadStage + (float)this.CurrentCarController.ValvetrainStage + 3f) / 15f;
		this.PowerBar.fillAmount = fillAmount;
		float fillAmount2 = ((float)this.CurrentCarController.GripStage + 1f) / 5f;
		this.GripBar.fillAmount = fillAmount2;
		float fillAmount3 = ((float)this.CurrentCarController.WeightStage + 1f) / 5f;
		this.WeightBar.fillAmount = fillAmount3;
		float fillAmount4 = ((float)this.CurrentCarController.DurabilityStage + 1f) / 5f;
		this.DurabilityBar.fillAmount = fillAmount4;
	}

	private void RepairVehicle(global::Currency currency)
	{
		if (this.CurrentVehicle == null)
		{
			return;
		}
		int num = (int)((float)this.FullRepairPrice * (100f - this.CurrentCarController.CarHealth) / 100f);
		if (currency == global::Currency.Gold)
		{
			num = Mathf.Max(1, num / 100);
		}
		if (this.ProcessPurchase(currency, num))
		{
			this.CurrentCarController.CarHealth = 100f;
			this.UpdateSideButtons();
			this.CurrentVehicle.SaveVehicleData();
		}
		else if (currency == global::Currency.Money && Utility.CashToGold(num) <= GameState.LoadStatsData().Gold)
		{
			this.RepairVehicle(global::Currency.Gold);
		}
		else
		{
			this.CurrentCarController.CarHealth = 100f;
			this.CurrentVehicle.SaveVehicleData();
		}
	}

	private void LoadWrap(int WrapID)
	{
		if (WrapID > 16)
		{
			WrapID = 0;
		}
		if (WrapID < 0)
		{
			WrapID = 16;
		}
		this.SelectedWrap = WrapID;
		this.WrapLayerCountText.text = "Layers: " + this.CurrentPartsSwitcher.WrapLayers.Count + "/5";
		this.WrapLayerCostText.text = ((this.CurrentPartsSwitcher.WrapLayers.Count != 0 && this.CurrentPartsSwitcher.WrapLayerCount <= this.CurrentPartsSwitcher.WrapLayers.Count && this.CurrentPartsSwitcher.SessionWrapLayerCount <= this.CurrentPartsSwitcher.WrapLayers.Count) ? "Apply   20" : "Apply");
		this.WrapGoldBars.SetActive(this.CurrentPartsSwitcher.WrapLayers.Count != 0 && this.CurrentPartsSwitcher.WrapLayerCount <= this.CurrentPartsSwitcher.WrapLayers.Count && this.CurrentPartsSwitcher.SessionWrapLayerCount <= this.CurrentPartsSwitcher.WrapLayers.Count);
		this.ApplyLayerButton.interactable = (WrapID > 0 && this.CurrentPartsSwitcher.WrapLayers.Count < 5);
		this.UpdateWrap();
		this.UpdateWrapPreview();
	}

	public void WrapOffsetChanged()
	{
		if (!this.WrapCoordsSlidersInitialized)
		{
			return;
		}
		this.WrapCoords = new Vector4(this.XOffsetSlider.value, this.YOffsetSlider.value, this.XTillingSlider.value, this.YTillingSlider.value);
		this.UpdateWrap();
	}

	private void UpdateWrap()
	{
		this.WrapColor.a = this.TransparencySlider.value;
		if (this.WrapCoords == Vector4.zero)
		{
			this.WrapCoords = new Vector4(0f, 0f, 1f, 1f);
		}
		this.CurrentPartsSwitcher.ChangeCurrentWrap(this.SelectedWrap, this.WrapColor, this.WrapCoords);
	}

	private void UpdateWrapPreview()
	{
		Sprite x = Resources.Load<Sprite>("Wraps/WrapPreview" + this.SelectedWrap);
		if (x != null)
		{
			this.WrapPreviewImage.sprite = Resources.Load<Sprite>("Wraps/WrapPreview" + this.SelectedWrap);
		}
	}

	public void ApplyWrap()
	{
		if (this.SelectedWrap == 0)
		{
			return;
		}
		int num = (this.CurrentPartsSwitcher.WrapLayers.Count != 0 && this.CurrentPartsSwitcher.WrapLayerCount <= this.CurrentPartsSwitcher.WrapLayers.Count && this.CurrentPartsSwitcher.SessionWrapLayerCount <= this.CurrentPartsSwitcher.WrapLayers.Count) ? 20 : 0;
		UnityEngine.Debug.Log("Layer Applying: " + this.CurrentPartsSwitcher.WrapLayers.Count);
		UnityEngine.Debug.Log("Layers Bought: " + this.CurrentPartsSwitcher.WrapLayerCount);
		UnityEngine.Debug.Log("Session Layers: " + this.CurrentPartsSwitcher.SessionWrapLayerCount);
		if (num == 0)
		{
			UnityEngine.Debug.Log("Free layer!");
		}
		if (this.ProcessPurchase(global::Currency.Gold, num))
		{
			this.CurrentPartsSwitcher.BakeWrap();
			this.ClearLayer();
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	public void ClearLayer()
	{
		this.WrapCoords = new Vector4(0f, 0f, 1f, 1f);
		this.XOffsetSlider.value = 0f;
		this.YOffsetSlider.value = 0f;
		this.XTillingSlider.value = 1f;
		this.YTillingSlider.value = 1f;
		this.TransparencySlider.value = 1f;
		this.WrapColor = Color.white;
		this.LoadWrap(0);
	}

	public void RemoveAllLayers()
	{
		this.ClearLayer();
		this.CurrentPartsSwitcher.ClearWraps();
		this.LoadWrap(0);
	}

	public void LoadMenu(MenuState state, bool ThroughFade, bool FromMainMenu)
	{
		base.StartCoroutine(this.LoadMenuCoroutine(state, ThroughFade, FromMainMenu));
	}

	private IEnumerator LoadMenuCoroutine(MenuState state, bool ThroughFade, bool FromMainMenu)
	{
		if (ThroughFade)
		{
			Resources.UnloadUnusedAssets();
			for (float f = 0f; f <= 1.1f; f += 0.1f)
			{
				this.FadeScreen.alpha = f;
				yield return null;
			}
		}
		this.FadeScreen.alpha = 1f;
		this.Logo.SetActive(false);
		this.Sidebar.gameObject.SetActive(false);
		this.menuState = state;
		this.UpdateScreen();
		switch (this.menuState)
		{
		case MenuState.MainMenu:
			UnityEngine.Debug.Log("Main menu: " + GameState.SelectedGarageVehicleID);
			this.isTrailerEquipped = (Utility.EqiuppedTrailer() != string.Empty);
			this.Sidebar.gameObject.SetActive(true);
			this.LoadVehiclesInGarage();
			if (this.SelectedVehicleInGarageID == 0)
			{
				this.SelectedVehicleInGarageID = GameState.SelectedGarageVehicleID;
			}
			if (this.SelectedVehicleInGarageID >= this.LoadedVehiclesInGarage.Count)
			{
				this.SelectedVehicleInGarageID = this.LoadedVehiclesInGarage.Count - 1;
			}
			if (this.LoadedVehiclesInGarage != null && this.LoadedVehiclesInGarage.Count > 0)
			{
				this.ChangeCurrentVehicle(this.LoadedVehiclesInGarage[this.SelectedVehicleInGarageID], true);
			}
			else
			{
				this.SetCameraTarget(this.GarageVehiclePoints[0].position, true);
			}
			this.UpdateSideButtons();
			this.Logo.SetActive(true);
			break;
		case MenuState.TruckSelector:
			this.SetCameraTarget(this.TruckSelectorSpawnPoint.position, true);
			this.UnloadVehiclesInGarage();
			this.SelectedTruckIDInSelector = 0;
			switch (this.SelectedVehicleType)
			{
			case VehicleType.Truck:
				this.SelectedArray = this.StreetTrucks;
				break;
			case VehicleType.ATV:
				this.SelectedArray = this.ATVs;
				break;
			case VehicleType.SideBySide:
				this.SelectedArray = this.SideBySides;
				break;
			case VehicleType.Crawler:
				this.SelectedArray = this.Crawlers;
				break;
			case VehicleType.Bike:
				this.SelectedArray = this.Bikes;
				break;
			case VehicleType.TurnKey:
				this.SelectedArray = this.TurnKeyVehicles;
				break;
			case VehicleType.Trailer:
				this.SelectedArray = this.trailers;
				break;
			}
			this.UpdateVehicleSelector(false);
			break;
		case MenuState.CustomizeCategorySelector:
			if (FromMainMenu)
			{
				this.ModConfirmation.SetActive(false);
				this.GetCurrentCarToCustomizationPoint();
				PartGroup[] partGroups = this.CurrentPartsSwitcher.partGroups;
				this.partGroupsBeforeEnteringCustomization = new PartGroup[partGroups.Length];
				for (int i = 0; i < partGroups.Length; i++)
				{
					this.partGroupsBeforeEnteringCustomization[i] = partGroups[i].DeepCopy();
				}
				this.FrontWheelsBeforeEnteringCustomization = this.CurrentSuspensionController.FrontWheelsControls.DeepCopy();
				this.RearWheelsBeforeEnteringCustomization = this.CurrentSuspensionController.RearWheelsControls.DeepCopy();
				this.FrontRimsColorBeforeEngeringCustomizaiton = this.CurrentPartsSwitcher.FRimsColor;
				this.FrontBeadlockColorBeforeEnteringCustomization = this.CurrentPartsSwitcher.FBeadlocksColor;
				this.RearRimsColorBeforeEngeringCustomizaiton = this.CurrentPartsSwitcher.RRimsColor;
				this.RearBeadlockColorBeforeEnteringCustomization = this.CurrentPartsSwitcher.RBeadlocksColor;
				this.WashCostText.text = "$" + this.WashPrice.ToString();
			}
			this.ToggleBodyColorPicker(false);
			this.SetCameraTarget(this.TruckSelectorSpawnPoint.position, true);
			this.PurchaseModsButton.SetActive(!FromMainMenu);
			this.PurchaseModsButtonGold.SetActive(!FromMainMenu);
			this.UpdatePurchaseModsButton();
			this.UpdateCameraSettings(null);
			if (this.LightsOn)
			{
				this.ToggleLights();
			}
			this.ClearLayer();
			break;
		case MenuState.CustomizeBodyParts:
			this.ChangePartGroup(0);
			break;
		case MenuState.CustomizePaint:
			this.UpdatePaintTypeButtons();
			break;
		case MenuState.CustomizeRims:
			this.ChangeRimSide(Side.Front);
			break;
		case MenuState.CustomizeTires:
			this.ChangeTireSide(Side.Front);
			break;
		case MenuState.CustomizeWraps:
			this.WrapCoordsSlidersInitialized = false;
			this.SelectedWrap = this.CurrentPartsSwitcher.AppliedWrapID;
			this.WrapColor = this.CurrentPartsSwitcher.WrapColor;
			this.WrapCoords = this.CurrentPartsSwitcher.WrapCoords;
			this.ClearLayer();
			this.WrapColorPicker.ColorChangedCallback = new Action(this.ChooseCustomWrapColor);
			this.WrapColorPicker.Color = Color.white;
			this.LoadWrap(this.SelectedWrap);
			this.WrapCoordsSlidersInitialized = true;
			break;
		case MenuState.Drivetrain:
			this.UpdateCameraSettings(null);
			this.ClearAdjustments();
			this.GetCurrentCarToCustomizationPoint();
			break;
		case MenuState.SwitchSuspension:
			this.ChangeSuspensionSide(Side.Front);
			break;
		case MenuState.TuneSuspension:
			this.ChangeSuspensionAdjustmentsSide(Side.Front);
			break;
		case MenuState.TuneWheels:
			this.ChangeWheelsSide(Side.Front);
			break;
		case MenuState.TestSuspension:
			if (SuspensionTestRoomController.Instance != null)
			{
				SuspensionTestRoomController.Instance.InitializeSuspensionTest(this.CurrentVehicle.gameObject);
				this.SetCameraTarget(SuspensionTestRoomController.Instance.CarPositionPoint.position, true);
				CameraController.Instance.SetCameraPos(160f, 5f, 5f);
				this.UpdateCameraSettings(null);
			}
			break;
		case MenuState.TuneGearing:
			this.BuildGearingList();
			break;
		case MenuState.Dyno:
			if (DynoRoomController.Instance != null)
			{
				DynoRoomController.Instance.InitializeDyno(this.CurrentVehicle.gameObject);
				Utility.AlignVehicleByGround(this.CurrentVehicle.transform, false);
				this.SetCameraTarget(DynoRoomController.Instance.CarPos.position, true);
				CameraController.Instance.SetCameraPos(90f, 5f, 5f);
				this.UpdateCameraSettings(null);
			}
			this.BuyTuningPackButton.interactable = !this.CurrentCarController.TuningEnginePurchased;
			this.BuyPerfectSetupButton.interactable = !this.CurrentCarController.PerfectSetupPurchased;
			this.DynoRunsLeftText.text = GameState.LoadStatsData().DynoRuns.ToString();
			this.EngineTuningSlider.gameObject.SetActive(false);
			this.DynoTutorialWindow.SetActive(false);
			break;
		case MenuState.Power:
			if (FromMainMenu)
			{
				this.DescriptionPanel.SetActive(false);
				this.StatsPanel.SetActive(true);
				this.UpdatePowerStats();
				this.GetCurrentCarToCustomizationPoint();
				CameraController.Instance.GetComponent<Camera>().fieldOfView = 69f;
			}
			break;
		case MenuState.Play:
			Resources.UnloadUnusedAssets();
			this.StatsPanel.SetActive(true);
			GameState.Clear();
			if (!PhotonNetwork.insideLobby)
			{
				MultiplayerManager.Connect();
			}
			if (Utility.EqiuppedTrailer() != string.Empty)
			{
				string @string = DataStore.GetString(Utility.EqiuppedTrailer());
				VehicleData vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(@string);
				TrailerController trailerController = Resources.Load("Vehicles/" + vehicleData.VehicleName, typeof(TrailerController)) as TrailerController;
				if (trailerController != null && trailerController.gooseneck && this.CurrentPartsSwitcher.gooseneckMount == null)
				{
					this.ShowMessage("Note: you can't take your trailer in map because gooseneck trailers can only be mounted on pickups!", true);
				}
			}
			break;
		case MenuState.Map:
		{
			StatsData statsData = GameState.LoadStatsData();
			this.StatsPanel.SetActive(true);
			this.DesertLockXPText.text = this.UnlockDesertXP.ToString() + "XP";
			this.DesertLockPanel.SetActive(statsData.XP < this.UnlockDesertXP && !DataStore.GetBool("MapDesertNGUnlocked", false) && !statsData.IsMember);
			this.RockParkLockXPText.text = this.UnlockRockParkXP.ToString() + "XP";
			this.RockParkLockPanel.SetActive(statsData.XP < this.UnlockRockParkXP && !DataStore.GetBool("RockParkNGUnlocked", false) && !statsData.IsMember);
			this.StuntParkLockXPText.text = this.UnlockStuntParkXP.ToString() + "XP";
			this.StuntParkLockPabel.SetActive(statsData.XP < this.UnlockStuntParkXP && !DataStore.GetBool("StuntParkUnlocked", false) && !statsData.IsMember);
			break;
		}
		case MenuState.MultiplayerGameType:
			if (GameState.Password != null && GameState.Password != string.Empty)
			{
				this.MultiplayerPrivateButton.SetActive(false);
			}
			else
			{
				this.MultiplayerPrivateButton.SetActive(true);
			}
			this.StatsPanel.SetActive(true);
			break;
		case MenuState.PrivateMultiplayer:
			UnityEngine.Debug.Log("Multiplayer!");
			this.StatsPanel.SetActive(true);
			break;
		case MenuState.TrailRaceLobby:
			this.CreateTrailRaceRoomsList(true);
			break;
		case MenuState.StorageArea:
		{
			this.SetCameraTarget(this.TruckSelectorSpawnPoint.position, true);
			this.UnloadVehiclesInGarage();
			this.SelectedTruckIDInSelector = 0;
			string[] storageVehiclesIDs = this.GetStorageVehiclesIDs();
			this.StoredVehicles = new List<VehicleData>();
			for (int j = 0; j < storageVehiclesIDs.Length; j++)
			{
				string string2 = DataStore.GetString(storageVehiclesIDs[j]);
				VehicleData vehicleData2 = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string2);
				vehicleData2.SavedID = storageVehiclesIDs[j];
				this.StoredVehicles.Add(vehicleData2);
			}
			this.UpdateVehicleSelector(true);
			break;
		}
		case MenuState.Drones:
			this.SetCameraTarget(this.TruckSelectorSpawnPoint.position + Vector3.up, true);
			CameraController.Instance.MaxDistance = 2f;
			CameraController.Instance.MinDistance = 1f;
			this.UnloadVehiclesInGarage();
			this.selectedDroneInSelectorID = 0;
			this.UpdateDrone();
			break;
		}
		if (ThroughFade)
		{
			for (float f2 = 1f; f2 >= 0f; f2 -= 0.1f)
			{
				this.FadeScreen.alpha = f2;
				yield return null;
			}
		}
		this.FadeScreen.alpha = 0f;
		yield break;
	}

	public void CreateTrailRaceRoomsList(bool Randomize = true)
	{
		if (this.InstantiatedRoomBars != null && this.InstantiatedRoomBars.Count > 0)
		{
			int count = this.InstantiatedRoomBars.Count;
			for (int i = 0; i < count; i++)
			{
				UnityEngine.Object.Destroy(this.InstantiatedRoomBars[i]);
			}
		}
		this.InstantiatedRoomBars = new List<GameObject>();
		this.TrailRaceRooms = MultiplayerManager.GetAllTrailRaceRooms();
		if (Randomize)
		{
			this.RandomizeArray<RoomInfo>(this.TrailRaceRooms);
		}
		this.FirstTrailRaceElement.SetActive(this.TrailRaceRooms.Length > 0);
		this.NoRaceAvailableText.gameObject.SetActive(this.TrailRaceRooms.Length == 0);
		if (this.TrailRaceRooms.Length > 0)
		{
			string text = "  ";
			text += this.TrailRaceRooms[0].CustomProperties["HostPlayerName"].ToString();
			text += "   -   ";
			string str = "Trail";
			if (this.TrailRaceRooms[0].CustomProperties["TrailID"] != null)
			{
				str = Trails.trails[(int)this.TrailRaceRooms[0].CustomProperties["TrailID"]].TrailName;
			}
			text += str;
			this.FirstTrailRaceElement.GetComponentInChildren<Text>().text = text;
		}
		if (this.TrailRaceRooms.Length > 1)
		{
			for (int j = 1; j < this.TrailRaceRooms.Length; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FirstTrailRaceElement);
				gameObject.transform.parent = this.FirstTrailRaceElement.transform.parent;
				gameObject.transform.localScale = this.FirstTrailRaceElement.transform.localScale;
				string text2 = "  ";
				text2 += this.TrailRaceRooms[j].CustomProperties["HostPlayerName"].ToString();
				text2 += "   -   ";
				string str2 = "Trail";
				if (this.TrailRaceRooms[j].CustomProperties["TrailID"] != null)
				{
					str2 = Trails.trails[(int)this.TrailRaceRooms[j].CustomProperties["TrailID"]].TrailName;
				}
				text2 += str2;
				gameObject.GetComponentInChildren<Text>().text = text2;
				this.InstantiatedRoomBars.Add(gameObject);
			}
		}
	}

	private void RandomizeArray<T>(T[] ar)
	{
		for (int i = 0; i < ar.Length; i++)
		{
			T t = ar[i];
			int num = UnityEngine.Random.Range(i, ar.Length);
			ar[i] = ar[num];
			ar[num] = t;
		}
	}

	private void UpdateSideButtons()
	{
		this.WashButton.SetActive(false);
		this.RepairIcon.SetActive(false);
		this.equipTrailerButton.SetActive(false);
		bool flag = DataStore.GetString("VehicleOnTrailer") != string.Empty;
		this.unequipTrailerButton.SetActive(this.isTrailerEquipped);
		if (this.CurrentVehicle != null)
		{
			this.loadCarOnTrailerButton.SetActive(this.isTrailerEquipped && this.CurrentVehicle.vehicleType != VehicleType.Trailer && this.CurrentVehicle.vehicleType != VehicleType.Bike);
			this.unloadCarsFromTrailerButton.SetActive(this.isTrailerEquipped && this.CurrentVehicle.vehicleType == VehicleType.Trailer && flag);
		}
		else
		{
			this.loadCarOnTrailerButton.SetActive(false);
			this.unloadCarsFromTrailerButton.SetActive(false);
		}
		if (this.CurrentVehicle != null && this.CurrentPartsSwitcher != null)
		{
			this.WashButton.SetActive(this.CurrentPartsSwitcher.Dirtiness > 0f && !this.CurrentPartsSwitcher.Washing);
			int num = (int)((float)this.FullRepairPrice * (100f - this.CurrentCarController.CarHealth) / 100f);
			this.RepairCostText.text = "$" + num;
			this.RepairIcon.SetActive(num > 0);
		}
		if (this.CurrentVehicle != null && this.CurrentVehicle.vehicleType == VehicleType.Trailer && !this.CurrentVehicle.equipped)
		{
			this.equipTrailerButton.SetActive(true);
		}
	}

	public void PickMultiplayerGameType(int gameType)
	{
		GameState.GameType = (GameType)gameType;
		if (gameType != 3)
		{
			this.LoadMenu(MenuState.Map, false, false);
		}
		else if (GameState.Password != null && GameState.Password != string.Empty)
		{
			this.LoadMenu(MenuState.TrailSelectorScreen, false, false);
		}
		else
		{
			this.LoadMenu(MenuState.TrailRaceLobby, false, false);
		}
	}

	public void GoPlaying(bool isMultiplayer)
	{
		this.GoPlaying(isMultiplayer, null);
	}

	public void GoPlaying(bool isMultiplayer, string password)
	{
		if (this.CurrentVehicle == null)
		{
			return;
		}
		if (isMultiplayer && !PhotonNetwork.connectedAndReady)
		{
			this.ShowMessage("You're not connected yet. Try again...", true);
			return;
		}
		GameState.Populate(this.CurrentVehicle.VehicleID, null, (!isMultiplayer) ? GameMode.SinglePlayer : GameMode.Multiplayer, GameType.FreeRoam, password);
		if (isMultiplayer)
		{
			this.LoadMenu(MenuState.MultiplayerGameType, false, false);
		}
		else
		{
			this.LoadMenu(MenuState.Map, false, false);
		}
	}

	public void PickMap()
	{
		this.LoadMenu(MenuState.Map, false, false);
	}

	public void ShowWaiting(string message)
	{
		this.SceneLoadingText.text = message;
		this.SceneLoading.SetActive(true);
	}

	public void HideWaiting()
	{
		this.SceneLoading.SetActive(false);
	}

	private void LoadCurrentComponents(GameObject currentVehicleObject)
	{
		this.CurrentVehicle = currentVehicleObject.GetComponent<VehicleDataManager>();
		this.CurrentCarController = currentVehicleObject.GetComponent<CarController>();
		this.CurrentPartsSwitcher = currentVehicleObject.GetComponent<BodyPartsSwitcher>();
		this.CurrentSuspensionController = currentVehicleObject.GetComponent<SuspensionController>();
	}

	private void GetCurrentCarToCustomizationPoint()
	{
		if (this.LoadedVehicleInSelector != null)
		{
			UnityEngine.Object.Destroy(this.LoadedVehicleInSelector);
		}
		if (this.LoadedVehicleInCustomization != null)
		{
			UnityEngine.Object.Destroy(this.LoadedVehicleInCustomization);
		}
		if (this.loadedVehicleOnTrailer != null)
		{
			UnityEngine.Object.Destroy(this.loadedVehicleOnTrailer);
		}
		if (this.loadedDrone != null)
		{
			UnityEngine.Object.Destroy(this.loadedDrone);
		}
		string name = this.CurrentVehicle.name;
		string vehicleID = this.CurrentVehicle.VehicleID;
		this.UnloadVehiclesInGarage();
		this.LoadedVehicleInCustomization = this.LoadVehicle(name, this.TruckSelectorSpawnPoint, false, string.Empty);
		this.LoadCurrentComponents(this.LoadedVehicleInCustomization);
		this.CurrentVehicle.VehicleID = vehicleID;
		this.CurrentVehicle.LoadVehicleData();
		this.CurrentPartsSwitcher.UpdateColor(false);
		this.CurrentPartsSwitcher.UpdateDirtiness();
		this.SetCameraTarget(this.TruckSelectorSpawnPoint.position, true);
		base.StartCoroutine(this.PreventVehicleJumpingOnLoad(this.LoadedVehicleInCustomization));
		CameraController.Instance.SetCameraPos(30f, 15f, (this.CurrentCarController.GarageMaxDistance + this.CurrentCarController.GarageMinDistance) / 2f);
	}

	public void UpdateScreen()
	{
		this.MainMenu.SetActive(this.menuState == MenuState.MainMenu);
		this.TruckTypeSelector.SetActive(this.menuState == MenuState.TruckTypeSelector);
		this.StorageArea.SetActive(this.menuState == MenuState.StorageArea);
		this.TruckSelector.SetActive(this.menuState == MenuState.TruckSelector);
		this.CustomizeCategorySelector.SetActive(this.menuState == MenuState.CustomizeCategorySelector);
		this.CustomizeBodyParts.SetActive(this.menuState == MenuState.CustomizeBodyParts);
		this.CustomizePaint.SetActive(this.menuState == MenuState.CustomizePaint);
		this.CustomizeRims.SetActive(this.menuState == MenuState.CustomizeRims);
		this.CustomizeTires.SetActive(this.menuState == MenuState.CustomizeTires);
		this.CustomizeWraps.SetActive(this.menuState == MenuState.CustomizeWraps);
		this.Drivetrain.SetActive(this.menuState == MenuState.Drivetrain);
		this.Power.SetActive(this.menuState == MenuState.Power);
		this.SwitchSuspension.SetActive(this.menuState == MenuState.SwitchSuspension);
		this.TuneSuspension.SetActive(this.menuState == MenuState.TuneSuspension);
		this.TuneWheels.SetActive(this.menuState == MenuState.TuneWheels);
		this.TestSuspension.SetActive(this.menuState == MenuState.TestSuspension);
		this.TuneGearing.SetActive(this.menuState == MenuState.TuneGearing);
		this.Dyno.SetActive(this.menuState == MenuState.Dyno);
		this.BuyingDynoRuns.SetActive(this.menuState == MenuState.BuyingDynoRuns);
		this.DynoResult.SetActive(this.menuState == MenuState.DynoResult);
		this.PlayMenu.SetActive(this.menuState == MenuState.Play);
		this.MapMenu.SetActive(this.menuState == MenuState.Map);
		this.MultiplayerGameType.SetActive(this.menuState == MenuState.MultiplayerGameType);
		this.PrivateMultiplayer.SetActive(this.menuState == MenuState.PrivateMultiplayer);
		this.PINEntryScreen.SetActive(this.menuState == MenuState.EnterPIN);
		this.TrailRaceLobby.SetActive(this.menuState == MenuState.TrailRaceLobby);
		this.TrailSelectorScreen.SetActive(this.menuState == MenuState.TrailSelectorScreen);
		this.TrailRaceBetScreen.SetActive(this.menuState == MenuState.TrailRaceBetScreen);
		this.communityMapsScreen.SetActive(this.menuState == MenuState.CommunityMaps);
		//this.communityMapsButton.SetActive(GameState.GameType != GameType.CaptureTheFlag);
		this.dronesMenu.SetActive(this.menuState == MenuState.Drones);
		this.UpdateStats();
	}

	public void EquipTrailerConfirmed()
	{
		this.UnequipTrailersWithoutUnloadingCars();
		this.CurrentVehicle.equipped = true;
		this.CurrentVehicle.SaveVehicleData();
		this.UpdateSideButtons();
		this.equipTrailerWarning.SetActive(false);
		this.LoadMainMenu(true);
	}

	public void UnequipTrailers()
	{
		this.UnequipTrailersWithoutUnloadingCars();
		this.UnloadCarsFromTrailer();
	}

	private void UnequipTrailersWithoutUnloadingCars()
	{
		string @string = DataStore.GetString("VehiclesList");
		if (@string == string.Empty)
		{
			return;
		}
		SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(@string);
		string[] array = savedVehiclesList.VehicleIDs.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			string string2 = DataStore.GetString(array[i]);
			VehicleData vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string2);
			vehicleData.equippedTrailer = false;
			string value = XmlSerialization.SerializeData<VehicleData>(vehicleData);
			DataStore.SetString(array[i], value);
		}
	}

	public void LoadCarOnTrailer()
	{
		DataStore.SetString("VehicleOnTrailer", this.CurrentVehicle.VehicleID);
		this.LoadMainMenu(true);
	}

	public void UnloadCarsFromTrailer()
	{
		DataStore.SetString("VehicleOnTrailer", string.Empty);
		this.LoadMainMenu(true);
	}

	private void UpdateStats()
	{
		StatsData statsData = GameState.LoadStatsData();
		if (statsData == null)
		{
			statsData = new StatsData();
		}
		this.Gold = statsData.Gold;
		this.Money = statsData.Money;
		this.XP = statsData.XP;
		this.MoneyText.text = statsData.Money.ToString();
		this.GoldText.text = statsData.Gold.ToString();
		this.XPText.text = statsData.XP.ToString() + "XP";
		this.VehicleTypeText.text = ((!(this.CurrentVehicle != null)) ? "No truck" : this.CurrentVehicle.vehicleType.ToString());
		this.MembershipButton.SetActive(!statsData.IsMember);
		this.AlreadyMemberText.SetActive(statsData.IsMember);
	}

	private GameObject LoadVehicle(string PrefabName, Transform AtPoint, bool forDealership = false, string vehicleID = "")
	{
		string path = "Vehicles/" + PrefabName;
		if (this.SelectedArray == this.TurnKeyVehicles && this.menuState != MenuState.MainMenu && this.menuState != MenuState.StorageArea)
		{
			path = "TurnKeyVehicles/" + PrefabName;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
		gameObject.name = PrefabName;
		gameObject.transform.position = AtPoint.position;
		gameObject.transform.rotation = AtPoint.rotation;
		VehicleDataManager component = gameObject.GetComponent<VehicleDataManager>();
		bool flag = component.vehicleType == VehicleType.Trailer;
		if (component.vehicleType != VehicleType.Bike)
		{
			gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)10;
		}
		if (!flag)
		{
			SuspensionController component2 = gameObject.GetComponent<SuspensionController>();
			CarController component3 = gameObject.GetComponent<CarController>();
			TankController component4 = gameObject.GetComponent<TankController>();
			BodyPartsSwitcher component5 = gameObject.GetComponent<BodyPartsSwitcher>();
			IKDriverController component6 = gameObject.GetComponent<IKDriverController>();
			LightsController component7 = gameObject.GetComponent<LightsController>();
			EngineSoundProcessor componentInChildren = gameObject.GetComponentInChildren<EngineSoundProcessor>();
			if (this.SelectedArray != this.TurnKeyVehicles)
			{
				component2.SetStockSuspensionsValues();
			}
			if (component6 != null)
			{
				component6.ToggleDriver(false, false);
				component6.enabled = false;
			}
			component3.vehicleIsActive = false;
			if (component4 != null)
			{
				component4.Start();
				component4.enabled = false;
			}
			gameObject.GetComponent<CarEffects>().enabled = false;
			if (component7 != null)
			{
				component7.LightsOn = this.LightsOn;
			}
			if (componentInChildren != null)
			{
				componentInChildren.enabled = false;
			}
			gameObject.GetComponent<EngineController>().enabled = false;
			component3.UpdateEngineModel();
		}
		else if (!forDealership && Utility.EqiuppedTrailer() != string.Empty && Utility.EqiuppedTrailer() == vehicleID)
		{
			UnityEngine.Debug.Log("Loading vehicle on trailer");
			string @string = DataStore.GetString("VehicleOnTrailer");
			if (Utility.DoesTruckExist(@string))
			{
				string string2 = DataStore.GetString(@string);
				VehicleData vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string2);
				string path2 = "Vehicles/" + vehicleData.VehicleName;
				GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load(path2, typeof(GameObject))) as GameObject;
				gameObject2.name = vehicleData.VehicleName;
				IKDriverController component8 = gameObject2.GetComponent<IKDriverController>();
				if (component8 != null)
				{
					component8.ToggleDriver(false, false);
					component8.enabled = false;
				}
				VehicleDataManager component9 = gameObject2.GetComponent<VehicleDataManager>();
				component9.VehicleID = @string;
				component9.LoadVehicleData();
				gameObject2.GetComponent<BodyPartsSwitcher>().UpdateColor(true);
				gameObject2.GetComponent<BodyPartsSwitcher>().UpdateDirtiness();
				gameObject2.GetComponent<CarController>().UpdateEngineModel();
				gameObject2.GetComponent<CarController>().SetCalculatedCOM();
				if (gameObject2.GetComponentInChildren<EngineSoundProcessor>() != null)
				{
					gameObject2.GetComponentInChildren<EngineSoundProcessor>().enabled = false;
				}
				component9.LoadOnTrailer(gameObject.GetComponent<TrailerController>(), true);
				this.loadedVehicleOnTrailer = gameObject2;
			}
		}
		return gameObject;
	}

	private IEnumerator PreventVehicleJumpingOnLoad(GameObject vehicle)
	{
		Utility.AlignVehicleByGround(vehicle.transform, false);
		yield return null;
		yield break;
	}

	private void LoadVehiclesInGarage()
	{
		this.UnloadVehiclesInGarage();
		if (this.LoadedVehiclesInGarage != null)
		{
			this.LoadedVehiclesInGarage.Clear();
		}
		else
		{
			this.LoadedVehiclesInGarage = new List<VehicleDataManager>();
		}
		string[] savedVehiclesIDs = this.GetSavedVehiclesIDs();
		if (savedVehiclesIDs == null)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		while (num < this.GarageVehiclePoints.Length && num2 < savedVehiclesIDs.Length)
		{
			if (!(DataStore.GetString("VehicleOnTrailer") == savedVehiclesIDs[num2]))
			{
				string @string = DataStore.GetString(savedVehiclesIDs[num2]);
				VehicleData vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(@string);
				GameObject gameObject = this.LoadVehicle(vehicleData.VehicleName, this.GarageVehiclePoints[num], false, savedVehiclesIDs[num2]);
				VehicleDataManager component = gameObject.GetComponent<VehicleDataManager>();
				component.VehicleID = savedVehiclesIDs[num2];
				component.LoadVehicleData();
				if (component.vehicleType != VehicleType.Trailer)
				{
					base.StartCoroutine(this.PreventVehicleJumpingOnLoad(gameObject));
					gameObject.GetComponent<BodyPartsSwitcher>().UpdateColor(false);
					gameObject.GetComponent<BodyPartsSwitcher>().UpdateDirtiness();
				}
				this.LoadedVehiclesInGarage.Add(component);
				component.GaragePlaceID = num;
				num++;
			}
			num2++;
		}
	}

	private void UnloadVehiclesInGarage()
	{
		if (this.LoadedVehiclesInGarage != null)
		{
			for (int i = 0; i < this.LoadedVehiclesInGarage.Count; i++)
			{
				if (this.LoadedVehiclesInGarage[i].gameObject != null)
				{
					UnityEngine.Object.Destroy(this.LoadedVehiclesInGarage[i].gameObject);
				}
			}
		}
		this.LoadedVehiclesInGarage.Clear();
		if (this.loadedVehicleOnTrailer != null)
		{
			UnityEngine.Object.Destroy(this.loadedVehicleOnTrailer);
		}
		if (this.LoadedVehicleInSelector != null)
		{
			UnityEngine.Object.Destroy(this.LoadedVehicleInSelector);
		}
		if (this.LoadedVehicleInCustomization != null)
		{
			UnityEngine.Object.Destroy(this.LoadedVehicleInCustomization);
		}
		if (this.loadedDrone != null)
		{
			UnityEngine.Object.Destroy(this.loadedDrone);
		}
	}

	public void ChangeCurrentVehicle(VehicleDataManager vehicle, bool InstantCameraMove)
	{
		if (this.menuState != MenuState.MainMenu || (this.blockRaycasts && !this.MessageBox.activeSelf))
		{
			return;
		}
		UnityEngine.Debug.Log("Changing current vehicle2");
		if (this.CurrentVehicle == vehicle)
		{
			return;
		}
		UnityEngine.Debug.Log("Changing current vehicle3");
		this.LoadCurrentComponents(vehicle.gameObject);
		this.SelectedVehicleInGarageID = vehicle.GaragePlaceID;
		this.SetCameraTarget(this.GarageVehiclePoints[this.SelectedVehicleInGarageID].position, InstantCameraMove);
		GameState.SelectedGarageVehicleID = this.SelectedVehicleInGarageID;
		this.UpdateCameraSettings(this.CurrentVehicle.gameObject);
		this.UpdateSideButtons();
		this.VehicleTypeText.text = ((!(this.CurrentVehicle != null)) ? "No truck" : this.CurrentVehicle.vehicleType.ToString());
	}

	public void ShowStorage()
	{
		if (this.blockRaycasts)
		{
			return;
		}
		string[] savedVehiclesIDs = this.GetSavedVehiclesIDs();
		if (savedVehiclesIDs.Length > this.GarageVehiclePoints.Length)
		{
			this.LoadStorageArea(true);
		}
		else
		{
			this.ShowMessage("You don't have any vehicles in storage!", true);
		}
	}

	public void MoveOutOfStorage()
	{
		VehicleDataManager component = this.LoadedVehicleInSelector.GetComponent<VehicleDataManager>();
		this.AddVehicleToSavedVehiclesList(component, true);
		this.SaveVehicleData(component);
		UnityEngine.Object.Destroy(this.LoadedVehicleInSelector);
		if (this.loadedVehicleOnTrailer != null)
		{
			UnityEngine.Object.Destroy(this.loadedVehicleOnTrailer);
		}
		this.LoadMenu(MenuState.MainMenu, true, false);
		this.SelectedVehicleInGarageID = 0;
		GameState.SelectedGarageVehicleID = this.SelectedVehicleInGarageID;
	}

	private void UpdateDrone()
	{
		if (this.selectedDroneInSelectorID >= this.drones.Length)
		{
			this.selectedDroneInSelectorID = 0;
		}
		if (this.selectedDroneInSelectorID < 0)
		{
			this.selectedDroneInSelectorID = this.drones.Length - 1;
		}
		this.prevDroneButton.SetActive(this.selectedDroneInSelectorID > 0);
		this.nextDroneButton.SetActive(this.selectedDroneInSelectorID < this.drones.Length - 1);
		if (this.loadedDrone != null)
		{
			UnityEngine.Object.DestroyImmediate(this.loadedDrone);
		}
		GameObject original = this.drones[this.selectedDroneInSelectorID];
		this.loadedDrone = UnityEngine.Object.Instantiate<GameObject>(original, this.TruckSelectorSpawnPoint.position + Vector3.up * 0.5f, Quaternion.identity);
		this.loadedDrone.GetComponent<DroneController>().SetInactive();
		int @int = DataStore.GetInt("SelectedDrone");
		this.droneSelectedText.SetActive(@int == this.selectedDroneInSelectorID);
		this.selectDroneButton.SetActive(!this.droneSelectedText.activeSelf);
	}

	public void SelectDrone()
	{
		DataStore.SetInt("SelectedDrone", this.selectedDroneInSelectorID);
		this.UpdateDrone();
	}

	private void UpdateVehicleSelector(bool isStorage = false)
	{
		if (!isStorage)
		{
			if (this.SelectedTruckIDInSelector < 0)
			{
				this.SelectedTruckIDInSelector = this.SelectedArray.Length - 1;
			}
			if (this.SelectedTruckIDInSelector > this.SelectedArray.Length - 1)
			{
				this.SelectedTruckIDInSelector = 0;
			}
		}
		else
		{
			if (this.SelectedTruckIDInSelector < 0)
			{
				this.SelectedTruckIDInSelector = this.StoredVehicles.Count - 1;
			}
			if (this.SelectedTruckIDInSelector > this.StoredVehicles.Count - 1)
			{
				this.SelectedTruckIDInSelector = 0;
			}
		}
		if (this.LoadedVehicleInCustomization != null)
		{
			UnityEngine.Object.Destroy(this.LoadedVehicleInCustomization);
		}
		if (this.LoadedVehicleInSelector != null)
		{
			UnityEngine.Object.Destroy(this.LoadedVehicleInSelector);
		}
		if (this.loadedVehicleOnTrailer != null)
		{
			UnityEngine.Object.Destroy(this.loadedVehicleOnTrailer);
		}
		if (this.loadedDrone != null)
		{
			UnityEngine.Object.Destroy(this.loadedDrone);
		}
		if (!isStorage)
		{
			bool flag = this.SelectedArray == this.trailers;
			this.LoadedVehicleInSelector = this.LoadVehicle(this.SelectedArray[this.SelectedTruckIDInSelector].name, this.TruckSelectorSpawnPoint, true, string.Empty);
			if (!flag)
			{
				base.StartCoroutine(this.PreventVehicleJumpingOnLoad(this.LoadedVehicleInSelector));
				if (this.SelectedArray != this.TurnKeyVehicles)
				{
					this.LoadedVehicleInSelector.GetComponent<BodyPartsSwitcher>().SetStockModification();
				}
				this.LoadedVehicleInSelector.GetComponent<BodyPartsSwitcher>().WashVehicle();
				this.LoadedVehicleInSelector.GetComponent<BodyPartsSwitcher>().UpdateColor(false);
			}
			VehicleDataManager component = this.LoadedVehicleInSelector.GetComponent<VehicleDataManager>();
			this.TruckPriceMoney.text = component.MoneyPrice.ToString();
			this.TruckPriceGold.text = component.GoldPrice.ToString();
			this.TruckPriceCash.text = "$" + component.CashPrice.ToString();
			if (component.IsBoughtWithads)
			{
				this.BuyForGoldButton.SetActive(false);
				this.BuyForMoneyButton.SetActive(true);
				this.BuyForAdsButton.SetActive(true);
				this.BuyForCashButton.SetActive(false);
				this.MembersOnlyPanel.SetActive(false);
				this.PremiumPanel.SetActive(false);
				this.ExclusivePanel.SetActive(false);
				this.MembersAndEveryoneElseAfterDatePanel.SetActive(false);
				GetAdData(component);
			}
			else if (component.IsAvailable)
			{
				this.BuyForGoldButton.SetActive(true);
				this.BuyForMoneyButton.SetActive(true);
				this.BuyForCashButton.SetActive(false);
				this.BuyForAdsButton.SetActive(false);
				this.MembersOnlyPanel.SetActive(false);
				this.PremiumPanel.SetActive(false);
				this.ExclusivePanel.SetActive(false);
				this.MembersAndEveryoneElseAfterDatePanel.SetActive(false);
			}
			else if (component.vehicleAvailability == Availability.MembersAndEveryoneAfterDate)
			{
				this.BuyForGoldButton.SetActive(false);
				this.BuyForMoneyButton.SetActive(false);
				this.BuyForCashButton.SetActive(true);
                this.BuyForAdsButton.SetActive(false);
				this.MembersOnlyPanel.SetActive(false);
				this.ExclusivePanel.SetActive(false);
				this.MembersAndEveryoneElseAfterDatePanel.SetActive(true);
				this.PremiumPanel.SetActive(false);
				int days = component.TimeLeft.Days;
				int hours = component.TimeLeft.Hours;
				int minutes = component.TimeLeft.Minutes;
				int seconds = component.TimeLeft.Seconds;
				this.DaysLeftText.text = string.Concat(new object[]
				{
					days,
					" days ",
					hours,
					" hours "
				});
				if (days == 0 && hours == 0)
				{
					this.DaysLeftText.text = string.Concat(new object[]
					{
						minutes,
						" min ",
						seconds,
						" sec "
					});
				}
			}
			else if (component.vehicleAvailability == Availability.MembersOnly)
			{
				this.BuyForGoldButton.SetActive(false);
				this.BuyForMoneyButton.SetActive(false);
				this.BuyForAdsButton.SetActive(false);
				this.BuyForCashButton.SetActive(false);
				this.MembersOnlyPanel.SetActive(true);
				this.ExclusivePanel.SetActive(true);
				this.PremiumPanel.SetActive(false);
				this.MembersAndEveryoneElseAfterDatePanel.SetActive(false);
			}
			
			if (this.SelectedArray == this.TurnKeyVehicles)
			{
				this.BuyForGoldButton.SetActive(false);
				this.BuyForMoneyButton.SetActive(false);
				this.BuyForCashButton.SetActive(true);
				this.PremiumPanel.SetActive(true);
			}
			this.UpdateCameraSettings(this.LoadedVehicleInSelector);
		}
		else
		{
			this.LoadedVehicleInSelector = this.LoadVehicle(this.StoredVehicles[this.SelectedTruckIDInSelector].VehicleName, this.TruckSelectorSpawnPoint, false, this.StoredVehicles[this.SelectedTruckIDInSelector].SavedID);
			VehicleDataManager component2 = this.LoadedVehicleInSelector.GetComponent<VehicleDataManager>();
			component2.VehicleID = this.StoredVehicles[this.SelectedTruckIDInSelector].SavedID;
			component2.LoadVehicleData();
			if (component2.vehicleType != VehicleType.Trailer)
			{
				base.StartCoroutine(this.PreventVehicleJumpingOnLoad(this.LoadedVehicleInSelector));
				this.LoadedVehicleInSelector.GetComponent<BodyPartsSwitcher>().UpdateColor(true);
				this.LoadedVehicleInSelector.GetComponent<BodyPartsSwitcher>().UpdateDirtiness();
			}
			this.UpdateCameraSettings(this.LoadedVehicleInSelector);
		}
	}

	private void SetAdData(VehicleDataManager component)
	{
		SaveAdData.Instance.SetKey(component.gameObject.name, component.adCountToWatch);
		var temp = SaveAdData.Instance.GetValue(component.gameObject.name);
		this.TruckPriceAds.text = temp + " / " + component.adCountToWatch;
	}

	private void GetAdData(VehicleDataManager component)
	{
		var temp = SaveAdData.Instance.GetValue(component.gameObject.name);
		this.TruckPriceAds.text = temp + " / " + component.adCountToWatch;
	}

	private void BuyVehicleForRealCash()
	{
		this.ShowMessage("Waiting for response from store...", false);
		this.storeCallbackTimerCounting = true;
		this.storeCallbackTimer = 0f;
		if (this.SelectedArray == this.TurnKeyVehicles)
		{
			this.PurchaseProduct("com.carcrash.carcrashinggames.premiumvehiclepurchase");
		}
		else
		{
			this.PurchaseProduct("com.carcrash.carcrashinggames.timedvehiclepurchase");
		}
	}

	public void StopStoreCallbackTimer()
	{
		this.storeCallbackTimerCounting = false;
	}

	public void BuyVehicle(global::Currency currency, bool iapPurchase = false)
	{
		bool flag = this.SelectedArray == this.TurnKeyVehicles;
		bool flag2 = this.SelectedArray == this.trailers;
		VehicleDataManager component = this.LoadedVehicleInSelector.GetComponent<VehicleDataManager>();
		int num = (currency != global::Currency.Money) ? component.GoldPrice : component.MoneyPrice;
		UnityEngine.Debug.Log("Buying: " + currency);
		UnityEngine.Debug.Log("Price: " + num);
		if (this.ProcessPurchase(currency, num) || iapPurchase)
		{
			if (flag)
			{
				BodyPartsSwitcher component2 = this.LoadedVehicleInSelector.GetComponent<BodyPartsSwitcher>();
				SuspensionController component3 = this.LoadedVehicleInSelector.GetComponent<SuspensionController>();
				foreach (PartGroup partGroup in component2.partGroups)
				{
					if (partGroup.Parts[partGroup.InstalledPart] != null && !component.PurchasedPartsList.Contains(partGroup.Parts[partGroup.InstalledPart].name))
					{
						component.PurchasedPartsList.Add(partGroup.Parts[partGroup.InstalledPart].name);
					}
				}
				int intValue = component3.FrontWheelsControls.Rim.IntValue;
				int intValue2 = component3.RearWheelsControls.Rim.IntValue;
				int intValue3 = component3.FrontWheelsControls.Tire.IntValue;
				int intValue4 = component3.RearWheelsControls.Tire.IntValue;
				if (!component.PurchasedPartsList.Contains("Rim" + intValue.ToString()))
				{
					component.PurchasedPartsList.Add("Rim" + intValue.ToString());
				}
				if (!component.PurchasedPartsList.Contains("Rim" + intValue2.ToString()))
				{
					component.PurchasedPartsList.Add("Rim" + intValue2.ToString());
				}
				if (!component.PurchasedPartsList.Contains("Tire" + intValue3.ToString()))
				{
					component.PurchasedPartsList.Add("Tire" + intValue3.ToString());
				}
				if (!component.PurchasedPartsList.Contains("Tire" + intValue4.ToString()))
				{
					component.PurchasedPartsList.Add("Tire" + intValue4.ToString());
				}
				component3.CurrentFrontSuspension.UpgradeStage = 4;
				component3.CurrentRearSuspension.UpgradeStage = 4;
			}
			component.Bought = true;
			component.VehicleID = this.GenerateRandomID();
			string vehicleID = component.VehicleID;
			this.SaveVehicleData(component);
			UnityEngine.Object.Destroy(this.LoadedVehicleInSelector);
			this.LoadMenu(MenuState.MainMenu, true, false);
			this.SelectedVehicleInGarageID = 0;
			GameState.SelectedGarageVehicleID = this.SelectedVehicleInGarageID;
		}
		else if (currency == global::Currency.Money && Utility.CashToGold(num) <= GameState.LoadStatsData().Gold)
		{
			this.BuyVehicle(global::Currency.Gold, false);
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more money!", true);
			this.LoadIAPMenu();
		}
	}

	public void SellVehicle()
	{
		if (this.CurrentVehicle == null)
		{
			return;
		}
		if (FieldFind.FieldFindNames.Contains(this.CurrentVehicle.name))
		{
			this.ShowMessage("You can't sell this!", true);
			return;
		}
		float num = (float)this.CurrentVehicle.MoneyPrice;
		if (this.CurrentVehicle.vehicleType != VehicleType.Trailer)
		{
			foreach (PartGroup partGroup in this.CurrentPartsSwitcher.partGroups)
			{
				foreach (GameObject gameObject in partGroup.Parts)
				{
					if (gameObject != null && this.CurrentVehicle.PurchasedPartsList.Contains(gameObject.name))
					{
						num += (float)VehicleParts.GetPart(this.CurrentVehicle.vehicleType, partGroup.partType, gameObject.name).partCost;
					}
				}
			}
			if (this.CurrentPartsSwitcher.GlossyPaintPurchased)
			{
				num += 10000f;
			}
			SuspensionControlLimit limit = SuspensionControlLimits.getLimit((this.currentSide != Side.Front) ? this.CurrentSuspensionController.CurrentRearSuspension.gameObject.name : this.CurrentSuspensionController.CurrentFrontSuspension.gameObject.name, "Rim");
			if (limit != null)
			{
				for (int k = 0; k < limit.iMax; k++)
				{
					string text = "Rim" + k;
					BodyPart part = VehicleParts.GetPart(this.CurrentVehicle.vehicleType, PartType.Wheel, text);
					bool flag = this.CurrentVehicle.PurchasedPartsList.Contains(text);
					if (flag)
					{
						num += (float)part.partCost;
					}
				}
			}
			SuspensionControlLimit limit2 = SuspensionControlLimits.getLimit((this.currentSide != Side.Front) ? this.CurrentSuspensionController.CurrentRearSuspension.gameObject.name : this.CurrentSuspensionController.CurrentFrontSuspension.gameObject.name, "Tire");
			if (limit2 != null)
			{
				for (int l = 0; l < limit2.iMax; l++)
				{
					string text2 = "Tire" + l;
					BodyPart part2 = VehicleParts.GetPart(this.CurrentVehicle.vehicleType, PartType.Wheel, text2);
					bool flag2 = this.CurrentVehicle.PurchasedPartsList.Contains(text2);
					if (flag2)
					{
						num += (float)part2.partCost;
					}
				}
			}
			for (int m = 0; m < 5; m++)
			{
				PowerPart part3 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.EngineBlock, m);
				bool flag3 = this.CurrentCarController.EngineBlockStage >= m;
				if (flag3)
				{
					num += (float)part3.partCost;
				}
			}
			for (int n = 0; n < 5; n++)
			{
				PowerPart part4 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Head, n);
				bool flag4 = this.CurrentCarController.HeadStage >= n;
				if (flag4)
				{
					num += (float)part4.partCost;
				}
			}
			for (int num2 = 0; num2 < 5; num2++)
			{
				PowerPart part5 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Grip, num2);
				bool flag5 = this.CurrentCarController.GripStage >= num2;
				if (flag5)
				{
					num += (float)part5.partCost;
				}
			}
			for (int num3 = 0; num3 < 5; num3++)
			{
				PowerPart part6 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Valvetrain, num3);
				bool flag6 = this.CurrentCarController.ValvetrainStage >= num3;
				if (flag6)
				{
					num += (float)part6.partCost;
				}
			}
			for (int num4 = 0; num4 < 5; num4++)
			{
				PowerPart part7 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Weight, num4);
				bool flag7 = this.CurrentCarController.WeightStage >= num4;
				if (flag7)
				{
					num += (float)part7.partCost;
				}
			}
			for (int num5 = 0; num5 < 5; num5++)
			{
				PowerPart part8 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Durability, num5);
				bool flag8 = this.CurrentCarController.DurabilityStage >= num5;
				if (flag8)
				{
					num += (float)part8.partCost;
				}
			}
			for (int num6 = 0; num6 < 5; num6++)
			{
				PowerPart part9 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Gearing, num6);
				bool flag9 = this.CurrentCarController.GearingStage >= num6;
				if (flag9)
				{
					num += (float)part9.partCost;
				}
			}
			for (int num7 = 0; num7 < 5; num7++)
			{
				PowerPart part10 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Turbo, num7);
				bool flag10 = this.CurrentCarController.PurchasedTurboStage >= num7;
				if (flag10)
				{
					num += (float)part10.partCost;
				}
			}
			for (int num8 = 0; num8 < 5; num8++)
			{
				PowerPart part11 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Blower, num8);
				bool flag11 = this.CurrentCarController.PurchasedBlowerStage >= num8;
				if (flag11)
				{
					num += (float)part11.partCost;
				}
			}
			PowerPart part12 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Gearbox, 1);
			if (this.CurrentCarController.ManualTransmissionPurchased)
			{
				num += (float)part12.partCost;
			}
			PowerPart part13 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.TankTracks, 1);
			if (this.CurrentCarController.TankTracksPurchased)
			{
				num += (float)part13.partCost;
			}
			PowerPart part14 = PowerParts.GetPart(this.CurrentVehicle.vehicleType, PowerPartType.Ebrake, 1);
			if (this.CurrentCarController.Ebrake == 1)
			{
				num += (float)part14.partCost;
			}
			foreach (Suspension suspension in this.CurrentSuspensionController.FrontSuspensions)
			{
				if (this.CurrentVehicle.PurchasedPartsList.Contains(suspension.name))
				{
					SuspensionPart suspension2 = Suspensions.GetSuspension(this.CurrentVehicle.vehicleType, suspension.name);
					num += (float)suspension2.partCost;
					for (int num9 = 0; num9 < 5; num9++)
					{
						SuspensionUpgrade suspensionUpgrade = Suspensions.GetSuspensionUpgrade(suspension.name, num9);
						if (suspension.UpgradeStage >= num9)
						{
							num += (float)suspensionUpgrade.upgradeCost;
						}
					}
				}
			}
			for (int num10 = 0; num10 < 5; num10++)
			{
				WheelsUpgrade wheelsUpgrade = Suspensions.GetWheelsUpgrade(num10);
				if (this.CurrentSuspensionController.FrontWheelsControls.Stage >= num10)
				{
					num += (float)wheelsUpgrade.upgradeCost;
				}
				if (this.CurrentSuspensionController.RearWheelsControls.Stage >= num10)
				{
					num += (float)wheelsUpgrade.upgradeCost;
				}
			}
		}
		num /= 2f;
		this.sellingTruckCost = (float)((int)num);
		this.truckSellText.text = "Do you really want to sell this vehicle for $" + this.sellingTruckCost + "? This action can't be undone!";
		this.truckSellWindow.SetActive(true);
	}

	public void ConfirmSellingTruck()
	{
		if (this.CurrentVehicle.vehicleType == VehicleType.Trailer && Utility.EqiuppedTrailer() == this.CurrentVehicle.VehicleID)
		{
			this.UnloadCarsFromTrailer();
			this.UnequipTrailers();
		}
		DataStore.DeleteKey(this.CurrentVehicle.VehicleID);
		if (DataStore.HasKey("VehiclesList"))
		{
			string text = DataStore.GetString("VehiclesList");
			SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(text);
			if (savedVehiclesList.VehicleIDs.Contains(this.CurrentVehicle.VehicleID))
			{
				savedVehiclesList.VehicleIDs.Remove(this.CurrentVehicle.VehicleID);
			}
			text = XmlSerialization.SerializeData<SavedVehiclesList>(savedVehiclesList);
			DataStore.SetString("VehiclesList", text);
		}
		this.truckSellWindow.SetActive(false);
		GameState.AddCurrency((int)this.sellingTruckCost, global::Currency.Money);
		this.LoadMainMenu(true);
	}

	public void DeclineSellingTruck()
	{
		this.truckSellWindow.SetActive(false);
	}

	private void OnFacebookInitialized()
	{
		Debug.Log("Facebook ready...");
		if (AccessToken.CurrentAccessToken != null)
		{
			ApplySettings();
			DataStore.SetBool("LinkedFB", value: true);
			Debug.Log("User is logged in with Facebook.. logging into PlayFab with that id");
			LoginWithFacebookRequest loginWithFacebookRequest = new LoginWithFacebookRequest();
			loginWithFacebookRequest.CreateAccount = true;
			loginWithFacebookRequest.AccessToken = AccessToken.CurrentAccessToken.TokenString;
			//PlayFabClientAPI.LoginWithFacebook(loginWithFacebookRequest, OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
			FB.API("/me?fields=name", HttpMethod.GET, GotFBName);
		}
		else
		{
			Debug.Log("User is not logged in with Facebook.. logging in with playfab");
			if (PlayFabLogin.Instance != null)
			{
				PlayFabLogin.Instance.Login();
			}
			if (DataStore.GetBool("LinkedFB"))
			{
				FacebookLoginWarning.SetActive(value: true);
			}
		}
	}

	public void LoginWithFaceBook()
	{
		UnityEngine.Debug.Log("Trying to login with FB");
		this.FacebookLoginWarning.SetActive(false);
		if (!FB.IsLoggedIn)
		{
			this.ShowWaiting("Logging in...");
			FB.LogInWithReadPermissions(null, new FacebookDelegate<ILoginResult>(this.OnFacebookLoggedIn));
		}
		else
		{
			this.ShowMessage("You're already logged in.", true);
		}
	}

	public void CloseFacebookLoginWarning()
	{
		DataStore.SetBool("LinkedFB", false);
		this.FacebookLoginWarning.SetActive(false);
	}

	public void LikeUs()
	{
		Application.OpenURL("http://facebook.com/OffroadOutlawsGame");
	}

	private void OnFacebookLoggedIn(ILoginResult result)
	{
		if (result == null || string.IsNullOrEmpty(result.Error))
		{
			UnityEngine.Debug.Log("Facebook Auth Complete! Logging into/Linking PlayFab w/FB...");
			if (result == null || result.AccessToken == null || result.AccessToken.TokenString == null)
			{
				UnityEngine.Debug.Log("No access token!");
				this.HideWaiting();
				this.ShowMessage("Couldn't link your Facebook account.", true);
				return;
			}
			//FB.API("/me?fields=name", HttpMethod.GET, new FacebookDelegate<IGraphResult>(this.GotFBName), null);
			/*PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest
			{
				CreateAccount = new bool?(true),
				AccessToken = AccessToken.CurrentAccessToken.TokenString
			}, new Action<LoginResult>(this.OnPlayfabFacebookAuthCompleteGetCloud), new Action<PlayFabError>(this.OnPlayfabFacebookAuthFailed), null, null);
		*/
		}
		else
		{
			this.ShowMessage("Facebook Login Failed: " + result.Error + "\n", true);
			this.HideWaiting();
		}
	}

	private void GotFBName(IGraphResult result)
	{
		if (result != null && result.ResultDictionary != null && result.ResultDictionary.ContainsKey("name") && result.ResultDictionary["name"] != null)
		{
			string text = result.ResultDictionary["name"].ToString();
			UnityEngine.Debug.Log("Facebook Name: " + text);
			GameState.PlayerName = text;
			PhotonNetwork.playerName = text;
			DataStore.SetBool("UseFBName", true);
		}
		this.ApplySettings();
	}

	public void ShowImportCloudDataBox(int localGold, int cloudGold, int localMoney, int cloudMoney, int localVehicles, int cloudVehicles)
	{
		this.LocalGoldLabel.text = "Gold: " + localGold.ToString();
		this.LocalMoneyLabel.text = "Money: " + localMoney.ToString();
		this.LocalVehiclesLabel.text = "Vehicles: " + localVehicles.ToString();
		this.CloudGoldLabel.text = "Gold: " + cloudGold.ToString();
		this.CloudMoneyLabel.text = "Money: " + cloudMoney.ToString();
		this.CloudVehiclesLabel.text = "Vehicles: " + cloudVehicles.ToString();
		this.YesNoCloudDataBox.SetActive(true);
	}

	public void UseCloudData()
	{
		this.ShowWaiting("Downloading cloud data...");
		this.YesNoCloudDataBox.SetActive(false);
		DataStore.ImportCloudData();
	}

	public void UseLocalData()
	{
		DataStore.disableCloudSave = false;
		this.YesNoCloudDataBox.SetActive(false);
		this.HideWaiting();
	}

	/*private void OnPlayfabFacebookAuthCompleteGetCloud(LoginResult result)
	{
		PlayFabClientAPI.LinkAndroidDeviceID(new LinkAndroidDeviceIDRequest
		{
			AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
			ForceLink = new bool?(true)
		}, delegate(LinkAndroidDeviceIDResult linkResult)
		{
			this.HideWaiting();
			DataStore.disableCloudSave = true;
			DataStore.SetBool("LinkedFB", true);
			DataStore.DownloadCloudData();
		}, delegate(PlayFabError error)
		{
			UnityEngine.Debug.Log(error.ErrorMessage);
			this.ShowMessage("Couldn't link your Facebook account.", true);
			this.HideWaiting();
		}, null, null);
		UnityEngine.Debug.Log("PlayFab Facebook Auth Complete. Session ticket: " + result.SessionTicket);
	}*/

	/*private void OnPlayfabFacebookAuthComplete(LoginResult result)
	{
		PlayFabClientAPI.LinkAndroidDeviceID(new LinkAndroidDeviceIDRequest
		{
			AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
			ForceLink = new bool?(true)
		}, delegate(LinkAndroidDeviceIDResult linkResult)
		{
			this.HideWaiting();
			DataStore.disableCloudSave = true;
			DataStore.SetBool("LinkedFB", true);
		}, delegate(PlayFabError error)
		{
			UnityEngine.Debug.Log(error.ErrorMessage);
			this.ShowMessage("Couldn't link your Facebook account.", true);
			this.HideWaiting();
		}, null, null);
		UnityEngine.Debug.Log("PlayFab Facebook Auth Complete. Session ticket: " + result.SessionTicket);
	}
	*/

	private void OnPlayfabFacebookAuthFailed(PlayFabError error)
	{
		UnityEngine.Debug.Log("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport());
	}

	public void CloudRestoreComplete(bool reloadScene = true)
	{
		this.FacebookLoginWarning.SetActive(false);
		this.MessageBox.SetActive(false);
		this.HideWaiting();
		if (reloadScene)
		{
			SceneManager.LoadScene("Menu");
		}
	}

	private void SaveVehicleData(VehicleDataManager vehicleDataManager)
	{
		this.AddVehicleToSavedVehiclesList(vehicleDataManager, false);
		vehicleDataManager.SaveVehicleData();
	}

	public void RemoveAllSaveData()
	{
		DataStore.Clear();
	}

	private void AddVehicleToSavedVehiclesList(VehicleDataManager vehicleDataManager, bool forceToFront = false)
	{
		if (DataStore.HasKey("VehiclesList"))
		{
			string @string = DataStore.GetString("VehiclesList");
			SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(@string);
			if (!savedVehiclesList.VehicleIDs.Contains(vehicleDataManager.VehicleID))
			{
				savedVehiclesList.VehicleIDs.Insert(0, vehicleDataManager.VehicleID);
			}
			else if (forceToFront)
			{
				savedVehiclesList.VehicleIDs.Remove(vehicleDataManager.VehicleID);
				savedVehiclesList.VehicleIDs.Insert(0, vehicleDataManager.VehicleID);
			}
			@string = XmlSerialization.SerializeData<SavedVehiclesList>(savedVehiclesList);
			DataStore.SetString("VehiclesList", @string);
		}
		else
		{
			SavedVehiclesList savedVehiclesList2 = new SavedVehiclesList();
			savedVehiclesList2.VehicleIDs = new List<string>();
			savedVehiclesList2.VehicleIDs.Add(vehicleDataManager.VehicleID);
			string value = XmlSerialization.SerializeData<SavedVehiclesList>(savedVehiclesList2);
			DataStore.SetString("VehiclesList", value);
		}
	}


	public void DynoFinished(float maxHP, float avgHP, float maxTQ, float avgTQ)
	{
		this.Drivetrain_DynoResult();
		this.MaxHPText.text = ((int)maxHP).ToString();
		this.AvgHPText.text = ((int)avgHP).ToString();
		this.MaxTQText.text = ((int)maxTQ).ToString();
		this.AvgTQText.text = ((int)avgTQ).ToString();
	}

	public void BuyTuningPack()
	{
		int amount = 100;
		if (this.ProcessPurchase(global::Currency.Gold, amount))
		{
			this.CurrentCarController.TuningEnginePurchased = true;
			this.SaveVehicle();
			this.BuyTuningPackButton.interactable = false;
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more!", true);
			this.LoadIAPMenu();
		}
	}

	public void BuyPerfectSetup()
	{
		int amount = 100;
		if (this.ProcessPurchase(global::Currency.Gold, amount))
		{
			this.CurrentCarController.PerfectSetupPurchased = true;
			this.SetPerfectSetup();
			this.SaveVehicle();
			this.BuyPerfectSetupButton.interactable = false;
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more!", true);
			this.LoadIAPMenu();
		}
	}

	public void SetPerfectSetup()
	{
		this.CurrentCarController.FuelRatio = this.CurrentCarController.PerfectFuelRatio;
		this.CurrentCarController.TimingRatio = this.CurrentCarController.PerfectTimingRatio;
		this.EngineTuningSlider.gameObject.SetActive(false);
	}

	public void BuyDynoRuns(int amount)
	{
		int amount2 = 100;
		if (amount == 50)
		{
			amount2 = 200;
		}
		if (amount == 100)
		{
			amount2 = 300;
		}
		if (this.ProcessPurchase(global::Currency.Gold, amount2))
		{
			StatsData statsData = GameState.LoadStatsData();
			statsData.DynoRuns += amount;
			GameState.SaveStatsData(statsData);
			this.Drivetrain_DynoTest();
		}
		else
		{
			this.ShowMessage("You don't have enough. To continue, you can purchase gold, or do more races to earn more!", true);
			this.LoadIAPMenu();
		}
	}

	public void RunDynoTest()
	{
		if (GameState.LoadStatsData().DynoRuns <= 0)
		{
			this.Drivetrain_BuyDynoRuns();
			return;
		}
		this.Dyno.SetActive(false);
		DynoRoomController.Instance.StartDyno();
		StatsData statsData = GameState.LoadStatsData();
		statsData.DynoRuns--;
		GameState.SaveStatsData(statsData);
	}

	public void SelectEngineTuningItem(int ID)
	{
		if (this.CurrentCarController.PerfectSetupPurchased)
		{
			this.ShowMessage("You already have perfect setup, no need to tune anymore!", true);
			return;
		}
		if (!this.CurrentCarController.TuningEnginePurchased)
		{
			this.ShowMessage("Purchase Tuning pack first!", true);
			return;
		}
		this.EngineTuningSlider.gameObject.SetActive(true);
		this.selectedEngineTuningItem = (MenuManager.EngineTuningItem)ID;
		string valueName = string.Empty;
		float currentValue = 0f;
		MenuManager.EngineTuningItem engineTuningItem = this.selectedEngineTuningItem;
		if (engineTuningItem != MenuManager.EngineTuningItem.FuelRatio)
		{
			if (engineTuningItem == MenuManager.EngineTuningItem.TimingRatio)
			{
				valueName = "Timing ratio";
				currentValue = this.CurrentCarController.TimingRatio;
			}
		}
		else
		{
			valueName = "Fuel ratio";
			currentValue = this.CurrentCarController.FuelRatio;
		}
		this.EngineTuningSlider.SetupFloatValue(valueName, -10f, 10f, -10f, 10f, currentValue);
	}

	public void EngineTuningChanged()
	{
		MenuManager.EngineTuningItem engineTuningItem = this.selectedEngineTuningItem;
		if (engineTuningItem != MenuManager.EngineTuningItem.FuelRatio)
		{
			if (engineTuningItem == MenuManager.EngineTuningItem.TimingRatio)
			{
				this.CurrentCarController.TimingRatio = this.EngineTuningSlider.slider.value;
			}
		}
		else
		{
			this.CurrentCarController.FuelRatio = this.EngineTuningSlider.slider.value;
		}
	}

	private bool ProcessPurchase(global::Currency currency, int amount)
	{
		if (currency == Currency.Ads) return true;
		
		StatsData statsData = GameState.LoadStatsData();
		bool flag = false;
		if (currency != global::Currency.Gold)
		{
			if (currency == global::Currency.Money)
			{
				flag = (statsData.Money >= amount);
				if (flag)
				{
					GameState.SubtractCurrency(amount, currency);
				}
			}
		}
		else
		{
			flag = (statsData.Gold >= amount);
			if (flag)
			{
				GameState.SubtractCurrency(amount, currency);
			}
		}
		this.UpdateScreen();
		return flag;
	}

	public string[] GetSavedVehiclesIDs()
	{
		string @string = DataStore.GetString("VehiclesList");
		if (@string == string.Empty)
		{
			return null;
		}
		SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(@string);
		return savedVehiclesList.VehicleIDs.ToArray();
	}

	private string[] GetStorageVehiclesIDs()
	{
		string @string = DataStore.GetString("VehiclesList");
		if (@string == string.Empty)
		{
			return null;
		}
		SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(@string);
		int i = 0;
		int num = 0;
		while (i < this.GarageVehiclePoints.Length)
		{
			savedVehiclesList.VehicleIDs.Remove(savedVehiclesList.VehicleIDs[0]);
			if (DataStore.GetString("VehicleOnTrailer") != savedVehiclesList.VehicleIDs[0])
			{
				i++;
			}
			num++;
		}
		return savedVehiclesList.VehicleIDs.ToArray();
	}

	private string GenerateRandomID()
	{
		string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text2 = "Vehicle_";
		for (int i = 0; i < 5; i++)
		{
			text2 += text[UnityEngine.Random.Range(0, text.Length)];
		}
		return text2;
	}

	private void SetCameraTarget(Vector3 To, bool Instantly)
	{
		this.cameraTargetPos = To + Vector3.down * 0.5f;
		if (Instantly)
		{
			this.CameraTarget.position = To;
		}
	}

	private void SetCameraTargetWithoutOffset(Vector3 To, bool Instantly)
	{
		this.cameraTargetPos = To;
		if (Instantly)
		{
			this.CameraTarget.position = To;
		}
	}

	private void UpdateCameraSettings(GameObject vehicle = null)
	{
		if (vehicle != null)
		{
			CarController component = vehicle.GetComponent<CarController>();
			if (component != null)
			{
				CameraController.Instance.MaxDistance = component.GarageMaxDistance;
				CameraController.Instance.MinDistance = component.GarageMinDistance;
				CameraController.Instance.DistanceCamTarget = component.GarageMinDistance + (component.GarageMinDistance + component.GarageMaxDistance) / 2f;
			}
			else
			{
				CameraController.Instance.MaxDistance = 6f;
				CameraController.Instance.MinDistance = 3f;
			}
		}
		if (this.menuState == MenuState.MainMenu)
		{
			CameraController.Instance.GetComponent<Camera>().fieldOfView = 50f;
		}
		else
		{
			CameraController.Instance.GetComponent<Camera>().fieldOfView = 69f;
		}
	}

	public void HideMessage()
	{
		this.MessageBox.SetActive(false);
	}

	public void ShowMessage(string message, bool okButtonEnabled = true)
	{
		this.MessageBox.SetActive(true);
		this.MessageText.text = message;
		this.messageOkButton.interactable = okButtonEnabled;
	}

	public void HideBecomeMember()
	{
		this.BecomeMember.SetActive(false);
	}

	public void ShowBecomeMember()
	{
		this.BecomeMember.SetActive(true);
	}

	public void ShowFieldFindMessage()
	{
		if (this.blockRaycasts)
		{
			return;
		}
		string @string = DataStore.GetString("FoundPartsFF" + DataStore.CurrentFieldFind().ToString(), string.Empty);
		string[] array = @string.Split(new char[]
		{
			','
		});
		Dictionary<CratePartType, string> dictionary = StashContent.CratePartTypeList();
		List<int> list = new List<int>();
		for (int i = 0; i <= 9; i++)
		{
			bool flag = false;
			foreach (string text in array)
			{
				if (text != null && text != string.Empty && text == i.ToString())
				{
					flag = true;
				}
			}
			if (!flag)
			{
				list.Add(i);
			}
		}
		this.FieldFindParts1.text = string.Empty;
		this.FieldFindParts2.text = string.Empty;
		for (int k = 0; k < list.Count; k++)
		{
			if (k < 5)
			{
				Text fieldFindParts = this.FieldFindParts1;
				fieldFindParts.text = fieldFindParts.text + "- " + dictionary[(CratePartType)list[k]] + "\r\n";
			}
			else
			{
				Text fieldFindParts2 = this.FieldFindParts2;
				fieldFindParts2.text = fieldFindParts2.text + "- " + dictionary[(CratePartType)list[k]] + "\r\n";
			}
		}
		this.FieldFindBox.SetActive(true);
	}

	public void BuyFieldFindParts(bool spendMoney = true)
	{
		StatsData statsData = GameState.LoadStatsData();
		if (statsData.Gold >= 500 || !spendMoney)
		{
			statsData.Gold -= 500;
			int num = DataStore.LastFoundFieldFind() - 1;
			string prefabName = FieldFind.FieldFindNames[num];
			GameObject gameObject = this.LoadVehicle(prefabName, this.TruckSelectorSpawnPoint, false, string.Empty);
			base.StartCoroutine(this.PreventVehicleJumpingOnLoad(gameObject));
			VehicleDataManager component = gameObject.GetComponent<VehicleDataManager>();
			component.Bought = true;
			component.VehicleID = this.GenerateRandomID();
			string vehicleID = component.VehicleID;
			this.SaveVehicleData(component);
			UnityEngine.Object.Destroy(this.LoadedVehicleInSelector);
			this.LoadMenu(MenuState.MainMenu, true, false);
			this.SelectedVehicleInGarageID = 0;
			GameState.SelectedGarageVehicleID = this.SelectedVehicleInGarageID;
			if (spendMoney)
			{
				GameState.SaveStatsData(statsData);
			}
			UnityEngine.Object.Destroy(gameObject);
			this.FieldFindBox.SetActive(false);
			this.StaticFieldFinds[num].SetActive(false);
		}
		else
		{
			this.FieldFindBox.SetActive(false);
			this.ShowMessage("You don't have enough gold. To continue, you can purchase gold.", true);
			this.LoadIAPMenu();
		}
	}

	public void HideFieldFindMessage()
	{
		this.FieldFindBox.SetActive(false);
	}

	private IEnumerator ShowMessageCor(string message)
	{
		if (this.MessageText.color.a > 0f)
		{
			yield break;
		}
		this.MessageBox.SetActive(true);
		this.MessageText.text = message;
		this.MessageText.color = Color.white;
		yield return new WaitForSeconds(1f);
		for (float f = 1f; f >= 0f; f -= 0.1f)
		{
			this.MessageText.color = new Color(1f, 1f, 1f, f);
			yield return null;
		}
		this.MessageText.color = Color.clear;
		yield break;
	}

	public void RequestAdForDuallyTires()
	{
		Advertisements.Instance.ShowRewardedVideo(DuallyAdsCompleted);
	}

	private void DuallyAdsCompleted()
	{	
		BuyDuallies();	
	}
	
	public void RequestAdForGlossyPaint()
	{
		Advertisements.Instance.ShowRewardedVideo(GlossyAdsCompleted);
	}

	private void GlossyAdsCompleted()
	{

		BuyGlossy();
	}
	
	public static MenuManager Instance;

	public MenuState menuState;

	private VehicleType SelectedVehicleType;

	[Header("Prices")]
	public int WashPrice;

	public int FullRepairPrice;

	[Header("Stats")]
	public int Money;

	public int Gold;

	public int XP;

	[Header("UI elements")]
	public CanvasGroup FadeScreen;

	public Text MoneyText;

	public Text GoldText;

	public Text XPText;

	public Text VehicleTypeText;

	public Text MessageText;

	public GameObject MessageBox;

	public GameObject BecomeMember;

	public Button messageOkButton;

	public GameObject FieldFindBox;

	public Text FieldFindParts1;

	public Text FieldFindParts2;

	public Text desertAdText;

	//public GameObject communityMapsButton;

	private bool storeCallbackTimerCounting;

	private float storeCallbackTimer;

	private const float storeCallbackTimeoutTime = 180f;

	public GameObject YesNoCloudDataBox;

	public GameObject FacebookLoginWarning;

	public GameObject equipTrailerWarning;

	[Header("Hosting Screen")]
	public Text CurrentPassword;

	[Header("PIN Entry Screen")]
	public Text CurrentPIN;

	[Header("Camera settings")]
	public Transform CameraTarget;

	public CameraPosition[] cameraPositions;

	private Vector3 cameraTargetPos;

	[Header("Truck selector screen")]
	public Text TruckPriceMoney;

	public Text TruckPriceGold;

	public Text TruckPriceCash;
	
	public Text TruckPriceAds;

	public GameObject BuyForGoldButton;

	public GameObject BuyForMoneyButton;

	public GameObject BuyForCashButton;
	public GameObject BuyForAdsButton;

	public GameObject MembersOnlyPanel;

	public GameObject ExclusivePanel;

	public GameObject PremiumPanel;

	public GameObject MembersAndEveryoneElseAfterDatePanel;

	public Text DaysLeftText;

	public GameObject truckSellWindow;

	public Text truckSellText;

	[Header("Trail race")]
	public Text NoRaceAvailableText;

	private RoomInfo[] TrailRaceRooms;

	private List<GameObject> InstantiatedRoomBars;

	[Space(10f)]
	public GameObject[] StreetTrucks;

	public GameObject[] ATVs;

	public GameObject[] SideBySides;

	public GameObject[] Crawlers;

	public GameObject[] TrophyTrucks;

	public GameObject[] Bikes;

	public GameObject[] TurnKeyVehicles;

	public GameObject[] trailers;

	public GameObject[] drones;

	private GameObject[] SelectedArray;

	private List<VehicleData> StoredVehicles;

	private GameObject LoadedVehicleInSelector;

	private int SelectedTruckIDInSelector;

	private GameObject loadedVehicleOnTrailer;

	[Header("Customize")]
	public Text PartCostText;

	public Text GroupNameText;

	public Text TotalModsCostText;

	public Text TotalModsCostGoldText;

	public Text WashCostText;

	public Text RimSideText;

	public Text RimCostText;

	public Text TireSideText;

	public Text TireCostText;

	public GameObject dualTiresMenu;

	public GameObject dualTiresButton;

	public GameObject buyDualTiresButton;

	public Text currentTiresTypeText;

	public Image WrapPreviewImage;

	public GameObject BodyPartColorMenu;

	public GameObject BodyPartColorBar;

	public GameObject WheelsColorBar;

	private RimColorMode rimColorMode;

	public CUIColorPicker BodyColorPicker;

	public CUIColorPicker WrapColorPicker;

	public CUIColorPicker RimColorPicker;

	public Slider XOffsetSlider;

	public Slider YOffsetSlider;

	public Slider XTillingSlider;

	public Slider YTillingSlider;

	public Slider TransparencySlider;

	private GameObject LoadedVehicleInCustomization;

	public GameObject PurchaseModsButton;

	public GameObject PurchaseModsButtonGold;

	public GameObject ModConfirmation;

	public GameObject BuyGlossyPaintButton;

	public GameObject SetGlossyPaintButton;

	public Text CurrentPaintTypeText;

	private PartGroup SelectedPartGroup;

	private int SelectedPartGroupID;

	private int SelectedPartID;

	internal WheelsControls SelectedWheelsControls { get; private set; }

	private int SelectedRimID;

	private int SelectedTireID;

	private Side currentSide;

	private int SelectedWrap;

	private Color WrapColor;

	private Vector4 WrapCoords;

	private Vector2 ColorHandlerCoords;

	private bool WrapCoordsSlidersInitialized;

	public Text WrapLayerCountText;

	public Text WrapLayerCostText;

	public GameObject WrapGoldBars;

	public Button ApplyLayerButton;

	private bool LightsOn;

	private PartGroup[] partGroupsBeforeEnteringCustomization;

	private WheelsControls FrontWheelsBeforeEnteringCustomization;

	private WheelsControls RearWheelsBeforeEnteringCustomization;

	private Color FrontRimsColorBeforeEngeringCustomizaiton;

	private Color FrontBeadlockColorBeforeEnteringCustomization;

	private Color RearRimsColorBeforeEngeringCustomizaiton;

	private Color RearBeadlockColorBeforeEnteringCustomization;

	[Header("Power")]
	public GameObject StatsPanel;

	public GameObject DescriptionPanel;

	public GameObject DieselDescriptionPanel;

	public Text DescriptionText;

	public Text UpgradeCostText;

	public Text UpgradeCostGoldText;

	public Text UpgradeCostDieselText;

	public Text StageText;

	public Image StageIcon;

	public Text TypeText;

	public Image TypeImage;

	public Text RepairCostText;

	public GameObject RepairIcon;

	public GameObject WashButton;

	public GameObject unequipTrailerButton;

	public GameObject equipTrailerButton;

	public GameObject loadCarOnTrailerButton;

	public GameObject unloadCarsFromTrailerButton;

	public Sprite EngineBlockImage;

	public Sprite HeadsImage;

	public Sprite GripImage;

	public Sprite ValvetrainImage;

	public Sprite WeightImage;

	public Sprite DieselImage;

	public Sprite TitanImage;

	public Sprite GearboxImage;

	public Sprite EbrakeImage;

	public Sprite TankTracksImage;

	public Sprite TurboImage;

	public Sprite BlowerImage;

	public Sprite Stage1Icon;

	public Sprite Stage2Icon;

	public Sprite Stage3Icon;

	public Sprite Stage4Icon;

	public Sprite Stage5Icon;

	public Image PowerBar;

	public Image GripBar;

	public Image WeightBar;

	public Image DurabilityBar;

	public Button UpgradeButton;

	public Button UpgradeButtonGold;

	public Button UpgradeButtonDiesel;

	public Button UninstallButton;

	private PowerPartType SelectedPowerPartType;

	internal PowerPartType SelectedSubPowerPartType { get; private set; }

	internal int CurrentPowerPartStage { get; private set; }

	[Header("Drivetrain")]
	public Text SuspensionSideText;

	public Text SuspensionCostText;

	public Text SuspensionCostGoldText;

	public Text SuspensionNameText;

	public Text SuspensionNameInUpgradeBarText;

	public Text SuspensionDescriptionText;

	public Text SuspensionUpgradeCostText;

	public Text SuspensionUpgradeCostGoldText;

	public Text SuspensionStageInUpgradeBarText;

	public Text GearingUpgradeCostText;

	public Text GearingStageInUpgradeBarText;

	public Text WheelsSideText;

	public Text WheelsStageText;

	public Text WheelsUpgradeCostText;

	public Text WheelsUpgradeCostGoldText;

	public Button WheelsUpgradeButton;

	public Button WheelsUpgradeButtonGold;

	public GameObject rimTuneButton;

	public GameObject tireWidthTuneButton;

	public GameObject tireRadiusTuneButton;

	public GameObject GearingTutorialWindow;

	private SuspensionValue SelectedSuspensionValue;

	public Button SuspensionUpgradeButton;

	public Button SuspensionUpgradeButtonGold;

	public Button GearingUpgradeButton;

	public Button FirstAdjustmentButton;

	public Button FirstGearButton;

	private List<Button> LoadedAdjustmentButtons;

	private int SelectedGear;

	public AdjustmentSlider SuspensionAdjustmentSlider;

	public AdjustmentSlider WheelsAdjustmentSlider;

	public AdjustmentSlider GearingAdjustmentSlider;

	private int SelectedSuspensionID;

	internal Suspension SelectedSuspension { get; private set; }

	[Header("Dyno")]
	public Button BuyTuningPackButton;

	public Button BuyPerfectSetupButton;

	public Text MaxHPText;

	public Text MaxTQText;

	public Text AvgHPText;

	public Text AvgTQText;

	public Text DynoRunsLeftText;

	public GameObject DynoTutorialWindow;

	public AdjustmentSlider EngineTuningSlider;

	public MenuManager.EngineTuningItem selectedEngineTuningItem;

	[Header("Drones")]
	public GameObject dronePurchaseMessage;

	public GameObject selectDroneButton;

	public GameObject droneSelectedText;

	public GameObject prevDroneButton;

	public GameObject nextDroneButton;

	private GameObject loadedDrone;

	private int selectedDroneInSelectorID;

	[Header("Transforms")]
	public Transform TruckSelectorSpawnPoint;

	public Transform[] GarageVehiclePoints;

	[Header("Logo")]
	public GameObject Logo;

	[Header("Menus holders")]
	public GameObject MainMenu;

	public GameObject IAPMenu;

	public GameObject TruckTypeSelector;

	public GameObject StorageArea;

	public GameObject TruckSelector;

	public GameObject CustomizeCategorySelector;

	public GameObject CustomizeBodyParts;

	public GameObject CustomizePaint;

	public GameObject CustomizeWraps;

	public GameObject CustomizeRims;

	public GameObject CustomizeTires;

	public GameObject Drivetrain;

	public GameObject SwitchSuspension;

	public GameObject TuneSuspension;

	public GameObject TuneWheels;

	public GameObject TestSuspension;

	public GameObject TuneGearing;

	public GameObject Dyno;

	public GameObject BuyingDynoRuns;

	public GameObject DynoResult;

	public GameObject Power;

	public GameObject communityMapsScreen;

	public GameObject dronesMenu;

	public GameObject loadingSettingsFileMenu;

	public GameObject PlayMenu;

	public GameObject MapMenu;

	public GameObject SceneLoading;

	public Text SceneLoadingText;

	public GameObject MultiplayerGameType;

	public GameObject PrivateMultiplayer;

	public GameObject MultiplayerPrivateButton;

	public GameObject FramerateWarning;

	public GameObject PINEntryScreen;

	public GameObject TrailRaceLobby;

	public GameObject TrailSelectorScreen;

	public GameObject TrailRaceBetScreen;

	public GameObject FirstTrailRaceElement;

	public GameObject DesertLockPanel;

	public GameObject StuntParkLockPabel;

	public GameObject RockParkLockPanel;

	public Text DesertLockXPText;

	public Text StuntParkLockXPText;

	public Text RockParkLockXPText;

	public int UnlockDesertXP;

	public int UnlockStuntParkXP;

	public int UnlockRockParkXP;

	public VehicleDataManager CurrentVehicle;

	internal BodyPartsSwitcher CurrentPartsSwitcher { get; private set; }

	internal CarController CurrentCarController { get; private set; }

	internal SuspensionController CurrentSuspensionController { get; private set; }

	public List<VehicleDataManager> LoadedVehiclesInGarage = new List<VehicleDataManager>();

	public int SelectedVehicleInGarageID;

	[Header("Side bar")]
	public RectTransform Sidebar;

	public GameObject SettingsTab;

	public GameObject DefaultTab;

	public Text GraphicsLevel;

	public Text MusicStatus;

	public Text SoundStatus;

	public Text ControlsType;

	public Text AcceleratorType;

	public Text TrailName;

	public Text TrailNameHint;

	public Text airControlPowerText;

	public Text postFxStatusText;

	private bool SideBarExpanded;

	public GameObject[] StaticFieldFinds;

	private static StoreListener storeListener;

	public GameObject MembershipButton;

	public GameObject AlreadyMemberText;

	private bool enableCloudSave = true;

	[Header("Cloud Stuff")]
	public Text LocalGoldLabel;

	public Text LocalMoneyLabel;

	public Text LocalVehiclesLabel;

	public Text CloudGoldLabel;

	public Text CloudMoneyLabel;

	public Text CloudVehiclesLabel;

	public Text TimeStamp;

	private TouchScreenKeyboard keyboard;

	private TouchScreenKeyboard devModeKeyboard;

	private float sellingTruckCost;

	private bool isTrailerEquipped;

	private Coroutine loadSettingsCor;

	private int LogoTaps;

	private string url = string.Empty;

	public enum EngineTuningItem
	{
		FuelRatio,
		TimingRatio
	}
}
