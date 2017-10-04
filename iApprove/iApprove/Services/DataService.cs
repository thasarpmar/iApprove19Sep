using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iApprove.Interface;
using iApprove.Model;
using PCLStorage;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using iApprove.View;
using Newtonsoft.Json;
using iApprove.Enum;
using Newtonsoft.Json.Linq;
using iApprove.View.PendingRequestDetailContentView;
using System.Net;
using System.Net.Http;

namespace iApprove.Services
{
    public class DataService : IDataSource
    {
        static HttpClient client;
        public DataService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
		public void GetCDSIDs()
        {
            throw new NotImplementedException();
        }

        public void GetComments()
        {
            throw new NotImplementedException();
        }

        public void GetDenyReasons()
        {
            throw new NotImplementedException();
        }

        public object GetNotifications()
        {
            return GetStubResponse(ServiceName.NOTIFICATIONS);
        }

		//     public Object GetPendingRequestDetails(string departmentIdentifier, string requestNumber)
		//     {
		//return GetStubResponse(ServiceName.PENDINGREQUESTDETAILS);
		//}
		public Object GetPendingRequestDetails(string departmentIdentifier)
		{
			return GetStubResponse(ServiceName.PENDINGREQUESTDETAILS);
		}
        public Object GetPendingRequests(string departmentIdentifier)
        {
            return GetStubResponse(ServiceName.PENDINGREQUEST);
        }

        //public async static Task<bool> IsFileExistAsync(this string fileName, IFolder rootFolder = null)
        //{
        //	// get hold of the file system  
        //	IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
        //	ExistenceCheckResult folderexist = await folder.CheckExistsAsync(fileName);
        //	// already run at least once, don't overwrite what's there  
        //	if (folderexist == ExistenceCheckResult.FileExists)
        //	{
        //		return true;

        //	}
        //	return false;
        //}

        public Object Login(LoginModel loginModel)
        {
            return GetStubResponse(ServiceName.LOGIN);
        }
        public void LoginService()
		{
			//return GetStubResponse(ServiceName.LOGIN);
           // GetService();
		}
        private static Object GetStubResponse(ServiceName enumValue)
        {
            try
            {
                Object rootobject = new Object();

                var assembly = typeof(LoginPage).GetTypeInfo().Assembly;
                Stream stream=new MemoryStream();

                switch (enumValue)
                {
                    case ServiceName.LOGIN:

                        stream = assembly.GetManifestResourceStream("iApprove.Stubs.loginResponse-SUCCESS.json");
						using (var reader = new System.IO.StreamReader(stream))
						{
							var json = reader.ReadToEnd();

							rootobject = JsonConvert.DeserializeObject<SessionInfo>(json);
						}
                        break;
                    case ServiceName.PENDINGREQUEST:
						stream = assembly.GetManifestResourceStream("iApprove.Stubs.getPendingRequestsResponse.json");
						using (var reader = new System.IO.StreamReader(stream))
						{
							var json = reader.ReadToEnd();

                            var jsonObject = JsonConvert.DeserializeObject(json) as JObject;

                            rootobject = jsonObject["PendingRequests"].ToObject<List<PendingRequests>>(); 
						}
						break;
                    case ServiceName.PENDINGREQUESTDETAILS:
						stream = assembly.GetManifestResourceStream("iApprove.Stubs.Temp.json");
						using (var reader = new System.IO.StreamReader(stream))
						{
							var json = reader.ReadToEnd();
							var jsonObject = JsonConvert.DeserializeObject(json) as JObject;

							rootobject = jsonObject["PendingRequestDetailModel"].ToObject<PendingRequestDetailModel>();

							
						}
						break;
                    case ServiceName.NOTIFICATIONS:
						stream = assembly.GetManifestResourceStream("iApprove.Stubs.getNotificationsResponse.json");
						using (var reader = new System.IO.StreamReader(stream))
						{
							var json = reader.ReadToEnd();
							var jsonObject = JsonConvert.DeserializeObject(json) as JObject;

                            rootobject = jsonObject["NotificationModel"].ToObject<List<NotificationModel>>();


						}
						break;
                    default:
                        break;
                }

              

                return rootobject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async static void GetService()
		{
            try
            {
                List<KeyValuePair<string, string>> kvpList = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("USER", "Value1"),
                    new KeyValuePair<string, string>("PASSWORD", "Value2"),
                };

                string url = "https://agsmqa.adient.com/siteminderagent/forms/ag_primary.fcc?TYPE=33554433&REALMOID=06-8d818df1-1478-4321-9054-8e012cf39021&GUID=&SMAUTHREASON=0&METHOD=GET&SMAGENTNAME=XhTkd5nKOaScDI466wMXH924wJkj3jDbtdMBzIMHuhJpeI10ZpGajDuWvgo7xW25&TARGET=-SM-https%3a%2f%2fagqa%2eadient%2ecom%2fiApproveService%2fapi%2ftodoListDummy%2fgetSystems\n";
                //using (var client = new HttpClient())
                //{

                //var content = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("Username", "User"),
                //    new KeyValuePair<string, string>("Password", "Password")
                //});
                //HttpResponseMessage response = await client.GetAsync(url, content);
                //if (response.IsSuccessStatusCode)
                //{

                    //using (HttpContent requestResponse = response.Content)
                    //{
                    //    string resultlogin = await requestResponse.ReadAsStringAsync();
                    //    if (resultlogin != null && result.Length >= 50)
                    //    {
                    //        //isconnected = Convert.ToBoolean(result);
                    //    }
                    //}
                    //}
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    //request.Method = "POST";
                    //            request.
                    //using GET - request.Headers.Add ("Authorization","Authorizaation value");
                    //request.ContentType = "application/json";
                    //            HttpWebResponse myResp =await (HttpWebResponse)request.GetResponseAsync();
                    //string responseText;

                    //using (var response = request.GetResponse())
                    //{
                    //	using (var reader = new StreamReader(response.GetResponseStream()))
                    //	{
                    //		responseText = reader.ReadToEnd();

                    //	}
                    //}
                    //var client = new HttpClient();
                    //var content = new StringContent(
                    //                JsonConvert.SerializeObject(kvpList));
                    //                var result = await client.PostAsync(url, content).ConfigureAwait(false);
                    //if (result.IsSuccessStatusCode)
                    //{
                    //	var tokenJson = await result.Content.ReadAsStringAsync();
                    //}
                //}
            }
			catch (WebException exception)
			{
				string responseText;
				using (var reader = new StreamReader(exception.Response.GetResponseStream()))
				{
					responseText = reader.ReadToEnd();
					//Console.WriteLine(responseText);
				}
			}
		}
    }
     

	public class PendingRequestClass
	{
        public List<PendingRequests> PendingRequestsList { get; set; }
	}
}
