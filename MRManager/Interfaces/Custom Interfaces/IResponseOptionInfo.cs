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
    }
}