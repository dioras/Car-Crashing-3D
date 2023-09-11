using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeatherControllerResources", menuName = "WeatherControllerResources", order = 2)]
public class WeatherConrollerResources : ScriptableObject
{
	public AnimationCurve[] lightningLightCurves;

	public AudioClip[] lightningClips;

	public float lightningInterval;

	public float lightningChance;

	public float overcastShadowStrength;

	public float rainMaxEmission;

	public float snowMaxEmission;

	public float weatherChangeRate;

	public float rainCheckInterval;

	public float rainChance;
}
