using System;
using System.Collections.Generic;
using CustomVP;

public static class PowerParts
{
	static PowerParts()
	{
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.EngineBlock, 0, 0, 0f, "Stock engine"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.EngineBlock, 1, 1000, 5f, "Bored engine block (more cubic inches!).\r\n +5% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.EngineBlock, 2, 2000, 10f, "Bored and stroked block.\r\n +10% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.EngineBlock, 3, 3000, 15f, "Big block swap.\r\n +15% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.EngineBlock, 4, 5000, 20f, "Blueprinted big block with all forged components.\r\n +20% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Head, 0, 0, 0f, "Stock heads"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Head, 1, 1000, 5f, "Hand ported steal heads.\r\n +5% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Head, 2, 1200, 10f, "CNC ported steal heads.\r\n +10% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Head, 3, 1500, 15f, "Large port aluminum heads.\r\n +15% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Head, 4, 2000, 20f, "Flow bench dyno and tweaked aluminum heads.\r\n +20% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Grip, 0, 0, 0f, "Stock tires"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Grip, 1, 1500, 5f, "Sticky tires.\r\n+5% Grip!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Grip, 2, 1500, 10f, "Tuned suspension geometry.\r\n+10% Grip!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Grip, 3, 2000, 15f, "Tuned vehicle COG.\r\n+15% Grip!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Grip, 4, 2000, 20f, "Tuned chassis stiffness.\r\n+20% Grip!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Valvetrain, 0, 0, 0f, "Stock valvetrain"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Valvetrain, 1, 1000, 5f, "Clean-up of intake and exhaust ports.\r\n+5% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Valvetrain, 2, 1000, 10f, "Polished rotating assembly.\r\n+10% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Valvetrain, 3, 1600, 15f, "Lightweight pistons.\r\n+15% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Valvetrain, 4, 1800, 20f, "Lightweight crankshaft.\r\n+20% power!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Weight, 0, 0, 0f, "Stock chassis"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Weight, 1, 1000, 5f, "Lightweight seats.\r\n-5% weight!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Weight, 2, 3000, 10f, "Lightweight interior components.\r\n-10% weight!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Weight, 3, 5000, 15f, "Lightweight drivetrain components.\r\n-15% weight!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Weight, 4, 8000, 20f, "Titanium chassis components.\r\n-20% weight!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Durability, 0, 0, 0f, "Stock fuel tank"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Durability, 1, 1000, 5f, "TITAN fuel tank and frame supports.\r\n+5% Durability!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Durability, 2, 1200, 10f, "TITAN fuel tank and upgraded differentials.\r\n+10% Durability!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Durability, 3, 1400, 15f, "TITAN fuel tank and upgraded cooling system.\r\n+15% Durability!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Durability, 4, 1600, 20f, "TITAN fuel tank and upgraded driveshafts.\r\n+20% Durability!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Truck, PowerPartType.Diesel, 3, 10000, 20f, "Swap to a diesel and ROLL COAL!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Truck, PowerPartType.Diesel, 4, 10000, 20f, "Swap to a diesel and ROLL COAL!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Gearbox, 0, 0, 0f, "Get manual transmission and control torque like a PRO!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Gearbox, 1, 10000, 0f, "Get manual transmission and control torque like a PRO!"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Ebrake, 0, 0, 0f, "E-brake not installed"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Ebrake, 1, 10000, 0f, "E-brake installed"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.TankTracks, 0, 0, 0f, "Tank tracks"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.TankTracks, 1, 10000, 0f, "Tank tracks"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Gearing, 0, 0, 0f, string.Empty));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Gearing, 1, 5000, 0f, string.Empty));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Gearing, 2, 5000, 0f, string.Empty));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Gearing, 3, 5000, 0f, string.Empty));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Gearing, 4, 5000, 0f, string.Empty));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Turbo, 0, 0, 0f, "No turbo"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Turbo, 1, 1000, 15f, "Turbo stage 1"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Turbo, 2, 1000, 17f, "Turbo stage 2"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Turbo, 3, 1600, 19f, "Turbo stage 3"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Turbo, 4, 1800, 21f, "Turbo stage 4"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Blower, 0, 0, 0f, "No blower"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Blower, 1, 1000, 15f, "Blower stage 1"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Blower, 2, 1000, 17f, "Blower stage 2"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Blower, 3, 1600, 19f, "Blower stage 3"));
		PowerParts.Parts.Add(new PowerPart(VehicleType.Any, PowerPartType.Blower, 4, 1800, 21f, "Blower stage 4"));
	}

	public static PowerPart GetPart(VehicleType _vehicleType, PowerPartType _partType, int _stage)
	{
		PowerPart powerPart = PowerParts.Parts.Find((PowerPart p) => p.partType == _partType && p.vehicleType == _vehicleType && p.Stage == _stage);
		if (powerPart == null)
		{
			powerPart = PowerParts.Parts.Find((PowerPart p) => p.partType == _partType && p.vehicleType == VehicleType.Any && p.Stage == _stage);
		}
		return powerPart;
	}

	public static List<PowerPart> Parts = new List<PowerPart>();
}
