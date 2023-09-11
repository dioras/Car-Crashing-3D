using System;
using UnityEngine;
using UnityEngine.UI;

public class WMG_Text_Functions : MonoBehaviour
{
	public void changeLabelText(GameObject obj, string aText)
	{
		Text component = obj.GetComponent<Text>();
		component.text = aText;
	}

	public void changeLabelFontSize(GameObject obj, int newFontSize)
	{
		Text component = obj.GetComponent<Text>();
		component.fontSize = newFontSize;
	}

	public Vector2 getTextSize(GameObject obj)
	{
		Text component = obj.GetComponent<Text>();
		return new Vector2(component.preferredWidth, component.preferredHeight);
	}

	public void changeSpritePivot(GameObject obj, WMG_Text_Functions.WMGpivotTypes theType)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		Text component2 = obj.GetComponent<Text>();
		if (component == null)
		{
			return;
		}
		if (theType == WMG_Text_Functions.WMGpivotTypes.Bottom)
		{
			component.pivot = new Vector2(0.5f, 0f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.LowerCenter;
			}
		}
		else if (theType == WMG_Text_Functions.WMGpivotTypes.BottomLeft)
		{
			component.pivot = new Vector2(0f, 0f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.LowerLeft;
			}
		}
		else if (theType == WMG_Text_Functions.WMGpivotTypes.BottomRight)
		{
			component.pivot = new Vector2(1f, 0f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.LowerRight;
			}
		}
		else if (theType == WMG_Text_Functions.WMGpivotTypes.Center)
		{
			component.pivot = new Vector2(0.5f, 0.5f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.MiddleCenter;
			}
		}
		else if (theType == WMG_Text_Functions.WMGpivotTypes.Left)
		{
			component.pivot = new Vector2(0f, 0.5f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.MiddleLeft;
			}
		}
		else if (theType == WMG_Text_Functions.WMGpivotTypes.Right)
		{
			component.pivot = new Vector2(1f, 0.5f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.MiddleRight;
			}
		}
		else if (theType == WMG_Text_Functions.WMGpivotTypes.Top)
		{
			component.pivot = new Vector2(0.5f, 1f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.UpperCenter;
			}
		}
		else if (theType == WMG_Text_Functions.WMGpivotTypes.TopLeft)
		{
			component.pivot = new Vector2(0f, 1f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.UpperLeft;
			}
		}
		else if (theType == WMG_Text_Functions.WMGpivotTypes.TopRight)
		{
			component.pivot = new Vector2(1f, 1f);
			if (component2 != null)
			{
				component2.alignment = TextAnchor.UpperRight;
			}
		}
	}

	public enum WMGpivotTypes
	{
		Bottom,
		BottomLeft,
		BottomRight,
		Center,
		Left,
		Right,
		Top,
		TopLeft,
		TopRight
	}
}
