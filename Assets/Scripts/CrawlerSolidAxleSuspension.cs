using System;
using CustomVP;
using UnityEngine;

public class CrawlerSolidAxleSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.ShocksGroup,
			this.Controls.ShocksSize,
			this.Controls.Stiffness,
			this.Controls.Travel,
			this.Controls.RearSteering,
			this.Controls.AxleType,
			this.Controls.BrakeType
		};
	}

	public override void SetControlValues(SuspensionValue[] values)
	{
		foreach (SuspensionValue suspensionValue in this.GetControlValues())
		{
			foreach (SuspensionValue suspensionValue2 in values)
			{
				if (suspensionValue2.ValueName == suspensionValue.ValueName)
				{
					suspensionValue.ReceiveValues(suspensionValue2);
				}
			}
		}
		this.OnValidate();
	}

	private void Start()
	{
		this.carController = base.GetComponentInParent<CarController>();
		this.OnValidate();
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.side = this.SuspensionSide;
		this.DoWidth();
		this.DoWheelColliderParameters();
		this.ChangeShocks();
		this.DoShocks();
		this.SwitchModels();
		if (this.carController != null && this.SuspensionSide == Side.Rear)
		{
			this.carController.InverseSteerMultiplier = this.Controls.RearSteering.FloatValue;
		}
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	private void SwitchModels()
	{
		this.RegularAxle.gameObject.SetActive(this.Controls.AxleType.IntValue == 0);
		this.RockwellAxle.gameObject.SetActive(this.Controls.AxleType.IntValue == 1);
		this.RegularBrake.gameObject.SetActive(this.Controls.BrakeType.IntValue == 0);
		this.PinionBrake.gameObject.SetActive(this.Controls.BrakeType.IntValue == 1);
	}

	private void DoShocks()
	{
		this.RWheel.ShockUps.LookAt(this.RWheel.ShockDowns, -base.transform.right);
		this.RWheel.ShockDowns.LookAt(this.RWheel.ShockUps, base.transform.right);
		this.LWheel.ShockUps.LookAt(this.LWheel.ShockDowns, -base.transform.right);
		this.LWheel.ShockDowns.LookAt(this.LWheel.ShockUps, base.transform.right);
		this.LWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.LWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.RWheel.Shocks.Length; i++)
		{
			this.LWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.RWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
	}

	private void DoWidth()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.LWheel.Axle.localPosition = new Vector3(0f, -this.Controls.AxisWidth.FloatValue, 0f);
		this.RWheel.Axle.localPosition = new Vector3(0f, this.Controls.AxisWidth.FloatValue, 0f);
		this.LWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.RWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
	}

	private void DoWheelColliderParameters()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		WheelComponent wheelComponent = this.wheelColliders[0];
		float floatValue = this.Controls.Travel.FloatValue;
		this.wheelColliders[1].suspensionLength = floatValue;
		wheelComponent.suspensionLength = floatValue;
		this.wheelColliders[0].spring = (this.wheelColliders[1].spring = this.Controls.Stiffness.FloatValue);
		this.wheelColliders[0].damper = (this.wheelColliders[1].damper = this.Controls.Damping.FloatValue);
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		if (this.SuspensionSide == Side.Rear)
		{
			rpm = -rpm;
			SteerAngle = -SteerAngle * this.Controls.RearSteering.FloatValue;
		}
		this.LWheel.BrakeDisk.Rotate(this.LWheel.BrakeDisk.up, rpm, Space.World);
		this.RWheel.BrakeDisk.Rotate(this.RWheel.BrakeDisk.up, rpm, Space.World);
		this.LWheel.Joint.Rotate(0f, rpm, 0f);
		this.LWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, SteerAngle);
		this.LWheel.TieRod.LookAt(this.RWheel.TieRod, base.transform.up);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.LWheel.Dummy.position = position;
		this.LWheel.ArmEnd.LookAt(this.LWheel.ArmStart, base.transform.up);
		this.LWheel.ArmStart.LookAt(this.LWheel.ArmEnd, base.transform.up);
		this.RWheel.Joint.Rotate(0f, rpm, 0f);
		this.RWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, SteerAngle);
		this.RWheel.TieRod.LookAt(this.LWheel.TieRod, base.transform.up);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RWheel.Dummy.position = position;
		this.RWheel.ArmEnd.LookAt(this.RWheel.ArmStart, base.transform.up);
		this.RWheel.ArmStart.LookAt(this.RWheel.ArmEnd, base.transform.up);
		Vector3 localPosition = this.AxleDummy.localPosition;
		localPosition.z = this.LWheel.Dummy.localPosition.z;
		this.AxleDummy.localPosition = localPosition;
		this.AxleDummy.LookAt(this.RWheel.Dummy, base.transform.up);
		this.RegularDriveshaft1Start.Rotate(new Vector3(0f, 0f, rpm));
		this.RegularDriveshaft1End.LookAt(this.RegularDriveshaft2End, this.RegularDriveshaft1Start.up);
		this.RegularDriveshaft2Start.Rotate(new Vector3(0f, 0f, -rpm));
		this.RegularDriveshaft2End.LookAt(this.RegularDriveshaft1End, this.RegularDriveshaft2Start.up);
		this.RockwellShaft.Rotate(new Vector3(rpm, 0f, 0f));
		this.RockwellDriveshaft1End.LookAt(this.RockwellDriveshaft2End, this.RockwellShaft.up);
		this.RockwellDriveshaft2Start.Rotate(new Vector3(0f, 0f, rpm));
		this.RockwellDriveshaft2End.LookAt(this.RockwellDriveshaft1End, this.RockwellDriveshaft2Start.up);
		this.SteeringRodEnd.LookAt(this.SteeringRodStart, base.transform.up);
		this.SteeringRodStart.LookAt(this.SteeringRodEnd, base.transform.up);
		if (this.carController != null)
		{
			this.SteeringRack.localPosition = new Vector3(Mathf.LerpUnclamped(-0.1f, 0f, SteerAngle / this.carController.maxSteeringAngle + 1f), 0f, 0f);
		}
		this.DoShocks();
	}

	private void FixedUpdate()
	{
		if (this.NoWheelColliders)
		{
			return;
		}
		foreach (WheelComponent x in this.wheelColliders)
		{
			if (x == null)
			{
				this.NoWheelColliders = true;
				return;
			}
		}
		float num = 0f;
		if (this.carController != null)
		{
			num = ((this.SuspensionSide != Side.Front) ? (-this.carController.Steering * this.carController.InverseSteerMultiplier) : this.carController.Steering);
		}
		float num2 = this.wheelColliders[0].wheelCollider.perFrameRotation;
		if (this.SuspensionSide == Side.Rear)
		{
			num2 = -num2;
		}
		this.LWheel.BrakeDisk.Rotate(this.LWheel.BrakeDisk.up, num2, Space.World);
		this.LWheel.Joint.Rotate(0f, num2, 0f);
		this.LWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, num);
		this.LWheel.TieRod.LookAt(this.RWheel.TieRod, base.transform.up);
		this.LWheel.Dummy.position = this.wheelColliders[0].GetVisualWheelPosition();
		this.LWheel.ArmEnd.LookAt(this.LWheel.ArmStart, base.transform.up);
		this.LWheel.ArmStart.LookAt(this.LWheel.ArmEnd, base.transform.up);
		num2 = this.wheelColliders[1].wheelCollider.perFrameRotation;
		if (this.SuspensionSide == Side.Rear)
		{
			num2 = -num2;
		}
		this.RWheel.BrakeDisk.Rotate(this.RWheel.BrakeDisk.up, num2, Space.World);
		this.RWheel.Joint.Rotate(0f, num2, 0f);
		this.RWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, num);
		this.RWheel.TieRod.LookAt(this.LWheel.TieRod, base.transform.up);
		this.RWheel.Dummy.position = this.wheelColliders[1].GetVisualWheelPosition();
		this.RWheel.ArmEnd.LookAt(this.RWheel.ArmStart, base.transform.up);
		this.RWheel.ArmStart.LookAt(this.RWheel.ArmEnd, base.transform.up);
		Vector3 localPosition = this.AxleDummy.localPosition;
		localPosition.z = this.LWheel.Dummy.localPosition.z;
		this.AxleDummy.localPosition = localPosition;
		this.AxleDummy.LookAt(this.RWheel.Dummy, base.transform.up);
		this.RegularDriveshaft1Start.Rotate(new Vector3(0f, 0f, num2));
		this.RegularDriveshaft1End.LookAt(this.RegularDriveshaft2End, this.RegularDriveshaft1Start.up);
		this.RegularDriveshaft2Start.Rotate(new Vector3(0f, 0f, -num2));
		this.RegularDriveshaft2End.LookAt(this.RegularDriveshaft1End, this.RegularDriveshaft2Start.up);
		this.RockwellShaft.Rotate(new Vector3(num2, 0f, 0f));
		this.RockwellDriveshaft1End.LookAt(this.RockwellDriveshaft2End, this.RockwellShaft.up);
		this.RockwellDriveshaft2Start.Rotate(new Vector3(0f, 0f, num2));
		this.RockwellDriveshaft2End.LookAt(this.RockwellDriveshaft1End, this.RockwellDriveshaft2Start.up);
		this.SteeringRodEnd.LookAt(this.SteeringRodStart, base.transform.up);
		this.SteeringRodStart.LookAt(this.SteeringRodEnd, base.transform.up);
		if (this.carController != null)
		{
			this.SteeringRack.localPosition = new Vector3(Mathf.LerpUnclamped(-0.1f, 0f, num / this.carController.maxSteeringAngle + 1f), 0f, 0f);
		}
		this.DoShocks();
	}

	public Side SuspensionSide;

	private CarController carController;

	public CrawlerSolidAxleWheel LWheel;

	public CrawlerSolidAxleWheel RWheel;

	public Transform RegularDriveshaft1Start;

	public Transform RegularDriveshaft1End;

	public Transform RegularDriveshaft2Start;

	public Transform RegularDriveshaft2End;

	public Transform RockwellDriveshaft1End;

	public Transform RockwellDriveshaft2Start;

	public Transform RockwellDriveshaft2End;

	public Transform RockwellShaft;

	public Transform RegularAxle;

	public Transform RockwellAxle;

	public Transform RegularBrake;

	public Transform PinionBrake;

	public Transform SteeringRodStart;

	public Transform SteeringRodEnd;

	public Transform AxleDummy;

	public Transform SteeringRack;

	[Space(10f)]
	public CrawlerSolidAxleControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
