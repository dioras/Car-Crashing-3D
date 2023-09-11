using System;
using System.Text;
using Ionic.Zlib;

namespace Assets.SimpleZip
{
	public static class Zip
	{
		public static byte[] Compress(string text)
		{
			return ZlibStream.CompressString(text);
		}

		public static string CompressToString(string text)
		{
			return Convert.ToBase64String(Zip.Compress(text));
		}

		public static string Decompress(byte[] bytes)
		{
			return Encoding.UTF8.GetString(ZlibStream.UncompressBuffer(bytes));
		}

		public static string Decompress(string data)
		{
			return Zip.Decompress(Convert.FromBase64String(data));
		}
	}
}
