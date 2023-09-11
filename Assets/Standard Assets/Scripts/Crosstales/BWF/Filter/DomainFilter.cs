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
	public class DomainFilter : BaseFilter
	{
		public DomainFilter(List<DomainProvider> domainProvider, string replaceCharacters, string markPrefix, string markPostfix)
		{
			this.tempDomainProvider = domainProvider;
			this.ReplaceCharacters = replaceCharacters;
			this.MarkPrefix = markPrefix;
			this.MarkPostfix = markPostfix;
		}

		public List<DomainProvider> DomainProvider
		{
			get
			{
				return this.domainProvider;
			}
			set
			{
				this.domainProvider = value;
				if (this.domainProvider != null && this.domainProvider.Count > 0)
				{
					foreach (DomainProvider domainProvider in this.domainProvider)
					{
						if (domainProvider != null)
						{
							if (Config.DEBUG_DOMAINS)
							{
								this.debugDomainsRegex.CTAddRange(domainProvider.DebugDomainsRegex);
							}
							else
							{
								this.domainsRegex.CTAddRange(domainProvider.DomainsRegex);
							}
						}
						else if (!Helper.isEditorMode)
						{
							UnityEngine.Debug.LogError("DomainProvider is null!");
						}
					}
				}
				else
				{
					this.domainProvider = new List<DomainProvider>();
					if (!Helper.isEditorMode)
					{
						UnityEngine.Debug.LogWarning("No 'DomainProvider' added!" + Environment.NewLine + "If you want to use this functionality, please add your desired 'DomainProvider' in the editor or script.");
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
					if (this.tempDomainProvider != null)
					{
						foreach (DomainProvider domainProvider in this.tempDomainProvider)
						{
							if (domainProvider != null && !domainProvider.isReady)
							{
								flag = false;
								break;
							}
						}
					}
					if (!this.readyFirstime && flag)
					{
						this.DomainProvider = this.tempDomainProvider;
						if (this.DomainProvider != null)
						{
							foreach (DomainProvider domainProvider2 in this.DomainProvider)
							{
								if (domainProvider2 != null)
								{
									foreach (Source source in domainProvider2.Sources)
									{
										if (!this.sources.ContainsKey(source.Name))
										{
											this.sources.Add(source.Name, source);
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
			bool result = false;
			if (this.isReady)
			{
				if (string.IsNullOrEmpty(testString))
				{
					base.logContains();
				}
				else if (Config.DEBUG_DOMAINS)
				{
					if (sources == null || sources.Length == 0)
					{
						foreach (List<Regex> list in this.debugDomainsRegex.Values)
						{
							foreach (Regex regex in list)
							{
								Match match = regex.Match(testString);
								if (match.Success)
								{
									UnityEngine.Debug.Log(string.Concat(new object[]
									{
										"Test string contains a domain: '",
										match.Value,
										"' detected by regex '",
										regex,
										"'"
									}));
									result = true;
									break;
								}
							}
						}
					}
					else
					{
						foreach (string text in sources)
						{
							List<Regex> list2;
							if (this.debugDomainsRegex.TryGetValue(text, out list2))
							{
								foreach (Regex regex2 in list2)
								{
									Match match2 = regex2.Match(testString);
									if (match2.Success)
									{
										UnityEngine.Debug.Log(string.Concat(new object[]
										{
											"Test string contains a domain: '",
											match2.Value,
											"' detected by regex '",
											regex2,
											"'' from source '",
											text,
											"'"
										}));
										result = true;
										break;
									}
								}
							}
							else
							{
								base.logResourceNotFound(text);
							}
						}
					}
				}
				else if (sources == null || sources.Length == 0)
				{
					foreach (Regex regex3 in this.domainsRegex.Values)
					{
						if (regex3.Match(testString).Success)
						{
							result = true;
							break;
						}
					}
				}
				else
				{
					foreach (string text2 in sources)
					{
						Regex regex4;
						if (this.domainsRegex.TryGetValue(text2, out regex4))
						{
							Match match3 = regex4.Match(testString);
							if (match3.Success)
							{
								result = true;
								break;
							}
						}
						else
						{
							base.logResourceNotFound(text2);
						}
					}
				}
			}
			else
			{
				base.logFilterNotReady();
			}
			return result;
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
				else if (Config.DEBUG_DOMAINS)
				{
					if (sources == null || sources.Length == 0)
					{
						foreach (List<Regex> list2 in this.debugDomainsRegex.Values)
						{
							foreach (Regex regex in list2)
							{
								MatchCollection matchCollection = regex.Matches(testString);
								IEnumerator enumerator3 = matchCollection.GetEnumerator();
								try
								{
									while (enumerator3.MoveNext())
									{
										object obj = enumerator3.Current;
										Match match = (Match)obj;
										IEnumerator enumerator4 = match.Captures.GetEnumerator();
										try
										{
											while (enumerator4.MoveNext())
											{
												object obj2 = enumerator4.Current;
												Capture capture = (Capture)obj2;
												UnityEngine.Debug.Log(string.Concat(new object[]
												{
													"Test string contains a domain: '",
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
											if ((disposable = (enumerator4 as IDisposable)) != null)
											{
												disposable.Dispose();
											}
										}
									}
								}
								finally
								{
									IDisposable disposable2;
									if ((disposable2 = (enumerator3 as IDisposable)) != null)
									{
										disposable2.Dispose();
									}
								}
							}
						}
					}
					else
					{
						foreach (string text in sources)
						{
							List<Regex> list3;
							if (this.debugDomainsRegex.TryGetValue(text, out list3))
							{
								foreach (Regex regex2 in list3)
								{
									MatchCollection matchCollection2 = regex2.Matches(testString);
									IEnumerator enumerator6 = matchCollection2.GetEnumerator();
									try
									{
										while (enumerator6.MoveNext())
										{
											object obj3 = enumerator6.Current;
											Match match2 = (Match)obj3;
											IEnumerator enumerator7 = match2.Captures.GetEnumerator();
											try
											{
												while (enumerator7.MoveNext())
												{
													object obj4 = enumerator7.Current;
													Capture capture2 = (Capture)obj4;
													UnityEngine.Debug.Log(string.Concat(new object[]
													{
														"Test string contains a domain: '",
														capture2.Value,
														"' detected by regex '",
														regex2,
														"'' from source '",
														text,
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
												if ((disposable3 = (enumerator7 as IDisposable)) != null)
												{
													disposable3.Dispose();
												}
											}
										}
									}
									finally
									{
										IDisposable disposable4;
										if ((disposable4 = (enumerator6 as IDisposable)) != null)
										{
											disposable4.Dispose();
										}
									}
								}
							}
							else
							{
								base.logResourceNotFound(text);
							}
						}
					}
				}
				else if (sources == null || sources.Length == 0)
				{
					foreach (Regex regex3 in this.domainsRegex.Values)
					{
						MatchCollection matchCollection3 = regex3.Matches(testString);
						IEnumerator enumerator9 = matchCollection3.GetEnumerator();
						try
						{
							while (enumerator9.MoveNext())
							{
								object obj5 = enumerator9.Current;
								Match match3 = (Match)obj5;
								IEnumerator enumerator10 = match3.Captures.GetEnumerator();
								try
								{
									while (enumerator10.MoveNext())
									{
										object obj6 = enumerator10.Current;
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
									if ((disposable5 = (enumerator10 as IDisposable)) != null)
									{
										disposable5.Dispose();
									}
								}
							}
						}
						finally
						{
							IDisposable disposable6;
							if ((disposable6 = (enumerator9 as IDisposable)) != null)
							{
								disposable6.Dispose();
							}
						}
					}
				}
				else
				{
					foreach (string text2 in sources)
					{
						Regex regex4;
						if (this.domainsRegex.TryGetValue(text2, out regex4))
						{
							MatchCollection matchCollection4 = regex4.Matches(testString);
							IEnumerator enumerator11 = matchCollection4.GetEnumerator();
							try
							{
								while (enumerator11.MoveNext())
								{
									object obj7 = enumerator11.Current;
									Match match4 = (Match)obj7;
									IEnumerator enumerator12 = match4.Captures.GetEnumerator();
									try
									{
										while (enumerator12.MoveNext())
										{
											object obj8 = enumerator12.Current;
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
										if ((disposable7 = (enumerator12 as IDisposable)) != null)
										{
											disposable7.Dispose();
										}
									}
								}
							}
							finally
							{
								IDisposable disposable8;
								if ((disposable8 = (enumerator11 as IDisposable)) != null)
								{
									disposable8.Dispose();
								}
							}
						}
						else
						{
							base.logResourceNotFound(text2);
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
			string text = testString;
			if (this.isReady)
			{
				if (string.IsNullOrEmpty(testString))
				{
					base.logReplaceAll();
					text = string.Empty;
				}
				else if (Config.DEBUG_DOMAINS)
				{
					if (sources == null || sources.Length == 0)
					{
						foreach (List<Regex> list in this.debugDomainsRegex.Values)
						{
							foreach (Regex regex in list)
							{
								MatchCollection matchCollection = regex.Matches(testString);
								IEnumerator enumerator3 = matchCollection.GetEnumerator();
								try
								{
									while (enumerator3.MoveNext())
									{
										object obj = enumerator3.Current;
										Match match = (Match)obj;
										IEnumerator enumerator4 = match.Captures.GetEnumerator();
										try
										{
											while (enumerator4.MoveNext())
											{
												object obj2 = enumerator4.Current;
												Capture capture = (Capture)obj2;
												UnityEngine.Debug.Log(string.Concat(new object[]
												{
													"Test string contains a domain: '",
													capture.Value,
													"' detected by regex '",
													regex,
													"'"
												}));
												text = text.Replace(capture.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture.Value.Length));
											}
										}
										finally
										{
											IDisposable disposable;
											if ((disposable = (enumerator4 as IDisposable)) != null)
											{
												disposable.Dispose();
											}
										}
									}
								}
								finally
								{
									IDisposable disposable2;
									if ((disposable2 = (enumerator3 as IDisposable)) != null)
									{
										disposable2.Dispose();
									}
								}
							}
						}
					}
					else
					{
						foreach (string text2 in sources)
						{
							List<Regex> list2;
							if (this.debugDomainsRegex.TryGetValue(text2, out list2))
							{
								foreach (Regex regex2 in list2)
								{
									MatchCollection matchCollection2 = regex2.Matches(testString);
									IEnumerator enumerator6 = matchCollection2.GetEnumerator();
									try
									{
										while (enumerator6.MoveNext())
										{
											object obj3 = enumerator6.Current;
											Match match2 = (Match)obj3;
											IEnumerator enumerator7 = match2.Captures.GetEnumerator();
											try
											{
												while (enumerator7.MoveNext())
												{
													object obj4 = enumerator7.Current;
													Capture capture2 = (Capture)obj4;
													UnityEngine.Debug.Log(string.Concat(new object[]
													{
														"Test string contains a domain: '",
														capture2.Value,
														"' detected by regex '",
														regex2,
														"'' from source '",
														text2,
														"'"
													}));
													text = text.Replace(capture2.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture2.Value.Length));
												}
											}
											finally
											{
												IDisposable disposable3;
												if ((disposable3 = (enumerator7 as IDisposable)) != null)
												{
													disposable3.Dispose();
												}
											}
										}
									}
									finally
									{
										IDisposable disposable4;
										if ((disposable4 = (enumerator6 as IDisposable)) != null)
										{
											disposable4.Dispose();
										}
									}
								}
							}
							else
							{
								base.logResourceNotFound(text2);
							}
						}
					}
				}
				else if (sources == null || sources.Length == 0)
				{
					foreach (Regex regex3 in this.domainsRegex.Values)
					{
						MatchCollection matchCollection3 = regex3.Matches(testString);
						IEnumerator enumerator9 = matchCollection3.GetEnumerator();
						try
						{
							while (enumerator9.MoveNext())
							{
								object obj5 = enumerator9.Current;
								Match match3 = (Match)obj5;
								IEnumerator enumerator10 = match3.Captures.GetEnumerator();
								try
								{
									while (enumerator10.MoveNext())
									{
										object obj6 = enumerator10.Current;
										Capture capture3 = (Capture)obj6;
										text = text.Replace(capture3.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture3.Value.Length));
									}
								}
								finally
								{
									IDisposable disposable5;
									if ((disposable5 = (enumerator10 as IDisposable)) != null)
									{
										disposable5.Dispose();
									}
								}
							}
						}
						finally
						{
							IDisposable disposable6;
							if ((disposable6 = (enumerator9 as IDisposable)) != null)
							{
								disposable6.Dispose();
							}
						}
					}
				}
				else
				{
					foreach (string text3 in sources)
					{
						Regex regex4;
						if (this.domainsRegex.TryGetValue(text3, out regex4))
						{
							MatchCollection matchCollection4 = regex4.Matches(testString);
							IEnumerator enumerator11 = matchCollection4.GetEnumerator();
							try
							{
								while (enumerator11.MoveNext())
								{
									object obj7 = enumerator11.Current;
									Match match4 = (Match)obj7;
									IEnumerator enumerator12 = match4.Captures.GetEnumerator();
									try
									{
										while (enumerator12.MoveNext())
										{
											object obj8 = enumerator12.Current;
											Capture capture4 = (Capture)obj8;
											text = text.Replace(capture4.Value, Helper.CreateReplaceString(this.ReplaceCharacters, capture4.Value.Length));
										}
									}
									finally
									{
										IDisposable disposable7;
										if ((disposable7 = (enumerator12 as IDisposable)) != null)
										{
											disposable7.Dispose();
										}
									}
								}
							}
							finally
							{
								IDisposable disposable8;
								if ((disposable8 = (enumerator11 as IDisposable)) != null)
								{
									disposable8.Dispose();
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
			else
			{
				base.logFilterNotReady();
			}
			return text;
		}

		public override string Replace(string text, List<string> domains)
		{
			string text2 = text;
			if (string.IsNullOrEmpty(text))
			{
				base.logReplace();
				text2 = string.Empty;
			}
			else if (domains == null || domains.Count == 0)
			{
				UnityEngine.Debug.LogWarning("Parameter 'badWords' is null or empty!" + Environment.NewLine + "=> 'Replace()' will return the original string.");
			}
			else
			{
				foreach (string text3 in domains)
				{
					text2 = text2.Replace(text3, Helper.CreateReplaceString(this.ReplaceCharacters, text3.Length));
				}
			}
			return text2;
		}

		public string ReplaceCharacters;

		private List<DomainProvider> domainProvider = new List<DomainProvider>();

		private readonly List<DomainProvider> tempDomainProvider;

		private readonly Dictionary<string, Regex> domainsRegex = new Dictionary<string, Regex>();

		private readonly Dictionary<string, List<Regex>> debugDomainsRegex = new Dictionary<string, List<Regex>>();

		private bool ready;

		private bool readyFirstime;
	}
}
