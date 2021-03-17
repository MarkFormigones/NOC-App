using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hydron.Models
{

    public class MenuItem
    {
        public MenuItem()
        {
            this.MenuItem1 = new HashSet<MenuItem>();
        }

        public int MenuItemId { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public Nullable<bool> Selected { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public string Iclass { get; set; }
        public Nullable<bool> HasLink { get; set; }
        public string aClass { get; set; }
        public Nullable<int> rid { get; set; }
        public string vw { get; set; }
       
        public virtual ICollection<MenuItem> MenuItem1 { get; set; }
        public virtual MenuItem MenuItem2 { get; set; }
    }
}