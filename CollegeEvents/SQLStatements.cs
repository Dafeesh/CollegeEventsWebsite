using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeEvents
{
    public static class SQLStatements
    {
        public static readonly string TryCreateTable_Accounts =
    @"
CREATE TABLE IF NOT EXISTS Accounts 
(id INTEGER, ...);
";

    }
}
