using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CollegeEvents
{
    public class DatabaseConnection : IDisposable
    {
        private SQLiteConnection _con;
        private bool _disposed = false;

        private DatabaseConnection()
        {
            this._con = new SQLiteConnection(MakeConnectionString());
            try
            {
                _con.Open();
            }
            catch (Exception e)
            {
                throw new Exception(
                    "Failed to open SQLite connection "
                    + "with connectionstring: " + _con.ConnectionString,
                    e);
            }
        }

        ~DatabaseConnection()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                try
                {
                    _con.Close();
                }
                catch (Exception)
                { }

                _disposed = true;
            }
        }

        private static string MakeConnectionString()
        {
            string s;
            s = "Data Source=";
            s += Path.GetFullPath(Path.Combine(Path.GetTempPath(), "UCFEventsWebsite_LocalDB.db"));
            s += "; Version = 3;";
            return s;
        }


        public void Execute(string q)
        {

        }
    }
}
