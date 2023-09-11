using System;
using System.Collections.Generic;
using Crosstales.BWF.Filter;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Manager
{
	[DisallowMultipleComponent]
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_manager_1_1_capitalization_manager.html")]
	public class CapitalizationManager : BaseManager
	{
		public static int CharacterNumber
		{
			get
			{
				if (CapitalizationManager.filter != null)
				{
					return CapitalizationManager.filter.CharacterNumber;
				}
				if (CapitalizationManager.manager != null)
				{
					return CapitalizationManager.manager.CapitalizationCharsNumber;
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
				if (CapitalizationManager.filter != null)
				{
					CapitalizationManager.filter.CharacterNumber = num;
					CapitalizationManager.manager.CapitalizationCharsNumber = num;
				}
				else if (CapitalizationManager.manager != null)
				{
					CapitalizationManager.manager.CapitalizationCharsNumber = num;
				}
			}
		}

		public static bool isReady
		{
			get
			{
				return CapitalizationManager.filter.isReady;
			}
		}

		public void OnEnable()
		{
			if (Helper.isEditorMode || !CapitalizationManager.initalized)
			{
				CapitalizationManager.manager = this;
				CapitalizationManager.Load();
				if (!Helper.isEditorMode && this.DontDestroy)
				{
					UnityEngine.Object.DontDestroyOnLoad(base.transform.root.gameObject);
					CapitalizationManager.initalized = true;
				}
			}
			else if (!Helper.isEditorMode && this.DontDestroy && CapitalizationManager.manager != this)
			{
				if (!CapitalizationManager.loggedOnlyOneInstance)
				{
					CapitalizationManager.loggedOnlyOneInstance = true;
					UnityEngine.Debug.LogWarning("Only one active instance of 'CapitalizationManager' allowed in all scenes!" + Environment.NewLine + "This object will now be destroyed.");
				}
				UnityEngine.Object.Destroy(base.transform.root.gameObject, 0.2f);
			}
		}

		public void OnValidate()
		{
			if (this.CapitalizationCharsNumber < 2)
			{
				this.CapitalizationCharsNumber = 2;
			}
		}

		public static void Load()
		{
			if (CapitalizationManager.manager != null)
			{
				CapitalizationManager.filter = new CapitalizationFilter(CapitalizationManager.manager.CapitalizationCharsNumber, CapitalizationManager.manager.MarkPrefix, CapitalizationManager.manager.MarkPostfix);
			}
		}

		public static bool Contains(string testString)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(testString))
			{
				if (CapitalizationManager.filter != null)
				{
					result = CapitalizationManager.filter.Contains(testString, new string[0]);
				}
				else
				{
					CapitalizationManager.logFilterIsNull("CapitalizationManager");
				}
			}
			return result;
		}

		public static void ContainsMT(out bool result, string testString)
		{
			result = CapitalizationManager.Contains(testString);
		}

		public static List<string> GetAll(string testString)
		{
			List<string> result = new List<string>();
			if (!string.IsNullOrEmpty(testString))
			{
				if (CapitalizationManager.filter != null)
				{
					result = CapitalizationManager.filter.GetAll(testString, new string[0]);
				}
				else
				{
					CapitalizationManager.logFilterIsNull("CapitalizationManager");
				}
			}
			return result;
		}

		public static void GetAllMT(out List<string> result, string testString)
		{
			result = CapitalizationManager.GetAll(testString);
		}

		public static string ReplaceAll(string testString)
		{
			string result = testString;
			if (!string.IsNullOrEmpty(testString))
			{
				if (CapitalizationManager.filter != null)
				{
					result = CapitalizationManager.filter.ReplaceAll(testString, new string[0]);
				}
				else
				{
					CapitalizationManager.logFilterIsNull("CapitalizationManager");
				}
			}
			return result;
		}

		public static void ReplaceAllMT(out string result, string testString)
		{
			result = CapitalizationManager.ReplaceAll(testString);
		}

		public static string Replace(string text, List<string> capitalWords)
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (CapitalizationManager.filter != null)
				{
					result = CapitalizationManager.filter.Replace(text, capitalWords);
				}
				else
				{
					CapitalizationManager.logFilterIsNull("CapitalizationManager");
				}
			}
			return result;
		}

		public static string Mark(string text, List<string> capitalWords, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (CapitalizationManager.filter != null)
				{
					result = CapitalizationManager.filter.Mark(text, capitalWords, prefix, postfix);
				}
				else
				{
					CapitalizationManager.logFilterIsNull("CapitalizationManager");
				}
			}
			return result;
		}

		public static string Unmark(string text, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (CapitalizationManager.filter != null)
				{
					result = CapitalizationManager.filter.Unmark(text, prefix, postfix);
				}
				else
				{
					CapitalizationManager.logFilterIsNull("CapitalizationManager");
				}
			}
			return result;
		}

		private static void logFilterIsNull(string clazz)
		{
			if (!CapitalizationManager.loggedFilterIsNull)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new string[]
				{
					"'filter' is null!",
					Environment.NewLine,
					"Did you add the '",
					clazz,
					"' to the current scene?"
				}));
				CapitalizationManager.loggedFilterIsNull = true;
			}
		}

		[Header("Specific Settings")]
		[Tooltip("Defines the number of allowed capital letters in a row. (default: 3).")]
		public int CapitalizationCharsNumber = 3;

		private static bool initalized;

		private static CapitalizationFilter filter;

		private static CapitalizationManager manager;

		private static bool loggedFilterIsNull;

		private static bool loggedOnlyOneInstance;

		private const string clazz = "CapitalizationManager";
	}
}
