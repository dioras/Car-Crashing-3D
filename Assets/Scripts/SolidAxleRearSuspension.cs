using System;
using CustomVP;
using UnityEngine;

public class SolidAxleRearSuspension : Suspension
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
			this.Controls.RearFrameOffset,
			this.Controls.RearSteering,
			this.Controls.ShocksGroup,
			this.Controls.ShocksHeight,
			this.Controls.ShocksOffset,
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

	private void Awake()
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
		this.DoWidth();
		this.DoSpringBrackets();
		this.DoShocksOffset();
		this.DoWheelColliderParameters();
		this.DoFramesWidth();
		this.DoLeafSprings();
		this.DoLeafSpringMountHeight();
		this.DoShocks();
		this.ChangeShocks();
		if (this.carController != null)
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
		if (this.RRWheel.Shocks == null || this.RRWheel.ShockDowns == null || this.RLWheel.ShockUps == null || this.RLWheel.ShockDowns == null)
		{
			return;
		}
		this.RRWheel.ShockUps.LookAt(this.RRWheel.ShockDowns, base.transform.right);
		this.RRWheel.ShockDowns.LookAt(this.RRWheel.ShockUps, -base.transform.right);
		this.RLWheel.ShockUps.LookAt(this.RLWheel.ShockDowns, -base.transform.right);
		this.RLWheel.ShockDowns.LookAt(this.RLWheel.ShockUps, base.transform.right);
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

	private void DoSpringBrackets()
	{
		this.RLWheel.SpringBracket.localEulerAngles = new Vector3((float)((this.Controls.SpringBracketsUpperMount.IntValue <= 0) ? 0 : 180), 0f, 0f);
		this.RRWheel.SpringBracket.localEulerAngles = new Vector3((float)((this.Controls.SpringBracketsUpperMount.IntValue <= 0) ? 0 : 180), 0f, 0f);
	}

	private void DoWidth()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null)
		{
			return;
		}
		this.RLWheel.Axle.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RRWheel.Axle.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RLWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.RRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
	}

	private void DoShocksOffset()
	{
		this.RLWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
		this.RRWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
	}

	private void DoFramesWidth()
	{
		this.RLWheel.Frame.localPosition = new Vector3(-this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RRWheel.Frame.localPosition = new Vector3(this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RLWheel.SpringBracket.localPosition = new Vector3(-this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RRWheel.SpringBracket.localPosition = new Vector3(this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RLWheel.FrontFrame.localPosition = new Vector3(0f, -this.Controls.FrontFrameOffset.FloatValue, 0f);
		this.RRWheel.FrontFrame.localPosition = new Vector3(0f, -this.Controls.FrontFrameOffset.FloatValue, 0f);
		this.RLWheel.RearFrame.localPosition = new Vector3(0f, this.Controls.RearFrameOffset.FloatValue, 0f);
		this.RRWheel.RearFrame.localPosition = new Vector3(0f, this.Controls.RearFrameOffset.FloatValue, 0f);
	}

	private void DoLeafSpringMountHeight()
	{
		this.RLWheel.FrontLeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.RRWheel.FrontLeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.RLWheel.RearLeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.RRWheel.RearLeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
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
		this.RLWheel.LeafSpringBone.position = this.RLWheel.LeafSpringPos.position;
		this.RRWheel.LeafSpringBone.position = this.RRWheel.LeafSpringPos.position;
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.RLWheel.BrakeDisk.Rotate(this.RLWheel.BrakeDisk.right, rpm, Space.World);
		this.RRWheel.BrakeDisk.Rotate(this.RRWheel.BrakeDisk.right, rpm, Space.World);
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RLWheel.Dummy.position = position;
		this.RLWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -SteerAngle * this.Controls.RearSteering.FloatValue);
		position = this.Raycasters[1].position - this.Raycasters[1].up * (this.Controls.Travel.FloatValue + 0.2f);
		if (Physics.Raycast(this.Raycasters[1].position, -this.Raycasters[1].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.RRWheel.Dummy.localPosition;
		localPosition.z = this.RRWheel.Dummy.parent.InverseTransformPoint(position).z;
		localPosition.x = this.RRWheel.Dummy.parent.InverseTransformPoint(position).x;
		this.RRWheel.Dummy.localPosition = localPosition;
		this.RRWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -SteerAngle * this.Controls.RearSteering.FloatValue);
		this.DriveshaftStart.Rotate(0f, rpm, 0f);
		Vector3 localPosition2 = this.RearAxleDummy.localPosition;
		localPosition2.z = this.RLWheel.Dummy.localPosition.z;
		this.RearAxleDummy.localPosition = localPosition2;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.forward);
		this.DriveshaftEnd.LookAt(this.DriveshaftTarget, this.DriveshaftStart.forward);
		if (this.TrackBarStart != null)
		{
			this.TrackBarEnd.LookAt(this.TrackBarStart, base.transform.forward);
			this.TrackBarStart.LookAt(this.TrackBarEnd, base.transform.forward);
		}
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
		this.RLWheel.BrakeDisk.Rotate(this.RLWheel.BrakeDisk.right, perFrameRotation, Space.World);
		Vector3 localPosition = this.RLWheel.Dummy.localPosition;
		localPosition.z = this.RLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).z;
		localPosition.x = this.RLWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).x;
		float num2 = this.Controls.AxisWidth.FloatValue + 0.03f;
		Vector3 from = -this.RLWheel.Dummy.right;
		Vector3 to = this.wheelColliders[1].GetVisualWheelPosition() - this.wheelColliders[0].GetVisualWheelPosition();
		float num3 = Vector3.SignedAngle(from, to, this.wheelColliders[0].transform.forward);
		float num4 = num2 * Mathf.Tan(num3 * 0.0174532924f);
		localPosition.z += num4;
		this.RLWheel.Dummy.localPosition = localPosition;
		this.RLWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -num * this.Controls.RearSteering.FloatValue);
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.RRWheel.BrakeDisk.Rotate(this.RRWheel.BrakeDisk.right, perFrameRotation, Space.World);
		localPosition = this.RRWheel.Dummy.localPosition;
		localPosition.z = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).z;
		localPosition.x = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).x;
		this.RRWheel.Dummy.localPosition = localPosition;
		this.RRWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -num * this.Controls.RearSteering.FloatValue);
		if (this.DriveshaftStart != null)
		{
			this.DriveshaftStart.Rotate(0f, perFrameRotation, 0f);
		}
		Vector3 localPosition2 = this.RearAxleDummy.localPosition;
		localPosition2.z = this.RLWheel.Dummy.localPosition.z;
		this.RearAxleDummy.localPosition = localPosition2;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.forward);
		this.DriveshaftEnd.LookAt(this.DriveshaftTarget, this.DriveshaftStart.forward);
		if (this.TrackBarStart != null)
		{
			this.TrackBarEnd.LookAt(this.TrackBarStart, base.transform.forward);
			this.TrackBarStart.LookAt(this.TrackBarEnd, base.transform.forward);
		}
		this.DoLeafSprings();
		this.DoShocks();
	}

	private CarController carController;

	public SolidAxleRearWheel RLWheel;

	public SolidAxleRearWheel RRWheel;

	public Transform RearAxleDummy;

	public Transform DriveshaftStart;

	public Transform DriveshaftEnd;

	public Transform DriveshaftTarget;

	public Transform TrackBarStart;

	public Transform TrackBarEnd;

	public SolidAxleRearSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
