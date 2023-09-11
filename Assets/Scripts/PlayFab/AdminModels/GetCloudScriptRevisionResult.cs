using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetCloudScriptRevisionResult : PlayFabResultCommon
	{
		public DateTime CreatedAt;

		public List<CloudScriptFile> Files;

		public bool IsPublished;

		public int Revision;

		public int Version;
	}
}
