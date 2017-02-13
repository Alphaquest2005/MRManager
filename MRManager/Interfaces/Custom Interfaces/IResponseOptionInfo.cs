using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IResponseOptionInfo : IEntityView<IResponseOptions>
    {
        Int32? ResponseId { get;  }
        Int32? PatientResponseId { get; }
        int QuestionResponseTypeId { get; }
        String Value { get; }
        Int32 QuestionId { get; }
        String Description { get; }
        String Type { get; }
        int ResponseNumber { get; }
        int PatientId { get; }
        int PatientVisitId { get; }
    }
}