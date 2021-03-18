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
    public class CustomerController : BaseController
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
            var pInfo = new DAL.Customer();
           
            


            int adminId = -1;
            if (rId == null || rId == -1)
            {
                //pInfo = dataMgr.GetCompanyInfoById(uId);
                //SetUserParam(User.Identity.Name);// this will apply changes on session, avatar and email info

                //pInfo.Date = DateTime.Now.Date;
            }
            else
            {
                pInfo = dataMgr.GetCustomerInfoById(rId);
            }




            var pInfoM = new CustomerModel(pInfo);
            pInfoM = populateDropDown(dataMgr, pInfoM);


            if (string.IsNullOrEmpty(pInfoM.CustomerLogo)) { pInfoM.UseDefaultLogo = true; }//logo initialization
           
            
            pInfoM.ReadEdit = vw;
            return View(pInfoM);
        }

        public CustomerModel populateDropDown(DAL.DataManager dataMgr, CustomerModel pInfoM)
        {
            var classList = dataMgr.GetCustomerClass_List();
            var categoryList = dataMgr.GetCustomerCategory_List();
            var typeList = dataMgr.GetCustomerType_List();

            pInfoM.CustomerClassList = ToCustomerListItems(classList, -1);
            pInfoM.CustomerClassName = classList.Where(x => x.Id == pInfoM.CustomerClass).Select(x => x.Name).FirstOrDefault();

            pInfoM.CustomerCategoryList = ToCustomerListItems(categoryList, -1);
            pInfoM.CustomerCategoryName = categoryList.Where(x => x.Id == pInfoM.CustomerCategory).Select(x => x.Name).FirstOrDefault();

            pInfoM.CustomerTypeList = ToCustomerListItems(typeList, -1);
            pInfoM.CustomerTypeName = typeList.Where(x => x.Id == pInfoM.CustomerType).Select(x => x.Name).FirstOrDefault();

            return pInfoM;
        }

        
        [HttpPost]
        public ActionResult Define(CustomerModel model, HttpPostedFileBase file)
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
                        var item = new DAL.Customer();
                       
                        if (model.Id > 0)
                        {
                            isnew = false;
                            item = dataMgr.Customers.Where(x => x.Id == model.Id).SingleOrDefault();
                                                      
                            item.UpdateDate = DateTime.Now;

                        }
                        else
                        {
                            int cnt = dataMgr.Customers.Where(x => x.CustomerName.Replace(" ", string.Empty).ToUpper() == model.CustomerName.Replace(" ", string.Empty).ToUpper()).Count();
                            if (cnt > 0)
                            {
                                ModelState.AddModelError("CustomerName", "Customer Name is already exists. ");
                                model = populateDropDown(dataMgr, model);
                                return View(model);
                            }                          
                        }


                        item.CustomerNo = model.CustomerNo;
                        item.CustomerAcctNo = model.CustomerAcctNo;
                        item.CustomerAcctNoSub = model.CustomerAcctNoSub;
                        
                        item.CustomerName = model.CustomerName.ToUpper();
                        item.CustomerAddress = model.CustomerAddress;
                        item.CustomerEmail = model.CustomerEmail;
                        item.CustomerContactNo = model.CustomerContactNo;
                        item.CustomerWebsite = model.CustomerWebsite;
                        item.CustomerContract = model.CustomerContract;

                     
                        item.CustomerClass = model.CustomerClass;
                        item.CustomerCategory = model.CustomerCategory;
                        item.CustomerType = model.CustomerType;
       
                    
                        if (model.FromActionType == -1) { item.IsDeleted = true; }
                       

                        item.IsActive = model.IsActive;
                        if (model.IsActive)
                            item.ActivateDate = DateTime.Now;

                        item.IsClosed = model.IsClosed;
                        if (model.IsClosed && model.ClosingDate == null)
                            item.ClosingDate = DateTime.Now;


                        if (model.UseDefaultLogo)
                        {
                            item.CustomerLogo = null;
                        }
                        else if (file != null)
                        {
                            item.CustomerLogo = UploadLogo(file);
                        }
                        if (isnew) {
                            item.CreationDate = DateTime.Now;
                            item.IsDeleted = false; 
                            dataMgr.Customers.Add(item); 
                        }//New Object
                        
                        dataMgr.SaveChanges();

                        ViewBag.status = "Success";
                        tempId = item.Id;
                        if (isnew)
                        { logMsg = "Customer Added-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.ADDNEW, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }
                        else
                        { logMsg = "Customer Updated-OK"; base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default,logMsg, tempId, -1, -1, -1,-1,-1,-1,-1,-1); }

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

            return RedirectToAction("define", "Customer", new { rid = tempId });
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
                        var items = dataMgr.Customers.Where(x => x.Id == rid).SingleOrDefault();

                        items.IsDeleted = true;  //Delete the Object                     
                        dataMgr.SaveChanges();
                        ViewBag.status = "Deleted";
                        logMsg = "Customer Deleted-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.DELETE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);

                        transaction.Complete();
                        base.SetOperationCompleted("success", "Deleted", "successfully");
                    }

                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "Customer Deleted-ERR";
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
            return RedirectToAction("CustomersList", "Customer");
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
                        var items = dataMgr.Customers.Where(x => x.Id == rid).SingleOrDefault();
                        items.IsActive = val;  //Delete the Object                     

                        if (val == true && items.ActivateDate == null)
                            items.ActivateDate = DateTime.Now;

                        
                        dataMgr.SaveChanges();
                        logMsg = "Customer" + msg + "-OK";
                        base.Logger(DAL.General.ActivityLokup.ACTIVITY, DAL.General.OperationLokup.UPDATE, DAL.General.ActionCommandReq.Default, logMsg, rid, -1, -1, -1, -1, -1, -1, -1, -1);
                        base.SetOperationCompleted("success", msg, "successfully");
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        ViewBag.status = "Error";
                        logMsg = "Customer" + msg + "-ERR";
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
            return RedirectToAction("CustomersList", "Customer");
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

     
        public ViewResult CustomersList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetCustomer_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.CustomerModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.CustomerModel();
                l_item.Id = item.Id;

                l_item.CustomerNo = item.CustomerNo;
                l_item.CustomerAcctNo = item.CustomerAcctNo;
                l_item.CustomerAcctNoSub = item.CustomerAcctNoSub;
                l_item.CustomerName = item.CustomerName;
                l_item.CustomerAddress = item.CustomerAddress;
                l_item.CustomerEmail = item.CustomerEmail;
                l_item.CustomerContactNo = item.CustomerContactNo;
                l_item.CustomerWebsite = item.CustomerWebsite;
                l_item.CustomerContract = item.CustomerContract;
                l_item.CustomerClass = item.CustomerClass;
                l_item.CustomerCategory = item.CustomerCategory;
                l_item.CustomerType = item.CustomerType;
                l_item.IsActive = item.IsActive;
                l_item.ActivateDate = item.ActivateDate;
                l_item.UpdateDate = item.UpdateDate;
                l_item.IsDeleted = item.IsDeleted;
                l_item.CustomerLogo = item.CustomerLogo;
                l_item.CreationDate = item.CreationDate;
                l_item.IsClosed = item.IsClosed;
                l_item.ClosingDate = item.ClosingDate;
                ls_items.Add(l_item);
            }

            return View(ls_items);
        }
        public PartialViewResult _GetList()
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetCustomer_List();
            var Roleitems = dataMgr.GetUser_Roles();
            var ls_items = new List<Models.Definitions.CustomerModel>();
            foreach (var item in items)
            {
                var l_item = new Models.Definitions.CustomerModel();
                l_item.CustomerNo = item.CustomerNo;
                l_item.CustomerAcctNo = item.CustomerAcctNo;
                l_item.CustomerAcctNoSub = item.CustomerAcctNoSub;
                l_item.CustomerName = item.CustomerName;
                l_item.CustomerAddress = item.CustomerAddress;
                l_item.CustomerEmail = item.CustomerEmail;
                l_item.CustomerContactNo = item.CustomerContactNo;
                l_item.CustomerWebsite = item.CustomerWebsite;
                l_item.CustomerContract = item.CustomerContract;
                l_item.CustomerClass = item.CustomerClass;
                l_item.CustomerCategory = item.CustomerCategory;
                l_item.CustomerType = item.CustomerType;
                l_item.IsActive = item.IsActive;
                l_item.ActivateDate = item.ActivateDate;
                l_item.UpdateDate = item.UpdateDate;
                l_item.IsDeleted = item.IsDeleted;
                l_item.CustomerLogo = item.CustomerLogo;
                l_item.CreationDate = item.CreationDate;
                l_item.IsClosed = item.IsClosed;
                l_item.ClosingDate = item.ClosingDate;
                ls_items.Add(l_item);
            }

            return PartialView(ls_items);

        }


        public static IEnumerable<System.Web.Mvc.SelectListItem> ToCustomerListItems(IEnumerable<DAL.ProcessCategory> lList, int selectedId)
        {
            return lList.OrderBy(obj => obj.Id).Select(obj => new System.Web.Mvc.SelectListItem { Selected = (obj.Id == selectedId), Text = obj.Name, Value = obj.Id.ToString() });
        }

  

    }
}
