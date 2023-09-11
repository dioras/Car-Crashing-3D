using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class XmlSerialization : MonoBehaviour
{
	public static object DeserializeData<T>(string xmlString)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		MemoryStream stream = new MemoryStream(XmlSerialization.StringToUTF8(xmlString));
		object result = null;
		try
		{
			result = xmlSerializer.Deserialize(stream);
		}
		catch
		{
			UnityEngine.Debug.LogError("Error while deserialization");
		}
		return result;
	}

	public static string SerializeData<T>(object Object)
	{
		MemoryStream memoryStream = new MemoryStream();
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
		xmlSerializer.Serialize(xmlTextWriter, Object);
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
		return XmlSerialization.UTF8ToString(memoryStream.ToArray());
	}

	public static string UTF8ToString(byte[] characters)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(characters);
	}

	public static byte[] StringToUTF8(string pXmlString)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetBytes(pXmlString);
	}
}
