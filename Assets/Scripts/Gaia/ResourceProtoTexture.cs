using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
	[Serializable]
	public class ResourceProtoTexture
	{
		public void Initialise(Spawner spawner)
		{
			foreach (SpawnCritera spawnCritera in this.m_spawnCriteria)
			{
				spawnCritera.Initialise(spawner);
			}
		}

		public bool HasActiveCriteria()
		{
			for (int i = 0; i < this.m_spawnCriteria.Length; i++)
			{
				if (this.m_spawnCriteria[i].m_isActive)
				{
					return true;
				}
			}
			return false;
		}

		public bool SetAssetAssociations()
		{
			return false;
		}

		public bool AssociateAssets()
		{
			return false;
		}

		public bool ChecksTextures()
		{
			for (int i = 0; i < this.m_spawnCriteria.Length; i++)
			{
				if (this.m_spawnCriteria[i].m_isActive && this.m_spawnCriteria[i].m_checkTexture)
				{
					return true;
				}
			}
			return false;
		}

		public bool ChecksProximity()
		{
			for (int i = 0; i < this.m_spawnCriteria.Length; i++)
			{
				if (this.m_spawnCriteria[i].m_isActive && this.m_spawnCriteria[i].m_checkProximity)
				{
					return true;
				}
			}
			return false;
		}

		public void AddTags(ref List<string> tagList)
		{
			for (int i = 0; i < this.m_spawnCriteria.Length; i++)
			{
				if (this.m_spawnCriteria[i].m_isActive && this.m_spawnCriteria[i].m_checkProximity && !tagList.Contains(this.m_spawnCriteria[i].m_proximityTag))
				{
					tagList.Add(this.m_spawnCriteria[i].m_proximityTag);
				}
			}
		}

		[Tooltip("Resource name.")]
		public string m_name;

		[Tooltip("Resource texture.")]
		public Texture2D m_texture;

		[HideInInspector]
		public string m_textureFileName;

		[Tooltip("Resource normal.")]
		public Texture2D m_normal;

		[HideInInspector]
		public string m_normalFileName;

		[Tooltip("The width over which the image will stretch on the terrain’s surface.")]
		public float m_sizeX = 10f;

		[Tooltip("The height over which the image will stretch on the terrain’s surface.")]
		public float m_sizeY = 10f;

		[Tooltip("How far from the terrain’s anchor point the tiling will start.")]
		public float m_offsetX;

		[Tooltip("How far from the terrain’s anchor point the tiling will start.")]
		public float m_offsetY;

		[Tooltip("Controls the overall metalness of the surface.")]
		[Range(0f, 1f)]
		public float m_metalic;

		[Tooltip("Controls the overall smoothness of the surface.")]
		[Range(0f, 1f)]
		public float m_smoothness;

		[Tooltip("SPAWN CRITERIA - Spawn criteria are run against the terrain to assess its fitness in a range of 0..1 for use by this resource. If you add multiple criteria then the fittest one will be selected.")]
		public SpawnCritera[] m_spawnCriteria = new SpawnCritera[0];

		[Tooltip("SPAWN EXTENSIONS - Spawn extensions allow fitness, spawning and post spawning extensions to be made to the spawning system.")]
		public SpawnRuleExtension[] m_spawnExtensions = new SpawnRuleExtension[0];
	}
}
