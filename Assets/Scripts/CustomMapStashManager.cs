using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomMapStashManager : MonoBehaviour
{
	private VehicleLoader vLoader
	{
		get
		{
			return VehicleLoader.Instance;
		}
	}

	private CarUIControl ui
	{
		get
		{
			return CarUIControl.Instance;
		}
	}

	private void Awake()
	{
		CustomMapStashManager.Instance = this;
	}

	public void RegisterCrate(CustomMapCrate crate)
	{
		this.crates.Add(crate);
	}

	public void Initialize()
	{
		int count = this.crates.Count;
		int num = Mathf.Min(count, this.activeCratesCount);
		this.ShuffleList<CustomMapCrate>(this.crates);
		for (int i = 0; i < this.crates.Count; i++)
		{
			this.crates[i].gameObject.SetActive(i < num);
		}
	}

	private void ShuffleList<T>(List<T> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			T value = list[index];
			list[index] = list[i];
			list[i] = value;
		}
	}

	public void PickupCrate(CustomMapCrate crate)
	{
		if (this.vLoader.levelEditor)
		{
			return;
		}
		if (this.vLoader.droneMode)
		{
			return;
		}
		int money = crate.m_money;
		int gold = crate.m_gold;
		StatsData statsData = GameState.LoadStatsData();
		statsData.Money += money;
		statsData.Gold += gold;
		GameState.SaveStatsData(statsData);
		this.ui.ShowMessage(string.Concat(new object[]
		{
			"Found a crate! We've given you $",
			money,
			" and ",
			gold,
			" gold!"
		}));
		crate.gameObject.SetActive(false);
	}

	public static CustomMapStashManager Instance;

	private List<CustomMapCrate> crates = new List<CustomMapCrate>();

	private int activeCratesCount = 3;
}
