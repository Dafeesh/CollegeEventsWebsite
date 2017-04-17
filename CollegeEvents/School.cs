using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeEvents
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string About { get; set; }
        public int NumStudents { get; set; }

        public School(
            int id = 0,
            string name = "", string location = "",
            string about = "", int numStudents = 0)
        {
            this.Id = id;
            this.Name = name;
            this.Location = location;
            this.About = about;
            this.NumStudents = numStudents;
        }

        public bool IsValid(out string err)
        {
            err = GetValidityError();
            return err == null;
        }

        public string GetValidityError()
        {
            // Name
            if (!Name.All(c => (char.IsLetterOrDigit(c) || c == '-' || c == ' ')))
            {
                return "Name can only contain alphanumeric characters, dashes, and spaces.";
            }
            if (Name.Trim().Length < 2 ||
                Name.Trim().Length > 30)
            {
                return "Name must be 2-30 characters in length.";
            }

            // Location
            if (Name.Trim().Length < 2 ||
                Name.Trim().Length > 30)
            {
                return "Location must be 2-30 characters in length.";
            }

            // About
            if (!About.All(c => (char.IsLetterOrDigit(c) || c == '-' || c == ' ')))
            {
                return "About can only contain alphanumeric characters, dashes, and spaces.";
            }
            if (About.Trim().Length < 2 ||
                About.Trim().Length > 30)
            {
                return "About must be at least 2 characters in length.";
            }

            // Name
            if (NumStudents <= 0)
            {
                return "Number of students must be a positive number.";
            }

            return null;
        }

        public static School[] Examples =
        {
            new School()
            {
                Name = "University of Central Florida",
                Location = "Orlando, FL",
                About = "Home of the Knights",
                NumStudents = 50000
            },
            new School()
            {
                Name = "University of Florida",
                Location = "Gainesville, FL",
                About = "Home of the Gators",
                NumStudents = 40000
            }
        };

    }
}
