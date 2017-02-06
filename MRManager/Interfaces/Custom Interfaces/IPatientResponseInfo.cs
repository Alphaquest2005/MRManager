using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IPatientResponseInfo : IEntityView<IPatientResponses>
    {
        Int32 PatientVisitId { get; }
        Int32 QuestionId { get; }
        Int32 InterviewId { get; }
        Int32 PatientSyntomId { get; }
        String Question { get; }
        String Interview { get; }
        String Category { get; }

        IList<IResponseOptionInfo> ResponseOptions { get; }
        IList<IResponseImage> ResponseImages { get; }
        int PatientId { get; }
    }
}