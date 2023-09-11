using System;
using UnityEngine;

public class BoostCard
{
	public static BoostCard GetCard()
	{
		return new BoostCard
		{
			MultiplyAmount = (float)UnityEngine.Random.Range(2, 4),
			Duration = UnityEngine.Random.Range(6, 12) * 10,
			Type = (BoostCardType)UnityEngine.Random.Range(0, 7)
		};
	}

	public BoostCardType Type;

	public int Duration;

	public int ExtensionCost;

	public float MultiplyAmount;
}
