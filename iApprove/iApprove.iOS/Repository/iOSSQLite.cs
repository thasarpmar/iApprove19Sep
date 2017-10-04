using System;
using SQLite;
using System.IO;
using Xamarin.Forms;
using ProjectTEmplate.iOS.Repository;
using iApprove.Repository;
using iApprove;

[assembly: Dependency(typeof(iOSSQLite))]
namespace ProjectTEmplate.iOS.Repository
{
    public class iOSSQLite : ISQLite
    {
        private string _sqliteFilename = Constants.STR_DBNAME;

        public SQLiteConnection GetConnection()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, _sqliteFilename);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}