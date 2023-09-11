using System;
using System.Collections.Generic;
using Crosstales.BWF.Filter;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Manager
{
	[DisallowMultipleComponent]
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_manager_1_1_punctuation_manager.html")]
	public class PunctuationManager : BaseManager
	{
		public static int CharacterNumber
		{
			get
			{
				if (PunctuationManager.filter != null)
				{
					return PunctuationManager.filter.CharacterNumber;
				}
				if (PunctuationManager.manager != null)
				{
					return PunctuationManager.manager.PunctuationCharsNumber;
				}
				return 3;
			}
			set
			{
				int num = value;
				if (num < 2)
				{
					num = 2;
				}
				if (PunctuationManager.filter != null)
				{
					PunctuationManager.filter.CharacterNumber = num;
					PunctuationManager.manager.PunctuationCharsNumber = num;
				}
				else if (PunctuationManager.manager != null)
				{
					PunctuationManager.manager.PunctuationCharsNumber = num;
				}
			}
		}

		public static bool isReady
		{
			get
			{
				return PunctuationManager.filter.isReady;
			}
		}

		public void OnEnable()
		{
			if (Helper.isEditorMode || !PunctuationManager.initalized)
			{
				PunctuationManager.manager = this;
				PunctuationManager.Load();
				if (!Helper.isEditorMode && this.DontDestroy)
				{
					UnityEngine.Object.DontDestroyOnLoad(base.transform.root.gameObject);
					PunctuationManager.initalized = true;
				}
			}
			else if (!Helper.isEditorMode && this.DontDestroy && PunctuationManager.manager != this)
			{
				if (!PunctuationManager.loggedOnlyOneInstance)
				{
					PunctuationManager.loggedOnlyOneInstance = true;
					UnityEngine.Debug.LogWarning("Only one active instance of 'PunctuationManager' allowed in all scenes!" + Environment.NewLine + "This object will now be destroyed.");
				}
				UnityEngine.Object.Destroy(base.transform.root.gameObject, 0.2f);
			}
		}

		public void OnValidate()
		{
			if (this.PunctuationCharsNumber < 2)
			{
				this.PunctuationCharsNumber = 2;
			}
		}

		public static void Load()
		{
			if (PunctuationManager.manager != null)
			{
				PunctuationManager.filter = new PunctuationFilter(PunctuationManager.manager.PunctuationCharsNumber, PunctuationManager.manager.MarkPrefix, PunctuationManager.manager.MarkPostfix);
			}
		}

		public static bool Contains(string testString)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(testString))
			{
				if (PunctuationManager.filter != null)
				{
					result = PunctuationManager.filter.Contains(testString, new string[0]);
				}
				else
				{
					PunctuationManager.logFilterIsNull("PunctuationManager");
				}
			}
			return result;
		}

		public static void ContainsMT(out bool result, string testString)
		{
			result = PunctuationManager.Contains(testString);
		}

		public static List<string> GetAll(string testString)
		{
			List<string> result = new List<string>();
			if (!string.IsNullOrEmpty(testString))
			{
				if (PunctuationManager.filter != null)
				{
					result = PunctuationManager.filter.GetAll(testString, new string[0]);
				}
				else
				{
					PunctuationManager.logFilterIsNull("PunctuationManager");
				}
			}
			return result;
		}

		public static void GetAllMT(out List<string> result, string testString)
		{
			result = PunctuationManager.GetAll(testString);
		}

		public static string ReplaceAll(string testString)
		{
			string result = testString;
			if (!string.IsNullOrEmpty(testString))
			{
				if (PunctuationManager.filter != null)
				{
					result = PunctuationManager.filter.ReplaceAll(testString, new string[0]);
				}
				else
				{
					PunctuationManager.logFilterIsNull("PunctuationManager");
				}
			}
			return result;
		}

		public static void ReplaceAllMT(out string result, string testString)
		{
			result = PunctuationManager.ReplaceAll(testString);
		}

		public static string Replace(string text, List<string> punctuations)
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (PunctuationManager.filter != null)
				{
					result = PunctuationManager.filter.Replace(text, punctuations);
				}
				else
				{
					PunctuationManager.logFilterIsNull("PunctuationManager");
				}
			}
			return result;
		}

		public static string Mark(string text, List<string> punctuations, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (PunctuationManager.filter != null)
				{
					result = PunctuationManager.filter.Mark(text, punctuations, prefix, postfix);
				}
				else
				{
					PunctuationManager.logFilterIsNull("PunctuationManager");
				}
			}
			return result;
		}

		public static string Unmark(string text, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (PunctuationManager.filter != null)
				{
					result = PunctuationManager.filter.Unmark(text, prefix, postfix);
				}
				else
				{
					PunctuationManager.logFilterIsNull("PunctuationManager");
				}
			}
			return result;
		}

		private static void logFilterIsNull(string clazz)
		{
			if (!PunctuationManager.loggedFilterIsNull)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new string[]
				{
					"'filter' is null!",
					Environment.NewLine,
					"Did you add the '",
					clazz,
					"' to the current scene?"
				}));
				PunctuationManager.loggedFilterIsNull = true;
			}
		}

		[Header("Specific Settings")]
		[Tooltip("Defines the number of allowed punctuation letters in a row (default: 3).")]
		public int PunctuationCharsNumber = 3;

		private static bool initalized;

		private static PunctuationFilter filter;

		private static PunctuationManager manager;

		private static bool loggedFilterIsNull;

		private static bool loggedOnlyOneInstance;

		private const string clazz = "PunctuationManager";
	}
}
