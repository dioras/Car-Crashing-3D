using System;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
	private void Start()
	{
		this.width = this.ColorPickerRect.rect.width;
		this.height = this.ColorPickerRect.rect.height;
	}

	public void HandlerMoved()
	{
		Color pixelBilinear = this.ColorImage.GetPixelBilinear(this.Handler.localPosition.x / this.width, this.Handler.localPosition.y / this.height);
		Vector3 localPosition = this.Handler.localPosition;
		localPosition.x = Mathf.Clamp(localPosition.x, 0f, this.width);
		localPosition.y = Mathf.Clamp(localPosition.y, 0f, this.height);
		this.Handler.localPosition = localPosition;
	}

	public Texture2D ColorImage;

	public RectTransform ColorPickerRect;

	public RectTransform Handler;

	public Color ResultColor;

	private float width;

	private float height;
}
