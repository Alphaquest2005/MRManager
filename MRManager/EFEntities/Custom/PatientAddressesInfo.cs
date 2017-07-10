using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IPatientAddressesInfo))]
    public class PatientAddressesInfo : EntityView<IPatients>, IPatientAddressesInfo
    {
        public IList<IPersonAddressInfo> Addresses { get; set; }
    }

    [Export(typeof(IPatientForeignAddressesInfo))]
    public class PatientForeignAddressesInfo : EntityView<IPatients>, IPatientForeignAddressesInfo
    {
        public IList<IForeignAddressInfo> Addresses { get; set; }
    }
}