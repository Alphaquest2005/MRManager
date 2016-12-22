using System;
using System.Runtime.CompilerServices;

namespace PropertyExtensions
{
    public static class ExtensionProperty
    {
        static ConditionalWeakTable<object, ValueStore> Table { get; } =
            new ConditionalWeakTable<object, ValueStore>();

        static ValueStore Store(object context) =>
            Table.GetOrCreateValue(context);

        public static T Add<T>(
            this object context,
            ExtensionProperty<T> property,
            T value) =>
                Store(context)
                    .Add(property, value);

        public static T Get<T>(
            this object context,
            ExtensionProperty<T> property) =>
                Store(context)
                    .Get(property);

        public static T GetOrAdd<T>(
            this object context,
            ExtensionProperty<T> property,
            Func<T> valueFactory) =>
                Store(context)
                    .GetOrAdd(property, valueFactory);
    }

    public class ExtensionProperty<T> : IExtensionProperty
    {
        public ExtensionProperty([CallerMemberName] string name = null)
        {
            Name = name ?? "UnknownProperty";
        }

        public string Name { get; }
        public override string ToString() => Name;
    }
}