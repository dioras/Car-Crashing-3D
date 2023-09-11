using System;
using CustomVP;
using Photon;
using UnityEngine;

public class NetworkMovement : Photon.MonoBehaviour
{
	private void Start()
	{
		this.target = base.transform;
		this.carController = base.GetComponent<CarController>();
		this.suspensionController = base.GetComponent<SuspensionController>();
		this.partsSwitcher = base.GetComponent<BodyPartsSwitcher>();
		if (!base.photonView.isMine)
		{
			if (this.suspensionController != null)
			{
				this.suspensionController.TurnToMultiplayerCar();
			}
			if (this.carController != null)
			{
				this.carController.enabled = false;
			}
		}
	}

	private void FixedUpdate()
	{
		if (base.photonView.isMine)
		{
			this.sendInfo();
		}
		else
		{
			this.recontiliation();
		}
	}

	protected void sendInfo()
	{
		if (this.send)
		{
			if (this.count == this.SendRate)
			{
				this.count = 0;
				this.send = false;
				Vector3 position = this.target.position;
				Quaternion rotation = this.target.rotation;
				float dirtiness = 0f;
				float wetness = 0f;
				float steeringAngle = 0f;
				if (this.partsSwitcher != null)
				{
					dirtiness = this.partsSwitcher.Dirtiness;
					wetness = this.partsSwitcher.MudWetness;
				}
				if (this.carController != null)
				{
					steeringAngle = this.carController.Steering;
				}
				this.CmdSendPosition(position, rotation, steeringAngle, dirtiness, wetness);
			}
			else
			{
				this.count++;
			}
		}
		else
		{
			this.checkIfSend();
		}
	}

	protected void checkIfSend()
	{
		if (this.sending)
		{
			this.send = true;
			this.sending = false;
			return;
		}
		Vector3 position = this.target.position;
		Quaternion rotation = this.target.rotation;
		float num = Vector3.Distance(this.lastPositionSent, position);
		float num2 = Quaternion.Angle(this.lastRotationSent, rotation);
		if (this.carController != null)
		{
			float steering = this.carController.Steering;
		}
		this.send = true;
		this.sending = true;
	}

	protected void recontiliation()
	{
		Vector3 vector = this.target.position;
		Quaternion quaternion = this.target.rotation;
		float num = Vector3.Distance(this.lastPositionSent, vector);
		float num2 = Vector3.Angle(this.lastRotationSent.eulerAngles, quaternion.eulerAngles);
		if (num > this.distanceBeforeSnap)
		{
			this.target.position = this.lastPositionSent;
		}
		if (num2 > this.angleBeforeSnap)
		{
			this.target.rotation = this.lastRotationSent;
		}
		vector += this.lastDirectionPerFrame;
		quaternion *= this.lastRotationDirectionPerFrame;
		Vector3 position = Vector3.Lerp(vector, this.lastPositionSent, this.movementInterpolation);
		Quaternion rotation = Quaternion.Lerp(quaternion, this.lastRotationSent, this.rotationInterpolation);
		this.target.position = position;
		this.target.rotation = rotation;
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (this.target == null)
		{
			return;
		}
		if (stream.isWriting)
		{
			Vector3 position = this.target.position;
			Quaternion rotation = this.target.rotation;
			stream.Serialize(ref position);
			stream.Serialize(ref rotation);
		}
		else
		{
			Vector3 zero = Vector3.zero;
			Quaternion identity = Quaternion.identity;
			stream.Serialize(ref zero);
			stream.Serialize(ref identity);
			this.target.position = zero;
			this.target.rotation = identity;
		}
	}

	protected void CmdSendPosition(Vector3 newPos, Quaternion newRot, float steeringAngle, float dirtiness, float wetness)
	{
		this.RpcReceivePosition(newPos, newRot, steeringAngle, dirtiness, wetness);
	}

	protected void RpcReceivePosition(Vector3 newPos, Quaternion newRot, float steeringAngle, float dirtiness, float wetness)
	{
		int num = this.SendRate + 1;
		this.lastDirectionPerFrame = newPos - this.lastPositionSent;
		this.lastDirectionPerFrame /= (float)num;
		if (this.lastDirectionPerFrame.magnitude > this.thresholdMovementPrediction)
		{
			this.lastDirectionPerFrame = Vector3.zero;
		}
		Vector3 eulerAngles = this.lastRotationSent.eulerAngles;
		Vector3 eulerAngles2 = newRot.eulerAngles;
		if (Quaternion.Angle(this.lastRotationDirectionPerFrame, newRot) < this.thresholdRotationPrediction)
		{
			this.lastRotationDirectionPerFrame = Quaternion.Euler((eulerAngles2 - eulerAngles) / (float)num);
		}
		else
		{
			this.lastRotationDirectionPerFrame = Quaternion.identity;
		}
		this.lastPositionSent = newPos;
		this.lastRotationSent = newRot;
		this.lastSteeringAngle = steeringAngle;
		if (this.partsSwitcher != null)
		{
			this.partsSwitcher.Dirtiness = dirtiness;
			this.partsSwitcher.MudWetness = wetness;
			this.partsSwitcher.UpdateDirtiness();
		}
	}

	[SerializeField]
	protected Transform target;

	[Header("Setup")]
	[Range(0f, 10f)]
	public int SendRate = 2;

	[Range(0f, 2f)]
	public float movementThreshold = 0.2f;

	[Range(0f, 30f)]
	public float angleThreshold = 5f;

	[Range(0f, 10f)]
	public float distanceBeforeSnap = 4f;

	[Range(0f, 90f)]
	public float angleBeforeSnap = 40f;

	[Header("Interpolation")]
	[Range(0f, 1f)]
	public float movementInterpolation = 0.1f;

	[Range(0f, 1f)]
	public float rotationInterpolation = 0.1f;

	public float thresholdMovementPrediction = 0.7f;

	public float thresholdRotationPrediction = 15f;

	protected Vector3 lastDirectionPerFrame = Vector3.zero;

	protected Vector3 lastPositionSent = Vector3.zero;

	protected Quaternion lastRotationSent = Quaternion.identity;

	protected Quaternion lastRotationDirectionPerFrame = Quaternion.identity;

	protected float lastSteeringAngle;

	protected bool send;

	protected bool sending;

	protected int count;

	private CarController carController;

	private SuspensionController suspensionController;

	private BodyPartsSwitcher partsSwitcher;
}
