using System;

[Serializable]
public class IBeamFrontSuspensionControls : SuspensionControls
{
	public SuspensionValue AxisWidth = new SuspensionValue("Axis width", global::ValueType.Float, 0f, 0);

	public SuspensionValue PerchHeight = new SuspensionValue("Perch height", global::ValueType.Float, 0f, 0);

	public SuspensionValue PerchWidth = new SuspensionValue("Perch width", global::ValueType.Float, 0f, 0);

	public SuspensionValue TrailingArmMountsWidth = new SuspensionValue("Trailing arm mount width", global::ValueType.Float, 0f, 0);
}
