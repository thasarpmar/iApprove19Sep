using System;
using iApprove.Interface;

namespace iApprove.Model
{
    public class ApplicationModel: IAppInfo
    {
        private bool _isAuthenticate;

        private SessionInfo _session;
        
        public ApplicationModel() 
        {
        }

        private static ApplicationModel _current;
        public static ApplicationModel Current
		{
            get { return _current ?? (_current = new ApplicationModel()); }
		}

        public bool IsAuthenticated()
        {
            return _isAuthenticate;
        }

        public void ClearSession()
        {
            
        }

        public void SetAuthenticate(bool isAutnenticated)
        {
            _isAuthenticate = isAutnenticated;
        }

        public SessionInfo GetSession()
        {
            return _session;
        }

        public void SetSession(SessionInfo sessionInfo)
        {
            _session = sessionInfo;
        }
    }
}
