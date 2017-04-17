using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeEvents.Database
{
    public static class DatabaseInterface
    {
        static readonly string Users = "Users";
        static readonly string Users_Id = "user_id";
        static readonly string Users_Username = "username";
        static readonly string Users_Password = "password";
        static readonly string Users_Privilege = "privilege";
        static readonly string Users_FirstName = "firstname";
        static readonly string Users_LastName = "lastname";
        static readonly string Users_Email = "email";

        static readonly string Schools = "Schools";
        static readonly string Schools_Id = "school_id";
        static readonly string Schools_Name = "name";
        static readonly string Schools_Location = "location";
        static readonly string Schools_About = "about";
        static readonly string Schools_NumStudents = "numstudents";

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

                // Users
                con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Users};");
                con.ExecuteNonQuery(
                    $"CREATE TABLE {Users} (" +
                        $"{Users_Id}        INT             NOT NULL PRIMARY KEY IDENTITY," +
                        $"{Users_Username}  varchar(30)     NOT NULL UNIQUE," +
                        $"{Users_Password}  nvarchar(MAX)   NOT NULL," +
                        $"{Users_Privilege} INT             NOT NULL," +
                        $"{Users_FirstName} nvarchar(MAX)   NOT NULL," +
                        $"{Users_LastName}  nvarchar(MAX)   NOT NULL," +
                        $"{Users_Email}     nvarchar(MAX)   NOT NULL" +
                    ");");

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

            InsertSchools(School.Examples);
            InsertUsers(User.Examples);
        }

        public static bool InsertUser(User u)
        {
            return InsertUsers(new User[] { u });
        }

        private static bool InsertUsers(IEnumerable<User> us)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    foreach (var u in us)
                    {
                        con.ExecuteNonQuery(
                            $"INSERT INTO {Users} " +
                            $"(" +
                                $"{Users_Username}, {Users_Password}, {Users_Privilege}, " +
                                $"{Users_FirstName}, {Users_LastName}, {Users_Email}" +
                            $") VALUES (" +
                                $"'{u.Username}', '{u.Password}', {(int)u.Privilege}, " +
                                $"'{u.FirstName}', '{u.LastName}', '{u.Email}'" +
                            ");");
                    }
                }
                catch (DatabaseException)
                {
                    throw;
                    return false;
                }
                return true;
            }
        }

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
                    return false;
                }
                return true;
            }
        }

        public static User[] SelectUsers()
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    List<User> us = new List<User>();
                    con.ExecuteReader(
                        $"SELECT * FROM {Users};",
                        (r) =>
                        {
                            while (r.Read())
                            {
                                us.Add(new User()
                                {
                                    Id = (int)r[Users_Id],
                                    Username = (string)r[Users_Username],
                                    Password = (string)r[Users_Password],
                                    Privilege = (PrivilegeLevel)r[Users_Privilege],
                                    FirstName = (string)r[Users_FirstName],
                                    LastName = (string)r[Users_LastName],
                                    Email = (string)r[Users_Email]
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

        public static School[] SelectSchools()
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    List<School> us = new List<School>();
                    con.ExecuteReader(
                        $"SELECT * FROM {Schools};",
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
    }
}