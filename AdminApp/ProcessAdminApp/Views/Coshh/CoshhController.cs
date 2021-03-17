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
    public class CoshhController : BaseController
    {
        //
        // GET: /ProcessFieldSetup/

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
            var pInfo = new DAL.COSHH();
            int adminId = -1;

            IList<DAL.OptionsLockup> OptionNameList;
            OptionNameList = dataMgr.GetRiskRating_List();

            if (rId == null || rId == -1)
            {
 
            }
            else
            {
                pInfo = dataMgr.GetCoshhInfoById(rId);
            }


            var pInfoM = new CoshhModel(pInfo);

            pInfoM.RiskRatingListData = ToRiskRatingListItems(OptionNameList, -1);

            /*if (string.IsNullOrEmpty(pInfoM.DepartmentLogo)) { pInfoM.UseDefaultLogo = true; }*///logo initialization



            //pInfoM.BUnitId = int.Parse(rId.ToString());
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }

        public ViewResult DefineNew(int? rId, string vw)
        {

            DAL.DataManager dataMgr = new DAL.DataManager();
            var pInfo = new DAL.ProcessMasterExt();
            int adminId = -1;

            IList<DAL.OptionsLockup> OptionNameList;
            OptionNameList = dataMgr.GetRiskRating_List();

            if (rId == null || rId == -1)
            {

            }
            else
            {
                pInfo = dataMgr.GetProcessMasterExtInfoById(rId);
            }


            var pInfoM = new ProcessMasterExtModel(pInfo);

            pInfoM.RiskRatingListData = ToRiskRatingListItems(OptionNameList, -1);

            /*if (string.IsNullOrEmpty(pInfoM.DepartmentLogo)) { pInfoM.UseDefaultLogo = true; }*///logo initialization



            //pInfoM.BUnitId = int.Parse(rId.ToString());
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }


        [HttpPost]
        public ActionResult Define(CoshhModel model)
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
                        var items = new DAL.COSHH();
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        if (model.Id > 0)
                        {
                            isnew = false;
                            items = dataMgr.COSHHs.Where(x => x.Id == model.Id).SingleOrDefault();
                        }
                        items.Location = model.Location;
                        items.Area = model.Area;
                        items.DateSubmit = model.DateSubmit;
                        items.PermitNo = model.PermitNo;
                        items.Description = model.Description;
                        items.Employee_risk = model.Employee_risk;
                        items.Contractors_risk = model.Contractors_risk;
                        items.Public_risk = model.Public_risk;
                        items.Substance = model.Substance;
                        items.WEL = model.WEL;
                        items.Hazards = model.Hazards;
                        items.ControlMeasures = model.ControlMeasures;
                        items.Monitor_required = model.Monitor_required;
                        items.FirstAidMeasure = model.FirstAidMeasure;
                        items.Storage = model.Storage;
                        items.IsControlled = model.IsControlled;
                        items.RiskRating = model.RiskRating;

                        if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
                        items.IsActive = model.IsActive;
                        
                        if (isnew) { items.IsDeleted = false; dataMgr.COSHHs.Add(items); }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.Id;
                        if (isnew)
                        { logMsg = "COSHH Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }
                        else
                        { logMsg = "COSHH Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }

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
                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
                base.SetOperationCompleted("info", "Operation", logMsg);
            }

            return RedirectToAction("define", "Coshh", new { rid = tempId });
        }

        [HttpPost]
        public ActionResult DefineNew(ProcessMasterExtModel model)
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
                        var items = new DAL.ProcessMasterExt();
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        if (model.ExtendId > 0)
                        {
                            isnew = false;
                            items = dataMgr.ProcessMasterExts.Where(x => x.ExtendId == model.ExtendId).SingleOrDefault();
                        }

                        items.Coshh_EmpRisk = model.Coshh_EmpRisk;
                        items.Coshh_ConRisk = model.Coshh_ConRisk;
                        items.Coshh_PubRisk = model.Coshh_PubRisk;
                        items.Coshh_Substance = model.Coshh_Substance;
                        items.Coshh_Wels = model.Coshh_Wels;
                        items.Coshh_Hazards = model.Coshh_Hazards;
                        items.coshh_ConMeasures = model.coshh_ConMeasures;
                        items.Coshh_isMonitored = model.Coshh_isMonitored;
                        items.Coshh_FirstAid = model.Coshh_FirstAid;
                        items.Storage = model.Storage;
                        items.Coshh_isControlled = model.Coshh_isControlled;
                        items.Coshh_Rating = model.Coshh_Rating;
                        items.ProcessMasterId = model.ProcessMasterId;

                        items.Chem_DateReceipt = DateTime.Now;

                        //region added by mark March 3, 2020
                        items.Coshh_isHazardGas = model.Coshh_isHazardGas;
                        items.Coshh_isHazardVapour = model.Coshh_isHazardVapour;
                        items.Coshh_isHazardMist = model.Coshh_isHazardMist;
                        items.Coshh_isHazardFume = model.Coshh_isHazardFume;
                        items.Coshh_isHazardDust = model.Coshh_isHazardDust;
                        items.Coshh_isHazardLiquid = model.Coshh_isHazardLiquid;
                        items.Coshh_isHazardSolid = model.Coshh_isHazardSolid;
                        items.Coshh_isHazardOther = model.Coshh_isHazardOther;
                        items.Coshh_HazardOtherText = model.Coshh_HazardOtherText;
                        items.Coshh_isExposeInhalation = model.Coshh_isExposeInhalation;
                        items.Coshh_isExposeSkin = model.Coshh_isExposeSkin;
                        items.Coshh_isExposeEyes = model.Coshh_isExposeEyes;
                        items.Coshh_isExposeIngestion = model.Coshh_isExposeIngestion;
                        items.Coshh_isExposeOther = model.Coshh_isExposeOther;
                        items.Coshh_ExposeOtherText = model.Coshh_ExposeOtherText;
                        items.Coshh_isDisposalHazard = model.Coshh_isDisposalHazard;
                        items.Coshh_isDisposalSkip = model.Coshh_isDisposalSkip;
                        items.Coshh_isDisposalMgtArea = model.Coshh_isDisposalMgtArea;
                        items.Coshh_isDisposalSupplier = model.Coshh_isDisposalSupplier;
                        items.Coshh_isDisposalOther = model.Coshh_isDisposalOther;
                        items.Coshh_DisposalOtherText = model.Coshh_DisposalOtherText;
                        //end region

                        //if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
                        //items.IsActive = model.IsActive;

                        if (isnew) {
                            //items.IsDeleted = false;
                            dataMgr.ProcessMasterExts.Add(items);
                        }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.ExtendId;
                        if (isnew)
                        { logMsg = "COSHH Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }
                        else
                        { logMsg = "COSHH Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }

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
                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
                base.SetOperationCompleted("info", "Operation", logMsg);
            }

            return RedirectToAction("DefineNew", "Coshh", new { rid = tempId });
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
                        var items = dataMgr.COSHHs.Where(x => x.Id == rid).SingleOrDefault();

                        items.IsDeleted = true;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "COSHH Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "COSHH Deleted-ERR";
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
            return RedirectToAction("CoshhList", "Coshh");
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
                        var items = dataMgr.COSHHs.Where(x => x.Id == rid).SingleOrDefault();
                        items.IsActive = val;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        logMsg = "COSHH" + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "COSHH" + msg + "-ERR";
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
            return RedirectToAction("CoshhList", "Coshh");
        }

        
        public ViewResult CoshhList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetCoshh_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.CoshhModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.CoshhModel();
                l_item.Id = item.Id;
                l_item.Location = item.Location;
                l_item.Area = item.Area;
                l_item.DateSubmit = item.DateSubmit;
                l_item.PermitNo = item.PermitNo;
                l_item.Description = item.Description;
                l_item.Employee_risk = item.Employee_risk;
                l_item.Contractors_risk = item.Contractors_risk;
                l_item.Public_risk = item.Public_risk;
                l_item.Substance = item.Substance;
                l_item.WEL = item.WEL;
                l_item.Hazards = item.Hazards;
                l_item.ControlMeasures = item.ControlMeasures;
                l_item.Monitor_required = item.Monitor_required;
                l_item.FirstAidMeasure = item.FirstAidMeasure;
                l_item.Storage = item.Storage;
                l_item.IsControlled = item.IsControlled;
                l_item.RiskRating = item.RiskRating;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;
                ls_items.Add(l_item);
            }

            return View(ls_items);
        }
        public PartialViewResult _GetList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetCoshh_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.CoshhModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.CoshhModel();
                l_item.Id = item.Id;
                l_item.Location = item.Location;
                l_item.Area = item.Area;
                l_item.DateSubmit = item.DateSubmit;
                l_item.PermitNo = item.PermitNo;
                l_item.Description = item.Description;
                l_item.Employee_risk = item.Employee_risk;
                l_item.Contractors_risk = item.Contractors_risk;
                l_item.Public_risk = item.Public_risk;
                l_item.Substance = item.Substance;
                l_item.WEL = item.WEL;
                l_item.Hazards = item.Hazards;
                l_item.ControlMeasures = item.ControlMeasures;
                l_item.Monitor_required = item.Monitor_required;
                l_item.FirstAidMeasure = item.FirstAidMeasure;
                l_item.Storage = item.Storage;
                l_item.IsControlled = item.IsControlled;
                l_item.RiskRating = item.RiskRating;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;
                ls_items.Add(l_item);
            }

            return PartialView(ls_items);

        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToRiskRatingListItems(IEnumerable<DAL.OptionsLockup> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Value).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.Text, Value = obj.Id.ToString()});
        }

   

    }
}
