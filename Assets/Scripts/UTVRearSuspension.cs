using System;
using CustomVP;
using UnityEngine;

public class UTVRearSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisOffset,
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.ShocksGroup,
			this.Controls.ShocksSize,
			this.Controls.Stiffness,
			this.Controls.Travel
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
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.side = Side.Rear;
		this.DoWheelColliderParameters();
		this.DoShocks();
		this.ChangeShocks();
		this.DoWidth();
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	private void DoWidth()
	{
		this.RLWheel.Hub.localPosition = new Vector3(this.Controls.AxisOffset.FloatValue, this.RLWheel.Hub.localPosition.y, -this.Controls.AxisWidth.FloatValue);
		this.RLWheel.WheelColliderHolder.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, this.RLWheel.WheelColliderHolder.localPosition.y, -this.Controls.AxisOffset.FloatValue);
		this.RRWheel.Hub.localPosition = new Vector3(-this.Controls.AxisOffset.FloatValue, this.RRWheel.Hub.localPosition.y, -this.Controls.AxisWidth.FloatValue);
		this.RRWheel.WheelColliderHolder.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, this.RRWheel.WheelColliderHolder.localPosition.y, -this.Controls.AxisOffset.FloatValue);
	}

	private void DoShocks()
	{
		this.RRWheel.ShockUps.LookAt(this.RRWheel.ShockDowns, -base.transform.right);
		this.RRWheel.ShockDowns.LookAt(this.RRWheel.ShockUps, -base.transform.right);
		this.RLWheel.ShockUps.LookAt(this.RLWheel.ShockDowns, -base.transform.right);
		this.RLWheel.ShockDowns.LookAt(this.RLWheel.ShockUps, -base.transform.right);
		this.RLWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RLWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.RRWheel.Shocks.Length; i++)
		{
			this.RLWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.RRWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
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
		this.RLWheel.BrakeDisk.Rotate(this.RLWheel.BrakeDisk.up, rpm, Space.World);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RLWheel.Dummy.position = position;
		Vector3 localPosition = this.RLWheel.Hub.localPosition;
		localPosition.y = this.RLWheel.Dummy.localPosition.y;
		this.RLWheel.Hub.localPosition = localPosition;
		this.RLWheel.ArmStart.LookAt(this.RLWheel.ArmEnd, base.transform.up);
		this.RLWheel.ArmEnd.LookAt(this.RLWheel.ArmStart, base.transform.up);
		this.RLWheel.ArmMount.LookAt(this.RLWheel.ArmStart, base.transform.up);
		this.RLWheel.ArmMount.localEulerAngles = new Vector3(this.RLWheel.ArmMount.localEulerAngles.x, -90f, this.RLWheel.ArmMount.localEulerAngles.z);
		this.RRWheel.BrakeDisk.Rotate(this.RRWheel.BrakeDisk.up, rpm, Space.World);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RRWheel.Dummy.position = position;
		localPosition = this.RRWheel.Hub.localPosition;
		localPosition.y = this.RRWheel.Dummy.localPosition.y;
		this.RRWheel.Hub.localPosition = localPosition;
		this.RRWheel.ArmStart.LookAt(this.RRWheel.ArmEnd, base.transform.up);
		this.RRWheel.ArmEnd.LookAt(this.RRWheel.ArmStart, base.transform.right);
		this.RRWheel.ArmMount.LookAt(this.RRWheel.ArmStart, base.transform.right);
		this.RRWheel.ArmMount.localEulerAngles = new Vector3(this.RRWheel.ArmMount.localEulerAngles.x, 90f, this.RRWheel.ArmMount.localEulerAngles.z);
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
		if (this.carController != null)
		{
			float steering = this.carController.Steering;
		}
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.RLWheel.BrakeDisk.Rotate(this.RLWheel.BrakeDisk.up, perFrameRotation, Space.World);
		this.RLWheel.Dummy.position = this.wheelColliders[0].GetVisualWheelPosition();
		Vector3 localPosition = this.RLWheel.Hub.localPosition;
		localPosition.y = this.RLWheel.Dummy.localPosition.y;
		this.RLWheel.Hub.localPosition = localPosition;
		this.RLWheel.ArmStart.LookAt(this.RLWheel.ArmEnd, base.transform.up);
		this.RLWheel.ArmEnd.LookAt(this.RLWheel.ArmStart, base.transform.up);
		this.RLWheel.ArmMount.LookAt(this.RLWheel.ArmStart, base.transform.up);
		this.RLWheel.ArmMount.localEulerAngles = new Vector3(this.RLWheel.ArmMount.localEulerAngles.x, -90f, this.RLWheel.ArmMount.localEulerAngles.z);
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.RRWheel.BrakeDisk.Rotate(this.RRWheel.BrakeDisk.up, perFrameRotation, Space.World);
		this.RRWheel.Dummy.position = this.wheelColliders[1].GetVisualWheelPosition();
		localPosition = this.RRWheel.Hub.localPosition;
		localPosition.y = this.RRWheel.Dummy.localPosition.y;
		this.RRWheel.Hub.localPosition = localPosition;
		this.RRWheel.ArmStart.LookAt(this.RRWheel.ArmEnd, base.transform.up);
		this.RRWheel.ArmEnd.LookAt(this.RRWheel.ArmStart, base.transform.right);
		this.RRWheel.ArmMount.LookAt(this.RRWheel.ArmStart, base.transform.right);
		this.RRWheel.ArmMount.localEulerAngles = new Vector3(this.RRWheel.ArmMount.localEulerAngles.x, 90f, this.RRWheel.ArmMount.localEulerAngles.z);
		this.DoShocks();
	}

	public UTVRearWheel RLWheel;

	public UTVRearWheel RRWheel;

	private CarController carController;

	public UTVRearSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
