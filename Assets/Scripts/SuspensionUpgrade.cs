using System;

public class SuspensionUpgrade
{
	public SuspensionUpgrade(string _suspensionName, int _stage, int _upgradeCost)
	{
		this.Stage = _stage;
		this.upgradeCost = _upgradeCost;
		this.SuspensionName = _suspensionName;
	}

	public int Stage;

	public int upgradeCost;

	public string SuspensionName;
}
