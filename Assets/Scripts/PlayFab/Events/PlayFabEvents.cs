using System;
using System.Diagnostics;
using PlayFab.AdminModels;
using PlayFab.ClientModels;
using PlayFab.Internal;
using PlayFab.MatchmakerModels;
using PlayFab.ServerModels;
using PlayFab.SharedModels;

namespace PlayFab.Events
{
	public class PlayFabEvents
	{
		private PlayFabEvents()
		{
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AbortTaskInstanceRequest> OnAdminAbortTaskInstanceRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.EmptyResult> OnAdminAbortTaskInstanceResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddNewsRequest> OnAdminAddNewsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddNewsResult> OnAdminAddNewsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.AddPlayerTagRequest> OnAdminAddPlayerTagRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.AddPlayerTagResult> OnAdminAddPlayerTagResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddServerBuildRequest> OnAdminAddServerBuildRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddServerBuildResult> OnAdminAddServerBuildResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.AddUserVirtualCurrencyRequest> OnAdminAddUserVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.ModifyUserVirtualCurrencyResult> OnAdminAddUserVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddVirtualCurrencyTypesRequest> OnAdminAddVirtualCurrencyTypesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<BlankResult> OnAdminAddVirtualCurrencyTypesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.BanUsersRequest> OnAdminBanUsersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.BanUsersResult> OnAdminBanUsersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CheckLimitedEditionItemAvailabilityRequest> OnAdminCheckLimitedEditionItemAvailabilityRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CheckLimitedEditionItemAvailabilityResult> OnAdminCheckLimitedEditionItemAvailabilityResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreateActionsOnPlayerSegmentTaskRequest> OnAdminCreateActionsOnPlayersInSegmentTaskRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreateTaskResult> OnAdminCreateActionsOnPlayersInSegmentTaskResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreateCloudScriptTaskRequest> OnAdminCreateCloudScriptTaskRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreateTaskResult> OnAdminCreateCloudScriptTaskResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreatePlayerSharedSecretRequest> OnAdminCreatePlayerSharedSecretRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreatePlayerSharedSecretResult> OnAdminCreatePlayerSharedSecretResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreatePlayerStatisticDefinitionRequest> OnAdminCreatePlayerStatisticDefinitionRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreatePlayerStatisticDefinitionResult> OnAdminCreatePlayerStatisticDefinitionResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteContentRequest> OnAdminDeleteContentRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<BlankResult> OnAdminDeleteContentResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeletePlayerRequest> OnAdminDeletePlayerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<DeletePlayerResult> OnAdminDeletePlayerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeletePlayerSharedSecretRequest> OnAdminDeletePlayerSharedSecretRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<DeletePlayerSharedSecretResult> OnAdminDeletePlayerSharedSecretResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteStoreRequest> OnAdminDeleteStoreRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<DeleteStoreResult> OnAdminDeleteStoreResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteTaskRequest> OnAdminDeleteTaskRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.EmptyResult> OnAdminDeleteTaskResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteTitleRequest> OnAdminDeleteTitleRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<DeleteTitleResult> OnAdminDeleteTitleResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTaskInstanceRequest> OnAdminGetActionsOnPlayersInSegmentTaskInstanceRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetActionsOnPlayersInSegmentTaskInstanceResult> OnAdminGetActionsOnPlayersInSegmentTaskInstanceResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetAllSegmentsRequest> OnAdminGetAllSegmentsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetAllSegmentsResult> OnAdminGetAllSegmentsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetCatalogItemsRequest> OnAdminGetCatalogItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetCatalogItemsResult> OnAdminGetCatalogItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetCloudScriptRevisionRequest> OnAdminGetCloudScriptRevisionRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCloudScriptRevisionResult> OnAdminGetCloudScriptRevisionResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTaskInstanceRequest> OnAdminGetCloudScriptTaskInstanceRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCloudScriptTaskInstanceResult> OnAdminGetCloudScriptTaskInstanceResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetCloudScriptVersionsRequest> OnAdminGetCloudScriptVersionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCloudScriptVersionsResult> OnAdminGetCloudScriptVersionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetContentListRequest> OnAdminGetContentListRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetContentListResult> OnAdminGetContentListResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetContentUploadUrlRequest> OnAdminGetContentUploadUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetContentUploadUrlResult> OnAdminGetContentUploadUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetDataReportRequest> OnAdminGetDataReportRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetDataReportResult> OnAdminGetDataReportResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetMatchmakerGameInfoRequest> OnAdminGetMatchmakerGameInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetMatchmakerGameInfoResult> OnAdminGetMatchmakerGameInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetMatchmakerGameModesRequest> OnAdminGetMatchmakerGameModesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetMatchmakerGameModesResult> OnAdminGetMatchmakerGameModesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerIdFromAuthTokenRequest> OnAdminGetPlayerIdFromAuthTokenRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerIdFromAuthTokenResult> OnAdminGetPlayerIdFromAuthTokenResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayerProfileRequest> OnAdminGetPlayerProfileRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayerProfileResult> OnAdminGetPlayerProfileResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayersSegmentsRequest> OnAdminGetPlayerSegmentsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayerSegmentsResult> OnAdminGetPlayerSegmentsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerSharedSecretsRequest> OnAdminGetPlayerSharedSecretsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerSharedSecretsResult> OnAdminGetPlayerSharedSecretsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayersInSegmentRequest> OnAdminGetPlayersInSegmentRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayersInSegmentResult> OnAdminGetPlayersInSegmentResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticDefinitionsRequest> OnAdminGetPlayerStatisticDefinitionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticDefinitionsResult> OnAdminGetPlayerStatisticDefinitionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayerStatisticVersionsRequest> OnAdminGetPlayerStatisticVersionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayerStatisticVersionsResult> OnAdminGetPlayerStatisticVersionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayerTagsRequest> OnAdminGetPlayerTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayerTagsResult> OnAdminGetPlayerTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPolicyRequest> OnAdminGetPolicyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPolicyResponse> OnAdminGetPolicyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPublisherDataRequest> OnAdminGetPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPublisherDataResult> OnAdminGetPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetRandomResultTablesRequest> OnAdminGetRandomResultTablesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetRandomResultTablesResult> OnAdminGetRandomResultTablesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetServerBuildInfoRequest> OnAdminGetServerBuildInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetServerBuildInfoResult> OnAdminGetServerBuildInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetServerBuildUploadURLRequest> OnAdminGetServerBuildUploadUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetServerBuildUploadURLResult> OnAdminGetServerBuildUploadUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetStoreItemsRequest> OnAdminGetStoreItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetStoreItemsResult> OnAdminGetStoreItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTaskInstancesRequest> OnAdminGetTaskInstancesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTaskInstancesResult> OnAdminGetTaskInstancesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTasksRequest> OnAdminGetTasksRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTasksResult> OnAdminGetTasksResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetTitleDataRequest> OnAdminGetTitleDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetTitleDataResult> OnAdminGetTitleDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetTitleDataRequest> OnAdminGetTitleInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetTitleDataResult> OnAdminGetTitleInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LookupUserAccountInfoRequest> OnAdminGetUserAccountInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LookupUserAccountInfoResult> OnAdminGetUserAccountInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserBansRequest> OnAdminGetUserBansRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserBansResult> OnAdminGetUserBansResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest> OnAdminGetUserDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult> OnAdminGetUserDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest> OnAdminGetUserInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult> OnAdminGetUserInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserInventoryRequest> OnAdminGetUserInventoryRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserInventoryResult> OnAdminGetUserInventoryResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest> OnAdminGetUserPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult> OnAdminGetUserPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest> OnAdminGetUserPublisherInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult> OnAdminGetUserPublisherInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest> OnAdminGetUserPublisherReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult> OnAdminGetUserPublisherReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest> OnAdminGetUserReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult> OnAdminGetUserReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GrantItemsToUsersRequest> OnAdminGrantItemsToUsersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GrantItemsToUsersResult> OnAdminGrantItemsToUsersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<IncrementLimitedEditionItemAvailabilityRequest> OnAdminIncrementLimitedEditionItemAvailabilityRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<IncrementLimitedEditionItemAvailabilityResult> OnAdminIncrementLimitedEditionItemAvailabilityResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<IncrementPlayerStatisticVersionRequest> OnAdminIncrementPlayerStatisticVersionRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<IncrementPlayerStatisticVersionResult> OnAdminIncrementPlayerStatisticVersionResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListBuildsRequest> OnAdminListServerBuildsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListBuildsResult> OnAdminListServerBuildsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListVirtualCurrencyTypesRequest> OnAdminListVirtualCurrencyTypesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListVirtualCurrencyTypesResult> OnAdminListVirtualCurrencyTypesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ModifyMatchmakerGameModesRequest> OnAdminModifyMatchmakerGameModesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ModifyMatchmakerGameModesResult> OnAdminModifyMatchmakerGameModesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ModifyServerBuildRequest> OnAdminModifyServerBuildRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ModifyServerBuildResult> OnAdminModifyServerBuildResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RefundPurchaseRequest> OnAdminRefundPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RefundPurchaseResponse> OnAdminRefundPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.RemovePlayerTagRequest> OnAdminRemovePlayerTagRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.RemovePlayerTagResult> OnAdminRemovePlayerTagResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveServerBuildRequest> OnAdminRemoveServerBuildRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RemoveServerBuildResult> OnAdminRemoveServerBuildResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveVirtualCurrencyTypesRequest> OnAdminRemoveVirtualCurrencyTypesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<BlankResult> OnAdminRemoveVirtualCurrencyTypesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ResetCharacterStatisticsRequest> OnAdminResetCharacterStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ResetCharacterStatisticsResult> OnAdminResetCharacterStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ResetPasswordRequest> OnAdminResetPasswordRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ResetPasswordResult> OnAdminResetPasswordResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ResetUserStatisticsRequest> OnAdminResetUserStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ResetUserStatisticsResult> OnAdminResetUserStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ResolvePurchaseDisputeRequest> OnAdminResolvePurchaseDisputeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ResolvePurchaseDisputeResponse> OnAdminResolvePurchaseDisputeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.RevokeAllBansForUserRequest> OnAdminRevokeAllBansForUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.RevokeAllBansForUserResult> OnAdminRevokeAllBansForUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.RevokeBansRequest> OnAdminRevokeBansRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.RevokeBansResult> OnAdminRevokeBansResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.RevokeInventoryItemRequest> OnAdminRevokeInventoryItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.RevokeInventoryResult> OnAdminRevokeInventoryItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RunTaskRequest> OnAdminRunTaskRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RunTaskResult> OnAdminRunTaskResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SendAccountRecoveryEmailRequest> OnAdminSendAccountRecoveryEmailRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SendAccountRecoveryEmailResult> OnAdminSendAccountRecoveryEmailResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateCatalogItemsRequest> OnAdminSetCatalogItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateCatalogItemsResult> OnAdminSetCatalogItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SetPlayerSecretRequest> OnAdminSetPlayerSecretRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SetPlayerSecretResult> OnAdminSetPlayerSecretResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetPublishedRevisionRequest> OnAdminSetPublishedRevisionRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetPublishedRevisionResult> OnAdminSetPublishedRevisionResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SetPublisherDataRequest> OnAdminSetPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SetPublisherDataResult> OnAdminSetPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateStoreItemsRequest> OnAdminSetStoreItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateStoreItemsResult> OnAdminSetStoreItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SetTitleDataRequest> OnAdminSetTitleDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SetTitleDataResult> OnAdminSetTitleDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SetTitleDataRequest> OnAdminSetTitleInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SetTitleDataResult> OnAdminSetTitleInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetupPushNotificationRequest> OnAdminSetupPushNotificationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetupPushNotificationResult> OnAdminSetupPushNotificationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SubtractUserVirtualCurrencyRequest> OnAdminSubtractUserVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.ModifyUserVirtualCurrencyResult> OnAdminSubtractUserVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateBansRequest> OnAdminUpdateBansRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateBansResult> OnAdminUpdateBansResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateCatalogItemsRequest> OnAdminUpdateCatalogItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateCatalogItemsResult> OnAdminUpdateCatalogItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateCloudScriptRequest> OnAdminUpdateCloudScriptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateCloudScriptResult> OnAdminUpdateCloudScriptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdatePlayerSharedSecretRequest> OnAdminUpdatePlayerSharedSecretRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdatePlayerSharedSecretResult> OnAdminUpdatePlayerSharedSecretResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdatePlayerStatisticDefinitionRequest> OnAdminUpdatePlayerStatisticDefinitionRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdatePlayerStatisticDefinitionResult> OnAdminUpdatePlayerStatisticDefinitionResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdatePolicyRequest> OnAdminUpdatePolicyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdatePolicyResponse> OnAdminUpdatePolicyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateRandomResultTablesRequest> OnAdminUpdateRandomResultTablesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateRandomResultTablesResult> OnAdminUpdateRandomResultTablesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateStoreItemsRequest> OnAdminUpdateStoreItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateStoreItemsResult> OnAdminUpdateStoreItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateTaskRequest> OnAdminUpdateTaskRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.EmptyResult> OnAdminUpdateTaskResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserDataRequest> OnAdminUpdateUserDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult> OnAdminUpdateUserDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserInternalDataRequest> OnAdminUpdateUserInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult> OnAdminUpdateUserInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserDataRequest> OnAdminUpdateUserPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult> OnAdminUpdateUserPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserInternalDataRequest> OnAdminUpdateUserPublisherInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult> OnAdminUpdateUserPublisherInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserDataRequest> OnAdminUpdateUserPublisherReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult> OnAdminUpdateUserPublisherReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserDataRequest> OnAdminUpdateUserReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult> OnAdminUpdateUserReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserTitleDisplayNameRequest> OnAdminUpdateUserTitleDisplayNameRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserTitleDisplayNameResult> OnAdminUpdateUserTitleDisplayNameResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LoginResult> OnLoginResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AcceptTradeRequest> OnAcceptTradeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AcceptTradeResponse> OnAcceptTradeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.AddFriendRequest> OnAddFriendRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddFriendResult> OnAddFriendResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddGenericIDRequest> OnAddGenericIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddGenericIDResult> OnAddGenericIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddOrUpdateContactEmailRequest> OnAddOrUpdateContactEmailRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddOrUpdateContactEmailResult> OnAddOrUpdateContactEmailResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.AddSharedGroupMembersRequest> OnAddSharedGroupMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.AddSharedGroupMembersResult> OnAddSharedGroupMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddUsernamePasswordRequest> OnAddUsernamePasswordRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddUsernamePasswordResult> OnAddUsernamePasswordResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.AddUserVirtualCurrencyRequest> OnAddUserVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ModifyUserVirtualCurrencyResult> OnAddUserVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest> OnAndroidDevicePushNotificationRegistrationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult> OnAndroidDevicePushNotificationRegistrationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AttributeInstallRequest> OnAttributeInstallRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AttributeInstallResult> OnAttributeInstallResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CancelTradeRequest> OnCancelTradeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CancelTradeResponse> OnCancelTradeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ConfirmPurchaseRequest> OnConfirmPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ConfirmPurchaseResult> OnConfirmPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.ConsumeItemRequest> OnConsumeItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ConsumeItemResult> OnConsumeItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.CreateSharedGroupRequest> OnCreateSharedGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.CreateSharedGroupResult> OnCreateSharedGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ExecuteCloudScriptRequest> OnExecuteCloudScriptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult> OnExecuteCloudScriptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetAccountInfoRequest> OnGetAccountInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetAccountInfoResult> OnGetAccountInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.ListUsersCharactersRequest> OnGetAllUsersCharactersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ListUsersCharactersResult> OnGetAllUsersCharactersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCatalogItemsRequest> OnGetCatalogItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCatalogItemsResult> OnGetCatalogItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterDataRequest> OnGetCharacterDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterDataResult> OnGetCharacterDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterInventoryRequest> OnGetCharacterInventoryRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterInventoryResult> OnGetCharacterInventoryResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterLeaderboardRequest> OnGetCharacterLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterLeaderboardResult> OnGetCharacterLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterDataRequest> OnGetCharacterReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterDataResult> OnGetCharacterReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterStatisticsRequest> OnGetCharacterStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterStatisticsResult> OnGetCharacterStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetContentDownloadUrlRequest> OnGetContentDownloadUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetContentDownloadUrlResult> OnGetContentDownloadUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CurrentGamesRequest> OnGetCurrentGamesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CurrentGamesResult> OnGetCurrentGamesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetFriendLeaderboardRequest> OnGetFriendLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetLeaderboardResult> OnGetFriendLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest> OnGetFriendLeaderboardAroundPlayerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult> OnGetFriendLeaderboardAroundPlayerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetFriendsListRequest> OnGetFriendsListRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetFriendsListResult> OnGetFriendsListResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GameServerRegionsRequest> OnGetGameServerRegionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GameServerRegionsResult> OnGetGameServerRegionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetLeaderboardRequest> OnGetLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetLeaderboardResult> OnGetLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetLeaderboardAroundCharacterRequest> OnGetLeaderboardAroundCharacterRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetLeaderboardAroundCharacterResult> OnGetLeaderboardAroundCharacterResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest> OnGetLeaderboardAroundPlayerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundPlayerResult> OnGetLeaderboardAroundPlayerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetLeaderboardForUsersCharactersRequest> OnGetLeaderboardForUserCharactersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetLeaderboardForUsersCharactersResult> OnGetLeaderboardForUserCharactersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPaymentTokenRequest> OnGetPaymentTokenRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPaymentTokenResult> OnGetPaymentTokenResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest> OnGetPhotonAuthenticationTokenRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPhotonAuthenticationTokenResult> OnGetPhotonAuthenticationTokenResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerCombinedInfoRequest> OnGetPlayerCombinedInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerCombinedInfoResult> OnGetPlayerCombinedInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerProfileRequest> OnGetPlayerProfileRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerProfileResult> OnGetPlayerProfileResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerSegmentsRequest> OnGetPlayerSegmentsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerSegmentsResult> OnGetPlayerSegmentsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerStatisticsRequest> OnGetPlayerStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerStatisticsResult> OnGetPlayerStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerStatisticVersionsRequest> OnGetPlayerStatisticVersionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerStatisticVersionsResult> OnGetPlayerStatisticVersionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerTagsRequest> OnGetPlayerTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerTagsResult> OnGetPlayerTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerTradesRequest> OnGetPlayerTradesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerTradesResponse> OnGetPlayerTradesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayFabIDsFromFacebookIDsRequest> OnGetPlayFabIDsFromFacebookIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayFabIDsFromFacebookIDsResult> OnGetPlayFabIDsFromFacebookIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest> OnGetPlayFabIDsFromGameCenterIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult> OnGetPlayFabIDsFromGameCenterIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest> OnGetPlayFabIDsFromGenericIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult> OnGetPlayFabIDsFromGenericIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest> OnGetPlayFabIDsFromGoogleIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult> OnGetPlayFabIDsFromGoogleIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest> OnGetPlayFabIDsFromKongregateIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult> OnGetPlayFabIDsFromKongregateIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayFabIDsFromSteamIDsRequest> OnGetPlayFabIDsFromSteamIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayFabIDsFromSteamIDsResult> OnGetPlayFabIDsFromSteamIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest> OnGetPlayFabIDsFromTwitchIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult> OnGetPlayFabIDsFromTwitchIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPublisherDataRequest> OnGetPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPublisherDataResult> OnGetPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPurchaseRequest> OnGetPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPurchaseResult> OnGetPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetSharedGroupDataRequest> OnGetSharedGroupDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetSharedGroupDataResult> OnGetSharedGroupDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetStoreItemsRequest> OnGetStoreItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetStoreItemsResult> OnGetStoreItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetTimeRequest> OnGetTimeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetTimeResult> OnGetTimeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetTitleDataRequest> OnGetTitleDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetTitleDataResult> OnGetTitleDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetTitleNewsRequest> OnGetTitleNewsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetTitleNewsResult> OnGetTitleNewsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTitlePublicKeyRequest> OnGetTitlePublicKeyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTitlePublicKeyResult> OnGetTitlePublicKeyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTradeStatusRequest> OnGetTradeStatusRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTradeStatusResponse> OnGetTradeStatusResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserDataRequest> OnGetUserDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserDataResult> OnGetUserDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserInventoryRequest> OnGetUserInventoryRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserInventoryResult> OnGetUserInventoryResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserDataRequest> OnGetUserPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserDataResult> OnGetUserPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserDataRequest> OnGetUserPublisherReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserDataResult> OnGetUserPublisherReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserDataRequest> OnGetUserReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserDataResult> OnGetUserReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetWindowsHelloChallengeRequest> OnGetWindowsHelloChallengeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetWindowsHelloChallengeResponse> OnGetWindowsHelloChallengeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GrantCharacterToUserRequest> OnGrantCharacterToUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GrantCharacterToUserResult> OnGrantCharacterToUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkAndroidDeviceIDRequest> OnLinkAndroidDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkAndroidDeviceIDResult> OnLinkAndroidDeviceIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkCustomIDRequest> OnLinkCustomIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkCustomIDResult> OnLinkCustomIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkFacebookAccountRequest> OnLinkFacebookAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkFacebookAccountResult> OnLinkFacebookAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkGameCenterAccountRequest> OnLinkGameCenterAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkGameCenterAccountResult> OnLinkGameCenterAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkGoogleAccountRequest> OnLinkGoogleAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkGoogleAccountResult> OnLinkGoogleAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkIOSDeviceIDRequest> OnLinkIOSDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkIOSDeviceIDResult> OnLinkIOSDeviceIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkKongregateAccountRequest> OnLinkKongregateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkKongregateAccountResult> OnLinkKongregateResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkSteamAccountRequest> OnLinkSteamAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkSteamAccountResult> OnLinkSteamAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkTwitchAccountRequest> OnLinkTwitchRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkTwitchAccountResult> OnLinkTwitchResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkWindowsHelloAccountRequest> OnLinkWindowsHelloRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkWindowsHelloAccountResponse> OnLinkWindowsHelloResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest> OnLoginWithAndroidDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithCustomIDRequest> OnLoginWithCustomIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithEmailAddressRequest> OnLoginWithEmailAddressRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookRequest> OnLoginWithFacebookRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithGameCenterRequest> OnLoginWithGameCenterRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithGoogleAccountRequest> OnLoginWithGoogleAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithIOSDeviceIDRequest> OnLoginWithIOSDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithKongregateRequest> OnLoginWithKongregateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithPlayFabRequest> OnLoginWithPlayFabRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithSteamRequest> OnLoginWithSteamRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithTwitchRequest> OnLoginWithTwitchRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithWindowsHelloRequest> OnLoginWithWindowsHelloRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<MatchmakeRequest> OnMatchmakeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<MatchmakeResult> OnMatchmakeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<OpenTradeRequest> OnOpenTradeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<OpenTradeResponse> OnOpenTradeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PayForPurchaseRequest> OnPayForPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PayForPurchaseResult> OnPayForPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PurchaseItemRequest> OnPurchaseItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PurchaseItemResult> OnPurchaseItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.RedeemCouponRequest> OnRedeemCouponRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.RedeemCouponResult> OnRedeemCouponResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RegisterForIOSPushNotificationRequest> OnRegisterForIOSPushNotificationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RegisterForIOSPushNotificationResult> OnRegisterForIOSPushNotificationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RegisterPlayFabUserRequest> OnRegisterPlayFabUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RegisterPlayFabUserResult> OnRegisterPlayFabUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RegisterWithWindowsHelloRequest> OnRegisterWithWindowsHelloRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveContactEmailRequest> OnRemoveContactEmailRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RemoveContactEmailResult> OnRemoveContactEmailResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.RemoveFriendRequest> OnRemoveFriendRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RemoveFriendResult> OnRemoveFriendResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveGenericIDRequest> OnRemoveGenericIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RemoveGenericIDResult> OnRemoveGenericIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.RemoveSharedGroupMembersRequest> OnRemoveSharedGroupMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.RemoveSharedGroupMembersResult> OnRemoveSharedGroupMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeviceInfoRequest> OnReportDeviceInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult> OnReportDeviceInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ReportPlayerClientRequest> OnReportPlayerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ReportPlayerClientResult> OnReportPlayerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RestoreIOSPurchasesRequest> OnRestoreIOSPurchasesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RestoreIOSPurchasesResult> OnRestoreIOSPurchasesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.SendAccountRecoveryEmailRequest> OnSendAccountRecoveryEmailRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.SendAccountRecoveryEmailResult> OnSendAccountRecoveryEmailResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.SetFriendTagsRequest> OnSetFriendTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetFriendTagsResult> OnSetFriendTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.SetPlayerSecretRequest> OnSetPlayerSecretRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.SetPlayerSecretResult> OnSetPlayerSecretResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.StartGameRequest> OnStartGameRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<StartGameResult> OnStartGameResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<StartPurchaseRequest> OnStartPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<StartPurchaseResult> OnStartPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.SubtractUserVirtualCurrencyRequest> OnSubtractUserVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ModifyUserVirtualCurrencyResult> OnSubtractUserVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest> OnUnlinkAndroidDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkAndroidDeviceIDResult> OnUnlinkAndroidDeviceIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkCustomIDRequest> OnUnlinkCustomIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkCustomIDResult> OnUnlinkCustomIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookAccountRequest> OnUnlinkFacebookAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkFacebookAccountResult> OnUnlinkFacebookAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkGameCenterAccountRequest> OnUnlinkGameCenterAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkGameCenterAccountResult> OnUnlinkGameCenterAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkGoogleAccountRequest> OnUnlinkGoogleAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkGoogleAccountResult> OnUnlinkGoogleAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkIOSDeviceIDRequest> OnUnlinkIOSDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkIOSDeviceIDResult> OnUnlinkIOSDeviceIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkKongregateAccountRequest> OnUnlinkKongregateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkKongregateAccountResult> OnUnlinkKongregateResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkSteamAccountRequest> OnUnlinkSteamAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkSteamAccountResult> OnUnlinkSteamAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkTwitchAccountRequest> OnUnlinkTwitchRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkTwitchAccountResult> OnUnlinkTwitchResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest> OnUnlinkWindowsHelloRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkWindowsHelloAccountResponse> OnUnlinkWindowsHelloResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UnlockContainerInstanceRequest> OnUnlockContainerInstanceRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UnlockContainerItemResult> OnUnlockContainerInstanceResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UnlockContainerItemRequest> OnUnlockContainerItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UnlockContainerItemResult> OnUnlockContainerItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateAvatarUrlRequest> OnUpdateAvatarUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult> OnUpdateAvatarUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateCharacterDataRequest> OnUpdateCharacterDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateCharacterDataResult> OnUpdateCharacterDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateCharacterStatisticsRequest> OnUpdateCharacterStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateCharacterStatisticsResult> OnUpdateCharacterStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdatePlayerStatisticsRequest> OnUpdatePlayerStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdatePlayerStatisticsResult> OnUpdatePlayerStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateSharedGroupDataRequest> OnUpdateSharedGroupDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateSharedGroupDataResult> OnUpdateSharedGroupDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateUserDataRequest> OnUpdateUserDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateUserDataResult> OnUpdateUserDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateUserDataRequest> OnUpdateUserPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateUserDataResult> OnUpdateUserPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest> OnUpdateUserTitleDisplayNameRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateUserTitleDisplayNameResult> OnUpdateUserTitleDisplayNameResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ValidateAmazonReceiptRequest> OnValidateAmazonIAPReceiptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ValidateAmazonReceiptResult> OnValidateAmazonIAPReceiptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest> OnValidateGooglePlayPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ValidateGooglePlayPurchaseResult> OnValidateGooglePlayPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ValidateIOSReceiptRequest> OnValidateIOSReceiptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ValidateIOSReceiptResult> OnValidateIOSReceiptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ValidateWindowsReceiptRequest> OnValidateWindowsStoreReceiptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ValidateWindowsReceiptResult> OnValidateWindowsStoreReceiptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<WriteClientCharacterEventRequest> OnWriteCharacterEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.WriteEventResponse> OnWriteCharacterEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<WriteClientPlayerEventRequest> OnWritePlayerEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.WriteEventResponse> OnWritePlayerEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.WriteTitleEventRequest> OnWriteTitleEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.WriteEventResponse> OnWriteTitleEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AuthUserRequest> OnMatchmakerAuthUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AuthUserResponse> OnMatchmakerAuthUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayerJoinedRequest> OnMatchmakerPlayerJoinedRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayerJoinedResponse> OnMatchmakerPlayerJoinedResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayerLeftRequest> OnMatchmakerPlayerLeftRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayerLeftResponse> OnMatchmakerPlayerLeftResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.MatchmakerModels.StartGameRequest> OnMatchmakerStartGameRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<StartGameResponse> OnMatchmakerStartGameResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UserInfoRequest> OnMatchmakerUserInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UserInfoResponse> OnMatchmakerUserInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddCharacterVirtualCurrencyRequest> OnServerAddCharacterVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ModifyCharacterVirtualCurrencyResult> OnServerAddCharacterVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.AddFriendRequest> OnServerAddFriendRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult> OnServerAddFriendResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.AddPlayerTagRequest> OnServerAddPlayerTagRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.AddPlayerTagResult> OnServerAddPlayerTagResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.AddSharedGroupMembersRequest> OnServerAddSharedGroupMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.AddSharedGroupMembersResult> OnServerAddSharedGroupMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.AddUserVirtualCurrencyRequest> OnServerAddUserVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ModifyUserVirtualCurrencyResult> OnServerAddUserVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AuthenticateSessionTicketRequest> OnServerAuthenticateSessionTicketRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AuthenticateSessionTicketResult> OnServerAuthenticateSessionTicketResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AwardSteamAchievementRequest> OnServerAwardSteamAchievementRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AwardSteamAchievementResult> OnServerAwardSteamAchievementResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.BanUsersRequest> OnServerBanUsersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.BanUsersResult> OnServerBanUsersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.ConsumeItemRequest> OnServerConsumeItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ConsumeItemResult> OnServerConsumeItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.CreateSharedGroupRequest> OnServerCreateSharedGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.CreateSharedGroupResult> OnServerCreateSharedGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteCharacterFromUserRequest> OnServerDeleteCharacterFromUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<DeleteCharacterFromUserResult> OnServerDeleteCharacterFromUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteSharedGroupRequest> OnServerDeleteSharedGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult> OnServerDeleteSharedGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.DeleteUsersRequest> OnServerDeleteUsersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.DeleteUsersResult> OnServerDeleteUsersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.DeregisterGameRequest> OnServerDeregisterGameRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.DeregisterGameResponse> OnServerDeregisterGameResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<EvaluateRandomResultTableRequest> OnServerEvaluateRandomResultTableRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<EvaluateRandomResultTableResult> OnServerEvaluateRandomResultTableResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ExecuteCloudScriptServerRequest> OnServerExecuteCloudScriptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ExecuteCloudScriptResult> OnServerExecuteCloudScriptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetAllSegmentsRequest> OnServerGetAllSegmentsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetAllSegmentsResult> OnServerGetAllSegmentsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.ListUsersCharactersRequest> OnServerGetAllUsersCharactersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ListUsersCharactersResult> OnServerGetAllUsersCharactersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCatalogItemsRequest> OnServerGetCatalogItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCatalogItemsResult> OnServerGetCatalogItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterDataRequest> OnServerGetCharacterDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterDataResult> OnServerGetCharacterDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterDataRequest> OnServerGetCharacterInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterDataResult> OnServerGetCharacterInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterInventoryRequest> OnServerGetCharacterInventoryRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterInventoryResult> OnServerGetCharacterInventoryResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterLeaderboardRequest> OnServerGetCharacterLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterLeaderboardResult> OnServerGetCharacterLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterDataRequest> OnServerGetCharacterReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterDataResult> OnServerGetCharacterReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterStatisticsRequest> OnServerGetCharacterStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterStatisticsResult> OnServerGetCharacterStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetContentDownloadUrlRequest> OnServerGetContentDownloadUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetContentDownloadUrlResult> OnServerGetContentDownloadUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetFriendLeaderboardRequest> OnServerGetFriendLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetLeaderboardResult> OnServerGetFriendLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetFriendsListRequest> OnServerGetFriendsListRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetFriendsListResult> OnServerGetFriendsListResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetLeaderboardRequest> OnServerGetLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetLeaderboardResult> OnServerGetLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetLeaderboardAroundCharacterRequest> OnServerGetLeaderboardAroundCharacterRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetLeaderboardAroundCharacterResult> OnServerGetLeaderboardAroundCharacterResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundUserRequest> OnServerGetLeaderboardAroundUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundUserResult> OnServerGetLeaderboardAroundUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetLeaderboardForUsersCharactersRequest> OnServerGetLeaderboardForUserCharactersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetLeaderboardForUsersCharactersResult> OnServerGetLeaderboardForUserCharactersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerCombinedInfoRequest> OnServerGetPlayerCombinedInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerCombinedInfoResult> OnServerGetPlayerCombinedInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerProfileRequest> OnServerGetPlayerProfileRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerProfileResult> OnServerGetPlayerProfileResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayersSegmentsRequest> OnServerGetPlayerSegmentsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerSegmentsResult> OnServerGetPlayerSegmentsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayersInSegmentRequest> OnServerGetPlayersInSegmentRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayersInSegmentResult> OnServerGetPlayersInSegmentResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerStatisticsRequest> OnServerGetPlayerStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerStatisticsResult> OnServerGetPlayerStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerStatisticVersionsRequest> OnServerGetPlayerStatisticVersionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerStatisticVersionsResult> OnServerGetPlayerStatisticVersionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerTagsRequest> OnServerGetPlayerTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerTagsResult> OnServerGetPlayerTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayFabIDsFromFacebookIDsRequest> OnServerGetPlayFabIDsFromFacebookIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayFabIDsFromFacebookIDsResult> OnServerGetPlayFabIDsFromFacebookIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayFabIDsFromSteamIDsRequest> OnServerGetPlayFabIDsFromSteamIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayFabIDsFromSteamIDsResult> OnServerGetPlayFabIDsFromSteamIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPublisherDataRequest> OnServerGetPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPublisherDataResult> OnServerGetPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetRandomResultTablesRequest> OnServerGetRandomResultTablesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetRandomResultTablesResult> OnServerGetRandomResultTablesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetSharedGroupDataRequest> OnServerGetSharedGroupDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetSharedGroupDataResult> OnServerGetSharedGroupDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetTimeRequest> OnServerGetTimeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetTimeResult> OnServerGetTimeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetTitleDataRequest> OnServerGetTitleDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetTitleDataResult> OnServerGetTitleDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetTitleDataRequest> OnServerGetTitleInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetTitleDataResult> OnServerGetTitleInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetTitleNewsRequest> OnServerGetTitleNewsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetTitleNewsResult> OnServerGetTitleNewsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetUserAccountInfoRequest> OnServerGetUserAccountInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetUserAccountInfoResult> OnServerGetUserAccountInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserBansRequest> OnServerGetUserBansRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserBansResult> OnServerGetUserBansResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest> OnServerGetUserDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult> OnServerGetUserDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest> OnServerGetUserInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult> OnServerGetUserInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserInventoryRequest> OnServerGetUserInventoryRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserInventoryResult> OnServerGetUserInventoryResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest> OnServerGetUserPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult> OnServerGetUserPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest> OnServerGetUserPublisherInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult> OnServerGetUserPublisherInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest> OnServerGetUserPublisherReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult> OnServerGetUserPublisherReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest> OnServerGetUserReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult> OnServerGetUserReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GrantCharacterToUserRequest> OnServerGrantCharacterToUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GrantCharacterToUserResult> OnServerGrantCharacterToUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GrantItemsToCharacterRequest> OnServerGrantItemsToCharacterRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GrantItemsToCharacterResult> OnServerGrantItemsToCharacterResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GrantItemsToUserRequest> OnServerGrantItemsToUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GrantItemsToUserResult> OnServerGrantItemsToUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GrantItemsToUsersRequest> OnServerGrantItemsToUsersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GrantItemsToUsersResult> OnServerGrantItemsToUsersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ModifyItemUsesRequest> OnServerModifyItemUsesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ModifyItemUsesResult> OnServerModifyItemUsesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<MoveItemToCharacterFromCharacterRequest> OnServerMoveItemToCharacterFromCharacterRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<MoveItemToCharacterFromCharacterResult> OnServerMoveItemToCharacterFromCharacterResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<MoveItemToCharacterFromUserRequest> OnServerMoveItemToCharacterFromUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<MoveItemToCharacterFromUserResult> OnServerMoveItemToCharacterFromUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<MoveItemToUserFromCharacterRequest> OnServerMoveItemToUserFromCharacterRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<MoveItemToUserFromCharacterResult> OnServerMoveItemToUserFromCharacterResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<NotifyMatchmakerPlayerLeftRequest> OnServerNotifyMatchmakerPlayerLeftRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<NotifyMatchmakerPlayerLeftResult> OnServerNotifyMatchmakerPlayerLeftResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RedeemCouponRequest> OnServerRedeemCouponRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RedeemCouponResult> OnServerRedeemCouponResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RedeemMatchmakerTicketRequest> OnServerRedeemMatchmakerTicketRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RedeemMatchmakerTicketResult> OnServerRedeemMatchmakerTicketResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RefreshGameServerInstanceHeartbeatRequest> OnServerRefreshGameServerInstanceHeartbeatRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RefreshGameServerInstanceHeartbeatResult> OnServerRefreshGameServerInstanceHeartbeatResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RegisterGameRequest> OnServerRegisterGameRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RegisterGameResponse> OnServerRegisterGameResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RemoveFriendRequest> OnServerRemoveFriendRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult> OnServerRemoveFriendResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RemovePlayerTagRequest> OnServerRemovePlayerTagRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RemovePlayerTagResult> OnServerRemovePlayerTagResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RemoveSharedGroupMembersRequest> OnServerRemoveSharedGroupMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RemoveSharedGroupMembersResult> OnServerRemoveSharedGroupMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ReportPlayerServerRequest> OnServerReportPlayerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ReportPlayerServerResult> OnServerReportPlayerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RevokeAllBansForUserRequest> OnServerRevokeAllBansForUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RevokeAllBansForUserResult> OnServerRevokeAllBansForUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RevokeBansRequest> OnServerRevokeBansRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RevokeBansResult> OnServerRevokeBansResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RevokeInventoryItemRequest> OnServerRevokeInventoryItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RevokeInventoryResult> OnServerRevokeInventoryItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SendCustomAccountRecoveryEmailRequest> OnServerSendCustomAccountRecoveryEmailRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SendCustomAccountRecoveryEmailResult> OnServerSendCustomAccountRecoveryEmailResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SendEmailFromTemplateRequest> OnServerSendEmailFromTemplateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SendEmailFromTemplateResult> OnServerSendEmailFromTemplateResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SendPushNotificationRequest> OnServerSendPushNotificationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SendPushNotificationResult> OnServerSendPushNotificationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetFriendTagsRequest> OnServerSetFriendTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult> OnServerSetFriendTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetGameServerInstanceDataRequest> OnServerSetGameServerInstanceDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetGameServerInstanceDataResult> OnServerSetGameServerInstanceDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetGameServerInstanceStateRequest> OnServerSetGameServerInstanceStateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetGameServerInstanceStateResult> OnServerSetGameServerInstanceStateResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetGameServerInstanceTagsRequest> OnServerSetGameServerInstanceTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetGameServerInstanceTagsResult> OnServerSetGameServerInstanceTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetPlayerSecretRequest> OnServerSetPlayerSecretRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.SetPlayerSecretResult> OnServerSetPlayerSecretResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetPublisherDataRequest> OnServerSetPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.SetPublisherDataResult> OnServerSetPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetTitleDataRequest> OnServerSetTitleDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.SetTitleDataResult> OnServerSetTitleDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetTitleDataRequest> OnServerSetTitleInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.SetTitleDataResult> OnServerSetTitleInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SubtractCharacterVirtualCurrencyRequest> OnServerSubtractCharacterVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ModifyCharacterVirtualCurrencyResult> OnServerSubtractCharacterVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SubtractUserVirtualCurrencyRequest> OnServerSubtractUserVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ModifyUserVirtualCurrencyResult> OnServerSubtractUserVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UnlockContainerInstanceRequest> OnServerUnlockContainerInstanceRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UnlockContainerItemResult> OnServerUnlockContainerInstanceResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UnlockContainerItemRequest> OnServerUnlockContainerItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UnlockContainerItemResult> OnServerUnlockContainerItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateAvatarUrlRequest> OnServerUpdateAvatarUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult> OnServerUpdateAvatarUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateBansRequest> OnServerUpdateBansRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateBansResult> OnServerUpdateBansResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateCharacterDataRequest> OnServerUpdateCharacterDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateCharacterDataResult> OnServerUpdateCharacterDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateCharacterDataRequest> OnServerUpdateCharacterInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateCharacterDataResult> OnServerUpdateCharacterInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateCharacterDataRequest> OnServerUpdateCharacterReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateCharacterDataResult> OnServerUpdateCharacterReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateCharacterStatisticsRequest> OnServerUpdateCharacterStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateCharacterStatisticsResult> OnServerUpdateCharacterStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdatePlayerStatisticsRequest> OnServerUpdatePlayerStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdatePlayerStatisticsResult> OnServerUpdatePlayerStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateSharedGroupDataRequest> OnServerUpdateSharedGroupDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateSharedGroupDataResult> OnServerUpdateSharedGroupDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserDataRequest> OnServerUpdateUserDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult> OnServerUpdateUserDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserInternalDataRequest> OnServerUpdateUserInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult> OnServerUpdateUserInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateUserInventoryItemDataRequest> OnServerUpdateUserInventoryItemCustomDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult> OnServerUpdateUserInventoryItemCustomDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserDataRequest> OnServerUpdateUserPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult> OnServerUpdateUserPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserInternalDataRequest> OnServerUpdateUserPublisherInternalDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult> OnServerUpdateUserPublisherInternalDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserDataRequest> OnServerUpdateUserPublisherReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult> OnServerUpdateUserPublisherReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserDataRequest> OnServerUpdateUserReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult> OnServerUpdateUserReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<WriteServerCharacterEventRequest> OnServerWriteCharacterEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.WriteEventResponse> OnServerWriteCharacterEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<WriteServerPlayerEventRequest> OnServerWritePlayerEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.WriteEventResponse> OnServerWritePlayerEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.WriteTitleEventRequest> OnServerWriteTitleEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.WriteEventResponse> OnServerWriteTitleEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabErrorEvent OnGlobalErrorEvent;

		public static PlayFabEvents Init()
		{
			if (PlayFabEvents._instance == null)
			{
				PlayFabEvents._instance = new PlayFabEvents();
			}
			PlayFabHttp.ApiProcessingEventHandler += PlayFabEvents._instance.OnProcessingEvent;
			PlayFabHttp.ApiProcessingErrorEventHandler += PlayFabEvents._instance.OnProcessingErrorEvent;
			return PlayFabEvents._instance;
		}

		public void UnregisterInstance(object instance)
		{
			if (this.OnLoginResultEvent != null)
			{
				foreach (Delegate @delegate in this.OnLoginResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(@delegate.Target, instance))
					{
						this.OnLoginResultEvent -= (PlayFabEvents.PlayFabResultEvent<LoginResult>)@delegate;
					}
				}
			}
			if (this.OnAdminAbortTaskInstanceRequestEvent != null)
			{
				foreach (Delegate delegate2 in this.OnAdminAbortTaskInstanceRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate2.Target, instance))
					{
						this.OnAdminAbortTaskInstanceRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AbortTaskInstanceRequest>)delegate2;
					}
				}
			}
			if (this.OnAdminAbortTaskInstanceResultEvent != null)
			{
				foreach (Delegate delegate3 in this.OnAdminAbortTaskInstanceResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate3.Target, instance))
					{
						this.OnAdminAbortTaskInstanceResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.EmptyResult>)delegate3;
					}
				}
			}
			if (this.OnAdminAddNewsRequestEvent != null)
			{
				foreach (Delegate delegate4 in this.OnAdminAddNewsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate4.Target, instance))
					{
						this.OnAdminAddNewsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddNewsRequest>)delegate4;
					}
				}
			}
			if (this.OnAdminAddNewsResultEvent != null)
			{
				foreach (Delegate delegate5 in this.OnAdminAddNewsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate5.Target, instance))
					{
						this.OnAdminAddNewsResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddNewsResult>)delegate5;
					}
				}
			}
			if (this.OnAdminAddPlayerTagRequestEvent != null)
			{
				foreach (Delegate delegate6 in this.OnAdminAddPlayerTagRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate6.Target, instance))
					{
						this.OnAdminAddPlayerTagRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.AddPlayerTagRequest>)delegate6;
					}
				}
			}
			if (this.OnAdminAddPlayerTagResultEvent != null)
			{
				foreach (Delegate delegate7 in this.OnAdminAddPlayerTagResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate7.Target, instance))
					{
						this.OnAdminAddPlayerTagResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.AddPlayerTagResult>)delegate7;
					}
				}
			}
			if (this.OnAdminAddServerBuildRequestEvent != null)
			{
				foreach (Delegate delegate8 in this.OnAdminAddServerBuildRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate8.Target, instance))
					{
						this.OnAdminAddServerBuildRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddServerBuildRequest>)delegate8;
					}
				}
			}
			if (this.OnAdminAddServerBuildResultEvent != null)
			{
				foreach (Delegate delegate9 in this.OnAdminAddServerBuildResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate9.Target, instance))
					{
						this.OnAdminAddServerBuildResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddServerBuildResult>)delegate9;
					}
				}
			}
			if (this.OnAdminAddUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate10 in this.OnAdminAddUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate10.Target, instance))
					{
						this.OnAdminAddUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.AddUserVirtualCurrencyRequest>)delegate10;
					}
				}
			}
			if (this.OnAdminAddUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate11 in this.OnAdminAddUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate11.Target, instance))
					{
						this.OnAdminAddUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.ModifyUserVirtualCurrencyResult>)delegate11;
					}
				}
			}
			if (this.OnAdminAddVirtualCurrencyTypesRequestEvent != null)
			{
				foreach (Delegate delegate12 in this.OnAdminAddVirtualCurrencyTypesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate12.Target, instance))
					{
						this.OnAdminAddVirtualCurrencyTypesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddVirtualCurrencyTypesRequest>)delegate12;
					}
				}
			}
			if (this.OnAdminAddVirtualCurrencyTypesResultEvent != null)
			{
				foreach (Delegate delegate13 in this.OnAdminAddVirtualCurrencyTypesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate13.Target, instance))
					{
						this.OnAdminAddVirtualCurrencyTypesResultEvent -= (PlayFabEvents.PlayFabResultEvent<BlankResult>)delegate13;
					}
				}
			}
			if (this.OnAdminBanUsersRequestEvent != null)
			{
				foreach (Delegate delegate14 in this.OnAdminBanUsersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate14.Target, instance))
					{
						this.OnAdminBanUsersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.BanUsersRequest>)delegate14;
					}
				}
			}
			if (this.OnAdminBanUsersResultEvent != null)
			{
				foreach (Delegate delegate15 in this.OnAdminBanUsersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate15.Target, instance))
					{
						this.OnAdminBanUsersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.BanUsersResult>)delegate15;
					}
				}
			}
			if (this.OnAdminCheckLimitedEditionItemAvailabilityRequestEvent != null)
			{
				foreach (Delegate delegate16 in this.OnAdminCheckLimitedEditionItemAvailabilityRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate16.Target, instance))
					{
						this.OnAdminCheckLimitedEditionItemAvailabilityRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CheckLimitedEditionItemAvailabilityRequest>)delegate16;
					}
				}
			}
			if (this.OnAdminCheckLimitedEditionItemAvailabilityResultEvent != null)
			{
				foreach (Delegate delegate17 in this.OnAdminCheckLimitedEditionItemAvailabilityResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate17.Target, instance))
					{
						this.OnAdminCheckLimitedEditionItemAvailabilityResultEvent -= (PlayFabEvents.PlayFabResultEvent<CheckLimitedEditionItemAvailabilityResult>)delegate17;
					}
				}
			}
			if (this.OnAdminCreateActionsOnPlayersInSegmentTaskRequestEvent != null)
			{
				foreach (Delegate delegate18 in this.OnAdminCreateActionsOnPlayersInSegmentTaskRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate18.Target, instance))
					{
						this.OnAdminCreateActionsOnPlayersInSegmentTaskRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateActionsOnPlayerSegmentTaskRequest>)delegate18;
					}
				}
			}
			if (this.OnAdminCreateActionsOnPlayersInSegmentTaskResultEvent != null)
			{
				foreach (Delegate delegate19 in this.OnAdminCreateActionsOnPlayersInSegmentTaskResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate19.Target, instance))
					{
						this.OnAdminCreateActionsOnPlayersInSegmentTaskResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateTaskResult>)delegate19;
					}
				}
			}
			if (this.OnAdminCreateCloudScriptTaskRequestEvent != null)
			{
				foreach (Delegate delegate20 in this.OnAdminCreateCloudScriptTaskRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate20.Target, instance))
					{
						this.OnAdminCreateCloudScriptTaskRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateCloudScriptTaskRequest>)delegate20;
					}
				}
			}
			if (this.OnAdminCreateCloudScriptTaskResultEvent != null)
			{
				foreach (Delegate delegate21 in this.OnAdminCreateCloudScriptTaskResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate21.Target, instance))
					{
						this.OnAdminCreateCloudScriptTaskResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateTaskResult>)delegate21;
					}
				}
			}
			if (this.OnAdminCreatePlayerSharedSecretRequestEvent != null)
			{
				foreach (Delegate delegate22 in this.OnAdminCreatePlayerSharedSecretRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate22.Target, instance))
					{
						this.OnAdminCreatePlayerSharedSecretRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreatePlayerSharedSecretRequest>)delegate22;
					}
				}
			}
			if (this.OnAdminCreatePlayerSharedSecretResultEvent != null)
			{
				foreach (Delegate delegate23 in this.OnAdminCreatePlayerSharedSecretResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate23.Target, instance))
					{
						this.OnAdminCreatePlayerSharedSecretResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreatePlayerSharedSecretResult>)delegate23;
					}
				}
			}
			if (this.OnAdminCreatePlayerStatisticDefinitionRequestEvent != null)
			{
				foreach (Delegate delegate24 in this.OnAdminCreatePlayerStatisticDefinitionRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate24.Target, instance))
					{
						this.OnAdminCreatePlayerStatisticDefinitionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreatePlayerStatisticDefinitionRequest>)delegate24;
					}
				}
			}
			if (this.OnAdminCreatePlayerStatisticDefinitionResultEvent != null)
			{
				foreach (Delegate delegate25 in this.OnAdminCreatePlayerStatisticDefinitionResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate25.Target, instance))
					{
						this.OnAdminCreatePlayerStatisticDefinitionResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreatePlayerStatisticDefinitionResult>)delegate25;
					}
				}
			}
			if (this.OnAdminDeleteContentRequestEvent != null)
			{
				foreach (Delegate delegate26 in this.OnAdminDeleteContentRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate26.Target, instance))
					{
						this.OnAdminDeleteContentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteContentRequest>)delegate26;
					}
				}
			}
			if (this.OnAdminDeleteContentResultEvent != null)
			{
				foreach (Delegate delegate27 in this.OnAdminDeleteContentResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate27.Target, instance))
					{
						this.OnAdminDeleteContentResultEvent -= (PlayFabEvents.PlayFabResultEvent<BlankResult>)delegate27;
					}
				}
			}
			if (this.OnAdminDeletePlayerRequestEvent != null)
			{
				foreach (Delegate delegate28 in this.OnAdminDeletePlayerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate28.Target, instance))
					{
						this.OnAdminDeletePlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeletePlayerRequest>)delegate28;
					}
				}
			}
			if (this.OnAdminDeletePlayerResultEvent != null)
			{
				foreach (Delegate delegate29 in this.OnAdminDeletePlayerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate29.Target, instance))
					{
						this.OnAdminDeletePlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<DeletePlayerResult>)delegate29;
					}
				}
			}
			if (this.OnAdminDeletePlayerSharedSecretRequestEvent != null)
			{
				foreach (Delegate delegate30 in this.OnAdminDeletePlayerSharedSecretRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate30.Target, instance))
					{
						this.OnAdminDeletePlayerSharedSecretRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeletePlayerSharedSecretRequest>)delegate30;
					}
				}
			}
			if (this.OnAdminDeletePlayerSharedSecretResultEvent != null)
			{
				foreach (Delegate delegate31 in this.OnAdminDeletePlayerSharedSecretResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate31.Target, instance))
					{
						this.OnAdminDeletePlayerSharedSecretResultEvent -= (PlayFabEvents.PlayFabResultEvent<DeletePlayerSharedSecretResult>)delegate31;
					}
				}
			}
			if (this.OnAdminDeleteStoreRequestEvent != null)
			{
				foreach (Delegate delegate32 in this.OnAdminDeleteStoreRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate32.Target, instance))
					{
						this.OnAdminDeleteStoreRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteStoreRequest>)delegate32;
					}
				}
			}
			if (this.OnAdminDeleteStoreResultEvent != null)
			{
				foreach (Delegate delegate33 in this.OnAdminDeleteStoreResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate33.Target, instance))
					{
						this.OnAdminDeleteStoreResultEvent -= (PlayFabEvents.PlayFabResultEvent<DeleteStoreResult>)delegate33;
					}
				}
			}
			if (this.OnAdminDeleteTaskRequestEvent != null)
			{
				foreach (Delegate delegate34 in this.OnAdminDeleteTaskRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate34.Target, instance))
					{
						this.OnAdminDeleteTaskRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteTaskRequest>)delegate34;
					}
				}
			}
			if (this.OnAdminDeleteTaskResultEvent != null)
			{
				foreach (Delegate delegate35 in this.OnAdminDeleteTaskResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate35.Target, instance))
					{
						this.OnAdminDeleteTaskResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.EmptyResult>)delegate35;
					}
				}
			}
			if (this.OnAdminDeleteTitleRequestEvent != null)
			{
				foreach (Delegate delegate36 in this.OnAdminDeleteTitleRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate36.Target, instance))
					{
						this.OnAdminDeleteTitleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteTitleRequest>)delegate36;
					}
				}
			}
			if (this.OnAdminDeleteTitleResultEvent != null)
			{
				foreach (Delegate delegate37 in this.OnAdminDeleteTitleResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate37.Target, instance))
					{
						this.OnAdminDeleteTitleResultEvent -= (PlayFabEvents.PlayFabResultEvent<DeleteTitleResult>)delegate37;
					}
				}
			}
			if (this.OnAdminGetActionsOnPlayersInSegmentTaskInstanceRequestEvent != null)
			{
				foreach (Delegate delegate38 in this.OnAdminGetActionsOnPlayersInSegmentTaskInstanceRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate38.Target, instance))
					{
						this.OnAdminGetActionsOnPlayersInSegmentTaskInstanceRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTaskInstanceRequest>)delegate38;
					}
				}
			}
			if (this.OnAdminGetActionsOnPlayersInSegmentTaskInstanceResultEvent != null)
			{
				foreach (Delegate delegate39 in this.OnAdminGetActionsOnPlayersInSegmentTaskInstanceResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate39.Target, instance))
					{
						this.OnAdminGetActionsOnPlayersInSegmentTaskInstanceResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetActionsOnPlayersInSegmentTaskInstanceResult>)delegate39;
					}
				}
			}
			if (this.OnAdminGetAllSegmentsRequestEvent != null)
			{
				foreach (Delegate delegate40 in this.OnAdminGetAllSegmentsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate40.Target, instance))
					{
						this.OnAdminGetAllSegmentsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetAllSegmentsRequest>)delegate40;
					}
				}
			}
			if (this.OnAdminGetAllSegmentsResultEvent != null)
			{
				foreach (Delegate delegate41 in this.OnAdminGetAllSegmentsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate41.Target, instance))
					{
						this.OnAdminGetAllSegmentsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetAllSegmentsResult>)delegate41;
					}
				}
			}
			if (this.OnAdminGetCatalogItemsRequestEvent != null)
			{
				foreach (Delegate delegate42 in this.OnAdminGetCatalogItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate42.Target, instance))
					{
						this.OnAdminGetCatalogItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetCatalogItemsRequest>)delegate42;
					}
				}
			}
			if (this.OnAdminGetCatalogItemsResultEvent != null)
			{
				foreach (Delegate delegate43 in this.OnAdminGetCatalogItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate43.Target, instance))
					{
						this.OnAdminGetCatalogItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetCatalogItemsResult>)delegate43;
					}
				}
			}
			if (this.OnAdminGetCloudScriptRevisionRequestEvent != null)
			{
				foreach (Delegate delegate44 in this.OnAdminGetCloudScriptRevisionRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate44.Target, instance))
					{
						this.OnAdminGetCloudScriptRevisionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCloudScriptRevisionRequest>)delegate44;
					}
				}
			}
			if (this.OnAdminGetCloudScriptRevisionResultEvent != null)
			{
				foreach (Delegate delegate45 in this.OnAdminGetCloudScriptRevisionResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate45.Target, instance))
					{
						this.OnAdminGetCloudScriptRevisionResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCloudScriptRevisionResult>)delegate45;
					}
				}
			}
			if (this.OnAdminGetCloudScriptTaskInstanceRequestEvent != null)
			{
				foreach (Delegate delegate46 in this.OnAdminGetCloudScriptTaskInstanceRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate46.Target, instance))
					{
						this.OnAdminGetCloudScriptTaskInstanceRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTaskInstanceRequest>)delegate46;
					}
				}
			}
			if (this.OnAdminGetCloudScriptTaskInstanceResultEvent != null)
			{
				foreach (Delegate delegate47 in this.OnAdminGetCloudScriptTaskInstanceResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate47.Target, instance))
					{
						this.OnAdminGetCloudScriptTaskInstanceResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCloudScriptTaskInstanceResult>)delegate47;
					}
				}
			}
			if (this.OnAdminGetCloudScriptVersionsRequestEvent != null)
			{
				foreach (Delegate delegate48 in this.OnAdminGetCloudScriptVersionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate48.Target, instance))
					{
						this.OnAdminGetCloudScriptVersionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCloudScriptVersionsRequest>)delegate48;
					}
				}
			}
			if (this.OnAdminGetCloudScriptVersionsResultEvent != null)
			{
				foreach (Delegate delegate49 in this.OnAdminGetCloudScriptVersionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate49.Target, instance))
					{
						this.OnAdminGetCloudScriptVersionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCloudScriptVersionsResult>)delegate49;
					}
				}
			}
			if (this.OnAdminGetContentListRequestEvent != null)
			{
				foreach (Delegate delegate50 in this.OnAdminGetContentListRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate50.Target, instance))
					{
						this.OnAdminGetContentListRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetContentListRequest>)delegate50;
					}
				}
			}
			if (this.OnAdminGetContentListResultEvent != null)
			{
				foreach (Delegate delegate51 in this.OnAdminGetContentListResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate51.Target, instance))
					{
						this.OnAdminGetContentListResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetContentListResult>)delegate51;
					}
				}
			}
			if (this.OnAdminGetContentUploadUrlRequestEvent != null)
			{
				foreach (Delegate delegate52 in this.OnAdminGetContentUploadUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate52.Target, instance))
					{
						this.OnAdminGetContentUploadUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetContentUploadUrlRequest>)delegate52;
					}
				}
			}
			if (this.OnAdminGetContentUploadUrlResultEvent != null)
			{
				foreach (Delegate delegate53 in this.OnAdminGetContentUploadUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate53.Target, instance))
					{
						this.OnAdminGetContentUploadUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetContentUploadUrlResult>)delegate53;
					}
				}
			}
			if (this.OnAdminGetDataReportRequestEvent != null)
			{
				foreach (Delegate delegate54 in this.OnAdminGetDataReportRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate54.Target, instance))
					{
						this.OnAdminGetDataReportRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetDataReportRequest>)delegate54;
					}
				}
			}
			if (this.OnAdminGetDataReportResultEvent != null)
			{
				foreach (Delegate delegate55 in this.OnAdminGetDataReportResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate55.Target, instance))
					{
						this.OnAdminGetDataReportResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetDataReportResult>)delegate55;
					}
				}
			}
			if (this.OnAdminGetMatchmakerGameInfoRequestEvent != null)
			{
				foreach (Delegate delegate56 in this.OnAdminGetMatchmakerGameInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate56.Target, instance))
					{
						this.OnAdminGetMatchmakerGameInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMatchmakerGameInfoRequest>)delegate56;
					}
				}
			}
			if (this.OnAdminGetMatchmakerGameInfoResultEvent != null)
			{
				foreach (Delegate delegate57 in this.OnAdminGetMatchmakerGameInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate57.Target, instance))
					{
						this.OnAdminGetMatchmakerGameInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMatchmakerGameInfoResult>)delegate57;
					}
				}
			}
			if (this.OnAdminGetMatchmakerGameModesRequestEvent != null)
			{
				foreach (Delegate delegate58 in this.OnAdminGetMatchmakerGameModesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate58.Target, instance))
					{
						this.OnAdminGetMatchmakerGameModesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMatchmakerGameModesRequest>)delegate58;
					}
				}
			}
			if (this.OnAdminGetMatchmakerGameModesResultEvent != null)
			{
				foreach (Delegate delegate59 in this.OnAdminGetMatchmakerGameModesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate59.Target, instance))
					{
						this.OnAdminGetMatchmakerGameModesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMatchmakerGameModesResult>)delegate59;
					}
				}
			}
			if (this.OnAdminGetPlayerIdFromAuthTokenRequestEvent != null)
			{
				foreach (Delegate delegate60 in this.OnAdminGetPlayerIdFromAuthTokenRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate60.Target, instance))
					{
						this.OnAdminGetPlayerIdFromAuthTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerIdFromAuthTokenRequest>)delegate60;
					}
				}
			}
			if (this.OnAdminGetPlayerIdFromAuthTokenResultEvent != null)
			{
				foreach (Delegate delegate61 in this.OnAdminGetPlayerIdFromAuthTokenResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate61.Target, instance))
					{
						this.OnAdminGetPlayerIdFromAuthTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerIdFromAuthTokenResult>)delegate61;
					}
				}
			}
			if (this.OnAdminGetPlayerProfileRequestEvent != null)
			{
				foreach (Delegate delegate62 in this.OnAdminGetPlayerProfileRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate62.Target, instance))
					{
						this.OnAdminGetPlayerProfileRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayerProfileRequest>)delegate62;
					}
				}
			}
			if (this.OnAdminGetPlayerProfileResultEvent != null)
			{
				foreach (Delegate delegate63 in this.OnAdminGetPlayerProfileResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate63.Target, instance))
					{
						this.OnAdminGetPlayerProfileResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayerProfileResult>)delegate63;
					}
				}
			}
			if (this.OnAdminGetPlayerSegmentsRequestEvent != null)
			{
				foreach (Delegate delegate64 in this.OnAdminGetPlayerSegmentsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate64.Target, instance))
					{
						this.OnAdminGetPlayerSegmentsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayersSegmentsRequest>)delegate64;
					}
				}
			}
			if (this.OnAdminGetPlayerSegmentsResultEvent != null)
			{
				foreach (Delegate delegate65 in this.OnAdminGetPlayerSegmentsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate65.Target, instance))
					{
						this.OnAdminGetPlayerSegmentsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayerSegmentsResult>)delegate65;
					}
				}
			}
			if (this.OnAdminGetPlayerSharedSecretsRequestEvent != null)
			{
				foreach (Delegate delegate66 in this.OnAdminGetPlayerSharedSecretsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate66.Target, instance))
					{
						this.OnAdminGetPlayerSharedSecretsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerSharedSecretsRequest>)delegate66;
					}
				}
			}
			if (this.OnAdminGetPlayerSharedSecretsResultEvent != null)
			{
				foreach (Delegate delegate67 in this.OnAdminGetPlayerSharedSecretsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate67.Target, instance))
					{
						this.OnAdminGetPlayerSharedSecretsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerSharedSecretsResult>)delegate67;
					}
				}
			}
			if (this.OnAdminGetPlayersInSegmentRequestEvent != null)
			{
				foreach (Delegate delegate68 in this.OnAdminGetPlayersInSegmentRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate68.Target, instance))
					{
						this.OnAdminGetPlayersInSegmentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayersInSegmentRequest>)delegate68;
					}
				}
			}
			if (this.OnAdminGetPlayersInSegmentResultEvent != null)
			{
				foreach (Delegate delegate69 in this.OnAdminGetPlayersInSegmentResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate69.Target, instance))
					{
						this.OnAdminGetPlayersInSegmentResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayersInSegmentResult>)delegate69;
					}
				}
			}
			if (this.OnAdminGetPlayerStatisticDefinitionsRequestEvent != null)
			{
				foreach (Delegate delegate70 in this.OnAdminGetPlayerStatisticDefinitionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate70.Target, instance))
					{
						this.OnAdminGetPlayerStatisticDefinitionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticDefinitionsRequest>)delegate70;
					}
				}
			}
			if (this.OnAdminGetPlayerStatisticDefinitionsResultEvent != null)
			{
				foreach (Delegate delegate71 in this.OnAdminGetPlayerStatisticDefinitionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate71.Target, instance))
					{
						this.OnAdminGetPlayerStatisticDefinitionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticDefinitionsResult>)delegate71;
					}
				}
			}
			if (this.OnAdminGetPlayerStatisticVersionsRequestEvent != null)
			{
				foreach (Delegate delegate72 in this.OnAdminGetPlayerStatisticVersionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate72.Target, instance))
					{
						this.OnAdminGetPlayerStatisticVersionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayerStatisticVersionsRequest>)delegate72;
					}
				}
			}
			if (this.OnAdminGetPlayerStatisticVersionsResultEvent != null)
			{
				foreach (Delegate delegate73 in this.OnAdminGetPlayerStatisticVersionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate73.Target, instance))
					{
						this.OnAdminGetPlayerStatisticVersionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayerStatisticVersionsResult>)delegate73;
					}
				}
			}
			if (this.OnAdminGetPlayerTagsRequestEvent != null)
			{
				foreach (Delegate delegate74 in this.OnAdminGetPlayerTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate74.Target, instance))
					{
						this.OnAdminGetPlayerTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPlayerTagsRequest>)delegate74;
					}
				}
			}
			if (this.OnAdminGetPlayerTagsResultEvent != null)
			{
				foreach (Delegate delegate75 in this.OnAdminGetPlayerTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate75.Target, instance))
					{
						this.OnAdminGetPlayerTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPlayerTagsResult>)delegate75;
					}
				}
			}
			if (this.OnAdminGetPolicyRequestEvent != null)
			{
				foreach (Delegate delegate76 in this.OnAdminGetPolicyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate76.Target, instance))
					{
						this.OnAdminGetPolicyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPolicyRequest>)delegate76;
					}
				}
			}
			if (this.OnAdminGetPolicyResultEvent != null)
			{
				foreach (Delegate delegate77 in this.OnAdminGetPolicyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate77.Target, instance))
					{
						this.OnAdminGetPolicyResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPolicyResponse>)delegate77;
					}
				}
			}
			if (this.OnAdminGetPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate78 in this.OnAdminGetPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate78.Target, instance))
					{
						this.OnAdminGetPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetPublisherDataRequest>)delegate78;
					}
				}
			}
			if (this.OnAdminGetPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate79 in this.OnAdminGetPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate79.Target, instance))
					{
						this.OnAdminGetPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetPublisherDataResult>)delegate79;
					}
				}
			}
			if (this.OnAdminGetRandomResultTablesRequestEvent != null)
			{
				foreach (Delegate delegate80 in this.OnAdminGetRandomResultTablesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate80.Target, instance))
					{
						this.OnAdminGetRandomResultTablesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetRandomResultTablesRequest>)delegate80;
					}
				}
			}
			if (this.OnAdminGetRandomResultTablesResultEvent != null)
			{
				foreach (Delegate delegate81 in this.OnAdminGetRandomResultTablesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate81.Target, instance))
					{
						this.OnAdminGetRandomResultTablesResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetRandomResultTablesResult>)delegate81;
					}
				}
			}
			if (this.OnAdminGetServerBuildInfoRequestEvent != null)
			{
				foreach (Delegate delegate82 in this.OnAdminGetServerBuildInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate82.Target, instance))
					{
						this.OnAdminGetServerBuildInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetServerBuildInfoRequest>)delegate82;
					}
				}
			}
			if (this.OnAdminGetServerBuildInfoResultEvent != null)
			{
				foreach (Delegate delegate83 in this.OnAdminGetServerBuildInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate83.Target, instance))
					{
						this.OnAdminGetServerBuildInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetServerBuildInfoResult>)delegate83;
					}
				}
			}
			if (this.OnAdminGetServerBuildUploadUrlRequestEvent != null)
			{
				foreach (Delegate delegate84 in this.OnAdminGetServerBuildUploadUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate84.Target, instance))
					{
						this.OnAdminGetServerBuildUploadUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetServerBuildUploadURLRequest>)delegate84;
					}
				}
			}
			if (this.OnAdminGetServerBuildUploadUrlResultEvent != null)
			{
				foreach (Delegate delegate85 in this.OnAdminGetServerBuildUploadUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate85.Target, instance))
					{
						this.OnAdminGetServerBuildUploadUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetServerBuildUploadURLResult>)delegate85;
					}
				}
			}
			if (this.OnAdminGetStoreItemsRequestEvent != null)
			{
				foreach (Delegate delegate86 in this.OnAdminGetStoreItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate86.Target, instance))
					{
						this.OnAdminGetStoreItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetStoreItemsRequest>)delegate86;
					}
				}
			}
			if (this.OnAdminGetStoreItemsResultEvent != null)
			{
				foreach (Delegate delegate87 in this.OnAdminGetStoreItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate87.Target, instance))
					{
						this.OnAdminGetStoreItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetStoreItemsResult>)delegate87;
					}
				}
			}
			if (this.OnAdminGetTaskInstancesRequestEvent != null)
			{
				foreach (Delegate delegate88 in this.OnAdminGetTaskInstancesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate88.Target, instance))
					{
						this.OnAdminGetTaskInstancesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTaskInstancesRequest>)delegate88;
					}
				}
			}
			if (this.OnAdminGetTaskInstancesResultEvent != null)
			{
				foreach (Delegate delegate89 in this.OnAdminGetTaskInstancesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate89.Target, instance))
					{
						this.OnAdminGetTaskInstancesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTaskInstancesResult>)delegate89;
					}
				}
			}
			if (this.OnAdminGetTasksRequestEvent != null)
			{
				foreach (Delegate delegate90 in this.OnAdminGetTasksRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate90.Target, instance))
					{
						this.OnAdminGetTasksRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTasksRequest>)delegate90;
					}
				}
			}
			if (this.OnAdminGetTasksResultEvent != null)
			{
				foreach (Delegate delegate91 in this.OnAdminGetTasksResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate91.Target, instance))
					{
						this.OnAdminGetTasksResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTasksResult>)delegate91;
					}
				}
			}
			if (this.OnAdminGetTitleDataRequestEvent != null)
			{
				foreach (Delegate delegate92 in this.OnAdminGetTitleDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate92.Target, instance))
					{
						this.OnAdminGetTitleDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetTitleDataRequest>)delegate92;
					}
				}
			}
			if (this.OnAdminGetTitleDataResultEvent != null)
			{
				foreach (Delegate delegate93 in this.OnAdminGetTitleDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate93.Target, instance))
					{
						this.OnAdminGetTitleDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetTitleDataResult>)delegate93;
					}
				}
			}
			if (this.OnAdminGetTitleInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate94 in this.OnAdminGetTitleInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate94.Target, instance))
					{
						this.OnAdminGetTitleInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetTitleDataRequest>)delegate94;
					}
				}
			}
			if (this.OnAdminGetTitleInternalDataResultEvent != null)
			{
				foreach (Delegate delegate95 in this.OnAdminGetTitleInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate95.Target, instance))
					{
						this.OnAdminGetTitleInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetTitleDataResult>)delegate95;
					}
				}
			}
			if (this.OnAdminGetUserAccountInfoRequestEvent != null)
			{
				foreach (Delegate delegate96 in this.OnAdminGetUserAccountInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate96.Target, instance))
					{
						this.OnAdminGetUserAccountInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LookupUserAccountInfoRequest>)delegate96;
					}
				}
			}
			if (this.OnAdminGetUserAccountInfoResultEvent != null)
			{
				foreach (Delegate delegate97 in this.OnAdminGetUserAccountInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate97.Target, instance))
					{
						this.OnAdminGetUserAccountInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<LookupUserAccountInfoResult>)delegate97;
					}
				}
			}
			if (this.OnAdminGetUserBansRequestEvent != null)
			{
				foreach (Delegate delegate98 in this.OnAdminGetUserBansRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate98.Target, instance))
					{
						this.OnAdminGetUserBansRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserBansRequest>)delegate98;
					}
				}
			}
			if (this.OnAdminGetUserBansResultEvent != null)
			{
				foreach (Delegate delegate99 in this.OnAdminGetUserBansResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate99.Target, instance))
					{
						this.OnAdminGetUserBansResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserBansResult>)delegate99;
					}
				}
			}
			if (this.OnAdminGetUserDataRequestEvent != null)
			{
				foreach (Delegate delegate100 in this.OnAdminGetUserDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate100.Target, instance))
					{
						this.OnAdminGetUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest>)delegate100;
					}
				}
			}
			if (this.OnAdminGetUserDataResultEvent != null)
			{
				foreach (Delegate delegate101 in this.OnAdminGetUserDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate101.Target, instance))
					{
						this.OnAdminGetUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult>)delegate101;
					}
				}
			}
			if (this.OnAdminGetUserInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate102 in this.OnAdminGetUserInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate102.Target, instance))
					{
						this.OnAdminGetUserInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest>)delegate102;
					}
				}
			}
			if (this.OnAdminGetUserInternalDataResultEvent != null)
			{
				foreach (Delegate delegate103 in this.OnAdminGetUserInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate103.Target, instance))
					{
						this.OnAdminGetUserInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult>)delegate103;
					}
				}
			}
			if (this.OnAdminGetUserInventoryRequestEvent != null)
			{
				foreach (Delegate delegate104 in this.OnAdminGetUserInventoryRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate104.Target, instance))
					{
						this.OnAdminGetUserInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserInventoryRequest>)delegate104;
					}
				}
			}
			if (this.OnAdminGetUserInventoryResultEvent != null)
			{
				foreach (Delegate delegate105 in this.OnAdminGetUserInventoryResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate105.Target, instance))
					{
						this.OnAdminGetUserInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserInventoryResult>)delegate105;
					}
				}
			}
			if (this.OnAdminGetUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate106 in this.OnAdminGetUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate106.Target, instance))
					{
						this.OnAdminGetUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest>)delegate106;
					}
				}
			}
			if (this.OnAdminGetUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate107 in this.OnAdminGetUserPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate107.Target, instance))
					{
						this.OnAdminGetUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult>)delegate107;
					}
				}
			}
			if (this.OnAdminGetUserPublisherInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate108 in this.OnAdminGetUserPublisherInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate108.Target, instance))
					{
						this.OnAdminGetUserPublisherInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest>)delegate108;
					}
				}
			}
			if (this.OnAdminGetUserPublisherInternalDataResultEvent != null)
			{
				foreach (Delegate delegate109 in this.OnAdminGetUserPublisherInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate109.Target, instance))
					{
						this.OnAdminGetUserPublisherInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult>)delegate109;
					}
				}
			}
			if (this.OnAdminGetUserPublisherReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate110 in this.OnAdminGetUserPublisherReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate110.Target, instance))
					{
						this.OnAdminGetUserPublisherReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest>)delegate110;
					}
				}
			}
			if (this.OnAdminGetUserPublisherReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate111 in this.OnAdminGetUserPublisherReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate111.Target, instance))
					{
						this.OnAdminGetUserPublisherReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult>)delegate111;
					}
				}
			}
			if (this.OnAdminGetUserReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate112 in this.OnAdminGetUserReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate112.Target, instance))
					{
						this.OnAdminGetUserReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GetUserDataRequest>)delegate112;
					}
				}
			}
			if (this.OnAdminGetUserReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate113 in this.OnAdminGetUserReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate113.Target, instance))
					{
						this.OnAdminGetUserReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GetUserDataResult>)delegate113;
					}
				}
			}
			if (this.OnAdminGrantItemsToUsersRequestEvent != null)
			{
				foreach (Delegate delegate114 in this.OnAdminGrantItemsToUsersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate114.Target, instance))
					{
						this.OnAdminGrantItemsToUsersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.GrantItemsToUsersRequest>)delegate114;
					}
				}
			}
			if (this.OnAdminGrantItemsToUsersResultEvent != null)
			{
				foreach (Delegate delegate115 in this.OnAdminGrantItemsToUsersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate115.Target, instance))
					{
						this.OnAdminGrantItemsToUsersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.GrantItemsToUsersResult>)delegate115;
					}
				}
			}
			if (this.OnAdminIncrementLimitedEditionItemAvailabilityRequestEvent != null)
			{
				foreach (Delegate delegate116 in this.OnAdminIncrementLimitedEditionItemAvailabilityRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate116.Target, instance))
					{
						this.OnAdminIncrementLimitedEditionItemAvailabilityRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<IncrementLimitedEditionItemAvailabilityRequest>)delegate116;
					}
				}
			}
			if (this.OnAdminIncrementLimitedEditionItemAvailabilityResultEvent != null)
			{
				foreach (Delegate delegate117 in this.OnAdminIncrementLimitedEditionItemAvailabilityResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate117.Target, instance))
					{
						this.OnAdminIncrementLimitedEditionItemAvailabilityResultEvent -= (PlayFabEvents.PlayFabResultEvent<IncrementLimitedEditionItemAvailabilityResult>)delegate117;
					}
				}
			}
			if (this.OnAdminIncrementPlayerStatisticVersionRequestEvent != null)
			{
				foreach (Delegate delegate118 in this.OnAdminIncrementPlayerStatisticVersionRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate118.Target, instance))
					{
						this.OnAdminIncrementPlayerStatisticVersionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<IncrementPlayerStatisticVersionRequest>)delegate118;
					}
				}
			}
			if (this.OnAdminIncrementPlayerStatisticVersionResultEvent != null)
			{
				foreach (Delegate delegate119 in this.OnAdminIncrementPlayerStatisticVersionResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate119.Target, instance))
					{
						this.OnAdminIncrementPlayerStatisticVersionResultEvent -= (PlayFabEvents.PlayFabResultEvent<IncrementPlayerStatisticVersionResult>)delegate119;
					}
				}
			}
			if (this.OnAdminListServerBuildsRequestEvent != null)
			{
				foreach (Delegate delegate120 in this.OnAdminListServerBuildsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate120.Target, instance))
					{
						this.OnAdminListServerBuildsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListBuildsRequest>)delegate120;
					}
				}
			}
			if (this.OnAdminListServerBuildsResultEvent != null)
			{
				foreach (Delegate delegate121 in this.OnAdminListServerBuildsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate121.Target, instance))
					{
						this.OnAdminListServerBuildsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListBuildsResult>)delegate121;
					}
				}
			}
			if (this.OnAdminListVirtualCurrencyTypesRequestEvent != null)
			{
				foreach (Delegate delegate122 in this.OnAdminListVirtualCurrencyTypesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate122.Target, instance))
					{
						this.OnAdminListVirtualCurrencyTypesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListVirtualCurrencyTypesRequest>)delegate122;
					}
				}
			}
			if (this.OnAdminListVirtualCurrencyTypesResultEvent != null)
			{
				foreach (Delegate delegate123 in this.OnAdminListVirtualCurrencyTypesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate123.Target, instance))
					{
						this.OnAdminListVirtualCurrencyTypesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListVirtualCurrencyTypesResult>)delegate123;
					}
				}
			}
			if (this.OnAdminModifyMatchmakerGameModesRequestEvent != null)
			{
				foreach (Delegate delegate124 in this.OnAdminModifyMatchmakerGameModesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate124.Target, instance))
					{
						this.OnAdminModifyMatchmakerGameModesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ModifyMatchmakerGameModesRequest>)delegate124;
					}
				}
			}
			if (this.OnAdminModifyMatchmakerGameModesResultEvent != null)
			{
				foreach (Delegate delegate125 in this.OnAdminModifyMatchmakerGameModesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate125.Target, instance))
					{
						this.OnAdminModifyMatchmakerGameModesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyMatchmakerGameModesResult>)delegate125;
					}
				}
			}
			if (this.OnAdminModifyServerBuildRequestEvent != null)
			{
				foreach (Delegate delegate126 in this.OnAdminModifyServerBuildRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate126.Target, instance))
					{
						this.OnAdminModifyServerBuildRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ModifyServerBuildRequest>)delegate126;
					}
				}
			}
			if (this.OnAdminModifyServerBuildResultEvent != null)
			{
				foreach (Delegate delegate127 in this.OnAdminModifyServerBuildResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate127.Target, instance))
					{
						this.OnAdminModifyServerBuildResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyServerBuildResult>)delegate127;
					}
				}
			}
			if (this.OnAdminRefundPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate128 in this.OnAdminRefundPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate128.Target, instance))
					{
						this.OnAdminRefundPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RefundPurchaseRequest>)delegate128;
					}
				}
			}
			if (this.OnAdminRefundPurchaseResultEvent != null)
			{
				foreach (Delegate delegate129 in this.OnAdminRefundPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate129.Target, instance))
					{
						this.OnAdminRefundPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<RefundPurchaseResponse>)delegate129;
					}
				}
			}
			if (this.OnAdminRemovePlayerTagRequestEvent != null)
			{
				foreach (Delegate delegate130 in this.OnAdminRemovePlayerTagRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate130.Target, instance))
					{
						this.OnAdminRemovePlayerTagRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.RemovePlayerTagRequest>)delegate130;
					}
				}
			}
			if (this.OnAdminRemovePlayerTagResultEvent != null)
			{
				foreach (Delegate delegate131 in this.OnAdminRemovePlayerTagResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate131.Target, instance))
					{
						this.OnAdminRemovePlayerTagResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.RemovePlayerTagResult>)delegate131;
					}
				}
			}
			if (this.OnAdminRemoveServerBuildRequestEvent != null)
			{
				foreach (Delegate delegate132 in this.OnAdminRemoveServerBuildRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate132.Target, instance))
					{
						this.OnAdminRemoveServerBuildRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveServerBuildRequest>)delegate132;
					}
				}
			}
			if (this.OnAdminRemoveServerBuildResultEvent != null)
			{
				foreach (Delegate delegate133 in this.OnAdminRemoveServerBuildResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate133.Target, instance))
					{
						this.OnAdminRemoveServerBuildResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveServerBuildResult>)delegate133;
					}
				}
			}
			if (this.OnAdminRemoveVirtualCurrencyTypesRequestEvent != null)
			{
				foreach (Delegate delegate134 in this.OnAdminRemoveVirtualCurrencyTypesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate134.Target, instance))
					{
						this.OnAdminRemoveVirtualCurrencyTypesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveVirtualCurrencyTypesRequest>)delegate134;
					}
				}
			}
			if (this.OnAdminRemoveVirtualCurrencyTypesResultEvent != null)
			{
				foreach (Delegate delegate135 in this.OnAdminRemoveVirtualCurrencyTypesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate135.Target, instance))
					{
						this.OnAdminRemoveVirtualCurrencyTypesResultEvent -= (PlayFabEvents.PlayFabResultEvent<BlankResult>)delegate135;
					}
				}
			}
			if (this.OnAdminResetCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate136 in this.OnAdminResetCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate136.Target, instance))
					{
						this.OnAdminResetCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ResetCharacterStatisticsRequest>)delegate136;
					}
				}
			}
			if (this.OnAdminResetCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate137 in this.OnAdminResetCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate137.Target, instance))
					{
						this.OnAdminResetCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ResetCharacterStatisticsResult>)delegate137;
					}
				}
			}
			if (this.OnAdminResetPasswordRequestEvent != null)
			{
				foreach (Delegate delegate138 in this.OnAdminResetPasswordRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate138.Target, instance))
					{
						this.OnAdminResetPasswordRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ResetPasswordRequest>)delegate138;
					}
				}
			}
			if (this.OnAdminResetPasswordResultEvent != null)
			{
				foreach (Delegate delegate139 in this.OnAdminResetPasswordResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate139.Target, instance))
					{
						this.OnAdminResetPasswordResultEvent -= (PlayFabEvents.PlayFabResultEvent<ResetPasswordResult>)delegate139;
					}
				}
			}
			if (this.OnAdminResetUserStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate140 in this.OnAdminResetUserStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate140.Target, instance))
					{
						this.OnAdminResetUserStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ResetUserStatisticsRequest>)delegate140;
					}
				}
			}
			if (this.OnAdminResetUserStatisticsResultEvent != null)
			{
				foreach (Delegate delegate141 in this.OnAdminResetUserStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate141.Target, instance))
					{
						this.OnAdminResetUserStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ResetUserStatisticsResult>)delegate141;
					}
				}
			}
			if (this.OnAdminResolvePurchaseDisputeRequestEvent != null)
			{
				foreach (Delegate delegate142 in this.OnAdminResolvePurchaseDisputeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate142.Target, instance))
					{
						this.OnAdminResolvePurchaseDisputeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ResolvePurchaseDisputeRequest>)delegate142;
					}
				}
			}
			if (this.OnAdminResolvePurchaseDisputeResultEvent != null)
			{
				foreach (Delegate delegate143 in this.OnAdminResolvePurchaseDisputeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate143.Target, instance))
					{
						this.OnAdminResolvePurchaseDisputeResultEvent -= (PlayFabEvents.PlayFabResultEvent<ResolvePurchaseDisputeResponse>)delegate143;
					}
				}
			}
			if (this.OnAdminRevokeAllBansForUserRequestEvent != null)
			{
				foreach (Delegate delegate144 in this.OnAdminRevokeAllBansForUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate144.Target, instance))
					{
						this.OnAdminRevokeAllBansForUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.RevokeAllBansForUserRequest>)delegate144;
					}
				}
			}
			if (this.OnAdminRevokeAllBansForUserResultEvent != null)
			{
				foreach (Delegate delegate145 in this.OnAdminRevokeAllBansForUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate145.Target, instance))
					{
						this.OnAdminRevokeAllBansForUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.RevokeAllBansForUserResult>)delegate145;
					}
				}
			}
			if (this.OnAdminRevokeBansRequestEvent != null)
			{
				foreach (Delegate delegate146 in this.OnAdminRevokeBansRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate146.Target, instance))
					{
						this.OnAdminRevokeBansRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.RevokeBansRequest>)delegate146;
					}
				}
			}
			if (this.OnAdminRevokeBansResultEvent != null)
			{
				foreach (Delegate delegate147 in this.OnAdminRevokeBansResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate147.Target, instance))
					{
						this.OnAdminRevokeBansResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.RevokeBansResult>)delegate147;
					}
				}
			}
			if (this.OnAdminRevokeInventoryItemRequestEvent != null)
			{
				foreach (Delegate delegate148 in this.OnAdminRevokeInventoryItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate148.Target, instance))
					{
						this.OnAdminRevokeInventoryItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.RevokeInventoryItemRequest>)delegate148;
					}
				}
			}
			if (this.OnAdminRevokeInventoryItemResultEvent != null)
			{
				foreach (Delegate delegate149 in this.OnAdminRevokeInventoryItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate149.Target, instance))
					{
						this.OnAdminRevokeInventoryItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.RevokeInventoryResult>)delegate149;
					}
				}
			}
			if (this.OnAdminRunTaskRequestEvent != null)
			{
				foreach (Delegate delegate150 in this.OnAdminRunTaskRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate150.Target, instance))
					{
						this.OnAdminRunTaskRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RunTaskRequest>)delegate150;
					}
				}
			}
			if (this.OnAdminRunTaskResultEvent != null)
			{
				foreach (Delegate delegate151 in this.OnAdminRunTaskResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate151.Target, instance))
					{
						this.OnAdminRunTaskResultEvent -= (PlayFabEvents.PlayFabResultEvent<RunTaskResult>)delegate151;
					}
				}
			}
			if (this.OnAdminSendAccountRecoveryEmailRequestEvent != null)
			{
				foreach (Delegate delegate152 in this.OnAdminSendAccountRecoveryEmailRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate152.Target, instance))
					{
						this.OnAdminSendAccountRecoveryEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SendAccountRecoveryEmailRequest>)delegate152;
					}
				}
			}
			if (this.OnAdminSendAccountRecoveryEmailResultEvent != null)
			{
				foreach (Delegate delegate153 in this.OnAdminSendAccountRecoveryEmailResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate153.Target, instance))
					{
						this.OnAdminSendAccountRecoveryEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SendAccountRecoveryEmailResult>)delegate153;
					}
				}
			}
			if (this.OnAdminSetCatalogItemsRequestEvent != null)
			{
				foreach (Delegate delegate154 in this.OnAdminSetCatalogItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate154.Target, instance))
					{
						this.OnAdminSetCatalogItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateCatalogItemsRequest>)delegate154;
					}
				}
			}
			if (this.OnAdminSetCatalogItemsResultEvent != null)
			{
				foreach (Delegate delegate155 in this.OnAdminSetCatalogItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate155.Target, instance))
					{
						this.OnAdminSetCatalogItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateCatalogItemsResult>)delegate155;
					}
				}
			}
			if (this.OnAdminSetPlayerSecretRequestEvent != null)
			{
				foreach (Delegate delegate156 in this.OnAdminSetPlayerSecretRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate156.Target, instance))
					{
						this.OnAdminSetPlayerSecretRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SetPlayerSecretRequest>)delegate156;
					}
				}
			}
			if (this.OnAdminSetPlayerSecretResultEvent != null)
			{
				foreach (Delegate delegate157 in this.OnAdminSetPlayerSecretResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate157.Target, instance))
					{
						this.OnAdminSetPlayerSecretResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SetPlayerSecretResult>)delegate157;
					}
				}
			}
			if (this.OnAdminSetPublishedRevisionRequestEvent != null)
			{
				foreach (Delegate delegate158 in this.OnAdminSetPublishedRevisionRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate158.Target, instance))
					{
						this.OnAdminSetPublishedRevisionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetPublishedRevisionRequest>)delegate158;
					}
				}
			}
			if (this.OnAdminSetPublishedRevisionResultEvent != null)
			{
				foreach (Delegate delegate159 in this.OnAdminSetPublishedRevisionResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate159.Target, instance))
					{
						this.OnAdminSetPublishedRevisionResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetPublishedRevisionResult>)delegate159;
					}
				}
			}
			if (this.OnAdminSetPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate160 in this.OnAdminSetPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate160.Target, instance))
					{
						this.OnAdminSetPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SetPublisherDataRequest>)delegate160;
					}
				}
			}
			if (this.OnAdminSetPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate161 in this.OnAdminSetPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate161.Target, instance))
					{
						this.OnAdminSetPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SetPublisherDataResult>)delegate161;
					}
				}
			}
			if (this.OnAdminSetStoreItemsRequestEvent != null)
			{
				foreach (Delegate delegate162 in this.OnAdminSetStoreItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate162.Target, instance))
					{
						this.OnAdminSetStoreItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateStoreItemsRequest>)delegate162;
					}
				}
			}
			if (this.OnAdminSetStoreItemsResultEvent != null)
			{
				foreach (Delegate delegate163 in this.OnAdminSetStoreItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate163.Target, instance))
					{
						this.OnAdminSetStoreItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateStoreItemsResult>)delegate163;
					}
				}
			}
			if (this.OnAdminSetTitleDataRequestEvent != null)
			{
				foreach (Delegate delegate164 in this.OnAdminSetTitleDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate164.Target, instance))
					{
						this.OnAdminSetTitleDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SetTitleDataRequest>)delegate164;
					}
				}
			}
			if (this.OnAdminSetTitleDataResultEvent != null)
			{
				foreach (Delegate delegate165 in this.OnAdminSetTitleDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate165.Target, instance))
					{
						this.OnAdminSetTitleDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SetTitleDataResult>)delegate165;
					}
				}
			}
			if (this.OnAdminSetTitleInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate166 in this.OnAdminSetTitleInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate166.Target, instance))
					{
						this.OnAdminSetTitleInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SetTitleDataRequest>)delegate166;
					}
				}
			}
			if (this.OnAdminSetTitleInternalDataResultEvent != null)
			{
				foreach (Delegate delegate167 in this.OnAdminSetTitleInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate167.Target, instance))
					{
						this.OnAdminSetTitleInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.SetTitleDataResult>)delegate167;
					}
				}
			}
			if (this.OnAdminSetupPushNotificationRequestEvent != null)
			{
				foreach (Delegate delegate168 in this.OnAdminSetupPushNotificationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate168.Target, instance))
					{
						this.OnAdminSetupPushNotificationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetupPushNotificationRequest>)delegate168;
					}
				}
			}
			if (this.OnAdminSetupPushNotificationResultEvent != null)
			{
				foreach (Delegate delegate169 in this.OnAdminSetupPushNotificationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate169.Target, instance))
					{
						this.OnAdminSetupPushNotificationResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetupPushNotificationResult>)delegate169;
					}
				}
			}
			if (this.OnAdminSubtractUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate170 in this.OnAdminSubtractUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate170.Target, instance))
					{
						this.OnAdminSubtractUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.SubtractUserVirtualCurrencyRequest>)delegate170;
					}
				}
			}
			if (this.OnAdminSubtractUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate171 in this.OnAdminSubtractUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate171.Target, instance))
					{
						this.OnAdminSubtractUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.ModifyUserVirtualCurrencyResult>)delegate171;
					}
				}
			}
			if (this.OnAdminUpdateBansRequestEvent != null)
			{
				foreach (Delegate delegate172 in this.OnAdminUpdateBansRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate172.Target, instance))
					{
						this.OnAdminUpdateBansRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateBansRequest>)delegate172;
					}
				}
			}
			if (this.OnAdminUpdateBansResultEvent != null)
			{
				foreach (Delegate delegate173 in this.OnAdminUpdateBansResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate173.Target, instance))
					{
						this.OnAdminUpdateBansResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateBansResult>)delegate173;
					}
				}
			}
			if (this.OnAdminUpdateCatalogItemsRequestEvent != null)
			{
				foreach (Delegate delegate174 in this.OnAdminUpdateCatalogItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate174.Target, instance))
					{
						this.OnAdminUpdateCatalogItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateCatalogItemsRequest>)delegate174;
					}
				}
			}
			if (this.OnAdminUpdateCatalogItemsResultEvent != null)
			{
				foreach (Delegate delegate175 in this.OnAdminUpdateCatalogItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate175.Target, instance))
					{
						this.OnAdminUpdateCatalogItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateCatalogItemsResult>)delegate175;
					}
				}
			}
			if (this.OnAdminUpdateCloudScriptRequestEvent != null)
			{
				foreach (Delegate delegate176 in this.OnAdminUpdateCloudScriptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate176.Target, instance))
					{
						this.OnAdminUpdateCloudScriptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateCloudScriptRequest>)delegate176;
					}
				}
			}
			if (this.OnAdminUpdateCloudScriptResultEvent != null)
			{
				foreach (Delegate delegate177 in this.OnAdminUpdateCloudScriptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate177.Target, instance))
					{
						this.OnAdminUpdateCloudScriptResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateCloudScriptResult>)delegate177;
					}
				}
			}
			if (this.OnAdminUpdatePlayerSharedSecretRequestEvent != null)
			{
				foreach (Delegate delegate178 in this.OnAdminUpdatePlayerSharedSecretRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate178.Target, instance))
					{
						this.OnAdminUpdatePlayerSharedSecretRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdatePlayerSharedSecretRequest>)delegate178;
					}
				}
			}
			if (this.OnAdminUpdatePlayerSharedSecretResultEvent != null)
			{
				foreach (Delegate delegate179 in this.OnAdminUpdatePlayerSharedSecretResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate179.Target, instance))
					{
						this.OnAdminUpdatePlayerSharedSecretResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdatePlayerSharedSecretResult>)delegate179;
					}
				}
			}
			if (this.OnAdminUpdatePlayerStatisticDefinitionRequestEvent != null)
			{
				foreach (Delegate delegate180 in this.OnAdminUpdatePlayerStatisticDefinitionRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate180.Target, instance))
					{
						this.OnAdminUpdatePlayerStatisticDefinitionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdatePlayerStatisticDefinitionRequest>)delegate180;
					}
				}
			}
			if (this.OnAdminUpdatePlayerStatisticDefinitionResultEvent != null)
			{
				foreach (Delegate delegate181 in this.OnAdminUpdatePlayerStatisticDefinitionResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate181.Target, instance))
					{
						this.OnAdminUpdatePlayerStatisticDefinitionResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdatePlayerStatisticDefinitionResult>)delegate181;
					}
				}
			}
			if (this.OnAdminUpdatePolicyRequestEvent != null)
			{
				foreach (Delegate delegate182 in this.OnAdminUpdatePolicyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate182.Target, instance))
					{
						this.OnAdminUpdatePolicyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdatePolicyRequest>)delegate182;
					}
				}
			}
			if (this.OnAdminUpdatePolicyResultEvent != null)
			{
				foreach (Delegate delegate183 in this.OnAdminUpdatePolicyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate183.Target, instance))
					{
						this.OnAdminUpdatePolicyResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdatePolicyResponse>)delegate183;
					}
				}
			}
			if (this.OnAdminUpdateRandomResultTablesRequestEvent != null)
			{
				foreach (Delegate delegate184 in this.OnAdminUpdateRandomResultTablesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate184.Target, instance))
					{
						this.OnAdminUpdateRandomResultTablesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateRandomResultTablesRequest>)delegate184;
					}
				}
			}
			if (this.OnAdminUpdateRandomResultTablesResultEvent != null)
			{
				foreach (Delegate delegate185 in this.OnAdminUpdateRandomResultTablesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate185.Target, instance))
					{
						this.OnAdminUpdateRandomResultTablesResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateRandomResultTablesResult>)delegate185;
					}
				}
			}
			if (this.OnAdminUpdateStoreItemsRequestEvent != null)
			{
				foreach (Delegate delegate186 in this.OnAdminUpdateStoreItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate186.Target, instance))
					{
						this.OnAdminUpdateStoreItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateStoreItemsRequest>)delegate186;
					}
				}
			}
			if (this.OnAdminUpdateStoreItemsResultEvent != null)
			{
				foreach (Delegate delegate187 in this.OnAdminUpdateStoreItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate187.Target, instance))
					{
						this.OnAdminUpdateStoreItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateStoreItemsResult>)delegate187;
					}
				}
			}
			if (this.OnAdminUpdateTaskRequestEvent != null)
			{
				foreach (Delegate delegate188 in this.OnAdminUpdateTaskRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate188.Target, instance))
					{
						this.OnAdminUpdateTaskRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateTaskRequest>)delegate188;
					}
				}
			}
			if (this.OnAdminUpdateTaskResultEvent != null)
			{
				foreach (Delegate delegate189 in this.OnAdminUpdateTaskResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate189.Target, instance))
					{
						this.OnAdminUpdateTaskResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.EmptyResult>)delegate189;
					}
				}
			}
			if (this.OnAdminUpdateUserDataRequestEvent != null)
			{
				foreach (Delegate delegate190 in this.OnAdminUpdateUserDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate190.Target, instance))
					{
						this.OnAdminUpdateUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserDataRequest>)delegate190;
					}
				}
			}
			if (this.OnAdminUpdateUserDataResultEvent != null)
			{
				foreach (Delegate delegate191 in this.OnAdminUpdateUserDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate191.Target, instance))
					{
						this.OnAdminUpdateUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult>)delegate191;
					}
				}
			}
			if (this.OnAdminUpdateUserInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate192 in this.OnAdminUpdateUserInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate192.Target, instance))
					{
						this.OnAdminUpdateUserInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserInternalDataRequest>)delegate192;
					}
				}
			}
			if (this.OnAdminUpdateUserInternalDataResultEvent != null)
			{
				foreach (Delegate delegate193 in this.OnAdminUpdateUserInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate193.Target, instance))
					{
						this.OnAdminUpdateUserInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult>)delegate193;
					}
				}
			}
			if (this.OnAdminUpdateUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate194 in this.OnAdminUpdateUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate194.Target, instance))
					{
						this.OnAdminUpdateUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserDataRequest>)delegate194;
					}
				}
			}
			if (this.OnAdminUpdateUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate195 in this.OnAdminUpdateUserPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate195.Target, instance))
					{
						this.OnAdminUpdateUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult>)delegate195;
					}
				}
			}
			if (this.OnAdminUpdateUserPublisherInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate196 in this.OnAdminUpdateUserPublisherInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate196.Target, instance))
					{
						this.OnAdminUpdateUserPublisherInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserInternalDataRequest>)delegate196;
					}
				}
			}
			if (this.OnAdminUpdateUserPublisherInternalDataResultEvent != null)
			{
				foreach (Delegate delegate197 in this.OnAdminUpdateUserPublisherInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate197.Target, instance))
					{
						this.OnAdminUpdateUserPublisherInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult>)delegate197;
					}
				}
			}
			if (this.OnAdminUpdateUserPublisherReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate198 in this.OnAdminUpdateUserPublisherReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate198.Target, instance))
					{
						this.OnAdminUpdateUserPublisherReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserDataRequest>)delegate198;
					}
				}
			}
			if (this.OnAdminUpdateUserPublisherReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate199 in this.OnAdminUpdateUserPublisherReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate199.Target, instance))
					{
						this.OnAdminUpdateUserPublisherReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult>)delegate199;
					}
				}
			}
			if (this.OnAdminUpdateUserReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate200 in this.OnAdminUpdateUserReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate200.Target, instance))
					{
						this.OnAdminUpdateUserReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserDataRequest>)delegate200;
					}
				}
			}
			if (this.OnAdminUpdateUserReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate201 in this.OnAdminUpdateUserReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate201.Target, instance))
					{
						this.OnAdminUpdateUserReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserDataResult>)delegate201;
					}
				}
			}
			if (this.OnAdminUpdateUserTitleDisplayNameRequestEvent != null)
			{
				foreach (Delegate delegate202 in this.OnAdminUpdateUserTitleDisplayNameRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate202.Target, instance))
					{
						this.OnAdminUpdateUserTitleDisplayNameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.AdminModels.UpdateUserTitleDisplayNameRequest>)delegate202;
					}
				}
			}
			if (this.OnAdminUpdateUserTitleDisplayNameResultEvent != null)
			{
				foreach (Delegate delegate203 in this.OnAdminUpdateUserTitleDisplayNameResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate203.Target, instance))
					{
						this.OnAdminUpdateUserTitleDisplayNameResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.AdminModels.UpdateUserTitleDisplayNameResult>)delegate203;
					}
				}
			}
			if (this.OnMatchmakerAuthUserRequestEvent != null)
			{
				foreach (Delegate delegate204 in this.OnMatchmakerAuthUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate204.Target, instance))
					{
						this.OnMatchmakerAuthUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AuthUserRequest>)delegate204;
					}
				}
			}
			if (this.OnMatchmakerAuthUserResultEvent != null)
			{
				foreach (Delegate delegate205 in this.OnMatchmakerAuthUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate205.Target, instance))
					{
						this.OnMatchmakerAuthUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<AuthUserResponse>)delegate205;
					}
				}
			}
			if (this.OnMatchmakerPlayerJoinedRequestEvent != null)
			{
				foreach (Delegate delegate206 in this.OnMatchmakerPlayerJoinedRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate206.Target, instance))
					{
						this.OnMatchmakerPlayerJoinedRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayerJoinedRequest>)delegate206;
					}
				}
			}
			if (this.OnMatchmakerPlayerJoinedResultEvent != null)
			{
				foreach (Delegate delegate207 in this.OnMatchmakerPlayerJoinedResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate207.Target, instance))
					{
						this.OnMatchmakerPlayerJoinedResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayerJoinedResponse>)delegate207;
					}
				}
			}
			if (this.OnMatchmakerPlayerLeftRequestEvent != null)
			{
				foreach (Delegate delegate208 in this.OnMatchmakerPlayerLeftRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate208.Target, instance))
					{
						this.OnMatchmakerPlayerLeftRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayerLeftRequest>)delegate208;
					}
				}
			}
			if (this.OnMatchmakerPlayerLeftResultEvent != null)
			{
				foreach (Delegate delegate209 in this.OnMatchmakerPlayerLeftResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate209.Target, instance))
					{
						this.OnMatchmakerPlayerLeftResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayerLeftResponse>)delegate209;
					}
				}
			}
			if (this.OnMatchmakerStartGameRequestEvent != null)
			{
				foreach (Delegate delegate210 in this.OnMatchmakerStartGameRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate210.Target, instance))
					{
						this.OnMatchmakerStartGameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.MatchmakerModels.StartGameRequest>)delegate210;
					}
				}
			}
			if (this.OnMatchmakerStartGameResultEvent != null)
			{
				foreach (Delegate delegate211 in this.OnMatchmakerStartGameResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate211.Target, instance))
					{
						this.OnMatchmakerStartGameResultEvent -= (PlayFabEvents.PlayFabResultEvent<StartGameResponse>)delegate211;
					}
				}
			}
			if (this.OnMatchmakerUserInfoRequestEvent != null)
			{
				foreach (Delegate delegate212 in this.OnMatchmakerUserInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate212.Target, instance))
					{
						this.OnMatchmakerUserInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UserInfoRequest>)delegate212;
					}
				}
			}
			if (this.OnMatchmakerUserInfoResultEvent != null)
			{
				foreach (Delegate delegate213 in this.OnMatchmakerUserInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate213.Target, instance))
					{
						this.OnMatchmakerUserInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<UserInfoResponse>)delegate213;
					}
				}
			}
			if (this.OnServerAddCharacterVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate214 in this.OnServerAddCharacterVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate214.Target, instance))
					{
						this.OnServerAddCharacterVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddCharacterVirtualCurrencyRequest>)delegate214;
					}
				}
			}
			if (this.OnServerAddCharacterVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate215 in this.OnServerAddCharacterVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate215.Target, instance))
					{
						this.OnServerAddCharacterVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyCharacterVirtualCurrencyResult>)delegate215;
					}
				}
			}
			if (this.OnServerAddFriendRequestEvent != null)
			{
				foreach (Delegate delegate216 in this.OnServerAddFriendRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate216.Target, instance))
					{
						this.OnServerAddFriendRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.AddFriendRequest>)delegate216;
					}
				}
			}
			if (this.OnServerAddFriendResultEvent != null)
			{
				foreach (Delegate delegate217 in this.OnServerAddFriendResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate217.Target, instance))
					{
						this.OnServerAddFriendResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult>)delegate217;
					}
				}
			}
			if (this.OnServerAddPlayerTagRequestEvent != null)
			{
				foreach (Delegate delegate218 in this.OnServerAddPlayerTagRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate218.Target, instance))
					{
						this.OnServerAddPlayerTagRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.AddPlayerTagRequest>)delegate218;
					}
				}
			}
			if (this.OnServerAddPlayerTagResultEvent != null)
			{
				foreach (Delegate delegate219 in this.OnServerAddPlayerTagResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate219.Target, instance))
					{
						this.OnServerAddPlayerTagResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.AddPlayerTagResult>)delegate219;
					}
				}
			}
			if (this.OnServerAddSharedGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate220 in this.OnServerAddSharedGroupMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate220.Target, instance))
					{
						this.OnServerAddSharedGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.AddSharedGroupMembersRequest>)delegate220;
					}
				}
			}
			if (this.OnServerAddSharedGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate221 in this.OnServerAddSharedGroupMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate221.Target, instance))
					{
						this.OnServerAddSharedGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.AddSharedGroupMembersResult>)delegate221;
					}
				}
			}
			if (this.OnServerAddUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate222 in this.OnServerAddUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate222.Target, instance))
					{
						this.OnServerAddUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.AddUserVirtualCurrencyRequest>)delegate222;
					}
				}
			}
			if (this.OnServerAddUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate223 in this.OnServerAddUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate223.Target, instance))
					{
						this.OnServerAddUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ModifyUserVirtualCurrencyResult>)delegate223;
					}
				}
			}
			if (this.OnServerAuthenticateSessionTicketRequestEvent != null)
			{
				foreach (Delegate delegate224 in this.OnServerAuthenticateSessionTicketRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate224.Target, instance))
					{
						this.OnServerAuthenticateSessionTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AuthenticateSessionTicketRequest>)delegate224;
					}
				}
			}
			if (this.OnServerAuthenticateSessionTicketResultEvent != null)
			{
				foreach (Delegate delegate225 in this.OnServerAuthenticateSessionTicketResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate225.Target, instance))
					{
						this.OnServerAuthenticateSessionTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<AuthenticateSessionTicketResult>)delegate225;
					}
				}
			}
			if (this.OnServerAwardSteamAchievementRequestEvent != null)
			{
				foreach (Delegate delegate226 in this.OnServerAwardSteamAchievementRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate226.Target, instance))
					{
						this.OnServerAwardSteamAchievementRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AwardSteamAchievementRequest>)delegate226;
					}
				}
			}
			if (this.OnServerAwardSteamAchievementResultEvent != null)
			{
				foreach (Delegate delegate227 in this.OnServerAwardSteamAchievementResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate227.Target, instance))
					{
						this.OnServerAwardSteamAchievementResultEvent -= (PlayFabEvents.PlayFabResultEvent<AwardSteamAchievementResult>)delegate227;
					}
				}
			}
			if (this.OnServerBanUsersRequestEvent != null)
			{
				foreach (Delegate delegate228 in this.OnServerBanUsersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate228.Target, instance))
					{
						this.OnServerBanUsersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.BanUsersRequest>)delegate228;
					}
				}
			}
			if (this.OnServerBanUsersResultEvent != null)
			{
				foreach (Delegate delegate229 in this.OnServerBanUsersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate229.Target, instance))
					{
						this.OnServerBanUsersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.BanUsersResult>)delegate229;
					}
				}
			}
			if (this.OnServerConsumeItemRequestEvent != null)
			{
				foreach (Delegate delegate230 in this.OnServerConsumeItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate230.Target, instance))
					{
						this.OnServerConsumeItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.ConsumeItemRequest>)delegate230;
					}
				}
			}
			if (this.OnServerConsumeItemResultEvent != null)
			{
				foreach (Delegate delegate231 in this.OnServerConsumeItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate231.Target, instance))
					{
						this.OnServerConsumeItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ConsumeItemResult>)delegate231;
					}
				}
			}
			if (this.OnServerCreateSharedGroupRequestEvent != null)
			{
				foreach (Delegate delegate232 in this.OnServerCreateSharedGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate232.Target, instance))
					{
						this.OnServerCreateSharedGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.CreateSharedGroupRequest>)delegate232;
					}
				}
			}
			if (this.OnServerCreateSharedGroupResultEvent != null)
			{
				foreach (Delegate delegate233 in this.OnServerCreateSharedGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate233.Target, instance))
					{
						this.OnServerCreateSharedGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.CreateSharedGroupResult>)delegate233;
					}
				}
			}
			if (this.OnServerDeleteCharacterFromUserRequestEvent != null)
			{
				foreach (Delegate delegate234 in this.OnServerDeleteCharacterFromUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate234.Target, instance))
					{
						this.OnServerDeleteCharacterFromUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteCharacterFromUserRequest>)delegate234;
					}
				}
			}
			if (this.OnServerDeleteCharacterFromUserResultEvent != null)
			{
				foreach (Delegate delegate235 in this.OnServerDeleteCharacterFromUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate235.Target, instance))
					{
						this.OnServerDeleteCharacterFromUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<DeleteCharacterFromUserResult>)delegate235;
					}
				}
			}
			if (this.OnServerDeleteSharedGroupRequestEvent != null)
			{
				foreach (Delegate delegate236 in this.OnServerDeleteSharedGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate236.Target, instance))
					{
						this.OnServerDeleteSharedGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteSharedGroupRequest>)delegate236;
					}
				}
			}
			if (this.OnServerDeleteSharedGroupResultEvent != null)
			{
				foreach (Delegate delegate237 in this.OnServerDeleteSharedGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate237.Target, instance))
					{
						this.OnServerDeleteSharedGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult>)delegate237;
					}
				}
			}
			if (this.OnServerDeleteUsersRequestEvent != null)
			{
				foreach (Delegate delegate238 in this.OnServerDeleteUsersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate238.Target, instance))
					{
						this.OnServerDeleteUsersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.DeleteUsersRequest>)delegate238;
					}
				}
			}
			if (this.OnServerDeleteUsersResultEvent != null)
			{
				foreach (Delegate delegate239 in this.OnServerDeleteUsersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate239.Target, instance))
					{
						this.OnServerDeleteUsersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.DeleteUsersResult>)delegate239;
					}
				}
			}
			if (this.OnServerDeregisterGameRequestEvent != null)
			{
				foreach (Delegate delegate240 in this.OnServerDeregisterGameRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate240.Target, instance))
					{
						this.OnServerDeregisterGameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.DeregisterGameRequest>)delegate240;
					}
				}
			}
			if (this.OnServerDeregisterGameResultEvent != null)
			{
				foreach (Delegate delegate241 in this.OnServerDeregisterGameResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate241.Target, instance))
					{
						this.OnServerDeregisterGameResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.DeregisterGameResponse>)delegate241;
					}
				}
			}
			if (this.OnServerEvaluateRandomResultTableRequestEvent != null)
			{
				foreach (Delegate delegate242 in this.OnServerEvaluateRandomResultTableRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate242.Target, instance))
					{
						this.OnServerEvaluateRandomResultTableRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<EvaluateRandomResultTableRequest>)delegate242;
					}
				}
			}
			if (this.OnServerEvaluateRandomResultTableResultEvent != null)
			{
				foreach (Delegate delegate243 in this.OnServerEvaluateRandomResultTableResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate243.Target, instance))
					{
						this.OnServerEvaluateRandomResultTableResultEvent -= (PlayFabEvents.PlayFabResultEvent<EvaluateRandomResultTableResult>)delegate243;
					}
				}
			}
			if (this.OnServerExecuteCloudScriptRequestEvent != null)
			{
				foreach (Delegate delegate244 in this.OnServerExecuteCloudScriptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate244.Target, instance))
					{
						this.OnServerExecuteCloudScriptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ExecuteCloudScriptServerRequest>)delegate244;
					}
				}
			}
			if (this.OnServerExecuteCloudScriptResultEvent != null)
			{
				foreach (Delegate delegate245 in this.OnServerExecuteCloudScriptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate245.Target, instance))
					{
						this.OnServerExecuteCloudScriptResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ExecuteCloudScriptResult>)delegate245;
					}
				}
			}
			if (this.OnServerGetAllSegmentsRequestEvent != null)
			{
				foreach (Delegate delegate246 in this.OnServerGetAllSegmentsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate246.Target, instance))
					{
						this.OnServerGetAllSegmentsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetAllSegmentsRequest>)delegate246;
					}
				}
			}
			if (this.OnServerGetAllSegmentsResultEvent != null)
			{
				foreach (Delegate delegate247 in this.OnServerGetAllSegmentsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate247.Target, instance))
					{
						this.OnServerGetAllSegmentsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetAllSegmentsResult>)delegate247;
					}
				}
			}
			if (this.OnServerGetAllUsersCharactersRequestEvent != null)
			{
				foreach (Delegate delegate248 in this.OnServerGetAllUsersCharactersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate248.Target, instance))
					{
						this.OnServerGetAllUsersCharactersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.ListUsersCharactersRequest>)delegate248;
					}
				}
			}
			if (this.OnServerGetAllUsersCharactersResultEvent != null)
			{
				foreach (Delegate delegate249 in this.OnServerGetAllUsersCharactersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate249.Target, instance))
					{
						this.OnServerGetAllUsersCharactersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ListUsersCharactersResult>)delegate249;
					}
				}
			}
			if (this.OnServerGetCatalogItemsRequestEvent != null)
			{
				foreach (Delegate delegate250 in this.OnServerGetCatalogItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate250.Target, instance))
					{
						this.OnServerGetCatalogItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCatalogItemsRequest>)delegate250;
					}
				}
			}
			if (this.OnServerGetCatalogItemsResultEvent != null)
			{
				foreach (Delegate delegate251 in this.OnServerGetCatalogItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate251.Target, instance))
					{
						this.OnServerGetCatalogItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCatalogItemsResult>)delegate251;
					}
				}
			}
			if (this.OnServerGetCharacterDataRequestEvent != null)
			{
				foreach (Delegate delegate252 in this.OnServerGetCharacterDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate252.Target, instance))
					{
						this.OnServerGetCharacterDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterDataRequest>)delegate252;
					}
				}
			}
			if (this.OnServerGetCharacterDataResultEvent != null)
			{
				foreach (Delegate delegate253 in this.OnServerGetCharacterDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate253.Target, instance))
					{
						this.OnServerGetCharacterDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterDataResult>)delegate253;
					}
				}
			}
			if (this.OnServerGetCharacterInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate254 in this.OnServerGetCharacterInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate254.Target, instance))
					{
						this.OnServerGetCharacterInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterDataRequest>)delegate254;
					}
				}
			}
			if (this.OnServerGetCharacterInternalDataResultEvent != null)
			{
				foreach (Delegate delegate255 in this.OnServerGetCharacterInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate255.Target, instance))
					{
						this.OnServerGetCharacterInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterDataResult>)delegate255;
					}
				}
			}
			if (this.OnServerGetCharacterInventoryRequestEvent != null)
			{
				foreach (Delegate delegate256 in this.OnServerGetCharacterInventoryRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate256.Target, instance))
					{
						this.OnServerGetCharacterInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterInventoryRequest>)delegate256;
					}
				}
			}
			if (this.OnServerGetCharacterInventoryResultEvent != null)
			{
				foreach (Delegate delegate257 in this.OnServerGetCharacterInventoryResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate257.Target, instance))
					{
						this.OnServerGetCharacterInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterInventoryResult>)delegate257;
					}
				}
			}
			if (this.OnServerGetCharacterLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate258 in this.OnServerGetCharacterLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate258.Target, instance))
					{
						this.OnServerGetCharacterLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterLeaderboardRequest>)delegate258;
					}
				}
			}
			if (this.OnServerGetCharacterLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate259 in this.OnServerGetCharacterLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate259.Target, instance))
					{
						this.OnServerGetCharacterLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterLeaderboardResult>)delegate259;
					}
				}
			}
			if (this.OnServerGetCharacterReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate260 in this.OnServerGetCharacterReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate260.Target, instance))
					{
						this.OnServerGetCharacterReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterDataRequest>)delegate260;
					}
				}
			}
			if (this.OnServerGetCharacterReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate261 in this.OnServerGetCharacterReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate261.Target, instance))
					{
						this.OnServerGetCharacterReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterDataResult>)delegate261;
					}
				}
			}
			if (this.OnServerGetCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate262 in this.OnServerGetCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate262.Target, instance))
					{
						this.OnServerGetCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetCharacterStatisticsRequest>)delegate262;
					}
				}
			}
			if (this.OnServerGetCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate263 in this.OnServerGetCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate263.Target, instance))
					{
						this.OnServerGetCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetCharacterStatisticsResult>)delegate263;
					}
				}
			}
			if (this.OnServerGetContentDownloadUrlRequestEvent != null)
			{
				foreach (Delegate delegate264 in this.OnServerGetContentDownloadUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate264.Target, instance))
					{
						this.OnServerGetContentDownloadUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetContentDownloadUrlRequest>)delegate264;
					}
				}
			}
			if (this.OnServerGetContentDownloadUrlResultEvent != null)
			{
				foreach (Delegate delegate265 in this.OnServerGetContentDownloadUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate265.Target, instance))
					{
						this.OnServerGetContentDownloadUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetContentDownloadUrlResult>)delegate265;
					}
				}
			}
			if (this.OnServerGetFriendLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate266 in this.OnServerGetFriendLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate266.Target, instance))
					{
						this.OnServerGetFriendLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetFriendLeaderboardRequest>)delegate266;
					}
				}
			}
			if (this.OnServerGetFriendLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate267 in this.OnServerGetFriendLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate267.Target, instance))
					{
						this.OnServerGetFriendLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetLeaderboardResult>)delegate267;
					}
				}
			}
			if (this.OnServerGetFriendsListRequestEvent != null)
			{
				foreach (Delegate delegate268 in this.OnServerGetFriendsListRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate268.Target, instance))
					{
						this.OnServerGetFriendsListRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetFriendsListRequest>)delegate268;
					}
				}
			}
			if (this.OnServerGetFriendsListResultEvent != null)
			{
				foreach (Delegate delegate269 in this.OnServerGetFriendsListResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate269.Target, instance))
					{
						this.OnServerGetFriendsListResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetFriendsListResult>)delegate269;
					}
				}
			}
			if (this.OnServerGetLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate270 in this.OnServerGetLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate270.Target, instance))
					{
						this.OnServerGetLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetLeaderboardRequest>)delegate270;
					}
				}
			}
			if (this.OnServerGetLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate271 in this.OnServerGetLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate271.Target, instance))
					{
						this.OnServerGetLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetLeaderboardResult>)delegate271;
					}
				}
			}
			if (this.OnServerGetLeaderboardAroundCharacterRequestEvent != null)
			{
				foreach (Delegate delegate272 in this.OnServerGetLeaderboardAroundCharacterRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate272.Target, instance))
					{
						this.OnServerGetLeaderboardAroundCharacterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetLeaderboardAroundCharacterRequest>)delegate272;
					}
				}
			}
			if (this.OnServerGetLeaderboardAroundCharacterResultEvent != null)
			{
				foreach (Delegate delegate273 in this.OnServerGetLeaderboardAroundCharacterResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate273.Target, instance))
					{
						this.OnServerGetLeaderboardAroundCharacterResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetLeaderboardAroundCharacterResult>)delegate273;
					}
				}
			}
			if (this.OnServerGetLeaderboardAroundUserRequestEvent != null)
			{
				foreach (Delegate delegate274 in this.OnServerGetLeaderboardAroundUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate274.Target, instance))
					{
						this.OnServerGetLeaderboardAroundUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundUserRequest>)delegate274;
					}
				}
			}
			if (this.OnServerGetLeaderboardAroundUserResultEvent != null)
			{
				foreach (Delegate delegate275 in this.OnServerGetLeaderboardAroundUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate275.Target, instance))
					{
						this.OnServerGetLeaderboardAroundUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundUserResult>)delegate275;
					}
				}
			}
			if (this.OnServerGetLeaderboardForUserCharactersRequestEvent != null)
			{
				foreach (Delegate delegate276 in this.OnServerGetLeaderboardForUserCharactersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate276.Target, instance))
					{
						this.OnServerGetLeaderboardForUserCharactersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetLeaderboardForUsersCharactersRequest>)delegate276;
					}
				}
			}
			if (this.OnServerGetLeaderboardForUserCharactersResultEvent != null)
			{
				foreach (Delegate delegate277 in this.OnServerGetLeaderboardForUserCharactersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate277.Target, instance))
					{
						this.OnServerGetLeaderboardForUserCharactersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetLeaderboardForUsersCharactersResult>)delegate277;
					}
				}
			}
			if (this.OnServerGetPlayerCombinedInfoRequestEvent != null)
			{
				foreach (Delegate delegate278 in this.OnServerGetPlayerCombinedInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate278.Target, instance))
					{
						this.OnServerGetPlayerCombinedInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerCombinedInfoRequest>)delegate278;
					}
				}
			}
			if (this.OnServerGetPlayerCombinedInfoResultEvent != null)
			{
				foreach (Delegate delegate279 in this.OnServerGetPlayerCombinedInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate279.Target, instance))
					{
						this.OnServerGetPlayerCombinedInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerCombinedInfoResult>)delegate279;
					}
				}
			}
			if (this.OnServerGetPlayerProfileRequestEvent != null)
			{
				foreach (Delegate delegate280 in this.OnServerGetPlayerProfileRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate280.Target, instance))
					{
						this.OnServerGetPlayerProfileRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerProfileRequest>)delegate280;
					}
				}
			}
			if (this.OnServerGetPlayerProfileResultEvent != null)
			{
				foreach (Delegate delegate281 in this.OnServerGetPlayerProfileResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate281.Target, instance))
					{
						this.OnServerGetPlayerProfileResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerProfileResult>)delegate281;
					}
				}
			}
			if (this.OnServerGetPlayerSegmentsRequestEvent != null)
			{
				foreach (Delegate delegate282 in this.OnServerGetPlayerSegmentsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate282.Target, instance))
					{
						this.OnServerGetPlayerSegmentsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayersSegmentsRequest>)delegate282;
					}
				}
			}
			if (this.OnServerGetPlayerSegmentsResultEvent != null)
			{
				foreach (Delegate delegate283 in this.OnServerGetPlayerSegmentsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate283.Target, instance))
					{
						this.OnServerGetPlayerSegmentsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerSegmentsResult>)delegate283;
					}
				}
			}
			if (this.OnServerGetPlayersInSegmentRequestEvent != null)
			{
				foreach (Delegate delegate284 in this.OnServerGetPlayersInSegmentRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate284.Target, instance))
					{
						this.OnServerGetPlayersInSegmentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayersInSegmentRequest>)delegate284;
					}
				}
			}
			if (this.OnServerGetPlayersInSegmentResultEvent != null)
			{
				foreach (Delegate delegate285 in this.OnServerGetPlayersInSegmentResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate285.Target, instance))
					{
						this.OnServerGetPlayersInSegmentResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayersInSegmentResult>)delegate285;
					}
				}
			}
			if (this.OnServerGetPlayerStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate286 in this.OnServerGetPlayerStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate286.Target, instance))
					{
						this.OnServerGetPlayerStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerStatisticsRequest>)delegate286;
					}
				}
			}
			if (this.OnServerGetPlayerStatisticsResultEvent != null)
			{
				foreach (Delegate delegate287 in this.OnServerGetPlayerStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate287.Target, instance))
					{
						this.OnServerGetPlayerStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerStatisticsResult>)delegate287;
					}
				}
			}
			if (this.OnServerGetPlayerStatisticVersionsRequestEvent != null)
			{
				foreach (Delegate delegate288 in this.OnServerGetPlayerStatisticVersionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate288.Target, instance))
					{
						this.OnServerGetPlayerStatisticVersionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerStatisticVersionsRequest>)delegate288;
					}
				}
			}
			if (this.OnServerGetPlayerStatisticVersionsResultEvent != null)
			{
				foreach (Delegate delegate289 in this.OnServerGetPlayerStatisticVersionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate289.Target, instance))
					{
						this.OnServerGetPlayerStatisticVersionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerStatisticVersionsResult>)delegate289;
					}
				}
			}
			if (this.OnServerGetPlayerTagsRequestEvent != null)
			{
				foreach (Delegate delegate290 in this.OnServerGetPlayerTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate290.Target, instance))
					{
						this.OnServerGetPlayerTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayerTagsRequest>)delegate290;
					}
				}
			}
			if (this.OnServerGetPlayerTagsResultEvent != null)
			{
				foreach (Delegate delegate291 in this.OnServerGetPlayerTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate291.Target, instance))
					{
						this.OnServerGetPlayerTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayerTagsResult>)delegate291;
					}
				}
			}
			if (this.OnServerGetPlayFabIDsFromFacebookIDsRequestEvent != null)
			{
				foreach (Delegate delegate292 in this.OnServerGetPlayFabIDsFromFacebookIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate292.Target, instance))
					{
						this.OnServerGetPlayFabIDsFromFacebookIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayFabIDsFromFacebookIDsRequest>)delegate292;
					}
				}
			}
			if (this.OnServerGetPlayFabIDsFromFacebookIDsResultEvent != null)
			{
				foreach (Delegate delegate293 in this.OnServerGetPlayFabIDsFromFacebookIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate293.Target, instance))
					{
						this.OnServerGetPlayFabIDsFromFacebookIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayFabIDsFromFacebookIDsResult>)delegate293;
					}
				}
			}
			if (this.OnServerGetPlayFabIDsFromSteamIDsRequestEvent != null)
			{
				foreach (Delegate delegate294 in this.OnServerGetPlayFabIDsFromSteamIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate294.Target, instance))
					{
						this.OnServerGetPlayFabIDsFromSteamIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPlayFabIDsFromSteamIDsRequest>)delegate294;
					}
				}
			}
			if (this.OnServerGetPlayFabIDsFromSteamIDsResultEvent != null)
			{
				foreach (Delegate delegate295 in this.OnServerGetPlayFabIDsFromSteamIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate295.Target, instance))
					{
						this.OnServerGetPlayFabIDsFromSteamIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPlayFabIDsFromSteamIDsResult>)delegate295;
					}
				}
			}
			if (this.OnServerGetPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate296 in this.OnServerGetPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate296.Target, instance))
					{
						this.OnServerGetPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetPublisherDataRequest>)delegate296;
					}
				}
			}
			if (this.OnServerGetPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate297 in this.OnServerGetPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate297.Target, instance))
					{
						this.OnServerGetPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetPublisherDataResult>)delegate297;
					}
				}
			}
			if (this.OnServerGetRandomResultTablesRequestEvent != null)
			{
				foreach (Delegate delegate298 in this.OnServerGetRandomResultTablesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate298.Target, instance))
					{
						this.OnServerGetRandomResultTablesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetRandomResultTablesRequest>)delegate298;
					}
				}
			}
			if (this.OnServerGetRandomResultTablesResultEvent != null)
			{
				foreach (Delegate delegate299 in this.OnServerGetRandomResultTablesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate299.Target, instance))
					{
						this.OnServerGetRandomResultTablesResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetRandomResultTablesResult>)delegate299;
					}
				}
			}
			if (this.OnServerGetSharedGroupDataRequestEvent != null)
			{
				foreach (Delegate delegate300 in this.OnServerGetSharedGroupDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate300.Target, instance))
					{
						this.OnServerGetSharedGroupDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetSharedGroupDataRequest>)delegate300;
					}
				}
			}
			if (this.OnServerGetSharedGroupDataResultEvent != null)
			{
				foreach (Delegate delegate301 in this.OnServerGetSharedGroupDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate301.Target, instance))
					{
						this.OnServerGetSharedGroupDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetSharedGroupDataResult>)delegate301;
					}
				}
			}
			if (this.OnServerGetTimeRequestEvent != null)
			{
				foreach (Delegate delegate302 in this.OnServerGetTimeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate302.Target, instance))
					{
						this.OnServerGetTimeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetTimeRequest>)delegate302;
					}
				}
			}
			if (this.OnServerGetTimeResultEvent != null)
			{
				foreach (Delegate delegate303 in this.OnServerGetTimeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate303.Target, instance))
					{
						this.OnServerGetTimeResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetTimeResult>)delegate303;
					}
				}
			}
			if (this.OnServerGetTitleDataRequestEvent != null)
			{
				foreach (Delegate delegate304 in this.OnServerGetTitleDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate304.Target, instance))
					{
						this.OnServerGetTitleDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetTitleDataRequest>)delegate304;
					}
				}
			}
			if (this.OnServerGetTitleDataResultEvent != null)
			{
				foreach (Delegate delegate305 in this.OnServerGetTitleDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate305.Target, instance))
					{
						this.OnServerGetTitleDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetTitleDataResult>)delegate305;
					}
				}
			}
			if (this.OnServerGetTitleInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate306 in this.OnServerGetTitleInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate306.Target, instance))
					{
						this.OnServerGetTitleInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetTitleDataRequest>)delegate306;
					}
				}
			}
			if (this.OnServerGetTitleInternalDataResultEvent != null)
			{
				foreach (Delegate delegate307 in this.OnServerGetTitleInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate307.Target, instance))
					{
						this.OnServerGetTitleInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetTitleDataResult>)delegate307;
					}
				}
			}
			if (this.OnServerGetTitleNewsRequestEvent != null)
			{
				foreach (Delegate delegate308 in this.OnServerGetTitleNewsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate308.Target, instance))
					{
						this.OnServerGetTitleNewsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetTitleNewsRequest>)delegate308;
					}
				}
			}
			if (this.OnServerGetTitleNewsResultEvent != null)
			{
				foreach (Delegate delegate309 in this.OnServerGetTitleNewsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate309.Target, instance))
					{
						this.OnServerGetTitleNewsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetTitleNewsResult>)delegate309;
					}
				}
			}
			if (this.OnServerGetUserAccountInfoRequestEvent != null)
			{
				foreach (Delegate delegate310 in this.OnServerGetUserAccountInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate310.Target, instance))
					{
						this.OnServerGetUserAccountInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserAccountInfoRequest>)delegate310;
					}
				}
			}
			if (this.OnServerGetUserAccountInfoResultEvent != null)
			{
				foreach (Delegate delegate311 in this.OnServerGetUserAccountInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate311.Target, instance))
					{
						this.OnServerGetUserAccountInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserAccountInfoResult>)delegate311;
					}
				}
			}
			if (this.OnServerGetUserBansRequestEvent != null)
			{
				foreach (Delegate delegate312 in this.OnServerGetUserBansRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate312.Target, instance))
					{
						this.OnServerGetUserBansRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserBansRequest>)delegate312;
					}
				}
			}
			if (this.OnServerGetUserBansResultEvent != null)
			{
				foreach (Delegate delegate313 in this.OnServerGetUserBansResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate313.Target, instance))
					{
						this.OnServerGetUserBansResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserBansResult>)delegate313;
					}
				}
			}
			if (this.OnServerGetUserDataRequestEvent != null)
			{
				foreach (Delegate delegate314 in this.OnServerGetUserDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate314.Target, instance))
					{
						this.OnServerGetUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest>)delegate314;
					}
				}
			}
			if (this.OnServerGetUserDataResultEvent != null)
			{
				foreach (Delegate delegate315 in this.OnServerGetUserDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate315.Target, instance))
					{
						this.OnServerGetUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult>)delegate315;
					}
				}
			}
			if (this.OnServerGetUserInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate316 in this.OnServerGetUserInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate316.Target, instance))
					{
						this.OnServerGetUserInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest>)delegate316;
					}
				}
			}
			if (this.OnServerGetUserInternalDataResultEvent != null)
			{
				foreach (Delegate delegate317 in this.OnServerGetUserInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate317.Target, instance))
					{
						this.OnServerGetUserInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult>)delegate317;
					}
				}
			}
			if (this.OnServerGetUserInventoryRequestEvent != null)
			{
				foreach (Delegate delegate318 in this.OnServerGetUserInventoryRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate318.Target, instance))
					{
						this.OnServerGetUserInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserInventoryRequest>)delegate318;
					}
				}
			}
			if (this.OnServerGetUserInventoryResultEvent != null)
			{
				foreach (Delegate delegate319 in this.OnServerGetUserInventoryResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate319.Target, instance))
					{
						this.OnServerGetUserInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserInventoryResult>)delegate319;
					}
				}
			}
			if (this.OnServerGetUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate320 in this.OnServerGetUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate320.Target, instance))
					{
						this.OnServerGetUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest>)delegate320;
					}
				}
			}
			if (this.OnServerGetUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate321 in this.OnServerGetUserPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate321.Target, instance))
					{
						this.OnServerGetUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult>)delegate321;
					}
				}
			}
			if (this.OnServerGetUserPublisherInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate322 in this.OnServerGetUserPublisherInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate322.Target, instance))
					{
						this.OnServerGetUserPublisherInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest>)delegate322;
					}
				}
			}
			if (this.OnServerGetUserPublisherInternalDataResultEvent != null)
			{
				foreach (Delegate delegate323 in this.OnServerGetUserPublisherInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate323.Target, instance))
					{
						this.OnServerGetUserPublisherInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult>)delegate323;
					}
				}
			}
			if (this.OnServerGetUserPublisherReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate324 in this.OnServerGetUserPublisherReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate324.Target, instance))
					{
						this.OnServerGetUserPublisherReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest>)delegate324;
					}
				}
			}
			if (this.OnServerGetUserPublisherReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate325 in this.OnServerGetUserPublisherReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate325.Target, instance))
					{
						this.OnServerGetUserPublisherReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult>)delegate325;
					}
				}
			}
			if (this.OnServerGetUserReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate326 in this.OnServerGetUserReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate326.Target, instance))
					{
						this.OnServerGetUserReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GetUserDataRequest>)delegate326;
					}
				}
			}
			if (this.OnServerGetUserReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate327 in this.OnServerGetUserReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate327.Target, instance))
					{
						this.OnServerGetUserReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GetUserDataResult>)delegate327;
					}
				}
			}
			if (this.OnServerGrantCharacterToUserRequestEvent != null)
			{
				foreach (Delegate delegate328 in this.OnServerGrantCharacterToUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate328.Target, instance))
					{
						this.OnServerGrantCharacterToUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GrantCharacterToUserRequest>)delegate328;
					}
				}
			}
			if (this.OnServerGrantCharacterToUserResultEvent != null)
			{
				foreach (Delegate delegate329 in this.OnServerGrantCharacterToUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate329.Target, instance))
					{
						this.OnServerGrantCharacterToUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GrantCharacterToUserResult>)delegate329;
					}
				}
			}
			if (this.OnServerGrantItemsToCharacterRequestEvent != null)
			{
				foreach (Delegate delegate330 in this.OnServerGrantItemsToCharacterRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate330.Target, instance))
					{
						this.OnServerGrantItemsToCharacterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GrantItemsToCharacterRequest>)delegate330;
					}
				}
			}
			if (this.OnServerGrantItemsToCharacterResultEvent != null)
			{
				foreach (Delegate delegate331 in this.OnServerGrantItemsToCharacterResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate331.Target, instance))
					{
						this.OnServerGrantItemsToCharacterResultEvent -= (PlayFabEvents.PlayFabResultEvent<GrantItemsToCharacterResult>)delegate331;
					}
				}
			}
			if (this.OnServerGrantItemsToUserRequestEvent != null)
			{
				foreach (Delegate delegate332 in this.OnServerGrantItemsToUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate332.Target, instance))
					{
						this.OnServerGrantItemsToUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GrantItemsToUserRequest>)delegate332;
					}
				}
			}
			if (this.OnServerGrantItemsToUserResultEvent != null)
			{
				foreach (Delegate delegate333 in this.OnServerGrantItemsToUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate333.Target, instance))
					{
						this.OnServerGrantItemsToUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<GrantItemsToUserResult>)delegate333;
					}
				}
			}
			if (this.OnServerGrantItemsToUsersRequestEvent != null)
			{
				foreach (Delegate delegate334 in this.OnServerGrantItemsToUsersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate334.Target, instance))
					{
						this.OnServerGrantItemsToUsersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.GrantItemsToUsersRequest>)delegate334;
					}
				}
			}
			if (this.OnServerGrantItemsToUsersResultEvent != null)
			{
				foreach (Delegate delegate335 in this.OnServerGrantItemsToUsersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate335.Target, instance))
					{
						this.OnServerGrantItemsToUsersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.GrantItemsToUsersResult>)delegate335;
					}
				}
			}
			if (this.OnServerModifyItemUsesRequestEvent != null)
			{
				foreach (Delegate delegate336 in this.OnServerModifyItemUsesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate336.Target, instance))
					{
						this.OnServerModifyItemUsesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ModifyItemUsesRequest>)delegate336;
					}
				}
			}
			if (this.OnServerModifyItemUsesResultEvent != null)
			{
				foreach (Delegate delegate337 in this.OnServerModifyItemUsesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate337.Target, instance))
					{
						this.OnServerModifyItemUsesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyItemUsesResult>)delegate337;
					}
				}
			}
			if (this.OnServerMoveItemToCharacterFromCharacterRequestEvent != null)
			{
				foreach (Delegate delegate338 in this.OnServerMoveItemToCharacterFromCharacterRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate338.Target, instance))
					{
						this.OnServerMoveItemToCharacterFromCharacterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<MoveItemToCharacterFromCharacterRequest>)delegate338;
					}
				}
			}
			if (this.OnServerMoveItemToCharacterFromCharacterResultEvent != null)
			{
				foreach (Delegate delegate339 in this.OnServerMoveItemToCharacterFromCharacterResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate339.Target, instance))
					{
						this.OnServerMoveItemToCharacterFromCharacterResultEvent -= (PlayFabEvents.PlayFabResultEvent<MoveItemToCharacterFromCharacterResult>)delegate339;
					}
				}
			}
			if (this.OnServerMoveItemToCharacterFromUserRequestEvent != null)
			{
				foreach (Delegate delegate340 in this.OnServerMoveItemToCharacterFromUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate340.Target, instance))
					{
						this.OnServerMoveItemToCharacterFromUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<MoveItemToCharacterFromUserRequest>)delegate340;
					}
				}
			}
			if (this.OnServerMoveItemToCharacterFromUserResultEvent != null)
			{
				foreach (Delegate delegate341 in this.OnServerMoveItemToCharacterFromUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate341.Target, instance))
					{
						this.OnServerMoveItemToCharacterFromUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<MoveItemToCharacterFromUserResult>)delegate341;
					}
				}
			}
			if (this.OnServerMoveItemToUserFromCharacterRequestEvent != null)
			{
				foreach (Delegate delegate342 in this.OnServerMoveItemToUserFromCharacterRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate342.Target, instance))
					{
						this.OnServerMoveItemToUserFromCharacterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<MoveItemToUserFromCharacterRequest>)delegate342;
					}
				}
			}
			if (this.OnServerMoveItemToUserFromCharacterResultEvent != null)
			{
				foreach (Delegate delegate343 in this.OnServerMoveItemToUserFromCharacterResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate343.Target, instance))
					{
						this.OnServerMoveItemToUserFromCharacterResultEvent -= (PlayFabEvents.PlayFabResultEvent<MoveItemToUserFromCharacterResult>)delegate343;
					}
				}
			}
			if (this.OnServerNotifyMatchmakerPlayerLeftRequestEvent != null)
			{
				foreach (Delegate delegate344 in this.OnServerNotifyMatchmakerPlayerLeftRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate344.Target, instance))
					{
						this.OnServerNotifyMatchmakerPlayerLeftRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<NotifyMatchmakerPlayerLeftRequest>)delegate344;
					}
				}
			}
			if (this.OnServerNotifyMatchmakerPlayerLeftResultEvent != null)
			{
				foreach (Delegate delegate345 in this.OnServerNotifyMatchmakerPlayerLeftResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate345.Target, instance))
					{
						this.OnServerNotifyMatchmakerPlayerLeftResultEvent -= (PlayFabEvents.PlayFabResultEvent<NotifyMatchmakerPlayerLeftResult>)delegate345;
					}
				}
			}
			if (this.OnServerRedeemCouponRequestEvent != null)
			{
				foreach (Delegate delegate346 in this.OnServerRedeemCouponRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate346.Target, instance))
					{
						this.OnServerRedeemCouponRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RedeemCouponRequest>)delegate346;
					}
				}
			}
			if (this.OnServerRedeemCouponResultEvent != null)
			{
				foreach (Delegate delegate347 in this.OnServerRedeemCouponResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate347.Target, instance))
					{
						this.OnServerRedeemCouponResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RedeemCouponResult>)delegate347;
					}
				}
			}
			if (this.OnServerRedeemMatchmakerTicketRequestEvent != null)
			{
				foreach (Delegate delegate348 in this.OnServerRedeemMatchmakerTicketRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate348.Target, instance))
					{
						this.OnServerRedeemMatchmakerTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RedeemMatchmakerTicketRequest>)delegate348;
					}
				}
			}
			if (this.OnServerRedeemMatchmakerTicketResultEvent != null)
			{
				foreach (Delegate delegate349 in this.OnServerRedeemMatchmakerTicketResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate349.Target, instance))
					{
						this.OnServerRedeemMatchmakerTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<RedeemMatchmakerTicketResult>)delegate349;
					}
				}
			}
			if (this.OnServerRefreshGameServerInstanceHeartbeatRequestEvent != null)
			{
				foreach (Delegate delegate350 in this.OnServerRefreshGameServerInstanceHeartbeatRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate350.Target, instance))
					{
						this.OnServerRefreshGameServerInstanceHeartbeatRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RefreshGameServerInstanceHeartbeatRequest>)delegate350;
					}
				}
			}
			if (this.OnServerRefreshGameServerInstanceHeartbeatResultEvent != null)
			{
				foreach (Delegate delegate351 in this.OnServerRefreshGameServerInstanceHeartbeatResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate351.Target, instance))
					{
						this.OnServerRefreshGameServerInstanceHeartbeatResultEvent -= (PlayFabEvents.PlayFabResultEvent<RefreshGameServerInstanceHeartbeatResult>)delegate351;
					}
				}
			}
			if (this.OnServerRegisterGameRequestEvent != null)
			{
				foreach (Delegate delegate352 in this.OnServerRegisterGameRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate352.Target, instance))
					{
						this.OnServerRegisterGameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RegisterGameRequest>)delegate352;
					}
				}
			}
			if (this.OnServerRegisterGameResultEvent != null)
			{
				foreach (Delegate delegate353 in this.OnServerRegisterGameResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate353.Target, instance))
					{
						this.OnServerRegisterGameResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RegisterGameResponse>)delegate353;
					}
				}
			}
			if (this.OnServerRemoveFriendRequestEvent != null)
			{
				foreach (Delegate delegate354 in this.OnServerRemoveFriendRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate354.Target, instance))
					{
						this.OnServerRemoveFriendRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RemoveFriendRequest>)delegate354;
					}
				}
			}
			if (this.OnServerRemoveFriendResultEvent != null)
			{
				foreach (Delegate delegate355 in this.OnServerRemoveFriendResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate355.Target, instance))
					{
						this.OnServerRemoveFriendResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult>)delegate355;
					}
				}
			}
			if (this.OnServerRemovePlayerTagRequestEvent != null)
			{
				foreach (Delegate delegate356 in this.OnServerRemovePlayerTagRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate356.Target, instance))
					{
						this.OnServerRemovePlayerTagRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RemovePlayerTagRequest>)delegate356;
					}
				}
			}
			if (this.OnServerRemovePlayerTagResultEvent != null)
			{
				foreach (Delegate delegate357 in this.OnServerRemovePlayerTagResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate357.Target, instance))
					{
						this.OnServerRemovePlayerTagResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RemovePlayerTagResult>)delegate357;
					}
				}
			}
			if (this.OnServerRemoveSharedGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate358 in this.OnServerRemoveSharedGroupMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate358.Target, instance))
					{
						this.OnServerRemoveSharedGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RemoveSharedGroupMembersRequest>)delegate358;
					}
				}
			}
			if (this.OnServerRemoveSharedGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate359 in this.OnServerRemoveSharedGroupMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate359.Target, instance))
					{
						this.OnServerRemoveSharedGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RemoveSharedGroupMembersResult>)delegate359;
					}
				}
			}
			if (this.OnServerReportPlayerRequestEvent != null)
			{
				foreach (Delegate delegate360 in this.OnServerReportPlayerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate360.Target, instance))
					{
						this.OnServerReportPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ReportPlayerServerRequest>)delegate360;
					}
				}
			}
			if (this.OnServerReportPlayerResultEvent != null)
			{
				foreach (Delegate delegate361 in this.OnServerReportPlayerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate361.Target, instance))
					{
						this.OnServerReportPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<ReportPlayerServerResult>)delegate361;
					}
				}
			}
			if (this.OnServerRevokeAllBansForUserRequestEvent != null)
			{
				foreach (Delegate delegate362 in this.OnServerRevokeAllBansForUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate362.Target, instance))
					{
						this.OnServerRevokeAllBansForUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RevokeAllBansForUserRequest>)delegate362;
					}
				}
			}
			if (this.OnServerRevokeAllBansForUserResultEvent != null)
			{
				foreach (Delegate delegate363 in this.OnServerRevokeAllBansForUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate363.Target, instance))
					{
						this.OnServerRevokeAllBansForUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RevokeAllBansForUserResult>)delegate363;
					}
				}
			}
			if (this.OnServerRevokeBansRequestEvent != null)
			{
				foreach (Delegate delegate364 in this.OnServerRevokeBansRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate364.Target, instance))
					{
						this.OnServerRevokeBansRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RevokeBansRequest>)delegate364;
					}
				}
			}
			if (this.OnServerRevokeBansResultEvent != null)
			{
				foreach (Delegate delegate365 in this.OnServerRevokeBansResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate365.Target, instance))
					{
						this.OnServerRevokeBansResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RevokeBansResult>)delegate365;
					}
				}
			}
			if (this.OnServerRevokeInventoryItemRequestEvent != null)
			{
				foreach (Delegate delegate366 in this.OnServerRevokeInventoryItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate366.Target, instance))
					{
						this.OnServerRevokeInventoryItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.RevokeInventoryItemRequest>)delegate366;
					}
				}
			}
			if (this.OnServerRevokeInventoryItemResultEvent != null)
			{
				foreach (Delegate delegate367 in this.OnServerRevokeInventoryItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate367.Target, instance))
					{
						this.OnServerRevokeInventoryItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.RevokeInventoryResult>)delegate367;
					}
				}
			}
			if (this.OnServerSendCustomAccountRecoveryEmailRequestEvent != null)
			{
				foreach (Delegate delegate368 in this.OnServerSendCustomAccountRecoveryEmailRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate368.Target, instance))
					{
						this.OnServerSendCustomAccountRecoveryEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SendCustomAccountRecoveryEmailRequest>)delegate368;
					}
				}
			}
			if (this.OnServerSendCustomAccountRecoveryEmailResultEvent != null)
			{
				foreach (Delegate delegate369 in this.OnServerSendCustomAccountRecoveryEmailResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate369.Target, instance))
					{
						this.OnServerSendCustomAccountRecoveryEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<SendCustomAccountRecoveryEmailResult>)delegate369;
					}
				}
			}
			if (this.OnServerSendEmailFromTemplateRequestEvent != null)
			{
				foreach (Delegate delegate370 in this.OnServerSendEmailFromTemplateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate370.Target, instance))
					{
						this.OnServerSendEmailFromTemplateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SendEmailFromTemplateRequest>)delegate370;
					}
				}
			}
			if (this.OnServerSendEmailFromTemplateResultEvent != null)
			{
				foreach (Delegate delegate371 in this.OnServerSendEmailFromTemplateResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate371.Target, instance))
					{
						this.OnServerSendEmailFromTemplateResultEvent -= (PlayFabEvents.PlayFabResultEvent<SendEmailFromTemplateResult>)delegate371;
					}
				}
			}
			if (this.OnServerSendPushNotificationRequestEvent != null)
			{
				foreach (Delegate delegate372 in this.OnServerSendPushNotificationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate372.Target, instance))
					{
						this.OnServerSendPushNotificationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SendPushNotificationRequest>)delegate372;
					}
				}
			}
			if (this.OnServerSendPushNotificationResultEvent != null)
			{
				foreach (Delegate delegate373 in this.OnServerSendPushNotificationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate373.Target, instance))
					{
						this.OnServerSendPushNotificationResultEvent -= (PlayFabEvents.PlayFabResultEvent<SendPushNotificationResult>)delegate373;
					}
				}
			}
			if (this.OnServerSetFriendTagsRequestEvent != null)
			{
				foreach (Delegate delegate374 in this.OnServerSetFriendTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate374.Target, instance))
					{
						this.OnServerSetFriendTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetFriendTagsRequest>)delegate374;
					}
				}
			}
			if (this.OnServerSetFriendTagsResultEvent != null)
			{
				foreach (Delegate delegate375 in this.OnServerSetFriendTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate375.Target, instance))
					{
						this.OnServerSetFriendTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult>)delegate375;
					}
				}
			}
			if (this.OnServerSetGameServerInstanceDataRequestEvent != null)
			{
				foreach (Delegate delegate376 in this.OnServerSetGameServerInstanceDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate376.Target, instance))
					{
						this.OnServerSetGameServerInstanceDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetGameServerInstanceDataRequest>)delegate376;
					}
				}
			}
			if (this.OnServerSetGameServerInstanceDataResultEvent != null)
			{
				foreach (Delegate delegate377 in this.OnServerSetGameServerInstanceDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate377.Target, instance))
					{
						this.OnServerSetGameServerInstanceDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetGameServerInstanceDataResult>)delegate377;
					}
				}
			}
			if (this.OnServerSetGameServerInstanceStateRequestEvent != null)
			{
				foreach (Delegate delegate378 in this.OnServerSetGameServerInstanceStateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate378.Target, instance))
					{
						this.OnServerSetGameServerInstanceStateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetGameServerInstanceStateRequest>)delegate378;
					}
				}
			}
			if (this.OnServerSetGameServerInstanceStateResultEvent != null)
			{
				foreach (Delegate delegate379 in this.OnServerSetGameServerInstanceStateResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate379.Target, instance))
					{
						this.OnServerSetGameServerInstanceStateResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetGameServerInstanceStateResult>)delegate379;
					}
				}
			}
			if (this.OnServerSetGameServerInstanceTagsRequestEvent != null)
			{
				foreach (Delegate delegate380 in this.OnServerSetGameServerInstanceTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate380.Target, instance))
					{
						this.OnServerSetGameServerInstanceTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetGameServerInstanceTagsRequest>)delegate380;
					}
				}
			}
			if (this.OnServerSetGameServerInstanceTagsResultEvent != null)
			{
				foreach (Delegate delegate381 in this.OnServerSetGameServerInstanceTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate381.Target, instance))
					{
						this.OnServerSetGameServerInstanceTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetGameServerInstanceTagsResult>)delegate381;
					}
				}
			}
			if (this.OnServerSetPlayerSecretRequestEvent != null)
			{
				foreach (Delegate delegate382 in this.OnServerSetPlayerSecretRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate382.Target, instance))
					{
						this.OnServerSetPlayerSecretRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetPlayerSecretRequest>)delegate382;
					}
				}
			}
			if (this.OnServerSetPlayerSecretResultEvent != null)
			{
				foreach (Delegate delegate383 in this.OnServerSetPlayerSecretResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate383.Target, instance))
					{
						this.OnServerSetPlayerSecretResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.SetPlayerSecretResult>)delegate383;
					}
				}
			}
			if (this.OnServerSetPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate384 in this.OnServerSetPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate384.Target, instance))
					{
						this.OnServerSetPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetPublisherDataRequest>)delegate384;
					}
				}
			}
			if (this.OnServerSetPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate385 in this.OnServerSetPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate385.Target, instance))
					{
						this.OnServerSetPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.SetPublisherDataResult>)delegate385;
					}
				}
			}
			if (this.OnServerSetTitleDataRequestEvent != null)
			{
				foreach (Delegate delegate386 in this.OnServerSetTitleDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate386.Target, instance))
					{
						this.OnServerSetTitleDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetTitleDataRequest>)delegate386;
					}
				}
			}
			if (this.OnServerSetTitleDataResultEvent != null)
			{
				foreach (Delegate delegate387 in this.OnServerSetTitleDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate387.Target, instance))
					{
						this.OnServerSetTitleDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.SetTitleDataResult>)delegate387;
					}
				}
			}
			if (this.OnServerSetTitleInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate388 in this.OnServerSetTitleInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate388.Target, instance))
					{
						this.OnServerSetTitleInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SetTitleDataRequest>)delegate388;
					}
				}
			}
			if (this.OnServerSetTitleInternalDataResultEvent != null)
			{
				foreach (Delegate delegate389 in this.OnServerSetTitleInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate389.Target, instance))
					{
						this.OnServerSetTitleInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.SetTitleDataResult>)delegate389;
					}
				}
			}
			if (this.OnServerSubtractCharacterVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate390 in this.OnServerSubtractCharacterVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate390.Target, instance))
					{
						this.OnServerSubtractCharacterVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SubtractCharacterVirtualCurrencyRequest>)delegate390;
					}
				}
			}
			if (this.OnServerSubtractCharacterVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate391 in this.OnServerSubtractCharacterVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate391.Target, instance))
					{
						this.OnServerSubtractCharacterVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyCharacterVirtualCurrencyResult>)delegate391;
					}
				}
			}
			if (this.OnServerSubtractUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate392 in this.OnServerSubtractUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate392.Target, instance))
					{
						this.OnServerSubtractUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.SubtractUserVirtualCurrencyRequest>)delegate392;
					}
				}
			}
			if (this.OnServerSubtractUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate393 in this.OnServerSubtractUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate393.Target, instance))
					{
						this.OnServerSubtractUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.ModifyUserVirtualCurrencyResult>)delegate393;
					}
				}
			}
			if (this.OnServerUnlockContainerInstanceRequestEvent != null)
			{
				foreach (Delegate delegate394 in this.OnServerUnlockContainerInstanceRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate394.Target, instance))
					{
						this.OnServerUnlockContainerInstanceRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UnlockContainerInstanceRequest>)delegate394;
					}
				}
			}
			if (this.OnServerUnlockContainerInstanceResultEvent != null)
			{
				foreach (Delegate delegate395 in this.OnServerUnlockContainerInstanceResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate395.Target, instance))
					{
						this.OnServerUnlockContainerInstanceResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UnlockContainerItemResult>)delegate395;
					}
				}
			}
			if (this.OnServerUnlockContainerItemRequestEvent != null)
			{
				foreach (Delegate delegate396 in this.OnServerUnlockContainerItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate396.Target, instance))
					{
						this.OnServerUnlockContainerItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UnlockContainerItemRequest>)delegate396;
					}
				}
			}
			if (this.OnServerUnlockContainerItemResultEvent != null)
			{
				foreach (Delegate delegate397 in this.OnServerUnlockContainerItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate397.Target, instance))
					{
						this.OnServerUnlockContainerItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UnlockContainerItemResult>)delegate397;
					}
				}
			}
			if (this.OnServerUpdateAvatarUrlRequestEvent != null)
			{
				foreach (Delegate delegate398 in this.OnServerUpdateAvatarUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate398.Target, instance))
					{
						this.OnServerUpdateAvatarUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateAvatarUrlRequest>)delegate398;
					}
				}
			}
			if (this.OnServerUpdateAvatarUrlResultEvent != null)
			{
				foreach (Delegate delegate399 in this.OnServerUpdateAvatarUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate399.Target, instance))
					{
						this.OnServerUpdateAvatarUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult>)delegate399;
					}
				}
			}
			if (this.OnServerUpdateBansRequestEvent != null)
			{
				foreach (Delegate delegate400 in this.OnServerUpdateBansRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate400.Target, instance))
					{
						this.OnServerUpdateBansRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateBansRequest>)delegate400;
					}
				}
			}
			if (this.OnServerUpdateBansResultEvent != null)
			{
				foreach (Delegate delegate401 in this.OnServerUpdateBansResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate401.Target, instance))
					{
						this.OnServerUpdateBansResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateBansResult>)delegate401;
					}
				}
			}
			if (this.OnServerUpdateCharacterDataRequestEvent != null)
			{
				foreach (Delegate delegate402 in this.OnServerUpdateCharacterDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate402.Target, instance))
					{
						this.OnServerUpdateCharacterDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateCharacterDataRequest>)delegate402;
					}
				}
			}
			if (this.OnServerUpdateCharacterDataResultEvent != null)
			{
				foreach (Delegate delegate403 in this.OnServerUpdateCharacterDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate403.Target, instance))
					{
						this.OnServerUpdateCharacterDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateCharacterDataResult>)delegate403;
					}
				}
			}
			if (this.OnServerUpdateCharacterInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate404 in this.OnServerUpdateCharacterInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate404.Target, instance))
					{
						this.OnServerUpdateCharacterInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateCharacterDataRequest>)delegate404;
					}
				}
			}
			if (this.OnServerUpdateCharacterInternalDataResultEvent != null)
			{
				foreach (Delegate delegate405 in this.OnServerUpdateCharacterInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate405.Target, instance))
					{
						this.OnServerUpdateCharacterInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateCharacterDataResult>)delegate405;
					}
				}
			}
			if (this.OnServerUpdateCharacterReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate406 in this.OnServerUpdateCharacterReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate406.Target, instance))
					{
						this.OnServerUpdateCharacterReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateCharacterDataRequest>)delegate406;
					}
				}
			}
			if (this.OnServerUpdateCharacterReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate407 in this.OnServerUpdateCharacterReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate407.Target, instance))
					{
						this.OnServerUpdateCharacterReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateCharacterDataResult>)delegate407;
					}
				}
			}
			if (this.OnServerUpdateCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate408 in this.OnServerUpdateCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate408.Target, instance))
					{
						this.OnServerUpdateCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateCharacterStatisticsRequest>)delegate408;
					}
				}
			}
			if (this.OnServerUpdateCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate409 in this.OnServerUpdateCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate409.Target, instance))
					{
						this.OnServerUpdateCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateCharacterStatisticsResult>)delegate409;
					}
				}
			}
			if (this.OnServerUpdatePlayerStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate410 in this.OnServerUpdatePlayerStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate410.Target, instance))
					{
						this.OnServerUpdatePlayerStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdatePlayerStatisticsRequest>)delegate410;
					}
				}
			}
			if (this.OnServerUpdatePlayerStatisticsResultEvent != null)
			{
				foreach (Delegate delegate411 in this.OnServerUpdatePlayerStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate411.Target, instance))
					{
						this.OnServerUpdatePlayerStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdatePlayerStatisticsResult>)delegate411;
					}
				}
			}
			if (this.OnServerUpdateSharedGroupDataRequestEvent != null)
			{
				foreach (Delegate delegate412 in this.OnServerUpdateSharedGroupDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate412.Target, instance))
					{
						this.OnServerUpdateSharedGroupDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateSharedGroupDataRequest>)delegate412;
					}
				}
			}
			if (this.OnServerUpdateSharedGroupDataResultEvent != null)
			{
				foreach (Delegate delegate413 in this.OnServerUpdateSharedGroupDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate413.Target, instance))
					{
						this.OnServerUpdateSharedGroupDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateSharedGroupDataResult>)delegate413;
					}
				}
			}
			if (this.OnServerUpdateUserDataRequestEvent != null)
			{
				foreach (Delegate delegate414 in this.OnServerUpdateUserDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate414.Target, instance))
					{
						this.OnServerUpdateUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserDataRequest>)delegate414;
					}
				}
			}
			if (this.OnServerUpdateUserDataResultEvent != null)
			{
				foreach (Delegate delegate415 in this.OnServerUpdateUserDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate415.Target, instance))
					{
						this.OnServerUpdateUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult>)delegate415;
					}
				}
			}
			if (this.OnServerUpdateUserInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate416 in this.OnServerUpdateUserInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate416.Target, instance))
					{
						this.OnServerUpdateUserInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserInternalDataRequest>)delegate416;
					}
				}
			}
			if (this.OnServerUpdateUserInternalDataResultEvent != null)
			{
				foreach (Delegate delegate417 in this.OnServerUpdateUserInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate417.Target, instance))
					{
						this.OnServerUpdateUserInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult>)delegate417;
					}
				}
			}
			if (this.OnServerUpdateUserInventoryItemCustomDataRequestEvent != null)
			{
				foreach (Delegate delegate418 in this.OnServerUpdateUserInventoryItemCustomDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate418.Target, instance))
					{
						this.OnServerUpdateUserInventoryItemCustomDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateUserInventoryItemDataRequest>)delegate418;
					}
				}
			}
			if (this.OnServerUpdateUserInventoryItemCustomDataResultEvent != null)
			{
				foreach (Delegate delegate419 in this.OnServerUpdateUserInventoryItemCustomDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate419.Target, instance))
					{
						this.OnServerUpdateUserInventoryItemCustomDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.EmptyResult>)delegate419;
					}
				}
			}
			if (this.OnServerUpdateUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate420 in this.OnServerUpdateUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate420.Target, instance))
					{
						this.OnServerUpdateUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserDataRequest>)delegate420;
					}
				}
			}
			if (this.OnServerUpdateUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate421 in this.OnServerUpdateUserPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate421.Target, instance))
					{
						this.OnServerUpdateUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult>)delegate421;
					}
				}
			}
			if (this.OnServerUpdateUserPublisherInternalDataRequestEvent != null)
			{
				foreach (Delegate delegate422 in this.OnServerUpdateUserPublisherInternalDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate422.Target, instance))
					{
						this.OnServerUpdateUserPublisherInternalDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserInternalDataRequest>)delegate422;
					}
				}
			}
			if (this.OnServerUpdateUserPublisherInternalDataResultEvent != null)
			{
				foreach (Delegate delegate423 in this.OnServerUpdateUserPublisherInternalDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate423.Target, instance))
					{
						this.OnServerUpdateUserPublisherInternalDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult>)delegate423;
					}
				}
			}
			if (this.OnServerUpdateUserPublisherReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate424 in this.OnServerUpdateUserPublisherReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate424.Target, instance))
					{
						this.OnServerUpdateUserPublisherReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserDataRequest>)delegate424;
					}
				}
			}
			if (this.OnServerUpdateUserPublisherReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate425 in this.OnServerUpdateUserPublisherReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate425.Target, instance))
					{
						this.OnServerUpdateUserPublisherReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult>)delegate425;
					}
				}
			}
			if (this.OnServerUpdateUserReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate426 in this.OnServerUpdateUserReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate426.Target, instance))
					{
						this.OnServerUpdateUserReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.UpdateUserDataRequest>)delegate426;
					}
				}
			}
			if (this.OnServerUpdateUserReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate427 in this.OnServerUpdateUserReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate427.Target, instance))
					{
						this.OnServerUpdateUserReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.UpdateUserDataResult>)delegate427;
					}
				}
			}
			if (this.OnServerWriteCharacterEventRequestEvent != null)
			{
				foreach (Delegate delegate428 in this.OnServerWriteCharacterEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate428.Target, instance))
					{
						this.OnServerWriteCharacterEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteServerCharacterEventRequest>)delegate428;
					}
				}
			}
			if (this.OnServerWriteCharacterEventResultEvent != null)
			{
				foreach (Delegate delegate429 in this.OnServerWriteCharacterEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate429.Target, instance))
					{
						this.OnServerWriteCharacterEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.WriteEventResponse>)delegate429;
					}
				}
			}
			if (this.OnServerWritePlayerEventRequestEvent != null)
			{
				foreach (Delegate delegate430 in this.OnServerWritePlayerEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate430.Target, instance))
					{
						this.OnServerWritePlayerEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteServerPlayerEventRequest>)delegate430;
					}
				}
			}
			if (this.OnServerWritePlayerEventResultEvent != null)
			{
				foreach (Delegate delegate431 in this.OnServerWritePlayerEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate431.Target, instance))
					{
						this.OnServerWritePlayerEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.WriteEventResponse>)delegate431;
					}
				}
			}
			if (this.OnServerWriteTitleEventRequestEvent != null)
			{
				foreach (Delegate delegate432 in this.OnServerWriteTitleEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate432.Target, instance))
					{
						this.OnServerWriteTitleEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ServerModels.WriteTitleEventRequest>)delegate432;
					}
				}
			}
			if (this.OnServerWriteTitleEventResultEvent != null)
			{
				foreach (Delegate delegate433 in this.OnServerWriteTitleEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate433.Target, instance))
					{
						this.OnServerWriteTitleEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ServerModels.WriteEventResponse>)delegate433;
					}
				}
			}
			if (this.OnAcceptTradeRequestEvent != null)
			{
				foreach (Delegate delegate434 in this.OnAcceptTradeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate434.Target, instance))
					{
						this.OnAcceptTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AcceptTradeRequest>)delegate434;
					}
				}
			}
			if (this.OnAcceptTradeResultEvent != null)
			{
				foreach (Delegate delegate435 in this.OnAcceptTradeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate435.Target, instance))
					{
						this.OnAcceptTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<AcceptTradeResponse>)delegate435;
					}
				}
			}
			if (this.OnAddFriendRequestEvent != null)
			{
				foreach (Delegate delegate436 in this.OnAddFriendRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate436.Target, instance))
					{
						this.OnAddFriendRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.AddFriendRequest>)delegate436;
					}
				}
			}
			if (this.OnAddFriendResultEvent != null)
			{
				foreach (Delegate delegate437 in this.OnAddFriendResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate437.Target, instance))
					{
						this.OnAddFriendResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddFriendResult>)delegate437;
					}
				}
			}
			if (this.OnAddGenericIDRequestEvent != null)
			{
				foreach (Delegate delegate438 in this.OnAddGenericIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate438.Target, instance))
					{
						this.OnAddGenericIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddGenericIDRequest>)delegate438;
					}
				}
			}
			if (this.OnAddGenericIDResultEvent != null)
			{
				foreach (Delegate delegate439 in this.OnAddGenericIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate439.Target, instance))
					{
						this.OnAddGenericIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddGenericIDResult>)delegate439;
					}
				}
			}
			if (this.OnAddOrUpdateContactEmailRequestEvent != null)
			{
				foreach (Delegate delegate440 in this.OnAddOrUpdateContactEmailRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate440.Target, instance))
					{
						this.OnAddOrUpdateContactEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddOrUpdateContactEmailRequest>)delegate440;
					}
				}
			}
			if (this.OnAddOrUpdateContactEmailResultEvent != null)
			{
				foreach (Delegate delegate441 in this.OnAddOrUpdateContactEmailResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate441.Target, instance))
					{
						this.OnAddOrUpdateContactEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddOrUpdateContactEmailResult>)delegate441;
					}
				}
			}
			if (this.OnAddSharedGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate442 in this.OnAddSharedGroupMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate442.Target, instance))
					{
						this.OnAddSharedGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.AddSharedGroupMembersRequest>)delegate442;
					}
				}
			}
			if (this.OnAddSharedGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate443 in this.OnAddSharedGroupMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate443.Target, instance))
					{
						this.OnAddSharedGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.AddSharedGroupMembersResult>)delegate443;
					}
				}
			}
			if (this.OnAddUsernamePasswordRequestEvent != null)
			{
				foreach (Delegate delegate444 in this.OnAddUsernamePasswordRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate444.Target, instance))
					{
						this.OnAddUsernamePasswordRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddUsernamePasswordRequest>)delegate444;
					}
				}
			}
			if (this.OnAddUsernamePasswordResultEvent != null)
			{
				foreach (Delegate delegate445 in this.OnAddUsernamePasswordResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate445.Target, instance))
					{
						this.OnAddUsernamePasswordResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddUsernamePasswordResult>)delegate445;
					}
				}
			}
			if (this.OnAddUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate446 in this.OnAddUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate446.Target, instance))
					{
						this.OnAddUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.AddUserVirtualCurrencyRequest>)delegate446;
					}
				}
			}
			if (this.OnAddUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate447 in this.OnAddUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate447.Target, instance))
					{
						this.OnAddUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ModifyUserVirtualCurrencyResult>)delegate447;
					}
				}
			}
			if (this.OnAndroidDevicePushNotificationRegistrationRequestEvent != null)
			{
				foreach (Delegate delegate448 in this.OnAndroidDevicePushNotificationRegistrationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate448.Target, instance))
					{
						this.OnAndroidDevicePushNotificationRegistrationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest>)delegate448;
					}
				}
			}
			if (this.OnAndroidDevicePushNotificationRegistrationResultEvent != null)
			{
				foreach (Delegate delegate449 in this.OnAndroidDevicePushNotificationRegistrationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate449.Target, instance))
					{
						this.OnAndroidDevicePushNotificationRegistrationResultEvent -= (PlayFabEvents.PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult>)delegate449;
					}
				}
			}
			if (this.OnAttributeInstallRequestEvent != null)
			{
				foreach (Delegate delegate450 in this.OnAttributeInstallRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate450.Target, instance))
					{
						this.OnAttributeInstallRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AttributeInstallRequest>)delegate450;
					}
				}
			}
			if (this.OnAttributeInstallResultEvent != null)
			{
				foreach (Delegate delegate451 in this.OnAttributeInstallResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate451.Target, instance))
					{
						this.OnAttributeInstallResultEvent -= (PlayFabEvents.PlayFabResultEvent<AttributeInstallResult>)delegate451;
					}
				}
			}
			if (this.OnCancelTradeRequestEvent != null)
			{
				foreach (Delegate delegate452 in this.OnCancelTradeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate452.Target, instance))
					{
						this.OnCancelTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CancelTradeRequest>)delegate452;
					}
				}
			}
			if (this.OnCancelTradeResultEvent != null)
			{
				foreach (Delegate delegate453 in this.OnCancelTradeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate453.Target, instance))
					{
						this.OnCancelTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<CancelTradeResponse>)delegate453;
					}
				}
			}
			if (this.OnConfirmPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate454 in this.OnConfirmPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate454.Target, instance))
					{
						this.OnConfirmPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConfirmPurchaseRequest>)delegate454;
					}
				}
			}
			if (this.OnConfirmPurchaseResultEvent != null)
			{
				foreach (Delegate delegate455 in this.OnConfirmPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate455.Target, instance))
					{
						this.OnConfirmPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConfirmPurchaseResult>)delegate455;
					}
				}
			}
			if (this.OnConsumeItemRequestEvent != null)
			{
				foreach (Delegate delegate456 in this.OnConsumeItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate456.Target, instance))
					{
						this.OnConsumeItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.ConsumeItemRequest>)delegate456;
					}
				}
			}
			if (this.OnConsumeItemResultEvent != null)
			{
				foreach (Delegate delegate457 in this.OnConsumeItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate457.Target, instance))
					{
						this.OnConsumeItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ConsumeItemResult>)delegate457;
					}
				}
			}
			if (this.OnCreateSharedGroupRequestEvent != null)
			{
				foreach (Delegate delegate458 in this.OnCreateSharedGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate458.Target, instance))
					{
						this.OnCreateSharedGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.CreateSharedGroupRequest>)delegate458;
					}
				}
			}
			if (this.OnCreateSharedGroupResultEvent != null)
			{
				foreach (Delegate delegate459 in this.OnCreateSharedGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate459.Target, instance))
					{
						this.OnCreateSharedGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.CreateSharedGroupResult>)delegate459;
					}
				}
			}
			if (this.OnExecuteCloudScriptRequestEvent != null)
			{
				foreach (Delegate delegate460 in this.OnExecuteCloudScriptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate460.Target, instance))
					{
						this.OnExecuteCloudScriptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ExecuteCloudScriptRequest>)delegate460;
					}
				}
			}
			if (this.OnExecuteCloudScriptResultEvent != null)
			{
				foreach (Delegate delegate461 in this.OnExecuteCloudScriptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate461.Target, instance))
					{
						this.OnExecuteCloudScriptResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult>)delegate461;
					}
				}
			}
			if (this.OnGetAccountInfoRequestEvent != null)
			{
				foreach (Delegate delegate462 in this.OnGetAccountInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate462.Target, instance))
					{
						this.OnGetAccountInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetAccountInfoRequest>)delegate462;
					}
				}
			}
			if (this.OnGetAccountInfoResultEvent != null)
			{
				foreach (Delegate delegate463 in this.OnGetAccountInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate463.Target, instance))
					{
						this.OnGetAccountInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetAccountInfoResult>)delegate463;
					}
				}
			}
			if (this.OnGetAllUsersCharactersRequestEvent != null)
			{
				foreach (Delegate delegate464 in this.OnGetAllUsersCharactersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate464.Target, instance))
					{
						this.OnGetAllUsersCharactersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.ListUsersCharactersRequest>)delegate464;
					}
				}
			}
			if (this.OnGetAllUsersCharactersResultEvent != null)
			{
				foreach (Delegate delegate465 in this.OnGetAllUsersCharactersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate465.Target, instance))
					{
						this.OnGetAllUsersCharactersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ListUsersCharactersResult>)delegate465;
					}
				}
			}
			if (this.OnGetCatalogItemsRequestEvent != null)
			{
				foreach (Delegate delegate466 in this.OnGetCatalogItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate466.Target, instance))
					{
						this.OnGetCatalogItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCatalogItemsRequest>)delegate466;
					}
				}
			}
			if (this.OnGetCatalogItemsResultEvent != null)
			{
				foreach (Delegate delegate467 in this.OnGetCatalogItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate467.Target, instance))
					{
						this.OnGetCatalogItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCatalogItemsResult>)delegate467;
					}
				}
			}
			if (this.OnGetCharacterDataRequestEvent != null)
			{
				foreach (Delegate delegate468 in this.OnGetCharacterDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate468.Target, instance))
					{
						this.OnGetCharacterDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterDataRequest>)delegate468;
					}
				}
			}
			if (this.OnGetCharacterDataResultEvent != null)
			{
				foreach (Delegate delegate469 in this.OnGetCharacterDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate469.Target, instance))
					{
						this.OnGetCharacterDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterDataResult>)delegate469;
					}
				}
			}
			if (this.OnGetCharacterInventoryRequestEvent != null)
			{
				foreach (Delegate delegate470 in this.OnGetCharacterInventoryRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate470.Target, instance))
					{
						this.OnGetCharacterInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterInventoryRequest>)delegate470;
					}
				}
			}
			if (this.OnGetCharacterInventoryResultEvent != null)
			{
				foreach (Delegate delegate471 in this.OnGetCharacterInventoryResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate471.Target, instance))
					{
						this.OnGetCharacterInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterInventoryResult>)delegate471;
					}
				}
			}
			if (this.OnGetCharacterLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate472 in this.OnGetCharacterLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate472.Target, instance))
					{
						this.OnGetCharacterLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterLeaderboardRequest>)delegate472;
					}
				}
			}
			if (this.OnGetCharacterLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate473 in this.OnGetCharacterLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate473.Target, instance))
					{
						this.OnGetCharacterLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterLeaderboardResult>)delegate473;
					}
				}
			}
			if (this.OnGetCharacterReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate474 in this.OnGetCharacterReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate474.Target, instance))
					{
						this.OnGetCharacterReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterDataRequest>)delegate474;
					}
				}
			}
			if (this.OnGetCharacterReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate475 in this.OnGetCharacterReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate475.Target, instance))
					{
						this.OnGetCharacterReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterDataResult>)delegate475;
					}
				}
			}
			if (this.OnGetCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate476 in this.OnGetCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate476.Target, instance))
					{
						this.OnGetCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetCharacterStatisticsRequest>)delegate476;
					}
				}
			}
			if (this.OnGetCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate477 in this.OnGetCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate477.Target, instance))
					{
						this.OnGetCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetCharacterStatisticsResult>)delegate477;
					}
				}
			}
			if (this.OnGetContentDownloadUrlRequestEvent != null)
			{
				foreach (Delegate delegate478 in this.OnGetContentDownloadUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate478.Target, instance))
					{
						this.OnGetContentDownloadUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetContentDownloadUrlRequest>)delegate478;
					}
				}
			}
			if (this.OnGetContentDownloadUrlResultEvent != null)
			{
				foreach (Delegate delegate479 in this.OnGetContentDownloadUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate479.Target, instance))
					{
						this.OnGetContentDownloadUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetContentDownloadUrlResult>)delegate479;
					}
				}
			}
			if (this.OnGetCurrentGamesRequestEvent != null)
			{
				foreach (Delegate delegate480 in this.OnGetCurrentGamesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate480.Target, instance))
					{
						this.OnGetCurrentGamesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CurrentGamesRequest>)delegate480;
					}
				}
			}
			if (this.OnGetCurrentGamesResultEvent != null)
			{
				foreach (Delegate delegate481 in this.OnGetCurrentGamesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate481.Target, instance))
					{
						this.OnGetCurrentGamesResultEvent -= (PlayFabEvents.PlayFabResultEvent<CurrentGamesResult>)delegate481;
					}
				}
			}
			if (this.OnGetFriendLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate482 in this.OnGetFriendLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate482.Target, instance))
					{
						this.OnGetFriendLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetFriendLeaderboardRequest>)delegate482;
					}
				}
			}
			if (this.OnGetFriendLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate483 in this.OnGetFriendLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate483.Target, instance))
					{
						this.OnGetFriendLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetLeaderboardResult>)delegate483;
					}
				}
			}
			if (this.OnGetFriendLeaderboardAroundPlayerRequestEvent != null)
			{
				foreach (Delegate delegate484 in this.OnGetFriendLeaderboardAroundPlayerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate484.Target, instance))
					{
						this.OnGetFriendLeaderboardAroundPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest>)delegate484;
					}
				}
			}
			if (this.OnGetFriendLeaderboardAroundPlayerResultEvent != null)
			{
				foreach (Delegate delegate485 in this.OnGetFriendLeaderboardAroundPlayerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate485.Target, instance))
					{
						this.OnGetFriendLeaderboardAroundPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult>)delegate485;
					}
				}
			}
			if (this.OnGetFriendsListRequestEvent != null)
			{
				foreach (Delegate delegate486 in this.OnGetFriendsListRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate486.Target, instance))
					{
						this.OnGetFriendsListRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetFriendsListRequest>)delegate486;
					}
				}
			}
			if (this.OnGetFriendsListResultEvent != null)
			{
				foreach (Delegate delegate487 in this.OnGetFriendsListResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate487.Target, instance))
					{
						this.OnGetFriendsListResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetFriendsListResult>)delegate487;
					}
				}
			}
			if (this.OnGetGameServerRegionsRequestEvent != null)
			{
				foreach (Delegate delegate488 in this.OnGetGameServerRegionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate488.Target, instance))
					{
						this.OnGetGameServerRegionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GameServerRegionsRequest>)delegate488;
					}
				}
			}
			if (this.OnGetGameServerRegionsResultEvent != null)
			{
				foreach (Delegate delegate489 in this.OnGetGameServerRegionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate489.Target, instance))
					{
						this.OnGetGameServerRegionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GameServerRegionsResult>)delegate489;
					}
				}
			}
			if (this.OnGetLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate490 in this.OnGetLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate490.Target, instance))
					{
						this.OnGetLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetLeaderboardRequest>)delegate490;
					}
				}
			}
			if (this.OnGetLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate491 in this.OnGetLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate491.Target, instance))
					{
						this.OnGetLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetLeaderboardResult>)delegate491;
					}
				}
			}
			if (this.OnGetLeaderboardAroundCharacterRequestEvent != null)
			{
				foreach (Delegate delegate492 in this.OnGetLeaderboardAroundCharacterRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate492.Target, instance))
					{
						this.OnGetLeaderboardAroundCharacterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetLeaderboardAroundCharacterRequest>)delegate492;
					}
				}
			}
			if (this.OnGetLeaderboardAroundCharacterResultEvent != null)
			{
				foreach (Delegate delegate493 in this.OnGetLeaderboardAroundCharacterResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate493.Target, instance))
					{
						this.OnGetLeaderboardAroundCharacterResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetLeaderboardAroundCharacterResult>)delegate493;
					}
				}
			}
			if (this.OnGetLeaderboardAroundPlayerRequestEvent != null)
			{
				foreach (Delegate delegate494 in this.OnGetLeaderboardAroundPlayerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate494.Target, instance))
					{
						this.OnGetLeaderboardAroundPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest>)delegate494;
					}
				}
			}
			if (this.OnGetLeaderboardAroundPlayerResultEvent != null)
			{
				foreach (Delegate delegate495 in this.OnGetLeaderboardAroundPlayerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate495.Target, instance))
					{
						this.OnGetLeaderboardAroundPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundPlayerResult>)delegate495;
					}
				}
			}
			if (this.OnGetLeaderboardForUserCharactersRequestEvent != null)
			{
				foreach (Delegate delegate496 in this.OnGetLeaderboardForUserCharactersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate496.Target, instance))
					{
						this.OnGetLeaderboardForUserCharactersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetLeaderboardForUsersCharactersRequest>)delegate496;
					}
				}
			}
			if (this.OnGetLeaderboardForUserCharactersResultEvent != null)
			{
				foreach (Delegate delegate497 in this.OnGetLeaderboardForUserCharactersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate497.Target, instance))
					{
						this.OnGetLeaderboardForUserCharactersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetLeaderboardForUsersCharactersResult>)delegate497;
					}
				}
			}
			if (this.OnGetPaymentTokenRequestEvent != null)
			{
				foreach (Delegate delegate498 in this.OnGetPaymentTokenRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate498.Target, instance))
					{
						this.OnGetPaymentTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPaymentTokenRequest>)delegate498;
					}
				}
			}
			if (this.OnGetPaymentTokenResultEvent != null)
			{
				foreach (Delegate delegate499 in this.OnGetPaymentTokenResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate499.Target, instance))
					{
						this.OnGetPaymentTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPaymentTokenResult>)delegate499;
					}
				}
			}
			if (this.OnGetPhotonAuthenticationTokenRequestEvent != null)
			{
				foreach (Delegate delegate500 in this.OnGetPhotonAuthenticationTokenRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate500.Target, instance))
					{
						this.OnGetPhotonAuthenticationTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest>)delegate500;
					}
				}
			}
			if (this.OnGetPhotonAuthenticationTokenResultEvent != null)
			{
				foreach (Delegate delegate501 in this.OnGetPhotonAuthenticationTokenResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate501.Target, instance))
					{
						this.OnGetPhotonAuthenticationTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPhotonAuthenticationTokenResult>)delegate501;
					}
				}
			}
			if (this.OnGetPlayerCombinedInfoRequestEvent != null)
			{
				foreach (Delegate delegate502 in this.OnGetPlayerCombinedInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate502.Target, instance))
					{
						this.OnGetPlayerCombinedInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerCombinedInfoRequest>)delegate502;
					}
				}
			}
			if (this.OnGetPlayerCombinedInfoResultEvent != null)
			{
				foreach (Delegate delegate503 in this.OnGetPlayerCombinedInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate503.Target, instance))
					{
						this.OnGetPlayerCombinedInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerCombinedInfoResult>)delegate503;
					}
				}
			}
			if (this.OnGetPlayerProfileRequestEvent != null)
			{
				foreach (Delegate delegate504 in this.OnGetPlayerProfileRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate504.Target, instance))
					{
						this.OnGetPlayerProfileRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerProfileRequest>)delegate504;
					}
				}
			}
			if (this.OnGetPlayerProfileResultEvent != null)
			{
				foreach (Delegate delegate505 in this.OnGetPlayerProfileResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate505.Target, instance))
					{
						this.OnGetPlayerProfileResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerProfileResult>)delegate505;
					}
				}
			}
			if (this.OnGetPlayerSegmentsRequestEvent != null)
			{
				foreach (Delegate delegate506 in this.OnGetPlayerSegmentsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate506.Target, instance))
					{
						this.OnGetPlayerSegmentsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerSegmentsRequest>)delegate506;
					}
				}
			}
			if (this.OnGetPlayerSegmentsResultEvent != null)
			{
				foreach (Delegate delegate507 in this.OnGetPlayerSegmentsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate507.Target, instance))
					{
						this.OnGetPlayerSegmentsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerSegmentsResult>)delegate507;
					}
				}
			}
			if (this.OnGetPlayerStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate508 in this.OnGetPlayerStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate508.Target, instance))
					{
						this.OnGetPlayerStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerStatisticsRequest>)delegate508;
					}
				}
			}
			if (this.OnGetPlayerStatisticsResultEvent != null)
			{
				foreach (Delegate delegate509 in this.OnGetPlayerStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate509.Target, instance))
					{
						this.OnGetPlayerStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerStatisticsResult>)delegate509;
					}
				}
			}
			if (this.OnGetPlayerStatisticVersionsRequestEvent != null)
			{
				foreach (Delegate delegate510 in this.OnGetPlayerStatisticVersionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate510.Target, instance))
					{
						this.OnGetPlayerStatisticVersionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerStatisticVersionsRequest>)delegate510;
					}
				}
			}
			if (this.OnGetPlayerStatisticVersionsResultEvent != null)
			{
				foreach (Delegate delegate511 in this.OnGetPlayerStatisticVersionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate511.Target, instance))
					{
						this.OnGetPlayerStatisticVersionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerStatisticVersionsResult>)delegate511;
					}
				}
			}
			if (this.OnGetPlayerTagsRequestEvent != null)
			{
				foreach (Delegate delegate512 in this.OnGetPlayerTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate512.Target, instance))
					{
						this.OnGetPlayerTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayerTagsRequest>)delegate512;
					}
				}
			}
			if (this.OnGetPlayerTagsResultEvent != null)
			{
				foreach (Delegate delegate513 in this.OnGetPlayerTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate513.Target, instance))
					{
						this.OnGetPlayerTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayerTagsResult>)delegate513;
					}
				}
			}
			if (this.OnGetPlayerTradesRequestEvent != null)
			{
				foreach (Delegate delegate514 in this.OnGetPlayerTradesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate514.Target, instance))
					{
						this.OnGetPlayerTradesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerTradesRequest>)delegate514;
					}
				}
			}
			if (this.OnGetPlayerTradesResultEvent != null)
			{
				foreach (Delegate delegate515 in this.OnGetPlayerTradesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate515.Target, instance))
					{
						this.OnGetPlayerTradesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerTradesResponse>)delegate515;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookIDsRequestEvent != null)
			{
				foreach (Delegate delegate516 in this.OnGetPlayFabIDsFromFacebookIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate516.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayFabIDsFromFacebookIDsRequest>)delegate516;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookIDsResultEvent != null)
			{
				foreach (Delegate delegate517 in this.OnGetPlayFabIDsFromFacebookIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate517.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayFabIDsFromFacebookIDsResult>)delegate517;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent != null)
			{
				foreach (Delegate delegate518 in this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate518.Target, instance))
					{
						this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest>)delegate518;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGameCenterIDsResultEvent != null)
			{
				foreach (Delegate delegate519 in this.OnGetPlayFabIDsFromGameCenterIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate519.Target, instance))
					{
						this.OnGetPlayFabIDsFromGameCenterIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult>)delegate519;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGenericIDsRequestEvent != null)
			{
				foreach (Delegate delegate520 in this.OnGetPlayFabIDsFromGenericIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate520.Target, instance))
					{
						this.OnGetPlayFabIDsFromGenericIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest>)delegate520;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGenericIDsResultEvent != null)
			{
				foreach (Delegate delegate521 in this.OnGetPlayFabIDsFromGenericIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate521.Target, instance))
					{
						this.OnGetPlayFabIDsFromGenericIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult>)delegate521;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGoogleIDsRequestEvent != null)
			{
				foreach (Delegate delegate522 in this.OnGetPlayFabIDsFromGoogleIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate522.Target, instance))
					{
						this.OnGetPlayFabIDsFromGoogleIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest>)delegate522;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGoogleIDsResultEvent != null)
			{
				foreach (Delegate delegate523 in this.OnGetPlayFabIDsFromGoogleIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate523.Target, instance))
					{
						this.OnGetPlayFabIDsFromGoogleIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult>)delegate523;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromKongregateIDsRequestEvent != null)
			{
				foreach (Delegate delegate524 in this.OnGetPlayFabIDsFromKongregateIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate524.Target, instance))
					{
						this.OnGetPlayFabIDsFromKongregateIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest>)delegate524;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromKongregateIDsResultEvent != null)
			{
				foreach (Delegate delegate525 in this.OnGetPlayFabIDsFromKongregateIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate525.Target, instance))
					{
						this.OnGetPlayFabIDsFromKongregateIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult>)delegate525;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromSteamIDsRequestEvent != null)
			{
				foreach (Delegate delegate526 in this.OnGetPlayFabIDsFromSteamIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate526.Target, instance))
					{
						this.OnGetPlayFabIDsFromSteamIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPlayFabIDsFromSteamIDsRequest>)delegate526;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromSteamIDsResultEvent != null)
			{
				foreach (Delegate delegate527 in this.OnGetPlayFabIDsFromSteamIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate527.Target, instance))
					{
						this.OnGetPlayFabIDsFromSteamIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPlayFabIDsFromSteamIDsResult>)delegate527;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromTwitchIDsRequestEvent != null)
			{
				foreach (Delegate delegate528 in this.OnGetPlayFabIDsFromTwitchIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate528.Target, instance))
					{
						this.OnGetPlayFabIDsFromTwitchIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest>)delegate528;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromTwitchIDsResultEvent != null)
			{
				foreach (Delegate delegate529 in this.OnGetPlayFabIDsFromTwitchIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate529.Target, instance))
					{
						this.OnGetPlayFabIDsFromTwitchIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult>)delegate529;
					}
				}
			}
			if (this.OnGetPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate530 in this.OnGetPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate530.Target, instance))
					{
						this.OnGetPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetPublisherDataRequest>)delegate530;
					}
				}
			}
			if (this.OnGetPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate531 in this.OnGetPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate531.Target, instance))
					{
						this.OnGetPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetPublisherDataResult>)delegate531;
					}
				}
			}
			if (this.OnGetPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate532 in this.OnGetPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate532.Target, instance))
					{
						this.OnGetPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPurchaseRequest>)delegate532;
					}
				}
			}
			if (this.OnGetPurchaseResultEvent != null)
			{
				foreach (Delegate delegate533 in this.OnGetPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate533.Target, instance))
					{
						this.OnGetPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPurchaseResult>)delegate533;
					}
				}
			}
			if (this.OnGetSharedGroupDataRequestEvent != null)
			{
				foreach (Delegate delegate534 in this.OnGetSharedGroupDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate534.Target, instance))
					{
						this.OnGetSharedGroupDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetSharedGroupDataRequest>)delegate534;
					}
				}
			}
			if (this.OnGetSharedGroupDataResultEvent != null)
			{
				foreach (Delegate delegate535 in this.OnGetSharedGroupDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate535.Target, instance))
					{
						this.OnGetSharedGroupDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetSharedGroupDataResult>)delegate535;
					}
				}
			}
			if (this.OnGetStoreItemsRequestEvent != null)
			{
				foreach (Delegate delegate536 in this.OnGetStoreItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate536.Target, instance))
					{
						this.OnGetStoreItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetStoreItemsRequest>)delegate536;
					}
				}
			}
			if (this.OnGetStoreItemsResultEvent != null)
			{
				foreach (Delegate delegate537 in this.OnGetStoreItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate537.Target, instance))
					{
						this.OnGetStoreItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetStoreItemsResult>)delegate537;
					}
				}
			}
			if (this.OnGetTimeRequestEvent != null)
			{
				foreach (Delegate delegate538 in this.OnGetTimeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate538.Target, instance))
					{
						this.OnGetTimeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetTimeRequest>)delegate538;
					}
				}
			}
			if (this.OnGetTimeResultEvent != null)
			{
				foreach (Delegate delegate539 in this.OnGetTimeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate539.Target, instance))
					{
						this.OnGetTimeResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetTimeResult>)delegate539;
					}
				}
			}
			if (this.OnGetTitleDataRequestEvent != null)
			{
				foreach (Delegate delegate540 in this.OnGetTitleDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate540.Target, instance))
					{
						this.OnGetTitleDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetTitleDataRequest>)delegate540;
					}
				}
			}
			if (this.OnGetTitleDataResultEvent != null)
			{
				foreach (Delegate delegate541 in this.OnGetTitleDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate541.Target, instance))
					{
						this.OnGetTitleDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetTitleDataResult>)delegate541;
					}
				}
			}
			if (this.OnGetTitleNewsRequestEvent != null)
			{
				foreach (Delegate delegate542 in this.OnGetTitleNewsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate542.Target, instance))
					{
						this.OnGetTitleNewsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetTitleNewsRequest>)delegate542;
					}
				}
			}
			if (this.OnGetTitleNewsResultEvent != null)
			{
				foreach (Delegate delegate543 in this.OnGetTitleNewsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate543.Target, instance))
					{
						this.OnGetTitleNewsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetTitleNewsResult>)delegate543;
					}
				}
			}
			if (this.OnGetTitlePublicKeyRequestEvent != null)
			{
				foreach (Delegate delegate544 in this.OnGetTitlePublicKeyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate544.Target, instance))
					{
						this.OnGetTitlePublicKeyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitlePublicKeyRequest>)delegate544;
					}
				}
			}
			if (this.OnGetTitlePublicKeyResultEvent != null)
			{
				foreach (Delegate delegate545 in this.OnGetTitlePublicKeyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate545.Target, instance))
					{
						this.OnGetTitlePublicKeyResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitlePublicKeyResult>)delegate545;
					}
				}
			}
			if (this.OnGetTradeStatusRequestEvent != null)
			{
				foreach (Delegate delegate546 in this.OnGetTradeStatusRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate546.Target, instance))
					{
						this.OnGetTradeStatusRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTradeStatusRequest>)delegate546;
					}
				}
			}
			if (this.OnGetTradeStatusResultEvent != null)
			{
				foreach (Delegate delegate547 in this.OnGetTradeStatusResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate547.Target, instance))
					{
						this.OnGetTradeStatusResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTradeStatusResponse>)delegate547;
					}
				}
			}
			if (this.OnGetUserDataRequestEvent != null)
			{
				foreach (Delegate delegate548 in this.OnGetUserDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate548.Target, instance))
					{
						this.OnGetUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserDataRequest>)delegate548;
					}
				}
			}
			if (this.OnGetUserDataResultEvent != null)
			{
				foreach (Delegate delegate549 in this.OnGetUserDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate549.Target, instance))
					{
						this.OnGetUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserDataResult>)delegate549;
					}
				}
			}
			if (this.OnGetUserInventoryRequestEvent != null)
			{
				foreach (Delegate delegate550 in this.OnGetUserInventoryRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate550.Target, instance))
					{
						this.OnGetUserInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserInventoryRequest>)delegate550;
					}
				}
			}
			if (this.OnGetUserInventoryResultEvent != null)
			{
				foreach (Delegate delegate551 in this.OnGetUserInventoryResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate551.Target, instance))
					{
						this.OnGetUserInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserInventoryResult>)delegate551;
					}
				}
			}
			if (this.OnGetUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate552 in this.OnGetUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate552.Target, instance))
					{
						this.OnGetUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserDataRequest>)delegate552;
					}
				}
			}
			if (this.OnGetUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate553 in this.OnGetUserPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate553.Target, instance))
					{
						this.OnGetUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserDataResult>)delegate553;
					}
				}
			}
			if (this.OnGetUserPublisherReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate554 in this.OnGetUserPublisherReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate554.Target, instance))
					{
						this.OnGetUserPublisherReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserDataRequest>)delegate554;
					}
				}
			}
			if (this.OnGetUserPublisherReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate555 in this.OnGetUserPublisherReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate555.Target, instance))
					{
						this.OnGetUserPublisherReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserDataResult>)delegate555;
					}
				}
			}
			if (this.OnGetUserReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate556 in this.OnGetUserReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate556.Target, instance))
					{
						this.OnGetUserReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GetUserDataRequest>)delegate556;
					}
				}
			}
			if (this.OnGetUserReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate557 in this.OnGetUserReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate557.Target, instance))
					{
						this.OnGetUserReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GetUserDataResult>)delegate557;
					}
				}
			}
			if (this.OnGetWindowsHelloChallengeRequestEvent != null)
			{
				foreach (Delegate delegate558 in this.OnGetWindowsHelloChallengeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate558.Target, instance))
					{
						this.OnGetWindowsHelloChallengeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetWindowsHelloChallengeRequest>)delegate558;
					}
				}
			}
			if (this.OnGetWindowsHelloChallengeResultEvent != null)
			{
				foreach (Delegate delegate559 in this.OnGetWindowsHelloChallengeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate559.Target, instance))
					{
						this.OnGetWindowsHelloChallengeResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetWindowsHelloChallengeResponse>)delegate559;
					}
				}
			}
			if (this.OnGrantCharacterToUserRequestEvent != null)
			{
				foreach (Delegate delegate560 in this.OnGrantCharacterToUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate560.Target, instance))
					{
						this.OnGrantCharacterToUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.GrantCharacterToUserRequest>)delegate560;
					}
				}
			}
			if (this.OnGrantCharacterToUserResultEvent != null)
			{
				foreach (Delegate delegate561 in this.OnGrantCharacterToUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate561.Target, instance))
					{
						this.OnGrantCharacterToUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.GrantCharacterToUserResult>)delegate561;
					}
				}
			}
			if (this.OnLinkAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate562 in this.OnLinkAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate562.Target, instance))
					{
						this.OnLinkAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkAndroidDeviceIDRequest>)delegate562;
					}
				}
			}
			if (this.OnLinkAndroidDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate563 in this.OnLinkAndroidDeviceIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate563.Target, instance))
					{
						this.OnLinkAndroidDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkAndroidDeviceIDResult>)delegate563;
					}
				}
			}
			if (this.OnLinkCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate564 in this.OnLinkCustomIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate564.Target, instance))
					{
						this.OnLinkCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkCustomIDRequest>)delegate564;
					}
				}
			}
			if (this.OnLinkCustomIDResultEvent != null)
			{
				foreach (Delegate delegate565 in this.OnLinkCustomIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate565.Target, instance))
					{
						this.OnLinkCustomIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkCustomIDResult>)delegate565;
					}
				}
			}
			if (this.OnLinkFacebookAccountRequestEvent != null)
			{
				foreach (Delegate delegate566 in this.OnLinkFacebookAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate566.Target, instance))
					{
						this.OnLinkFacebookAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkFacebookAccountRequest>)delegate566;
					}
				}
			}
			if (this.OnLinkFacebookAccountResultEvent != null)
			{
				foreach (Delegate delegate567 in this.OnLinkFacebookAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate567.Target, instance))
					{
						this.OnLinkFacebookAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkFacebookAccountResult>)delegate567;
					}
				}
			}
			if (this.OnLinkGameCenterAccountRequestEvent != null)
			{
				foreach (Delegate delegate568 in this.OnLinkGameCenterAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate568.Target, instance))
					{
						this.OnLinkGameCenterAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkGameCenterAccountRequest>)delegate568;
					}
				}
			}
			if (this.OnLinkGameCenterAccountResultEvent != null)
			{
				foreach (Delegate delegate569 in this.OnLinkGameCenterAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate569.Target, instance))
					{
						this.OnLinkGameCenterAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkGameCenterAccountResult>)delegate569;
					}
				}
			}
			if (this.OnLinkGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate570 in this.OnLinkGoogleAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate570.Target, instance))
					{
						this.OnLinkGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkGoogleAccountRequest>)delegate570;
					}
				}
			}
			if (this.OnLinkGoogleAccountResultEvent != null)
			{
				foreach (Delegate delegate571 in this.OnLinkGoogleAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate571.Target, instance))
					{
						this.OnLinkGoogleAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkGoogleAccountResult>)delegate571;
					}
				}
			}
			if (this.OnLinkIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate572 in this.OnLinkIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate572.Target, instance))
					{
						this.OnLinkIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkIOSDeviceIDRequest>)delegate572;
					}
				}
			}
			if (this.OnLinkIOSDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate573 in this.OnLinkIOSDeviceIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate573.Target, instance))
					{
						this.OnLinkIOSDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkIOSDeviceIDResult>)delegate573;
					}
				}
			}
			if (this.OnLinkKongregateRequestEvent != null)
			{
				foreach (Delegate delegate574 in this.OnLinkKongregateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate574.Target, instance))
					{
						this.OnLinkKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkKongregateAccountRequest>)delegate574;
					}
				}
			}
			if (this.OnLinkKongregateResultEvent != null)
			{
				foreach (Delegate delegate575 in this.OnLinkKongregateResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate575.Target, instance))
					{
						this.OnLinkKongregateResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkKongregateAccountResult>)delegate575;
					}
				}
			}
			if (this.OnLinkSteamAccountRequestEvent != null)
			{
				foreach (Delegate delegate576 in this.OnLinkSteamAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate576.Target, instance))
					{
						this.OnLinkSteamAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkSteamAccountRequest>)delegate576;
					}
				}
			}
			if (this.OnLinkSteamAccountResultEvent != null)
			{
				foreach (Delegate delegate577 in this.OnLinkSteamAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate577.Target, instance))
					{
						this.OnLinkSteamAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkSteamAccountResult>)delegate577;
					}
				}
			}
			if (this.OnLinkTwitchRequestEvent != null)
			{
				foreach (Delegate delegate578 in this.OnLinkTwitchRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate578.Target, instance))
					{
						this.OnLinkTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkTwitchAccountRequest>)delegate578;
					}
				}
			}
			if (this.OnLinkTwitchResultEvent != null)
			{
				foreach (Delegate delegate579 in this.OnLinkTwitchResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate579.Target, instance))
					{
						this.OnLinkTwitchResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkTwitchAccountResult>)delegate579;
					}
				}
			}
			if (this.OnLinkWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate580 in this.OnLinkWindowsHelloRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate580.Target, instance))
					{
						this.OnLinkWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkWindowsHelloAccountRequest>)delegate580;
					}
				}
			}
			if (this.OnLinkWindowsHelloResultEvent != null)
			{
				foreach (Delegate delegate581 in this.OnLinkWindowsHelloResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate581.Target, instance))
					{
						this.OnLinkWindowsHelloResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkWindowsHelloAccountResponse>)delegate581;
					}
				}
			}
			if (this.OnLoginWithAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate582 in this.OnLoginWithAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate582.Target, instance))
					{
						this.OnLoginWithAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest>)delegate582;
					}
				}
			}
			if (this.OnLoginWithCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate583 in this.OnLoginWithCustomIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate583.Target, instance))
					{
						this.OnLoginWithCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithCustomIDRequest>)delegate583;
					}
				}
			}
			if (this.OnLoginWithEmailAddressRequestEvent != null)
			{
				foreach (Delegate delegate584 in this.OnLoginWithEmailAddressRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate584.Target, instance))
					{
						this.OnLoginWithEmailAddressRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithEmailAddressRequest>)delegate584;
					}
				}
			}
			if (this.OnLoginWithFacebookRequestEvent != null)
			{
				foreach (Delegate delegate585 in this.OnLoginWithFacebookRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate585.Target, instance))
					{
						this.OnLoginWithFacebookRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookRequest>)delegate585;
					}
				}
			}
			if (this.OnLoginWithGameCenterRequestEvent != null)
			{
				foreach (Delegate delegate586 in this.OnLoginWithGameCenterRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate586.Target, instance))
					{
						this.OnLoginWithGameCenterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithGameCenterRequest>)delegate586;
					}
				}
			}
			if (this.OnLoginWithGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate587 in this.OnLoginWithGoogleAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate587.Target, instance))
					{
						this.OnLoginWithGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithGoogleAccountRequest>)delegate587;
					}
				}
			}
			if (this.OnLoginWithIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate588 in this.OnLoginWithIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate588.Target, instance))
					{
						this.OnLoginWithIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithIOSDeviceIDRequest>)delegate588;
					}
				}
			}
			if (this.OnLoginWithKongregateRequestEvent != null)
			{
				foreach (Delegate delegate589 in this.OnLoginWithKongregateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate589.Target, instance))
					{
						this.OnLoginWithKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithKongregateRequest>)delegate589;
					}
				}
			}
			if (this.OnLoginWithPlayFabRequestEvent != null)
			{
				foreach (Delegate delegate590 in this.OnLoginWithPlayFabRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate590.Target, instance))
					{
						this.OnLoginWithPlayFabRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithPlayFabRequest>)delegate590;
					}
				}
			}
			if (this.OnLoginWithSteamRequestEvent != null)
			{
				foreach (Delegate delegate591 in this.OnLoginWithSteamRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate591.Target, instance))
					{
						this.OnLoginWithSteamRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithSteamRequest>)delegate591;
					}
				}
			}
			if (this.OnLoginWithTwitchRequestEvent != null)
			{
				foreach (Delegate delegate592 in this.OnLoginWithTwitchRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate592.Target, instance))
					{
						this.OnLoginWithTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithTwitchRequest>)delegate592;
					}
				}
			}
			if (this.OnLoginWithWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate593 in this.OnLoginWithWindowsHelloRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate593.Target, instance))
					{
						this.OnLoginWithWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithWindowsHelloRequest>)delegate593;
					}
				}
			}
			if (this.OnMatchmakeRequestEvent != null)
			{
				foreach (Delegate delegate594 in this.OnMatchmakeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate594.Target, instance))
					{
						this.OnMatchmakeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<MatchmakeRequest>)delegate594;
					}
				}
			}
			if (this.OnMatchmakeResultEvent != null)
			{
				foreach (Delegate delegate595 in this.OnMatchmakeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate595.Target, instance))
					{
						this.OnMatchmakeResultEvent -= (PlayFabEvents.PlayFabResultEvent<MatchmakeResult>)delegate595;
					}
				}
			}
			if (this.OnOpenTradeRequestEvent != null)
			{
				foreach (Delegate delegate596 in this.OnOpenTradeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate596.Target, instance))
					{
						this.OnOpenTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<OpenTradeRequest>)delegate596;
					}
				}
			}
			if (this.OnOpenTradeResultEvent != null)
			{
				foreach (Delegate delegate597 in this.OnOpenTradeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate597.Target, instance))
					{
						this.OnOpenTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<OpenTradeResponse>)delegate597;
					}
				}
			}
			if (this.OnPayForPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate598 in this.OnPayForPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate598.Target, instance))
					{
						this.OnPayForPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PayForPurchaseRequest>)delegate598;
					}
				}
			}
			if (this.OnPayForPurchaseResultEvent != null)
			{
				foreach (Delegate delegate599 in this.OnPayForPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate599.Target, instance))
					{
						this.OnPayForPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<PayForPurchaseResult>)delegate599;
					}
				}
			}
			if (this.OnPurchaseItemRequestEvent != null)
			{
				foreach (Delegate delegate600 in this.OnPurchaseItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate600.Target, instance))
					{
						this.OnPurchaseItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PurchaseItemRequest>)delegate600;
					}
				}
			}
			if (this.OnPurchaseItemResultEvent != null)
			{
				foreach (Delegate delegate601 in this.OnPurchaseItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate601.Target, instance))
					{
						this.OnPurchaseItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PurchaseItemResult>)delegate601;
					}
				}
			}
			if (this.OnRedeemCouponRequestEvent != null)
			{
				foreach (Delegate delegate602 in this.OnRedeemCouponRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate602.Target, instance))
					{
						this.OnRedeemCouponRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.RedeemCouponRequest>)delegate602;
					}
				}
			}
			if (this.OnRedeemCouponResultEvent != null)
			{
				foreach (Delegate delegate603 in this.OnRedeemCouponResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate603.Target, instance))
					{
						this.OnRedeemCouponResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.RedeemCouponResult>)delegate603;
					}
				}
			}
			if (this.OnRegisterForIOSPushNotificationRequestEvent != null)
			{
				foreach (Delegate delegate604 in this.OnRegisterForIOSPushNotificationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate604.Target, instance))
					{
						this.OnRegisterForIOSPushNotificationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterForIOSPushNotificationRequest>)delegate604;
					}
				}
			}
			if (this.OnRegisterForIOSPushNotificationResultEvent != null)
			{
				foreach (Delegate delegate605 in this.OnRegisterForIOSPushNotificationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate605.Target, instance))
					{
						this.OnRegisterForIOSPushNotificationResultEvent -= (PlayFabEvents.PlayFabResultEvent<RegisterForIOSPushNotificationResult>)delegate605;
					}
				}
			}
			if (this.OnRegisterPlayFabUserRequestEvent != null)
			{
				foreach (Delegate delegate606 in this.OnRegisterPlayFabUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate606.Target, instance))
					{
						this.OnRegisterPlayFabUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterPlayFabUserRequest>)delegate606;
					}
				}
			}
			if (this.OnRegisterPlayFabUserResultEvent != null)
			{
				foreach (Delegate delegate607 in this.OnRegisterPlayFabUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate607.Target, instance))
					{
						this.OnRegisterPlayFabUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<RegisterPlayFabUserResult>)delegate607;
					}
				}
			}
			if (this.OnRegisterWithWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate608 in this.OnRegisterWithWindowsHelloRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate608.Target, instance))
					{
						this.OnRegisterWithWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterWithWindowsHelloRequest>)delegate608;
					}
				}
			}
			if (this.OnRemoveContactEmailRequestEvent != null)
			{
				foreach (Delegate delegate609 in this.OnRemoveContactEmailRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate609.Target, instance))
					{
						this.OnRemoveContactEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveContactEmailRequest>)delegate609;
					}
				}
			}
			if (this.OnRemoveContactEmailResultEvent != null)
			{
				foreach (Delegate delegate610 in this.OnRemoveContactEmailResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate610.Target, instance))
					{
						this.OnRemoveContactEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveContactEmailResult>)delegate610;
					}
				}
			}
			if (this.OnRemoveFriendRequestEvent != null)
			{
				foreach (Delegate delegate611 in this.OnRemoveFriendRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate611.Target, instance))
					{
						this.OnRemoveFriendRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.RemoveFriendRequest>)delegate611;
					}
				}
			}
			if (this.OnRemoveFriendResultEvent != null)
			{
				foreach (Delegate delegate612 in this.OnRemoveFriendResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate612.Target, instance))
					{
						this.OnRemoveFriendResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveFriendResult>)delegate612;
					}
				}
			}
			if (this.OnRemoveGenericIDRequestEvent != null)
			{
				foreach (Delegate delegate613 in this.OnRemoveGenericIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate613.Target, instance))
					{
						this.OnRemoveGenericIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveGenericIDRequest>)delegate613;
					}
				}
			}
			if (this.OnRemoveGenericIDResultEvent != null)
			{
				foreach (Delegate delegate614 in this.OnRemoveGenericIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate614.Target, instance))
					{
						this.OnRemoveGenericIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveGenericIDResult>)delegate614;
					}
				}
			}
			if (this.OnRemoveSharedGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate615 in this.OnRemoveSharedGroupMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate615.Target, instance))
					{
						this.OnRemoveSharedGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.RemoveSharedGroupMembersRequest>)delegate615;
					}
				}
			}
			if (this.OnRemoveSharedGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate616 in this.OnRemoveSharedGroupMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate616.Target, instance))
					{
						this.OnRemoveSharedGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.RemoveSharedGroupMembersResult>)delegate616;
					}
				}
			}
			if (this.OnReportDeviceInfoRequestEvent != null)
			{
				foreach (Delegate delegate617 in this.OnReportDeviceInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate617.Target, instance))
					{
						this.OnReportDeviceInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeviceInfoRequest>)delegate617;
					}
				}
			}
			if (this.OnReportDeviceInfoResultEvent != null)
			{
				foreach (Delegate delegate618 in this.OnReportDeviceInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate618.Target, instance))
					{
						this.OnReportDeviceInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)delegate618;
					}
				}
			}
			if (this.OnReportPlayerRequestEvent != null)
			{
				foreach (Delegate delegate619 in this.OnReportPlayerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate619.Target, instance))
					{
						this.OnReportPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ReportPlayerClientRequest>)delegate619;
					}
				}
			}
			if (this.OnReportPlayerResultEvent != null)
			{
				foreach (Delegate delegate620 in this.OnReportPlayerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate620.Target, instance))
					{
						this.OnReportPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<ReportPlayerClientResult>)delegate620;
					}
				}
			}
			if (this.OnRestoreIOSPurchasesRequestEvent != null)
			{
				foreach (Delegate delegate621 in this.OnRestoreIOSPurchasesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate621.Target, instance))
					{
						this.OnRestoreIOSPurchasesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RestoreIOSPurchasesRequest>)delegate621;
					}
				}
			}
			if (this.OnRestoreIOSPurchasesResultEvent != null)
			{
				foreach (Delegate delegate622 in this.OnRestoreIOSPurchasesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate622.Target, instance))
					{
						this.OnRestoreIOSPurchasesResultEvent -= (PlayFabEvents.PlayFabResultEvent<RestoreIOSPurchasesResult>)delegate622;
					}
				}
			}
			if (this.OnSendAccountRecoveryEmailRequestEvent != null)
			{
				foreach (Delegate delegate623 in this.OnSendAccountRecoveryEmailRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate623.Target, instance))
					{
						this.OnSendAccountRecoveryEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.SendAccountRecoveryEmailRequest>)delegate623;
					}
				}
			}
			if (this.OnSendAccountRecoveryEmailResultEvent != null)
			{
				foreach (Delegate delegate624 in this.OnSendAccountRecoveryEmailResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate624.Target, instance))
					{
						this.OnSendAccountRecoveryEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.SendAccountRecoveryEmailResult>)delegate624;
					}
				}
			}
			if (this.OnSetFriendTagsRequestEvent != null)
			{
				foreach (Delegate delegate625 in this.OnSetFriendTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate625.Target, instance))
					{
						this.OnSetFriendTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.SetFriendTagsRequest>)delegate625;
					}
				}
			}
			if (this.OnSetFriendTagsResultEvent != null)
			{
				foreach (Delegate delegate626 in this.OnSetFriendTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate626.Target, instance))
					{
						this.OnSetFriendTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetFriendTagsResult>)delegate626;
					}
				}
			}
			if (this.OnSetPlayerSecretRequestEvent != null)
			{
				foreach (Delegate delegate627 in this.OnSetPlayerSecretRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate627.Target, instance))
					{
						this.OnSetPlayerSecretRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.SetPlayerSecretRequest>)delegate627;
					}
				}
			}
			if (this.OnSetPlayerSecretResultEvent != null)
			{
				foreach (Delegate delegate628 in this.OnSetPlayerSecretResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate628.Target, instance))
					{
						this.OnSetPlayerSecretResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.SetPlayerSecretResult>)delegate628;
					}
				}
			}
			if (this.OnStartGameRequestEvent != null)
			{
				foreach (Delegate delegate629 in this.OnStartGameRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate629.Target, instance))
					{
						this.OnStartGameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.StartGameRequest>)delegate629;
					}
				}
			}
			if (this.OnStartGameResultEvent != null)
			{
				foreach (Delegate delegate630 in this.OnStartGameResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate630.Target, instance))
					{
						this.OnStartGameResultEvent -= (PlayFabEvents.PlayFabResultEvent<StartGameResult>)delegate630;
					}
				}
			}
			if (this.OnStartPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate631 in this.OnStartPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate631.Target, instance))
					{
						this.OnStartPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<StartPurchaseRequest>)delegate631;
					}
				}
			}
			if (this.OnStartPurchaseResultEvent != null)
			{
				foreach (Delegate delegate632 in this.OnStartPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate632.Target, instance))
					{
						this.OnStartPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<StartPurchaseResult>)delegate632;
					}
				}
			}
			if (this.OnSubtractUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate633 in this.OnSubtractUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate633.Target, instance))
					{
						this.OnSubtractUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.SubtractUserVirtualCurrencyRequest>)delegate633;
					}
				}
			}
			if (this.OnSubtractUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate634 in this.OnSubtractUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate634.Target, instance))
					{
						this.OnSubtractUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ModifyUserVirtualCurrencyResult>)delegate634;
					}
				}
			}
			if (this.OnUnlinkAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate635 in this.OnUnlinkAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate635.Target, instance))
					{
						this.OnUnlinkAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest>)delegate635;
					}
				}
			}
			if (this.OnUnlinkAndroidDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate636 in this.OnUnlinkAndroidDeviceIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate636.Target, instance))
					{
						this.OnUnlinkAndroidDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkAndroidDeviceIDResult>)delegate636;
					}
				}
			}
			if (this.OnUnlinkCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate637 in this.OnUnlinkCustomIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate637.Target, instance))
					{
						this.OnUnlinkCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkCustomIDRequest>)delegate637;
					}
				}
			}
			if (this.OnUnlinkCustomIDResultEvent != null)
			{
				foreach (Delegate delegate638 in this.OnUnlinkCustomIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate638.Target, instance))
					{
						this.OnUnlinkCustomIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkCustomIDResult>)delegate638;
					}
				}
			}
			if (this.OnUnlinkFacebookAccountRequestEvent != null)
			{
				foreach (Delegate delegate639 in this.OnUnlinkFacebookAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate639.Target, instance))
					{
						this.OnUnlinkFacebookAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookAccountRequest>)delegate639;
					}
				}
			}
			if (this.OnUnlinkFacebookAccountResultEvent != null)
			{
				foreach (Delegate delegate640 in this.OnUnlinkFacebookAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate640.Target, instance))
					{
						this.OnUnlinkFacebookAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkFacebookAccountResult>)delegate640;
					}
				}
			}
			if (this.OnUnlinkGameCenterAccountRequestEvent != null)
			{
				foreach (Delegate delegate641 in this.OnUnlinkGameCenterAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate641.Target, instance))
					{
						this.OnUnlinkGameCenterAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkGameCenterAccountRequest>)delegate641;
					}
				}
			}
			if (this.OnUnlinkGameCenterAccountResultEvent != null)
			{
				foreach (Delegate delegate642 in this.OnUnlinkGameCenterAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate642.Target, instance))
					{
						this.OnUnlinkGameCenterAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkGameCenterAccountResult>)delegate642;
					}
				}
			}
			if (this.OnUnlinkGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate643 in this.OnUnlinkGoogleAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate643.Target, instance))
					{
						this.OnUnlinkGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkGoogleAccountRequest>)delegate643;
					}
				}
			}
			if (this.OnUnlinkGoogleAccountResultEvent != null)
			{
				foreach (Delegate delegate644 in this.OnUnlinkGoogleAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate644.Target, instance))
					{
						this.OnUnlinkGoogleAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkGoogleAccountResult>)delegate644;
					}
				}
			}
			if (this.OnUnlinkIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate645 in this.OnUnlinkIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate645.Target, instance))
					{
						this.OnUnlinkIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkIOSDeviceIDRequest>)delegate645;
					}
				}
			}
			if (this.OnUnlinkIOSDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate646 in this.OnUnlinkIOSDeviceIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate646.Target, instance))
					{
						this.OnUnlinkIOSDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkIOSDeviceIDResult>)delegate646;
					}
				}
			}
			if (this.OnUnlinkKongregateRequestEvent != null)
			{
				foreach (Delegate delegate647 in this.OnUnlinkKongregateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate647.Target, instance))
					{
						this.OnUnlinkKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkKongregateAccountRequest>)delegate647;
					}
				}
			}
			if (this.OnUnlinkKongregateResultEvent != null)
			{
				foreach (Delegate delegate648 in this.OnUnlinkKongregateResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate648.Target, instance))
					{
						this.OnUnlinkKongregateResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkKongregateAccountResult>)delegate648;
					}
				}
			}
			if (this.OnUnlinkSteamAccountRequestEvent != null)
			{
				foreach (Delegate delegate649 in this.OnUnlinkSteamAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate649.Target, instance))
					{
						this.OnUnlinkSteamAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkSteamAccountRequest>)delegate649;
					}
				}
			}
			if (this.OnUnlinkSteamAccountResultEvent != null)
			{
				foreach (Delegate delegate650 in this.OnUnlinkSteamAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate650.Target, instance))
					{
						this.OnUnlinkSteamAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkSteamAccountResult>)delegate650;
					}
				}
			}
			if (this.OnUnlinkTwitchRequestEvent != null)
			{
				foreach (Delegate delegate651 in this.OnUnlinkTwitchRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate651.Target, instance))
					{
						this.OnUnlinkTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkTwitchAccountRequest>)delegate651;
					}
				}
			}
			if (this.OnUnlinkTwitchResultEvent != null)
			{
				foreach (Delegate delegate652 in this.OnUnlinkTwitchResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate652.Target, instance))
					{
						this.OnUnlinkTwitchResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkTwitchAccountResult>)delegate652;
					}
				}
			}
			if (this.OnUnlinkWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate653 in this.OnUnlinkWindowsHelloRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate653.Target, instance))
					{
						this.OnUnlinkWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest>)delegate653;
					}
				}
			}
			if (this.OnUnlinkWindowsHelloResultEvent != null)
			{
				foreach (Delegate delegate654 in this.OnUnlinkWindowsHelloResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate654.Target, instance))
					{
						this.OnUnlinkWindowsHelloResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkWindowsHelloAccountResponse>)delegate654;
					}
				}
			}
			if (this.OnUnlockContainerInstanceRequestEvent != null)
			{
				foreach (Delegate delegate655 in this.OnUnlockContainerInstanceRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate655.Target, instance))
					{
						this.OnUnlockContainerInstanceRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UnlockContainerInstanceRequest>)delegate655;
					}
				}
			}
			if (this.OnUnlockContainerInstanceResultEvent != null)
			{
				foreach (Delegate delegate656 in this.OnUnlockContainerInstanceResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate656.Target, instance))
					{
						this.OnUnlockContainerInstanceResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UnlockContainerItemResult>)delegate656;
					}
				}
			}
			if (this.OnUnlockContainerItemRequestEvent != null)
			{
				foreach (Delegate delegate657 in this.OnUnlockContainerItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate657.Target, instance))
					{
						this.OnUnlockContainerItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UnlockContainerItemRequest>)delegate657;
					}
				}
			}
			if (this.OnUnlockContainerItemResultEvent != null)
			{
				foreach (Delegate delegate658 in this.OnUnlockContainerItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate658.Target, instance))
					{
						this.OnUnlockContainerItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UnlockContainerItemResult>)delegate658;
					}
				}
			}
			if (this.OnUpdateAvatarUrlRequestEvent != null)
			{
				foreach (Delegate delegate659 in this.OnUpdateAvatarUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate659.Target, instance))
					{
						this.OnUpdateAvatarUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateAvatarUrlRequest>)delegate659;
					}
				}
			}
			if (this.OnUpdateAvatarUrlResultEvent != null)
			{
				foreach (Delegate delegate660 in this.OnUpdateAvatarUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate660.Target, instance))
					{
						this.OnUpdateAvatarUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)delegate660;
					}
				}
			}
			if (this.OnUpdateCharacterDataRequestEvent != null)
			{
				foreach (Delegate delegate661 in this.OnUpdateCharacterDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate661.Target, instance))
					{
						this.OnUpdateCharacterDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateCharacterDataRequest>)delegate661;
					}
				}
			}
			if (this.OnUpdateCharacterDataResultEvent != null)
			{
				foreach (Delegate delegate662 in this.OnUpdateCharacterDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate662.Target, instance))
					{
						this.OnUpdateCharacterDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateCharacterDataResult>)delegate662;
					}
				}
			}
			if (this.OnUpdateCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate663 in this.OnUpdateCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate663.Target, instance))
					{
						this.OnUpdateCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateCharacterStatisticsRequest>)delegate663;
					}
				}
			}
			if (this.OnUpdateCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate664 in this.OnUpdateCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate664.Target, instance))
					{
						this.OnUpdateCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateCharacterStatisticsResult>)delegate664;
					}
				}
			}
			if (this.OnUpdatePlayerStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate665 in this.OnUpdatePlayerStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate665.Target, instance))
					{
						this.OnUpdatePlayerStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdatePlayerStatisticsRequest>)delegate665;
					}
				}
			}
			if (this.OnUpdatePlayerStatisticsResultEvent != null)
			{
				foreach (Delegate delegate666 in this.OnUpdatePlayerStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate666.Target, instance))
					{
						this.OnUpdatePlayerStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdatePlayerStatisticsResult>)delegate666;
					}
				}
			}
			if (this.OnUpdateSharedGroupDataRequestEvent != null)
			{
				foreach (Delegate delegate667 in this.OnUpdateSharedGroupDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate667.Target, instance))
					{
						this.OnUpdateSharedGroupDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateSharedGroupDataRequest>)delegate667;
					}
				}
			}
			if (this.OnUpdateSharedGroupDataResultEvent != null)
			{
				foreach (Delegate delegate668 in this.OnUpdateSharedGroupDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate668.Target, instance))
					{
						this.OnUpdateSharedGroupDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateSharedGroupDataResult>)delegate668;
					}
				}
			}
			if (this.OnUpdateUserDataRequestEvent != null)
			{
				foreach (Delegate delegate669 in this.OnUpdateUserDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate669.Target, instance))
					{
						this.OnUpdateUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateUserDataRequest>)delegate669;
					}
				}
			}
			if (this.OnUpdateUserDataResultEvent != null)
			{
				foreach (Delegate delegate670 in this.OnUpdateUserDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate670.Target, instance))
					{
						this.OnUpdateUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateUserDataResult>)delegate670;
					}
				}
			}
			if (this.OnUpdateUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate671 in this.OnUpdateUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate671.Target, instance))
					{
						this.OnUpdateUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateUserDataRequest>)delegate671;
					}
				}
			}
			if (this.OnUpdateUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate672 in this.OnUpdateUserPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate672.Target, instance))
					{
						this.OnUpdateUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateUserDataResult>)delegate672;
					}
				}
			}
			if (this.OnUpdateUserTitleDisplayNameRequestEvent != null)
			{
				foreach (Delegate delegate673 in this.OnUpdateUserTitleDisplayNameRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate673.Target, instance))
					{
						this.OnUpdateUserTitleDisplayNameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest>)delegate673;
					}
				}
			}
			if (this.OnUpdateUserTitleDisplayNameResultEvent != null)
			{
				foreach (Delegate delegate674 in this.OnUpdateUserTitleDisplayNameResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate674.Target, instance))
					{
						this.OnUpdateUserTitleDisplayNameResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.UpdateUserTitleDisplayNameResult>)delegate674;
					}
				}
			}
			if (this.OnValidateAmazonIAPReceiptRequestEvent != null)
			{
				foreach (Delegate delegate675 in this.OnValidateAmazonIAPReceiptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate675.Target, instance))
					{
						this.OnValidateAmazonIAPReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateAmazonReceiptRequest>)delegate675;
					}
				}
			}
			if (this.OnValidateAmazonIAPReceiptResultEvent != null)
			{
				foreach (Delegate delegate676 in this.OnValidateAmazonIAPReceiptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate676.Target, instance))
					{
						this.OnValidateAmazonIAPReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateAmazonReceiptResult>)delegate676;
					}
				}
			}
			if (this.OnValidateGooglePlayPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate677 in this.OnValidateGooglePlayPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate677.Target, instance))
					{
						this.OnValidateGooglePlayPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest>)delegate677;
					}
				}
			}
			if (this.OnValidateGooglePlayPurchaseResultEvent != null)
			{
				foreach (Delegate delegate678 in this.OnValidateGooglePlayPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate678.Target, instance))
					{
						this.OnValidateGooglePlayPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateGooglePlayPurchaseResult>)delegate678;
					}
				}
			}
			if (this.OnValidateIOSReceiptRequestEvent != null)
			{
				foreach (Delegate delegate679 in this.OnValidateIOSReceiptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate679.Target, instance))
					{
						this.OnValidateIOSReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateIOSReceiptRequest>)delegate679;
					}
				}
			}
			if (this.OnValidateIOSReceiptResultEvent != null)
			{
				foreach (Delegate delegate680 in this.OnValidateIOSReceiptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate680.Target, instance))
					{
						this.OnValidateIOSReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateIOSReceiptResult>)delegate680;
					}
				}
			}
			if (this.OnValidateWindowsStoreReceiptRequestEvent != null)
			{
				foreach (Delegate delegate681 in this.OnValidateWindowsStoreReceiptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate681.Target, instance))
					{
						this.OnValidateWindowsStoreReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateWindowsReceiptRequest>)delegate681;
					}
				}
			}
			if (this.OnValidateWindowsStoreReceiptResultEvent != null)
			{
				foreach (Delegate delegate682 in this.OnValidateWindowsStoreReceiptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate682.Target, instance))
					{
						this.OnValidateWindowsStoreReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateWindowsReceiptResult>)delegate682;
					}
				}
			}
			if (this.OnWriteCharacterEventRequestEvent != null)
			{
				foreach (Delegate delegate683 in this.OnWriteCharacterEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate683.Target, instance))
					{
						this.OnWriteCharacterEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteClientCharacterEventRequest>)delegate683;
					}
				}
			}
			if (this.OnWriteCharacterEventResultEvent != null)
			{
				foreach (Delegate delegate684 in this.OnWriteCharacterEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate684.Target, instance))
					{
						this.OnWriteCharacterEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.WriteEventResponse>)delegate684;
					}
				}
			}
			if (this.OnWritePlayerEventRequestEvent != null)
			{
				foreach (Delegate delegate685 in this.OnWritePlayerEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate685.Target, instance))
					{
						this.OnWritePlayerEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteClientPlayerEventRequest>)delegate685;
					}
				}
			}
			if (this.OnWritePlayerEventResultEvent != null)
			{
				foreach (Delegate delegate686 in this.OnWritePlayerEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate686.Target, instance))
					{
						this.OnWritePlayerEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.WriteEventResponse>)delegate686;
					}
				}
			}
			if (this.OnWriteTitleEventRequestEvent != null)
			{
				foreach (Delegate delegate687 in this.OnWriteTitleEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate687.Target, instance))
					{
						this.OnWriteTitleEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PlayFab.ClientModels.WriteTitleEventRequest>)delegate687;
					}
				}
			}
			if (this.OnWriteTitleEventResultEvent != null)
			{
				foreach (Delegate delegate688 in this.OnWriteTitleEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate688.Target, instance))
					{
						this.OnWriteTitleEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.WriteEventResponse>)delegate688;
					}
				}
			}
		}

		private void OnProcessingErrorEvent(PlayFabRequestCommon request, PlayFabError error)
		{
			if (PlayFabEvents._instance.OnGlobalErrorEvent != null)
			{
				PlayFabEvents._instance.OnGlobalErrorEvent(request, error);
			}
		}

		private void OnProcessingEvent(ApiProcessingEventArgs e)
		{
			if (e.EventType == ApiProcessingEventType.Pre)
			{
				Type type = e.Request.GetType();
				if (type == typeof(AbortTaskInstanceRequest) && PlayFabEvents._instance.OnAdminAbortTaskInstanceRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminAbortTaskInstanceRequestEvent((AbortTaskInstanceRequest)e.Request);
					return;
				}
				if (type == typeof(AddNewsRequest) && PlayFabEvents._instance.OnAdminAddNewsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddNewsRequestEvent((AddNewsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.AddPlayerTagRequest) && PlayFabEvents._instance.OnAdminAddPlayerTagRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddPlayerTagRequestEvent((PlayFab.AdminModels.AddPlayerTagRequest)e.Request);
					return;
				}
				if (type == typeof(AddServerBuildRequest) && PlayFabEvents._instance.OnAdminAddServerBuildRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddServerBuildRequestEvent((AddServerBuildRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.AddUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnAdminAddUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddUserVirtualCurrencyRequestEvent((PlayFab.AdminModels.AddUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(AddVirtualCurrencyTypesRequest) && PlayFabEvents._instance.OnAdminAddVirtualCurrencyTypesRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddVirtualCurrencyTypesRequestEvent((AddVirtualCurrencyTypesRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.BanUsersRequest) && PlayFabEvents._instance.OnAdminBanUsersRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminBanUsersRequestEvent((PlayFab.AdminModels.BanUsersRequest)e.Request);
					return;
				}
				if (type == typeof(CheckLimitedEditionItemAvailabilityRequest) && PlayFabEvents._instance.OnAdminCheckLimitedEditionItemAvailabilityRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminCheckLimitedEditionItemAvailabilityRequestEvent((CheckLimitedEditionItemAvailabilityRequest)e.Request);
					return;
				}
				if (type == typeof(CreateActionsOnPlayerSegmentTaskRequest) && PlayFabEvents._instance.OnAdminCreateActionsOnPlayersInSegmentTaskRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminCreateActionsOnPlayersInSegmentTaskRequestEvent((CreateActionsOnPlayerSegmentTaskRequest)e.Request);
					return;
				}
				if (type == typeof(CreateCloudScriptTaskRequest) && PlayFabEvents._instance.OnAdminCreateCloudScriptTaskRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminCreateCloudScriptTaskRequestEvent((CreateCloudScriptTaskRequest)e.Request);
					return;
				}
				if (type == typeof(CreatePlayerSharedSecretRequest) && PlayFabEvents._instance.OnAdminCreatePlayerSharedSecretRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminCreatePlayerSharedSecretRequestEvent((CreatePlayerSharedSecretRequest)e.Request);
					return;
				}
				if (type == typeof(CreatePlayerStatisticDefinitionRequest) && PlayFabEvents._instance.OnAdminCreatePlayerStatisticDefinitionRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminCreatePlayerStatisticDefinitionRequestEvent((CreatePlayerStatisticDefinitionRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteContentRequest) && PlayFabEvents._instance.OnAdminDeleteContentRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeleteContentRequestEvent((DeleteContentRequest)e.Request);
					return;
				}
				if (type == typeof(DeletePlayerRequest) && PlayFabEvents._instance.OnAdminDeletePlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeletePlayerRequestEvent((DeletePlayerRequest)e.Request);
					return;
				}
				if (type == typeof(DeletePlayerSharedSecretRequest) && PlayFabEvents._instance.OnAdminDeletePlayerSharedSecretRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeletePlayerSharedSecretRequestEvent((DeletePlayerSharedSecretRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteStoreRequest) && PlayFabEvents._instance.OnAdminDeleteStoreRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeleteStoreRequestEvent((DeleteStoreRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteTaskRequest) && PlayFabEvents._instance.OnAdminDeleteTaskRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeleteTaskRequestEvent((DeleteTaskRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteTitleRequest) && PlayFabEvents._instance.OnAdminDeleteTitleRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeleteTitleRequestEvent((DeleteTitleRequest)e.Request);
					return;
				}
				if (type == typeof(GetTaskInstanceRequest) && PlayFabEvents._instance.OnAdminGetActionsOnPlayersInSegmentTaskInstanceRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetActionsOnPlayersInSegmentTaskInstanceRequestEvent((GetTaskInstanceRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetAllSegmentsRequest) && PlayFabEvents._instance.OnAdminGetAllSegmentsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetAllSegmentsRequestEvent((PlayFab.AdminModels.GetAllSegmentsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetCatalogItemsRequest) && PlayFabEvents._instance.OnAdminGetCatalogItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetCatalogItemsRequestEvent((PlayFab.AdminModels.GetCatalogItemsRequest)e.Request);
					return;
				}
				if (type == typeof(GetCloudScriptRevisionRequest) && PlayFabEvents._instance.OnAdminGetCloudScriptRevisionRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetCloudScriptRevisionRequestEvent((GetCloudScriptRevisionRequest)e.Request);
					return;
				}
				if (type == typeof(GetTaskInstanceRequest) && PlayFabEvents._instance.OnAdminGetCloudScriptTaskInstanceRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetCloudScriptTaskInstanceRequestEvent((GetTaskInstanceRequest)e.Request);
					return;
				}
				if (type == typeof(GetCloudScriptVersionsRequest) && PlayFabEvents._instance.OnAdminGetCloudScriptVersionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetCloudScriptVersionsRequestEvent((GetCloudScriptVersionsRequest)e.Request);
					return;
				}
				if (type == typeof(GetContentListRequest) && PlayFabEvents._instance.OnAdminGetContentListRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetContentListRequestEvent((GetContentListRequest)e.Request);
					return;
				}
				if (type == typeof(GetContentUploadUrlRequest) && PlayFabEvents._instance.OnAdminGetContentUploadUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetContentUploadUrlRequestEvent((GetContentUploadUrlRequest)e.Request);
					return;
				}
				if (type == typeof(GetDataReportRequest) && PlayFabEvents._instance.OnAdminGetDataReportRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetDataReportRequestEvent((GetDataReportRequest)e.Request);
					return;
				}
				if (type == typeof(GetMatchmakerGameInfoRequest) && PlayFabEvents._instance.OnAdminGetMatchmakerGameInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetMatchmakerGameInfoRequestEvent((GetMatchmakerGameInfoRequest)e.Request);
					return;
				}
				if (type == typeof(GetMatchmakerGameModesRequest) && PlayFabEvents._instance.OnAdminGetMatchmakerGameModesRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetMatchmakerGameModesRequestEvent((GetMatchmakerGameModesRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerIdFromAuthTokenRequest) && PlayFabEvents._instance.OnAdminGetPlayerIdFromAuthTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerIdFromAuthTokenRequestEvent((GetPlayerIdFromAuthTokenRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetPlayerProfileRequest) && PlayFabEvents._instance.OnAdminGetPlayerProfileRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerProfileRequestEvent((PlayFab.AdminModels.GetPlayerProfileRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetPlayersSegmentsRequest) && PlayFabEvents._instance.OnAdminGetPlayerSegmentsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerSegmentsRequestEvent((PlayFab.AdminModels.GetPlayersSegmentsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerSharedSecretsRequest) && PlayFabEvents._instance.OnAdminGetPlayerSharedSecretsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerSharedSecretsRequestEvent((GetPlayerSharedSecretsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetPlayersInSegmentRequest) && PlayFabEvents._instance.OnAdminGetPlayersInSegmentRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayersInSegmentRequestEvent((PlayFab.AdminModels.GetPlayersInSegmentRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerStatisticDefinitionsRequest) && PlayFabEvents._instance.OnAdminGetPlayerStatisticDefinitionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerStatisticDefinitionsRequestEvent((GetPlayerStatisticDefinitionsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetPlayerStatisticVersionsRequest) && PlayFabEvents._instance.OnAdminGetPlayerStatisticVersionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerStatisticVersionsRequestEvent((PlayFab.AdminModels.GetPlayerStatisticVersionsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetPlayerTagsRequest) && PlayFabEvents._instance.OnAdminGetPlayerTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerTagsRequestEvent((PlayFab.AdminModels.GetPlayerTagsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPolicyRequest) && PlayFabEvents._instance.OnAdminGetPolicyRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPolicyRequestEvent((GetPolicyRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetPublisherDataRequest) && PlayFabEvents._instance.OnAdminGetPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPublisherDataRequestEvent((PlayFab.AdminModels.GetPublisherDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetRandomResultTablesRequest) && PlayFabEvents._instance.OnAdminGetRandomResultTablesRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetRandomResultTablesRequestEvent((PlayFab.AdminModels.GetRandomResultTablesRequest)e.Request);
					return;
				}
				if (type == typeof(GetServerBuildInfoRequest) && PlayFabEvents._instance.OnAdminGetServerBuildInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetServerBuildInfoRequestEvent((GetServerBuildInfoRequest)e.Request);
					return;
				}
				if (type == typeof(GetServerBuildUploadURLRequest) && PlayFabEvents._instance.OnAdminGetServerBuildUploadUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetServerBuildUploadUrlRequestEvent((GetServerBuildUploadURLRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetStoreItemsRequest) && PlayFabEvents._instance.OnAdminGetStoreItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetStoreItemsRequestEvent((PlayFab.AdminModels.GetStoreItemsRequest)e.Request);
					return;
				}
				if (type == typeof(GetTaskInstancesRequest) && PlayFabEvents._instance.OnAdminGetTaskInstancesRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetTaskInstancesRequestEvent((GetTaskInstancesRequest)e.Request);
					return;
				}
				if (type == typeof(GetTasksRequest) && PlayFabEvents._instance.OnAdminGetTasksRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetTasksRequestEvent((GetTasksRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetTitleDataRequest) && PlayFabEvents._instance.OnAdminGetTitleDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetTitleDataRequestEvent((PlayFab.AdminModels.GetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetTitleDataRequest) && PlayFabEvents._instance.OnAdminGetTitleInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetTitleInternalDataRequestEvent((PlayFab.AdminModels.GetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(LookupUserAccountInfoRequest) && PlayFabEvents._instance.OnAdminGetUserAccountInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserAccountInfoRequestEvent((LookupUserAccountInfoRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetUserBansRequest) && PlayFabEvents._instance.OnAdminGetUserBansRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserBansRequestEvent((PlayFab.AdminModels.GetUserBansRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetUserDataRequest) && PlayFabEvents._instance.OnAdminGetUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserDataRequestEvent((PlayFab.AdminModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetUserDataRequest) && PlayFabEvents._instance.OnAdminGetUserInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserInternalDataRequestEvent((PlayFab.AdminModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetUserInventoryRequest) && PlayFabEvents._instance.OnAdminGetUserInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserInventoryRequestEvent((PlayFab.AdminModels.GetUserInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetUserDataRequest) && PlayFabEvents._instance.OnAdminGetUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserPublisherDataRequestEvent((PlayFab.AdminModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetUserDataRequest) && PlayFabEvents._instance.OnAdminGetUserPublisherInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserPublisherInternalDataRequestEvent((PlayFab.AdminModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetUserDataRequest) && PlayFabEvents._instance.OnAdminGetUserPublisherReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserPublisherReadOnlyDataRequestEvent((PlayFab.AdminModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GetUserDataRequest) && PlayFabEvents._instance.OnAdminGetUserReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserReadOnlyDataRequestEvent((PlayFab.AdminModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.GrantItemsToUsersRequest) && PlayFabEvents._instance.OnAdminGrantItemsToUsersRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminGrantItemsToUsersRequestEvent((PlayFab.AdminModels.GrantItemsToUsersRequest)e.Request);
					return;
				}
				if (type == typeof(IncrementLimitedEditionItemAvailabilityRequest) && PlayFabEvents._instance.OnAdminIncrementLimitedEditionItemAvailabilityRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminIncrementLimitedEditionItemAvailabilityRequestEvent((IncrementLimitedEditionItemAvailabilityRequest)e.Request);
					return;
				}
				if (type == typeof(IncrementPlayerStatisticVersionRequest) && PlayFabEvents._instance.OnAdminIncrementPlayerStatisticVersionRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminIncrementPlayerStatisticVersionRequestEvent((IncrementPlayerStatisticVersionRequest)e.Request);
					return;
				}
				if (type == typeof(ListBuildsRequest) && PlayFabEvents._instance.OnAdminListServerBuildsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminListServerBuildsRequestEvent((ListBuildsRequest)e.Request);
					return;
				}
				if (type == typeof(ListVirtualCurrencyTypesRequest) && PlayFabEvents._instance.OnAdminListVirtualCurrencyTypesRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminListVirtualCurrencyTypesRequestEvent((ListVirtualCurrencyTypesRequest)e.Request);
					return;
				}
				if (type == typeof(ModifyMatchmakerGameModesRequest) && PlayFabEvents._instance.OnAdminModifyMatchmakerGameModesRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminModifyMatchmakerGameModesRequestEvent((ModifyMatchmakerGameModesRequest)e.Request);
					return;
				}
				if (type == typeof(ModifyServerBuildRequest) && PlayFabEvents._instance.OnAdminModifyServerBuildRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminModifyServerBuildRequestEvent((ModifyServerBuildRequest)e.Request);
					return;
				}
				if (type == typeof(RefundPurchaseRequest) && PlayFabEvents._instance.OnAdminRefundPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminRefundPurchaseRequestEvent((RefundPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.RemovePlayerTagRequest) && PlayFabEvents._instance.OnAdminRemovePlayerTagRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminRemovePlayerTagRequestEvent((PlayFab.AdminModels.RemovePlayerTagRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveServerBuildRequest) && PlayFabEvents._instance.OnAdminRemoveServerBuildRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminRemoveServerBuildRequestEvent((RemoveServerBuildRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveVirtualCurrencyTypesRequest) && PlayFabEvents._instance.OnAdminRemoveVirtualCurrencyTypesRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminRemoveVirtualCurrencyTypesRequestEvent((RemoveVirtualCurrencyTypesRequest)e.Request);
					return;
				}
				if (type == typeof(ResetCharacterStatisticsRequest) && PlayFabEvents._instance.OnAdminResetCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminResetCharacterStatisticsRequestEvent((ResetCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(ResetPasswordRequest) && PlayFabEvents._instance.OnAdminResetPasswordRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminResetPasswordRequestEvent((ResetPasswordRequest)e.Request);
					return;
				}
				if (type == typeof(ResetUserStatisticsRequest) && PlayFabEvents._instance.OnAdminResetUserStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminResetUserStatisticsRequestEvent((ResetUserStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(ResolvePurchaseDisputeRequest) && PlayFabEvents._instance.OnAdminResolvePurchaseDisputeRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminResolvePurchaseDisputeRequestEvent((ResolvePurchaseDisputeRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.RevokeAllBansForUserRequest) && PlayFabEvents._instance.OnAdminRevokeAllBansForUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminRevokeAllBansForUserRequestEvent((PlayFab.AdminModels.RevokeAllBansForUserRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.RevokeBansRequest) && PlayFabEvents._instance.OnAdminRevokeBansRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminRevokeBansRequestEvent((PlayFab.AdminModels.RevokeBansRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.RevokeInventoryItemRequest) && PlayFabEvents._instance.OnAdminRevokeInventoryItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminRevokeInventoryItemRequestEvent((PlayFab.AdminModels.RevokeInventoryItemRequest)e.Request);
					return;
				}
				if (type == typeof(RunTaskRequest) && PlayFabEvents._instance.OnAdminRunTaskRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminRunTaskRequestEvent((RunTaskRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.SendAccountRecoveryEmailRequest) && PlayFabEvents._instance.OnAdminSendAccountRecoveryEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSendAccountRecoveryEmailRequestEvent((PlayFab.AdminModels.SendAccountRecoveryEmailRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateCatalogItemsRequest) && PlayFabEvents._instance.OnAdminSetCatalogItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetCatalogItemsRequestEvent((UpdateCatalogItemsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.SetPlayerSecretRequest) && PlayFabEvents._instance.OnAdminSetPlayerSecretRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetPlayerSecretRequestEvent((PlayFab.AdminModels.SetPlayerSecretRequest)e.Request);
					return;
				}
				if (type == typeof(SetPublishedRevisionRequest) && PlayFabEvents._instance.OnAdminSetPublishedRevisionRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetPublishedRevisionRequestEvent((SetPublishedRevisionRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.SetPublisherDataRequest) && PlayFabEvents._instance.OnAdminSetPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetPublisherDataRequestEvent((PlayFab.AdminModels.SetPublisherDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateStoreItemsRequest) && PlayFabEvents._instance.OnAdminSetStoreItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetStoreItemsRequestEvent((UpdateStoreItemsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.SetTitleDataRequest) && PlayFabEvents._instance.OnAdminSetTitleDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetTitleDataRequestEvent((PlayFab.AdminModels.SetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.SetTitleDataRequest) && PlayFabEvents._instance.OnAdminSetTitleInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetTitleInternalDataRequestEvent((PlayFab.AdminModels.SetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(SetupPushNotificationRequest) && PlayFabEvents._instance.OnAdminSetupPushNotificationRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetupPushNotificationRequestEvent((SetupPushNotificationRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.SubtractUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnAdminSubtractUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminSubtractUserVirtualCurrencyRequestEvent((PlayFab.AdminModels.SubtractUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.UpdateBansRequest) && PlayFabEvents._instance.OnAdminUpdateBansRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateBansRequestEvent((PlayFab.AdminModels.UpdateBansRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateCatalogItemsRequest) && PlayFabEvents._instance.OnAdminUpdateCatalogItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateCatalogItemsRequestEvent((UpdateCatalogItemsRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateCloudScriptRequest) && PlayFabEvents._instance.OnAdminUpdateCloudScriptRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateCloudScriptRequestEvent((UpdateCloudScriptRequest)e.Request);
					return;
				}
				if (type == typeof(UpdatePlayerSharedSecretRequest) && PlayFabEvents._instance.OnAdminUpdatePlayerSharedSecretRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdatePlayerSharedSecretRequestEvent((UpdatePlayerSharedSecretRequest)e.Request);
					return;
				}
				if (type == typeof(UpdatePlayerStatisticDefinitionRequest) && PlayFabEvents._instance.OnAdminUpdatePlayerStatisticDefinitionRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdatePlayerStatisticDefinitionRequestEvent((UpdatePlayerStatisticDefinitionRequest)e.Request);
					return;
				}
				if (type == typeof(UpdatePolicyRequest) && PlayFabEvents._instance.OnAdminUpdatePolicyRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdatePolicyRequestEvent((UpdatePolicyRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateRandomResultTablesRequest) && PlayFabEvents._instance.OnAdminUpdateRandomResultTablesRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateRandomResultTablesRequestEvent((UpdateRandomResultTablesRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateStoreItemsRequest) && PlayFabEvents._instance.OnAdminUpdateStoreItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateStoreItemsRequestEvent((UpdateStoreItemsRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateTaskRequest) && PlayFabEvents._instance.OnAdminUpdateTaskRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateTaskRequestEvent((UpdateTaskRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnAdminUpdateUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserDataRequestEvent((PlayFab.AdminModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.UpdateUserInternalDataRequest) && PlayFabEvents._instance.OnAdminUpdateUserInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserInternalDataRequestEvent((PlayFab.AdminModels.UpdateUserInternalDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnAdminUpdateUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserPublisherDataRequestEvent((PlayFab.AdminModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.UpdateUserInternalDataRequest) && PlayFabEvents._instance.OnAdminUpdateUserPublisherInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserPublisherInternalDataRequestEvent((PlayFab.AdminModels.UpdateUserInternalDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnAdminUpdateUserPublisherReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserPublisherReadOnlyDataRequestEvent((PlayFab.AdminModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnAdminUpdateUserReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserReadOnlyDataRequestEvent((PlayFab.AdminModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.AdminModels.UpdateUserTitleDisplayNameRequest) && PlayFabEvents._instance.OnAdminUpdateUserTitleDisplayNameRequestEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserTitleDisplayNameRequestEvent((PlayFab.AdminModels.UpdateUserTitleDisplayNameRequest)e.Request);
					return;
				}
				if (type == typeof(AuthUserRequest) && PlayFabEvents._instance.OnMatchmakerAuthUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerAuthUserRequestEvent((AuthUserRequest)e.Request);
					return;
				}
				if (type == typeof(PlayerJoinedRequest) && PlayFabEvents._instance.OnMatchmakerPlayerJoinedRequestEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerPlayerJoinedRequestEvent((PlayerJoinedRequest)e.Request);
					return;
				}
				if (type == typeof(PlayerLeftRequest) && PlayFabEvents._instance.OnMatchmakerPlayerLeftRequestEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerPlayerLeftRequestEvent((PlayerLeftRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.MatchmakerModels.StartGameRequest) && PlayFabEvents._instance.OnMatchmakerStartGameRequestEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerStartGameRequestEvent((PlayFab.MatchmakerModels.StartGameRequest)e.Request);
					return;
				}
				if (type == typeof(UserInfoRequest) && PlayFabEvents._instance.OnMatchmakerUserInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerUserInfoRequestEvent((UserInfoRequest)e.Request);
					return;
				}
				if (type == typeof(AddCharacterVirtualCurrencyRequest) && PlayFabEvents._instance.OnServerAddCharacterVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerAddCharacterVirtualCurrencyRequestEvent((AddCharacterVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.AddFriendRequest) && PlayFabEvents._instance.OnServerAddFriendRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerAddFriendRequestEvent((PlayFab.ServerModels.AddFriendRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.AddPlayerTagRequest) && PlayFabEvents._instance.OnServerAddPlayerTagRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerAddPlayerTagRequestEvent((PlayFab.ServerModels.AddPlayerTagRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.AddSharedGroupMembersRequest) && PlayFabEvents._instance.OnServerAddSharedGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerAddSharedGroupMembersRequestEvent((PlayFab.ServerModels.AddSharedGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.AddUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnServerAddUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerAddUserVirtualCurrencyRequestEvent((PlayFab.ServerModels.AddUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(AuthenticateSessionTicketRequest) && PlayFabEvents._instance.OnServerAuthenticateSessionTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerAuthenticateSessionTicketRequestEvent((AuthenticateSessionTicketRequest)e.Request);
					return;
				}
				if (type == typeof(AwardSteamAchievementRequest) && PlayFabEvents._instance.OnServerAwardSteamAchievementRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerAwardSteamAchievementRequestEvent((AwardSteamAchievementRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.BanUsersRequest) && PlayFabEvents._instance.OnServerBanUsersRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerBanUsersRequestEvent((PlayFab.ServerModels.BanUsersRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.ConsumeItemRequest) && PlayFabEvents._instance.OnServerConsumeItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerConsumeItemRequestEvent((PlayFab.ServerModels.ConsumeItemRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.CreateSharedGroupRequest) && PlayFabEvents._instance.OnServerCreateSharedGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerCreateSharedGroupRequestEvent((PlayFab.ServerModels.CreateSharedGroupRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteCharacterFromUserRequest) && PlayFabEvents._instance.OnServerDeleteCharacterFromUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerDeleteCharacterFromUserRequestEvent((DeleteCharacterFromUserRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteSharedGroupRequest) && PlayFabEvents._instance.OnServerDeleteSharedGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerDeleteSharedGroupRequestEvent((DeleteSharedGroupRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.DeleteUsersRequest) && PlayFabEvents._instance.OnServerDeleteUsersRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerDeleteUsersRequestEvent((PlayFab.ServerModels.DeleteUsersRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.DeregisterGameRequest) && PlayFabEvents._instance.OnServerDeregisterGameRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerDeregisterGameRequestEvent((PlayFab.ServerModels.DeregisterGameRequest)e.Request);
					return;
				}
				if (type == typeof(EvaluateRandomResultTableRequest) && PlayFabEvents._instance.OnServerEvaluateRandomResultTableRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerEvaluateRandomResultTableRequestEvent((EvaluateRandomResultTableRequest)e.Request);
					return;
				}
				if (type == typeof(ExecuteCloudScriptServerRequest) && PlayFabEvents._instance.OnServerExecuteCloudScriptRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerExecuteCloudScriptRequestEvent((ExecuteCloudScriptServerRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetAllSegmentsRequest) && PlayFabEvents._instance.OnServerGetAllSegmentsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetAllSegmentsRequestEvent((PlayFab.ServerModels.GetAllSegmentsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.ListUsersCharactersRequest) && PlayFabEvents._instance.OnServerGetAllUsersCharactersRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetAllUsersCharactersRequestEvent((PlayFab.ServerModels.ListUsersCharactersRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetCatalogItemsRequest) && PlayFabEvents._instance.OnServerGetCatalogItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCatalogItemsRequestEvent((PlayFab.ServerModels.GetCatalogItemsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetCharacterDataRequest) && PlayFabEvents._instance.OnServerGetCharacterDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterDataRequestEvent((PlayFab.ServerModels.GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetCharacterDataRequest) && PlayFabEvents._instance.OnServerGetCharacterInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterInternalDataRequestEvent((PlayFab.ServerModels.GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetCharacterInventoryRequest) && PlayFabEvents._instance.OnServerGetCharacterInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterInventoryRequestEvent((PlayFab.ServerModels.GetCharacterInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetCharacterLeaderboardRequest) && PlayFabEvents._instance.OnServerGetCharacterLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterLeaderboardRequestEvent((PlayFab.ServerModels.GetCharacterLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetCharacterDataRequest) && PlayFabEvents._instance.OnServerGetCharacterReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterReadOnlyDataRequestEvent((PlayFab.ServerModels.GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetCharacterStatisticsRequest) && PlayFabEvents._instance.OnServerGetCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterStatisticsRequestEvent((PlayFab.ServerModels.GetCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetContentDownloadUrlRequest) && PlayFabEvents._instance.OnServerGetContentDownloadUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetContentDownloadUrlRequestEvent((PlayFab.ServerModels.GetContentDownloadUrlRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetFriendLeaderboardRequest) && PlayFabEvents._instance.OnServerGetFriendLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetFriendLeaderboardRequestEvent((PlayFab.ServerModels.GetFriendLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetFriendsListRequest) && PlayFabEvents._instance.OnServerGetFriendsListRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetFriendsListRequestEvent((PlayFab.ServerModels.GetFriendsListRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetLeaderboardRequest) && PlayFabEvents._instance.OnServerGetLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetLeaderboardRequestEvent((PlayFab.ServerModels.GetLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetLeaderboardAroundCharacterRequest) && PlayFabEvents._instance.OnServerGetLeaderboardAroundCharacterRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetLeaderboardAroundCharacterRequestEvent((PlayFab.ServerModels.GetLeaderboardAroundCharacterRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardAroundUserRequest) && PlayFabEvents._instance.OnServerGetLeaderboardAroundUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetLeaderboardAroundUserRequestEvent((GetLeaderboardAroundUserRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetLeaderboardForUsersCharactersRequest) && PlayFabEvents._instance.OnServerGetLeaderboardForUserCharactersRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetLeaderboardForUserCharactersRequestEvent((PlayFab.ServerModels.GetLeaderboardForUsersCharactersRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayerCombinedInfoRequest) && PlayFabEvents._instance.OnServerGetPlayerCombinedInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerCombinedInfoRequestEvent((PlayFab.ServerModels.GetPlayerCombinedInfoRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayerProfileRequest) && PlayFabEvents._instance.OnServerGetPlayerProfileRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerProfileRequestEvent((PlayFab.ServerModels.GetPlayerProfileRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayersSegmentsRequest) && PlayFabEvents._instance.OnServerGetPlayerSegmentsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerSegmentsRequestEvent((PlayFab.ServerModels.GetPlayersSegmentsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayersInSegmentRequest) && PlayFabEvents._instance.OnServerGetPlayersInSegmentRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayersInSegmentRequestEvent((PlayFab.ServerModels.GetPlayersInSegmentRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayerStatisticsRequest) && PlayFabEvents._instance.OnServerGetPlayerStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerStatisticsRequestEvent((PlayFab.ServerModels.GetPlayerStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayerStatisticVersionsRequest) && PlayFabEvents._instance.OnServerGetPlayerStatisticVersionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerStatisticVersionsRequestEvent((PlayFab.ServerModels.GetPlayerStatisticVersionsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayerTagsRequest) && PlayFabEvents._instance.OnServerGetPlayerTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerTagsRequestEvent((PlayFab.ServerModels.GetPlayerTagsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayFabIDsFromFacebookIDsRequest) && PlayFabEvents._instance.OnServerGetPlayFabIDsFromFacebookIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayFabIDsFromFacebookIDsRequestEvent((PlayFab.ServerModels.GetPlayFabIDsFromFacebookIDsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPlayFabIDsFromSteamIDsRequest) && PlayFabEvents._instance.OnServerGetPlayFabIDsFromSteamIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayFabIDsFromSteamIDsRequestEvent((PlayFab.ServerModels.GetPlayFabIDsFromSteamIDsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetPublisherDataRequest) && PlayFabEvents._instance.OnServerGetPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPublisherDataRequestEvent((PlayFab.ServerModels.GetPublisherDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetRandomResultTablesRequest) && PlayFabEvents._instance.OnServerGetRandomResultTablesRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetRandomResultTablesRequestEvent((PlayFab.ServerModels.GetRandomResultTablesRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetSharedGroupDataRequest) && PlayFabEvents._instance.OnServerGetSharedGroupDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetSharedGroupDataRequestEvent((PlayFab.ServerModels.GetSharedGroupDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetTimeRequest) && PlayFabEvents._instance.OnServerGetTimeRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetTimeRequestEvent((PlayFab.ServerModels.GetTimeRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetTitleDataRequest) && PlayFabEvents._instance.OnServerGetTitleDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetTitleDataRequestEvent((PlayFab.ServerModels.GetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetTitleDataRequest) && PlayFabEvents._instance.OnServerGetTitleInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetTitleInternalDataRequestEvent((PlayFab.ServerModels.GetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetTitleNewsRequest) && PlayFabEvents._instance.OnServerGetTitleNewsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetTitleNewsRequestEvent((PlayFab.ServerModels.GetTitleNewsRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserAccountInfoRequest) && PlayFabEvents._instance.OnServerGetUserAccountInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserAccountInfoRequestEvent((GetUserAccountInfoRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetUserBansRequest) && PlayFabEvents._instance.OnServerGetUserBansRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserBansRequestEvent((PlayFab.ServerModels.GetUserBansRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetUserDataRequest) && PlayFabEvents._instance.OnServerGetUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserDataRequestEvent((PlayFab.ServerModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetUserDataRequest) && PlayFabEvents._instance.OnServerGetUserInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserInternalDataRequestEvent((PlayFab.ServerModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetUserInventoryRequest) && PlayFabEvents._instance.OnServerGetUserInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserInventoryRequestEvent((PlayFab.ServerModels.GetUserInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetUserDataRequest) && PlayFabEvents._instance.OnServerGetUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserPublisherDataRequestEvent((PlayFab.ServerModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetUserDataRequest) && PlayFabEvents._instance.OnServerGetUserPublisherInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserPublisherInternalDataRequestEvent((PlayFab.ServerModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetUserDataRequest) && PlayFabEvents._instance.OnServerGetUserPublisherReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserPublisherReadOnlyDataRequestEvent((PlayFab.ServerModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GetUserDataRequest) && PlayFabEvents._instance.OnServerGetUserReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserReadOnlyDataRequestEvent((PlayFab.ServerModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GrantCharacterToUserRequest) && PlayFabEvents._instance.OnServerGrantCharacterToUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGrantCharacterToUserRequestEvent((PlayFab.ServerModels.GrantCharacterToUserRequest)e.Request);
					return;
				}
				if (type == typeof(GrantItemsToCharacterRequest) && PlayFabEvents._instance.OnServerGrantItemsToCharacterRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGrantItemsToCharacterRequestEvent((GrantItemsToCharacterRequest)e.Request);
					return;
				}
				if (type == typeof(GrantItemsToUserRequest) && PlayFabEvents._instance.OnServerGrantItemsToUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGrantItemsToUserRequestEvent((GrantItemsToUserRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.GrantItemsToUsersRequest) && PlayFabEvents._instance.OnServerGrantItemsToUsersRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerGrantItemsToUsersRequestEvent((PlayFab.ServerModels.GrantItemsToUsersRequest)e.Request);
					return;
				}
				if (type == typeof(ModifyItemUsesRequest) && PlayFabEvents._instance.OnServerModifyItemUsesRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerModifyItemUsesRequestEvent((ModifyItemUsesRequest)e.Request);
					return;
				}
				if (type == typeof(MoveItemToCharacterFromCharacterRequest) && PlayFabEvents._instance.OnServerMoveItemToCharacterFromCharacterRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerMoveItemToCharacterFromCharacterRequestEvent((MoveItemToCharacterFromCharacterRequest)e.Request);
					return;
				}
				if (type == typeof(MoveItemToCharacterFromUserRequest) && PlayFabEvents._instance.OnServerMoveItemToCharacterFromUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerMoveItemToCharacterFromUserRequestEvent((MoveItemToCharacterFromUserRequest)e.Request);
					return;
				}
				if (type == typeof(MoveItemToUserFromCharacterRequest) && PlayFabEvents._instance.OnServerMoveItemToUserFromCharacterRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerMoveItemToUserFromCharacterRequestEvent((MoveItemToUserFromCharacterRequest)e.Request);
					return;
				}
				if (type == typeof(NotifyMatchmakerPlayerLeftRequest) && PlayFabEvents._instance.OnServerNotifyMatchmakerPlayerLeftRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerNotifyMatchmakerPlayerLeftRequestEvent((NotifyMatchmakerPlayerLeftRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.RedeemCouponRequest) && PlayFabEvents._instance.OnServerRedeemCouponRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRedeemCouponRequestEvent((PlayFab.ServerModels.RedeemCouponRequest)e.Request);
					return;
				}
				if (type == typeof(RedeemMatchmakerTicketRequest) && PlayFabEvents._instance.OnServerRedeemMatchmakerTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRedeemMatchmakerTicketRequestEvent((RedeemMatchmakerTicketRequest)e.Request);
					return;
				}
				if (type == typeof(RefreshGameServerInstanceHeartbeatRequest) && PlayFabEvents._instance.OnServerRefreshGameServerInstanceHeartbeatRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRefreshGameServerInstanceHeartbeatRequestEvent((RefreshGameServerInstanceHeartbeatRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.RegisterGameRequest) && PlayFabEvents._instance.OnServerRegisterGameRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRegisterGameRequestEvent((PlayFab.ServerModels.RegisterGameRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.RemoveFriendRequest) && PlayFabEvents._instance.OnServerRemoveFriendRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRemoveFriendRequestEvent((PlayFab.ServerModels.RemoveFriendRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.RemovePlayerTagRequest) && PlayFabEvents._instance.OnServerRemovePlayerTagRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRemovePlayerTagRequestEvent((PlayFab.ServerModels.RemovePlayerTagRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.RemoveSharedGroupMembersRequest) && PlayFabEvents._instance.OnServerRemoveSharedGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRemoveSharedGroupMembersRequestEvent((PlayFab.ServerModels.RemoveSharedGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(ReportPlayerServerRequest) && PlayFabEvents._instance.OnServerReportPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerReportPlayerRequestEvent((ReportPlayerServerRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.RevokeAllBansForUserRequest) && PlayFabEvents._instance.OnServerRevokeAllBansForUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRevokeAllBansForUserRequestEvent((PlayFab.ServerModels.RevokeAllBansForUserRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.RevokeBansRequest) && PlayFabEvents._instance.OnServerRevokeBansRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRevokeBansRequestEvent((PlayFab.ServerModels.RevokeBansRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.RevokeInventoryItemRequest) && PlayFabEvents._instance.OnServerRevokeInventoryItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerRevokeInventoryItemRequestEvent((PlayFab.ServerModels.RevokeInventoryItemRequest)e.Request);
					return;
				}
				if (type == typeof(SendCustomAccountRecoveryEmailRequest) && PlayFabEvents._instance.OnServerSendCustomAccountRecoveryEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSendCustomAccountRecoveryEmailRequestEvent((SendCustomAccountRecoveryEmailRequest)e.Request);
					return;
				}
				if (type == typeof(SendEmailFromTemplateRequest) && PlayFabEvents._instance.OnServerSendEmailFromTemplateRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSendEmailFromTemplateRequestEvent((SendEmailFromTemplateRequest)e.Request);
					return;
				}
				if (type == typeof(SendPushNotificationRequest) && PlayFabEvents._instance.OnServerSendPushNotificationRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSendPushNotificationRequestEvent((SendPushNotificationRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.SetFriendTagsRequest) && PlayFabEvents._instance.OnServerSetFriendTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSetFriendTagsRequestEvent((PlayFab.ServerModels.SetFriendTagsRequest)e.Request);
					return;
				}
				if (type == typeof(SetGameServerInstanceDataRequest) && PlayFabEvents._instance.OnServerSetGameServerInstanceDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSetGameServerInstanceDataRequestEvent((SetGameServerInstanceDataRequest)e.Request);
					return;
				}
				if (type == typeof(SetGameServerInstanceStateRequest) && PlayFabEvents._instance.OnServerSetGameServerInstanceStateRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSetGameServerInstanceStateRequestEvent((SetGameServerInstanceStateRequest)e.Request);
					return;
				}
				if (type == typeof(SetGameServerInstanceTagsRequest) && PlayFabEvents._instance.OnServerSetGameServerInstanceTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSetGameServerInstanceTagsRequestEvent((SetGameServerInstanceTagsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.SetPlayerSecretRequest) && PlayFabEvents._instance.OnServerSetPlayerSecretRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSetPlayerSecretRequestEvent((PlayFab.ServerModels.SetPlayerSecretRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.SetPublisherDataRequest) && PlayFabEvents._instance.OnServerSetPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSetPublisherDataRequestEvent((PlayFab.ServerModels.SetPublisherDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.SetTitleDataRequest) && PlayFabEvents._instance.OnServerSetTitleDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSetTitleDataRequestEvent((PlayFab.ServerModels.SetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.SetTitleDataRequest) && PlayFabEvents._instance.OnServerSetTitleInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSetTitleInternalDataRequestEvent((PlayFab.ServerModels.SetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(SubtractCharacterVirtualCurrencyRequest) && PlayFabEvents._instance.OnServerSubtractCharacterVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSubtractCharacterVirtualCurrencyRequestEvent((SubtractCharacterVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.SubtractUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnServerSubtractUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerSubtractUserVirtualCurrencyRequestEvent((PlayFab.ServerModels.SubtractUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UnlockContainerInstanceRequest) && PlayFabEvents._instance.OnServerUnlockContainerInstanceRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUnlockContainerInstanceRequestEvent((PlayFab.ServerModels.UnlockContainerInstanceRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UnlockContainerItemRequest) && PlayFabEvents._instance.OnServerUnlockContainerItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUnlockContainerItemRequestEvent((PlayFab.ServerModels.UnlockContainerItemRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateAvatarUrlRequest) && PlayFabEvents._instance.OnServerUpdateAvatarUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateAvatarUrlRequestEvent((PlayFab.ServerModels.UpdateAvatarUrlRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateBansRequest) && PlayFabEvents._instance.OnServerUpdateBansRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateBansRequestEvent((PlayFab.ServerModels.UpdateBansRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateCharacterDataRequest) && PlayFabEvents._instance.OnServerUpdateCharacterDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateCharacterDataRequestEvent((PlayFab.ServerModels.UpdateCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateCharacterDataRequest) && PlayFabEvents._instance.OnServerUpdateCharacterInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateCharacterInternalDataRequestEvent((PlayFab.ServerModels.UpdateCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateCharacterDataRequest) && PlayFabEvents._instance.OnServerUpdateCharacterReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateCharacterReadOnlyDataRequestEvent((PlayFab.ServerModels.UpdateCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateCharacterStatisticsRequest) && PlayFabEvents._instance.OnServerUpdateCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateCharacterStatisticsRequestEvent((PlayFab.ServerModels.UpdateCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdatePlayerStatisticsRequest) && PlayFabEvents._instance.OnServerUpdatePlayerStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdatePlayerStatisticsRequestEvent((PlayFab.ServerModels.UpdatePlayerStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateSharedGroupDataRequest) && PlayFabEvents._instance.OnServerUpdateSharedGroupDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateSharedGroupDataRequestEvent((PlayFab.ServerModels.UpdateSharedGroupDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnServerUpdateUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserDataRequestEvent((PlayFab.ServerModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateUserInternalDataRequest) && PlayFabEvents._instance.OnServerUpdateUserInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserInternalDataRequestEvent((PlayFab.ServerModels.UpdateUserInternalDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateUserInventoryItemDataRequest) && PlayFabEvents._instance.OnServerUpdateUserInventoryItemCustomDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserInventoryItemCustomDataRequestEvent((UpdateUserInventoryItemDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnServerUpdateUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserPublisherDataRequestEvent((PlayFab.ServerModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateUserInternalDataRequest) && PlayFabEvents._instance.OnServerUpdateUserPublisherInternalDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserPublisherInternalDataRequestEvent((PlayFab.ServerModels.UpdateUserInternalDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnServerUpdateUserPublisherReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserPublisherReadOnlyDataRequestEvent((PlayFab.ServerModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnServerUpdateUserReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserReadOnlyDataRequestEvent((PlayFab.ServerModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(WriteServerCharacterEventRequest) && PlayFabEvents._instance.OnServerWriteCharacterEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerWriteCharacterEventRequestEvent((WriteServerCharacterEventRequest)e.Request);
					return;
				}
				if (type == typeof(WriteServerPlayerEventRequest) && PlayFabEvents._instance.OnServerWritePlayerEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerWritePlayerEventRequestEvent((WriteServerPlayerEventRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ServerModels.WriteTitleEventRequest) && PlayFabEvents._instance.OnServerWriteTitleEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnServerWriteTitleEventRequestEvent((PlayFab.ServerModels.WriteTitleEventRequest)e.Request);
					return;
				}
				if (type == typeof(AcceptTradeRequest) && PlayFabEvents._instance.OnAcceptTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnAcceptTradeRequestEvent((AcceptTradeRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.AddFriendRequest) && PlayFabEvents._instance.OnAddFriendRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddFriendRequestEvent((PlayFab.ClientModels.AddFriendRequest)e.Request);
					return;
				}
				if (type == typeof(AddGenericIDRequest) && PlayFabEvents._instance.OnAddGenericIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddGenericIDRequestEvent((AddGenericIDRequest)e.Request);
					return;
				}
				if (type == typeof(AddOrUpdateContactEmailRequest) && PlayFabEvents._instance.OnAddOrUpdateContactEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddOrUpdateContactEmailRequestEvent((AddOrUpdateContactEmailRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.AddSharedGroupMembersRequest) && PlayFabEvents._instance.OnAddSharedGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddSharedGroupMembersRequestEvent((PlayFab.ClientModels.AddSharedGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(AddUsernamePasswordRequest) && PlayFabEvents._instance.OnAddUsernamePasswordRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddUsernamePasswordRequestEvent((AddUsernamePasswordRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.AddUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnAddUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddUserVirtualCurrencyRequestEvent((PlayFab.ClientModels.AddUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(AndroidDevicePushNotificationRegistrationRequest) && PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationRequestEvent != null)
				{
					PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationRequestEvent((AndroidDevicePushNotificationRegistrationRequest)e.Request);
					return;
				}
				if (type == typeof(AttributeInstallRequest) && PlayFabEvents._instance.OnAttributeInstallRequestEvent != null)
				{
					PlayFabEvents._instance.OnAttributeInstallRequestEvent((AttributeInstallRequest)e.Request);
					return;
				}
				if (type == typeof(CancelTradeRequest) && PlayFabEvents._instance.OnCancelTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnCancelTradeRequestEvent((CancelTradeRequest)e.Request);
					return;
				}
				if (type == typeof(ConfirmPurchaseRequest) && PlayFabEvents._instance.OnConfirmPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnConfirmPurchaseRequestEvent((ConfirmPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.ConsumeItemRequest) && PlayFabEvents._instance.OnConsumeItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnConsumeItemRequestEvent((PlayFab.ClientModels.ConsumeItemRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.CreateSharedGroupRequest) && PlayFabEvents._instance.OnCreateSharedGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnCreateSharedGroupRequestEvent((PlayFab.ClientModels.CreateSharedGroupRequest)e.Request);
					return;
				}
				if (type == typeof(ExecuteCloudScriptRequest) && PlayFabEvents._instance.OnExecuteCloudScriptRequestEvent != null)
				{
					PlayFabEvents._instance.OnExecuteCloudScriptRequestEvent((ExecuteCloudScriptRequest)e.Request);
					return;
				}
				if (type == typeof(GetAccountInfoRequest) && PlayFabEvents._instance.OnGetAccountInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetAccountInfoRequestEvent((GetAccountInfoRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.ListUsersCharactersRequest) && PlayFabEvents._instance.OnGetAllUsersCharactersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetAllUsersCharactersRequestEvent((PlayFab.ClientModels.ListUsersCharactersRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetCatalogItemsRequest) && PlayFabEvents._instance.OnGetCatalogItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCatalogItemsRequestEvent((PlayFab.ClientModels.GetCatalogItemsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetCharacterDataRequest) && PlayFabEvents._instance.OnGetCharacterDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterDataRequestEvent((PlayFab.ClientModels.GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetCharacterInventoryRequest) && PlayFabEvents._instance.OnGetCharacterInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterInventoryRequestEvent((PlayFab.ClientModels.GetCharacterInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetCharacterLeaderboardRequest) && PlayFabEvents._instance.OnGetCharacterLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterLeaderboardRequestEvent((PlayFab.ClientModels.GetCharacterLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetCharacterDataRequest) && PlayFabEvents._instance.OnGetCharacterReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterReadOnlyDataRequestEvent((PlayFab.ClientModels.GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetCharacterStatisticsRequest) && PlayFabEvents._instance.OnGetCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterStatisticsRequestEvent((PlayFab.ClientModels.GetCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetContentDownloadUrlRequest) && PlayFabEvents._instance.OnGetContentDownloadUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetContentDownloadUrlRequestEvent((PlayFab.ClientModels.GetContentDownloadUrlRequest)e.Request);
					return;
				}
				if (type == typeof(CurrentGamesRequest) && PlayFabEvents._instance.OnGetCurrentGamesRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCurrentGamesRequestEvent((CurrentGamesRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetFriendLeaderboardRequest) && PlayFabEvents._instance.OnGetFriendLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardRequestEvent((PlayFab.ClientModels.GetFriendLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(GetFriendLeaderboardAroundPlayerRequest) && PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerRequestEvent((GetFriendLeaderboardAroundPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetFriendsListRequest) && PlayFabEvents._instance.OnGetFriendsListRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendsListRequestEvent((PlayFab.ClientModels.GetFriendsListRequest)e.Request);
					return;
				}
				if (type == typeof(GameServerRegionsRequest) && PlayFabEvents._instance.OnGetGameServerRegionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetGameServerRegionsRequestEvent((GameServerRegionsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetLeaderboardRequest) && PlayFabEvents._instance.OnGetLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardRequestEvent((PlayFab.ClientModels.GetLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetLeaderboardAroundCharacterRequest) && PlayFabEvents._instance.OnGetLeaderboardAroundCharacterRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundCharacterRequestEvent((PlayFab.ClientModels.GetLeaderboardAroundCharacterRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardAroundPlayerRequest) && PlayFabEvents._instance.OnGetLeaderboardAroundPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundPlayerRequestEvent((GetLeaderboardAroundPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetLeaderboardForUsersCharactersRequest) && PlayFabEvents._instance.OnGetLeaderboardForUserCharactersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardForUserCharactersRequestEvent((PlayFab.ClientModels.GetLeaderboardForUsersCharactersRequest)e.Request);
					return;
				}
				if (type == typeof(GetPaymentTokenRequest) && PlayFabEvents._instance.OnGetPaymentTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPaymentTokenRequestEvent((GetPaymentTokenRequest)e.Request);
					return;
				}
				if (type == typeof(GetPhotonAuthenticationTokenRequest) && PlayFabEvents._instance.OnGetPhotonAuthenticationTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPhotonAuthenticationTokenRequestEvent((GetPhotonAuthenticationTokenRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetPlayerCombinedInfoRequest) && PlayFabEvents._instance.OnGetPlayerCombinedInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerCombinedInfoRequestEvent((PlayFab.ClientModels.GetPlayerCombinedInfoRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetPlayerProfileRequest) && PlayFabEvents._instance.OnGetPlayerProfileRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerProfileRequestEvent((PlayFab.ClientModels.GetPlayerProfileRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerSegmentsRequest) && PlayFabEvents._instance.OnGetPlayerSegmentsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerSegmentsRequestEvent((GetPlayerSegmentsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetPlayerStatisticsRequest) && PlayFabEvents._instance.OnGetPlayerStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticsRequestEvent((PlayFab.ClientModels.GetPlayerStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetPlayerStatisticVersionsRequest) && PlayFabEvents._instance.OnGetPlayerStatisticVersionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticVersionsRequestEvent((PlayFab.ClientModels.GetPlayerStatisticVersionsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetPlayerTagsRequest) && PlayFabEvents._instance.OnGetPlayerTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTagsRequestEvent((PlayFab.ClientModels.GetPlayerTagsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerTradesRequest) && PlayFabEvents._instance.OnGetPlayerTradesRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTradesRequestEvent((GetPlayerTradesRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetPlayFabIDsFromFacebookIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsRequestEvent((PlayFab.ClientModels.GetPlayFabIDsFromFacebookIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGameCenterIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsRequestEvent((GetPlayFabIDsFromGameCenterIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGenericIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsRequestEvent((GetPlayFabIDsFromGenericIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGoogleIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsRequestEvent((GetPlayFabIDsFromGoogleIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromKongregateIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsRequestEvent((GetPlayFabIDsFromKongregateIDsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetPlayFabIDsFromSteamIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsRequestEvent((PlayFab.ClientModels.GetPlayFabIDsFromSteamIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromTwitchIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsRequestEvent((GetPlayFabIDsFromTwitchIDsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetPublisherDataRequest) && PlayFabEvents._instance.OnGetPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPublisherDataRequestEvent((PlayFab.ClientModels.GetPublisherDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetPurchaseRequest) && PlayFabEvents._instance.OnGetPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPurchaseRequestEvent((GetPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetSharedGroupDataRequest) && PlayFabEvents._instance.OnGetSharedGroupDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetSharedGroupDataRequestEvent((PlayFab.ClientModels.GetSharedGroupDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetStoreItemsRequest) && PlayFabEvents._instance.OnGetStoreItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetStoreItemsRequestEvent((PlayFab.ClientModels.GetStoreItemsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetTimeRequest) && PlayFabEvents._instance.OnGetTimeRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTimeRequestEvent((PlayFab.ClientModels.GetTimeRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetTitleDataRequest) && PlayFabEvents._instance.OnGetTitleDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleDataRequestEvent((PlayFab.ClientModels.GetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetTitleNewsRequest) && PlayFabEvents._instance.OnGetTitleNewsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleNewsRequestEvent((PlayFab.ClientModels.GetTitleNewsRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitlePublicKeyRequest) && PlayFabEvents._instance.OnGetTitlePublicKeyRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitlePublicKeyRequestEvent((GetTitlePublicKeyRequest)e.Request);
					return;
				}
				if (type == typeof(GetTradeStatusRequest) && PlayFabEvents._instance.OnGetTradeStatusRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTradeStatusRequestEvent((GetTradeStatusRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetUserDataRequest) && PlayFabEvents._instance.OnGetUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserDataRequestEvent((PlayFab.ClientModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetUserInventoryRequest) && PlayFabEvents._instance.OnGetUserInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserInventoryRequestEvent((PlayFab.ClientModels.GetUserInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetUserDataRequest) && PlayFabEvents._instance.OnGetUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherDataRequestEvent((PlayFab.ClientModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetUserDataRequest) && PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataRequestEvent((PlayFab.ClientModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GetUserDataRequest) && PlayFabEvents._instance.OnGetUserReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserReadOnlyDataRequestEvent((PlayFab.ClientModels.GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetWindowsHelloChallengeRequest) && PlayFabEvents._instance.OnGetWindowsHelloChallengeRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetWindowsHelloChallengeRequestEvent((GetWindowsHelloChallengeRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.GrantCharacterToUserRequest) && PlayFabEvents._instance.OnGrantCharacterToUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnGrantCharacterToUserRequestEvent((PlayFab.ClientModels.GrantCharacterToUserRequest)e.Request);
					return;
				}
				if (type == typeof(LinkAndroidDeviceIDRequest) && PlayFabEvents._instance.OnLinkAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkAndroidDeviceIDRequestEvent((LinkAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkCustomIDRequest) && PlayFabEvents._instance.OnLinkCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkCustomIDRequestEvent((LinkCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkFacebookAccountRequest) && PlayFabEvents._instance.OnLinkFacebookAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookAccountRequestEvent((LinkFacebookAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkGameCenterAccountRequest) && PlayFabEvents._instance.OnLinkGameCenterAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkGameCenterAccountRequestEvent((LinkGameCenterAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkGoogleAccountRequest) && PlayFabEvents._instance.OnLinkGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkGoogleAccountRequestEvent((LinkGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkIOSDeviceIDRequest) && PlayFabEvents._instance.OnLinkIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkIOSDeviceIDRequestEvent((LinkIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkKongregateAccountRequest) && PlayFabEvents._instance.OnLinkKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkKongregateRequestEvent((LinkKongregateAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkSteamAccountRequest) && PlayFabEvents._instance.OnLinkSteamAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkSteamAccountRequestEvent((LinkSteamAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkTwitchAccountRequest) && PlayFabEvents._instance.OnLinkTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkTwitchRequestEvent((LinkTwitchAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkWindowsHelloAccountRequest) && PlayFabEvents._instance.OnLinkWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkWindowsHelloRequestEvent((LinkWindowsHelloAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithAndroidDeviceIDRequest) && PlayFabEvents._instance.OnLoginWithAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithAndroidDeviceIDRequestEvent((LoginWithAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithCustomIDRequest) && PlayFabEvents._instance.OnLoginWithCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithCustomIDRequestEvent((LoginWithCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithEmailAddressRequest) && PlayFabEvents._instance.OnLoginWithEmailAddressRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithEmailAddressRequestEvent((LoginWithEmailAddressRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithFacebookRequest) && PlayFabEvents._instance.OnLoginWithFacebookRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithFacebookRequestEvent((LoginWithFacebookRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithGameCenterRequest) && PlayFabEvents._instance.OnLoginWithGameCenterRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithGameCenterRequestEvent((LoginWithGameCenterRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithGoogleAccountRequest) && PlayFabEvents._instance.OnLoginWithGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithGoogleAccountRequestEvent((LoginWithGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithIOSDeviceIDRequest) && PlayFabEvents._instance.OnLoginWithIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithIOSDeviceIDRequestEvent((LoginWithIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithKongregateRequest) && PlayFabEvents._instance.OnLoginWithKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithKongregateRequestEvent((LoginWithKongregateRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithPlayFabRequest) && PlayFabEvents._instance.OnLoginWithPlayFabRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithPlayFabRequestEvent((LoginWithPlayFabRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithSteamRequest) && PlayFabEvents._instance.OnLoginWithSteamRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithSteamRequestEvent((LoginWithSteamRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithTwitchRequest) && PlayFabEvents._instance.OnLoginWithTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithTwitchRequestEvent((LoginWithTwitchRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithWindowsHelloRequest) && PlayFabEvents._instance.OnLoginWithWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithWindowsHelloRequestEvent((LoginWithWindowsHelloRequest)e.Request);
					return;
				}
				if (type == typeof(MatchmakeRequest) && PlayFabEvents._instance.OnMatchmakeRequestEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakeRequestEvent((MatchmakeRequest)e.Request);
					return;
				}
				if (type == typeof(OpenTradeRequest) && PlayFabEvents._instance.OnOpenTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnOpenTradeRequestEvent((OpenTradeRequest)e.Request);
					return;
				}
				if (type == typeof(PayForPurchaseRequest) && PlayFabEvents._instance.OnPayForPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnPayForPurchaseRequestEvent((PayForPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(PurchaseItemRequest) && PlayFabEvents._instance.OnPurchaseItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnPurchaseItemRequestEvent((PurchaseItemRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.RedeemCouponRequest) && PlayFabEvents._instance.OnRedeemCouponRequestEvent != null)
				{
					PlayFabEvents._instance.OnRedeemCouponRequestEvent((PlayFab.ClientModels.RedeemCouponRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterForIOSPushNotificationRequest) && PlayFabEvents._instance.OnRegisterForIOSPushNotificationRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterForIOSPushNotificationRequestEvent((RegisterForIOSPushNotificationRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterPlayFabUserRequest) && PlayFabEvents._instance.OnRegisterPlayFabUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterPlayFabUserRequestEvent((RegisterPlayFabUserRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterWithWindowsHelloRequest) && PlayFabEvents._instance.OnRegisterWithWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterWithWindowsHelloRequestEvent((RegisterWithWindowsHelloRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveContactEmailRequest) && PlayFabEvents._instance.OnRemoveContactEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveContactEmailRequestEvent((RemoveContactEmailRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.RemoveFriendRequest) && PlayFabEvents._instance.OnRemoveFriendRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveFriendRequestEvent((PlayFab.ClientModels.RemoveFriendRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveGenericIDRequest) && PlayFabEvents._instance.OnRemoveGenericIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveGenericIDRequestEvent((RemoveGenericIDRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.RemoveSharedGroupMembersRequest) && PlayFabEvents._instance.OnRemoveSharedGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveSharedGroupMembersRequestEvent((PlayFab.ClientModels.RemoveSharedGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(DeviceInfoRequest) && PlayFabEvents._instance.OnReportDeviceInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnReportDeviceInfoRequestEvent((DeviceInfoRequest)e.Request);
					return;
				}
				if (type == typeof(ReportPlayerClientRequest) && PlayFabEvents._instance.OnReportPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnReportPlayerRequestEvent((ReportPlayerClientRequest)e.Request);
					return;
				}
				if (type == typeof(RestoreIOSPurchasesRequest) && PlayFabEvents._instance.OnRestoreIOSPurchasesRequestEvent != null)
				{
					PlayFabEvents._instance.OnRestoreIOSPurchasesRequestEvent((RestoreIOSPurchasesRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.SendAccountRecoveryEmailRequest) && PlayFabEvents._instance.OnSendAccountRecoveryEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnSendAccountRecoveryEmailRequestEvent((PlayFab.ClientModels.SendAccountRecoveryEmailRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.SetFriendTagsRequest) && PlayFabEvents._instance.OnSetFriendTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnSetFriendTagsRequestEvent((PlayFab.ClientModels.SetFriendTagsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.SetPlayerSecretRequest) && PlayFabEvents._instance.OnSetPlayerSecretRequestEvent != null)
				{
					PlayFabEvents._instance.OnSetPlayerSecretRequestEvent((PlayFab.ClientModels.SetPlayerSecretRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.StartGameRequest) && PlayFabEvents._instance.OnStartGameRequestEvent != null)
				{
					PlayFabEvents._instance.OnStartGameRequestEvent((PlayFab.ClientModels.StartGameRequest)e.Request);
					return;
				}
				if (type == typeof(StartPurchaseRequest) && PlayFabEvents._instance.OnStartPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnStartPurchaseRequestEvent((StartPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.SubtractUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnSubtractUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnSubtractUserVirtualCurrencyRequestEvent((PlayFab.ClientModels.SubtractUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkAndroidDeviceIDRequest) && PlayFabEvents._instance.OnUnlinkAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkAndroidDeviceIDRequestEvent((UnlinkAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkCustomIDRequest) && PlayFabEvents._instance.OnUnlinkCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkCustomIDRequestEvent((UnlinkCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkFacebookAccountRequest) && PlayFabEvents._instance.OnUnlinkFacebookAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookAccountRequestEvent((UnlinkFacebookAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkGameCenterAccountRequest) && PlayFabEvents._instance.OnUnlinkGameCenterAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGameCenterAccountRequestEvent((UnlinkGameCenterAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkGoogleAccountRequest) && PlayFabEvents._instance.OnUnlinkGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGoogleAccountRequestEvent((UnlinkGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkIOSDeviceIDRequest) && PlayFabEvents._instance.OnUnlinkIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkIOSDeviceIDRequestEvent((UnlinkIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkKongregateAccountRequest) && PlayFabEvents._instance.OnUnlinkKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkKongregateRequestEvent((UnlinkKongregateAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkSteamAccountRequest) && PlayFabEvents._instance.OnUnlinkSteamAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkSteamAccountRequestEvent((UnlinkSteamAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkTwitchAccountRequest) && PlayFabEvents._instance.OnUnlinkTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkTwitchRequestEvent((UnlinkTwitchAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkWindowsHelloAccountRequest) && PlayFabEvents._instance.OnUnlinkWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkWindowsHelloRequestEvent((UnlinkWindowsHelloAccountRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UnlockContainerInstanceRequest) && PlayFabEvents._instance.OnUnlockContainerInstanceRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerInstanceRequestEvent((PlayFab.ClientModels.UnlockContainerInstanceRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UnlockContainerItemRequest) && PlayFabEvents._instance.OnUnlockContainerItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerItemRequestEvent((PlayFab.ClientModels.UnlockContainerItemRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UpdateAvatarUrlRequest) && PlayFabEvents._instance.OnUpdateAvatarUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateAvatarUrlRequestEvent((PlayFab.ClientModels.UpdateAvatarUrlRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UpdateCharacterDataRequest) && PlayFabEvents._instance.OnUpdateCharacterDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterDataRequestEvent((PlayFab.ClientModels.UpdateCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UpdateCharacterStatisticsRequest) && PlayFabEvents._instance.OnUpdateCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterStatisticsRequestEvent((PlayFab.ClientModels.UpdateCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UpdatePlayerStatisticsRequest) && PlayFabEvents._instance.OnUpdatePlayerStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdatePlayerStatisticsRequestEvent((PlayFab.ClientModels.UpdatePlayerStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UpdateSharedGroupDataRequest) && PlayFabEvents._instance.OnUpdateSharedGroupDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateSharedGroupDataRequestEvent((PlayFab.ClientModels.UpdateSharedGroupDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnUpdateUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserDataRequestEvent((PlayFab.ClientModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UpdateUserDataRequest) && PlayFabEvents._instance.OnUpdateUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserPublisherDataRequestEvent((PlayFab.ClientModels.UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest) && PlayFabEvents._instance.OnUpdateUserTitleDisplayNameRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserTitleDisplayNameRequestEvent((PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateAmazonReceiptRequest) && PlayFabEvents._instance.OnValidateAmazonIAPReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateAmazonIAPReceiptRequestEvent((ValidateAmazonReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateGooglePlayPurchaseRequest) && PlayFabEvents._instance.OnValidateGooglePlayPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateGooglePlayPurchaseRequestEvent((ValidateGooglePlayPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateIOSReceiptRequest) && PlayFabEvents._instance.OnValidateIOSReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateIOSReceiptRequestEvent((ValidateIOSReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateWindowsReceiptRequest) && PlayFabEvents._instance.OnValidateWindowsStoreReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateWindowsStoreReceiptRequestEvent((ValidateWindowsReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(WriteClientCharacterEventRequest) && PlayFabEvents._instance.OnWriteCharacterEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWriteCharacterEventRequestEvent((WriteClientCharacterEventRequest)e.Request);
					return;
				}
				if (type == typeof(WriteClientPlayerEventRequest) && PlayFabEvents._instance.OnWritePlayerEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWritePlayerEventRequestEvent((WriteClientPlayerEventRequest)e.Request);
					return;
				}
				if (type == typeof(PlayFab.ClientModels.WriteTitleEventRequest) && PlayFabEvents._instance.OnWriteTitleEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWriteTitleEventRequestEvent((PlayFab.ClientModels.WriteTitleEventRequest)e.Request);
					return;
				}
			}
			else
			{
				Type type2 = e.Result.GetType();
				if (type2 == typeof(PlayFab.AdminModels.EmptyResult) && PlayFabEvents._instance.OnAdminAbortTaskInstanceResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminAbortTaskInstanceResultEvent((PlayFab.AdminModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(AddNewsResult) && PlayFabEvents._instance.OnAdminAddNewsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddNewsResultEvent((AddNewsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.AddPlayerTagResult) && PlayFabEvents._instance.OnAdminAddPlayerTagResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddPlayerTagResultEvent((PlayFab.AdminModels.AddPlayerTagResult)e.Result);
					return;
				}
				if (type2 == typeof(AddServerBuildResult) && PlayFabEvents._instance.OnAdminAddServerBuildResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddServerBuildResultEvent((AddServerBuildResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnAdminAddUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddUserVirtualCurrencyResultEvent((PlayFab.AdminModels.ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(BlankResult) && PlayFabEvents._instance.OnAdminAddVirtualCurrencyTypesResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminAddVirtualCurrencyTypesResultEvent((BlankResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.BanUsersResult) && PlayFabEvents._instance.OnAdminBanUsersResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminBanUsersResultEvent((PlayFab.AdminModels.BanUsersResult)e.Result);
					return;
				}
				if (type2 == typeof(CheckLimitedEditionItemAvailabilityResult) && PlayFabEvents._instance.OnAdminCheckLimitedEditionItemAvailabilityResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminCheckLimitedEditionItemAvailabilityResultEvent((CheckLimitedEditionItemAvailabilityResult)e.Result);
					return;
				}
				if (type2 == typeof(CreateTaskResult) && PlayFabEvents._instance.OnAdminCreateActionsOnPlayersInSegmentTaskResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminCreateActionsOnPlayersInSegmentTaskResultEvent((CreateTaskResult)e.Result);
					return;
				}
				if (type2 == typeof(CreateTaskResult) && PlayFabEvents._instance.OnAdminCreateCloudScriptTaskResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminCreateCloudScriptTaskResultEvent((CreateTaskResult)e.Result);
					return;
				}
				if (type2 == typeof(CreatePlayerSharedSecretResult) && PlayFabEvents._instance.OnAdminCreatePlayerSharedSecretResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminCreatePlayerSharedSecretResultEvent((CreatePlayerSharedSecretResult)e.Result);
					return;
				}
				if (type2 == typeof(CreatePlayerStatisticDefinitionResult) && PlayFabEvents._instance.OnAdminCreatePlayerStatisticDefinitionResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminCreatePlayerStatisticDefinitionResultEvent((CreatePlayerStatisticDefinitionResult)e.Result);
					return;
				}
				if (type2 == typeof(BlankResult) && PlayFabEvents._instance.OnAdminDeleteContentResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeleteContentResultEvent((BlankResult)e.Result);
					return;
				}
				if (type2 == typeof(DeletePlayerResult) && PlayFabEvents._instance.OnAdminDeletePlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeletePlayerResultEvent((DeletePlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(DeletePlayerSharedSecretResult) && PlayFabEvents._instance.OnAdminDeletePlayerSharedSecretResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeletePlayerSharedSecretResultEvent((DeletePlayerSharedSecretResult)e.Result);
					return;
				}
				if (type2 == typeof(DeleteStoreResult) && PlayFabEvents._instance.OnAdminDeleteStoreResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeleteStoreResultEvent((DeleteStoreResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.EmptyResult) && PlayFabEvents._instance.OnAdminDeleteTaskResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeleteTaskResultEvent((PlayFab.AdminModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(DeleteTitleResult) && PlayFabEvents._instance.OnAdminDeleteTitleResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminDeleteTitleResultEvent((DeleteTitleResult)e.Result);
					return;
				}
				if (type2 == typeof(GetActionsOnPlayersInSegmentTaskInstanceResult) && PlayFabEvents._instance.OnAdminGetActionsOnPlayersInSegmentTaskInstanceResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetActionsOnPlayersInSegmentTaskInstanceResultEvent((GetActionsOnPlayersInSegmentTaskInstanceResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetAllSegmentsResult) && PlayFabEvents._instance.OnAdminGetAllSegmentsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetAllSegmentsResultEvent((PlayFab.AdminModels.GetAllSegmentsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetCatalogItemsResult) && PlayFabEvents._instance.OnAdminGetCatalogItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetCatalogItemsResultEvent((PlayFab.AdminModels.GetCatalogItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCloudScriptRevisionResult) && PlayFabEvents._instance.OnAdminGetCloudScriptRevisionResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetCloudScriptRevisionResultEvent((GetCloudScriptRevisionResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCloudScriptTaskInstanceResult) && PlayFabEvents._instance.OnAdminGetCloudScriptTaskInstanceResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetCloudScriptTaskInstanceResultEvent((GetCloudScriptTaskInstanceResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCloudScriptVersionsResult) && PlayFabEvents._instance.OnAdminGetCloudScriptVersionsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetCloudScriptVersionsResultEvent((GetCloudScriptVersionsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetContentListResult) && PlayFabEvents._instance.OnAdminGetContentListResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetContentListResultEvent((GetContentListResult)e.Result);
					return;
				}
				if (type2 == typeof(GetContentUploadUrlResult) && PlayFabEvents._instance.OnAdminGetContentUploadUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetContentUploadUrlResultEvent((GetContentUploadUrlResult)e.Result);
					return;
				}
				if (type2 == typeof(GetDataReportResult) && PlayFabEvents._instance.OnAdminGetDataReportResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetDataReportResultEvent((GetDataReportResult)e.Result);
					return;
				}
				if (type2 == typeof(GetMatchmakerGameInfoResult) && PlayFabEvents._instance.OnAdminGetMatchmakerGameInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetMatchmakerGameInfoResultEvent((GetMatchmakerGameInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(GetMatchmakerGameModesResult) && PlayFabEvents._instance.OnAdminGetMatchmakerGameModesResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetMatchmakerGameModesResultEvent((GetMatchmakerGameModesResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerIdFromAuthTokenResult) && PlayFabEvents._instance.OnAdminGetPlayerIdFromAuthTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerIdFromAuthTokenResultEvent((GetPlayerIdFromAuthTokenResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetPlayerProfileResult) && PlayFabEvents._instance.OnAdminGetPlayerProfileResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerProfileResultEvent((PlayFab.AdminModels.GetPlayerProfileResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetPlayerSegmentsResult) && PlayFabEvents._instance.OnAdminGetPlayerSegmentsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerSegmentsResultEvent((PlayFab.AdminModels.GetPlayerSegmentsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerSharedSecretsResult) && PlayFabEvents._instance.OnAdminGetPlayerSharedSecretsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerSharedSecretsResultEvent((GetPlayerSharedSecretsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetPlayersInSegmentResult) && PlayFabEvents._instance.OnAdminGetPlayersInSegmentResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayersInSegmentResultEvent((PlayFab.AdminModels.GetPlayersInSegmentResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerStatisticDefinitionsResult) && PlayFabEvents._instance.OnAdminGetPlayerStatisticDefinitionsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerStatisticDefinitionsResultEvent((GetPlayerStatisticDefinitionsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetPlayerStatisticVersionsResult) && PlayFabEvents._instance.OnAdminGetPlayerStatisticVersionsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerStatisticVersionsResultEvent((PlayFab.AdminModels.GetPlayerStatisticVersionsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetPlayerTagsResult) && PlayFabEvents._instance.OnAdminGetPlayerTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPlayerTagsResultEvent((PlayFab.AdminModels.GetPlayerTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPolicyResponse) && PlayFabEvents._instance.OnAdminGetPolicyResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPolicyResultEvent((GetPolicyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetPublisherDataResult) && PlayFabEvents._instance.OnAdminGetPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetPublisherDataResultEvent((PlayFab.AdminModels.GetPublisherDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetRandomResultTablesResult) && PlayFabEvents._instance.OnAdminGetRandomResultTablesResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetRandomResultTablesResultEvent((PlayFab.AdminModels.GetRandomResultTablesResult)e.Result);
					return;
				}
				if (type2 == typeof(GetServerBuildInfoResult) && PlayFabEvents._instance.OnAdminGetServerBuildInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetServerBuildInfoResultEvent((GetServerBuildInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(GetServerBuildUploadURLResult) && PlayFabEvents._instance.OnAdminGetServerBuildUploadUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetServerBuildUploadUrlResultEvent((GetServerBuildUploadURLResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetStoreItemsResult) && PlayFabEvents._instance.OnAdminGetStoreItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetStoreItemsResultEvent((PlayFab.AdminModels.GetStoreItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTaskInstancesResult) && PlayFabEvents._instance.OnAdminGetTaskInstancesResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetTaskInstancesResultEvent((GetTaskInstancesResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTasksResult) && PlayFabEvents._instance.OnAdminGetTasksResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetTasksResultEvent((GetTasksResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetTitleDataResult) && PlayFabEvents._instance.OnAdminGetTitleDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetTitleDataResultEvent((PlayFab.AdminModels.GetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetTitleDataResult) && PlayFabEvents._instance.OnAdminGetTitleInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetTitleInternalDataResultEvent((PlayFab.AdminModels.GetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(LookupUserAccountInfoResult) && PlayFabEvents._instance.OnAdminGetUserAccountInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserAccountInfoResultEvent((LookupUserAccountInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetUserBansResult) && PlayFabEvents._instance.OnAdminGetUserBansResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserBansResultEvent((PlayFab.AdminModels.GetUserBansResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetUserDataResult) && PlayFabEvents._instance.OnAdminGetUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserDataResultEvent((PlayFab.AdminModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetUserDataResult) && PlayFabEvents._instance.OnAdminGetUserInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserInternalDataResultEvent((PlayFab.AdminModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetUserInventoryResult) && PlayFabEvents._instance.OnAdminGetUserInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserInventoryResultEvent((PlayFab.AdminModels.GetUserInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetUserDataResult) && PlayFabEvents._instance.OnAdminGetUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserPublisherDataResultEvent((PlayFab.AdminModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetUserDataResult) && PlayFabEvents._instance.OnAdminGetUserPublisherInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserPublisherInternalDataResultEvent((PlayFab.AdminModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetUserDataResult) && PlayFabEvents._instance.OnAdminGetUserPublisherReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserPublisherReadOnlyDataResultEvent((PlayFab.AdminModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GetUserDataResult) && PlayFabEvents._instance.OnAdminGetUserReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGetUserReadOnlyDataResultEvent((PlayFab.AdminModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.GrantItemsToUsersResult) && PlayFabEvents._instance.OnAdminGrantItemsToUsersResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminGrantItemsToUsersResultEvent((PlayFab.AdminModels.GrantItemsToUsersResult)e.Result);
					return;
				}
				if (type2 == typeof(IncrementLimitedEditionItemAvailabilityResult) && PlayFabEvents._instance.OnAdminIncrementLimitedEditionItemAvailabilityResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminIncrementLimitedEditionItemAvailabilityResultEvent((IncrementLimitedEditionItemAvailabilityResult)e.Result);
					return;
				}
				if (type2 == typeof(IncrementPlayerStatisticVersionResult) && PlayFabEvents._instance.OnAdminIncrementPlayerStatisticVersionResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminIncrementPlayerStatisticVersionResultEvent((IncrementPlayerStatisticVersionResult)e.Result);
					return;
				}
				if (type2 == typeof(ListBuildsResult) && PlayFabEvents._instance.OnAdminListServerBuildsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminListServerBuildsResultEvent((ListBuildsResult)e.Result);
					return;
				}
				if (type2 == typeof(ListVirtualCurrencyTypesResult) && PlayFabEvents._instance.OnAdminListVirtualCurrencyTypesResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminListVirtualCurrencyTypesResultEvent((ListVirtualCurrencyTypesResult)e.Result);
					return;
				}
				if (type2 == typeof(ModifyMatchmakerGameModesResult) && PlayFabEvents._instance.OnAdminModifyMatchmakerGameModesResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminModifyMatchmakerGameModesResultEvent((ModifyMatchmakerGameModesResult)e.Result);
					return;
				}
				if (type2 == typeof(ModifyServerBuildResult) && PlayFabEvents._instance.OnAdminModifyServerBuildResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminModifyServerBuildResultEvent((ModifyServerBuildResult)e.Result);
					return;
				}
				if (type2 == typeof(RefundPurchaseResponse) && PlayFabEvents._instance.OnAdminRefundPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminRefundPurchaseResultEvent((RefundPurchaseResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.RemovePlayerTagResult) && PlayFabEvents._instance.OnAdminRemovePlayerTagResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminRemovePlayerTagResultEvent((PlayFab.AdminModels.RemovePlayerTagResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveServerBuildResult) && PlayFabEvents._instance.OnAdminRemoveServerBuildResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminRemoveServerBuildResultEvent((RemoveServerBuildResult)e.Result);
					return;
				}
				if (type2 == typeof(BlankResult) && PlayFabEvents._instance.OnAdminRemoveVirtualCurrencyTypesResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminRemoveVirtualCurrencyTypesResultEvent((BlankResult)e.Result);
					return;
				}
				if (type2 == typeof(ResetCharacterStatisticsResult) && PlayFabEvents._instance.OnAdminResetCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminResetCharacterStatisticsResultEvent((ResetCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(ResetPasswordResult) && PlayFabEvents._instance.OnAdminResetPasswordResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminResetPasswordResultEvent((ResetPasswordResult)e.Result);
					return;
				}
				if (type2 == typeof(ResetUserStatisticsResult) && PlayFabEvents._instance.OnAdminResetUserStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminResetUserStatisticsResultEvent((ResetUserStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(ResolvePurchaseDisputeResponse) && PlayFabEvents._instance.OnAdminResolvePurchaseDisputeResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminResolvePurchaseDisputeResultEvent((ResolvePurchaseDisputeResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.RevokeAllBansForUserResult) && PlayFabEvents._instance.OnAdminRevokeAllBansForUserResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminRevokeAllBansForUserResultEvent((PlayFab.AdminModels.RevokeAllBansForUserResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.RevokeBansResult) && PlayFabEvents._instance.OnAdminRevokeBansResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminRevokeBansResultEvent((PlayFab.AdminModels.RevokeBansResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.RevokeInventoryResult) && PlayFabEvents._instance.OnAdminRevokeInventoryItemResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminRevokeInventoryItemResultEvent((PlayFab.AdminModels.RevokeInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(RunTaskResult) && PlayFabEvents._instance.OnAdminRunTaskResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminRunTaskResultEvent((RunTaskResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.SendAccountRecoveryEmailResult) && PlayFabEvents._instance.OnAdminSendAccountRecoveryEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSendAccountRecoveryEmailResultEvent((PlayFab.AdminModels.SendAccountRecoveryEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateCatalogItemsResult) && PlayFabEvents._instance.OnAdminSetCatalogItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetCatalogItemsResultEvent((UpdateCatalogItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.SetPlayerSecretResult) && PlayFabEvents._instance.OnAdminSetPlayerSecretResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetPlayerSecretResultEvent((PlayFab.AdminModels.SetPlayerSecretResult)e.Result);
					return;
				}
				if (type2 == typeof(SetPublishedRevisionResult) && PlayFabEvents._instance.OnAdminSetPublishedRevisionResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetPublishedRevisionResultEvent((SetPublishedRevisionResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.SetPublisherDataResult) && PlayFabEvents._instance.OnAdminSetPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetPublisherDataResultEvent((PlayFab.AdminModels.SetPublisherDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateStoreItemsResult) && PlayFabEvents._instance.OnAdminSetStoreItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetStoreItemsResultEvent((UpdateStoreItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.SetTitleDataResult) && PlayFabEvents._instance.OnAdminSetTitleDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetTitleDataResultEvent((PlayFab.AdminModels.SetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.SetTitleDataResult) && PlayFabEvents._instance.OnAdminSetTitleInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetTitleInternalDataResultEvent((PlayFab.AdminModels.SetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(SetupPushNotificationResult) && PlayFabEvents._instance.OnAdminSetupPushNotificationResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSetupPushNotificationResultEvent((SetupPushNotificationResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnAdminSubtractUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminSubtractUserVirtualCurrencyResultEvent((PlayFab.AdminModels.ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.UpdateBansResult) && PlayFabEvents._instance.OnAdminUpdateBansResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateBansResultEvent((PlayFab.AdminModels.UpdateBansResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateCatalogItemsResult) && PlayFabEvents._instance.OnAdminUpdateCatalogItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateCatalogItemsResultEvent((UpdateCatalogItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateCloudScriptResult) && PlayFabEvents._instance.OnAdminUpdateCloudScriptResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateCloudScriptResultEvent((UpdateCloudScriptResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdatePlayerSharedSecretResult) && PlayFabEvents._instance.OnAdminUpdatePlayerSharedSecretResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdatePlayerSharedSecretResultEvent((UpdatePlayerSharedSecretResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdatePlayerStatisticDefinitionResult) && PlayFabEvents._instance.OnAdminUpdatePlayerStatisticDefinitionResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdatePlayerStatisticDefinitionResultEvent((UpdatePlayerStatisticDefinitionResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdatePolicyResponse) && PlayFabEvents._instance.OnAdminUpdatePolicyResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdatePolicyResultEvent((UpdatePolicyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UpdateRandomResultTablesResult) && PlayFabEvents._instance.OnAdminUpdateRandomResultTablesResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateRandomResultTablesResultEvent((UpdateRandomResultTablesResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateStoreItemsResult) && PlayFabEvents._instance.OnAdminUpdateStoreItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateStoreItemsResultEvent((UpdateStoreItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.EmptyResult) && PlayFabEvents._instance.OnAdminUpdateTaskResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateTaskResultEvent((PlayFab.AdminModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.UpdateUserDataResult) && PlayFabEvents._instance.OnAdminUpdateUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserDataResultEvent((PlayFab.AdminModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.UpdateUserDataResult) && PlayFabEvents._instance.OnAdminUpdateUserInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserInternalDataResultEvent((PlayFab.AdminModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.UpdateUserDataResult) && PlayFabEvents._instance.OnAdminUpdateUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserPublisherDataResultEvent((PlayFab.AdminModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.UpdateUserDataResult) && PlayFabEvents._instance.OnAdminUpdateUserPublisherInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserPublisherInternalDataResultEvent((PlayFab.AdminModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.UpdateUserDataResult) && PlayFabEvents._instance.OnAdminUpdateUserPublisherReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserPublisherReadOnlyDataResultEvent((PlayFab.AdminModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.UpdateUserDataResult) && PlayFabEvents._instance.OnAdminUpdateUserReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserReadOnlyDataResultEvent((PlayFab.AdminModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.AdminModels.UpdateUserTitleDisplayNameResult) && PlayFabEvents._instance.OnAdminUpdateUserTitleDisplayNameResultEvent != null)
				{
					PlayFabEvents._instance.OnAdminUpdateUserTitleDisplayNameResultEvent((PlayFab.AdminModels.UpdateUserTitleDisplayNameResult)e.Result);
					return;
				}
				if (type2 == typeof(AuthUserResponse) && PlayFabEvents._instance.OnMatchmakerAuthUserResultEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerAuthUserResultEvent((AuthUserResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayerJoinedResponse) && PlayFabEvents._instance.OnMatchmakerPlayerJoinedResultEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerPlayerJoinedResultEvent((PlayerJoinedResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayerLeftResponse) && PlayFabEvents._instance.OnMatchmakerPlayerLeftResultEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerPlayerLeftResultEvent((PlayerLeftResponse)e.Result);
					return;
				}
				if (type2 == typeof(StartGameResponse) && PlayFabEvents._instance.OnMatchmakerStartGameResultEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerStartGameResultEvent((StartGameResponse)e.Result);
					return;
				}
				if (type2 == typeof(UserInfoResponse) && PlayFabEvents._instance.OnMatchmakerUserInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakerUserInfoResultEvent((UserInfoResponse)e.Result);
					return;
				}
				if (type2 == typeof(ModifyCharacterVirtualCurrencyResult) && PlayFabEvents._instance.OnServerAddCharacterVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnServerAddCharacterVirtualCurrencyResultEvent((ModifyCharacterVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.EmptyResult) && PlayFabEvents._instance.OnServerAddFriendResultEvent != null)
				{
					PlayFabEvents._instance.OnServerAddFriendResultEvent((PlayFab.ServerModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.AddPlayerTagResult) && PlayFabEvents._instance.OnServerAddPlayerTagResultEvent != null)
				{
					PlayFabEvents._instance.OnServerAddPlayerTagResultEvent((PlayFab.ServerModels.AddPlayerTagResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.AddSharedGroupMembersResult) && PlayFabEvents._instance.OnServerAddSharedGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnServerAddSharedGroupMembersResultEvent((PlayFab.ServerModels.AddSharedGroupMembersResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnServerAddUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnServerAddUserVirtualCurrencyResultEvent((PlayFab.ServerModels.ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(AuthenticateSessionTicketResult) && PlayFabEvents._instance.OnServerAuthenticateSessionTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnServerAuthenticateSessionTicketResultEvent((AuthenticateSessionTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(AwardSteamAchievementResult) && PlayFabEvents._instance.OnServerAwardSteamAchievementResultEvent != null)
				{
					PlayFabEvents._instance.OnServerAwardSteamAchievementResultEvent((AwardSteamAchievementResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.BanUsersResult) && PlayFabEvents._instance.OnServerBanUsersResultEvent != null)
				{
					PlayFabEvents._instance.OnServerBanUsersResultEvent((PlayFab.ServerModels.BanUsersResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.ConsumeItemResult) && PlayFabEvents._instance.OnServerConsumeItemResultEvent != null)
				{
					PlayFabEvents._instance.OnServerConsumeItemResultEvent((PlayFab.ServerModels.ConsumeItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.CreateSharedGroupResult) && PlayFabEvents._instance.OnServerCreateSharedGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnServerCreateSharedGroupResultEvent((PlayFab.ServerModels.CreateSharedGroupResult)e.Result);
					return;
				}
				if (type2 == typeof(DeleteCharacterFromUserResult) && PlayFabEvents._instance.OnServerDeleteCharacterFromUserResultEvent != null)
				{
					PlayFabEvents._instance.OnServerDeleteCharacterFromUserResultEvent((DeleteCharacterFromUserResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.EmptyResult) && PlayFabEvents._instance.OnServerDeleteSharedGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnServerDeleteSharedGroupResultEvent((PlayFab.ServerModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.DeleteUsersResult) && PlayFabEvents._instance.OnServerDeleteUsersResultEvent != null)
				{
					PlayFabEvents._instance.OnServerDeleteUsersResultEvent((PlayFab.ServerModels.DeleteUsersResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.DeregisterGameResponse) && PlayFabEvents._instance.OnServerDeregisterGameResultEvent != null)
				{
					PlayFabEvents._instance.OnServerDeregisterGameResultEvent((PlayFab.ServerModels.DeregisterGameResponse)e.Result);
					return;
				}
				if (type2 == typeof(EvaluateRandomResultTableResult) && PlayFabEvents._instance.OnServerEvaluateRandomResultTableResultEvent != null)
				{
					PlayFabEvents._instance.OnServerEvaluateRandomResultTableResultEvent((EvaluateRandomResultTableResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.ExecuteCloudScriptResult) && PlayFabEvents._instance.OnServerExecuteCloudScriptResultEvent != null)
				{
					PlayFabEvents._instance.OnServerExecuteCloudScriptResultEvent((PlayFab.ServerModels.ExecuteCloudScriptResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetAllSegmentsResult) && PlayFabEvents._instance.OnServerGetAllSegmentsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetAllSegmentsResultEvent((PlayFab.ServerModels.GetAllSegmentsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.ListUsersCharactersResult) && PlayFabEvents._instance.OnServerGetAllUsersCharactersResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetAllUsersCharactersResultEvent((PlayFab.ServerModels.ListUsersCharactersResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetCatalogItemsResult) && PlayFabEvents._instance.OnServerGetCatalogItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCatalogItemsResultEvent((PlayFab.ServerModels.GetCatalogItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetCharacterDataResult) && PlayFabEvents._instance.OnServerGetCharacterDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterDataResultEvent((PlayFab.ServerModels.GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetCharacterDataResult) && PlayFabEvents._instance.OnServerGetCharacterInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterInternalDataResultEvent((PlayFab.ServerModels.GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetCharacterInventoryResult) && PlayFabEvents._instance.OnServerGetCharacterInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterInventoryResultEvent((PlayFab.ServerModels.GetCharacterInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetCharacterLeaderboardResult) && PlayFabEvents._instance.OnServerGetCharacterLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterLeaderboardResultEvent((PlayFab.ServerModels.GetCharacterLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetCharacterDataResult) && PlayFabEvents._instance.OnServerGetCharacterReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterReadOnlyDataResultEvent((PlayFab.ServerModels.GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetCharacterStatisticsResult) && PlayFabEvents._instance.OnServerGetCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetCharacterStatisticsResultEvent((PlayFab.ServerModels.GetCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetContentDownloadUrlResult) && PlayFabEvents._instance.OnServerGetContentDownloadUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetContentDownloadUrlResultEvent((PlayFab.ServerModels.GetContentDownloadUrlResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetLeaderboardResult) && PlayFabEvents._instance.OnServerGetFriendLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetFriendLeaderboardResultEvent((PlayFab.ServerModels.GetLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetFriendsListResult) && PlayFabEvents._instance.OnServerGetFriendsListResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetFriendsListResultEvent((PlayFab.ServerModels.GetFriendsListResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetLeaderboardResult) && PlayFabEvents._instance.OnServerGetLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetLeaderboardResultEvent((PlayFab.ServerModels.GetLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetLeaderboardAroundCharacterResult) && PlayFabEvents._instance.OnServerGetLeaderboardAroundCharacterResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetLeaderboardAroundCharacterResultEvent((PlayFab.ServerModels.GetLeaderboardAroundCharacterResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardAroundUserResult) && PlayFabEvents._instance.OnServerGetLeaderboardAroundUserResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetLeaderboardAroundUserResultEvent((GetLeaderboardAroundUserResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetLeaderboardForUsersCharactersResult) && PlayFabEvents._instance.OnServerGetLeaderboardForUserCharactersResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetLeaderboardForUserCharactersResultEvent((PlayFab.ServerModels.GetLeaderboardForUsersCharactersResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayerCombinedInfoResult) && PlayFabEvents._instance.OnServerGetPlayerCombinedInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerCombinedInfoResultEvent((PlayFab.ServerModels.GetPlayerCombinedInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayerProfileResult) && PlayFabEvents._instance.OnServerGetPlayerProfileResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerProfileResultEvent((PlayFab.ServerModels.GetPlayerProfileResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayerSegmentsResult) && PlayFabEvents._instance.OnServerGetPlayerSegmentsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerSegmentsResultEvent((PlayFab.ServerModels.GetPlayerSegmentsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayersInSegmentResult) && PlayFabEvents._instance.OnServerGetPlayersInSegmentResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayersInSegmentResultEvent((PlayFab.ServerModels.GetPlayersInSegmentResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayerStatisticsResult) && PlayFabEvents._instance.OnServerGetPlayerStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerStatisticsResultEvent((PlayFab.ServerModels.GetPlayerStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayerStatisticVersionsResult) && PlayFabEvents._instance.OnServerGetPlayerStatisticVersionsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerStatisticVersionsResultEvent((PlayFab.ServerModels.GetPlayerStatisticVersionsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayerTagsResult) && PlayFabEvents._instance.OnServerGetPlayerTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayerTagsResultEvent((PlayFab.ServerModels.GetPlayerTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayFabIDsFromFacebookIDsResult) && PlayFabEvents._instance.OnServerGetPlayFabIDsFromFacebookIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayFabIDsFromFacebookIDsResultEvent((PlayFab.ServerModels.GetPlayFabIDsFromFacebookIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPlayFabIDsFromSteamIDsResult) && PlayFabEvents._instance.OnServerGetPlayFabIDsFromSteamIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPlayFabIDsFromSteamIDsResultEvent((PlayFab.ServerModels.GetPlayFabIDsFromSteamIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetPublisherDataResult) && PlayFabEvents._instance.OnServerGetPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetPublisherDataResultEvent((PlayFab.ServerModels.GetPublisherDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetRandomResultTablesResult) && PlayFabEvents._instance.OnServerGetRandomResultTablesResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetRandomResultTablesResultEvent((PlayFab.ServerModels.GetRandomResultTablesResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetSharedGroupDataResult) && PlayFabEvents._instance.OnServerGetSharedGroupDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetSharedGroupDataResultEvent((PlayFab.ServerModels.GetSharedGroupDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetTimeResult) && PlayFabEvents._instance.OnServerGetTimeResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetTimeResultEvent((PlayFab.ServerModels.GetTimeResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetTitleDataResult) && PlayFabEvents._instance.OnServerGetTitleDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetTitleDataResultEvent((PlayFab.ServerModels.GetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetTitleDataResult) && PlayFabEvents._instance.OnServerGetTitleInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetTitleInternalDataResultEvent((PlayFab.ServerModels.GetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetTitleNewsResult) && PlayFabEvents._instance.OnServerGetTitleNewsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetTitleNewsResultEvent((PlayFab.ServerModels.GetTitleNewsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserAccountInfoResult) && PlayFabEvents._instance.OnServerGetUserAccountInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserAccountInfoResultEvent((GetUserAccountInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetUserBansResult) && PlayFabEvents._instance.OnServerGetUserBansResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserBansResultEvent((PlayFab.ServerModels.GetUserBansResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetUserDataResult) && PlayFabEvents._instance.OnServerGetUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserDataResultEvent((PlayFab.ServerModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetUserDataResult) && PlayFabEvents._instance.OnServerGetUserInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserInternalDataResultEvent((PlayFab.ServerModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetUserInventoryResult) && PlayFabEvents._instance.OnServerGetUserInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserInventoryResultEvent((PlayFab.ServerModels.GetUserInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetUserDataResult) && PlayFabEvents._instance.OnServerGetUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserPublisherDataResultEvent((PlayFab.ServerModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetUserDataResult) && PlayFabEvents._instance.OnServerGetUserPublisherInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserPublisherInternalDataResultEvent((PlayFab.ServerModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetUserDataResult) && PlayFabEvents._instance.OnServerGetUserPublisherReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserPublisherReadOnlyDataResultEvent((PlayFab.ServerModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GetUserDataResult) && PlayFabEvents._instance.OnServerGetUserReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGetUserReadOnlyDataResultEvent((PlayFab.ServerModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GrantCharacterToUserResult) && PlayFabEvents._instance.OnServerGrantCharacterToUserResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGrantCharacterToUserResultEvent((PlayFab.ServerModels.GrantCharacterToUserResult)e.Result);
					return;
				}
				if (type2 == typeof(GrantItemsToCharacterResult) && PlayFabEvents._instance.OnServerGrantItemsToCharacterResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGrantItemsToCharacterResultEvent((GrantItemsToCharacterResult)e.Result);
					return;
				}
				if (type2 == typeof(GrantItemsToUserResult) && PlayFabEvents._instance.OnServerGrantItemsToUserResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGrantItemsToUserResultEvent((GrantItemsToUserResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.GrantItemsToUsersResult) && PlayFabEvents._instance.OnServerGrantItemsToUsersResultEvent != null)
				{
					PlayFabEvents._instance.OnServerGrantItemsToUsersResultEvent((PlayFab.ServerModels.GrantItemsToUsersResult)e.Result);
					return;
				}
				if (type2 == typeof(ModifyItemUsesResult) && PlayFabEvents._instance.OnServerModifyItemUsesResultEvent != null)
				{
					PlayFabEvents._instance.OnServerModifyItemUsesResultEvent((ModifyItemUsesResult)e.Result);
					return;
				}
				if (type2 == typeof(MoveItemToCharacterFromCharacterResult) && PlayFabEvents._instance.OnServerMoveItemToCharacterFromCharacterResultEvent != null)
				{
					PlayFabEvents._instance.OnServerMoveItemToCharacterFromCharacterResultEvent((MoveItemToCharacterFromCharacterResult)e.Result);
					return;
				}
				if (type2 == typeof(MoveItemToCharacterFromUserResult) && PlayFabEvents._instance.OnServerMoveItemToCharacterFromUserResultEvent != null)
				{
					PlayFabEvents._instance.OnServerMoveItemToCharacterFromUserResultEvent((MoveItemToCharacterFromUserResult)e.Result);
					return;
				}
				if (type2 == typeof(MoveItemToUserFromCharacterResult) && PlayFabEvents._instance.OnServerMoveItemToUserFromCharacterResultEvent != null)
				{
					PlayFabEvents._instance.OnServerMoveItemToUserFromCharacterResultEvent((MoveItemToUserFromCharacterResult)e.Result);
					return;
				}
				if (type2 == typeof(NotifyMatchmakerPlayerLeftResult) && PlayFabEvents._instance.OnServerNotifyMatchmakerPlayerLeftResultEvent != null)
				{
					PlayFabEvents._instance.OnServerNotifyMatchmakerPlayerLeftResultEvent((NotifyMatchmakerPlayerLeftResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.RedeemCouponResult) && PlayFabEvents._instance.OnServerRedeemCouponResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRedeemCouponResultEvent((PlayFab.ServerModels.RedeemCouponResult)e.Result);
					return;
				}
				if (type2 == typeof(RedeemMatchmakerTicketResult) && PlayFabEvents._instance.OnServerRedeemMatchmakerTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRedeemMatchmakerTicketResultEvent((RedeemMatchmakerTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(RefreshGameServerInstanceHeartbeatResult) && PlayFabEvents._instance.OnServerRefreshGameServerInstanceHeartbeatResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRefreshGameServerInstanceHeartbeatResultEvent((RefreshGameServerInstanceHeartbeatResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.RegisterGameResponse) && PlayFabEvents._instance.OnServerRegisterGameResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRegisterGameResultEvent((PlayFab.ServerModels.RegisterGameResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.EmptyResult) && PlayFabEvents._instance.OnServerRemoveFriendResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRemoveFriendResultEvent((PlayFab.ServerModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.RemovePlayerTagResult) && PlayFabEvents._instance.OnServerRemovePlayerTagResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRemovePlayerTagResultEvent((PlayFab.ServerModels.RemovePlayerTagResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.RemoveSharedGroupMembersResult) && PlayFabEvents._instance.OnServerRemoveSharedGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRemoveSharedGroupMembersResultEvent((PlayFab.ServerModels.RemoveSharedGroupMembersResult)e.Result);
					return;
				}
				if (type2 == typeof(ReportPlayerServerResult) && PlayFabEvents._instance.OnServerReportPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnServerReportPlayerResultEvent((ReportPlayerServerResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.RevokeAllBansForUserResult) && PlayFabEvents._instance.OnServerRevokeAllBansForUserResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRevokeAllBansForUserResultEvent((PlayFab.ServerModels.RevokeAllBansForUserResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.RevokeBansResult) && PlayFabEvents._instance.OnServerRevokeBansResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRevokeBansResultEvent((PlayFab.ServerModels.RevokeBansResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.RevokeInventoryResult) && PlayFabEvents._instance.OnServerRevokeInventoryItemResultEvent != null)
				{
					PlayFabEvents._instance.OnServerRevokeInventoryItemResultEvent((PlayFab.ServerModels.RevokeInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(SendCustomAccountRecoveryEmailResult) && PlayFabEvents._instance.OnServerSendCustomAccountRecoveryEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSendCustomAccountRecoveryEmailResultEvent((SendCustomAccountRecoveryEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(SendEmailFromTemplateResult) && PlayFabEvents._instance.OnServerSendEmailFromTemplateResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSendEmailFromTemplateResultEvent((SendEmailFromTemplateResult)e.Result);
					return;
				}
				if (type2 == typeof(SendPushNotificationResult) && PlayFabEvents._instance.OnServerSendPushNotificationResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSendPushNotificationResultEvent((SendPushNotificationResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.EmptyResult) && PlayFabEvents._instance.OnServerSetFriendTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSetFriendTagsResultEvent((PlayFab.ServerModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(SetGameServerInstanceDataResult) && PlayFabEvents._instance.OnServerSetGameServerInstanceDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSetGameServerInstanceDataResultEvent((SetGameServerInstanceDataResult)e.Result);
					return;
				}
				if (type2 == typeof(SetGameServerInstanceStateResult) && PlayFabEvents._instance.OnServerSetGameServerInstanceStateResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSetGameServerInstanceStateResultEvent((SetGameServerInstanceStateResult)e.Result);
					return;
				}
				if (type2 == typeof(SetGameServerInstanceTagsResult) && PlayFabEvents._instance.OnServerSetGameServerInstanceTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSetGameServerInstanceTagsResultEvent((SetGameServerInstanceTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.SetPlayerSecretResult) && PlayFabEvents._instance.OnServerSetPlayerSecretResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSetPlayerSecretResultEvent((PlayFab.ServerModels.SetPlayerSecretResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.SetPublisherDataResult) && PlayFabEvents._instance.OnServerSetPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSetPublisherDataResultEvent((PlayFab.ServerModels.SetPublisherDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.SetTitleDataResult) && PlayFabEvents._instance.OnServerSetTitleDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSetTitleDataResultEvent((PlayFab.ServerModels.SetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.SetTitleDataResult) && PlayFabEvents._instance.OnServerSetTitleInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSetTitleInternalDataResultEvent((PlayFab.ServerModels.SetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(ModifyCharacterVirtualCurrencyResult) && PlayFabEvents._instance.OnServerSubtractCharacterVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSubtractCharacterVirtualCurrencyResultEvent((ModifyCharacterVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnServerSubtractUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnServerSubtractUserVirtualCurrencyResultEvent((PlayFab.ServerModels.ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UnlockContainerItemResult) && PlayFabEvents._instance.OnServerUnlockContainerInstanceResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUnlockContainerInstanceResultEvent((PlayFab.ServerModels.UnlockContainerItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UnlockContainerItemResult) && PlayFabEvents._instance.OnServerUnlockContainerItemResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUnlockContainerItemResultEvent((PlayFab.ServerModels.UnlockContainerItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.EmptyResult) && PlayFabEvents._instance.OnServerUpdateAvatarUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateAvatarUrlResultEvent((PlayFab.ServerModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateBansResult) && PlayFabEvents._instance.OnServerUpdateBansResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateBansResultEvent((PlayFab.ServerModels.UpdateBansResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateCharacterDataResult) && PlayFabEvents._instance.OnServerUpdateCharacterDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateCharacterDataResultEvent((PlayFab.ServerModels.UpdateCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateCharacterDataResult) && PlayFabEvents._instance.OnServerUpdateCharacterInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateCharacterInternalDataResultEvent((PlayFab.ServerModels.UpdateCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateCharacterDataResult) && PlayFabEvents._instance.OnServerUpdateCharacterReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateCharacterReadOnlyDataResultEvent((PlayFab.ServerModels.UpdateCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateCharacterStatisticsResult) && PlayFabEvents._instance.OnServerUpdateCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateCharacterStatisticsResultEvent((PlayFab.ServerModels.UpdateCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdatePlayerStatisticsResult) && PlayFabEvents._instance.OnServerUpdatePlayerStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdatePlayerStatisticsResultEvent((PlayFab.ServerModels.UpdatePlayerStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateSharedGroupDataResult) && PlayFabEvents._instance.OnServerUpdateSharedGroupDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateSharedGroupDataResultEvent((PlayFab.ServerModels.UpdateSharedGroupDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateUserDataResult) && PlayFabEvents._instance.OnServerUpdateUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserDataResultEvent((PlayFab.ServerModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateUserDataResult) && PlayFabEvents._instance.OnServerUpdateUserInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserInternalDataResultEvent((PlayFab.ServerModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.EmptyResult) && PlayFabEvents._instance.OnServerUpdateUserInventoryItemCustomDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserInventoryItemCustomDataResultEvent((PlayFab.ServerModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateUserDataResult) && PlayFabEvents._instance.OnServerUpdateUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserPublisherDataResultEvent((PlayFab.ServerModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateUserDataResult) && PlayFabEvents._instance.OnServerUpdateUserPublisherInternalDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserPublisherInternalDataResultEvent((PlayFab.ServerModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateUserDataResult) && PlayFabEvents._instance.OnServerUpdateUserPublisherReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserPublisherReadOnlyDataResultEvent((PlayFab.ServerModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.UpdateUserDataResult) && PlayFabEvents._instance.OnServerUpdateUserReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnServerUpdateUserReadOnlyDataResultEvent((PlayFab.ServerModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.WriteEventResponse) && PlayFabEvents._instance.OnServerWriteCharacterEventResultEvent != null)
				{
					PlayFabEvents._instance.OnServerWriteCharacterEventResultEvent((PlayFab.ServerModels.WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.WriteEventResponse) && PlayFabEvents._instance.OnServerWritePlayerEventResultEvent != null)
				{
					PlayFabEvents._instance.OnServerWritePlayerEventResultEvent((PlayFab.ServerModels.WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ServerModels.WriteEventResponse) && PlayFabEvents._instance.OnServerWriteTitleEventResultEvent != null)
				{
					PlayFabEvents._instance.OnServerWriteTitleEventResultEvent((PlayFab.ServerModels.WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(LoginResult) && PlayFabEvents._instance.OnLoginResultEvent != null)
				{
					PlayFabEvents._instance.OnLoginResultEvent((LoginResult)e.Result);
					return;
				}
				if (type2 == typeof(AcceptTradeResponse) && PlayFabEvents._instance.OnAcceptTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnAcceptTradeResultEvent((AcceptTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(AddFriendResult) && PlayFabEvents._instance.OnAddFriendResultEvent != null)
				{
					PlayFabEvents._instance.OnAddFriendResultEvent((AddFriendResult)e.Result);
					return;
				}
				if (type2 == typeof(AddGenericIDResult) && PlayFabEvents._instance.OnAddGenericIDResultEvent != null)
				{
					PlayFabEvents._instance.OnAddGenericIDResultEvent((AddGenericIDResult)e.Result);
					return;
				}
				if (type2 == typeof(AddOrUpdateContactEmailResult) && PlayFabEvents._instance.OnAddOrUpdateContactEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnAddOrUpdateContactEmailResultEvent((AddOrUpdateContactEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.AddSharedGroupMembersResult) && PlayFabEvents._instance.OnAddSharedGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnAddSharedGroupMembersResultEvent((PlayFab.ClientModels.AddSharedGroupMembersResult)e.Result);
					return;
				}
				if (type2 == typeof(AddUsernamePasswordResult) && PlayFabEvents._instance.OnAddUsernamePasswordResultEvent != null)
				{
					PlayFabEvents._instance.OnAddUsernamePasswordResultEvent((AddUsernamePasswordResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnAddUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnAddUserVirtualCurrencyResultEvent((PlayFab.ClientModels.ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(AndroidDevicePushNotificationRegistrationResult) && PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationResultEvent != null)
				{
					PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationResultEvent((AndroidDevicePushNotificationRegistrationResult)e.Result);
					return;
				}
				if (type2 == typeof(AttributeInstallResult) && PlayFabEvents._instance.OnAttributeInstallResultEvent != null)
				{
					PlayFabEvents._instance.OnAttributeInstallResultEvent((AttributeInstallResult)e.Result);
					return;
				}
				if (type2 == typeof(CancelTradeResponse) && PlayFabEvents._instance.OnCancelTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnCancelTradeResultEvent((CancelTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(ConfirmPurchaseResult) && PlayFabEvents._instance.OnConfirmPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnConfirmPurchaseResultEvent((ConfirmPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.ConsumeItemResult) && PlayFabEvents._instance.OnConsumeItemResultEvent != null)
				{
					PlayFabEvents._instance.OnConsumeItemResultEvent((PlayFab.ClientModels.ConsumeItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.CreateSharedGroupResult) && PlayFabEvents._instance.OnCreateSharedGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnCreateSharedGroupResultEvent((PlayFab.ClientModels.CreateSharedGroupResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.ExecuteCloudScriptResult) && PlayFabEvents._instance.OnExecuteCloudScriptResultEvent != null)
				{
					PlayFabEvents._instance.OnExecuteCloudScriptResultEvent((PlayFab.ClientModels.ExecuteCloudScriptResult)e.Result);
					return;
				}
				if (type2 == typeof(GetAccountInfoResult) && PlayFabEvents._instance.OnGetAccountInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnGetAccountInfoResultEvent((GetAccountInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.ListUsersCharactersResult) && PlayFabEvents._instance.OnGetAllUsersCharactersResultEvent != null)
				{
					PlayFabEvents._instance.OnGetAllUsersCharactersResultEvent((PlayFab.ClientModels.ListUsersCharactersResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetCatalogItemsResult) && PlayFabEvents._instance.OnGetCatalogItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCatalogItemsResultEvent((PlayFab.ClientModels.GetCatalogItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetCharacterDataResult) && PlayFabEvents._instance.OnGetCharacterDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterDataResultEvent((PlayFab.ClientModels.GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetCharacterInventoryResult) && PlayFabEvents._instance.OnGetCharacterInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterInventoryResultEvent((PlayFab.ClientModels.GetCharacterInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetCharacterLeaderboardResult) && PlayFabEvents._instance.OnGetCharacterLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterLeaderboardResultEvent((PlayFab.ClientModels.GetCharacterLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetCharacterDataResult) && PlayFabEvents._instance.OnGetCharacterReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterReadOnlyDataResultEvent((PlayFab.ClientModels.GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetCharacterStatisticsResult) && PlayFabEvents._instance.OnGetCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterStatisticsResultEvent((PlayFab.ClientModels.GetCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetContentDownloadUrlResult) && PlayFabEvents._instance.OnGetContentDownloadUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnGetContentDownloadUrlResultEvent((PlayFab.ClientModels.GetContentDownloadUrlResult)e.Result);
					return;
				}
				if (type2 == typeof(CurrentGamesResult) && PlayFabEvents._instance.OnGetCurrentGamesResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCurrentGamesResultEvent((CurrentGamesResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetLeaderboardResult) && PlayFabEvents._instance.OnGetFriendLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardResultEvent((PlayFab.ClientModels.GetLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(GetFriendLeaderboardAroundPlayerResult) && PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerResultEvent((GetFriendLeaderboardAroundPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetFriendsListResult) && PlayFabEvents._instance.OnGetFriendsListResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendsListResultEvent((PlayFab.ClientModels.GetFriendsListResult)e.Result);
					return;
				}
				if (type2 == typeof(GameServerRegionsResult) && PlayFabEvents._instance.OnGetGameServerRegionsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetGameServerRegionsResultEvent((GameServerRegionsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetLeaderboardResult) && PlayFabEvents._instance.OnGetLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardResultEvent((PlayFab.ClientModels.GetLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetLeaderboardAroundCharacterResult) && PlayFabEvents._instance.OnGetLeaderboardAroundCharacterResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundCharacterResultEvent((PlayFab.ClientModels.GetLeaderboardAroundCharacterResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardAroundPlayerResult) && PlayFabEvents._instance.OnGetLeaderboardAroundPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundPlayerResultEvent((GetLeaderboardAroundPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetLeaderboardForUsersCharactersResult) && PlayFabEvents._instance.OnGetLeaderboardForUserCharactersResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardForUserCharactersResultEvent((PlayFab.ClientModels.GetLeaderboardForUsersCharactersResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPaymentTokenResult) && PlayFabEvents._instance.OnGetPaymentTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPaymentTokenResultEvent((GetPaymentTokenResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPhotonAuthenticationTokenResult) && PlayFabEvents._instance.OnGetPhotonAuthenticationTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPhotonAuthenticationTokenResultEvent((GetPhotonAuthenticationTokenResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPlayerCombinedInfoResult) && PlayFabEvents._instance.OnGetPlayerCombinedInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerCombinedInfoResultEvent((PlayFab.ClientModels.GetPlayerCombinedInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPlayerProfileResult) && PlayFabEvents._instance.OnGetPlayerProfileResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerProfileResultEvent((PlayFab.ClientModels.GetPlayerProfileResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPlayerSegmentsResult) && PlayFabEvents._instance.OnGetPlayerSegmentsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerSegmentsResultEvent((PlayFab.ClientModels.GetPlayerSegmentsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPlayerStatisticsResult) && PlayFabEvents._instance.OnGetPlayerStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticsResultEvent((PlayFab.ClientModels.GetPlayerStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPlayerStatisticVersionsResult) && PlayFabEvents._instance.OnGetPlayerStatisticVersionsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticVersionsResultEvent((PlayFab.ClientModels.GetPlayerStatisticVersionsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPlayerTagsResult) && PlayFabEvents._instance.OnGetPlayerTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTagsResultEvent((PlayFab.ClientModels.GetPlayerTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerTradesResponse) && PlayFabEvents._instance.OnGetPlayerTradesResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTradesResultEvent((GetPlayerTradesResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPlayFabIDsFromFacebookIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsResultEvent((PlayFab.ClientModels.GetPlayFabIDsFromFacebookIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGameCenterIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsResultEvent((GetPlayFabIDsFromGameCenterIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGenericIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsResultEvent((GetPlayFabIDsFromGenericIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGoogleIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsResultEvent((GetPlayFabIDsFromGoogleIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromKongregateIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsResultEvent((GetPlayFabIDsFromKongregateIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPlayFabIDsFromSteamIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsResultEvent((PlayFab.ClientModels.GetPlayFabIDsFromSteamIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromTwitchIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsResultEvent((GetPlayFabIDsFromTwitchIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetPublisherDataResult) && PlayFabEvents._instance.OnGetPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPublisherDataResultEvent((PlayFab.ClientModels.GetPublisherDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPurchaseResult) && PlayFabEvents._instance.OnGetPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPurchaseResultEvent((GetPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetSharedGroupDataResult) && PlayFabEvents._instance.OnGetSharedGroupDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetSharedGroupDataResultEvent((PlayFab.ClientModels.GetSharedGroupDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetStoreItemsResult) && PlayFabEvents._instance.OnGetStoreItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetStoreItemsResultEvent((PlayFab.ClientModels.GetStoreItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetTimeResult) && PlayFabEvents._instance.OnGetTimeResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTimeResultEvent((PlayFab.ClientModels.GetTimeResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetTitleDataResult) && PlayFabEvents._instance.OnGetTitleDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleDataResultEvent((PlayFab.ClientModels.GetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetTitleNewsResult) && PlayFabEvents._instance.OnGetTitleNewsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleNewsResultEvent((PlayFab.ClientModels.GetTitleNewsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTitlePublicKeyResult) && PlayFabEvents._instance.OnGetTitlePublicKeyResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitlePublicKeyResultEvent((GetTitlePublicKeyResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTradeStatusResponse) && PlayFabEvents._instance.OnGetTradeStatusResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTradeStatusResultEvent((GetTradeStatusResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetUserDataResult) && PlayFabEvents._instance.OnGetUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserDataResultEvent((PlayFab.ClientModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetUserInventoryResult) && PlayFabEvents._instance.OnGetUserInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserInventoryResultEvent((PlayFab.ClientModels.GetUserInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetUserDataResult) && PlayFabEvents._instance.OnGetUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherDataResultEvent((PlayFab.ClientModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetUserDataResult) && PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataResultEvent((PlayFab.ClientModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GetUserDataResult) && PlayFabEvents._instance.OnGetUserReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserReadOnlyDataResultEvent((PlayFab.ClientModels.GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetWindowsHelloChallengeResponse) && PlayFabEvents._instance.OnGetWindowsHelloChallengeResultEvent != null)
				{
					PlayFabEvents._instance.OnGetWindowsHelloChallengeResultEvent((GetWindowsHelloChallengeResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.GrantCharacterToUserResult) && PlayFabEvents._instance.OnGrantCharacterToUserResultEvent != null)
				{
					PlayFabEvents._instance.OnGrantCharacterToUserResultEvent((PlayFab.ClientModels.GrantCharacterToUserResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkAndroidDeviceIDResult) && PlayFabEvents._instance.OnLinkAndroidDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkAndroidDeviceIDResultEvent((LinkAndroidDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkCustomIDResult) && PlayFabEvents._instance.OnLinkCustomIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkCustomIDResultEvent((LinkCustomIDResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkFacebookAccountResult) && PlayFabEvents._instance.OnLinkFacebookAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookAccountResultEvent((LinkFacebookAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkGameCenterAccountResult) && PlayFabEvents._instance.OnLinkGameCenterAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkGameCenterAccountResultEvent((LinkGameCenterAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkGoogleAccountResult) && PlayFabEvents._instance.OnLinkGoogleAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkGoogleAccountResultEvent((LinkGoogleAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkIOSDeviceIDResult) && PlayFabEvents._instance.OnLinkIOSDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkIOSDeviceIDResultEvent((LinkIOSDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkKongregateAccountResult) && PlayFabEvents._instance.OnLinkKongregateResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkKongregateResultEvent((LinkKongregateAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkSteamAccountResult) && PlayFabEvents._instance.OnLinkSteamAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkSteamAccountResultEvent((LinkSteamAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkTwitchAccountResult) && PlayFabEvents._instance.OnLinkTwitchResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkTwitchResultEvent((LinkTwitchAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkWindowsHelloAccountResponse) && PlayFabEvents._instance.OnLinkWindowsHelloResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkWindowsHelloResultEvent((LinkWindowsHelloAccountResponse)e.Result);
					return;
				}
				if (type2 == typeof(MatchmakeResult) && PlayFabEvents._instance.OnMatchmakeResultEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakeResultEvent((MatchmakeResult)e.Result);
					return;
				}
				if (type2 == typeof(OpenTradeResponse) && PlayFabEvents._instance.OnOpenTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnOpenTradeResultEvent((OpenTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(PayForPurchaseResult) && PlayFabEvents._instance.OnPayForPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnPayForPurchaseResultEvent((PayForPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(PurchaseItemResult) && PlayFabEvents._instance.OnPurchaseItemResultEvent != null)
				{
					PlayFabEvents._instance.OnPurchaseItemResultEvent((PurchaseItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.RedeemCouponResult) && PlayFabEvents._instance.OnRedeemCouponResultEvent != null)
				{
					PlayFabEvents._instance.OnRedeemCouponResultEvent((PlayFab.ClientModels.RedeemCouponResult)e.Result);
					return;
				}
				if (type2 == typeof(RegisterForIOSPushNotificationResult) && PlayFabEvents._instance.OnRegisterForIOSPushNotificationResultEvent != null)
				{
					PlayFabEvents._instance.OnRegisterForIOSPushNotificationResultEvent((RegisterForIOSPushNotificationResult)e.Result);
					return;
				}
				if (type2 == typeof(RegisterPlayFabUserResult) && PlayFabEvents._instance.OnRegisterPlayFabUserResultEvent != null)
				{
					PlayFabEvents._instance.OnRegisterPlayFabUserResultEvent((RegisterPlayFabUserResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveContactEmailResult) && PlayFabEvents._instance.OnRemoveContactEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveContactEmailResultEvent((RemoveContactEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveFriendResult) && PlayFabEvents._instance.OnRemoveFriendResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveFriendResultEvent((RemoveFriendResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveGenericIDResult) && PlayFabEvents._instance.OnRemoveGenericIDResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveGenericIDResultEvent((RemoveGenericIDResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.RemoveSharedGroupMembersResult) && PlayFabEvents._instance.OnRemoveSharedGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveSharedGroupMembersResultEvent((PlayFab.ClientModels.RemoveSharedGroupMembersResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResult) && PlayFabEvents._instance.OnReportDeviceInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnReportDeviceInfoResultEvent((PlayFab.ClientModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(ReportPlayerClientResult) && PlayFabEvents._instance.OnReportPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnReportPlayerResultEvent((ReportPlayerClientResult)e.Result);
					return;
				}
				if (type2 == typeof(RestoreIOSPurchasesResult) && PlayFabEvents._instance.OnRestoreIOSPurchasesResultEvent != null)
				{
					PlayFabEvents._instance.OnRestoreIOSPurchasesResultEvent((RestoreIOSPurchasesResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.SendAccountRecoveryEmailResult) && PlayFabEvents._instance.OnSendAccountRecoveryEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnSendAccountRecoveryEmailResultEvent((PlayFab.ClientModels.SendAccountRecoveryEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(SetFriendTagsResult) && PlayFabEvents._instance.OnSetFriendTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnSetFriendTagsResultEvent((SetFriendTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.SetPlayerSecretResult) && PlayFabEvents._instance.OnSetPlayerSecretResultEvent != null)
				{
					PlayFabEvents._instance.OnSetPlayerSecretResultEvent((PlayFab.ClientModels.SetPlayerSecretResult)e.Result);
					return;
				}
				if (type2 == typeof(StartGameResult) && PlayFabEvents._instance.OnStartGameResultEvent != null)
				{
					PlayFabEvents._instance.OnStartGameResultEvent((StartGameResult)e.Result);
					return;
				}
				if (type2 == typeof(StartPurchaseResult) && PlayFabEvents._instance.OnStartPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnStartPurchaseResultEvent((StartPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnSubtractUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnSubtractUserVirtualCurrencyResultEvent((PlayFab.ClientModels.ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkAndroidDeviceIDResult) && PlayFabEvents._instance.OnUnlinkAndroidDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkAndroidDeviceIDResultEvent((UnlinkAndroidDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkCustomIDResult) && PlayFabEvents._instance.OnUnlinkCustomIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkCustomIDResultEvent((UnlinkCustomIDResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkFacebookAccountResult) && PlayFabEvents._instance.OnUnlinkFacebookAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookAccountResultEvent((UnlinkFacebookAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkGameCenterAccountResult) && PlayFabEvents._instance.OnUnlinkGameCenterAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGameCenterAccountResultEvent((UnlinkGameCenterAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkGoogleAccountResult) && PlayFabEvents._instance.OnUnlinkGoogleAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGoogleAccountResultEvent((UnlinkGoogleAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkIOSDeviceIDResult) && PlayFabEvents._instance.OnUnlinkIOSDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkIOSDeviceIDResultEvent((UnlinkIOSDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkKongregateAccountResult) && PlayFabEvents._instance.OnUnlinkKongregateResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkKongregateResultEvent((UnlinkKongregateAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkSteamAccountResult) && PlayFabEvents._instance.OnUnlinkSteamAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkSteamAccountResultEvent((UnlinkSteamAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkTwitchAccountResult) && PlayFabEvents._instance.OnUnlinkTwitchResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkTwitchResultEvent((UnlinkTwitchAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkWindowsHelloAccountResponse) && PlayFabEvents._instance.OnUnlinkWindowsHelloResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkWindowsHelloResultEvent((UnlinkWindowsHelloAccountResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UnlockContainerItemResult) && PlayFabEvents._instance.OnUnlockContainerInstanceResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerInstanceResultEvent((PlayFab.ClientModels.UnlockContainerItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UnlockContainerItemResult) && PlayFabEvents._instance.OnUnlockContainerItemResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerItemResultEvent((PlayFab.ClientModels.UnlockContainerItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResult) && PlayFabEvents._instance.OnUpdateAvatarUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateAvatarUrlResultEvent((PlayFab.ClientModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UpdateCharacterDataResult) && PlayFabEvents._instance.OnUpdateCharacterDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterDataResultEvent((PlayFab.ClientModels.UpdateCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UpdateCharacterStatisticsResult) && PlayFabEvents._instance.OnUpdateCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterStatisticsResultEvent((PlayFab.ClientModels.UpdateCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UpdatePlayerStatisticsResult) && PlayFabEvents._instance.OnUpdatePlayerStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdatePlayerStatisticsResultEvent((PlayFab.ClientModels.UpdatePlayerStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UpdateSharedGroupDataResult) && PlayFabEvents._instance.OnUpdateSharedGroupDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateSharedGroupDataResultEvent((PlayFab.ClientModels.UpdateSharedGroupDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UpdateUserDataResult) && PlayFabEvents._instance.OnUpdateUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserDataResultEvent((PlayFab.ClientModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UpdateUserDataResult) && PlayFabEvents._instance.OnUpdateUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserPublisherDataResultEvent((PlayFab.ClientModels.UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.UpdateUserTitleDisplayNameResult) && PlayFabEvents._instance.OnUpdateUserTitleDisplayNameResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserTitleDisplayNameResultEvent((PlayFab.ClientModels.UpdateUserTitleDisplayNameResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateAmazonReceiptResult) && PlayFabEvents._instance.OnValidateAmazonIAPReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateAmazonIAPReceiptResultEvent((ValidateAmazonReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateGooglePlayPurchaseResult) && PlayFabEvents._instance.OnValidateGooglePlayPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateGooglePlayPurchaseResultEvent((ValidateGooglePlayPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateIOSReceiptResult) && PlayFabEvents._instance.OnValidateIOSReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateIOSReceiptResultEvent((ValidateIOSReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateWindowsReceiptResult) && PlayFabEvents._instance.OnValidateWindowsStoreReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateWindowsStoreReceiptResultEvent((ValidateWindowsReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.WriteEventResponse) && PlayFabEvents._instance.OnWriteCharacterEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWriteCharacterEventResultEvent((PlayFab.ClientModels.WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.WriteEventResponse) && PlayFabEvents._instance.OnWritePlayerEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWritePlayerEventResultEvent((PlayFab.ClientModels.WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.WriteEventResponse) && PlayFabEvents._instance.OnWriteTitleEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWriteTitleEventResultEvent((PlayFab.ClientModels.WriteEventResponse)e.Result);
					return;
				}
			}
		}

		private static PlayFabEvents _instance;

		public delegate void PlayFabErrorEvent(PlayFabRequestCommon request, PlayFabError error);

		public delegate void PlayFabResultEvent<in TResult>(TResult result) where TResult : PlayFabResultCommon;

		public delegate void PlayFabRequestEvent<in TRequest>(TRequest request) where TRequest : PlayFabRequestCommon;
	}
}
