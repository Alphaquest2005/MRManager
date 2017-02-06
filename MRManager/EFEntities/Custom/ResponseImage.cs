using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IResponseImage))]
    public partial class ResponseImage : BaseEntity, IResponseImage
    {
        public int MediaId { get; set; }
        public int PatientResponseId { get; set; }
        public byte[] Media { get; set; }
    }
}