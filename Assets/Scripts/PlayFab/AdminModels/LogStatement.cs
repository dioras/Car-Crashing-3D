using System;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class LogStatement
	{
		public object Data;

		public string Level;

		public string Message;
	}
}
