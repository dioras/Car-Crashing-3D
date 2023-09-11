using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTab : MonoBehaviour
{
	private void Start()
	{
		this.cBox = ChatBox.Instance;
	}

	public void Fill(PhotonPlayer player, bool muted)
	{
		this.playerNameText.text = player.NickName;
		bool flag = player == PhotonNetwork.player;
		this.muteButton.SetActive(!muted && !flag);
		this.unMuteButton.SetActive(muted && !flag);
		this.playerRef = player;
		this.filled = true;
		base.GetComponent<Image>().color = ((!flag) ? this.otherPlayerColor : this.localPlayerColor);
	}

	public void MutePlayer()
	{
		this.cBox.MutePlayer(this.playerRef);
		this.muteButton.SetActive(false);
		this.unMuteButton.SetActive(true);
	}

	public void UnMutePlayer()
	{
		this.cBox.UnMutePlayer(this.playerRef);
		this.muteButton.SetActive(true);
		this.unMuteButton.SetActive(false);
	}

	public Text playerNameText;

	public GameObject muteButton;

	public GameObject unMuteButton;

	public PhotonPlayer playerRef;

	public Color otherPlayerColor;

	public Color localPlayerColor;

	private bool filled;

	private ChatBox cBox;
}
