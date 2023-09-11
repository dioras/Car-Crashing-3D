using System;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
	private void Start()
	{
		Application.targetFrameRate = this.m_targetFrameRate;
		this.UpdateQualitySettings();
	}

	private void UpdateQualitySettings()
	{
		if (FastMobileBloom.Instance != null)
		{
			FastMobileBloom.Instance.enabled = DataStore.GetBool("PostFX", true);
		}
		if (SimpleLUT.Instance != null)
		{
			SimpleLUT.Instance.enabled = DataStore.GetBool("PostFX", true);
		}
		bool flag = false;
		/*if (QualitySettings.GetQualityLevel() < 3 && !flag)
		{
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				terrain.materialType = Terrain.MaterialType.BuiltInLegacyDiffuse;
				SplatPrototype[] splatPrototypes = terrain.terrainData.splatPrototypes;
				foreach (SplatPrototype splatPrototype in splatPrototypes)
				{
					splatPrototype.normalMap = null;
				}
				terrain.terrainData.splatPrototypes = splatPrototypes;
			}
		}*/
        if (QualitySettings.GetQualityLevel() < 3 && !flag)
        {
            foreach (Terrain terrain in Terrain.activeTerrains)
            {
                // Replace the obsolete materialType property with the new materialTemplate property
                terrain.materialType = Terrain.MaterialType.BuiltInLegacyDiffuse;

                TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
                foreach (TerrainLayer terrainLayer in terrainLayers)
                {
                    // Replace the obsolete splatPrototypes property with the new terrainLayers property
                    terrainLayer.normalMapTexture = null;
                }
                terrain.terrainData.terrainLayers = terrainLayers;
            }
        }

        switch (QualitySettings.GetQualityLevel())
		{
		case 0:
			Terrain.activeTerrain.heightmapPixelError = 120f;
			Terrain.activeTerrain.heightmapMaximumLOD = 1;
			Terrain.activeTerrain.basemapDistance = 70f;
			if (!this.dontChangeDistanceSettings)
			{
				RenderSettings.fogStartDistance = 35f;
				RenderSettings.fogEndDistance = 100f;
				if (Camera.main != null)
				{
					Camera.main.farClipPlane = 100f;
				}
			}
			break;
		case 1:
			Terrain.activeTerrain.heightmapPixelError = 80f;
			Terrain.activeTerrain.heightmapMaximumLOD = 1;
			Terrain.activeTerrain.basemapDistance = 80f;
			if (!this.dontChangeDistanceSettings)
			{
				RenderSettings.fogStartDistance = 35f;
				RenderSettings.fogEndDistance = 120f;
				if (Camera.main != null)
				{
					Camera.main.farClipPlane = 120f;
				}
			}
			break;
		case 2:
			Terrain.activeTerrain.heightmapPixelError = 40f;
			Terrain.activeTerrain.heightmapMaximumLOD = 0;
			Terrain.activeTerrain.basemapDistance = 90f;
			if (!this.dontChangeDistanceSettings)
			{
				RenderSettings.fogStartDistance = 35f;
				RenderSettings.fogEndDistance = 300f;
				if (Camera.main != null)
				{
					Camera.main.farClipPlane = 300f;
				}
			}
			break;
		case 3:
			Terrain.activeTerrain.heightmapPixelError = 40f;
			Terrain.activeTerrain.heightmapMaximumLOD = 0;
			Terrain.activeTerrain.basemapDistance = 90f;
			if (!this.dontChangeDistanceSettings)
			{
				RenderSettings.fogStartDistance = 35f;
				RenderSettings.fogEndDistance = 300f;
				if (Camera.main != null)
				{
					Camera.main.farClipPlane = 300f;
				}
			}
			break;
		case 4:
			Terrain.activeTerrain.heightmapPixelError = 20f;
			Terrain.activeTerrain.heightmapMaximumLOD = 0;
			Terrain.activeTerrain.basemapDistance = 150f;
			if (!this.dontChangeDistanceSettings)
			{
				RenderSettings.fogStartDistance = 35f;
				RenderSettings.fogEndDistance = 500f;
				if (Camera.main != null)
				{
					Camera.main.farClipPlane = 500f;
				}
			}
			break;
		case 5:
			Terrain.activeTerrain.heightmapPixelError = 10f;
			Terrain.activeTerrain.heightmapMaximumLOD = 0;
			Terrain.activeTerrain.basemapDistance = 300f;
			if (!this.dontChangeDistanceSettings)
			{
				RenderSettings.fogStartDistance = 35f;
				RenderSettings.fogEndDistance = 600f;
				if (Camera.main != null)
				{
					Camera.main.farClipPlane = 600f;
				}
			}
			break;
		}
	}

	public int m_targetFrameRate = 60;

	public bool dontChangeDistanceSettings;
}
