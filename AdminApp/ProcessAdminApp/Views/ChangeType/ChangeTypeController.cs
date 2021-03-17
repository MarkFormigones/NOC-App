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
    public class ChangeTypeController : BaseController
    {
        //
        // GET: /ChangeType/

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
            var ParentList = dataMgr.GetParent_List();



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


            DAL.ProcessCategory newCat = new DAL.ProcessCategory();
            newCat.Id = 0;

            newCat.Name = "Root Parent";
            newCat.Value = "0";
            ParentList.Add(newCat);


            pInfoM.ParentListData = ToGroupParentListItems(ParentList, -1);
            //SelectListItem newRoot = new SelectListItem();
            //newRoot.Text = "Root Parent";
            //newRoot.Value = "0";
            //pInfoM.ParentListData.ToList().Add(newRoot);
            pInfoM.ParentListData = pInfoM.ParentListData;
            //pInfoM.BUnitId = int.Parse(rId.ToString());
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }


        public static IEnumerable<System.Web.Mvc.SelectListItem> ToGroupParentListItems(IEnumerable<DAL.ProcessCategory> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.Name, Value = obj.Id.ToString() });
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
                        //var parentList = dataMgr.ProcessCategories
                        //    .Where(x => x.TypeId == 1 && x.ParentId == 0 && x.IsDeleted != true).OrderByDescending(x => x.Id)
                        //    .ToList();

                        //model.ParentName = ToGroupParentListItems(parentList, -1);
                        items.Name = model.Name;
                        items.Value = model.Value;
                        items.CompanyId = model.CompanyId;
                        items.ProcessId = model.ProcessId;
                        items.BUnitId = model.BUnitId;
                        items.DivisionId = model.DivisionId;
                        items.ParentId = model.ParentListId;
                        items.Dated = model.Dated;
                        items.TypeId = 1;

                        if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
                        items.IsActive = model.IsActive;
                      
                        if (isnew) { items.IsDeleted = false; dataMgr.ProcessCategories.Add(items); }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.Id;
                        if (isnew)
                        { logMsg = "ChangeType Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }
                        else
                        { logMsg = "ChangeType Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }

                        transaction.Complete();

                        base.SetOperationCompleted("success", "Saved", "successfully");

                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("error", "Operation", "failed");
                        base.SetInnerOperationCompleted("error", "Operation", e.Message);
                        ModelState.AddModelError("error", e.Message);
                    }
                }
            }
            else
            {
                logMsg = "invalid model state";
                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1);
                base.SetOperationCompleted("info", "Operation", logMsg);
            }

            return RedirectToAction("define", "ChangeType", new { rid = tempId });
        }
        public ActionResult Delete(int rid)
        {
            string logMsg = "";

            DAL.DataManager dataMgr = new DAL.DataManager();
            //check if parent and has a child.
            var parent = dataMgr.ProcessCategories.Where(x => x.ParentId == rid && x.IsActive == true && x.TypeId == 1).ToList();
            if (parent.Count > 0)
            {
                ModelState.AddModelError("Error", "Deletion Error! The item is a parent and has one or more dependencies.");
                TempData["ParentError"] = ModelState["Error"].Errors[0].ErrorMessage;
              
            }

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {

                        //DAL.DataManager dataMgr = new DAL.DataManager();                       
                        var items = dataMgr.ProcessCategories.Where(x => x.Id == rid).SingleOrDefault();

                        items.IsDeleted = true;  //Delete the Object 
                        items.IsActive = false;                    
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "ChangeType Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {

                        ViewBag.status = "Error";
                        logMsg = "ChangeType Deleted-ERR";
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
            return RedirectToAction("ChangeTypeList", "ChangeType");
        }
        public ActionResult Deactivate(int rid, Boolean val)
        {
            string msg = "Deactivated";
            DAL.DataManager dataMgr = new DAL.DataManager();
            if (val == true) { msg = "Activated"; }

            string logMsg = "";
            //check if parent and has a child.
            var parent = dataMgr.ProcessCategories.Where(x => x.ParentId == rid && x.IsActive == true && x.TypeId == 1).ToList();
            if (parent.Count > 0)
            {
                ModelState.AddModelError("Error", "Deactivate Error! The item is a parent and has one or more dependencies.");
                TempData["ParentError"] = ModelState["Error"].Errors[0].ErrorMessage;

            }

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        var items = dataMgr.ProcessCategories.Where(x => x.Id == rid).SingleOrDefault();
                        items.IsActive = val;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        logMsg = "ChangeType" + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "ChangeType" + msg + "-ERR";
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
            return RedirectToAction("ChangeTypeList", "ChangeType");
        }

        [HttpGet]

     
        public ViewResult ChangeTypeList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetChangeType_List();
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
            var items = dataMgr.GetChangeType_List();
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


    }
}
