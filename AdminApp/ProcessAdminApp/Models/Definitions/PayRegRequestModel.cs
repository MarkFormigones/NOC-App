using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class PayRegRequestModel
    {
        public PayRegRequestModel() { }

        public PayRegRequest Registration { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
        
        [JsonIgnore]
        public int NocId { get; set; }

        [JsonIgnore]
        public DateTime TransDateTime { get; set; }

    }

    public class PayRegRequest
    {
        public string Customer { get; set; }
        public string Store { get; set; }
        public string Terminal { get; set; }
        public string Channel { get; set; }

        public string Amount { get; set; }

        public string Currency { get; set; }

        public string OrderID { get; set; }

        public string OrderName { get; set; }

        public string OrderInfo { get; set; }

        public string TransactionHint { get; set; }

        public string ReturnPath { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }

}