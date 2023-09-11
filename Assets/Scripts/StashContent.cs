using System;
using System.Collections.Generic;

public class StashContent
{
	public static Dictionary<CratePartType, string> CratePartTypeList()
	{
		return new Dictionary<CratePartType, string>
		{
			{
				CratePartType.FrontAxle,
				"Front Axle"
			},
			{
				CratePartType.RearAxle,
				"Rear Axle"
			},
			{
				CratePartType.Engine,
				"Engine"
			},
			{
				CratePartType.Wheels,
				"Wheels"
			},
			{
				CratePartType.Seats,
				"Seats"
			},
			{
				CratePartType.Drivetrain,
				"Drivetrain"
			},
			{
				CratePartType.Tires,
				"Tires"
			},
			{
				CratePartType.SteeringRack,
				"Steering Rack"
			},
			{
				CratePartType.WindowGlass,
				"Window Glass"
			},
			{
				CratePartType.Transmission,
				"Transmission"
			}
		};
	}

	public int CashAmount;

	public int GoldAmount;

	public BoostCard BoostCard;

	public CratePartType MissingPart;
}
