using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CollegeEvents.Website.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.EventsList = Database.DatabaseInterface.SelectEvents();

            Session.Pull(this);
            return View();
        }

        public IActionResult Add()
        {
            Session.Pull(this);
            return View();
        }

        [HttpPost]
        public IActionResult Add(Microsoft.AspNetCore.Http.IFormCollection data)
        {
            PlannedEvent e = new PlannedEvent()
            {
                Name = HttpContext.Request.Form["name"].ToString().Trim(),
                Location = HttpContext.Request.Form["location"].ToString().Trim(),
                About = HttpContext.Request.Form["about"].ToString().Trim(),
                StartTime = DateTime.Parse(HttpContext.Request.Form["starttime"].ToString().Trim()),
                EndTime = DateTime.Parse(HttpContext.Request.Form["endtime"].ToString().Trim())
            };

            string errorMessage;
            if (e.IsValid(out errorMessage))
            {
                if (Database.DatabaseInterface.InsertEvent(e))
                {
                    ViewBag.Message = $"{e.Name} was successfully added!";
                    ViewBag.MessageIsError = false;
                }
                else
                {
                    ViewBag.Message = $"That event already exists!";
                    ViewBag.MessageIsError = true;
                }
            }
            else
            {
                ViewBag.Message = "Error: " + errorMessage;
                ViewBag.MessageIsError = true;
            }

            Session.Pull(this);
            return RedirectToAction("Index");
        }

        public IActionResult FromSchool()
        {
            ViewBag.EventsList = Database.DatabaseInterface.SelectEvents(
                hostSchoolId: Session.GetUser(this).AssociatedSchoolId);

            Session.Pull(this);
            return View("Index");
        }

        public IActionResult FromOrg()
        {
            ViewBag.EventsList = Database.DatabaseInterface.SelectEvents(
                hostSchoolId: Session.GetUser(this).AssociatedSchoolId);

            Session.Pull(this);
            return View("Index");
        }

        public IActionResult Inspect(int? id)
        {
            ViewBag.Event = Database.DatabaseInterface.SelectEvents(selId: id.Value).FirstOrDefault();

            Session.Pull(this);
            return View();
        }
    }
}
