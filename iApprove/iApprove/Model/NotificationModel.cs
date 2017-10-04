using System;
using Newtonsoft.Json;

namespace iApprove.Model
{
    public class NotificationModel
    {
        public NotificationModel()
        {
        }

        //public string Identifier
        //{
        //    get;
        //    set;
        //}
        [JsonProperty("deptName")]
        public string DeaprtmentName
        {
            get;
            set;
        }
        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }
        [JsonProperty("timeNotified")]
        public int NotificationTime
        {
            get;
            set;
        }
    }
}
