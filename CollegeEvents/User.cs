using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeEvents
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public PrivilegeLevel Privilege { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public User(
            int id = 0, 
            string username = "", string pass = "", PrivilegeLevel priv = PrivilegeLevel.None,
            string firstName = "", string lastName = "", string email = "")
        {
            this.Id = id;
            this.Username = username;
            this.Password = pass;
            this.Privilege = priv;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        public bool IsValid(out string err)
        {
            err = GetValidityError();
            return err == null;
        }

        public string GetValidityError()
        {
            //Username
            if (!Username.All(c => (char.IsLetterOrDigit(c) || c == '_')))
            {
                return "Username can only contain alphanumeric and underscore characters.";
            }
            if (Username.Trim().Length < 5 ||
                Username.Trim().Length > 30)
            {
                return "Username must be 5-30 characters in length.";
            }

            //Password
            if (!Password.All(c => (char.IsLetterOrDigit(c) || c == '_')))
            {
                return "Password can only contain alphanumeric and underscore characters.";
            }
            if (Password.Trim().Length < 5 ||
                Password.Trim().Length > 30)
            {
                return "Password must be 5-30 characters in length.";
            }

            //FirstName
            if (!FirstName.All(c => (char.IsLetterOrDigit(c) || c == '_')))
            {
                return "First name can only contain alphanumeric and underscore characters.";
            }
            if (FirstName.Trim().Length < 2 ||
                FirstName.Trim().Length > 30)
            {
                return "First name must be 2-30 characters in length.";
            }

            //FirstName
            if (!LastName.All(c => (char.IsLetterOrDigit(c) || c == '-' || c == ' ')))
            {
                return "Last name can only contain alphanumeric characters, dashes, and spaces.";
            }
            if (LastName.Trim().Length < 2 ||
                LastName.Trim().Length > 30)
            {
                return "Last name must be 2-30 characters in length.";
            }

            //Email
            string el = Email.ToLower();
            if (!el.Contains("@"))
            {
                return "Email must contain an @ character.";
            }

            return null;
        }

        public static User[] Examples =
        {
            new User()
            {
                //Id = ...
                Username = "SuperUser",
                Password = "12345",
                Privilege = PrivilegeLevel.SuperAdmin,
                FirstName = "SuperFirst",
                LastName = "SuperLast",
                Email = "super@knights.ucf.edu"
            },
            new User()
            {
                //Id = ...
                Username = "AdminUser",
                Password = "12345",
                Privilege = PrivilegeLevel.Admin,
                FirstName = "AdminFirst",
                LastName = "AdminLast",
                Email = "admin@knights.ucf.edu"
            },
            new User()
            {
                //Id = ...
                Username = "StudentUser",
                Password = "12345",
                Privilege = PrivilegeLevel.Student,
                FirstName = "StudentFirst",
                LastName = "StudentLast",
                Email = "student@knights.ucf.edu"
            }
        };
    }
}
