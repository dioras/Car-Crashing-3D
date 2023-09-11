using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class ButtonHandler : MonoBehaviour
	{
		private void OnEnable()
		{
			this.m_Button = base.GetComponent<Button>();
		}

		public void SetDownState()
		{
			if (this.m_Button != null && !this.m_Button.interactable)
			{
				return;
			}
			CrossPlatformInputManager.SetButtonDown(this.Name);
		}

		public void SetUpState()
		{
			if (this.m_Button != null && !this.m_Button.interactable)
			{
				return;
			}
			CrossPlatformInputManager.SetButtonUp(this.Name);
		}

		public void SetAxisPositiveState()
		{
			CrossPlatformInputManager.SetAxisPositive(this.Name);
		}

		public void SetAxisNeutralState()
		{
			CrossPlatformInputManager.SetAxisZero(this.Name);
		}

		public void SetAxisNegativeState()
		{
			CrossPlatformInputManager.SetAxisNegative(this.Name);
		}

		public void Update()
		{
		}

		private Button m_Button;

		public string Name;
	}
}
