using System;
using CustomVP;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TrailerController : MonoBehaviour
{
	private CarController carController
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerCarController;
			}
			return null;
		}
	}

	private BodyPartsSwitcher partsSwitcher
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerPartsSwitcher;
			}
			return null;
		}
	}

	private VehicleDataManager dataManager
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerDataManager;
			}
			return null;
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

	private CarUIControl ui
	{
		get
		{
			if (this._ui == null)
			{
				this._ui = CarUIControl.Instance;
			}
			return this._ui;
		}
	}

	public Rigidbody rb
	{
		get
		{
			if (this._rb == null)
			{
				this._rb = base.GetComponent<Rigidbody>();
			}
			return this._rb;
		}
	}

	private void Start()
	{
		if (this.rb != null)
		{
			this.rb.drag = 0.4f;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(base.transform.TransformPoint(this.connectPoint), 0.05f);
		Gizmos.DrawSphere(base.transform.TransformPoint(this.center), 0.05f);
	}

	public void ConnectToCar()
	{
		if (this.connected)
		{
			this.Detach();
		}
		this.rb.isKinematic = true;
		this.AlignByVehicle();
		if (this.loadedVehicle != null)
		{
			this.loadedVehicle.GetComponent<Rigidbody>().isKinematic = true;
			this.loadedVehicle.AlignOnTrailer(this);
		}
		this.joint = base.gameObject.AddComponent<ConfigurableJoint>();
		this.joint.connectedBody = this.carController.GetComponent<Rigidbody>();
		this.joint.xMotion = ConfigurableJointMotion.Locked;
		this.joint.yMotion = ConfigurableJointMotion.Locked;
		this.joint.zMotion = ConfigurableJointMotion.Locked;
		this.joint.angularXMotion = ConfigurableJointMotion.Limited;
		this.joint.angularYMotion = ConfigurableJointMotion.Limited;
		this.joint.angularZMotion = ConfigurableJointMotion.Limited;
		this.joint.anchor = this.connectPoint;
		this.joint.autoConfigureConnectedAnchor = false;
		this.joint.connectedAnchor = this.carController.transform.InverseTransformPoint(this.partsSwitcher.TrailerMountPos(this.gooseneck).position);
		SoftJointLimit lowAngularXLimit = this.joint.lowAngularXLimit;
		lowAngularXLimit.limit = -30f;
		this.joint.lowAngularXLimit = lowAngularXLimit;
		SoftJointLimit highAngularXLimit = this.joint.highAngularXLimit;
		highAngularXLimit.limit = 30f;
		this.joint.highAngularXLimit = highAngularXLimit;
		SoftJointLimit angularYLimit = this.joint.angularYLimit;
		angularYLimit.limit = 60f;
		this.joint.angularYLimit = angularYLimit;
		SoftJointLimit angularZLimit = this.joint.angularZLimit;
		angularZLimit.limit = 30f;
		this.joint.angularZLimit = angularZLimit;
		this.supportCollider.gameObject.SetActive(false);
		this.connected = true;
		this.rb.velocity = Vector3.zero;
		this.rb.angularVelocity = Vector3.zero;
		this.rb.isKinematic = false;
		if (this.loadedVehicle != null)
		{
			this.loadedVehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
			this.loadedVehicle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			this.loadedVehicle.GetComponent<Rigidbody>().isKinematic = false;
		}
		if (this.ui != null)
		{
			this.ui.detachTrailerButton.SetActive(true);
		}
		if (PhotonNetwork.inRoom)
		{
			this.photonTransformView.ChangeTrailerMpConnectedState(this.connected);
		}
	}

	public void AlignByVehicle()
	{
		base.transform.position = this.partsSwitcher.TrailerMountPos(this.gooseneck).position - this.partsSwitcher.transform.TransformVector(this.connectPoint * base.transform.localScale.x);
		base.transform.rotation = this.carController.transform.rotation;
	}

	public void VehicleLoadedOnMe(GameObject vehicle)
	{
		this.loadedVehicle = vehicle.GetComponent<VehicleDataManager>();
		this.loadedVehicleWheelColliders = this.loadedVehicle.GetComponentsInChildren<WheelComponent>();
	}

	public void Detach()
	{
		if (this.joint != null)
		{
			UnityEngine.Object.DestroyImmediate(this.joint);
		}
		this.connected = false;
		this.supportCollider.gameObject.SetActive(true);
		if (this.ui != null)
		{
			this.ui.detachTrailerButton.SetActive(false);
		}
		if (PhotonNetwork.inRoom)
		{
			this.photonTransformView.ChangeTrailerMpConnectedState(this.connected);
		}
	}

	public void Attach()
	{
		if (this.carController == null)
		{
			return;
		}
		this.ConnectToCar();
	}

	private void Update()
	{
		if (this.multiplayerTrailer)
		{
			if (this.playerView == null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		for (int i = 0; i < this.straps.Length; i++)
		{
			if (this.loadedVehicle != null && this.loadedVehicleWheelColliders != null && this.loadedVehicleWheelColliders.Length > 0)
			{
				this.straps[i].SetPositions(new Vector3[]
				{
					this.strapsMounts[i].position,
					this.loadedVehicleWheelColliders[i].GetVisualWheelPosition()
				});
			}
			this.straps[i].gameObject.SetActive(this.loadedVehicle != null);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2) || CrossPlatformInputManager.GetButtonUp("DetachTrailer"))
		{
			this.Detach();
		}
		if (CrossPlatformInputManager.GetButtonUp("AttachTrailer"))
		{
			this.Attach();
		}
		if (this.joint == null && this.connected)
		{
			this.Detach();
		}
		for (int j = 0; j < this.wheels.Length; j++)
		{
			this.wheels[j].transform.position = this.wc[j].GetVisualWheelPosition();
			float num = (float)((j % 2 != 0) ? 1 : -1);
			this.wheels[j].Rotate(this.wheels[j].right, this.wc[j].perFrameRotation * num, Space.World);
			if (this.connected)
			{
				this.wc[j].currentBrakeTorque = this.carController.currentBrakeTorque;
			}
			else
			{
				this.wc[j].currentBrakeTorque = 1000f;
			}
		}
		if (this.ui != null && this.partsSwitcher != null)
		{
			Vector3 a = Vector3.zero;
			if (this.partsSwitcher.TrailerMountPos(this.gooseneck) != null)
			{
				a = this.partsSwitcher.TrailerMountPos(this.gooseneck).position;
			}
			a.y = 0f;
			Vector3 b = base.transform.TransformPoint(this.connectPoint);
			b.y = 0f;
			bool flag = true;
			if (this.gooseneck && this.partsSwitcher.gooseneckMount == null)
			{
				flag = false;
			}
			this.ui.attachTrailerButton.SetActive(flag && Vector3.Distance(a, b) < 1f && !this.connected && this.dataManager.vehicleType != VehicleType.ATV && base.transform.up.y > 0f && !WinchManager.Instance.BeingWinchTarget && !WinchManager.Instance.WinchMode && !this.carController.loadedOnOtherPlayerTrailer);
			this.ui.swapVehiclesButton.gameObject.SetActive(Vector3.Distance(base.transform.position, this.partsSwitcher.transform.position) < 10f && !this.connected && this.loadedVehicle != null && base.transform.up.y > 0f && !WinchManager.Instance.BeingWinchTarget && !WinchManager.Instance.WinchMode && !this.carController.loadedOnOtherPlayerTrailer);
		}
	}

	private CarUIControl _ui;

	public TrailerWheelCollider[] wc;

	public Transform[] wheels;

	public LineRenderer[] straps;

	public Transform[] strapsMounts;

	public Collider supportCollider;

	public Vector3 connectPoint;

	public Vector3 center;

	public bool gooseneck;

	private Rigidbody _rb;

	[HideInInspector]
	public bool connected;

	private ConfigurableJoint joint;

	[HideInInspector]
	public bool multiplayerTrailer;

	[HideInInspector]
	public PhotonView playerView;

	public bool mpConnected;

	public GameObject mpCarOnMe;

	private VehicleDataManager loadedVehicle;

	private WheelComponent[] loadedVehicleWheelColliders;
}
