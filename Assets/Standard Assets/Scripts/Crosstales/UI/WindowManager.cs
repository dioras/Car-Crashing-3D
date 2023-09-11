using System;
using UnityEngine;

namespace Crosstales.UI
{
	public class WindowManager : MonoBehaviour
	{
		public void Start()
		{
			this.panel = base.transform.Find("Panel").gameObject;
			this.startPos = base.transform.position;
			this.ClosePanel();
		}

		public void Update()
		{
			this.centerPos = new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f);
			if (this.open && this.openProgress < 1f)
			{
				this.openProgress += this.Speed * Time.deltaTime;
				base.transform.position = Vector3.Lerp(this.lerpPos, this.centerPos, this.openProgress);
			}
			else if (this.close)
			{
				if (this.closeProgress < 1f)
				{
					this.closeProgress += this.Speed * Time.deltaTime;
					base.transform.position = Vector3.Lerp(this.lerpPos, this.startPos, this.closeProgress);
				}
				else
				{
					this.panel.SetActive(false);
				}
			}
		}

		public void SwitchPanel()
		{
			if (this.open)
			{
				this.ClosePanel();
			}
			else
			{
				this.OpenPanel();
			}
		}

		public void OpenPanel()
		{
			this.panel.SetActive(true);
			this.Focus = base.gameObject.GetComponent<UIFocus>();
			this.Focus.onPanelEnter();
			this.lerpPos = base.transform.position;
			this.open = true;
			this.close = false;
			this.openProgress = 0f;
		}

		public void ClosePanel()
		{
			this.lerpPos = base.transform.position;
			this.open = false;
			this.close = true;
			this.closeProgress = 0f;
		}

		[Tooltip("Window movement speed (default: 3).")]
		public float Speed = 3f;

		private UIFocus Focus;

		private bool open;

		private bool close;

		private Vector3 startPos;

		private Vector3 centerPos;

		private Vector3 lerpPos;

		private float openProgress;

		private float closeProgress;

		private GameObject panel;
	}
}
