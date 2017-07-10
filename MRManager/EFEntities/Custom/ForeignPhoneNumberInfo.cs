using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IForeignPhoneNumberInfo))]
    public class ForeignPhoneNumberInfo : EntityView<IPatients>, IForeignPhoneNumberInfo
    {
        public int PersonId { get; set; }
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _phoneType;

        public string PhoneType
        {
            get { return _phoneType; }
            set
            {
                _phoneType = value;
                OnPropertyChanged();
            }
        }
    }
}