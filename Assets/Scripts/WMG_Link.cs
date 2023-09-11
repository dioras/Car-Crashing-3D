using System;
using UnityEngine;

public class WMG_Link : WMG_GUI_Functions
{
	public void Setup(GameObject fromNode, GameObject toNode, int linkId, bool repos)
	{
		this.fromNode = fromNode;
		this.toNode = toNode;
		this.SetId(linkId);
		WMG_Node component = fromNode.GetComponent<WMG_Node>();
		WMG_Node component2 = toNode.GetComponent<WMG_Node>();
		base.name = string.Concat(new object[]
		{
			"WMG_Link_",
			component.id,
			"_",
			component2.id
		});
		if (repos)
		{
			this.Reposition();
		}
	}

	public void Reposition()
	{
		float num = base.getSpritePositionX(this.toNode) - base.getSpritePositionX(this.fromNode);
		float num2 = base.getSpritePositionY(this.toNode) - base.getSpritePositionY(this.fromNode);
		float num3 = Mathf.Atan2(num2, num) * 57.29578f + 90f;
		WMG_Node component = this.fromNode.GetComponent<WMG_Node>();
		WMG_Node component2 = this.toNode.GetComponent<WMG_Node>();
		this.SetNodeAngles(num3, component, component2);
		float num4 = component.radius + component2.radius;
		float num5 = Mathf.Sqrt(Mathf.Pow(num2, 2f) + Mathf.Pow(num, 2f)) - num4;
		if (num5 < 0f)
		{
			num5 = 0f;
		}
		float num6 = (component.radius - component2.radius) / 2f * Mathf.Cos(0.0174532924f * (num3 - 90f));
		float num7 = (component.radius - component2.radius) / 2f * Mathf.Sin(0.0174532924f * (num3 - 90f));
		float squareCircleOffsetLength = this.getSquareCircleOffsetLength(component, num3, true);
		float squareCircleOffsetLength2 = this.getSquareCircleOffsetLength(component2, num3, false);
		num5 = num5 - squareCircleOffsetLength - squareCircleOffsetLength2;
		float num8 = (squareCircleOffsetLength - squareCircleOffsetLength2) / 2f * Mathf.Cos(0.0174532924f * (num3 - 90f));
		float num9 = (squareCircleOffsetLength - squareCircleOffsetLength2) / 2f * Mathf.Sin(0.0174532924f * (num3 - 90f));
		if (this.weightIsLength)
		{
			this.weight = num5;
		}
		if (this.updateLabelWithLength && this.objectToLabel != null)
		{
			base.changeLabelText(this.objectToLabel, Mathf.Round(num5).ToString());
			this.objectToLabel.transform.localEulerAngles = new Vector3(0f, 0f, 360f - num3);
		}
		base.transform.localPosition = new Vector3(base.getSpriteFactorY2(this.objectToScale) * num + this.fromNode.transform.localPosition.x + num6 + num8, base.getSpriteFactorY2(this.objectToScale) * num2 + this.fromNode.transform.localPosition.y + num7 + num9, base.transform.localPosition.z);
		base.changeSpriteHeight(this.objectToScale, Mathf.RoundToInt(num5));
		base.transform.localEulerAngles = new Vector3(0f, 0f, num3);
	}

	public void SetId(int linkId)
	{
		this.id = linkId;
	}

	private void SetNodeAngles(float angle, WMG_Node fromN, WMG_Node toN)
	{
		for (int i = 0; i < fromN.numLinks; i++)
		{
			WMG_Link component = fromN.links[i].GetComponent<WMG_Link>();
			if (component.id == this.id)
			{
				fromN.linkAngles[i] = angle - 90f;
			}
		}
		for (int j = 0; j < toN.numLinks; j++)
		{
			WMG_Link component2 = toN.links[j].GetComponent<WMG_Link>();
			if (component2.id == this.id)
			{
				toN.linkAngles[j] = angle + 90f;
			}
		}
	}

	private float getSquareCircleOffsetLength(WMG_Node theNode, float angle, bool isFrom)
	{
		if (theNode.isSquare)
		{
			int squareCircleOffsetAngle = this.getSquareCircleOffsetAngle(angle, isFrom);
			float num = theNode.radius - theNode.radius * Mathf.Cos(0.0174532924f * (float)squareCircleOffsetAngle);
			float num2 = num * Mathf.Tan(0.0174532924f * (float)squareCircleOffsetAngle);
			return Mathf.Sqrt(num * num + num2 * num2);
		}
		return 0f;
	}

	private int getSquareCircleOffsetAngle(float angle, bool isFrom)
	{
		int num;
		if (isFrom)
		{
			num = (Mathf.RoundToInt(angle) - 90) % 90;
		}
		else
		{
			num = (Mathf.RoundToInt(angle) + 90) % 90;
		}
		if (Mathf.Abs(num) > 45)
		{
			if (num > 0)
			{
				num -= 2 * (num - 45);
			}
			else
			{
				num -= 2 * (num + 45);
			}
		}
		return num;
	}

	public int id;

	public GameObject fromNode;

	public GameObject toNode;

	public GameObject objectToScale;

	public GameObject objectToColor;

	public GameObject objectToLabel;

	public bool weightIsLength;

	public bool updateLabelWithLength;

	public bool isSelected;

	public bool wasSelected;

	public float weight;
}
