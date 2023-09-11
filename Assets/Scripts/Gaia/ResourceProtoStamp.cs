using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
	[Serializable]
	public class ResourceProtoStamp
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

		public void AddStamps(ref List<string> stampList)
		{
			if (this.m_texture != null)
			{
			}
		}

		[Tooltip("Stamp name.")]
		public string m_name;

		[Tooltip("Stamp texture.")]
		public Texture2D m_texture;

		[Tooltip("How far from the terrain’s anchor point the tiling will start.")]
		public bool m_stickToGround = true;

		[Tooltip("DNA - Used by the spawner to control how and where the tree will be spawned.")]
		public ResourceProtoDNA m_dna;

		[Tooltip("SPAWN CRITERIA - Spawn criteria are run against the terrain to assess its fitness in a range of 0..1 for use by this resource. If you add multiple criteria then the fittest one will be selected.")]
		public SpawnCritera[] m_spawnCriteria = new SpawnCritera[0];
	}
}
