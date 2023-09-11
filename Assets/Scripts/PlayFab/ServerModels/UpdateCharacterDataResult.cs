using System;
using PlayFab.SharedModels;

namespace PlayFab.ServerModels
{
	[Serializable]
	public class UpdateCharacterDataResult : PlayFabResultCommon
	{
		public uint DataVersion;
	}
}
