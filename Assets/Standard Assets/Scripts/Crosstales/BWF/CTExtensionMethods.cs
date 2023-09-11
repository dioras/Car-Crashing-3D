using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Crosstales.BWF
{
	public static class CTExtensionMethods
	{
		public static void CTAddRange<T, S>(this Dictionary<T, S> source, Dictionary<T, S> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (KeyValuePair<T, S> keyValuePair in collection)
			{
				if (!source.ContainsKey(keyValuePair.Key))
				{
					source.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					UnityEngine.Debug.LogWarning("Duplicate key found: " + keyValuePair.Key);
				}
			}
		}

		public static bool CTEquals(this string str, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (toCheck == null)
			{
				throw new ArgumentNullException("toCheck");
			}
			return str.Equals(toCheck, comp);
		}

		public static bool CTContains(this string str, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (toCheck == null)
			{
				throw new ArgumentNullException("toCheck");
			}
			return str.IndexOf(toCheck, comp) >= 0;
		}

		public static bool CTContainsAny(this string str, string searchTerms, char splitChar = ' ')
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (string.IsNullOrEmpty(searchTerms))
			{
				return true;
			}
			char[] separator = new char[]
			{
				splitChar
			};
			return searchTerms.Split(separator, StringSplitOptions.RemoveEmptyEntries).Any((string searchTerm) => str.CTContains(searchTerm, StringComparison.OrdinalIgnoreCase));
		}

		public static bool CTContainsAll(this string str, string searchTerms, char splitChar = ' ')
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (string.IsNullOrEmpty(searchTerms))
			{
				return true;
			}
			char[] separator = new char[]
			{
				splitChar
			};
			return searchTerms.Split(separator, StringSplitOptions.RemoveEmptyEntries).All((string searchTerm) => str.CTContains(searchTerm, StringComparison.OrdinalIgnoreCase));
		}

		public static void CTShuffle<T>(this IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			int i = list.Count;
			while (i > 1)
			{
				int index = CTExtensionMethods.rd.Next(i--);
				T value = list[i];
				list[i] = list[index];
				list[index] = value;
			}
		}

		public static void CTShuffle<T>(this T[] array)
		{
			if (array == null || array.Length <= 0)
			{
				throw new ArgumentNullException("array");
			}
			int i = array.Length;
			while (i > 1)
			{
				int num = CTExtensionMethods.rd.Next(i--);
				T t = array[i];
				array[i] = array[num];
				array[num] = t;
			}
		}

		public static string CTDump<T>(this T[] array)
		{
			if (array == null || array.Length <= 0)
			{
				throw new ArgumentNullException("array");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (T t in array)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(t.ToString());
			}
			return stringBuilder.ToString();
		}

		public static string CTDump<T>(this List<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (T t in list)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(t.ToString());
			}
			return stringBuilder.ToString();
		}

		private static readonly System.Random rd = new System.Random();
	}
}
