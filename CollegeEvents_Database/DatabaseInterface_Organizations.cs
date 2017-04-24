using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeEvents.Database
{
    public static partial class DatabaseInterface
    {
        static readonly string Org = "Organizations";
        static readonly string Org_Id = "org_id";
        static readonly string Org_Name = "name";
        static readonly string Org_About = "about";
        static readonly string Org_HostSchoolId = "host_school_id";

        public static bool InsertOrganization(Organization s)
        {
            return InsertOrganizations(new Organization[] { s });
        }
        private static bool InsertOrganizations(IEnumerable<Organization> oo)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    foreach (var o in oo)
                    {
                        con.ExecuteNonQuery(
                            $"INSERT INTO {Org} " +
                            $"(" +
                                $"{Org_Name}, {Org_About}, {Org_HostSchoolId} " +
                            $") VALUES (" +
                                $"'{o.Name}', '{o.About}', {o.HostSchoolId} " +
                            ");");
                    }
                }
                catch (DatabaseException)
                {
                    throw;
                    //return false;
                }
                return true;
            }
        }

        public static Organization[] SelectOrganizations(int? id = null, string name = null, int? hostSchoolId = null)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    string q = $"SELECT * FROM {Org} ";
                    if (id != null || name != null)
                    {
                        q += "WHERE ";
                        bool condUsed = false;

                        if (id != null)
                        {
                            q += $"{Org_Id} = {id.Value} ";
                        }
                        if (name != null)
                        {
                            if (condUsed)
                            {
                                q += "AND ";
                                condUsed = true;
                            }
                            q += $"{Org_Name} = '{name}' ";
                        }
                        if (hostSchoolId != null)
                        {
                            if (condUsed)
                            {
                                q += "AND ";
                                condUsed = true;
                            }
                            q += $"{Org_HostSchoolId} = {hostSchoolId.Value} ";
                        }
                    }
                    q += ";";

                    List<Organization> os = new List<Organization>();
                    con.ExecuteReader(q,
                        (r) =>
                        {
                            while (r.Read())
                            {
                                os.Add(new Organization()
                                {
                                    Id = (int)r[Org_Id],
                                    Name = (string)r[Org_Name],
                                    About = (string)r[Org_About],
                                    HostSchoolId = (int)r[Org_HostSchoolId]
                                });
                            }
                        });
                    return os.ToArray();
                }
                catch (DatabaseException)
                {
                    throw;
                }
            }
        }

        private static void ClearOrgs(DatabaseConnection con)
        {
            // Schools
            con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Org};");
            con.ExecuteNonQuery(
                $"CREATE TABLE {Org} (" +
                    $"{Org_Id}          INT             NOT NULL PRIMARY KEY IDENTITY," +
                    $"{Org_Name}        varchar(30)     NOT NULL UNIQUE," +
                    $"{Org_About}       nvarchar(MAX)   NOT NULL," +
                    $"{Org_HostSchoolId}      INT       NOT NULL" +
                ");");
        }
    }
}
