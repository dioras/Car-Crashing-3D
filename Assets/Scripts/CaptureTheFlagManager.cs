using System;
using UnityEngine;
using UnityEngine.UI;

public class CaptureTheFlagManager : MonoBehaviour
{
	public CaptureTheFlagManager()
	{
		if (CaptureTheFlagManager.Instance == null)
		{
			CaptureTheFlagManager.Instance = this;
		}
	}

	public int PlayerCount
	{
		get
		{
			return this._PlayerCount;
		}
	}

	private void Awake()
	{
		CaptureTheFlagManager.Instance = this;
	}

	private void Start()
	{
		this.FlagPoints = Utility.FindObjectsOfTypeAll<FlagPoint>().ToArray();
		if (this.FlagPoints != null && this.FlagPoints.Length > 0)
		{
			Array.Sort<FlagPoint>(this.FlagPoints, (FlagPoint fp1, FlagPoint fp2) => fp1.gameObject.name.CompareTo(fp2.gameObject.name));
		}
		for (int i = 0; i < this.FlagPoints.Length; i++)
		{
			this.FlagPoints[i].FlagPointID = i;
		}
		if (GameState.GameType != GameType.CaptureTheFlag)
		{
			this.CTFIndicators.SetActive(false);
			for (int j = 0; j < this.FlagPoints.Length; j++)
			{
				this.FlagPoints[j].gameObject.SetActive(false);
			}
		}
		else
		{
			this.CTFIndicators.SetActive(true);
			for (int k = 0; k < this.FlagPoints.Length; k++)
			{
				this.FlagPoints[k].gameObject.SetActive(true);
			}
		}
	}

	public void GameOn()
	{
		if (this.photonView == null || this.GameInProgress || !MultiplayerManager.Instance.TotallyReady)
		{
			return;
		}
		this.playersWaitingMessage.SetActive(false);
		this.carUIControl.GameOn();
		this.GameInProgress = true;
		PhotonNetwork.room.IsOpen = false;
		PhotonNetwork.room.IsVisible = false;
	}

	public void GameWaiting()
	{
		this.playersWaitingMessage.SetActive(true);
	}

	private void LoadView()
	{
		PhotonView[] array = UnityEngine.Object.FindObjectsOfType<PhotonView>();
		foreach (PhotonView photonView in array)
		{
			UnityEngine.Debug.Log("Looking for view.. Current: " + photonView.viewID);
			if (photonView.isMine)
			{
				UnityEngine.Debug.Log("Found our view!");
				this.myTeam = PhotonNetwork.player.GetTeam();
				UnityEngine.Debug.Log("Assigned our team as: " + this.myTeam);
				this.photonView = photonView;
				this.transformView = photonView.gameObject.GetComponent<PhotonTransformView>();
				this.carUIControl = UnityEngine.Object.FindObjectOfType<CarUIControl>();
				photonView.gameObject.GetComponent<VehicleDataManager>().Team = this.myTeam;
				this.carUIControl.ShowTeam(this.myTeam);
				if (PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
				{
					this.PlayerTeamIndicator.color = Color.blue;
				}
				else
				{
					this.PlayerTeamIndicator.color = Color.red;
				}
				foreach (Image image in this.FlagIndicators)
				{
					image.color = Color.green;
				}
				break;
			}
		}
	}

	private void Update()
	{
		if (PhotonNetwork.player.GetTeam() == PunTeams.Team.none)
		{
			return;
		}
		if (this.photonView == null && GameState.GameType == GameType.CaptureTheFlag && MultiplayerManager.Instance.TotallyReady)
		{
			this.LoadView();
			return;
		}
		if (this.GameOver || GameState.GameType != GameType.CaptureTheFlag || !MultiplayerManager.Instance.TotallyReady)
		{
			return;
		}
		bool flag = true;
		Color color = Color.clear;
		foreach (FlagPoint flagPoint in this.FlagPoints)
		{
			if (flagPoint.CurrentColor != Color.red && flagPoint.CurrentColor != Color.blue)
			{
				flag = false;
			}
			else if (color != Color.clear && flagPoint.CurrentColor != color)
			{
				flag = false;
			}
			else if (flagPoint.CurrentColor == Color.green)
			{
				flag = false;
			}
			if (color == Color.clear)
			{
				color = flagPoint.CurrentColor;
			}
			if (!flag)
			{
				break;
			}
		}
		if (flag && !this.SentGameOver && this.GameInProgress)
		{
			this.winningTeam = ((!(color == Color.red)) ? PunTeams.Team.blue : PunTeams.Team.red);
			this.transformView.SendGameOverReport();
			this.SentGameOver = true;
			UnityEngine.Debug.Log("SENT GAME OVER - WAITING FOR OTHERS - " + this.winningTeam.ToString());
		}
		if (this.SentGameOver && (this.PlayersReportingGameOver == PhotonNetwork.playerList.Length || this.PlayersReportingGameOver > 1))
		{
			UnityEngine.Debug.Log("GAME IS OVER!");
			this.GameOver = true;
			this.GameInProgress = false;
			this.carUIControl.CaptureTheFlagGameOver(this.winningTeam, PhotonNetwork.player.GetTeam());
		}
	}

	public void ReportGameOver()
	{
		UnityEngine.Debug.Log("Someone reported game over!");
		this.PlayersReportingGameOver++;
	}

	public void SetFlagCaptured(int flagID, PunTeams.Team team)
	{
		foreach (FlagPoint flagPoint in this.FlagPoints)
		{
			if (flagPoint.FlagPointID == flagID)
			{
				flagPoint.SwitchColor((team != PunTeams.Team.blue) ? Color.red : Color.blue);
			}
		}
		if (flagID < this.FlagIndicators.Length)
		{
			this.FlagIndicators[flagID].color = ((team != PunTeams.Team.blue) ? Color.red : Color.blue);
		}
	}

	private int _PlayerCount = 4;

	public Color NeutralColor = Color.green;

	public FlagPoint[] FlagPoints;

	public GameObject CTFIndicators;

	public Image[] FlagIndicators;

	public Image PlayerTeamIndicator;

	public int PlayersReportingGameOver;

	public GameObject playersWaitingMessage;

	public bool SentGameOver;

	public bool GameOver;

	public bool GameInProgress;

	public PhotonView photonView;

	public PhotonTransformView transformView;

	public CarUIControl carUIControl;

	public PunTeams.Team winningTeam;

	public PunTeams.Team myTeam;

	public static CaptureTheFlagManager Instance;
}
