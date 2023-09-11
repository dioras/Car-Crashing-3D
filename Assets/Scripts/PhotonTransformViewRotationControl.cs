using System;
using UnityEngine;

public class PhotonTransformViewRotationControl
{
	public Quaternion GetRotation(Quaternion currentRotation)
	{
		return Quaternion.Lerp(currentRotation, this.m_NetworkRotation, Time.deltaTime * 3f);
	}

	public void OnPhotonSerializeView(Quaternion currentRotation, PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(currentRotation);
			this.m_NetworkRotation = currentRotation;
		}
		else
		{
			this.m_NetworkRotation = (Quaternion)stream.ReceiveNext();
		}
	}

	public Quaternion m_NetworkRotation;
}
