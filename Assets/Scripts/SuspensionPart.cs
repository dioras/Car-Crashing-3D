using System;
using CustomVP;

public class SuspensionPart
{
	public SuspensionPart(VehicleType _vehicleType, string _partName, string _displayedName, string _partDescription, int _partCost)
	{
		this.vehicleType = _vehicleType;
		this.partName = _partName;
		this.partDescription = _partDescription;
		this.partCost = _partCost;
		this.displayedName = _displayedName;
	}

	public VehicleType vehicleType;

	public string partName;

	public string displayedName;

	public string partDescription;

	public int partCost;
}
