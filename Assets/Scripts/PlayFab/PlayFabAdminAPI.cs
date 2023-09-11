using System;
using System.Collections.Generic;
using PlayFab.AdminModels;
using PlayFab.Internal;

namespace PlayFab
{
	public static class PlayFabAdminAPI
	{
		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void AbortTaskInstance(AbortTaskInstanceRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Admin/AbortTaskInstance", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddNews(AddNewsRequest request, Action<AddNewsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<AddNewsResult>("/Admin/AddNews", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddPlayerTag(AddPlayerTagRequest request, Action<AddPlayerTagResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<AddPlayerTagResult>("/Admin/AddPlayerTag", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddServerBuild(AddServerBuildRequest request, Action<AddServerBuildResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<AddServerBuildResult>("/Admin/AddServerBuild", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddUserVirtualCurrency(AddUserVirtualCurrencyRequest request, Action<ModifyUserVirtualCurrencyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyUserVirtualCurrencyResult>("/Admin/AddUserVirtualCurrency", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddVirtualCurrencyTypes(AddVirtualCurrencyTypesRequest request, Action<BlankResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<BlankResult>("/Admin/AddVirtualCurrencyTypes", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void BanUsers(BanUsersRequest request, Action<BanUsersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<BanUsersResult>("/Admin/BanUsers", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CheckLimitedEditionItemAvailability(CheckLimitedEditionItemAvailabilityRequest request, Action<CheckLimitedEditionItemAvailabilityResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<CheckLimitedEditionItemAvailabilityResult>("/Admin/CheckLimitedEditionItemAvailability", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreateActionsOnPlayersInSegmentTask(CreateActionsOnPlayerSegmentTaskRequest request, Action<CreateTaskResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<CreateTaskResult>("/Admin/CreateActionsOnPlayersInSegmentTask", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreateCloudScriptTask(CreateCloudScriptTaskRequest request, Action<CreateTaskResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<CreateTaskResult>("/Admin/CreateCloudScriptTask", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreatePlayerSharedSecret(CreatePlayerSharedSecretRequest request, Action<CreatePlayerSharedSecretResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<CreatePlayerSharedSecretResult>("/Admin/CreatePlayerSharedSecret", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreatePlayerStatisticDefinition(CreatePlayerStatisticDefinitionRequest request, Action<CreatePlayerStatisticDefinitionResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<CreatePlayerStatisticDefinitionResult>("/Admin/CreatePlayerStatisticDefinition", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteContent(DeleteContentRequest request, Action<BlankResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<BlankResult>("/Admin/DeleteContent", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeletePlayer(DeletePlayerRequest request, Action<DeletePlayerResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<DeletePlayerResult>("/Admin/DeletePlayer", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeletePlayerSharedSecret(DeletePlayerSharedSecretRequest request, Action<DeletePlayerSharedSecretResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<DeletePlayerSharedSecretResult>("/Admin/DeletePlayerSharedSecret", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteStore(DeleteStoreRequest request, Action<DeleteStoreResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<DeleteStoreResult>("/Admin/DeleteStore", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteTask(DeleteTaskRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Admin/DeleteTask", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteTitle(DeleteTitleRequest request, Action<DeleteTitleResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<DeleteTitleResult>("/Admin/DeleteTitle", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetActionsOnPlayersInSegmentTaskInstance(GetTaskInstanceRequest request, Action<GetActionsOnPlayersInSegmentTaskInstanceResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetActionsOnPlayersInSegmentTaskInstanceResult>("/Admin/GetActionsOnPlayersInSegmentTaskInstance", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetAllSegments(GetAllSegmentsRequest request, Action<GetAllSegmentsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetAllSegmentsResult>("/Admin/GetAllSegments", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCatalogItems(GetCatalogItemsRequest request, Action<GetCatalogItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCatalogItemsResult>("/Admin/GetCatalogItems", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCloudScriptRevision(GetCloudScriptRevisionRequest request, Action<GetCloudScriptRevisionResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCloudScriptRevisionResult>("/Admin/GetCloudScriptRevision", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCloudScriptTaskInstance(GetTaskInstanceRequest request, Action<GetCloudScriptTaskInstanceResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCloudScriptTaskInstanceResult>("/Admin/GetCloudScriptTaskInstance", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCloudScriptVersions(GetCloudScriptVersionsRequest request, Action<GetCloudScriptVersionsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCloudScriptVersionsResult>("/Admin/GetCloudScriptVersions", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetContentList(GetContentListRequest request, Action<GetContentListResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetContentListResult>("/Admin/GetContentList", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetContentUploadUrl(GetContentUploadUrlRequest request, Action<GetContentUploadUrlResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetContentUploadUrlResult>("/Admin/GetContentUploadUrl", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetDataReport(GetDataReportRequest request, Action<GetDataReportResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetDataReportResult>("/Admin/GetDataReport", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetMatchmakerGameInfo(GetMatchmakerGameInfoRequest request, Action<GetMatchmakerGameInfoResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetMatchmakerGameInfoResult>("/Admin/GetMatchmakerGameInfo", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetMatchmakerGameModes(GetMatchmakerGameModesRequest request, Action<GetMatchmakerGameModesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetMatchmakerGameModesResult>("/Admin/GetMatchmakerGameModes", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerIdFromAuthToken(GetPlayerIdFromAuthTokenRequest request, Action<GetPlayerIdFromAuthTokenResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerIdFromAuthTokenResult>("/Admin/GetPlayerIdFromAuthToken", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerProfile(GetPlayerProfileRequest request, Action<GetPlayerProfileResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerProfileResult>("/Admin/GetPlayerProfile", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerSegments(GetPlayersSegmentsRequest request, Action<GetPlayerSegmentsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerSegmentsResult>("/Admin/GetPlayerSegments", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerSharedSecrets(GetPlayerSharedSecretsRequest request, Action<GetPlayerSharedSecretsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerSharedSecretsResult>("/Admin/GetPlayerSharedSecrets", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayersInSegment(GetPlayersInSegmentRequest request, Action<GetPlayersInSegmentResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayersInSegmentResult>("/Admin/GetPlayersInSegment", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerStatisticDefinitions(GetPlayerStatisticDefinitionsRequest request, Action<GetPlayerStatisticDefinitionsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerStatisticDefinitionsResult>("/Admin/GetPlayerStatisticDefinitions", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerStatisticVersions(GetPlayerStatisticVersionsRequest request, Action<GetPlayerStatisticVersionsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerStatisticVersionsResult>("/Admin/GetPlayerStatisticVersions", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerTags(GetPlayerTagsRequest request, Action<GetPlayerTagsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerTagsResult>("/Admin/GetPlayerTags", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPolicy(GetPolicyRequest request, Action<GetPolicyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPolicyResponse>("/Admin/GetPolicy", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPublisherData(GetPublisherDataRequest request, Action<GetPublisherDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPublisherDataResult>("/Admin/GetPublisherData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetRandomResultTables(GetRandomResultTablesRequest request, Action<GetRandomResultTablesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetRandomResultTablesResult>("/Admin/GetRandomResultTables", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetServerBuildInfo(GetServerBuildInfoRequest request, Action<GetServerBuildInfoResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetServerBuildInfoResult>("/Admin/GetServerBuildInfo", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetServerBuildUploadUrl(GetServerBuildUploadURLRequest request, Action<GetServerBuildUploadURLResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetServerBuildUploadURLResult>("/Admin/GetServerBuildUploadUrl", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetStoreItems(GetStoreItemsRequest request, Action<GetStoreItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetStoreItemsResult>("/Admin/GetStoreItems", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTaskInstances(GetTaskInstancesRequest request, Action<GetTaskInstancesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTaskInstancesResult>("/Admin/GetTaskInstances", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTasks(GetTasksRequest request, Action<GetTasksResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTasksResult>("/Admin/GetTasks", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitleData(GetTitleDataRequest request, Action<GetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTitleDataResult>("/Admin/GetTitleData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitleInternalData(GetTitleDataRequest request, Action<GetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTitleDataResult>("/Admin/GetTitleInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserAccountInfo(LookupUserAccountInfoRequest request, Action<LookupUserAccountInfoResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<LookupUserAccountInfoResult>("/Admin/GetUserAccountInfo", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserBans(GetUserBansRequest request, Action<GetUserBansResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserBansResult>("/Admin/GetUserBans", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Admin/GetUserData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserInternalData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Admin/GetUserInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserInventory(GetUserInventoryRequest request, Action<GetUserInventoryResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserInventoryResult>("/Admin/GetUserInventory", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserPublisherData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Admin/GetUserPublisherData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserPublisherInternalData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Admin/GetUserPublisherInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserPublisherReadOnlyData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Admin/GetUserPublisherReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserReadOnlyData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Admin/GetUserReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GrantItemsToUsers(GrantItemsToUsersRequest request, Action<GrantItemsToUsersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GrantItemsToUsersResult>("/Admin/GrantItemsToUsers", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void IncrementLimitedEditionItemAvailability(IncrementLimitedEditionItemAvailabilityRequest request, Action<IncrementLimitedEditionItemAvailabilityResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<IncrementLimitedEditionItemAvailabilityResult>("/Admin/IncrementLimitedEditionItemAvailability", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void IncrementPlayerStatisticVersion(IncrementPlayerStatisticVersionRequest request, Action<IncrementPlayerStatisticVersionResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<IncrementPlayerStatisticVersionResult>("/Admin/IncrementPlayerStatisticVersion", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListServerBuilds(ListBuildsRequest request, Action<ListBuildsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ListBuildsResult>("/Admin/ListServerBuilds", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListVirtualCurrencyTypes(ListVirtualCurrencyTypesRequest request, Action<ListVirtualCurrencyTypesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ListVirtualCurrencyTypesResult>("/Admin/ListVirtualCurrencyTypes", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ModifyMatchmakerGameModes(ModifyMatchmakerGameModesRequest request, Action<ModifyMatchmakerGameModesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyMatchmakerGameModesResult>("/Admin/ModifyMatchmakerGameModes", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ModifyServerBuild(ModifyServerBuildRequest request, Action<ModifyServerBuildResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyServerBuildResult>("/Admin/ModifyServerBuild", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RefundPurchase(RefundPurchaseRequest request, Action<RefundPurchaseResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RefundPurchaseResponse>("/Admin/RefundPurchase", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemovePlayerTag(RemovePlayerTagRequest request, Action<RemovePlayerTagResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RemovePlayerTagResult>("/Admin/RemovePlayerTag", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveServerBuild(RemoveServerBuildRequest request, Action<RemoveServerBuildResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RemoveServerBuildResult>("/Admin/RemoveServerBuild", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveVirtualCurrencyTypes(RemoveVirtualCurrencyTypesRequest request, Action<BlankResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<BlankResult>("/Admin/RemoveVirtualCurrencyTypes", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ResetCharacterStatistics(ResetCharacterStatisticsRequest request, Action<ResetCharacterStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ResetCharacterStatisticsResult>("/Admin/ResetCharacterStatistics", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ResetPassword(ResetPasswordRequest request, Action<ResetPasswordResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ResetPasswordResult>("/Admin/ResetPassword", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ResetUserStatistics(ResetUserStatisticsRequest request, Action<ResetUserStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ResetUserStatisticsResult>("/Admin/ResetUserStatistics", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ResolvePurchaseDispute(ResolvePurchaseDisputeRequest request, Action<ResolvePurchaseDisputeResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ResolvePurchaseDisputeResponse>("/Admin/ResolvePurchaseDispute", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RevokeAllBansForUser(RevokeAllBansForUserRequest request, Action<RevokeAllBansForUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RevokeAllBansForUserResult>("/Admin/RevokeAllBansForUser", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RevokeBans(RevokeBansRequest request, Action<RevokeBansResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RevokeBansResult>("/Admin/RevokeBans", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RevokeInventoryItem(RevokeInventoryItemRequest request, Action<RevokeInventoryResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RevokeInventoryResult>("/Admin/RevokeInventoryItem", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RunTask(RunTaskRequest request, Action<RunTaskResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RunTaskResult>("/Admin/RunTask", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SendAccountRecoveryEmail(SendAccountRecoveryEmailRequest request, Action<SendAccountRecoveryEmailResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SendAccountRecoveryEmailResult>("/Admin/SendAccountRecoveryEmail", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetCatalogItems(UpdateCatalogItemsRequest request, Action<UpdateCatalogItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCatalogItemsResult>("/Admin/SetCatalogItems", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetPlayerSecret(SetPlayerSecretRequest request, Action<SetPlayerSecretResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetPlayerSecretResult>("/Admin/SetPlayerSecret", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetPublishedRevision(SetPublishedRevisionRequest request, Action<SetPublishedRevisionResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetPublishedRevisionResult>("/Admin/SetPublishedRevision", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetPublisherData(SetPublisherDataRequest request, Action<SetPublisherDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetPublisherDataResult>("/Admin/SetPublisherData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetStoreItems(UpdateStoreItemsRequest request, Action<UpdateStoreItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateStoreItemsResult>("/Admin/SetStoreItems", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetTitleData(SetTitleDataRequest request, Action<SetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetTitleDataResult>("/Admin/SetTitleData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetTitleInternalData(SetTitleDataRequest request, Action<SetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetTitleDataResult>("/Admin/SetTitleInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetupPushNotification(SetupPushNotificationRequest request, Action<SetupPushNotificationResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetupPushNotificationResult>("/Admin/SetupPushNotification", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SubtractUserVirtualCurrency(SubtractUserVirtualCurrencyRequest request, Action<ModifyUserVirtualCurrencyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyUserVirtualCurrencyResult>("/Admin/SubtractUserVirtualCurrency", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateBans(UpdateBansRequest request, Action<UpdateBansResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateBansResult>("/Admin/UpdateBans", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateCatalogItems(UpdateCatalogItemsRequest request, Action<UpdateCatalogItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCatalogItemsResult>("/Admin/UpdateCatalogItems", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateCloudScript(UpdateCloudScriptRequest request, Action<UpdateCloudScriptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCloudScriptResult>("/Admin/UpdateCloudScript", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdatePlayerSharedSecret(UpdatePlayerSharedSecretRequest request, Action<UpdatePlayerSharedSecretResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdatePlayerSharedSecretResult>("/Admin/UpdatePlayerSharedSecret", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdatePlayerStatisticDefinition(UpdatePlayerStatisticDefinitionRequest request, Action<UpdatePlayerStatisticDefinitionResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdatePlayerStatisticDefinitionResult>("/Admin/UpdatePlayerStatisticDefinition", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdatePolicy(UpdatePolicyRequest request, Action<UpdatePolicyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdatePolicyResponse>("/Admin/UpdatePolicy", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateRandomResultTables(UpdateRandomResultTablesRequest request, Action<UpdateRandomResultTablesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateRandomResultTablesResult>("/Admin/UpdateRandomResultTables", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateStoreItems(UpdateStoreItemsRequest request, Action<UpdateStoreItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateStoreItemsResult>("/Admin/UpdateStoreItems", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateTask(UpdateTaskRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Admin/UpdateTask", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Admin/UpdateUserData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserInternalData(UpdateUserInternalDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Admin/UpdateUserInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserPublisherData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Admin/UpdateUserPublisherData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserPublisherInternalData(UpdateUserInternalDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Admin/UpdateUserPublisherInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserPublisherReadOnlyData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Admin/UpdateUserPublisherReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserReadOnlyData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Admin/UpdateUserReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserTitleDisplayName(UpdateUserTitleDisplayNameRequest request, Action<UpdateUserTitleDisplayNameResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserTitleDisplayNameResult>("/Admin/UpdateUserTitleDisplayName", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
