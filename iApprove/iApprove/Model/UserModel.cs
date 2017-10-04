using System;
using Newtonsoft.Json;

namespace iApprove.Model
{
    public class UserModel
    {
        public UserModel()
        {
        }

        [JsonProperty("userName")]
        public string UserName
        {
            get;set;
        }
        [JsonProperty("profilePhoto")]
        public string ProfilePhoto
        {
            get;set;
        }
        [JsonProperty("role")]
        public string Role
        {
            get;set;
        }
        [JsonProperty("lastLoggin")]
        public string LastLoggin
        {
            get;set;
        }
    }
}
