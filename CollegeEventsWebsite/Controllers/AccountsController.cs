using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollegeEvents;

namespace CollegeEventsWebsite.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult All()
        {
            var db = new DatabaseConnection();

            var list = new List<Account>();
            list.Add(new Account()
            {
                Name = "Bob Smith",
                Email = "bob@ucf.edu",
                GrantedAccess = AccessLevel.SuperAdmin
            });
            list.Add(new Account()
            {
                Name = "Tim Timmy",
                Email = "tim@ucf.edu",
                GrantedAccess = AccessLevel.Admin
            });
            list.Add(new Account()
            {
                Name = "Stew Stewerson",
                Email = "stew@ucf.edu",
                GrantedAccess = AccessLevel.Student
            });
            ViewBag.AccountsList = list;
            return View(ViewNames.Accounts.ShowAccList);
        }
    }
}