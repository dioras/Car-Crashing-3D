using System;
using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class SmoothSyncMovement : Photon.MonoBehaviour, IPunObservable
{
	public void Awake()
	{
		bool flag = false;
		foreach (Component x in base.photonView.ObservedComponents)
		{
			if (x == this)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			UnityEngine.Debug.LogWarning(this + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used.");
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(base.transform.rotation);
		}
		else
		{
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}

	public void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
		}
	}

	public float SmoothingDelay = 5f;

	private Vector3 correctPlayerPos = Vector3.zero;

	private Quaternion correctPlayerRot = Quaternion.identity;
}
