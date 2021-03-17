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
    public class ReportController : BaseController
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

      

        public ActionResult _ProcessListReport()
        {

            ReportModel reportModel = new ReportModel();
            reportModel.ReportName = "Process Details Report";
            reportModel.ReportDate = DateTime.Now;
            reportModel.ReportProcessName = "MOC";
            reportModel.ReportCompanyName = "ELEMEGA";
            reportModel.ReportWorkspaceName = "WORKSPACE NAme";
            reportModel.PrinteUnitSerial = "123456";
            reportModel.PrinterUser = "MARK";
            reportModel.PrinteProcessSerial = "12300000";

            return View(reportModel);
        }

        //public ActionResult PrintAll()
        //{
        // //   var q = new  Rot("_ProcessListReport");

        //  //  return q;
        //}


        public PartialViewResult proPrint(int? rId, string guid, string vw, int? pId)
        {


            ReportModel reportModel = new ReportModel();
            reportModel.ReportName = "Process Details Report";
            reportModel.ReportDate = DateTime.Now;
            reportModel.PrinterUser = "MARK";
            //reportModel.ReportProcessName = "MOC";
            //reportModel.ReportCompanyName = "ELEMEGA";
            //reportModel.ReportWorkspaceName = "WORKSPACE NAme";
            //reportModel.PrinteUnitSerial = "123456";
            //reportModel.PrinterUser = "MARK";
            //reportModel.PrinteProcessSerial = "12300000";



            // return View(reportModel);
            DAL.DataManager dataMgr = new DAL.DataManager();
            var pInfo = new DAL.Vw_ProcessMaster();
            int adminId = -1;
            if (rId == null || rId == -1)
            {
              
                if (guid.Length > 10)
                {
                    pInfo = dataMgr.GetVW_ProcessMasterInfoByGUId(guid);
                }


            }
            else
            {
                pInfo = dataMgr.GetVW_ProcessMasterInfoById(rId);         
            }

            if (pInfo == null)
            {
                throw new Exception("Invalid request from user:" + GlobalUserName);

            }

            var pInfoM = new vw_ProcessMasterModel(pInfo);// GlobalUserId GlobalDelegatorsList

            //var pInfoM = new DepartmentModel(pInfo);
            //if (string.IsNullOrEmpty(pInfoM.DepartmentLogo)) { pInfoM.UseDefaultLogo = true; }//logo initialization
            

            reportModel.ProcessMaster = pInfoM;

            //pInfoM.BUnitId = int.Parse(rId.ToString());
            pInfoM.ReadEdit = vw;
            return PartialView("_ProcessMasterReport", reportModel);
          //  return View(reportModel);
        }

        
        [HttpPost]
        public ActionResult Define(DepartmentModel model, HttpPostedFileBase file)
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
                        var items = new DAL.Department();
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        if (model.DepartmentId > 0)
                        {
                            isnew = false;
                            items = dataMgr.Departments.Where(x => x.DepartmentId == model.DepartmentId).SingleOrDefault();
                        }
                        items.BUnitId = 1;// model.BUnitId;
                        items.DepartmentName = model.DepartmentName;
                        items.DepartmentDesc = model.DepartmentDesc;
                        items.CompanyId = 1;// model.CompanyId;
                        items.DepartmentMembers = model.DepartmentMembers;

                        items.Dated = model.Dated;
                        if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
                        items.IsActive = model.IsActive;

                        if (model.UseDefaultLogo)
                        { 
                            items.DepartmentLogo = null; 
                        }
                        else if (file != null)
                        {
                            items.DepartmentLogo = UploadLogo(file);
                        }
                        if (isnew) { items.IsDeleted = false; dataMgr.Departments.Add(items); }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.DepartmentId;
                        if (isnew)
                        { logMsg = "Department Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }
                        else
                        { logMsg = "Department Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }

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

            return RedirectToAction("define", "Department", new { rid = tempId });
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
                        var items = dataMgr.Departments.Where(x => x.DepartmentId == rid).SingleOrDefault();

                        items.IsDeleted = true;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "Department Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "Department Deleted-ERR";
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
            return RedirectToAction("DepartmentsList", "Department");
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
                        var items = dataMgr.Departments.Where(x => x.DepartmentId == rid).SingleOrDefault();
                        items.IsActive = val;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        logMsg = "Department" + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "Department" + msg + "-ERR";
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
            return RedirectToAction("DepartmentsList", "Department");
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

     
        public ViewResult DepartmentsList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetDepartment_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.DepartmentModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.DepartmentModel();
                l_item.DepartmentId = item.DepartmentId;
                l_item.DepartmentName = item.DepartmentName;
                l_item.DepartmentDesc = item.DepartmentDesc;
                l_item.DepartmentLogo = item.DepartmentLogo;
                l_item.DepartmentMembers = item.DepartmentMembers;

                l_item.BUnitId = item.BUnitId;
                l_item.CompanyId = item.CompanyId;

                l_item.Dated = item.Dated;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;
                ls_items.Add(l_item);
            }

            return View(ls_items);
        }
        public PartialViewResult _GetList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetDepartment_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.DepartmentModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.DepartmentModel();
                l_item.DepartmentId = item.DepartmentId;
                l_item.DepartmentName = item.DepartmentName;
                l_item.DepartmentDesc = item.DepartmentDesc;
                l_item.DepartmentLogo = item.DepartmentLogo;
                l_item.DepartmentMembers = item.DepartmentMembers;

                l_item.BUnitId = item.BUnitId;
                l_item.CompanyId = item.CompanyId;

                l_item.Dated = item.Dated;
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
