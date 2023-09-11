using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeNa
{
	[Serializable]
	public class Prototype
	{
		public Prototype()
		{
		}

		public Prototype(Prototype src)
		{
			this.m_name = src.m_name;
			this.m_active = src.m_active;
			this.m_size = src.m_size;
			this.m_extents = src.m_extents;
			this.m_boundsBorder = src.m_boundsBorder;
			this.m_constrainWithinMaskedBounds = src.m_constrainWithinMaskedBounds;
			this.m_invertMaskedAlpha = src.m_invertMaskedAlpha;
			this.m_scaleOnMaskedAlpha = src.m_scaleOnMaskedAlpha;
			this.m_scaleOnMaskedAlphaMin = src.m_scaleOnMaskedAlphaMin;
			this.m_scaleOnMaskedAlphaMax = src.m_scaleOnMaskedAlphaMax;
			this.m_successOnMaskedAlpha = src.m_successOnMaskedAlpha;
			this.m_forwardRotation = src.m_forwardRotation;
			this.m_resources = new List<Resource>();
			foreach (Resource src2 in src.m_resources)
			{
				Resource item = new Resource(src2);
				this.m_resources.Add(item);
			}
			this.m_resourceType = src.m_resourceType;
			this.m_hasColliders = src.m_hasColliders;
			this.m_hasMeshes = src.m_hasMeshes;
			this.m_hasRigidBody = src.m_hasRigidBody;
			this.m_displayedInEditor = src.m_displayedInEditor;
			this.m_instancesSpawned = src.m_instancesSpawned;
			this.m_imageFilterColour = src.m_imageFilterColour;
			this.m_imageFilterFuzzyMatch = src.m_imageFilterFuzzyMatch;
		}

		public float GetSuccessChance()
		{
			if (!this.m_active)
			{
				return 0f;
			}
			float num = 0f;
			foreach (Resource resource in this.m_resources)
			{
				if (resource.m_successRate > num)
				{
					num = resource.m_successRate;
				}
			}
			return num;
		}

		public string m_name;

		public bool m_active = true;

		public Vector3 m_size = Vector3.one;

		public Vector3 m_extents = Vector3.one;

		public float m_boundsBorder;

		public bool m_constrainWithinMaskedBounds;

		public bool m_invertMaskedAlpha;

		public bool m_scaleOnMaskedAlpha;

		public float m_scaleOnMaskedAlphaMin = 0.5f;

		public float m_scaleOnMaskedAlphaMax = 1f;

		public bool m_successOnMaskedAlpha;

		public float m_forwardRotation;

		public List<Resource> m_resources = new List<Resource>();

		public Constants.ResourceType m_resourceType;

		public bool m_hasColliders;

		public bool m_hasMeshes;

		public bool m_hasRigidBody;

		public long m_instancesSpawned;

		public Color m_imageFilterColour = Color.white;

		public float m_imageFilterFuzzyMatch = 0.8f;

		public bool m_displayedInEditor;
	}
}
