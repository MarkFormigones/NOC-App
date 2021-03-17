using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
 
 


namespace Hydron.Controllers
{
    public class BaseController : Controller
    {


        protected override void OnAuthorization(AuthorizationContext filterContext) // catching all requsts to the server
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (Session[DAL.General.UserPara.ID.ToString()] == null)
                    {
                        SetUserParam(User.Identity.Name);// fill session variables when server is down/restart
                    }
                }
                else
                {
                    string logMsg = User.Identity.Name + " Is not authenticated for action: " + filterContext.ActionDescriptor.ActionName.ToString();
                    this.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, -1, -1, -1, -1 - 1, -1,-1,-1,-1,-1);
                    this.SetOperationCompleted("error", "Operation", logMsg);
                    throw new UnauthorizedAccessException();
                }
             

                base.OnAuthorization(filterContext);

            }
            catch (Exception)
            {
                string logMsg = User.Identity.Name + " Is not authenticated for action: " + filterContext.ActionDescriptor.ActionName.ToString();
                this.Logger(DAL.General.ActivityLokup.ERROR, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, -1, -1, -1, -1 - 1, -1,-1,-1,-1,-1);
                this.SetOperationCompleted("error", "Operation", logMsg);
               // throw new UnauthorizedAccessException();

            }
         
         
        }

        public int GlobalUserId
        {
            get
            {
                if (Session != null && Session[DAL.General.UserPara.ID.ToString()]!=null) { return int.Parse(Session[DAL.General.UserPara.ID.ToString()].ToString()); } else { return -1; };
            }
            set
            {
                Session[DAL.General.UserPara.ID.ToString()] = value;
            }
        }
        public string GlobalUserName
        {
            get
            {
                if (Session != null && Session[DAL.General.UserPara.NAME.ToString()] != null) { return Session[DAL.General.UserPara.NAME.ToString()].ToString(); } else { return "anonymous"; };
            }
            set
            {
                Session[DAL.General.UserPara.NAME.ToString()] = value;
            }
        }
        public string GlobalUserEmail
        {
            get
            {
                if (Session != null && Session[DAL.General.UserPara.EMAIL.ToString()] != null) { return Session[DAL.General.UserPara.EMAIL.ToString()].ToString(); } else { return "anonymous@elemega.com"; };
            }
            set
            {
                Session[DAL.General.UserPara.EMAIL.ToString()] = value;
            }
        }
        public string GlobalUserPicURL
        {
            get
            {
                if (Session != null && Session[DAL.General.UserPara.PIC.ToString()] != null) { return Session[DAL.General.UserPara.PIC.ToString()].ToString(); } else { return "anonymous@elemega.com"; };
            }
            set
            {
                Session[DAL.General.UserPara.PIC.ToString()] = value;
            }
        }
        public int GlobalBUnitID
        {
            get
            {
                if (Session != null && Session[DAL.General.UserPara.BUNIT.ToString()]!=null) { return int.Parse(Session[DAL.General.UserPara.BUNIT.ToString()].ToString()); } else { return -1; };
            }
            set
            {
                Session[DAL.General.UserPara.BUNIT.ToString()] = value;
            }
        }
        public int GlobalProcessID
        {
            get
            {
                if (Session != null && Session[DAL.General.UserPara.PROCCESS.ToString()] != null) { return int.Parse(Session[DAL.General.UserPara.PROCCESS.ToString()].ToString()); } else { return -1; };
            }
            set
            {
                Session[DAL.General.UserPara.PROCCESS.ToString()] = value;
            }
        }

        public void InitUserParam(string userName)
        {//Called when LOGOUT 
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetUserParams(userName);


            GlobalUserPicURL = "";
            GlobalUserId = -1;
            GlobalUserName = "";
            GlobalUserEmail = "";
            GlobalBUnitID = -1; 
            GlobalProcessID = -1; 
        }
        public void SetUserParam(string userName)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.GetUserParams(userName);


            GlobalUserPicURL = items.UserPic;
            GlobalUserId = items.UserId;
            GlobalUserName = items.UserName;
            GlobalUserEmail = items.UserEmail;
            GlobalBUnitID = 999;//temp
           GlobalProcessID = 999;//temp
        }

        public void SetUserParam(string userName, int uId, string email, string url)
        {

            GlobalUserPicURL = url;
            GlobalUserId = uId;
            GlobalUserName = userName;
            GlobalUserEmail = email;
            GlobalBUnitID = 999;//temp
            GlobalProcessID = 999;//temp
        }


        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            ProfileUpdateSuccess,
            InvalidFile
        }


        public bool Logger(DAL.General.ActivityLokup actionLog, DAL.General.OperationLokup operationType, DAL.General.ActionCommandReq command, string msg, int fileId, int userId, int companyId, int divId, int bUnitId, int processId, int actionUnitId, int projectId, int workspaceId)
        {
            //if (Session != null && GlobalUserId != null)
            //{
            //    if (userId == -1 || userId == null) { userId = GlobalUserId; }// if the user id =-1 this set to the current user
            //    if (bUnitId == -1 || bUnitId == null) { bUnitId = GlobalLoggedInBUnitId; }// if the BUnit id =-1 this set to the current BUnit
            //    if (divId == -1 || divId == null) { divId = GlobalLoggedInDivisionId; }// if the BUnit id =-1 this set to the current BUnit
            //    if (processId == -1 || processId == null) { processId = GlobalProcessID; }// NOT prefered --if the Process id =-1 this set to the current Process
            //    if (companyId == -1 || companyId == null) { companyId = GlobalLoggedInCompanyId; }// if the Process id =-1 this set to the current Process
            //    if (projectId == -1 || projectId == null) { projectId = GlobalProjectId; }// if the Process id =-1 this set to the current Process
            //}

            var cIP = GetVisitorIpAddress();
            var gClass = new DAL.General();
            gClass.Logger(actionLog, operationType, command, msg, fileId, userId, companyId, divId, bUnitId, processId, actionUnitId, cIP, projectId, workspaceId);


            return true;

        }

        //public bool Logger(DAL.General.ActivityLokup actionLog, DAL.General.OperationLokup operationType, string msg, int fileId, int userId, int bUnitId, int processId)
        //{
        //    //   if (Session != null && GlobalUserId!= null)
        //    //{
        //    //    if (userId == -1) { userId = GlobalUserId; }// if the user id =-1 this set to the current user
        //    //    if (bUnitId == -1) { bUnitId = GlobalBUnitID; }// if the BUnit id =-1 this set to the current BUnit
        //    //    if (processId == -1) { processId = GlobalProcessID; }// if the Process id =-1 this set to the current Process
        //    //}

        //    //var cIP=GetVisitorIpAddress();
        //    //var gClass= new DAL.General();
        //    //gClass.Logger(actionLog, operationType, msg, fileId, userId, bUnitId, processId, cIP);

        //    ////DAL.DataManager dataMgr = new DAL.DataManager();
        //    ////var filelogRow = new DAL.ActivityLog();
        //    //////   var filerowlast = dataMgr.FilesUploadeds.LastOrDefault();
        //    ////filelogRow.RecordId = fileId;
        //    ////switch (actionLog)
        //    ////{
        //    ////    case ActivityLokup.FILES:
        //    ////        filelogRow.Activity_Lok = 1; break;
        //    ////    case ActivityLokup.ACTIVITY:
        //    ////        filelogRow.Activity_Lok = 2; break;
        //    ////    case ActivityLokup.ERROR:
        //    ////        filelogRow.Activity_Lok = 3; break;
        //    ////    case ActivityLokup.EXCEPTION:
        //    ////        filelogRow.Activity_Lok = 4; break;
        //    ////}

        //    ////switch (operationType)
        //    ////{
        //    ////    case OperationLokup.LOGIN:
        //    ////        filelogRow.Operation_Lok = 1; break;
        //    ////    case OperationLokup.LOGOUT:
        //    ////        filelogRow.Operation_Lok = 2; break;
        //    ////    case OperationLokup.ADDNEW:
        //    ////        filelogRow.Operation_Lok = 3; break;
        //    ////    case OperationLokup.UPDATE:
        //    ////        filelogRow.Operation_Lok = 4; break;
        //    ////    case OperationLokup.DELETE:
        //    ////        filelogRow.Operation_Lok = 5; break;
        //    ////    case OperationLokup.FILE_OPERATION:
        //    ////        filelogRow.Operation_Lok = 6; break;
        //    ////    case OperationLokup.NONE:
        //    ////        filelogRow.Operation_Lok = 7; break;
        //    ////}

        //    ////filelogRow.UserId = userId;
        //    ////filelogRow.BUnitId = bUnitId;
        //    ////filelogRow.ProcessId = processId;

        //    ////filelogRow.Dated = DateTime.Now;
        //    ////filelogRow.Msg = msg;
        //    ////filelogRow.IsDeleted = false;
        //    ////filelogRow.ClientIP = GetVisitorIpAddress();
        //    ////dataMgr.ActivityLogs.Add(filelogRow);
        //    //////Log the file Activity
        //    ////dataMgr.SaveChanges();
        //    return true;

        //}


        public void SetOperationCompleted(string flag, string title, string msg)
        {

            TempData["flag"] = flag;
            TempData["title"] = title;
            TempData["statusMsg"] = msg;
        }

         public void SetInnerOperationCompleted(string flag, string title, string msg)
        {

            TempData["innerFlag"] = flag;
            TempData["innerTitle"] = title;
            TempData["innerMsg"] = msg;
        }

        
        public string GetVisitorIpAddress()
        {

            string stringIpAddress = "0.0.0.0";
            try
            {
                stringIpAddress = Request.ServerVariables["REMOTE_ADDR"];

                if (stringIpAddress == null) //may be the HTTP_X_FORWARDED_FOR is null
                    stringIpAddress = Request.ServerVariables["REMOTE_ADDR"]; //we can use REMOTE_ADDR
                else if (stringIpAddress == null)
                    stringIpAddress = GetLanIPAddress();
            }
            catch (Exception)
            {


            }


            return stringIpAddress;
        }

        //Get Lan Connected IP address method
        public string GetLanIPAddress()
        {
            //Get the Host Name
            string stringHostName = Dns.GetHostName();
            //Get The Ip Host Entry
            IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
            //Get The Ip Address From The Ip Host Entry Address List
            System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;
            return arrIpAddress[arrIpAddress.Length - 1].ToString();
        }
    }
}