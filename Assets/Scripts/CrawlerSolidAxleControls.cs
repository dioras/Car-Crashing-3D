using System;

[Serializable]
public class CrawlerSolidAxleControls : SuspensionControls
{
	public SuspensionValue AxisWidth = new SuspensionValue("Axis width", global::ValueType.Float, 0f, 0);

	public SuspensionValue AxleType = new SuspensionValue("Axle type", global::ValueType.Int, 0f, 0);

	public SuspensionValue BrakeType = new SuspensionValue("Brake type", global::ValueType.Int, 0f, 0);

	public SuspensionValue RearSteering = new SuspensionValue("Rear steering", global::ValueType.Float, 0f, 0);
}
