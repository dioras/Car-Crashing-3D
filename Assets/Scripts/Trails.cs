using System;
using System.Collections.Generic;

public static class Trails
{
	static Trails()
	{
		Trails.trails.Add(new Trail(0, "Snakebit", "Map1NG"));
		Trails.trails.Add(new Trail(1, "Mudpit", "Map1NG"));
		Trails.trails.Add(new Trail(2, "Long Road", "Map1NG"));
		Trails.trails.Add(new Trail(3, "The Strip", "MapDesertNG"));
		Trails.trails.Add(new Trail(4, "Table Top", "MapDesertNG"));
		Trails.trails.Add(new Trail(5, "Baja", "MapDesertNG"));
	}

	public static Trail GetByID(int id)
	{
		return Trails.trails.Find((Trail t) => t.id == id);
	}

	public static List<Trail> trails = new List<Trail>();
}
