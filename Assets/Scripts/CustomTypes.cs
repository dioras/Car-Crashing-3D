using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

internal static class CustomTypes
{
	internal static void Register()
	{
		Type typeFromHandle = typeof(Vector2);
		byte code = 87;
		if (CustomTypes._003C_003Ef__mg_0024cache0 == null)
		{
			CustomTypes._003C_003Ef__mg_0024cache0 = new SerializeStreamMethod(CustomTypes.SerializeVector2);
		}
		SerializeStreamMethod serializeMethod = CustomTypes._003C_003Ef__mg_0024cache0;
		if (CustomTypes._003C_003Ef__mg_0024cache1 == null)
		{
			CustomTypes._003C_003Ef__mg_0024cache1 = new DeserializeStreamMethod(CustomTypes.DeserializeVector2);
		}
		PhotonPeer.RegisterType(typeFromHandle, code, serializeMethod, CustomTypes._003C_003Ef__mg_0024cache1);
		Type typeFromHandle2 = typeof(Vector3);
		byte code2 = 86;
		if (CustomTypes._003C_003Ef__mg_0024cache2 == null)
		{
			CustomTypes._003C_003Ef__mg_0024cache2 = new SerializeStreamMethod(CustomTypes.SerializeVector3);
		}
		SerializeStreamMethod serializeMethod2 = CustomTypes._003C_003Ef__mg_0024cache2;
		if (CustomTypes._003C_003Ef__mg_0024cache3 == null)
		{
			CustomTypes._003C_003Ef__mg_0024cache3 = new DeserializeStreamMethod(CustomTypes.DeserializeVector3);
		}
		PhotonPeer.RegisterType(typeFromHandle2, code2, serializeMethod2, CustomTypes._003C_003Ef__mg_0024cache3);
		Type typeFromHandle3 = typeof(Quaternion);
		byte code3 = 81;
		if (CustomTypes._003C_003Ef__mg_0024cache4 == null)
		{
			CustomTypes._003C_003Ef__mg_0024cache4 = new SerializeStreamMethod(CustomTypes.SerializeQuaternion);
		}
		SerializeStreamMethod serializeMethod3 = CustomTypes._003C_003Ef__mg_0024cache4;
		if (CustomTypes._003C_003Ef__mg_0024cache5 == null)
		{
			CustomTypes._003C_003Ef__mg_0024cache5 = new DeserializeStreamMethod(CustomTypes.DeserializeQuaternion);
		}
		PhotonPeer.RegisterType(typeFromHandle3, code3, serializeMethod3, CustomTypes._003C_003Ef__mg_0024cache5);
		Type typeFromHandle4 = typeof(PhotonPlayer);
		byte code4 = 80;
		if (CustomTypes._003C_003Ef__mg_0024cache6 == null)
		{
			CustomTypes._003C_003Ef__mg_0024cache6 = new SerializeStreamMethod(CustomTypes.SerializePhotonPlayer);
		}
		SerializeStreamMethod serializeMethod4 = CustomTypes._003C_003Ef__mg_0024cache6;
		if (CustomTypes._003C_003Ef__mg_0024cache7 == null)
		{
			CustomTypes._003C_003Ef__mg_0024cache7 = new DeserializeStreamMethod(CustomTypes.DeserializePhotonPlayer);
		}
		PhotonPeer.RegisterType(typeFromHandle4, code4, serializeMethod4, CustomTypes._003C_003Ef__mg_0024cache7);
	}

	private static short SerializeVector3(StreamBuffer outStream, object customobject)
	{
		Vector3 vector = (Vector3)customobject;
		int num = 0;
		object obj = CustomTypes.memVector3;
		lock (obj)
		{
			byte[] array = CustomTypes.memVector3;
			Protocol.Serialize(vector.x, array, ref num);
			Protocol.Serialize(vector.y, array, ref num);
			Protocol.Serialize(vector.z, array, ref num);
			outStream.Write(array, 0, 12);
		}
		return 12;
	}

	private static object DeserializeVector3(StreamBuffer inStream, short length)
	{
		Vector3 vector = default(Vector3);
		object obj = CustomTypes.memVector3;
		lock (obj)
		{
			inStream.Read(CustomTypes.memVector3, 0, 12);
			int num = 0;
			Protocol.Deserialize(out vector.x, CustomTypes.memVector3, ref num);
			Protocol.Deserialize(out vector.y, CustomTypes.memVector3, ref num);
			Protocol.Deserialize(out vector.z, CustomTypes.memVector3, ref num);
		}
		return vector;
	}

	private static short SerializeVector2(StreamBuffer outStream, object customobject)
	{
		Vector2 vector = (Vector2)customobject;
		object obj = CustomTypes.memVector2;
		lock (obj)
		{
			byte[] array = CustomTypes.memVector2;
			int num = 0;
			Protocol.Serialize(vector.x, array, ref num);
			Protocol.Serialize(vector.y, array, ref num);
			outStream.Write(array, 0, 8);
		}
		return 8;
	}

	private static object DeserializeVector2(StreamBuffer inStream, short length)
	{
		Vector2 vector = default(Vector2);
		object obj = CustomTypes.memVector2;
		lock (obj)
		{
			inStream.Read(CustomTypes.memVector2, 0, 8);
			int num = 0;
			Protocol.Deserialize(out vector.x, CustomTypes.memVector2, ref num);
			Protocol.Deserialize(out vector.y, CustomTypes.memVector2, ref num);
		}
		return vector;
	}

	private static short SerializeQuaternion(StreamBuffer outStream, object customobject)
	{
		Quaternion quaternion = (Quaternion)customobject;
		object obj = CustomTypes.memQuarternion;
		lock (obj)
		{
			byte[] array = CustomTypes.memQuarternion;
			int num = 0;
			Protocol.Serialize(quaternion.w, array, ref num);
			Protocol.Serialize(quaternion.x, array, ref num);
			Protocol.Serialize(quaternion.y, array, ref num);
			Protocol.Serialize(quaternion.z, array, ref num);
			outStream.Write(array, 0, 16);
		}
		return 16;
	}

	private static object DeserializeQuaternion(StreamBuffer inStream, short length)
	{
		Quaternion quaternion = default(Quaternion);
		object obj = CustomTypes.memQuarternion;
		lock (obj)
		{
			inStream.Read(CustomTypes.memQuarternion, 0, 16);
			int num = 0;
			Protocol.Deserialize(out quaternion.w, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.x, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.y, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.z, CustomTypes.memQuarternion, ref num);
		}
		return quaternion;
	}

	private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
	{
		int id = ((PhotonPlayer)customobject).ID;
		object obj = CustomTypes.memPlayer;
		short result;
		lock (obj)
		{
			byte[] array = CustomTypes.memPlayer;
			int num = 0;
			Protocol.Serialize(id, array, ref num);
			outStream.Write(array, 0, 4);
			result = 4;
		}
		return result;
	}

	private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
	{
		object obj = CustomTypes.memPlayer;
		int key;
		lock (obj)
		{
			inStream.Read(CustomTypes.memPlayer, 0, (int)length);
			int num = 0;
			Protocol.Deserialize(out key, CustomTypes.memPlayer, ref num);
		}
		if (PhotonNetwork.networkingPeer.mActors.ContainsKey(key))
		{
			return PhotonNetwork.networkingPeer.mActors[key];
		}
		return null;
	}

	public static readonly byte[] memVector3 = new byte[12];

	public static readonly byte[] memVector2 = new byte[8];

	public static readonly byte[] memQuarternion = new byte[16];

	public static readonly byte[] memPlayer = new byte[4];

	[CompilerGenerated]
	private static SerializeStreamMethod _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static DeserializeStreamMethod _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static SerializeStreamMethod _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static DeserializeStreamMethod _003C_003Ef__mg_0024cache3;

	[CompilerGenerated]
	private static SerializeStreamMethod _003C_003Ef__mg_0024cache4;

	[CompilerGenerated]
	private static DeserializeStreamMethod _003C_003Ef__mg_0024cache5;

	[CompilerGenerated]
	private static SerializeStreamMethod _003C_003Ef__mg_0024cache6;

	[CompilerGenerated]
	private static DeserializeStreamMethod _003C_003Ef__mg_0024cache7;
}
