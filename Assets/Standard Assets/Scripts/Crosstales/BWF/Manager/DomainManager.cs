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
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_manager_1_1_domain_manager.html")]
	public class DomainManager : BaseManager
	{
		public static string ReplaceCharacters
		{
			get
			{
				if (DomainManager.filter != null)
				{
					return DomainManager.filter.ReplaceCharacters;
				}
				if (DomainManager.manager != null)
				{
					return DomainManager.manager.ReplaceChars;
				}
				return "*";
			}
			set
			{
				if (DomainManager.filter != null)
				{
					DomainManager.filter.ReplaceCharacters = value;
					DomainManager.manager.ReplaceChars = value;
				}
				else if (DomainManager.manager != null)
				{
					DomainManager.manager.ReplaceChars = value;
				}
			}
		}

		public static bool isReady
		{
			get
			{
				bool result = false;
				if (DomainManager.filter != null)
				{
					result = DomainManager.filter.isReady;
				}
				else
				{
					DomainManager.logFilterIsNull("DomainManager");
				}
				return result;
			}
		}

		public static List<Source> Sources
		{
			get
			{
				List<Source> result = new List<Source>();
				if (DomainManager.filter != null)
				{
					result = DomainManager.filter.Sources;
				}
				else
				{
					DomainManager.logFilterIsNull("DomainManager");
				}
				return result;
			}
		}

		public void OnEnable()
		{
			if (Helper.isEditorMode || !DomainManager.initalized)
			{
				DomainManager.manager = this;
				DomainManager.Load();
				if (!Helper.isEditorMode && this.DontDestroy)
				{
					UnityEngine.Object.DontDestroyOnLoad(base.transform.root.gameObject);
					DomainManager.initalized = true;
				}
			}
			else if (!Helper.isEditorMode && this.DontDestroy && DomainManager.manager != this)
			{
				if (!DomainManager.loggedOnlyOneInstance)
				{
					DomainManager.loggedOnlyOneInstance = true;
					UnityEngine.Debug.LogWarning("Only one active instance of 'DomainManager' allowed in all scenes!" + Environment.NewLine + "This object will now be destroyed.");
				}
				UnityEngine.Object.Destroy(base.transform.root.gameObject, 0.2f);
			}
		}

		public static void Load()
		{
			if (DomainManager.manager != null)
			{
				DomainManager.filter = new DomainFilter(DomainManager.manager.DomainProvider, DomainManager.manager.ReplaceChars, DomainManager.manager.MarkPrefix, DomainManager.manager.MarkPostfix);
			}
		}

		public static bool Contains(string testString, params string[] sources)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(testString))
			{
				if (DomainManager.filter != null)
				{
					result = DomainManager.filter.Contains(testString, sources);
				}
				else
				{
					DomainManager.logFilterIsNull("DomainManager");
				}
			}
			return result;
		}

		public static void ContainsMT(out bool result, string testString, params string[] sources)
		{
			result = DomainManager.Contains(testString, sources);
		}

		public static List<string> GetAll(string testString, params string[] sources)
		{
			List<string> result = new List<string>();
			if (!string.IsNullOrEmpty(testString))
			{
				if (DomainManager.filter != null)
				{
					result = DomainManager.filter.GetAll(testString, sources);
				}
				else
				{
					DomainManager.logFilterIsNull("DomainManager");
				}
			}
			return result;
		}

		public static void GetAllMT(out List<string> result, string testString, params string[] sources)
		{
			result = DomainManager.GetAll(testString, sources);
		}

		public static string ReplaceAll(string testString, params string[] sources)
		{
			string result = testString;
			if (!string.IsNullOrEmpty(testString))
			{
				if (DomainManager.filter != null)
				{
					result = DomainManager.filter.ReplaceAll(testString, sources);
				}
				else
				{
					DomainManager.logFilterIsNull("DomainManager");
				}
			}
			return result;
		}

		public static void ReplaceAllMT(out string result, string testString, params string[] sources)
		{
			result = DomainManager.ReplaceAll(testString, sources);
		}

		public static string Replace(string text, List<string> domains)
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (DomainManager.filter != null)
				{
					result = DomainManager.filter.Replace(text, domains);
				}
				else
				{
					DomainManager.logFilterIsNull("DomainManager");
				}
			}
			return result;
		}

		public static string Mark(string text, List<string> domains, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (DomainManager.filter != null)
				{
					result = DomainManager.filter.Mark(text, domains, prefix, postfix);
				}
				else
				{
					DomainManager.logFilterIsNull("DomainManager");
				}
			}
			return result;
		}

		public static string Unmark(string text, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string result = text;
			if (!string.IsNullOrEmpty(text))
			{
				if (DomainManager.filter != null)
				{
					result = DomainManager.filter.Unmark(text, prefix, postfix);
				}
				else
				{
					DomainManager.logFilterIsNull("DomainManager");
				}
			}
			return result;
		}

		private static void logFilterIsNull(string clazz)
		{
			if (!DomainManager.loggedFilterIsNull)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new string[]
				{
					"'filter' is null!",
					Environment.NewLine,
					"Did you add the '",
					clazz,
					"' to the current scene?"
				}));
				DomainManager.loggedFilterIsNull = true;
			}
		}

		[Header("Specific Settings")]
		[Tooltip("Replace characters for domains (default: *).")]
		public string ReplaceChars = "*";

		[Header("Domain Providers")]
		[Tooltip("List of all domain providers.")]
		public List<DomainProvider> DomainProvider;

		private static bool initalized;

		private static DomainFilter filter;

		private static DomainManager manager;

		private static bool loggedFilterIsNull;

		private static bool loggedOnlyOneInstance;

		private const string clazz = "DomainManager";
	}
}
