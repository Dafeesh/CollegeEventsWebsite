using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CollegeEvents.Website.Controllers
{
    public class OrgsController : Controller
    {
        public IActionResult Index()
        {
            List<Tuple<Organization, User[]>> orgs = new List<Tuple<Organization, User[]>>();

            var orgList = Database.DatabaseInterface.SelectOrganizations();
            foreach (var org in orgList)
            {
                orgs.Add(new Tuple<Organization, CollegeEvents.User[]>(
                    org, Database.DatabaseInterface.SelectMembersOfOrg(org.Id) ));
            }

            ViewBag.Orgs = orgs.ToArray();
            Session.Pull(this);
            return View();
        }

        public IActionResult ToggleJoin(int? id = 0)
        {
            var u = Session.GetUser(this);
            if (u != null)
            {
                Database.DatabaseInterface.InsertMemberOfOrg(u.Id, id.Value);
            }

            Session.Pull(this);
            return RedirectToAction("Index");
        }
    }
}
