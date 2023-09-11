using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class LogView : ConsoleBase
	{
		public static void AddLog(string log)
		{
			LogView.events.Insert(0, string.Format("{0}\n{1}\n", DateTime.Now.ToString(LogView.datePatt), log));
		}

		protected void OnGUI()
		{
			
		}

		private static string datePatt = "M/d/yyyy hh:mm:ss tt";

		private static IList<string> events = new List<string>();
	}
}
