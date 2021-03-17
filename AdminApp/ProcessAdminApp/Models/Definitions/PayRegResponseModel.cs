using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{

    public class PayRegResponseModel
    {
        public PayRegResponseModel() { }

        public PaymentResponse Transaction { get; set; }


    }

    public class PaymentResponse
    {
        public string PaymentPortal { get; set; }

        public string PaymentPage { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseClass { get; set; }

        public string ResponseDescription { get; set; }

        public string ResponseClassDescription { get; set; }

        public string TransactionID { get; set; }

        public PayBalance Balance { get; set; }

        public PayAmount Amount { get; set; }

        public PayFees Fees { get; set; }

        public string Payer { get; set; }

        public string UniqueID { get; set; }
    }


    public class PayBalance
    {
        public string Value { get; set; }
    }

    public class PayAmount
    {
        public string Value { get; set; }
    }

    public class PayFees
    {
        public string Value { get; set; }
    }


}