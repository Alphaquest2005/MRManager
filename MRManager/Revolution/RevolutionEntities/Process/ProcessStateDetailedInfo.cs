using System;
using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class ProcessStateDetailedInfo: IProcessStateDetailedInfo
    {
        public ProcessStateDetailedInfo(string status, string notes)
        {
            Status = status;
            Notes = notes;
        }

        public string Status { get; }
        public string Notes { get; }
    }

}