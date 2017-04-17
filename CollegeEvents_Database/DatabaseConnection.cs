using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CollegeEvents.Database
{
    internal class DatabaseConnection : IDisposable
    {
        internal static readonly string DbName = "CollegeEvents_COP4710";
        private static readonly string ConnectionString =
            $@"Server=localhost\SQLEXPRESS;Database={DbName};Trusted_Connection=True;";
        private static readonly string UseDbString = $"USE {DbName};";

        private SqlConnection _connection;
        private bool _disposed = false;

        public DatabaseConnection()
            : this(null)
        { }

        internal DatabaseConnection(string overrideDbName)
        {
            this._connection = new SqlConnection(
                overrideDbName == null ?
                    ConnectionString :
                    ConnectionString.Replace(DbName, overrideDbName));
            this._connection.Open();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                try
                {
                    _connection.Dispose();
                }
                catch (Exception)
                { }

                _disposed = true;
            }
        }
        public object ExecuteScaler(string q)
        {
            try
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = q;
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }

        public void ExecuteReader(string q, Action<IDataReader> readAction)
        {
            try
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = q;
                    readAction.Invoke(cmd.ExecuteReader());
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }

        public int ExecuteNonQuery(string q)
        {
            try
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = q;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }
    }
}
