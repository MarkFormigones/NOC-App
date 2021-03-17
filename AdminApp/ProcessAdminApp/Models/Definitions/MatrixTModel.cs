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
    public class MatrixTModel
    {
        private MatrixT matrix;
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }
        public enum UnitType { Company = 1, Division = 2, Unit = 3 }
        public MatrixTModel()
        {


        }

        public class TreeModel
        {
            public string name { get; set; }

            public string controller { get; set; }
            public int Id { get; set; }
            public string type { get; set; }
            public string additionalParameters { get; set; }


        }

        public MatrixTModel(MatrixT matrix)
        {
            // TODO: Complete member initialization
            this.MatrixId = matrix.MatrixId;
            this.BUnitId = matrix.BUnitId;
            this.CompanyId = matrix.CompanyId;
            this.MatrixTypeId = matrix.MatrixTypeId;
            this.ProcessId = matrix.ProcessId;

            this.MatrixName = matrix.MatrixName;
            this.DimX = matrix.DimX;
            this.DimY = matrix.DimY;
            this.MatrixTitle = matrix.MatrixTitle;
            this.MatrixDesc = matrix.MatrixDesc;

            Dated = matrix.Dated;
            this.IsActive = matrix.IsActive;
            this.IsDeleted = matrix.IsDeleted;

        }

        public int MatrixId { get; set; }
        public int BUnitId { get; set; }
        public int MatrixTypeId { get; set; }
        public int ProcessId { get; set; }
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string MatrixName { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string MatrixTitle { get; set; }

        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string MatrixDesc { get; set; }
        //private string _BUnitCountryId;
        //[Required]
        //[Display(Name = "Country")]
        //public string BUnitCountryId
        //{
        //    get { if (_BUnitCountryId == null) { return "-1"; } else return _BUnitCountryId; }
        //    set { _BUnitCountryId = value; }
        //}

        [Required]
        public int DimX { get; set; }
        [Required]
        public int DimY { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public System.DateTime Dated { get; set; }


    }
}