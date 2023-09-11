using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Crosstales.BWF.Model;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Provider
{
	public abstract class BadWordProvider : BaseProvider
	{
		public Dictionary<string, Regex> ExactBadwordsRegex
		{
			get
			{
				return this.exactBadwordsRegex;
			}
			protected set
			{
				this.exactBadwordsRegex = value;
			}
		}

		public Dictionary<string, List<Regex>> DebugExactBadwordsRegex
		{
			get
			{
				return this.debugExactBadwordsRegex;
			}
			protected set
			{
				this.debugExactBadwordsRegex = value;
			}
		}

		public Dictionary<string, List<string>> SimpleBadwords
		{
			get
			{
				return this.simpleBadwords;
			}
			protected set
			{
				this.simpleBadwords = value;
			}
		}

		public override void Load()
		{
			if (this.ClearOnLoad)
			{
				this.badwords.Clear();
			}
		}

		protected override void init()
		{
			this.ExactBadwordsRegex.Clear();
			this.DebugExactBadwordsRegex.Clear();
			this.SimpleBadwords.Clear();
			if (Config.DEBUG_BADWORDS)
			{
				UnityEngine.Debug.Log("++ BadWordProvider '" + this.Name + "' started in debug-mode ++");
			}
			foreach (BadWords badWords in this.badwords)
			{
				if (Config.DEBUG_BADWORDS)
				{
					try
					{
						List<Regex> list = new List<Regex>(badWords.BadWordList.Count);
						foreach (string str in badWords.BadWordList)
						{
							list.Add(new Regex("(?<![\\w\\d])" + str + "s?(?![\\w\\d])", this.RegexOption1 | this.RegexOption2 | this.RegexOption3 | this.RegexOption4 | this.RegexOption5));
						}
						if (!this.DebugExactBadwordsRegex.ContainsKey(badWords.Source.Name))
						{
							this.DebugExactBadwordsRegex.Add(badWords.Source.Name, list);
						}
					}
					catch (Exception ex)
					{
						UnityEngine.Debug.LogError(string.Concat(new object[]
						{
							"Could not generate debug regex for source '",
							badWords.Source.Name,
							"': ",
							ex
						}));
						if (Constants.DEV_DEBUG)
						{
							UnityEngine.Debug.Log(badWords.BadWordList.CTDump<string>());
						}
					}
				}
				else
				{
					try
					{
						if (!this.ExactBadwordsRegex.ContainsKey(badWords.Source.Name))
						{
							this.ExactBadwordsRegex.Add(badWords.Source.Name, new Regex("(?<![\\w\\d])(" + string.Join("|", badWords.BadWordList.ToArray()) + ")s?(?![\\w\\d])", this.RegexOption1 | this.RegexOption2 | this.RegexOption3 | this.RegexOption4 | this.RegexOption5));
						}
					}
					catch (Exception ex2)
					{
						UnityEngine.Debug.LogError(string.Concat(new object[]
						{
							"Could not generate exact regex for source '",
							badWords.Source.Name,
							"': ",
							ex2
						}));
						if (Constants.DEV_DEBUG)
						{
							UnityEngine.Debug.Log(badWords.BadWordList.CTDump<string>());
						}
					}
				}
				List<string> list2 = new List<string>(badWords.BadWordList.Count);
				list2.AddRange(badWords.BadWordList);
				if (!this.SimpleBadwords.ContainsKey(badWords.Source.Name))
				{
					this.SimpleBadwords.Add(badWords.Source.Name, list2);
				}
				if (Config.DEBUG_BADWORDS)
				{
					UnityEngine.Debug.Log(string.Concat(new object[]
					{
						"Bad word resource '",
						badWords.Source.Name,
						"' loaded and ",
						badWords.BadWordList.Count,
						" entries found."
					}));
				}
			}
			base.isReady = true;
		}

		protected List<BadWords> badwords = new List<BadWords>();

		private const string exactRegexStart = "(?<![\\w\\d])";

		private const string exactRegexEnd = "s?(?![\\w\\d])";

		private Dictionary<string, Regex> exactBadwordsRegex = new Dictionary<string, Regex>();

		private Dictionary<string, List<Regex>> debugExactBadwordsRegex = new Dictionary<string, List<Regex>>();

		private Dictionary<string, List<string>> simpleBadwords = new Dictionary<string, List<string>>();
	}
}
