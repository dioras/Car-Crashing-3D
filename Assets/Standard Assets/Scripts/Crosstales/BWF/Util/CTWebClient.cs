using System;
using System.Net;

namespace Crosstales.BWF.Util
{
	public class CTWebClient : WebClient
	{
		public CTWebClient() : this(5000)
		{
		}

		public CTWebClient(int timeout)
		{
			this.Timeout = timeout;
		}

		public int Timeout { get; set; }

		protected override WebRequest GetWebRequest(Uri uri)
		{
			WebRequest webRequest = base.GetWebRequest(uri);
			if (webRequest != null)
			{
				webRequest.Timeout = this.Timeout;
			}
			return webRequest;
		}
	}
}
