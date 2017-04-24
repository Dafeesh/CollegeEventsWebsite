using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeEvents.Database
{
    public static partial class DatabaseInterface
    {
        #region Headers

        static readonly string Users = "Users";
        static readonly string Users_Id = "user_id";
        static readonly string Users_Username = "username";
        static readonly string Users_Password = "password";
        static readonly string Users_Privilege = "privilege";
        static readonly string Users_FirstName = "firstname";
        static readonly string Users_LastName = "lastname";
        static readonly string Users_Email = "email";
        static readonly string Users_AssocSchoolId = "assoc_school_id";

        static readonly string Users_Admin = "Users_Admin";
        static readonly string Users_Admin_Id = "admin_id";
        static readonly string Users_Admin_UserId = "user_id";

        static readonly string Users_SuperAdmin = "Users_SuperAdmin";
        static readonly string Users_SuperAdmin_Id = "superadmin_id";
        static readonly string Users_SuperAdmin_UserId = "user_id";

        static readonly string Users_Student = "Users_Student";
        static readonly string Users_Student_Id = "student_id";
        static readonly string Users_Student_UserId = "user_id";

        static readonly string Users_MemberOfRSO = "Users_MemberOfRSO";
        static readonly string Users_MemberOfRSO_UserId = "user_id";
        static readonly string Users_MemberOfRSO_OrgId = "org_id";

        static readonly string Users_SubToEvent = "Users_SubToEvent";
        static readonly string Users_SubToEvent_UserId = "user_id";
        static readonly string Users_SubToEvent_EventId = "event_id";

        #endregion


        public static bool TryLogin(string usernameInput, string passwordInput, out User u)
        {
            u = SelectUsers(username: usernameInput).FirstOrDefault();
            if (u == null)
                return false;
            return u.Password.Equals(passwordInput);

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
                                $"{Users_FirstName}, {Users_LastName}, {Users_Email}," +
                                $"{Users_AssocSchoolId} " +
                            $") VALUES (" +
                                $"'{u.Username}', '{u.Password}', {(int)u.Privilege}, " +
                                $"'{u.FirstName}', '{u.LastName}', '{u.Email}', " +
                                $"{u.AssociatedSchoolId} " +
                            ");");

                        var newUser = SelectUsers(username: u.Username).First();
                        if (newUser == null)
                            throw new DatabaseException("Could not find the user just inserted into DB.");

                        switch (u.Privilege)
                        {
                            case PrivilegeLevel.Student:
                                con.ExecuteNonQuery(
                                    $"INSERT INTO {Users_Student} " +
                                    $"(" +
                                        $"{Users_Student_UserId}" +
                                    $") VALUES (" +
                                        $"'{newUser.Id}'" +
                                    ");");
                                break;

                            case PrivilegeLevel.Admin:
                                con.ExecuteNonQuery(
                                    $"INSERT INTO {Users_Admin} " +
                                    $"(" +
                                        $"{Users_Student_UserId}" +
                                    $") VALUES (" +
                                        $"'{newUser.Id}'" +
                                    ");");
                                break;

                            case PrivilegeLevel.SuperAdmin:
                                con.ExecuteNonQuery(
                                    $"INSERT INTO {Users_SuperAdmin} " +
                                    $"(" +
                                        $"{Users_Student_UserId}" +
                                    $") VALUES (" +
                                        $"'{newUser.Id}'" +
                                    ");");
                                break;
                        }
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

        public static User[] SelectUsersSubToEvent(int event_id)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    string q = $"SELECT * FROM {Users_SubToEvent} WHERE " +
                        $"{Users_SubToEvent_EventId} = {event_id} ;";

                    List<User> us = new List<User>();
                    con.ExecuteReader(q,
                        (r) =>
                        {
                            User u;
                            while (r.Read())
                            {
                                u = SelectUsers(id: (int)r[Users_MemberOfRSO_UserId]).FirstOrDefault();
                                if (u != null)
                                    us.Add(u);
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

        public static User[] SelectMembersOfOrg(int org_id)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    string q = $"SELECT * FROM {Users_MemberOfRSO} WHERE " +
                        $"{Users_MemberOfRSO_OrgId} = {org_id} ;";

                    List<User> us = new List<User>();
                    con.ExecuteReader(q,
                        (r) =>
                        {
                            User u;
                            while (r.Read())
                            {
                                u = SelectUsers(id: (int)r[Users_MemberOfRSO_UserId]).FirstOrDefault();
                                if (u != null)
                                    us.Add(u);
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
        public static User[] SelectUsers(int? id = null, string username = null)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    string q = $"SELECT * FROM {Users} ";
                    if (id != null || username != null)
                    {
                        q += "WHERE ";

                        if (id != null)
                        {
                            q += $"{Users_Id} = {id.Value} ";
                        }
                        if (username != null)
                        {
                            if (id != null)
                                q += "AND ";
                            q += $"{Users_Username} = '{username}' ";
                        }
                    }
                    q += ";";

                    List<User> us = new List<User>();
                    con.ExecuteReader(q,
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

        public static bool InsertMemberOfOrg(int user_id, int org_id)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    con.ExecuteNonQuery(
                        $"INSERT INTO {Users_MemberOfRSO} " +
                        $"(" +
                            $"{Users_MemberOfRSO_UserId}, {Users_MemberOfRSO_OrgId} " +
                        $") VALUES (" +
                            $"{user_id}, {org_id} " +
                        ");");
                }
                catch (DatabaseException)
                {
                    throw;
                    //return false;
                }
                return true;
            }
        }

        public static bool InsertSubToEvent(int user_id, int event_id)
        {
            using (DatabaseConnection con = new DatabaseConnection())
            {
                try
                {
                    con.ExecuteNonQuery(
                        $"INSERT INTO {Users_SubToEvent} " +
                        $"(" +
                            $"{Users_SubToEvent_UserId}, {Users_SubToEvent_EventId} " +
                        $") VALUES (" +
                            $"{user_id}, {event_id} " +
                        ");");
                }
                catch (DatabaseException)
                {
                    throw;
                    //return false;
                }
                return true;
            }
        }

        private static void ClearUsers(DatabaseConnection con)
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
                    $"{Users_Email}     nvarchar(MAX)   NOT NULL," +
                    $"{Users_AssocSchoolId}     INT     NOT NULL" +
                ");");
            // Users_Student
            con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Users_Student};");
            con.ExecuteNonQuery(
                $"CREATE TABLE {Users_Student} (" +
                    $"{Users_Student_Id}        INT         NOT NULL PRIMARY KEY IDENTITY," +
                    $"{Users_Student_UserId}    INT         NOT NULL UNIQUE" +
                ");");
            // Users_Admin
            con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Users_Admin};");
            con.ExecuteNonQuery(
                $"CREATE TABLE {Users_Admin} (" +
                    $"{Users_Admin_Id}        INT         NOT NULL PRIMARY KEY IDENTITY," +
                    $"{Users_Admin_UserId}    INT         NOT NULL UNIQUE" +
                ");");
            // Users_SuperAdmin
            con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Users_SuperAdmin};");
            con.ExecuteNonQuery(
                $"CREATE TABLE {Users_SuperAdmin} (" +
                    $"{Users_SuperAdmin_Id}        INT         NOT NULL PRIMARY KEY IDENTITY," +
                    $"{Users_SuperAdmin_UserId}    INT         NOT NULL UNIQUE" +
                ");");

            // Users_SuperAdmin
            con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Users_MemberOfRSO};");
            con.ExecuteNonQuery(
                $"CREATE TABLE {Users_MemberOfRSO} (" +
                    $"{Users_MemberOfRSO_UserId}    INT     NOT NULL," +
                    $"{Users_MemberOfRSO_OrgId}     INT     NOT NULL" +
                ");");

            // Users_SubToEvent
            con.ExecuteNonQuery($"DROP TABLE IF EXISTS {Users_SubToEvent};");
            con.ExecuteNonQuery(
                $"CREATE TABLE {Users_SubToEvent} (" +
                    $"{Users_SubToEvent_UserId}      INT     NOT NULL," +
                    $"{Users_SubToEvent_EventId}     INT     NOT NULL" +
                ");");
        }
    }
}
