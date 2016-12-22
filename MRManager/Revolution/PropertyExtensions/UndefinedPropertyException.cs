namespace PropertyExtensions
{
    public class UndefinedPropertyException : ExtensionPropertyException
    {
        public UndefinedPropertyException(IExtensionProperty property)
            : base($"Undefined {property.Name}.")
        {
        }
    }
}