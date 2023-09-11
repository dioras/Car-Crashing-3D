using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : PunBehaviour
{
	public ChatBox()
	{
		if (ChatBox.Instance == null)
		{
			ChatBox.Instance = this;
		}
	}

	private PhotonTransformView photonTransformView
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerTView;
			}
			return null;
		}
	}

	private void Awake()
	{
		ChatBox.Instance = this;
		this.ChatExpanded = false;
		this.myColor = this.colors[UnityEngine.Random.Range(0, this.colors.Length)];
		this.myName = PhotonNetwork.playerName;
		if (GameState.GameMode != GameMode.Multiplayer)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.playerTabTemplate.SetActive(false);
		this.ChangeTab(true);
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		this.UpdatePlayersList();
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		this.UpdatePlayersList();
	}

	public void ChangeTab(bool chat)
	{
		this.chatButton.GetComponent<Image>().color = ((!chat) ? this.deselectedButtonColor : this.selectedButtonColor);
		this.playerListButton.GetComponent<Image>().color = ((!chat) ? this.selectedButtonColor : this.deselectedButtonColor);
		this.chatTab.SetActive(chat);
		this.playerListTab.SetActive(!chat);
		this.UpdatePlayersList();
	}

	public void TouchUp()
	{
		if (this.DraggedScroll)
		{
			return;
		}
		this.OpenKeyboard();
	}

	public void DragBegan()
	{
		this.DraggedScroll = true;
		this.CustomScrollPos = true;
	}

	public void DragEnded()
	{
		this.DraggedScroll = false;
	}

	private void StartBlinking()
	{
		if (this.BlinkingCoroutine != null)
		{
			return;
		}
		this.BlinkingCoroutine = base.StartCoroutine(this.Blinking());
	}

	private IEnumerator Blinking()
	{
		Color tempColor = this.ChatTabArrow.color;
		for (float f = 0f; f < 4f; f += 0.1f)
		{
			tempColor.a = 1f - Mathf.PingPong(f, 1f);
			this.ChatTabArrow.color = tempColor;
			yield return null;
		}
		this.BlinkingCoroutine = null;
		yield break;
	}

	public void OpenKeyboard()
	{
		this.keyboard = TouchScreenKeyboard.Open(string.Empty, TouchScreenKeyboardType.Default);
	}

	private void Update()
	{
		if (this.keyboard != null && this.keyboard.done)
		{
			this.SendChatMessage(this.keyboard.text);
			this.keyboard = null;
		}
		if (this.photonTransformView != null && !this.joiningMessageSent)
		{
			this.SendJoiningMessage();
		}
		if (!this.CustomScrollPos)
		{
			this.scrollRect.verticalNormalizedPosition = 0f;
		}
		float b = (float)((!this.ChatExpanded) ? 0 : 1);
		this.expandRatio = Mathf.Lerp(this.expandRatio, b, Time.deltaTime * 10f);
		Vector3 a = new Vector3(-568f, 0f, 0f);
		Vector3 b2 = new Vector3(-400f, 0f, 0f);
		this.ChatBoxHolder.localPosition = Vector3.Lerp(a, b2, this.expandRatio);
		this.chatButton.alpha = this.expandRatio;
		this.playerListButton.alpha = this.expandRatio;
	}

	public void ToggleChat()
	{
		this.ChatExpanded = !this.ChatExpanded;
	}

	public void UpdatePlayersList()
	{
		foreach (PlayerTab playerTab in this.playerTabs)
		{
			if (playerTab != null)
			{
				UnityEngine.Object.Destroy(playerTab.gameObject);
			}
		}
		this.playerTabs.Clear();
		foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
		{
			PlayerTab component = UnityEngine.Object.Instantiate<GameObject>(this.playerTabTemplate, this.playerTabTemplate.transform.parent).GetComponent<PlayerTab>();
			component.Fill(photonPlayer, this.mutedPlayers.Contains(photonPlayer));
			component.gameObject.SetActive(true);
			this.playerTabs.Add(component);
		}
	}

	public void MutePlayer(PhotonPlayer player)
	{
		if (!this.mutedPlayers.Contains(player))
		{
			this.mutedPlayers.Add(player);
		}
	}

	public void UnMutePlayer(PhotonPlayer player)
	{
		if (this.mutedPlayers.Contains(player))
		{
			this.mutedPlayers.Remove(player);
		}
	}

	private void SendChatMessage(string msg)
	{
		if (msg.Trim().Length == 0)
		{
			return;
		}
		msg = Utility.CleanBadWords(msg);
		msg = this.AddMyName(msg);
		msg = this.AddLineBreak(msg);
		this.photonTransformView.SendChatMessage(msg);
	}

	public void SendJoiningMessage()
	{
		string msg = "\n<i>" + this.myName + " has joined </i>";
		this.photonTransformView.SendChatMessage(msg);
		this.joiningMessageSent = true;
	}

	public void ReceiveChatMessage(string msg, PhotonPlayer fromPlayer)
	{
		if (this.mutedPlayers.Contains(fromPlayer))
		{
			return;
		}
		Text chatText = this.ChatText;
		chatText.text += msg;
		this.scrollRect.verticalNormalizedPosition = 0f;
		this.CustomScrollPos = false;
		if (msg.Contains("</color>"))
		{
			this.StartBlinking();
		}
	}

	private string AddMyName(string source)
	{
		return string.Concat(new string[]
		{
			"<color=",
			this.myColor,
			">[",
			this.myName,
			"] </color>",
			source
		});
	}

	private string AddLineBreak(string source)
	{
		return "\n" + source;
	}

	public static ChatBox Instance;

	public Text ChatText;

	public ScrollRect scrollRect;

	public RectTransform ChatBoxHolder;

	public Image ChatTabArrow;

	public GameObject playerTabTemplate;

	public GameObject chatTab;

	public GameObject playerListTab;

	public CanvasGroup chatButton;

	public CanvasGroup playerListButton;

	public Color selectedButtonColor;

	public Color deselectedButtonColor;

	private string myName;

	private string[] colors = new string[]
	{
		"#00ffffff",
		"#ff00ffff",
		"#a52a2aff",
		"#00ff00ff",
		"#add8e6ff",
		"#ffa500ff",
		"#ffff00ff"
	};

	private string myColor;

	private bool DraggedScroll;

	private bool CustomScrollPos;

	private bool ChatExpanded;

	private float expandRatio;

	private TouchScreenKeyboard keyboard;

	private Coroutine BlinkingCoroutine;

	private bool joiningMessageSent;

	public List<PhotonPlayer> mutedPlayers = new List<PhotonPlayer>();

	public List<PlayerTab> playerTabs = new List<PlayerTab>();
}
