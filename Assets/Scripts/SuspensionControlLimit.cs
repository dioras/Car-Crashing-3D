using System;

public class SuspensionControlLimit
{
	public SuspensionControlLimit(string suspensionName, string name, float fmin, float fmax, float fdef, int imin, int imax, int idef, bool modifiableByPlayer)
	{
		this.SuspensionName = suspensionName;
		this.ValueName = name;
		this.fMin = fmin;
		this.fMax = fmax;
		this.fDef = fdef;
		this.iMin = imin;
		this.iMax = imax;
		this.iDef = idef;
		this.ModifiableByPlayer = modifiableByPlayer;
	}

	public string SuspensionName;

	public string ValueName;

	public float fMin;

	public float fMax;

	public float fDef;

	public int iMin;

	public int iMax;

	public int iDef;

	public bool ModifiableByPlayer;
}
