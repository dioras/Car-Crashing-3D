using System;
using UnityEngine;
using UnityEngine.UI;

public class MapElement : MonoBehaviour
{
	public void ToggleStar(bool fav)
	{
		this.inactiveStar.SetActive(!fav);
		this.activeStar.SetActive(fav);
	}

	public void ToggleFavorite(bool fav)
	{
		CommunityMapsMenu componentInParent = base.GetComponentInParent<CommunityMapsMenu>();
		if (componentInParent == null)
		{
			return;
		}
		if (fav)
		{
			componentInParent.AddMapToFavs(this.mapFileName);
		}
		else
		{
			componentInParent.RemoveFromFavs(this.mapFileName);
		}
		this.ToggleStar(fav);
	}

	public void SetMapPic(Texture2D tex)
	{
		this.mapPicImage.sprite = Sprite.Create(tex, new Rect(0f, 0f, (float)tex.width, (float)tex.height), new Vector2(0f, 0f));
		this.previewLoadingMessage.SetActive(false);
	}

	public void ToggleSelection(bool selected, bool hidden)
	{
		this.hiddenMap = hidden;
		this.mainTabImage.color = ((!selected) ? ((!this.hiddenMap) ? this.deselectedMapColor : this.hiddenMapColor) : this.selectedMapColor);
		this.selection = selected;
	}

	public void ToggleSelection(bool selected)
	{
		this.mainTabImage.color = ((!selected) ? ((!this.hiddenMap) ? this.deselectedMapColor : this.hiddenMapColor) : this.selectedMapColor);
		this.selection = selected;
	}

	public void SelectMyMap()
	{
		CommunityMapsMenu componentInParent = base.GetComponentInParent<CommunityMapsMenu>();
		if (componentInParent == null)
		{
			return;
		}
		componentInParent.SelectMap(this.mapFileName);
	}

	[HideInInspector]
	public string mapFileName;

	[HideInInspector]
	public int rating;

	public Text mapNameText;

	public Text mapDescriptionText;

	public Text mapRatingText;

	public Text mapAuthorText;

	public GameObject inactiveStar;

	public GameObject activeStar;

	public Image mainTabImage;

	public Color selectedMapColor;

	public Color deselectedMapColor;

	public Color hiddenMapColor;

	public Image mapPicImage;

	public GameObject previewLoadingMessage;

	private bool hiddenMap;

	private bool selection;
}
