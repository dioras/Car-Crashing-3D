using System;
using UnityEngine;

public class PlayerRouteCheckpoint : MonoBehaviour
{
	public void ToggleTapTarget(bool on)
	{
		this.checkpointTapTarget.SetActive(on);
	}

	public int checkpointID;

	public GameObject checkpointTapTarget;
}
