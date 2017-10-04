using System;
using Newtonsoft.Json;

namespace iApprove.Model
{
    public class DepartmentModel
    {
        public DepartmentModel()
        {
        }

        [JsonProperty("id")]
        public int Identifier
        {
            get;set;
        }

        [JsonProperty("name")]
        public string DeptName
        {
            get;set;
        }

        [JsonProperty("logo")]
        public string DeptLogo
        {
            get;set;
        }

        [JsonProperty("pendingRequestCount")]
        public int PendingRequestCount
        {
            get;set;
        }
    }
}
