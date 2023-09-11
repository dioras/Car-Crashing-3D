using System;
using UnityEngine;

public class DirtBikeRearSuspension : Suspension
{
	public override SuspensionValue[] GetControlValues()
	{
		return new SuspensionValue[]
		{
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

	public override void OnValidate()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.DoWheelColliderParameters();
		this.DoShocks();
		this.ChangeShocks();
		this.side = Side.Rear;
		if (this.wheelColliders[0] == null)
		{
			return;
		}
		this.wheelColliders[0].OnValidate();
	}

	private void DoShocks()
	{
		this.ShockDowns.LookAt(this.ShockUps, base.transform.right);
		this.ShockUps.LookAt(this.ShockDowns, base.transform.right);
	}

	private void ChangeShocks()
	{
		for (int i = 0; i < this.Shocks.Length; i++)
		{
			this.Shocks[i].gameObject.SetActive(i == this.Controls.ShocksGroup.IntValue);
		}
		this.ShockDowns.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
		this.ShockUps.localScale = new Vector3(this.Controls.ShocksSize.FloatValue, this.Controls.ShocksSize.FloatValue, 1f);
	}

	private void DoWheelColliderParameters()
	{
		if (this.wheelColliders[0] == null)
		{
			return;
		}
		this.wheelColliders[0].suspensionLength = this.Controls.Travel.FloatValue;
		this.wheelColliders[0].spring = this.Controls.Stiffness.FloatValue;
		this.wheelColliders[0].damper = this.Controls.Damping.FloatValue;
	}

	public override void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
		Vector3 position = this.Raycasters[0].position - this.Raycasters[0].up * (this.Controls.Travel.FloatValue + 0.2f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.Raycasters[0].position, -this.Raycasters[0].up, out raycastHit) && raycastHit.distance < this.Controls.Travel.FloatValue + WheelRadius + 0.2f)
		{
			position = raycastHit.point + new Vector3(0f, WheelRadius, 0f);
		}
		Vector3 localPosition = this.Dummy.transform.localPosition;
		localPosition.y = this.Dummy.parent.InverseTransformPoint(position).y;
		this.Dummy.localPosition = localPosition;
		this.Arm.LookAt(this.Dummy, this.Body.up);
		this.WheelHolders[0].Rotate(this.WheelHolders[0].right, rpm, Space.World);
		this.DoShocks();
	}

	private void FixedUpdate()
	{
		if (this.NoWheelColliders)
		{
			return;
		}
		if (this.wheelColliders[0] == null)
		{
			this.NoWheelColliders = true;
			return;
		}
		Vector3 localPosition = this.Dummy.transform.localPosition;
		localPosition.y = this.Dummy.parent.InverseTransformPoint(this.wheelColliders[0].GetVisualWheelPosition()).y;
		this.Dummy.localPosition = localPosition;
		this.Arm.LookAt(this.Dummy, this.Body.up);
		float perFrameRotation = this.wheelColliders[0].wheelCollider.perFrameRotation;
		this.WheelHolders[0].Rotate(this.WheelHolders[0].right, perFrameRotation, Space.World);
		this.DoShocks();
	}

	public Transform Dummy;

	public Transform Arm;

	public Transform Body;

	public Transform[] Shocks;

	public Transform ShockUps;

	public Transform ShockDowns;

	public DirtBikeFrontSuspensionControls Controls;

	private bool NoWheelColliders;

	private Vector3 previousPosition;

	private float previousUpdateTime;
}
