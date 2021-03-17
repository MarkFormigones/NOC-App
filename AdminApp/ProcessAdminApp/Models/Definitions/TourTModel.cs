using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class TourTModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }
        public bool UseDefaultLogoAr { get; set; }

        public TourTModel() { }
        public TourTModel(DAL.TourT pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.Controller = pInfo.Controller;
            this.Action = pInfo.Action;
            this.TargetOrder = pInfo.TargetOrder;
            this.TargetClass = pInfo.TargetClass;
            this.TargetSubject = pInfo.TargetSubject;
            this.TargetDesc = pInfo.TargetDesc;

            this.TargetIconUrl = pInfo.TargetIconUrl;
            this.TargetUrl = pInfo.TargetUrl;

            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
            this.placement = pInfo.placement;

            this.TargetSubjectAr = pInfo.TargetSubjectAr;
            this.TargetDescAr = pInfo.TargetDescAr;
            this.TargetIconUrlAr = pInfo.TargetIconUrlAr;
            this.BackgroudImg = pInfo.BackgroudImg;
            this.VideoUrl = pInfo.VideoUrl;
            this.ExtraText = pInfo.ExtraText;
            this.ExtraInt1 = pInfo.ExtraInt1;
            this.ExtraInt2 = pInfo.ExtraInt2;
            this.ExtraBit1 = pInfo.ExtraBit1;
            this.ExtraBit2 = pInfo.ExtraBit2;

        }

        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Controller { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Action { get; set; }
        public Nullable<int> TargetOrder { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string TargetClass { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string TargetSubject { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string TargetDesc { get; set; }
        public string TargetIconUrl { get; set; }
        [StringLength(150, ErrorMessage = "Field cannot be longer than 150 characters.")]
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Field cannot be longer than 10 characters.")]
        public string placement { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string TargetSubjectAr { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string TargetDescAr { get; set; }
        public string TargetIconUrlAr { get; set; }

        [StringLength(150, ErrorMessage = "Field cannot be longer than 150 characters.")]
        public string BackgroudImg { get; set; }
        [StringLength(150, ErrorMessage = "Field cannot be longer than 150 characters.")]
        public string VideoUrl { get; set; }
        [StringLength(200, ErrorMessage = "Field cannot be longer than 200 characters.")]
        public string ExtraText { get; set; }
        public Nullable<int> ExtraInt1 { get; set; }
        public Nullable<int> ExtraInt2 { get; set; }
        public bool ExtraBit1 { get; set; }
        public bool ExtraBit2 { get; set; }

    }
}
//[Required]
//[Display(Name = "Name")]
//[StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]