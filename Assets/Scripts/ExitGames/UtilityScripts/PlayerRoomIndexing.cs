using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

namespace ExitGames.UtilityScripts
{
	public class PlayerRoomIndexing : PunBehaviour
	{
		public int[] PlayerIds
		{
			get
			{
				return this._playerIds;
			}
		}

		public void Awake()
		{
			if (PlayerRoomIndexing.instance != null)
			{
				UnityEngine.Debug.LogError("Existing instance of PlayerRoomIndexing found. Only One instance is required at the most. Please correct and have only one at any time.");
			}
			PlayerRoomIndexing.instance = this;
			if (PhotonNetwork.room != null)
			{
				this.SanitizeIndexing(true);
			}
		}

		public override void OnJoinedRoom()
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.AssignIndex(PhotonNetwork.player);
			}
			else
			{
				this.RefreshData();
			}
		}

		public override void OnLeftRoom()
		{
			this.RefreshData();
		}

		public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.AssignIndex(newPlayer);
			}
		}

		public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.UnAssignIndex(otherPlayer);
			}
		}

		public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
		{
			if (propertiesThatChanged.ContainsKey("PlayerIndexes"))
			{
				this.RefreshData();
			}
		}

		public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
		{
			if (PhotonNetwork.isMasterClient)
			{
				this.SanitizeIndexing(false);
			}
		}

		public int GetRoomIndex(PhotonPlayer player)
		{
			if (this._indexesLUT != null && this._indexesLUT.ContainsKey(player.ID))
			{
				return this._indexesLUT[player.ID];
			}
			return -1;
		}

		private void SanitizeIndexing(bool forceIndexing = false)
		{
			if (!forceIndexing && !PhotonNetwork.isMasterClient)
			{
				return;
			}
			if (PhotonNetwork.room == null)
			{
				return;
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			if (PhotonNetwork.room.CustomProperties.TryGetValue("PlayerIndexes", out this._indexes))
			{
				dictionary = (this._indexes as Dictionary<int, int>);
			}
			if (dictionary.Count != PhotonNetwork.room.PlayerCount)
			{
				foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
				{
					if (!dictionary.ContainsKey(photonPlayer.ID))
					{
						this.AssignIndex(photonPlayer);
					}
				}
			}
		}

		private void RefreshData()
		{
			if (PhotonNetwork.room != null)
			{
				this._playerIds = new int[PhotonNetwork.room.MaxPlayers];
				if (PhotonNetwork.room.CustomProperties.TryGetValue("PlayerIndexes", out this._indexes))
				{
					this._indexesLUT = (this._indexes as Dictionary<int, int>);
					foreach (KeyValuePair<int, int> keyValuePair in this._indexesLUT)
					{
						this._p = PhotonPlayer.Find(keyValuePair.Key);
						this._playerIds[keyValuePair.Value] = this._p.ID;
					}
				}
			}
			else
			{
				this._playerIds = new int[0];
			}
			if (this.OnRoomIndexingChanged != null)
			{
				this.OnRoomIndexingChanged();
			}
		}

		private void AssignIndex(PhotonPlayer player)
		{
			if (PhotonNetwork.room.CustomProperties.TryGetValue("PlayerIndexes", out this._indexes))
			{
				this._indexesLUT = (this._indexes as Dictionary<int, int>);
			}
			else
			{
				this._indexesLUT = new Dictionary<int, int>();
			}
			List<bool> list = new List<bool>(new bool[PhotonNetwork.room.MaxPlayers]);
			foreach (KeyValuePair<int, int> keyValuePair in this._indexesLUT)
			{
				list[keyValuePair.Value] = true;
			}
			this._indexesLUT[player.ID] = Mathf.Max(0, list.IndexOf(false));
			PhotonNetwork.room.SetCustomProperties(new Hashtable
			{
				{
					"PlayerIndexes",
					this._indexesLUT
				}
			}, null, false);
			this.RefreshData();
		}

		private void UnAssignIndex(PhotonPlayer player)
		{
			if (PhotonNetwork.room.CustomProperties.TryGetValue("PlayerIndexes", out this._indexes))
			{
				this._indexesLUT = (this._indexes as Dictionary<int, int>);
				this._indexesLUT.Remove(player.ID);
				PhotonNetwork.room.SetCustomProperties(new Hashtable
				{
					{
						"PlayerIndexes",
						this._indexesLUT
					}
				}, null, false);
			}
			this.RefreshData();
		}

		public static PlayerRoomIndexing instance;

		public PlayerRoomIndexing.RoomIndexingChanged OnRoomIndexingChanged;

		public const string RoomPlayerIndexedProp = "PlayerIndexes";

		private int[] _playerIds;

		private object _indexes;

		private Dictionary<int, int> _indexesLUT;

		private List<bool> _indexesPool;

		private PhotonPlayer _p;

		public delegate void RoomIndexingChanged();
	}
}
