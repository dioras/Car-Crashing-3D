using System;
using ExitGames.Client.Photon;
using UnityEngine;

public static class TeamExtensions
{
	public static PunTeams.Team GetTeam(this PhotonPlayer player)
	{
		object obj;
		if (player.CustomProperties.TryGetValue("team", out obj))
		{
			return (PunTeams.Team)obj;
		}
		return PunTeams.Team.none;
	}

	public static void SetTeam(this PhotonPlayer player, PunTeams.Team team)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			UnityEngine.Debug.LogWarning("JoinTeam was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
			return;
		}
		PunTeams.Team team2 = player.GetTeam();
		if (team2 != team || team2 == PunTeams.Team.none)
		{
			UnityEngine.Debug.Log("Setting the custom property");
			player.SetCustomProperties(new Hashtable
			{
				{
					"team",
					(byte)team
				}
			}, null, false);
		}
	}
}
