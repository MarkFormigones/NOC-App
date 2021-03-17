using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Hydron.Models.Definitions
{
    public class ReportModel
    {
        public ReportModel()
        {
            this.PrinterUser = "";
            this.ReportName = "";

            //this.reportlistProcess = new List<Models.Definitions.Vw_ProcessMasterModel>();
            //this.reportlistProjects = new List<Models.Definitions.Vw_ProjectsTModel>();

        }
        public string PrinterUser { get; set; }
        public string ReportName { get; set; }
        public DateTime ReportDate { get; set; }
        //public List<Vw_ProjectsTModel> reportlistProjects;
        //public List<Models.Definitions.Vw_ProcessMasterModel> reportlistProcess { get; set; }
        public string PrinterDivisionName { get; set; }
        public string ReportWorkspaceName { get; set; }
        public string ReportCompanyName { get; set; }
        public string PrinterProjectName { get; set; }
        public string PrinteProcessSerial { get; set; }
        public string PrinterWorkSpaceSerial { get; set; }
        public string ReportProcessName { get; set; }
        public string ReportQuery { get;   set; }
        public string PrinterUnitName { get;   set; }
        public string PrinteUnitSerial { get;   set; }
        public vw_ProcessMasterModel ProcessMaster { get;   set; }
    }
}