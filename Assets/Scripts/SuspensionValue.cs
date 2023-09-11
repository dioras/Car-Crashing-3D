using System;

[Serializable]
public class SuspensionValue
{
	public SuspensionValue()
	{
	}

	public SuspensionValue(string name, global::ValueType type, float floatValue, int intValue)
	{
		this.ValueName = name;
		this.valueType = type;
		this.FloatValue = floatValue;
		this.IntValue = intValue;
	}

	public SuspensionValue DeepCopy()
	{
		return new SuspensionValue
		{
			FloatValue = this.FloatValue,
			IntValue = this.IntValue,
			ValueName = this.ValueName,
			valueType = this.valueType
		};
	}

	public void ReceiveValues(SuspensionValue receiveFrom)
	{
		this.IntValue = receiveFrom.IntValue;
		this.FloatValue = receiveFrom.FloatValue;
	}

	public string ValueName;

	public global::ValueType valueType;

	public float FloatValue;

	public int IntValue;
}
