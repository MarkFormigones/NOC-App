using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eNOC.Views.Dashboard
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult External()
        {
            return View();
        }

        public ActionResult User()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}