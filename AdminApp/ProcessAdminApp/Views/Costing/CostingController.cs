using ImageResizer;
using Hydron.Controllers;
using Hydron.Models.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Hydron.Views
{
    public class CostingController : BaseController
    {
        //
        // GET: /Department/

        public PartialViewResult _subTree(int parentId)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetBDivisions_List(parentId);


            var ls_items = new List<Models.Definitions.TreeModel>();
            foreach (var item in items)
            {
                var treeModel = new Models.Definitions.TreeModel();
                treeModel.name = item.BUnitName;
                treeModel.Id = item.BUnitId;
                treeModel.controller = "BDivisions";
                treeModel.type = "folder";
                treeModel.additionalParameters = "{ id: 'F11' }";
                ls_items.Add(treeModel);
            }

            return PartialView(ls_items);

        }


        public PartialViewResult Portfolio()
        {
            return PartialView();
        }
        public ViewResult Define(int? rId, string vw)
        {

            DAL.DataManager dataMgr = new DAL.DataManager();
            var pInfo = new DAL.ProcessCategory();
            int adminId = -1;
            if (rId == null || rId == -1)
            {
                //pInfo = dataMgr.GetCompanyInfoById(uId);
                //SetUserParam(User.Identity.Name);// this will apply changes on session, avatar and email info
            }
            else
            {
                pInfo = dataMgr.GetCostingInfoById(rId);
            }


            var pInfoM = new ProcessCategoryModel(pInfo);
            //if (string.IsNullOrEmpty(pInfoM.DepartmentLogo)) { pInfoM.UseDefaultLogo = true; }//logo initialization
           
            //pInfoM.BUnitId = int.Parse(rId.ToString());
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }

        
        [HttpPost]
        public ActionResult Define(ProcessCategoryModel model)
        {
            int tempId = 0; string logMsg = "Division";
            if (ModelState.IsValid)
            {

                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {

                    try
                    {
                        bool isnew = true;
                        var items = new DAL.ProcessCategory();
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        if (model.Id > 0)
                        {
                            isnew = false;
                            items = dataMgr.ProcessCategories.Where(x => x.Id == model.Id).SingleOrDefault();
                        }
                        //items.BUnitId = 1;// model.BUnitId;
                        //items.DepartmentName = model.DepartmentName;
                        items.Name = model.Name;
                        items.Value = model.Value;
                        items.CompanyId = model.CompanyId;
                        items.ProcessId = model.ProcessId;
                        items.BUnitId = model.BUnitId;
                        items.DivisionId = model.DivisionId;
                        items.ParentId = 0;
                        items.Dated = model.Dated;
                        items.TypeId = 2;

                        if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
                        items.IsActive = model.IsActive;
                      
                        if (isnew) { items.IsDeleted = false; dataMgr.ProcessCategories.Add(items); }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.Id;
                        if (isnew)
                        { logMsg = "Costing Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW,  DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }
                        else
                        { logMsg = "Costing Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }

                        transaction.Complete();

                        base.SetOperationCompleted("success", "Saved", "successfully");

                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg + e.Message, tempId, -1, -1, -1,-1,-1,-1,-1,-1);
                        base.SetOperationCompleted("error", "Operation", "failed");
                        base.SetInnerOperationCompleted("error", "Operation", e.Message);
                        ModelState.AddModelError("error", e.Message);
                    }
                }
            }
            else
            {
                logMsg = "invalid model state";
                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
                base.SetOperationCompleted("info", "Operation", logMsg);
            }

            return RedirectToAction("define", "Costing", new { rid = tempId });
        }
        public ActionResult Delete(int rid)
        {
            string logMsg = "";
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {

                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var items = dataMgr.ProcessCategories.Where(x => x.Id == rid).SingleOrDefault();

                        items.IsDeleted = true;  //Delete the Object  
                        items.IsActive = false;                   
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "Costing Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "Costing Deleted-ERR";
                        ModelState.AddModelError("error", e.Message);
                        base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("error", "Operation", "failed");
                    }
                }
            }
            else
            {
                logMsg = "invalid model state";
                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                base.SetOperationCompleted("info", "Operation", logMsg);
            }
            return RedirectToAction("CostingList", "Costing");
        }
        public ActionResult Deactivate(int rid, Boolean val)
        {
            string msg = "Deactivated";
            if (val == true) { msg = "Activated"; }

            string logMsg = "";

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var items = dataMgr.ProcessCategories.Where(x => x.Id == rid).SingleOrDefault();
                        items.IsActive = val;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        logMsg = "Costing" + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "Costing" + msg + "-ERR";
                        ModelState.AddModelError("error", e.Message);
                        base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("error", msg, "failed");
                    }
                }
            }
            else
            {
                logMsg = "invalid model state";
                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                base.SetOperationCompleted("info", msg, "invalid model state");
            }

            //TempData["title"] = msg;
            //TempData["statusMsg"] = "successfully";
            return RedirectToAction("CostingList", "Costing");
        }

        [HttpGet]

     
        public ViewResult CostingList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetCosting_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.ProcessCategoryModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.ProcessCategoryModel();
                l_item.Id = item.Id;
                l_item.Name = item.Name;
                l_item.Value = item.Value;
                l_item.CompanyId = item.CompanyId;
                l_item.ProcessId = item.ProcessId;
                l_item.BUnitId = item.BUnitId;
                l_item.DivisionId = item.DivisionId;
                l_item.ParentId = item.ParentId;
                l_item.IsDeleted = item.IsDeleted;
                l_item.IsActive = item.IsActive;
                l_item.Dated = item.Dated;
                l_item.TypeId = item.TypeId;
                ls_items.Add(l_item);
            }

            return View(ls_items);
        }
        public PartialViewResult _GetList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetCosting_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.ProcessCategoryModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.ProcessCategoryModel();
                l_item.Id = item.Id;
                l_item.Name = item.Name;
                l_item.Value = item.Value;
                l_item.CompanyId = item.CompanyId;
                l_item.ProcessId = item.ProcessId;
                l_item.BUnitId = item.BUnitId;
                l_item.DivisionId = item.DivisionId;
                l_item.ParentId = item.ParentId;
                l_item.IsDeleted = item.IsDeleted;
                l_item.IsActive = item.IsActive;
                l_item.Dated = item.Dated;
                l_item.TypeId = item.TypeId;
                ls_items.Add(l_item);
            }

            return PartialView(ls_items);

        }

        //public static IEnumerable<System.Web.Mvc.SelectListItem> ToCountryListItems(IEnumerable<DAL.CountryList> lList, string selectedId)
        //{
        //    return lList.OrderBy(obj => obj.CountryId).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.CountryVal == selectedId.Trim().ToString()), Text = obj.CountryText, Value = obj.CountryVal.ToString() });
        //}


    }
}
