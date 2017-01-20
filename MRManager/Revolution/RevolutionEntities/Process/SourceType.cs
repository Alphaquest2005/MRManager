using System;
using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class SourceType : ISourceType
    {
        public SourceType(Type sourceType)
        {
            Source_Type = sourceType;
        }

        public Type Source_Type { get; }
    }
}