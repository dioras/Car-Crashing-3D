using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	public class BipedIKvsAnimatorIK : MonoBehaviour
	{
		private void Update()
		{
			this.bipedIK.SetLookAtPosition(this.lookAtTargetBiped.position);
			this.bipedIK.SetLookAtWeight(this.lookAtWeight, this.lookAtBodyWeight, this.lookAtHeadWeight, this.lookAtEyesWeight, this.lookAtClampWeight, this.lookAtClampWeightHead, this.lookAtClampWeightEyes);
			this.bipedIK.SetIKPosition(AvatarIKGoal.LeftHand, this.handTargetBiped.position);
			this.bipedIK.SetIKRotation(AvatarIKGoal.LeftHand, this.handTargetBiped.rotation);
			this.bipedIK.SetIKPositionWeight(AvatarIKGoal.LeftHand, this.handPositionWeight);
			this.bipedIK.SetIKRotationWeight(AvatarIKGoal.LeftHand, this.handRotationWeight);
			this.bipedIK.SetIKPosition(AvatarIKGoal.RightHand, this.rightHandTargetBiped.position);
			this.bipedIK.SetIKRotation(AvatarIKGoal.RightHand, this.rightHandTargetBiped.rotation);
			this.bipedIK.SetIKPositionWeight(AvatarIKGoal.RightHand, this.handPositionWeight);
			this.bipedIK.SetIKRotationWeight(AvatarIKGoal.RightHand, this.handRotationWeight);
		}

		[LargeHeader("References")]
		public BipedIK bipedIK;

		[LargeHeader("Look At")]
		public Transform lookAtTargetBiped;

		[Range(0f, 1f)]
		public float lookAtWeight = 1f;

		[Range(0f, 1f)]
		public float lookAtBodyWeight = 1f;

		[Range(0f, 1f)]
		public float lookAtHeadWeight = 1f;

		[Range(0f, 1f)]
		public float lookAtEyesWeight = 1f;

		[Range(0f, 1f)]
		public float lookAtClampWeight = 0.5f;

		[Range(0f, 1f)]
		public float lookAtClampWeightHead = 0.5f;

		[Range(0f, 1f)]
		public float lookAtClampWeightEyes = 0.5f;

		[LargeHeader("Hand")]
		public Transform handTargetBiped;

		public Transform rightHandTargetBiped;

		[Range(0f, 1f)]
		public float handPositionWeight;

		[Range(0f, 1f)]
		public float handRotationWeight;
	}
}
