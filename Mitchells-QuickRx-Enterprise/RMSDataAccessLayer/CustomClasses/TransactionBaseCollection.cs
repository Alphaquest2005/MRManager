using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMSDataAccessLayer
{
   public class SearchItem : ISearchItem 
    {
        public object SearchObject { get; set; }
        public virtual string SearchCriteria
        {
            get;
            set;
        }

       public virtual string DisplayName { get; set; }

       public virtual string Key { get; set; }
    }
}
