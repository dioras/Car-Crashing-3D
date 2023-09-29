using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public static class GameState
{
	public static bool devRights
	{
		get
		{
			return DataStore.GetBool("DevRights", false);
		}
		set
		{
			DataStore.SetBool("DevRights", value);
		}
	}

	public static byte MaxPlayers
	{
		get
		{
			if (GameState.GameType == GameType.TrailRace)
			{
				return 2;
			}
			return 5;
		}
	}

	public static string playerName
	{
		get
		{
			return (!DataStore.GetBool("UseFBName", false)) ? DataStore.GetString("GeneratedName") : GameState.PlayerName;
		}
	}

	public static void Populate(string vehicleId, string sceneName, GameMode gameMode, GameType gameType, string password)
	{
		GameState.Clear();
		GameState.CurrentVehicleID = vehicleId;
		GameState.GameMode = gameMode;
		GameState.GameType = gameType;
		GameState.Password = password;
		GameState.SceneName = sceneName;
		GameState.RoomName = Utility.RandomDigits(10);
	}

	public static void Clear()
	{
		GameState.CurrentVehicleID = null;
		GameState.GameMode = GameMode.None;
		GameState.GameType = GameType.None;
		GameState.Password = null;
		GameState.RoomName = null;
		GameState.SceneName = null;
		GameState.WaitingForRoom = false;
		GameState.TrailRaceBet = 0;
		GameState.TrailID = 0;
		GameState.mapToDownload = string.Empty;
	}

	public static void SaveStatsData(StatsData newData)
	{
		string value = XmlSerialization.SerializeData<StatsData>(newData);
		DataStore.SetString("Stats", value);
		DataStore.SetString("SH", Utility.MD5(Utility.MD5(Utility.MD5(newData.Dump()))));
	}

	public static StatsData LoadStatsData()
	{
		string @string = DataStore.GetString("Stats", string.Empty);
		StatsData statsData = new StatsData();
		if (@string != string.Empty && @string != null)
		{
			statsData = (StatsData)XmlSerialization.DeserializeData<StatsData>(@string);
			string string2 = DataStore.GetString("SH", string.Empty);
			string b = Utility.MD5(Utility.MD5(Utility.MD5(statsData.Dump())));
			if (string2 != b)
			{
				statsData = new StatsData();
			}
		}
		return statsData;
	}

	public static void SetMembership(bool isMember)
	{
		StatsData statsData = GameState.LoadStatsData();
		statsData.IsMember = isMember;
		try
		{
			if (PlayFabClientAPI.IsClientLoggedIn())
			{
				if (isMember)
				{
					PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest
					{
						Amount = 1,
						VirtualCurrency = "MB"
					}, delegate(ModifyUserVirtualCurrencyResult success)
					{
						UnityEngine.Debug.Log("Saved currency to cloud");
					}, delegate(PlayFabError errorCallback)
					{
						UnityEngine.Debug.Log("Could not set currency: " + errorCallback.GenerateErrorReport());
					}, null, null);
				}
				else
				{
					PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest
					{
						Amount = 0,
						VirtualCurrency = "MB"
					}, delegate(ModifyUserVirtualCurrencyResult success)
					{
						UnityEngine.Debug.Log("Saved currency to cloud");
					}, delegate(PlayFabError errorCallback)
					{
						UnityEngine.Debug.Log("Could not set currency: " + errorCallback.GenerateErrorReport());
					}, null, null);
				}
			}
		}
		catch
		{
			UnityEngine.Debug.Log("Couldn't make cloud backup!");
		}
		GameState.SaveStatsData(statsData);
	}

	public static void SetUnlimitedFuel(bool hasUnlimitedFuel)
	{
		StatsData statsData = GameState.LoadStatsData();
		statsData.HasUnlimitedFuel = hasUnlimitedFuel;
		GameState.SaveStatsData(statsData);
	}

	public static void AddXP(int amount)
	{
		StatsData statsData = GameState.LoadStatsData();
		if (statsData == null)
		{
			statsData = new StatsData();
		}
		statsData.XP += amount;
		GameState.SaveStatsData(statsData);
	}

	public static void AddCurrency(int amount, global::Currency currency)
	{
		StatsData statsData = GameState.LoadStatsData();
		if (statsData == null)
		{
			statsData = new StatsData();
		}
		if (currency == global::Currency.Money)
		{
			statsData.Money += amount;
		}
		if (currency == global::Currency.Gold)
		{
			statsData.Gold += amount;
		}
		try
		{
			if (PlayFabClientAPI.IsClientLoggedIn())
			{
				UnityEngine.Debug.Log("Client is logged in.. Backup up currency to cloud");
				PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest
				{
					Amount = amount,
					VirtualCurrency = ((currency != global::Currency.Gold) ? "CC" : "GC")
				}, delegate(ModifyUserVirtualCurrencyResult success)
				{
					UnityEngine.Debug.Log("Saved currency to cloud");
				}, delegate(PlayFabError errorCallback)
				{
					UnityEngine.Debug.Log("Could not set currency: " + errorCallback.GenerateErrorReport());
				}, null, null);
			}
		}
		catch
		{
			UnityEngine.Debug.Log("Couldn't make cloud backup!");
		}
		GameState.SaveStatsData(statsData);
	}

	public static void SubtractCurrency(int amount, global::Currency currency)
	{
		StatsData statsData = GameState.LoadStatsData();
		if (statsData == null)
		{
			statsData = new StatsData();
		}
		if (currency == global::Currency.Money)
		{
			statsData.Money -= amount;
		}
		if (currency == global::Currency.Gold)
		{
			statsData.Gold -= amount;
		}
		GameState.SaveStatsData(statsData);
	}

	public static string CurrentVehicleID = string.Empty;

	public static GameMode GameMode;

	public static GameType GameType;

	public static string RoomName;

	public static string Password = string.Empty;

	public static string SceneName;

	public static bool WaitingForRoom;

	public static bool FramerateWarning;

	public static int SelectedGarageVehicleID;

	public static bool FailedToJoin;

	public static string PlayerName;

	public static bool JustOpenedGame = true;

	public static int TrailID;

	public static int TrailRaceBet;

	public static string mapToDownload;

	public static bool customMapsAvailable;

	public static bool settingsFileLoaded;
}
