using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameMapMenu : MonoBehaviour
{
	private void Start()
	{
		this.menu.SetActive(false);
	}

	public void OpenMenu()
	{
		this.menu.SetActive(true);
		int num;
		if (LevelEditorTools.DidIVoteForMap(GameState.mapToDownload, out num))
		{
			this.upvoteButton.interactable = false;
			this.downvoteButton.interactable = false;
			this.upvoteButton.GetComponent<Image>().color = ((num != 1) ? this.deselectedVoteButtonColor : this.selectedVoteButtonColor);
			this.downvoteButton.GetComponent<Image>().color = ((num != -1) ? this.deselectedVoteButtonColor : this.selectedVoteButtonColor);
		}
		this.activeStar.SetActive(LevelEditorTools.IsMapInFavs(GameState.mapToDownload));
		this.inactiveStar.SetActive(!LevelEditorTools.IsMapInFavs(GameState.mapToDownload));
	}

	public void AddMapToFavs()
	{
		if (!LevelEditorTools.IsMapInFavs(GameState.mapToDownload))
		{
			LevelEditorTools.AddMapToFavs(GameState.mapToDownload);
		}
		else
		{
			LevelEditorTools.RemoveFromFavs(GameState.mapToDownload);
		}
		this.activeStar.SetActive(LevelEditorTools.IsMapInFavs(GameState.mapToDownload));
		this.inactiveStar.SetActive(!LevelEditorTools.IsMapInFavs(GameState.mapToDownload));
	}

	public void VoteForMap(bool up)
	{
		int num;
		if (LevelEditorTools.DidIVoteForMap(GameState.mapToDownload, out num))
		{
			return;
		}
		this.upvoteButton.interactable = false;
		this.downvoteButton.interactable = false;
		this.upvoteButton.GetComponent<Image>().color = ((!up) ? this.deselectedVoteButtonColor : this.selectedVoteButtonColor);
		this.downvoteButton.GetComponent<Image>().color = (up ? this.deselectedVoteButtonColor : this.selectedVoteButtonColor);
		LevelEditorTools.AddMapToVoted(GameState.mapToDownload, up);
		//WWW www = new WWW("http://offroadoutlaws.online/ChangeMapRating.php?ID=" + GameState.mapToDownload + "&amount=" + ((!up) ? "-1" : "1"));
	}

	public GameObject menu;

	public GameObject activeStar;

	public GameObject inactiveStar;

	public Button upvoteButton;

	public Button downvoteButton;

	public Color selectedVoteButtonColor;

	public Color deselectedVoteButtonColor;
}
