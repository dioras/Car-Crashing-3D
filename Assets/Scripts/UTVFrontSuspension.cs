using System;
using CustomVP;
using UnityEngine;

public class UTVFrontSuspension : Suspension
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
		this.OnValidate();
	}

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.DoWidth();
		this.DoWheelColliderParameters();
		this.DoShocks();
		this.ChangeShocks();
		this.side = Side.Front;
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	private void DoShocks()
	{
		this.FRWheel.ShockUps.LookAt(this.FRWheel.ShockDowns, -base.transform.forward);
		this.FRWheel.ShockDowns.LookAt(this.FRWheel.ShockUps, -base.transform.forward);
		this.FLWheel.ShockUps.LookAt(this.FLWheel.ShockDowns, -base.transform.forward);
		this.FLWheel.ShockDowns.LookAt(this.FLWheel.ShockUps, -base.transform.forward);
		this.FLWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FLWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FRWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FRWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.FRWheel.Shocks.Length; i++)
		{
			this.FLWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.FRWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
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

	private void DoWidth()
	{
		this.FLWheel.LowerArmEnd.localPosition = new Vector3(0f, 0f, this.Controls.AxisWidth.FloatValue / this.FLWheel.LowerArmEnd.lossyScale.x);
		this.FLWheel.UpperArmEnd.localPosition = new Vector3(0f, 0f, this.Controls.AxisWidth.FloatValue / this.FLWheel.LowerArmEnd.lossyScale.x);
		this.FRWheel.LowerArmEnd.localPosition = new Vector3(0f, 0f, this.Controls.AxisWidth.FloatValue / this.FRWheel.LowerArmEnd.lossyScale.x);
		this.FRWheel.UpperArmEnd.localPosition = new Vector3(0f, 0f, this.Controls.AxisWidth.FloatValue / this.FRWheel.LowerArmEnd.lossyScale.x);
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.up, rpm, Space.World);
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.up, rpm, Space.World);
		this.FLWheel.TieRod.LookAt(this.SteeringRack, base.transform.up);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 position2 = this.FLWheel.LowerArmStart.parent.InverseTransformPoint(position);
		position2.x = this.FLWheel.LowerArmStart.localPosition.x;
		position2.z -= 0.36f;
		float x = this.FLWheel.LowerArmStart.lossyScale.x;
		float num = Vector3.Distance(this.FLWheel.LowerArmStart.position, this.FLWheel.LowerArmEnd.position);
		float num2 = (position2.z - this.FLWheel.LowerArmStart.localPosition.z) * x;
		position2.y = this.FLWheel.LowerArmStart.localPosition.y - Mathf.Sqrt(num * num - num2 * num2) / x;
		Vector3 a = this.FLWheel.LowerArmStart.parent.TransformPoint(position2);
		if (!float.IsNaN(a.x))
		{
			Quaternion rotation = Quaternion.LookRotation(a - this.FLWheel.LowerArmStart.position, base.transform.up);
			this.FLWheel.LowerArmStart.rotation = rotation;
		}
		if (this.FLWheel.LowerArmStart.localEulerAngles.x < 25f && this.FLWheel.LowerArmStart.localEulerAngles.y > 90f && this.FLWheel.LowerArmStart.localEulerAngles.z > 90f)
		{
			this.FLWheel.LowerArmStart.localEulerAngles = new Vector3(25f, 180f, 180f);
		}
		this.FLWheel.Knuckle.position = this.FLWheel.KnucklePosition.position;
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, SteerAngle);
		this.FLWheel.UpperArmStart.LookAt(this.FLWheel.UpperArmTarget, base.transform.up);
		this.FRWheel.TieRod.LookAt(this.SteeringRack, base.transform.up);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		x = this.FRWheel.LowerArmStart.lossyScale.x;
		this.FRWheel.TieRod.LookAt(this.SteeringRack, base.transform.up);
		position2 = this.FRWheel.LowerArmStart.parent.InverseTransformPoint(position);
		position2.x = this.FRWheel.LowerArmStart.localPosition.x;
		position2.z -= 0.36f;
		num = Vector3.Distance(this.FRWheel.LowerArmStart.position, this.FRWheel.LowerArmEnd.position);
		num2 = (position2.z - this.FRWheel.LowerArmStart.localPosition.z) * x;
		position2.y = this.FRWheel.LowerArmStart.localPosition.y + Mathf.Sqrt(num * num - num2 * num2) / x;
		a = this.FRWheel.LowerArmStart.parent.TransformPoint(position2);
		if (!float.IsNaN(a.x))
		{
			Quaternion rotation2 = Quaternion.LookRotation(a - this.FRWheel.LowerArmStart.position, base.transform.up);
			this.FRWheel.LowerArmStart.rotation = rotation2;
		}
		if (this.FRWheel.LowerArmStart.localEulerAngles.x > 335f && this.FRWheel.LowerArmStart.localEulerAngles.y > 90f)
		{
			this.FRWheel.LowerArmStart.localEulerAngles = new Vector3(335f, 180f, 0f);
		}
		this.FRWheel.Knuckle.position = this.FRWheel.KnucklePosition.position;
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, SteerAngle);
		this.FRWheel.UpperArmStart.LookAt(this.FRWheel.UpperArmTarget, base.transform.up);
		this.DoShocks();
	}

	private void FixedUpdate()
	{
		if (this.NoWheelColliders)
		{
			return;
		}
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			this.NoWheelColliders = true;
			return;
		}
		float num = 0f;
		if (this.carController != null)
		{
			num = this.carController.Steering;
			this.SteeringRack.localPosition = new Vector3(0f, Mathf.Lerp(-0.03f, 0.03f, (-num + this.carController.maxSteeringAngle) / (this.carController.maxSteeringAngle * 2f)), 0f);
		}
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.up, perFrameRotation, Space.World);
		this.FLWheel.TieRod.LookAt(this.SteeringRack, base.transform.up);
		Vector3 position = this.FLWheel.LowerArmStart.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition());
		position.x = this.FLWheel.LowerArmStart.localPosition.x;
		position.z -= 0.36f;
		float x = this.FLWheel.LowerArmStart.lossyScale.x;
		float num2 = Vector3.Distance(this.FLWheel.LowerArmStart.position, this.FLWheel.LowerArmEnd.position);
		float num3 = (position.z - this.FLWheel.LowerArmStart.localPosition.z) * x;
		position.y = this.FLWheel.LowerArmStart.localPosition.y - Mathf.Sqrt(num2 * num2 - num3 * num3) / x;
		Vector3 a = this.FLWheel.LowerArmStart.parent.TransformPoint(position);
		if (!float.IsNaN(a.x))
		{
			Quaternion rotation = Quaternion.LookRotation(a - this.FLWheel.LowerArmStart.position, base.transform.up);
			this.FLWheel.LowerArmStart.rotation = rotation;
		}
		if (this.FLWheel.LowerArmStart.localEulerAngles.x < 25f && this.FLWheel.LowerArmStart.localEulerAngles.y > 90f && this.FLWheel.LowerArmStart.localEulerAngles.z > 90f)
		{
			this.FLWheel.LowerArmStart.localEulerAngles = new Vector3(25f, 180f, 180f);
		}
		this.FLWheel.Knuckle.position = this.FLWheel.KnucklePosition.position;
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, num);
		this.FLWheel.UpperArmStart.LookAt(this.FLWheel.UpperArmTarget, base.transform.up);
		this.FLWheel.WheelColliderHolder.localPosition = new Vector3(this.FLWheel.WheelColliderHolder.parent.InverseTransformPoint(this.WheelHolders[0].position).x, 0f, 0f);
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.up, perFrameRotation, Space.World);
		x = this.FRWheel.LowerArmStart.lossyScale.x;
		this.FRWheel.TieRod.LookAt(this.SteeringRack, base.transform.up);
		position = this.FRWheel.LowerArmStart.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition());
		position.x = this.FRWheel.LowerArmStart.localPosition.x;
		position.z -= 0.36f;
		num2 = Vector3.Distance(this.FRWheel.LowerArmStart.position, this.FRWheel.LowerArmEnd.position);
		num3 = (position.z - this.FRWheel.LowerArmStart.localPosition.z) * x;
		position.y = this.FRWheel.LowerArmStart.localPosition.y + Mathf.Sqrt(num2 * num2 - num3 * num3) / x;
		a = this.FRWheel.LowerArmStart.parent.TransformPoint(position);
		if (!float.IsNaN(a.x))
		{
			Quaternion rotation2 = Quaternion.LookRotation(a - this.FRWheel.LowerArmStart.position, base.transform.up);
			this.FRWheel.LowerArmStart.rotation = rotation2;
		}
		if (this.FRWheel.LowerArmStart.localEulerAngles.x > 335f && this.FRWheel.LowerArmStart.localEulerAngles.y > 90f)
		{
			this.FRWheel.LowerArmStart.localEulerAngles = new Vector3(335f, 180f, 0f);
		}
		this.FRWheel.Knuckle.position = this.FRWheel.KnucklePosition.position;
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(0f, 0f, num);
		this.FRWheel.UpperArmStart.LookAt(this.FRWheel.UpperArmTarget, base.transform.up);
		this.FRWheel.WheelColliderHolder.localPosition = new Vector3(this.FRWheel.WheelColliderHolder.parent.InverseTransformPoint(this.WheelHolders[1].position).x, 0f, 0f);
		this.DoShocks();
	}

	private CarController carController;

	public UTVFrontWheel FLWheel;

	public UTVFrontWheel FRWheel;

	public Transform SteeringRack;

	public UTVFrontSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
