using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RammingChecker : MonoBehaviour
{
	private void Start()
	{
		this.tView = base.GetComponent<PhotonTransformView>();
		if (!base.gameObject.GetPhotonView().isMine || !PhotonNetwork.inRoom)
		{
			base.enabled = false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!base.enabled)
		{
			return;
		}
		if (collision.collider.gameObject.layer != 26)
		{
			return;
		}
		if (this.ShowingRammingMessage)
		{
			return;
		}
		if (Vector3.Angle(collision.gameObject.transform.position - base.transform.position, base.transform.forward) < 60f)
		{
			return;
		}
		PhotonView view = collision.gameObject.GetPhotonView();
		if (view == null)
		{
			return;
		}
		RammingChecker.RammingVehicle rammingVehicle = this.rammingVehicles.Find((RammingChecker.RammingVehicle rv) => rv.pView.Equals(view));
		if (rammingVehicle != null && rammingVehicle.AllowCollisions)
		{
			return;
		}
		if (rammingVehicle == null)
		{
			this.rammingVehicles.Add(new RammingChecker.RammingVehicle(collision.gameObject.GetPhotonView()));
		}
		else if (rammingVehicle.RammingCount == 10)
		{
			this.currentRammingVehicle = rammingVehicle;
			this.ShowBlockCollisionMessage();
		}
		else if (rammingVehicle.TimeSinceLastRamming > 1f)
		{
			rammingVehicle.RammingCount++;
			rammingVehicle.TimeSinceLastRamming = 0f;
		}
	}

	private void Update()
	{
		for (int i = 0; i < this.rammingVehicles.Count; i++)
		{
			this.rammingVehicles[i].TimeSinceLastRamming += Time.deltaTime;
			if (this.rammingVehicles[i].TimeSinceLastRamming > 60f)
			{
				this.rammingVehicles.RemoveAt(i);
			}
		}
		if (CrossPlatformInputManager.GetButtonDown("BlockCollisions"))
		{
			this.BlockCollisions();
		}
		if (CrossPlatformInputManager.GetButtonDown("AllowCollisions"))
		{
			this.AllowCollisions();
		}
	}

	private void ShowBlockCollisionMessage()
	{
		if (VehicleLoader.Instance.droneMode)
		{
			return;
		}
		this.ShowingRammingMessage = true;
		CarUIControl.Instance.ShowRammingWindow(this.currentRammingVehicle.playerName);
	}

	private void BlockCollisions()
	{
		UnityEngine.Debug.Log("Blocking collisions");
		UnityEngine.Debug.Log(this.currentRammingVehicle.playerName);
		foreach (Collider collider in this.currentRammingVehicle.pView.gameObject.GetComponentsInChildren<Collider>())
		{
			collider.enabled = false;
		}
		this.tView.SendDisableMyCollidersEvent(this.currentRammingVehicle.pView.owner);
		this.rammingVehicles.Remove(this.currentRammingVehicle);
		this.currentRammingVehicle = null;
	}

	private void AllowCollisions()
	{
		UnityEngine.Debug.Log("Allowing collisions");
		this.currentRammingVehicle.AllowCollisions = true;
		this.currentRammingVehicle = null;
	}

	public List<RammingChecker.RammingVehicle> rammingVehicles;

	private RammingChecker.RammingVehicle currentRammingVehicle;

	private bool ShowingRammingMessage;

	private PhotonTransformView tView;

	[Serializable]
	public class RammingVehicle
	{
		public RammingVehicle(PhotonView view)
		{
			this.pView = view;
			this.TimeSinceLastRamming = 0f;
			this.RammingCount = 1;
			this.playerName = view.owner.CustomProperties["DisplayName"].ToString();
		}

		public PhotonView pView;

		public int RammingCount;

		public float TimeSinceLastRamming;

		public string playerName;

		public bool AllowCollisions;
	}
}
