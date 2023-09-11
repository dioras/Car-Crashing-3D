using System;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.UI
{
	public class UIFocus : MonoBehaviour
	{
		public void Start()
		{
			this.manager = GameObject.Find(this.CanvasName).GetComponent<UIWindowManager>();
			this.image = base.transform.Find("Panel/Header").GetComponent<Image>();
		}

		public void onPanelEnter()
		{
			this.manager.ChangeState(base.gameObject);
			Color color = this.image.color;
			color.a = 255f;
			this.image.color = color;
			base.transform.SetAsLastSibling();
			base.transform.SetAsFirstSibling();
			base.transform.SetSiblingIndex(-1);
			base.transform.GetSiblingIndex();
		}

		public string CanvasName = "Canvas";

		private UIWindowManager manager;

		private Image image;
	}
}
