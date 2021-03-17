using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class ProcessMasterExtModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        //public bool UseDefaultLogo { get; set; }

        public ProcessMasterExtModel() { }
        public ProcessMasterExtModel(DAL.ProcessMasterExt pInfo)
        {
            // TODO: Complete member initialization
            this.ExtendId = pInfo.ExtendId;
            this.Coshh_EmpRisk = pInfo.Coshh_EmpRisk;
            this.Coshh_ConRisk = pInfo.Coshh_ConRisk;
            this.Coshh_PubRisk = pInfo.Coshh_PubRisk;
            this.Coshh_Substance = pInfo.Coshh_Substance;
            this.Coshh_Wels = pInfo.Coshh_Wels;
            this.Coshh_Hazards = pInfo.Coshh_Hazards;
            this.coshh_ConMeasures = pInfo.coshh_ConMeasures;
            this.Coshh_isMonitored = pInfo.Coshh_isMonitored;
            this.Coshh_FirstAid = pInfo.Coshh_FirstAid;
            this.Storage = pInfo.Storage;
            this.Coshh_isControlled = pInfo.Coshh_isControlled;
            this.Coshh_Rating = pInfo.Coshh_Rating;
            this.Chem_Qty = pInfo.Chem_Qty;
            this.Chem_isDischarged = pInfo.Chem_isDischarged;
            this.Chem_FormMaterial = pInfo.Chem_FormMaterial;
            this.Chem_TypeStorage = pInfo.Chem_TypeStorage;
            this.Chem_DisposeContainer = pInfo.Chem_DisposeContainer;
            this.Chem_DisposeChemical = pInfo.Chem_DisposeChemical;
            this.Chem_Use = pInfo.Chem_Use;
            this.Chem_DateReceipt = pInfo.Chem_DateReceipt;
            this.Chem_isRegulate = pInfo.Chem_isRegulate;
            this.Coshh_isCompleted = pInfo.Coshh_isCompleted;
            this.Chem_Impact = pInfo.Chem_Impact;
            this.Chem_isNew = pInfo.Chem_isNew;
            this.Chem_isStock = pInfo.Chem_isStock;
            this.Chem_StockOrder = pInfo.Chem_StockOrder;
            this.Chem_StockMaxInven = pInfo.Chem_StockMaxInven;
            this.Chem_StockUsage = pInfo.Chem_StockUsage;
            this.Chem_isNonStock = pInfo.Chem_isNonStock;
            this.Chem_NonStockOrder = pInfo.Chem_NonStockOrder;
            this.Chem_isReplacement = pInfo.Chem_isReplacement;
            this.Chem_ReplaceDetails = pInfo.Chem_ReplaceDetails;
            this.Chem_isTemp = pInfo.Chem_isTemp;
            this.Chem_TempOrder = pInfo.Chem_TempOrder;
            this.Chem_ExtraInfo = pInfo.Chem_ExtraInfo;
            this.ProcessMasterId = pInfo.ProcessMasterId;
            this.Chem_EquipArea = pInfo.Chem_EquipArea;

            //region added by mark March 3, 2020
        this.Coshh_isHazardGas = pInfo.Coshh_isHazardGas;
        this.Coshh_isHazardVapour       = pInfo.Coshh_isHazardVapour    ;
        this.Coshh_isHazardMist         = pInfo.Coshh_isHazardMist      ;
        this.Coshh_isHazardFume         = pInfo.Coshh_isHazardFume      ;
        this.Coshh_isHazardDust         = pInfo.Coshh_isHazardDust      ;
        this.Coshh_isHazardLiquid       = pInfo.Coshh_isHazardLiquid    ;
        this.Coshh_isHazardSolid        = pInfo.Coshh_isHazardSolid     ;
        this.Coshh_isHazardOther        = pInfo.Coshh_isHazardOther     ;
        this.Coshh_HazardOtherText      = pInfo.Coshh_HazardOtherText   ;
        this.Coshh_isExposeInhalation   = pInfo.Coshh_isExposeInhalation;
        this.Coshh_isExposeSkin         = pInfo.Coshh_isExposeSkin      ;
        this.Coshh_isExposeEyes         = pInfo.Coshh_isExposeEyes      ;
        this.Coshh_isExposeIngestion    = pInfo.Coshh_isExposeIngestion ;
        this.Coshh_isExposeOther        = pInfo.Coshh_isExposeOther     ;
        this.Coshh_ExposeOtherText      = pInfo.Coshh_ExposeOtherText   ;
        this.Coshh_isDisposalHazard     = pInfo.Coshh_isDisposalHazard  ;
        this.Coshh_isDisposalSkip       = pInfo.Coshh_isDisposalSkip    ;
        this.Coshh_isDisposalMgtArea    = pInfo.Coshh_isDisposalMgtArea ;
        this.Coshh_isDisposalSupplier   = pInfo.Coshh_isDisposalSupplier;
        this.Coshh_isDisposalOther      = pInfo.Coshh_isDisposalOther   ;
        this.Coshh_DisposalOtherText    = pInfo.Coshh_DisposalOtherText;
            //endregion
        }

        public int ExtendId { get; set; }
        public bool Coshh_EmpRisk { get; set; }
        public bool Coshh_ConRisk { get; set; }
        public bool Coshh_PubRisk { get; set; }
        [Display(Name = "Name the substance involved")]
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string Coshh_Substance { get; set; }
        [Display(Name = "Coshh WEL's")]
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string Coshh_Wels { get; set; }
        public string Coshh_Hazards { get; set; }
        public string coshh_ConMeasures { get; set; }
        public bool Coshh_isMonitored { get; set; }
        public string Coshh_FirstAid { get; set; }
        public string Storage { get; set; }
        public bool Coshh_isControlled { get; set; }
        public int Coshh_Rating { get; set; }
        public int Chem_Qty { get; set; }
        public bool Chem_isDischarged { get; set; }
        public string Chem_FormMaterial { get; set; }
        public string Chem_TypeStorage { get; set; }
        public string Chem_DisposeContainer { get; set; }
        public string Chem_DisposeChemical { get; set; }
        public string Chem_Use { get; set; }
        public System.DateTime Chem_DateReceipt { get; set; }
        public bool Chem_isRegulate { get; set; }
        public bool Coshh_isCompleted { get; set; }
        public string Chem_Impact { get; set; }
        public bool Chem_isNew { get; set; }
        public bool Chem_isStock { get; set; }
        public string Chem_StockOrder { get; set; }
        public string Chem_StockMaxInven { get; set; }
        public string Chem_StockUsage { get; set; }
        public bool Chem_isNonStock { get; set; }
        public string Chem_NonStockOrder { get; set; }
        public bool Chem_isReplacement { get; set; }
        public string Chem_ReplaceDetails { get; set; }
        public bool Chem_isTemp { get; set; }
        public string Chem_TempOrder { get; set; }
        public string Chem_ExtraInfo { get; set; }
        public int ProcessMasterId { get; set; }
        public string Chem_EquipArea { get; set; }
        public virtual IEnumerable<System.Web.Mvc.SelectListItem> RiskRatingListData { get; set; }

        //region added by mark March 03, 2020
        public bool Coshh_isHazardGas { get; set; }
        public bool Coshh_isHazardVapour { get; set; }
        public bool Coshh_isHazardMist { get; set; }
        public bool Coshh_isHazardFume { get; set; }
        public bool Coshh_isHazardDust { get; set; }
        public bool Coshh_isHazardLiquid { get; set; }
        public bool Coshh_isHazardSolid { get; set; }
        public bool Coshh_isHazardOther { get; set; }
        public string Coshh_HazardOtherText { get; set; }
        public bool Coshh_isExposeInhalation { get; set; }
        public bool Coshh_isExposeSkin { get; set; }
        public bool Coshh_isExposeEyes { get; set; }
        public bool Coshh_isExposeIngestion { get; set; }
        public bool Coshh_isExposeOther { get; set; }
        public string Coshh_ExposeOtherText { get; set; }
        public bool Coshh_isDisposalHazard { get; set; }
        public bool Coshh_isDisposalSkip { get; set; }
        public bool Coshh_isDisposalMgtArea { get; set; }
        public bool Coshh_isDisposalSupplier { get; set; }
        public bool Coshh_isDisposalOther { get; set; }
        public string Coshh_DisposalOtherText { get; set; }
        //end region

    }
}