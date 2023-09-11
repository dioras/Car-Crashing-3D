using System;

[Serializable]
public class UTVFrontSuspensionControls : SuspensionControls
{
	public SuspensionValue AxisWidth = new SuspensionValue("Axis width", global::ValueType.Float, 0f, 0);
}
