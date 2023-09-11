using System;
using CustomVP;

public class BodyPart
{
	public BodyPart(VehicleType _vehicleType, PartType _partType, string _partName, int _partCost)
	{
		this.vehicleType = _vehicleType;
		this.partType = _partType;
		this.partName = _partName;
		this.partCost = _partCost;
	}

	public VehicleType vehicleType;

	public PartType partType;

	public string partName;

	public int partCost;
}
