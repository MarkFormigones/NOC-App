using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hydron.Models.Definitions
{ 
    public class TreeModel
    {

        public   TreeModel()
        { ChildernList = new List<TreeModel>(); }
        public string name { get; set; }
        public enum TreeSTYLE { STYLE1, STYLE2, STYLE3, STYLE4, ALLSTYLES }
        public string controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public string LogoUrl { get; set; }
        public string CountryIdflag { get; set; }
        public int Id { get; set; }
        public string type { get; set; }
        public int Style { get; set; }
        public bool IsActive { get; set; }
        public DateTime Dated { get; set; }
        public string additionalParameters { get; set; }
        public List<TreeModel> ChildernList { get; set; }

    }
}