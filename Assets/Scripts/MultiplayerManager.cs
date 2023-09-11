using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerManager : PunBehaviour
{
	public MultiplayerManager()
	{
		MultiplayerManager.Instance = this;
	}

	private void Awake()
	{
		if (UnityEngine.Object.FindObjectsOfType(base.GetType()).Length > 1)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
		PhotonNetwork.autoJoinLobby = true;
		PhotonNetwork.sendRate = 60;
		PhotonNetwork.sendRateOnSerialize = 60;
	}

	private void Update()
	{
		if (PhotonNetwork.inRoom && Time.time > this.nextRefreshTime)
		{
			MultiplayerManager.RefreshCurrentPlayers();
			this.nextRefreshTime = Time.time + this.playersRefreshRate;
		}
	}

	public static void RefreshCurrentPlayers()
	{
		MultiplayerManager.CurrentPlayerViews = PhotonNetwork.networkingPeer.photonViewList.Values.ToArray<PhotonView>();
		MultiplayerManager.CurrentPlayers.Clear();
		if (MultiplayerManager.CurrentPlayerViews != null)
		{
			foreach (PhotonView photonView in MultiplayerManager.CurrentPlayerViews)
			{
				if (!photonView.isMine)
				{
					MultiplayerManager.CurrentPlayers.Add(photonView.gameObject);
				}
			}
		}
	}

	public static void Connect()
	{
		if (PhotonNetwork.connectionState != ConnectionState.Connected && PhotonNetwork.connectionState != ConnectionState.Connecting)
		{
			PhotonNetwork.ConnectUsingSettings(MultiplayerManager.ServerVersion);
		}
	}

	public static RoomInfo FindPrivateRoom(string password)
	{
		foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		{
			if (roomInfo.CustomProperties["Password"] != null && roomInfo.CustomProperties["Password"].ToString() == password && roomInfo.PlayerCount < (int)roomInfo.MaxPlayers)
			{
				return roomInfo;
			}
		}
		return null;
	}

	public static RoomInfo[] GetAllTrailRaceRooms()
	{
		List<RoomInfo> list = new List<RoomInfo>();
		foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		{
			if ((GameType)roomInfo.CustomProperties["GameType"] == GameType.TrailRace && (roomInfo.CustomProperties["Password"] == null || roomInfo.CustomProperties["Password"] == string.Empty) && roomInfo.CustomProperties["TrailID"] != null && roomInfo.PlayerCount < 2 && roomInfo.IsOpen)
			{
				list.Add(roomInfo);
			}
		}
		return list.ToArray();
	}

	public static void JoinRoom()
	{
		StatsData statsData = GameState.LoadStatsData();
		PhotonNetwork.player.SetCustomProperties(new Hashtable
		{
			{
				"IsMember",
				statsData.IsMember.ToString()
			},
			{
				"XP",
				statsData.XP.ToString()
			},
			{
				"DisplayName",
				GameState.playerName
			}
		}, null, false);
		GameType gameType = GameState.GameType;
		if (gameType != GameType.FreeRoam && gameType != GameType.CaptureTheFlag)
		{
			if (gameType == GameType.TrailRace)
			{
				if (GameState.Password == null || GameState.Password == string.Empty)
				{
					MultiplayerManager.JoinPublicTrailRace();
				}
				else
				{
					MultiplayerManager.JoinPrivateTrailRace();
				}
			}
		}
		else if (GameState.Password == null || GameState.Password == string.Empty)
		{
			MultiplayerManager.JoinPublicFreeRoam();
		}
		else
		{
			MultiplayerManager.JoinPrivateFreeRoam();
		}
	}

	public static void JoinPublicFreeRoam()
	{
		RoomInfo[] roomList = PhotonNetwork.GetRoomList();
		bool flag = false;
		foreach (RoomInfo roomInfo in roomList)
		{
			bool flag2 = true;
			if (roomInfo.CustomProperties["Password"] != null && roomInfo.customProperties["Password"] != string.Empty)
			{
				flag2 = false;
			}
			if (roomInfo.CustomProperties["Scene"].ToString() != GameState.SceneName)
			{
				flag2 = false;
			}
			if (roomInfo.CustomProperties["CustomMapID"] != null && roomInfo.CustomProperties["CustomMapID"].ToString() != GameState.mapToDownload)
			{
				flag2 = false;
			}
			if (roomInfo.PlayerCount >= (int)roomInfo.MaxPlayers)
			{
				flag2 = false;
			}
			if (roomInfo.CustomProperties["GameType"] == null || (GameType)roomInfo.CustomProperties["GameType"] != GameState.GameType)
			{
				flag2 = false;
			}
			if (MultiplayerManager.RoomsAttempted.Contains(roomInfo.Name))
			{
				flag2 = false;
			}
			if (flag2)
			{
				MultiplayerManager.RoomsAttempted.Add(roomInfo.Name);
				flag = true;
				PhotonNetwork.JoinRoom(roomInfo.Name);
				break;
			}
		}
		if (!flag)
		{
			RoomOptions roomOptions = MultiplayerManager.GetRoomOptions();
			PhotonNetwork.CreateRoom(GameState.RoomName, roomOptions, TypedLobby.Default);
		}
	}

	public static void JoinPrivateFreeRoam()
	{
		RoomOptions roomOptions = MultiplayerManager.GetRoomOptions();
		PhotonNetwork.JoinOrCreateRoom(GameState.RoomName, roomOptions, TypedLobby.Default);
	}

	public static void JoinPublicTrailRace()
	{
		RoomOptions roomOptions = MultiplayerManager.GetRoomOptions();
		PhotonNetwork.JoinOrCreateRoom(GameState.RoomName, roomOptions, TypedLobby.Default);
	}

	public static void JoinPrivateTrailRace()
	{
		RoomOptions roomOptions = MultiplayerManager.GetRoomOptions();
		PhotonNetwork.JoinOrCreateRoom(GameState.RoomName, roomOptions, TypedLobby.Default);
	}

	private static RoomOptions GetRoomOptions()
	{
		return new RoomOptions
		{
			MaxPlayers = GameState.MaxPlayers,
			CustomRoomProperties = MultiplayerManager.GetCustomProperties(),
			CustomRoomPropertiesForLobby = MultiplayerManager.GetCustomPropertiesForLobby()
		};
	}

	private static Hashtable GetCustomProperties()
	{
		return new Hashtable
		{
			{
				"Scene",
				GameState.SceneName
			},
			{
				"GameType",
				GameState.GameType
			},
			{
				"Password",
				GameState.Password
			},
			{
				"TrailID",
				GameState.TrailID
			},
			{
				"HostPlayerName",
				GameState.playerName
			},
			{
				"TrailRaceBet",
				GameState.TrailRaceBet
			},
			{
				"CustomMapID",
				GameState.mapToDownload
			}
		};
	}

	private static string[] GetCustomPropertiesForLobby()
	{
		string[] result = null;
		GameType gameType = GameState.GameType;
		if (gameType != GameType.CaptureTheFlag && gameType != GameType.FreeRoam)
		{
			if (gameType == GameType.TrailRace)
			{
				result = new string[]
				{
					"Scene",
					"GameType",
					"Password",
					"TrailID",
					"HostPlayerName",
					"TrailRaceBet",
					"CustomMapID"
				};
			}
		}
		else
		{
			result = new string[]
			{
				"Scene",
				"GameType",
				"Password",
				"CustomMapID"
			};
		}
		return result;
	}

	public static void LeaveRoom()
	{
		if (PhotonNetwork.inRoom)
		{
			PhotonNetwork.LeaveRoom();
		}
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedRoom()
	{
		MultiplayerManager.RoomsAttempted = new List<string>();
		if (PhotonNetwork.room.CustomProperties["CustomMapID"] != null)
		{
			GameState.mapToDownload = PhotonNetwork.room.CustomProperties["CustomMapID"].ToString();
		}
		PhotonNetwork.LoadLevel(GameState.SceneName);
	}

	public override void OnJoinedLobby()
	{
		this.TotallyReady = false;
		if (GameState.WaitingForRoom)
		{
			MultiplayerManager.JoinRoom();
		}
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		MultiplayerManager.RefreshCurrentPlayers();
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		MultiplayerManager.RefreshCurrentPlayers();
		if (GameState.GameMode == GameMode.Multiplayer && GameState.GameType == GameType.TrailRace)
		{
			TrailRaceManager.Instance.OnOtherPlayerDisconnected();
		}
	}

	public override void OnLeftRoom()
	{
		if (SceneManager.GetActiveScene().name.ToLower() != "menu")
		{
			SceneManager.LoadScene("Menu");
		}
	}

	public override void OnCreatedRoom()
	{
		if (PhotonNetwork.room != null)
		{
			UnityEngine.Debug.Log("Created room: " + PhotonNetwork.room.Name);
			MultiplayerManager.RoomsAttempted = new List<string>();
		}
	}

	public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		MultiplayerManager.JoinRoom();
	}

	public static MultiplayerManager Instance;

	public static List<GameObject> CurrentPlayers = new List<GameObject>();

	public static PhotonView[] CurrentPlayerViews;

	public static List<string> RoomsAttempted = new List<string>();

	public static string ServerVersion = "3.0";

	[HideInInspector]
	public bool TotallyReady;

	private float playersRefreshRate = 10f;

	private float nextRefreshTime;

	[HideInInspector]
	public int traileringRequesterViewID;
}
