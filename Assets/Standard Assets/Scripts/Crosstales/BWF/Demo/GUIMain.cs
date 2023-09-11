using System;
using System.Collections.Generic;
using Crosstales.BWF.Manager;
using Crosstales.BWF.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.BWF.Demo
{
	[HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_demo_1_1_g_u_i_main.html")]
	public class GUIMain : MonoBehaviour
	{
		public void Start()
		{
			BadWordManager.isReplaceLeetSpeak = this.ReplaceLeet;
			if (!this.ReplaceLeet && this.LeetReplace != null)
			{
				this.LeetReplace.isOn = false;
			}
			BadWordManager.isSimpleCheck = this.SimpleCheck;
			if (!this.SimpleCheck && this.SimpleCheckToggle != null)
			{
				this.SimpleCheckToggle.isOn = false;
			}
		}

		public void Update()
		{
			this.elapsedTimeCheck += Time.deltaTime;
			this.elapsedTimeReplace += Time.deltaTime;
			if (this.AutoTest && !this.AutoReplace && this.elapsedTimeCheck > this.IntervalCheck)
			{
				this.Test();
				this.elapsedTimeCheck = 0f;
			}
			if (this.AutoReplace && this.elapsedTimeReplace > this.IntervalReplace)
			{
				this.Replace();
				this.elapsedTimeReplace = 0f;
			}
			if (this.BadwordReplaceChars != null)
			{
				BadWordManager.ReplaceCharacters = this.BadwordReplaceChars.text;
			}
			if (this.DomainReplaceChars != null)
			{
				DomainManager.ReplaceCharacters = this.DomainReplaceChars.text;
			}
			if (this.CapsTrigger != null)
			{
				int num;
				bool flag = int.TryParse(this.CapsTrigger.text, out num);
				CapitalizationManager.CharacterNumber = ((!flag) ? 2 : ((num <= 2) ? 2 : num));
				this.CapsTrigger.text = CapitalizationManager.CharacterNumber.ToString();
			}
			if (this.PuncTrigger != null)
			{
				int num;
				bool flag = int.TryParse(this.PuncTrigger.text, out num);
				PunctuationManager.CharacterNumber = ((!flag) ? 2 : ((num <= 2) ? 2 : num));
				this.PuncTrigger.text = PunctuationManager.CharacterNumber.ToString();
			}
			if (this.tested)
			{
				if (this.badWords.Count > 0)
				{
					this.BadWordList.text = string.Join(Environment.NewLine, this.badWords.ToArray());
					this.BadWordListImage.color = this.BadColor;
				}
				else
				{
					this.BadWordList.text = "No bad words found";
					this.BadWordListImage.color = this.GoodColor;
				}
			}
			if (this.BadWordCounter != null)
			{
				this.BadWordCounter.text = this.badWords.Count.ToString();
			}
			if (this.OutputText != null)
			{
				this.OutputText.text = BWFManager.Mark(this.Text.text, this.badWords, "<b><color=red>", "</color></b>");
			}
		}

		public void TestChanged(bool val)
		{
			this.AutoTest = val;
		}

		public void ReplaceChanged(bool val)
		{
			this.AutoReplace = val;
		}

		public void BadwordChanged(bool val)
		{
			this.BadwordManager = ((!val) ? ManagerMask.None : ManagerMask.BadWord);
		}

		public void DomainChanged(bool val)
		{
			this.DomManager = ((!val) ? ManagerMask.None : ManagerMask.Domain);
		}

		public void CapitalizationChanged(bool val)
		{
			this.CapsManager = ((!val) ? ManagerMask.None : ManagerMask.Capitalization);
		}

		public void PunctuationChanged(bool val)
		{
			this.PuncManager = ((!val) ? ManagerMask.None : ManagerMask.Punctuation);
		}

		public void LeetChanged(bool val)
		{
			BadWordManager.isReplaceLeetSpeak = val;
		}

		public void SimpleChanged(bool val)
		{
			BadWordManager.isSimpleCheck = val;
		}

		public void FullscreenChanged(bool val)
		{
			Screen.fullScreen = val;
		}

		public void Test()
		{
			this.tested = true;
			this.badWords = BWFManager.GetAll(this.Text.text, this.BadwordManager | this.DomManager | this.CapsManager | this.PuncManager, this.Sources.ToArray());
		}

		public void Replace()
		{
			this.tested = true;
			string text = "fuck it";
			text = BWFManager.ReplaceAll(text, ManagerMask.All, new string[0]);
			MonoBehaviour.print(text);
		}

		public void OpenAssetURL()
		{
			Application.OpenURL("https://www.assetstore.unity3d.com/#!/list/42213-crosstales?aid=1011lNGT&pubref=BWF PRO");
		}

		public void OpenCTURL()
		{
			Application.OpenURL("https://www.crosstales.com");
		}

		public void Quit()
		{
			if (!Application.isEditor)
			{
				Application.Quit();
			}
		}

		public bool AutoTest = true;

		public bool AutoReplace;

		public bool ReplaceLeet = true;

		public bool SimpleCheck = true;

		public float IntervalCheck = 0.5f;

		public float IntervalReplace = 0.5f;

		public InputField Text;

		public Text OutputText;

		public Text BadWordList;

		public Text BadWordCounter;

		public Text Name;

		public Text Version;

		public Text Scene;

		public Toggle TestEnabled;

		public Toggle ReplaceEnabled;

		public Toggle Badword;

		public Toggle Domain;

		public Toggle Capitalization;

		public Toggle Punctuation;

		public InputField BadwordReplaceChars;

		public InputField DomainReplaceChars;

		public InputField CapsTrigger;

		public InputField PuncTrigger;

		public Toggle LeetReplace;

		public Toggle SimpleCheckToggle;

		public Image BadWordListImage;

		public Color32 GoodColor = new Color32(0, byte.MaxValue, 0, 192);

		public Color32 BadColor = new Color32(byte.MaxValue, 0, 0, 192);

		public ManagerMask BadwordManager = ManagerMask.BadWord;

		public ManagerMask DomManager = ManagerMask.Domain;

		public ManagerMask CapsManager = ManagerMask.Capitalization;

		public ManagerMask PuncManager = ManagerMask.Punctuation;

		public List<string> Sources = new List<string>(30);

		private List<string> badWords = new List<string>();

		private float elapsedTimeCheck;

		private float elapsedTimeReplace;

		private bool tested;
	}
}
