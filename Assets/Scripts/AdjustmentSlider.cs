using System;
using UnityEngine;
using UnityEngine.UI;

public class AdjustmentSlider : MonoBehaviour
{
	private string GetDecimalPlaces()
	{
		if (this.slider.wholeNumbers)
		{
			return "F0";
		}
		if (this.maxClamp <= 1f)
		{
			return "F2";
		}
		if (this.maxClamp > 1f && this.maxClamp <= 10f)
		{
			return "F1";
		}
		if (this.maxClamp > 10f)
		{
			return "F0";
		}
		return "F2";
	}

	public void SliderValueChanged()
	{
		if (!this.SliderInitialized)
		{
			return;
		}
		if (!this.slider.wholeNumbers)
		{
			if (this.slider.value > this.maxClamp)
			{
				this.slider.value = this.maxClamp;
				return;
			}
			if (this.slider.value < this.minClamp)
			{
				this.slider.value = this.minClamp;
				return;
			}
			if (this.SnapToInterval)
			{
				this.slider.value = Mathf.Round(this.slider.value / this.Interval) * this.Interval;
			}
		}
		this.ValueNameText.text = this.ValueName + ": " + this.slider.value.ToString(this.GetDecimalPlaces());
	}

	public void SetupFloatValue(string valueName, float MinValue, float MaxValue, float MinClamp, float MaxClamp, float CurrentValue)
	{
		this.SliderInitialized = false;
		this.slider.wholeNumbers = false;
		this.ValueName = valueName;
		this.slider.minValue = MinValue;
		this.slider.maxValue = MaxValue;
		this.slider.value = CurrentValue;
		this.minClamp = MinClamp;
		this.maxClamp = MaxClamp;
		this.MinClampImage.fillAmount = Mathf.InverseLerp(this.slider.minValue, this.slider.maxValue, MinClamp);
		this.MaxClampImage.fillAmount = 1f - Mathf.InverseLerp(this.slider.minValue, this.slider.maxValue, MaxClamp);
		this.ValueNameText.text = this.ValueName + ": " + this.slider.value.ToString(this.GetDecimalPlaces());
		this.SliderInitialized = true;
	}

	public void SetupIntValue(string valueName, int MinValue, int MaxValue, int MinClamp, int MaxClamp, int CurrentValue)
	{
		this.SliderInitialized = false;
		this.slider.wholeNumbers = true;
		this.ValueName = valueName;
		this.slider.minValue = (float)MinValue;
		this.slider.maxValue = (float)MaxValue;
		this.slider.value = (float)CurrentValue;
		this.minClamp = (float)MinClamp;
		this.maxClamp = (float)MaxClamp;
		this.MinClampImage.fillAmount = 0f;
		this.MaxClampImage.fillAmount = 0f;
		this.ValueNameText.text = this.ValueName + ": " + this.slider.value.ToString(this.GetDecimalPlaces());
		this.SliderInitialized = true;
	}

	public Text ValueNameText;

	public Slider slider;

	public Image MinClampImage;

	public Image MaxClampImage;

	public bool SnapToInterval;

	public float Interval;

	private string ValueName;

	private bool SliderInitialized;

	private float minClamp;

	private float maxClamp;
}
