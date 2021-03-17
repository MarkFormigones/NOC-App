using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class ProcessFieldLookupModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public ProcessFieldLookupModel() { }
        public ProcessFieldLookupModel(DAL.ProcessFieldLookup pInfo)
        {
            // TODO: Complete member initialization
            this.ColumnId = pInfo.ColumnId;
            this.FieldName = pInfo.FieldName;
            this.TableId = pInfo.TableId;
            this.TableName = pInfo.TableName;
            this.IsPublic = pInfo.IsPublic;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
        }
        [Required]
        public int ColumnId { get; set; }
        [Required]
        [Display(Name = "Field Name")]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string FieldName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Field cannot be longer than 10 characters.")]
        public string TableId { get; set; }
        [Required]
        [Display(Name = "Table Name")]
        [StringLength(50, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string TableName { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        

    }
}