using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class ChatModel
    {

        public ChatModel()
        {
        }

        public ChatModel(DAL.Chat pInfo)
        {
            // TODO: Complete member initialization
            this.ChatId = pInfo.ChatId;
            this.Message = pInfo.Message;
            this.DateSent = pInfo.DateSent;

            this.UserId = pInfo.UserId;
            this.CompanyId = pInfo.CompanyId;
            this.BUnitId = pInfo.BUnitId;
            this.ProccessId = pInfo.ProccessId;

            this.Dated = pInfo.Dated;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
        }
       
        // public properities
        public int ChatId { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        public System.DateTime DateSent { get; set; }

        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int BUnitId { get; set; }
        public int ProccessId { get; set; }


        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Dated { get; set; }
        

        //optional
        public string UserFullName { get; set; }
        public string UserPic { get; set; }

    }
}