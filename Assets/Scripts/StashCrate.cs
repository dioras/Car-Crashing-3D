using System;
using UnityEngine;

public class StashCrate : MonoBehaviour
{
	private void Start()
	{
		this.stashManager = StashManager.Instance;
		if (!(SceneManagerHelper.ActiveSceneName != "Menu"))
		{
			return;
		}
		this.stashManager.RegisterStashCrate(this);
		base.gameObject.SetActive(false);
		if (this.IsMissingParts)
		{
			return;
		}
		if (this.MissingPartsLabels != null)
		{
			this.MissingPartsLabels.SetActive(false);
		}
		if (this.Size != CrateSize.Vehicle)
		{
			this.RegularLabels = base.transform.Find("Regular").gameObject;
			this.LockboxLabels = base.transform.Find("Explosive").gameObject;
			this.Explosion = base.transform.Find("Explosion").GetComponent<ParticleSystem>();
		}
		if (this.Size != CrateSize.Vehicle && !this.enableTimer)
		{
			this.LockboxLabels.SetActive(false);
			this.RegularLabels.SetActive(true);
		}
	}

	private void Update()
	{
		if (this.enableTimer)
		{
			this.TimeLeft -= Time.deltaTime;
		}
		if (this.TimeLeft <= 0f && this.TimeLeft != -1f)
		{
			this.LockTimer = -1;
			this.TimeLeft = -1f;
			StashManager.Instance.LockboxDisabled(true);
			UnityEngine.Debug.Log("Lockbox expired!");
			this.Explosion.Play();
			base.Invoke("DisableMe", 0.4f);
			this.enableTimer = false;
		}
	}

	public void SetAsMissingParts()
	{
		this.RegularLabels = base.transform.Find("Regular").gameObject;
		this.LockboxLabels = base.transform.Find("Explosive").gameObject;
		this.MissingPartsLabels = base.transform.Find("MissingParts").gameObject;
		this.LockboxLabels.SetActive(false);
		this.RegularLabels.SetActive(false);
		if (this.MissingPartsLabels != null)
		{
			this.MissingPartsLabels.SetActive(true);
		}
		this.IsMissingParts = true;
	}

	public void DisableMe()
	{
		base.gameObject.SetActive(false);
	}

	public void SetLockTimer(int time)
	{
		this.LockTimer = time;
		this.TimeLeft = (float)time;
		this.enableTimer = true;
		this.Explosion = base.transform.Find("Explosion").GetComponent<ParticleSystem>();
		this.RegularLabels = base.transform.Find("Regular").gameObject;
		this.LockboxLabels = base.transform.Find("Explosive").gameObject;
		this.RegularLabels.SetActive(false);
		this.LockboxLabels.SetActive(true);
	}

	[ContextMenu("Pick up")]
	private void OnMouseDown()
	{
		if (SceneManagerHelper.ActiveSceneName == "Menu" || VehicleLoader.Instance.droneMode)
		{
			return;
		}
		if (this.stashManager != null)
		{
			this.stashManager.FoundStashCrate(this);
		}
	}

	public CrateSize Size;

	public int LockTimer = -1;

	public float TimeLeft = -1f;

	private StashManager stashManager;

	public GameObject RegularLabels;

	public GameObject LockboxLabels;

	public GameObject MissingPartsLabels;

	public ParticleSystem Explosion;

	public StashContent Content;

	public bool IsMissingParts;

	public int FieldFindID = 1;

	private bool enableTimer;
}
