using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeEvents.Website.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            Session.Pull(this);
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            Session.SetUser(this, null);
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection data)
        {
            User u;
            if (Database.DatabaseInterface.TryLogin(data["username"], data["password"], out u))
            {
                Session.SetUser(this, u);
                return RedirectToAction("Index");
            }
            else
            {
                Session.Pull(this);
                ViewBag.Message = "Failed to login. Username or password incorrect.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Session.SetUser(this, null);
            return RedirectToAction("Index");
        }
    }
}
