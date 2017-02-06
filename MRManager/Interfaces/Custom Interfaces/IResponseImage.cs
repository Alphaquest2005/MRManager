using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IResponseImage : IEntity
    {
        Int32 MediaId { get; }
        Int32 PatientResponseId { get; }

        Byte[] Media { get; }
    }
}