using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{

    public class PayFinalResponseModel
    {
        public PayFinalResponseModel() { }

        public FinalResponse Transaction { get; set; }


    }

    public class FinalResponse
    {
        public string ResponseCode { get; set; }

        public string ResponseClass { get; set; }

        public string ResponseDescription { get; set; }

        public string ResponseClassDescription { get; set; }

        public string Language { get; set; }

        public string ApprovalCode { get; set; }

        public string Account { get; set; }

        public FinalBalance Balance { get; set; }

        public string OrderID { get; set; }

        public FinalAmount Amount { get; set; }

        public FinalFees Fees { get; set; }

        public string CardNumber { get; set; }

        public FinalPayer Payer { get; set; }

        public string CardToken { get; set; }

        public string CardBrand { get; set; }

        public string UniqueID { get; set; }


    }


    public class FinalBalance
    {
        public string Value { get; set; }
    }

    public class FinalAmount
    {
        public string Value { get; set; }
    }

    public class FinalFees
    {
        public string Value { get; set; }
    }

    public class FinalPayer
    {
        public string Information { get; set; }
    }


}