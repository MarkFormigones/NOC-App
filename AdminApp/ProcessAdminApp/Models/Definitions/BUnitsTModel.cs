using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
  

namespace Hydron.Models.Definitions
{
    public class BUnitsTModel
    {
        private BUnitsT pInfo;
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }
        public enum UnitType { Company = 1, Division = 2, Unit = 3  }
        public BUnitsTModel( )
        {
             

        }

        public class TreeModel
        { public string name { get; set; }

        public string controller { get; set; }
        public int Id { get; set; } 
            public string type { get; set; }
            public string additionalParameters { get; set; }
        
        
        }
        public BUnitsTModel(BUnitsT pInfo)
        {
            // TODO: Complete member initialization
            this.BUnitId = pInfo.BUnitId;
            this.BUnitType = pInfo.BUnitType;

            this.BUnitName = pInfo.BUnitName;
            this.BUnitLogo = pInfo.BUnitLogo;
            this.BUnitAddress1 = pInfo.BUnitAddress1;
            this.BUnitAddress2 = pInfo.BUnitAddress2;
            this.BUnitFax = pInfo.BUnitFax;
            this.BUnitZip = pInfo.BUnitZip;
            this.BUnitTelephone = pInfo.BUnitTelephone;
            this.BUnitPOBOX = pInfo.BUnitPOBOX;
         
            this.BUnitAbout = pInfo.BUnitAbout;
            this.BUnitCountryId = pInfo.BUnitCountryId;
            this.BUnitAccount = pInfo.BUnitAccount;
            this.BUnitParentId = pInfo.BUnitParentId;
            this.BUnitState = pInfo.BUnitState;
            this.BUnitAdminGroup = pInfo.BUnitAdminGroup;
           
            this.BUnitWebsite = pInfo.BUnitWebsite;
            Dated = pInfo.Dated;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
          
        }
      
        public int BUnitId { get; set; }
        public int BUnitType { get; set; }
        
        [Required]
              [Display(Name = "Name")]
              [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string BUnitName { get; set; }
        [Required]
        [DataType(DataType.Url)]
        [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        [Display(Name = "Website")]
        public string BUnitWebsite { get; set; }
        private string _BUnitCountryId;
        [Required]
        [Display(Name = "Country")]
        public string BUnitCountryId
        {
            get {   if (_BUnitCountryId == null) { return "-1"; } else return _BUnitCountryId; }
            set { _BUnitCountryId = value; }
        }


        [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string BUnitAddress1 { get; set; }
        [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string BUnitAddress2 { get; set; }
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string BUnitAbout { get; set; }
        public string BUnitLogo { get; set; }
        [StringLength(25, ErrorMessage = "Field cannot be longer than 25 characters.")]
        public string BUnitTelephone { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true,  DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public System.DateTime Dated { get; set; }
       
        public virtual IEnumerable<System.Web.Mvc.SelectListItem> CountryListData { get; set; }
        public virtual IEnumerable<System.Web.Mvc.SelectListItem> ParnetListData { get; set; }

        [StringLength(25, ErrorMessage = "Field cannot be longer than 25 characters.")]
        public string BUnitZip { get; set; }
        [StringLength(25, ErrorMessage = "Field cannot be longer than 25 characters.")]
        public string BUnitFax { get; set; }


        public int BUnitAccount { get; set; }

        public string BUnitState { get; set; }

        public int? BUnitAdminGroup { get; set; }

        public int BUnitParentId { get; set; }
        [StringLength(25, ErrorMessage = "Field cannot be longer than 25 characters.")]
        public string BUnitPOBOX { get; set; }

        public TreeModel treeModelJason { get; set; }
    }
}