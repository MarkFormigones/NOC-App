using ImageResizer;
using Hydron.Controllers;
using Hydron.Models.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;


namespace Hydron.Views
{
    public class UserTreeController : BaseController
    {
        // GET: Tree
        public ActionResult Index(int groupId)
        {
            ViewBag.groupId = groupId;
            if(groupId>0)
            {
                DAL.DataManager dataMgr = new DAL.DataManager();

                var groups = dataMgr.UserGroups.Where(x => x.IsDeleted == false && x.GroupId == groupId).Single();

                @ViewBag.groupTitle = groups.GroupName;
            }
            else
            {
                throw new Exception("group not found");
            }
           
            return View();
        }

        public ActionResult UserPyramid()
        {
            return View();
        }
        public ActionResult UserManage(int groupId)
        {
            List<SelectListItem> userRole = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Admin", Value = "0" },
                new SelectListItem { Text = "User", Value = "1" },
                new SelectListItem { Text = "Team leader", Value = "2" },
                new SelectListItem { Text = "Manager", Value = "3" },

            };
            //Assigning generic list to ViewBag
            ViewBag.userRoleList = userRole;
            ViewBag.groupId = groupId;
            if (groupId > 0)
            {
                DAL.DataManager dataMgr = new DAL.DataManager();

                var groups = dataMgr.UserGroups.Where(x => x.IsDeleted == false && x.GroupId == groupId).Single();

                @ViewBag.groupTitle = groups.GroupName;
            }
            else
            {
                throw new Exception("group not found");
            }

            return View();
        }
        public PartialViewResult _GetListUserTeamHirarchy(int? uId, int? listType, int? groupId)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();

            if (listType == null)
            { listType = 1; }
            if (uId == null)
            { uId = -1; }
            if (groupId != null && groupId > 0)
            {

                var items = dataMgr.Vw_UserInHirarchy.Where(x => x.GroupId == groupId).ToList(); ;
                //   var itemsMyLeader = dataMgr.Vw_UserInHirarchy.Where(x => x.GroupId == groupId);
                //var Roleitems = dataMgr.GetUser_Roles();
                var ls_items = new List<Models.Definitions.UserTeamHirarchyModel>();
                int count = 0;
                foreach (var item in items)
                {
                    count += 1;
                    var l_item = new Models.Definitions.UserTeamHirarchyModel(item);
                    if (item.LeaderId == item.UserId)
                    {

                        l_item.GroupDesc = "Root";
                    }
                    l_item.ListType = listType; // defines the type of view to render 



                    ls_items.Add(l_item);
                }

                //foreach (var item in itemsMyLeader)
                //{
                //    var l_item = new Models.Definitions.UserTeamHirarchyModel(item);
                //    l_item.GroupCount = count;
                //    l_item.GroupDesc = "Leader";

                //    l_item.ListType = listType; // defines the type of view to render 



                //    ls_items.Add(l_item);
                //}
                return PartialView(ls_items);
            }
            else
            {
                var items = dataMgr.Vw_UserInHirarchy.Where(x => x.LeaderId == uId || x.LeaderId == 0);
                var itemsMyLeader = dataMgr.Vw_UserInHirarchy.Where(x => x.UserId == uId);
                //var Roleitems = dataMgr.GetUser_Roles();
                var ls_items = new List<Models.Definitions.UserTeamHirarchyModel>();
                int count = 0;
                foreach (var item in items)
                {
                    count += 1;
                    var l_item = new Models.Definitions.UserTeamHirarchyModel(item);

                    l_item.ListType = listType; // defines the type of view to render 



                    ls_items.Add(l_item);
                }

                foreach (var item in itemsMyLeader)
                {
                    var l_item = new Models.Definitions.UserTeamHirarchyModel(item);
                    l_item.GroupCount = count;
                    l_item.GroupDesc = "Leader";

                    l_item.ListType = listType; // defines the type of view to render 



                    ls_items.Add(l_item);
                }
                return PartialView(ls_items);
            }




        }
        public JsonResult GetTreeData(int? uId, int? listType, int? groupId,int?selectedGroup)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            List<TreeNode> nodes = new List<TreeNode>();

           
            int group = int.Parse(groupId.ToString());

            if (listType == null)
            {
                var items = dataMgr.Vw_UserInHirarchy.Where(x => x.GroupId == groupId && x.LeaderId == x.UserId ).ToList();  // get }
                
                TreeNode rootNode = new TreeNode
                {
                    key = 0,
                    title = "-----",
                    folder = false,
                    lazy = false,
                    extraClasses = "0",
                    userRole = -1
                };
                nodes.Add(rootNode);

                foreach (var item in items)
                {
                    var node = new TreeNode();
                    node.key = item.UserId;
                    node.extraClasses = item.UserLevelId.ToString();
                    node.title = item.UserName;
                    int counter = dataMgr.Vw_UserInHirarchy.Where(x => x.LeaderId == item.UserId && x.GroupId == groupId && x.LeaderId != x.UserId).Count();  // get Second level leaders
                    if (counter > 0)
                    {
                        node.folder = true;
                        node.lazy = true;
                    }
                    else
                    {
                        node.folder = false;
                        node.lazy = false;
                    }
                    #region UPDATE
                    node.userRole = (int)item.UserLevelId;
                    #endregion
                    nodes.Add(node);
                }
            }
            else if(listType==99)
            {
                var Hirarchy_users = dataMgr.Vw_UserInHirarchy.Where(x => x.GroupId == selectedGroup).Select(x => x.UserId);  // get }
                var items  = dataMgr.vw_UserGroups.Where(x => x.GroupId == group).ToList();  // get  

                //if (  items.Count==0 )
                //{
                //    var groupItem = dataMgr.UserGroups.Where(x => x.GroupId == group).Single();  // get  
                //      items = dataMgr.vw_UserGroups.Where(x => x.GroupId == groupItem.ParentGroupId).ToList();  // get  
                //}

                var itemFilter = from a in items
                                 where !Hirarchy_users.Contains(a.UserId)
                                 select a;
                TreeNode rootNode = new TreeNode
                {
                    key = 0,
                    title = "-----",
                    folder = false,
                    lazy = false,
                    extraClasses = "0"
                };
                nodes.Add(rootNode);

                foreach (var item in itemFilter)
                {
                    var node = new TreeNode();
                    node.key = item.UserId;
                    node.extraClasses = "-1";
                    node.title = item.UserName;
                    node.folder = false;
                    node.lazy = false;                   
                    nodes.Add(node);
                }
            }
           
          
            
           
 
            return new JsonResult { Data = nodes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetSubTreeData(int Node, int? groupId)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
           
            var items = dataMgr.Vw_UserInHirarchy.Where(x =>  x.LeaderId == Node && x.GroupId== groupId && x.LeaderId!= x.UserId).ToList();  // get Second level leaders
            List<TreeNode> nodes = new List<TreeNode>();

            foreach (var item in items)
            {
                var node = new TreeNode();
                node.key = item.UserId;
                int counter = dataMgr.Vw_UserInHirarchy.Where(x => x.LeaderId == item.UserId && x.GroupId == groupId && x.LeaderId != x.UserId).Count();  // get Second level leaders
                node.extraClasses = item.UserLevelId.ToString();
                node.title = item.UserName;
                if(counter>0)
                {
                    node.folder = true;
                    node.lazy = true;
                }
                else
                {
                    node.folder = false;
                    node.lazy = false;
                }
                #region UPDATE
                node.userRole = (int)item.UserLevelId;
                #endregion

                nodes.Add(node);
            }

            return new JsonResult { Data = nodes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult AddNode(int userId, string userName, int parentId, int userRole,int groupId)
        {
            var result = 0;
            if (userName != null)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var userExist = dataMgr.UsersInHirarchies.Where(x => x.UserId == userId && x.IsDeleted == false &&x.GroupId== groupId).ToList();
                        if (userExist.Count == 0)
                        {
                            var item = new DAL.UsersInHirarchy();
                            item.UserId = userId;
                           item.GroupId = groupId;
                            if(parentId==0)
                            {
                                item.LeaderId = item.UserId;
                            }
                            else
                            {
                                item.LeaderId = parentId;
                            }
                           
                            item.CompanyId = -1;
                            item.Dated = DateTime.Now;
                            item.IsDeleted = false;
                            item.IsActive = true;
                            item.UserLevelId = userRole;
                            dataMgr.UsersInHirarchies.Add(item);

                            dataMgr.SaveChanges();
                            ViewBag.status = "Success";
                            transaction.Complete();
                            result = item.Id;
                        }

                    }
                    catch (Exception e)
                    {
                        transaction.Dispose();
                        ModelState.AddModelError("error", e.Message);
                    }
                }
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult UpdateNodeParent(int selectedId, int parentId, int groupId)
        {
            var result = false;
            if (selectedId != 0)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var item = dataMgr.UsersInHirarchies.Where(x => x.UserId == selectedId && x.IsDeleted == false && x.GroupId == groupId).SingleOrDefault();

                        item.LeaderId = parentId;
                        dataMgr.SaveChanges();
                        transaction.Complete();
                        result = true;
                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        ModelState.AddModelError("error", e.Message);
                        result = false;
                    }
                }
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult UpdateNodeRole(int userRole, int selectedId, int groupId) 
        {
            var result = false;
            if (userRole > -1)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var item = dataMgr.UsersInHirarchies.Where(x => x.UserId == selectedId && x.IsDeleted == false && x.GroupId == groupId).SingleOrDefault();

                        item.UserLevelId = userRole;

                       

                        dataMgr.SaveChanges();
                        transaction.Complete();
                        result = true;
                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        ModelState.AddModelError("error", e.Message);
                        result = false;
                    }
                }
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult DeleteNode(int selectedId, int groupId)
        {
            var result = false;
            if (selectedId != 0)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var item = dataMgr.UsersInHirarchies.Where(x => x.UserId == selectedId && x.IsDeleted == false && x.GroupId == groupId).SingleOrDefault();


                        var itemChilds = dataMgr.UsersInHirarchies.Where(x => x.LeaderId == selectedId && x.IsDeleted == false && x.GroupId == groupId && x.UserId!=x.LeaderId).Count();
                        if(itemChilds>0)
                        {
                            result = false;
                            throw new Exception("cannot delete parent node");
                        }
                        else
                        {
                            item.IsDeleted = true;
                            item.IsActive = false;
                            dataMgr.SaveChanges();
                            transaction.Complete();
                            result = true;
                        }
                       
                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        ModelState.AddModelError("error", e.Message);
                        result = false;
                    }
                }
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //used for the userManage view. select group by parentgroupId.
        public JsonResult GetUserGroups(int groupId)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var groups = dataMgr.UserGroups.Where(x => x.IsDeleted == false && x.ParentGroupId == groupId)
                .ToList();

            List<TreeNode> nodes = new List<TreeNode>();

            TreeNode rootNode = new TreeNode
            {
                key = groupId,
                title = "---",
                folder = true,
                lazy = false,
                extraClasses = "-1"
            };
            nodes.Add(rootNode);

            foreach (var item in groups)
            {
                var node = new TreeNode();
                node.key = item.GroupId;
                node.title = item.GroupName;
                node.folder = true;
                node.lazy = false;
                node.extraClasses = "-1";
                nodes.Add(node);
            }

            return new JsonResult { Data = nodes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //used for the userManage view. add new group.
        [HttpPost]
        public JsonResult AddGroup(string nodeName, int parentGroupId)
        {
            var result = 0;
            if (nodeName != null)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var groupExist = dataMgr.UserGroups.Where(x => x.GroupName.ToLower() == nodeName.ToLower() && x.IsDeleted == false).ToList();
                        if (groupExist.Count == 0)
                        {
                            var item = new DAL.UserGroup();
                            item.GroupName = nodeName;
                            item.GroupDesc = nodeName;
                            item.CompanyId = -1;
                            item.DivisionId = -1;
                            item.BUnitId = -1;
                            item.ParentGroupId = parentGroupId;
                            item.IsDeleted = false;
                            item.IsActive = true;
                            item.Dated = DateTime.Now;
                            dataMgr.UserGroups.Add(item);
                            dataMgr.SaveChanges();
                            ViewBag.status = "Success";
                            transaction.Complete();
                            result = item.GroupId;
                        }

                    }
                    catch (Exception e)
                    {
                        transaction.Dispose();
                        ModelState.AddModelError("error", e.Message);
                    }
                }
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //used for the userManage view. update name of the selected group.
        [HttpPost]
        public JsonResult UpdateGroup(string nodeName, int selectedId)
        {
            var result = false;
            if (nodeName != null)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var item = dataMgr.UserGroups.Where(x => x.GroupId == selectedId && x.IsDeleted == false).SingleOrDefault();

                        item.GroupName = nodeName;
                        item.GroupDesc = nodeName;

                        dataMgr.SaveChanges();
                        transaction.Complete();
                        result = true;
                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        ModelState.AddModelError("error", e.Message);
                        result = false;
                    }
                }
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //used for the userManage view. delete the selected group.
        [HttpPost]
        public JsonResult DeleteGroup(int selectedId)
        {
            var result = false;
            if (selectedId != 0)
            {
                // Attempt to register the user
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        DAL.DataManager dataMgr = new DAL.DataManager();
                        var item = dataMgr.UserGroups.Where(x => x.GroupId == selectedId && x.IsDeleted == false).SingleOrDefault();
                        item.IsDeleted = true;
                        item.IsActive = false;
                        dataMgr.SaveChanges();

                        //will delete also the users inside
                        var users = dataMgr.UsersInHirarchies.Where(x => x.GroupId == selectedId && x.IsDeleted == false).ToList();
                        if (users.Count > 0)
                        {
                            foreach (var user in users)
                            {
                                user.IsDeleted = true;
                                user.IsActive = false;
                                dataMgr.SaveChanges();
                            }

                        }
                        transaction.Complete();
                        result = true;
                    }

                    catch (Exception e)
                    {
                        transaction.Dispose();
                        ModelState.AddModelError("error", e.Message);
                        result = false;
                    }
                }
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //used for the index view. first time load the user check tree.
        public JsonResult GetTemplateCheckTree(int parentGroupId)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            if (parentGroupId > 0)
            {
                DAL.DataManager dataMgr = new DAL.DataManager();
                //select groups
                var groups = dataMgr.UserGroups.Where(x => x.IsDeleted == false && x.ParentGroupId == parentGroupId)
                    .ToList();

                foreach (var itemGroup in groups)
                {
                    //select users of the group
                    var users = dataMgr.UsersInHirarchies.Where(x => x.IsDeleted == false && x.GroupId == itemGroup.GroupId && x.LeaderId == x.UserId).ToList();
                    if (users.Count > 0)
                    {
                        var node = new TreeNode();
                        node.key = itemGroup.GroupId;
                        node.title = itemGroup.GroupName;
                        node.folder = true;
                        node.lazy = false;
                        #region UPDATE
                        node.userRole = -1;
                        #endregion
                        List<TreeNode> ChildNodes = new List<TreeNode>();
                        //select users in groups
                        foreach (var user in users)
                        {
                            var child = new TreeNode();
                            child.key = (int)user.UserId;
                            var profile = dataMgr.UserProfiles.Where(x => x.UserId == user.UserId && x.IsDeleted == false).SingleOrDefault();
                            child.title = profile.UserName;

                            var childSub = dataMgr.UsersInHirarchies.Where(x => x.LeaderId == user.UserId && x.IsDeleted == false && x.LeaderId != x.UserId
                            && x.GroupId == user.GroupId).Count();
                            if (childSub > 0)
                            { child.folder = true; child.lazy = true; }
                            else
                            { child.folder = false; child.lazy = false; }
                            #region UPDATE
                            child.userRole = (int)user.UserLevelId;
                            #endregion
                            ChildNodes.Add(child);
                        }
                        node.children = ChildNodes;
                        nodes.Add(node);
                    }
                }
            }

            return new JsonResult { Data = nodes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult UserCheck()
        {
            return View();
        }
        //used for the usercheck view. If Manager/TL wants to view or modify the check tree.
        #region UPDATE
        public JsonResult GetTemplateTCheckTree(int parentGroupId, string serial, int? groupId, int? leaderId, int? roleId)
        {
        #endregion

            List<TreeNode> nodes = new List<TreeNode>();
            if (parentGroupId > 0)
            {
                DAL.DataManager dataMgr = new DAL.DataManager();
                //select groups
                var groups = dataMgr.UserGroups.Where(x => x.IsDeleted == false && x.ParentGroupId == parentGroupId)
                    .ToList();

                #region UPDATE
                if (groupId > 0)
                {
                    groups = groups.Where(x => x.GroupId == groupId).ToList();
                }
                #endregion

                foreach (var itemGroup in groups)
                {
                    #region UPDATE
                    List<DAL.UsersInHirarchyT> users = null;
                    if(roleId > 0 && leaderId > 0)
                    {
                        switch (roleId)
                        {
                            case (int)UserType.TeamLeader: //team leader tree
                                users = dataMgr.UsersInHirarchyTs.Where(x => x.IsDeleted == false
                                && x.ComSerial == serial && x.GroupId == itemGroup.GroupId)
                                .Where(x => x.UserId == leaderId)
                                .ToList();
                                break;

                            case (int)UserType.Manager: // manager tree
                                users = dataMgr.UsersInHirarchyTs.Where(x => x.IsDeleted == false
                                && x.ComSerial == serial && x.GroupId == itemGroup.GroupId && x.LeaderId == x.UserId)
                                .Where(x=> x.UserId == leaderId)              
                                .ToList();
                                break;
                        }

                    }
                    else
                    {
                        //no filter
                        users = dataMgr.UsersInHirarchyTs.Where(x => x.IsDeleted == false
                        && x.ComSerial == serial && x.GroupId == itemGroup.GroupId && x.LeaderId == x.UserId).ToList();
                    }
                    #endregion


                    if (users.Count > 0)
                    {
                        var node = new TreeNode();
                        node.key = itemGroup.GroupId;
                        node.title = itemGroup.GroupName;
                        node.folder = true;
                        node.lazy = false;
                        #region UPDATE
                        node.userRole = -1;
                        #endregion

                        List<TreeNode> ChildNodes = new List<TreeNode>();

                        //select users in groups
                        foreach (var user in users)
                        {
                            var child = new TreeNode();
                            child.key = (int)user.UserId;
                            var profile = dataMgr.UserProfiles.Where(x => x.UserId == user.UserId && x.IsDeleted == false).SingleOrDefault();
                            child.title = profile.UserName;


                            var childSub = dataMgr.UsersInHirarchyTs.Where(x => x.LeaderId == user.UserId && x.LeaderId != x.UserId
                            && x.IsDeleted == false && x.GroupId == user.GroupId && x.ComSerial == user.ComSerial).Count();
                            if (childSub > 0)
                            { child.folder = true; child.lazy = true; }
                            else
                            { child.folder = false; child.lazy = false; }

                            #region UPDATE 21-10-19
                            if (user.IsSelected==true && user.DevId > 0)
                            {
                                child.extraClasses = "1";
                            }
                            else
                            {
                                child.extraClasses = "0";
                            }
                            #endregion

                            #region UPDATE
                            child.userRole = (int)user.UserLevelId;
                            #endregion

                            ChildNodes.Add(child);
                        }
                        node.children = ChildNodes;
                        nodes.Add(node);
                    }
                }
            }

            return new JsonResult { Data = nodes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //used for the usercheck view. lazyload function.
        public JsonResult GetSubTemplateTCheckTree(int Node, int groupId, string serial)
        {
            DAL.DataManager dataMgr = new DAL.DataManager();
            var items = dataMgr.UsersInHirarchyTs.Where(x => x.LeaderId == Node && x.IsDeleted == false 
            && x.GroupId == groupId && x.ComSerial == serial && x.LeaderId != x.UserId).ToList();

            List<TreeNode> nodes = new List<TreeNode>();

            foreach (var item in items)
            {
                var node = new TreeNode();
                node.key = (int)item.UserId;
                var profile = dataMgr.UserProfiles.Where(x => x.UserId == item.UserId && x.IsDeleted == false).SingleOrDefault();
                node.title = profile.UserName;
                var Children = dataMgr.UsersInHirarchyTs.Where(x => x.LeaderId == item.UserId && x.IsDeleted == false && x.LeaderId != x.UserId
                && x.GroupId == groupId && x.ComSerial == serial).Count();

                if (Children > 0)
                { node.folder = true; node.lazy = true; }
                else
                { node.folder = false; node.lazy = false; }


                //node.selected = item.IsSelected;

                #region UPDATE 21-10-19
                if (item.IsSelected==true && item.DevId > 0)
                {
                    node.extraClasses = "1";
                }
                else
                {
                    node.extraClasses = "0";
                }
                #endregion

                #region UPDATE
                node.userRole = (int)item.UserLevelId;
                #endregion
                nodes.Add(node);
            }

            return new JsonResult { Data = nodes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        //---------------------------------------------------------------------------------------------
        //used for index and usercheck views. If user/manager/tl wants to save the check tree.
        #region UPDATE
        [HttpPost]
        public JsonResult SaveTemplateTCheckTree(string serial, int parentGroupId, string selectedIds, bool? isUpdate)
        {
        #endregion
            var result = false;
            // Attempt to register the user
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    #region UPDATE 21-10-19
                    List<NodeValues> n_values = new List<NodeValues>();
                    string[] values = selectedIds.Split(',');
                    foreach (var value in values)
                    {
                        string[] keyvalue = value.Split(':');

                        if (keyvalue[0].IndexOf("G-") == -1)
                        {
                            var n_value = new NodeValues();
                            n_value.group = int.Parse(keyvalue[1]);

                            if (keyvalue[0].IndexOf("M-") == 1)
                            {                               
                                n_value.key = int.Parse(keyvalue[0].Substring(3));                               
                                n_value.extraInt = -1;
                            }
                            else
                            {                              
                                n_value.key = int.Parse(keyvalue[0]);                              
                                n_value.extraInt = 1;                               
                            }

                            n_values.Add(n_value);
                        }
                        
                    }

                    //var map = new List<KeyValuePair<int, int>>();
                    //string[] values = selectedIds.Split(',');
                    //foreach (var value in values)
                    //{
                    //    string[] keyvalue = value.Split(':');


                    //    if (keyvalue[0].IndexOf("G-") == -1)
                    //    {
                    //        int k = int.Parse(keyvalue[0]);
                    //        int v = int.Parse(keyvalue[1]);
                    //        map.Add(new KeyValuePair<int, int>(k, v));
                    //    }
                    //}
                    #endregion

                    DAL.DataManager dataMgr = new DAL.DataManager();
                    #region UPDATE
                    if ((bool)isUpdate == true)
                    {                                                
                       var users = dataMgr.UsersInHirarchyTs.Where(x => x.ComSerial == serial && x.IsDeleted == false)
                       .ToList();
                        foreach (var user in users)
                        {
                            #region UPDATE 21-10-19
                            //var sel = map.Where(x => x.Key == user.UserId && x.Value == user.GroupId).Count();
                            var sel = n_values.Where(x => x.key == user.UserId && x.group == user.GroupId).Count();
                            #endregion

                            if (sel > 0)
                            {
                                var extraId = n_values.Where(x => x.key == user.UserId && x.group == user.GroupId).Single().extraInt;
                                user.IsSelected = true;
                                user.DevId = extraId;
                           }
                            else
                            {
                                user.IsSelected = false;
                                user.DevId = -1;
                            }
                            dataMgr.SaveChanges();
                        }
                     
                        }
                    else
                    {
                        var records = dataMgr.UsersInHirarchyTs.Where(x => x.ComSerial == serial && x.IsDeleted == false)
                       .ToList();
                        foreach (var record in records) //delete when record exists
                        {
                            record.IsActive = false;
                            record.IsDeleted = true;
                            dataMgr.SaveChanges();
                        }
                        //select groups
                        var groups = dataMgr.UserGroups.Where(x => x.IsDeleted == false && x.ParentGroupId == parentGroupId)
                            .ToList();
                        foreach (var group in groups)
                        {
                            var users = dataMgr.UsersInHirarchies.Where(x => x.GroupId == group.GroupId && x.IsDeleted == false)
                                                   .ToList();
                            foreach (var user in users) //copy the tree
                            {
                                var item = new DAL.UsersInHirarchyT();
                                item.LeaderId = user.LeaderId;
                                item.UserId = user.UserId;
                                item.CompanyId = -1;
                                item.IsActive = (bool)user.IsActive;
                                item.IsDeleted = (bool)user.IsDeleted;
                                item.Dated = DateTime.Now;
                                item.DevId = -1;
                                item.BUnitId = -1;
                                item.WorkspaceId = -1;
                                item.ProcessId = -1;
                                item.ProcessTempateId = -1;                               
                                item.UserLevelId = user.UserLevelId;
                                item.ExtraText = "";
                                item.GroupId = user.GroupId;
                                item.ComSerial = serial;

                                #region UPDATE 21-10-19
                                var sel = n_values.Where(x => x.key == user.UserId && x.group == user.GroupId).Count();
                                if (sel > 0)
                                {
                                    var extraId = n_values.Where(x => x.key == user.UserId && x.group == user.GroupId).Single().extraInt;
                                    item.IsSelected = true;
                                    item.DevId = extraId;
                                }
                                else
                                {
                                    item.IsSelected = false;
                                    item.DevId = -1;
                                }
                                #endregion

                                dataMgr.UsersInHirarchyTs.Add(item);
                                dataMgr.SaveChanges();
                            }
                        }
                    }
                    #endregion

                    ViewBag.status = "Success";
                    transaction.Complete();
                    result = true;
                }
                catch (Exception e)
                {
                    transaction.Dispose();
                    ModelState.AddModelError("error", e.Message);
                }
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }

    public class TreeNode
    {
        public int key;
        public string title;
        public bool folder;
        public List<TreeNode> children;
        public bool lazy;
        public string extraClasses;
        #region UPDATE
        public int userRole;
        #endregion
    }
    #region UPDATE
    enum UserType
    {
        Admin = 0,
        User = 1,
        TeamLeader = 2,
        Manager = 3,
    }
    #endregion

    #region UPDATE 21-10-19
    public class NodeValues
    {
        public int key;
        public int group;
        public int extraInt;
    }
    #endregion


}