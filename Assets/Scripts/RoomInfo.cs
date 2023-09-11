using System;
using ExitGames.Client.Photon;

public class RoomInfo
{
	protected internal RoomInfo(string roomName, Hashtable properties)
	{
		this.InternalCacheProperties(properties);
		this.nameField = roomName;
	}

	public bool removedFromList { get; internal set; }

	protected internal bool serverSideMasterClient { get; private set; }

	public Hashtable CustomProperties
	{
		get
		{
			return this.customPropertiesField;
		}
	}

	public string Name
	{
		get
		{
			return this.nameField;
		}
	}

	public int PlayerCount { get; private set; }

	public bool IsLocalClientInside { get; set; }

	public byte MaxPlayers
	{
		get
		{
			return this.maxPlayersField;
		}
	}

	public bool IsOpen
	{
		get
		{
			return this.openField;
		}
	}

	public bool IsVisible
	{
		get
		{
			return this.visibleField;
		}
	}

	public override bool Equals(object other)
	{
		RoomInfo roomInfo = other as RoomInfo;
		return roomInfo != null && this.Name.Equals(roomInfo.nameField);
	}

	public override int GetHashCode()
	{
		return this.nameField.GetHashCode();
	}

	public override string ToString()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", new object[]
		{
			this.nameField,
			(!this.visibleField) ? "hidden" : "visible",
			(!this.openField) ? "closed" : "open",
			this.maxPlayersField,
			this.PlayerCount
		});
	}

	public string ToStringFull()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", new object[]
		{
			this.nameField,
			(!this.visibleField) ? "hidden" : "visible",
			(!this.openField) ? "closed" : "open",
			this.maxPlayersField,
			this.PlayerCount,
			this.customPropertiesField.ToStringFull()
		});
	}

	protected internal void InternalCacheProperties(Hashtable propertiesToCache)
	{
		if (propertiesToCache == null || propertiesToCache.Count == 0 || this.customPropertiesField.Equals(propertiesToCache))
		{
			return;
		}
		if (propertiesToCache.ContainsKey(251))
		{
			this.removedFromList = (bool)propertiesToCache[251];
			if (this.removedFromList)
			{
				return;
			}
		}
		if (propertiesToCache.ContainsKey(255))
		{
			this.maxPlayersField = (byte)propertiesToCache[byte.MaxValue];
		}
		if (propertiesToCache.ContainsKey(253))
		{
			this.openField = (bool)propertiesToCache[253];
		}
		if (propertiesToCache.ContainsKey(254))
		{
			this.visibleField = (bool)propertiesToCache[254];
		}
		if (propertiesToCache.ContainsKey(252))
		{
			this.PlayerCount = (int)((byte)propertiesToCache[252]);
		}
		if (propertiesToCache.ContainsKey(249))
		{
			this.autoCleanUpField = (bool)propertiesToCache[249];
		}
		if (propertiesToCache.ContainsKey(248))
		{
			this.serverSideMasterClient = true;
			bool flag = this.masterClientIdField != 0;
			this.masterClientIdField = (int)propertiesToCache[248];
			if (flag)
			{
				PhotonNetwork.networkingPeer.UpdateMasterClient();
			}
		}
		if (propertiesToCache.ContainsKey(247))
		{
			this.expectedUsersField = (string[])propertiesToCache[247];
		}
		this.customPropertiesField.MergeStringKeys(propertiesToCache);
		this.customPropertiesField.StripKeysWithNullValues();
	}

	[Obsolete("Please use CustomProperties (updated case for naming).")]
	public Hashtable customProperties
	{
		get
		{
			return this.CustomProperties;
		}
	}

	[Obsolete("Please use Name (updated case for naming).")]
	public string name
	{
		get
		{
			return this.Name;
		}
	}

	[Obsolete("Please use PlayerCount (updated case for naming).")]
	public int playerCount
	{
		get
		{
			return this.PlayerCount;
		}
		set
		{
			this.PlayerCount = value;
		}
	}

	[Obsolete("Please use IsLocalClientInside (updated case for naming).")]
	public bool isLocalClientInside
	{
		get
		{
			return this.IsLocalClientInside;
		}
		set
		{
			this.IsLocalClientInside = value;
		}
	}

	[Obsolete("Please use MaxPlayers (updated case for naming).")]
	public byte maxPlayers
	{
		get
		{
			return this.MaxPlayers;
		}
	}

	[Obsolete("Please use IsOpen (updated case for naming).")]
	public bool open
	{
		get
		{
			return this.IsOpen;
		}
	}

	[Obsolete("Please use IsVisible (updated case for naming).")]
	public bool visible
	{
		get
		{
			return this.IsVisible;
		}
	}

	private Hashtable customPropertiesField = new Hashtable();

	protected byte maxPlayersField;

	protected string[] expectedUsersField;

	protected bool openField = true;

	protected bool visibleField = true;

	protected bool autoCleanUpField = PhotonNetwork.autoCleanUpPlayerObjects;

	protected string nameField;

	protected internal int masterClientIdField;
}
