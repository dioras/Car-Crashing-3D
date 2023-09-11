using System;

public class Map
{
	public Map(string name, bool isCurrent)
	{
		this.Name = name;
		this.IsCurrent = isCurrent;
	}

	public string Name;

	public bool IsCurrent;
}
