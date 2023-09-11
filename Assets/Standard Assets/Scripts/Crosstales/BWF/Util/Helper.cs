using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Crosstales.BWF.Util
{
	public static class Helper
	{
		public static bool isInternetAvailable
		{
			get
			{
				return Application.internetReachability != NetworkReachability.NotReachable;
			}
		}

		public static bool isWindowsPlatform
		{
			get
			{
				return Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
			}
		}

		public static bool isMacOSPlatform
		{
			get
			{
				return Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor;
			}
		}

		public static bool isLinuxPlatform
		{
			get
			{
				return Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.LinuxEditor;
			}
		}

		public static bool isAndroidPlatform
		{
			get
			{
				return Application.platform == RuntimePlatform.Android;
			}
		}

		public static bool isIOSPlatform
		{
			get
			{
				return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS;
			}
		}

		public static bool isWSAPlatform
		{
			get
			{
				return Application.platform == RuntimePlatform.MetroPlayerARM || Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.XboxOne;
			}
		}

		public static bool isWebGLPlatform
		{
			get
			{
				return Application.platform == RuntimePlatform.WebGLPlayer;
			}
		}

		public static bool isWebPlayerPlatform
		{
			get
			{
				return false;
			}
		}

		public static bool isWebPlatform
		{
			get
			{
				return Helper.isWebPlayerPlatform || Helper.isWebGLPlatform;
			}
		}

		public static bool isWindowsBasedPlatform
		{
			get
			{
				return Helper.isWindowsPlatform || Helper.isWSAPlatform;
			}
		}

		public static bool isAppleBasedPlatform
		{
			get
			{
				return Helper.isMacOSPlatform || Helper.isIOSPlatform;
			}
		}

		public static bool isEditorMode
		{
			get
			{
				return (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor) && !Application.isPlaying;
			}
		}

		public static bool isSupportedPlatform
		{
			get
			{
				return true;
			}
		}

		public static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			bool result = true;
			if (sslPolicyErrors != SslPolicyErrors.None)
			{
				for (int i = 0; i < chain.ChainStatus.Length; i++)
				{
					if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
					{
						chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
						chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
						chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
						chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
						result = chain.Build((X509Certificate2)certificate);
					}
				}
			}
			return result;
		}

		public static string FormatBytesToHRF(long bytes)
		{
			string[] array = new string[]
			{
				"B",
				"KB",
				"MB",
				"GB",
				"TB"
			};
			double num = (double)bytes;
			int num2 = 0;
			while (num >= 1024.0 && num2 < array.Length - 1)
			{
				num2++;
				num /= 1024.0;
			}
			return string.Format("{0:0.##} {1}", num, array[num2]);
		}

		public static string ValidatePath(string path)
		{
			string text;
			if (Helper.isWindowsPlatform)
			{
				text = path.Replace('/', '\\');
				if (!text.EndsWith("\\"))
				{
					text += "\\";
				}
			}
			else
			{
				text = path.Replace('\\', '/');
				if (!text.EndsWith("/"))
				{
					text += "/";
				}
			}
			return text;
		}

		public static List<string> SplitStringToLines(string text, int skipHeaderLines = 0, int skipFooterLines = 0, char splitChar = '#')
		{
			List<string> list = new List<string>(200);
			if (string.IsNullOrEmpty(text))
			{
				UnityEngine.Debug.LogWarning("Parameter 'text' is null or empty!" + Environment.NewLine + "=> 'SplitStringToLines()' will return an empty string list.");
			}
			else
			{
				string[] array = Helper.lineEndingsRegex.Split(text);
				for (int i = 0; i < array.Length; i++)
				{
					if (i + 1 > skipHeaderLines && i < array.Length - skipFooterLines && !string.IsNullOrEmpty(array[i]) && !array[i].StartsWith("#", StringComparison.OrdinalIgnoreCase))
					{
						list.Add(array[i].Split(new char[]
						{
							splitChar
						})[0]);
					}
				}
			}
			return list;
		}

		public static string CreateReplaceString(string replaceChars, int stringLength)
		{
			if (replaceChars.Length > 1)
			{
				char[] array = new char[stringLength];
				for (int i = 0; i < stringLength; i++)
				{
					array[i] = replaceChars[Helper.rd.Next(0, replaceChars.Length)];
				}
				return new string(array);
			}
			if (replaceChars.Length == 1)
			{
				return new string(replaceChars[0], stringLength);
			}
			return string.Empty;
		}

		public static Color HSVToRGB(float h, float s, float v, float a = 1f)
		{
			if (s == 0f)
			{
				return new Color(v, v, v, a);
			}
			h /= 60f;
			int num = Mathf.FloorToInt(h);
			float num2 = h - (float)num;
			float num3 = v * (1f - s);
			float num4 = v * (1f - s * num2);
			float num5 = v * (1f - s * (1f - num2));
			switch (num)
			{
			case 0:
				return new Color(v, num5, num3, a);
			case 1:
				return new Color(num4, v, num3, a);
			case 2:
				return new Color(num3, v, num5, a);
			case 3:
				return new Color(num3, num4, v, a);
			case 4:
				return new Color(num5, num3, v, a);
			default:
				return new Color(v, num3, num4, a);
			}
		}

		private static readonly Regex lineEndingsRegex = new Regex("\\r\\n|\\r|\\n");

		private static readonly System.Random rd = new System.Random();

		private const string WINDOWS_PATH_DELIMITER = "\\";

		private const string UNIX_PATH_DELIMITER = "/";
	}
}
