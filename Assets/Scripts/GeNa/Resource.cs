using System;
using UnityEngine;

namespace GeNa
{
	[Serializable]
	public class Resource
	{
		public Resource()
		{
		}

		public Resource(Resource src)
		{
			this.m_name = src.m_name;
			this.m_resourceType = src.m_resourceType;
			this.m_prefab = src.m_prefab;
			this.m_successRate = src.m_successRate;
			this.m_conformToSlope = src.m_conformToSlope;
			this.m_minOffset = src.m_minOffset;
			this.m_maxOffset = src.m_maxOffset;
			this.m_minRotation = src.m_minRotation;
			this.m_maxRotation = src.m_maxRotation;
			this.m_sameScale = src.m_sameScale;
			this.m_minScale = src.m_minScale;
			this.m_maxScale = src.m_maxScale;
			this.m_terrainProtoIdx = src.m_terrainProtoIdx;
			this.m_hasColliders = src.m_hasColliders;
			this.m_hasRootCollider = src.m_hasRootCollider;
			this.m_hasMeshes = src.m_hasMeshes;
			this.m_hasRigidBody = src.m_hasRigidBody;
			this.m_flagLightmapStatic = src.m_flagLightmapStatic;
			this.m_flagBatchingStatic = src.m_flagBatchingStatic;
			this.m_flagOccludeeStatic = src.m_flagOccludeeStatic;
			this.m_flagOccluderStatic = src.m_flagOccluderStatic;
			this.m_flagNavigationStatic = src.m_flagNavigationStatic;
			this.m_flagOffMeshLinkGeneration = src.m_flagOffMeshLinkGeneration;
			this.m_flagReflectionProbeStatic = src.m_flagReflectionProbeStatic;
			this.m_flagMovingObject = src.m_flagMovingObject;
			this.m_flagForceOptimise = src.m_flagForceOptimise;
			this.m_flagCanBeOptimised = src.m_flagCanBeOptimised;
			this.m_flagIsOutdoorObject = src.m_flagIsOutdoorObject;
			this.m_baseScale = src.m_baseScale;
			this.m_baseSize = src.m_baseSize;
			this.m_baseColliderCenter = src.m_baseColliderCenter;
			this.m_baseColliderUseConstScale = src.m_baseColliderUseConstScale;
			this.m_baseColliderConstScaleAmount = src.m_baseColliderConstScaleAmount;
			this.m_baseColliderScale = src.m_baseColliderScale;
			this.m_displayedInEditor = src.m_displayedInEditor;
			this.m_instancesSpawned = src.m_instancesSpawned;
		}

		public string m_name;

		public Constants.ResourceType m_resourceType;

		public GameObject m_prefab;

		public float m_successRate = 1f;

		public bool m_conformToSlope = true;

		public Vector3 m_minOffset = new Vector3(0f, -0.15f, 0f);

		public Vector3 m_maxOffset = new Vector3(0f, -0.15f, 0f);

		public Vector3 m_minRotation = Vector3.zero;

		public Vector3 m_maxRotation = new Vector3(0f, 360f, 0f);

		public bool m_sameScale = true;

		public Vector3 m_minScale = Vector3.one;

		public Vector3 m_maxScale = Vector3.one;

		public int m_terrainProtoIdx;

		public bool m_hasColliders;

		public bool m_hasRootCollider;

		public bool m_hasMeshes = true;

		public bool m_hasRigidBody;

		public bool m_flagLightmapStatic = true;

		public bool m_flagBatchingStatic = true;

		public bool m_flagOccludeeStatic = true;

		public bool m_flagOccluderStatic = true;

		public bool m_flagNavigationStatic = true;

		public bool m_flagOffMeshLinkGeneration = true;

		public bool m_flagReflectionProbeStatic = true;

		public bool m_flagMovingObject;

		public bool m_flagCanBeOptimised = true;

		public bool m_flagForceOptimise;

		public bool m_flagIsOutdoorObject = true;

		public Vector3 m_baseScale = Vector3.one;

		public Vector3 m_baseSize = Vector3.one;

		public Vector3 m_baseColliderCenter = Vector3.zero;

		public bool m_baseColliderUseConstScale = true;

		public float m_baseColliderConstScaleAmount = 0.75f;

		public Vector3 m_baseColliderScale = Vector3.one;

		public long m_instancesSpawned;

		public bool m_displayedInEditor;
	}
}
