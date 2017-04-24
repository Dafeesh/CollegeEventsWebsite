using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeEvents.Database
{
    public static partial class DatabaseInterface
    {
        public static void ResetDatabase()
        {
            /*
            using (DatabaseConnection masterCon = new DatabaseConnection("master"))
            {
                // Database
                masterCon.ExecuteNonQuery(
                    $"DROP DATABASE IF EXISTS {DatabaseConnection.DbName};" +
                    $"CREATE DATABASE {DatabaseConnection.DbName};");
            }
            */

            using (DatabaseConnection con = new DatabaseConnection())
            {
                ClearEvents(con);
                ClearOrgs(con);
                ClearSchools(con);
                ClearUsers(con);
            }

            InsertSchools(School.Examples);
            InsertUsers(User.Examples);
            InsertOrganizations(Organization.Examples);
            InsertEvents(PlannedEvent.Examples);

            foreach (var o in Organization.Examples)
            {
                foreach (var u in User.Examples)
                {
                    InsertMemberOfOrg(u.Id, o.Id);
                }
            }
        }
    }
}