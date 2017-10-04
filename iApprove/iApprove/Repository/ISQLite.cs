using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iApprove.Repository
{
    /// <summary>
    /// This interface is used for declaring the GetConnection which will be used when required .
    /// </summary>
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
