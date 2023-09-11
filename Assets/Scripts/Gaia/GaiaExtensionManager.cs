using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Gaia
{
	public class GaiaExtensionManager
	{
		public void ScanForExtensions()
		{
			this.m_extensions.Clear();
			string text = string.Empty;
			string packageName = string.Empty;
			string packageImageName = string.Empty;
			string packageDescription = string.Empty;
			string packageURL = string.Empty;
			List<Type> typesInNamespace = this.GetTypesInNamespace("Gaia.GX.");
			for (int i = 0; i < typesInNamespace.Count; i++)
			{
				string[] array = typesInNamespace[i].FullName.Split(new char[]
				{
					'.'
				});
				text = Regex.Replace(array[2], "(\\B[A-Z])", " $1");
				packageName = Regex.Replace(array[3], "(\\B[A-Z])", " $1");
				MethodInfo[] methods = typesInNamespace[i].GetMethods(BindingFlags.Static | BindingFlags.Public);
				List<MethodInfo> list = new List<MethodInfo>();
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.Name.StartsWith("GX_"))
					{
						list.Add(methodInfo);
					}
					else if (methodInfo.Name == "GetPublisherName")
					{
						text = (string)methodInfo.Invoke(null, null);
					}
					else if (methodInfo.Name == "GetPackageName")
					{
						packageName = (string)methodInfo.Invoke(null, null);
					}
				}
				GaiaCompatiblePublisher gaiaCompatiblePublisher = null;
				if (!this.m_extensions.TryGetValue(text, out gaiaCompatiblePublisher))
				{
					gaiaCompatiblePublisher = new GaiaCompatiblePublisher();
					gaiaCompatiblePublisher.m_publisherName = text;
					gaiaCompatiblePublisher.m_compatibleFoldedOut = false;
					gaiaCompatiblePublisher.m_installedFoldedOut = false;
					this.m_extensions.Add(text, gaiaCompatiblePublisher);
				}
				GaiaCompatiblePackage gaiaCompatiblePackage = gaiaCompatiblePublisher.GetPackage(packageName);
				if (gaiaCompatiblePackage == null)
				{
					gaiaCompatiblePackage = new GaiaCompatiblePackage();
					gaiaCompatiblePackage.m_compatibleFoldedOut = false;
					gaiaCompatiblePackage.m_installedFoldedOut = false;
					gaiaCompatiblePackage.m_packageName = packageName;
					gaiaCompatiblePublisher.AddPackage(gaiaCompatiblePackage);
				}
				if (list.Count > 0)
				{
					gaiaCompatiblePackage.m_isInstalled = true;
				}
				else
				{
					gaiaCompatiblePackage.m_isInstalled = false;
				}
				gaiaCompatiblePackage.m_methods = new List<MethodInfo>(list);
			}
			typesInNamespace = this.GetTypesInNamespace("Gaia.GXC.");
			for (int k = 0; k < typesInNamespace.Count; k++)
			{
				string[] array = typesInNamespace[k].FullName.Split(new char[]
				{
					'.'
				});
				text = Regex.Replace(array[2], "(\\B[A-Z])", " $1");
				packageName = Regex.Replace(array[3], "(\\B[A-Z])", " $1");
				foreach (MethodInfo methodInfo in typesInNamespace[k].GetMethods(BindingFlags.Static | BindingFlags.Public))
				{
					if (methodInfo.Name == "GetPublisherName")
					{
						text = (string)methodInfo.Invoke(null, null);
					}
					else if (methodInfo.Name == "GetPackageName")
					{
						packageName = (string)methodInfo.Invoke(null, null);
					}
					else if (methodInfo.Name == "GetPackageImage")
					{
						packageImageName = (string)methodInfo.Invoke(null, null);
					}
					else if (methodInfo.Name == "GetPackageDescription")
					{
						packageDescription = (string)methodInfo.Invoke(null, null);
					}
					else if (methodInfo.Name == "GetPackageURL")
					{
						packageURL = (string)methodInfo.Invoke(null, null);
					}
				}
				GaiaCompatiblePublisher gaiaCompatiblePublisher2 = null;
				if (!this.m_extensions.TryGetValue(text, out gaiaCompatiblePublisher2))
				{
					gaiaCompatiblePublisher2 = new GaiaCompatiblePublisher();
					gaiaCompatiblePublisher2.m_publisherName = text;
					gaiaCompatiblePublisher2.m_compatibleFoldedOut = false;
					gaiaCompatiblePublisher2.m_installedFoldedOut = false;
					this.m_extensions.Add(text, gaiaCompatiblePublisher2);
				}
				GaiaCompatiblePackage gaiaCompatiblePackage2 = gaiaCompatiblePublisher2.GetPackage(packageName);
				if (gaiaCompatiblePackage2 == null)
				{
					gaiaCompatiblePackage2 = new GaiaCompatiblePackage();
					gaiaCompatiblePackage2.m_compatibleFoldedOut = false;
					gaiaCompatiblePackage2.m_installedFoldedOut = false;
					gaiaCompatiblePackage2.m_packageName = packageName;
					gaiaCompatiblePublisher2.AddPackage(gaiaCompatiblePackage2);
				}
				gaiaCompatiblePackage2.m_isCompatible = true;
				gaiaCompatiblePackage2.m_packageDescription = packageDescription;
				gaiaCompatiblePackage2.m_packageImageName = packageImageName;
				gaiaCompatiblePackage2.m_packageURL = packageURL;
			}
		}

		public int GetInstalledExtensionCount()
		{
			int num = 0;
			foreach (GaiaCompatiblePublisher gaiaCompatiblePublisher in this.m_extensions.Values)
			{
				num += gaiaCompatiblePublisher.InstalledPackages();
			}
			return num;
		}

		public List<GaiaCompatiblePublisher> GetPublishers()
		{
			List<GaiaCompatiblePublisher> list = new List<GaiaCompatiblePublisher>(this.m_extensions.Values);
			list.Sort((GaiaCompatiblePublisher a, GaiaCompatiblePublisher b) => a.m_publisherName.CompareTo(b.m_publisherName));
			return list;
		}

		public List<Type> GetTypesInNamespace(string nameSpace)
		{
			List<Type> list = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				if (assemblies[i].FullName.StartsWith("Assembly"))
				{
					Type[] types = assemblies[i].GetTypes();
					for (int j = 0; j < types.Length; j++)
					{
						if (!string.IsNullOrEmpty(types[j].Namespace) && types[j].Namespace.StartsWith(nameSpace))
						{
							list.Add(types[j]);
						}
					}
				}
			}
			return list;
		}

		private Dictionary<string, GaiaCompatiblePublisher> m_extensions = new Dictionary<string, GaiaCompatiblePublisher>();
	}
}
