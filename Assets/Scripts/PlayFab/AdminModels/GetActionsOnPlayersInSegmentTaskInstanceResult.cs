using System;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetActionsOnPlayersInSegmentTaskInstanceResult : PlayFabResultCommon
	{
		public ActionsOnPlayersInSegmentTaskParameter Parameter;

		public ActionsOnPlayersInSegmentTaskSummary Summary;
	}
}
