using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;

namespace iApprove.View.PendingRequestDetailContentView
{
    public class PendingRequestDetailModel 
    {
        public PendingRequestDetailModel()
        {
        }
		
        [JsonProperty("data")]
		public List<Datum> data { get; set; }
        [JsonProperty("template")]
		public Template template { get; set; }
    }

    public class Datum 
    {
		
        [JsonProperty("title")]
		public string title { get; set; }
        [JsonProperty("value")]
		public string Value { get; set; }
    }

    public class Template
    {
		[JsonProperty("orientation")]
        public string orientation { get; set; }
    }
	
}



