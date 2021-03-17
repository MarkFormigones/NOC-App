using Hydron.Helpers;
using Hydron.Models.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Hydron.Views.Payment
{
    enum Status
    {
        INITIAL = -1,
        SUCCESS = 0,
        NOT_SUFFIENT_FUND = 51,
        DO_NOT_HONOR = 5,
        ISSUER_SWITCH_INOPERATE = 91
    }


    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index(int? status = -1)
        {
            ViewBag.Status = status;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(PayRegRequestModel model)
        {
           

            //this is a registration api
            PayRegRequest item = new PayRegRequest();
            item.Customer = "Demo Merchant";
            item.Currency = "AED";
            item.ReturnPath = "https://localhost:44379/Payment/Finalization";
            item.TransactionHint = "CPT:Y;VCC:Y;";
            item.OrderID = "7210055701315195";
            item.Store = "0000";
            item.Terminal = "0000";
            item.Channel = "Web";
            item.Amount = "94.50";
            item.OrderName = "NOC Certification";
            item.UserName = "Demo_fY9c";
            item.Password = "Comtrust@20182018";


            PayRegRequestModel mod = new PayRegRequestModel();
            mod.Registration = item;
            mod.NocId = -1;
            mod.UserId = -1;
            mod.TransDateTime = DateTime.Now;


            PayRegResponseModel response = null;

            if (ModelState.IsValid)
            {

                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        HttpHelper httpHelper = new HttpHelper();
                        response = await httpHelper.postPayRegisterAsync(mod).ConfigureAwait(false);
                        transaction.Complete();

                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        //base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
                        //base.SetOperationCompleted("error", "Operation", "failed");
                        //base.SetInnerOperationCompleted("error", "Operation", e.Message);
                        //ModelState.AddModelError("error", e.Message);
                    }


                }


            }

            TempData["ResponseModel"] = response;
          
            return RedirectToAction("CheckOut", "Payment");
        }


        public ActionResult CheckOut()
        {
            PayRegResponseModel model = (PayRegResponseModel)TempData["ResponseModel"];
            Response.Cookies["TransactionID"].Value = model.Transaction.TransactionID;

            return View(model);
        }


        public async Task<ActionResult> Finalization()
        {
            string TransactionID = Request.Cookies["TransactionID"].Value;


            PayFinalRequest item = new PayFinalRequest();
            item.TransactionID = TransactionID;
            item.Customer = "Demo Merchant";
            item.UserName = "Demo_fY9c";
            item.Password = "Comtrust@20182018";

            PayFinalRequestModel mod = new PayFinalRequestModel();
            mod.Finalization = item;


            PayFinalResponseModel response = null;

            if (ModelState.IsValid)
            {

                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        HttpHelper httpHelper = new HttpHelper();
                        response = await httpHelper.postPayFinalAsync(mod).ConfigureAwait(false);
                        transaction.Complete();

                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        //base.Logger(DAL.General.ActivityLokup.EXCEPTION, DAL.General.OperationLokup.NONE, DAL.General.ActionCommandReq.Default, logMsg, tempId, -1, -1, -1, -1, -1, -1, -1, -1);
                        //base.SetOperationCompleted("error", "Operation", "failed");
                        //base.SetInnerOperationCompleted("error", "Operation", e.Message);
                        //ModelState.AddModelError("error", e.Message);
                    }


                }


            }

            TempData["ResponseModel"] = response;
            return RedirectToAction("Index", "Payment", new { status = int.Parse(response.Transaction.ResponseCode)});
        }


    }
}