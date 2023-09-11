using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public MapManager()
	{
		MapManager.Maps.Add(new Map("Map1", true));
	}

	public Map Current()
	{
		return MapManager.Maps.Find((Map m) => m.IsCurrent);
	}

	public static List<Map> Maps = new List<Map>();
}
