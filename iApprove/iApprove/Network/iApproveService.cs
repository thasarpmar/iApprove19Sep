using System;
using Newtonsoft.Json;
using System.Net.Http;

namespace iApprove.Network
{
    public class iApproveService
    {
        public System.Threading.Tasks.Task<Object> GetDataAsync()
		{
            //HttpClient client = new HttpClient();
            //var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            //var response = await client.GetAsync(uri);
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    Items = JsonConvert.DeserializeObject<Object>(content);
            //    return Items;
            //}
            //else
                return null;
        }
    }
}
