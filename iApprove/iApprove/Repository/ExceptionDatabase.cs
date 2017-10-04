using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iApprove.Repository
{
    public class ExceptionDatabase: BaseRepository<ExceptionModel>
    {
        public ExceptionDatabase()
        {
            try
            {
                database = DependencyService.Get<ISQLite>().GetConnection();
                // create the tables
                database.CreateTable<ExceptionModel>();
            }
            catch (Exception exception)
            {
                var exc = exception;
            }
        }

        public override IEnumerable<ExceptionModel> Get()
        {
            lock (locker)
            {
                return database.Table<ExceptionModel>().ToList();
            }
        }

        public IEnumerable<ExceptionModel> Get(DateTime from, DateTime to)
        {
            return database.Table<ExceptionModel>().Where(x =>x.TimeStamp >= from && x.TimeStamp <= to);
        }
    }
}