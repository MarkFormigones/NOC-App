using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class OperationLockupModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public OperationLockupModel() { }
        public OperationLockupModel(DAL.OperationLockup pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.value = pInfo.value;
            this.Command = pInfo.Command;
            this.Comment = pInfo.Comment;
            this.IsDeleted = pInfo.IsDeleted;
            this.IsActive = pInfo.IsActive;
        }
        public int Id { get; set; }
        [Required]
        public Nullable<int> value { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        [Required]
        public string Command { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Comment { get; set; }
        public bool IsActive { get; set; }       

    }
}