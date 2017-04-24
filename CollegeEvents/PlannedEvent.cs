using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeEvents
{
    public class PlannedEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? HostSchoolId { get; set; }
        public int? HostOrgId { get; set; }
        
        public PlannedEvent(
            int id = 0,
            string name = "", string about = "", string location = "", 
            DateTime startTime = default(DateTime), DateTime endTime = default(DateTime),
            int? hostSchId = null, int? hostOrgId = null)
        {
            this.Id = id;
            this.Name = name;
            this.About = about;
            this.Location = location;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.HostSchoolId = hostSchId;
            this.HostOrgId = hostOrgId;
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

            //Time
            if (StartTime < DateTime.Now || EndTime < DateTime.Now)
            {
                return "Time must be in the future.";
            }

            return null;
        }

        private static int idIter = 1;
        public static PlannedEvent[] Examples =
        {
            new PlannedEvent()
            {
                Id = idIter++,
                Name = "Some Public Event",
                About = "An event for everyone!!!",
                Location = "Classroom Building II",
                StartTime = DateTime.Now + TimeSpan.FromDays(1),
                EndTime = DateTime.Now + TimeSpan.FromDays(1) + TimeSpan.FromHours(2),
            },
            new PlannedEvent()
            {
                Id = idIter++,
                Name = "Some Private Event",
                About = "An event only for UCF students.",
                Location = "Student Union",
                StartTime = DateTime.Now + TimeSpan.FromDays(2),
                EndTime = DateTime.Now + TimeSpan.FromDays(2) + TimeSpan.FromHours(2),
                HostSchoolId = 1
            },
            new PlannedEvent()
            {
                Id = idIter++,
                Name = "Some RSO Event",
                About = "An event only for one RSO...",
                Location = "Engineering 1",
                StartTime = DateTime.Now + TimeSpan.FromDays(3),
                EndTime = DateTime.Now + TimeSpan.FromDays(3) + TimeSpan.FromHours(2),
                HostSchoolId = 1,
                HostOrgId = 1
            }
        };
    }
}
