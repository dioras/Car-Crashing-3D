using System;
using System.Collections.Generic;

namespace Gaia
{
	public class GaiaCompatiblePublisher
	{
		public GaiaCompatiblePackage GetPackage(string packageName)
		{
			GaiaCompatiblePackage result;
			if (this.m_packages.TryGetValue(packageName, out result))
			{
				return result;
			}
			return null;
		}

		public List<GaiaCompatiblePackage> GetPackages()
		{
			List<GaiaCompatiblePackage> list = new List<GaiaCompatiblePackage>(this.m_packages.Values);
			list.Sort((GaiaCompatiblePackage a, GaiaCompatiblePackage b) => a.m_packageName.CompareTo(b.m_packageName));
			return list;
		}

		public int InstalledPackages()
		{
			int num = 0;
			foreach (KeyValuePair<string, GaiaCompatiblePackage> keyValuePair in this.m_packages)
			{
				if (keyValuePair.Value.m_isInstalled)
				{
					num++;
				}
			}
			return num;
		}

		public int CompatiblePackages()
		{
			int num = 0;
			foreach (KeyValuePair<string, GaiaCompatiblePackage> keyValuePair in this.m_packages)
			{
				if (keyValuePair.Value.m_isCompatible)
				{
					num++;
				}
			}
			return num;
		}

		public void AddPackage(GaiaCompatiblePackage package)
		{
			this.m_packages.Add(package.m_packageName, package);
		}

		public string m_publisherName;

		public bool m_installedFoldedOut;

		public bool m_compatibleFoldedOut;

		private Dictionary<string, GaiaCompatiblePackage> m_packages = new Dictionary<string, GaiaCompatiblePackage>();
	}
}
