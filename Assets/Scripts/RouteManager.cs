using System;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
	private void Start()
	{
		RouteManager.Instance = this;
		if (this.mapName == string.Empty || this.mapName == "SET_ME_TO_MAP_NAME")
		{
			UnityEngine.Debug.LogError("RouteManager needs to know the current map's name!");
		}
		Route[] componentsInChildren = base.GetComponentsInChildren<Route>();
		foreach (Route route in componentsInChildren)
		{
			if (route.MultiplayerTrail && GameState.GameType != GameType.TrailRace)
			{
				route.gameObject.SetActive(false);
			}
			route.Manager = this;
		}
		if (GameState.GameType == GameType.CaptureTheFlag)
		{
			foreach (Route route2 in componentsInChildren)
			{
				route2.gameObject.SetActive(false);
				route2.Manager = this;
			}
		}
	}

	public int RefreshInterval = 20;

	public string mapName;

	public static RouteManager Instance;
}
