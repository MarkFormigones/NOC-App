using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class NotificationPriority_LockupModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public NotificationPriority_LockupModel() { }
        public NotificationPriority_LockupModel(DAL.NotificationPriority_Lockup pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.Prioirity = pInfo.Prioirity;
            this.Icon = pInfo.Icon;
            this.IconClass = pInfo.IconClass;
            this.EscalationInterval = pInfo.EscalationInterval;
            this.ReminerFrequency = pInfo.ReminerFrequency;
            this.Ex1 = pInfo.Ex1;
            this.Ex2 = pInfo.Ex2;

            this.LifetimeInDays = pInfo.LifetimeInDays;
            this.ExtensionsAllowed = pInfo.ExtensionsAllowed;
            this.Description = pInfo.Description;

            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;

        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Prioirity { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Icon { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string IconClass { get; set; }
        public Nullable<int> EscalationInterval { get; set; }
        public Nullable<int> ReminerFrequency { get; set; }
        public Nullable<int> Ex1 { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Ex2 { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Nullable<int> LifetimeInDays { get; set; }
        public Nullable<int> ExtensionsAllowed { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Description { get; set; }



    }
}