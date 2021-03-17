using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessAdminApp.Filters
{
    public class HandleExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            var gClass = new   DAL.General();

            gClass.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, filterContext.Exception.Message, -1, -1, -1, -1, -1, -1, -1, "", -1,-1);
            base.OnException(filterContext);
        }


    }
}