using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Markup;
using System.Xaml;
using System.Xaml.Schema;

namespace XamlGenericTypeExtension
{
	[ContentProperty("TypeArguments")]
	[MarkupExtensionReturnType(typeof(Type))]
	public class GenericTypeExtension : MarkupExtension
	{
		private readonly List<Type> _typeArguments = new List<Type>();
		private string _typeName;
		private Type _type;

		public GenericTypeExtension(string typeName)
		{
			if (string.IsNullOrEmpty(typeName)) throw new ArgumentNullException("typeName");
			_typeName = typeName;
		}

		public GenericTypeExtension(string typeName, Type typeArgument)
			: this(typeName)
		{
			if (typeArgument != null)
			{
				_typeArguments.Add(typeArgument);
			}
		}

		public GenericTypeExtension(string typeName, Type typeArgument1, Type typeArgument2)
			: this(typeName, typeArgument1)
		{
			if (typeArgument2 != null)
			{
				_typeArguments.Add(typeArgument2);
			}
		}

		public GenericTypeExtension(string typeName, Type typeArgument1, Type typeArgument2, Type typeArgument3)
			: this(typeName, typeArgument1, typeArgument2)
		{
			if (typeArgument3 != null)
			{
				_typeArguments.Add(typeArgument3);
			}
		}

		public GenericTypeExtension(string typeName, Type typeArgument1, Type typeArgument2, Type typeArgument3, Type typeArgument4)
			: this(typeName, typeArgument1, typeArgument2, typeArgument3)
		{
			if (typeArgument4 != null)
			{
				_typeArguments.Add(typeArgument4);
			}
		}

		public GenericTypeExtension()
		{
		}

		public string BaseTypeName
		{
			get
			{
				return _typeName;
			}
			set
			{
				if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
				_typeName = value;
				_type = null;
			}
		}

		[TypeConverter(typeof(TypeArgumentsConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public IList TypeArguments
		{
			get
			{
				return _typeArguments;
			}
			set
			{
				_typeArguments.Clear();
				if (null != value && 0 != value.Count)
				{
					_typeArguments.AddRange(value.OfType<Type>());
				}
			}
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null) throw new ArgumentNullException("serviceProvider");

			if (_type == null)
			{
				if (string.IsNullOrEmpty(_typeName))
				{
					throw new InvalidOperationException("No base type name was specified.");
				}

				if (_typeArguments.Count == 0)
				{
					_type = ResolveNonGenericType(serviceProvider, _typeName);
				}
				else
				{
					_type = ResolveGenericType(serviceProvider, _typeName, _typeArguments.ToArray());
				}
			}

			return _type;
		}

		private static Type ResolveNonGenericType(IServiceProvider serviceProvider, string typeName)
		{
			var resolver = GetRequiredService<IXamlTypeResolver>(serviceProvider);
			Type type = resolver.Resolve(typeName);

			if (type == null)
			{
				throw new InvalidOperationException(string.Format("Unable to resolve type '{0}'.", typeName));
			}

			return type;
		}

		private static Type ResolveGenericType(IServiceProvider serviceProvider, string typeName, Type[] typeArguments)
		{
			string namespaceName;
			string[] splitTypeName = typeName.Split(':');
			if (2 == splitTypeName.Length)
			{
				namespaceName = splitTypeName[0];
				typeName = splitTypeName[1];
			}
			else
			{
				namespaceName = string.Empty;
			}

			var resolver = GetRequiredService<IXamlNamespaceResolver>(serviceProvider);
			var schema = GetRequiredService<IXamlSchemaContextProvider>(serviceProvider);

			var xamlNs = resolver.GetNamespace(namespaceName);
			string genericTypeName = string.Format(CultureInfo.InvariantCulture, "{0}`{1:D}", typeName, typeArguments.Length);

			var xamlTypeName = new XamlTypeName(xamlNs, genericTypeName);
			var xamlType = schema.SchemaContext.GetXamlType(xamlTypeName);
			if (xamlType == null)
			{
				throw new InvalidOperationException(string.Format("Unable to resolve type '{0}'.", xamlTypeName));
			}

			Type genericType = xamlType.UnderlyingType;
			if (genericType == null)
			{
				throw new InvalidOperationException(string.Format("Unable to resolve type '{0}'.", xamlTypeName));
			}

			return genericType.MakeGenericType(typeArguments);
		}

		private static T GetRequiredService<T>(IServiceProvider serviceProvider) where T : class
		{
			var result = serviceProvider.GetService(typeof(T)) as T;
			if (null != result) return result;

			throw new InvalidOperationException(string.Format(
				"Markup extension '{0}' requires '{1}' be implemented in the IServiceProvider for ProvideValue.",
				typeof(GenericTypeExtension).Name,
				typeof(T).Name));
		}
	}
}