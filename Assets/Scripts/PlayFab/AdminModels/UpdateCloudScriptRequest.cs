using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateCloudScriptRequest : PlayFabRequestCommon
	{
		public string DeveloperPlayFabId;

		public List<CloudScriptFile> Files;

		public bool Publish;
	}
}
