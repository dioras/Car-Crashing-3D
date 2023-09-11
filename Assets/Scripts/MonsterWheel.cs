using System;
using UnityEngine;

[Serializable]
public class MonsterWheel
{
	public Transform BrakeDisk;

	public Transform Knuckle;

	public Transform AxleEnd;

	public Transform TieRodEnd;

	public Transform ArmEnd;

	public Transform ArmStart;

	public Transform SupportArmEnd;

	public Transform SupportArmStart;

	public Transform Joint;

	public Transform ShockUps;

	public Transform ShockDowns;

	public Transform Dummy;

	public Transform WheelColliderHolder;

	public Transform[] Shocks;
}
