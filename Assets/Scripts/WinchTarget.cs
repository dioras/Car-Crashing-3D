using System;
using UnityEngine;

public class WinchTarget : MonoBehaviour
{
	public SpriteRenderer spriteRenderer
	{
		get
		{
			if (this._spriteRenderer == null)
			{
				this._spriteRenderer = base.GetComponent<SpriteRenderer>();
			}
			return this._spriteRenderer;
		}
	}

	public PhotonTransformView tView
	{
		get
		{
			if (!this.lookedForTransformView)
			{
				this._tView = base.GetComponentInParent<PhotonTransformView>();
				this.lookedForTransformView = true;
			}
			return this._tView;
		}
	}

	private SpriteRenderer _spriteRenderer;

	private bool lookedForTransformView;

	private PhotonTransformView _tView;

	[HideInInspector]
	public bool DynamicTarget;
}
