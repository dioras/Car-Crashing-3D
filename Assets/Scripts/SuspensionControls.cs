using System;

[Serializable]
public class SuspensionControls
{
	public SuspensionValue Travel = new SuspensionValue("Travel", global::ValueType.Float, 0.3f, 0);

	public SuspensionValue Stiffness = new SuspensionValue("Stiffness", global::ValueType.Float, 20000f, 0);

	public SuspensionValue Damping = new SuspensionValue("Damping", global::ValueType.Float, 1000f, 0);

	public SuspensionValue ShocksGroup = new SuspensionValue("Shocks", global::ValueType.Int, 0f, 0);

	public SuspensionValue ShocksSize = new SuspensionValue("Shocks size", global::ValueType.Float, 1f, 0);
}
