using System;
using CustomVP;
using UnityEngine;

[Serializable]
public class RouteGoal
{
	public RoutePayment GetPayment(int completionTime, int winchCount, int flipCount, float damageCount)
	{
		RoutePayment routePayment = new RoutePayment();
		if (this.RecordTime > 0L)
		{
			routePayment.AwardLevel = this.GetAwardLevelAchieved(completionTime, winchCount, flipCount, damageCount);
		}
		else if (this.Route.TrailblazerEligible)
		{
			routePayment.AwardLevel = AwardLevel.Gold;
			routePayment.Trailblazer = true;
			routePayment.TrailblazerGoldBonus = this.TrailblazerGoldBonus;
		}
		else
		{
			routePayment.AwardLevel = AwardLevel.Completion;
		}
		routePayment.Cash = this.BaseCashPayment + this.LevelUpCashIncrement * (int)routePayment.AwardLevel;
		routePayment.Gold = this.BaseGoldPayment + this.LevelUpGoldIncrement * (int)routePayment.AwardLevel;
		routePayment.XP = this.BaseXPPayment + this.LevelUpXPPayment * (int)routePayment.AwardLevel;
		return routePayment;
	}

	private AwardLevel GetAwardLevelAchieved(int completionTime, int winchCount, int flipCount, float damageCount)
	{
		AwardLevel result = AwardLevel.Completion;
		completionTime += winchCount * this.WinchPenalty * 100 + flipCount * this.FlipPenalty * 100;
		for (int i = 4; i >= 1; i--)
		{
			RouteGoalLimit limits = this.GetLimits((AwardLevel)i);
			if ((long)completionTime <= limits.TimeLimit)
			{
				result = (AwardLevel)i;
				break;
			}
		}
		return result;
	}

	public RouteGoalLimit GetLimits(AwardLevel awardLevel)
	{
		RouteGoalLimit routeGoalLimit = new RouteGoalLimit();
		routeGoalLimit.AwardLevel = awardLevel;
		routeGoalLimit.TimeLimit = this.RecordTime;
		if (awardLevel != AwardLevel.Gold)
		{
			if (awardLevel != AwardLevel.Silver)
			{
				if (awardLevel == AwardLevel.Copper)
				{
					routeGoalLimit.TimeLimit += (long)((float)routeGoalLimit.TimeLimit * ((float)this.CopperPercentage / 100f));
				}
			}
			else
			{
				routeGoalLimit.TimeLimit += (long)((float)routeGoalLimit.TimeLimit * ((float)this.SilverPercentage / 100f));
			}
		}
		else
		{
			routeGoalLimit.TimeLimit += (long)((float)routeGoalLimit.TimeLimit * ((float)this.GoldPercentage / 100f));
		}
		routeGoalLimit.TimeLimit = ((routeGoalLimit.TimeLimit >= 0L) ? routeGoalLimit.TimeLimit : 0L);
		routeGoalLimit.WinchLimit = Mathf.Max(routeGoalLimit.WinchLimit, 0);
		routeGoalLimit.FlipLimit = Mathf.Max(routeGoalLimit.FlipLimit, 0);
		routeGoalLimit.DamageLimit = Mathf.Max(routeGoalLimit.DamageLimit, 0f);
		return routeGoalLimit;
	}

	public static RouteGoal Default(long record, Route route = null, VehicleType type = VehicleType.ATV)
	{
		int num = 0;
		if (record > 0L)
		{
			if (record < 3000L)
			{
				num = -1000;
			}
			else if (record < 7500L)
			{
				num = -500;
			}
			if (record > 24000L)
			{
				num = 1000;
			}
			else if (record > 18000L)
			{
				num = 500;
			}
		}
		if (route != null && !route.TrailblazerEligible && record == 0L)
		{
			num = -500;
		}
		if (type == VehicleType.Crawler)
		{
			return new RouteGoal
			{
				RecordTime = record,
				BaseCashPayment = Mathf.Max(1500 + num, 750),
				BaseGoldPayment = 1,
				BaseXPPayment = 5,
				LevelUpCashIncrement = 150,
				LevelUpGoldIncrement = 1,
				LevelUpXPPayment = 1,
				TrailblazerGoldBonus = 10,
				Route = route
			};
		}
		if (type == VehicleType.Truck)
		{
			return new RouteGoal
			{
				RecordTime = record,
				BaseCashPayment = Mathf.Max(1200 + num, 750),
				BaseGoldPayment = 0,
				BaseXPPayment = 3,
				LevelUpCashIncrement = 100,
				LevelUpGoldIncrement = 1,
				LevelUpXPPayment = 1,
				TrailblazerGoldBonus = 10,
				Route = route
			};
		}
		if (type == VehicleType.SideBySide)
		{
			return new RouteGoal
			{
				RecordTime = record,
				BaseCashPayment = Mathf.Max(1100 + num, 500),
				BaseGoldPayment = 0,
				BaseXPPayment = 3,
				LevelUpCashIncrement = 75,
				LevelUpGoldIncrement = 0,
				LevelUpXPPayment = 1,
				TrailblazerGoldBonus = 5,
				Route = route
			};
		}
		return new RouteGoal
		{
			RecordTime = record,
			BaseCashPayment = Mathf.Max(750 + Mathf.Max(num, -500), 400),
			BaseGoldPayment = 0,
			BaseXPPayment = 3,
			LevelUpCashIncrement = 50,
			LevelUpGoldIncrement = 0,
			LevelUpXPPayment = 1,
			TrailblazerGoldBonus = 5,
			Route = route
		};
	}

	[Header("Awards")]
	public int BaseCashPayment;

	public int BaseGoldPayment;

	public int BaseXPPayment;

	public int TrailblazerGoldBonus;

	public int LevelUpCashIncrement;

	public int LevelUpGoldIncrement;

	public int LevelUpXPPayment;

	[Space(5f)]
	[Header("Constraints")]
	public long RecordTime;

	public Route Route;

	private long GoldPercentage = 20L;

	private long SilverPercentage = 40L;

	private long CopperPercentage = 100L;

	public int WinchPenalty = 10;

	public int FlipPenalty = 20;

	[Space(5f)]
	[Header("Vehicle")]
	public VehicleType VehicleType;
}
