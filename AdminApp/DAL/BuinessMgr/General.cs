using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class General
    {

        public enum ActionCommandReq
        {

            Default=0 
         
        }
        public enum UserPara
        {
            ID,
            NAME,
            EMAIL,
            PIC,
            PROCCESS,
            BUNIT,

        }


        public enum ActivityLokup
        {
            FILES = 1,
            ACTIVITY = 2,
            ERROR = 3,
            EXCEPTION = 4
        }
        public enum OperationLokup
        {
            LOGIN = 1,
            LOGOUT = 2,
            ADDNEW = 3,
            UPDATE = 4,
            DELETE = 5,
            FILE_OPERATION = 6,
            NONE = 7
        }

        public bool Logger(ActivityLokup actionLog, OperationLokup operationType, ActionCommandReq command, string msg, int fileId, int userId, int companyId, int divId, int bUnitId, int processId, int actionUnitId, string clientIP, int projectId, int workspaceId)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var filelogRow = new DAL.ActivityLog();
            //   var filerowlast = dataMgr.FilesUploadeds.LastOrDefault();
            filelogRow.RecordId = fileId;


            filelogRow.Activity_Lok = (int)actionLog;
            filelogRow.Operation_Lok = (int)operationType;
            //filelogRow.CommandId = (int)command;
            //filelogRow.DivisionId = (int)divId;
            //filelogRow.ProjectId = (int)projectId;
            //filelogRow.WorkSpaceId = (int)workspaceId;



            //filelogRow.UserId = userId;
            //filelogRow.CompanyId = companyId;
            //filelogRow.BUnitId = bUnitId;
            //filelogRow.ProcessId = processId;
            //filelogRow.ActionUnitId = actionUnitId;
            //filelogRow.CommandStatus = 1;
            //switch (actionLog)// status is based on log, 1 if file/activity, -1 when error or exc
            //{
            //    case ActivityLokup.ERROR:
            //        filelogRow.CommandStatus = -1; break;
            //    case ActivityLokup.EXCEPTION:
            //        filelogRow.CommandStatus = -1; break;
            //}


            filelogRow.Dated = DateTime.Now;
            filelogRow.Msg = msg;
            filelogRow.IsDeleted = false;
            filelogRow.ClientIP = clientIP;
            dataMgr.ActivityLogs.Add(filelogRow);
            //Log the file Activity
            dataMgr.SaveChanges();
            return true;

        }



        //public bool Logger(ActivityLokup actionLog, OperationLokup operationType, string msg, int fileId, int userId, int bUnitId, int processId, string clientIP)
        //{
        //    DAL.DataManager dataMgr = new DAL.DataManager();
        //    var filelogRow = new DAL.ActivityLog();
        //    //   var filerowlast = dataMgr.FilesUploadeds.LastOrDefault();
        //    filelogRow.RecordId = fileId;
        //    switch (actionLog)
        //    {
        //        case ActivityLokup.FILES:
        //            filelogRow.Activity_Lok = 1; break;
        //        case ActivityLokup.ACTIVITY:
        //            filelogRow.Activity_Lok = 2; break;
        //        case ActivityLokup.ERROR:
        //            filelogRow.Activity_Lok = 3; break;
        //        case ActivityLokup.EXCEPTION:
        //            filelogRow.Activity_Lok = 4; break;
        //    }

        //    switch (operationType)
        //    {
        //        case OperationLokup.LOGIN:
        //            filelogRow.Operation_Lok = 1; break;
        //        case OperationLokup.LOGOUT:
        //            filelogRow.Operation_Lok = 2; break;
        //        case OperationLokup.ADDNEW:
        //            filelogRow.Operation_Lok = 3; break;
        //        case OperationLokup.UPDATE:
        //            filelogRow.Operation_Lok = 4; break;
        //        case OperationLokup.DELETE:
        //            filelogRow.Operation_Lok = 5; break;
        //        case OperationLokup.FILE_OPERATION:
        //            filelogRow.Operation_Lok = 6; break;
        //        case OperationLokup.NONE:
        //            filelogRow.Operation_Lok = 7; break;
        //    }

        //    filelogRow.UserId = userId;
        //    filelogRow.BUnitId = bUnitId;
        //    filelogRow.ProcessId = processId;

        //    filelogRow.Dated = DateTime.Now;
        //    filelogRow.Msg = msg;
        //    filelogRow.IsDeleted = false;
        //    filelogRow.ClientIP = clientIP;
        //    dataMgr.ActivityLogs.Add(filelogRow);
        //    //Log the file Activity
        //    //dataMgr.SaveChanges();
        //    return true;

        //}


    }
}