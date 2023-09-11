using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
	public void Populate(string name, bool isMember, PhotonView view)
	{
		this.MemberLogo.SetActive(isMember);
		this.MemberBadge.SetActive(isMember);
		this.NormalBadge.SetActive(!isMember);
		this.PlayerName.text = name;
		this.myView = view;
		this.alpha = 1f;
		this.member = isMember;
	}

	public void ToggleDroneBadge(bool drone)
	{
		this.inDrone = drone;
		this.droneBadge.SetActive(drone);
		this.NormalBadge.SetActive(!drone && !this.member);
		this.MemberBadge.SetActive(!drone && this.member);
	}

	private void Update()
	{
		if (this.myView == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		this.myCanvasGroup.alpha = this.alpha;
	}

	public GameObject MemberLogo;

	public GameObject MemberBadge;

	public GameObject NormalBadge;

	public GameObject droneBadge;

	public Text PlayerName;

	public PhotonView myView;

	public CanvasGroup myCanvasGroup;

	public float alpha;

	private bool member;

	private bool inDrone;
}
