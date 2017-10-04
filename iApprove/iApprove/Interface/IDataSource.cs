using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iApprove.Model;

namespace iApprove.Interface
{
    public interface IDataSource
    {
        Object Login(LoginModel loginModel);
        object GetNotifications();
        Object GetPendingRequests(string departmentIdentifier);
        //Object GetPendingRequestDetails(string departmentIdentifier, string requestNumber);
        Object GetPendingRequestDetails(string departmentIdentifier);
        void GetDenyReasons();
        void GetComments();
        void GetCDSIDs();
    }
}
