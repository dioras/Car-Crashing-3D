using System;
using CustomVP;
using UnityEngine;

public class Suspension : MonoBehaviour
{
	public virtual void UpdateSuspension(float SteerAngle, float WheelRadius, float rpm)
	{
	}

	public virtual float[] ExportData()
	{
		return null;
	}

	public virtual SuspensionValue[] GetControlValues()
	{
		return null;
	}

	public virtual void SetControlValues(SuspensionValue[] values)
	{
	}

	public virtual void OnValidate()
	{
	}

	public string SuspensionName;

	public int UpgradeStage;

	public bool DontLoadWheels;

	public bool DirtBikeWheels;

	public bool ATVWheels;

	[HideInInspector]
	public Side side;

	public WheelComponent[] wheelColliders;

	public Transform[] Raycasters;

	public Transform[] WheelHolders;
}
