using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class CommandLockupModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public CommandLockupModel() { }
        public CommandLockupModel(DAL.CommandLockup pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.value = pInfo.value;
            this.Command = pInfo.Command;
            this.IsDeleted = pInfo.IsDeleted;
            this.Comment = pInfo.Comment;
            this.ComamandIcon = pInfo.ComamandIcon;
            this.CommandClass = pInfo.CommandClass;
            this.IsActive = pInfo.IsActive;

    }

        public int Id { get; set; }
        public Nullable<int> value { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Command { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Comment { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string ComamandIcon { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string CommandClass { get; set; }
        public bool IsActive { get; set; }
    }
}