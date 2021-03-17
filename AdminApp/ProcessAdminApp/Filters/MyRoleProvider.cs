using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProcessAdminApp.Filters
{
    public class MyRoleProvider:RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
           
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //MembershipUser user = Membership.GetUser(username);
            //EmployeeT empl = new EmployeeT();
            //empl.ValidateEmployeeLogin(user.Email);
            //string[] RolesArr = new string[4];

            //if (empl.IsActive)
            //{ RolesArr[0] = "Active"; }
            //else { RolesArr[0] = ""; }
            //if (empl.IsAdmin)
            //{ RolesArr[1] = "Administrator"; }
            //else { RolesArr[1] = ""; }
            //if (empl.IsDeleted)
            //{ RolesArr[2] = "Deleted"; }
            //else { RolesArr[2] = ""; }
            //if (empl.IsResponsible)
            //{ RolesArr[3] = "Responsible"; }
            //else { RolesArr[3] = ""; }
            //return RolesArr;
            
            throw new NotImplementedException();
            
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            //MembershipUser user = Membership.GetUser(username);
            //EmployeeT empl = new EmployeeT();
            //empl.ValidateEmployeeLogin(user.Email);
            //string[] RolesArr = new string[3];
            //bool isOk = false;
            //switch (roleName)
            //{
            //    case "Active":
            //        isOk = empl.IsActive;
            //        break;
            //    case "Administrator":
            //        isOk = empl.IsAdmin;
            //        break;
            //    case "Deleted":
            //        isOk = empl.IsDeleted;
            //        break;
            //    case "Responsible":
            //        isOk = empl.IsResponsible;
            //        break;
            //}

           // return isOk;
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}