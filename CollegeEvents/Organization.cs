using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeEvents
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public int HostSchoolId { get; set; }

        public Organization(
            int id = 0,
            string name = "", string about = "", int hostSchoolId = 0)
        {
            this.Id = id;
            this.Name = name;
            this.About = about;
            this.HostSchoolId = hostSchoolId;
        }

        public bool IsValid(out string err)
        {
            err = GetValidityError();
            return err == null;
        }

        public string GetValidityError()
        {
            // Name
            if (!Name.IsSQLValid())
            {
                return "Name contains invalid characters.";
            }
            if (Name.Trim().Length < 2 ||
                Name.Trim().Length > 30)
            {
                return "Name must be 2-30 characters in length.";
            }

            // About
            if (!About.IsSQLValid())
            {
                return "About contains invalid characters.";
            }
            if (About.Trim().Length < 2 ||
                About.Trim().Length > 30)
            {
                return "About must be 2-30 characters in length.";
            }

            return null;
        }

        private static int idIter = 1;
        public static Organization[] Examples =
        {
            new Organization()
            {
                Id = idIter++,
                Name = "SGA",
                About = "Student Government Association",
                HostSchoolId = 1
            },
            new Organization()
            {
                Id = idIter++,
                Name = "CAB",
                About = "Student Activities Board",
                HostSchoolId = 1
            }
        };

    }
}
