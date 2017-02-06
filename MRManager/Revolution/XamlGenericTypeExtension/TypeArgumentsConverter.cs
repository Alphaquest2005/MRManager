using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Markup;

namespace XamlGenericTypeExtension
{
	public class TypeArgumentsConverter : TypeConverter
	{
		public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(Type)) return true;
			if (context == null) return false;

			var resolver = context.GetService(typeof(IXamlTypeResolver)) as IXamlTypeResolver;
			return resolver != null;
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value is IList<Type>) return value;

			Type valueType = value as Type;
			if (valueType != null)
			{
				return new List<Type> { valueType };
			}

			string valueString = value as string;
			if (valueString != null && context != null)
			{
				var resolver = context.GetService(typeof(IXamlTypeResolver)) as IXamlTypeResolver;
				if (resolver != null)
				{
					var list = valueString.Split(',').Select(s => resolver.Resolve(s.Trim())).ToList();
					if (list.All(t => t != null)) return list;
				}
			}

			throw GetConvertFromException(value);
		}
	}
}