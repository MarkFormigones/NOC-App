//using ImageResizer;
//using Hydron.Controllers;
//using Hydron.Models.Definitions;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Transactions;
//using System.Web;
//using System.Web.Configuration;
//using System.Web.Mvc;
//using System.Web.Security;

//namespace Hydron.Views
//{
//    public class AppLockupController : BaseController
//    {
//        //
//        // GET: /AppLockup/

//        public PartialViewResult _subTree(int parentId)
//        {
//            DAL.DataManager dataMgr = new DAL.DataManager();
//            var items = dataMgr.GetBDivisions_List(parentId);


//            var ls_items = new List<Models.Definitions.TreeModel>();
//            foreach (var item in items)
//            {
//                var treeModel = new Models.Definitions.TreeModel();
//                treeModel.name = item.BUnitName;
//                treeModel.Id = item.BUnitId;
//                treeModel.controller = "BDivisions";
//                treeModel.type = "folder";
//                treeModel.additionalParameters = "{ id: 'F11' }";
//                ls_items.Add(treeModel);
//            }

//            return PartialView(ls_items);

//        }


//        public PartialViewResult Portfolio()
//        {
//            return PartialView();
//        }
//        public ViewResult Define(int? rId, string vw)
//        {

//            DAL.DataManager dataMgr = new DAL.DataManager();
//            var pInfo = new DAL.AppLockup();
//            int adminId = -1;
//            if (rId == null || rId == -1)
//            {
//                //pInfo = dataMgr.GetCompanyInfoById(uId);
//                //SetUserParam(User.Identity.Name);// this will apply changes on session, avatar and email info
//            }
//            else
//            {
//                pInfo = dataMgr.GetAppLockupInfoById(rId);
//            }


//            var pInfoM = new AppLockupModel(pInfo);
//            //if (string.IsNullOrEmpty(pInfoM.DepartmentLogo)) { pInfoM.UseDefaultLogo = true; }//logo initialization
           
//            //pInfoM.BUnitId = int.Parse(rId.ToString());
//            pInfoM.ReadEdit = vw;
//            return View(pInfoM);
//        }

        
//        [HttpPost]
//        public ActionResult Define(AppLockupModel model)
//        {
//            int tempId = 0; string logMsg = "Division";
//            if (ModelState.IsValid)
//            {

//                // Attempt to register the user
//                using (TransactionScope transaction = new TransactionScope())
//                {

//                    try
//                    {
//                        bool isnew = true;
//                        var items = new DAL.AppLockup();
//                        DAL.DataManager dataMgr = new DAL.DataManager();
//                        if (model.Id > 0)
//                        {
//                            isnew = false;
//                            items = dataMgr.AppLockups.Where(x => x.Id == model.Id).SingleOrDefault();
//                        }

//                        items.Name = model.Name;
//                        items.CategoryId = model.CategoryId;
//                        items.Dated = model.Dated;
//                        items.Desc = model.Desc;                       
//                        items.Icon = model.Icon;
//                        items.IconClass = model.IconClass;
//                        items.GeneralClass = model.GeneralClass;

//                        if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
//                        items.IsActive = model.IsActive;

//                        if (isnew) { items.IsDeleted = false; dataMgr.AppLockups.Add(items); }//New Object
//                        dataMgr.SaveChanges();

//                        ViewBag.status = "Success";
//                        tempId = items.Id;
//                        if (isnew)
//                        { logMsg = "AppLockup Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }
//                        else
//                        { logMsg = "AppLockup Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }

//                        transaction.Complete();

//                        base.SetOperationCompleted("success", "Saved", "successfully");

//                    }

//                    catch (Exception e)
//                    {
//                        transaction.Dispose();
//                        base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
//                        base.SetOperationCompleted("error", "Operation", "failed");
//                        base.SetInnerOperationCompleted("error", "Operation", e.Message);
//                        ModelState.AddModelError("error", e.Message);
//                    }
//                }
//            }
//            else
//            {
//                logMsg = "invalid model state";
//                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1);
//                base.SetOperationCompleted("info", "Operation", logMsg);
//            }

//            return RedirectToAction("define", "AppLockup", new { rid = tempId });
//        }
//        public ActionResult Delete(int rid)
//        {
//            string logMsg = "";
//            if (ModelState.IsValid)
//            {
//                // Attempt to register the user
//                using (TransactionScope transaction = new TransactionScope())
//                {
//                    try
//                    {

//                        DAL.DataManager dataMgr = new DAL.DataManager();
//                        var items = dataMgr.AppLockups.Where(x => x.Id == rid).SingleOrDefault();

//                        items.IsDeleted = true;  //Delete the Object   
//                        items.IsActive = false;                  
//                        dataMgr.SaveChanges();
//                        ViewBag.status = "Deleted";
//                        logMsg = "AppLockup Deleted-OK";
//                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

//                        transaction.Complete();
//                        base.SetOperationCompleted("success", "Deleted", "successfully");
//                    }

//                    catch (Exception e)
//                    {
//                        ViewBag.status = "Error";
//                        logMsg = "AppLockup Deleted-ERR";
//                        ModelState.AddModelError("error", e.Message);
//                        base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
//                        base.SetOperationCompleted("error", "Operation", "failed");
//                    }
//                }
//            }
//            else
//            {
//                logMsg = "invalid model state";
//                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
//                base.SetOperationCompleted("info", "Operation", logMsg);
//            }
//            return RedirectToAction("AppLockupList", "AppLockup");
//        }

//        public ActionResult Deactivate(int rid, Boolean val)
//        {
//            string msg = "Deactivated";
//            if (val == true) { msg = "Activated"; }

//            string logMsg = "";

//            if (ModelState.IsValid)
//            {
//                // Attempt to register the user
//                using (TransactionScope transaction = new TransactionScope())
//                {
//                    try
//                    {
//                        DAL.DataManager dataMgr = new DAL.DataManager();
//                        var items = dataMgr.AppLockups.Where(x => x.Id == rid).SingleOrDefault();
//                        items.IsActive = val;  //Delete the Object                     
//                        dataMgr.SaveChanges();
//                        logMsg = "AppLockup" + msg + "-OK";
//                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
//                        base.SetOperationCompleted("success", msg, "successfully");
//                        transaction.Complete();
//                    }
//                    catch (Exception e)
//                    {
//                        ViewBag.status = "Error";
//                        logMsg = "AppLockup" + msg + "-ERR";
//                        ModelState.AddModelError("error", e.Message);
//                        base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
//                        base.SetOperationCompleted("error", msg, "failed");
//                    }
//                }
//            }
//            else
//            {
//                logMsg = "invalid model state";
//                base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
//                base.SetOperationCompleted("info", msg, "invalid model state");
//            }

//            //TempData["title"] = msg;
//            //TempData["statusMsg"] = "successfully";
//            return RedirectToAction("AppLockupList", "AppLockup");
//        }

//        public string UploadLogo(HttpPostedFileBase file)
//        {
//            string errorMsg = "";
//            string fileEx = "JPG,JPEG,PNG,TIF,TIFF,GIF,BMP,ICO";
//            string fileNamex = "";
//            if (!fileEx.Contains(file.ContentType.Split('/')[1].ToUpper()))
//            {
//                ViewBag.status = "Error";
//                errorMsg = "Invalid Image File Type";
//                // base.SetOperationCompleted("warning", "UploadLogo", errorMsg);
//                base.SetInnerOperationCompleted("warning", "UploadLogo", errorMsg);

//                ModelState.AddModelError("error", errorMsg); throw new IOException(errorMsg);
//            }
//            //var ufile = Request.Files[0];
//            if (file.ContentLength > 3200000)
//            {
//                errorMsg = "File is too large >3MB";
//                // base.SetOperationCompleted("warning", "UploadLogo", errorMsg);
//                base.SetInnerOperationCompleted("warning", "UploadLogo", errorMsg);

//                ModelState.AddModelError("error", errorMsg); throw new IOException(errorMsg);
//            }

//            var path = "";

//            string newguid = System.Guid.NewGuid().ToString();
//            var fileName = newguid;



//            if (file != null && file.ContentLength > 0)
//            {
//                Dictionary<string, string> versions = new Dictionary<string, string>();

//                //Define the versions to generate

//                versions.Add("_thumb", "width=32&height=32&crop=auto&format=jpg"); //Crop to square thumbnail
//                versions.Add("_small", "width=80&height=80&crop=auto&format=jpg");
//                versions.Add("_medium", "width=320&height=320&crop=auto&format=jpg"); //Fit inside 400x400 area, jpeg
//                versions.Add("_large", "maxwidth=960&maxheight=640&format=jpg"); //Fit inside 1900x1200 area
//                versions.Add("_base", "maxwidth=480&maxheight=360&format=jpg"); //Fit inside 1900x1200 area



//                foreach (string fileKey in Request.Files.Keys)
//                {

//                    HttpPostedFileBase xfile = Request.Files[fileKey];

//                    if (xfile.ContentLength <= 0) continue; //Skip unused file controls.

//                    //Get the physical path for the uploads folder and make sure it exists

//                    //string uploadFolder = Server.MapPath("~/uploads");
//                    string appfolder = WebConfigurationManager.AppSettings["ImageDirectory"].ToString();
//                    string uploadFolder = Server.MapPath(appfolder);
//                    if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);
//                    //Generate each version
//                    foreach (string suffix in versions.Keys)
//                    {

//                        //Generate a filename (GUIDs are best).
//                        fileNamex = Path.Combine(uploadFolder, newguid + suffix);
//                        //Let the image builder add the correct extension based on the output file type

//                        fileNamex = ImageBuilder.Current.Build(file, fileNamex, new ResizeSettings(versions[suffix]), false, true);

//                    }
//                    path = appfolder + newguid;


//                }


//            }
//            return path;



//        }
//        [HttpGet]

     
//        public ViewResult AppLockupList()
//        {
//            DAL.DataManager dataMgr = new DAL.DataManager();
//            var items = dataMgr.GetAppLockup_List();
//            var Roleitems = dataMgr.GetUser_Roles();
//            var ls_items = new List<Models.Definitions.AppLockupModel>();
//            foreach (var item in items)
//            {
//                var l_item = new Models.Definitions.AppLockupModel();
//                l_item.Id = item.Id;
//                l_item.Name = item.Name;
//                l_item.CategoryId = item.CategoryId;
//                l_item.IsDeleted = item.IsDeleted;
//                l_item.Dated = item.Dated;
//                l_item.Desc = item.Desc;
//                l_item.Icon = item.Icon;
//                l_item.IconClass = item.IconClass;
//                l_item.GeneralClass = item.GeneralClass;
//                l_item.IsActive = item.IsActive;
//                ls_items.Add(l_item);
//            }

//            return View(ls_items);
//        }
//        public PartialViewResult _GetList()
//        {
//            DAL.DataManager dataMgr = new DAL.DataManager();
//            var items = dataMgr.GetAppLockup_List();
//            var Roleitems = dataMgr.GetUser_Roles();
//            var ls_items = new List<Models.Definitions.AppLockupModel>();
//            foreach (var item in items)
//            {
//                var l_item = new Models.Definitions.AppLockupModel();
//                l_item.Id = item.Id;
//                l_item.Name = item.Name;
//                l_item.CategoryId = item.CategoryId;
//                l_item.IsDeleted = item.IsDeleted;
//                l_item.Dated = item.Dated;
//                l_item.Desc = item.Desc;
//                l_item.Icon = item.Icon;
//                l_item.IconClass = item.IconClass;
//                l_item.GeneralClass = item.GeneralClass;
//                l_item.IsActive = item.IsActive;
//                ls_items.Add(l_item);
//            }

//            return PartialView(ls_items);

//        }

//        //public static IEnumerable<System.Web.Mvc.SelectListItem> ToCountryListItems(IEnumerable<DAL.CountryList> lList, string selectedId)
//        //{
//        //    return lList.OrderBy(obj => obj.CountryId).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.CountryVal == selectedId.Trim().ToString()), Text = obj.CountryText, Value = obj.CountryVal.ToString() });
//        //}


//    }
//}
