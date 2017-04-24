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
        public int AssociatedSchoolId { get; set; }

        public User(
            int id = 0, 
            string username = "", string pass = "", PrivilegeLevel priv = PrivilegeLevel.None,
            string firstName = "", string lastName = "", string email = "", int assocSchoolId = 0)
        {
            this.Id = id;
            this.Username = username;
            this.Password = pass;
            this.Privilege = priv;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.AssociatedSchoolId = assocSchoolId;
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

            //Associated School
            if (AssociatedSchoolId <= 0)
            {
                return "Must associate with an existing school.";
            }

            return null;
        }

        private static int idIter = 1;
        public static User[] Examples =
        {
            new User()
            {
                Id = idIter++,
                Username = "super1",
                Password = "12345",
                Privilege = PrivilegeLevel.SuperAdmin,
                FirstName = "SuperFirst",
                LastName = "SuperLast",
                Email = "super@knights.ucf.edu",
                AssociatedSchoolId = 1
            },
            new User()
            {
                Id = idIter++,
                Username = "admin1",
                Password = "12345",
                Privilege = PrivilegeLevel.Admin,
                FirstName = "AdminFirst1",
                LastName = "AdminLast1",
                Email = "admin1@knights.ucf.edu",
                AssociatedSchoolId = 1
            },
            new User()
            {
                Id = idIter++,
                Username = "admin2",
                Password = "12345",
                Privilege = PrivilegeLevel.Admin,
                FirstName = "AdminFirst2",
                LastName = "AdminLast2",
                Email = "admin2@knights.ucf.edu",
                AssociatedSchoolId = 1
            },
            new User()
            {
                Id = idIter++,
                Username = "student1",
                Password = "12345",
                Privilege = PrivilegeLevel.Student,
                FirstName = "StudentFirst1",
                LastName = "StudentLast1",
                Email = "student1@knights.ucf.edu",
                AssociatedSchoolId = 1
            },
            new User()
            {
                Id = idIter++,
                Username = "student2",
                Password = "12345",
                Privilege = PrivilegeLevel.Student,
                FirstName = "StudentFirst2",
                LastName = "StudentLast2",
                Email = "student2@knights.ucf.edu",
                AssociatedSchoolId = 1
            },
            new User()
            {
                Id = idIter++,
                Username = "student3",
                Password = "12345",
                Privilege = PrivilegeLevel.Student,
                FirstName = "StudentFirst3",
                LastName = "StudentLast3",
                Email = "student3@knights.ucf.edu",
                AssociatedSchoolId = 1
            },
            new User()
            {
                Id = idIter++,
                Username = "student4",
                Password = "12345",
                Privilege = PrivilegeLevel.Student,
                FirstName = "StudentFirst4",
                LastName = "StudentLast4",
                Email = "student4@knights.ucf.edu",
                AssociatedSchoolId = 1
            },
            new User()
            {
                Id = idIter++,
                Username = "student5",
                Password = "12345",
                Privilege = PrivilegeLevel.Student,
                FirstName = "StudentFirst5",
                LastName = "StudentLast5",
                Email = "student5@knights.ucf.edu",
                AssociatedSchoolId = 1
            }
        };
    }
}
