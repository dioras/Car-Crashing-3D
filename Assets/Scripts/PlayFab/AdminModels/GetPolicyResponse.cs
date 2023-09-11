using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class GetPolicyResponse : PlayFabResultCommon
	{
		public string PolicyName;

		public List<PermissionStatement> Statements;
	}
}
