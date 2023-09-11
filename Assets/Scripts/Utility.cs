using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CustomVP;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utility
{
	public static string RandomDigits(int length)
	{
		string text = string.Empty;
		for (int i = 0; i < length; i++)
		{
			text += UnityEngine.Random.Range(0, 10);
		}
		return text;
	}

	public static List<T> FindObjectsOfTypeAll<T>()
	{
		List<T> list = new List<T>();
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			Scene sceneAt = SceneManager.GetSceneAt(i);
			if (sceneAt.isLoaded)
			{
				foreach (GameObject gameObject in sceneAt.GetRootGameObjects())
				{
					list.AddRange(gameObject.GetComponentsInChildren<T>(true));
				}
			}
		}
		return list;
	}

	public static string GenerateName()
	{
		string[] array = new string[]
		{
			"Trail",
			"Winch",
			"Diesel",
			"Mud",
			"Tread",
			"Rut",
			"Dirt",
			"Bog",
			"Gear",
			"Fast",
			"Quick",
			"Rock",
			"Stone",
			"Water",
			"Sand"
		};
		string[] array2 = new string[]
		{
			"Dog",
			"Master",
			"Hawk",
			"Killer",
			"Man",
			"Boss",
			"Rig",
			"Smoke",
			"Dust"
		};
		string str = array[UnityEngine.Random.Range(0, array.Length)];
		string str2 = array2[UnityEngine.Random.Range(0, array2.Length)];
		string str3 = UnityEngine.Random.Range(0, 500).ToString();
		return str + str2 + str3;
	}

	public static bool HashMatch(string check, string hash)
	{
		return Utility.MD5(check) == hash;
	}

	public static string MD5(string data)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		byte[] bytes = utf8Encoding.GetBytes(data);
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = md5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}

	public static int CashToGold(int cashAmount)
	{
		int a = (int)Mathf.Ceil((float)cashAmount / (float)Utility.CashToGoldRatio);
		return Mathf.Max(a, 1);
	}

	public static int AdjustedWinnings(int amount)
	{
		StatsData statsData = GameState.LoadStatsData();
		if (statsData.IsMember)
		{
			return amount * Utility.MemberBonusMultiplier;
		}
		return amount;
	}

	public static bool FoundAllParts(string id)
	{
		string @string = DataStore.GetString("FoundPartsFF" + id, string.Empty);
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
		return list.Count == 0;
	}

	public static bool OwnsVehicle(string name)
	{
		bool result = false;
		string @string = DataStore.GetString("VehiclesList", null);
		if (@string != null && @string != string.Empty)
		{
			SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(@string);
			for (int i = 0; i < savedVehiclesList.VehicleIDs.Count; i++)
			{
				string string2 = DataStore.GetString(savedVehiclesList.VehicleIDs[i]);
				VehicleData vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string2);
				if (name.ToUpper() == vehicleData.VehicleName.ToUpper())
				{
					return true;
				}
			}
		}
		return result;
	}

	public static void AlignVehicleByGround(Transform vehicle, bool customMap)
	{
		WheelComponent[] componentsInChildren = vehicle.GetComponentsInChildren<WheelComponent>();
		TankWheelCollider[] componentsInChildren2 = vehicle.GetComponentsInChildren<TankWheelCollider>();
		if ((componentsInChildren == null || componentsInChildren.Length == 0) && (componentsInChildren2 == null || componentsInChildren2.Length == 0))
		{
			return;
		}
		Vector3 position = vehicle.position;
		float d = 2f;
		if (customMap)
		{
			d = 10f;
		}
		RaycastHit[] array = Physics.RaycastAll(position + Vector3.up * d, Vector3.down, 50f);
		List<Vector3> list = new List<Vector3>();
		foreach (RaycastHit raycastHit in array)
		{
			VehicleDataManager component = raycastHit.collider.transform.root.GetComponent<VehicleDataManager>();
			if (!(component != null) || component.vehicleType == VehicleType.Trailer)
			{
				list.Add(raycastHit.point);
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		Vector3 vector = list[0];
		if (list.Count > 1)
		{
			for (int j = 1; j < list.Count; j++)
			{
				if (list[j].y > vector.y)
				{
					vector = list[j];
				}
			}
		}
		Vector3 vector2 = vector;
		float num = 0f;
		int num2 = 0;
		foreach (WheelComponent wheelComponent in vehicle.GetComponentsInChildren<WheelComponent>())
		{
			num += wheelComponent.wheelRadius;
			num2++;
		}
		foreach (TankWheelCollider tankWheelCollider in vehicle.GetComponentsInChildren<TankWheelCollider>())
		{
			num += tankWheelCollider.wheelRadius;
			num2++;
		}
		num /= (float)num2;
		float num3 = 0f;
		foreach (WheelComponent wheelComponent2 in vehicle.GetComponentsInChildren<WheelComponent>())
		{
			num3 += wheelComponent2.suspensionLength;
		}
		foreach (TankWheelCollider tankWheelCollider2 in vehicle.GetComponentsInChildren<TankWheelCollider>())
		{
			num3 += tankWheelCollider2.suspensionLength;
		}
		num3 /= (float)num2;
		Vector3 vector3 = Vector3.zero;
		foreach (WheelComponent wheelComponent3 in vehicle.GetComponentsInChildren<WheelComponent>())
		{
			vector3 += wheelComponent3.transform.position;
		}
		foreach (TankWheelCollider tankWheelCollider3 in vehicle.GetComponentsInChildren<TankWheelCollider>())
		{
			vector3 += tankWheelCollider3.transform.position;
		}
		vector3 /= (float)num2;
		float num6 = -vehicle.InverseTransformPoint(vector3).y * vehicle.localScale.y;
		vector2 += Vector3.up * (num + num3 + num6);
		vehicle.position = vector2;
	}

	public static void AlignHeightOnTrailer(Transform vehicle, TrailerController trailer)
	{
		WheelComponent[] componentsInChildren = vehicle.GetComponentsInChildren<WheelComponent>();
		TankWheelCollider[] componentsInChildren2 = vehicle.GetComponentsInChildren<TankWheelCollider>();
		if ((componentsInChildren == null || componentsInChildren.Length == 0) && (componentsInChildren2 == null || componentsInChildren2.Length == 0))
		{
			return;
		}
		Vector3 position = vehicle.position;
		RaycastHit[] array = Physics.RaycastAll(position + vehicle.up * 2f, -vehicle.up, 50f);
		List<Vector3> list = new List<Vector3>();
		foreach (RaycastHit raycastHit in array)
		{
			if (!(raycastHit.collider.GetComponentInParent<TrailerController>() == null))
			{
				list.Add(raycastHit.point);
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		Vector3 vector = list[0];
		if (list.Count > 1)
		{
			for (int j = 1; j < list.Count; j++)
			{
				Vector3 vector2 = vehicle.InverseTransformPoint(list[j]);
				Vector3 vector3 = vehicle.InverseTransformPoint(vector);
				if (vector2.y > vector3.y)
				{
					vector = list[j];
				}
			}
		}
		Vector3 vector4 = vector;
		float num = 0f;
		int num2 = 0;
		if (componentsInChildren2 == null || componentsInChildren2.Length == 0)
		{
			foreach (WheelComponent wheelComponent in vehicle.GetComponentsInChildren<WheelComponent>())
			{
				num += wheelComponent.wheelRadius;
				num2++;
			}
			num /= (float)num2;
		}
		else
		{
			foreach (TankWheelCollider tankWheelCollider in vehicle.GetComponentsInChildren<TankWheelCollider>())
			{
				num += tankWheelCollider.wheelRadius;
				num2++;
			}
			num /= (float)num2;
		}
		float num3 = 0f;
		if (componentsInChildren2 == null || componentsInChildren2.Length == 0)
		{
			foreach (WheelComponent wheelComponent2 in vehicle.GetComponentsInChildren<WheelComponent>())
			{
				num3 += wheelComponent2.suspensionLength;
			}
			num3 /= (float)num2;
		}
		else
		{
			foreach (TankWheelCollider tankWheelCollider2 in vehicle.GetComponentsInChildren<TankWheelCollider>())
			{
				num3 += tankWheelCollider2.suspensionLength;
			}
			num3 /= (float)num2;
		}
		Vector3 vector5 = Vector3.zero;
		if (componentsInChildren2 == null || componentsInChildren2.Length == 0)
		{
			foreach (WheelComponent wheelComponent3 in vehicle.GetComponentsInChildren<WheelComponent>())
			{
				vector5 += wheelComponent3.transform.position;
			}
			vector5 /= (float)num2;
		}
		else
		{
			foreach (TankWheelCollider tankWheelCollider3 in vehicle.GetComponentsInChildren<TankWheelCollider>())
			{
				vector5 += tankWheelCollider3.transform.position;
			}
			vector5 /= (float)num2;
		}
		float num6 = -vehicle.InverseTransformPoint(vector5).y * vehicle.localScale.y;
		vector4 += vehicle.up * (num + num3 + num6);
		vehicle.position = vector4;
	}

	public static string EqiuppedTrailer()
	{
		string @string = DataStore.GetString("VehiclesList");
		if (@string == string.Empty)
		{
			return string.Empty;
		}
		SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(@string);
		string[] array = savedVehiclesList.VehicleIDs.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			string string2 = DataStore.GetString(array[i]);
			VehicleData vehicleData = (VehicleData)XmlSerialization.DeserializeData<VehicleData>(string2);
			if (vehicleData.equippedTrailer)
			{
				return array[i];
			}
		}
		return string.Empty;
	}

	public static bool DoesTruckExist(string vehicleID)
	{
		string @string = DataStore.GetString("VehiclesList");
		if (@string == string.Empty)
		{
			return false;
		}
		SavedVehiclesList savedVehiclesList = (SavedVehiclesList)XmlSerialization.DeserializeData<SavedVehiclesList>(@string);
		return savedVehiclesList.VehicleIDs.Contains(vehicleID);
	}

	private static TWordFilter wordFilter
	{
		get
		{
			TWordFilter twordFilter = new TWordFilter();
			twordFilter.parseStringLine("*shit");
			twordFilter.parseStringLine("*shitter");
			twordFilter.parseStringLine("*fucking");
			twordFilter.parseStringLine("*fuck");
			twordFilter.parseStringLine("*fucka");
			twordFilter.parseStringLine("*fucker");
			twordFilter.parseStringLine("*damn");
			twordFilter.parseStringLine("*bitch");
			twordFilter.parseStringLine("*gay");
			twordFilter.parseStringLine("*fag");
			twordFilter.parseStringLine("*faggot");
			twordFilter.parseStringLine("*crap");
			twordFilter.parseStringLine("*piss");
			twordFilter.parseStringLine("*dick");
			twordFilter.parseStringLine("*pussy");
			twordFilter.parseStringLine("*douche");
			twordFilter.parseStringLine("*douchebag");
			twordFilter.parseStringLine("*douche bag");
			twordFilter.parseStringLine("*ass");
			twordFilter.parseStringLine("*asswhipe");
			twordFilter.parseStringLine("*asshole");
			twordFilter.parseStringLine("*slut");
			twordFilter.parseStringLine("*bastard");
			twordFilter.parseStringLine("*cock");
			twordFilter.parseStringLine("*cunt");
			twordFilter.parseStringLine("*lesbo");
			twordFilter.parseStringLine("*nigga");
			twordFilter.parseStringLine("*nigger");
			twordFilter.parseStringLine("*retard");
			twordFilter.parseStringLine("*retarded");
			twordFilter.parseStringLine("*motherfucker");
			twordFilter.parseStringLine("*mother fucker");
			return twordFilter;
		}
	}

	public static string CleanBadWords(string text)
	{
		return Utility.wordFilter.cleanString(text);
	}

	public static bool HasBadWord(string text)
	{
		string value = Utility.wordFilter.cleanString(text);
		return !text.Equals(value);
	}

	public static bool IsXMLDataCompressed(string xmlData)
	{
		string text = Utility.DecompressXMLData(xmlData);
		return xmlData.Length != text.Length;
	}

	public static string CompressXMLData(string rawXMLData)
	{
		string text = rawXMLData.Replace("FloatValue", "@F");
		text = text.Replace("IntValue", "@I");
		text = text.Replace("SuspensionValue", "@SV");
		text = text.Replace("ValueName", "@VN");
		return text.Replace("valueType", "@vT");
	}

	public static string DecompressXMLData(string compressedData)
	{
		string text = compressedData.Replace("@F", "FloatValue");
		text = text.Replace("@I", "IntValue");
		text = text.Replace("@SV", "SuspensionValue");
		text = text.Replace("@VN", "ValueName");
		return text.Replace("@vT", "valueType");
	}

	public static int CashToGoldRatio = 200;

	public static int MemberBonusMultiplier = 3;
}
