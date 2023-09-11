using System;
using CustomVP;
using UnityEngine;

public class SolidAxleDoubleRearSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
			this.Controls.AxisWidth,
			this.Controls.Damping,
			this.Controls.FramesWidth,
			this.Controls.LeafSpringMountHeight,
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
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null || this.wheelColliders[2] == null || this.wheelColliders[3] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
		this.wheelColliders[1].OnValidate();
		this.wheelColliders[2].OnValidate();
		this.wheelColliders[3].OnValidate();
	}

	private void DoShocks()
	{
		if (this.RRWheel.Shocks == null || this.RRWheel.ShockDowns == null || this.RLWheel.ShockUps == null || this.RLWheel.ShockDowns == null)
		{
			return;
		}
		if (this.RRRWheel.Shocks == null || this.RRRWheel.ShockDowns == null || this.RRLWheel.ShockUps == null || this.RRLWheel.ShockDowns == null)
		{
			return;
		}
		this.RRWheel.ShockUps.LookAt(this.RRWheel.ShockDowns, base.transform.right);
		this.RRWheel.ShockDowns.LookAt(this.RRWheel.ShockUps, -base.transform.right);
		this.RLWheel.ShockUps.LookAt(this.RLWheel.ShockDowns, -base.transform.right);
		this.RLWheel.ShockDowns.LookAt(this.RLWheel.ShockUps, base.transform.right);
		this.RRRWheel.ShockUps.LookAt(this.RRRWheel.ShockDowns, base.transform.right);
		this.RRRWheel.ShockDowns.LookAt(this.RRRWheel.ShockUps, -base.transform.right);
		this.RRLWheel.ShockUps.LookAt(this.RRLWheel.ShockDowns, -base.transform.right);
		this.RRLWheel.ShockDowns.LookAt(this.RRLWheel.ShockUps, base.transform.right);
		this.RLWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RLWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRLWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRLWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRRWheel.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.RRRWheel.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.RRWheel.Shocks.Length; i++)
		{
			this.RLWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.RRWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.RRLWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
			this.RRRWheel.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
	}

	private void DoSpringBrackets()
	{
		this.RLWheel.SpringBracket.localEulerAngles = new Vector3((float)((this.Controls.SpringBracketsUpperMount.IntValue <= 0) ? 0 : 180), 0f, 0f);
		this.RRWheel.SpringBracket.localEulerAngles = new Vector3((float)((this.Controls.SpringBracketsUpperMount.IntValue <= 0) ? 0 : 180), 0f, 0f);
		this.RRLWheel.SpringBracket.localEulerAngles = new Vector3((float)((this.Controls.SpringBracketsUpperMount.IntValue <= 0) ? 0 : 180), 0f, 0f);
		this.RRRWheel.SpringBracket.localEulerAngles = new Vector3((float)((this.Controls.SpringBracketsUpperMount.IntValue <= 0) ? 0 : 180), 0f, 0f);
	}

	private void DoWidth()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null || this.wheelColliders[2] == null || this.wheelColliders[3] == null)
		{
			return;
		}
		this.RLWheel.Axle.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RRWheel.Axle.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RLWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.RRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.RRLWheel.Axle.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RRRWheel.Axle.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue, 0f, 0f);
		this.RRLWheel.WheelColliderHolder.transform.localPosition = new Vector3(-this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
		this.RRRWheel.WheelColliderHolder.transform.localPosition = new Vector3(this.Controls.AxisWidth.FloatValue * base.transform.lossyScale.x, 0f, 0f);
	}

	private void DoShocksOffset()
	{
		this.RLWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
		this.RRWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
		this.RRLWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
		this.RRRWheel.ShockUps.localPosition = new Vector3(this.Controls.ShocksOffset.FloatValue, 0f, -this.Controls.ShocksHeight.FloatValue);
	}

	private void DoFramesWidth()
	{
		this.RLWheel.Frame.localPosition = new Vector3(-this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RRWheel.Frame.localPosition = new Vector3(this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RLWheel.SpringBracket.localPosition = new Vector3(-this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RRWheel.SpringBracket.localPosition = new Vector3(this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RRLWheel.Frame.localPosition = new Vector3(-this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RRRWheel.Frame.localPosition = new Vector3(this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RRLWheel.SpringBracket.localPosition = new Vector3(-this.Controls.FramesWidth.FloatValue, 0f, 0f);
		this.RRRWheel.SpringBracket.localPosition = new Vector3(this.Controls.FramesWidth.FloatValue, 0f, 0f);
	}

	private void DoLeafSpringMountHeight()
	{
		this.RLWheel.LeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.RRWheel.LeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.RRLWheel.LeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
		this.RRRWheel.LeafMount.localPosition = new Vector3(0f, 0f, this.Controls.LeafSpringMountHeight.FloatValue);
	}

	private void DoWheelColliderParameters()
	{
		if (this.wheelColliders[0] == null || this.wheelColliders[1] == null || this.wheelColliders[2] == null || this.wheelColliders[3] == null)
		{
			return;
		}
		WheelComponent wheelComponent = this.wheelColliders[0];
		float floatValue = this.Controls.Travel.FloatValue;
		this.wheelColliders[1].suspensionLength = floatValue;
		wheelComponent.suspensionLength = floatValue;
		this.wheelColliders[0].spring = (this.wheelColliders[1].spring = this.Controls.Stiffness.FloatValue);
		this.wheelColliders[0].damper = (this.wheelColliders[1].damper = this.Controls.Damping.FloatValue);
		WheelComponent wheelComponent2 = this.wheelColliders[2];
		floatValue = this.Controls.Travel.FloatValue;
		this.wheelColliders[3].suspensionLength = floatValue;
		wheelComponent2.suspensionLength = floatValue;
		this.wheelColliders[2].spring = (this.wheelColliders[3].spring = this.Controls.Stiffness.FloatValue);
		this.wheelColliders[2].damper = (this.wheelColliders[3].damper = this.Controls.Damping.FloatValue);
	}

	private void DoLeafSprings()
	{
		this.RLWheel.LeafSpringBone.position = this.RLWheel.LeafSpringPos.position;
		this.RRWheel.LeafSpringBone.position = this.RRWheel.LeafSpringPos.position;
		this.RRLWheel.LeafSpringBone.position = this.RRLWheel.LeafSpringPos.position;
		this.RRRWheel.LeafSpringBone.position = this.RRRWheel.LeafSpringPos.position;
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		this.RLWheel.BrakeDisk.Rotate(new Vector3(rpm, 0f, 0f));
		this.RRWheel.BrakeDisk.Rotate(new Vector3(rpm, 0f, 0f));
		this.RRLWheel.BrakeDisk.Rotate(new Vector3(rpm, 0f, 0f));
		this.RRRWheel.BrakeDisk.Rotate(new Vector3(rpm, 0f, 0f));
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
		position = this.Raycasters[2].position - this.Raycasters[2].up * this.Controls.Travel.FloatValue;
		if (Physics.Raycast(this.Raycasters[2].position, -this.Raycasters[2].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		this.RRLWheel.Dummy.position = position;
		this.RRLWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -SteerAngle * this.Controls.RearSteering.FloatValue);
		position = this.Raycasters[3].position - this.Raycasters[3].up * this.Controls.Travel.FloatValue;
		if (Physics.Raycast(this.Raycasters[3].position, -this.Raycasters[3].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		localPosition = this.RRRWheel.Dummy.localPosition;
		localPosition.z = this.RRRWheel.Dummy.parent.InverseTransformPoint(position).z;
		localPosition.x = this.RRRWheel.Dummy.parent.InverseTransformPoint(position).x;
		this.RRRWheel.Dummy.localPosition = localPosition;
		this.RRRWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -SteerAngle * this.Controls.RearSteering.FloatValue);
		this.DriveshaftStart.Rotate(0f, -rpm, 0f);
		this.DriveshaftEnd.LookAt(this.DriveshaftTarget, this.DriveshaftStart.forward);
		this.Driveshaft3rdStart.Rotate(0f, -rpm, 0f);
		this.Driveshaft3rdEnd.LookAt(this.DriveshaftConnectingEnd, this.Driveshaft3rdStart.forward);
		this.DriveshaftConnectingStart.Rotate(0f, -rpm, 0f);
		this.DriveshaftConnectingEnd.LookAt(this.Driveshaft3rdEnd, this.DriveshaftConnectingStart.forward);
		Vector3 localPosition2 = this.RearAxleDummy.localPosition;
		localPosition2.z = this.RLWheel.Dummy.localPosition.z;
		this.RearAxleDummy.localPosition = localPosition2;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.forward);
		localPosition2 = this.RearRearAxleDummy.localPosition;
		localPosition2.z = this.RRLWheel.Dummy.localPosition.z;
		this.RearRearAxleDummy.localPosition = localPosition2;
		this.RearRearAxleDummy.LookAt(this.RRRWheel.Dummy, base.transform.forward);
		this.DoLeafSprings();
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
			num = this.carController.Steering;
		}
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.RLWheel.BrakeDisk.Rotate(new Vector3(perFrameRotation, 0f, 0f));
		this.RLWheel.Dummy.position = this.wheelColliders[0].GetVisualWheelPosition();
		this.RLWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -num * this.Controls.RearSteering.FloatValue);
		perFrameRotation = this.wheelColliders[1].wheelCollider.perFrameRotation;
		this.RRWheel.BrakeDisk.Rotate(new Vector3(perFrameRotation, 0f, 0f));
		Vector3 localPosition = this.RRWheel.Dummy.localPosition;
		localPosition.z = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).z;
		localPosition.x = this.RRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[1].GetVisualWheelPosition()).x;
		this.RRWheel.Dummy.localPosition = localPosition;
		this.RRWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -num * this.Controls.RearSteering.FloatValue);
		perFrameRotation = this.wheelColliders[2].wheelCollider.perFrameRotation;
		this.RRLWheel.Dummy.position = this.wheelColliders[2].GetVisualWheelPosition();
		this.RRLWheel.BrakeDisk.Rotate(new Vector3(perFrameRotation, 0f, 0f));
		this.RRLWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -num * this.Controls.RearSteering.FloatValue);
		perFrameRotation = this.wheelColliders[3].wheelCollider.perFrameRotation;
		this.RRRWheel.BrakeDisk.Rotate(new Vector3(perFrameRotation, 0f, 0f));
		localPosition = this.RRRWheel.Dummy.localPosition;
		localPosition.z = this.RRRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[3].GetVisualWheelPosition()).z;
		localPosition.x = this.RRRWheel.Dummy.parent.InverseTransformPoint(this.wheelColliders[3].GetVisualWheelPosition()).x;
		this.RRRWheel.Dummy.localPosition = localPosition;
		this.RRRWheel.SteeringAxle.localEulerAngles = new Vector3(0f, 0f, -num * this.Controls.RearSteering.FloatValue);
		this.DriveshaftStart.Rotate(0f, perFrameRotation, 0f);
		this.DriveshaftEnd.LookAt(this.DriveshaftTarget, this.DriveshaftStart.forward);
		this.Driveshaft3rdStart.Rotate(0f, perFrameRotation, 0f);
		this.Driveshaft3rdEnd.LookAt(this.DriveshaftConnectingEnd, this.Driveshaft3rdStart.forward);
		this.DriveshaftConnectingStart.Rotate(0f, perFrameRotation, 0f);
		this.DriveshaftConnectingEnd.LookAt(this.Driveshaft3rdEnd, this.DriveshaftConnectingStart.forward);
		Vector3 localPosition2 = this.RearAxleDummy.localPosition;
		localPosition2.z = this.RLWheel.Dummy.localPosition.z;
		this.RearAxleDummy.localPosition = localPosition2;
		this.RearAxleDummy.LookAt(this.RRWheel.Dummy, base.transform.forward);
		localPosition2 = this.RearRearAxleDummy.localPosition;
		localPosition2.z = this.RRLWheel.Dummy.localPosition.z;
		this.RearRearAxleDummy.localPosition = localPosition2;
		this.RearRearAxleDummy.LookAt(this.RRRWheel.Dummy, base.transform.forward);
		this.DoLeafSprings();
		this.DoShocks();
	}

	private CarController carController;

	public SolidAxleRearWheel RLWheel;

	public SolidAxleRearWheel RRWheel;

	public SolidAxleRearWheel RRLWheel;

	public SolidAxleRearWheel RRRWheel;

	public Transform RearAxleDummy;

	public Transform RearRearAxleDummy;

	public Transform DriveshaftStart;

	public Transform DriveshaftEnd;

	public Transform Driveshaft3rdStart;

	public Transform Driveshaft3rdEnd;

	public Transform DriveshaftConnectingStart;

	public Transform DriveshaftConnectingEnd;

	public Transform DriveshaftTarget;

	public SolidAxleDoubleRearSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
