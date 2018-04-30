using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace RMSDataAccessLayer
{
    public partial class Cashier
    {
        string _password = "pass";
        public  string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        
    }
}
