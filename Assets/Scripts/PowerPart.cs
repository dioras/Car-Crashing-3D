using System;
using CustomVP;

public class PowerPart
{
	public PowerPart(VehicleType _vehicleType, PowerPartType _partType, int _stage, int _partCost, float _incrementPercentage, string _description)
	{
		this.vehicleType = _vehicleType;
		this.partType = _partType;
		this.Stage = _stage;
		this.partCost = _partCost;
		this.IncrementPercantage = _incrementPercentage;
		this.Description = _description;
	}

	public VehicleType vehicleType;

	public PowerPartType partType;

	public int Stage;

	public int partCost;

	public float IncrementPercantage;

	public string Description;
}
