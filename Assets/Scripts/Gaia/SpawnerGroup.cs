using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
	[ExecuteInEditMode]
	public class SpawnerGroup : MonoBehaviour
	{
		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void StartEditorUpdates()
		{
		}

		public void StopEditorUpdates()
		{
		}

		private void EditorUpdate()
		{
			if (this.m_updateCoroutine == null)
			{
				this.StopEditorUpdates();
				return;
			}
			this.m_updateCoroutine.MoveNext();
		}

		public void RunSpawnerIteration()
		{
			this.m_cancelSpawn = false;
			this.m_progress = 0;
			base.StartCoroutine(this.RunSpawnerIterationCoRoutine());
		}

		public IEnumerator RunSpawnerIterationCoRoutine()
		{
			for (int idx = 0; idx < this.m_spawners.Count; idx++)
			{
				SpawnerGroup.SpawnerInstance si = this.m_spawners[idx];
				if (si != null && si.m_spawner != null)
				{
					for (int iter = 0; iter < si.m_interationsPerSpawn; iter++)
					{
						if (!this.m_cancelSpawn)
						{
							si.m_spawner.RunSpawnerIteration();
							yield return new WaitForSeconds(0.2f);
							while (!si.m_spawner.m_spawnComplete)
							{
								this.m_progress++;
								yield return new WaitForSeconds(0.5f);
							}
							this.m_progress++;
						}
					}
				}
			}
			this.m_progress = 0;
			this.m_updateCoroutine = null;
			yield break;
		}

		public void CancelSpawn()
		{
			this.m_cancelSpawn = true;
			for (int i = 0; i < this.m_spawners.Count; i++)
			{
				this.m_spawners[i].m_spawner.CancelSpawn();
			}
			for (int j = 0; j < this.m_spawnerGroups.Count; j++)
			{
				this.m_spawnerGroups[j].CancelSpawn();
			}
		}

		public bool FixNames()
		{
			bool result = false;
			for (int i = 0; i < this.m_spawners.Count; i++)
			{
				SpawnerGroup.SpawnerInstance spawnerInstance = this.m_spawners[i];
				if (spawnerInstance != null && spawnerInstance.m_spawner != null && spawnerInstance.m_name != spawnerInstance.m_spawner.name)
				{
					spawnerInstance.m_name = spawnerInstance.m_spawner.name;
					result = true;
				}
			}
			return result;
		}

		public void ResetSpawner()
		{
			for (int i = 0; i < this.m_spawners.Count; i++)
			{
				SpawnerGroup.SpawnerInstance spawnerInstance = this.m_spawners[i];
				if (spawnerInstance != null && spawnerInstance.m_spawner != null)
				{
					spawnerInstance.m_spawner.ResetSpawner();
				}
			}
			for (int j = 0; j < this.m_spawnerGroups.Count; j++)
			{
				SpawnerGroup spawnerGroup = this.m_spawnerGroups[j];
				if (spawnerGroup != null)
				{
					spawnerGroup.ResetSpawner();
				}
			}
		}

		public List<SpawnerGroup.SpawnerInstance> m_spawners = new List<SpawnerGroup.SpawnerInstance>();

		[HideInInspector]
		public List<SpawnerGroup> m_spawnerGroups = new List<SpawnerGroup>();

		public IEnumerator m_updateCoroutine;

		private bool m_cancelSpawn;

		[HideInInspector]
		public int m_progress;

		[Serializable]
		public class SpawnerInstance
		{
			public string m_name;

			public Spawner m_spawner;

			public int m_interationsPerSpawn = 1;
		}
	}
}
