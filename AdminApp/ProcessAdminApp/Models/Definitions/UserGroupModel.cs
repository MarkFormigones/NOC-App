using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class UserGroupModel
    {
        public UserGroupModel() { }
        public UserGroupModel(DAL.UserGroup pInfo)
        {
            // TODO: Complete member initialization
            this.GroupId = pInfo.GroupId;
            this.GroupName = pInfo.GroupName;

            this.GroupDesc = pInfo.GroupDesc;

            this.GroupLogo = pInfo.GroupLogo;
            this.CompanyId = pInfo.CompanyId;

            this.Dated = pInfo.Dated;
            this.IsActive = pInfo.IsActive;
            this.IsDeleted = pInfo.IsDeleted;
            this.GroupMembers = pInfo.GroupMembers;

          

        }
        public enum GetListType { Grid = 1, Rows = 2, Mix = 3 }
        public int? ListType { get; set; }
        public int? GroupMembers { get; set; }
        public string ReadEdit { get; set; }
        public int GroupId { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Field cannot be longer than 100 characters.")]
        public string GroupName { get; set; }
        [StringLength(500, ErrorMessage = "Field cannot be longer than 500 characters.")]
        public string GroupDesc { get; set; }
        public string GroupLogo { get; set; }
        public Nullable<int> AccountId { get; set; }
        public int CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Dated { get; set; }
        public virtual IEnumerable<System.Web.Mvc.SelectListItem> ParnetListData { get; set; }

        //private virtual ICollection<vw_UserGroups> usersInfoList;


        public IEnumerable<System.Web.Mvc.SelectListItem> OptionList { get; set; }
      
        public IEnumerable<string> SelectedOptionList { get; set; }
        public virtual IList<vw_UserGroups> UsersInGroupsList { get; set; }
        public virtual IList<vw_userslist> ALLUsersInGroupsList { get; set; }

    }
}