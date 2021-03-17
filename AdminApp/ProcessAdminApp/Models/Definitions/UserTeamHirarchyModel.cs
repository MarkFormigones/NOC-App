using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hydron.Models.Definitions
{ 
    public class UserTeamHirarchyModel
    {
        public UserTeamHirarchyModel(DAL.Vw_UserInHirarchy pInfo)
        {

            this.Dated = pInfo.Dated;
            this.LeaderId = pInfo.LeaderId;
            this.LeaderName = pInfo.LeaderName;
            this.LeaderFName = pInfo.LeaderFName;
            this.LeaderLName = pInfo.LeaderLName;
            this.LeaderEmail = pInfo.LeaderEmail;
            this.LeaderPic = pInfo.LeaderPic;
            this.LeaderIsActive = pInfo.LeaderIsActive;
            this.Id = pInfo.Id;
            this.UserGUID = pInfo.UserGUID;
            this.LeaderGUID = pInfo.LeaderGUID;
            this.CompanyId = pInfo.CompanyId;
            this.DevId = pInfo.DevId;
            this.BUnitId = pInfo.BUnitId;
            this.WorkspaceId = pInfo.WorkspaceId;
            this.ProcessId = pInfo.ProcessId;
            this.ProcessTempateId = pInfo.ProcessTempateId;
            this.ExtraId = pInfo.ExtraId;
            this.UserLevelId = pInfo.UserLevelId;
            this.ExtraText = pInfo.ExtraText;
            this.IsActiveHirarchy = pInfo.IsActiveHirarchy;
            this.IsDeleted = pInfo.IsDeleted;             
            this.GroupId = pInfo.GroupId;
    }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool UserDeleted { get; set; }
        public string UserFName { get; set; }
        public Nullable<System.DateTime> UserDOB { get; set; }
        public string UserEmail { get; set; }
        public string UserPic { get; set; }
        public bool IsActive { get; set; }
        public string UserLastName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<System.DateTime> LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        public Nullable<System.DateTime> PasswordChangedDate { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
        public Nullable<int> LeaderId { get; set; }
        public string LeaderName { get; set; }
        public string LeaderFName { get; set; }
        public string LeaderLName { get; set; }
        public string LeaderEmail { get; set; }
        public string LeaderPic { get; set; }
        public bool LeaderIsActive { get; set; }
        public int Id { get; set; }
        public string UserGUID { get; set; }
        public string LeaderGUID { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> DevId { get; set; }
        public Nullable<int> BUnitId { get; set; }
        public Nullable<int> WorkspaceId { get; set; }
        public Nullable<int> ProcessId { get; set; }
        public Nullable<int> ProcessTempateId { get; set; }
        public Nullable<int> ExtraId { get; set; }
        public Nullable<int> UserLevelId { get; set; }
        public string ExtraText { get; set; }
        public Nullable<bool> IsActiveHirarchy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> GroupId { get; set; }

        public string GroupDesc { get; set; }
        public int? ListType { get; set; }
        public int GroupCount { get; set; }
    }
}