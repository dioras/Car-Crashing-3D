using System;
using CustomVP;
using UnityEngine;

public class MonsterSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.ShocksSize,
			this.Controls.Stiffness,
			this.Controls.Travel,
			this.Controls.RearSteering
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
		this.DoShocks();
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

	private void DoShocks()
	{
		this.RWheel.ShockUps.LookAt(this.RWheel.ShockDowns, -base.transform.forward);
		this.RWheel.ShockDowns.LookAt(this.RWheel.ShockUps, -base.transform.forward);
		this.LWheel.ShockUps.LookAt(this.LWheel.ShockDowns, -base.transform.forward);
		this.LWheel.ShockDowns.LookAt(this.LWheel.ShockUps, -base.transform.forward);
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
		this.LWheel.AxleEnd.localPosition = new Vector3(0f, 0f, this.Controls.AxisWidth.FloatValue);
		this.RWheel.AxleEnd.localPosition = new Vector3(0f, 0f, this.Controls.AxisWidth.FloatValue);
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
		this.LWheel.BrakeDisk.Rotate(this.LWheel.BrakeDisk.forward, -rpm, Space.World);
		this.RWheel.BrakeDisk.Rotate(this.RWheel.BrakeDisk.forward, rpm, Space.World);
		this.LWheel.Joint.Rotate(0f, 0f, rpm);
		this.LWheel.Knuckle.localEulerAngles = new Vector3(0f, SteerAngle, 0f);
		this.LWheel.TieRodEnd.LookAt(this.RWheel.TieRodEnd, base.transform.up);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.LWheel.Dummy.position = position;
		this.LWheel.ArmEnd.LookAt(this.LWheel.ArmStart, base.transform.up);
		this.LWheel.ArmStart.LookAt(this.LWheel.ArmEnd, base.transform.up);
		this.LWheel.SupportArmStart.LookAt(this.LWheel.SupportArmEnd, base.transform.up);
		this.LWheel.SupportArmEnd.LookAt(this.LWheel.SupportArmStart, base.transform.up);
		this.RWheel.Joint.Rotate(0f, 0f, rpm);
		this.RWheel.Knuckle.localEulerAngles = new Vector3(0f, SteerAngle, 0f);
		this.RWheel.TieRodEnd.LookAt(this.LWheel.TieRodEnd, base.transform.up);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RWheel.Dummy.position = position;
		this.RWheel.ArmEnd.LookAt(this.RWheel.ArmStart, base.transform.up);
		this.RWheel.ArmStart.LookAt(this.RWheel.ArmEnd, base.transform.up);
		this.RWheel.SupportArmStart.LookAt(this.RWheel.SupportArmEnd, base.transform.up);
		this.RWheel.SupportArmEnd.LookAt(this.RWheel.SupportArmStart, base.transform.up);
		Vector3 localPosition = this.AxleDummy.localPosition;
		localPosition.y = this.LWheel.Dummy.localPosition.y;
		this.AxleDummy.localPosition = localPosition;
		this.AxleDummy.LookAt(this.RWheel.Dummy, base.transform.up);
		this.DriveshaftStart.Rotate(this.DriveshaftStart.forward, rpm, Space.World);
		this.DriveshaftEnd.LookAt(this.DriveshaftLookPos, this.DriveshaftStart.up);
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
		float y = 0f;
		if (this.carController != null)
		{
			y = ((this.SuspensionSide != Side.Front) ? (-this.carController.Steering * this.carController.InverseSteerMultiplier) : this.carController.Steering);
		}
		float num = this.wheelColliders[0].wheelCollider.perFrameRotation;
		if (this.SuspensionSide == Side.Rear)
		{
			num = -num;
		}
		this.LWheel.BrakeDisk.Rotate(this.LWheel.BrakeDisk.forward, -num, Space.World);
		this.LWheel.Joint.Rotate(0f, 0f, -num);
		this.LWheel.Knuckle.localEulerAngles = new Vector3(0f, y, 0f);
		this.LWheel.TieRodEnd.LookAt(this.RWheel.TieRodEnd, base.transform.up);
		this.LWheel.Dummy.position = this.wheelColliders[0].GetVisualWheelPosition();
		this.LWheel.ArmEnd.LookAt(this.LWheel.ArmStart, base.transform.up);
		this.LWheel.ArmStart.LookAt(this.LWheel.ArmEnd, base.transform.up);
		this.LWheel.SupportArmStart.LookAt(this.LWheel.SupportArmEnd, base.transform.up);
		this.LWheel.SupportArmEnd.LookAt(this.LWheel.SupportArmStart, base.transform.up);
		num = this.wheelColliders[1].wheelCollider.perFrameRotation;
		if (this.SuspensionSide == Side.Rear)
		{
			num = -num;
		}
		this.RWheel.BrakeDisk.Rotate(this.RWheel.BrakeDisk.forward, num, Space.World);
		this.RWheel.Joint.Rotate(0f, 0f, num);
		this.RWheel.Knuckle.localEulerAngles = new Vector3(0f, y, 0f);
		this.RWheel.TieRodEnd.LookAt(this.LWheel.TieRodEnd, base.transform.up);
		this.RWheel.Dummy.position = this.wheelColliders[1].GetVisualWheelPosition();
		this.RWheel.ArmEnd.LookAt(this.RWheel.ArmStart, base.transform.up);
		this.RWheel.ArmStart.LookAt(this.RWheel.ArmEnd, base.transform.up);
		this.RWheel.SupportArmStart.LookAt(this.LWheel.SupportArmEnd, base.transform.up);
		this.RWheel.SupportArmEnd.LookAt(this.LWheel.SupportArmStart, base.transform.up);
		Vector3 localPosition = this.AxleDummy.localPosition;
		localPosition.y = this.LWheel.Dummy.localPosition.y;
		this.AxleDummy.localPosition = localPosition;
		this.AxleDummy.LookAt(this.RWheel.Dummy, base.transform.up);
		this.DriveshaftStart.Rotate(this.DriveshaftStart.forward, num, Space.World);
		this.DriveshaftEnd.LookAt(this.DriveshaftLookPos, this.DriveshaftStart.up);
		this.DoShocks();
	}

	public Side SuspensionSide;

	private CarController carController;

	public MonsterWheel LWheel;

	public MonsterWheel RWheel;

	public Transform DriveshaftStart;

	public Transform DriveshaftEnd;

	public Transform DriveshaftLookPos;

	public Transform AxleDummy;

	[Space(10f)]
	public MonsterControls Controls;

	private bool NoWheelColliders;
}
