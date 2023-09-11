using System;

namespace Crosstales.BWF.Util
{
	public static class Config
	{
		public static void Reset()
		{
			Config.DEBUG = false;
			Config.DEBUG_BADWORDS = false;
			Config.DEBUG_DOMAINS = false;
		}

		public static void Load()
		{
			if (CTPlayerPrefs.HasKey("BWF_CFG_DEBUG"))
			{
				Config.DEBUG = CTPlayerPrefs.GetBool("BWF_CFG_DEBUG");
			}
			if (CTPlayerPrefs.HasKey("BWF_CFG_DEBUG_BADWORDS"))
			{
				Config.DEBUG_BADWORDS = CTPlayerPrefs.GetBool("BWF_CFG_DEBUG_BADWORDS");
			}
			if (CTPlayerPrefs.HasKey("BWF_CFG_DEBUG_DOMAINS"))
			{
				Config.DEBUG_DOMAINS = CTPlayerPrefs.GetBool("BWF_CFG_DEBUG_DOMAINS");
			}
			Config.isLoaded = true;
		}

		public static void Save()
		{
			CTPlayerPrefs.SetBool("BWF_CFG_DEBUG", Config.DEBUG);
			CTPlayerPrefs.SetBool("BWF_CFG_DEBUG_BADWORDS", Config.DEBUG_BADWORDS);
			CTPlayerPrefs.SetBool("BWF_CFG_DEBUG_DOMAINS", Config.DEBUG_DOMAINS);
			CTPlayerPrefs.Save();
		}

		public static bool DEBUG;

		public static bool DEBUG_BADWORDS;

		public static bool DEBUG_DOMAINS;

		public static bool isLoaded;
	}
}
