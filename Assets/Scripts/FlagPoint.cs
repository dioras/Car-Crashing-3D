using System;
using UnityEngine;

public class FlagPoint : MonoBehaviour
{
	private void Start()
	{
		this.CurrentMaterial = this.CurrentRay.GetComponent<MeshRenderer>().material;
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		UnityEngine.Debug.Log("Collided!");
		if (other.transform != null && other.transform.parent != null)
		{
			VehicleDataManager component = other.transform.parent.GetComponent<VehicleDataManager>();
			PhotonView component2 = other.transform.parent.GetComponent<PhotonView>();
			PhotonTransformView component3 = other.transform.parent.GetComponent<PhotonTransformView>();
			if (component != null && component2.isMine)
			{
				this.SwitchColor((component.Team != PunTeams.Team.blue) ? Color.red : Color.blue);
				if (component.Team == PunTeams.Team.blue)
				{
					component3.SendFlagCapturedBlue(this.FlagPointID);
				}
				if (component.Team == PunTeams.Team.red)
				{
					component3.SendFlagCapturedRed(this.FlagPointID);
				}
			}
		}
	}

	public Color CurrentColor
	{
		get
		{
			if (this.CurrentMaterial == null)
			{
				this.CurrentMaterial = this.CurrentRay.GetComponent<MeshRenderer>().material;
			}
			return this.CurrentMaterial.color;
		}
	}

	public void SwitchColor(Color color)
	{
		this.CurrentMaterial.color = color;
	}

	public int FlagPointID;

	public GameObject CurrentRay;

	private Material CurrentMaterial;
}
