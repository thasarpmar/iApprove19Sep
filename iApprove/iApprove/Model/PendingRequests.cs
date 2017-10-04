using System;
using Newtonsoft.Json;

namespace iApprove.Model
{
    public class PendingRequests
    {
        public PendingRequests()
        {
        }
        [JsonProperty("requestNo")]
        public string RequestNo
        {
            get;
            set;
        }
        [JsonProperty("dateReceived")]
        public string DateReceived
        {
            get;
            set;
        }
        [JsonProperty("applicationName")]
        public string ApplicationName
        {
            get;
            set;
        }

		[JsonProperty("requestType")]
		public string RequestType
		{
			get;
			set;
		}


		[JsonProperty("requestorName")]
		public string RequestorName
		{
			get;
			set;
		}

    }
}
