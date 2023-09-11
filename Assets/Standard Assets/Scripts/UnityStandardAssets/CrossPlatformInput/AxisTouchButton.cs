using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class AxisTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		private void OnEnable()
		{
			if (!CrossPlatformInputManager.AxisExists(this.axisName))
			{
				Debug.Log("I am here...");
				this.m_Axis = new CrossPlatformInputManager.VirtualAxis(this.axisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_Axis);
			}
			else
			{
				Debug.Log("====  I am here...");

				this.m_Axis = CrossPlatformInputManager.VirtualAxisReference(this.axisName);
			}
			
			this.FindPairedButton();
		}

		public void UpdateValue()
		{
			float value = this.AxisSlider.value;
			this.m_Axis.Update(value);
			this.ControlSlider.value = this.AxisSlider.value;
		}

		private void FindPairedButton()
		{
			Debug.Log("Finding Paired Button...");
			AxisTouchButton[] array = UnityEngine.Object.FindObjectsOfType(typeof(AxisTouchButton)) as AxisTouchButton[];
			Debug.Log("Lentgh of array for AxisTouch BUtton is:" + array.Length);
			if (array != null)
			{
				
				for (int i = 0; i < array.Length; i++)
				{
					Debug.Log("Assigning Button..");

					if (array[i].axisName == this.axisName && array[i] != this)
					{
						this.m_PairedWith = array[i];
					}
				}
			}
		}

		private void OnDisable()
		{
			this.m_Axis.Remove();
		}

		public void OnPointerDown(PointerEventData data)
		{
			
			Debug.Log("On Pointer Down....");
			if (this.AxisSlider != null)
			{
				return;
			}
			if (this.m_PairedWith == null)
			{
				this.FindPairedButton();
			}
			this.m_Axis.Update(this.axisValue);
		}

		public void OnPointerUp(PointerEventData data)
		{
			Debug.Log("On Pointer Up....");

			if (this.AxisSlider != null)
			{
				this.AxisSlider.value = 0f;
			}
			else
			{
				this.m_Axis.Update(Mathf.MoveTowards(this.m_Axis.GetValue, 0f, this.responseSpeed * Time.deltaTime));
			}
		}

		public string axisName = "Horizontal";

		public float axisValue = 1f;

		public float responseSpeed = 3f;

		public float returnToCentreSpeed = 3f;

		public Slider AxisSlider;

		public Slider ControlSlider;

		private AxisTouchButton m_PairedWith;

		private CrossPlatformInputManager.VirtualAxis m_Axis;
	}
}
