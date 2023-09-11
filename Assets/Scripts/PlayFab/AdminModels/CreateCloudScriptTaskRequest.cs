using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class CreateCloudScriptTaskRequest : PlayFabRequestCommon
	{
		public string Description;

		public bool IsActive;

		public string Name;

		public CloudScriptTaskParameter Parameter;

		public string Schedule;
	}
}
