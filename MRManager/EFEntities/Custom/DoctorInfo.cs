using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IDoctorInfo))]
    public class DoctorInfo : EntityView<IPersons_Doctor>, IDoctorInfo
    {
       public string Name { get; set; }
       public string Code { get; set; }
    }
}