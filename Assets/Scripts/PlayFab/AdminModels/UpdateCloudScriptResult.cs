using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdateCloudScriptResult : PlayFabResultCommon
	{
		public int Revision;

		public int Version;
	}
}
