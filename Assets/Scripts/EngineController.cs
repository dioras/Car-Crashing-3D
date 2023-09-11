using System;
using CustomVP;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class EngineController : MonoBehaviour
{
	public float[] Gears
	{
		get
		{
			if (this.carController != null)
			{
				return this.carController.GearRatios;
			}
			return GearsManager.DefaultGears;
		}
	}

	public int MaxGear
	{
		get
		{
			if (this.carController != null)
			{
				return this.carController.MaxGear;
			}
			return 5;
		}
	}

	public float minRpm
	{
		get
		{
			return 800f;
		}
	}

	public float maxRpm
	{
		get
		{
			return 6000f;
		}
	}

	public float gearDownRpm
	{
		get
		{
			return 3000f;
		}
	}

	public float underThrottleGearDownRpm
	{
		get
		{
			return 3500f;
		}
	}

	public float gearUpRpm
	{
		get
		{
			return 5500f;
		}
	}

	public float throttleRpmBoost
	{
		get
		{
			return 500f;
		}
	}

	public void SetDiesel(bool isDiesel)
	{
		this.Diesel = isDiesel;
	}

	private bool MovingBack()
	{
		if (this.carController == null)
		{
			return false;
		}
		if (this.tankController == null)
		{
			for (int i = 0; i < this.carController.wheels.Count; i++)
			{
				if (this.carController.wheels[i].wc.rpm > 0f)
				{
					return false;
				}
			}
		}
		if (this.tankController != null)
		{
			for (int j = 0; j < this.tankController.borderWheelColliders.Length; j++)
			{
				if (this.tankController.borderWheelColliders[j].perFrameRotation > 0f)
				{
					return false;
				}
			}
		}
		return true;
	}

	private void Awake()
	{
		this.surfaceManager = SurfaceManager.Instance;
		this.carController = base.GetComponent<CarController>();
		this.tankController = base.GetComponent<TankController>();
		if (this.ThrottleValve != null && this.ThrottleValve.t != null)
		{
			this.ThrottleValveDefaultRotation = this.ThrottleValve.t.localEulerAngles;
		}
	}

	private void Update()
	{
		if (this.engineSoundProcessor == null)
		{
			this.LoadEngineSounds();
		}
		this.CalculateSpeed();
		this.SimulateEngine();
		this.DoGearShifting();
		this.DoEngineSounds();
		this.RotatePulleys();
	}

	private void OnDisable()
	{
		if (this.engineSoundProcessor != null)
		{
			this.engineSoundProcessor.enabled = false;
		}
	}

	private void OnEnable()
	{
		if (this.engineSoundProcessor != null)
		{
			this.engineSoundProcessor.enabled = true;
		}
	}

	private void LoadEngineSounds()
	{
		if (this.carController == null || this.carController.vehicleDataManager == null)
		{
			return;
		}
		VehicleData vehicleData = this.carController.vehicleDataManager.GetVehicleData();
		this.engineSoundProcessor = base.GetComponentInChildren<EngineSoundProcessor>();
		if (this.engineSoundProcessor != null)
		{
			return;
		}
		if (this.Diesel)
		{
			this.engineSoundProcessor = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Sounds/DieselTruck"), base.transform.position, Quaternion.identity, base.transform)).GetComponent<EngineSoundProcessor>();
			this.Turbo = true;
			this.engineSoundProcessor.Turbo = true;
		}
		else if (this.carController.vehicleDataManager.vehicleType == VehicleType.Truck)
		{
			if (this.carController.EngineBlockStage >= 2)
			{
				this.engineSoundProcessor = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Sounds/GasTruckBigBlock"), base.transform.position, Quaternion.identity, base.transform)).GetComponent<EngineSoundProcessor>();
			}
			else
			{
				this.engineSoundProcessor = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Sounds/GasTruck"), base.transform.position, Quaternion.identity, base.transform)).GetComponent<EngineSoundProcessor>();
			}
			this.Turbo = this.PurchasedTurbo;
			this.engineSoundProcessor.Turbo = this.PurchasedTurbo;
		}
		else if (this.carController.vehicleDataManager.vehicleType == VehicleType.SideBySide)
		{
			this.engineSoundProcessor = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Sounds/SideBySide"), base.transform.position, Quaternion.identity, base.transform)).GetComponent<EngineSoundProcessor>();
			this.Turbo = false;
			this.engineSoundProcessor.Turbo = false;
		}
		else if (this.carController.vehicleDataManager.vehicleType == VehicleType.Crawler)
		{
			this.engineSoundProcessor = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Sounds/Crawler"), base.transform.position, Quaternion.identity, base.transform)).GetComponent<EngineSoundProcessor>();
			this.Turbo = false;
			this.engineSoundProcessor.Turbo = false;
		}
		else
		{
			this.engineSoundProcessor = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Sounds/ATV"), base.transform.position, Quaternion.identity, base.transform)).GetComponent<EngineSoundProcessor>();
			this.Turbo = false;
			this.engineSoundProcessor.Turbo = false;
		}
	}

	private void RotatePulleys()
	{
		if (this.pulleys != null)
		{
			foreach (Pulley pulley in this.pulleys)
			{
				pulley.t.Rotate(pulley.Rotation + pulley.Rotation * this.smoothThrottle);
			}
		}
		if (this.ThrottleValve != null && this.ThrottleValve.t != null)
		{
			this.ThrottleValve.t.localEulerAngles = Vector3.Lerp(this.ThrottleValveDefaultRotation, this.ThrottleValveDefaultRotation + this.ThrottleValve.Rotation, this.smoothThrottle);
		}
	}

	private void CalculateSpeed()
	{
		if (this.carController != null)
		{
			this.Speed = Mathf.Abs(this.carController.Speed);
			return;
		}
		Vector3 vector = base.transform.position - this.lastPos;
		float target = Vector3.ProjectOnPlane(vector, base.transform.up).magnitude / Time.deltaTime * 2.33f;
		this.Speed = Mathf.MoveTowards(this.Speed, target, Time.deltaTime * 20f);
		this.Speed = Mathf.Abs(this.Speed);
		this.lastPos = base.transform.position;
	}

	private void SimulateEngine()
	{
		this.throttle = ((!(this.carController != null)) ? 0f : Mathf.Abs(this.carController.Throttle));
		if (this.FakeRPMTarget > 1000f)
		{
			this.throttle = 1f;
		}
		this.smoothThrottle = Mathf.MoveTowards(this.smoothThrottle, this.throttle, Time.deltaTime * 10f);
		float num = 0f;
		this.Skidding = false;
		this.SkiddingReallyMuch = false;
		this.CarGrounded = true;
		float num2;
		if (this.carController == null)
		{
			num2 = this.minRpm + this.Speed * 5f * this.Gears[this.Gear] * this.TopGear;
		}
		else
		{
			this.yInput = UnityEngine.Input.GetAxis("Vertical") + CrossPlatformInputManager.GetAxis("Vertical");
			float num3 = this.throttleRpmBoost;
			float num4 = (!this.carController.LowGear) ? 1f : this.carController.LowGearRatio;
			this.CarGrounded = this.carController.Grounded;
			if (!this.carController.vehicleIsActive)
			{
				this.throttle = this.yInput;
				num3 = this.maxRpm - this.minRpm;
			}
			if (this.NeutralGear)
			{
				num3 = this.maxRpm - this.minRpm;
			}
			int num5 = 0;
			if (this.tankController == null)
			{
				for (int i = 0; i < this.carController.wheels.Count; i++)
				{
					if (this.carController.wheels[i].wc.wheelCollider != null)
					{
						this.carController.wheels[i].wc.wheelCollider.rpmLimit = 275f / this.carController.wheels[i].wc.wheelRadius / this.Gears[this.Gear] / this.TopGear / num4;
						if (this.carController.wheels[i].power)
						{
							num += this.carController.wheels[i].wc.rpm * this.carController.wheels[i].wc.wheelRadius;
							num5++;
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < this.tankController.allWheelColliders.Length; j++)
				{
					this.tankController.allWheelColliders[j].rpmLimit = 275f / this.tankController.allWheelColliders[j].wheelRadius / this.Gears[this.Gear] / this.TopGear / num4;
					num += this.tankController.allWheelColliders[j].rpm * this.tankController.allWheelColliders[j].wheelRadius;
					num5++;
				}
			}
			if (num5 > 0)
			{
				num /= (float)num5;
			}
			num2 = this.minRpm + Mathf.Abs(num * 2f) * this.Gears[this.Gear] * this.TopGear * num4 + num3 * this.throttle;
			num2 = Mathf.Clamp(num2, this.minRpm, this.maxRpm);
			this.Skidding = (Mathf.Abs(num / 2.5f / this.Speed) > 2f && this.CarGrounded);
			if (this.carController.wheels.Count == 2)
			{
				this.Skidding = false;
			}
			this.SkiddingReallyMuch = (Mathf.Abs(num / 2.5f / this.Speed) > 4f && this.CarGrounded);
		}
		if (this.FakeRPMTarget > 0f)
		{
			num2 = this.FakeRPMTarget;
		}
		float num6 = (float)((!this.NeutralGear) ? ((this.RPM >= num2) ? 5 : 25) : 40);
		this.RPM = Mathf.SmoothDamp(this.RPM, num2, ref this.revvingSpeed, Time.deltaTime * num6);
	}

	private void DoGearShifting()
	{
		if (this.FakeRPMTarget > 0f)
		{
			return;
		}
		int gear = this.Gear;
		if (this.carController != null && this.carController.transmissionType == TransmissionType.Manual)
		{
			if (CrossPlatformInputManager.GetButtonDown("ShiftUp") && this.Gear + 1 < this.MaxGear)
			{
				this.ShiftGear(true);
			}
			if (CrossPlatformInputManager.GetButtonDown("ShiftDown"))
			{
				this.ShiftGear(false);
			}
		}
		bool flag = false;
		if (this.carController == null)
		{
			flag = true;
		}
		if (this.carController != null && this.carController.transmissionType == TransmissionType.AT)
		{
			flag = true;
		}
		if (flag)
		{
			if (this.RPM > this.gearUpRpm && !this.Skidding && this.TimeSinceGearSwitching > 0.5f && this.CarGrounded && this.Gear + 1 < this.MaxGear)
			{
				this.ShiftGear(true);
			}
			if ((this.RPM < this.gearDownRpm || this.SkiddingReallyMuch || (this.throttle > 0.5f && this.RPM < this.underThrottleGearDownRpm)) && this.Gear > 0)
			{
				this.ShiftGear(false);
			}
			if (this.carController != null && (this.carController.LowGear || this.MovingBack()) && this.Gear > 0)
			{
				this.ShiftGear(false);
			}
		}
		this.TimeSinceGearSwitching += Time.deltaTime;
		if (gear != this.Gear)
		{
			if (this.TimeSinceGearSwitching > 1f && this.engineSoundProcessor != null)
			{
				this.engineSoundProcessor.GearShift();
			}
			this.TimeSinceGearSwitching = 0f;
		}
	}

	private void ShiftGear(bool Up)
	{
		if (!Up)
		{
			if (!Up)
			{
				if (!this.NeutralGear)
				{
					if (this.Gear > 0)
					{
						this.Gear--;
					}
					else if (this.Gear == 0 && !this.ReverseGear)
					{
						this.NeutralGear = true;
					}
				}
				else if (!this.ReverseGear)
				{
					this.ReverseGear = true;
					this.NeutralGear = false;
				}
			}
		}
		else if (this.ReverseGear)
		{
			this.ReverseGear = false;
			this.NeutralGear = true;
		}
		else if (this.NeutralGear)
		{
			this.NeutralGear = false;
		}
		else
		{
			this.Gear++;
		}
		int currentGear = this.Gear + 1;
		if (this.NeutralGear)
		{
			currentGear = -1;
		}
		if (this.ReverseGear)
		{
			currentGear = -2;
		}
		if (this.carController != null)
		{
			CarUIControl.Instance.SetCurrentGear(currentGear);
		}
	}

	private void DoEngineSounds()
	{
		if (this.engineSoundProcessor == null)
		{
			return;
		}
		float target = this.smoothThrottle;
		if (this.TimeSinceGearSwitching < 0.3f && !this.Skidding && this.FakeRPMTarget == 0f)
		{
			target = 0f;
		}
		this.engineSoundProcessor.RevLimiterAllowed = (this.Skidding || this.NeutralGear || !this.CarGrounded || this.FakeRPMTarget > 0f);
		this.engineSoundProcessor.RPM = this.RPM;
		this.engineSoundProcessor.load = Mathf.MoveTowards(this.engineSoundProcessor.load, target, Time.deltaTime * 50f);
		this.engineSoundProcessor.Turbo = this.Turbo;
	}

	public Pulley[] pulleys;

	public Pulley ThrottleValve;

	public bool Turbo;

	public bool PurchasedTurbo;

	[HideInInspector]
	public bool Diesel;

	private SurfaceManager surfaceManager;

	private CarController carController;

	private TankController tankController;

	private EngineSoundProcessor engineSoundProcessor;

	private float revvingSpeed;

	private float Speed;

	private Vector3 lastPos;

	private float TimeSinceGearSwitching;

	private bool CarGrounded;

	private bool Skidding;

	private bool SkiddingReallyMuch;

	private float throttle;

	private float yInput;

	private Vector3 ThrottleValveDefaultRotation;

	[HideInInspector]
	public bool NeutralGear;

	[HideInInspector]
	public bool ReverseGear;

	[HideInInspector]
	public float RPM;

	[HideInInspector]
	public int Gear;

	[HideInInspector]
	public float TopGear = 9f;

	public float FakeRPMTarget;

	private float smoothThrottle;
}
