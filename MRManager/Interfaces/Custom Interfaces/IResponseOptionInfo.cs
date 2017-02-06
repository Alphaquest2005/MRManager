using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IResponseOptionInfo : IEntityView<IResponseOptions>
    {
        Int32? ResponseId { get; set; }
        Int32? PatientResponseId { get; set; }
        String Value { get; set; }
        Int32 QuestionId { get; }
        String Description { get; }
        String Type { get; }
        int ResponseNumber { get; }
        int PatientId { get; }
    }

    
    public partial interface IResponseInfo : IEntityView<IResponse>
    {
        int PatientResponseId { get; }
        int ResponseOptionId { get; }
        String Value { get; }
        
    }
}