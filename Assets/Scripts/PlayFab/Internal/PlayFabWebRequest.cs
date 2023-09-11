using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using PlayFab.Json;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	public class PlayFabWebRequest : IPlayFabHttp
	{
		public bool SessionStarted
		{
			get
			{
				return PlayFabWebRequest._sessionStarted;
			}
			set
			{
				PlayFabWebRequest._sessionStarted = value;
			}
		}

		public string AuthKey { get; set; }

		public string EntityToken { get; set; }

		public void InitializeHttp()
		{
			this.SetupCertificates();
			PlayFabWebRequest._isApplicationPlaying = true;
			PlayFabWebRequest._unityVersion = Application.unityVersion;
		}

		public void OnDestroy()
		{
			PlayFabWebRequest._isApplicationPlaying = false;
			object resultQueue = PlayFabWebRequest.ResultQueue;
			lock (resultQueue)
			{
				PlayFabWebRequest.ResultQueue.Clear();
			}
			object activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				PlayFabWebRequest.ActiveRequests.Clear();
			}
			object threadLock = PlayFabWebRequest._ThreadLock;
			lock (threadLock)
			{
				PlayFabWebRequest._requestQueueThread = null;
			}
		}

		private void SetupCertificates()
		{
			ServicePointManager.DefaultConnectionLimit = 10;
			ServicePointManager.Expect100Continue = false;
			RemoteCertificateValidationCallback serverCertificateValidationCallback = new RemoteCertificateValidationCallback(PlayFabWebRequest.AcceptAllCertifications);
			ServicePointManager.ServerCertificateValidationCallback = serverCertificateValidationCallback;
		}

		private static bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		public void MakeApiCall(CallRequestContainer reqContainer)
		{
			reqContainer.HttpState = HttpRequestState.Idle;
			object activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				PlayFabWebRequest.ActiveRequests.Insert(0, reqContainer);
			}
			PlayFabWebRequest.ActivateThreadWorker();
		}

		private static void ActivateThreadWorker()
		{
			object threadLock = PlayFabWebRequest._ThreadLock;
			lock (threadLock)
			{
				if (PlayFabWebRequest._requestQueueThread == null)
				{
					if (PlayFabWebRequest._003C_003Ef__mg_0024cache0 == null)
					{
						PlayFabWebRequest._003C_003Ef__mg_0024cache0 = new ThreadStart(PlayFabWebRequest.WorkerThreadMainLoop);
					}
					PlayFabWebRequest._requestQueueThread = new Thread(PlayFabWebRequest._003C_003Ef__mg_0024cache0);
					PlayFabWebRequest._requestQueueThread.Start();
				}
			}
		}

		private static void WorkerThreadMainLoop()
		{
			try
			{
				object threadLock = PlayFabWebRequest._ThreadLock;
				lock (threadLock)
				{
					PlayFabWebRequest._threadKillTime = DateTime.UtcNow + PlayFabWebRequest.ThreadKillTimeout;
				}
				List<CallRequestContainer> list = new List<CallRequestContainer>();
				bool flag;
				do
				{
					object activeRequests = PlayFabWebRequest.ActiveRequests;
					lock (activeRequests)
					{
						list.AddRange(PlayFabWebRequest.ActiveRequests);
						PlayFabWebRequest.ActiveRequests.Clear();
						PlayFabWebRequest._activeCallCount = list.Count;
					}
					int count = list.Count;
					for (int i = count - 1; i >= 0; i--)
					{
						switch (list[i].HttpState)
						{
						case HttpRequestState.Sent:
							if (list[i].HttpRequest.HaveResponse)
							{
								PlayFabWebRequest.ProcessHttpResponse(list[i]);
							}
							break;
						case HttpRequestState.Received:
							PlayFabWebRequest.ProcessJsonResponse(list[i]);
							list.RemoveAt(i);
							break;
						case HttpRequestState.Idle:
							PlayFabWebRequest.Post(list[i]);
							break;
						case HttpRequestState.Error:
							list.RemoveAt(i);
							break;
						}
					}
					object threadLock2 = PlayFabWebRequest._ThreadLock;
					lock (threadLock2)
					{
						DateTime utcNow = DateTime.UtcNow;
						if (count > 0 && PlayFabWebRequest._isApplicationPlaying)
						{
							PlayFabWebRequest._threadKillTime = utcNow + PlayFabWebRequest.ThreadKillTimeout;
						}
						flag = (utcNow <= PlayFabWebRequest._threadKillTime);
						if (!flag)
						{
							PlayFabWebRequest._requestQueueThread = null;
						}
					}
					Thread.Sleep(1);
				}
				while (flag);
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				PlayFabWebRequest._requestQueueThread = null;
			}
		}

		private static void Post(CallRequestContainer reqContainer)
		{
			try
			{
				reqContainer.HttpRequest = (HttpWebRequest)WebRequest.Create(reqContainer.FullUrl);
				reqContainer.HttpRequest.UserAgent = "UnityEngine-Unity; Version: " + PlayFabWebRequest._unityVersion;
				reqContainer.HttpRequest.SendChunked = false;
				reqContainer.HttpRequest.Proxy = null;
				foreach (KeyValuePair<string, string> keyValuePair in reqContainer.RequestHeaders)
				{
					reqContainer.HttpRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
				}
				reqContainer.HttpRequest.ContentType = "application/json";
				reqContainer.HttpRequest.Method = "POST";
				reqContainer.HttpRequest.KeepAlive = PlayFabSettings.RequestKeepAlive;
				reqContainer.HttpRequest.Timeout = PlayFabSettings.RequestTimeout;
				reqContainer.HttpRequest.AllowWriteStreamBuffering = false;
				reqContainer.HttpRequest.Proxy = null;
				reqContainer.HttpRequest.ContentLength = reqContainer.Payload.LongLength;
				reqContainer.HttpRequest.ReadWriteTimeout = PlayFabSettings.RequestTimeout;
				using (Stream requestStream = reqContainer.HttpRequest.GetRequestStream())
				{
					requestStream.Write(reqContainer.Payload, 0, reqContainer.Payload.Length);
				}
				reqContainer.HttpState = HttpRequestState.Sent;
			}
			catch (WebException ex)
			{
				reqContainer.JsonResponse = (PlayFabWebRequest.ResponseToString(ex.Response) ?? (ex.Status + ": WebException making http request to: " + reqContainer.FullUrl));
				WebException exception = new WebException(reqContainer.JsonResponse, ex);
				UnityEngine.Debug.LogException(exception);
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
			catch (Exception innerException)
			{
				reqContainer.JsonResponse = "Unhandled exception in Post : " + reqContainer.FullUrl;
				Exception exception2 = new Exception(reqContainer.JsonResponse, innerException);
				UnityEngine.Debug.LogException(exception2);
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		private static void ProcessHttpResponse(CallRequestContainer reqContainer)
		{
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)reqContainer.HttpRequest.GetResponse();
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					reqContainer.JsonResponse = PlayFabWebRequest.ResponseToString(httpWebResponse);
				}
				if (httpWebResponse.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(reqContainer.JsonResponse))
				{
					reqContainer.JsonResponse = (reqContainer.JsonResponse ?? "No response from server");
					PlayFabWebRequest.QueueRequestError(reqContainer);
				}
				else
				{
					reqContainer.HttpState = HttpRequestState.Received;
				}
			}
			catch (Exception innerException)
			{
				string text = "Unhandled exception in ProcessHttpResponse : " + reqContainer.FullUrl;
				reqContainer.JsonResponse = (reqContainer.JsonResponse ?? text);
				Exception exception = new Exception(text, innerException);
				UnityEngine.Debug.LogException(exception);
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		private static void QueueRequestError(CallRequestContainer reqContainer)
		{
			reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.JsonResponse, reqContainer.CustomData);
			reqContainer.HttpState = HttpRequestState.Error;
			object resultQueue = PlayFabWebRequest.ResultQueue;
			lock (resultQueue)
			{
				PlayFabWebRequest.ResultQueue.Enqueue(delegate
				{
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					if (reqContainer.ErrorCallback != null)
					{
						reqContainer.ErrorCallback(reqContainer.Error);
					}
				});
			}
		}

		private static void ProcessJsonResponse(CallRequestContainer reqContainer)
		{
			try
			{
				HttpResponseObject httpResponseObject = JsonWrapper.DeserializeObject<HttpResponseObject>(reqContainer.JsonResponse);
				if (httpResponseObject == null || httpResponseObject.code != 200)
				{
					PlayFabWebRequest.QueueRequestError(reqContainer);
				}
				else
				{
					reqContainer.JsonResponse = JsonWrapper.SerializeObject(httpResponseObject.data);
					reqContainer.DeserializeResultJson();
					reqContainer.ApiResult.Request = reqContainer.ApiRequest;
					reqContainer.ApiResult.CustomData = reqContainer.CustomData;
					SingletonMonoBehaviour<PlayFabHttp>.instance.OnPlayFabApiResult(reqContainer.ApiResult);
					object resultQueue = PlayFabWebRequest.ResultQueue;
					lock (resultQueue)
					{
						PlayFabWebRequest.ResultQueue.Enqueue(delegate
						{
							PlayFabDeviceUtil.OnPlayFabLogin(reqContainer.ApiResult);
						});
					}
					object resultQueue2 = PlayFabWebRequest.ResultQueue;
					lock (resultQueue2)
					{
						PlayFabWebRequest.ResultQueue.Enqueue(delegate
						{
							try
							{
								PlayFabHttp.SendEvent(reqContainer.ApiEndpoint, reqContainer.ApiRequest, reqContainer.ApiResult, ApiProcessingEventType.Post);
								reqContainer.InvokeSuccessCallback();
							}
							catch (Exception exception2)
							{
								UnityEngine.Debug.LogException(exception2);
							}
						});
					}
				}
			}
			catch (Exception innerException)
			{
				string text = "Unhandled exception in ProcessJsonResponse : " + reqContainer.FullUrl;
				reqContainer.JsonResponse = (reqContainer.JsonResponse ?? text);
				Exception exception = new Exception(text, innerException);
				UnityEngine.Debug.LogException(exception);
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		public void Update()
		{
			object resultQueue = PlayFabWebRequest.ResultQueue;
			lock (resultQueue)
			{
				while (PlayFabWebRequest.ResultQueue.Count > 0)
				{
					Action item = PlayFabWebRequest.ResultQueue.Dequeue();
					PlayFabWebRequest._tempActions.Enqueue(item);
				}
			}
			while (PlayFabWebRequest._tempActions.Count > 0)
			{
				Action action = PlayFabWebRequest._tempActions.Dequeue();
				action();
			}
		}

		private static string ResponseToString(WebResponse webResponse)
		{
			if (webResponse == null)
			{
				return null;
			}
			string result;
			try
			{
				using (Stream responseStream = webResponse.GetResponseStream())
				{
					if (responseStream == null)
					{
						result = null;
					}
					else
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			catch (WebException ex)
			{
				try
				{
					using (Stream responseStream2 = ex.Response.GetResponseStream())
					{
						if (responseStream2 == null)
						{
							result = null;
						}
						else
						{
							using (StreamReader streamReader2 = new StreamReader(responseStream2))
							{
								result = streamReader2.ReadToEnd();
							}
						}
					}
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
					result = null;
				}
			}
			catch (Exception exception2)
			{
				UnityEngine.Debug.LogException(exception2);
				result = null;
			}
			return result;
		}

		public int GetPendingMessages()
		{
			int num = 0;
			object activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				num += PlayFabWebRequest.ActiveRequests.Count + PlayFabWebRequest._activeCallCount;
			}
			object resultQueue = PlayFabWebRequest.ResultQueue;
			lock (resultQueue)
			{
				num += PlayFabWebRequest.ResultQueue.Count;
			}
			return num;
		}

		private static readonly Queue<Action> ResultQueue = new Queue<Action>();

		private static readonly Queue<Action> _tempActions = new Queue<Action>();

		private static readonly List<CallRequestContainer> ActiveRequests = new List<CallRequestContainer>();

		private static Thread _requestQueueThread;

		private static readonly object _ThreadLock = new object();

		private static readonly TimeSpan ThreadKillTimeout = TimeSpan.FromSeconds(60.0);

		private static DateTime _threadKillTime = DateTime.UtcNow + PlayFabWebRequest.ThreadKillTimeout;

		private static bool _isApplicationPlaying;

		private static int _activeCallCount;

		private static string _unityVersion;

		private static bool _sessionStarted;

		[CompilerGenerated]
		private static ThreadStart _003C_003Ef__mg_0024cache0;
	}
}
