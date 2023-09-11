using System;

[Serializable]
public class DragATVRearSuspensionControls : SuspensionControls
{
	public SuspensionValue RearAxleOffset = new SuspensionValue("Rear axle offset", global::ValueType.Float, 0f, 0);

	public SuspensionValue AxisWidth = new SuspensionValue("Axis width", global::ValueType.Float, 0f, 0);

	public SuspensionValue ShocksUpsHeight = new SuspensionValue("Shock ups height", global::ValueType.Float, 0f, 0);
}
