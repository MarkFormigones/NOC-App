using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class ProcessFieldSetupModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public ProcessFieldSetupModel() { }
        public ProcessFieldSetupModel(DAL.ProcessFieldSetup pInfo)
        {
            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.ColumnId = pInfo.ColumnId;
            this.ProcessId = pInfo.ProcessId;
            this.TableId = pInfo.TableId;
            this.ShowHide = pInfo.ShowHide;
            this.DefaultValue = pInfo.DefaultValue;
            this.ManOptional = pInfo.ManOptional;
            this.DefaultInt = pInfo.DefaultInt;
            this.ExtraText = pInfo.ExtraText;
            this.ExtraInt = pInfo.ExtraInt;
            this.Enabled = pInfo.Enabled;
            this.FieldNameListId = pInfo.ColumnId;


        }
        [Required]
        public int Id { get; set; }
        [Required]
        public int ColumnId { get; set; }
        [Required]
        public int ProcessId { get; set; }
        [Required]
       
        public int TableId { get; set; }
        [Required]
        public bool ShowHide { get; set; }
        [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string DefaultValue { get; set; }

        public bool ManOptional { get; set; }
        [StringLength(10, ErrorMessage = "Field cannot be longer than 10 characters.")]
        public string DefaultInt { get; set; }
        public string ExtraText { get; set; }
        [StringLength(10, ErrorMessage = "Field cannot be longer than 10 characters.")]
        public string ExtraInt { get; set; }
        public bool Enabled { get; set; }

        public virtual IEnumerable<System.Web.Mvc.SelectListItem> FieldNameListData { get; internal set; }
        public virtual int FieldNameListId { get; set; }
        public string FieldName { get;  set; }
    }
}