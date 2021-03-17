using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class ProcessCategoryModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public ProcessCategoryModel() { }
        public ProcessCategoryModel(DAL.ProcessCategory pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.Name = pInfo.Name;
            this.Value = pInfo.Value;
            this.CompanyId = pInfo.CompanyId;
            this.ProcessId = pInfo.ProcessId;
            this.BUnitId = pInfo.BUnitId;
            this.DivisionId = pInfo.DivisionId;

            this.ParentId = pInfo.ParentId;
            this.IsDeleted = pInfo.IsDeleted;
            this.IsActive = pInfo.IsActive;
            this.Dated = pInfo.Dated;
            this.TypeId = pInfo.TypeId;

            if(this.ParentId == 0)
            {
                this.ParentListId = pInfo.Id;
            }
            else
            {
                this.ParentListId = pInfo.ParentId;
            }

    }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Value { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int ProcessId { get; set; }
        [Required]
        public int BUnitId { get; set; }
        [Required]
        public int DivisionId { get; set; }
        [Required]
        public int ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Dated { get; set; }
        public Nullable<int> TypeId { get; set; }
        public virtual IEnumerable<System.Web.Mvc.SelectListItem> ParentListData { get;  internal set; }
        public virtual int ParentListId { get; set; }

        // public IEnumerable<SelectListItem> ParentListData { get; internal set; }

    }
}