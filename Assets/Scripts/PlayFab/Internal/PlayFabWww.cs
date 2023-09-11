using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Ionic.Zlib;
using PlayFab.Json;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	public class PlayFabWww : IPlayFabHttp
	{
		public bool SessionStarted { get; set; }

		public string AuthKey { get; set; }

		public string EntityToken { get; set; }

		public void InitializeHttp()
		{
		}

		public void Update()
		{
		}

		public void OnDestroy()
		{
		}

		public void MakeApiCall(CallRequestContainer reqContainer)
		{
			reqContainer.RequestHeaders["Content-Type"] = "application/json";
			if (PlayFabSettings.CompressApiData)
			{
				reqContainer.RequestHeaders["Content-Encoding"] = "GZIP";
				reqContainer.RequestHeaders["Accept-Encoding"] = "GZIP";
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
					{
						gzipStream.Write(reqContainer.Payload, 0, reqContainer.Payload.Length);
					}
					reqContainer.Payload = memoryStream.ToArray();
				}
			}
			WWW www = new WWW(reqContainer.FullUrl, reqContainer.Payload, reqContainer.RequestHeaders);
			Action<string> wwwSuccessCallback = delegate(string response)
			{
				try
				{
					HttpResponseObject httpResponseObject = JsonWrapper.DeserializeObject<HttpResponseObject>(response);
					if (httpResponseObject.code == 200)
					{
						reqContainer.JsonResponse = JsonWrapper.SerializeObject(httpResponseObject.data);
						reqContainer.DeserializeResultJson();
						reqContainer.ApiResult.Request = reqContainer.ApiRequest;
						reqContainer.ApiResult.CustomData = reqContainer.CustomData;
						SingletonMonoBehaviour<PlayFabHttp>.instance.OnPlayFabApiResult(reqContainer.ApiResult);
						PlayFabDeviceUtil.OnPlayFabLogin(reqContainer.ApiResult);
						try
						{
							PlayFabHttp.SendEvent(reqContainer.ApiEndpoint, reqContainer.ApiRequest, reqContainer.ApiResult, ApiProcessingEventType.Post);
						}
						catch (Exception exception)
						{
							UnityEngine.Debug.LogException(exception);
						}
						try
						{
							reqContainer.InvokeSuccessCallback();
						}
						catch (Exception exception2)
						{
							UnityEngine.Debug.LogException(exception2);
						}
					}
					else if (reqContainer.ErrorCallback != null)
					{
						reqContainer.Error = PlayFabHttp.GeneratePlayFabError(response, reqContainer.CustomData);
						PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
						reqContainer.ErrorCallback(reqContainer.Error);
					}
				}
				catch (Exception exception3)
				{
					UnityEngine.Debug.LogException(exception3);
				}
			};
			Action<string> wwwErrorCallback = delegate(string errorCb)
			{
				reqContainer.JsonResponse = errorCb;
				if (reqContainer.ErrorCallback != null)
				{
					reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.JsonResponse, reqContainer.CustomData);
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					reqContainer.ErrorCallback(reqContainer.Error);
				}
			};
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(this.Post(www, wwwSuccessCallback, wwwErrorCallback));
		}

		private IEnumerator Post(WWW www, Action<string> wwwSuccessCallback, Action<string> wwwErrorCallback)
		{
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				wwwErrorCallback(www.error);
			}
			else
			{
				try
				{
					Dictionary<string, string> dictionary = null;
					try
					{
						dictionary = www.responseHeaders;
					}
					catch (Exception)
					{
					}
					string text;
					if (dictionary != null && dictionary.TryGetValue("Content-Encoding", out text) && text.ToLower() == "gzip")
					{
						MemoryStream stream = new MemoryStream(www.bytes);
						using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress, false))
						{
							byte[] array = new byte[4096];
							using (MemoryStream memoryStream = new MemoryStream())
							{
								int count;
								while ((count = gzipStream.Read(array, 0, array.Length)) > 0)
								{
									memoryStream.Write(array, 0, count);
								}
								memoryStream.Seek(0L, SeekOrigin.Begin);
								StreamReader streamReader = new StreamReader(memoryStream);
								string obj = streamReader.ReadToEnd();
								wwwSuccessCallback(obj);
							}
						}
					}
					else
					{
						wwwSuccessCallback(www.text);
					}
				}
				catch (Exception arg)
				{
					wwwErrorCallback("Unhandled error in PlayFabWWW: " + arg);
				}
			}
			yield break;
		}

		public int GetPendingMessages()
		{
			return this._pendingWwwMessages;
		}

		private int _pendingWwwMessages;
	}
}
