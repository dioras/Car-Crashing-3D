using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Crosstales.BWF.Model;
using Crosstales.BWF.Provider;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Filter
{
	public class BadWordFilter : BaseFilter
	{
		public BadWordFilter(List<BadWordProvider> badWordProviderLTR, List<BadWordProvider> badWordProviderRTL, string replaceCharacters, bool leetSpeak, bool simpleCheck, string markPrefix, string markPostfix)
		{
			this.tempBadWordProviderLTR = badWordProviderLTR;
			this.tempBadWordProviderRTL = badWordProviderRTL;
			this.ReplaceCharacters = replaceCharacters;
			this.ReplaceLeetSpeak = leetSpeak;
			this.SimpleCheck = simpleCheck;
			this.MarkPrefix = markPrefix;
			this.MarkPostfix = markPostfix;
		}

		public List<BadWordProvider> BadWordProviderLTR
		{
			get
			{
				return this.badWordProviderLTR;
			}
			set
			{
				this.badWordProviderLTR = value;
				if (this.badWordProviderLTR != null && this.badWordProviderLTR.Count > 0)
				{
					foreach (BadWordProvider badWordProvider in this.badWordProviderLTR)
					{
						if (badWordProvider != null)
						{
							if (Config.DEBUG_BADWORDS)
							{
								this.debugExactBadwordsRegex.CTAddRange(badWordProvider.DebugExactBadwordsRegex);
							}
							else
							{
								this.exactBadwordsRegex.CTAddRange(badWordProvider.ExactBadwordsRegex);
							}
							this.simpleBadwords.CTAddRange(badWordProvider.SimpleBadwords);
						}
						else if (!Helper.isEditorMode)
						{
							UnityEngine.Debug.LogError("A LTR-BadWordProvider is null!");
						}
					}
				}
				else
				{
					this.badWordProviderLTR = new List<BadWordProvider>();
					if (!Helper.isEditorMode)
					{
						UnityEngine.Debug.LogWarning("No 'BadWordProviderLTR' added!" + Environment.NewLine + "If you want to use this functionality, please add your desired 'BadWordProviderLTR' in the editor or script.");
					}
				}
			}
		}

		public List<BadWordProvider> BadWordProviderRTL
		{
			get
			{
				return this.badWordProviderRTL;
			}
			set
			{
				this.badWordProviderRTL = value;
				if (this.badWordProviderRTL != null && this.badWordProviderRTL.Count > 0)
				{
					foreach (BadWordProvider badWordProvider in this.badWordProviderRTL)
					{
						if (badWordProvider != null)
						{
							if (Config.DEBUG_BADWORDS)
							{
								this.debugExactBadwordsRegex.CTAddRange(badWordProvider.DebugExactBadwordsRegex);
							}
							else
							{
								this.exactBadwordsRegex.CTAddRange(badWordProvider.ExactBadwordsRegex);
							}
							this.simpleBadwords.CTAddRange(badWordProvider.SimpleBadwords);
						}
						else if (!Helper.isEditorMode)
						{
							UnityEngine.Debug.LogError("A RTL-BadWordProvider is null!");
						}
					}
				}
				else
				{
					this.badWordProviderRTL = new List<BadWordProvider>();
					if (!Helper.isEditorMode)
					{
						UnityEngine.Debug.LogWarning("No 'BadWordProviderRTL' added!" + Environment.NewLine + "If you want to use this functionality, please add your desired 'BadWordProviderRTL' in the editor or script.");
					}
				}
			}
		}

		public override bool isReady
		{
			get
			{
				bool flag = true;
				if (!this.ready)
				{
					if (this.tempBadWordProviderLTR != null)
					{
						foreach (BadWordProvider badWordProvider in this.tempBadWordProviderLTR)
						{
							if (badWordProvider != null && !badWordProvider.isReady)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag && this.tempBadWordProviderRTL != null)
					{
						foreach (BadWordProvider badWordProvider2 in this.tempBadWordProviderRTL)
						{
							if (badWordProvider2 != null && !badWordProvider2.isReady)
							{
								flag = false;
								break;
							}
						}
					}
					if (!this.readyFirstime && flag)
					{
						this.BadWordProviderLTR = this.tempBadWordProviderLTR;
						this.BadWordProviderRTL = this.tempBadWordProviderRTL;
						if (this.BadWordProviderLTR != null)
						{
							foreach (BadWordProvider badWordProvider3 in this.BadWordProviderLTR)
							{
								if (badWordProvider3 != null)
								{
									foreach (Source source in badWordProvider3.Sources)
									{
										if (!this.sources.ContainsKey(source.Name))
										{
											this.sources.Add(source.Name, source);
										}
									}
								}
							}
						}
						if (this.BadWordProviderRTL != null)
						{
							foreach (BadWordProvider badWordProvider4 in this.BadWordProviderRTL)
							{
								if (badWordProvider4 != null)
								{
									foreach (Source source2 in badWordProvider4.Sources)
									{
										if (!this.sources.ContainsKey(source2.Name))
										{
											this.sources.Add(source2.Name, source2);
										}
									}
								}
							}
						}
						this.readyFirstime = true;
					}
				}
				this.ready = flag;
				return flag;
			}
		}

		public override bool Contains(string testString, params string[] sources)
		{
			bool flag = false;
			if (this.isReady)
			{
				if (string.IsNullOrEmpty(testString))
				{
					base.logContains();
				}
				else
				{
					string text = this.replaceLeetSpeak(testString);
					if (Config.DEBUG_BADWORDS)
					{
						if (sources == null || sources.Length == 0)
						{
							if (this.SimpleCheck)
							{
								foreach (List<string> list in this.simpleBadwords.Values)
								{
									foreach (string text2 in list)
									{
										if (text.CTContains(text2, StringComparison.OrdinalIgnoreCase))
										{
											UnityEngine.Debug.Log("Test string contains a bad word detected by word '" + text2 + "'");
											flag = true;
											break;
										}
									}
									if (flag)
									{
										break;
									}
								}
							}
							else
							{
								foreach (List<Regex> list2 in this.debugExactBadwordsRegex.Values)
								{
									foreach (Regex regex in list2)
									{
										Match match = regex.Match(text);
										if (match.Success)
										{
											UnityEngine.Debug.Log(string.Concat(new object[]
											{
												"Test string contains a bad word: '",
												match.Value,
												"' detected by regex '",
												regex,
												"'"
											}));
											flag = true;
											break;
										}
										if (flag)
										{
											break;
										}
									}
								}
							}
						}
						else
						{
							int num = 0;
							while (num < sources.Length && !flag)
							{
								List<Regex> list4;
								if (this.SimpleCheck)
								{
									List<string> list3;
									if (this.simpleBadwords.TryGetValue(sources[num], out list3))
									{
										foreach (string text3 in list3)
										{
											if (text.CTContains(text3, StringComparison.OrdinalIgnoreCase))
											{
												UnityEngine.Debug.Log(string.Concat(new string[]
												{
													"Test string contains a bad word detected by word '",
													text3,
													"'' from source '",
													sources[num],
													"'"
												}));
												flag = true;
												break;
											}
										}
										if (flag)
										{
											break;
										}
									}
									else
									{
										base.logResourceNotFound(sources[num]);
									}
								}
								else if (this.debugExactBadwordsRegex.TryGetValue(sources[num], out list4))
								{
									foreach (Regex regex2 in list4)
									{
										Match match = regex2.Match(text);
										if (match.Success)
										{
											UnityEngine.Debug.Log(string.Concat(new object[]
											{
												"Test string contains a bad word: '",
												match.Value,
												"' detected by regex '",
												regex2,
												"'' from source '",
												sources[num],
												"'"
											}));
											flag = true;
											break;
										}
									}
								}
								else
								{
									base.logResourceNotFound(sources[num]);
								}
								num++;
							}
						}
					}
					else if (sources == null || sources.Length == 0)
					{
						if (this.SimpleCheck)
						{
							foreach (List<string> list5 in this.simpleBadwords.Values)
							{
								foreach (string toCheck in list5)
								{
									if (text.CTContains(toCheck, StringComparison.OrdinalIgnoreCase))
									{
										flag = true;
										break;
									}
								}
								if (flag)
								{
									break;
								}
							}
						}
						else
						{
							foreach (Regex regex3 in this.exactBadwordsRegex.Values)
							{
								if (regex3.Match(text).Success)
								{
									flag = true;
									break;
								}
							}
						}
					}
					else
					{
						foreach (string text4 in sources)
						{
							Regex regex4;
							if (this.SimpleCheck)
							{
								List<string> list6;
								if (this.simpleBadwords.TryGetValue(text4, out list6))
								{
									foreach (string toCheck2 in list6)
									{
										if (text.CTContains(toCheck2, StringComparison.OrdinalIgnoreCase))
										{
											flag = true;
											break;
										}
									}
									if (flag)
									{
										break;
									}
								}
								else
								{
									base.logResourceNotFound(text4);
								}
							}
							else if (this.exactBadwordsRegex.TryGetValue(text4, out regex4))
							{
								Match match = regex4.Match(text);
								if (match.Success)
								{
									flag = true;
									break;
								}
							}
							else
							{
								base.logResourceNotFound(text4);
							}
						}
					}
				}
			}
			else
			{
				base.logFilterNotReady();
			}
			return flag;
		}

		public override List<string> GetAll(string testString, params string[] sources)
		{
			List<string> list = new List<string>();
			if (this.isReady)
			{
				if (string.IsNullOrEmpty(testString))
				{
					base.logGetAll();
				}
				else
				{
					string text = this.replaceLeetSpeak(testString);
					if (Config.DEBUG_BADWORDS)
					{
						if (sources == null || sources.Length == 0)
						{
							if (this.SimpleCheck)
							{
								foreach (List<string> list2 in this.simpleBadwords.Values)
								{
									foreach (string text2 in list2)
									{
										if (text.CTContains(text2, StringComparison.OrdinalIgnoreCase))
										{
											UnityEngine.Debug.Log("Test string contains a bad word detected by word '" + text2 + "'");
											if (!list.Contains(text2))
											{
												list.Add(text2);
											}
										}
									}
								}
							}
							else
							{
								foreach (List<Regex> list3 in this.debugExactBadwordsRegex.Values)
								{
									foreach (Regex regex in list3)
									{
										MatchCollection matchCollection = regex.Matches(text);
										IEnumerator enumerator5 = matchCollection.GetEnumerator();
										try
										{
											while (enumerator5.MoveNext())
											{
												object obj = enumerator5.Current;
												Match match = (Match)obj;
												IEnumerator enumerator6 = match.Captures.GetEnumerator();
												try
												{
													while (enumerator6.MoveNext())
													{
														object obj2 = enumerator6.Current;
														Capture capture = (Capture)obj2;
														UnityEngine.Debug.Log(string.Concat(new object[]
														{
															"Test string contains a bad word: '",
															capture.Value,
															"' detected by regex '",
															regex,
															"'"
														}));
														if (!list.Contains(capture.Value))
														{
															list.Add(capture.Value);
														}
													}
												}
												finally
												{
													IDisposable disposable;
													if ((disposable = (enumerator6 as IDisposable)) != null)
													{
														disposable.Dispose();
													}
												}
											}
										}
										finally
										{
											IDisposable disposable2;
											if ((disposable2 = (enumerator5 as IDisposable)) != null)
											{
												disposable2.Dispose();
											}
										}
									}
								}
							}
						}
						else
						{
							foreach (string text3 in sources)
							{
								List<Regex> list5;
								if (this.SimpleCheck)
								{
									List<string> list4;
									if (this.simpleBadwords.TryGetValue(text3, out list4))
									{
										foreach (string text4 in list4)
										{
											if (text.CTContains(text4, StringComparison.OrdinalIgnoreCase))
											{
												UnityEngine.Debug.Log(string.Concat(new string[]
												{
													"Test string contains a bad word detected by word '",
													text4,
													"'' from source '",
													text3,
													"'"
												}));
												if (!list.Contains(text4))
												{
													list.Add(text4);
												}
											}
										}
									}
									else
									{
										base.logResourceNotFound(text3);
									}
								}
								else if (this.debugExactBadwordsRegex.TryGetValue(text3, out list5))
								{
									foreach (Regex regex2 in list5)
									{
										MatchCollection matchCollection2 = regex2.Matches(text);
										IEnumerator enumerator9 = matchCollection2.GetEnumerator();
										try
										{
											while (enumerator9.MoveNext())
											{
												object obj3 = enumerator9.Current;
												Match match2 = (Match)obj3;
												IEnumerator enumerator10 = match2.Captures.GetEnumerator();
												try
												{
													while (enumerator10.MoveNext())
													{
														object obj4 = enumerator10.Current;
														Capture capture2 = (Capture)obj4;
														UnityEngine.Debug.Log(string.Concat(new object[]
														{
															"Test string contains a bad word: '",
															capture2.Value,
															"' detected by regex '",
															regex2,
															"'' from source '",
															text3,
															"'"
														}));
														if (!list.Contains(capture2.Value))
														{
															list.Add(capture2.Value);
														}
													}
												}
												finally
												{
													IDisposable disposable3;
													if ((disposable3 = (enumerator10 as IDisposable)) != null)
													{
														disposable3.Dispose();
													}
												}
											}
										}
										finally
										{
											IDisposable disposable4;
											if ((disposable4 = (enumerator9 as IDisposable)) != null)
											{
												disposable4.Dispose();
											}
										}
									}
								}
								else
								{
									base.logResourceNotFound(text3);
								}
							}
						}
					}
					else if (sources == null || sources.Length == 0)
					{
						if (this.SimpleCheck)
						{
							foreach (List<string> list6 in this.simpleBadwords.Values)
							{
								foreach (string text5 in list6)
								{
									if (text.CTContains(text5, StringComparison.OrdinalIgnoreCase) && !list.Contains(text5))
									{
										list.Add(text5);
									}
								}
							}
						}
						else
						{
							foreach (Regex regex3 in this.exactBadwordsRegex.Values)
							{
								MatchCollection matchCollection3 = regex3.Matches(text);
								IEnumerator enumerator14 = matchCollection3.GetEnumerator();
								try
								{
									while (enumerator14.MoveNext())
									{
										object obj5 = enumerator14.Current;
										Match match3 = (Match)obj5;
										IEnumerator enumerator15 = match3.Captures.GetEnumerator();
										try
										{
											while (enumerator15.MoveNext())
											{
												object obj6 = enumerator15.Current;
												Capture capture3 = (Capture)obj6;
												if (!list.Contains(capture3.Value))
												{
													list.Add(capture3.Value);
												}
											}
										}
										finally
										{
											IDisposable disposable5;
											if ((disposable5 = (enumerator15 as IDisposable)) != null)
											{
												disposable5.Dispose();
											}
										}
									}
								}
								finally
								{
									IDisposable disposable6;
									if ((disposable6 = (enumerator14 as IDisposable)) != null)
									{
										disposable6.Dispose();
									}
								}
							}
						}
					}
					else
					{
						foreach (string text6 in sources)
						{
							Regex regex4;
							if (this.SimpleCheck)
							{
								List<string> list7;
								if (this.simpleBadwords.TryGetValue(text6, out list7))
								{
									foreach (string text7 in list7)
									{
										if (text.CTContains(text7, StringComparison.OrdinalIgnoreCase) && !list.Contains(text7))
										{
											list.Add(text7);
										}
									}
								}
								else
								{
									base.logResourceNotFound(text6);
								}
							}
							else if (this.exactBadwordsRegex.TryGetValue(text6, out regex4))
							{
								MatchCollection matchCollection4 = regex4.Matches(text);
								IEnumerator enumerator17 = matchCollection4.GetEnumerator();
								try
								{
									while (enumerator17.MoveNext())
									{
										object obj7 = enumerator17.Current;
										Match match4 = (Match)obj7;
										IEnumerator enumerator18 = match4.Captures.GetEnumerator();
										try
										{
											while (enumerator18.MoveNext())
											{
												object obj8 = enumerator18.Current;
												Capture capture4 = (Capture)obj8;
												if (!list.Contains(capture4.Value))
												{
													list.Add(capture4.Value);
												}
											}
										}
										finally
										{
											IDisposable disposable7;
											if ((disposable7 = (enumerator18 as IDisposable)) != null)
											{
												disposable7.Dispose();
											}
										}
									}
								}
								finally
								{
									IDisposable disposable8;
									if ((disposable8 = (enumerator17 as IDisposable)) != null)
									{
										disposable8.Dispose();
									}
								}
							}
							else
							{
								base.logResourceNotFound(text6);
							}
						}
					}
				}
			}
			else
			{
				base.logFilterNotReady();
			}
			return (from x in list.Distinct<string>()
			orderby x
			select x).ToList<string>();
		}

		public override string ReplaceAll(string testString, params string[] sources)
		{
			string text = this.replaceLeetSpeak(testString);
			bool flag = false;
			if (this.isReady)
			{
				if (string.IsNullOrEmpty(testString))
				{
					base.logReplaceAll();
					text = string.Empty;
				}
				else
				{
					string text2 = this.replaceLeetSpeak(testString);
					if (this.SimpleCheck)
					{
						foreach (string text3 in this.GetAll(text2, sources))
						{
							text2 = Regex.Replace(text2, text3, Helper.CreateReplaceString(this.ReplaceCharacters, text3.Length), RegexOptions.IgnoreCase);
							flag = true;
						}
						text = text2;
					}
					else if (Config.DEBUG_BADWORDS)
					{
						if (sources == null || sources.Length == 0)
						{
							foreach (List<Regex> list in this.debugExactBadwordsRegex.Values)
							{
								foreach (Regex regex in list)
								{
									MatchCollection matchCollection = regex.Matches(text2);
									IEnumerator enumerator4 = matchCollection.GetEnumerator();
									try
									{
										while (enumerator4.MoveNext())
										{
											object obj = enumerator4.Current;
											Match match = (Match)obj;
											IEnumerator enumerator5 = match.Captures.GetEnumerator();
											try
											{
												while (enumerator5.MoveNext())
												{
													object obj2 = enumerator5.Current;
													Capture capture = (Capture)obj2;
													UnityEngine.Debug.Log(string.Concat(new object[]
													{
														"Test string contains a bad word: '",
														capture.Value,
														"' detected by regex '",
														regex,
														"'"
													}));
													text = text.Replace(capture.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture.Value.Length));
													text = text.Replace(capture.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture.Value.Length));
													flag = true;
												}
											}
											finally
											{
												IDisposable disposable;
												if ((disposable = (enumerator5 as IDisposable)) != null)
												{
													disposable.Dispose();
												}
											}
										}
									}
									finally
									{
										IDisposable disposable2;
										if ((disposable2 = (enumerator4 as IDisposable)) != null)
										{
											disposable2.Dispose();
										}
									}
								}
							}
						}
						else
						{
							foreach (string text4 in sources)
							{
								List<Regex> list2;
								if (this.debugExactBadwordsRegex.TryGetValue(text4, out list2))
								{
									foreach (Regex regex2 in list2)
									{
										MatchCollection matchCollection2 = regex2.Matches(text2);
										IEnumerator enumerator7 = matchCollection2.GetEnumerator();
										try
										{
											while (enumerator7.MoveNext())
											{
												object obj3 = enumerator7.Current;
												Match match2 = (Match)obj3;
												IEnumerator enumerator8 = match2.Captures.GetEnumerator();
												try
												{
													while (enumerator8.MoveNext())
													{
														object obj4 = enumerator8.Current;
														Capture capture2 = (Capture)obj4;
														UnityEngine.Debug.Log(string.Concat(new object[]
														{
															"Test string contains a bad word: '",
															capture2.Value,
															"' detected by regex '",
															regex2,
															"'' from source '",
															text4,
															"'"
														}));
														text = text.Replace(capture2.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture2.Value.Length));
														flag = true;
													}
												}
												finally
												{
													IDisposable disposable3;
													if ((disposable3 = (enumerator8 as IDisposable)) != null)
													{
														disposable3.Dispose();
													}
												}
											}
										}
										finally
										{
											IDisposable disposable4;
											if ((disposable4 = (enumerator7 as IDisposable)) != null)
											{
												disposable4.Dispose();
											}
										}
									}
								}
								else
								{
									base.logResourceNotFound(text4);
								}
							}
						}
					}
					else if (sources == null || sources.Length == 0)
					{
						foreach (Regex regex3 in this.exactBadwordsRegex.Values)
						{
							MatchCollection matchCollection3 = regex3.Matches(text2);
							IEnumerator enumerator10 = matchCollection3.GetEnumerator();
							try
							{
								while (enumerator10.MoveNext())
								{
									object obj5 = enumerator10.Current;
									Match match3 = (Match)obj5;
									IEnumerator enumerator11 = match3.Captures.GetEnumerator();
									try
									{
										while (enumerator11.MoveNext())
										{
											object obj6 = enumerator11.Current;
											Capture capture3 = (Capture)obj6;
											text = text.Replace(capture3.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture3.Value.Length));
											flag = true;
										}
									}
									finally
									{
										IDisposable disposable5;
										if ((disposable5 = (enumerator11 as IDisposable)) != null)
										{
											disposable5.Dispose();
										}
									}
								}
							}
							finally
							{
								IDisposable disposable6;
								if ((disposable6 = (enumerator10 as IDisposable)) != null)
								{
									disposable6.Dispose();
								}
							}
						}
					}
					else
					{
						foreach (string text5 in sources)
						{
							Regex regex4;
							if (this.exactBadwordsRegex.TryGetValue(text5, out regex4))
							{
								MatchCollection matchCollection4 = regex4.Matches(text2);
								IEnumerator enumerator12 = matchCollection4.GetEnumerator();
								try
								{
									while (enumerator12.MoveNext())
									{
										object obj7 = enumerator12.Current;
										Match match4 = (Match)obj7;
										IEnumerator enumerator13 = match4.Captures.GetEnumerator();
										try
										{
											while (enumerator13.MoveNext())
											{
												object obj8 = enumerator13.Current;
												Capture capture4 = (Capture)obj8;
												text = text.Replace(capture4.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture4.Value.Length));
												flag = true;
											}
										}
										finally
										{
											IDisposable disposable7;
											if ((disposable7 = (enumerator13 as IDisposable)) != null)
											{
												disposable7.Dispose();
											}
										}
									}
								}
								finally
								{
									IDisposable disposable8;
									if ((disposable8 = (enumerator12 as IDisposable)) != null)
									{
										disposable8.Dispose();
									}
								}
							}
							else
							{
								base.logResourceNotFound(text5);
							}
						}
					}
				}
			}
			else
			{
				base.logFilterNotReady();
			}
			if (flag)
			{
				return text;
			}
			return testString;
		}

		public override string Replace(string text, List<string> badWords)
		{
			string text2 = this.replaceLeetSpeak(text);
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
					text2 = text2.Replace(text3, Helper.CreateReplaceString(this.ReplaceCharacters, text3.Length));
				}
			}
			return text2;
		}

		protected string replaceLeetSpeak(string input)
		{
			string text = input;
			if (this.ReplaceLeetSpeak && !string.IsNullOrEmpty(input))
			{
				text = text.Replace("@", "a");
				text = text.Replace("4", "a");
				text = text.Replace("^", "a");
				text = text.Replace("8", "b");
				text = text.Replace("©", "c");
				text = text.Replace('¢', 'c');
				text = text.Replace("€", "e");
				text = text.Replace("3", "e");
				text = text.Replace("£", "e");
				text = text.Replace("ƒ", "f");
				text = text.Replace("6", "g");
				text = text.Replace("9", "g");
				text = text.Replace("#", "h");
				text = text.Replace("1", "i");
				text = text.Replace("!", "i");
				text = text.Replace("|", "i");
				text = text.Replace("0", "o");
				text = text.Replace("2", "r");
				text = text.Replace("®", "r");
				text = text.Replace("$", "s");
				text = text.Replace("5", "s");
				text = text.Replace("§", "s");
				text = text.Replace("7", "t");
				text = text.Replace("+", "t");
				text = text.Replace("†", "t");
				text = text.Replace("¥", "y");
			}
			return text;
		}

		public string ReplaceCharacters;

		public bool ReplaceLeetSpeak;

		public bool SimpleCheck;

		private readonly List<BadWordProvider> tempBadWordProviderLTR;

		private readonly List<BadWordProvider> tempBadWordProviderRTL;

		private readonly Dictionary<string, Regex> exactBadwordsRegex = new Dictionary<string, Regex>(30);

		private readonly Dictionary<string, List<Regex>> debugExactBadwordsRegex = new Dictionary<string, List<Regex>>(30);

		private readonly Dictionary<string, List<string>> simpleBadwords = new Dictionary<string, List<string>>(30);

		private bool ready;

		private bool readyFirstime;

		private List<BadWordProvider> badWordProviderLTR = new List<BadWordProvider>();

		private List<BadWordProvider> badWordProviderRTL = new List<BadWordProvider>();
	}
}
