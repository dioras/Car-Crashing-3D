using System;
using System.Collections;
using CustomVP;
using RootMotion.FinalIK;
using UnityEngine;

public class IKDriverController : MonoBehaviour
{
	private void Awake()
	{
		this.DriverIKComponent = base.GetComponentInChildren<FullBodyBipedIK>(true);
		if (this.DriverIKComponent != null)
		{
			this.Driver = this.DriverIKComponent.gameObject;
		}
		LimbIK componentInChildren = base.GetComponentInChildren<LimbIK>(true);
		if (componentInChildren != null)
		{
			this.Hands = componentInChildren.gameObject;
		}
		if (this.Driver != null)
		{
			this.DefaultDriverParent = this.Driver.transform.parent;
			this.DefaultDriverPos = this.Driver.transform.localPosition;
			this.DefaultDriverRot = this.Driver.transform.localRotation;
			this.PelvisRigidbody = this.Driver.GetComponentInChildren<Rigidbody>();
			this.ragdollUtility = this.Driver.GetComponent<RagdollUtility>();
		}
		this.carController = base.GetComponent<CarController>();
		this.motorcycleAssistant = base.GetComponent<MotorcycleAssistant>();
		this.photonTransformView = base.GetComponent<PhotonTransformView>();
		this.rb = base.GetComponent<Rigidbody>();
		int layer = base.gameObject.layer;
		int num = layer;
		RagdollUtility componentInChildren2 = base.GetComponentInChildren<RagdollUtility>();
		if (componentInChildren2 != null)
		{
			num = componentInChildren2.gameObject.layer;
			this.RagdollColliders = componentInChildren2.GetComponentsInChildren<Collider>(true);
			for (int i = 0; i < this.RagdollColliders.Length; i++)
			{
				this.RagdollColliders[i].enabled = false;
				if (this.carController != null)
				{
					for (int j = 0; j < this.carController.BodyColliders.Length; j++)
					{
						Physics.IgnoreCollision(this.RagdollColliders[i], this.carController.BodyColliders[j], true);
					}
				}
			}
		}
		this.driverCollisionLayermask = ~(1 << layer | 1 << num);
	}

	private void Start()
	{
		this.camController = CameraController.Instance;
	}

	private void FixedUpdate()
	{
		if (this.driverMode == IKDriverController.DriverMode.ATV || this.driverMode == IKDriverController.DriverMode.Bike)
		{
			this.CheckHeadCollision();
		}
	}

	private void Update()
	{
		this.DoDriver();
		this.KnockdownTimeout += Time.deltaTime;
		if (this.motorcycleAssistant != null && (this.frontBumpCollider == null || this.rearBumpCollider == null) && this.motorcycleAssistant.FrontWC != null)
		{
			this.AssignBumpColliders();
		}
	}

	private void AssignBumpColliders()
	{
		this.frontBumpCollider = this.motorcycleAssistant.FrontWC.GetComponentInChildren<Collider>();
		this.rearBumpCollider = this.motorcycleAssistant.RearWC.GetComponentInChildren<Collider>();
	}

	private void CheckHeadCollision()
	{
		int layerMask = -67108865;
		if (Physics.CheckSphere(this.DriverShouldersHolder.position, 0.2f, layerMask))
		{
			this.DoKnockOut(Vector3.zero);
		}
	}

	private void DoDriver()
	{
		if (this.DriverShouldersHolder == null)
		{
			return;
		}
		IKDriverController.DriverMode driverMode = this.driverMode;
		if (driverMode != IKDriverController.DriverMode.Truck)
		{
			if (driverMode != IKDriverController.DriverMode.ATV)
			{
				if (driverMode == IKDriverController.DriverMode.Bike)
				{
					this.DoBike();
				}
			}
			else
			{
				this.DoATV();
			}
		}
		else
		{
			this.DoTruck();
		}
	}

	private void DoATV()
	{
		Vector3 target = -this.carController.acceleration / 50f * this.LeanPower;
		this.WantedShouldersPos = Vector3.SmoothDamp(this.WantedShouldersPos, target, ref this.refVel, 0.2f);
		this.WantedShouldersPos.y = Mathf.Clamp(this.WantedShouldersPos.y, -this.MaxShoulderVerticalOffset, this.MaxShoulderVerticalOffset);
		this.WantedShouldersPos.x = (this.WantedShouldersPos.z = 0f);
		this.DriverShouldersHolder.localPosition = Vector3.MoveTowards(this.DriverShouldersHolder.localPosition, this.WantedShouldersPos, Time.deltaTime * this.LeanSpeed);
		if (this.carController != null)
		{
			if (this.LeftShoulder != null && this.RightShoulder != null)
			{
				this.LeftShoulder.localPosition = new Vector3(-Mathf.LerpUnclamped(this.MaxShoulderLateralOffset, 0f, this.carController.Steering / this.carController.maxSteeringAngle + 1f), 0f, Mathf.Lerp(0f, this.MaxShoulderLongitudinalOffset, Mathf.Clamp(this.carController.Steering, 0f, this.carController.maxSteeringAngle) / this.carController.maxSteeringAngle));
				this.RightShoulder.localPosition = new Vector3(-Mathf.LerpUnclamped(this.MaxShoulderLateralOffset, 0f, this.carController.Steering / this.carController.maxSteeringAngle + 1f), 0f, Mathf.Lerp(0f, this.MaxShoulderLongitudinalOffset, -Mathf.Clamp(this.carController.Steering, -this.carController.maxSteeringAngle, 0f) / this.carController.maxSteeringAngle));
			}
			if (this.DriverBody != null)
			{
				this.DriverBody.localPosition = new Vector3(Mathf.LerpUnclamped(-this.HipsMaxOffset, 0f, this.carController.Steering / this.carController.maxSteeringAngle + 1f), 0f, 0f);
			}
		}
	}

	private void DoTruck()
	{
		Vector3 target = -this.carController.acceleration / 50f * this.LeanPower;
		this.WantedShouldersPos = Vector3.SmoothDamp(this.WantedShouldersPos, target, ref this.refVel, 0.2f);
		this.WantedShouldersPos.x = Mathf.Clamp(this.WantedShouldersPos.x, -this.MaxShoulderLateralOffset, this.MaxShoulderLateralOffset);
		this.WantedShouldersPos.y = Mathf.Clamp(this.WantedShouldersPos.y, -this.MaxShoulderVerticalOffset, this.MaxShoulderVerticalOffset);
		this.WantedShouldersPos.z = Mathf.Clamp(this.WantedShouldersPos.z, -this.MaxShoulderLongitudinalOffset, this.MaxShoulderLongitudinalOffset);
		this.DriverShouldersHolder.localPosition = Vector3.MoveTowards(this.DriverShouldersHolder.localPosition, this.WantedShouldersPos, Time.deltaTime * this.LeanSpeed);
		if (this.carController != null)
		{
			this.DriverLookTarget.localPosition = new Vector3(Mathf.LerpUnclamped(-this.MaxLookOffset, 0f, this.carController.Steering / this.carController.maxSteeringAngle + 1f), 0f, 0f);
		}
	}

	private void DoBike()
	{
		Vector3 target = -this.carController.acceleration / 50f * this.LeanPower;
		this.WantedShouldersPos = Vector3.SmoothDamp(this.WantedShouldersPos, target, ref this.refVel, 0.2f);
		this.WantedShouldersPos.y = Mathf.Clamp(this.WantedShouldersPos.y, -this.MaxShoulderVerticalOffset, this.MaxShoulderVerticalOffset);
		this.WantedShouldersPos.x = (this.WantedShouldersPos.z = 0f);
		this.DriverShouldersHolder.localPosition = Vector3.MoveTowards(this.DriverShouldersHolder.localPosition, this.WantedShouldersPos, Time.deltaTime * this.LeanSpeed);
		if (this.motorcycleAssistant == null)
		{
			return;
		}
		float f = 0f;
		if (this.rb != null)
		{
			f = base.transform.InverseTransformDirection(this.rb.velocity).z * 3.6f;
		}
		if (Mathf.Abs(f) < 2f && this.motorcycleAssistant.fullyGrounded)
		{
			this.RightLegTargetPos = this.RightLeg_StandTarget.position;
			this.RightLegTargetRot = this.RightLeg_StandTarget.rotation;
		}
		else
		{
			this.RightLegTargetPos = Vector3.Lerp(this.RightLeg_RideTarget.position, this.RightLeg_TurnTarget.position, -this.motorcycleAssistant.lean / this.motorcycleAssistant.MaxLean);
			this.RightLegTargetRot = Quaternion.Lerp(this.RightLeg_RideTarget.rotation, this.RightLeg_TurnTarget.rotation, -this.motorcycleAssistant.lean / this.motorcycleAssistant.MaxLean);
		}
		this.LeftLegTargetPos = Vector3.Lerp(this.LeftLeg_RideTarget.position, this.LeftLeg_TurnTarget.position, this.motorcycleAssistant.lean / this.motorcycleAssistant.MaxLean);
		this.LeftLegTargetRot = Quaternion.Lerp(this.LeftLeg_RideTarget.rotation, this.LeftLeg_TurnTarget.rotation, this.motorcycleAssistant.lean / this.motorcycleAssistant.MaxLean);
		this.RightLegEffector.position = Vector3.MoveTowards(this.RightLegEffector.position, this.RightLegTargetPos, Time.deltaTime * 2f);
		this.RightLegEffector.rotation = Quaternion.RotateTowards(this.RightLegEffector.rotation, this.RightLegTargetRot, Time.deltaTime * 100f);
		this.LeftLegEffector.position = Vector3.MoveTowards(this.LeftLegEffector.position, this.LeftLegTargetPos, Time.deltaTime * 2f);
		this.LeftLegEffector.rotation = Quaternion.RotateTowards(this.LeftLegEffector.rotation, this.LeftLegTargetRot, Time.deltaTime * 100f);
	}

	private void OnCollisionEnter(Collision col)
	{
		if (this.carController == null)
		{
			return;
		}
		if (this.driverMode == IKDriverController.DriverMode.Truck)
		{
			return;
		}
		if (col.collider.transform.root.gameObject.GetPhotonView() != null)
		{
			return;
		}
		if (col.collider.GetComponentInParent<DroneController>() != null)
		{
			return;
		}
		if (col.impulse.magnitude > this.KnockOutForce)
		{
			if (Vector3.Angle(-base.transform.forward, col.impulse) < 40f)
			{
				this.DoKnockOut(-col.relativeVelocity / 5f + Vector3.up * col.relativeVelocity.magnitude / 5f);
			}
			if (Vector3.Angle(-base.transform.up, col.impulse) < 30f)
			{
				this.DoKnockOut(Vector3.zero);
			}
		}
		if (this.motorcycleAssistant != null)
		{
			foreach (ContactPoint contactPoint in col.contacts)
			{
				if (contactPoint.thisCollider.Equals(this.frontBumpCollider) || contactPoint.thisCollider.Equals(this.rearBumpCollider))
				{
					if (Vector3.Angle(base.transform.up, contactPoint.normal) > 60f && Vector3.Angle(base.transform.forward, contactPoint.normal) > 45f)
					{
						this.DoKnockOut(Vector3.zero);
					}
					break;
				}
			}
		}
		this.TouchingGround = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		this.TouchingGround = false;
	}

	public void DoKnockOut(Vector3 force)
	{
		base.StartCoroutine(this.TurnToRagdoll(force));
	}

	public IEnumerator TurnToRagdoll(Vector3 force)
	{
		if (!this.DriverIKComponent.enabled || !this.DriverIKComponent.gameObject.activeSelf || this.KnockdownTimeout < 5f)
		{
			yield break;
		}
		for (int i = 0; i < this.RagdollColliders.Length; i++)
		{
			this.RagdollColliders[i].enabled = true;
		}
		if (GameState.GameMode == GameMode.Multiplayer)
		{
			this.photonTransformView.RiderKnockOut(force);
		}
		this.KnockdownTimeout = 0f;
		bool DriverWasDisabled = !this.Driver.activeSelf;
		if (DriverWasDisabled)
		{
			this.ToggleDriver(true, false);
		}
		this.KnockedOut = true;
		this.DriverIKComponent.enabled = false;
		this.Driver.transform.parent = null;
		this.ragdollUtility.EnableRagdoll();
		if (this.carController != null)
		{
			this.carController.vehicleIsActive = false;
		}
		yield return new WaitForSeconds(0.01f);
		this.PelvisRigidbody.AddForce(force * 3000f);
		if (this.carController != null && this.camController.cameraMode != CameraController.CameraMode.Cinematic)
		{
			this.camController.SetRagdollCamera();
			this.camController.Ragdoll = this.PelvisRigidbody.transform;
		}
		yield return new WaitForSeconds(3f);
		this.GetDriverBack();
		if (this.carController != null)
		{
			this.carController.FlipCar();
			if (this.camController.cameraMode != CameraController.CameraMode.Cinematic)
			{
				this.camController.GetCameraBack();
			}
			this.carController.vehicleIsActive = true;
		}
		if (DriverWasDisabled)
		{
			this.ToggleDriver(false, true);
		}
		this.KnockedOut = false;
		for (int j = 0; j < this.RagdollColliders.Length; j++)
		{
			this.RagdollColliders[j].enabled = false;
		}
		yield break;
	}

	private void GetDriverBack()
	{
		this.DriverIKComponent.enabled = true;
		if (this.DefaultDriverParent != null)
		{
			this.Driver.transform.parent = this.DefaultDriverParent;
		}
		this.ragdollUtility.DisableRagdoll();
		this.Driver.transform.localPosition = this.DefaultDriverPos;
		this.Driver.transform.localRotation = this.DefaultDriverRot;
	}

	public void ToggleDriver(bool ShowDriver, bool ShowHands)
	{
		if (this.Driver == null || this.Hands == null)
		{
			return;
		}
		this.Driver.SetActive(ShowDriver);
		this.Hands.SetActive(ShowHands);
	}

	public IKDriverController.DriverMode driverMode;

	public float LeanPower = 1f;

	public float LeanSpeed = 1f;

	public float MaxShoulderLongitudinalOffset;

	public float MaxShoulderLateralOffset;

	public float MaxShoulderVerticalOffset;

	public float KnockOutForce = 3000f;

	public Transform DriverShouldersHolder;

	public Transform DriverBody;

	public Transform LeftShoulder;

	public Transform RightShoulder;

	public Transform DriverLookTarget;

	public float HipsMaxOffset = 0.3f;

	public float MaxLookOffset;

	private Transform DefaultDriverParent;

	private Vector3 DefaultDriverPos;

	private Quaternion DefaultDriverRot;

	private Vector3 WantedShouldersPos;

	private Vector3 refVel = Vector3.one;

	private Rigidbody PelvisRigidbody;

	private RagdollUtility ragdollUtility;

	private FullBodyBipedIK DriverIKComponent;

	private CarController carController;

	private CameraController camController;

	private MotorcycleAssistant motorcycleAssistant;

	private Rigidbody rb;

	private PhotonTransformView photonTransformView;

	private GameObject Driver;

	private GameObject Hands;

	[HideInInspector]
	public bool KnockedOut;

	private bool TouchingGround;

	private LayerMask driverCollisionLayermask;

	public Transform RightLegEffector;

	public Transform RightLeg_StandTarget;

	public Transform RightLeg_RideTarget;

	public Transform RightLeg_TurnTarget;

	public Transform LeftLegEffector;

	public Transform LeftLeg_RideTarget;

	public Transform LeftLeg_TurnTarget;

	private Vector3 RightLegTargetPos;

	private Vector3 LeftLegTargetPos;

	private Quaternion RightLegTargetRot;

	private Quaternion LeftLegTargetRot;

	private Collider frontBumpCollider;

	private Collider rearBumpCollider;

	private float KnockdownTimeout;

	private Collider[] RagdollColliders;

	public enum DriverMode
	{
		Truck,
		ATV,
		Bike
	}
}
