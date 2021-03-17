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
    public class TourTController : BaseController
    {
        //
        // GET: /TourT/

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
            var pInfo = new DAL.TourT();
            int adminId = -1;
            if (rId == null || rId == -1)
            {
                //pInfo = dataMgr.GetCompanyInfoById(uId);
                //SetUserParam(User.Identity.Name);// this will apply changes on session, avatar and email info
            }
            else
            {
                pInfo = dataMgr.GetTourTInfoById(rId);
            }


            var pInfoM = new TourTModel(pInfo);
            if (string.IsNullOrEmpty(pInfoM.TargetIconUrl)) { pInfoM.UseDefaultLogo = true; pInfoM.UseDefaultLogoAr = true; }//logo initialization
           
            //pInfoM.BUnitId = int.Parse(rId.ToString());
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }

        
        [HttpPost]
        public ActionResult Define(TourTModel model, HttpPostedFileBase Icon, HttpPostedFileBase IconAr, HttpPostedFileBase Vid, HttpPostedFileBase Img)
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
                        var items = new DAL.TourT();
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        if (model.Id > 0)
                        {
                            isnew = false;
                            items = dataMgr.TourTs.Where(x => x.Id == model.Id).SingleOrDefault();
                        }
                        items.Controller = model.Controller;
                        items.Action = model.Action;
                        items.TargetOrder = model.TargetOrder;

                        items.TargetClass = model.TargetClass;
                        items.TargetSubject = model.TargetSubject;
                        items.TargetDesc = model.TargetDesc;
                        items.TargetUrl = model.TargetUrl;
                        items.placement = model.placement;

                        //Ar
                        items.TargetSubjectAr = model.TargetSubjectAr;
                        items.TargetDescAr = model.TargetDescAr;

                        //extra fields
                        items.ExtraText = model.ExtraText;
                        items.ExtraInt1 = model.ExtraInt1;
                        items.ExtraInt2 = model.ExtraInt2;
                        items.ExtraBit1 = model.ExtraBit1;
                        items.ExtraBit2 = model.ExtraBit2;

                        if (model.FromActionType == -1) { items.IsDeleted = true; } //Delete the Object
                        items.IsActive = model.IsActive;

                        if (model.UseDefaultLogo)
                        { 
                            items.TargetIconUrl = null; 
                        }
                        else if (Icon != null)
                        {
                            items.TargetIconUrl = UploadMedia(Icon);
                        }

                        if (model.UseDefaultLogoAr)
                        {
                            items.TargetIconUrlAr = null;
                        }
                        else if (IconAr != null)
                        {
                            items.TargetIconUrlAr = UploadMedia(IconAr);
                        }

                        if (Vid != null)
                        {
                            items.VideoUrl = UploadMedia(Vid);
                        }
                        if (Img != null)
                        {
                            items.BackgroudImg = UploadLogo(Img);
                        }

                        if (isnew) { items.IsDeleted = false; dataMgr.TourTs.Add(items); }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.Id;
                        if (isnew)
                        { logMsg = "TourT Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }
                        else
                        { logMsg = "TourT Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }

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

            return RedirectToAction("define", "TourT", new { rid = tempId });
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
                        var items = dataMgr.TourTs.Where(x => x.Id == rid).SingleOrDefault();

                        items.IsDeleted = true;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "TourT Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "TourT Deleted-ERR";
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
            return RedirectToAction("TourTList", "TourT");
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
                        var items = dataMgr.TourTs.Where(x => x.Id == rid).SingleOrDefault();
                        items.IsActive = val;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        logMsg = "TourT" + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "TourT" + msg + "-ERR";
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
            return RedirectToAction("TourTList", "TourT");
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

        public string UploadMedia(HttpPostedFileBase media)
        {
            string errorMsg = "";
            string fileEx = "JPG,JPEG,PNG,TIF,TIFF,GIF,BMP,ICO,MP4,MP3";
            string fileNamex = "";

            string fileExImage = "JPG,JPEG,PNG,TIF,TIFF, BMP,ICO";
            string fileExAV = "GIF, MP4, MP3";

            string fileOriginalExtension = media.ContentType.Split('/')[1].ToUpper();

            if (!fileEx.Contains(media.ContentType.Split('/')[1].ToUpper()))
            {
                ViewBag.status = "Error";
                errorMsg = "Invalid Image File Type";
                // base.SetOperationCompleted("warning", "UploadLogo", errorMsg);
                base.SetInnerOperationCompleted("warning", "UploadLogo", errorMsg);


                ModelState.AddModelError("error", errorMsg); throw new IOException(errorMsg);
            }
            //var ufile = Request.Files[0];
            if (media.ContentLength > 50000000)
            {
                errorMsg = "File is too large >50MB";
                // base.SetOperationCompleted("warning", "UploadLogo", errorMsg);
                base.SetInnerOperationCompleted("warning", "UploadLogo", errorMsg);

                ModelState.AddModelError("error", errorMsg); throw new IOException(errorMsg);
            }

            var path = "";

            string newguid = System.Guid.NewGuid().ToString();
            var fileName = newguid;



            if (media != null && media.ContentLength > 0)
            {
                Dictionary<string, string> versions = new Dictionary<string, string>();

                //Define the versions to generate
                if (fileExImage.Contains(media.ContentType.Split('/')[1].ToUpper()))
                {
                    versions.Add("_thumb", "width=32&height=32&crop=auto&format=jpg"); //Crop to square thumbnail
                    versions.Add("_small", "width=80&height=80&crop=auto&format=jpg");
                    versions.Add("_medium", "width=320&height=320&crop=auto&format=jpg"); //Fit inside 400x400 area, jpeg
                    versions.Add("_large", "maxwidth=960&maxheight=640&format=jpg"); //Fit inside 1900x1200 area
                    versions.Add("_base", "maxwidth=480&maxheight=360&format=jpg"); //Fit inside 1900x1200 area
                    versions.Add("_wide", "width=2000&crop=auto&format=jpg");
                }

                if (fileExAV.Contains(media.ContentType.Split('/')[1].ToUpper()))
                {
                    versions.Add("", "");
                }


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

                        if (fileExImage.Contains(media.ContentType.Split('/')[1].ToUpper()))
                        {
                            //Generate a filename (GUIDs are best).
                            fileNamex = Path.Combine(uploadFolder, newguid + suffix);
                            //Let the image builder add the correct extension based on the output file type
                            fileNamex = ImageBuilder.Current.Build(media, fileNamex, new ResizeSettings(versions[suffix]), false, true);
                        }
                        else
                        {
                            fileNamex = Path.Combine(uploadFolder, newguid + System.IO.Path.GetExtension(media.FileName));
                            media.SaveAs(fileNamex);
                        }
                    }
                    path = appfolder + newguid;


                }


            }
            return path + System.IO.Path.GetExtension(media.FileName);
        }

        [HttpGet]

     
        public ViewResult TourTList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetTourT_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.TourTModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.TourTModel();
                l_item.Id = item.Id;
                l_item.Controller = item.Controller;

                l_item.Action = item.Action;
                l_item.TargetOrder = item.TargetOrder;
                l_item.TargetClass = item.TargetClass;
                l_item.TargetSubject = item.TargetSubject;
                l_item.TargetDesc = item.TargetDesc;
                l_item.TargetIconUrl = item.TargetIconUrl;
                l_item.TargetUrl = item.TargetUrl;
                l_item.placement = item.placement;
                l_item.BackgroudImg = item.BackgroudImg;
                l_item.VideoUrl = item.VideoUrl;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;


                l_item.TargetSubjectAr = item.TargetSubjectAr;
                l_item.TargetDescAr = item.TargetDescAr;
                l_item.TargetIconUrlAr = item.TargetIconUrlAr;
                l_item.ExtraText = item.ExtraText;
                l_item.ExtraInt1 = item.ExtraInt1;
                l_item.ExtraInt2 = item.ExtraInt2;
                l_item.ExtraBit1 = item.ExtraBit1;
                l_item.ExtraBit2 = item.ExtraBit2;

                ls_items.Add(l_item);
            }

            return View(ls_items);
        }
        public PartialViewResult _GetList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetTourT_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.TourTModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.TourTModel();
                l_item.Id = item.Id;
                l_item.Controller = item.Controller;

                l_item.Action = item.Action;
                l_item.TargetOrder = item.TargetOrder;
                l_item.TargetClass = item.TargetClass;
                l_item.TargetSubject = item.TargetSubject;
                l_item.TargetDesc = item.TargetDesc;
                l_item.TargetIconUrl = item.TargetIconUrl;
                l_item.TargetUrl = item.TargetUrl;
                l_item.placement = item.placement;
                l_item.BackgroudImg = item.BackgroudImg;
                l_item.VideoUrl = item.VideoUrl;
                l_item.IsActive = item.IsActive;
                l_item.IsDeleted = item.IsDeleted;


                l_item.TargetSubjectAr = item.TargetSubjectAr;
                l_item.TargetDescAr = item.TargetDescAr;
                l_item.TargetIconUrlAr = item.TargetIconUrlAr;
                l_item.ExtraText = item.ExtraText;
                l_item.ExtraInt1 = item.ExtraInt1;
                l_item.ExtraInt2 = item.ExtraInt2;
                l_item.ExtraBit1 = item.ExtraBit1;
                l_item.ExtraBit2 = item.ExtraBit2;
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
