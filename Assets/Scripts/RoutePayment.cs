using System;

[Serializable]
public class RoutePayment
{
	public string AwardLevelString()
	{
		string result = string.Empty;
		switch (this.AwardLevel)
		{
		case AwardLevel.Completion:
			result = "Completion";
			break;
		case AwardLevel.Copper:
			result = "Copper";
			break;
		case AwardLevel.Silver:
			result = "Silver";
			break;
		default:
			result = "Gold";
			break;
		}
		return result;
	}

	public AwardLevel AwardLevel;

	public int Cash;

	public int Gold;

	public int XP;

	public int CompletionCash;

	public int CompletionXP;

	public bool Trailblazer;

	public int TrailblazerGoldBonus;
}
