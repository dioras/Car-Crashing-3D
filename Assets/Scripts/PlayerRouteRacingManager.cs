using System;
using System.Collections;
using CustomVP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRouteRacingManager : MonoBehaviour
{
	private GameObject playerVehicle
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerVehicle;
			}
			return null;
		}
	}

	private Transform CurrentCheckpoint
	{
		get
		{
			return this.currentRoute.checkpoints[this.currentCheckpointID];
		}
	}

	private void Awake()
	{
		PlayerRouteRacingManager.Instance = this;
	}

	public void Initialize()
	{
		this.routes = LevelEditorTools.RoutesParent.GetComponentsInChildren<PlayerRoute>();
		this.carUIControl = CarUIControl.Instance;
		this.initialized = true;
	}

	private void Update()
	{
		if (!this.initialized)
		{
			return;
		}
		if (this.playerVehicle == null)
		{
			return;
		}
		if (!this.inRace)
		{
			this.CheckClosestRoutes();
		}
		else
		{
			this.CheckCheckpoints();
			if (this.raceTimerEnabled)
			{
				this.raceTime += Time.deltaTime;
				this.carUIControl.UpdateTimer(this.raceTime);
			}
		}
		if (this.currentRoute != this.lastRoute && this.currentRoute != null && this.loadRouteInfoCor == null)
		{
			this.loadRouteInfoCor = base.StartCoroutine(this.LoadRouteRecordCor(this.currentRoute));
		}
		this.lastRoute = this.currentRoute;
		if (this.currentRoute == null)
		{
			this.carUIControl.HidePlayerRouteInfo();
		}
	}

	private IEnumerator LoadRouteRecordCor(PlayerRoute route)
	{
		for (int i = 0; i < 10; i++)
		{
			WWW query = new WWW("http://offroadoutlaws.online/RoutesHandlerNew.php?record&routeID=" + route.routeID);
			yield return query;
			if (query.error == null)
			{
				this.OnRouteRecordLoaded(query.text, route);
				break;
			}
		}
		this.loadRouteInfoCor = null;
		yield break;
	}

	private void OnRouteRecordLoaded(string rawData, PlayerRoute route)
	{
		UnityEngine.Debug.Log("Route record loaded:" + rawData);
		float num = 0f;
		float bronzeTime = 0f;
		float silverTime = 0f;
		float goldTime = 0f;
		string routeRecordKeeper = GameState.playerName;
		string[] array = rawData.Split(new char[]
		{
			'\n'
		});
		if (array.Length != 0)
		{
			num = float.Parse(array[1]);
			routeRecordKeeper = array[0];
			bronzeTime = num * 2f;
			silverTime = num + num * 0.4f;
			goldTime = num + num * 0.2f;
		}
		route.routeRecord = num;
		route.routeRecordKeeper = routeRecordKeeper;
		float num2 = 1f;
		float num3 = route.RouteLength();
		if (num3 > 0f)
		{
			num2 = Mathf.InverseLerp(0f, 2000f, num3);
		}
		float finishReward = (float)Mathf.RoundToInt(PlayerRoute.completionMoney * num2);
		float bronzeReward = (float)Mathf.RoundToInt(PlayerRoute.bronzeMoney * num2);
		float silverReward = (float)Mathf.RoundToInt(PlayerRoute.silverMoney * num2);
		float goldReward = (float)Mathf.RoundToInt(PlayerRoute.goldMoney * num2);
		this.carUIControl.DisplayPlayerRouteValues(bronzeTime, silverTime, goldTime, finishReward, bronzeReward, silverReward, goldReward, num);
	}

	private void OnDisable()
	{
		if (this.initialized)
		{
			this.CancelRace();
		}
	}

	public void GetToGarage()
	{
		if (this.playerVehicle != null)
		{
			this.playerVehicle.GetComponent<VehicleDataManager>().SaveVehicleData();
		}
		SceneManager.LoadScene("Menu");
	}

	private void CheckClosestRoutes()
	{
		foreach (PlayerRoute playerRoute in this.routes)
		{
			if (Vector3.Distance(this.playerVehicle.transform.position, playerRoute.checkpoints[0].position) < this.routeDetectionRadius)
			{
				this.currentRoute = playerRoute;
				this.carUIControl.DisplayPlayerRouteInfo();
				return;
			}
		}
		this.currentRoute = null;
	}

	private void CheckCheckpoints()
	{
		if (Vector3.Distance(this.playerVehicle.transform.position, this.CurrentCheckpoint.position) < this.checkpointDetectionRadius)
		{
			if (this.currentCheckpointID < this.currentRoute.checkpoints.Count - 1)
			{
				this.NextCheckpoint();
			}
			else if (this.currentCheckpointID == this.currentRoute.checkpoints.Count - 1)
			{
				this.Finish();
			}
		}
	}

	private void NextCheckpoint()
	{
		this.currentCheckpointID++;
		this.carUIControl.CurrentCheckpoint = this.CurrentCheckpoint;
		this.carUIControl.ShowNotification("Checkpoint", false);
	}

	private void Finish()
	{
		if (!this.raceTimerEnabled)
		{
			return;
		}
		this.raceTimerEnabled = false;
		this.currentRoute.ToggleCheckpoints(false);
		this.playerVehicle.GetComponent<CarController>().vehicleIsActive = false;
		this.carUIControl.DisplayPlayerRouteFinish();
		bool flag = this.currentRoute.routeRecord == 0f || this.raceTime < this.currentRoute.routeRecord;
		float record = (!flag) ? this.currentRoute.routeRecord : this.raceTime;
		string keeper = (!flag) ? this.currentRoute.routeRecordKeeper : GameState.playerName;
		string awardLevel = "Gold";
		float xp = PlayerRoute.goldXP;
		float num = PlayerRoute.goldMoney;
		float num2 = PlayerRoute.goldGolds;
		if (this.currentRoute.routeRecord != 0f)
		{
			float num3 = this.currentRoute.routeRecord * 2f;
			float num4 = this.currentRoute.routeRecord + this.currentRoute.routeRecord * 0.4f;
			float num5 = this.currentRoute.routeRecord + this.currentRoute.routeRecord * 0.2f;
			if (this.raceTime < num3)
			{
				awardLevel = "Bronze";
				xp = PlayerRoute.bronzeXP;
				num = PlayerRoute.bronzeMoney;
				num2 = PlayerRoute.bronzeGolds;
			}
			if (this.raceTime < num4)
			{
				awardLevel = "Silver";
				xp = PlayerRoute.silverXP;
				num = PlayerRoute.silverMoney;
				num2 = PlayerRoute.silverGolds;
			}
			if (this.raceTime < num5)
			{
				awardLevel = "Gold";
				xp = PlayerRoute.goldXP;
				num = PlayerRoute.goldMoney;
				num2 = PlayerRoute.goldGolds;
			}
		}
		float num6 = 1f;
		float num7 = this.currentRoute.RouteLength();
		if (num7 > 0f)
		{
			num6 = Mathf.InverseLerp(0f, 2000f, num7);
		}
		num *= num6;
		num = (float)Mathf.RoundToInt(num);
		num2 = (float)((int)(num2 * num6));
		this.carUIControl.ShowPlayerRouteFinish(this.raceTime, record, keeper, awardLevel, xp, num2, num);
		GameState.AddCurrency((int)num, Currency.Money);
		GameState.AddCurrency((int)num2, Currency.Gold);
		base.StartCoroutine(this.SubmitTimeCor());
	}

	private IEnumerator SubmitTimeCor()
	{
		for (int i = 0; i < 10; i++)
		{
			WWW query = new WWW(string.Concat(new object[]
			{
				"http://offroadoutlaws.online/RoutesHandlerNew.php?submit&playerID=",
				GameState.playerName,
				"&routeID=",
				this.currentRoute.routeID,
				"&time=",
				this.raceTime
			}));
			UnityEngine.Debug.Log(query.url);
			yield return query;
			if (query.error == null)
			{
				this.OnTimeSubmitted(query.text);
				break;
			}
		}
		yield break;
	}

	private void OnTimeSubmitted(string info)
	{
	}

	public void Continue()
	{
		this.carUIControl.HidePlayerRouteFinish();
		this.playerVehicle.GetComponent<CarController>().vehicleIsActive = true;
		this.carUIControl.HideShowRaceUI(false, false);
		this.inRace = false;
	}

	public void StartRace()
	{
		this.carUIControl.HidePlayerRouteInfo();
		this.carUIControl.HideShowRaceUI(true, true);
		this.raceTime = 0f;
		this.currentCheckpointID = 1;
		this.inRace = true;
		this.raceTimerEnabled = false;
		this.carUIControl.CurrentCheckpoint = this.CurrentCheckpoint;
		this.carUIControl.UpdateTimer(this.raceTime);
		this.currentRoute.BakeRoute();
		this.currentRoute.ToggleCheckpoints(true);
		this.playerVehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.playerVehicle.transform.position = this.currentRoute.checkpoints[0].position;
		this.playerVehicle.transform.LookAt(this.CurrentCheckpoint);
		Utility.AlignVehicleByGround(this.playerVehicle.transform, false);
		this.countdownCor = base.StartCoroutine(this.CountdownCor());
	}

	public void CancelRace()
	{
		this.inRace = false;
		this.carUIControl.HideShowRaceUI(false, false);
		if (this.currentRoute != null)
		{
			this.currentRoute.ToggleCheckpoints(false);
		}
		if (this.countdownCor != null)
		{
			base.StopCoroutine(this.countdownCor);
		}
		if (this.playerVehicle != null)
		{
			this.playerVehicle.GetComponent<CarController>().vehicleIsActive = true;
		}
		this.carUIControl.HideShowCountdown(false);
		this.carUIControl.HidePlayerRouteFinish();
	}

	private IEnumerator CountdownCor()
	{
		this.carUIControl.HideShowCountdown(true);
		this.playerVehicle.GetComponent<CarController>().vehicleIsActive = false;
		for (int c = 3; c > 0; c--)
		{
			this.carUIControl.ShowCountdownText(c);
			yield return new WaitForSeconds(1f);
		}
		this.carUIControl.HideShowCountdown(false);
		this.playerVehicle.GetComponent<CarController>().vehicleIsActive = true;
		this.raceTimerEnabled = true;
		this.countdownCor = null;
		yield break;
	}

	private CarUIControl carUIControl;

	public float routeDetectionRadius;

	public float checkpointDetectionRadius;

	public bool inRace;

	public bool raceTimerEnabled;

	private PlayerRoute[] routes;

	private int currentCheckpointID;

	private float raceTime;

	private PlayerRoute currentRoute;

	private PlayerRoute lastRoute;

	private PlayerRoute tempRoute;

	private Coroutine countdownCor;

	private Coroutine loadRouteInfoCor;

	private bool initialized;

	public static PlayerRouteRacingManager Instance;
}
