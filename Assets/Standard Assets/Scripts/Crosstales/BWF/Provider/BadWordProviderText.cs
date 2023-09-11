using System;
using System.Collections;
using System.Collections.Generic;
using Crosstales.BWF.Model;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Provider
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_provider_1_1_bad_word_provider_text.html")]
	public class BadWordProviderText : BadWordProvider
	{
		public override void Load()
		{
			base.Load();
			if (this.Sources != null)
			{
				this.loading = true;
				if (!Helper.isEditorMode)
				{
					foreach (Source source in this.Sources)
					{
						if (source.Resource != null)
						{
							base.StartCoroutine(this.loadResource(source));
						}
						if (!string.IsNullOrEmpty(source.URL))
						{
							base.StartCoroutine(this.loadWeb(source));
						}
					}
				}
			}
		}

		public override void Save()
		{
			UnityEngine.Debug.LogWarning("Save not implemented!");
		}

		private IEnumerator loadWeb(Source src)
		{
			string uid = Guid.NewGuid().ToString();
			this.coRoutines.Add(uid);
			if (!string.IsNullOrEmpty(src.URL))
			{
				using (WWW www = new WWW(src.URL.Trim()))
				{
					do
					{
						yield return www;
					}
					while (!www.isDone);
					if (string.IsNullOrEmpty(www.error) && !string.IsNullOrEmpty(www.text))
					{
						List<string> list = Helper.SplitStringToLines(www.text, 0, 0, '#');
						yield return null;
						if (list.Count > 0)
						{
							this.badwords.Add(new BadWords(src, list));
						}
						else
						{
							UnityEngine.Debug.LogWarning("Source: '" + src.URL + "' does not contain any active bad words!");
						}
					}
					else
					{
						UnityEngine.Debug.LogWarning(string.Concat(new string[]
						{
							"Could not load source: '",
							src.URL,
							"'",
							Environment.NewLine,
							www.error,
							Environment.NewLine,
							"Did you set the correct 'URL'?"
						}));
					}
				}
			}
			else
			{
				UnityEngine.Debug.LogWarning("'URL' is null or empty!" + Environment.NewLine + "Please add a valid URL.");
			}
			this.coRoutines.Remove(uid);
			if (this.loading && this.coRoutines.Count == 0)
			{
				this.loading = false;
				this.init();
			}
			yield break;
		}

		private IEnumerator loadResource(Source src)
		{
			string uid = Guid.NewGuid().ToString();
			this.coRoutines.Add(uid);
			if (src.Resource != null)
			{
				List<string> list = Helper.SplitStringToLines(src.Resource.text, 0, 0, '#');
				yield return null;
				if (list.Count > 0)
				{
					this.badwords.Add(new BadWords(src, list));
				}
				else
				{
					UnityEngine.Debug.LogWarning("Resource: '" + src.Resource + "' does not contain any active bad words!");
				}
			}
			else
			{
				UnityEngine.Debug.LogWarning("Resource field 'Source' is null or empty!" + Environment.NewLine + "Please add a valid resource.");
			}
			this.coRoutines.Remove(uid);
			if (this.loading && this.coRoutines.Count == 0)
			{
				this.loading = false;
				this.init();
			}
			yield break;
		}
	}
}
