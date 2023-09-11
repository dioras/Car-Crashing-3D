using System;
using CustomVP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRigidbodyView : MonoBehaviour, IPunObservable
{
	private void Awake()
	{
		if (!PhotonNetwork.inRoom || SceneManager.GetActiveScene().name.ToLower() == "menu")
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.carController = base.GetComponent<CarController>();
		base.Invoke("CheckForces", 10f);
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(this.rb.velocity);
			stream.SendNext(this.rb.angularVelocity);
		}
		else if (stream.isReading)
		{
			if (stream.PeekNext() != null)
			{
				this.Velocity = (Vector3)stream.ReceiveNext();
			}
			if (stream.PeekNext() != null)
			{
				this.AngularVelocity = (Vector3)stream.ReceiveNext();
			}
		}
	}

	private void Update()
	{
		if (this.rb == null)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	private void CheckForces()
	{
		if (this.Velocity == Vector3.zero && this.AngularVelocity == Vector3.zero && this.carController == null)
		{
			this.SwitchToOldApproach();
		}
	}

	private void SwitchToOldApproach()
	{
		UnityEngine.Object.Destroy(base.GetComponent<Rigidbody>());
		UnityEngine.Object.Destroy(this);
	}

	[HideInInspector]
	public Vector3 Velocity;

	[HideInInspector]
	public Vector3 AngularVelocity;

	private Rigidbody rb;

	private CarController carController;
}
