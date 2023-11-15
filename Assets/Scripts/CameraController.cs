 using System;
using CustomVP;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraController : MonoBehaviour
{
	public CameraController()
	{
		if (CameraController.Instance == null)
		{
			CameraController.Instance = this;
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

	private IKDriverController driver
	{
		get
		{
			if (VehicleLoader.Instance != null)
			{
				return VehicleLoader.Instance.playerDriver;
			}
			return null;
		}
	}

	private Transform target
	{
		get
		{
			if (this.forcedTarget != null)
			{
				return this.forcedTarget;
			}
			if (this.carController != null)
			{
				return this.carController.transform;
			}
			if (MenuManager.Instance != null)
			{
				return MenuManager.Instance.CameraTarget;
			}
			return null;
		}
	}

	private Camera cam
	{
		get
		{
			if (this._cam == null)
			{
				this._cam = base.GetComponent<Camera>();
			}
			return this._cam;
		}
	}

	private void Awake()
	{
		CameraController.Instance = this;
	}

	private void Start()
	{
		this.DistanceCamTarget = this.DistanceStart;
		this.AngleX = this.XStart;
		this.TargetYAngle = this.YStart;
	}

	public void Shake()
	{
		this.ShakeAmount = 1f;
	}

	private void ToggleDriver(bool Show)
	{
		if (this.driver == null)
		{
			return;
		}
		this.driver.ToggleDriver(Show, !Show);
	}

	public string SwitchCamera()
	{
		bool flag = this.driver != null && this.driver.KnockedOut;
		if (flag)
		{
			return null;
		}
		this.ToggleDriver(true);
		if (this.cameraMode == CameraController.CameraMode.Follow)
		{
			this.cameraMode = CameraController.CameraMode.Free;
			return "Free camera";
		}
		if (this.cameraMode == CameraController.CameraMode.Free)
		{
			this.cameraMode = CameraController.CameraMode.FirstPerson;
			this.ToggleDriver(false);
			return "First Person";
		}
		if (this.cameraMode == CameraController.CameraMode.FirstPerson)
		{
			this.cameraMode = CameraController.CameraMode.Cinematic;
			this.GenerateCinematicCameraPoint();
			return "Cinematic Camera";
		}
		this.cameraMode = CameraController.CameraMode.Follow;
		return "Follow Camera";
	}

	public void SetDroneCamera(DroneController dr)
	{
		this.drone = dr;
		this.desiredYAngle = 30f;
		this.droneAngleX = 0f;
		this.cameraMode = CameraController.CameraMode.DroneFollow;
		this.ToggleDriver(true);
	}

	public string SwitchDroneCamera()
	{
		if (this.cameraMode == CameraController.CameraMode.DroneFollow)
		{
			this.droneAngleX = this.drone.transform.eulerAngles.y;
			this.cameraMode = CameraController.CameraMode.DroneLook;
			return "Look camera";
		}
		if (this.cameraMode == CameraController.CameraMode.DroneLook)
		{
			this.AngleX = this.drone.transform.eulerAngles.y;
			this.cameraMode = CameraController.CameraMode.DroneFollow;
			return "Follow camera";
		}
		return string.Empty;
	}

	private void GenerateCinematicCameraPoint()
	{
		this.CinematicCameraPoint = this.target.position + UnityEngine.Random.insideUnitSphere * 40f;
		this.CinematicCameraPoint.y = this.CinematicCameraPoint.y + 100f;
		this.HeightAboveGround = (float)UnityEngine.Random.Range(1, 10);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.CinematicCameraPoint, Vector3.down, out raycastHit))
		{
			this.CinematicCameraPoint.y = raycastHit.point.y + this.HeightAboveGround;
		}
	}

	public void SetCameraPos(float X, float Y, float Distance)
	{
		this.AngleX = X;
		this.TargetYAngle = Y;
		this.DistanceCamTarget = Distance;
	}

	public void SetWinchCamera()
	{
		this.cameraModeBeforeWinchMode = this.cameraMode;
		this.cameraMode = CameraController.CameraMode.WinchTargetSelection;
	}

	private void OnDisable()
	{
		this.cameraMode = CameraController.CameraMode.Follow;
	}

	public void SetRagdollCamera()
	{
		this.cameraModeBeforeRagdollMode = this.cameraMode;
		this.cameraMode = CameraController.CameraMode.Ragdoll;
	}

	public void SetSideCamera()
	{
		this.cameraMode = CameraController.CameraMode.Side;
	}

	public void GetCameraBack()
	{
		if (this.cameraMode == CameraController.CameraMode.WinchTargetSelection)
		{
			this.cameraMode = this.cameraModeBeforeWinchMode;
		}
		if (this.cameraMode == CameraController.CameraMode.Side)
		{
			this.cameraMode = this.cameraModeBeforeWinchMode;
		}
		if (this.cameraMode == CameraController.CameraMode.Ragdoll)
		{
			this.cameraMode = this.cameraModeBeforeRagdollMode;
		}
		this.SelectedWinchTarget = null;
	}

	private void ToggleSlowMo()
	{
		this.SlowMo = !this.SlowMo;
		Time.timeScale = ((!this.SlowMo) ? 1f : 0.3f);
	}

	private void LateUpdate()
	{
		if (this.target == null)
		{
			return;
		}
		if (CrossPlatformInputManager.GetButtonDown("Swipe"))
		{
			this.Swiping = true;
		}
		if (UnityEngine.Input.touchCount == 0)
		{
			this.Swiping = false;
		}
		this.ShakeAmount = Mathf.MoveTowards(this.ShakeAmount, 0f, Time.deltaTime * 4f);
		if (UnityEngine.Input.GetKeyDown(this.SlowMoToggleButton))
		{
			this.ToggleSlowMo();
		}
		switch (this.cameraMode)
		{
		case CameraController.CameraMode.Free:
			this.DoFreeNavigation();
			this.DistanceCamTarget = Mathf.Clamp(this.DistanceCamTarget - UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 3f, this.MinDistance, this.MaxDistance);
			this.DoSphereCam();
			this.cam.fieldOfView = 60f;
			break;
		case CameraController.CameraMode.Follow:
			if (!(this.carController == null))
			{
				if (this.carController.Speed >= 0f)
				{
					this.AngleX = 0f;
				}
				if (this.ForceRearView || (this.carController.Speed < -10f && this.carController.WheelsOffTheGround == 0))
				{
					this.AngleX = 180f;
				}
				this.desiredYAngle = this.carController.FollowYAngle;
				this.DistanceCamTarget = this.carController.FollowDistance;
				bool flag = Physics.CheckSphere(base.transform.position, 0.7f);
				if (Mathf.Abs(this.TargetYAngle - this.desiredYAngle) > 3f && !flag)
				{
					this.TargetYAngle = Mathf.MoveTowards(this.TargetYAngle, this.desiredYAngle, Time.deltaTime * 50f);
				}
				this.DoSphereCam();
				this.cam.fieldOfView = 60f;
			}
			break;
		case CameraController.CameraMode.Side:
			if (!(this.carController == null))
			{
				this.AngleX = this.SideXAngle;
				this.desiredYAngle = this.carController.FollowYAngle;
				bool flag = Physics.CheckSphere(base.transform.position, 0.7f);
				if (Mathf.Abs(this.TargetYAngle - this.desiredYAngle) > 3f && !flag)
				{
					this.TargetYAngle = Mathf.MoveTowards(this.TargetYAngle, this.desiredYAngle, Time.deltaTime * 50f);
				}
				this.DistanceCamTarget = this.carController.FollowDistance;
				this.DoSphereCam();
				this.cam.fieldOfView = 60f;
			}
			break;
		case CameraController.CameraMode.FirstPerson:
			if (!(this.carController == null))
			{
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.target.transform.rotation, this.FirstPersonDamping * Time.deltaTime);
				base.transform.position = this.carController.FirstPersonPoint.position;
				this.cam.fieldOfView = 60f;
			}
			break;
		case CameraController.CameraMode.WinchTargetSelection:
			base.transform.position = Vector3.Lerp(base.transform.position, this.target.transform.position + Vector3.up * this.WinchTargetSelectionHeight, Time.deltaTime * this.CameraMovingSpeed);
			if (this.SelectedWinchTarget != null)
			{
				Quaternion b = base.transform.rotation;
				if (this.SelectedWinchTarget != null)
				{
					b = Quaternion.LookRotation(this.SelectedWinchTarget.position - base.transform.position);
				}
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 2f);
			}
			this.cam.fieldOfView = 60f;
			break;
		case CameraController.CameraMode.Photo:
			this.DoFreeNavigation();
			this.DistanceCamTarget = Mathf.Clamp(this.DistanceCamTarget - UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 3f, this.MinDistance, this.MaxDistance * 2f);
			this.DoSphereCam();
			this.cam.fieldOfView = 60f;
			break;
		case CameraController.CameraMode.Cinematic:
		{
			base.transform.position = Vector3.SmoothDamp(base.transform.position, this.CinematicCameraPoint + UnityEngine.Random.insideUnitSphere * 5f, ref this.movingSpeed, Time.deltaTime * 100f);
			this._height = Mathf.MoveTowards(this._height, this.HeightAboveGround, Time.deltaTime);
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 10f, Vector3.down, out raycastHit) && raycastHit.collider.GetType() == typeof(TerrainCollider))
			{
				base.transform.position = raycastHit.point + Vector3.up * this._height;
			}
			if (Physics.Raycast(base.transform.position, this.target.position - base.transform.position, out raycastHit) && raycastHit.collider.transform.root != this.target && raycastHit.collider.GetType() == typeof(TerrainCollider))
			{
				this.GenerateCinematicCameraPoint();
			}
			base.transform.LookAt(this.target);
			if (Vector3.Distance(this.target.position, new Vector3(this.CinematicCameraPoint.x, this.target.position.y, this.CinematicCameraPoint.z)) > 30f)
			{
				this.GenerateCinematicCameraPoint();
			}
			this.cam.fieldOfView = 60f;
			break;
		}
		case CameraController.CameraMode.DroneFollow:
			this.DoDroneFollowCam();
			this.cam.fieldOfView = 60f;
			break;
		case CameraController.CameraMode.DroneLook:
			this.DoDroneLookCam();
			break;
		}
	}

	private void DoFreeNavigation()
	{
		if (this.Swiping)
		{
			if (UnityEngine.Input.touchCount == 1)
			{
				this.AngleX += UnityEngine.Input.GetTouch(0).deltaPosition.x / 10f * this.SwipeSpeed;
				this.droneAngleX += UnityEngine.Input.GetTouch(0).deltaPosition.x / 10f * this.SwipeSpeed;
				float num = UnityEngine.Input.GetTouch(0).deltaPosition.y / 10f * this.SwipeSpeed;
				if (!this.CameraDislocated || (this.CameraDislocated && num < 0f))
				{
					this.TargetYAngle -= num;
				}
				this.desiredYAngle = this.TargetYAngle;
			}
			if (UnityEngine.Input.touchCount == 2)
			{
				Vector2 a = UnityEngine.Input.GetTouch(0).position - UnityEngine.Input.GetTouch(0).deltaPosition;
				Vector2 b = UnityEngine.Input.GetTouch(1).position - UnityEngine.Input.GetTouch(1).deltaPosition;
				float magnitude = (a - b).magnitude;
				float magnitude2 = (UnityEngine.Input.GetTouch(0).position - UnityEngine.Input.GetTouch(1).position).magnitude;
				float num2 = magnitude - magnitude2;
				this.DistanceCamTarget += num2 * Time.deltaTime / 2f * this.ScrollSpeed;
				this.droneFOV -= num2 * Time.deltaTime / 4f * this.ScrollSpeed;
			}
		}
		this.droneFOV = Mathf.Clamp01(this.droneFOV);
		bool flag = Physics.CheckSphere(base.transform.position, 0.7f);
		if ((Mathf.Abs(this.TargetYAngle - this.desiredYAngle) < 3f && !flag) || this.desiredYAngle == 0f)
		{
			this.desiredYAngle = this.TargetYAngle;
		}
		if (Mathf.Abs(this.TargetYAngle - this.desiredYAngle) > 3f && !flag)
		{
			this.TargetYAngle = Mathf.MoveTowards(this.TargetYAngle, this.desiredYAngle, Time.deltaTime * 50f);
		}
	}

	private void DoDroneFreeNavigation()
	{
		if (this.Swiping)
		{
			if (UnityEngine.Input.touchCount == 1 && DroneJoystick.Instance != null && !DroneJoystick.Instance.dragging)
			{
				this.AngleX += UnityEngine.Input.GetTouch(0).deltaPosition.x / 10f * this.SwipeSpeed * 2f;
				this.droneAngleX += UnityEngine.Input.GetTouch(0).deltaPosition.x / 10f * this.SwipeSpeed * 2f;
				float num = UnityEngine.Input.GetTouch(0).deltaPosition.y / 10f * this.SwipeSpeed * 2f;
				this.TargetYAngle -= num;
				this.desiredYAngle = this.TargetYAngle;
			}
			if (UnityEngine.Input.touchCount == 2 && DroneJoystick.Instance != null && DroneJoystick.Instance.dragging)
			{
				Touch touch = UnityEngine.Input.GetTouch(0);
				Touch touch2 = UnityEngine.Input.GetTouch(1);
				if (touch.position.x > touch2.position.x)
				{
					touch = UnityEngine.Input.GetTouch(1);
					touch2 = UnityEngine.Input.GetTouch(0);
				}
				this.AngleX += touch2.deltaPosition.x / 10f * this.SwipeSpeed * 2f;
				this.droneAngleX += touch2.deltaPosition.x / 10f * this.SwipeSpeed * 2f;
				float num2 = touch2.deltaPosition.y / 10f * this.SwipeSpeed * 2f;
				this.TargetYAngle -= num2;
				this.desiredYAngle = this.TargetYAngle;
			}
			if (UnityEngine.Input.touchCount == 2 && DroneJoystick.Instance != null && !DroneJoystick.Instance.dragging)
			{
				Vector2 a = UnityEngine.Input.GetTouch(0).position - UnityEngine.Input.GetTouch(0).deltaPosition;
				Vector2 b = UnityEngine.Input.GetTouch(1).position - UnityEngine.Input.GetTouch(1).deltaPosition;
				float magnitude = (a - b).magnitude;
				float magnitude2 = (UnityEngine.Input.GetTouch(0).position - UnityEngine.Input.GetTouch(1).position).magnitude;
				float num3 = magnitude - magnitude2;
				this.DistanceCamTarget += num3 * Time.deltaTime / 2f * this.ScrollSpeed;
				this.droneFOV -= num3 * Time.deltaTime / 4f * this.ScrollSpeed;
			}
		}
		this.droneFOV = Mathf.Clamp01(this.droneFOV);
		bool flag = Physics.CheckSphere(base.transform.position, 0.7f);
		if ((Mathf.Abs(this.TargetYAngle - this.desiredYAngle) < 3f && !flag) || this.desiredYAngle == 0f)
		{
			this.desiredYAngle = this.TargetYAngle;
		}
		if (Mathf.Abs(this.TargetYAngle - this.desiredYAngle) > 3f && !flag)
		{
			this.TargetYAngle = Mathf.MoveTowards(this.TargetYAngle, this.desiredYAngle, Time.deltaTime * 50f);
		}
	}

	private void FixedUpdate()
	{
		if (this.target == null)
		{
			return;
		}
		if (this.cameraMode == CameraController.CameraMode.Ragdoll)
		{
			Vector3 b = this.Ragdoll.position - this.target.transform.forward * 2f + Vector3.up * 2f;
			base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * 10f);
			base.transform.LookAt(this.Ragdoll, base.transform.up);
		}
	}

	private void DoDroneFollowCam()
	{
		if (this.drone == null)
		{
			return;
		}
		this.DoDroneFreeNavigation();
		this.DistanceCamTarget = Mathf.Clamp(this.DistanceCamTarget - UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 3f, this.minDroneDistance, this.maxDroneDistance);
		this.DistanceCam = Mathf.Lerp(this.DistanceCam, this.DistanceCamTarget, 10f * Time.deltaTime);
		Vector3 a = this.drone.transform.position + Vector3.ProjectOnPlane(this.drone.transform.forward, Vector3.up).normalized * this.droneForwardOffset + Vector3.up * this.droneUpOffset;
		this.desiredYAngle = Mathf.Clamp(this.desiredYAngle, -30f, 80f);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(this.desiredYAngle, this.AngleX, 0f), Time.deltaTime * 10f);
		base.transform.position = a - base.transform.rotation * Vector3.forward * this.DistanceCam;
		this.drone.FeedWantedYAngle(this.AngleX);
	}

	private void DoDroneLookCam()
	{
		this.DoDroneFreeNavigation();
		this.droneFOV = Mathf.Clamp(this.droneFOV + UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 1.5f, 0f, 1f);
		this.desiredYAngle = Mathf.Clamp(this.desiredYAngle, -45f, 80f);
		base.transform.position = this.drone.cameraPos.position;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(this.desiredYAngle, this.droneAngleX, 0f), Time.deltaTime * 10f);
		float b = Mathf.Lerp(this.maxDroneFOV, this.minDroneFOV, this.droneFOV);
		this.cam.fieldOfView = Mathf.Lerp(this.cam.fieldOfView, b, Time.deltaTime * 4f);
		this.drone.FeedWantedYAngle(this.droneAngleX);
	}

	private void DoSphereCam()
	{
		this.TargetYAngle = Mathf.Clamp(this.TargetYAngle, 2, this.YMax); //minAngle2 was -45 before
		this.AngleY = this.TargetYAngle;
		this.DistanceCam = Mathf.Lerp(this.DistanceCam, this.DistanceCamTarget, 10f * Time.deltaTime);
		bool flag = false;
		if (this.carController != null && this.carController.WheelsOffTheGround < this.carController.WheelsCount)
		{
			flag = true;
		}
		float y = this.target.transform.eulerAngles.y;
		if (flag)
		{
			this.CurrentXAngle = Mathf.LerpAngle(this.CurrentXAngle, y, this.RotationDamping * Time.deltaTime);
		}
		float num = (!flag) ? 0f : this.target.transform.eulerAngles.x;
		if (this.AngleX == 180f)
		{
			num = -num;
		}
		if (this.cameraMode == CameraController.CameraMode.Follow)
		{
			this.CurrentYAngle = Mathf.LerpAngle(this.CurrentYAngle, num, this.HeightDamping * Time.deltaTime);
		}
		Vector3 b = UnityEngine.Random.onUnitSphere * this.ShakeAmount * this.ShakeAmplitude;
		Quaternion rotation = Quaternion.Euler(this.CurrentYAngle + this.AngleY, this.CurrentXAngle + this.AngleX, 0f);
		Vector3 position = this.target.transform.position - rotation * Vector3.forward * this.DistanceCam;
		int num2 = 0;
		while (Physics.CheckSphere(position, 0.5f) && num2 < 20)
		{
			num2++;
			this.TargetYAngle += 1f;
			this.AngleY = this.TargetYAngle;
			rotation = Quaternion.Euler(this.CurrentYAngle + this.AngleY, this.CurrentXAngle + this.AngleX, 0f);
			position = this.target.transform.position - rotation * Vector3.forward * this.DistanceCam;
		}
		Quaternion rotation2 = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(this.CurrentYAngle + this.AngleY, this.CurrentXAngle + this.AngleX, 0f), Time.deltaTime * 10f);
		Vector3 position2 = this.target.transform.position - rotation2 * Vector3.forward * this.DistanceCam + b;
		this.CameraDislocated = (this.AngleY != this.TargetYAngle);
		base.transform.position = position2;
		base.transform.rotation = rotation2;
	}

	public static CameraController Instance;

	[HideInInspector]
	public Transform forcedTarget;

	private Camera _cam;

	public DroneController drone;

	public CameraController.CameraMode cameraMode;

	private CameraController.CameraMode cameraModeBeforeWinchMode;

	private CameraController.CameraMode cameraModeBeforeRagdollMode;

	[Header("Start settings")]
	public float XStart;

	public float YStart;

	public float DistanceStart = 5f;

	[Header("Common settings")]
	public float RotationDamping = 2f;

	public float HeightDamping = 2f;

	public float ShakeAmplitude = 0.5f;

	public float YMax = 70f;

	public KeyCode SlowMoToggleButton;

	[Header("Free")]
	public float SwipeSpeed = 1f;

	public float ScrollSpeed = 1f;

	public float MinDistance = 3f;

	public float MaxDistance = 10f;

	[Header("Side")]
	public float SideXAngle = 60f;

	[Header("First Person")]
	public float FirstPersonDamping = 10f;

	private float droneForwardOffset = 0.3f;

	private float droneUpOffset = 0.3f;

	private float minDroneDistance = 1f;

	private float maxDroneDistance = 3f;

	private float maxDroneFOV = 60f;

	private float minDroneFOV = 10f;

	private float droneFOV;

	private float droneAngleX;

	[Header("WinchTargetSelection")]
	public float WinchTargetSelectionHeight = 3f;

	public float CameraMovingSpeed = 1f;

	[HideInInspector]
	public Transform SelectedWinchTarget;

	[HideInInspector]
	public Transform Ragdoll;

	private float DistanceCam;

	[HideInInspector]
	public float DistanceCamTarget;

	private float CurrentXAngle;

	private float CurrentYAngle;

	private float ShakeAmount;

	private float AngleX;

	private float AngleY;

	private float TargetYAngle;

	private float desiredYAngle;

	private bool Swiping;

	private bool SlowMo;

	private bool CameraDislocated;

	public bool ForceRearView;

	private Vector3 CinematicCameraPoint;

	private float HeightAboveGround;

	private float _height;

	private Vector3 movingSpeed;

	public enum CameraMode
	{
		Free,
		Follow,
		Side,
		FirstPerson,
		Ragdoll,
		WinchTargetSelection,
		Photo,
		Cinematic,
		DroneFollow,
		DroneLook
	}
}
