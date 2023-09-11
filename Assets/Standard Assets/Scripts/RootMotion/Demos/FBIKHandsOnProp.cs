using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	public class FBIKHandsOnProp : MonoBehaviour
	{
		private void Awake()
		{
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreRead, new IKSolver.UpdateDelegate(this.OnPreRead));
		}

		private void OnPreRead()
		{
			if (this.leftHanded)
			{
				this.HandsOnProp(this.ik.solver.leftHandEffector, this.ik.solver.rightHandEffector);
			}
			else
			{
				this.HandsOnProp(this.ik.solver.rightHandEffector, this.ik.solver.leftHandEffector);
			}
		}

		private void HandsOnProp(IKEffector mainHand, IKEffector otherHand)
		{
			Vector3 vector = otherHand.bone.position - mainHand.bone.position;
			Vector3 point = Quaternion.Inverse(mainHand.bone.rotation) * vector;
			Vector3 b = mainHand.bone.position + vector * 0.5f;
			Quaternion rhs = Quaternion.Inverse(mainHand.bone.rotation) * otherHand.bone.rotation;
			Vector3 toDirection = otherHand.bone.position + otherHand.positionOffset - (mainHand.bone.position + mainHand.positionOffset);
			Vector3 a = mainHand.bone.position + mainHand.positionOffset + vector * 0.5f;
			mainHand.position = mainHand.bone.position + mainHand.positionOffset + (a - b);
			mainHand.positionWeight = 1f;
			Quaternion lhs = Quaternion.FromToRotation(vector, toDirection);
			mainHand.rotation = lhs * mainHand.bone.rotation;
			mainHand.rotationWeight = 1f;
			otherHand.position = mainHand.position + mainHand.rotation * point;
			otherHand.positionWeight = 1f;
			otherHand.rotation = mainHand.rotation * rhs;
			otherHand.rotationWeight = 1f;
		}

		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreRead, new IKSolver.UpdateDelegate(this.OnPreRead));
			}
		}

		public FullBodyBipedIK ik;

		public bool leftHanded;
	}
}
