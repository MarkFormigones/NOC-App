using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hydron.Models.ActivityLog
{
    //public class ActvityLogModel
    //{
    //    public int Id { get; set; }
    //    public Nullable<int> UserId { get; set; }
    //    public Nullable<int> RecordId { get; set; }
    //    public Nullable<int> Activity_Lok { get; set; }
    //    public Nullable<int> Operation_Lok { get; set; }
    //    public Nullable<int> BUnitId { get; set; }
    //    public Nullable<int> ProcessId { get; set; }
    //    public string Msg { get; set; }
    //    public string ClientIP { get; set; }
    //    public System.DateTime Dated { get; set; }
    //    public Nullable<bool> IsDeleted { get; set; }

        
    //}
     public class ActvityLogtotals{
         public int TAct { get; set; }
         public int TErr { get; set; }

         public int TFil { get; set; }

         public int TExc { get; set; }

         public int Total { get; set; }
     }
    public class ActvityLogViewTotalsModel
    {
        public ActvityLogViewTotalsModel()
        {
            ActivityLogModelList = new List<ActvityLogViewModel>();
            LogTotals = new ActvityLogtotals();
        }
        public List<ActvityLogViewModel> ActivityLogModelList { get;set;}
        public ActvityLogtotals LogTotals { get; set; }
       
    }
    public class ActvityLogViewModel
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RecordId { get; set; }
        public Nullable<int> Activity_Lok { get; set; }
        public Nullable<int> Operation_Lok { get; set; }
        public Nullable<int> BUnitId { get; set; }
        public Nullable<int> ProcessId { get; set; }
        public string Msg { get; set; }
        public string ClientIP { get; set; }
        public System.DateTime Dated { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string UserName { get; set; }
        public string ActvityType { get; set; }
        
    }
}