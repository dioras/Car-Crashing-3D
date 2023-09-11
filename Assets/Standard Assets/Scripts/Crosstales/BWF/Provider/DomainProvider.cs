using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Crosstales.BWF.Model;
using Crosstales.BWF.Util;
using UnityEngine;

namespace Crosstales.BWF.Provider
{
	public abstract class DomainProvider : BaseProvider
	{
		public Dictionary<string, Regex> DomainsRegex
		{
			get
			{
				return this.domainsRegex;
			}
			protected set
			{
				this.domainsRegex = value;
			}
		}

		public Dictionary<string, List<Regex>> DebugDomainsRegex
		{
			get
			{
				return this.debugDomainsRegex;
			}
			protected set
			{
				this.debugDomainsRegex = value;
			}
		}

		public override void Load()
		{
			if (this.ClearOnLoad)
			{
				this.domains.Clear();
			}
		}

		protected override void init()
		{
			this.DomainsRegex.Clear();
			if (Config.DEBUG_DOMAINS)
			{
				UnityEngine.Debug.Log("++ DomainProvider '" + this.Name + "' started in debug-mode ++");
			}
			foreach (Domains domains in this.domains)
			{
				if (Config.DEBUG_DOMAINS)
				{
					try
					{
						List<Regex> list = new List<Regex>(domains.DomainList.Count);
						foreach (string str in domains.DomainList)
						{
							list.Add(new Regex("\\b{0,1}((ht|f)tp(s?)\\:\\/\\/)?[\\w\\-\\.\\@]*[\\.]" + str + "(:\\d{1,5})?(\\/|\\b)", this.RegexOption1 | this.RegexOption2 | this.RegexOption3 | this.RegexOption4 | this.RegexOption5));
						}
						if (!this.DebugDomainsRegex.ContainsKey(domains.Source.Name))
						{
							this.DebugDomainsRegex.Add(domains.Source.Name, list);
						}
					}
					catch (Exception ex)
					{
						UnityEngine.Debug.LogError(string.Concat(new object[]
						{
							"Could not generate debug regex for source '",
							domains.Source.Name,
							"': ",
							ex
						}));
						if (Constants.DEV_DEBUG)
						{
							UnityEngine.Debug.Log(domains.DomainList.CTDump<string>());
						}
					}
				}
				else
				{
					try
					{
						if (!this.DomainsRegex.ContainsKey(domains.Source.Name))
						{
							this.DomainsRegex.Add(domains.Source.Name, new Regex("\\b{0,1}((ht|f)tp(s?)\\:\\/\\/)?[\\w\\-\\.\\@]*[\\.](" + string.Join("|", domains.DomainList.ToArray()) + ")(:\\d{1,5})?(\\/|\\b)", this.RegexOption1 | this.RegexOption2 | this.RegexOption3 | this.RegexOption4 | this.RegexOption5));
						}
					}
					catch (Exception ex2)
					{
						UnityEngine.Debug.LogError(string.Concat(new object[]
						{
							"Could not generate exact regex for source '",
							domains.Source.Name,
							"': ",
							ex2
						}));
						if (Constants.DEV_DEBUG)
						{
							UnityEngine.Debug.Log(domains.DomainList.CTDump<string>());
						}
					}
				}
				if (Config.DEBUG_DOMAINS)
				{
					UnityEngine.Debug.Log(string.Concat(new object[]
					{
						"Domain resource '",
						domains.Source,
						"' loaded and ",
						domains.DomainList.Count,
						" entries found."
					}));
				}
			}
			base.isReady = true;
		}

		protected List<Domains> domains = new List<Domains>();

		private const string domainRegexStart = "\\b{0,1}((ht|f)tp(s?)\\:\\/\\/)?[\\w\\-\\.\\@]*[\\.]";

		private const string domainRegexEnd = "(:\\d{1,5})?(\\/|\\b)";

		private Dictionary<string, Regex> domainsRegex = new Dictionary<string, Regex>();

		private Dictionary<string, List<Regex>> debugDomainsRegex = new Dictionary<string, List<Regex>>();
	}
}
