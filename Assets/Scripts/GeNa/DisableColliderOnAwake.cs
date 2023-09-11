using System;
using UnityEngine;

namespace GeNa
{
	public class DisableColliderOnAwake : MonoBehaviour
	{
		private void Awake()
		{
			SphereCollider component = base.GetComponent<SphereCollider>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
	}
}
