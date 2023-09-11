using System;

public class VehicleStatus
{
	public VehicleStatus(float steeringAngle, float dirtiness, float wetness, float wheelsRPM)
	{
		this.SteeringAngle = steeringAngle;
		this.Dirtiness = dirtiness;
		this.Wetness = wetness;
		this.WheelsRPM = wheelsRPM;
	}

	public string Serialize()
	{
		return string.Concat(new string[]
		{
			VehicleStatus.ConvertFromFloat(this.SteeringAngle).ToString(),
			"|",
			VehicleStatus.ConvertFromFloat(this.Dirtiness).ToString(),
			"|",
			VehicleStatus.ConvertFromFloat(this.Wetness).ToString(),
			"|",
			VehicleStatus.ConvertFromFloat(this.WheelsRPM).ToString()
		});
	}

	public static VehicleStatus DeSerialize(string data)
	{
		VehicleStatus result = new VehicleStatus(0f, 0f, 0f, 0f);
		string[] array = data.Split(new char[]
		{
			'|'
		});
		if (array.Length == 4)
		{
			result = new VehicleStatus(VehicleStatus.ConvertToFloat(int.Parse(array[0])), VehicleStatus.ConvertToFloat(int.Parse(array[1])), VehicleStatus.ConvertToFloat(int.Parse(array[2])), VehicleStatus.ConvertToFloat(int.Parse(array[3])));
		}
		return result;
	}

	private static int ConvertFromFloat(float value)
	{
		return (int)(value * 100f);
	}

	private static float ConvertToFloat(int value)
	{
		return (float)value / 100f;
	}

	public float SteeringAngle;

	public float Dirtiness;

	public float Wetness;

	public float WheelsRPM;
}
