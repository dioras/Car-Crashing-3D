using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ExitGames.Client.Photon;
using UnityEngine;

internal class NetworkingPeer : LoadBalancingPeer, IPhotonPeerListener
{
	public NetworkingPeer(string playername, ConnectionProtocol connectionProtocol) : base(connectionProtocol)
	{
		base.Listener = this;
		base.LimitOfUnreliableCommands = 40;
		this.lobby = TypedLobby.Default;
		this.PlayerName = playername;
		this.LocalPlayer = new PhotonPlayer(true, -1, this.playername);
		this.AddNewPlayer(this.LocalPlayer.ID, this.LocalPlayer);
		this.rpcShortcuts = new Dictionary<string, int>(PhotonNetwork.PhotonServerSettings.RpcList.Count);
		for (int i = 0; i < PhotonNetwork.PhotonServerSettings.RpcList.Count; i++)
		{
			string key = PhotonNetwork.PhotonServerSettings.RpcList[i];
			this.rpcShortcuts[key] = i;
		}
		this.State = ClientState.PeerCreated;
	}

	protected internal string AppVersion
	{
		get
		{
			return string.Format("{0}_{1}", PhotonNetwork.gameVersion, "1.87");
		}
	}

	public AuthenticationValues AuthValues { get; set; }

	private string TokenForInit
	{
		get
		{
			if (this.AuthMode == AuthModeOption.Auth)
			{
				return null;
			}
			return (this.AuthValues == null) ? null : this.AuthValues.Token;
		}
	}

	public bool IsUsingNameServer { get; protected internal set; }

	public string NameServerAddress
	{
		get
		{
			return this.GetNameServerAddress();
		}
	}

	public string MasterServerAddress { get; protected internal set; }

	public string GameServerAddress { get; protected internal set; }

	protected internal ServerConnection Server { get; private set; }

	public ClientState State { get; internal set; }

	public TypedLobby lobby { get; set; }

	private bool requestLobbyStatistics
	{
		get
		{
			return PhotonNetwork.EnableLobbyStatistics && this.Server == ServerConnection.MasterServer;
		}
	}

	public string PlayerName
	{
		get
		{
			return this.playername;
		}
		set
		{
			if (string.IsNullOrEmpty(value) || value.Equals(this.playername))
			{
				return;
			}
			if (this.LocalPlayer != null)
			{
				this.LocalPlayer.NickName = value;
			}
			this.playername = value;
			if (this.CurrentRoom != null)
			{
				this.SendPlayerName();
			}
		}
	}

	public Room CurrentRoom
	{
		get
		{
			if (this.currentRoom != null && this.currentRoom.IsLocalClientInside)
			{
				return this.currentRoom;
			}
			return null;
		}
		private set
		{
			this.currentRoom = value;
		}
	}

	public PhotonPlayer LocalPlayer { get; internal set; }

	public int PlayersOnMasterCount { get; internal set; }

	public int PlayersInRoomsCount { get; internal set; }

	public int RoomsCount { get; internal set; }

	protected internal int FriendListAge
	{
		get
		{
			return (!this.isFetchingFriendList && this.friendListTimestamp != 0) ? (Environment.TickCount - this.friendListTimestamp) : 0;
		}
	}

	public bool IsAuthorizeSecretAvailable
	{
		get
		{
			return this.AuthValues != null && !string.IsNullOrEmpty(this.AuthValues.Token);
		}
	}

	public List<Region> AvailableRegions { get; protected internal set; }

	public CloudRegionCode CloudRegion { get; protected internal set; }

	public int mMasterClientId
	{
		get
		{
			if (PhotonNetwork.offlineMode)
			{
				return this.LocalPlayer.ID;
			}
			return (this.CurrentRoom != null) ? this.CurrentRoom.MasterClientId : 0;
		}
		private set
		{
			if (this.CurrentRoom != null)
			{
				this.CurrentRoom.MasterClientId = value;
			}
		}
	}

	private string GetNameServerAddress()
	{
		ConnectionProtocol transportProtocol = base.TransportProtocol;
		int num = 0;
		NetworkingPeer.ProtocolToNameServerPort.TryGetValue(transportProtocol, out num);
		string arg = string.Empty;
		if (transportProtocol == ConnectionProtocol.WebSocket)
		{
			arg = "ws://";
		}
		else if (transportProtocol == ConnectionProtocol.WebSocketSecure)
		{
			arg = "wss://";
		}
		return string.Format("{0}{1}:{2}", arg, "ns.exitgames.com", num);
	}

	public override bool Connect(string serverAddress, string applicationName)
	{
		UnityEngine.Debug.LogError("Avoid using this directly. Thanks.");
		return false;
	}

	public bool ReconnectToMaster()
	{
		if (this.AuthValues == null)
		{
			UnityEngine.Debug.LogWarning("ReconnectToMaster() with AuthValues == null is not correct!");
			this.AuthValues = new AuthenticationValues();
		}
		this.AuthValues.Token = this.tokenCache;
		return this.Connect(this.MasterServerAddress, ServerConnection.MasterServer);
	}

	public bool ReconnectAndRejoin()
	{
		if (this.AuthValues == null)
		{
			UnityEngine.Debug.LogWarning("ReconnectAndRejoin() with AuthValues == null is not correct!");
			this.AuthValues = new AuthenticationValues();
		}
		this.AuthValues.Token = this.tokenCache;
		if (!string.IsNullOrEmpty(this.GameServerAddress) && this.enterRoomParamsCache != null)
		{
			this.lastJoinType = JoinType.JoinRoom;
			this.enterRoomParamsCache.RejoinOnly = true;
			return this.Connect(this.GameServerAddress, ServerConnection.GameServer);
		}
		return false;
	}

	public bool Connect(string serverAddress, ServerConnection type)
	{
		if (PhotonHandler.AppQuits)
		{
			UnityEngine.Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		if (this.State == ClientState.Disconnecting)
		{
			UnityEngine.Debug.LogError("Connect() failed. Can't connect while disconnecting (still). Current state: " + PhotonNetwork.connectionStateDetailed);
			return false;
		}
		this.cachedProtocolType = type;
		this.cachedServerAddress = serverAddress;
		this.cachedApplicationName = string.Empty;
		this.SetupProtocol(type);
		bool flag = base.Connect(serverAddress, string.Empty, this.TokenForInit);
		if (flag)
		{
			if (type != ServerConnection.NameServer)
			{
				if (type != ServerConnection.MasterServer)
				{
					if (type == ServerConnection.GameServer)
					{
						this.State = ClientState.ConnectingToGameserver;
					}
				}
				else
				{
					this.State = ClientState.ConnectingToMasterserver;
				}
			}
			else
			{
				this.State = ClientState.ConnectingToNameServer;
			}
		}
		return flag;
	}

	private bool Reconnect()
	{
		this._isReconnecting = true;
		PhotonNetwork.SwitchToProtocol(PhotonNetwork.PhotonServerSettings.Protocol);
		this.SetupProtocol(this.cachedProtocolType);
		bool flag = base.Connect(this.cachedServerAddress, this.cachedApplicationName, this.TokenForInit);
		if (flag)
		{
			ServerConnection serverConnection = this.cachedProtocolType;
			if (serverConnection != ServerConnection.NameServer)
			{
				if (serverConnection != ServerConnection.MasterServer)
				{
					if (serverConnection == ServerConnection.GameServer)
					{
						this.State = ClientState.ConnectingToGameserver;
					}
				}
				else
				{
					this.State = ClientState.ConnectingToMasterserver;
				}
			}
			else
			{
				this.State = ClientState.ConnectingToNameServer;
			}
		}
		return flag;
	}

	public bool ConnectToNameServer()
	{
		if (PhotonHandler.AppQuits)
		{
			UnityEngine.Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		this.IsUsingNameServer = true;
		this.CloudRegion = CloudRegionCode.none;
		if (this.State == ClientState.ConnectedToNameServer)
		{
			return true;
		}
		this.SetupProtocol(ServerConnection.NameServer);
		this.cachedProtocolType = ServerConnection.NameServer;
		this.cachedServerAddress = this.NameServerAddress;
		this.cachedApplicationName = "ns";
		if (!base.Connect(this.NameServerAddress, "ns", this.TokenForInit))
		{
			return false;
		}
		this.State = ClientState.ConnectingToNameServer;
		return true;
	}

	public bool ConnectToRegionMaster(CloudRegionCode region)
	{
		if (PhotonHandler.AppQuits)
		{
			UnityEngine.Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
			return false;
		}
		this.IsUsingNameServer = true;
		this.CloudRegion = region;
		if (this.State == ClientState.ConnectedToNameServer)
		{
			return this.CallAuthenticate();
		}
		this.cachedProtocolType = ServerConnection.NameServer;
		this.cachedServerAddress = this.NameServerAddress;
		this.cachedApplicationName = "ns";
		this.SetupProtocol(ServerConnection.NameServer);
		if (!base.Connect(this.NameServerAddress, "ns", this.TokenForInit))
		{
			return false;
		}
		this.State = ClientState.ConnectingToNameServer;
		return true;
	}

	protected internal void SetupProtocol(ServerConnection serverType)
	{
		ConnectionProtocol connectionProtocol = base.TransportProtocol;
		if (this.AuthMode == AuthModeOption.AuthOnceWss)
		{
			if (serverType != ServerConnection.NameServer)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
				{
					UnityEngine.Debug.LogWarning("Using PhotonServerSettings.Protocol when leaving the NameServer (AuthMode is AuthOnceWss): " + PhotonNetwork.PhotonServerSettings.Protocol);
				}
				connectionProtocol = PhotonNetwork.PhotonServerSettings.Protocol;
			}
			else
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
				{
					UnityEngine.Debug.LogWarning("Using WebSocket to connect NameServer (AuthMode is AuthOnceWss).");
				}
				connectionProtocol = ConnectionProtocol.WebSocketSecure;
			}
		}
		Type type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
		if (type == null)
		{
			type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
		}
		if (type != null)
		{
			this.SocketImplementationConfig[ConnectionProtocol.WebSocket] = type;
			this.SocketImplementationConfig[ConnectionProtocol.WebSocketSecure] = type;
		}
		if (PhotonHandler.PingImplementation == null)
		{
			PhotonHandler.PingImplementation = typeof(PingMono);
		}
		if (base.TransportProtocol == connectionProtocol)
		{
			return;
		}
		if (PhotonNetwork.logLevel >= PhotonLogLevel.ErrorsOnly)
		{
			UnityEngine.Debug.LogWarning(string.Concat(new object[]
			{
				"Protocol switch from: ",
				base.TransportProtocol,
				" to: ",
				connectionProtocol,
				"."
			}));
		}
		base.TransportProtocol = connectionProtocol;
	}

	public override void Disconnect()
	{
		if (base.PeerState == PeerStateValue.Disconnected)
		{
			if (!PhotonHandler.AppQuits)
			{
				UnityEngine.Debug.LogWarning(string.Format("Can't execute Disconnect() while not connected. Nothing changed. State: {0}", this.State));
			}
			return;
		}
		this.State = ClientState.Disconnecting;
		base.Disconnect();
	}

	private bool CallAuthenticate()
	{
		AuthenticationValues authenticationValues;
		if ((authenticationValues = this.AuthValues) == null)
		{
			authenticationValues = new AuthenticationValues
			{
				UserId = this.PlayerName
			};
		}
		AuthenticationValues authValues = authenticationValues;
		if (this.AuthMode == AuthModeOption.Auth)
		{
			return this.OpAuthenticate(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.requestLobbyStatistics);
		}
		return this.OpAuthenticateOnce(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.EncryptionMode, PhotonNetwork.PhotonServerSettings.Protocol);
	}

	private void DisconnectToReconnect()
	{
		ServerConnection server = this.Server;
		if (server != ServerConnection.NameServer)
		{
			if (server != ServerConnection.MasterServer)
			{
				if (server == ServerConnection.GameServer)
				{
					this.State = ClientState.DisconnectingFromGameserver;
					base.Disconnect();
				}
			}
			else
			{
				this.State = ClientState.DisconnectingFromMasterserver;
				base.Disconnect();
			}
		}
		else
		{
			this.State = ClientState.DisconnectingFromNameServer;
			base.Disconnect();
		}
	}

	public bool GetRegions()
	{
		if (this.Server != ServerConnection.NameServer)
		{
			return false;
		}
		bool flag = this.OpGetRegions(this.AppId);
		if (flag)
		{
			this.AvailableRegions = null;
		}
		return flag;
	}

	public override bool OpFindFriends(string[] friendsToFind)
	{
		if (this.isFetchingFriendList)
		{
			return false;
		}
		this.friendListRequested = friendsToFind;
		this.isFetchingFriendList = true;
		return base.OpFindFriends(friendsToFind);
	}

	public bool OpCreateGame(EnterRoomParams enterRoomParams)
	{
		bool flag = this.Server == ServerConnection.GameServer;
		enterRoomParams.OnGameServer = flag;
		enterRoomParams.PlayerProperties = this.GetLocalActorProperties();
		if (!flag)
		{
			this.enterRoomParamsCache = enterRoomParams;
		}
		this.lastJoinType = JoinType.CreateRoom;
		return base.OpCreateRoom(enterRoomParams);
	}

	public override bool OpJoinRoom(EnterRoomParams opParams)
	{
		bool flag = this.Server == ServerConnection.GameServer;
		opParams.OnGameServer = flag;
		if (!flag)
		{
			this.enterRoomParamsCache = opParams;
		}
		this.lastJoinType = ((!opParams.CreateIfNotExists) ? JoinType.JoinRoom : JoinType.JoinOrCreateRoom);
		return base.OpJoinRoom(opParams);
	}

	public override bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams)
	{
		this.enterRoomParamsCache = new EnterRoomParams();
		this.enterRoomParamsCache.Lobby = opJoinRandomRoomParams.TypedLobby;
		this.enterRoomParamsCache.ExpectedUsers = opJoinRandomRoomParams.ExpectedUsers;
		this.lastJoinType = JoinType.JoinRandomRoom;
		return base.OpJoinRandomRoom(opJoinRandomRoomParams);
	}

	public virtual bool OpLeave()
	{
		if (this.State != ClientState.Joined)
		{
			UnityEngine.Debug.LogWarning("Not sending leave operation. State is not 'Joined': " + this.State);
			return false;
		}
		return this.OpCustom(254, null, true, 0);
	}

	public override bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
	{
		return !PhotonNetwork.offlineMode && base.OpRaiseEvent(eventCode, customEventContent, sendReliable, raiseEventOptions);
	}

	private void ReadoutProperties(ExitGames.Client.Photon.Hashtable gameProperties, ExitGames.Client.Photon.Hashtable pActorProperties, int targetActorNr)
	{
		if (pActorProperties != null && pActorProperties.Count > 0)
		{
			if (targetActorNr > 0)
			{
				PhotonPlayer playerWithId = this.GetPlayerWithId(targetActorNr);
				if (playerWithId != null)
				{
					ExitGames.Client.Photon.Hashtable hashtable = this.ReadoutPropertiesForActorNr(pActorProperties, targetActorNr);
					playerWithId.InternalCacheProperties(hashtable);
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, new object[]
					{
						playerWithId,
						hashtable
					});
				}
			}
			else
			{
				foreach (object obj in pActorProperties.Keys)
				{
					int num = (int)obj;
					ExitGames.Client.Photon.Hashtable hashtable2 = (ExitGames.Client.Photon.Hashtable)pActorProperties[obj];
					string name = (string)hashtable2[byte.MaxValue];
					PhotonPlayer photonPlayer = this.GetPlayerWithId(num);
					if (photonPlayer == null)
					{
						photonPlayer = new PhotonPlayer(false, num, name);
						this.AddNewPlayer(num, photonPlayer);
					}
					photonPlayer.InternalCacheProperties(hashtable2);
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, new object[]
					{
						photonPlayer,
						hashtable2
					});
				}
			}
		}
		if (this.CurrentRoom != null && gameProperties != null)
		{
			this.CurrentRoom.InternalCacheProperties(gameProperties);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, new object[]
			{
				gameProperties
			});
			if (PhotonNetwork.automaticallySyncScene)
			{
				this.LoadLevelIfSynced();
			}
		}
	}

	private ExitGames.Client.Photon.Hashtable ReadoutPropertiesForActorNr(ExitGames.Client.Photon.Hashtable actorProperties, int actorNr)
	{
		if (actorProperties.ContainsKey(actorNr))
		{
			return (ExitGames.Client.Photon.Hashtable)actorProperties[actorNr];
		}
		return actorProperties;
	}

	public void ChangeLocalID(int newID)
	{
		if (this.LocalPlayer == null)
		{
			UnityEngine.Debug.LogWarning(string.Format("LocalPlayer is null or not in mActors! LocalPlayer: {0} mActors==null: {1} newID: {2}", this.LocalPlayer, this.mActors == null, newID));
		}
		if (this.mActors.ContainsKey(this.LocalPlayer.ID))
		{
			this.mActors.Remove(this.LocalPlayer.ID);
		}
		this.LocalPlayer.InternalChangeLocalID(newID);
		this.mActors[this.LocalPlayer.ID] = this.LocalPlayer;
		this.RebuildPlayerListCopies();
	}

	private void LeftLobbyCleanup()
	{
		this.mGameList = new Dictionary<string, RoomInfo>();
		this.mGameListCopy = new RoomInfo[0];
		if (this.insideLobby)
		{
			this.insideLobby = false;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftLobby, new object[0]);
		}
	}

	private void LeftRoomCleanup()
	{
		bool flag = this.CurrentRoom != null;
		bool flag2 = (this.CurrentRoom == null) ? PhotonNetwork.autoCleanUpPlayerObjects : this.CurrentRoom.AutoCleanUp;
		this.hasSwitchedMC = false;
		this.CurrentRoom = null;
		this.mActors = new Dictionary<int, PhotonPlayer>();
		this.mPlayerListCopy = new PhotonPlayer[0];
		this.mOtherPlayerListCopy = new PhotonPlayer[0];
		this.allowedReceivingGroups = new HashSet<byte>();
		this.blockSendingGroups = new HashSet<byte>();
		this.mGameList = new Dictionary<string, RoomInfo>();
		this.mGameListCopy = new RoomInfo[0];
		this.isFetchingFriendList = false;
		this.ChangeLocalID(-1);
		if (flag2)
		{
			this.LocalCleanupAnythingInstantiated(true);
			PhotonNetwork.manuallyAllocatedViewIds = new List<int>();
		}
		if (flag)
		{
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom, new object[0]);
		}
	}

	protected internal void LocalCleanupAnythingInstantiated(bool destroyInstantiatedGameObjects)
	{
		if (this.tempInstantiationData.Count > 0)
		{
			UnityEngine.Debug.LogWarning("It seems some instantiation is not completed, as instantiation data is used. You should make sure instantiations are paused when calling this method. Cleaning now, despite this.");
		}
		if (destroyInstantiatedGameObjects)
		{
			HashSet<GameObject> hashSet = new HashSet<GameObject>();
			foreach (PhotonView photonView in this.photonViewList.Values)
			{
				if (photonView.isRuntimeInstantiated)
				{
					hashSet.Add(photonView.gameObject);
				}
			}
			foreach (GameObject go in hashSet)
			{
				this.RemoveInstantiatedGO(go, true);
			}
		}
		this.tempInstantiationData.Clear();
		PhotonNetwork.lastUsedViewSubId = 0;
		PhotonNetwork.lastUsedViewSubIdStatic = 0;
	}

	private void GameEnteredOnGameServer(OperationResponse operationResponse)
	{
		if (operationResponse.ReturnCode != 0)
		{
			byte operationCode = operationResponse.OperationCode;
			if (operationCode != 227)
			{
				if (operationCode != 226)
				{
					if (operationCode == 225)
					{
						if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
						{
							UnityEngine.Debug.Log("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
							if (operationResponse.ReturnCode == 32758)
							{
								UnityEngine.Debug.Log("Most likely the game became empty during the switch to GameServer.");
							}
						}
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, new object[]
						{
							operationResponse.ReturnCode,
							operationResponse.DebugMessage
						});
					}
				}
				else
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						UnityEngine.Debug.Log("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
						if (operationResponse.ReturnCode == 32758)
						{
							UnityEngine.Debug.Log("Most likely the game became empty during the switch to GameServer.");
						}
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, new object[]
					{
						operationResponse.ReturnCode,
						operationResponse.DebugMessage
					});
				}
			}
			else
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					UnityEngine.Debug.Log("Create failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
			}
			this.DisconnectToReconnect();
			return;
		}
		this.CurrentRoom = new Room(this.enterRoomParamsCache.RoomName, null)
		{
			IsLocalClientInside = true
		};
		this.State = ClientState.Joined;
		if (operationResponse.Parameters.ContainsKey(252))
		{
			int[] actorsInRoom = (int[])operationResponse.Parameters[252];
			this.UpdatedActorList(actorsInRoom);
		}
		int newID = (int)operationResponse[254];
		this.ChangeLocalID(newID);
		ExitGames.Client.Photon.Hashtable pActorProperties = (ExitGames.Client.Photon.Hashtable)operationResponse[249];
		ExitGames.Client.Photon.Hashtable gameProperties = (ExitGames.Client.Photon.Hashtable)operationResponse[248];
		this.ReadoutProperties(gameProperties, pActorProperties, 0);
		if (!this.CurrentRoom.serverSideMasterClient)
		{
			this.CheckMasterClient(-1);
		}
		if (this.mPlayernameHasToBeUpdated)
		{
			this.SendPlayerName();
		}
		byte operationCode2 = operationResponse.OperationCode;
		if (operationCode2 != 227)
		{
			if (operationCode2 != 226 && operationCode2 != 225)
			{
			}
		}
		else
		{
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom, new object[0]);
		}
	}

	private void AddNewPlayer(int ID, PhotonPlayer player)
	{
		if (!this.mActors.ContainsKey(ID))
		{
			this.mActors[ID] = player;
			this.RebuildPlayerListCopies();
		}
		else
		{
			UnityEngine.Debug.LogError("Adding player twice: " + ID);
		}
	}

	private void RemovePlayer(int ID, PhotonPlayer player)
	{
		this.mActors.Remove(ID);
		if (!player.IsLocal)
		{
			this.RebuildPlayerListCopies();
		}
	}

	private void RebuildPlayerListCopies()
	{
		this.mPlayerListCopy = new PhotonPlayer[this.mActors.Count];
		this.mActors.Values.CopyTo(this.mPlayerListCopy, 0);
		List<PhotonPlayer> list = new List<PhotonPlayer>();
		for (int i = 0; i < this.mPlayerListCopy.Length; i++)
		{
			PhotonPlayer photonPlayer = this.mPlayerListCopy[i];
			if (!photonPlayer.IsLocal)
			{
				list.Add(photonPlayer);
			}
		}
		this.mOtherPlayerListCopy = list.ToArray();
	}

	private void ResetPhotonViewsOnSerialize()
	{
		foreach (PhotonView photonView in this.photonViewList.Values)
		{
			photonView.lastOnSerializeDataSent = null;
		}
	}

	private void HandleEventLeave(int actorID, EventData evLeave)
	{
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"HandleEventLeave for player ID: ",
				actorID,
				" evLeave: ",
				evLeave.ToStringFull()
			}));
		}
		PhotonPlayer playerWithId = this.GetPlayerWithId(actorID);
		if (playerWithId == null)
		{
			UnityEngine.Debug.LogError(string.Format("Received event Leave for unknown player ID: {0}", actorID));
			return;
		}
		bool isInactive = playerWithId.IsInactive;
		if (evLeave.Parameters.ContainsKey(233))
		{
			playerWithId.IsInactive = (bool)evLeave.Parameters[233];
			if (playerWithId.IsInactive != isInactive)
			{
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerActivityChanged, new object[]
				{
					playerWithId
				});
			}
			if (playerWithId.IsInactive && isInactive)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new object[]
				{
					"HandleEventLeave for player ID: ",
					actorID,
					" isInactive: ",
					playerWithId.IsInactive,
					". Stopping handling if inactive."
				}));
				return;
			}
		}
		if (evLeave.Parameters.ContainsKey(203))
		{
			int num = (int)evLeave[203];
			if (num != 0)
			{
				this.mMasterClientId = (int)evLeave[203];
				this.UpdateMasterClient();
			}
		}
		else if (!this.CurrentRoom.serverSideMasterClient)
		{
			this.CheckMasterClient(actorID);
		}
		if (playerWithId.IsInactive && !isInactive)
		{
			return;
		}
		if (this.CurrentRoom != null && this.CurrentRoom.AutoCleanUp)
		{
			this.DestroyPlayerObjects(actorID, true);
		}
		this.RemovePlayer(actorID, playerWithId);
		NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerDisconnected, new object[]
		{
			playerWithId
		});
	}

	private void CheckMasterClient(int leavingPlayerId)
	{
		bool flag = this.mMasterClientId == leavingPlayerId;
		bool flag2 = leavingPlayerId > 0;
		if (flag2 && !flag)
		{
			return;
		}
		int num;
		if (this.mActors.Count <= 1)
		{
			num = this.LocalPlayer.ID;
		}
		else
		{
			num = int.MaxValue;
			foreach (int num2 in this.mActors.Keys)
			{
				if (num2 < num && num2 != leavingPlayerId)
				{
					num = num2;
				}
			}
		}
		this.mMasterClientId = num;
		if (flag2)
		{
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, new object[]
			{
				this.GetPlayerWithId(num)
			});
		}
	}

	protected internal void UpdateMasterClient()
	{
		NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, new object[]
		{
			PhotonNetwork.masterClient
		});
	}

	private static int ReturnLowestPlayerId(PhotonPlayer[] players, int playerIdToIgnore)
	{
		if (players == null || players.Length == 0)
		{
			return -1;
		}
		int num = int.MaxValue;
		foreach (PhotonPlayer photonPlayer in players)
		{
			if (photonPlayer.ID != playerIdToIgnore)
			{
				if (photonPlayer.ID < num)
				{
					num = photonPlayer.ID;
				}
			}
		}
		return num;
	}

	protected internal bool SetMasterClient(int playerId, bool sync)
	{
		bool flag = this.mMasterClientId != playerId;
		if (!flag || !this.mActors.ContainsKey(playerId))
		{
			return false;
		}
		if (sync && !this.OpRaiseEvent(208, new ExitGames.Client.Photon.Hashtable
		{
			{
				1,
				playerId
			}
		}, true, null))
		{
			return false;
		}
		this.hasSwitchedMC = true;
		this.CurrentRoom.MasterClientId = playerId;
		NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, new object[]
		{
			this.GetPlayerWithId(playerId)
		});
		return true;
	}

	public bool SetMasterClient(int nextMasterId)
	{
		ExitGames.Client.Photon.Hashtable gameProperties = new ExitGames.Client.Photon.Hashtable
		{
			{
				248,
				nextMasterId
			}
		};
		ExitGames.Client.Photon.Hashtable expectedProperties = new ExitGames.Client.Photon.Hashtable
		{
			{
				248,
				this.mMasterClientId
			}
		};
		return base.OpSetPropertiesOfRoom(gameProperties, expectedProperties, false);
	}

	protected internal PhotonPlayer GetPlayerWithId(int number)
	{
		if (this.mActors == null)
		{
			return null;
		}
		PhotonPlayer result = null;
		this.mActors.TryGetValue(number, out result);
		return result;
	}

	private void SendPlayerName()
	{
		if (this.State == ClientState.Joining)
		{
			this.mPlayernameHasToBeUpdated = true;
			return;
		}
		if (this.LocalPlayer != null)
		{
			this.LocalPlayer.NickName = this.PlayerName;
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable[byte.MaxValue] = this.PlayerName;
			if (this.LocalPlayer.ID > 0)
			{
				base.OpSetPropertiesOfActor(this.LocalPlayer.ID, hashtable, null, false);
				this.mPlayernameHasToBeUpdated = false;
			}
		}
	}

	private ExitGames.Client.Photon.Hashtable GetLocalActorProperties()
	{
		if (PhotonNetwork.player != null)
		{
			return PhotonNetwork.player.AllProperties;
		}
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[byte.MaxValue] = this.PlayerName;
		return hashtable;
	}

	public void DebugReturn(DebugLevel level, string message)
	{
		if (level == DebugLevel.ERROR)
		{
			UnityEngine.Debug.LogError(message);
		}
		else if (level == DebugLevel.WARNING)
		{
			UnityEngine.Debug.LogWarning(message);
		}
		else if (level == DebugLevel.INFO && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			UnityEngine.Debug.Log(message);
		}
		else if (level == DebugLevel.ALL && PhotonNetwork.logLevel == PhotonLogLevel.Full)
		{
			UnityEngine.Debug.Log(message);
		}
	}

	public void OnOperationResponse(OperationResponse operationResponse)
	{
		if (PhotonNetwork.networkingPeer.State == ClientState.Disconnecting)
		{
			if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
			{
				UnityEngine.Debug.Log("OperationResponse ignored while disconnecting. Code: " + operationResponse.OperationCode);
			}
			return;
		}
		if (operationResponse.ReturnCode == 0)
		{
			if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
			{
				UnityEngine.Debug.Log(operationResponse.ToString());
			}
		}
		else if (operationResponse.ReturnCode == -3)
		{
			UnityEngine.Debug.LogError("Operation " + operationResponse.OperationCode + " could not be executed (yet). Wait for state JoinedLobby or ConnectedToMaster and their callbacks before calling operations. WebRPCs need a server-side configuration. Enum OperationCode helps identify the operation.");
		}
		else if (operationResponse.ReturnCode == 32752)
		{
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"Operation ",
				operationResponse.OperationCode,
				" failed in a server-side plugin. Check the configuration in the Dashboard. Message from server-plugin: ",
				operationResponse.DebugMessage
			}));
		}
		else if (operationResponse.ReturnCode == 32760)
		{
			UnityEngine.Debug.LogWarning("Operation failed: " + operationResponse.ToStringFull());
		}
		else
		{
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"Operation failed: ",
				operationResponse.ToStringFull(),
				" Server: ",
				this.Server
			}));
		}
		if (operationResponse.Parameters.ContainsKey(221))
		{
			if (this.AuthValues == null)
			{
				this.AuthValues = new AuthenticationValues();
			}
			this.AuthValues.Token = (operationResponse[221] as string);
			this.tokenCache = this.AuthValues.Token;
		}
		byte operationCode = operationResponse.OperationCode;
		switch (operationCode)
		{
		case 217:
			if (operationResponse.ReturnCode != 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "GetGameList failed: " + operationResponse.ToStringFull());
			}
			else
			{
				this.mGameList = new Dictionary<string, RoomInfo>();
				ExitGames.Client.Photon.Hashtable hashtable = (ExitGames.Client.Photon.Hashtable)operationResponse[222];
				foreach (object obj in hashtable.Keys)
				{
					string text = (string)obj;
					this.mGameList[text] = new RoomInfo(text, (ExitGames.Client.Photon.Hashtable)hashtable[obj]);
				}
				this.mGameListCopy = new RoomInfo[this.mGameList.Count];
				this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate, new object[0]);
			}
			break;
		default:
			switch (operationCode)
			{
			case 251:
			{
				ExitGames.Client.Photon.Hashtable pActorProperties = (ExitGames.Client.Photon.Hashtable)operationResponse[249];
				ExitGames.Client.Photon.Hashtable gameProperties = (ExitGames.Client.Photon.Hashtable)operationResponse[248];
				this.ReadoutProperties(gameProperties, pActorProperties, 0);
				break;
			}
			case 252:
				break;
			case 253:
				break;
			case 254:
				this.DisconnectToReconnect();
				break;
			default:
				UnityEngine.Debug.LogWarning(string.Format("OperationResponse unhandled: {0}", operationResponse.ToString()));
				break;
			}
			break;
		case 219:
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnWebRpcResponse, new object[]
			{
				operationResponse
			});
			break;
		case 220:
			if (operationResponse.ReturnCode == 32767)
			{
				UnityEngine.Debug.LogError(string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account.", new object[0]));
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
				{
					DisconnectCause.InvalidAuthentication
				});
				this.State = ClientState.Disconnecting;
				this.Disconnect();
			}
			else if (operationResponse.ReturnCode != 0)
			{
				UnityEngine.Debug.LogError(string.Concat(new object[]
				{
					"GetRegions failed. Can't provide regions list. Error: ",
					operationResponse.ReturnCode,
					": ",
					operationResponse.DebugMessage
				}));
			}
			else
			{
				string[] array = operationResponse[210] as string[];
				string[] array2 = operationResponse[230] as string[];
				if (array == null || array2 == null || array.Length != array2.Length)
				{
					UnityEngine.Debug.LogError(string.Concat(new object[]
					{
						"The region arrays from Name Server are not ok. Must be non-null and same length. ",
						array == null,
						" ",
						array2 == null,
						"\n",
						operationResponse.ToStringFull()
					}));
				}
				else
				{
					this.AvailableRegions = new List<Region>(array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						string text2 = array[i];
						if (!string.IsNullOrEmpty(text2))
						{
							text2 = text2.ToLower();
							CloudRegionCode cloudRegionCode = Region.Parse(text2);
							bool flag = true;
							if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.BestRegion && PhotonNetwork.PhotonServerSettings.EnabledRegions != (CloudRegionFlag)0)
							{
								CloudRegionFlag cloudRegionFlag = Region.ParseFlag(cloudRegionCode);
								flag = ((PhotonNetwork.PhotonServerSettings.EnabledRegions & cloudRegionFlag) != (CloudRegionFlag)0);
								if (!flag && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
								{
									UnityEngine.Debug.Log("Skipping region because it's not in PhotonServerSettings.EnabledRegions: " + cloudRegionCode);
								}
							}
							if (flag)
							{
								this.AvailableRegions.Add(new Region(cloudRegionCode, text2, array2[i]));
							}
						}
					}
					if (PhotonNetwork.PhotonServerSettings.HostType == ServerSettings.HostingOption.BestRegion)
					{
						PhotonHandler.PingAvailableRegionsAndConnectToBest();
					}
				}
			}
			break;
		case 222:
		{
			bool[] array3 = operationResponse[1] as bool[];
			string[] array4 = operationResponse[2] as string[];
			if (array3 != null && array4 != null && this.friendListRequested != null && array3.Length == this.friendListRequested.Length)
			{
				List<FriendInfo> list = new List<FriendInfo>(this.friendListRequested.Length);
				for (int j = 0; j < this.friendListRequested.Length; j++)
				{
					list.Insert(j, new FriendInfo
					{
						Name = this.friendListRequested[j],
						Room = array4[j],
						IsOnline = array3[j]
					});
				}
				PhotonNetwork.Friends = list;
			}
			else
			{
				UnityEngine.Debug.LogError("FindFriends failed to apply the result, as a required value wasn't provided or the friend list length differed from result.");
			}
			this.friendListRequested = null;
			this.isFetchingFriendList = false;
			this.friendListTimestamp = Environment.TickCount;
			if (this.friendListTimestamp == 0)
			{
				this.friendListTimestamp = 1;
			}
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnUpdatedFriendList, new object[0]);
			break;
		}
		case 225:
			if (operationResponse.ReturnCode != 0)
			{
				if (operationResponse.ReturnCode == 32760)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
					{
						UnityEngine.Debug.Log("JoinRandom failed: No open game. Calling: OnPhotonRandomJoinFailed() and staying on master server.");
					}
				}
				else if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					UnityEngine.Debug.LogWarning(string.Format("JoinRandom failed: {0}.", operationResponse.ToStringFull()));
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
			}
			else
			{
				string roomName = (string)operationResponse[byte.MaxValue];
				this.enterRoomParamsCache.RoomName = roomName;
				this.GameServerAddress = (string)operationResponse[230];
				this.DisconnectToReconnect();
			}
			break;
		case 226:
			if (this.Server != ServerConnection.GameServer)
			{
				if (operationResponse.ReturnCode != 0)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						UnityEngine.Debug.Log(string.Format("JoinRoom failed (room maybe closed by now). Client stays on masterserver: {0}. State: {1}", operationResponse.ToStringFull(), this.State));
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, new object[]
					{
						operationResponse.ReturnCode,
						operationResponse.DebugMessage
					});
				}
				else
				{
					this.GameServerAddress = (string)operationResponse[230];
					this.DisconnectToReconnect();
				}
			}
			else
			{
				this.GameEnteredOnGameServer(operationResponse);
			}
			break;
		case 227:
			if (this.Server == ServerConnection.GameServer)
			{
				this.GameEnteredOnGameServer(operationResponse);
			}
			else if (operationResponse.ReturnCode != 0)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					UnityEngine.Debug.LogWarning(string.Format("CreateRoom failed, client stays on masterserver: {0}.", operationResponse.ToStringFull()));
				}
				this.State = ((!this.insideLobby) ? ClientState.ConnectedToMaster : ClientState.JoinedLobby);
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, new object[]
				{
					operationResponse.ReturnCode,
					operationResponse.DebugMessage
				});
			}
			else
			{
				string text3 = (string)operationResponse[byte.MaxValue];
				if (!string.IsNullOrEmpty(text3))
				{
					this.enterRoomParamsCache.RoomName = text3;
				}
				this.GameServerAddress = (string)operationResponse[230];
				this.DisconnectToReconnect();
			}
			break;
		case 228:
			this.State = ClientState.Authenticated;
			this.LeftLobbyCleanup();
			break;
		case 229:
			this.State = ClientState.JoinedLobby;
			this.insideLobby = true;
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedLobby, new object[0]);
			break;
		case 230:
		case 231:
			if (operationResponse.ReturnCode != 0)
			{
				if (operationResponse.ReturnCode == -2)
				{
					UnityEngine.Debug.LogError(string.Format("If you host Photon yourself, make sure to start the 'Instance LoadBalancing' " + base.ServerAddress, new object[0]));
				}
				else if (operationResponse.ReturnCode == 32767)
				{
					UnityEngine.Debug.LogError(string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account.", new object[0]));
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
					{
						DisconnectCause.InvalidAuthentication
					});
				}
				else if (operationResponse.ReturnCode == 32755)
				{
					UnityEngine.Debug.LogError(string.Format("Custom Authentication failed (either due to user-input or configuration or AuthParameter string format). Calling: OnCustomAuthenticationFailed()", new object[0]));
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationFailed, new object[]
					{
						operationResponse.DebugMessage
					});
				}
				else
				{
					UnityEngine.Debug.LogError(string.Format("Authentication failed: '{0}' Code: {1}", operationResponse.DebugMessage, operationResponse.ReturnCode));
				}
				this.State = ClientState.Disconnecting;
				this.Disconnect();
				if (operationResponse.ReturnCode == 32757)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						UnityEngine.Debug.LogWarning(string.Format("Currently, the limit of users is reached for this title. Try again later. Disconnecting", new object[0]));
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonMaxCccuReached, new object[0]);
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
					{
						DisconnectCause.MaxCcuReached
					});
				}
				else if (operationResponse.ReturnCode == 32756)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						UnityEngine.Debug.LogError(string.Format("The used master server address is not available with the subscription currently used. Got to Photon Cloud Dashboard or change URL. Disconnecting.", new object[0]));
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
					{
						DisconnectCause.InvalidRegion
					});
				}
				else if (operationResponse.ReturnCode == 32753)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
					{
						UnityEngine.Debug.LogError(string.Format("The authentication ticket expired. You need to connect (and authenticate) again. Disconnecting.", new object[0]));
					}
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
					{
						DisconnectCause.AuthenticationTicketExpired
					});
				}
			}
			else
			{
				if (this.Server == ServerConnection.NameServer || this.Server == ServerConnection.MasterServer)
				{
					if (operationResponse.Parameters.ContainsKey(225))
					{
						string text4 = (string)operationResponse.Parameters[225];
						if (!string.IsNullOrEmpty(text4))
						{
							if (this.AuthValues == null)
							{
								this.AuthValues = new AuthenticationValues();
							}
							this.AuthValues.UserId = text4;
							PhotonNetwork.player.UserId = text4;
							if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
							{
								this.DebugReturn(DebugLevel.INFO, string.Format("Received your UserID from server. Updating local value to: {0}", text4));
							}
						}
					}
					if (operationResponse.Parameters.ContainsKey(202))
					{
						this.PlayerName = (string)operationResponse.Parameters[202];
						if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
						{
							this.DebugReturn(DebugLevel.INFO, string.Format("Received your NickName from server. Updating local value to: {0}", this.playername));
						}
					}
					if (operationResponse.Parameters.ContainsKey(192))
					{
						this.SetupEncryption((Dictionary<byte, object>)operationResponse.Parameters[192]);
					}
				}
				if (this.Server == ServerConnection.NameServer)
				{
					this.MasterServerAddress = (operationResponse[230] as string);
					this.DisconnectToReconnect();
				}
				else if (this.Server == ServerConnection.MasterServer)
				{
					if (this.AuthMode != AuthModeOption.Auth)
					{
						this.OpSettings(this.requestLobbyStatistics);
					}
					if (PhotonNetwork.autoJoinLobby)
					{
						this.State = ClientState.Authenticated;
						this.OpJoinLobby(this.lobby);
					}
					else
					{
						this.State = ClientState.ConnectedToMaster;
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster, new object[0]);
					}
				}
				else if (this.Server == ServerConnection.GameServer)
				{
					this.State = ClientState.Joining;
					this.enterRoomParamsCache.PlayerProperties = this.GetLocalActorProperties();
					this.enterRoomParamsCache.OnGameServer = true;
					if (this.lastJoinType == JoinType.JoinRoom || this.lastJoinType == JoinType.JoinRandomRoom || this.lastJoinType == JoinType.JoinOrCreateRoom)
					{
						this.OpJoinRoom(this.enterRoomParamsCache);
					}
					else if (this.lastJoinType == JoinType.CreateRoom)
					{
						this.OpCreateGame(this.enterRoomParamsCache);
					}
				}
				if (operationResponse.Parameters.ContainsKey(245))
				{
					Dictionary<string, object> dictionary = (Dictionary<string, object>)operationResponse.Parameters[245];
					if (dictionary != null)
					{
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationResponse, new object[]
						{
							dictionary
						});
					}
				}
			}
			break;
		}
	}

	public void OnStatusChanged(StatusCode statusCode)
	{
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			UnityEngine.Debug.Log(string.Format("OnStatusChanged: {0} current State: {1}", statusCode.ToString(), this.State));
		}
		switch (statusCode)
		{
		case StatusCode.ExceptionOnReceive:
		case StatusCode.DisconnectByServer:
		case StatusCode.DisconnectByServerUserLimit:
		case StatusCode.DisconnectByServerLogic:
			if (this.IsInitialConnect)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new object[]
				{
					statusCode,
					" while connecting to: ",
					base.ServerAddress,
					". Check if the server is available."
				}));
				this.IsInitialConnect = false;
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
				{
					disconnectCause
				});
			}
			else
			{
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
				{
					disconnectCause
				});
			}
			if (this.AuthValues != null)
			{
				this.AuthValues.Token = null;
			}
			this.Disconnect();
			return;
		case StatusCode.TimeoutDisconnect:
			if (this.IsInitialConnect)
			{
				if (!this._isReconnecting)
				{
					UnityEngine.Debug.LogWarning(string.Concat(new object[]
					{
						statusCode,
						" while connecting to: ",
						base.ServerAddress,
						". Check if the server is available."
					}));
					this.IsInitialConnect = false;
					DisconnectCause disconnectCause = (DisconnectCause)statusCode;
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
					{
						disconnectCause
					});
				}
			}
			else
			{
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
				{
					disconnectCause
				});
			}
			if (this.AuthValues != null)
			{
				this.AuthValues.Token = null;
			}
			this.Disconnect();
			return;
		default:
			switch (statusCode)
			{
			case StatusCode.SecurityExceptionOnConnect:
			case StatusCode.ExceptionOnConnect:
			{
				this.IsInitialConnect = false;
				this.State = ClientState.PeerCreated;
				if (this.AuthValues != null)
				{
					this.AuthValues.Token = null;
				}
				DisconnectCause disconnectCause = (DisconnectCause)statusCode;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
				{
					disconnectCause
				});
				return;
			}
			case StatusCode.Connect:
				if (this.State == ClientState.ConnectingToNameServer)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
					{
						UnityEngine.Debug.Log("Connected to NameServer.");
					}
					this.Server = ServerConnection.NameServer;
					if (this.AuthValues != null)
					{
						this.AuthValues.Token = null;
					}
				}
				if (this.State == ClientState.ConnectingToGameserver)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
					{
						UnityEngine.Debug.Log("Connected to gameserver.");
					}
					this.Server = ServerConnection.GameServer;
					this.State = ClientState.ConnectedToGameserver;
				}
				if (this.State == ClientState.ConnectingToMasterserver)
				{
					if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
					{
						UnityEngine.Debug.Log("Connected to masterserver.");
					}
					this.Server = ServerConnection.MasterServer;
					this.State = ClientState.Authenticating;
					if (this.IsInitialConnect)
					{
						this.IsInitialConnect = false;
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectedToPhoton, new object[0]);
					}
				}
				if (base.TransportProtocol != ConnectionProtocol.WebSocketSecure)
				{
					if (this.Server == ServerConnection.NameServer || this.AuthMode == AuthModeOption.Auth)
					{
						base.EstablishEncryption();
					}
					return;
				}
				if (this.DebugOut == DebugLevel.INFO)
				{
					UnityEngine.Debug.Log("Skipping EstablishEncryption. Protocol is secure.");
				}
				goto IL_1AC;
			case StatusCode.Disconnect:
				this.didAuthenticate = false;
				this.isFetchingFriendList = false;
				if (this.Server == ServerConnection.GameServer)
				{
					this.LeftRoomCleanup();
				}
				if (this.Server == ServerConnection.MasterServer)
				{
					this.LeftLobbyCleanup();
				}
				if (this.State == ClientState.DisconnectingFromMasterserver)
				{
					if (this.Connect(this.GameServerAddress, ServerConnection.GameServer))
					{
						this.State = ClientState.ConnectingToGameserver;
					}
				}
				else if (this.State == ClientState.DisconnectingFromGameserver || this.State == ClientState.DisconnectingFromNameServer)
				{
					this.SetupProtocol(ServerConnection.MasterServer);
					if (this.Connect(this.MasterServerAddress, ServerConnection.MasterServer))
					{
						this.State = ClientState.ConnectingToMasterserver;
					}
				}
				else
				{
					if (this._isReconnecting)
					{
						return;
					}
					if (this.AuthValues != null)
					{
						this.AuthValues.Token = null;
					}
					this.IsInitialConnect = false;
					this.State = ClientState.PeerCreated;
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnDisconnectedFromPhoton, new object[0]);
				}
				return;
			case StatusCode.Exception:
				if (this.IsInitialConnect)
				{
					UnityEngine.Debug.LogError("Exception while connecting to: " + base.ServerAddress + ". Check if the server is available.");
					if (base.ServerAddress == null || base.ServerAddress.StartsWith("127.0.0.1"))
					{
						UnityEngine.Debug.LogWarning("The server address is 127.0.0.1 (localhost): Make sure the server is running on this machine. Android and iOS emulators have their own localhost.");
						if (base.ServerAddress == this.GameServerAddress)
						{
							UnityEngine.Debug.LogWarning("This might be a misconfiguration in the game server config. You need to edit it to a (public) address.");
						}
					}
					this.State = ClientState.PeerCreated;
					DisconnectCause disconnectCause = (DisconnectCause)statusCode;
					this.IsInitialConnect = false;
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, new object[]
					{
						disconnectCause
					});
				}
				else
				{
					this.State = ClientState.PeerCreated;
					DisconnectCause disconnectCause = (DisconnectCause)statusCode;
					NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, new object[]
					{
						disconnectCause
					});
				}
				this.Disconnect();
				return;
			case StatusCode.SendError:
				return;
			}
			UnityEngine.Debug.LogError("Received unknown status code: " + statusCode);
			return;
		case StatusCode.EncryptionEstablished:
			break;
		case StatusCode.EncryptionFailedToEstablish:
		{
			UnityEngine.Debug.LogError("Encryption wasn't established: " + statusCode + ". Going to authenticate anyways.");
			AuthenticationValues authenticationValues;
			if ((authenticationValues = this.AuthValues) == null)
			{
				authenticationValues = new AuthenticationValues
				{
					UserId = this.PlayerName
				};
			}
			AuthenticationValues authValues = authenticationValues;
			this.OpAuthenticate(this.AppId, this.AppVersion, authValues, this.CloudRegion.ToString(), this.requestLobbyStatistics);
			return;
		}
		}
		IL_1AC:
		this._isReconnecting = false;
		if (this.Server == ServerConnection.NameServer)
		{
			this.State = ClientState.ConnectedToNameServer;
			if (!this.didAuthenticate && this.CloudRegion == CloudRegionCode.none)
			{
				this.OpGetRegions(this.AppId);
			}
		}
		if (this.Server != ServerConnection.NameServer && (this.AuthMode == AuthModeOption.AuthOnce || this.AuthMode == AuthModeOption.AuthOnceWss))
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"didAuthenticate ",
				this.didAuthenticate,
				" AuthMode ",
				this.AuthMode
			}));
		}
		else if (!this.didAuthenticate && (!this.IsUsingNameServer || this.CloudRegion != CloudRegionCode.none))
		{
			this.didAuthenticate = this.CallAuthenticate();
			if (this.didAuthenticate)
			{
				this.State = ClientState.Authenticating;
			}
		}
	}

	public void OnEvent(EventData photonEvent)
	{
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			UnityEngine.Debug.Log(string.Format("OnEvent: {0}", photonEvent.ToString()));
		}
		int num = -1;
		PhotonPlayer photonPlayer = null;
		if (photonEvent.Parameters.ContainsKey(254))
		{
			num = (int)photonEvent[254];
			photonPlayer = this.GetPlayerWithId(num);
		}
		byte code = photonEvent.Code;
		switch (code)
		{
		case 200:
			this.ExecuteRpc(photonEvent[245] as ExitGames.Client.Photon.Hashtable, photonPlayer.ID);
			break;
		case 201:
		case 206:
		{
			ExitGames.Client.Photon.Hashtable hashtable = (ExitGames.Client.Photon.Hashtable)photonEvent[245];
			int networkTime = (int)hashtable[0];
			short correctPrefix = -1;
			byte b = 10;
			int num2 = 1;
			if (hashtable.ContainsKey(1))
			{
				correctPrefix = (short)hashtable[1];
				num2 = 2;
			}
			byte b2 = b;
			while ((int)(b2 - b) < hashtable.Count - num2)
			{
				this.OnSerializeRead(hashtable[b2] as object[], photonPlayer, networkTime, correctPrefix);
				b2 += 1;
			}
			break;
		}
		case 202:
			this.DoInstantiate((ExitGames.Client.Photon.Hashtable)photonEvent[245], photonPlayer, null);
			break;
		case 203:
			if (photonPlayer == null || !photonPlayer.IsMasterClient)
			{
				UnityEngine.Debug.LogError("Error: Someone else(" + photonPlayer + ") then the masterserver requests a disconnect!");
			}
			else
			{
				PhotonNetwork.LeaveRoom();
			}
			break;
		case 204:
		{
			ExitGames.Client.Photon.Hashtable hashtable2 = (ExitGames.Client.Photon.Hashtable)photonEvent[245];
			int num3 = (int)hashtable2[0];
			PhotonView photonView = null;
			if (this.photonViewList.TryGetValue(num3, out photonView))
			{
				this.RemoveInstantiatedGO(photonView.gameObject, true);
			}
			else if (this.DebugOut >= DebugLevel.ERROR)
			{
				UnityEngine.Debug.LogError(string.Concat(new object[]
				{
					"Ev Destroy Failed. Could not find PhotonView with instantiationId ",
					num3,
					". Sent by actorNr: ",
					num
				}));
			}
			break;
		}
		default:
			switch (code)
			{
			case 223:
				if (this.AuthValues == null)
				{
					this.AuthValues = new AuthenticationValues();
				}
				this.AuthValues.Token = (photonEvent[221] as string);
				this.tokenCache = this.AuthValues.Token;
				break;
			case 224:
			{
				string[] array = photonEvent[213] as string[];
				byte[] array2 = photonEvent[212] as byte[];
				int[] array3 = photonEvent[229] as int[];
				int[] array4 = photonEvent[228] as int[];
				this.LobbyStatistics.Clear();
				for (int i = 0; i < array.Length; i++)
				{
					TypedLobbyInfo typedLobbyInfo = new TypedLobbyInfo();
					typedLobbyInfo.Name = array[i];
					typedLobbyInfo.Type = (LobbyType)array2[i];
					typedLobbyInfo.PlayerCount = array3[i];
					typedLobbyInfo.RoomCount = array4[i];
					this.LobbyStatistics.Add(typedLobbyInfo);
				}
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnLobbyStatisticsUpdate, new object[0]);
				break;
			}
			default:
				switch (code)
				{
				case 251:
					if (PhotonNetwork.OnEventCall != null)
					{
						object content = photonEvent[218];
						PhotonNetwork.OnEventCall(photonEvent.Code, content, num);
					}
					else
					{
						UnityEngine.Debug.LogWarning("Warning: Unhandled Event ErrorInfo (251). Set PhotonNetwork.OnEventCall to the method PUN should call for this event.");
					}
					return;
				case 253:
				{
					int num4 = (int)photonEvent[253];
					ExitGames.Client.Photon.Hashtable gameProperties = null;
					ExitGames.Client.Photon.Hashtable pActorProperties = null;
					if (num4 == 0)
					{
						gameProperties = (ExitGames.Client.Photon.Hashtable)photonEvent[251];
					}
					else
					{
						pActorProperties = (ExitGames.Client.Photon.Hashtable)photonEvent[251];
					}
					this.ReadoutProperties(gameProperties, pActorProperties, num4);
					return;
				}
				case 254:
					this.HandleEventLeave(num, photonEvent);
					return;
				case 255:
				{
					bool flag = false;
					ExitGames.Client.Photon.Hashtable properties = (ExitGames.Client.Photon.Hashtable)photonEvent[249];
					if (photonPlayer == null)
					{
						bool isLocal = this.LocalPlayer.ID == num;
						this.AddNewPlayer(num, new PhotonPlayer(isLocal, num, properties));
						this.ResetPhotonViewsOnSerialize();
					}
					else
					{
						flag = photonPlayer.IsInactive;
						photonPlayer.InternalCacheProperties(properties);
						photonPlayer.IsInactive = false;
					}
					if (num == this.LocalPlayer.ID)
					{
						int[] actorsInRoom = (int[])photonEvent[252];
						this.UpdatedActorList(actorsInRoom);
						if (this.lastJoinType == JoinType.JoinOrCreateRoom && this.LocalPlayer.ID == 1)
						{
							NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom, new object[0]);
						}
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom, new object[0]);
					}
					else
					{
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerConnected, new object[]
						{
							this.mActors[num]
						});
						if (flag)
						{
							NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerActivityChanged, new object[]
							{
								this.mActors[num]
							});
						}
					}
					return;
				}
				}
				if (photonEvent.Code < 200)
				{
					if (PhotonNetwork.OnEventCall != null)
					{
						object content2 = photonEvent[245];
						PhotonNetwork.OnEventCall(photonEvent.Code, content2, num);
					}
					else
					{
						UnityEngine.Debug.LogWarning("Warning: Unhandled event " + photonEvent + ". Set PhotonNetwork.OnEventCall.");
					}
				}
				break;
			case 226:
				this.PlayersInRoomsCount = (int)photonEvent[229];
				this.PlayersOnMasterCount = (int)photonEvent[227];
				this.RoomsCount = (int)photonEvent[228];
				break;
			case 229:
			{
				ExitGames.Client.Photon.Hashtable hashtable3 = (ExitGames.Client.Photon.Hashtable)photonEvent[222];
				foreach (object obj in hashtable3.Keys)
				{
					string text = (string)obj;
					RoomInfo roomInfo = new RoomInfo(text, (ExitGames.Client.Photon.Hashtable)hashtable3[obj]);
					if (roomInfo.removedFromList)
					{
						this.mGameList.Remove(text);
					}
					else
					{
						this.mGameList[text] = roomInfo;
					}
				}
				this.mGameListCopy = new RoomInfo[this.mGameList.Count];
				this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate, new object[0]);
				break;
			}
			case 230:
			{
				this.mGameList = new Dictionary<string, RoomInfo>();
				ExitGames.Client.Photon.Hashtable hashtable4 = (ExitGames.Client.Photon.Hashtable)photonEvent[222];
				foreach (object obj2 in hashtable4.Keys)
				{
					string text2 = (string)obj2;
					this.mGameList[text2] = new RoomInfo(text2, (ExitGames.Client.Photon.Hashtable)hashtable4[obj2]);
				}
				this.mGameListCopy = new RoomInfo[this.mGameList.Count];
				this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate, new object[0]);
				break;
			}
			}
			break;
		case 207:
		{
			ExitGames.Client.Photon.Hashtable hashtable2 = (ExitGames.Client.Photon.Hashtable)photonEvent[245];
			int num5 = (int)hashtable2[0];
			if (num5 >= 0)
			{
				this.DestroyPlayerObjects(num5, true);
			}
			else
			{
				if (this.DebugOut >= DebugLevel.INFO)
				{
					UnityEngine.Debug.Log("Ev DestroyAll! By PlayerId: " + num);
				}
				this.DestroyAll(true);
			}
			break;
		}
		case 208:
		{
			ExitGames.Client.Photon.Hashtable hashtable2 = (ExitGames.Client.Photon.Hashtable)photonEvent[245];
			int playerId = (int)hashtable2[1];
			this.SetMasterClient(playerId, false);
			break;
		}
		case 209:
		{
			int[] array5 = (int[])photonEvent.Parameters[245];
			int num6 = array5[0];
			int num7 = array5[1];
			PhotonView photonView2 = PhotonView.Find(num6);
			if (photonView2 == null)
			{
				UnityEngine.Debug.LogWarning("Can't find PhotonView of incoming OwnershipRequest. ViewId not found: " + num6);
			}
			else
			{
				if (PhotonNetwork.logLevel == PhotonLogLevel.Informational)
				{
					UnityEngine.Debug.Log(string.Concat(new object[]
					{
						"Ev OwnershipRequest ",
						photonView2.ownershipTransfer,
						". ActorNr: ",
						num,
						" takes from: ",
						num7,
						". local RequestedView.ownerId: ",
						photonView2.ownerId,
						" isOwnerActive: ",
						photonView2.isOwnerActive,
						". MasterClient: ",
						this.mMasterClientId,
						". This client's player: ",
						PhotonNetwork.player.ToStringFull()
					}));
				}
				switch (photonView2.ownershipTransfer)
				{
				case OwnershipOption.Fixed:
					UnityEngine.Debug.LogWarning("Ownership mode == fixed. Ignoring request.");
					break;
				case OwnershipOption.Takeover:
					if (num7 == photonView2.ownerId || (num7 == 0 && photonView2.ownerId == this.mMasterClientId) || photonView2.ownerId == 0)
					{
						photonView2.OwnerShipWasTransfered = true;
						int ownerId = photonView2.ownerId;
						PhotonPlayer playerWithId = this.GetPlayerWithId(ownerId);
						photonView2.ownerId = num;
						if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
						{
							UnityEngine.Debug.LogWarning(photonView2 + " ownership transfered to: " + num);
						}
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipTransfered, new object[]
						{
							photonView2,
							photonPlayer,
							playerWithId
						});
					}
					break;
				case OwnershipOption.Request:
					if ((num7 == PhotonNetwork.player.ID || PhotonNetwork.player.IsMasterClient) && (photonView2.ownerId == PhotonNetwork.player.ID || (PhotonNetwork.player.IsMasterClient && !photonView2.isOwnerActive)))
					{
						NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipRequest, new object[]
						{
							photonView2,
							photonPlayer
						});
					}
					break;
				}
			}
			break;
		}
		case 210:
		{
			int[] array6 = (int[])photonEvent.Parameters[245];
			if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
			{
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"Ev OwnershipTransfer. ViewID ",
					array6[0],
					" to: ",
					array6[1],
					" Time: ",
					Environment.TickCount % 1000
				}));
			}
			int viewID = array6[0];
			int num8 = array6[1];
			PhotonView photonView3 = PhotonView.Find(viewID);
			if (photonView3 != null)
			{
				int ownerId2 = photonView3.ownerId;
				photonView3.OwnerShipWasTransfered = true;
				photonView3.ownerId = num8;
				NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnOwnershipTransfered, new object[]
				{
					photonView3,
					PhotonPlayer.Find(num8),
					PhotonPlayer.Find(ownerId2)
				});
			}
			break;
		}
		}
	}

	public void OnMessage(object messages)
	{
	}

	private void SetupEncryption(Dictionary<byte, object> encryptionData)
	{
		if (this.AuthMode == AuthModeOption.Auth && this.DebugOut == DebugLevel.ERROR)
		{
			UnityEngine.Debug.LogWarning("SetupEncryption() called but ignored. Not XB1 compiled. EncryptionData: " + encryptionData.ToStringFull());
			return;
		}
		if (this.DebugOut == DebugLevel.INFO)
		{
			UnityEngine.Debug.Log("SetupEncryption() got called. " + encryptionData.ToStringFull());
		}
		EncryptionMode encryptionMode = (EncryptionMode)((byte)encryptionData[0]);
		if (encryptionMode != EncryptionMode.PayloadEncryption)
		{
			if (encryptionMode != EncryptionMode.DatagramEncryption)
			{
				throw new ArgumentOutOfRangeException();
			}
			byte[] encryptionSecret = (byte[])encryptionData[1];
			byte[] hmacSecret = (byte[])encryptionData[2];
			base.InitDatagramEncryption(encryptionSecret, hmacSecret);
		}
		else
		{
			byte[] secret = (byte[])encryptionData[1];
			base.InitPayloadEncryption(secret);
		}
	}

	protected internal void UpdatedActorList(int[] actorsInRoom)
	{
		foreach (int num in actorsInRoom)
		{
			if (this.LocalPlayer.ID != num && !this.mActors.ContainsKey(num))
			{
				this.AddNewPlayer(num, new PhotonPlayer(false, num, string.Empty));
			}
		}
	}

	private void SendVacantViewIds()
	{
		UnityEngine.Debug.Log("SendVacantViewIds()");
		List<int> list = new List<int>();
		foreach (PhotonView photonView in this.photonViewList.Values)
		{
			if (!photonView.isOwnerActive)
			{
				list.Add(photonView.viewID);
			}
		}
		UnityEngine.Debug.Log("Sending vacant view IDs. Length: " + list.Count);
		this.OpRaiseEvent(211, list.ToArray(), true, null);
	}

	public static void SendMonoMessage(PhotonNetworkingMessage methodString, params object[] parameters)
	{
		HashSet<GameObject> hashSet;
		if (PhotonNetwork.SendMonoMessageTargets != null)
		{
			hashSet = PhotonNetwork.SendMonoMessageTargets;
		}
		else
		{
			hashSet = PhotonNetwork.FindGameObjectsWithComponent(PhotonNetwork.SendMonoMessageTargetType);
		}
		string methodName = methodString.ToString();
		object value = (parameters == null || parameters.Length != 1) ? parameters : parameters[0];
		foreach (GameObject gameObject in hashSet)
		{
			if (gameObject != null)
			{
				gameObject.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected internal void ExecuteRpc(ExitGames.Client.Photon.Hashtable rpcData, int senderID = 0)
	{
		if (rpcData == null || !rpcData.ContainsKey(0))
		{
			UnityEngine.Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
			return;
		}
		int num = (int)rpcData[0];
		int num2 = 0;
		if (rpcData.ContainsKey(1))
		{
			num2 = (int)((short)rpcData[1]);
		}
		string text;
		if (rpcData.ContainsKey(5))
		{
			int num3 = (int)((byte)rpcData[5]);
			if (num3 > PhotonNetwork.PhotonServerSettings.RpcList.Count - 1)
			{
				UnityEngine.Debug.LogError("Could not find RPC with index: " + num3 + ". Going to ignore! Check PhotonServerSettings.RpcList");
				return;
			}
			text = PhotonNetwork.PhotonServerSettings.RpcList[num3];
		}
		else
		{
			text = (string)rpcData[3];
		}
		object[] array = null;
		if (rpcData.ContainsKey(4))
		{
			array = (object[])rpcData[4];
		}
		if (array == null)
		{
			array = new object[0];
		}
		PhotonView photonView = this.GetPhotonView(num);
		if (photonView == null)
		{
			int num4 = num / PhotonNetwork.MAX_VIEW_IDS;
			bool flag = num4 == this.LocalPlayer.ID;
			bool flag2 = num4 == senderID;
			if (flag)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new object[]
				{
					"Received RPC \"",
					text,
					"\" for viewID ",
					num,
					" but this PhotonView does not exist! View was/is ours.",
					(!flag2) ? " Remote called." : " Owner called.",
					" By: ",
					senderID
				}));
			}
			else
			{
				UnityEngine.Debug.LogWarning(string.Concat(new object[]
				{
					"Received RPC \"",
					text,
					"\" for viewID ",
					num,
					" but this PhotonView does not exist! Was remote PV.",
					(!flag2) ? " Remote called." : " Owner called.",
					" By: ",
					senderID,
					" Maybe GO was destroyed but RPC not cleaned up."
				}));
			}
			return;
		}
		if (photonView.prefix != num2)
		{
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"Received RPC \"",
				text,
				"\" on viewID ",
				num,
				" with a prefix of ",
				num2,
				", our prefix is ",
				photonView.prefix,
				". The RPC has been ignored."
			}));
			return;
		}
		if (string.IsNullOrEmpty(text))
		{
			UnityEngine.Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
			return;
		}
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
		{
			UnityEngine.Debug.Log("Received RPC: " + text);
		}
		if (photonView.group != 0 && !this.allowedReceivingGroups.Contains(photonView.group))
		{
			return;
		}
		Type[] array2 = new Type[0];
		if (array.Length > 0)
		{
			array2 = new Type[array.Length];
			int num5 = 0;
			foreach (object obj in array)
			{
				if (obj == null)
				{
					array2[num5] = null;
				}
				else
				{
					array2[num5] = obj.GetType();
				}
				num5++;
			}
		}
		int num6 = 0;
		int num7 = 0;
		if (!PhotonNetwork.UseRpcMonoBehaviourCache || photonView.RpcMonoBehaviours == null || photonView.RpcMonoBehaviours.Length == 0)
		{
			photonView.RefreshRpcMonoBehaviourCache();
		}
		for (int j = 0; j < photonView.RpcMonoBehaviours.Length; j++)
		{
			MonoBehaviour monoBehaviour = photonView.RpcMonoBehaviours[j];
			if (monoBehaviour == null)
			{
				UnityEngine.Debug.LogError("ERROR You have missing MonoBehaviours on your gameobjects!");
				UnityEngine.Debug.LogError("Object: " + photonView.gameObject.name);
			}
			else
			{
				Type type = monoBehaviour.GetType();
				List<MethodInfo> list = null;
				if (!this.monoRPCMethodsCache.TryGetValue(type, out list))
				{
					List<MethodInfo> methods = SupportClass.GetMethods(type, typeof(PunRPC));
					this.monoRPCMethodsCache[type] = methods;
					list = methods;
				}
				if (list != null)
				{
					for (int k = 0; k < list.Count; k++)
					{
						MethodInfo methodInfo = list[k];
						if (methodInfo.Name.Equals(text))
						{
							num7++;
							ParameterInfo[] cachedParemeters = methodInfo.GetCachedParemeters();
							if (cachedParemeters.Length == array2.Length)
							{
								if (this.CheckTypeMatch(cachedParemeters, array2))
								{
									num6++;
									object obj2 = methodInfo.Invoke(monoBehaviour, array);
									if (PhotonNetwork.StartRpcsAsCoroutine && methodInfo.ReturnType == typeof(IEnumerator))
									{
										monoBehaviour.StartCoroutine((IEnumerator)obj2);
									}
								}
							}
							else if (cachedParemeters.Length - 1 == array2.Length)
							{
								if (this.CheckTypeMatch(cachedParemeters, array2) && cachedParemeters[cachedParemeters.Length - 1].ParameterType == typeof(PhotonMessageInfo))
								{
									num6++;
									int timestamp = (int)rpcData[2];
									object[] array3 = new object[array.Length + 1];
									array.CopyTo(array3, 0);
									array3[array3.Length - 1] = new PhotonMessageInfo(this.GetPlayerWithId(senderID), timestamp, photonView);
									object obj3 = methodInfo.Invoke(monoBehaviour, array3);
									if (PhotonNetwork.StartRpcsAsCoroutine && methodInfo.ReturnType == typeof(IEnumerator))
									{
										monoBehaviour.StartCoroutine((IEnumerator)obj3);
									}
								}
							}
							else if (cachedParemeters.Length == 1 && cachedParemeters[0].ParameterType.IsArray)
							{
								num6++;
								object obj4 = methodInfo.Invoke(monoBehaviour, new object[]
								{
									array
								});
								if (PhotonNetwork.StartRpcsAsCoroutine && methodInfo.ReturnType == typeof(IEnumerator))
								{
									monoBehaviour.StartCoroutine((IEnumerator)obj4);
								}
							}
						}
					}
				}
			}
		}
		if (num6 != 1)
		{
			string text2 = string.Empty;
			foreach (Type type2 in array2)
			{
				if (text2 != string.Empty)
				{
					text2 += ", ";
				}
				if (type2 == null)
				{
					text2 += "null";
				}
				else
				{
					text2 += type2.Name;
				}
			}
			if (num6 == 0)
			{
				if (num7 == 0)
				{
					UnityEngine.Debug.LogError(string.Concat(new object[]
					{
						"PhotonView with ID ",
						num,
						" has no method \"",
						text,
						"\" marked with the [PunRPC](C#) or @PunRPC(JS) property! Args: ",
						text2
					}));
				}
				else
				{
					UnityEngine.Debug.LogError(string.Concat(new object[]
					{
						"PhotonView with ID ",
						num,
						" has no method \"",
						text,
						"\" that takes ",
						array2.Length,
						" argument(s): ",
						text2
					}));
				}
			}
			else
			{
				UnityEngine.Debug.LogError(string.Concat(new object[]
				{
					"PhotonView with ID ",
					num,
					" has ",
					num6,
					" methods \"",
					text,
					"\" that takes ",
					array2.Length,
					" argument(s): ",
					text2,
					". Should be just one?"
				}));
			}
		}
	}

	private bool CheckTypeMatch(ParameterInfo[] methodParameters, Type[] callParameterTypes)
	{
		if (methodParameters.Length < callParameterTypes.Length)
		{
			return false;
		}
		for (int i = 0; i < callParameterTypes.Length; i++)
		{
			Type parameterType = methodParameters[i].ParameterType;
			if (callParameterTypes[i] != null && !parameterType.IsAssignableFrom(callParameterTypes[i]) && (!parameterType.IsEnum || !Enum.GetUnderlyingType(parameterType).IsAssignableFrom(callParameterTypes[i])))
			{
				return false;
			}
		}
		return true;
	}

	internal ExitGames.Client.Photon.Hashtable SendInstantiate(string prefabName, Vector3 position, Quaternion rotation, byte group, int[] viewIDs, object[] data, bool isGlobalObject)
	{
		int num = viewIDs[0];
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[0] = prefabName;
		if (position != Vector3.zero)
		{
			hashtable[1] = position;
		}
		if (rotation != Quaternion.identity)
		{
			hashtable[2] = rotation;
		}
		if (group != 0)
		{
			hashtable[3] = group;
		}
		if (viewIDs.Length > 1)
		{
			hashtable[4] = viewIDs;
		}
		if (data != null)
		{
			hashtable[5] = data;
		}
		if (this.currentLevelPrefix > 0)
		{
			hashtable[8] = this.currentLevelPrefix;
		}
		hashtable[6] = PhotonNetwork.ServerTimestamp;
		hashtable[7] = num;
		this.OpRaiseEvent(202, hashtable, true, new RaiseEventOptions
		{
			CachingOption = ((!isGlobalObject) ? EventCaching.AddToRoomCache : EventCaching.AddToRoomCacheGlobal)
		});
		return hashtable;
	}

	internal GameObject DoInstantiate(ExitGames.Client.Photon.Hashtable evData, PhotonPlayer photonPlayer, GameObject resourceGameObject)
	{
		string text = (string)evData[0];
		int timestamp = (int)evData[6];
		int num = (int)evData[7];
		Vector3 position;
		if (evData.ContainsKey(1))
		{
			position = (Vector3)evData[1];
		}
		else
		{
			position = Vector3.zero;
		}
		Quaternion rotation = Quaternion.identity;
		if (evData.ContainsKey(2))
		{
			rotation = (Quaternion)evData[2];
		}
		byte b = 0;
		if (evData.ContainsKey(3))
		{
			b = (byte)evData[3];
		}
		short prefix = 0;
		if (evData.ContainsKey(8))
		{
			prefix = (short)evData[8];
		}
		int[] array;
		if (evData.ContainsKey(4))
		{
			array = (int[])evData[4];
		}
		else
		{
			array = new int[]
			{
				num
			};
		}
		object[] array2;
		if (evData.ContainsKey(5))
		{
			array2 = (object[])evData[5];
		}
		else
		{
			array2 = null;
		}
		if (b != 0 && !this.allowedReceivingGroups.Contains(b))
		{
			return null;
		}
		if (this.ObjectPool != null)
		{
			GameObject gameObject = this.ObjectPool.Instantiate(text, position, rotation);
			PhotonView[] photonViewsInChildren = gameObject.GetPhotonViewsInChildren();
			if (photonViewsInChildren.Length != array.Length)
			{
				throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
			}
			for (int i = 0; i < photonViewsInChildren.Length; i++)
			{
				photonViewsInChildren[i].didAwake = false;
				photonViewsInChildren[i].viewID = 0;
				photonViewsInChildren[i].prefix = (int)prefix;
				photonViewsInChildren[i].instantiationId = num;
				photonViewsInChildren[i].isRuntimeInstantiated = true;
				photonViewsInChildren[i].instantiationDataField = array2;
				photonViewsInChildren[i].didAwake = true;
				photonViewsInChildren[i].viewID = array[i];
			}
			gameObject.SendMessage(NetworkingPeer.OnPhotonInstantiateString, new PhotonMessageInfo(photonPlayer, timestamp, null), SendMessageOptions.DontRequireReceiver);
			return gameObject;
		}
		else
		{
			if (resourceGameObject == null)
			{
				if (!NetworkingPeer.UsePrefabCache || !NetworkingPeer.PrefabCache.TryGetValue(text, out resourceGameObject))
				{
					resourceGameObject = (GameObject)Resources.Load(text, typeof(GameObject));
					if (NetworkingPeer.UsePrefabCache)
					{
						NetworkingPeer.PrefabCache.Add(text, resourceGameObject);
					}
				}
				if (resourceGameObject == null)
				{
					UnityEngine.Debug.LogError("PhotonNetwork error: Could not Instantiate the prefab [" + text + "]. Please verify you have this gameobject in a Resources folder.");
					return null;
				}
			}
			PhotonView[] photonViewsInChildren2 = resourceGameObject.GetPhotonViewsInChildren();
			if (photonViewsInChildren2.Length != array.Length)
			{
				throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
			}
			for (int j = 0; j < array.Length; j++)
			{
				photonViewsInChildren2[j].viewID = array[j];
				photonViewsInChildren2[j].prefix = (int)prefix;
				photonViewsInChildren2[j].instantiationId = num;
				photonViewsInChildren2[j].isRuntimeInstantiated = true;
			}
			this.StoreInstantiationData(num, array2);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(resourceGameObject, position, rotation);
			for (int k = 0; k < array.Length; k++)
			{
				photonViewsInChildren2[k].viewID = 0;
				photonViewsInChildren2[k].prefix = -1;
				photonViewsInChildren2[k].prefixBackup = -1;
				photonViewsInChildren2[k].instantiationId = -1;
				photonViewsInChildren2[k].isRuntimeInstantiated = false;
			}
			this.RemoveInstantiationData(num);
			gameObject2.SendMessage(NetworkingPeer.OnPhotonInstantiateString, new PhotonMessageInfo(photonPlayer, timestamp, null), SendMessageOptions.DontRequireReceiver);
			return gameObject2;
		}
	}

	private void StoreInstantiationData(int instantiationId, object[] instantiationData)
	{
		this.tempInstantiationData[instantiationId] = instantiationData;
	}

	public object[] FetchInstantiationData(int instantiationId)
	{
		object[] result = null;
		if (instantiationId == 0)
		{
			return null;
		}
		this.tempInstantiationData.TryGetValue(instantiationId, out result);
		return result;
	}

	private void RemoveInstantiationData(int instantiationId)
	{
		this.tempInstantiationData.Remove(instantiationId);
	}

	public void DestroyPlayerObjects(int playerId, bool localOnly)
	{
		if (playerId <= 0)
		{
			UnityEngine.Debug.LogError("Failed to Destroy objects of playerId: " + playerId);
			return;
		}
		if (!localOnly)
		{
			this.OpRemoveFromServerInstantiationsOfPlayer(playerId);
			this.OpCleanRpcBuffer(playerId);
			this.SendDestroyOfPlayer(playerId);
		}
		HashSet<GameObject> hashSet = new HashSet<GameObject>();
		foreach (PhotonView photonView in this.photonViewList.Values)
		{
			if (photonView != null && photonView.CreatorActorNr == playerId)
			{
				hashSet.Add(photonView.gameObject);
			}
		}
		foreach (GameObject go in hashSet)
		{
			this.RemoveInstantiatedGO(go, true);
		}
		foreach (PhotonView photonView2 in this.photonViewList.Values)
		{
			if (photonView2.ownerId == playerId)
			{
				photonView2.ownerId = photonView2.CreatorActorNr;
			}
		}
	}

	public void DestroyAll(bool localOnly)
	{
		if (!localOnly)
		{
			this.OpRemoveCompleteCache();
			this.SendDestroyOfAll();
		}
		this.LocalCleanupAnythingInstantiated(true);
	}

	protected internal void RemoveInstantiatedGO(GameObject go, bool localOnly)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogError("Failed to 'network-remove' GameObject because it's null.");
			return;
		}
		PhotonView[] componentsInChildren = go.GetComponentsInChildren<PhotonView>(true);
		if (componentsInChildren == null || componentsInChildren.Length <= 0)
		{
			UnityEngine.Debug.LogError("Failed to 'network-remove' GameObject because has no PhotonView components: " + go);
			return;
		}
		PhotonView photonView = componentsInChildren[0];
		int creatorActorNr = photonView.CreatorActorNr;
		int instantiationId = photonView.instantiationId;
		if (!localOnly)
		{
			if (!photonView.isMine)
			{
				UnityEngine.Debug.LogError("Failed to 'network-remove' GameObject. Client is neither owner nor masterClient taking over for owner who left: " + photonView);
				return;
			}
			if (instantiationId < 1)
			{
				UnityEngine.Debug.LogError("Failed to 'network-remove' GameObject because it is missing a valid InstantiationId on view: " + photonView + ". Not Destroying GameObject or PhotonViews!");
				return;
			}
		}
		if (!localOnly)
		{
			this.ServerCleanInstantiateAndDestroy(instantiationId, creatorActorNr, photonView.isRuntimeInstantiated);
		}
		for (int i = componentsInChildren.Length - 1; i >= 0; i--)
		{
			PhotonView photonView2 = componentsInChildren[i];
			if (!(photonView2 == null))
			{
				if (photonView2.instantiationId >= 1)
				{
					this.LocalCleanPhotonView(photonView2);
				}
				if (!localOnly)
				{
					this.OpCleanRpcBuffer(photonView2);
				}
			}
		}
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
		{
			UnityEngine.Debug.Log("Network destroy Instantiated GO: " + go.name);
		}
		if (this.ObjectPool != null)
		{
			PhotonView[] photonViewsInChildren = go.GetPhotonViewsInChildren();
			for (int j = 0; j < photonViewsInChildren.Length; j++)
			{
				photonViewsInChildren[j].viewID = 0;
			}
			this.ObjectPool.Destroy(go);
		}
		else
		{
			UnityEngine.Object.Destroy(go);
		}
	}

	private void ServerCleanInstantiateAndDestroy(int instantiateId, int creatorId, bool isRuntimeInstantiated)
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[7] = instantiateId;
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			TargetActors = new int[]
			{
				creatorId
			}
		};
		this.OpRaiseEvent(202, hashtable, true, raiseEventOptions);
		ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
		hashtable2[0] = instantiateId;
		raiseEventOptions = null;
		if (!isRuntimeInstantiated)
		{
			raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.CachingOption = EventCaching.AddToRoomCacheGlobal;
			UnityEngine.Debug.Log("Destroying GO as global. ID: " + instantiateId);
		}
		this.OpRaiseEvent(204, hashtable2, true, raiseEventOptions);
	}

	private void SendDestroyOfPlayer(int actorNr)
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[0] = actorNr;
		this.OpRaiseEvent(207, hashtable, true, null);
	}

	private void SendDestroyOfAll()
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[0] = -1;
		this.OpRaiseEvent(207, hashtable, true, null);
	}

	private void OpRemoveFromServerInstantiationsOfPlayer(int actorNr)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			TargetActors = new int[]
			{
				actorNr
			}
		};
		this.OpRaiseEvent(202, null, true, raiseEventOptions);
	}

	protected internal void RequestOwnership(int viewID, int fromOwner)
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"RequestOwnership(): ",
			viewID,
			" from: ",
			fromOwner,
			" Time: ",
			Environment.TickCount % 1000
		}));
		this.OpRaiseEvent(209, new int[]
		{
			viewID,
			fromOwner
		}, true, new RaiseEventOptions
		{
			Receivers = ReceiverGroup.All
		});
	}

	protected internal void TransferOwnership(int viewID, int playerID)
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"TransferOwnership() view ",
			viewID,
			" to: ",
			playerID,
			" Time: ",
			Environment.TickCount % 1000
		}));
		this.OpRaiseEvent(210, new int[]
		{
			viewID,
			playerID
		}, true, new RaiseEventOptions
		{
			Receivers = ReceiverGroup.All
		});
	}

	public bool LocalCleanPhotonView(PhotonView view)
	{
		view.removedFromLocalViewList = true;
		return this.photonViewList.Remove(view.viewID);
	}

	public PhotonView GetPhotonView(int viewID)
	{
		PhotonView photonView = null;
		this.photonViewList.TryGetValue(viewID, out photonView);
		if (photonView == null)
		{
			foreach (PhotonView photonView2 in UnityEngine.Object.FindObjectsOfType(typeof(PhotonView)) as PhotonView[])
			{
				if (photonView2.viewID == viewID)
				{
					if (photonView2.didAwake)
					{
						UnityEngine.Debug.LogWarning("Had to lookup view that wasn't in photonViewList: " + photonView2);
					}
					return photonView2;
				}
			}
		}
		return photonView;
	}

	public void RegisterPhotonView(PhotonView netView)
	{
		if (!Application.isPlaying)
		{
			this.photonViewList = new Dictionary<int, PhotonView>();
			return;
		}
		if (netView.viewID == 0)
		{
			UnityEngine.Debug.Log("PhotonView register is ignored, because viewID is 0. No id assigned yet to: " + netView);
			return;
		}
		PhotonView photonView = null;
		bool flag = this.photonViewList.TryGetValue(netView.viewID, out photonView);
		if (flag)
		{
			if (!(netView != photonView))
			{
				return;
			}
			UnityEngine.Debug.LogError(string.Format("PhotonView ID duplicate found: {0}. New: {1} old: {2}. Maybe one wasn't destroyed on scene load?! Check for 'DontDestroyOnLoad'. Destroying old entry, adding new.", netView.viewID, netView, photonView));
			this.RemoveInstantiatedGO(photonView.gameObject, true);
		}
		this.photonViewList.Add(netView.viewID, netView);
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
		{
			UnityEngine.Debug.Log("Registered PhotonView: " + netView.viewID);
		}
	}

	public void OpCleanRpcBuffer(int actorNumber)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			TargetActors = new int[]
			{
				actorNumber
			}
		};
		this.OpRaiseEvent(200, null, true, raiseEventOptions);
	}

	public void OpRemoveCompleteCacheOfPlayer(int actorNumber)
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			TargetActors = new int[]
			{
				actorNumber
			}
		};
		this.OpRaiseEvent(0, null, true, raiseEventOptions);
	}

	public void OpRemoveCompleteCache()
	{
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache,
			Receivers = ReceiverGroup.MasterClient
		};
		this.OpRaiseEvent(0, null, true, raiseEventOptions);
	}

	private void RemoveCacheOfLeftPlayers()
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary[244] = 0;
		dictionary[247] = 7;
		this.OpCustom(253, dictionary, true, 0);
	}

	public void CleanRpcBufferIfMine(PhotonView view)
	{
		if (view.ownerId != this.LocalPlayer.ID && !this.LocalPlayer.IsMasterClient)
		{
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"Cannot remove cached RPCs on a PhotonView thats not ours! ",
				view.owner,
				" scene: ",
				view.isSceneView
			}));
			return;
		}
		this.OpCleanRpcBuffer(view);
	}

	public void OpCleanRpcBuffer(PhotonView view)
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[0] = view.viewID;
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache
		};
		this.OpRaiseEvent(200, hashtable, true, raiseEventOptions);
	}

	public void RemoveRPCsInGroup(int group)
	{
		foreach (PhotonView photonView in this.photonViewList.Values)
		{
			if ((int)photonView.group == group)
			{
				this.CleanRpcBufferIfMine(photonView);
			}
		}
	}

	public void SetLevelPrefix(short prefix)
	{
		this.currentLevelPrefix = prefix;
	}

	internal void RPC(PhotonView view, string methodName, PhotonTargets target, PhotonPlayer player, bool encrypt, params object[] parameters)
	{
		if (this.blockSendingGroups.Contains(view.group))
		{
			return;
		}
		if (view.viewID < 1)
		{
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"Illegal view ID:",
				view.viewID,
				" method: ",
				methodName,
				" GO:",
				view.gameObject.name
			}));
		}
		if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Sending RPC \"",
				methodName,
				"\" to target: ",
				target,
				" or player:",
				player,
				"."
			}));
		}
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable[0] = view.viewID;
		if (view.prefix > 0)
		{
			hashtable[1] = (short)view.prefix;
		}
		hashtable[2] = PhotonNetwork.ServerTimestamp;
		int num = 0;
		if (this.rpcShortcuts.TryGetValue(methodName, out num))
		{
			hashtable[5] = (byte)num;
		}
		else
		{
			hashtable[3] = methodName;
		}
		if (parameters != null && parameters.Length > 0)
		{
			hashtable[4] = parameters;
		}
		if (player != null)
		{
			if (this.LocalPlayer.ID == player.ID)
			{
				this.ExecuteRpc(hashtable, player.ID);
			}
			else
			{
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions
				{
					TargetActors = new int[]
					{
						player.ID
					},
					Encrypt = encrypt
				};
				this.OpRaiseEvent(200, hashtable, true, raiseEventOptions);
			}
			return;
		}
		if (target == PhotonTargets.All)
		{
			RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions
			{
				InterestGroup = view.group,
				Encrypt = encrypt
			};
			this.OpRaiseEvent(200, hashtable, true, raiseEventOptions2);
			this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
		}
		else if (target == PhotonTargets.Others)
		{
			RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions
			{
				InterestGroup = view.group,
				Encrypt = encrypt
			};
			this.OpRaiseEvent(200, hashtable, true, raiseEventOptions3);
		}
		else if (target == PhotonTargets.AllBuffered)
		{
			RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions
			{
				CachingOption = EventCaching.AddToRoomCache,
				Encrypt = encrypt
			};
			this.OpRaiseEvent(200, hashtable, true, raiseEventOptions4);
			this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
		}
		else if (target == PhotonTargets.OthersBuffered)
		{
			RaiseEventOptions raiseEventOptions5 = new RaiseEventOptions
			{
				CachingOption = EventCaching.AddToRoomCache,
				Encrypt = encrypt
			};
			this.OpRaiseEvent(200, hashtable, true, raiseEventOptions5);
		}
		else if (target == PhotonTargets.MasterClient)
		{
			if (this.mMasterClientId == this.LocalPlayer.ID)
			{
				this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
			}
			else
			{
				RaiseEventOptions raiseEventOptions6 = new RaiseEventOptions
				{
					Receivers = ReceiverGroup.MasterClient,
					Encrypt = encrypt
				};
				this.OpRaiseEvent(200, hashtable, true, raiseEventOptions6);
			}
		}
		else if (target == PhotonTargets.AllViaServer)
		{
			RaiseEventOptions raiseEventOptions7 = new RaiseEventOptions
			{
				InterestGroup = view.group,
				Receivers = ReceiverGroup.All,
				Encrypt = encrypt
			};
			this.OpRaiseEvent(200, hashtable, true, raiseEventOptions7);
			if (PhotonNetwork.offlineMode)
			{
				this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
			}
		}
		else if (target == PhotonTargets.AllBufferedViaServer)
		{
			RaiseEventOptions raiseEventOptions8 = new RaiseEventOptions
			{
				InterestGroup = view.group,
				Receivers = ReceiverGroup.All,
				CachingOption = EventCaching.AddToRoomCache,
				Encrypt = encrypt
			};
			this.OpRaiseEvent(200, hashtable, true, raiseEventOptions8);
			if (PhotonNetwork.offlineMode)
			{
				this.ExecuteRpc(hashtable, this.LocalPlayer.ID);
			}
		}
		else
		{
			UnityEngine.Debug.LogError("Unsupported target enum: " + target);
		}
	}

	public void SetInterestGroups(byte[] disableGroups, byte[] enableGroups)
	{
		if (disableGroups != null)
		{
			if (disableGroups.Length == 0)
			{
				this.allowedReceivingGroups.Clear();
			}
			else
			{
				foreach (byte b in disableGroups)
				{
					if (b <= 0)
					{
						UnityEngine.Debug.LogError("Error: PhotonNetwork.SetInterestGroups was called with an illegal group number: " + b + ". The group number should be at least 1.");
					}
					else if (this.allowedReceivingGroups.Contains(b))
					{
						this.allowedReceivingGroups.Remove(b);
					}
				}
			}
		}
		if (enableGroups != null)
		{
			if (enableGroups.Length == 0)
			{
				for (byte b2 = 0; b2 < 255; b2 += 1)
				{
					this.allowedReceivingGroups.Add(b2);
				}
				this.allowedReceivingGroups.Add(byte.MaxValue);
			}
			else
			{
				foreach (byte b3 in enableGroups)
				{
					if (b3 <= 0)
					{
						UnityEngine.Debug.LogError("Error: PhotonNetwork.SetInterestGroups was called with an illegal group number: " + b3 + ". The group number should be at least 1.");
					}
					else
					{
						this.allowedReceivingGroups.Add(b3);
					}
				}
			}
		}
		this.OpChangeGroups(disableGroups, enableGroups);
	}

	public void SetSendingEnabled(byte group, bool enabled)
	{
		if (!enabled)
		{
			this.blockSendingGroups.Add(group);
		}
		else
		{
			this.blockSendingGroups.Remove(group);
		}
	}

	public void SetSendingEnabled(byte[] disableGroups, byte[] enableGroups)
	{
		if (disableGroups != null)
		{
			foreach (byte item in disableGroups)
			{
				this.blockSendingGroups.Add(item);
			}
		}
		if (enableGroups != null)
		{
			foreach (byte item2 in enableGroups)
			{
				this.blockSendingGroups.Remove(item2);
			}
		}
	}

	public void NewSceneLoaded()
	{
		if (this.loadingLevelAndPausedNetwork)
		{
			this.loadingLevelAndPausedNetwork = false;
			PhotonNetwork.isMessageQueueRunning = true;
		}
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, PhotonView> keyValuePair in this.photonViewList)
		{
			PhotonView value = keyValuePair.Value;
			if (value == null)
			{
				list.Add(keyValuePair.Key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			int key = list[i];
			this.photonViewList.Remove(key);
		}
		if (list.Count > 0 && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
		{
			UnityEngine.Debug.Log("New level loaded. Removed " + list.Count + " scene view IDs from last level.");
		}
	}

	public void RunViewUpdate()
	{
		if (!PhotonNetwork.connected || PhotonNetwork.offlineMode || this.mActors == null)
		{
			return;
		}
		if (this.mActors.Count <= 1)
		{
			return;
		}
		int num = 0;
		this.options.Reset();
		List<int> list = null;
		foreach (KeyValuePair<int, PhotonView> keyValuePair in this.photonViewList)
		{
			PhotonView value = keyValuePair.Value;
			if (value == null)
			{
				/*string format = "PhotonView with ID {0} wasn't properly unregistered! Please report this case to developer@photonengine.com";
				Dictionary<int, PhotonView>.Enumerator enumerator;
				KeyValuePair<int, PhotonView> keyValuePair2 = enumerator.Current;
				UnityEngine.Debug.LogError(string.Format(format, keyValuePair2.Key));
				if (list == null)
				{
					list = new List<int>(4);
				}
				List<int> list2 = list;
				KeyValuePair<int, PhotonView> keyValuePair3 = enumerator.Current;
				list2.Add(keyValuePair3.Key);*/
			}
			else if (value.synchronization != ViewSynchronization.Off && value.isMine && value.gameObject.activeInHierarchy)
			{
				if (!this.blockSendingGroups.Contains(value.group))
				{
					object[] array = this.OnSerializeWrite(value);
					if (array != null)
					{
						if (value.synchronization == ViewSynchronization.ReliableDeltaCompressed || value.mixedModeIsReliable)
						{
							ExitGames.Client.Photon.Hashtable hashtable = null;
							if (!this.dataPerGroupReliable.TryGetValue((int)value.group, out hashtable))
							{
								hashtable = new ExitGames.Client.Photon.Hashtable(NetworkingPeer.ObjectsInOneUpdate);
								this.dataPerGroupReliable[(int)value.group] = hashtable;
							}
							hashtable.Add((byte)(hashtable.Count + 10), array);
							num++;
							if (hashtable.Count >= NetworkingPeer.ObjectsInOneUpdate)
							{
								num -= hashtable.Count;
								this.options.InterestGroup = value.group;
								hashtable[0] = PhotonNetwork.ServerTimestamp;
								if (this.currentLevelPrefix >= 0)
								{
									hashtable[1] = this.currentLevelPrefix;
								}
								this.OpRaiseEvent(206, hashtable, true, this.options);
								hashtable.Clear();
							}
						}
						else
						{
							ExitGames.Client.Photon.Hashtable hashtable2 = null;
							if (!this.dataPerGroupUnreliable.TryGetValue((int)value.group, out hashtable2))
							{
								hashtable2 = new ExitGames.Client.Photon.Hashtable(NetworkingPeer.ObjectsInOneUpdate);
								this.dataPerGroupUnreliable[(int)value.group] = hashtable2;
							}
							hashtable2.Add((byte)(hashtable2.Count + 10), array);
							num++;
							if (hashtable2.Count >= NetworkingPeer.ObjectsInOneUpdate)
							{
								num -= hashtable2.Count;
								this.options.InterestGroup = value.group;
								hashtable2[0] = PhotonNetwork.ServerTimestamp;
								if (this.currentLevelPrefix >= 0)
								{
									hashtable2[1] = this.currentLevelPrefix;
								}
								this.OpRaiseEvent(201, hashtable2, false, this.options);
								hashtable2.Clear();
							}
						}
					}
				}
			}
		}
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				this.photonViewList.Remove(list[i]);
				i++;
			}
		}
		if (num == 0)
		{
			return;
		}
		foreach (int num2 in this.dataPerGroupReliable.Keys)
		{
			this.options.InterestGroup = (byte)num2;
			ExitGames.Client.Photon.Hashtable hashtable3 = this.dataPerGroupReliable[num2];
			if (hashtable3.Count != 0)
			{
				hashtable3[0] = PhotonNetwork.ServerTimestamp;
				if (this.currentLevelPrefix >= 0)
				{
					hashtable3[1] = this.currentLevelPrefix;
				}
				this.OpRaiseEvent(206, hashtable3, true, this.options);
				hashtable3.Clear();
			}
		}
		foreach (int num3 in this.dataPerGroupUnreliable.Keys)
		{
			this.options.InterestGroup = (byte)num3;
			ExitGames.Client.Photon.Hashtable hashtable4 = this.dataPerGroupUnreliable[num3];
			if (hashtable4.Count != 0)
			{
				hashtable4[0] = PhotonNetwork.ServerTimestamp;
				if (this.currentLevelPrefix >= 0)
				{
					hashtable4[1] = this.currentLevelPrefix;
				}
				this.OpRaiseEvent(201, hashtable4, false, this.options);
				hashtable4.Clear();
			}
		}
	}

	private object[] OnSerializeWrite(PhotonView view)
	{
		if (view.synchronization == ViewSynchronization.Off)
		{
			return null;
		}
		PhotonMessageInfo info = new PhotonMessageInfo(this.LocalPlayer, PhotonNetwork.ServerTimestamp, view);
		this.pStream.ResetWriteStream();
		this.pStream.SendNext(null);
		this.pStream.SendNext(null);
		this.pStream.SendNext(null);
		view.SerializeView(this.pStream, info);
		if (this.pStream.Count <= 3)
		{
			return null;
		}
		object[] array = this.pStream.ToArray();
		array[0] = view.viewID;
		array[1] = false;
		array[2] = null;
		if (view.synchronization == ViewSynchronization.Unreliable)
		{
			return array;
		}
		if (view.synchronization == ViewSynchronization.UnreliableOnChange)
		{
			if (this.AlmostEquals(array, view.lastOnSerializeDataSent))
			{
				if (view.mixedModeIsReliable)
				{
					return null;
				}
				view.mixedModeIsReliable = true;
				view.lastOnSerializeDataSent = array;
			}
			else
			{
				view.mixedModeIsReliable = false;
				view.lastOnSerializeDataSent = array;
			}
			return array;
		}
		if (view.synchronization == ViewSynchronization.ReliableDeltaCompressed)
		{
			object[] result = this.DeltaCompressionWrite(view.lastOnSerializeDataSent, array);
			view.lastOnSerializeDataSent = array;
			return result;
		}
		return null;
	}

	private void OnSerializeRead(object[] data, PhotonPlayer sender, int networkTime, short correctPrefix)
	{
		int num = (int)data[0];
		PhotonView photonView = this.GetPhotonView(num);
		if (photonView == null)
		{
			UnityEngine.Debug.LogWarning(string.Concat(new object[]
			{
				"Received OnSerialization for view ID ",
				num,
				". We have no such PhotonView! Ignored this if you're leaving a room. State: ",
				this.State
			}));
			return;
		}
		if (photonView.prefix > 0 && (int)correctPrefix != photonView.prefix)
		{
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"Received OnSerialization for view ID ",
				num,
				" with prefix ",
				correctPrefix,
				". Our prefix is ",
				photonView.prefix
			}));
			return;
		}
		if (photonView.group != 0 && !this.allowedReceivingGroups.Contains(photonView.group))
		{
			return;
		}
		if (photonView.synchronization == ViewSynchronization.ReliableDeltaCompressed)
		{
			object[] array = this.DeltaCompressionRead(photonView.lastOnSerializeDataReceived, data);
			if (array == null)
			{
				if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
				{
					UnityEngine.Debug.Log(string.Concat(new object[]
					{
						"Skipping packet for ",
						photonView.name,
						" [",
						photonView.viewID,
						"] as we haven't received a full packet for delta compression yet. This is OK if it happens for the first few frames after joining a game."
					}));
				}
				return;
			}
			photonView.lastOnSerializeDataReceived = array;
			data = array;
		}
		if (sender.ID != photonView.ownerId && (!photonView.OwnerShipWasTransfered || photonView.ownerId == 0) && photonView.currentMasterID == -1)
		{
			photonView.ownerId = sender.ID;
		}
		this.readStream.SetReadStream(data, 3);
		PhotonMessageInfo info = new PhotonMessageInfo(sender, networkTime, photonView);
		photonView.DeserializeView(this.readStream, info);
	}

	private object[] DeltaCompressionWrite(object[] previousContent, object[] currentContent)
	{
		if (currentContent == null || previousContent == null || previousContent.Length != currentContent.Length)
		{
			return currentContent;
		}
		if (currentContent.Length <= 3)
		{
			return null;
		}
		previousContent[1] = false;
		int num = 0;
		Queue<int> queue = null;
		for (int i = 3; i < currentContent.Length; i++)
		{
			object obj = currentContent[i];
			object two = previousContent[i];
			if (this.AlmostEquals(obj, two))
			{
				num++;
				previousContent[i] = null;
			}
			else
			{
				previousContent[i] = obj;
				if (obj == null)
				{
					if (queue == null)
					{
						queue = new Queue<int>(currentContent.Length);
					}
					queue.Enqueue(i);
				}
			}
		}
		if (num > 0)
		{
			if (num == currentContent.Length - 3)
			{
				return null;
			}
			previousContent[1] = true;
			if (queue != null)
			{
				previousContent[2] = queue.ToArray();
			}
		}
		previousContent[0] = currentContent[0];
		return previousContent;
	}

	private object[] DeltaCompressionRead(object[] lastOnSerializeDataReceived, object[] incomingData)
	{
		if (!(bool)incomingData[1])
		{
			return incomingData;
		}
		if (lastOnSerializeDataReceived == null)
		{
			return null;
		}
		int[] array = incomingData[2] as int[];
		for (int i = 3; i < incomingData.Length; i++)
		{
			if (array == null || !array.Contains(i))
			{
				if (incomingData[i] == null)
				{
					object obj = lastOnSerializeDataReceived[i];
					incomingData[i] = obj;
				}
			}
		}
		return incomingData;
	}

	private bool AlmostEquals(object[] lastData, object[] currentContent)
	{
		if (lastData == null && currentContent == null)
		{
			return true;
		}
		if (lastData == null || currentContent == null || lastData.Length != currentContent.Length)
		{
			return false;
		}
		for (int i = 0; i < currentContent.Length; i++)
		{
			object one = currentContent[i];
			object two = lastData[i];
			if (!this.AlmostEquals(one, two))
			{
				return false;
			}
		}
		return true;
	}

	private bool AlmostEquals(object one, object two)
	{
		if (one == null || two == null)
		{
			return one == null && two == null;
		}
		if (!one.Equals(two))
		{
			if (one is Vector3)
			{
				Vector3 target = (Vector3)one;
				Vector3 second = (Vector3)two;
				if (target.AlmostEquals(second, PhotonNetwork.precisionForVectorSynchronization))
				{
					return true;
				}
			}
			else if (one is Vector2)
			{
				Vector2 target2 = (Vector2)one;
				Vector2 second2 = (Vector2)two;
				if (target2.AlmostEquals(second2, PhotonNetwork.precisionForVectorSynchronization))
				{
					return true;
				}
			}
			else if (one is Quaternion)
			{
				Quaternion target3 = (Quaternion)one;
				Quaternion second3 = (Quaternion)two;
				if (target3.AlmostEquals(second3, PhotonNetwork.precisionForQuaternionSynchronization))
				{
					return true;
				}
			}
			else if (one is float)
			{
				float target4 = (float)one;
				float second4 = (float)two;
				if (target4.AlmostEquals(second4, PhotonNetwork.precisionForFloatSynchronization))
				{
					return true;
				}
			}
			return false;
		}
		return true;
	}

	protected internal static bool GetMethod(MonoBehaviour monob, string methodType, out MethodInfo mi)
	{
		mi = null;
		if (monob == null || string.IsNullOrEmpty(methodType))
		{
			return false;
		}
		List<MethodInfo> methods = SupportClass.GetMethods(monob.GetType(), null);
		for (int i = 0; i < methods.Count; i++)
		{
			MethodInfo methodInfo = methods[i];
			if (methodInfo.Name.Equals(methodType))
			{
				mi = methodInfo;
				return true;
			}
		}
		return false;
	}

	protected internal void LoadLevelIfSynced()
	{
		if (!PhotonNetwork.automaticallySyncScene || PhotonNetwork.isMasterClient || PhotonNetwork.room == null)
		{
			return;
		}
		if (!PhotonNetwork.room.CustomProperties.ContainsKey("curScn"))
		{
			return;
		}
		object obj = PhotonNetwork.room.CustomProperties["curScn"];
		if (obj is int)
		{
			if (SceneManagerHelper.ActiveSceneBuildIndex != (int)obj)
			{
				PhotonNetwork.LoadLevel((int)obj);
			}
		}
		else if (obj is string && SceneManagerHelper.ActiveSceneName != (string)obj)
		{
			PhotonNetwork.LoadLevel((string)obj);
		}
	}

	protected internal void SetLevelInPropsIfSynced(object levelId)
	{
		if (!PhotonNetwork.automaticallySyncScene || !PhotonNetwork.isMasterClient || PhotonNetwork.room == null)
		{
			return;
		}
		if (levelId == null)
		{
			UnityEngine.Debug.LogError("Parameter levelId can't be null!");
			return;
		}
		if (PhotonNetwork.room.CustomProperties.ContainsKey("curScn"))
		{
			object obj = PhotonNetwork.room.CustomProperties["curScn"];
			if (obj is int && SceneManagerHelper.ActiveSceneBuildIndex == (int)obj)
			{
				return;
			}
			if (obj is string && SceneManagerHelper.ActiveSceneName != null && SceneManagerHelper.ActiveSceneName.Equals((string)obj))
			{
				return;
			}
		}
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		if (levelId is int)
		{
			hashtable["curScn"] = (int)levelId;
		}
		else if (levelId is string)
		{
			hashtable["curScn"] = (string)levelId;
		}
		else
		{
			UnityEngine.Debug.LogError("Parameter levelId must be int or string!");
		}
		PhotonNetwork.room.SetCustomProperties(hashtable, null, false);
		this.SendOutgoingCommands();
	}

	public void SetApp(string appId, string gameVersion)
	{
		this.AppId = appId.Trim();
		if (!string.IsNullOrEmpty(gameVersion))
		{
			PhotonNetwork.gameVersion = gameVersion.Trim();
		}
	}

	public bool WebRpc(string uriPath, object parameters)
	{
		return this.OpCustom(219, new Dictionary<byte, object>
		{
			{
				209,
				uriPath
			},
			{
				208,
				parameters
			}
		}, true);
	}

	protected internal string AppId;

	private string tokenCache;

	public AuthModeOption AuthMode;

	public EncryptionMode EncryptionMode;

	public const string NameServerHost = "ns.exitgames.com";

	public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";

	private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort = new Dictionary<ConnectionProtocol, int>
	{
		{
			ConnectionProtocol.Udp,
			5058
		},
		{
			ConnectionProtocol.Tcp,
			4533
		},
		{
			ConnectionProtocol.WebSocket,
			9093
		},
		{
			ConnectionProtocol.WebSocketSecure,
			19093
		}
	};

	public bool IsInitialConnect;

	public bool insideLobby;

	protected internal List<TypedLobbyInfo> LobbyStatistics = new List<TypedLobbyInfo>();

	public Dictionary<string, RoomInfo> mGameList = new Dictionary<string, RoomInfo>();

	public RoomInfo[] mGameListCopy = new RoomInfo[0];

	private string playername = string.Empty;

	private bool mPlayernameHasToBeUpdated;

	private Room currentRoom;

	private JoinType lastJoinType;

	protected internal EnterRoomParams enterRoomParamsCache;

	private bool didAuthenticate;

	private string[] friendListRequested;

	private int friendListTimestamp;

	private bool isFetchingFriendList;

	public Dictionary<int, PhotonPlayer> mActors = new Dictionary<int, PhotonPlayer>();

	public PhotonPlayer[] mOtherPlayerListCopy = new PhotonPlayer[0];

	public PhotonPlayer[] mPlayerListCopy = new PhotonPlayer[0];

	public bool hasSwitchedMC;

	private HashSet<byte> allowedReceivingGroups = new HashSet<byte>();

	private HashSet<byte> blockSendingGroups = new HashSet<byte>();

	protected internal Dictionary<int, PhotonView> photonViewList = new Dictionary<int, PhotonView>();

	private readonly PhotonStream readStream = new PhotonStream(false, null);

	private readonly PhotonStream pStream = new PhotonStream(true, null);

	private readonly Dictionary<int, ExitGames.Client.Photon.Hashtable> dataPerGroupReliable = new Dictionary<int, ExitGames.Client.Photon.Hashtable>();

	private readonly Dictionary<int, ExitGames.Client.Photon.Hashtable> dataPerGroupUnreliable = new Dictionary<int, ExitGames.Client.Photon.Hashtable>();

	protected internal short currentLevelPrefix;

	protected internal bool loadingLevelAndPausedNetwork;

	protected internal const string CurrentSceneProperty = "curScn";

	public static bool UsePrefabCache = true;

	internal IPunPrefabPool ObjectPool;

	public static Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();

	private Dictionary<Type, List<MethodInfo>> monoRPCMethodsCache = new Dictionary<Type, List<MethodInfo>>();

	private readonly Dictionary<string, int> rpcShortcuts;

	private static readonly string OnPhotonInstantiateString = PhotonNetworkingMessage.OnPhotonInstantiate.ToString();

	private string cachedServerAddress;

	private string cachedApplicationName;

	private ServerConnection cachedProtocolType;

	private bool _isReconnecting;

	private Dictionary<int, object[]> tempInstantiationData = new Dictionary<int, object[]>();

	public static int ObjectsInOneUpdate = 10;

	private RaiseEventOptions options = new RaiseEventOptions();

	public const int SyncViewId = 0;

	public const int SyncCompressed = 1;

	public const int SyncNullValues = 2;

	public const int SyncFirstValue = 3;
}
