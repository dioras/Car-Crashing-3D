using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetContentListResult : PlayFabResultCommon
	{
		public List<ContentInfo> Contents;

		public int ItemCount;

		public uint TotalSize;
	}
}
