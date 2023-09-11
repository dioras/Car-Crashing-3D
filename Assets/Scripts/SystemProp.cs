using System;
using UnityEngine;

public class SystemProp : Prop
{
	public override void Start()
	{
		base.Start();
		this.mainCamera = Camera.main;
	}

	public override void Update()
	{
		base.Update();
		if (this.text != null)
		{
			this.text.transform.rotation = Quaternion.LookRotation(this.text.transform.position - this.mainCamera.transform.position);
		}
	}

	public void RemoveEditorGizmos()
	{
		if (this.visualElementsParent != null)
		{
			UnityEngine.Object.Destroy(this.visualElementsParent);
		}
	}

	[Header("System prop parameters")]
	public Transform text;

	public GameObject visualElementsParent;

	private Camera mainCamera;
}
