using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AdminModels
{
	[Serializable]
	public class UpdatePolicyRequest : PlayFabRequestCommon
	{
		public bool OverwritePolicy;

		public string PolicyName;

		public List<PermissionStatement> Statements;
	}
}
