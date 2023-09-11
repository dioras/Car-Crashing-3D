using System;

[Serializable]
public class MonsterControls : SuspensionControls
{
	public SuspensionValue AxisWidth = new SuspensionValue("Axis width", global::ValueType.Float, 0f, 0);

	public SuspensionValue RearSteering = new SuspensionValue("Rear steering", global::ValueType.Float, 0f, 0);
}
