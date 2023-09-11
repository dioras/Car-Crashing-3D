using System;
using UnityEngine;

[Serializable]
public class PartGroup
{
	public PartGroupData returnData()
	{
		return new PartGroupData
		{
			GroupName = this.GroupName,
			InstalledPart = this.InstalledPart,
			color = this.color
		};
	}

	public PartGroup DeepCopy()
	{
		PartGroup partGroup = new PartGroup();
		partGroup.GroupName = this.GroupName;
		partGroup.partType = this.partType;
		partGroup.InstalledPart = this.InstalledPart;
		partGroup.color = this.color;
		partGroup.Parts = new GameObject[this.Parts.Length];
		for (int i = 0; i < this.Parts.Length; i++)
		{
			partGroup.Parts[i] = this.Parts[i];
		}
		return partGroup;
	}

	public void PaintPart()
	{
		if (!this.Paintable)
		{
			return;
		}
		if (this.color == Color.clear)
		{
			return;
		}
		foreach (GameObject gameObject in this.Parts)
		{
			if (gameObject != null)
			{
				MeshRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
				if (componentsInChildren.Length > 0)
				{
					foreach (MeshRenderer meshRenderer in componentsInChildren)
					{
						foreach (Material material in meshRenderer.materials)
						{
							material.SetColor("_BaseColor", this.color);
						}
					}
				}
			}
		}
	}

	public string GroupName;

	public PartType partType;

	public GameObject[] Parts;

	public bool Paintable;

	public Color color;

	public bool HideRearFenders;

	public bool dontBake;

	[HideInInspector]
	public int InstalledPart;
}
