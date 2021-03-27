using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DAL
{
    public class DataManager : ProcessadmindbEntities
    {

        private DAL.ProcessadmindbEntities _DbContext = null;


        public DataManager()
        {
            _DbContext = new DAL.ProcessadmindbEntities();
            _DbContext.Configuration.ProxyCreationEnabled = false;



        }
        //Dynami Menu
        public IEnumerable<DAL.MenuItem> GetMenuItems()
        {
            var query = _DbContext.MenuItems.Where(x => x.ParentId == null).ToList();

            foreach (MenuItem itm in query)
            {
                itm.MenuItem1 = _DbContext.MenuItems.Where(x => x.ParentId == itm.MenuItemId).ToList();

                foreach (MenuItem itm2 in itm.MenuItem1)
                {
                    itm2.MenuItem1 = _DbContext.MenuItems.Where(x => x.ParentId == itm2.MenuItemId).ToList();
                }
            }



            return query;
        }
        public DAL.UserProfile GetUserParams(string username)
        {
            var query = _DbContext.UserProfiles.Where(x => x.UserName == username).Single();
            return query;
        }
        public DAL.UserProfile GetUserPersonaInfo(string username)
        {
            var query = _DbContext.UserProfiles.Where(x => x.UserName == username).Single();
            return query;
        }

        public DAL.UserProfile GetUserPersonaInfoById(int? userId)
        {
            var query = _DbContext.UserProfiles.Where(x => x.UserId == userId).Single();
            return query;
        }
        ////Company Definition Start
        //public DAL.CompanyT GetCompanyInfoById(int? Id)
        //{
        //    var query = _DbContext.CompanyTs.Where(x => x.CompanyId == Id).Single();
        //    return query;
        //}
        //public IList<DAL.CompanyT> GetCompany_List()
        //{
        //    var query = _DbContext.CompanyTs.Where(x => x.IsDeleted==false).ToList();
        //    return query;
        //}


        public IList<DAL.BUnitsT> GetBCompanies_List(int? parentId)
        {//Same Table with Type=1
            IList<DAL.BUnitsT> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.BUnitsTs.Where(x => x.IsDeleted == false && x.BUnitType == 1 && x.BUnitParentId == parentId).ToList(); }
            else
            { query = _DbContext.BUnitsTs.Where(x => x.IsDeleted == false && x.BUnitType == 1).ToList(); }

            return query;
        }

        public IList<DAL.BUnitsT> GetBDivisions_List(int? parentId)
        {//Same Table with Type=2

            IList<DAL.BUnitsT> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.BUnitsTs.Where(x => x.IsDeleted == false && x.BUnitType == 2 && x.BUnitParentId == parentId).ToList(); }
            else
            { query = _DbContext.BUnitsTs.Where(x => x.IsDeleted == false && x.BUnitType == 2).ToList(); }

            return query;
        }


        public IList<DAL.BUnitsT> GetBUnits_List(int? parentId)
        {//Same Table with Type=3
            IList<DAL.BUnitsT> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.BUnitsTs.Where(x => x.IsDeleted == false && x.BUnitType == 3 && x.BUnitParentId == parentId).ToList(); }
            else
            { query = _DbContext.BUnitsTs.Where(x => x.IsDeleted == false && x.BUnitType == 3).ToList(); }


            return query;
        }
        //public IList<DAL.ProcessT> GetProcesses_List(int? parentId)
        //{//Same Table with Type=2

        //    IList<DAL.ProcessT> query;
        //    if (parentId != null && parentId > 0)
        //    { query = _DbContext.ProcessTs.Where(x => x.IsDeleted == false && x.ProcessUnitId == parentId).ToList(); }
        //    else
        //    { query = _DbContext.ProcessTs.Where(x => x.IsDeleted == false).ToList(); }

        //    return query;
        //}
        //public IList<DAL.ProcessStructureT> GetProcess_List(int? parentId)
        //{//Same Table with Type=0

        //    IList<DAL.ProcessStructureT> query;
        //    if (parentId != null && parentId > 0)
        //    { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.ParentId == parentId && x.TypeId == 0).ToList(); }
        //    else
        //    { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.TypeId == 0).ToList(); } // type 2 is the Process

        //    return query;
        //}

        //public IList<DAL.ProcessStructureT> GetStages_List(int? parentId)
        //{//Same Table with Type=2

        //    IList<DAL.ProcessStructureT> query;
        //    if (parentId != null && parentId > 0)
        //    { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.ParentId == parentId && x.TypeId == 1).ToList(); }
        //    else
        //    { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.TypeId == 1).ToList(); }// type one is the stages

        //    return query;
        //}

        //public IList<DAL.ProcessStructureT> GetSections_List(int? parentId)
        //{//Same Table with Type=2

        //    IList<DAL.ProcessStructureT> query;
        //    if (parentId != null && parentId > 0)
        //    { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.ParentId == parentId && x.TypeId == 2).ToList(); }
        //    else
        //    { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.TypeId == 2).ToList(); } // type 2 is the sections

        //    return query;
        //}

        //public IList<DAL.ProcessStructureT> GetActionUnit_List(int? parentId)
        //{//Same Table with Type=2

        //    IList<DAL.ProcessStructureT> query;
        //    if (parentId != null && parentId > 0)
        //    { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.ParentId == parentId && x.TypeId == 3).ToList(); }
        //    else
        //    { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.TypeId == 3).ToList(); } // type 2 is the Action Unit

        //    return query;
        //}

        public IList<DAL.vw_ActvityLog> GetActvityLog_List()
        {
            var query = _DbContext.vw_ActvityLog.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        //Company Definition End
        //User Account Start
        public IList<DAL.vw_userslist> GetUser_vw_List()
        {
            var query = _DbContext.vw_userslist.ToList();
            return query;
        }


        public IList<DAL.vw_UserRoles> GetUser_Roles()
        {
            var query = _DbContext.vw_UserRoles.ToList();
            return query;
        }

        public List<DAL.UserPicture> GetUserPictures(int userId)
        {
            var query = _DbContext.UserPictures.Where(x => x.UserId == userId).Where(x => x.IsDeleted == false).ToList();
            return query;
        }
        //public DAL.UserProfile GetUserPersonaInfo(DAL.UserProfile datamodel)
        //{
        //    _DbContext.SaveChanges();
        //    var query = _DbContext.UserProfiles.Where(x => x.UserName == username).Single();
        //    return query;
        //}
        public class CommodityCategoryMap : EntityTypeConfiguration<MenuItem>
        {

        }

        public IEnumerable<DAL.CountryList> GetCountryListItems()
        {
            var query = _DbContext.CountryLists.ToList();
            return query;
        }

        public BUnitsT GetBUnitsInfoById(int? uId)
        {
            var query = _DbContext.BUnitsTs.Where(x => x.BUnitId == uId).Single();
            return query;
        }

        //public ProcessT GetProcessInfoById(int? uId)
        //{
        //    var query = _DbContext.ProcessTs.Where(x => x.ProcessId == uId).Single();
        //    return query;
        //}

        //public ProcessStructureT GetStageInfoById(int? uId)
        //{
        //    var query = _DbContext.ProcessStructureTs.Where(x => x.Id == uId).Single();
        //    return query;
        //}

        //public ProcessStructureT GetProcessInfoById(int? uId)
        //{
        //    var query = _DbContext.ProcessStructureTs.Where(x => x.Id == uId).Single();
        //    return query;
        //}

        //public ProcessStructureT GetSectionInfoById(int? uId)
        //{
        //    var query = _DbContext.ProcessStructureTs.Where(x => x.Id == uId).Single();
        //    return query;
        //}

        //public ProcessStructureT GetActionUnitInfoById(int? uId)
        //{
        //    var query = _DbContext.ProcessStructureTs.Where(x => x.Id == uId).Single();
        //    return query;
        //}



        public UserGroup GetUserGroupInfoById(int? uId)
        {
            var query = _DbContext.UserGroups.Where(x => x.GroupId == uId).Single();
            return query;
        }


        public IList<DAL.UserGroup> GetUserGroup_List(int? parentId)
        {//Same Table with Type=2

            IList<DAL.UserGroup> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.UserGroups.Where(x => x.IsDeleted == false && x.CompanyId == parentId).ToList(); }
            else
            { query = _DbContext.UserGroups.Where(x => x.IsDeleted == false).ToList(); }

            return query;
        }

        public IList<DAL.vw_UserGroups> GetViewUsersInGroups_List(int? parentId)
        {//Same Table with Type=2, 
            //In this View, it will show only the un deleted users under the undeleted group under the un deleted company

            IList<DAL.vw_UserGroups> query;
            if (parentId != null && parentId > -2)
            { query = _DbContext.vw_UserGroups.Where(x => x.UserDeleted == false && x.GroupId == parentId).ToList(); }
            else
            { query = _DbContext.vw_UserGroups.Where(x => x.UserDeleted == false).ToList(); }

            return query;
        }


        //#region Matrix
        //public MatrixT GetMatrixInfoById(int? uId)
        //{
        //    var query = _DbContext.MatrixTs.Where(x => x.MatrixId == uId).Single();
        //    return query;
        //}

        //public IList<DAL.MatrixT> GetMatrix_List()
        //{//Same Table with Type=1
        //    IList<DAL.MatrixT> query;
        //    query = _DbContext.MatrixTs.Where(x => x.IsDeleted == false).ToList();
        //    return query;
        //}

        //#endregion

        #region Department
        public Department GetDepartmentInfoById(int? uId)
        {
            var query = _DbContext.Departments.Where(x => x.DepartmentId == uId).Single();
            return query;
        }

        public IList<DAL.Department> GetDepartment_List()
        {//Same Table with Type=1
            IList<DAL.Department> query;
            query = _DbContext.Departments.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        #endregion


        #region AppLockup
        //public AppLockup GetAppLockupInfoById(int? uId)
        //{
        //    var query = _DbContext.AppLockups.Where(x => x.Id == uId).Single();
        //    return query;
        //}

        //public IList<DAL.AppLockup> GetAppLockup_List()
        //{//Same Table with Type=1
        //    IList<DAL.AppLockup> query;
        //    query = _DbContext.AppLockups.Where(x => x.IsDeleted != true).ToList();
        //    return query;
        //}

        #endregion

        #region NotificationPriority_Lockup
        public NotificationPriority_Lockup GetNotificationPriority_LockupInfoById(int? uId)
        {
            var query = _DbContext.NotificationPriority_Lockup.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.NotificationPriority_Lockup> GetNotificationPriority_Lockup_List()
        {//Same Table with Type=1
            IList<DAL.NotificationPriority_Lockup> query;
            query = _DbContext.NotificationPriority_Lockup.Where(x => x.IsDeleted != true).ToList();
            return query;
        }
        #endregion


        #region Costing & ChangeType
        public ProcessCategory GetCostingInfoById(int? uId)
        {
            var query = _DbContext.ProcessCategories.Where(x => x.Id == uId).Single();
            return query;
        }
        #endregion

        #region Costing
        public IList<DAL.ProcessCategory> GetCosting_List()
        {
            IList<DAL.ProcessCategory> query;
            query = _DbContext.ProcessCategories.Where(x => x.IsDeleted == false && x.TypeId == 2).ToList();
            return query;
        }
        #endregion

        #region ChangeType
        public IList<DAL.ProcessCategory> GetChangeType_List()
        {
            IList<DAL.ProcessCategory> query;
            query = _DbContext.ProcessCategories.Where(x => x.IsDeleted == false && x.TypeId == 1).ToList();
            return query;
        }

        public IList<DAL.ProcessCategory> GetParent_List()
        {

            IList<DAL.ProcessCategory> query;
            query = _DbContext.ProcessCategories
                            .Where(x => x.TypeId == 1 && x.ParentId == 0 && x.IsDeleted != true && x.IsActive==true)
                            .OrderByDescending(x => x.Id)
                            .ToList();
            


            return query;
        }


        #endregion

        //#region OptionsLockup
        //public OptionsLockup GetOptionsLockupInfoById(int? uId)
        //{
        //    var query = _DbContext.OptionsLockups.Where(x => x.Id == uId).Single();
        //    return query;
        //}

        //public IList<DAL.OptionsLockup> GetOptionsLockup_List()
        //{//Same Table with Type=1
        //    IList<DAL.OptionsLockup> query;
        //    query = _DbContext.OptionsLockups.Where(x => x.IsDeleted == false).ToList();
        //    return query;
        //}

        //#endregion

        #region OperationLockup
        public OperationLockup GetOperationLockupInfoById(int? uId)
        {
            var query = _DbContext.OperationLockups.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.OperationLockup> GetOperationLockup_List()
        {//Same Table with Type=1
            IList<DAL.OperationLockup> query;
            query = _DbContext.OperationLockups.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        #endregion

        #region NotificationLockup
        public NotificationLockup GetNotificationLockupInfoById(int? uId)
        {
            var query = _DbContext.NotificationLockups.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.NotificationLockup> GetNotificationLockup_List()
        {//Same Table with Type=1
            IList<DAL.NotificationLockup> query;
            query = _DbContext.NotificationLockups.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        #endregion

        #region Consequence_Lockup
        public Consequence_Lockup GetConsequence_LockupInfoById(int? uId)
        {
            var query = _DbContext.Consequence_Lockup.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.Consequence_Lockup> GetConsequence_Lockup_List()
        {//Same Table with Type=1
            IList<DAL.Consequence_Lockup> query;
            query = _DbContext.Consequence_Lockup.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        #endregion

        #region CommandLockup
        public CommandLockup GetCommandLockupInfoById(int? uId)
        {
            var query = _DbContext.CommandLockups.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.CommandLockup> GetCommandLockup_List()
        {//Same Table with Type=1
            IList<DAL.CommandLockup> query;
            query = _DbContext.CommandLockups.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        #endregion

        #region TourT
        public TourT GetTourTInfoById(int? uId)
        {
            var query = _DbContext.TourTs.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.TourT> GetTourT_List()
        {//Same Table with Type=1
            IList<DAL.TourT> query;
            query = _DbContext.TourTs.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        #endregion

        #region ProcessFieldLookup
        public ProcessFieldLookup GetProcessFieldLookupInfoById(int? uId)
        {
            var query = _DbContext.ProcessFieldLookups.Where(x => x.ColumnId == uId).Single();
            return query;
        }

        public IList<DAL.ProcessFieldLookup> GetProcessFieldLookup_List()
        {//Same Table with Type=1
            IList<DAL.ProcessFieldLookup> query;
            query = _DbContext.ProcessFieldLookups.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        #endregion

        #region ProcessFieldSetup
        public ProcessFieldSetup GetProcessFieldSetupInfoById(int? uId)
        {
            var query = _DbContext.ProcessFieldSetups.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.ProcessFieldSetup> GetProcessFieldSetup_List()
        {
           
            IList<DAL.ProcessFieldSetup> query;
            query = _DbContext.ProcessFieldSetups.ToList();
            return query;
        }

        public IList<DAL.ProcessFieldSetup> GetProcessFieldSetup_ListbyProcessId(int pId)
        {
            IList<DAL.ProcessFieldSetup> query;
            query = _DbContext.ProcessFieldSetups.Where(x => x.ProcessId == pId).ToList();
            return query;
        }

        public IList<DAL.ProcessFieldLookup> GetFieldNameAll_List(int? cId)
        {

            IList<DAL.ProcessFieldLookup> query;
            query = _DbContext.ProcessFieldLookups.Where(x => x.ColumnId == cId)
                            .ToList();



            return query;
        }
        public IList<DAL.ProcessFieldLookup> GetFieldNameNew_List()
        {

            IList<DAL.ProcessFieldLookup> query;

            //var qry = _DbContext.ProcessFieldSetups.ToList();
            //query = _DbContext.ProcessFieldLookups
            var qry = from fieldsetup in _DbContext.ProcessFieldSetups select fieldsetup;
            //                .ToList();
            //return query;

            query = _DbContext.ProcessFieldLookups.Where(c => !qry.Select(fc => fc.ColumnId).Contains(c.ColumnId)).ToList();

            return query;
        }

        #endregion




        //#region Chat
        //public IList<DAL.Chat> GetChat_List()
        //{//Same Table with Type=1
        //    IList<DAL.Chat> query;
        //    query = _DbContext.Chats.Where(x => x.IsDeleted == false).ToList();
        //    return query;
        //}

        //public IList<DAL.vw_GetChatMessagesWithUserInfo> GetChatListWithUserInfo()
        //{
        //    IList<DAL.vw_GetChatMessagesWithUserInfo> query;
        //    query = _DbContext.vw_GetChatMessagesWithUserInfo.ToList();
        //    return query;
        //}

        //#endregion


        #region ProcessMaster

        public Vw_ProcessMaster GetVW_ProcessMasterInfoByGUId(string guid)
        {
            var query = _DbContext.Vw_ProcessMaster.Where(x => x.GUID == guid).Single();
            return query;
        }

        public Vw_ProcessMaster GetVW_ProcessMasterInfoById(int? id)
        {
            var query = _DbContext.Vw_ProcessMaster.Where(x => x.Id == id).Single();
            return query;
        }


        #endregion


        #region ChemicalT
        //public ChemicalT GetChemicalTInfoById(int? uId)
        //{
        //    var query = _DbContext.ChemicalTs.Where(x => x.Id == uId).Single();
        //    return query;
        //}

        //public IList<DAL.ChemicalT> GetChemicalT_List()
        //{//Same Table with Type=1
        //    IList<DAL.ChemicalT> query;
        //    query = _DbContext.ChemicalTs.Where(x => x.IsDeleted == false).ToList();
        //    return query;
        //}

        #endregion


        #region Coshh
        //public COSHH GetCoshhInfoById(int? uId)
        //{
        //    var query = _DbContext.COSHHs.Where(x => x.Id == uId).Single();
        //    return query;
        //}

        //public IList<DAL.COSHH> GetCoshh_List()
        //{//Same Table with Type=1
        //    IList<DAL.COSHH> query;
        //    query = _DbContext.COSHHs.Where(x => x.IsDeleted == false).ToList();
        //    return query;
        //}

        //public IList<DAL.OptionsLockup> GetRiskRating_List()
        //{

        //    IList<DAL.OptionsLockup> query;           
        //    query = _DbContext.OptionsLockups.Where(c => c.Type == 103 && c.IsDeleted == false).OrderBy(x=>x.Value).ToList();

        //    return query;
        //}

        #endregion

        #region ProcessMasterExt
        //public ProcessMasterExt GetProcessMasterExtInfoById(int? uId)
        //{
        //    var query = _DbContext.ProcessMasterExts.Where(x => x.ExtendId == uId).Single();
        //    return query;
        //}
        #endregion


        #region Customer
        public Customer GetCustomerInfoById(int? uId)
        {
            var query = _DbContext.Customers.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.Customer> GetCustomer_List()
        {//Same Table with Type=1
            IList<DAL.Customer> query;
            query = _DbContext.Customers.Where(x => x.IsDeleted == false).ToList();
            return query;
        }


       


        public IList<DAL.ProcessCategory> GetCustomerClass_List()
        {

            IList<DAL.ProcessCategory> query;
            query = _DbContext.ProcessCategories
                            .Where(x => x.TypeId == 4 && x.ParentId == 0 && x.IsDeleted != true && x.IsActive == true)
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        public IList<DAL.ProcessCategory> GetCustomerCategory_List()
        {

            IList<DAL.ProcessCategory> query;
            query = _DbContext.ProcessCategories
                            .Where(x => x.TypeId == 5 && x.ParentId == 0 && x.IsDeleted != true && x.IsActive == true)
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        public IList<DAL.ProcessCategory> GetCustomerType_List()
        {

            IList<DAL.ProcessCategory> query;
            query = _DbContext.ProcessCategories
                            .Where(x => x.TypeId == 6 && x.ParentId == 0 && x.IsDeleted != true && x.IsActive == true)
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        #endregion

        #region Request Transaction
        public RequestTransaction GetRequestTransactionInfoById(int? uId)
        {
            var query = _DbContext.RequestTransactions.Where(x => x.Id == uId).Single();
            return query;
        }

        public IList<DAL.RequestTransaction> GetRequestTransaction_List()
        {//Same Table with Type=1
            IList<DAL.RequestTransaction> query;
            query = _DbContext.RequestTransactions.Where(x => x.IsDeleted == false).Take(10).ToList();
            return query;
        }

        public IList<DAL.Vw_RequestDetails> GetVwRequestDetails_ListByParty(int? pId)
        {//Same Table with Type=1
            IList<DAL.Vw_RequestDetails> query;
            query = _DbContext.Vw_RequestDetails.Where(x => x.IsDeleted == false && x.PartyId == pId).OrderBy(x => x.Id).ToList();
            return query;
        }

        public IList<DAL.Product> GetProduct_List()
        {

            IList<DAL.Product> query;
            query = _DbContext.Products
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        public IList<DAL.RequestType> GetRequestType_List()
        {

            IList<DAL.RequestType> query;
            query = _DbContext.RequestTypes
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        public IList<DAL.Party> GetParty_List(int pId)
        {

            IList<DAL.Party> query;
            query = _DbContext.Parties
                            .Where(x => x.Id == pId)
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        public IList<DAL.AcctCategory> GetAcctCategory_List()
        {

            IList<DAL.AcctCategory> query;
            query = _DbContext.AcctCategories
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        public IList<DAL.ESSegment> GetESSegment_List()
        {

            IList<DAL.ESSegment> query;
            query = _DbContext.ESSegments
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        public IList<DAL.RPlan> GetRatePlan_List()
        {

            IList<DAL.RPlan> query;
            query = _DbContext.RPlans
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        public IList<DAL.Contract> GetContract_List()
        {

            IList<DAL.Contract> query;
            query = _DbContext.Contracts
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }
        public IList<DAL.ApprovalRequestStatu> GetApprovalRequestStatus_List()
        {

            IList<DAL.ApprovalRequestStatu> query;
            query = _DbContext.ApprovalRequestStatus
                            .OrderByDescending(x => x.Id)
                            .ToList();

            return query;
        }

        #endregion
    }
}
