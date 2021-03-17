using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Hydron.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Administrator Page";

            return View();
        }

        public ActionResult SajaaAsset()
        {
            return View();
        }

        public ActionResult tree()
        {
            return View();
        }


        //public string _GetTour(string contName,string actName)
        //{
        //    DAL.DataManager dataMgr = new DAL.DataManager();
            //var items = dataMgr.TourTs.Where(x=>x.Controller==contName && x.Action==actName && x.IsDeleted==false).OrderBy(x=>x.TargetOrder).ToList();

            //var ls_items = new List<Models.Definitions.AppLockupModel>();
            //foreach (var item in items)
            //{
            //    var l_item = new Models.Definitions.AppLockupModel();
            //    l_item.Id = item.Id;
            //    l_item.Name = item.Name;
            //    l_item.CategoryId = item.CategoryId;
            //    l_item.IsDeleted = item.IsDeleted;
            //    l_item.Dated = item.Dated;
            //    l_item.Desc = item.Desc;
            //    l_item.Icon = item.Icon;
            //    l_item.IconClass = item.IconClass;
            //    l_item.GeneralClass = item.GeneralClass;
            //    l_item.IsActive = item.IsActive;
            //    ls_items.Add(l_item);
            //}

            //JavaScriptSerializer serializer = new JavaScriptSerializer();

            //foreach (var item in items)
            //{
            //    item.placement = item.placement.Trim();
            //}
            //var viewData =   items;
            //return  serializer.Serialize(viewData) ;


           

       // }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Message="Dashboard";

            return View();

        }






    }
}
