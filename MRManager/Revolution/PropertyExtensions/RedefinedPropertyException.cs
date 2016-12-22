namespace PropertyExtensions
{
    public class RedefinedPropertyException : ExtensionPropertyException
    {
        public RedefinedPropertyException(IExtensionProperty property)
            : base($"Redefined {property.Name}.")
        {
        }
    }
}