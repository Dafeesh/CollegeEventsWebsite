using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeEvents.Database
{
    public class DatabaseException : Exception
    {
        public DatabaseException(Exception baseException)
            : base("Failed to execute database command.", baseException)
        { }
    }
}
