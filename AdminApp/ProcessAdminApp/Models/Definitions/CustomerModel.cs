using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class CustomerModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public CustomerModel() { }
        public CustomerModel(DAL.Customer pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.CustomerNo = pInfo.CustomerNo;
            this.CustomerName = pInfo.CustomerName;
            this.CustomerAcctNo = pInfo.CustomerAcctNo;
            this.CustomerAcctNoSub = pInfo.CustomerAcctNoSub;        
            this.CustomerEmail = pInfo.CustomerEmail;
            this.CustomerAddress = pInfo.CustomerAddress;
            this.CustomerContactNo = pInfo.CustomerContactNo;
            this.CustomerWebsite = pInfo.CustomerWebsite;
            this.CustomerContract = pInfo.CustomerContract;
            this.CustomerLogo = pInfo.CustomerLogo;

            this.CreationDate = pInfo.CreationDate;
            this.ActivateDate = pInfo.ActivateDate;
            this.IsActive = pInfo.IsActive;
            this.UpdateDate = pInfo.UpdateDate;
            this.IsDeleted = pInfo.IsDeleted;
            
            this.CustomerClass = pInfo.CustomerClass;           
            this.CustomerCategory = pInfo.CustomerCategory;
            this.CustomerType = pInfo.CustomerType;

            this.IsClosed = pInfo.IsClosed;
            this.ClosingDate = pInfo.ClosingDate;


        }


        public int Id { get; set; }

        [Required, Display(Name = "Customer No"), StringLength(20, ErrorMessage = "Field cannot be longer than 20 characters.")]
        public string CustomerNo { get; set; }

        [Required, Display(Name = "Customer Name"), StringLength(200, ErrorMessage = "Field cannot be longer than 200 characters.")]
        public string CustomerName { get; set; }

        [Required, Display(Name = "Account No"), StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string CustomerAcctNo { get; set; }

        [Required, Display(Name = "Sub Account No"), StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string CustomerAcctNoSub { get; set; }

        [Display(Name = "Customer Address"), StringLength(200, ErrorMessage = "Field cannot be longer than 200 characters.")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Email"), StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Customer Contact No"), StringLength(200, ErrorMessage = "Field cannot be longer than 200 characters.")]
        public string CustomerContactNo { get; set; }
        [Display(Name = "Customer Website "), StringLength(200, ErrorMessage = "Field cannot be longer than 200 characters.")]
        public string CustomerWebsite { get; set; }

        [Display(Name = "Contract / SLA "), StringLength(50, ErrorMessage = "Field cannot be longer than 200 characters.")]
        public string CustomerContract { get; set; }
        public string CustomerLogo { get; set; }
        public int CustomerClass { get; set; }
        public int CustomerCategory { get; set; }
        public int CustomerType { get; set; }
        public System.DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> ActivateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsClosed { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }



        public string CustomerClassName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> CustomerClassList { get; set; }


        public string CustomerCategoryName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> CustomerCategoryList { get; set; }

        public string CustomerTypeName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> CustomerTypeList { get; set; }




    }
}