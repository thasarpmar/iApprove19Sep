using SQLite;
using Xamarin.Forms;
using System.IO;
using iApprove.Repository;
using iApprove.Droid.Repository;

[assembly: Dependency(typeof(DroidSQLite))]
namespace iApprove.Droid.Repository
{
    public class DroidSQLite : ISQLite
    {
        private string _sqliteFilename = Constants.STR_DBNAME;
        public SQLiteConnection GetConnection()
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, _sqliteFilename);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}