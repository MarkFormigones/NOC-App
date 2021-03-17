using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class FBMessagingModel
    {
        public FBMessagingModel() { }

        public FBMessage message { get; set; }

           
    }

    public class FBMessage
    {
        public string topic { get; set; }
        public FBNotification notification { get; set; }
    }

    public class FBNotification
    {
        public string body { get; set; }
        public string title { get; set; }
    }

}