using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class PayFinalRequestModel
    {
        public PayFinalRequestModel() { }

        public PayFinalRequest Finalization { get; set; }

           
    }

    public class PayFinalRequest
    {
        public string TransactionID { get; set; }
        public string Customer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
   
    }

}