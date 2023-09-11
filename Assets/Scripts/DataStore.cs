using System;
using System.Collections.Generic;
using Assets.SimpleZip;
using Facebook.Unity;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class DataStore
{
	static DataStore()
	{
		if (DataStore.usedKeys.Count == 0)
		{
			string @string = PlayerPrefs.GetString("UsedKeys", string.Empty);
			string[] array = @string.Split(new char[]
			{
				'|'
			});
			foreach (string text in array)
			{
				if (text != string.Empty && text != null)
				{
					string[] array3 = text.Split(new char[]
					{
						'}'
					}, 4);
					if (array3.Length == 4)
					{
						DataStore.usedKeys.Add(Key.MakeKey(array3[0], long.Parse(array3[1]), array3[2] == "1", (KeyType)int.Parse(array3[3])));
					}
				}
			}
		}
		DataStore.cloudExceptions.Add("LastCloudSave");
		DataStore.lastCloudSave = DataStore.GetLong("LastCloudSave");
	}

	public static int CurrentFieldFind()
	{
		int num = DataStore.LastFoundFieldFind();
		if (Utility.FoundAllParts(num.ToString()))
		{
			num++;
		}
		return num;
	}

	public static int LastFoundFieldFind()
	{
		int result = 0;
		for (int i = 1; i < 10; i++)
		{
			if (DataStore.GetBool("FoundFieldFind" + i.ToString(), false))
			{
				result = i;
			}
		}
		return result;
	}

	public static bool NeedToCloudSave()
	{
		bool result = false;
		foreach (Key key in DataStore.usedKeys)
		{
			if (!key.Uploaded && !DataStore.cloudExceptions.Contains(key.Name))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public static void CloudSave(bool ignoreTime = false)
	{
		if (DataStore.lastCloudSave > 0L && DateTime.Now.Ticks - DataStore.lastCloudSave < TimeSpan.FromSeconds((double)DataStore.saveInterval).Ticks && !ignoreTime)
		{
			return;
		}
		if (DataStore.lastCloudSave == 0L)
		{
			UnityEngine.Debug.Log("No cloud save recorded yet");
			DataStore.SetLastCloudSave();
			return;
		}
		if (DataStore.disableCloudSave)
		{
			UnityEngine.Debug.Log("Not saving, disabled right now");
			DataStore.SetLastCloudSave();
			return;
		}
		if (!DataStore.NeedToCloudSave())
		{
			UnityEngine.Debug.Log("No keys need to be saved right now");
			DataStore.SetLastCloudSave();
			return;
		}
		if (!FB.IsInitialized || !FB.IsLoggedIn || !PlayFabClientAPI.IsClientLoggedIn())
		{
			UnityEngine.Debug.Log("Could not save to cloud - not logged into FB or PF");
			DataStore.SetLastCloudSave();
			return;
		}
		if (DataStore.cloudSaveInProgress)
		{
			return;
		}
		string value = DataStore.DumpData();
		DataStore.cloudSaveInProgress = true;
		PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string>
			{
				{
					"PlayerData",
					value
				}
			}
		}, delegate(UpdateUserDataResult result)
		{
			UnityEngine.Debug.Log("User data saved to PlayFab");
			DataStore.SetLastCloudSave();
			DataStore.cloudSaveInProgress = false;
			foreach (Key key in DataStore.usedKeys)
			{
				key.Uploaded = true;
			}
			DataStore.SaveKeysToDisk();
			UnityEngine.Debug.Log("Just saved data.. any to update? " + DataStore.NeedToCloudSave());
		}, delegate(PlayFabError error)
		{
			UnityEngine.Debug.Log("Error saving user data to PlayFab: " + error.GenerateErrorReport());
			DataStore.SetLastCloudSave();
			DataStore.cloudSaveInProgress = false;
		}, null, null);
	}

	public static void SetLastCloudSave()
	{
		DataStore.SetLong("LastCloudSave", DateTime.Now.Ticks);
		DataStore.lastCloudSave = DateTime.Now.Ticks;
	}

	public static string DumpData()
	{
		string text = string.Empty;
		foreach (Key key in DataStore.usedKeys)
		{
			key.Populate();
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				key.Name,
				"<;>",
				key.Type.ToString(),
				"<;>",
				key.ValueAsString(),
				"<{-_-}>"
			});
		}
		UnityEngine.Debug.Log("Raw Length: " + text.Length);
		string text3 = Zip.CompressToString(text);
		UnityEngine.Debug.Log("Compressed Length: " + text3.Length);
		string text4 = Zip.Decompress(text3);
		UnityEngine.Debug.Log("Uncompressed Length: " + text4.Length);
		return text3;
	}

	public static void LoadStringData()
	{
		string text = "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";
		string text2 = text;
		if (text2 == null || text2.Length < 10)
		{
			DataStore.disableCloudSave = false;
			MenuManager.Instance.CloudRestoreComplete(false);
			return;
		}
		DataStore.uncompressedData = Zip.Decompress(text2);
		UnityEngine.Debug.Log("Got user data: " + DataStore.uncompressedData);
		string[] savedVehiclesIDs = MenuManager.Instance.GetSavedVehiclesIDs();
		int num = (savedVehiclesIDs == null) ? 0 : savedVehiclesIDs.Length;
		int num2 = 0;
		StatsData statsData = GameState.LoadStatsData();
		StatsData statsData2 = new StatsData();
		if (DataStore.uncompressedData != null)
		{
			string[] array = DataStore.uncompressedData.Split(new string[]
			{
				"<{-_-}>"
			}, StringSplitOptions.None);
			foreach (string text3 in array)
			{
				string[] array3 = text3.Split(new string[]
				{
					"<;>"
				}, 3, StringSplitOptions.None);
				if (array3.Length == 3)
				{
					if (array3[0].ToUpper().Contains("VEHICLE_"))
					{
						UnityEngine.Debug.Log("VEHICLE: " + text3);
						num2++;
					}
					if (array3[0].ToUpper() == "STATS")
					{
						statsData2 = (StatsData)XmlSerialization.DeserializeData<StatsData>(array3[2]);
						UnityEngine.Debug.Log("Stats:\r\n" + array3[2]);
					}
				}
			}
		}
		if (num2 <= num && statsData2.Gold <= statsData.Gold && statsData2.Money <= statsData.Money)
		{
			UnityEngine.Debug.Log("Cloud Vehicles: " + num2);
			UnityEngine.Debug.Log("Local Vehicles: " + num);
			UnityEngine.Debug.Log("Cloud Gold: " + statsData2.Gold);
			UnityEngine.Debug.Log("Local Gold: " + statsData.Gold);
			UnityEngine.Debug.Log("Cloud Money: " + statsData2.Money);
			UnityEngine.Debug.Log("Local Money: " + statsData.Money);
			DataStore.disableCloudSave = false;
			MenuManager.Instance.CloudRestoreComplete(false);
			DataStore.uncompressedData = null;
			return;
		}
		if (statsData2.Gold > statsData.Gold && statsData2.Money > statsData.Money && num2 > num)
		{
			DataStore.ImportCloudData();
		}
		else
		{
			UnityEngine.Debug.Log("Cloud Vehicles: " + num2);
			UnityEngine.Debug.Log("Local Vehicles: " + num);
			UnityEngine.Debug.Log("Cloud Gold: " + statsData2.Gold);
			UnityEngine.Debug.Log("Local Gold: " + statsData.Gold);
			UnityEngine.Debug.Log("Cloud Money: " + statsData2.Money);
			UnityEngine.Debug.Log("Local Money: " + statsData.Money);
			MenuManager.Instance.ShowImportCloudDataBox(statsData.Gold, statsData2.Gold, statsData.Money, statsData2.Money, num, num2);
		}
	}

	public static void DownloadCloudData()
	{
		if (!FB.IsInitialized || !FB.IsLoggedIn || !PlayFabClientAPI.IsClientLoggedIn())
		{
			UnityEngine.Debug.Log("User not logged in to FB or PF, can't download");
			return;
		}
		DataStore.disableCloudSave = true;
		PlayFabClientAPI.GetUserData(new GetUserDataRequest
		{
			Keys = new List<string>
			{
				"PlayerData"
			}
		}, delegate(GetUserDataResult result)
		{
			if (result.Data == null || result.Data.Count == 0)
			{
				UnityEngine.Debug.Log("Looks like there was no cloud data");
				DataStore.disableCloudSave = false;
				MenuManager.Instance.CloudRestoreComplete(false);
				return;
			}
			UserDataRecord userDataRecord = result.Data["PlayerData"];
			string value = userDataRecord.Value;
			if (value == null || value.Length < 10)
			{
				DataStore.disableCloudSave = false;
				MenuManager.Instance.CloudRestoreComplete(false);
				return;
			}
			DataStore.uncompressedData = Zip.Decompress(value);
			UnityEngine.Debug.Log("Got user data: " + DataStore.uncompressedData);
			string[] savedVehiclesIDs = MenuManager.Instance.GetSavedVehiclesIDs();
			int num = (savedVehiclesIDs == null) ? 0 : savedVehiclesIDs.Length;
			int num2 = 0;
			StatsData statsData = GameState.LoadStatsData();
			StatsData statsData2 = new StatsData();
			if (DataStore.uncompressedData != null)
			{
				string[] array = DataStore.uncompressedData.Split(new string[]
				{
					"<{-_-}>"
				}, StringSplitOptions.None);
				foreach (string text in array)
				{
					string[] array3 = text.Split(new string[]
					{
						"<;>"
					}, 3, StringSplitOptions.None);
					if (array3.Length == 3)
					{
						if (array3[0].ToUpper().Contains("VEHICLE_"))
						{
							UnityEngine.Debug.Log("VEHICLE: " + text);
							num2++;
						}
						if (array3[0].ToUpper() == "STATS")
						{
							statsData2 = (StatsData)XmlSerialization.DeserializeData<StatsData>(array3[2]);
							UnityEngine.Debug.Log("Stats:\r\n" + array3[2]);
						}
					}
				}
			}
			if (num2 <= num && statsData2.Gold <= statsData.Gold && statsData2.Money <= statsData.Money)
			{
				UnityEngine.Debug.Log("Cloud Vehicles: " + num2);
				UnityEngine.Debug.Log("Local Vehicles: " + num);
				UnityEngine.Debug.Log("Cloud Gold: " + statsData2.Gold);
				UnityEngine.Debug.Log("Local Gold: " + statsData.Gold);
				UnityEngine.Debug.Log("Cloud Money: " + statsData2.Money);
				UnityEngine.Debug.Log("Local Money: " + statsData.Money);
				DataStore.disableCloudSave = false;
				MenuManager.Instance.CloudRestoreComplete(false);
				DataStore.uncompressedData = null;
				return;
			}
			if (statsData2.Gold > statsData.Gold && statsData2.Money > statsData.Money && num2 > num)
			{
				DataStore.ImportCloudData();
			}
			else
			{
				UnityEngine.Debug.Log("Cloud Vehicles: " + num2);
				UnityEngine.Debug.Log("Local Vehicles: " + num);
				UnityEngine.Debug.Log("Cloud Gold: " + statsData2.Gold);
				UnityEngine.Debug.Log("Local Gold: " + statsData.Gold);
				UnityEngine.Debug.Log("Cloud Money: " + statsData2.Money);
				UnityEngine.Debug.Log("Local Money: " + statsData.Money);
				MenuManager.Instance.ShowImportCloudDataBox(statsData.Gold, statsData2.Gold, statsData.Money, statsData2.Money, num, num2);
			}
		}, delegate(PlayFabError error)
		{
			UnityEngine.Debug.Log("Couldn't get user data: " + error.GenerateErrorReport());
			DataStore.disableCloudSave = false;
		}, null, null);
	}

	public static void ImportCloudData()
	{
		DataStore.Clear();
		DataStore.savingCloudKeys = true;
		string[] array = DataStore.uncompressedData.Split(new string[]
		{
			"<{-_-}>"
		}, StringSplitOptions.None);
		foreach (string text in array)
		{
			string[] array3 = text.Split(new string[]
			{
				"<;>"
			}, 3, StringSplitOptions.None);
			if (array3.Length == 3)
			{
				string text2 = array3[1];
				if (text2 != null)
				{
					if (!(text2 == "String"))
					{
						if (!(text2 == "Int"))
						{
							if (!(text2 == "Long"))
							{
								if (!(text2 == "Bool"))
								{
									if (text2 == "Float")
									{
										DataStore.SetFloat(array3[0], float.Parse(array3[2]));
									}
								}
								else
								{
									DataStore.SetBool(array3[0], bool.Parse(array3[2]));
								}
							}
							else
							{
								DataStore.SetLong(array3[0], long.Parse(array3[2]));
							}
						}
						else
						{
							DataStore.SetInt(array3[0], int.Parse(array3[2]));
						}
					}
					else
					{
						DataStore.SetString(array3[0], array3[2]);
					}
				}
			}
			else
			{
				UnityEngine.Debug.Log("Not 3: " + array3.Length);
			}
		}
		DataStore.SetInt("GraphicsLevel", 2);
		UnityEngine.Debug.Log("Data downloaded and imported.");
		MenuManager.Instance.CloudRestoreComplete(true);
		DataStore.disableCloudSave = false;
		DataStore.savingCloudKeys = false;
		DataStore.uncompressedData = null;
	}

	private static void UpdateKey(string key, KeyType type)
	{
		try
		{
			Key key2 = DataStore.usedKeys.Find((Key k) => k.Name == key && k.Type == type);
			if (key2 == null && key != string.Empty)
			{
				key2 = Key.MakeKey(key, type);
				DataStore.usedKeys.Add(key2);
			}
			if (!DataStore.savingCloudKeys)
			{
				key2.Uploaded = false;
			}
			else
			{
				key2.Uploaded = true;
			}
			Key key3 = DataStore.usedKeys.Find((Key k) => k.Name == key && k.Type == type);
			DataStore.SaveKeysToDisk();
		}
		catch
		{
			UnityEngine.Debug.Log("Exception adding key for datastore");
		}
	}

	public static void DeleteKey(string key)
	{
		try
		{
			PlayerPrefs.DeleteKey(key);
			if (DataStore.usedKeys.Find((Key k) => k.Name == key) != null && key != string.Empty)
			{
				DataStore.usedKeys.Remove(DataStore.usedKeys.Find((Key k) => k.Name == key));
				DataStore.SaveKeysToDisk();
			}
		}
		catch
		{
			UnityEngine.Debug.Log("Exception removing key from datastore");
		}
	}

	public static void SaveKeysToDisk()
	{
		string text = string.Empty;
		foreach (Key key in DataStore.usedKeys)
		{
			string text2 = (!key.Uploaded) ? "0" : "1";
			object[] array = new object[9];
			array[0] = text;
			array[1] = key.Name;
			array[2] = "}";
			array[3] = DateTime.Now.Ticks;
			array[4] = "}";
			array[5] = text2;
			array[6] = "}";
			int num = 7;
			int type = (int)key.Type;
			array[num] = type.ToString();
			array[8] = "|";
			text = string.Concat(array);
		}
		PlayerPrefs.SetString("UsedKeys", text);
	}

	private static void ClearKeys()
	{
		DataStore.usedKeys.Clear();
	}

	public static void SetString(string key, string value)
	{
		DataStore.UpdateKey(key, KeyType.String);
		PlayerPrefs.SetString(key, value);
	}

	public static void SetLong(string key, long value)
	{
		DataStore.UpdateKey(key, KeyType.Long);
		PlayerPrefs.SetString(key, value.ToString());
	}

	public static void SetBool(string key, bool value)
	{
		DataStore.UpdateKey(key, KeyType.Bool);
		PlayerPrefs.SetInt(key, (!value) ? 0 : 1);
	}

	public static void SetInt(string key, int value)
	{
		DataStore.UpdateKey(key, KeyType.Int);
		PlayerPrefs.SetInt(key, value);
	}

	public static void Clear()
	{
		DataStore.ClearKeys();
		PlayerPrefs.DeleteAll();
	}

	public static bool HasKey(string key)
	{
		return PlayerPrefs.HasKey(key);
	}

	public static void SetFloat(string key, float value)
	{
		DataStore.UpdateKey(key, KeyType.Float);
		PlayerPrefs.SetFloat(key, value);
	}

	public static string GetString(string key)
	{
		return DataStore.GetString(key, null);
	}

	public static long GetLong(string key)
	{
		return long.Parse(DataStore.GetString(key, "0"));
	}

	public static bool GetBool(string key, bool defaultValue = false)
	{
		int defaultValue2 = (!defaultValue) ? 0 : 1;
		return DataStore.GetInt(key, defaultValue2) == 1;
	}

	public static int GetInt(string key)
	{
		return DataStore.GetInt(key, 0);
	}

	public static float GetFloat(string key)
	{
		return DataStore.GetFloat(key, 0f);
	}

	public static string GetString(string key, string defaultValue)
	{
		return PlayerPrefs.GetString(key, defaultValue);
	}

	public static int GetInt(string key, int defaultValue)
	{
		return PlayerPrefs.GetInt(key, defaultValue);
	}

	public static float GetFloat(string key, float defaultValue)
	{
		return PlayerPrefs.GetFloat(key, defaultValue);
	}

	public static List<Key> usedKeys = new List<Key>();

	private static int saveInterval = 10;

	private static long lastCloudSave = 0L;

	private static bool cloudSaveInProgress = false;

	public static bool disableCloudSave = false;

	private static List<string> cloudExceptions = new List<string>();

	private static bool savingCloudKeys = false;

	private static string uncompressedData = string.Empty;
}
