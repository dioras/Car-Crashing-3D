using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class ModifyServerBuildRequest : PlayFabRequestCommon
	{
		public List<Region> ActiveRegions;

		public string BuildId;

		public string CommandLineTemplate;

		public string Comment;

		public string ExecutablePath;

		public int MaxGamesPerHost;

		public int MinFreeGameSlots;

		public DateTime? Timestamp;
	}
}
