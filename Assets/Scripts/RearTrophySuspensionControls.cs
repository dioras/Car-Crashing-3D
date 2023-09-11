using System;

[Serializable]
public class RearTrophySuspensionControls : SuspensionControls
{
	public SuspensionValue RearSteering = new SuspensionValue("Rear steering", global::ValueType.Float, 0f, 0);

	public SuspensionValue AxisWidth = new SuspensionValue("Axis width", global::ValueType.Float, 0f, 0);

	public SuspensionValue ShocksOffset = new SuspensionValue("Shocks offset", global::ValueType.Float, 0f, 0);

	public SuspensionValue ShocksTravel = new SuspensionValue("Shocks height", global::ValueType.Float, 0f, 0);

	public SuspensionValue TrailingArmsOffset = new SuspensionValue("Trailing arms offset", global::ValueType.Float, 0f, 0);

	public SuspensionValue TrailingArmsHeight = new SuspensionValue("Trailing arms height", global::ValueType.Float, 0f, 0);
}
