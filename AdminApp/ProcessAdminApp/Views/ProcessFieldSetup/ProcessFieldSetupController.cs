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
    public class ProcessFieldSetupController : BaseController
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
        public ViewResult Define(int? rId, int? cId, string vw)
        {

            DAL.DataManager dataMgr = new DAL.DataManager();
            var pInfo = new DAL.ProcessFieldSetup();
            IList<DAL.ProcessFieldLookup> FieldNameList;
            if (rId > 0)
            {
                FieldNameList = dataMgr.GetFieldNameAll_List(cId);
            }
            else
            {
                FieldNameList = dataMgr.GetFieldNameNew_List();
            }
                
            int adminId = -1;
            if (rId == null || rId <= 0)
            {
                //pInfo = dataMgr.GetCompanyInfoById(uId);
                //SetUserParam(User.Identity.Name);// this will apply changes on session, avatar and email info
            }
            else
            {
                pInfo = dataMgr.GetProcessFieldSetupInfoById(rId);
            }


            var pInfoM = new ProcessFieldSetupModel(pInfo);
            //if (string.IsNullOrEmpty(pInfoM.DepartmentLogo)) { pInfoM.UseDefaultLogo = true; }//logo initialization


            pInfoM.FieldNameListData = ToFieldNameListItems(FieldNameList, -1);           
            pInfoM.FieldNameListData = pInfoM.FieldNameListData;


            //pInfoM.BUnitId = int.Parse(rId.ToString());
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }

        
        [HttpPost]
        public ActionResult Define(ProcessFieldSetupModel model) //, HttpPostedFileBase file
        {
            int tempId = 0; int tempcId=0; string logMsg = "Division";
            if (ModelState.IsValid)
            {

                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {

                    try
                    {
                        bool isnew = true;
                        var items = new DAL.ProcessFieldSetup();
                        DAL.DataManager dataMgr = new DAL.DataManager();

                        var rec = dataMgr.ProcessFieldSetups.Where(x => x.Id == model.Id).Count();

                        if (rec > 0)
                        {
                            isnew = false;
                            items = dataMgr.ProcessFieldSetups.Where(x => x.Id == model.Id).SingleOrDefault();
                        }

                        //if (model.ColumnId > 0)
                        //{
                        //    isnew = false;
                        //    items = dataMgr.ProcessFieldSetups.Where(x => x.ColumnId == model.ColumnId).SingleOrDefault();
                        //}

                        items.ColumnId = model.FieldNameListId;
                        //items.ColumnId = model.ColumnId;
                        items.ProcessId = model.ProcessId;
                        items.TableId = model.TableId;
                        items.ShowHide = model.ShowHide;


                        items.DefaultValue = model.DefaultValue;
                        items.ManOptional = model.ManOptional;
                        items.DefaultInt = model.DefaultInt;
                        items.ExtraText = model.ExtraText;
                        items.ExtraInt = model.ExtraInt;
                        items.Enabled = model.Enabled;
                        

                        /* if (model.FromActionType == -1)*/ /*{ items.IsDeleted = true; }*/ //Delete the Object

                        //if (model.UseDefaultLogo)
                        //{ 
                        //    items.DepartmentLogo = null; 
                        //}
                        //else if (file != null)
                        //{
                        //    items.DepartmentLogo = UploadLogo(file);
                        //}
                        if (isnew)
                        {
                        //items.IsDeleted = false;
                        dataMgr.ProcessFieldSetups.Add(items);
                        }//New Object
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = items.Id;
                        tempcId = items.ColumnId;

                        if (isnew)
                        { logMsg = "ProcessFieldSetup Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }
                        else
                        { logMsg = "ProcessFieldSetup Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }

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

            return RedirectToAction("define", "ProcessFieldSetup", new { rid = tempId, cId = tempcId });
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
                        var items = dataMgr.ProcessFieldSetups.Where(x => x.Id == rid).SingleOrDefault();
                        /*items.IsPublic = val;*/  //Delete the Object                     
                        dataMgr.SaveChanges();
                        logMsg = "ProcessFieldSetup" + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "ProcessFieldSetup" + msg + "-ERR";
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
            return RedirectToAction("ProcessFieldSetupList", "ProcessFieldSetup");
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
                        var items = dataMgr.ProcessFieldSetups.Where(x => x.Id == rid).SingleOrDefault();

                        /*items.IsDeleted = true;*/  //Delete the Object                     
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "ProcessFieldSetup Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "ProcessFieldSetup Deleted-ERR";
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
            return RedirectToAction("ProcessFieldSetupList", "ProcessFieldSetup");
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

     
        public ViewResult ProcessFieldSetupList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetProcessFieldSetup_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.ProcessFieldSetupModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.ProcessFieldSetupModel();

                var records = dataMgr.GetFieldNameAll_List(item.ColumnId);

                foreach (var i in records)
                {
                    l_item.FieldName = i.FieldName;
                }

                l_item.Id = item.Id;
                l_item.ColumnId = item.ColumnId;
                l_item.ProcessId = item.ProcessId;
                l_item.TableId = item.TableId;
                l_item.ShowHide = item.ShowHide;
                l_item.DefaultValue = item.DefaultValue;
                l_item.ManOptional = item.ManOptional;

                l_item.DefaultInt = item.DefaultInt;
                l_item.ExtraText = item.ExtraText;
                l_item.ExtraInt = item.ExtraInt;
                l_item.Enabled = item.Enabled;
                
                ls_items.Add(l_item);
            }

            return View(ls_items);
        }
        public PartialViewResult _GetListE()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetProcessFieldSetup_List();
            //GetProcessFieldSetup_ListbyProcessId(id) ->
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.ProcessFieldSetupModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.ProcessFieldSetupModel();

                var records = dataMgr.GetFieldNameAll_List(item.ColumnId);
                
                foreach(var i in records)
                {
                    l_item.FieldName = i.FieldName;
                }
                
                l_item.Id = item.Id;
                l_item.ColumnId = item.ColumnId;
                l_item.ProcessId = item.ProcessId;
                l_item.TableId = item.TableId;
                l_item.ShowHide = item.ShowHide;
                l_item.DefaultValue = item.DefaultValue;
                l_item.ManOptional = item.ManOptional;

                l_item.DefaultInt = item.DefaultInt;
                l_item.ExtraText = item.ExtraText;
                l_item.ExtraInt = item.ExtraInt;
                l_item.Enabled = item.Enabled;

                ls_items.Add(l_item);
            }

            return PartialView(ls_items);

        }




         public static IEnumerable<System.Web.Mvc.SelectListItem> ToFieldNameListItems(IEnumerable<DAL.ProcessFieldLookup> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.ColumnId).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.ColumnId == selectedId), Text = obj.FieldName, Value = obj.ColumnId.ToString() });
        }


        [HttpPost]
        public ActionResult QuickUpdate(ProcessFieldSetupModel model)
        {
            if (ModelState.IsValid)
            {

                using (TransactionScope transaction = new TransactionScope())
                {

                    try
                    {
                        var items = new DAL.ProcessFieldSetup();
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        items = dataMgr.ProcessFieldSetups.Where(x => x.Id == model.Id).SingleOrDefault();

                        //items.ColumnId = model.FieldNameListId;
                        //items.ProcessId = model.ProcessId;
                        //items.TableId = model.TableId;
                        items.ShowHide = model.ShowHide;
                        items.DefaultValue = model.DefaultValue;
                        items.ManOptional = model.ManOptional;
                        //items.DefaultInt = model.DefaultInt;
                        //items.ExtraText = model.ExtraText;
                        //items.ExtraInt = model.ExtraInt;
                        items.Enabled = model.Enabled;
                                            
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                                          
                        transaction.Complete();

                        //base.SetOperationCompleted("success", "Saved", "successfully");

                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        //base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
                        //base.SetOperationCompleted("error", "Operation", "failed");
                        //base.SetInnerOperationCompleted("error", "Operation", e.Message);
                        ModelState.AddModelError("error", e.Message);
                    }
                }
            }
            else
            {
                //logMsg = "invalid model state";
                //base.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
                //base.SetOperationCompleted("info", "Operation", logMsg);
            }
            return Json(new { success = "Changes Saved."});
        }

        public PartialViewResult _GetListbyProcId(int pId)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetProcessFieldSetup_ListbyProcessId(pId);

            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.ProcessFieldSetupModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.ProcessFieldSetupModel();

                var records = dataMgr.GetFieldNameAll_List(item.ColumnId);

                foreach (var i in records)
                {
                    l_item.FieldName = i.FieldName;
                }

                l_item.Id = item.Id;
                l_item.ColumnId = item.ColumnId;
                l_item.ProcessId = item.ProcessId;
                l_item.TableId = item.TableId;
                l_item.ShowHide = item.ShowHide;
                l_item.DefaultValue = item.DefaultValue;
                l_item.ManOptional = item.ManOptional;

                l_item.DefaultInt = item.DefaultInt;
                l_item.ExtraText = item.ExtraText;
                l_item.ExtraInt = item.ExtraInt;
                l_item.Enabled = item.Enabled;

                ls_items.Add(l_item);
            }

            return PartialView(ls_items);

        }


        public ActionResult InitializeProcessSetup(int pId)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            //check if processId is exist
            var items = dataMgr.GetProcessFieldSetup_ListbyProcessId(pId);
            if (items.Count == 0)
                //if exist get lookups data
            {                   
                var lookups = dataMgr.GetProcessFieldLookup_List();
                foreach (var lookup in lookups) //add lookups one by one.
                {                 
                    var Setup = new DAL.ProcessFieldSetup();
                    Setup.ColumnId = lookup.ColumnId;
                    Setup.ProcessId = pId;
                    Setup.TableId = Convert.ToInt32(lookup.TableId);
                    Setup.ShowHide = true;
                    //Setup.DefaultValue = null;
                    Setup.ManOptional = true;
                    //Setup.DefaultInt = null;
                    //Setup.ExtraText = null;
                    //Setup.ExtraInt = null;
                    Setup.Enabled = true;

                   dataMgr.ProcessFieldSetups.Add(Setup);
                   dataMgr.SaveChanges();
                }
            }

            return RedirectToAction("ProcessFieldSetuplist", "ProcessFieldSetup");

        }

    }
}
