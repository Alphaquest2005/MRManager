using System;
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface ISource
    {
        Guid SourceId { get; }
        string SourceName { get; } 
        ISourceType SourceType { get; }
    }
    
    public interface ISourceType
    {
        Type Source_Type { get; }
    }
}