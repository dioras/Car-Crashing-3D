using System;
using UnityEngine;

public class WMG_EnumFlagAttribute : PropertyAttribute
{
	public WMG_EnumFlagAttribute()
	{
	}

	public WMG_EnumFlagAttribute(string name)
	{
		this.enumName = name;
	}

	public string enumName;
}
