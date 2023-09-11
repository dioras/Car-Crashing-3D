using System;
using ExitGames.Client.Photon;

public static class TurnExtensions
{
	public static void SetTurn(this Room room, int turn, bool setStartTime = false)
	{
		if (room == null || room.CustomProperties == null)
		{
			return;
		}
		Hashtable hashtable = new Hashtable();
		hashtable[TurnExtensions.TurnPropKey] = turn;
		if (setStartTime)
		{
			hashtable[TurnExtensions.TurnStartPropKey] = PhotonNetwork.ServerTimestamp;
		}
		room.SetCustomProperties(hashtable, null, false);
	}

	public static int GetTurn(this RoomInfo room)
	{
		if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnExtensions.TurnPropKey))
		{
			return 0;
		}
		return (int)room.CustomProperties[TurnExtensions.TurnPropKey];
	}

	public static int GetTurnStart(this RoomInfo room)
	{
		if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnExtensions.TurnStartPropKey))
		{
			return 0;
		}
		return (int)room.CustomProperties[TurnExtensions.TurnStartPropKey];
	}

	public static int GetFinishedTurn(this PhotonPlayer player)
	{
		Room room = PhotonNetwork.room;
		if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnExtensions.TurnPropKey))
		{
			return 0;
		}
		string key = TurnExtensions.FinishedTurnPropKey + player.ID;
		return (int)room.CustomProperties[key];
	}

	public static void SetFinishedTurn(this PhotonPlayer player, int turn)
	{
		Room room = PhotonNetwork.room;
		if (room == null || room.CustomProperties == null)
		{
			return;
		}
		string key = TurnExtensions.FinishedTurnPropKey + player.ID;
		Hashtable hashtable = new Hashtable();
		hashtable[key] = turn;
		room.SetCustomProperties(hashtable, null, false);
	}

	public static readonly string TurnPropKey = "Turn";

	public static readonly string TurnStartPropKey = "TStart";

	public static readonly string FinishedTurnPropKey = "FToA";
}
