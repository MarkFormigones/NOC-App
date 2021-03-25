using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class RequestTransactionModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public RequestTransactionModel() { }
        public RequestTransactionModel(DAL.RequestTransaction pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
this.ProductCodeId              = pInfo.ProductCodeId          ;
this.RequestNumber              = pInfo.RequestNumber          ;
this.SubRequestNumber           = pInfo.SubRequestNumber       ;
this.RequestTypeId              = pInfo.RequestTypeId          ;
this.PartyId                    = pInfo.PartyId                ;
this.AccountNumber              = pInfo.AccountNumber          ;
this.AcctCategoryId             = pInfo.AcctCategoryId         ;
this.ESSegmentId                = pInfo.ESSegmentId            ;
this.RatePlanId                 = pInfo.RatePlanId             ;
this.ContractId                 = pInfo.ContractId             ;
this.ApprovalRequestStatusId    = pInfo.ApprovalRequestStatusId;
this.VariableAmount_MRC         = pInfo.VariableAmount_MRC     ;
this.VariableAmount_OTC         = pInfo.VariableAmount_OTC     ;
this.VariableAmount_QRC         = pInfo.VariableAmount_QRC     ;
this.VariableAmount_YRC         = pInfo.VariableAmount_YRC     ;
this.Created                    = pInfo.Created                ;
this.ChargeDetails              = pInfo.ChargeDetails          ;
this.IsActive                   = pInfo.IsActive               ;
            this.IsDeleted      = pInfo.IsDeleted;



        }


        public int Id { get; set; }
        public int ProductCodeId { get; set; }

        [Required, Display(Name = "Request Number"), StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string RequestNumber { get; set; }

        [Required, Display(Name = "Sub Request Number"), StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string SubRequestNumber { get; set; }
        public int RequestTypeId { get; set; }
        public Nullable<int> PartyId { get; set; }

        [Required, Display(Name = "Account Number"), StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string AccountNumber { get; set; }
        public Nullable<int> AcctCategoryId { get; set; }
        public Nullable<int> ESSegmentId { get; set; }
        public int RatePlanId { get; set; }

        [Display(Name = "Contract Id"), Range(0, int.MaxValue, ErrorMessage = "Please enter valid contract Id number")]
        public Nullable<int> ContractId { get; set; }
        public Nullable<int> ApprovalRequestStatusId { get; set; }

        [Display(Name = "Amount MRC"), StringLength(20, ErrorMessage = "Field cannot be longer than 20 characters.")]
        public string VariableAmount_MRC { get; set; }

        [Display(Name = "Amount OTC"), StringLength(20, ErrorMessage = "Field cannot be longer than 20 characters.")]
        public string VariableAmount_OTC { get; set; }
        
        [Display(Name = "Amount QRC"), StringLength(20, ErrorMessage = "Field cannot be longer than 20 characters.")]
        public string VariableAmount_QRC { get; set; }

        [Display(Name = "Amount YRC"), StringLength(20, ErrorMessage = "Field cannot be longer than 20 characters.")]
        public string VariableAmount_YRC { get; set; }
        public System.DateTime Created { get; set; }

        [Display(Name = "Remarks"), StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string ChargeDetails { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


        public string ProductName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> ProductNameList { get; set; }

        public string RequestTypeName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> RequestTypeList { get; set; }

        public string PartyName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> PartyNameList { get; set; }

        public string AcctCategoryName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AcctCategoryList { get; set; }

        public string ESSegmentName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> ESSegmentList { get; set; }

        public string RatePlanName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> RatePlanList { get; set; }

        public string RatePlanDesc { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> RatePlanDescList { get; set; }

        public string ContractName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> ContractList { get; set; }


        public string ApprovalRequestStatusName { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> ApprovalRequestStatusList { get; set; }

    }
}