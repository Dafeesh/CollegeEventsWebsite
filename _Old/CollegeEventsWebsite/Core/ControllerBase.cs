using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeEventsWebsite
{
    public abstract class CollegeEventsController : Controller
    {
        protected CollegeEventsController()
        {
            this.ControllerName = this.GetType().Name.Replace("Controller", "");
        }

        protected string ControllerName
        {
            get { return (string)ViewBag.ControllerName; }
            set { ViewBag.ControllerName = value; }
        }
    }
}