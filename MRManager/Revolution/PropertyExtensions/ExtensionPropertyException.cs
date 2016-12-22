using System;

namespace PropertyExtensions
{
    public abstract class ExtensionPropertyException : Exception
    {
        protected ExtensionPropertyException(string message)
            : base(message)
        {
        }
    }
}