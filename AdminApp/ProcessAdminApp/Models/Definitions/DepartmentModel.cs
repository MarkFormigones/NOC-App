using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class DepartmentModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public DepartmentModel() { }
        public DepartmentModel(DAL.Department pInfo)
        {
            // TODO: Complete member initialization
            this.DepartmentId = pInfo.DepartmentId;
            this.DepartmentName = pInfo.DepartmentName;
            this.DepartmentDesc = pInfo.DepartmentDesc;

            this.DepartmentLogo = pInfo.DepartmentLogo;

            this.CompanyId = pInfo.CompanyId;
            this.BUnitId = pInfo.BUnitId;

            this.Dated = pInfo.Dated;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
            this.DepartmentMembers = pInfo.DepartmentMembers;
        }
       
        public int? DepartmentMembers { get; set; }
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string DepartmentName { get; set; }
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string DepartmentDesc { get; set; }
        public string DepartmentLogo { get; set; }
        public int CompanyId { get; set; }
        public int BUnitId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Dated { get; set; }
        public virtual IEnumerable<System.Web.Mvc.SelectListItem> ParnetListData { get; set; }

        //private virtual ICollection<vw_UserDepartments> usersInfoList;


        public IEnumerable<System.Web.Mvc.SelectListItem> OptionList { get; set; }
      
        public IEnumerable<string> SelectedOptionList { get; set; }
        //public virtual IList<vw_UserDepartments> UsersInDepartmentsList { get; set; }
        public virtual IList<vw_userslist> ALLUsersInDepartmentsList { get; set; }

    }
}