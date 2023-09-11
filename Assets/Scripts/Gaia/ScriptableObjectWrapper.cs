using System;
using System.IO;

namespace Gaia
{
	[Serializable]
	public class ScriptableObjectWrapper
	{
		public string GetSessionedFileName(string sessionName)
		{
			return Utils.FixFileName(sessionName) + "_" + Path.GetFileName(this.m_fileName);
		}

		public static string GetSessionedFileName(string sessionName, string soFileName)
		{
			return Utils.FixFileName(sessionName) + "_" + Path.GetFileName(soFileName);
		}

		public string m_name;

		public string m_fileName;

		public byte[] m_content;
	}
}
