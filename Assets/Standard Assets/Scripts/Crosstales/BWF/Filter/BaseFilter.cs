using System;
using System.Collections.Generic;
using System.Linq;
using Crosstales.BWF.Model;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Filter
{
	public abstract class BaseFilter
	{
		public virtual List<Source> Sources
		{
			get
			{
				List<Source> result = new List<Source>();
				if (this.isReady)
				{
					result = (from x in this.sources
					orderby x.Key
					select x into y
					select y.Value).ToList<Source>();
				}
				else
				{
					this.logFilterNotReady();
				}
				return result;
			}
		}

		public abstract bool isReady { get; }

		public abstract bool Contains(string testString, params string[] sources);

		public abstract List<string> GetAll(string testString, params string[] sources);

		public abstract string ReplaceAll(string testString, params string[] sources);

		public abstract string Replace(string text, List<string> badWords);

		public virtual string Mark(string text, List<string> badWords, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string text2 = text;
			string str = prefix;
			string str2 = postfix;
			if (string.IsNullOrEmpty(text))
			{
				if (Constants.DEV_DEBUG)
				{
					UnityEngine.Debug.LogWarning("Parameter 'text' is null or empty!" + Environment.NewLine + "=> 'Mark()' will return an empty string.");
				}
				text2 = string.Empty;
			}
			else
			{
				if (string.IsNullOrEmpty(prefix))
				{
					if (Constants.DEV_DEBUG)
					{
						UnityEngine.Debug.LogWarning("Parameter 'prefix' is null!" + Environment.NewLine + "=> Using an empty string as prefix.");
					}
					str = (this.MarkPrefix ?? string.Empty);
				}
				if (string.IsNullOrEmpty(postfix))
				{
					if (Constants.DEV_DEBUG)
					{
						UnityEngine.Debug.LogWarning("Parameter 'postfix' is null!" + Environment.NewLine + "=> Using an empty string as postfix.");
					}
					str2 = (this.MarkPostfix ?? string.Empty);
				}
				if (badWords == null || badWords.Count == 0)
				{
					if (Constants.DEV_DEBUG)
					{
						UnityEngine.Debug.LogWarning("Parameter 'badWords' is null or empty!" + Environment.NewLine + "=> 'Mark()' will return the original string.");
					}
				}
				else
				{
					foreach (string text3 in badWords)
					{
						if (!string.IsNullOrEmpty(text3))
						{
							text2 = text2.Replace(text3, str + text3 + str2);
						}
					}
				}
			}
			return text2;
		}

		public virtual string Unmark(string text, string prefix = "<b><color=red>", string postfix = "</color></b>")
		{
			string oldValue = prefix;
			string oldValue2 = postfix;
			string text2;
			if (string.IsNullOrEmpty(text))
			{
				if (Constants.DEV_DEBUG)
				{
					UnityEngine.Debug.LogWarning("Parameter 'text' is null or empty!" + Environment.NewLine + "=> 'Unmark()' will return an empty string.");
				}
				text2 = string.Empty;
			}
			else
			{
				if (string.IsNullOrEmpty(prefix))
				{
					if (Constants.DEV_DEBUG)
					{
						UnityEngine.Debug.LogWarning("Parameter 'prefix' is null!" + Environment.NewLine + "=> Using an empty string as prefix.");
					}
					oldValue = (this.MarkPrefix ?? string.Empty);
				}
				if (string.IsNullOrEmpty(postfix))
				{
					if (Constants.DEV_DEBUG)
					{
						UnityEngine.Debug.LogWarning("Parameter 'postfix' is null!" + Environment.NewLine + "=> Using an empty string as postfix.");
					}
					oldValue2 = (this.MarkPostfix ?? string.Empty);
				}
				text2 = text.Replace(oldValue, string.Empty);
				text2 = text2.Replace(oldValue2, string.Empty);
			}
			return text2;
		}

		protected void logFilterNotReady()
		{
			UnityEngine.Debug.LogWarning("Filter is not ready - please wait until 'isReady' returns true.");
		}

		protected void logResourceNotFound(string res)
		{
			if (Constants.DEV_DEBUG)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new string[]
				{
					"Resource not found: '",
					res,
					"'",
					Environment.NewLine,
					"Did you call the method with the correct resource name?"
				}));
			}
		}

		protected void logContains()
		{
			if (Constants.DEV_DEBUG)
			{
				UnityEngine.Debug.LogWarning("Parameter 'testString' is null or empty!" + Environment.NewLine + "=> 'Contains()' will return 'false'.");
			}
		}

		protected void logGetAll()
		{
			if (Constants.DEV_DEBUG)
			{
				UnityEngine.Debug.LogWarning("Parameter 'testString' is null or empty!" + Environment.NewLine + "=> 'GetAll()' will return an empty list.");
			}
		}

		protected void logReplaceAll()
		{
			if (Constants.DEV_DEBUG)
			{
				UnityEngine.Debug.LogWarning("Parameter 'testString' is null or empty!" + Environment.NewLine + "=> 'ReplaceAll()' will return an empty string.");
			}
		}

		protected void logReplace()
		{
			if (Constants.DEV_DEBUG)
			{
				UnityEngine.Debug.LogWarning("Parameter 'text' is null or empty!" + Environment.NewLine + "=> 'Replace()' will return an empty string.");
			}
		}

		public string MarkPrefix = "<color=red>";

		public string MarkPostfix = "</color>";

		protected Dictionary<string, Source> sources = new Dictionary<string, Source>();
	}
}
