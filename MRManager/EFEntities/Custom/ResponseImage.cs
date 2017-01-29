using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public partial class ResponseImage : BaseEntity, IResponseImage
    {
        public int MediaId { get; set; }
        public int PatientResponseId { get; set; }
        public byte[] Media { get; set; }
    }
}