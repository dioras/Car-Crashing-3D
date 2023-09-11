using System;
using System.Collections;
using UnityEngine;

namespace GeNa
{
	public class RuntimeSpawner : MonoBehaviour
	{
		private void Start()
		{
			base.StartCoroutine(this.RunSpawnerIteration(this.m_spawnInterval));
		}

		private IEnumerator RunSpawnerIteration(float waitTime)
		{
			for (;;)
			{
				yield return new WaitForSeconds(waitTime);
				if (this.m_spawner != null)
				{
					if (this.m_showDebug)
					{
						UnityEngine.Debug.Log("Running spawner iteration");
					}
					if (this.m_updateSpawnerSettings)
					{
						Ray ray = new Ray(base.transform.position, Vector3.down);
						RaycastHit raycastHit;
						if (Physics.Raycast(ray, out raycastHit, 10000f))
						{
							this.m_spawner.SetSpawnOriginAndUpdateRanges(raycastHit.transform, raycastHit.point, raycastHit.normal);
							this.m_spawner.Spawn(raycastHit.point, false);
						}
						else
						{
							this.m_spawner.SetSpawnOriginAndUpdateRanges(null, base.transform.position, Vector3.up);
							this.m_spawner.Spawn(base.transform.position, false);
						}
					}
					else
					{
						this.m_spawner.Spawn(base.transform.position, false);
					}
				}
				else if (this.m_showDebug)
				{
					UnityEngine.Debug.Log("Need a spawner in order to do the spawn!");
				}
			}
			yield break;
		}

		[Tooltip("The amount of time in seconds that the spawner will run a spawn iteration.")]
		public float m_spawnInterval = 10f;

		[Tooltip("The spawner that will run the spawn iteration.")]
		public Spawner m_spawner;

		[Tooltip("Update the spawner settings on every spawn iteration, otherwise just use the original cxriteria / settings ant apply them at the current location.")]
		public bool m_updateSpawnerSettings = true;

		[Tooltip("Show debug messages when it runs.")]
		public bool m_showDebug;
	}
}
