using System;
using SystemInterfaces;

namespace Interfaces
{
    public partial interface IResponseInfo : IEntityView<IResponse>
    {
        int PatientResponseId { get; }
        int ResponseOptionId { get; }
        String Value { get; }
        int QuestionId { get; }

    }
}