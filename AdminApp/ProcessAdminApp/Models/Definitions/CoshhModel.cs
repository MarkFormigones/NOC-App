using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class CoshhModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        //public bool UseDefaultLogo { get; set; }

        public CoshhModel() { }
        public CoshhModel(DAL.COSHH pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.Location = pInfo.Location;
            this.Area = pInfo.Area;
            this.DateSubmit = pInfo.DateSubmit;
            this.PermitNo = pInfo.PermitNo;
            this.Description = pInfo.Description;
            this.Employee_risk = pInfo.Employee_risk;
            this.Contractors_risk = pInfo.Contractors_risk;
            this.Public_risk = pInfo.Public_risk;
            this.Substance = pInfo.Substance;
            this.WEL = pInfo.WEL;
            this.Hazards = pInfo.Hazards;
            this.ControlMeasures = pInfo.ControlMeasures;
            this.Monitor_required = pInfo.Monitor_required;
            this.FirstAidMeasure = pInfo.FirstAidMeasure;
            this.Storage = pInfo.Storage;
            this.IsControlled = pInfo.IsControlled;
            this.RiskRating = pInfo.RiskRating;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Location")]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string Location { get; set; }
        public string Area { get; set; }
        public System.DateTime DateSubmit { get; set; }
        public string PermitNo { get; set; }
        public string Description { get; set; }
        public bool Employee_risk { get; set; }
        public bool Contractors_risk { get; set; }
        public bool Public_risk { get; set; }
        public string Substance { get; set; }
        public string WEL { get; set; }
        public string Hazards { get; set; }
        public string ControlMeasures { get; set; }
        public bool Monitor_required { get; set; }
        public string FirstAidMeasure { get; set; }
        public string Storage { get; set; }
        public bool IsControlled { get; set; }
        public int RiskRating { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IEnumerable<System.Web.Mvc.SelectListItem> RiskRatingListData { get; set; }
    }
}