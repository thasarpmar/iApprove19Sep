using System;
using System.Collections.Generic;
using System.Linq;
using iApprove.Model;

namespace iApprove
{
    public class Helper 
    {
        public Helper()
        {
        }

        private static Helper _instance;
        public static Helper Instance => _instance ?? (_instance = new Helper());

        public List<PendingRequests> PendingRequestsList
        {
            get;
            set;
        }

		public List<PendingRequests> SearchByRequests(string searchText)
		{
			int count = 0;
			searchText = searchText.Trim();
			string[] searchItems = string.IsNullOrEmpty(searchText)
			  ? new string[0]
				 : searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			var filteredRequests = new List<PendingRequests>();
			
			if (PendingRequestsList != null && PendingRequestsList.Count > 0)
			{
				if (searchItems.Any())
				{
					foreach (var item in searchItems)
					{
						count++;
						int RequestId = Int32.MinValue;
						Int32.TryParse(item, out RequestId);

						IEnumerable<PendingRequests> query =
							from p in PendingRequestsList
							where
					p.RequestType.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0
							|| p.RequestNo.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0
							|| p.RequestorName.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0
							orderby p.RequestType
							select p;
						filteredRequests.AddRange(query);
					}
					return filteredRequests.Distinct().ToList();
				}

			}
			return PendingRequestsList;

		}

    }
}
