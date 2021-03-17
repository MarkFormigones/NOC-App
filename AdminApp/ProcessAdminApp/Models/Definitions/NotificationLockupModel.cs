using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class NotificationLockupModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public NotificationLockupModel() { }
        public NotificationLockupModel(DAL.NotificationLockup pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.Name = pInfo.Name;
            this.Icon = pInfo.Icon;
            this.IconClass = pInfo.IconClass;
            this.Tag = pInfo.Tag;
            this.TypeId = pInfo.TypeId;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
            this.Dated = pInfo.Dated;
            this.IsEscalatable = pInfo.IsEscalatable;
            this.IsMentionDueDate = pInfo.IsMentionDueDate;
            this.RedirectController = pInfo.RedirectController;
            this.RedirectAction = pInfo.RedirectAction;
            this.RedirectExtraParam = pInfo.RedirectExtraParam;
            this.RedirectExParam1 = pInfo.RedirectExParam1;
            this.RedirectExParam2 = pInfo.RedirectExParam2;
            this.RedirectExParam3 = pInfo.RedirectExParam3;
            this.RedirectExParamDefault = pInfo.RedirectExParamDefault;

    }

        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Icon { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string IconClass { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Tag { get; set; }
        public Nullable<int> TypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
        public bool IsEscalatable { get; set; }
        public bool IsMentionDueDate { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string RedirectController { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string RedirectAction { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string RedirectExtraParam { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string RedirectExParam1 { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string RedirectExParam2 { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string RedirectExParam3 { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string RedirectExParamDefault { get; set; }   
    }
}