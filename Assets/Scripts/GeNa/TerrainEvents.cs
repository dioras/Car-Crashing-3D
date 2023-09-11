using System;
using UnityEngine;

namespace GeNa
{
	[ExecuteInEditMode]
	public class TerrainEvents : MonoBehaviour
	{
		private void OnTerrainChanged(int flags)
		{
			UnityEngine.Debug.Log((TerrainEvents.TerrainChangedFlags)flags);
		}

		[Flags]
		internal enum TerrainChangedFlags
		{
			NoChange = 0,
			Heightmap = 1,
			TreeInstances = 2,
			DelayedHeightmapUpdate = 4,
			FlushEverythingImmediately = 8,
			RemoveDirtyDetailsImmediately = 16,
			WillBeDestroyed = 256
		}
	}
}
