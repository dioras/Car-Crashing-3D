using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.Json;
using PlayFab.ServerModels;

namespace PlayFab
{
	public static class PlayFabServerAPI
	{
		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void AddCharacterVirtualCurrency(AddCharacterVirtualCurrencyRequest request, Action<ModifyCharacterVirtualCurrencyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyCharacterVirtualCurrencyResult>("/Server/AddCharacterVirtualCurrency", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddFriend(AddFriendRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Server/AddFriend", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddPlayerTag(AddPlayerTagRequest request, Action<AddPlayerTagResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<AddPlayerTagResult>("/Server/AddPlayerTag", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddSharedGroupMembers(AddSharedGroupMembersRequest request, Action<AddSharedGroupMembersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<AddSharedGroupMembersResult>("/Server/AddSharedGroupMembers", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddUserVirtualCurrency(AddUserVirtualCurrencyRequest request, Action<ModifyUserVirtualCurrencyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyUserVirtualCurrencyResult>("/Server/AddUserVirtualCurrency", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AuthenticateSessionTicket(AuthenticateSessionTicketRequest request, Action<AuthenticateSessionTicketResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<AuthenticateSessionTicketResult>("/Server/AuthenticateSessionTicket", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AwardSteamAchievement(AwardSteamAchievementRequest request, Action<AwardSteamAchievementResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<AwardSteamAchievementResult>("/Server/AwardSteamAchievement", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void BanUsers(BanUsersRequest request, Action<BanUsersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<BanUsersResult>("/Server/BanUsers", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ConsumeItem(ConsumeItemRequest request, Action<ConsumeItemResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ConsumeItemResult>("/Server/ConsumeItem", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreateSharedGroup(CreateSharedGroupRequest request, Action<CreateSharedGroupResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<CreateSharedGroupResult>("/Server/CreateSharedGroup", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteCharacterFromUser(DeleteCharacterFromUserRequest request, Action<DeleteCharacterFromUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<DeleteCharacterFromUserResult>("/Server/DeleteCharacterFromUser", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteSharedGroup(DeleteSharedGroupRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Server/DeleteSharedGroup", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteUsers(DeleteUsersRequest request, Action<DeleteUsersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<DeleteUsersResult>("/Server/DeleteUsers", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeregisterGame(DeregisterGameRequest request, Action<DeregisterGameResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<DeregisterGameResponse>("/Server/DeregisterGame", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void EvaluateRandomResultTable(EvaluateRandomResultTableRequest request, Action<EvaluateRandomResultTableResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EvaluateRandomResultTableResult>("/Server/EvaluateRandomResultTable", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ExecuteCloudScript(ExecuteCloudScriptServerRequest request, Action<ExecuteCloudScriptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ExecuteCloudScriptResult>("/Server/ExecuteCloudScript", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ExecuteCloudScript<TOut>(ExecuteCloudScriptServerRequest request, Action<ExecuteCloudScriptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			Action<ExecuteCloudScriptResult> resultCallback2 = delegate(ExecuteCloudScriptResult wrappedResult)
			{
				string text = JsonWrapper.SerializeObject(wrappedResult.FunctionResult);
				try
				{
					wrappedResult.FunctionResult = JsonWrapper.DeserializeObject<TOut>(text);
				}
				catch (Exception)
				{
					wrappedResult.FunctionResult = text;
					wrappedResult.Logs.Add(new LogStatement
					{
						Level = "Warning",
						Data = text,
						Message = "Sdk Message: Could not deserialize result as: " + typeof(TOut).Name
					});
				}
				resultCallback(wrappedResult);
			};
			PlayFabServerAPI.ExecuteCloudScript(request, resultCallback2, errorCallback, customData, extraHeaders);
		}

		public static void GetAllSegments(GetAllSegmentsRequest request, Action<GetAllSegmentsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetAllSegmentsResult>("/Server/GetAllSegments", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetAllUsersCharacters(ListUsersCharactersRequest request, Action<ListUsersCharactersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ListUsersCharactersResult>("/Server/GetAllUsersCharacters", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCatalogItems(GetCatalogItemsRequest request, Action<GetCatalogItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCatalogItemsResult>("/Server/GetCatalogItems", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterData(GetCharacterDataRequest request, Action<GetCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterDataResult>("/Server/GetCharacterData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterInternalData(GetCharacterDataRequest request, Action<GetCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterDataResult>("/Server/GetCharacterInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterInventory(GetCharacterInventoryRequest request, Action<GetCharacterInventoryResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterInventoryResult>("/Server/GetCharacterInventory", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterLeaderboard(GetCharacterLeaderboardRequest request, Action<GetCharacterLeaderboardResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterLeaderboardResult>("/Server/GetCharacterLeaderboard", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterReadOnlyData(GetCharacterDataRequest request, Action<GetCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterDataResult>("/Server/GetCharacterReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterStatistics(GetCharacterStatisticsRequest request, Action<GetCharacterStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterStatisticsResult>("/Server/GetCharacterStatistics", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetContentDownloadUrl(GetContentDownloadUrlRequest request, Action<GetContentDownloadUrlResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetContentDownloadUrlResult>("/Server/GetContentDownloadUrl", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetFriendLeaderboard(GetFriendLeaderboardRequest request, Action<GetLeaderboardResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardResult>("/Server/GetFriendLeaderboard", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetFriendsList(GetFriendsListRequest request, Action<GetFriendsListResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetFriendsListResult>("/Server/GetFriendsList", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetLeaderboard(GetLeaderboardRequest request, Action<GetLeaderboardResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardResult>("/Server/GetLeaderboard", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetLeaderboardAroundCharacter(GetLeaderboardAroundCharacterRequest request, Action<GetLeaderboardAroundCharacterResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardAroundCharacterResult>("/Server/GetLeaderboardAroundCharacter", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetLeaderboardAroundUser(GetLeaderboardAroundUserRequest request, Action<GetLeaderboardAroundUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardAroundUserResult>("/Server/GetLeaderboardAroundUser", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetLeaderboardForUserCharacters(GetLeaderboardForUsersCharactersRequest request, Action<GetLeaderboardForUsersCharactersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardForUsersCharactersResult>("/Server/GetLeaderboardForUserCharacters", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerCombinedInfo(GetPlayerCombinedInfoRequest request, Action<GetPlayerCombinedInfoResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerCombinedInfoResult>("/Server/GetPlayerCombinedInfo", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerProfile(GetPlayerProfileRequest request, Action<GetPlayerProfileResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerProfileResult>("/Server/GetPlayerProfile", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerSegments(GetPlayersSegmentsRequest request, Action<GetPlayerSegmentsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerSegmentsResult>("/Server/GetPlayerSegments", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayersInSegment(GetPlayersInSegmentRequest request, Action<GetPlayersInSegmentResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayersInSegmentResult>("/Server/GetPlayersInSegment", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerStatistics(GetPlayerStatisticsRequest request, Action<GetPlayerStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerStatisticsResult>("/Server/GetPlayerStatistics", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerStatisticVersions(GetPlayerStatisticVersionsRequest request, Action<GetPlayerStatisticVersionsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerStatisticVersionsResult>("/Server/GetPlayerStatisticVersions", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerTags(GetPlayerTagsRequest request, Action<GetPlayerTagsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerTagsResult>("/Server/GetPlayerTags", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromFacebookIDs(GetPlayFabIDsFromFacebookIDsRequest request, Action<GetPlayFabIDsFromFacebookIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromFacebookIDsResult>("/Server/GetPlayFabIDsFromFacebookIDs", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromSteamIDs(GetPlayFabIDsFromSteamIDsRequest request, Action<GetPlayFabIDsFromSteamIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromSteamIDsResult>("/Server/GetPlayFabIDsFromSteamIDs", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPublisherData(GetPublisherDataRequest request, Action<GetPublisherDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPublisherDataResult>("/Server/GetPublisherData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetRandomResultTables(GetRandomResultTablesRequest request, Action<GetRandomResultTablesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetRandomResultTablesResult>("/Server/GetRandomResultTables", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetSharedGroupData(GetSharedGroupDataRequest request, Action<GetSharedGroupDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetSharedGroupDataResult>("/Server/GetSharedGroupData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTime(GetTimeRequest request, Action<GetTimeResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTimeResult>("/Server/GetTime", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitleData(GetTitleDataRequest request, Action<GetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTitleDataResult>("/Server/GetTitleData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitleInternalData(GetTitleDataRequest request, Action<GetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTitleDataResult>("/Server/GetTitleInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitleNews(GetTitleNewsRequest request, Action<GetTitleNewsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTitleNewsResult>("/Server/GetTitleNews", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserAccountInfo(GetUserAccountInfoRequest request, Action<GetUserAccountInfoResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserAccountInfoResult>("/Server/GetUserAccountInfo", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserBans(GetUserBansRequest request, Action<GetUserBansResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserBansResult>("/Server/GetUserBans", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Server/GetUserData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserInternalData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Server/GetUserInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserInventory(GetUserInventoryRequest request, Action<GetUserInventoryResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserInventoryResult>("/Server/GetUserInventory", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserPublisherData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Server/GetUserPublisherData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserPublisherInternalData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Server/GetUserPublisherInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserPublisherReadOnlyData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Server/GetUserPublisherReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserReadOnlyData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Server/GetUserReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GrantCharacterToUser(GrantCharacterToUserRequest request, Action<GrantCharacterToUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GrantCharacterToUserResult>("/Server/GrantCharacterToUser", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GrantItemsToCharacter(GrantItemsToCharacterRequest request, Action<GrantItemsToCharacterResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GrantItemsToCharacterResult>("/Server/GrantItemsToCharacter", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GrantItemsToUser(GrantItemsToUserRequest request, Action<GrantItemsToUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GrantItemsToUserResult>("/Server/GrantItemsToUser", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GrantItemsToUsers(GrantItemsToUsersRequest request, Action<GrantItemsToUsersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<GrantItemsToUsersResult>("/Server/GrantItemsToUsers", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ModifyItemUses(ModifyItemUsesRequest request, Action<ModifyItemUsesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyItemUsesResult>("/Server/ModifyItemUses", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void MoveItemToCharacterFromCharacter(MoveItemToCharacterFromCharacterRequest request, Action<MoveItemToCharacterFromCharacterResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<MoveItemToCharacterFromCharacterResult>("/Server/MoveItemToCharacterFromCharacter", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void MoveItemToCharacterFromUser(MoveItemToCharacterFromUserRequest request, Action<MoveItemToCharacterFromUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<MoveItemToCharacterFromUserResult>("/Server/MoveItemToCharacterFromUser", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void MoveItemToUserFromCharacter(MoveItemToUserFromCharacterRequest request, Action<MoveItemToUserFromCharacterResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<MoveItemToUserFromCharacterResult>("/Server/MoveItemToUserFromCharacter", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void NotifyMatchmakerPlayerLeft(NotifyMatchmakerPlayerLeftRequest request, Action<NotifyMatchmakerPlayerLeftResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<NotifyMatchmakerPlayerLeftResult>("/Server/NotifyMatchmakerPlayerLeft", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RedeemCoupon(RedeemCouponRequest request, Action<RedeemCouponResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RedeemCouponResult>("/Server/RedeemCoupon", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RedeemMatchmakerTicket(RedeemMatchmakerTicketRequest request, Action<RedeemMatchmakerTicketResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RedeemMatchmakerTicketResult>("/Server/RedeemMatchmakerTicket", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RefreshGameServerInstanceHeartbeat(RefreshGameServerInstanceHeartbeatRequest request, Action<RefreshGameServerInstanceHeartbeatResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RefreshGameServerInstanceHeartbeatResult>("/Server/RefreshGameServerInstanceHeartbeat", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RegisterGame(RegisterGameRequest request, Action<RegisterGameResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RegisterGameResponse>("/Server/RegisterGame", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveFriend(RemoveFriendRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Server/RemoveFriend", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemovePlayerTag(RemovePlayerTagRequest request, Action<RemovePlayerTagResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RemovePlayerTagResult>("/Server/RemovePlayerTag", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveSharedGroupMembers(RemoveSharedGroupMembersRequest request, Action<RemoveSharedGroupMembersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RemoveSharedGroupMembersResult>("/Server/RemoveSharedGroupMembers", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ReportPlayer(ReportPlayerServerRequest request, Action<ReportPlayerServerResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ReportPlayerServerResult>("/Server/ReportPlayer", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RevokeAllBansForUser(RevokeAllBansForUserRequest request, Action<RevokeAllBansForUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RevokeAllBansForUserResult>("/Server/RevokeAllBansForUser", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RevokeBans(RevokeBansRequest request, Action<RevokeBansResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RevokeBansResult>("/Server/RevokeBans", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RevokeInventoryItem(RevokeInventoryItemRequest request, Action<RevokeInventoryResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<RevokeInventoryResult>("/Server/RevokeInventoryItem", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SendCustomAccountRecoveryEmail(SendCustomAccountRecoveryEmailRequest request, Action<SendCustomAccountRecoveryEmailResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SendCustomAccountRecoveryEmailResult>("/Server/SendCustomAccountRecoveryEmail", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SendEmailFromTemplate(SendEmailFromTemplateRequest request, Action<SendEmailFromTemplateResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SendEmailFromTemplateResult>("/Server/SendEmailFromTemplate", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SendPushNotification(SendPushNotificationRequest request, Action<SendPushNotificationResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SendPushNotificationResult>("/Server/SendPushNotification", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetFriendTags(SetFriendTagsRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Server/SetFriendTags", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetGameServerInstanceData(SetGameServerInstanceDataRequest request, Action<SetGameServerInstanceDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetGameServerInstanceDataResult>("/Server/SetGameServerInstanceData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetGameServerInstanceState(SetGameServerInstanceStateRequest request, Action<SetGameServerInstanceStateResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetGameServerInstanceStateResult>("/Server/SetGameServerInstanceState", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetGameServerInstanceTags(SetGameServerInstanceTagsRequest request, Action<SetGameServerInstanceTagsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetGameServerInstanceTagsResult>("/Server/SetGameServerInstanceTags", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetPlayerSecret(SetPlayerSecretRequest request, Action<SetPlayerSecretResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetPlayerSecretResult>("/Server/SetPlayerSecret", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetPublisherData(SetPublisherDataRequest request, Action<SetPublisherDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetPublisherDataResult>("/Server/SetPublisherData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetTitleData(SetTitleDataRequest request, Action<SetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetTitleDataResult>("/Server/SetTitleData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetTitleInternalData(SetTitleDataRequest request, Action<SetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<SetTitleDataResult>("/Server/SetTitleInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SubtractCharacterVirtualCurrency(SubtractCharacterVirtualCurrencyRequest request, Action<ModifyCharacterVirtualCurrencyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyCharacterVirtualCurrencyResult>("/Server/SubtractCharacterVirtualCurrency", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SubtractUserVirtualCurrency(SubtractUserVirtualCurrencyRequest request, Action<ModifyUserVirtualCurrencyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyUserVirtualCurrencyResult>("/Server/SubtractUserVirtualCurrency", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlockContainerInstance(UnlockContainerInstanceRequest request, Action<UnlockContainerItemResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlockContainerItemResult>("/Server/UnlockContainerInstance", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlockContainerItem(UnlockContainerItemRequest request, Action<UnlockContainerItemResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlockContainerItemResult>("/Server/UnlockContainerItem", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateAvatarUrl(UpdateAvatarUrlRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Server/UpdateAvatarUrl", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateBans(UpdateBansRequest request, Action<UpdateBansResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateBansResult>("/Server/UpdateBans", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateCharacterData(UpdateCharacterDataRequest request, Action<UpdateCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCharacterDataResult>("/Server/UpdateCharacterData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateCharacterInternalData(UpdateCharacterDataRequest request, Action<UpdateCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCharacterDataResult>("/Server/UpdateCharacterInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateCharacterReadOnlyData(UpdateCharacterDataRequest request, Action<UpdateCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCharacterDataResult>("/Server/UpdateCharacterReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateCharacterStatistics(UpdateCharacterStatisticsRequest request, Action<UpdateCharacterStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCharacterStatisticsResult>("/Server/UpdateCharacterStatistics", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdatePlayerStatistics(UpdatePlayerStatisticsRequest request, Action<UpdatePlayerStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdatePlayerStatisticsResult>("/Server/UpdatePlayerStatistics", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateSharedGroupData(UpdateSharedGroupDataRequest request, Action<UpdateSharedGroupDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateSharedGroupDataResult>("/Server/UpdateSharedGroupData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Server/UpdateUserData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserInternalData(UpdateUserInternalDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Server/UpdateUserInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserInventoryItemCustomData(UpdateUserInventoryItemDataRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Server/UpdateUserInventoryItemCustomData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserPublisherData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Server/UpdateUserPublisherData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserPublisherInternalData(UpdateUserInternalDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Server/UpdateUserPublisherInternalData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserPublisherReadOnlyData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Server/UpdateUserPublisherReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserReadOnlyData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Server/UpdateUserReadOnlyData", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void WriteCharacterEvent(WriteServerCharacterEventRequest request, Action<WriteEventResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<WriteEventResponse>("/Server/WriteCharacterEvent", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void WritePlayerEvent(WriteServerPlayerEventRequest request, Action<WriteEventResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<WriteEventResponse>("/Server/WritePlayerEvent", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void WriteTitleEvent(WriteTitleEventRequest request, Action<WriteEventResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (PlayFabSettings.DeveloperSecretKey == null)
			{
				throw new Exception("Must have PlayFabSettings.DeveloperSecretKey set to call this method");
			}
			PlayFabHttp.MakeApiCall<WriteEventResponse>("/Server/WriteTitleEvent", request, AuthType.DevSecretKey, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
