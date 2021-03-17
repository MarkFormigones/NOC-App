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
    public class ChemicalTController : BaseController
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
            var pInfo = new DAL.ChemicalT();
            int adminId = -1;
            if (rId == null || rId == -1)
            {
 
            }
            else
            {
                pInfo = dataMgr.GetChemicalTInfoById(rId);
            }


            var pInfoM = new ChemicalTModel(pInfo);
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
            if (rId == null || rId == -1)
            {

            }
            else
            {
                pInfo = dataMgr.GetProcessMasterExtInfoById(rId);
            }


            var pInfoM = new ProcessMasterExtModel(pInfo);
            /*if (string.IsNullOrEmpty(pInfoM.DepartmentLogo)) { pInfoM.UseDefaultLogo = true; }*///logo initialization

            //pInfoM.BUnitId = int.Parse(rId.ToString());
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }


        [HttpPost]
        public ActionResult Define(ChemicalTModel model)
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
                        var items = new DAL.ChemicalT();
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        if (model.Id > 0)
                        {
                            isnew = false;
                            items = dataMgr.ChemicalTs.Where(x => x.Id == model.Id).SingleOrDefault();
                        }

                        items.SubstanceName = model.SubstanceName;
                        items.Supplier = model.Supplier;
                        items.Description = model.Description;
                        items.DischargeToEnvironment = model.DischargeToEnvironment;
                        items.AreaUse = model.AreaUse;
                        items.Quantity = model.Quantity;
                        items.MaterialUsed = model.MaterialUsed;
                        items.Storage = model.Storage;
                        items.DisposalContainers = model.DisposalContainers;
                        items.DisposalChemical = model.DisposalChemical;
                        items.ChemicalUse = model.ChemicalUse;
                        items.DateReciept = model.DateReciept;
                        items.StorageLocation = model.StorageLocation;
                        items.RegulatedbyLaw = model.RegulatedbyLaw;
                        items.COSHH_Completed = model.COSHH_Completed;
                        items.HSE_Impact = model.HSE_Impact;
                        items.NewChemical = model.NewChemical;
                        items.StockItem = model.StockItem;
                        items.StockOrderQty = model.StockOrderQty;
                        items.MaxInventory = model.MaxInventory;
                        items.UsageRate = model.UsageRate;
                        items.NonStockItem = model.NonStockItem;
                        items.NonStockOrderQty = model.NonStockOrderQty;
                        items.ChemicalReplacement = model.ChemicalReplacement;
                        items.RepliacementDetails = model.RepliacementDetails;
                        items.TempChemical = model.TempChemical;
                        items.TempOrderQty = model.TempOrderQty;
                        items.AdditionalInfo = model.AdditionalInfo;

                        if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
                        items.IsActive = model.IsActive;
                        
                        if (isnew) { items.IsDeleted = false; dataMgr.ChemicalTs.Add(items); }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.Id;
                        if (isnew)
                        { logMsg = "ChemicalT Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }
                        else
                        { logMsg = "ChemicalT Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }

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

            return RedirectToAction("define", "ChemicalT", new { rid = tempId });
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

                       
                        items.Chem_isDischarged = model.Chem_isDischarged;
                        items.Chem_EquipArea = model.Chem_EquipArea;
                        items.Chem_Qty = model.Chem_Qty;
                        items.Chem_FormMaterial = model.Chem_FormMaterial;
                        items.Chem_TypeStorage = model.Chem_TypeStorage;
                        items.Chem_DisposeContainer = model.Chem_DisposeContainer;
                        items.Chem_DisposeChemical = model.Chem_DisposeChemical;
                        items.Chem_Use = model.Chem_Use;
                        items.Chem_DateReceipt = model.Chem_DateReceipt;
                        items.Storage = model.Storage;
                        items.Chem_isRegulate = model.Chem_isRegulate;
                        items.Coshh_isCompleted = model.Coshh_isCompleted;
                        items.Chem_Impact = model.Chem_Impact;
                        items.Chem_isNew = model.Chem_isNew;
                        items.Chem_isStock = model.Chem_isStock;
                        items.Chem_StockOrder = model.Chem_StockOrder;
                        items.Chem_StockMaxInven = model.Chem_StockMaxInven;
                        items.Chem_StockUsage = model.Chem_StockUsage;
                        items.Chem_isNonStock = model.Chem_isNonStock;
                        items.Chem_NonStockOrder = model.Chem_NonStockOrder;
                        items.Chem_isReplacement = model.Chem_isReplacement;
                        items.Chem_ReplaceDetails = model.Chem_ReplaceDetails;
                        items.Chem_isTemp = model.Chem_isTemp;
                        items.Chem_TempOrder = model.Chem_TempOrder;
                        items.Chem_ExtraInfo = model.Chem_ExtraInfo;
                        items.ProcessMasterId = model.ProcessMasterId;

                        //if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
                        //items.IsActive = model.IsActive;

                        if (isnew) {
                            //items.IsDeleted = false;
                            dataMgr.ProcessMasterExts.Add(items); }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.ExtendId;
                        if (isnew)
                        { logMsg = "ChemicalT Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }
                        else
                        { logMsg = "ChemicalT Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1); }

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

            return RedirectToAction("define", "ChemicalT", new { rid = tempId });
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
                        var items = dataMgr.ChemicalTs.Where(x => x.Id == rid).SingleOrDefault();

                        items.IsDeleted = true;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "ChemicalT Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "ChemicalT Deleted-ERR";
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
            return RedirectToAction("ChemicalTList", "ChemicalT");
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
                        var items = dataMgr.ChemicalTs.Where(x => x.Id == rid).SingleOrDefault();
                        items.IsActive = val;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        logMsg = "ChemicalT" + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "ChemicalT" + msg + "-ERR";
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
            return RedirectToAction("ChemicalTList", "ChemicalT");
        }

        
        public ViewResult ChemicalTList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetChemicalT_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.ChemicalTModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.ChemicalTModel();
                l_item.Id = item.Id;
                l_item.SubstanceName = item.SubstanceName;
                l_item.Supplier = item.Supplier;
                l_item.Description = item.Description;
                l_item.DischargeToEnvironment = item.DischargeToEnvironment;
                l_item.AreaUse = item.AreaUse;
                l_item.Quantity = item.Quantity;
                l_item.MaterialUsed = item.MaterialUsed;
                l_item.Storage = item.Storage;
                l_item.DisposalContainers = item.DisposalContainers;
                l_item.DisposalChemical = item.DisposalChemical;
                l_item.ChemicalUse = item.ChemicalUse;
                l_item.DateReciept = item.DateReciept;
                l_item.StorageLocation = item.StorageLocation;
                l_item.RegulatedbyLaw = item.RegulatedbyLaw;
                l_item.COSHH_Completed = item.COSHH_Completed;
                l_item.HSE_Impact = item.HSE_Impact;
                l_item.NewChemical = item.NewChemical;
                l_item.StockItem = item.StockItem;
                l_item.StockOrderQty = item.StockOrderQty;
                l_item.MaxInventory = item.MaxInventory;
                l_item.UsageRate = item.UsageRate;
                l_item.NonStockItem = item.NonStockItem;
                l_item.NonStockOrderQty = item.NonStockOrderQty;
                l_item.ChemicalReplacement = item.ChemicalReplacement;
                l_item.RepliacementDetails = item.RepliacementDetails;
                l_item.TempChemical = item.TempChemical;
                l_item.TempOrderQty = item.TempOrderQty;
                l_item.AdditionalInfo = item.AdditionalInfo;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;
                ls_items.Add(l_item);
            }

            return View(ls_items);
        }
        public PartialViewResult _GetList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetChemicalT_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.ChemicalTModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.ChemicalTModel();
                l_item.Id = item.Id;
                l_item.SubstanceName = item.SubstanceName;
                l_item.Supplier = item.Supplier;
                l_item.Description = item.Description;
                l_item.DischargeToEnvironment = item.DischargeToEnvironment;
                l_item.AreaUse = item.AreaUse;
                l_item.Quantity = item.Quantity;
                l_item.MaterialUsed = item.MaterialUsed;
                l_item.Storage = item.Storage;
                l_item.DisposalContainers = item.DisposalContainers;
                l_item.DisposalChemical = item.DisposalChemical;
                l_item.ChemicalUse = item.ChemicalUse;
                l_item.DateReciept = item.DateReciept;
                l_item.StorageLocation = item.StorageLocation;
                l_item.RegulatedbyLaw = item.RegulatedbyLaw;
                l_item.COSHH_Completed = item.COSHH_Completed;
                l_item.HSE_Impact = item.HSE_Impact;
                l_item.NewChemical = item.NewChemical;
                l_item.StockItem = item.StockItem;
                l_item.StockOrderQty = item.StockOrderQty;
                l_item.MaxInventory = item.MaxInventory;
                l_item.UsageRate = item.UsageRate;
                l_item.NonStockItem = item.NonStockItem;
                l_item.NonStockOrderQty = item.NonStockOrderQty;
                l_item.ChemicalReplacement = item.ChemicalReplacement;
                l_item.RepliacementDetails = item.RepliacementDetails;
                l_item.TempChemical = item.TempChemical;
                l_item.TempOrderQty = item.TempOrderQty;
                l_item.AdditionalInfo = item.AdditionalInfo;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;
                ls_items.Add(l_item);
            }

            return PartialView(ls_items);

        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToCountryListItems(IEnumerable<DAL.CountryList> lList, string selectedId)
        {
            return lList.OrderBy(obj => obj.CountryId).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.CountryVal == selectedId.Trim().ToString()), Text = obj.CountryText, Value = obj.CountryVal.ToString() });
        }

    }
}
