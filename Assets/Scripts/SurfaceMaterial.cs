using System;
using UnityEngine;

[Serializable]
public class SurfaceMaterial
{
	public string name;

	public Texture2D TerrainTexture;

	public PhysicMaterial physicMaterial;

	public SurfaceMaterial.SurfaceType surfaceType;

	[Header("Particles")]
	public SurfaceMaterial.ParticlesType particlesType;

	public SurfaceMaterial.TireTracks tireTracks;

	[Header("Sounds")]
	public bool OffroadSounds;

	public bool SkidSounds;

	public bool WaterSplashSounds;

	[Header("Deformable surface")]
	[Range(0f, 1f)]
	public float Hardness;

	public float MaxDepth;

	public float MaxExtrudeHeight;

	public bool MudWater;

	public float MudWaterDepth;

	public float DirtinessSpeed = 1f;

	public float CleaningSpeed = 1f;

	public float MinimumDirtiness;

	public MaxSmallTerrainResolution MaxResolution = MaxSmallTerrainResolution.x257;

	[Header("Physics")]
	public float AddedDrag;

	[Range(0f, 1f)]
	public float[] TiresFriction;

	public enum SurfaceType
	{
		Hard,
		Mud,
		Mesh,
		Snow
	}

	public enum ParticlesType
	{
		None,
		MudPieces,
		SkidSmoke,
		SandDust,
		WaterSplash,
		Snow
	}

	public enum TireTracks
	{
		None,
		SnowMark,
		SandMark,
		MudMark,
		SnowTrack,
		SkidMark
	}
}
