using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
	[RequireComponent(typeof(Text))]
	public class TextButtonTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		public void Awake()
		{
			this._text = base.GetComponent<Text>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			this._text.color = this.HoverColor;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			this._text.color = this.NormalColor;
		}

		private Text _text;

		public Color NormalColor = Color.white;

		public Color HoverColor = Color.black;
	}
}
