using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterNut.DataLayer
{
    public partial class Customs_Procedure
    {
        public string DisplayName
        {
            get
            {
                return string.Format("{0}-{1}", Extended_customs_procedure, National_customs_procedure);
            }
        }
    }
}
