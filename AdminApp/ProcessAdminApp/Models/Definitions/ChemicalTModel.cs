using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class ChemicalTModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        //public bool UseDefaultLogo { get; set; }

        public ChemicalTModel() { }
        public ChemicalTModel(DAL.ChemicalT pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.SubstanceName = pInfo.SubstanceName;
            this.Supplier = pInfo.Supplier;
            this.Description = pInfo.Description;
            this.DischargeToEnvironment = pInfo.DischargeToEnvironment;
            this.AreaUse = pInfo.AreaUse;
            this.Quantity = pInfo.Quantity;
            this.MaterialUsed = pInfo.MaterialUsed;
            this.Storage = pInfo.Storage;
            this.DisposalContainers = pInfo.DisposalContainers;
            this.DisposalChemical = pInfo.DisposalChemical;
            this.ChemicalUse = pInfo.ChemicalUse;
            this.DateReciept = pInfo.DateReciept;
            this.StorageLocation = pInfo.StorageLocation;
            this.RegulatedbyLaw = pInfo.RegulatedbyLaw;
            this.COSHH_Completed = pInfo.COSHH_Completed;
            this.HSE_Impact = pInfo.HSE_Impact;
            this.NewChemical = pInfo.NewChemical;
            this.StockItem = pInfo.StockItem;
            this.StockOrderQty = pInfo.StockOrderQty;
            this.MaxInventory = pInfo.MaxInventory;
            this.UsageRate = pInfo.UsageRate;
            this.NonStockItem = pInfo.NonStockItem;
            this.NonStockOrderQty = pInfo.NonStockOrderQty;
            this.ChemicalReplacement = pInfo.ChemicalReplacement;
            this.RepliacementDetails = pInfo.RepliacementDetails;
            this.TempChemical = pInfo.TempChemical;
            this.TempOrderQty = pInfo.TempOrderQty;
            this.AdditionalInfo = pInfo.AdditionalInfo;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Name of Substance")]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 50 characters.")]
        public string SubstanceName { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public bool DischargeToEnvironment { get; set; }
        public string AreaUse { get; set; }
        public int Quantity { get; set; }
        public string MaterialUsed { get; set; }
        public string Storage { get; set; }
        public string DisposalContainers { get; set; }
        public string DisposalChemical { get; set; }
        public string ChemicalUse { get; set; }
        public System.DateTime DateReciept { get; set; }
        public string StorageLocation { get; set; }
        public bool RegulatedbyLaw { get; set; }
        public bool COSHH_Completed { get; set; }
        public string HSE_Impact { get; set; }
        public bool NewChemical { get; set; }
        public bool StockItem { get; set; }
        public int StockOrderQty { get; set; }
        public int MaxInventory { get; set; }
        public int UsageRate { get; set; }
        public bool NonStockItem { get; set; }
        public int NonStockOrderQty { get; set; }
        public bool ChemicalReplacement { get; set; }
        public string RepliacementDetails { get; set; }
        public bool TempChemical { get; set; }
        public int TempOrderQty { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}