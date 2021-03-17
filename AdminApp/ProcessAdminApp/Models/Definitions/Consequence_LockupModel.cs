using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class Consequence_LockupModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public Consequence_LockupModel() { }
        public Consequence_LockupModel(DAL.Consequence_Lockup pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.Name = pInfo.Name;
            this.Value = pInfo.Value;
            this.CompanyId = pInfo.CompanyId;
            this.ProcessId = pInfo.ProcessId;
            this.BUnitId = pInfo.BUnitId;
            this.DivisionId = pInfo.DivisionId;
            this.IsDeleted = pInfo.IsDeleted;
            this.IsActive = pInfo.IsActive;
            this.Dated = pInfo.Dated;

    }
        public int Id { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string Value { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int ProcessId { get; set; }
        [Required]
        public int BUnitId { get; set; }
        [Required]
        public int DivisionId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public System.DateTime Dated { get; set; }
      

    }
}