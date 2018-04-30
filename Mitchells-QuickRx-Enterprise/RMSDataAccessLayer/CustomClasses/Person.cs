using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMSDataAccessLayer
{
    public partial class Person :  ISearchItem
    {
       
        public string Name
        {
            get { return FirstName.Trim() + " " + LastName.Trim(); }
            
        }

        public string SearchCriteria
        {
            get { return FirstName.Trim() + " " + LastName.Trim(); }
            
        }

        public string DisplayName
        {
            get { return FirstName.Trim() + " " + LastName.Trim(); }
        }

        public string Key
        {
            get { return Id.ToString(); }
        }
    }
}
