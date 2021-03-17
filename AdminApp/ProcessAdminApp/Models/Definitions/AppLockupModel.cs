using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    //public class AppLockupModel
    //{
    //    public int FromActionType { get; set; }
    //    public string ReadEdit { get; set; }
    //    public bool UseDefaultLogo { get; set; }

    //    public AppLockupModel() { }
    //    public AppLockupModel(DAL.AppLockup pInfo)
    //    {
    //        // TODO: Complete member initialization
    //        this.Id = pInfo.Id;
    //        this.Name = pInfo.Name;
    //        this.CategoryId = pInfo.CategoryId;
    //        this.IsDeleted = pInfo.IsDeleted;
    //        this.Dated = pInfo.Dated;
    //        this.Desc = pInfo.Desc;
    //        this.Icon = pInfo.Icon;
    //        this.IconClass = pInfo.IconClass;
    //        this.GeneralClass = pInfo.GeneralClass;
    //        this.IsActive = pInfo.IsActive;

    //    }

    //    public int Id { get; set; }
    //    [Required]
    //    [Display(Name = "Name")]
    //    [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
    //    public string Name { get; set; }
    //    public Nullable<int> CategoryId { get; set; }
    //    public bool IsDeleted { get; set; }
    //    public Nullable<System.DateTime> Dated { get; set; }
    //    [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
    //    public string Desc { get; set; }
    //    [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
    //    public string Icon { get; set; }
    //    [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
    //    public string IconClass { get; set; }
    //    [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
    //    public string GeneralClass { get; set; }
    //    public bool IsActive { get; set; }

    //}
}