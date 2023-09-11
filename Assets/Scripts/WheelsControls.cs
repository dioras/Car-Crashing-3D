using System;
using UnityEngine;

[Serializable]
public class WheelsControls
{
	public SuspensionValue[] GetAllValues()
	{
		return new SuspensionValue[]
		{
			this.Rim,
			this.Tire,
			this.RimSize,
			this.WheelsRadius,
			this.WheelsWidth
		};
	}

	public void SetAllValues(SuspensionValue[] values)
	{
		foreach (SuspensionValue suspensionValue in this.GetAllValues())
		{
			foreach (SuspensionValue suspensionValue2 in values)
			{
				if (suspensionValue.ValueName == suspensionValue2.ValueName)
				{
					suspensionValue.ReceiveValues(suspensionValue2);
				}
			}
		}
	}

	public WheelsControls DeepCopy()
	{
		return new WheelsControls
		{
			DefaultWheelColliderRadius = this.DefaultWheelColliderRadius,
			Rim = this.Rim.DeepCopy(),
			RimSize = this.RimSize.DeepCopy(),
			Tire = this.Tire.DeepCopy(),
			WheelsRadius = this.WheelsRadius.DeepCopy(),
			WheelsWidth = this.WheelsWidth.DeepCopy()
		};
	}

	public void SetStock()
	{
		this.Rim.IntValue = 0;
		this.Tire.IntValue = 0;
		this.RimSize.FloatValue = 1f;
		this.WheelsRadius.FloatValue = 1f;
		this.WheelsWidth.FloatValue = 1f;
	}

	public void SetRandom(Suspension suspension)
	{
		if (suspension.DontLoadWheels)
		{
			return;
		}
		SuspensionControlLimit limit = SuspensionControlLimits.getLimit(suspension.gameObject.name, "Rim");
		SuspensionControlLimit limit2 = SuspensionControlLimits.getLimit(suspension.gameObject.name, "Tire");
		SuspensionControlLimit limit3 = SuspensionControlLimits.getLimit(suspension.gameObject.name, "Rim size");
		SuspensionControlLimit limit4 = SuspensionControlLimits.getLimit(suspension.gameObject.name, "Wheels radius");
		SuspensionControlLimit limit5 = SuspensionControlLimits.getLimit(suspension.gameObject.name, "Wheels width");
		this.Rim.IntValue = UnityEngine.Random.Range(0, limit.iMax);
		this.Tire.IntValue = UnityEngine.Random.Range(0, limit2.iMax);
		float max = 1f + (limit3.fMax - 1f) / 5f * (float)(this.Stage + 1);
		float min = 1f - (1f - limit3.fMin) / 5f * (float)(this.Stage + 1);
		if (limit.iMax > 0)
		{
			this.RimSize.FloatValue = UnityEngine.Random.Range(min, max);
		}
		float max2 = 1f + (limit4.fMax - 1f) / (float)(5 - this.Stage);
		float min2 = 1f - (1f - limit4.fMin) / (float)(5 - this.Stage);
		this.WheelsRadius.FloatValue = UnityEngine.Random.Range(min2, max2);
		float max3 = 1f + (limit5.fMax - 1f) / (float)(5 - this.Stage);
		float min3 = 1f - (1f - limit5.fMin) / (float)(5 - this.Stage);
		this.WheelsWidth.FloatValue = UnityEngine.Random.Range(min3, max3);
	}

	public float DefaultWheelColliderRadius = 0.4f;

	public int Stage;

	public bool TankTracks;

	public float TankTracksWheelCollidersRadius = 1.4f;

	public bool duallyTires;

	public SuspensionValue Rim = new SuspensionValue("Rim", global::ValueType.Int, 0f, 0);

	public SuspensionValue Tire = new SuspensionValue("Tire", global::ValueType.Int, 0f, 0);

	public SuspensionValue RimSize = new SuspensionValue("Rim size", global::ValueType.Float, 1f, 0);

	public SuspensionValue WheelsRadius = new SuspensionValue("Wheels radius", global::ValueType.Float, 1f, 0);

	public SuspensionValue WheelsWidth = new SuspensionValue("Wheels width", global::ValueType.Float, 1f, 0);
}
