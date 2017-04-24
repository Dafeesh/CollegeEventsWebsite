using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollegeEvents.Website.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeEvents.Website
{
    public static class Session
    {
        public static void SetUser(Controller c, User u)
        {
            c.HttpContext.Session.SetObjectAsJson("USER", u);
        }
        public static User GetUser(Controller c)
        {
            try
            {
                return c.HttpContext.Session.GetObjectFromJson<User>("USER");
            }
            catch (Exception)
            { return null; }
        }

        public static void Pull(Controller c)
        {
            var u = GetUser(c);
            c.ViewBag.User = u;
            if (u != null)
            {
                c.ViewBag.FirstName = u.FirstName;
                c.ViewBag.Privilege = u.Privilege;
            }
        }
    }
}
