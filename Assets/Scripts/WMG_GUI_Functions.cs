using System;
using UnityEngine;
using UnityEngine.UI;

public class WMG_GUI_Functions : WMG_Text_Functions
{
	public void SetActive(GameObject obj, bool state)
	{
		obj.SetActive(state);
	}

	public bool activeInHierarchy(GameObject obj)
	{
		return obj.activeInHierarchy;
	}

	public void SetActiveAnchoredSprite(GameObject obj, bool state)
	{
		this.SetActive(obj, state);
	}

	public void SetActiveImage(GameObject obj, bool state)
	{
		obj.GetComponent<Image>().enabled = state;
	}

	public Texture2D getTexture(GameObject obj)
	{
		return (Texture2D)obj.GetComponent<Image>().mainTexture;
	}

	public void setTexture(GameObject obj, Sprite sprite)
	{
		obj.GetComponent<Image>().sprite = sprite;
	}

	public void changeSpriteFill(GameObject obj, float fill)
	{
		Image component = obj.GetComponent<Image>();
		component.fillAmount = fill;
	}

	public void changeRadialSpriteRotation(GameObject obj, Vector3 newRot)
	{
		obj.transform.localEulerAngles = newRot;
	}

	public void changeSpriteColor(GameObject obj, Color aColor)
	{
		Graphic component = obj.GetComponent<Graphic>();
		component.color = aColor;
	}

	public void changeSpriteWidth(GameObject obj, int aWidth)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		if (component == null)
		{
			return;
		}
		component.sizeDelta = new Vector2((float)aWidth, component.rect.height);
	}

	public void changeSpriteHeight(GameObject obj, int aHeight)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		if (component == null)
		{
			return;
		}
		component.sizeDelta = new Vector2(component.rect.width, (float)aHeight);
	}

	public void setTextureMaterial(GameObject obj, Material aMat)
	{
		Image component = obj.GetComponent<Image>();
		component.material = new Material(aMat);
	}

	public Material getTextureMaterial(GameObject obj)
	{
		Image component = obj.GetComponent<Image>();
		if (component == null)
		{
			return null;
		}
		return component.material;
	}

	public void changeSpriteSize(GameObject obj, int aWidth, int aHeight)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		if (component == null)
		{
			return;
		}
		component.sizeDelta = new Vector2((float)aWidth, (float)aHeight);
	}

	public void changeSpriteSizeFloat(GameObject obj, float aWidth, float aHeight)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		if (component == null)
		{
			return;
		}
		component.sizeDelta = new Vector2(aWidth, aHeight);
	}

	public Vector2 getSpriteSize(GameObject obj)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		return component.sizeDelta;
	}

	public void changeBarWidthHeight(GameObject obj, int aWidth, int aHeight)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		if (component == null)
		{
			return;
		}
		component.sizeDelta = new Vector2((float)aWidth, (float)aHeight);
	}

	public float getSpriteWidth(GameObject obj)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		return component.rect.width;
	}

	public float getSpriteHeight(GameObject obj)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		return component.rect.height;
	}

	public void forceUpdateUI()
	{
		Canvas.ForceUpdateCanvases();
	}

	public void setAnchor(GameObject go, Vector2 anchor, Vector2 pivot, Vector2 anchoredPosition)
	{
		RectTransform component = go.GetComponent<RectTransform>();
		component.pivot = pivot;
		component.anchorMin = anchor;
		component.anchorMax = anchor;
		component.anchoredPosition = anchoredPosition;
	}

	public void stretchToParent(GameObject go)
	{
		RectTransform component = go.GetComponent<RectTransform>();
		component.anchorMin = Vector2.zero;
		component.anchorMax = Vector2.one;
		component.sizeDelta = Vector2.zero;
	}

	public bool rectIntersectRect(GameObject r1, GameObject r2)
	{
		RectTransform component = r1.GetComponent<RectTransform>();
		Vector3[] array = new Vector3[4];
		component.GetWorldCorners(array);
		RectTransform component2 = r2.GetComponent<RectTransform>();
		Vector3[] array2 = new Vector3[4];
		component2.GetWorldCorners(array2);
		return array[1].x <= array2[3].x && array2[1].x <= array[3].x && array[1].y >= array2[3].y && array2[1].y >= array[3].y;
	}

	public void getRectDiffs(GameObject child, GameObject container, ref Vector2 xDif, ref Vector2 yDif)
	{
		RectTransform component = child.GetComponent<RectTransform>();
		Vector3[] array = new Vector3[4];
		component.GetWorldCorners(array);
		RectTransform component2 = container.GetComponent<RectTransform>();
		Vector3[] array2 = new Vector3[4];
		component2.GetWorldCorners(array2);
		Vector2 vector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
		Vector2 vector2 = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
		Vector2 vector3 = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
		Vector2 vector4 = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
		Graphic component3 = component.GetComponent<Graphic>();
		this.getMinMaxFromCorners(ref vector, ref vector2, array, (!(component3 == null)) ? component3.canvas : null);
		this.getMinMaxFromCorners(ref vector3, ref vector4, array2, (!(component3 == null)) ? component3.canvas : null);
		float num = (!(component3 == null)) ? ((!(component3.canvas == null)) ? component3.canvas.scaleFactor : 1f) : 1f;
		xDif = new Vector2((vector.x - vector3.x) / num, (vector4.x - vector2.x) / num);
		yDif = new Vector2((vector.y - vector3.y) / num, (vector4.y - vector2.y) / num);
	}

	private void getMinMaxFromCorners(ref Vector2 min, ref Vector2 max, Vector3[] corners, Canvas canvas)
	{
		Camera cam = (!(canvas == null)) ? canvas.worldCamera : null;
		if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
		{
			cam = null;
		}
		for (int i = 0; i < 4; i++)
		{
			Vector3 vector = RectTransformUtility.WorldToScreenPoint(cam, corners[i]);
			if (vector.x < min.x)
			{
				min = new Vector2(vector.x, min.y);
			}
			if (vector.y < min.y)
			{
				min = new Vector2(min.x, vector.y);
			}
			if (vector.x > max.x)
			{
				max = new Vector2(vector.x, max.y);
			}
			if (vector.y > max.y)
			{
				max = new Vector2(max.x, vector.y);
			}
		}
	}

	public float getSpritePositionX(GameObject obj)
	{
		return obj.transform.localPosition.x;
	}

	public float getSpritePositionY(GameObject obj)
	{
		return obj.transform.localPosition.y;
	}

	public float getSpriteFactorY2(GameObject obj)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		return 1f - component.pivot.y;
	}

	public Vector3 getPositionRelativeTransform(GameObject obj, GameObject relative)
	{
		return relative.transform.InverseTransformPoint(obj.transform.TransformPoint(Vector3.zero));
	}

	public void changePositionByRelativeTransform(GameObject obj, GameObject relative, Vector2 delta)
	{
		obj.transform.position = relative.transform.TransformPoint(this.getPositionRelativeTransform(obj, relative) + new Vector3(delta.x, delta.y, 0f));
	}

	public void changeSpritePositionTo(GameObject obj, Vector3 newPos)
	{
		obj.transform.localPosition = new Vector3(newPos.x, newPos.y, newPos.z);
	}

	public void changeSpritePositionToX(GameObject obj, float newPos)
	{
		Vector3 localPosition = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(newPos, localPosition.y, localPosition.z);
	}

	public void changeSpritePositionToY(GameObject obj, float newPos)
	{
		Vector3 localPosition = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(localPosition.x, newPos, localPosition.z);
	}

	public Vector2 getChangeSpritePositionTo(GameObject obj, Vector2 newPos)
	{
		return new Vector2(newPos.x, newPos.y);
	}

	public void changeSpritePositionRelativeToObjBy(GameObject obj, GameObject relObj, Vector3 changeAmt)
	{
		Vector3 localPosition = relObj.transform.localPosition;
		obj.transform.localPosition = new Vector3(localPosition.x + changeAmt.x, localPosition.y + changeAmt.y, localPosition.z + changeAmt.z);
	}

	public void changeSpritePositionRelativeToObjByX(GameObject obj, GameObject relObj, float changeAmt)
	{
		Vector3 localPosition = relObj.transform.localPosition;
		Vector3 localPosition2 = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(localPosition.x + changeAmt, localPosition2.y, localPosition2.z);
	}

	public void changeSpritePositionRelativeToObjByY(GameObject obj, GameObject relObj, float changeAmt)
	{
		Vector3 localPosition = relObj.transform.localPosition;
		Vector3 localPosition2 = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(localPosition2.x, localPosition.y + changeAmt, localPosition2.z);
	}

	public Vector2 getSpritePivot(GameObject obj)
	{
		RectTransform component = obj.GetComponent<RectTransform>();
		return component.pivot;
	}

	public void changeSpriteParent(GameObject child, GameObject parent)
	{
		child.transform.SetParent(parent.transform, false);
	}

	public void bringSpriteToFront(GameObject obj)
	{
		obj.transform.SetAsLastSibling();
	}

	public void sendSpriteToBack(GameObject obj)
	{
		obj.transform.SetAsFirstSibling();
	}

	public string getDropdownSelection(GameObject obj)
	{
		return null;
	}

	public void setDropdownSelection(GameObject obj, string newval)
	{
	}

	public void addDropdownItem(GameObject obj, string item)
	{
	}

	public void deleteDropdownItem(GameObject obj)
	{
	}

	public void setDropdownIndex(GameObject obj, int index)
	{
	}

	public void setButtonColor(Color aColor, GameObject obj)
	{
	}

	public bool getToggle(GameObject obj)
	{
		return false;
	}

	public void setToggle(GameObject obj, bool state)
	{
	}

	public float getSliderVal(GameObject obj)
	{
		return 0f;
	}

	public void setSliderVal(GameObject obj, float val)
	{
	}

	public void showControl(GameObject obj)
	{
		this.SetActive(obj, true);
	}

	public void hideControl(GameObject obj)
	{
		this.SetActive(obj, false);
	}

	public bool getControlVisibility(GameObject obj)
	{
		return this.activeInHierarchy(obj);
	}
}
