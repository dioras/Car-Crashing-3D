using System;
using System.Collections.Generic;
using Crosstales.BWF.Filter;
using Crosstales.BWF.Model;
using Crosstales.BWF.Provider;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Manager
{
	[DisallowMultipleComponent]
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_manager_1_1_bad_word_manager.html")]
	public class BadWordManager : BaseManager
	{
		public static string ReplaceCharacters
		{
			get
			{
				if (BadWordManager.filter != null)
				{
					return BadWordManager.filter.ReplaceCharacters;
				}
				if (BadWordManager.manager != null)
				{
					return BadWordManager.manager.ReplaceChars;
				}
				return "*";
			}
			set
			{
				if (BadWordManager.filter != null)
				{
					BadWordManager.filter.ReplaceCharacters = value;
					BadWordManager.manager.ReplaceChars = value;
				}
				else if (BadWordManager.manager != null)
				{
					BadWordManager.manager.ReplaceChars = value;
				}
			}
		}

		public static bool isReplaceLeetSpeak
		{
			get
			{
				if (BadWordManager.filter != null)
				{
					return BadWordManager.filter.ReplaceLeetSpeak;
				}
				return BadWordManager.manager != null && BadWordManager.manager.ReplaceLeetSpeak;
			}
			set
			{
				if (BadWordManager.filter != null)
				{
					BadWordManager.filter.ReplaceLeetSpeak = value;
					BadWordManager.manager.ReplaceLeetSpeak = value;
				}
				else if (BadWordManager.manager != null)
				{
					BadWordManager.manager.ReplaceLeetSpeak = value;
				}
			}
		}

		public static bool isSimpleCheck
		{
			get
			{
				if (BadWordManager.filter != null)
				{
					return BadWordManager.filter.SimpleCheck;
				}
				return BadWordManager.manager != null && BadWordManager.manager.SimpleCheck;
			}
			set
			{
				if (BadWordManager.filter != null)
				{
					BadWordManager.filter.SimpleCheck = value;
					BadWordManager.manager.SimpleCheck = value;
				}
				else if (BadWordManager.manager != null)
				{
					BadWordManager.manager.SimpleCheck = value;
				}
			}
		}

		public static bool isReady
		{
			get
			{
				bool result = false;
				if (BadWordManager.filter != null)
				{
					result = BadWordManager.filter.isReady;
				}
				else
				{
					BadWordManager.logFilterIsNull("BadWordManager");
				}
				return result;
			}
		}

		public static List<Source> Sources
		{
			get
			{
				List<Source> result = new List<Source>();
				if (BadWordManager.filter != null)
				{
					result = BadWordManager.filter.Sources;
				}
				else
				{
					BadWordManager.logFilterIsNull("BadWordManager");
				}
				return result;
			}
		}

		public void OnEnable()
		{
			if (Helper.isEditorMode || !BadWordManager.initalized)
			{
				BadWordManager.manager = this;
				BadWordManager.Load();
				if (!Helper.isEditorMode && this.DontDestroy)
				{
					UnityEngine.Object.DontDestroyOnLoad(base.transform.root.gameObject);
					BadWordManager.initalized = true;
				}
			}
			else if (!Helper.isEditorMode && this.DontDestroy && BadWordManager.manager != this)
			{
				if (!BadWordManager.loggedOnlyOneInstance)
				{
					BadWordManager.loggedOnlyOneInstance = true;
					UnityEngine.Debug.LogWarning("Only one active instance of 'BadWordManager' allowed in all scenes!" + Environment.NewLine + "This object will now be destroyed.");
				}
				UnityEngine.Object.Destroy(base.transform.root.gameObject, 0.2f);
			}
		}

		public static void Load()
		{
			if (BadWordManager.manager != null)
			{
				BadWordManager.filter = new BadWordFilter(BadWordManager.manager.BadWordProviderLTR, BadWordManager.manager.BadWordProviderRTL, BadWordManager.manager.ReplaceChars, BadWordManager.manager.ReplaceLeetSpeak, BadWordManager.manager.SimpleCheck, BadWordManager.manager.MarkPrefix, BadWordManager.manager.MarkPostfix);
			}
		}

		public static bool Contains(string testString, params string[] sources)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(testString))
			{
				if (BadWordManager.filter != null)
				{
					result = BadWordManager.filter.Contains(testString, sources);
				}
				else
				{
					BadWordManager.logFilterIsNull("BadWordManager");
				}
			}
			return result;
		}

		public static void ContainsMT(out bool result, string testString, params string[] sources)
		{
			result = BadWordManager.Contains(testString, sources);
		}

		public static List<string> GetAll(string testString, params string[] sources)
		{
			List<string> result = new List<string>();
			if (!string.IsNullOrEmpty(testString))
			{
				if (BadWordManager.filter != null)
				{
					result = BadWordManager.filter.GetAll(testString, sources);
				}
				else
				{
					BadWordManager.logFilterIsNull("BadWordManager");
				}
			}
			return result;
		}

		public static void GetAllMT(out List<string> result, string testString, params string[] sources)
		{
			result = BadWordManager.GetAll(testString, sources);
		}

		public static string ReplaceAll(string testString, params string[] sources)
		{
			string result = testString;
			if (!string.IsNullOrEmpty(testString))
			{
				if (BadWordManager.filter != null)
				{
					result = BadWordManager.filter.ReplaceAll(testString, sources);
				}
				else
				{
					BadWordManager.logFilterIsNull("BadWordManager");
				}
			}
			return result;
		}

		public static void ReplaceAllMT(out string result, string testString, params string[] sources)
		{
			result = BadWordManager.ReplaceAll(testString, sources);
		}

		public static string Replace(string text, List<string> badWords)
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (BadWordManager.filter != null)
				{
					result = BadWordManager.filter.Replace(text, badWords);
				}
				else
				{
					BadWordManager.logFilterIsNull("BadWordManager");
				}
			}
			return result;
		}

		public static string Mark(string text, List<string> badWords, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (BadWordManager.filter != null)
				{
					result = BadWordManager.filter.Mark(text, badWords, prefix, postfix);
				}
				else
				{
					BadWordManager.logFilterIsNull("BadWordManager");
				}
			}
			return result;
		}

		public static string Unmark(string text, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (BadWordManager.filter != null)
				{
					result = BadWordManager.filter.Unmark(text, prefix, postfix);
				}
				else
				{
					BadWordManager.logFilterIsNull("BadWordManager");
				}
			}
			return result;
		}

		private static void logFilterIsNull(string clazz)
		{
			if (!BadWordManager.loggedFilterIsNull)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new string[]
				{
					"'filter' is null!",
					Environment.NewLine,
					"Did you add the '",
					clazz,
					"' to the current scene?"
				}));
				BadWordManager.loggedFilterIsNull = true;
			}
		}

		[Header("Specific Settings")]
		[Tooltip("Replace characters for bad words (default: *).")]
		public string ReplaceChars = "*";

		[Tooltip("Replace Leet speak in the input string (default: true).")]
		public bool ReplaceLeetSpeak;

		[Tooltip("Use simple detection algorithm. This is the way to check for Chinese, Japanese, Korean and Thai bad words (default: false).")]
		public bool SimpleCheck;

		[Header("Bad Word Providers")]
		[Tooltip("List of all left-to-right providers.")]
		public List<BadWordProvider> BadWordProviderLTR;

		[Tooltip("List of all right-to-left providers.")]
		public List<BadWordProvider> BadWordProviderRTL;

		private static bool initalized;

		private static BadWordFilter filter;

		private static BadWordManager manager;

		private static bool loggedFilterIsNull;

		private static bool loggedOnlyOneInstance;

		private const string clazz = "BadWordManager";
	}
}
