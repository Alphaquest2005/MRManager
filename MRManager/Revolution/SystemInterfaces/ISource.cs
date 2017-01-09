using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ISource
    {
        Guid SourceId { get; }
        string SourceName { get; } 
        ISourceType SourceType { get; }
    }
    [InheritedExport]
    public interface ISourceType
    {
        Type Source_Type { get; }
    }
}