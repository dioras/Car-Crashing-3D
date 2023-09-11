using System;
using System.Collections.Generic;
using CustomVP;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WinchManager : MonoBehaviour
{
	public WinchManager()
	{
		if (WinchManager.Instance == null)
		{
			WinchManager.Instance = this;
		}
	}

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

	private Rigidbody playerRigidbody
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerRigidbody;
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

	private void Awake()
	{
		WinchManager.Instance = this;
	}

	private void Start()
	{
		this.CreateWinchTargets();
		this.OtherPlayersCables = new List<WinchCable>();
		this.lineRenderer = this.CreateLineRenderer("Player cable line renderer");
	}

	private void Update()
	{
		if (this.carController != null)
		{
			this.DoWinch();
			this.carController.DontPreventFromSliding = (this.WinchMode || this.BeingWinchTarget);
			if (this.OtherPlayersCables != null)
			{
				foreach (WinchCable winchCable in this.OtherPlayersCables)
				{
					if (winchCable.IsCarMissing())
					{
						this.OnCableDestroyed(winchCable.CableID);
					}
					else
					{
						winchCable.UpdateCable();
					}
				}
			}
			return;
		}
	}

	private LineRenderer CreateLineRenderer(string name)
	{
		GameObject gameObject = new GameObject(name);
		LineRenderer lineRenderer = new LineRenderer();
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = (Resources.Load("Materials/WinchRope", typeof(Material)) as Material);
		lineRenderer.useWorldSpace = true;
		lineRenderer.positionCount = 2;
		lineRenderer.textureMode = LineTextureMode.Tile;
		lineRenderer.widthMultiplier = 0.1f;
		return lineRenderer;
	}

	private void TurnToLandAnchor()
	{
		CarUIControl.Instance.SwitchWinchTargetSelector(false);
		CarUIControl.Instance.ShowNotification("Tap on ground", true);
		this.LandAnchorMode = true;
		CameraController.Instance.cameraMode = CameraController.CameraMode.Free;
		this.ShowHideWinchTargets(false);
	}

	private void CheckLandAnchorTap(Vector3 pos)
	{
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit) && Vector3.Distance(this.carController.transform.position, raycastHit.point) < this.WinchRadius && raycastHit.collider.GetType() == typeof(TerrainCollider))
		{
			this.LandAnchor = (UnityEngine.Object.Instantiate(Resources.Load("Other/LandAnchor")) as GameObject);
			this.LandAnchor.transform.position = raycastHit.point + Vector3.up * 0.25f;
			this.LandAnchor.transform.LookAt(this.carController.transform, Vector3.up);
			this.AttachWinch(this.LandAnchor.transform);
		}
	}

	private void DoWinch()
	{
		if (CrossPlatformInputManager.GetButtonDown("ToggleWinch"))
		{
			this.ToggleWinch();
		}
		if (CrossPlatformInputManager.GetButtonDown("LeftArrow"))
		{
			this.SwitchToLeftTarget();
		}
		if (CrossPlatformInputManager.GetButtonDown("RightArrow"))
		{
			this.SwitchToRightTarget();
		}
		if (CrossPlatformInputManager.GetButtonDown("Attach"))
		{
			this.AttachWinch(null);
		}
		if (CrossPlatformInputManager.GetButtonDown("SendWinchRequest"))
		{
			this.SendWinchRequest();
		}
		if (CrossPlatformInputManager.GetButtonDown("AcceptWinchRequest"))
		{
			this.AcceptWinchRequest();
		}
		if (CrossPlatformInputManager.GetButtonDown("DeclineWinchRequest"))
		{
			this.DeclineWinchRequest();
		}
		if (CrossPlatformInputManager.GetButtonDown("Detach"))
		{
			this.DetachAttachedCar();
		}
		if (CrossPlatformInputManager.GetButtonUp("LandAnchor"))
		{
			this.TurnToLandAnchor();
		}
		if (this.WinchAttached)
		{
			this.WinchTowing = CrossPlatformInputManager.GetButton("TowWinch");
		}
		if (this.LandAnchorMode && UnityEngine.Input.touchCount == 1)
		{
			if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				this.TouchMoved = true;
			}
			if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended && !this.TouchMoved)
			{
				this.CheckLandAnchorTap(UnityEngine.Input.GetTouch(0).position);
			}
			if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				this.TouchMoved = false;
			}
			if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
			{
				this.TouchMoved = false;
			}
		}
		if (this.WinchMode && !this.WinchAttached)
		{
			this.carController.ExtremeBraking = 1f;
		}
		else
		{
			this.carController.ExtremeBraking = 0f;
		}
		if (this.WinchAttached)
		{
			if (this.CurrentWinchTarget == null)
			{
				this.ToggleWinch();
			}
			this.ApplyTug(this.CurrentWinchTarget.position, this.partsSwitcher.FrontWinchPoint.position, false);
			if (Time.frameCount % 2 == 0)
			{
				this.UpdateMyWinchCable();
			}
			if (this.WinchTowing)
			{
				this.ApplyTug(this.CurrentWinchTarget.position, this.partsSwitcher.FrontWinchPoint.position, true);
			}
		}
		if (this.BeingWinchTarget)
		{
			if (this.CarAttachedToUs == null)
			{
				this.StopBeingWinchTarget();
			}
			this.ApplyTug(this.WinchOfCarAttachedToUs.position, this.MyCurrentWinchPoint.position, false);
		}
	}

	private void ApplyTug(Vector3 targetPos, Vector3 winchPos, bool ManualTowing = false)
	{
		float num = Vector3.Distance(winchPos, targetPos) - this.MinCableLength;
		if (this.myCableType == CableType.CarToCar && num <= 0f && !ManualTowing)
		{
			return;
		}
		if (Vector3.Distance(winchPos, targetPos) < 2f)
		{
			return;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		float t = Mathf.InverseLerp(10f, 0f, Mathf.Abs(this.carController.Speed));
		Vector3 normalized = (targetPos - winchPos).normalized;
		Vector3 normalized2 = Vector3.ProjectOnPlane(normalized, this.carController.transform.up).normalized;
		Vector3 vector = Vector3.Lerp(normalized2, normalized, t);
		Vector3 vector2 = Vector3.zero;
		Vector3 b = this.ropeDamping * -Vector3.Project(this.playerRigidbody.velocity, vector);
		if (ManualTowing && num == 0f)
		{
			b = Vector3.zero;
		}
		float num2 = (float)((!ManualTowing) ? 0 : 1);
		CableType cableType = this.myCableType;
		if (cableType != CableType.CarToCar)
		{
			if (cableType == CableType.CarToStatic)
			{
				float d = Mathf.InverseLerp(10f, 0f, this.carController.Speed);
				vector2 = vector * this.ropeForce * (Mathf.Clamp01(this.carController.Throttle) + num + num2) * 2f * Mathf.Max(1f, num2 * 3f);
				if (num == 0f)
				{
					vector2 *= d;
				}
				else
				{
					vector2 += b;
				}
			}
		}
		else
		{
			Vector3 vector3 = Vector3.zero;
			if (this.carController.tankController == null)
			{
				for (int i = 0; i < this.carController.wheels.Count; i++)
				{
					vector3 += this.carController.wheels[i].wc.wheelCollider.LongForce;
				}
			}
			else
			{
				for (int j = 0; j < this.carController.tankController.allWheelColliders.Length; j++)
				{
					vector3 += this.carController.tankController.allWheelColliders[j].LongForce;
				}
			}
			vector2 = vector * num * this.ropeForce + vector3 + b + num2 * vector * this.ropeForce * 5f;
		}
		UnityEngine.Debug.DrawRay(winchPos, vector2, Color.magenta);
		this.playerRigidbody.AddForceAtPosition(vector2 * Time.timeScale, winchPos, ForceMode.Force);
	}

	private void SendWinchRequest()
	{
		if (GameState.GameType == GameType.TrailRace)
		{
			return;
		}
		this.CarWeWantToAttachTo = this.AvailableWinchTargets[this.SelectedTargetIndex].transform.root.GetComponent<PhotonView>();
		this.photonTransformView.SendWinchRequest(this.CarWeWantToAttachTo);
		this.WaitingForResponse = true;
		CarUIControl.Instance.ToggleAttachButton(false, false);
		CarUIControl.Instance.ShowNotification("Waiting for response...", true);
	}

	public void OnWinchRequestAccepted(PhotonView AcceptingCar)
	{
		if (!this.WaitingForResponse)
		{
			return;
		}
		if (this.CarWeWantToAttachTo != AcceptingCar)
		{
			return;
		}
		this.AttachWinch(null);
	}

	public void OnWinchRequestDeclined()
	{
		CarUIControl.Instance.ShowNotification("Other player declined winch request", false);
		if (this.WinchMode)
		{
			this.ToggleWinch();
		}
	}

	public void GetWinchRequest(string RequestingCarID)
	{
		this.CarThatSentWinchRequest = RequestingCarID;
	}

	private void AcceptWinchRequest()
	{
		if (this.WinchMode)
		{
			this.ToggleWinch();
		}
		foreach (PhotonView photonView in PhotonNetwork.networkingPeer.photonViewList.Values)
		{
			if (photonView.ownerId == int.Parse(this.CarThatSentWinchRequest))
			{
				this.photonTransformView.SendWinchAcceptation(photonView);
				break;
			}
		}
	}

	public void OnOtherCarAttachedToUs(PhotonView AttachingCar)
	{
		this.CarAttachedToUs = AttachingCar;
		this.WinchOfCarAttachedToUs = this.GetClosestTransform(this.CarAttachedToUs.GetComponent<BodyPartsSwitcher>().FrontWinchPoint, this.CarAttachedToUs.GetComponent<BodyPartsSwitcher>().RearWinchPoint, base.transform.position);
		this.MinCableLength = Vector3.Distance(this.partsSwitcher.FrontWinchPoint.position, this.WinchOfCarAttachedToUs.position);
		this.BeingWinchTarget = true;
		CarUIControl.Instance.SwitchDetachButton(true);
		this.myCableType = CableType.CarToCar;
		this.MyCurrentWinchPoint = this.GetClosestTransform(this.partsSwitcher.FrontWinchPoint, this.partsSwitcher.RearWinchPoint, this.CarAttachedToUs.transform.position);
	}

	public void OnOtherCarDetachedFromUs()
	{
		this.StopBeingWinchTarget();
	}

	public void DeclineWinchRequest()
	{
		foreach (PhotonView photonView in PhotonNetwork.networkingPeer.photonViewList.Values)
		{
			if (photonView.ownerId == int.Parse(this.CarThatSentWinchRequest))
			{
				this.photonTransformView.SendWinchDeclination(photonView);
				break;
			}
		}
		this.StopBeingWinchTarget();
	}

	private void DetachAttachedCar()
	{
		this.photonTransformView.SendWinchDeclination(this.CarAttachedToUs);
		this.StopBeingWinchTarget();
	}

	private void UpdateMyWinchCable()
	{
		this.lineRenderer.enabled = true;
		this.lineRenderer.SetPosition(0, this.partsSwitcher.FrontWinchPoint.position);
		this.lineRenderer.SetPosition(1, this.CurrentWinchTarget.position);
	}

	public void OnDynamicCableCreated(string CableID, PhotonView car1, PhotonView car2)
	{
		WinchCable winchCable = new WinchCable();
		winchCable.CableID = CableID;
		winchCable.t1 = car1.GetComponent<BodyPartsSwitcher>().FrontWinchPoint.transform;
		BodyPartsSwitcher component = car2.GetComponent<BodyPartsSwitcher>();
		winchCable.t2 = this.GetClosestTransform(component.FrontWinchPoint, component.RearWinchPoint, car1.transform.position);
		winchCable.cableType = CableType.CarToCar;
		winchCable.lineRenderer = this.CreateLineRenderer("Cable:" + CableID);
		this.OtherPlayersCables.Add(winchCable);
	}

	public void OnStaticCableCreated(string CableID, PhotonView car, Vector3 Target)
	{
		WinchCable winchCable = new WinchCable();
		winchCable.CableID = CableID;
		winchCable.Car = car.GetComponent<BodyPartsSwitcher>().Winch.transform;
		winchCable.CarTargetPos = Target;
		winchCable.cableType = CableType.CarToStatic;
		winchCable.lineRenderer = this.CreateLineRenderer("Cable:" + CableID);
		this.OtherPlayersCables.Add(winchCable);
	}

	public void OnCableDestroyed(string CableID)
	{
		if (this.OtherPlayersCables.Count == 0)
		{
			return;
		}
		WinchCable winchCable = this.OtherPlayersCables.Find((WinchCable cable) => cable.CableID == CableID);
		if (winchCable == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(winchCable.lineRenderer.gameObject);
		this.OtherPlayersCables.Remove(winchCable);
	}

	public void StopBeingWinchTarget()
	{
		this.BeingWinchTarget = false;
		CarUIControl.Instance.SwitchDetachButton(false);
	}

	public void ToggleWinch()
	{
		if (this.BeingWinchTarget)
		{
			return;
		}
		if (this.carController.loadedOnOtherPlayerTrailer)
		{
			return;
		}
		this.WinchMode = !this.WinchMode;
		CarUIControl.Instance.ToggleCarControls(!this.WinchMode);
		CarUIControl.Instance.ToggleCarExtras(!this.WinchMode);
		this.ShowHideWinchTargets(this.WinchMode);
		this.ShowHideWinchZoneProjector(this.WinchMode);
		CarUIControl.Instance.SwitchWinchTargetSelector(this.WinchMode);
		if (this.WinchMode)
		{
			CameraController.Instance.SetWinchCamera();
			this.AvailableWinchTargets = new List<WinchTarget>();
			foreach (WinchTarget winchTarget in this.WinchTargets)
			{
				if (winchTarget != null && winchTarget.gameObject != null && winchTarget.gameObject.activeSelf && Vector3.Distance(this.carController.transform.position, winchTarget.transform.position) < this.WinchRadius)
				{
					this.AvailableWinchTargets.Add(winchTarget);
				}
			}
			if (this.AvailableWinchTargets.Count > 0)
			{
				CameraController.Instance.SelectedWinchTarget = this.AvailableWinchTargets[0].transform;
				WinchTarget winchTarget2 = this.AvailableWinchTargets[0];
				CarUIControl.Instance.ToggleAttachButton(winchTarget2.DynamicTarget, true);
			}
			CarUIControl.Instance.ShowNotification((this.AvailableWinchTargets.Count <= 0) ? "No winch targets available" : "Choose winch target", true);
			this.SelectedTargetIndex = 0;
		}
		else
		{
			this.WinchAttached = false;
			this.lineRenderer.enabled = false;
			this.LandAnchorMode = false;
			CarUIControl.Instance.ToggleAttachButton(false, false);
			CarUIControl.Instance.HideNotification();
			CarUIControl.Instance.SwitchWinchTowButton(false);
			CameraController.Instance.GetCameraBack();
			this.WaitingForResponse = false;
			CameraController.Instance.SelectedWinchTarget = null;
			this.WinchTowing = false;
			if (this.LandAnchor != null)
			{
				UnityEngine.Object.Destroy(this.LandAnchor);
			}
			if (GameState.GameMode == GameMode.Multiplayer)
			{
				this.photonTransformView.SendCableDestroyingEvent(this.MyCableID);
			}
			if (this.CarWeWantToAttachTo != null)
			{
				this.photonTransformView.SendWinchDetachEvent(this.CarWeWantToAttachTo);
				this.CarWeWantToAttachTo = null;
			}
		}
	}

	public void AttachWinch(Transform landAnchor = null)
	{
		if (!this.WinchMode)
		{
			return;
		}
		Transform transform = null;
		Transform frontWinchPoint = this.partsSwitcher.FrontWinchPoint;
		this.myCableType = CableType.CarToStatic;
		if (this.AvailableWinchTargets.Count > 0)
		{
			transform = this.AvailableWinchTargets[this.SelectedTargetIndex].transform;
		}
		if (this.CarWeWantToAttachTo != null)
		{
			BodyPartsSwitcher component = this.CarWeWantToAttachTo.GetComponent<BodyPartsSwitcher>();
			transform = this.GetClosestTransform(component.FrontWinchPoint, component.RearWinchPoint, this.carController.transform.position);
			this.myCableType = CableType.CarToCar;
		}
		if (landAnchor != null)
		{
			transform = landAnchor;
		}
		this.CurrentWinchTarget = transform;
		this.ShowHideWinchTargets(false);
		this.ShowHideWinchZoneProjector(false);
		this.MinCableLength = Vector3.Distance(transform.position, frontWinchPoint.position);
		this.WinchAttached = true;
		this.LandAnchorMode = false;
		CarUIControl.Instance.HideNotification();
		CarUIControl.Instance.ToggleAttachButton(false, false);
		CarUIControl.Instance.ToggleCarControls(true);
		CarUIControl.Instance.ToggleCarExtras(true);
		CarUIControl.Instance.SwitchWinchTowButton(true);
		CarUIControl.Instance.SwitchWinchTargetSelector(false);
		CameraController.Instance.SetSideCamera();
		CameraController.Instance.GetCameraBack();
		if (GameState.GameMode != GameMode.Multiplayer)
		{
			return;
		}
		this.MyCableID = this.GenerateRandomID();
		if (this.CarWeWantToAttachTo != null)
		{
			this.photonTransformView.SendWinchAttachEvent(this.CarWeWantToAttachTo);
			this.photonTransformView.SendDynamicCableCreationEvent(this.MyCableID, this.CarWeWantToAttachTo.ownerId.ToString());
		}
		else
		{
			this.photonTransformView.SendStaticCableCreationEvent(this.MyCableID, transform.position);
		}
	}

	private Transform GetClosestTransform(Transform t1, Transform t2, Vector3 origin)
	{
		if (Vector3.Distance(origin, t1.position) < Vector3.Distance(origin, t2.position))
		{
			return t1;
		}
		return t2;
	}

	public void SwitchToLeftTarget()
	{
		if (this.AvailableWinchTargets.Count <= 1)
		{
			return;
		}
		if (this.SelectedTargetIndex > 0)
		{
			this.SelectedTargetIndex--;
		}
		else
		{
			this.SelectedTargetIndex = this.AvailableWinchTargets.Count - 1;
		}
		WinchTarget winchTarget = this.AvailableWinchTargets[this.SelectedTargetIndex];
		CarUIControl.Instance.ToggleAttachButton(winchTarget.DynamicTarget, true);
		CameraController.Instance.SelectedWinchTarget = this.AvailableWinchTargets[this.SelectedTargetIndex].transform;
	}

	public void SwitchToRightTarget()
	{
		if (this.AvailableWinchTargets.Count <= 1)
		{
			return;
		}
		if (this.SelectedTargetIndex == this.AvailableWinchTargets.Count - 1)
		{
			this.SelectedTargetIndex = 0;
		}
		else
		{
			this.SelectedTargetIndex++;
		}
		WinchTarget winchTarget = this.AvailableWinchTargets[this.SelectedTargetIndex];
		CarUIControl.Instance.ToggleAttachButton(winchTarget.DynamicTarget, true);
		CameraController.Instance.SelectedWinchTarget = this.AvailableWinchTargets[this.SelectedTargetIndex].transform;
	}

	private void ShowHideWinchTargets(bool Show)
	{
		foreach (WinchTarget winchTarget in this.WinchTargets)
		{
			if (winchTarget != null)
			{
				if (winchTarget.tView != null)
				{
					if (winchTarget.tView.trailer != null)
					{
						bool mpConnected = winchTarget.tView.trailer.mpConnected;
						if (mpConnected && Show)
						{
							continue;
						}
					}
					if (winchTarget.tView.onOtherPlayerTrailer)
					{
						continue;
					}
				}
				Vector3 position = winchTarget.transform.position;
				position.y = this.carController.transform.position.y;
				winchTarget.spriteRenderer.color = Color.green;
				winchTarget.gameObject.SetActive(Vector3.Distance(this.carController.transform.position, position) < this.WinchRadius && Show);
				winchTarget.transform.LookAt(this.carController.transform);
			}
		}
	}

	private void ShowHideWinchZoneProjector(bool Show)
	{
		this.WinchZoneProjector.gameObject.SetActive(Show);
		this.WinchZoneProjector.orthographicSize = this.WinchRadius * 2f;
		this.WinchZoneProjector.transform.position = this.carController.transform.position + Vector3.up * 10f;
	}

	public void AddWinchTarget(Vector3 Pos, Transform Parent, bool DynamicTarget)
	{
		WinchTarget component = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Other/WinchTarget"), Pos, Quaternion.identity, Parent)).GetComponent<WinchTarget>();
		component.transform.localScale = Vector3.one;
		component.gameObject.SetActive(false);
		component.DynamicTarget = DynamicTarget;
		this.WinchTargets.Add(component);
	}

	private void CreateWinchTargets()
	{
		this.WinchTargets = new List<WinchTarget>();
		if (SurfaceManager.Instance == null)
		{
			return;
		}
		if (SurfaceManager.Instance.BaseTerrain == null)
		{
			return;
		}
		Terrain baseTerrain = SurfaceManager.Instance.BaseTerrain;
		foreach (TreeInstance treeInstance in baseTerrain.terrainData.treeInstances)
		{
			Vector3 position = treeInstance.position;
			position.x *= baseTerrain.terrainData.size.x;
			position.y *= baseTerrain.terrainData.size.y;
			position.z *= baseTerrain.terrainData.size.z;
			this.AddWinchTarget(position + baseTerrain.GetPosition() + Vector3.up, baseTerrain.transform, false);
		}
		this.WinchZoneProjector = (UnityEngine.Object.Instantiate(Resources.Load("Other/WinchZoneProjector", typeof(GameObject))) as GameObject).GetComponent<Projector>();
		this.WinchZoneProjector.gameObject.SetActive(false);
	}

	private string GenerateRandomID()
	{
		string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text2 = string.Empty;
		for (int i = 0; i < 5; i++)
		{
			text2 += text[UnityEngine.Random.Range(0, text.Length)];
		}
		return text2;
	}

	public static WinchManager Instance;

	private Projector WinchZoneProjector;

	private CableType myCableType;

	private GameObject LandAnchor;

	private Transform MyCurrentWinchPoint;

	private int WinchTargetLayer = 8;

	private float WinchRadius = 20f;

	private List<WinchTarget> WinchTargets = new List<WinchTarget>();

	private List<WinchTarget> AvailableWinchTargets;

	private int SelectedTargetIndex;

	private Transform CurrentWinchTarget;

	private PhotonView CarAttachedToUs;

	private Transform WinchOfCarAttachedToUs;

	private PhotonView CarWeWantToAttachTo;

	private string CarThatSentWinchRequest;

	private float MinCableLength;

	private LineRenderer lineRenderer;

	public string MyCableID;

	public List<WinchCable> OtherPlayersCables;

	[HideInInspector]
	public bool WinchMode;

	[HideInInspector]
	public bool WinchAttached;

	[HideInInspector]
	public bool BeingWinchTarget;

	[HideInInspector]
	public bool WaitingForResponse;

	[HideInInspector]
	public bool LandAnchorMode;

	[HideInInspector]
	public bool TouchMoved;

	[HideInInspector]
	public bool WinchTowing;

	private float ropeDamping = 5000f;

	private float ropeForce = 3000f;
}
