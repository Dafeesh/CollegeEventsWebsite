using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CollegeEvents.Website.Controllers
{
    public class SchoolsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.SchoolsList = Database.DatabaseInterface.SelectSchools();
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            //ViewBag.ErrorMessage = "Hello GET";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Microsoft.AspNetCore.Http.IFormCollection data)
        {
            School s = new School()
            {
                Name = HttpContext.Request.Form["name"].ToString().Trim(),
                Location = HttpContext.Request.Form["location"].ToString().Trim(),
                About = HttpContext.Request.Form["about"].ToString().Trim(),
                NumStudents = int.Parse("0" + HttpContext.Request.Form["numstudents"].ToString().Trim())
            };

            ViewBag.Form_Name = s.Name;
            ViewBag.Form_Location = s.Location;
            ViewBag.Form_About = s.About;
            ViewBag.Form_NumStudents = s.NumStudents.ToString();

            string errorMessage;
            if (s.IsValid(out errorMessage))
            {
                if (Database.DatabaseInterface.InsertSchool(s))
                {
                    ViewBag.Message = $"{s.Name} was successfully added!";
                    ViewBag.MessageIsError = false;
                }
                else
                {
                    ViewBag.Message = $"That school already exists!";
                    ViewBag.MessageIsError = true;
                }
            }
            else
            {
                ViewBag.Message = "Error: " + errorMessage;
                ViewBag.MessageIsError = true;
            }

            return View();
        }
    }
}
