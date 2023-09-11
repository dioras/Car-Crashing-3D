using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class SetPublishedRevisionRequest : PlayFabRequestCommon
	{
		public int Revision;

		public int Version;
	}
}
