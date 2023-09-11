using System;

public class Trail
{
	public Trail(int _id, string _name, string _map)
	{
		this.id = _id;
		this.TrailName = _name;
		this.MapName = _map;
	}

	public int id;

	public string TrailName;

	public string MapName;
}
