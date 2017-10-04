using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iApprove.Interface
{
    public interface ISecureStorage
    {
        bool Contains(string key);
        void Delete(string key);
        byte[] Retrieve(string key);
        void Store(string key, byte[] dataBytes);
		string RetrieveString(string key);
		void StoreString(string key, string datastr);
    }
}
