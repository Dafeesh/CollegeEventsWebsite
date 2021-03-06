﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeEvents.Website.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Session.Pull(this);

            ViewBag.UsersList = Database.DatabaseInterface.SelectUsers();
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            Session.Pull(this);

            ViewBag.Schools = Database.DatabaseInterface.SelectSchools();
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(IFormCollection data)
        {
            Session.Pull(this);

            ViewBag.Schools = Database.DatabaseInterface.SelectSchools();

            User u = new User()
            {
                Username = HttpContext.Request.Form["username"].ToString().Trim(),
                Password = HttpContext.Request.Form["password"].ToString().Trim(),
                Privilege = (PrivilegeLevel)int.Parse("0" + HttpContext.Request.Form["selectprivilege"].ToString().Trim()),
                FirstName = HttpContext.Request.Form["firstname"].ToString().Trim(),
                LastName = HttpContext.Request.Form["lastname"].ToString().Trim(),
                Email = HttpContext.Request.Form["email"].ToString().Trim(),
                AssociatedSchoolId = int.Parse("0" + HttpContext.Request.Form["selectschool"].ToString())
            };

            try
            {
                string errorMessage;
                if (u.IsValid(out errorMessage))
                {
                    if (Database.DatabaseInterface.InsertUser(u))
                    {
                        ViewBag.Message = $"{u.Username} was successfully added!";
                        ViewBag.MessageIsError = false;
                    }
                    else
                    {
                        ViewBag.Message = $"That username already exists!";
                        ViewBag.MessageIsError = true;
                    }
                }
                else
                {
                    ViewBag.Message = "Error: " + errorMessage;
                    ViewBag.MessageIsError = true;
                }
            }
            finally
            {
                if (ViewBag.MessageIsError == true)
                {
                    ViewBag.Form_Username = u.Username;
                    ViewBag.Form_Password = u.Password;
                    ViewBag.Form_FirstName = u.FirstName;
                    ViewBag.Form_LastName = u.LastName;
                    ViewBag.Form_Email = u.Email;
                }
            }

            return View("Add");
        }
    }
}
