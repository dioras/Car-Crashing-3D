using System;

[Serializable]
public class StatsData
{
	public string Dump()
	{
		return this.Money.ToString() + this.Gold.ToString() + this.XP.ToString() + this.IsMember.ToString() + this.HasUnlimitedFuel.ToString();
	}

	public int Money;

	public int Gold;

	public int XP;

	public bool IsMember;
	public bool HasUnlimitedFuel;

	public int SelectedTruckID;

	public int DynoRuns;
}
