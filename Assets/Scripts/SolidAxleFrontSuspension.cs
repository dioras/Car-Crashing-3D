using System;
using CustomVP;
using UnityEngine;

public class SolidAxleFrontSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.FramesWidth,
			this.Controls.FrontFrameOffset,
			this.Controls.LeafSpringMountHeight,
			this.Controls.PerchHeight,
			this.Controls.PerchWidth,
			this.Controls.RearFrameOffset,
			this.Controls.ShocksGroup,
			this.Controls.ShocksSize,
			this.Controls.SpringBracketsUpperMount,
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
		this.side = Side.Front;
		this.DoWidth();
		this.DoSpringBrackets();
		this.DoFramesWidth();
		this.DoLeafSprings();
		this.DoLeafSpringMountHeight();
		this.DoPerchWidth();
		this.DoShocks();
		this.ChangeShocks();
		this.DoWheelColliderParameters();
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
	}

	private void DoShocks()
	{
		this.FRWheel.ShockUps.LookAt(this.FRWheel.ShockDowns, -base.transform.right);
		this.FRWheel.ShockDowns.LookAt(this.FRWheel.ShockUps, base.transform.right);
		this.FLWheel.ShockUps.LookAt(this.FLWheel.ShockDowns, base.transform.right);
		this.FLWheel.ShockDowns.LookAt(this.FLWheel.ShockUps, -base.transform.right);
		this.FLWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FLWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FRWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.FRWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.FLWheel.Shocks.Length; i++)
		{
			this.FLWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.FRWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
	}

	private void DoPerchWidth()
	{
		this.FLWheel.Perch.localPosition = new Vector3(this.Controls.PerchWidth.FloatValue, 0f, this.Controls.PerchHeight.FloatValue);
		this.FRWheel.Perch.localPosition = new Vector3(-this.Controls.PerchWidth.FloatValue, 0f, this.Controls.PerchHeight.FloatValue);
	}

	private void DoSpringBrackets()
	{
		this.FLWheel.SpringBracket.localEulerAngles = new Vector3((float)((this.Controls.SpringBracketsUpperMount.IntValue <= 0) ? 0 : 180), 0f, 0f);
		this.FRWheel.SpringBracket.localEulerAngles = new Vector3((float)((this.Controls.SpringBracketsUpperMount.IntValue <= 0) ? 0 : 180), 0f, 0f);
	}

	private void DoWidth()
	{
		this.FLWheel.Axle.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.FRWheel.Axle.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.FLWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.FRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
	}

	private void DoFramesWidth()
	{
		this.FLWheel.Frame.localPosition = new Vector3(this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.FRWheel.Frame.localPosition = new Vector3(-this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.FLWheel.SpringBracket.localPosition = new Vector3(this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.FRWheel.SpringBracket.localPosition = new Vector3(-this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.FLWheel.FrameFront.localPosition = new Vector3(0f, this.Controls.FrontFrameOffset.FloatValue, 0f);
		this.FRWheel.FrameFront.localPosition = new Vector3(0f, this.Controls.FrontFrameOffset.FloatValue, 0f);
		this.FLWheel.FrameRear.localPosition = new Vector3(0f, this.Controls.RearFrameOffset.FloatValue, 0f);
		this.FRWheel.FrameRear.localPosition = new Vector3(0f, this.Controls.RearFrameOffset.FloatValue, 0f);
	}

	private void DoLeafSpringMountHeight()
	{
		this.FLWheel.RearLeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.FRWheel.RearLeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.FLWheel.FrontLeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.FRWheel.FrontLeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
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

	private void DoLeafSprings()
	{
		this.FLWheel.LeafSpringBone.position = this.FLWheel.LeafSpringPos.position;
		this.FRWheel.LeafSpringBone.position = this.FRWheel.LeafSpringPos.position;
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.forward, -rpm, Space.World);
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.forward, -rpm, Space.World);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.x = this.FLWheel.Dummy.parent.InverseTransformPoint(position).x;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(position).z;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(-SteerAngle, 90f, 90f);
		this.FLWheel.TieRodBone.LookAt(this.FRWheel.TieRodBone, base.transform.forward);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.x = this.FRWheel.Dummy.parent.InverseTransformPoint(position).x;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(position).z;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(-SteerAngle, 90f, 90f);
		this.FRWheel.TieRodBone.LookAt(this.FLWheel.TieRodBone, base.transform.forward);
		this.DriveshaftStart.Rotate(-rpm, 0f, 0f);
		localPosition = this.FrontAxleDummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.localPosition.z;
		this.FrontAxleDummy.localPosition = localPosition;
		this.FrontAxleDummy.LookAt(this.FRWheel.Dummy, base.transform.forward);
		this.DriveshaftEnd.LookAt(this.DriveshaftTarget, this.DriveshaftStart.up);
		this.TieRodEnd.LookAt(this.TieRodStart, base.transform.forward);
		this.TrackBarEnd.LookAt(this.TrackBarStart, base.transform.forward);
		this.TrackBarStart.LookAt(this.TrackBarEnd, base.transform.forward);
		this.DoShocks();
		this.DoLeafSprings();
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
			num = this.carController.Steering;
		}
		if (!wheelColliders[0].wheelCollider) return;
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		Vector3 localPosition = this.FLWheel.Dummy.localPosition;
		localPosition.z = this.FLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).z;
		float num2 = this.Controls.AxisWidth.FloatValue + 0.03f;
		Vector3 forward = this.FLWheel.Dummy.forward;
		Vector3 to = this.wheelColliders[1].GetVisualWheelPosition() - this.wheelColliders[0].GetVisualWheelPosition();
		float num3 = Vector3.SignedAngle(forward, to, this.wheelColliders[0].transform.forward);
		float num4 = num2 * Mathf.Tan(num3 * 0.0174532924f);
		localPosition.z += num4;
		localPosition.x = this.FLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).x;
		this.FLWheel.Dummy.localPosition = localPosition;
		this.FLWheel.BrakeDisk.Rotate(this.FLWheel.BrakeDisk.forward, -perFrameRotation, Space.World);
		this.FLWheel.Knuckle.localEulerAngles = new Vector3(-num, 90f, 90f);
		this.FLWheel.TieRodBone.LookAt(this.FRWheel.TieRodBone, base.transform.forward);
		this.FLWheel.Driveshaft.Rotate(new Vector3(0f, 0f, -perFrameRotation));
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.FRWheel.BrakeDisk.Rotate(this.FRWheel.BrakeDisk.forward, -perFrameRotation, Space.World);
		localPosition = this.FRWheel.Dummy.localPosition;
		localPosition.z = this.FRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).z;
		localPosition.x = this.FRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).x;
		this.FRWheel.Dummy.localPosition = localPosition;
		this.FRWheel.Knuckle.localEulerAngles = new Vector3(-num, 90f, 90f);
		this.FRWheel.TieRodBone.LookAt(this.FLWheel.TieRodBone, base.transform.forward);
		this.FRWheel.Driveshaft.Rotate(new Vector3(0f, 0f, -perFrameRotation));
		this.DriveshaftStart.Rotate(perFrameRotation, 0f, 0f);
		Vector3 localPosition2 = this.FrontAxleDummy.localPosition;
		localPosition2.z = this.FLWheel.Dummy.localPosition.z;
		this.FrontAxleDummy.localPosition = localPosition2;
		this.FrontAxleDummy.LookAt(this.FRWheel.Dummy, base.transform.forward);
		this.TieRodEnd.LookAt(this.TieRodStart, base.transform.forward);
		if (this.carController != null)
		{
			this.SteeringRack.localPosition = new Vector3(0f, 0f, Mathf.Lerp(0.02f, -0.02f, (num + this.carController.maxSteeringAngle) / (this.carController.maxSteeringAngle * 2f)));
		}
		this.DriveshaftEnd.LookAt(this.DriveshaftTarget, this.DriveshaftStart.up);
		this.TrackBarEnd.LookAt(this.TrackBarStart, base.transform.forward);
		this.TrackBarStart.LookAt(this.TrackBarEnd, base.transform.forward);
		this.DoShocks();
		this.DoLeafSprings();
	}

	private CarController carController;

	public SolidAxleFrontWheel FLWheel;

	public SolidAxleFrontWheel FRWheel;

	public Transform FrontAxleDummy;

	public Transform DriveshaftStart;

	public Transform DriveshaftEnd;

	public Transform DriveshaftTarget;

	public Transform TieRodStart;

	public Transform TieRodEnd;

	public Transform SteeringRack;

	public Transform TrackBarStart;

	public Transform TrackBarEnd;

	private float Steering;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;

	public SolidAxleFrontSuspensionControls Controls;
}
