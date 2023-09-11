using System;
using UnityEngine;
using UnityEngine.UI;

public class DroneInfoUI : MonoBehaviour
{
	public void Populate(string name, bool isMember, DroneController drone)
	{
		this.PlayerName.text = name;
		this.myDrone = drone;
		this.alpha = 1f;
		this.badge.color = ((!isMember) ? this.badgeColorNormal : this.badgeColorMember);
	}

	private void Update()
	{
		if (this.myDrone == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		this.myCanvasGroup.alpha = this.alpha;
	}

	public Text PlayerName;

	public Image badge;

	public DroneController myDrone;

	public CanvasGroup myCanvasGroup;

	public float alpha;

	public Color badgeColorNormal;

	public Color badgeColorMember;
}
