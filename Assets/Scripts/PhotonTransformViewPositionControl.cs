using System;
using UnityEngine;

public class PhotonTransformViewPositionControl
{
	public Vector3 UpdatePosition(Vector3 currentPosition)
	{
		Vector3 networkPosition = this.m_NetworkPosition;
		currentPosition = Vector3.Lerp(currentPosition, networkPosition, Time.deltaTime * 4f);
		if (Vector3.Distance(currentPosition, this.m_NetworkPosition) > 15f)
		{
			currentPosition = this.m_NetworkPosition;
		}
		return currentPosition;
	}

	public void OnPhotonSerializeView(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			this.SerializeData(currentPosition, stream, info);
		}
		else
		{
			this.DeserializeData(stream, info);
		}
	}

	private void SerializeData(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
	{
		stream.SendNext(currentPosition);
		this.m_NetworkPosition = currentPosition;
		if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
		{
			stream.SendNext(Vector3.zero);
			stream.SendNext(0f);
		}
	}

	private void DeserializeData(PhotonStream stream, PhotonMessageInfo info)
	{
		Vector3 networkPosition = (Vector3)stream.ReceiveNext();
		if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
		{
			Vector3 vector = (Vector3)stream.ReceiveNext();
			float num = (float)stream.ReceiveNext();
		}
		this.m_NetworkPosition = networkPosition;
	}

	private PhotonTransformViewPositionModel m_Model = new PhotonTransformViewPositionModel();

	public Vector3 m_NetworkPosition;
}
