using System;
namespace iApprove.Model
{
    public class LoginModel
    {
        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public ResponseStatus LoginStatus
        {
            get;
            set;
        }
    }
}
