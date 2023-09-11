using System;

[Serializable]
public class AssetRearSuspensionControls : SuspensionControls
{
	public SuspensionValue ShowArms = new SuspensionValue("Show arms", global::ValueType.Int, 0f, 1);

	public SuspensionValue RearSteering = new SuspensionValue("Rear steering", global::ValueType.Float, 0f, 0);

	public SuspensionValue AxisWidth = new SuspensionValue("Axis width", global::ValueType.Float, 0f, 0);

	public SuspensionValue ShocksOffset = new SuspensionValue("Shocks offset", global::ValueType.Float, 0f, 0);

	public SuspensionValue ShocksHeight = new SuspensionValue("Shocks height", global::ValueType.Float, 0f, 0);

	public SuspensionValue MiddleFrameWidth = new SuspensionValue("Middle frame width", global::ValueType.Float, 0f, 0);

	public SuspensionValue AxleType = new SuspensionValue("Axle type", global::ValueType.Int, 0f, 0);

	public SuspensionValue BrakeType = new SuspensionValue("Brake type", global::ValueType.Int, 0f, 0);
}
