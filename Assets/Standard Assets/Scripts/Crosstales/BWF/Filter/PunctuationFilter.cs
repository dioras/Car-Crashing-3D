using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Filter
{
	public class PunctuationFilter : BaseFilter
	{
		public PunctuationFilter(int punctuationCharacterNumber, string markPrefix, string markPostfix)
		{
			this.CharacterNumber = punctuationCharacterNumber;
			this.MarkPrefix = markPrefix;
			this.MarkPostfix = markPostfix;
		}

		public Regex RegularExpression { get; private set; }

		public int CharacterNumber
		{
			get
			{
				return this.characterNumber;
			}
			set
			{
				if (value < 2)
				{
					this.characterNumber = 2;
				}
				else
				{
					this.characterNumber = value;
				}
				this.RegularExpression = new Regex("[?!,.;:-]{" + (this.characterNumber + 1) + ",}", RegexOptions.CultureInvariant);
			}
		}

		public override bool isReady
		{
			get
			{
				return true;
			}
		}

		public override bool Contains(string testString, params string[] sources)
		{
			bool result = false;
			if (string.IsNullOrEmpty(testString))
			{
				base.logContains();
			}
			else
			{
				result = this.RegularExpression.Match(testString).Success;
			}
			return result;
		}

		public override List<string> GetAll(string testString, params string[] sources)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(testString))
			{
				base.logGetAll();
			}
			else
			{
				MatchCollection matchCollection = this.RegularExpression.Matches(testString);
				IEnumerator enumerator = matchCollection.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Match match = (Match)obj;
						IEnumerator enumerator2 = match.Captures.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								Capture capture = (Capture)obj2;
								if (Constants.DEV_DEBUG)
								{
									UnityEngine.Debug.Log("Test string contains an excessive punctuation: '" + capture.Value + "'");
								}
								if (!list.Contains(capture.Value))
								{
									list.Add(capture.Value);
								}
							}
						}
						finally
						{
							IDisposable disposable;
							if ((disposable = (enumerator2 as IDisposable)) != null)
							{
								disposable.Dispose();
							}
						}
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
			}
			return (from x in list.Distinct<string>()
			orderby x
			select x).ToList<string>();
		}

		public override string ReplaceAll(string testString, params string[] sources)
		{
			string text = testString;
			if (string.IsNullOrEmpty(testString))
			{
				base.logReplaceAll();
				text = string.Empty;
			}
			else
			{
				MatchCollection matchCollection = this.RegularExpression.Matches(testString);
				IEnumerator enumerator = matchCollection.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Match match = (Match)obj;
						IEnumerator enumerator2 = match.Captures.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								Capture capture = (Capture)obj2;
								if (Constants.DEV_DEBUG)
								{
									UnityEngine.Debug.Log("Test string contains an excessive punctuation: '" + capture.Value + "'");
								}
								text = text.Replace(capture.Value, capture.Value.Substring(0, this.characterNumber));
							}
						}
						finally
						{
							IDisposable disposable;
							if ((disposable = (enumerator2 as IDisposable)) != null)
							{
								disposable.Dispose();
							}
						}
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
			}
			return text;
		}

		public override string Replace(string text, List<string> badWords)
		{
			string text2 = text;
			if (string.IsNullOrEmpty(text))
			{
				base.logReplace();
				text2 = string.Empty;
			}
			else if (badWords == null || badWords.Count == 0)
			{
				UnityEngine.Debug.LogWarning("Parameter 'badWords' is null or empty!" + Environment.NewLine + "=> 'Replace()' will return the original string.");
			}
			else
			{
				foreach (string text3 in badWords)
				{
					text2 = text2.Replace(text3, text3.Substring(0, this.characterNumber));
				}
			}
			return text2;
		}

		private int characterNumber;
	}
}
