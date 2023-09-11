using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
	private void Start()
	{
		this.slider = base.GetComponent<Slider>();
		this.UpdateValueDisplay();
	}

	public void IncreaseValue()
	{
		if (this.slider == null)
		{
			return;
		}
		this.slider.value += this.step;
	}

	public void DecreaseValue()
	{
		if (this.slider == null)
		{
			return;
		}
		this.slider.value -= this.step;
	}

	public void UpdateValueDisplay()
	{
		if (this.slider == null || this.valueDisplayText == null)
		{
			return;
		}
		string text = (Mathf.Round(this.slider.value * Mathf.Pow(10f, (float)this.digitsAfterPoint)) / Mathf.Pow(10f, (float)this.digitsAfterPoint)).ToString();
		this.valueDisplayText.text = text;
	}

	private Slider slider;

	public float step;

	public Text valueDisplayText;

	public int digitsAfterPoint = 1;
}
