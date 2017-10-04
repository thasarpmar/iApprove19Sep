using System;
namespace iApprove.Model
{
    public class History
    {
        public History()
        {
        }

        public string RequestStatus
        {
            get;
            set;
        }
  
        public DateTime RequestDate
        {
            get;
            set;
        }
  
        public string Approver
        {
            get;
            set;
        }
    }
}
