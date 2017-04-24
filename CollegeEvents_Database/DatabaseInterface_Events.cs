using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeEvents.Database
{
    public static partial class DatabaseInterface
    {
        public static readonly string Events = "Events";
        public static readonly string Events_Id = "event_id";
        public static readonly string Events_Name = "name";
        public static readonly string Events_About = "about";
        public static readonly string Events_Location = "location";
        public static readonly string Events_StartTime = "starttime";
        public static readonly string Events_EndTime = "endtime";
        public static readonly string Events_HostSchoolId = "host_school_id";
        public static readonly string Events_HostOrgId = "host_org_id";

        public static bool InsertEvent(PlannedEvent e)
        {
            return InsertEvents(new PlannedEvent[] { e });
        }
        private static bool InsertEvents(IEnumerable<PlannedEvent> ee)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    foreach (var e in ee)
                    {
                        var q = $"INSERT INTO {Events} " +
                            $"(" +
                                $"{Events_Name}, {Events_About}, {Events_Location}, " +
                                $"{Events_StartTime}, {Events_EndTime} ";
                        if (e.HostSchoolId != null)
                            q += $", {Events_HostSchoolId} ";
                        if (e.HostOrgId != null)
                            q += $", {Events_HostOrgId} ";
                        q += $") VALUES (" +
                                $"'{e.Name}', '{e.About}', '{e.Location}', " +
                                $"'{e.StartTime}', '{e.EndTime}'";
                        if (e.HostSchoolId != null)
                            q += $", {e.HostSchoolId.Value} ";
                        if (e.HostOrgId != null)
                            q += $", {e.HostOrgId} ";
                        q += ");";

                        con.ExecuteNonQuery(q);
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

        public static PlannedEvent[] SelectEvents(int? hostSchoolId = null, int? hostOrgId = null, int ? selId = null)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    string q = $"SELECT * FROM {Events} ";
                    if (hostSchoolId != null || hostOrgId != null)
                    {
                        q += "WHERE ";
                        bool condUsed = false;

                        if (hostSchoolId != null)
                        {
                            q += $"{Events_HostSchoolId} = {hostSchoolId.Value} ";
                        }
                        if (hostOrgId != null)
                        {
                            if (condUsed)
                            {
                                q += "AND ";
                                condUsed = true;
                            }
                            q += $"{Events_HostOrgId} = {hostOrgId.Value} ";
                        }
                        if (hostOrgId != null)
                        {
                            if (condUsed)
                            {
                                q += "AND ";
                                condUsed = true;
                            }
                            q += $"{Events_Id} = {selId.Value} ";
                        }
                    }
                    q += ";";

                    List<PlannedEvent> ps = new List<PlannedEvent>();
                    con.ExecuteReader(q,
                        (r) =>
                        {
                            while (r.Read())
                            {
                                ps.Add(new PlannedEvent()
                                {
                                    Id = (int)r[Events_Id],
                                    Name = (string)r[Events_Name],
                                    About = (string)r[Events_About],
                                    Location = (string)r[Events_Location],
                                    StartTime = DateTime.Parse(r[Events_StartTime].ToString()),
                                    EndTime = DateTime.Parse(r[Events_EndTime].ToString()),
                                    HostSchoolId = (r[Events_HostSchoolId] == DBNull.Value ? null : (int?)r[Events_HostSchoolId]),
                                    HostOrgId = (r[Events_HostSchoolId] == DBNull.Value ? null : (int?)r[Events_HostSchoolId])
                                });
                            }
                        });
                    return ps.ToArray();
                }
                catch (DatabaseException)
                {
                    throw;
                }
            }
        }

        private static void ClearEvents(DatabaseConnection con)
        {
            // Schools
            con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Events};");
            con.ExecuteNonQuery(
                $"CREATE TABLE {Events} (" +
                    $"{Events_Id}           INT             NOT NULL PRIMARY KEY IDENTITY," +
                    $"{Events_Name}         varchar(30)     NOT NULL UNIQUE," +
                    $"{Events_About}        nvarchar(MAX)   NOT NULL," +
                    $"{Events_Location}     nvarchar(MAX)   NOT NULL," +
                    $"{Events_StartTime}    nvarchar(MAX)   NOT NULL," +
                    $"{Events_EndTime}      nvarchar(MAX)   NOT NULL," +
                    $"{Events_HostSchoolId} INT," +
                    $"{Events_HostOrgId}    INT" +
                ");");
        }
    }
}
