using System;
using UnityEngine;

[Serializable]
public class Wrap
{
	public Wrap(int _id, Vector4 _coords, Color _color)
	{
		this.ID = _id;
		this.Coords = _coords;
		this.color = _color;
	}

	public Wrap()
	{
	}

	public int ID;

	public Vector4 Coords;

	public Color color;
}
