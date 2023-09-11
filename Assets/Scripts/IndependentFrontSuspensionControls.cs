using System;

[Serializable]
public class IndependentFrontSuspensionControls : SuspensionControls
{
	public SuspensionValue AWD = new SuspensionValue("AWD", global::ValueType.Int, 0f, 0);

	public SuspensionValue AxisWidth = new SuspensionValue("Axis width", global::ValueType.Float, 0f, 0);

	public SuspensionValue PerchWidth = new SuspensionValue("Perch width", global::ValueType.Float, 0f, 0);

	public SuspensionValue PerchHeight = new SuspensionValue("Perch height", global::ValueType.Float, 0f, 0);
}
