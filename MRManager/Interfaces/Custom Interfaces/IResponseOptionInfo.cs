using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public partial interface IResponseOptionInfo : IEntityView<IResponseOptions>
    {
        Int32? ResponseId { get; }
        Int32? PatientResponseId { get; }
        String Value { get; }
        Int32 QuestionId { get; }
        String Description { get; }
        String Type { get; }
        int ResponseNumber { get; }
    }

    [InheritedExport]
    public partial interface IResponseInfo : IEntityView<IResponse>
    {
        int PatientResponseId { get; }
        int ResponseOptionId { get; }
        String Value { get; }
        
    }
}