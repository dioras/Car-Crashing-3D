using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StashManager : MonoBehaviour
{
	private void Awake()
	{
		StashManager.Instance = this;
	}

	private void Start()
	{
		this.camController = CameraController.Instance;
		this.carUIControl = CarUIControl.Instance;
		this.racingManager = RacingManager.Instance;
		this.nextLockboxStashShown = Time.time + (float)UnityEngine.Random.Range(this.MinLockboxStashInterval, this.MaxLockboxStashInterval);
		this.StashMessage.SetActive(false);
	}

	public void CloseStashMessage()
	{
		Time.timeScale = 1f;
		this.StashMessage.SetActive(false);
		this.LockboxEnabledMessage.SetActive(false);
		if (this.CurrentLockboxData != null)
		{
			this.LockboxTimer.SetActive(true);
		}
		this.camController.GetComponent<Camera>().GetComponent<AudioListener>().enabled = true;
	}

	public void RegisterStashCrate(StashCrate crate)
	{
		this.crates.Add(crate);
	}

	public void RefreshCrates()
	{
		for (int i = 0; i < this.crates.Count; i++)
		{
			this.crates[i].gameObject.SetActive(false);
		}
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			base.enabled = false;
			return;
		}
		int num = DataStore.LastFoundFieldFind();
		string text = string.Empty;
		if (num > 0)
		{
			text = FieldFind.FieldFindNames[num - 1];
		}
		if (text != string.Empty && num > 0 && !Utility.OwnsVehicle(text))
		{
			UnityEngine.Debug.Log("NEED TO MAKE PARTS BOXES!!");
			string @string = DataStore.GetString("FoundPartsFF" + num, string.Empty);
			string[] array = @string.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			for (int j = 0; j <= 9; j++)
			{
				bool flag = false;
				foreach (string a in array)
				{
					if (a == j.ToString())
					{
						flag = true;
					}
				}
				if (!flag)
				{
					list.Add(j);
				}
			}
			int num2 = 0;
			while (num2 < 3 && num2 < list.Count)
			{
				UnityEngine.Debug.Log("Making parts box!");
				StashCrate stashCrate = this.EnableCrate(CrateSize.Large);
				if (stashCrate != null)
				{
					UnityEngine.Debug.Log("Got a box!");
					stashCrate.Content = new StashContent
					{
						MissingPart = (CratePartType)list[num2]
					};
					stashCrate.SetAsMissingParts();
				}
				num2++;
			}
		}
		for (int l = 0; l < this.ActiveCrates; l++)
		{
			if (!this.EnableCrate(this.CrateSizeNeeded(true, true)))
			{
				this.EnableCrate(CrateSize.Small);
			}
		}
		num = DataStore.LastFoundFieldFind();
		text = string.Empty;
		bool flag2 = false;
		if (num > 0)
		{
			text = FieldFind.FieldFindNames[num - 1];
			flag2 = Utility.OwnsVehicle(text);
		}
		if (flag2 || num == 0)
		{
			for (int m = 0; m < this.crates.Count; m++)
			{
				if (this.crates[m].Size == CrateSize.Vehicle)
				{
					this.crates[m].gameObject.SetActive(this.crates[m].FieldFindID == num + 1);
				}
			}
		}
	}

	public CrateSize CrateSizeNeeded(bool allowVehicles = true, bool allowSmallCrates = true)
	{
		int num = UnityEngine.Random.Range(1, 101);
		if (num <= this.VehicleChance && !this.madeVehicle && allowVehicles)
		{
			return CrateSize.Vehicle;
		}
		if (num <= this.LargeChance)
		{
			return CrateSize.Large;
		}
		if (num <= this.MediumChance)
		{
			return CrateSize.Medium;
		}
		if (!allowSmallCrates)
		{
			return (UnityEngine.Random.Range(1, 3) != 1) ? CrateSize.Large : CrateSize.Medium;
		}
		return CrateSize.Small;
	}

	public void FoundStashCrate(StashCrate crate)
	{
		if (crate.Size == CrateSize.Vehicle)
		{
			CarUIControl.Instance.ShowMessage("Great find! We'll ship this back home for you and place it in your yard.");
			DataStore.SetBool("FoundFieldFind" + crate.FieldFindID, true);
			crate.gameObject.SetActive(false);
			return;
		}
		if (crate.Content != null && crate.IsMissingParts)
		{
			this.FoundStashMissingPart.SetActive(true);
			this.FoundStashMoneyAndGold.SetActive(false);
			Dictionary<CratePartType, string> dictionary = StashContent.CratePartTypeList();
			this.MissingPartFoundText.text = dictionary[crate.Content.MissingPart];
			crate.gameObject.SetActive(false);
			this.StashMessage.SetActive(true);
			int num = DataStore.LastFoundFieldFind();
			string @string = DataStore.GetString("FoundPartsFF" + num, string.Empty);
			DataStore.SetString("FoundPartsFF" + num, @string + "," + (int)crate.Content.MissingPart);
			if (Utility.FoundAllParts(num.ToString()))
			{
				Text missingPartFoundText = this.MissingPartFoundText;
				missingPartFoundText.text += "\r\n\r\nYou've found them all!!";
			}
			return;
		}
		StashContent stashContent = this.GetStashContent(crate);
		StatsData statsData = GameState.LoadStatsData();
		if (statsData != null)
		{
			this.LockboxLabel.SetActive(false);
			this.StashCrateLabel.SetActive(false);
			statsData.Gold += Utility.AdjustedWinnings(stashContent.GoldAmount);
			statsData.Money += Utility.AdjustedWinnings(stashContent.CashAmount);
			statsData.XP += Utility.AdjustedWinnings(10);
			GameState.SaveStatsData(statsData);
			statsData = GameState.LoadStatsData();
			CarUIControl.Instance.ShowMessage("Good find! We've given you 10XP" + ((!statsData.IsMember) ? string.Empty : "x3"));
			this.FoundStashMissingPart.SetActive(false);
			this.FoundStashMoneyAndGold.SetActive(true);
			this.StashMessage.SetActive(true);
			Time.timeScale = 0f;
			this.GoldAmount.text = stashContent.GoldAmount.ToString() + ((!statsData.IsMember) ? string.Empty : "x3");
			if (stashContent.CashAmount > 0)
			{
				this.MoneyAmount.text = stashContent.CashAmount.ToString("$0,0") + ((!statsData.IsMember) ? string.Empty : "x3");
			}
			else
			{
				this.MoneyAmount.text = "$0";
			}
			if (crate.TimeLeft > 0f)
			{
				UnityEngine.Debug.Log("This was a lockbox!");
				this.LockboxDisabled(false);
				this.LockboxLabel.SetActive(true);
			}
			else
			{
				this.StashCrateLabel.SetActive(true);
			}
		}
		if (stashContent.BoostCard != null)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Boost Card: ",
				stashContent.BoostCard.Type,
				" x",
				stashContent.BoostCard.MultiplyAmount,
				" for ",
				stashContent.BoostCard.Duration,
				" seconds."
			}));
		}
		this.camController.GetComponent<Camera>().GetComponent<AudioListener>().enabled = false;
		crate.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (!this.createdCrates)
		{
			this.RefreshCrates();
			this.createdCrates = true;
			return;
		}
		if (this.racingManager != null && (this.racingManager.IsPlayerBusy || !this.racingManager.gameObject.activeInHierarchy) && this.nextLockboxStashShown != -1f && Time.time + (float)this.LockboxDelayIfBusy > this.nextLockboxStashShown)
		{
			this.nextLockboxStashShown = Time.time + (float)this.LockboxDelayIfBusy;
		}
		if (Time.frameCount % 2 == 0 && this.CurrentLockboxData != null)
		{
			if (this.CurrentLockboxData.TimeLeft < 10f)
			{
				this.carUIControl.LockboxCountdown(this.CurrentLockboxData.TimeLeft);
			}
			this.LockboxTimeLeft.text = (int)this.CurrentLockboxData.TimeLeft + " seconds";
		}
		if (this.nextLockboxStashShown != -1f && Time.time > this.nextLockboxStashShown && !this.LockboxActive && !VehicleLoader.Instance.droneMode)
		{
			UnityEngine.Debug.Log("Making lockbox");
			StashCrate stashCrate = this.EnableCrate(this.CrateSizeNeeded(false, false));
			if (stashCrate != null)
			{
				stashCrate.SetLockTimer(UnityEngine.Random.Range(this.MinLockboxStashLockTimer, this.MaxLockboxStashLockTimer));
				this.lastLockboxStashShown = Time.time;
				this.LockboxActive = true;
				this.CurrentLockbox = stashCrate.gameObject;
				this.CurrentLockboxData = stashCrate;
				UnityEngine.Debug.Log("Lockbox activated!");
				this.nextLockboxStashShown = -1f;
				this.carUIControl.DirectionalArrow.gameObject.SetActive(true);
				this.carUIControl.DirectionalArrowTarget = stashCrate.transform;
				this.camController.GetComponent<Camera>().GetComponent<AudioListener>().enabled = false;
				this.LockboxEnabledMessage.SetActive(true);
				Time.timeScale = 0f;
			}
			else
			{
				this.nextLockboxStashShown = Time.time + (float)UnityEngine.Random.Range(this.MinLockboxStashInterval, this.MaxLockboxStashInterval);
				UnityEngine.Debug.Log("Couldn't make lockbox");
			}
		}
	}

	public void LockboxDisabled(bool expired)
	{
		if (expired)
		{
			this.camController.Shake();
			this.carUIControl.LockboxBomb();
		}
		this.carUIControl.DirectionalArrow.gameObject.SetActive(false);
		this.carUIControl.DirectionalArrowTarget = null;
		this.CurrentLockbox = null;
		this.CurrentLockboxData = null;
		this.LockboxActive = false;
		this.LockboxTimer.SetActive(false);
		this.nextLockboxStashShown = Time.time + (float)UnityEngine.Random.Range(this.MinLockboxStashInterval, this.MaxLockboxStashInterval);
		this.carUIControl.TickingSound.Stop();
	}

	public StashContent GetStashContent(StashCrate crate)
	{
		StashContent stashContent = new StashContent();
		if (crate.Size >= CrateSize.Small)
		{
			stashContent.CashAmount = UnityEngine.Random.Range(this.MinAmount, this.MaxAmount) * 100;
		}
		if (crate.Size >= CrateSize.Medium)
		{
			stashContent.GoldAmount = UnityEngine.Random.Range(this.MinAmount, this.MaxAmount);
		}
		return stashContent;
	}

	public StashCrate EnableCrate(CrateSize size)
	{
		List<StashCrate> list = new List<StashCrate>();
		foreach (StashCrate stashCrate in this.crates)
		{
			if (!stashCrate.gameObject.activeInHierarchy && stashCrate.Size == size)
			{
				list.Add(stashCrate);
			}
		}
		if (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			list[index].gameObject.SetActive(true);
			return list[index];
		}
		return null;
	}

	public static StashManager Instance;

	private RacingManager racingManager;

	private CameraController camController;

	private CarUIControl carUIControl;

	public int ActiveCrates = 4;

	public int VehicleChance;

	public int LargeChance = 20;

	public int MediumChance = 40;

	public int MinAmount = 5;

	public int MaxAmount = 50;

	public GameObject CurrentLockbox;

	public StashCrate CurrentLockboxData;

	public bool LockboxActive;

	public GameObject StashMessage;

	public GameObject LockboxEnabledMessage;

	public GameObject LockboxTimer;

	public Text MissingPartFoundText;

	public GameObject FoundStashMoneyAndGold;

	public GameObject FoundStashMissingPart;

	public Text GoldAmount;

	public Text MoneyAmount;

	public Text LockboxTimeLeft;

	public GameObject LockboxLabel;

	public GameObject StashCrateLabel;

	public GameObject FieldFindLabel;

	private int BoostCardChance;

	private int MinLockboxStashInterval = 300;

	private int MaxLockboxStashInterval = 600;

	private int MinLockboxStashLockTimer = 60;

	private int MaxLockboxStashLockTimer = 65;

	private int LockboxDelayIfBusy = 60;

	private float lastLockboxStashShown;

	private float nextLockboxStashShown;

	private bool madeVehicle;

	private List<StashCrate> crates = new List<StashCrate>();

	private bool createdCrates;
}
