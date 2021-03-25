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
    public class RequestTransactionController : BaseController
    {
        //
        // GET: /Customer/

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
            var pInfo = new DAL.RequestTransaction();
           
            


            int adminId = -1;
            if (rId == null || rId == -1)
            {
               
            }
            else
            {
                pInfo = dataMgr.GetRequestTransactionInfoById(rId);
            }




            var pInfoM = new RequestTransactionModel(pInfo);
            pInfoM = populateDropDown(dataMgr, pInfoM);

                       
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }

        public RequestTransactionModel populateDropDown(DAL.DataManager dataMgr, RequestTransactionModel pInfoM)
        {
            var productList = dataMgr.GetProduct_List();
            var requestTypeList = dataMgr.GetRequestType_List();
            var partyList = dataMgr.GetParty_List();
            var acctCategoryList = dataMgr.GetAcctCategory_List();
            var esSegmentList = dataMgr.GetESSegment_List();
            var ratePlanList = dataMgr.GetRatePlan_List();
            var contractList = dataMgr.GetContract_List();
            var approvalRequestStatusList = dataMgr.GetApprovalRequestStatus_List();

            pInfoM.ProductNameList = ToProductListItems(productList, -1);
            pInfoM.ProductName = productList.Where(x => x.Id == pInfoM.ProductCodeId).Select(x => x.ProductCode).FirstOrDefault();

            pInfoM.RequestTypeList = ToRequestTypeListItems(requestTypeList, -1);
            pInfoM.RequestTypeName = requestTypeList.Where(x => x.Id == pInfoM.RequestTypeId).Select(x => x.SubRequestType).FirstOrDefault();

            pInfoM.PartyNameList = ToPartyListItems(partyList, -1);
            pInfoM.PartyName = partyList.Where(x => x.Id == pInfoM.PartyId ).Select(x => x.PartyName).FirstOrDefault();

            pInfoM.AcctCategoryList = ToAcctCategoryListItems(acctCategoryList, -1);
            pInfoM.AcctCategoryName = acctCategoryList.Where(x => x.Id == pInfoM.AcctCategoryId).Select(x => x.AccountCategory).FirstOrDefault();

            pInfoM.ESSegmentList = ToESSegmentListItems(esSegmentList, -1);
            pInfoM.ESSegmentName = esSegmentList.Where(x => x.Id == pInfoM.ESSegmentId).Select(x => x.ESSubSegment).FirstOrDefault();

            pInfoM.RatePlanList = ToRatePlanListItems(ratePlanList, -1);
            pInfoM.RatePlanName = ratePlanList.Where(x => x.Id == pInfoM.RatePlanId).Select(x => x.RatePlan).FirstOrDefault();

            pInfoM.ContractList = ToContractListItems(contractList, -1);
            pInfoM.ContractName = contractList.Where(x => x.Id == pInfoM.ContractId).Select(x => x.ContractPeriod).FirstOrDefault();

            pInfoM.ApprovalRequestStatusList = ToApprovalRequestStatusListItems(approvalRequestStatusList, -1);
            pInfoM.ApprovalRequestStatusName = approvalRequestStatusList.Where(x => x.Id == pInfoM.ApprovalRequestStatusId).Select(x => x.ApprovalRequestStatusName).FirstOrDefault();

            return pInfoM;
        }

        
        [HttpPost]
        public ActionResult Define(RequestTransactionModel model, HttpPostedFileBase file)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();


            int tempId = 0; string logMsg = "Division";
            
           
            if (ModelState.IsValid)
            {

                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {

                    try
                    {
                        bool isnew = true;
                        var item = new DAL.RequestTransaction();
                       
                        if (model.Id > 0)
                        {
                            isnew = false;
                            item = dataMgr.RequestTransactions.Where(x => x.Id == model.Id).SingleOrDefault();
                                                                              
                        }                      

                        item.ProductCodeId = model.ProductCodeId;
                        item.RequestNumber = model.RequestNumber;
                        item.SubRequestNumber = model.SubRequestNumber;
                        item.RequestTypeId = model.RequestTypeId;
                        item.PartyId = model.PartyId;
                        item.AccountNumber = model.AccountNumber;
                        item.AcctCategoryId = model.AcctCategoryId;
                        item.ESSegmentId = model.ESSegmentId;
                        item.RatePlanId = model.RatePlanId;
                        item.ContractId = model.ContractId;
                        item.ApprovalRequestStatusId = model.ApprovalRequestStatusId;
                        item.VariableAmount_MRC = model.VariableAmount_MRC;
                        item.VariableAmount_OTC = model.VariableAmount_OTC;
                        item.VariableAmount_QRC = model.VariableAmount_QRC;
                        item.VariableAmount_YRC = model.VariableAmount_YRC;
                        item.ChargeDetails = model.ChargeDetails;
                                                             
                        if (model.FromActionType == -1) { item.IsDeleted = true; }
                       


                        //if (model.UseDefaultLogo)
                        //{
                        //    item.CustomerLogo = null;
                        //}
                        //else if (file != null)
                        //{
                        //    item.CustomerLogo = UploadLogo(file);
                        //}

                        if (isnew) {
                            item.Created = DateTime.Now;
                            item.IsDeleted = false; 
                            dataMgr.RequestTransactions.Add(item); 
                        }//New Object
                        
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = item.Id;
                        if (isnew)
                        { logMsg = "Request Transaction Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }
                        else
                        { logMsg = "Request Transaction  Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }

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

            return RedirectToAction("define", "RequestTransaction", new { rid = tempId });
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
                        var items = dataMgr.RequestTransactions.Where(x => x.Id == rid).SingleOrDefault();

                        items.IsDeleted = true;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "Request Transaction  Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "Request Transaction  Deleted-ERR";
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
            return RedirectToAction("RequestTransactionList", "RequestTransaction");
        }
        public ActionResult Deactivate(int rid, Boolean val)
        {
            string msg = "Deactivated";
            if (val == true) { 
                msg = "Activated"; 

                
            }

            string logMsg = "";

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var items = dataMgr.RequestTransactions.Where(x => x.Id == rid).SingleOrDefault();
                        items.IsActive = val;  //Delete the Object                     

                                        
                        dataMgr.SaveChanges();
                        logMsg = "Request Transaction " + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "Request Transaction " + msg + "-ERR";
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
            return RedirectToAction("RequestTransactionList", "RequestTransaction ");
        }

        public string UploadLogo(HttpPostedFileBase file)
        {
            string errorMsg = "";
            string fileEx = "JPG,JPEG,PNG,TIF,TIFF,GIF,BMP,ICO";
            string fileNamex = "";
            if (!fileEx.Contains(file.ContentType.Split('/')[1].ToUpper()))
            {
                ViewBag.status = "Error";
                errorMsg = "Invalid Image File Type";
                // base.SetOperationCompleted("warning", "UploadLogo", errorMsg);
                base.SetInnerOperationCompleted("warning", "UploadLogo", errorMsg);

                ModelState.AddModelError("error", errorMsg); throw new IOException(errorMsg);
            }
            //var ufile = Request.Files[0];
            if (file.ContentLength > 3200000)
            {
                errorMsg = "File is too large >3MB";
                // base.SetOperationCompleted("warning", "UploadLogo", errorMsg);
                base.SetInnerOperationCompleted("warning", "UploadLogo", errorMsg);

                ModelState.AddModelError("error", errorMsg); throw new IOException(errorMsg);
            }

            var path = "";

            string newguid = System.Guid.NewGuid().ToString();
            var fileName = newguid;



            if (file != null && file.ContentLength > 0)
            {
                Dictionary<string, string> versions = new Dictionary<string, string>();

                //Define the versions to generate

                versions.Add("_thumb", "width=32&height=32&crop=auto&format=jpg"); //Crop to square thumbnail
                versions.Add("_small", "width=80&height=80&crop=auto&format=jpg");
                versions.Add("_medium", "width=320&height=320&crop=auto&format=jpg"); //Fit inside 400x400 area, jpeg
                versions.Add("_large", "maxwidth=960&maxheight=640&format=jpg"); //Fit inside 1900x1200 area
                versions.Add("_base", "maxwidth=480&maxheight=360&format=jpg"); //Fit inside 1900x1200 area



                foreach (string fileKey in Request.Files.Keys)
                {

                    HttpPostedFileBase xfile = Request.Files[fileKey];

                    if (xfile.ContentLength <= 0) continue; //Skip unused file controls.

                    //Get the physical path for the uploads folder and make sure it exists

                    //string uploadFolder = Server.MapPath("~/uploads");
                    string appfolder = WebConfigurationManager.AppSettings["ImageDirectory"].ToString();
                    string uploadFolder = Server.MapPath(appfolder);
                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);
                    //Generate each version
                    foreach (string suffix in versions.Keys)
                    {

                        //Generate a filename (GUIDs are best).
                        fileNamex = Path.Combine(uploadFolder, newguid + suffix);
                        //Let the image builder add the correct extension based on the output file type

                        fileNamex = ImageBuilder.Current.Build(file, fileNamex, new ResizeSettings(versions[suffix]), false, true);

                    }
                    path = appfolder + newguid;


                }


            }
            return path;



        }
        [HttpGet]

     
        public ViewResult RequestTransactionList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetVwRequestDetails_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.VwRequestDetailsModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.VwRequestDetailsModel();
                l_item.Id = item.Id;
                l_item.ProductCode = item.ProductCode;
                l_item.RequestNumber = item.RequestNumber;
                l_item.SubRequestNumber = item.SubRequestNumber;
                l_item.SubRequestType = item.SubRequestType;
                l_item.PartyName = item.PartyName;
                l_item.AccountNumber = item.AccountNumber;
                l_item.AccountCategory = item.AccountCategory;
                l_item.ESSubSegment = item.ESSubSegment;
                l_item.RatePlan = item.RatePlan;
                l_item.RatePlanDescription = item.RatePlanDescription;
                l_item.VariableAmount_MRC = item.VariableAmount_MRC;
                l_item.VariableAmount_OTC = item.VariableAmount_OTC;
                l_item.VariableAmount_QRC = item.VariableAmount_QRC;
                l_item.VariableAmount_YRC = item.VariableAmount_YRC;
                l_item.ContractPeriod = item.ContractPeriod;
                l_item.ChargeDetails = item.ChargeDetails;
                l_item.ApprovalRequestStatusName = item.ApprovalRequestStatusName;
                l_item.CBCMStatus = item.CBCMStatus;
                l_item.Created = item.Created;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;
                ls_items.Add(l_item);
            }

            return View(ls_items);
        }
        public PartialViewResult _GetList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetVwRequestDetails_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.VwRequestDetailsModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.VwRequestDetailsModel();
                l_item.Id = item.Id;
                l_item.ProductCode = item.ProductCode;
                l_item.RequestNumber = item.RequestNumber;
                l_item.SubRequestNumber = item.SubRequestNumber;
                l_item.SubRequestType = item.SubRequestType;
                l_item.PartyName = item.PartyName;
                l_item.AccountNumber = item.AccountNumber;
                l_item.AccountCategory = item.AccountCategory;
                l_item.ESSubSegment = item.ESSubSegment;
                l_item.RatePlan = item.RatePlan;
                l_item.RatePlanDescription = item.RatePlanDescription;
                l_item.VariableAmount_MRC = item.VariableAmount_MRC;
                l_item.VariableAmount_OTC = item.VariableAmount_OTC;
                l_item.VariableAmount_QRC = item.VariableAmount_QRC;
                l_item.VariableAmount_YRC = item.VariableAmount_YRC;
                l_item.ContractPeriod = item.ContractPeriod;
                l_item.ChargeDetails = item.ChargeDetails;
                l_item.ApprovalRequestStatusName = item.ApprovalRequestStatusName;
                l_item.CBCMStatus = item.CBCMStatus;
                l_item.Created = item.Created;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;
                ls_items.Add(l_item);
            }

            return PartialView(ls_items);

        }


        public static IEnumerable<System.Web.Mvc.SelectListItem> ToProductListItems(IEnumerable<DAL.Product> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.ProductCode, Value = obj.Id.ToString() });
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToRequestTypeListItems(IEnumerable<DAL.RequestType> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.SubRequestType, Value = obj.Id.ToString() });
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToPartyListItems(IEnumerable<DAL.Party> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.PartyName, Value = obj.Id.ToString() });
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToAcctCategoryListItems(IEnumerable<DAL.AcctCategory> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.AccountCategory, Value = obj.Id.ToString() });
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToESSegmentListItems(IEnumerable<DAL.ESSegment> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.ESSubSegment, Value = obj.Id.ToString() });
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToRatePlanListItems(IEnumerable<DAL.RPlan> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.RatePlan, Value = obj.Id.ToString() });
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToContractListItems(IEnumerable<DAL.Contract> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.ContractPeriod, Value = obj.Id.ToString() });
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToApprovalRequestStatusListItems(IEnumerable<DAL.ApprovalRequestStatu> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.ApprovalRequestStatusName, Value = obj.Id.ToString() });
        }


    }
}
