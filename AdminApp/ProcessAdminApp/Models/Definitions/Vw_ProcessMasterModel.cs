using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class vw_ProcessMasterModel
    {
        public int FromActionType { get; set; }
        public string ReadEdit { get; set; }
        public bool UseDefaultLogo { get; set; }

        public vw_ProcessMasterModel() { }
        public vw_ProcessMasterModel(DAL.Vw_ProcessMaster pInfo)
        {

            // TODO: Complete member initialization
            this.Id = pInfo.Id;
            this.Number = pInfo.Number;
            this.Serial = pInfo.Serial;
            this.Title = pInfo.Title;
            this.Description = pInfo.Description;
            this.UserId = pInfo.UserId;
            this.OwnerId = pInfo.OwnerId;
            this.PartnerId = pInfo.PartnerId;
            this.ReviewerId = pInfo.ReviewerId;
            this.RefProcessId = pInfo.RefProcessId;
            this.RefProcessNo = pInfo.RefProcessNo;
            this.LocationTxt = pInfo.LocationTxt;
            this.DateCreated2 = pInfo.DateCreated2;
            this.DateExtension = pInfo.DateExtension;
            this.DateExtension2 = pInfo.DateExtension2;
            this.DateModified = pInfo.DateModified;
            this.DateModified2 = pInfo.DateModified2;
            this.DateClosed = pInfo.DateClosed;
            this.DateClosed2 = pInfo.DateClosed2;
            this.FlowTypeId = pInfo.FlowTypeId;
            this.CategoryId = pInfo.CategoryId;
            this.SubCategoryId = pInfo.SubCategoryId;
            this.ProjectId = pInfo.ProjectId;
            this.StatusPercent = pInfo.StatusPercent;
            this.StatusVal = pInfo.StatusVal;
            this.CompanyId = pInfo.CompanyId;
            this.BUnitId = pInfo.BUnitId;
            this.DivisionId = pInfo.DivisionId;
            this.Mark = pInfo.Mark;
            this.MarkSeq = pInfo.MarkSeq;
            this.MarkLogo = pInfo.MarkLogo;
            this.RootId = pInfo.RootId;
            this.IsTemp = pInfo.IsTemp;
            this.IsKey = pInfo.IsKey;
            this.RenderInLine = pInfo.RenderInLine;
            this.RenderAutoOpen = pInfo.RenderAutoOpen;
            this.IsDeleted = pInfo.IsDeleted;
            this.IsActive = pInfo.IsActive;
            this.IsPosted = pInfo.IsPosted;
            this.Extra1 = pInfo.Extra1;
            this.Extra2 = pInfo.Extra2;
            this.Taggs = pInfo.Taggs;
            this.UserTaggs = pInfo.UserTaggs;
            this.BUnitAbbr = pInfo.BUnitAbbr;
            this.BUnitName = pInfo.BUnitName;
            this.ProjectSerial = pInfo.ProjectSerial;
            this.ProjectTitle = pInfo.ProjectTitle;
            this.ProjectStartDate = pInfo.ProjectStartDate;
            this.ProjectEndtDate = pInfo.ProjectEndtDate;
            this.ProjectCountry = pInfo.ProjectCountry;
            this.UserName = pInfo.UserName;
            this.UserEmail = pInfo.UserEmail;
            this.UserTelephone = pInfo.UserTelephone;
            this.UserMobile = pInfo.UserMobile;
            this.UserPic = pInfo.UserPic;
            this.OwnerName = pInfo.OwnerName;
            this.OwnerEmail = pInfo.OwnerEmail;
            this.OwnerTele = pInfo.OwnerTele;
            this.OwnerMobile = pInfo.OwnerMobile;
            this.OwnerPic = pInfo.OwnerPic;
            this.ReviewerName = pInfo.ReviewerName;
            this.ReviewerEmail = pInfo.ReviewerEmail;
            this.ReviewerTele = pInfo.ReviewerTele;
            this.ReviewerMobile = pInfo.ReviewerMobile;
            this.ReviewerPic = pInfo.ReviewerPic;
            this.ProjectPic = pInfo.ProjectPic;
            this.FlowName = pInfo.FlowName;
            this.FlowAbbr = pInfo.FlowAbbr;
            this.FlowDesc = pInfo.FlowDesc;
            this.FlowTitle = pInfo.FlowTitle;
            this.Logo = pInfo.Logo;
            this.RClassA = pInfo.RClassA;
            this.RClassB = pInfo.RClassB;
            this.RClassC = pInfo.RClassC;
            this.TotalSuccess = pInfo.TotalSuccess;
            this.TotalFailure = pInfo.TotalFailure;
            this.TotalNeurtal = pInfo.TotalNeurtal;
            this.TotalVoteLike = pInfo.TotalVoteLike;
            this.TotalVoteDislike = pInfo.TotalVoteDislike;
            this.TotalActiveActionUnits = pInfo.TotalActiveActionUnits;
            this.TotalActiveActionUnitsCompleted = pInfo.TotalActiveActionUnitsCompleted;
            this.TotalActiveSections = pInfo.TotalActiveSections;
            this.TotalActiveSectionsCompleted = pInfo.TotalActiveSectionsCompleted;
            this.TotalActiveStages = pInfo.TotalActiveStages;
            this.TotalActiveStagesCompleted = pInfo.TotalActiveStagesCompleted;
            this.ProcessStructCoreId = pInfo.ProcessStructCoreId;
            this.DateSubmitted = pInfo.DateSubmitted;
            this.DateSubmitted2 = pInfo.DateSubmitted2;
            this.IsSubmitted = pInfo.IsSubmitted;
            this.DateReviewed = pInfo.DateReviewed;
            this.DateReviewed2 = pInfo.DateReviewed2;
            this.IsReviewed = pInfo.IsReviewed;
            this.QuestionaireMasterId = pInfo.QuestionaireMasterId;
            this.WorkSpaceId = pInfo.WorkSpaceId;
            this.WorkSpaceSerial = pInfo.WorkSpaceSerial;
            this.WorkSpaceTitle = pInfo.WorkSpaceTitle;
            this.GUID = pInfo.GUID;
            this.ExtensionTypeId = pInfo.ExtensionTypeId;
            this.PeriodTypeText = pInfo.PeriodTypeText;
            this.PeriodTypeClass = pInfo.PeriodTypeClass;
            this.PeriodTypeClass2 = pInfo.PeriodTypeClass2;
            this.ProcessFlowGUID = pInfo.ProcessFlowGUID;
            this.BUnitGUID = pInfo.BUnitGUID;
            this.WorkGUID = pInfo.WorkGUID;
            this.ProjGUID = pInfo.ProjGUID;
            this.SystemProcessId = pInfo.SystemProcessId;
            this.DivName = pInfo.DivName;
            this.DivGUID = pInfo.DivGUID;
            this.ComGUID = pInfo.ComGUID;
            this.ComName = pInfo.ComName;
            this.ComAbbr = pInfo.ComAbbr;
            this.DivAbbr = pInfo.DivAbbr;
            this.FlowMasterId = pInfo.FlowMasterId;
            this.Name = pInfo.Name;
            this.QuestionaireUserGroupIdRev = pInfo.QuestionaireUserGroupIdRev;
            this.QuestionaireMasterIdA = pInfo.QuestionaireMasterIdA;
            this.IsQuestionaire = pInfo.IsQuestionaire;
            this.IsQuestionaireRequired = pInfo.IsQuestionaireRequired;
            this.IsQuestionaireCompleted = pInfo.IsQuestionaireCompleted;
            this.QMasterAGIUD = pInfo.QMasterAGIUD;
            this.Qlogo = pInfo.Qlogo;
            this.QTitle = pInfo.QTitle;
            this.FIWNumber = pInfo.FIWNumber;
            this.FIWSerial = pInfo.FIWSerial;
            this.AdminUsersGroupId = pInfo.AdminUsersGroupId;
            this.MemberUsersGroupId = pInfo.MemberUsersGroupId;
            this.AuditorUsersGroupId = pInfo.AuditorUsersGroupId;
            this.BaseUserGroupId = pInfo.BaseUserGroupId;
            this.FlowWFolder = pInfo.FlowWFolder;
            this.ComplexityId = pInfo.ComplexityId;
            this.CostRangeId = pInfo.CostRangeId;
            this.PriorityId = pInfo.PriorityId;
            this.CostBasic = pInfo.CostBasic;
            this.CostFinal = pInfo.CostFinal;
this.EquipmentTag  = pInfo.EquipmentTag ;
this.CategoryName  = pInfo.CategoryName ;
this.SubCategoryName  = pInfo.SubCategoryName ;
this.CostRangeName  = pInfo.CostRangeName ;
this.PriorityName  = pInfo.PriorityName ;
this.PriorityClass  = pInfo.PriorityClass ;
this.Complexityname  = pInfo.Complexityname ;
this.ComplexityClass  = pInfo.ComplexityClass ;
this.LocationName  = pInfo.LocationName ;
this.SubLocationName  = pInfo.SubLocationName ;
this.LocationId  = pInfo.LocationId ;
this.SubLocationId  = pInfo.SubLocationId ;
this.FolderId  = pInfo.FolderId ;
this.UserGUID  = pInfo.UserGUID ;
this.OwerGUID  = pInfo.OwerGUID ;
this.RevGUID  = pInfo.RevGUID ;
this.FStauseText  = pInfo.FStauseText ;
this.FStausClass  = pInfo.FStausClass ;
this.FStausClass2  = pInfo.FStausClass2 ;
this.FinalStatusId  = pInfo.FinalStatusId ;
this.ClosingUserId  = pInfo.ClosingUserId ;
this.FlowStatusId  = pInfo.FlowStatusId ;
this.COwnerId  = pInfo.COwnerId ;
this.COwnerName  = pInfo.COwnerName ;
this.COwnerGUID  = pInfo.COwnerGUID ;
this.COwnerEmail  = pInfo.COwnerEmail ;
this.COwnerTele  = pInfo.COwnerTele ;
this.COwnerMobile  = pInfo.COwnerMobile ;
this.COwnerPic  = pInfo.COwnerPic ;
this.DescriptionRev  = pInfo.DescriptionRev ;
this.Flag1  = pInfo.Flag1 ;
this.Flag2  = pInfo.Flag2 ;
this.Flag3  = pInfo.Flag3 ;
this.Flag4  = pInfo.Flag4 ;
this.Flag5  = pInfo.Flag5 ;
this.Flag6  = pInfo.Flag6 ;
this.Flag7  = pInfo.Flag7 ;
this.Flag8  = pInfo.Flag8 ;
this.Flag9  = pInfo.Flag9 ;
this.Flag10  = pInfo.Flag10 ;
this.Flag11  = pInfo.Flag11 ;
this.Flag12  = pInfo.Flag12 ;
this.Flag13  = pInfo.Flag13 ;
this.Flag14  = pInfo.Flag14 ;
this.Flag15  = pInfo.Flag15 ;
this.ActiveStageId  = pInfo.ActiveStageId ;
this.ActiveSectionId  = pInfo.ActiveSectionId ;
this.ActiveActionUnitId  = pInfo.ActiveActionUnitId ;
this.ActiveStage  = pInfo.ActiveStage ;
this.ActiveSection  = pInfo.ActiveSection ;
this.IsSucessful  = pInfo.IsSucessful ;
this.IsTimerStarted  = pInfo.IsTimerStarted ;
this.TimerStartDatetime  = pInfo.TimerStartDatetime ;
this.TimerStartingUnitId  = pInfo.TimerStartingUnitId ;
this.IsTimerTopped  = pInfo.IsTimerTopped ;
this.TimerStopDatetime  = pInfo.TimerStopDatetime ;
this.TimerStopUnitId  = pInfo.TimerStopUnitId ;
this.LockMasterFeatures  = pInfo.LockMasterFeatures ;
this.LockMasterFlow  = pInfo.LockMasterFlow ;
this.RootProcessId  = pInfo.RootProcessId ;
this.IsSelectionQuestionnaire  = pInfo.IsSelectionQuestionnaire ;
this.ScoringQuestionnaireId  = pInfo.ScoringQuestionnaireId ;
           
        }






        public int Id { get; set; }
        public string Number { get; set; }
        public string Serial { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public Nullable<int> PartnerId { get; set; }
        public Nullable<int> ReviewerId { get; set; }
        public Nullable<int> RefProcessId { get; set; }
        public string RefProcessNo { get; set; }
        public string LocationTxt { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateCreated2 { get; set; }
        public Nullable<System.DateTime> DateExtension { get; set; }
        public Nullable<System.DateTime> DateExtension2 { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<System.DateTime> DateModified2 { get; set; }
        public Nullable<System.DateTime> DateClosed { get; set; }
        public Nullable<System.DateTime> DateClosed2 { get; set; }
        public int FlowTypeId { get; set; }
        public int CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public double StatusPercent { get; set; }
        public int StatusVal { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> BUnitId { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public Nullable<bool> Mark { get; set; }
        public Nullable<int> MarkSeq { get; set; }
        public string MarkLogo { get; set; }
        public string RootId { get; set; }
        public bool IsTemp { get; set; }
        public bool IsKey { get; set; }
        public bool RenderInLine { get; set; }
        public bool RenderAutoOpen { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int IsPosted { get; set; }
        public Nullable<int> Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Taggs { get; set; }
        public string UserTaggs { get; set; }
        public string BUnitAbbr { get; set; }
        public string BUnitName { get; set; }
        public string ProjectSerial { get; set; }
        public string ProjectTitle { get; set; }
        public Nullable<System.DateTime> ProjectStartDate { get; set; }
        public Nullable<System.DateTime> ProjectEndtDate { get; set; }
        public string ProjectCountry { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserTelephone { get; set; }
        public string UserMobile { get; set; }
        public string UserPic { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerTele { get; set; }
        public string OwnerMobile { get; set; }
        public string OwnerPic { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public string ReviewerTele { get; set; }
        public string ReviewerMobile { get; set; }
        public string ReviewerPic { get; set; }
        public string ProjectPic { get; set; }
        public string FlowName { get; set; }
        public string FlowAbbr { get; set; }
        public string FlowDesc { get; set; }
        public string FlowTitle { get; set; }
        public string Logo { get; set; }
        public string RClassA { get; set; }
        public string RClassB { get; set; }
        public string RClassC { get; set; }
        public Nullable<int> TotalSuccess { get; set; }
        public Nullable<int> TotalFailure { get; set; }
        public Nullable<int> TotalNeurtal { get; set; }
        public Nullable<int> TotalVoteLike { get; set; }
        public Nullable<int> TotalVoteDislike { get; set; }
        public Nullable<int> TotalActiveActionUnits { get; set; }
        public Nullable<int> TotalActiveActionUnitsCompleted { get; set; }
        public Nullable<int> TotalActiveSections { get; set; }
        public Nullable<int> TotalActiveSectionsCompleted { get; set; }
        public Nullable<int> TotalActiveStages { get; set; }
        public Nullable<int> TotalActiveStagesCompleted { get; set; }
        public int ProcessStructCoreId { get; set; }
        public Nullable<System.DateTime> DateSubmitted { get; set; }
        public Nullable<System.DateTime> DateSubmitted2 { get; set; }
        public bool IsSubmitted { get; set; }
        public Nullable<System.DateTime> DateReviewed { get; set; }
        public Nullable<System.DateTime> DateReviewed2 { get; set; }
        public bool IsReviewed { get; set; }
        public Nullable<int> QuestionaireMasterId { get; set; }
        public int WorkSpaceId { get; set; }
        public string WorkSpaceSerial { get; set; }
        public string WorkSpaceTitle { get; set; }
        public string GUID { get; set; }
        public Nullable<int> ExtensionTypeId { get; set; }
        public string PeriodTypeText { get; set; }
        public string PeriodTypeClass { get; set; }
        public string PeriodTypeClass2 { get; set; }
        public string ProcessFlowGUID { get; set; }
        public string BUnitGUID { get; set; }
        public string WorkGUID { get; set; }
        public string ProjGUID { get; set; }
        public int SystemProcessId { get; set; }
        public string DivName { get; set; }
        public string DivGUID { get; set; }
        public string ComGUID { get; set; }
        public string ComName { get; set; }
        public string ComAbbr { get; set; }
        public string DivAbbr { get; set; }
        public Nullable<int> FlowMasterId { get; set; }
        public string Name { get; set; }
        public Nullable<int> QuestionaireUserGroupIdRev { get; set; }
        public Nullable<int> QuestionaireMasterIdA { get; set; }
        public Nullable<bool> IsQuestionaire { get; set; }
        public Nullable<bool> IsQuestionaireRequired { get; set; }
        public Nullable<bool> IsQuestionaireCompleted { get; set; }
        public string QMasterAGIUD { get; set; }
        public string Qlogo { get; set; }
        public string QTitle { get; set; }
        public string FIWNumber { get; set; }
        public string FIWSerial { get; set; }
        public Nullable<int> AdminUsersGroupId { get; set; }
        public Nullable<int> MemberUsersGroupId { get; set; }
        public Nullable<int> AuditorUsersGroupId { get; set; }
        public Nullable<int> BaseUserGroupId { get; set; }
        public Nullable<int> FlowWFolder { get; set; }
        public int ComplexityId { get; set; }
        public int CostRangeId { get; set; }
        public int PriorityId { get; set; }
        public Nullable<decimal> CostBasic { get; set; }
        public Nullable<decimal> CostFinal { get; set; }
        public string EquipmentTag { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CostRangeName { get; set; }
        public string PriorityName { get; set; }
        public string PriorityClass { get; set; }
        public string Complexityname { get; set; }
        public string ComplexityClass { get; set; }
        public string LocationName { get; set; }
        public string SubLocationName { get; set; }
        public int LocationId { get; set; }
        public Nullable<int> SubLocationId { get; set; }
        public Nullable<int> FolderId { get; set; }
        public string UserGUID { get; set; }
        public string OwerGUID { get; set; }
        public string RevGUID { get; set; }
        public string FStauseText { get; set; }
        public string FStausClass { get; set; }
        public string FStausClass2 { get; set; }
        public Nullable<int> FinalStatusId { get; set; }
        public Nullable<int> ClosingUserId { get; set; }
        public Nullable<int> FlowStatusId { get; set; }
        public Nullable<int> COwnerId { get; set; }
        public string COwnerName { get; set; }
        public string COwnerGUID { get; set; }
        public string COwnerEmail { get; set; }
        public string COwnerTele { get; set; }
        public string COwnerMobile { get; set; }
        public string COwnerPic { get; set; }
        public string DescriptionRev { get; set; }
        public bool Flag1 { get; set; }
        public bool Flag2 { get; set; }
        public bool Flag3 { get; set; }
        public bool Flag4 { get; set; }
        public bool Flag5 { get; set; }
        public bool Flag6 { get; set; }
        public bool Flag7 { get; set; }
        public bool Flag8 { get; set; }
        public bool Flag9 { get; set; }
        public bool Flag10 { get; set; }
        public bool Flag11 { get; set; }
        public bool Flag12 { get; set; }
        public bool Flag13 { get; set; }
        public bool Flag14 { get; set; }
        public bool Flag15 { get; set; }
        public Nullable<int> ActiveStageId { get; set; }
        public Nullable<int> ActiveSectionId { get; set; }
        public Nullable<int> ActiveActionUnitId { get; set; }
        public string ActiveStage { get; set; }
        public string ActiveSection { get; set; }
        public Nullable<int> IsSucessful { get; set; }
        public Nullable<bool> IsTimerStarted { get; set; }
        public Nullable<System.DateTime> TimerStartDatetime { get; set; }
        public Nullable<int> TimerStartingUnitId { get; set; }
        public Nullable<bool> IsTimerTopped { get; set; }
        public Nullable<System.DateTime> TimerStopDatetime { get; set; }
        public Nullable<int> TimerStopUnitId { get; set; }
        public bool LockMasterFeatures { get; set; }
        public bool LockMasterFlow { get; set; }
        public Nullable<int> RootProcessId { get; set; }
        public Nullable<bool> IsSelectionQuestionnaire { get; set; }
        public Nullable<int> ScoringQuestionnaireId { get; set; }
        public List<QuestionnaireMasterA> QuestionnaiteMasterIDList { get; set; }

    }
}