using System;
using iApprove.Model;

namespace iApprove.Interface
{
    public interface IAppInfo
    {
        bool IsAuthenticated();
        void ClearSession();
        void SetAuthenticate(bool isAutnenticated);
        SessionInfo GetSession();
        void SetSession(SessionInfo sessionInfo);

    }
}
