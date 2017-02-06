using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(INextOfKinInfo))]
    public class NextOfKinInfo:EntityView<IPersons_Patient>, INextOfKinInfo
    {
        public int PatientId { get; set; }
        public string Relationship { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? MediaId { get; set; }
        public IList<IPersonAddressInfo> Addresses { get; set; }
        public IList<IPersonPhoneNumberInfo> PhoneNumbers { get; set; }
    }
}
