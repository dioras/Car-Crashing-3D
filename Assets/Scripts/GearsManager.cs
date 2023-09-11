using System;

public static class GearsManager
{
	public static float[] DefaultGears
	{
		get
		{
			return new float[]
			{
				3f,
				1.4f,
				0.9f,
				0.7f,
				0.55f
			};
		}
	}

	public static float[] GearsMinLimits
	{
		get
		{
			return new float[]
			{
				0.1f,
				0.1f,
				0.1f,
				0.1f,
				0.1f
			};
		}
	}

	public static float[] GearsMaxLimits
	{
		get
		{
			return new float[]
			{
				4f,
				4f,
				4f,
				4f,
				4f
			};
		}
	}

	public static float DefaultLowGear
	{
		get
		{
			return 2f;
		}
	}

	public static float LowGearMinLimit
	{
		get
		{
			return 0.1f;
		}
	}

	public static float LowGearMaxLimit
	{
		get
		{
			return 4f;
		}
	}

	public static float TopGear
	{
		get
		{
			return 9f;
		}
	}

	public static float GetDefaultGear(int GearID)
	{
		if (GearID == -1)
		{
			return GearsManager.DefaultLowGear;
		}
		if (GearID < GearsManager.DefaultGears.Length)
		{
			return GearsManager.DefaultGears[GearID];
		}
		return GearsManager.DefaultGears[GearsManager.DefaultGears.Length - 1];
	}

	public static float GetMinLimit(int GearID)
	{
		if (GearID == -1)
		{
			return GearsManager.LowGearMinLimit;
		}
		if (GearID < GearsManager.GearsMinLimits.Length)
		{
			return GearsManager.GearsMinLimits[GearID];
		}
		return GearsManager.GearsMinLimits[GearsManager.GearsMinLimits.Length - 1];
	}

	public static float GetMaxLimit(int GearID)
	{
		if (GearID == -1)
		{
			return GearsManager.LowGearMaxLimit;
		}
		if (GearID < GearsManager.GearsMaxLimits.Length)
		{
			return GearsManager.GearsMaxLimits[GearID];
		}
		return GearsManager.GearsMaxLimits[GearsManager.GearsMaxLimits.Length - 1];
	}
}
