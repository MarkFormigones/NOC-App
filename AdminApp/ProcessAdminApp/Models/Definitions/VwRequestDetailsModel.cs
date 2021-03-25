using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class VwRequestDetailsModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public VwRequestDetailsModel() { }
        public VwRequestDetailsModel(DAL.Vw_RequestDetails pInfo)
        {
            // TODO: Complete member initialization
        this.Id = pInfo.Id;
        this.ProductCode = pInfo.ProductCode;
        this.RequestNumber = pInfo.RequestNumber;
        this.SubRequestNumber = pInfo.SubRequestNumber;
        this.SubRequestType = pInfo.SubRequestType;
        this.PartyName = pInfo.PartyName;
        this.AccountNumber = pInfo.AccountNumber;
        this.AccountCategory = pInfo.AccountCategory;
        this.ESSubSegment = pInfo.ESSubSegment;
        this.RatePlan = pInfo.RatePlan;
        this.RatePlanDescription = pInfo.RatePlanDescription;
        this.VariableAmount_MRC = pInfo.VariableAmount_MRC;
        this.VariableAmount_OTC = pInfo.VariableAmount_OTC;
        this.VariableAmount_QRC = pInfo.VariableAmount_QRC;
        this.VariableAmount_YRC = pInfo.VariableAmount_YRC;
        this.ContractPeriod = pInfo.ContractPeriod;
        this.ChargeDetails = pInfo.ChargeDetails;
        this.ApprovalRequestStatusName = pInfo.ApprovalRequestStatusName;
        this.CBCMStatus = pInfo.CBCMStatus;
        this.Created = pInfo.Created;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;



        }
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string RequestNumber { get; set; }
        public string SubRequestNumber { get; set; }
        public string SubRequestType { get; set; }
        public string PartyName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountCategory { get; set; }
        public string ESSubSegment { get; set; }
        public string RatePlan { get; set; }
        public string RatePlanDescription { get; set; }
        public string VariableAmount_MRC { get; set; }
        public string VariableAmount_OTC { get; set; }
        public string VariableAmount_QRC { get; set; }
        public string VariableAmount_YRC { get; set; }
        public string ContractPeriod { get; set; }
        public string ChargeDetails { get; set; }
        public string ApprovalRequestStatusName { get; set; }
        public string CBCMStatus { get; set; }
        public System.DateTime Created { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


    }
}