using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;





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
        public  DAL.UserProfile  GetUserParams(string username)
        {
            var query = _DbContext.UserProfiles.Where(x => x.UserName == username).Single();
            return query;
        }
        public  DAL.UserProfile  GetUserPersonaInfo(string username)
        {
            var query = _DbContext.UserProfiles.Where(x => x.UserName == username).Single();
            return query;
        }
      
        public DAL.UserProfile GetUserPersonaInfoById(int? userId)
        {
            var query = _DbContext.UserProfiles.Where(x => x.UserId == userId).Single();
            return query;
        }
        //Company Definition Start
        public DAL.CompanyT GetCompanyInfoById(int? Id)
        {
            var query = _DbContext.CompanyTs.Where(x => x.CompanyId == Id).Single();
            return query;
        }
        public IList<DAL.CompanyT> GetCompany_List()
        {
            var query = _DbContext.CompanyTs.Where(x => x.IsDeleted==false).ToList();
            return query;
        }


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
        public IList<DAL.ProcessStructureT> GetProcess_List(int? parentId)
        {//Same Table with Type=0

            IList<DAL.ProcessStructureT> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.ParentId == parentId && x.TypeId == 0).ToList(); }
            else
            { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.TypeId == 0).ToList(); } // type 2 is the Process

            return query;
        }

        public IList<DAL.ProcessStructureT> GetStages_List(int? parentId)
        {//Same Table with Type=2

            IList<DAL.ProcessStructureT> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.ParentId == parentId && x.TypeId == 1).ToList(); }
            else
            { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.TypeId == 1).ToList(); }// type one is the stages

            return query;
        }

        public IList<DAL.ProcessStructureT> GetSections_List(int? parentId)
        {//Same Table with Type=2

            IList<DAL.ProcessStructureT> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.ParentId == parentId && x.TypeId == 2).ToList(); }
            else
            { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.TypeId == 2).ToList(); } // type 2 is the sections

            return query;
        }

        public IList<DAL.ProcessStructureT> GetActionUnit_List(int? parentId)
        {//Same Table with Type=2

            IList<DAL.ProcessStructureT> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.ParentId == parentId && x.TypeId == 3).ToList(); }
            else
            { query = _DbContext.ProcessStructureTs.Where(x => x.IsDeleted == false && x.TypeId == 3).ToList(); } // type 2 is the Action Unit

            return query;
        }

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
            var query = _DbContext.UserPictures.Where(x => x.UserId == userId).Where(x=>x.IsDeleted==false).ToList() ;
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

        public IEnumerable <DAL.CountryList> GetCountryListItems()
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

        public ProcessStructureT GetStageInfoById(int? uId)
        {
            var query = _DbContext.ProcessStructureTs.Where(x => x.Id == uId).Single();
            return query;
        }

        public ProcessStructureT GetProcessInfoById(int? uId)
        {
            var query = _DbContext.ProcessStructureTs.Where(x => x.Id == uId).Single();
            return query;
        }

        public ProcessStructureT GetSectionInfoById(int? uId)
        {
            var query = _DbContext.ProcessStructureTs.Where(x => x.Id == uId).Single();
            return query;
        }

        public ProcessStructureT GetActionUnitInfoById(int? uId)
        {
            var query = _DbContext.ProcessStructureTs.Where(x => x.Id == uId).Single();
            return query;
        }



        public UserGroup GetUserGroupInfoById(int? uId)
        {
            var query = _DbContext.UserGroups.Where(x => x.GroupId == uId).Single();
            return query;
        }


        public IList<DAL.UserGroup> GetUserGroup_List(int? parentId)
        {//Same Table with Type=2

            IList<DAL.UserGroup> query;
            if (parentId != null && parentId > 0)
            { query = _DbContext.UserGroups.Where(x => x.IsDeleted == false && x.CompanyId == parentId  ).ToList(); }
            else
            { query = _DbContext.UserGroups.Where(x => x.IsDeleted == false  ).ToList(); } 

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


        #region Matrix
        public MatrixT GetMatrixInfoById(int? uId)
        {
            var query = _DbContext.MatrixTs.Where(x => x.MatrixId == uId).Single();
            return query;
        }

        public IList<DAL.MatrixT> GetMatrix_List()
        {//Same Table with Type=1
            IList<DAL.MatrixT> query;
            query = _DbContext.MatrixTs.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        #endregion

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

        #region Chat
        public IList<DAL.Chat> GetChat_List()
        {//Same Table with Type=1
            IList<DAL.Chat> query;
            query = _DbContext.Chats.Where(x => x.IsDeleted == false).ToList();
            return query;
        }

        public IList<DAL.vw_GetChatMessagesWithUserInfo> GetChatListWithUserInfo()
        {
            IList<DAL.vw_GetChatMessagesWithUserInfo> query;
            query = _DbContext.vw_GetChatMessagesWithUserInfo.ToList();
            return query;
        }

        #endregion
    }
}
