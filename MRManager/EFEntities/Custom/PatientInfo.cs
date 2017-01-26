using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public partial class PatientInfo : EntityView<IPatients>, IPatientInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }
        public string BirthCountry { get; set; }
        public string Email { get; set; }
        public int? MediaId { get; set; }
    }
}