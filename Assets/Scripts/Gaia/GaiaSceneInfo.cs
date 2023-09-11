using System;
using UnityEngine;

namespace Gaia
{
	public class GaiaSceneInfo
	{
		public static GaiaSceneInfo GetSceneInfo()
		{
			GaiaSceneInfo gaiaSceneInfo = new GaiaSceneInfo();
			Terrain activeTerrain = TerrainHelper.GetActiveTerrain();
			if (activeTerrain == null)
			{
				UnityEngine.Debug.LogWarning("You must have a valid terrain for sceneinfo to work correctly.");
			}
			else
			{
				GaiaSessionManager sessionManager = GaiaSessionManager.GetSessionManager(false);
				TerrainHelper.GetTerrainBounds(activeTerrain, ref gaiaSceneInfo.m_sceneBounds);
				gaiaSceneInfo.m_seaLevel = sessionManager.GetSeaLevel();
				gaiaSceneInfo.m_centrePointOnTerrain = new Vector3(gaiaSceneInfo.m_sceneBounds.center.x, activeTerrain.SampleHeight(gaiaSceneInfo.m_sceneBounds.center), gaiaSceneInfo.m_sceneBounds.center.z);
			}
			return gaiaSceneInfo;
		}

		public Bounds m_sceneBounds = default(Bounds);

		public Vector3 m_centrePointOnTerrain = Vector3.zero;

		public float m_seaLevel;
	}
}
