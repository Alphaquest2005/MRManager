using System;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IPersonPhoneNumberInfo))]
    public class PhoneNumbersInfo : EntityView<IPersons_Patient>, IPersonPhoneNumberInfo
    {
        public Int32 PersonId { get; set; }
        public String PhoneNumber { get; set; }
        public String Type { get; set; }
    }
}