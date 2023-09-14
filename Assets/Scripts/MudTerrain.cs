using System;
using UnityEngine;

public class MudTerrain : MonoBehaviour
{
	private void Start()
	{
		base.GetComponent<TerrainCollider>().enabled = false;
		this.DefaultHeights = this.terrainData.GetHeights(0, 0, this.hRes, this.hRes);
	}

	//[HideInInspector]
	public Terrain terrain;

	//[HideInInspector]
	public TerrainData terrainData;

	//[HideInInspector]
	public Texture2D[] textures;

	//[HideInInspector]
	public int hRes;

	//[HideInInspector]
	public int aRes;

	//[HideInInspector]
	public Rect terRect;

	//[HideInInspector]
	public float[,] DefaultHeights;

	//[HideInInspector]
	public float[,,] DefaultAlphaMap;

	//[HideInInspector]
	public bool LODS_Baked;

	//[HideInInspector]
	public float DeformableMaterialMaxDepth;

	public float drag;
}
