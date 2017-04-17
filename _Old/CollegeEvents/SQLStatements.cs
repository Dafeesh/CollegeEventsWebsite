using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeEvents
{
    public static class SQLStatements
    {
        public static class Tables
        {
            public static string TryCreate_Accounts()
            {
                return
                    @"CREATE TABLE IF NOT EXISTS Accounts" +
                    @"(id INTEGER, ...);";
            }

        }
    }
}
