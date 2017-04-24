using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeEvents.Database
{
    public static partial class DatabaseInterface
    {
        static readonly string Schools = "Schools";
        static readonly string Schools_Id = "school_id";
        static readonly string Schools_Name = "name";
        static readonly string Schools_Location = "location";
        static readonly string Schools_About = "about";
        static readonly string Schools_NumStudents = "numstudents";

        public static bool InsertSchool(School s)
        {
            return InsertSchools(new School[] { s });
        }
        private static bool InsertSchools(IEnumerable<School> ss)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    foreach (var s in ss)
                    {
                        con.ExecuteNonQuery(
                            $"INSERT INTO {Schools} " +
                            $"(" +
                                $"{Schools_Name}, {Schools_Location}, " +
                                $"{Schools_About}, {Schools_NumStudents}" +
                            $") VALUES (" +
                                $"'{s.Name}', '{s.Location}', " +
                                $"'{s.About}', {s.NumStudents}" +
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
        public static School[] SelectSchools(int? id = null, string name = null)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    string q = $"SELECT * FROM {Schools} ";
                    if (id != null || name != null)
                    {
                        q += "WHERE ";

                        if (id != null)
                        {
                            q += $"{Schools_Id} = {id.Value} ";
                        }
                        if (name != null)
                        {
                            if (id != null)
                                q += "AND ";
                            q += $"{Schools_Name} = '{name}' ";
                        }
                    }
                    q += ";";

                    List<School> us = new List<School>();
                    con.ExecuteReader(q,
                        (r) =>
                        {
                            while (r.Read())
                            {
                                us.Add(new School()
                                {
                                    Id = (int)r[Schools_Id],
                                    Name = (string)r[Schools_Name],
                                    Location = (string)r[Schools_Location],
                                    About = (string)r[Schools_About],
                                    NumStudents = (int)r[Schools_NumStudents]
                                });
                            }
                        });
                    return us.ToArray();
                }
                catch (DatabaseException)
                {
                    throw;
                }
            }
        }

        private static void ClearSchools(DatabaseConnection con)
        {
            // Schools
            con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Schools};");
            con.ExecuteNonQuery(
                $"CREATE TABLE {Schools} (" +
                    $"{Schools_Id}          INT             NOT NULL PRIMARY KEY IDENTITY," +
                    $"{Schools_Name}        varchar(30)     NOT NULL UNIQUE," +
                    $"{Schools_Location}    nvarchar(MAX)   NOT NULL," +
                    $"{Schools_About}       nvarchar(MAX)   NOT NULL," +
                    $"{Schools_NumStudents} INT             NOT NULL" +
                ");");
        }
    }
}
